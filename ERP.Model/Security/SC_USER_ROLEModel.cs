using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Model
{
    public class SC_USER_ROLEModel
    {
        public Int64 SC_USER_ROLE_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public Int64 SC_ROLE_ID { get; set; }
        public string IS_ACTIVE { get; set; }

        public string ROLE_NAME_EN { get; set; }
    }
}