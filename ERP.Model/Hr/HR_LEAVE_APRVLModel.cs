using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Model
{
    public class HR_LEAVE_APRVLModel
    {
        public Int64 HR_LEAVE_APRVL_ID { get; set; }
        public Int64 HR_LEAVE_TRANS_ID { get; set; }
        public Int64 APPROVER_ID { get; set; }
        public Int64 EXEC_BY_ID { get; set; }
        public DateTime ACTION_DATE { get; set; }
        public Int64 LK_LV_STATUS_ID { get; set; }
        public Int64 APROVER_LVL_NO { get; set; }
        public string IS_VISIBLE { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
    }
}