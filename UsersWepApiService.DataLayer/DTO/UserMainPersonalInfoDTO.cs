using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersWepApiService.DataLayer.DTO
{
    public class UserMainPersonalInfoDTO
    {
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsActive { get; set; }

    }
}
