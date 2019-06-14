using System;
using System.Collections.Generic;
using System.Text;

namespace HelloParent.Entities.Enums
{
    public class Paging
    {
        public int Page { get; set; }

        public int Count { get; set; }

        public Paging()
        {
            Page = 1;
            Count = 10;
        }
    }
    public enum StatusEnum
    {
        Available,
        Dead,
        Lost,
        Issued
    }
    public enum CategoryEnum
    {
        Book,
        Novel,
        Journal,
        Newspaper
    }
    public enum StudentFeeFrequency
    {
        Monthly,
        Quarterly,
        HalfYearly,
        Annually
    }
    public enum SubscriptionPlan
    {
        Premium,
        Basic,
        Standard,
        Advanced
    }
    public enum InvoiceType
    {
        Annually,
        //[Display(Name = "Bi-Annually")]
        BiAnnually,
        Quarterly
    }
    public enum LateFeeType
    {
        Fixed,
        Daily,
        Weekly,
        Monthly
    }
    public enum SchoolType
    {
        PreSchool,
        K12,
        Coaching
    }

    public enum Periodicity
    {
        Monthly,
        Yearly,
        //[DisplayN(Name = "One Time")]
        OneTime
    }
    public enum NotificationDeviceType
    {
        Android,
        Apple
    }
    public enum DayCareMenuType
    {
        Breakfast,
        Lunch,
        Snacks
    }
    public enum SMSType
    {
        Transactional,
        Marketing
    }
    public enum EngagementType
    {
        Alert,
        Response,
        ImageRequest,
        Question,
        Tooltip,
        HpBlog,
        Blog,
        Album,
        ReviewRequest,
        ForumComment,
        Forum,
        Event,
        Holiday,
        PublicEvent,
        BlogAlert,
        EventAlert,
        UpdatedEvent,
        SendConsent,
        ConsentReminder,
        MealUpload,
        ActivityUpload,
        DayCare,
        Birthday,
        TimelinePost,
        DailyReport,
        AuthorizedPersonApproval,
        PollResult,
        Logout,
        TeachKidsContent,
        Fees
    }
    public enum UserType
    {
        HPCreated,
        Free
    }
}
