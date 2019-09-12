using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class eLoanMailServiceBeforeApprove : Email
    {
        public string To { get; set; }

        public string EMP_FULL_NAME_EN { get; set; }

        public string DESIGNATION_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string EMPLOYEE_CODE { get; set; }

        public string ADV_REF_NO { get; set; }

        public string LK_ADV_TYPE { get; set; }

        public Decimal ADV_RQST_AMT { get; set; }
    }
}
