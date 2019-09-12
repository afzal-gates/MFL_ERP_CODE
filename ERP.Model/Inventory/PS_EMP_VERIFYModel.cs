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
    public class PS_EMP_VERIFYModel
    {
        public Int64 DAYS_PRESENT { get; set; }
        public Int64 DAYS_ABSENT { get; set; }
        public Int64 LAST_PURCHASE_AMT { get; set; }
        public DateTime? LAST_PURCHASE_DATE { get; set; }
        public Int64 CRD_LMT_EARNED { get; set; }
        public Int64 ALREADY_SOLD_AMT { get; set; }
        public Int64 CREDIT_BALANCE_AMT { get; set; }
        public Decimal DISC_PCT { get; set; }
        public Decimal VAT_PCT { get; set; }

        
        public PS_EMP_VERIFYModel EmpPOSVerify(Int64? pHR_EMPLOYEE_ID, DateTime pMEMO_DT)
        {
            string sp = "pkg_fair_shop.emp_pos_verify_select";
            try
            {
                var ob = new PS_EMP_VERIFYModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value =pHR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value =pMEMO_DT},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   ob.DAYS_PRESENT = (dr["DAYS_PRESENT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_PRESENT"]);
                   ob.DAYS_ABSENT = (dr["DAYS_ABSENT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_ABSENT"]);
                   ob.LAST_PURCHASE_AMT = (dr["LAST_PURCHASE_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_PURCHASE_AMT"]);
                   ob.LAST_PURCHASE_DATE = (dr["LAST_PURCHASE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_PURCHASE_DATE"]);
                   ob.CRD_LMT_EARNED = (dr["CRD_LMT_EARNED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CRD_LMT_EARNED"]);
                   ob.ALREADY_SOLD_AMT = (dr["ALREADY_SOLD_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALREADY_SOLD_AMT"]);
                   ob.CREDIT_BALANCE_AMT = (dr["CREDIT_BALANCE_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREDIT_BALANCE_AMT"]);
                   ob.DISC_PCT = (dr["DISC_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DISC_PCT"]);
                   ob.VAT_PCT = (dr["VAT_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["VAT_PCT"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}