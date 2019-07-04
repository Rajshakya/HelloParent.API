using HelloParent.Entities.Enums;
using HelloParent.Entities.Model;
using HelloParent.IServices;
using HelloParent.MongoBase.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HelloParent.Services
{
   public class FeeService : MongoBaseService<Fee>, IFeeService
    {
        private readonly ISchoolService _schoolService;
        private readonly IStudentService _studentService;
        public FeeService(IRepository<Fee> repository, ISchoolService schoolService,IStudentService studentService) : base(repository)
        {
            this._schoolService = schoolService;
            this._studentService = studentService;
        }

        public Task CreateAndSendInvoice(bool sendNotification, Transaction transaction, School school, ApplicationUser userWithFeeRight)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTransactionFromFees(ObjectId feeId, ObjectId transactionId, School school)
        {
            throw new NotImplementedException();
        }

        public Task<Fee> GenerateFee(Student student, FeeCycle cycle, School school)
        {
            throw new NotImplementedException();
        }

        public Task GenerateFeeCycles(ObjectId sessionId, ObjectId schoolId)
        {
            throw new NotImplementedException();
        }

        public string GenerateInvoicePdf(ObjectId transactionId)
        {
            throw new NotImplementedException();
        }

        public string GeneratePdf(ObjectId transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<Fee> GetById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fee>> GetFeeByExpression(IEnumerable<Expression<Func<Fee, bool>>> filter)
        {
           return await Get(filter);
        }

        public async Task UpdateFeeStatus(IEnumerable<ObjectId> feeIds, bool toNotify, FeeStatus toUpdate, ApplicationUser user, string remark = null)
        {
            var allCalculatedFee =
                 await Get(x => feeIds.Contains(x.Id));
            var notificationStudentList = new List<ObjectId>();
            if (allCalculatedFee.Any())
            {
                var singleFee = allCalculatedFee.FirstOrDefault();
                if (singleFee != null)
                {
                    var school = await _schoolService.GetSchoolById(singleFee.SchoolId.ToString());
                    var feeCycles = school.FeeCycles;
                    foreach (var fee in allCalculatedFee)
                    {
                        if (toUpdate == FeeStatus.Approved)
                        {
                            fee.ApprovedDate = DateTime.Now;
                            fee.ApprovedBy = user.Id;

                            fee.FeeStatus = FeeStatus.Approved;

                            if (toNotify)
                            {
                                notificationStudentList.Add(fee.StudentId);
                            }
                        }
                        else
                        {
                            if (toUpdate == FeeStatus.Cancelled)
                            {
                                fee.CancelledDate = DateTime.Now;
                                fee.CancelledBy = user.Id;
                                fee.Remark = remark;
                                fee.FeeStatus = FeeStatus.Cancelled;
                            }
                        }
                        await Update(fee);
                    }
                    //if (toNotify)
                    //{
                    //    var users =
                    //        await
                    //            _userService.Get(
                    //                x =>
                    //                    x.Rights.CanManageFee && x.SchoolId == singleFee.SchoolId &&
                    //                    x.DeactivateDate == null);
                    //    var single = users.FirstOrDefault(x => !x.Roles.Contains(RoleNames.SchoolAdmin));
                    //    var distinctStudent = notificationStudentList.Distinct();
                    //    if (single == null)
                    //    {
                    //        single = users.FirstOrDefault();
                    //    }
                    //    foreach (var student in distinctStudent)
                    //    {
                    //        var feesForStudent =
                    //            allCalculatedFee.Where(x => x.StudentId == student).ToList();
                    //        if (feesForStudent.Any())
                    //        {
                    //            var listofCycles = new List<FeeCycle>();
                    //            var amount = 0.0;

                    //            var singleStudent = await _studentService.Get(x => x.Id == student);
                    //            var firstStudent = singleStudent.FirstOrDefault();


                    //            foreach (var fee in feesForStudent)
                    //            {
                    //                var cycle = feeCycles.FirstOrDefault(x => x.Id == fee.FeeCycleId);
                    //                listofCycles.Add(cycle);
                    //                var pending = fee.GetPendingFee(DateTime.Now, cycle, school, firstStudent.JoiningDate);
                    //                amount = amount + pending;
                    //            }
                    //            var names = listofCycles.Select(x => x.Name);
                    //            var onlyNameOfCycle = string.Join(",", names);

                    //            var message = new Message
                    //            {
                    //                SchoolId = single.SchoolId,
                    //                MessageType = MessageType.SentToGroup,
                    //                ClassIds = new Collection<ObjectId>(),
                    //                GroupIds = new Collection<ObjectId>(),
                    //                StudentIds = new Collection<ObjectId> { student },
                    //                Subject = string.Format("Fees for {0}", onlyNameOfCycle),
                    //                Body =
                    //                    string.Format(
                    //                        "Dear Parent <br><br> The fees for period {0} has been generated. <br> The total fees to be paid is Rs {1}/-.<br>Remarks: None  <br><br> Regards<br>{2} ",
                    //                        onlyNameOfCycle, amount,
                    //                        single.Name),
                    //                SenderId = single.Id.AsObjectId(),
                    //                UserIds = new Collection<string>(),
                    //                Attachments = new Collection<Attachement>()
                    //            };
                    //            if (!singleFee.Remark.IsNullOrEmpty())
                    //            {
                    //                message.Body = string.Format(
                    //                    "Dear Parent <br><br> The fees for period {0} has been generated. <br> The total fees to be paid is Rs {1}/-.<br>Remarks: {2}  <br><br> Regards<br> {3} ",
                    //                    onlyNameOfCycle, amount, singleFee.Remark,
                    //                    single.Name);
                    //            }
                    //            await _unitOfWork.MessageRepository.AddAsync(message);

                    //            Task.Run(
                    //                () =>
                    //                    ProcessHelper.ProcessMessage(message, _messageService, _userService,
                    //                        _smsService, _schoolService, _smsLogService));
                    //        }
                    //    }
                    //}
                }
            }
        }

        public Task UpdateLateFeesIfChanged(ObjectId feeId, double amount, DateTime date)
        {
            throw new NotImplementedException();
        }

        public  async Task UpdateStudentFee(IDictionary<ObjectId, double> components, ObjectId feeId, School school, FeeStatus feeStatus, string remark = null)
        {
            var feeCollections = await Get(x => x.Id == feeId);
            var fee = feeCollections.FirstOrDefault();
            var singleStudent = await _studentService.Get(x => x.Id == fee.StudentId);
            var firstStudent = singleStudent.FirstOrDefault();

            if (fee != null)
            {
                fee.Components = components.Select(comp => new Component
                {
                    ComponetId = comp.Key,
                    Value = comp.Value
                }).ToArray();
                if (fee.FeeStatus == FeeStatus.PartialPaid)
                {
                    var pendingFees = fee.GetPendingFee(DateTime.Today,
                        school.FeeCycles.First(x => x.Id == fee.FeeCycleId),
                        school, firstStudent.JoiningDate);

                    if (Math.Abs(pendingFees) < 0.02)
                    {
                        fee.FeeStatus = FeeStatus.Paid;
                    }
                }
                if (feeStatus == FeeStatus.Paid)
                {
                    fee.FeeStatus = FeeStatus.PartialPaid;
                    if (fee.LateFee != null)
                    {
                        fee.LateFee = 0;
                    }
                }
                fee.Remark = remark;
                await Update(fee);
            }
        }
    }
}
