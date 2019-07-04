using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public enum FeeStatus
    {
        [Display(Name = "Pending Approval")] PendingApproval,
        Approved,
        Paid,
        [Display(Name = "Partial Paid")] PartialPaid,
        Cancelled
    }
    public enum JobStatus
    {
        Unknown = 0,

        /// <summary>
        /// New job scheduled and ready to be picked.
        /// </summary>
        New = 1,

        /// <summary>
        /// Job is in progress so that same job is not picked twice.
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Job is completed.
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Job failed in last execution.
        /// </summary>
        Failed = 4,

        /// <summary>
        /// Job status when retry is needed. Each job should decide if failure occurred to we retry or mark it as failed.
        /// Failed jobs will be never be retried only retry needed jobs will be retried 3 times after which they will be marked as failed.
        /// </summary>
        RetryNeeded = 5
    }
    public enum TransactionStatus
    {
        Pending,
        Completed,
        Cancelled
    }
    public enum AmountMode
    {
        Cash,
        [Display(Name = "Bank Transfer")] BankDraft,
        Cheque,
        [Display(Name = "Online Payment")] OnlinePayement,
        [Display(Name = "Card Payment")] CardPayment,
        IMPS
    }
    public enum TransactionItemType
    {
        Fee
    }
}
