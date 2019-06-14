using HelloParent.Entities.Enums;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HelloParent.Entities.Model
{
    [BsonIgnoreExtraElements]
    public class ApplicationUser : IdentityUser, ITrackable
    {
        public ApplicationUser()
        {
            Otps = new Collection<Otp>();
            GcmIds = new Collection<string>();
            ApnsIds = new Collection<string>();
            EngagementInstances = new Collection<EngagementInstance>();
        }

        public ObjectId SchoolId { get; set; }

        public string CreatedById { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager,
        //    string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

        public UserRights Rights { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Otp> Otps { get; set; }
        public string Name { get; set; }
        public ICollection<string> GcmIds { get; set; }
        public ICollection<string> ApnsIds { get; set; }
        public string Image { get; set; }
        public DateTime? DOB { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public DateTime? Anniversary { get; set; }
        public string Designation { get; set; }
        public Boolean check { get; set; }
        public Boolean Gender { get; set; }
        public String SpouseName { get; set; }
        public String SpouseMobile { get; set; }
        public Children[] Children { get; set; }


        public Collection<EngagementInstance> EngagementInstances { get; set; }

        public string About { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public DateTime? LastTimelineDate { get; set; }
        public DateTime? LastForumDate { get; set; }
        public UserType Type { get; set; }
        public string Country { get; set; }
        public string Signature { get; set; }

        public bool IsAppleAppUser()
        {
            // return true;
            return ApnsIds != null && ApnsIds.Any() && !GcmIds.Any();
        }
    }

    public class EngagementInstance
    {
        public ObjectId EngagementId { get; set; }

        public DateTime Date { get; set; }

        public EngagementType EngagementType { get; set; }
    }


    public class Children
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public string SchoolName { get; set; }
    }

    public class ApplicationUserLoaded : ApplicationUser
    {
        public School[] SchoolArray { get; set; }

        public School School
        {
            get
            {
                if (SchoolArray == null)
                {
                    return null;
                }
                return SchoolArray.FirstOrDefault();
            }
        }
    }

    public class Otp
    {
        public DateTime ExpiryDate { get; set; }
        public string Value { get; set; }

        public bool IsValid
        {
            get { return ExpiryDate > DateTime.Now; }
        }
    }

    public class RoleNames
    {
        public const string SuperAdmin = "superadmin";
        public const string SchoolAdmin = "schooladmin";
        public const string School = "school";
        public const string Parent = "parent";
        public const string Admin = "admin";
        public const string HpUser = "hpuser";
    }

    public class UserRights
    {
        public UserRights()
        {
            this.CanReceiveAdminMessages = false;
        }

        //        Student Rules
        public bool CanManageStudent { get; set; }
        public bool CanViewStudent { get; set; }


        //        Class Rules
        public bool CanManageClass { get; set; }

        //        User Rules
        public bool CanModifyUsers { get; set; }

        //        Communication Rules
        public bool CanSeeUserMessage { get; set; }
        public bool CanReceiveAdminMessages { get; set; }
        public bool CanSendMessage { get; set; }
        public bool CanCreateEvents { get; set; }
        public bool CanViewReadReport { get; set; }

        //        Fees Rules
        public bool CanManageFee { get; set; }

        //        Lead Management Rules
        public bool CanManageLead { get; set; }

        //        Attendance Rules
        public bool CanManageAttendance { get; set; }
        //       School Web Page Rules
        public bool CanManageSchoolPage { get; set; }

        //      Album Rules
        public bool CanManageAlbum { get; set; }
        public bool CanEditApprovedFee { get; set; }
    }
}
