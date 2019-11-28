using System;
using System.Collections.Generic;

namespace Barbecue.ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<EventUser> EventUsers { get; set; }
    }
}