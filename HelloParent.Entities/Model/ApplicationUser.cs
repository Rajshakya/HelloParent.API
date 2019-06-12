using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.Model
{
    //[BsonIgnoreExtraElements]
    //public class ApplicationUser : IdentityUser, ITrackable
    //{
    //    public ApplicationUser()
    //    {
    //        Otps = new Collection<Otp>();
    //        GcmIds = new Collection<string>();
    //        ApnsIds = new Collection<string>();
    //        EngagementInstances = new Collection<EngagementInstance>();
    //    }

    //    public ObjectId SchoolId { get; set; }

    //    public string CreatedById { get; set; }

    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager,
    //        string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }

    //    public UserRights Rights { get; set; }

    //    public DateTime CreatedAt { get; set; }
    //    public DateTime? UpdatedAt { get; set; }
    //    public DateTime? DeletedAt { get; set; }

    //    public ICollection<Otp> Otps { get; set; }
    //    public string Name { get; set; }
    //    public ICollection<string> GcmIds { get; set; }
    //    public ICollection<string> ApnsIds { get; set; }
    //    public string Image { get; set; }
    //    public DateTime? DOB { get; set; }
    //    public string Occupation { get; set; }
    //    public string Address { get; set; }
    //    public DateTime? Anniversary { get; set; }
    //    public string Designation { get; set; }
    //    public Boolean check { get; set; }
    //    public Boolean Gender { get; set; }
    //    public String SpouseName { get; set; }
    //    public String SpouseMobile { get; set; }
    //    public Children[] Children { get; set; }


    //    public Collection<EngagementInstance> EngagementInstances { get; set; }

    //    public string About { get; set; }
    //    public DateTime? DeactivateDate { get; set; }
    //    public DateTime? LastTimelineDate { get; set; }
    //    public DateTime? LastForumDate { get; set; }
    //    public UserType Type { get; set; }
    //    public string Country { get; set; }
    //    public string Signature { get; set; }

    //    public bool IsAppleAppUser()
    //    {
    //        // return true;
    //        return ApnsIds != null && ApnsIds.Any() && !GcmIds.Any();
    //    }
    //}
}
