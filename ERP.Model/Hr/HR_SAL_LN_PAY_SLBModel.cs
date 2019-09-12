using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Model
{
    public class HR_SAL_LN_PAY_SLBModel
    {
        public Int64 HR_SAL_LN_PAY_SLB_ID { get; set; }
        public Int64 HR_SAL_ADVANCE_ID { get; set; }
        public Int64 SLAB_NO { get; set; }
        public Int64 NO_OF_INSTL { get; set; }
        public Decimal INSTL_AMT { get; set; }
        public Int64 TOT_INSTL_PAID { get; set; }
        public string IS_CLOSED { get; set; }
        //public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 VERSION_NO { get; set; }

        public Int64? CAN_BE_DEL { get; set; }
    }
}