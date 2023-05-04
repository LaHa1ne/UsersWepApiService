using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersWepApiService.DataLayer.DTO
{
    public class CreatedUserInfoDTO
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Birthday { get; set; } = null!; 
        public string Admin { get; set; } = null!; 
    }
}
