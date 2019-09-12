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
    public class LABEL_INModel
    {
        public Int64 LABEL_IN_ID { get; set; }
        public Int64 KNT_FAB_ROLL_ID { get; set; }
        public string LABEL_NAME { get; set; }
        public DateTime DATE_TIME { get; set; }
        public Decimal WEIGHT { get; set; }
        public string LINE1 { get; set; }
        public string LINE2 { get; set; }
        public string LINE3 { get; set; }
        public string LINE4 { get; set; }
        public string LINE5 { get; set; }
        public string LINE6 { get; set; }
        public string LINE7 { get; set; }
        public string LINE8 { get; set; }
        public string LINE9 { get; set; }
        public string LINE10 { get; set; }
        public string FILE_PATH { get; set; }

    }
}