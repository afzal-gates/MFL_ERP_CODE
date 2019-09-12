using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class SickLeaveMailService : Email
    {
        public string To { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string DESIGNATION_NAME_EN { get; set; }
    }
}
