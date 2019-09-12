using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace ERP.Model
{
    public class ATTN_CORR_VM
    {
        public Int64 RF_FISCAL_YEAR_ID { get; set; }
        public Int64 MONTH_VALUE { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public string CORRECTION_TYPE { get; set; }
        public Int64? LK_PN_COR_REASON_ID { get; set; }
        public string IS_CORRECTION_REASON_OTHER { get; set; }

        public string CORRECTION_REASON_OTHER { get; set; }
        public Int64 PUNCH_TYPE { get; set; }
        public DateTime FROM_DATE { get; set; }
        public DateTime TO_DATE { get; set; }
        public string CORRECTED_STATUS { get; set; }
        public Int64? HR_DEPARTMENT_ID { get; set; }
        public Int64? HR_SHIFT_TEAM_ID { get; set; }
        public Int64? EMPLOYEE_TYPE_ID { get; set; }
        public string FLOOR { get; set; }
        public Int64? LINE_NO { get; set; }
        public string HR_EMPLOYEE_IDS { get; set; }

        public Int64? HR_EMPLOYEE_ID { get; set; }

        public DateTime? OT_HR { get; set; }
        public Int64? HR_DAY_TYPE_ID { get; set; }

        public Int64? HR_OFFICE_ID { get; set; }
    }
}