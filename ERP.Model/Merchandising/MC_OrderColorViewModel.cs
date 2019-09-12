using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.ComponentModel;

namespace ERP.Model
{    
    public class MC_OrderShipDateVM
    {          
        public Int64? ITEM_INDEX { get; set; }
        public Int64? SHIP_DT_INDEX { get; set; }
        public DateTime? SHIP_DT { get; set; }
        public Int64? MC_DEST_POINT_ID { get; set; }
        public Int64? TOTAL_QTY { get; set; }

        public List<MC_OrderColorViewModel> itemsColor { get; set; }
    }


    public class MC_OrderColorViewModel
    {
        public Int64? MC_COLOR_ID { get; set; }
        public Int64? COLOR_INDEX { get; set; }
        public Int64? COLOR_DISPLAY_ORDER { get; set; }
        public Int64? ITEM_INDEX { get; set; }        
        public string COLOR_NAME_EN { get; set; }
        public string COLOR_REF { get; set; }
        public string COMBO_NO { get; set; }
        public List<COMBO_NO_VmModel> COMBO_NO_LIST { get; set; }
        public Int64? PACK_NO { get; set; }
        public Int64? TOTAL_QTY { get; set; }

        public List<MC_ORDER_SIZEModel> itemsSize { get; set; }
    }

    public class COMBO_NO_VmModel
    {
        public string COMBO_NO { get; set; }
    }
    

}