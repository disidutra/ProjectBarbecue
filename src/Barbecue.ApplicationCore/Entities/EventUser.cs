using System;

namespace Barbecue.ApplicationCore.Entities
{
    public class EventUser
    {
        //public int Id { get; set; }
        public bool EventValue { get; set; }
        public bool DrinksValue { get; set; }
        public decimal ValuePaid { get; set; }
        public virtual int EventId { get; set; }
        public virtual Event Event { get; set; }
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }
}