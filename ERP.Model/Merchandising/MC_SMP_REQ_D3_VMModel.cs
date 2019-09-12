using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class MC_SMP_REQ_D3_VMModel
    {                
        public Int64? RF_GARM_PART_ID { get; set; }        
        public Int64? REQ_D_INDEX { get; set; }
        public List<MC_SMP_REQ_D3Model> itemsSize { get; set; }       
    }
}