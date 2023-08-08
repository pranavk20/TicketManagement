using System;
using System.Collections.Generic;

#nullable disable

namespace TicketManagementSystem.Models
{
    public  class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public int? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
