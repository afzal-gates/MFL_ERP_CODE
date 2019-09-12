using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class eLoanMailServiceAfterApprove : Email
    {
        public string To { get; set; }

        public string EMP_FULL_NAME_EN { get; set; }

        public string DESIGNATION_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string EMPLOYEE_CODE { get; set; }

        public string ADV_REF_NO { get; set; }

        public string LK_ADV_TYPE { get; set; }

        public Decimal ADV_APRV_AMT { get; set; }
        public string APPROVED_BY { get; set; }
        public DateTime DEDU_ST_DT { get; set; }
        public Int64 NO_OF_INSTL { get; set; }



    }
}
