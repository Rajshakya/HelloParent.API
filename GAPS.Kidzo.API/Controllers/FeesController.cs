using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using GAPS.Kidzo.API.Views;
using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.Utilities.Constants;
using HelloParent.Utilities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace GAPS.Kidzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly IMapperService _mapperService;
        private readonly IFeeService _feeService;
        private readonly ISchoolService _schoolService;
        private readonly ISchoolClassService _schoolClassService;
        private readonly IStudentService _studentService;
        public FeesController(IFeeService feeService, ISchoolService schoolService, ISchoolClassService schoolClassService,
            IStudentService studentService, IMapperService mapperService)
        {
            this._feeService = feeService;
            this._schoolService = schoolService;
            this._schoolClassService = schoolClassService;
            this._studentService = studentService;
            this._mapperService = mapperService;
        }
        [HttpPost("getfees")]
        public async Task<ActionResult> GetFees([FromBody] FeeFilterView model)
        {
            try
            {
                FeeStudentModelView FeeStudentView = new FeeStudentModelView();
                //Get School //
                var school = await _schoolService.GetSchoolById(Constants.TestingSchool_Id);
                if (school == null)
                {
                    return BadRequest("School not valid");
                }

                /// Get Active session from school
                var sessionToUse = school.GetActiveSession();

                /// Update Current Session if user choose other session from filter
                if (model.SessionId.HasSomething())
                {
                    sessionToUse = school.Sessions.FirstOrDefault(x => x.Id == model.SessionId.AsObjectId());
                }
                if (sessionToUse != null)
                {
                    model.ActiveSessionName = sessionToUse.Name;
                    var allClassesForSchool =
                        await
                            _schoolClassService.Get(x => x.SchoolId == school.Id && x.SessionId == sessionToUse.Id && x.DeactivateDate == null);
                    var activeClassIds = allClassesForSchool.Select(x => x.Id);

                    var student =
                        await
                            _studentService.Get(
                                x => activeClassIds.Contains(x.ClassId) && x.DeactivateDate == null && x.SchoolId == school.Id);

                    if (!school.FeeCycles.Any())
                    {
                        return BadRequest("School has no feecylces");
                    }
                    IEnumerable<Student> allstudent;
                    if (model.ClassId != null)
                    {
                        allstudent = student.Where(x => x.ClassId == model.ClassId.AsObjectId()).ToList();

                    }
                    else
                    {
                        allstudent = student;
                    }
                    if (model.StudentId != null)
                    {
                        allstudent = allstudent.Where(x => x.Id == model.StudentId.AsObjectId());
                    }
                    if (model.StudentFeeFrequency != null && !model.StudentFeeFrequency.Contains("All"))
                    {
                        allstudent =
                            allstudent.Where(x => x.FeeFrequency.ToString() == model.StudentFeeFrequency);
                    }
                    var allstudentIds = allstudent.Select(x => x.Id);

                    var allCalculatedFee = new List<Fee>();

                    if (model.FeeCycleId != null)
                    {
                        allCalculatedFee =
                            await
                                _feeService.Get(
                                    x =>
                                        allstudentIds.Contains(x.StudentId) &&
                                        x.FeeCycleId == model.FeeCycleId.AsObjectId());
                    }
                    else
                    {
                        var feeCycleIdsforSchool =
                            school.FeeCycles.Where(x => x.SessionId == sessionToUse.Id).Select(x => x.Id);
                        allCalculatedFee =
                            await
                                _feeService.Get(
                                    x =>
                                        allstudentIds.Contains(x.StudentId) &&
                                        feeCycleIdsforSchool.Contains(x.FeeCycleId));
                    }

                    var students = student.Where(x => allstudentIds.Contains(x.Id));
                    var schoolComponents = school.SchoolFeeComponents;
                    var allfeeCycle = school.FeeCycles;
                    if (model.FeeStatus != null && !model.FeeStatus.Contains("All"))
                    {
                        allCalculatedFee =
                            allCalculatedFee.Where(x => model.FeeStatus.Contains(x.FeeStatus.ToString())).ToList();
                    }

                    var sortedFees =
                        allCalculatedFee.OrderByDescending(x => x.CreatedAt)
                            .ThenBy(x => x.FeeStatus == FeeStatus.PendingApproval);
                    var classDict = allClassesForSchool.OrderBy(x => x.Name)
                   .ToDictionary(x => x.Id.ToString(), x => x.Name);
                    foreach (var fee in sortedFees)
                    {
                        var singleFeeModel = new FeeStudentModel
                        {
                            StudentId = fee.StudentId.ToString(),
                            FeeId = fee.Id.ToString(),
                            FeeStatus = EnumHelper.DisplayName(typeof(FeeStatus), fee.FeeStatus.ToString()),
                            FeeStatusEnum = fee.FeeStatus,
                            Remark = fee.Remark,
                            CreateAt = fee.CreatedAt,
                            Checked = true
                        };

                        var singleStudent = students.FirstOrDefault(x => x.Id == fee.StudentId);
                        singleFeeModel.ClassName = allClassesForSchool.First(x => x.Id == singleStudent.ClassId).Name;
                        var feeCycleForFee = allfeeCycle.FirstOrDefault(x => x.Id == fee.FeeCycleId);
                        var totalPayable = Math.Round(fee.GetPendingFee(DateTime.Now, feeCycleForFee, school, singleStudent.JoiningDate), 2);
                        singleFeeModel.TotalPayable = totalPayable > 0
                            ? Math.Round(fee.GetPendingFee(DateTime.Now, feeCycleForFee, school, singleStudent.JoiningDate), 2)
                            : 0;

                        singleFeeModel.TotalPaid = fee.Transactions.Sum(x => x.Amount);
                        singleFeeModel.FeeCycle = _mapperService.MapFeeCycleToFeeCycleSingleModel(feeCycleForFee);

                        //var singleStudent = students.FirstOrDefault(x => x.Id == fee.StudentId);
                        if (singleStudent != null)
                        {
                            singleFeeModel.StudentName = singleStudent.Name;
                            singleFeeModel.AdmNumber = singleStudent.Identifier;
                            singleFeeModel.StudentMotherName = singleStudent.MotherName;
                            singleFeeModel.StudentClass = classDict.ContainsKey(singleStudent.ClassId.ToString())
                                ? classDict[singleStudent.ClassId.ToString()]
                                : string.Empty;
                            singleFeeModel.StudentFeeFrequency = EnumHelper.DisplayName(
                                typeof(StudentFeeFrequency), singleStudent.FeeFrequency.ToString());
                        }

                        if (Convert.ToDateTime(singleStudent.JoiningDate).ToLocalTime().Date > feeCycleForFee.LastDueDate.ToLocalTime().Date)
                        {
                            singleFeeModel.LateFee = Math.Round(fee.GetTotalLateFeesOfLateJoinedStudent(school, DateTime.Today, feeCycleForFee, Convert.ToDateTime(singleStudent.JoiningDate).ToLocalTime().Date), 2);
                        }
                        else
                        {
                            singleFeeModel.LateFee = Math.Round(fee.GetTotalLateFees(school, DateTime.Today, feeCycleForFee), 2);
                        }

                        //singleFeeModel.LateFee = fee.GetTotalLateFees(school, DateTime.Today, feeCycleForFee);


                        if (fee.ApprovedBy != null || fee.CancelledBy != null)
                        {
                            singleFeeModel.ApprovedById = fee.ApprovedBy;
                            singleFeeModel.CancelledById = fee.CancelledBy;
                        }
                        else
                        {
                            model.ToCheckUnapprovedStudents = model.ToCheckUnapprovedStudents + 1;
                        }


                        foreach (var component in fee.Components)
                        {
                            var singleComponentModel = new FeeModel
                            {
                                ComponentValue = component.Value,
                                SchoolFeeComponentId = component.ComponetId.ToString()
                            };

                            singleFeeModel.TotalFeeForStudent = Math.Round(singleFeeModel.TotalFeeForStudent +
                                                                           singleComponentModel.ComponentValue,
                                2);

                            var singlecomponent =
                                schoolComponents.FirstOrDefault(x => x.Id == component.ComponetId);
                            if (singlecomponent != null)
                            {
                                singleComponentModel.ComponentName = singlecomponent.Name;
                            }
                            singleFeeModel.StudentAllFee = singleFeeModel.StudentAllFee != null
                                ? singleFeeModel.StudentAllFee.Concat(new[] { singleComponentModel }).ToArray()
                                : new[] { singleComponentModel };

                            singleFeeModel.ComponetDict = singleFeeModel.StudentAllFee.GroupBy(
                                x => x.SchoolFeeComponentId.ToString()).ToDictionary(
                                    x => x.Key, x => x.First().ComponentValue);
                        }
                        FeeStudentView.FeeStudents = FeeStudentView.FeeStudents != null
                        ? FeeStudentView.FeeStudents.Concat(new[] { singleFeeModel }).ToArray()
                        : new[] { singleFeeModel };
                    }
                    FeeStudentView.Maxdata =
                  FeeStudentView.FeeStudents.OrderByDescending(x => x.StudentAllFee.Length).FirstOrDefault();
                    if (FeeStudentView.Maxdata != null)
                    {
                        FeeStudentView.FeeComponentWithDictinctValueDict =
                            FeeStudentView.Maxdata.StudentAllFee.GroupBy(x => x.SchoolFeeComponentId)
                                .ToDictionary(x => x.Key,
                                    x => x.First().ComponentValue
                                );
                    }
                }
                FeeStudentView.FeeStudents =
               FeeStudentView.FeeStudents.OrderBy(x => x.FeeCycle.LastDueDate).ThenBy(x => x.StudentName).ToArray();
                return Ok(FeeStudentView);


            }
            catch (ArgumentNullException argNullEx)
            {
                return BadRequest(argNullEx.Message);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


        [HttpPost("approveselectedfee")]
        public async Task<ActionResult> ApproveSelectedFee(FeeApproveModel feeModel)
        {

            if (feeModel.FeeIds.Any())
            {
                var msg = "";
                var Testing_UserId = Constants.TestingUser_Id;
                // Raj    var user = await _userService.GetById(User.Identity.GetUserId());
                var user = new ApplicationUser();
                var ids = feeModel.FeeIds.Select(x => x.AsObjectId());

                //var allCalculatedFee =
                //    await _feeService.Get(x => ids.Contains(x.Id));
                // var notificationStudentList = new List<ObjectId>();
                //if (allCalculatedFee.Any()) {
                //    var singleFee = allCalculatedFee.FirstOrDefault();
                //if (singleFee != null) {
                //var school = await _schoolService.GetById(singleFee.SchoolId);
                //var feeCycles = school.FeeCycles;
                //  foreach (var fee in allCalculatedFee) {
                if (feeModel.Command == "Approve fees")
                {
                    await
                        _feeService.UpdateFeeStatus(ids, feeModel.IsNotification, FeeStatus.Approved,
                            user);
                    //fee.ApprovedDate = DateTime.Now;
                    //fee.ApprovedBy = User.Identity.GetUserId();

                    //fee.FeeStatus = FeeStatus.Approved;

                    //if (feeModel.IsNotification) {
                    //    notificationStudentList.Add(fee.StudentId);
                    //}


                    msg = "Fees have been approved successfully";
                }
                else
                {
                    if (feeModel.Command == "Cancel fees")
                    {
                        await
                            _feeService.UpdateFeeStatus(ids, feeModel.IsNotification, FeeStatus.Cancelled,
                                user);
                        //fee.CancelledDate = DateTime.Now;
                        //fee.CancelledBy = User.Identity.GetUserId();
                        //fee.Remark = feeModel.ReasonToSend;
                        //fee.FeeStatus = FeeStatus.Cancelled;
                        msg = "Fees have been cancelled successfully";
                    }
                }
                //await _feeService.Update(fee);
                // }

                // }
                // }
                // TempData["SuccessMessage"] = msg;  Raj
            }
            return Ok();
        }

        [HttpGet("getsessionname/{sessionId}")]
        public async Task<IActionResult> GetSessionName(string sessionId)
        {
            try
            {
                var school = await _schoolService.GetSchoolById(Constants.TestingSchool_Id);
                if (school == null)
                {
                    return BadRequest("School not valid");
                }

                CalculateFeeModel model = new CalculateFeeModel();

                var sessionToUse = school.GetActiveSession();
                if (sessionId.HasSomething())
                {
                    sessionToUse = school.Sessions.FirstOrDefault(x => x.Id == sessionId.AsObjectId());
                }
                model.SessionDict = school.Sessions.ToDictionary(x => x.Id.ToString(), x => x.Name);
                model.SessionId = sessionToUse.Id.ToString();

                var feeCycles = school.FeeCycles;

                model.FeeCycles =
                feeCycles.Where(x => x.SessionId == sessionToUse.Id)
                    .OrderByDescending(x => x.CreatedAt)
                    .ThenBy(x => x.StartDate)
                    .Select(_mapperService.MapFeeCycleToFeeCycleSingleModel)
                    .ToList();
                model.SessionId = sessionToUse.Id.ToString();

                if (model.FeeCycles.Any())
                {
                    var feeCycleSingleModel = model.FeeCycles.FirstOrDefault();
                    if (feeCycleSingleModel != null)
                    {
                        var selectedId = sessionId ?? feeCycleSingleModel.Id.ToString();

                        var cycle = school.FeeCycles.FirstOrDefault(x => x.SessionId == selectedId.AsObjectId());
                        model.FeeCycle = _mapperService.MapFeeCycleToFeeCycleSingleModel(cycle);
                        if (model.FeeCycle != null)
                        {
                            model.FeeCycle.IdString = model.FeeCycle.Id.ToString();
                        }
                    }
                    if (model.FeeCycle != null)
                    {
                        model.FeeCycleId = model.FeeCycle.Id.ToString();
                    }
                }

                model.SchoolId = Constants.TestingSchool_Id.ToString();
                var schoolId = Constants.TestingSchool_Id;

                var allClassesForSchool =
                    await
                        _schoolClassService.Get(
                            x =>
                                x.SessionId == model.FeeCycle.SessionId && x.DeactivateDate == null &&
                                x.SchoolId == schoolId.AsObjectId());
                var classDict = allClassesForSchool.OrderBy(x => x.Name)
                    .ToDictionary(x => x.Id.ToString(), x => x.Name);
                model.ClassDict.Add("All", "All");
                classDict.ToList().ForEach(x => model.ClassDict[x.Key] = x.Value);
                var now = DateTime.Now;
                var stdate = new DateTime(now.Year, now.Month, 1);
                var endDate = stdate.AddMonths(1).AddDays(-1);
                var dtModel = new FeeCycleSingleModel
                {
                    StartDate = stdate,
                    EndDate = endDate
                };
                // model.FeeCycleForPopup = dtModel;

                model.FeeCycles = model.FeeCycles.OrderBy(x => x.StartDate);

                return Ok(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("updatefeecycle")]
        public async Task<ActionResult> UpdateFeeCycle([FromBody] FeeCycleView model)
        {
            //var rights = GetMyRights();
            //if (rights == null)
            //{
            //    return RedirectToAction("LogOff", "Account");
            //}
            //if (!rights.CanManageFee)
            //{
            //    return RedirectToAction("Index", "Messages");
            //}
            var school = await _schoolService.GetSchoolById(Constants.TestingSchool_Id);

            var feeCycleForSession = school.FeeCycles.FirstOrDefault(x => x.SessionId == model.SessionId.AsObjectId() && x.Name.ToLower().Trim() == model.Name.ToLower().Trim());
            if (feeCycleForSession != null && model.Id != feeCycleForSession.Id.ToString())
            {

                return BadRequest("Invalid fee cycle");
            }

            var feeCycle = school.FeeCycles.FirstOrDefault(x => x.Id == model.Id.AsObjectId());

            if (feeCycle != null)
            {
                feeCycle.Name = model.Name.Trim();
                feeCycle.StartDate = feeCycle.StartDate;
                feeCycle.EndDate = feeCycle.EndDate;
                feeCycle.LastDueDate = model.LastDueDate;
                feeCycle.LateFee = model.LateFee;
                school.FeeCycles[Array.IndexOf(school.FeeCycles, feeCycle)] = feeCycle;
            }
            await _schoolService.Update(school);
            
            return Ok(feeCycle);
        }

        [HttpPost("CalCulateFeeForClass")]
        public async Task<ActionResult> CalCulateFeeForClass(FeeCycleDashboardModel model)
        {
            //var rights = GetMyRights();
            //if (rights == null)
            //{
            //    return RedirectToAction("LogOff", "Account");
            //}
            //if (!rights.CanManageFee)
            //{
            //    return RedirectToAction("Index", "Messages");
            //}
            var school = await _schoolService.GetSchoolById(Constants.TestingSchool_Id);
            var activeSessions = school.GetActiveSession();


            var cycle = school.FeeCycles.FirstOrDefault(x => x.Id == model.FeeCycleId.AsObjectId());
            var students = new List<Student>();
            if (model.ClassId != "All")
            {
                students =
                    await
                        _studentService.Get(
                            x => x.ClassId == model.ClassId.AsObjectId() && x.DeactivateDate == null);
            }
            else
            {
                if (model.ClassId == "All")
                {
                    var allclassForSession =
                        await
                            _schoolClassService.Get(
                                x =>
                                    x.SessionId == activeSessions.Id && x.DeactivateDate == null &&
                                    x.SchoolId == Constants.TestingSchool_Id.AsObjectId());
                    var clssIds = allclassForSession.Select(x => x.Id);
                    students =
                        await _studentService.Get(x => clssIds.Contains(x.ClassId) && x.DeactivateDate == null);
                }
            }
            if (students.Count == 0)
            {
                return BadRequest("The students are not avilable in the selected class");
            }
            var count = 0;
            var existingcount = 0;
            foreach (var student in students)
            {
                var issuccessfullyCalculate = await _feeService.GenerateFee(student, cycle, school);
                if (issuccessfullyCalculate != null)
                {
                    if (issuccessfullyCalculate.FeeStatus == FeeStatus.PendingApproval)
                    {
                        count = count + 1;
                    }
                    else
                    {
                        existingcount = existingcount + 1;
                    }
                }
            }

            if (count == 0 && existingcount == 0)
            {
                return BadRequest("Please create fee components for the respective class before calculating fee");
            }
            if (existingcount > 0 && count == 0)
            {
                return BadRequest("The fee has been already calculated for this class");
            }
            return BadRequest("Fee for {0} students has been calculated successfully");
        }



    }
}