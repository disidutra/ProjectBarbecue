using System;
using System.Collections.Generic;
using System.Linq;

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
        public int TotalUsers
        {
            get
            {
                return EventUsers != null ? EventUsers.Count() : 0;
            }
        }
        public decimal TotalValue
        {
            get
            {
                return EventUsers != null ? (EventValue * EventUsers.Sum(x => x.EventValue ? 1 : 0)
                                            + DrinksValue * EventUsers.Sum(x => x.DrinksValue ? 1 : 0))
                                        : 0;
            }
        }
        public decimal TotalPaid
        {
            get
            {
                return EventUsers != null ? EventUsers.Sum(x => x.ValuePaid) : 0;
            }
        }
    }
}