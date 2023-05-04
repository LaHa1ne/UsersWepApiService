using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersWepApiService.DataLayer.Entities
{
    public class User
    {
        [Column("Guid", TypeName = "uniqueidentifier")]
        public Guid Guid { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }

        public bool Admin { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; } 
        public string? ModifiedBy { get; set; } 
        public DateTime? RevokedOn { get; set; }
        public string? RevokedBy { get; set; }

        public User CreatedByUser { get; set; } = null!;
        public User? ModifiedByUser { get; set; } 
        public User? RevokedByUser { get; set; } 

        public List<User> CreatedUsers { get; set; } = new();
        public List<User> ModifiedUsers { get; set; } = new();
        public List<User> RevokedUsers { get; set; } = new();

    }
}
