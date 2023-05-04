using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersWepApiService.DataLayer.DTO
{
    public class UserPersonalInfoDTO
    {
        public string Guid{ get; set; } = null!;
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Birthday { get; set; }

    }
}
