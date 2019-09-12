using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.Model
{
    public class HrPunchTypeModel //: IHrCompanyModel
    {
        public Int64 HR_PUNCH_TYPE_ID { get; set; }

        public string PUNCH_TYPE_CODE { get; set; }

        [Required]
        public string PUNCH_TYPE_NAME_EN { get; set; }
        [Required]
        public string PUNCH_TYPE_NAME_BN { get; set; }
        
        public string PUNCH_TYPE_DESC { get; set; }


        public string COLOR_CODE { get; set; }
        public string IS_ACTIVE { get; set; }
       

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }        
        

    }
}
