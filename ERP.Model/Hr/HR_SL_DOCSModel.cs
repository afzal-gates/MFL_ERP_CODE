using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.Model
{
    public class HR_SL_DOCSModel 
    {
        public Int64? HR_SL_DOCS_ID { get; set; }
        public Int64 HR_LEAVE_TRANS_ID { get; set; }
        public Int64 DOC_ORDER { get; set; }
        public string DOC_NAME_EN { get; set; }
        public string DOC_NAME_BN { get; set; }
        public string DOC_PATH_REF { get; set; }
        public string REMOVE { get; set; }
        public HttpPostedFileBase ATT_FILE { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
    }
}