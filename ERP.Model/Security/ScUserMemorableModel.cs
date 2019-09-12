using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class ScUserMemorableModel
    {
        public Int64 SC_USER_MEMORABLE_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public string MEMORABLE_TEXT { get; set; }
        public DateTime? MEMO_START_DATE { get; set; }
        public DateTime? MEMO_END_DATE { get; set; }
        public string IS_ACTIVE { get; set; }
    }

}
