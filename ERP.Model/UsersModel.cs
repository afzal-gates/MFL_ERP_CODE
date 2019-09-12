using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class UsersModel //: IUsersModel
    {

        public int SC_USER_ID { get; set; }

        public Int64 ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public Int64 Role { get; set; }
    }
        

}
