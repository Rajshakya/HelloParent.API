
using HelloParent.Entities.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HelloParent.Entities.Model
{
    public class School : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<string> SuperAdmins { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public ParentInvite[] ParentInvites { get; set; }
        public string About { get; set; }
        public string PaymentGatewayKey { get; set; }

        public ICollection<string> MonthlyReports { get; set; }

        public School()
        {
            SuperAdmins = new Collection<string>();
            ParentInvites = new ParentInvite[] { };
            Testimonials = new Testimonial[] { };
            Teams = new Team[] { };
            Blogs = new Collection<ObjectId>();
            SchoolFeeComponents = new SchoolFeeComponent[] { };
            FeeCycles = new FeeCycle[] { };
            Sessions = new Session[] { };
            Reviews = new Review[] { };
            Activities = new ActivityMaster[] { };
            DayCareMenus = new DayCareMenu[] { };
            VirtualTour = new VirtualTourImage[] { };
            MonthlyReports = new Collection<string>();
            CreditHistory = new CreditLog[] { };
            News = new NewsFeed[] { };
            AboutUs = new AboutUs[] { };
            MasterSubjects = new MasterSubject[] { };
            MasterGradedDisciplines = new MasterGradedDiscipline[] { };

        }

        public string GetShortCode()
        {
            return School.GetShortCode(this.Name);
        }
        //Designation and check
        public string Designation { get; set; }
        public bool check { get; set; }

        public static string GetShortCode(string name)
        {
            return
                string.Join("", name.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.First()))
                    .ToUpper();
        }

        public string ContactEmail { get; set; }
        public string Website { get; set; }
        public string FacebookUrl { get; set; }

        public Testimonial[] Testimonials { get; set; }
        public Team[] Teams { get; set; }
        public ICollection<ObjectId> Blogs { get; set; }
        public SchoolFeeComponent[] SchoolFeeComponents { get; set; }
        public FeeCycle[] FeeCycles { get; set; }
        public Session[] Sessions { get; set; }
        public LocationCorrdinates Coordinates { get; set; }
        public DateTime? SignUpDate { get; set; }
        public string Notes { get; set; }
        public Review[] Reviews { get; set; }
        public Modules Modules { get; set; }
        public bool CanParentReply { get; set; }
        public VirtualTourImage[] VirtualTour { get; set; }
        public DayCareMenu[] DayCareMenus { get; set; }
        public ActivityMaster[] Activities { get; set; }
        public Team Principal { get; set; }
        public Feature[] Features { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }


        public DayCareMenuLabels DayCareMenuLabels { get; set; }
        public bool SmsEnabled { get; set; }
        public int SmsCredits { get; set; }
        public int MarketingSmsCredits { get; set; }
        public CreditLog[] CreditHistory { get; set; }
        public PaymentGatewayDetails PaymentGatewayDetails { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public AboutUs[] AboutUs { get; set; }
        public Feature[] Programs { get; set; }
        public NewsFeed[] News { get; set; }
        public Team Director { get; set; }
        public int PageViews { get; set; }
        public MasterSubject[] MasterSubjects { get; set; }
        public MasterGradedDiscipline[] MasterGradedDisciplines { get; set; }
        public string ExecutiveName { get; set; }
        public string ExecutiveEmail { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public string PaymentNotes { get; set; }
        public LateFeeType LateFeeType { get; set; }
        public double LateFeeAmount { get; set; }
        public MarketingData MarketingData { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public SchoolType Type { get; set; }

        public Session GetActiveSession()
        {
            var activeSession = this.Sessions.FirstOrDefault(x => x.IsActive);
            return activeSession;
        }

        public IEnumerable<FeeCycle> GetActiveFeeCycles()
        {
            var session = GetActiveSession();
            if (session == null)
            {
                return new FeeCycle[0];
            }
            return this.FeeCycles.Where(x => x.SessionId == session.Id);
        }
    }


    public class LocationCorrdinates
    {
        public LocationCorrdinates()
        {
            coordinates = new double[] { };
        }

        //        public decimal Latitude { get; set; }
        //        public decimal Longitude { get; set; }

        public string type { get; set; }
        public double[] coordinates { get; set; }
    }

    public class Team
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class Testimonial
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Content { get; set; }
    }

    public class ParentInvite
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
        public string SentBy { get; set; }
    }


    public class SchoolFeeComponent
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public Periodicity Periodicity { get; set; }
    }

    public class FeeCycle
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastDueDate { get; set; }
        public double LateFee { get; set; }
        public ObjectId SessionId { get; set; }
    }

    public class Session
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }


    public class Review
    {
        public Review()
        {
            Rating = new Dictionary<string, decimal>();
        }

        public ObjectId Id { get; set; }
        public ObjectId? UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public IDictionary<string, decimal> Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? RejectionDate { get; set; }
        public int Priority { get; set; }
        public NotificationDeviceType DeviceType { get; set; }
    }

    public class Modules
    {
        public bool HasMessage { get; set; }
        public bool HasEvent { get; set; }

        public bool HasLead { get; set; }

        public bool HasBlog { get; set; }

        public bool HasAlbum { get; set; }

        public bool HasAttendance { get; set; }

        public bool HasFee { get; set; }

        public bool HasOnlineFee { get; set; }
        public bool HasChildQrCode { get; set; }
        public bool HasCabQrCode { get; set; }
        public bool HasSchedulingMessage { get; set; }

        public bool HasSchoolPage { get; set; }
        public bool HasCreateBlog { get; set; }
        public bool HasMealPlanner { get; set; }
        public bool HasActivityPlanner { get; set; }

        public bool HasParentReadTime { get; set; }

        public bool HasUserLimit { get; set; }
        public bool HasGpsTracker { get; set; }
        public bool HasUserAccessRights { get; set; }
        public bool HasDayCare { get; set; }
        public bool HasParentLeadForm { get; set; }

        public bool HasViewUserMessages { get; set; }
        public bool HasConsentRsvp { get; set; }
        public bool HasPickDrop { get; set; }
        public bool HasReplyToggle { get; set; }
    }

    public class VirtualTourImage
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }


    public class DayCareMenu
    {
        public DateTime Date { get; set; }
        public string Food { get; set; }
        public DayCareMenuType MenuType { get; set; }
        public DateTime MealTime { get; set; }
    }

    public class ActivityMaster
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDateTime { get; set; }
    }

    public class Feature
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
    }

    public class DayCareMenuLabels
    {
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Snacks { get; set; }
    }

    public class CreditLog
    {
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; }
        public int Credits { get; set; }
        public string Notes { get; set; }
        public SMSType Type { get; set; }
    }

    public class PaymentGatewayDetails
    {
        public string SellerId { get; set; }

        public string Name { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }


        public string PinCode { get; set; }

        public string Country { get; set; }

        public string BusinessUrl { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Ifsc { get; set; }

        public string AccountNumber { get; set; }
    }

    public class AboutUs
    {
        public AboutUs()
        {
            Images = new Attachement[] { };
        }

        public string Body { get; set; }

        public IEnumerable<Attachement> Images { get; set; }
        public ObjectId Id { get; set; }
    }

    public class NewsFeed
    {
        public NewsFeed()
        {
            Images = new Attachement[] { };
        }

        public string Body { get; set; }

        public IEnumerable<Attachement> Images { get; set; }
        public ObjectId Id { get; set; }
    }

    public class MasterSubject
    {
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class MasterGradedDiscipline
    {
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
    }

    public class MarketingData
    {
        public string Body { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
    }
}
