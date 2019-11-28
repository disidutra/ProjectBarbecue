using System;
using System.Collections.Generic;

namespace Barbecue.ApplicationCore.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public decimal EventValue { get; set; }
        public decimal DrinksValue { get; set; }
        public virtual ICollection<EventUser> EventUsers { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalValue { get; set; } 
        public decimal TotalPaid { get; set; }
    }
}