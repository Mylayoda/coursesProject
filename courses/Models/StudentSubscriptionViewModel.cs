using courses.Entities;
using System.Collections.Generic;


namespace courses.Models
{
    public class StudentSubscriptionViewModel
    {
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<StudentSubscriptionModel> StudentSubscriptions { get; set; }
        public bool DisableDropDown { get; set; }
        public string UserId { get; set; }
        public int SubscriptionId { get; set; }
    }
}