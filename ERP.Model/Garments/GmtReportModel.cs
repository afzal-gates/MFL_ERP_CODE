using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.ComponentModel;
using System.IO;

namespace ERP.Model
{
    public class GmtReportModel
    {
        public string REPORT_CODE { get; set; }
        public string IS_EXCEL_FORMAT { get; set; }
        public string PAGE_SIZE_NAME { get; set; }

        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public Int64? GMT_SW_MCN_SPEC_ID { get; set; }




        public DataSet SwMcnSpec()
        {
            var ob = this;
            string sp = "pkg_gmt_report.gmt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pGMT_SW_MCN_SPEC_ID", Value = ob.GMT_SW_MCN_SPEC_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 1500}
                }, sp);

            ds.Tables[0].TableName = "SW_MCN_SPEC";
            ds.Tables[1].TableName = "SW_MCN_THRD_CONS";
            ds.Tables[2].TableName = "SW_MCN_GUIDE";
            ds.Tables[3].TableName = "SW_MCN_PROFILE";

            return ds;
        }


    }
}