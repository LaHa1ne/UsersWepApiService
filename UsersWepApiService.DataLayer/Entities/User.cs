using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersWepApiService.DataLayer.Entities
{
    public class User
    {
        public Guid Guid { get; set; }
        public string Login { get; set; } 
        public string Password { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }

        public bool Admin { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; } 
        public string? ModifiedBy { get; set; } 
        public DateTime? RevokedOn { get; set; }
        public string? RevokedBy { get; set; }

        public User CreatedByUser { get; set; } 
        public User ModifiedByUser { get; set; } 
        public User RevokedByUser { get; set; } 

        public List<User> CreatedUsers { get; set; } = new();
        public List<User> ModifiedUsers { get; set; } = new();
        public List<User> RevokedUsers { get; set; } = new();

    }
}
