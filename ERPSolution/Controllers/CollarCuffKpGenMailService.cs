using Postal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERPSolution.Controllers
{
    public class CollarCuffKpGenMailService : Email
    {
        public string KNT_PLAN_NO { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string ORDER_NO_LIST { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string CREATION_DATE { get; set; }
        public string CREATED_BY_NAME { get; set; }
        public string FAB_TYPE_NAME { get; set; }
    }
}
