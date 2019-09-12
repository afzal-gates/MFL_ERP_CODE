using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.Model
{
    public class HrScheduleD011Model //: IHrCompanyModel
    {
        public Int64 HR_SCHEDULE_D011_ID { get; set; }

        [Required]
        public Int64 HR_SCHEDULE_D01_ID { get; set; }


        [Required]
        public Int64 RF_CALENDAR_DAY_ID { get; set; }


        public string CALENDAR_DAY_NAME_EN { get; set; }
        public string IS_ACTIVE { get; set; }


        //public DateTime? CREATION_DATE { get; set; }
        //public int CREATED_BY { get; set; }
        //public DateTime? LAST_UPDATE_DATE { get; set; }
        //public int LAST_UPDATED_BY { get; set; }        
        //public int LAST_UPDATE_LOGIN { get; set; }
        //public int VERSION_NO { get; set; }



    }
}
