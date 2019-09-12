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
    public class HR_INCR_GRADEModel
    {
        public Int64 HR_INCR_GRADE_ID { get; set; }
        public string INCR_GRADE_CODE { get; set; }
        public string GRADE_DESC { get; set; }
        public string IS_P_F { get; set; }
        public Decimal GRADE_AMT { get; set; }
        public string IS_B_G { get; set; }
        public Int64? ADDL_AMT { get; set; }
        public Decimal? HR_PCT { get; set; }
        public Int32? DESIG_ORDER_LOW { get; set; }
        public Int32? DESIG_ORDER_HIGH { get; set; }



        public List<HR_INCR_GRADEModel> GetIncrGradeList(Int64? pHR_EMPLOYEE_TYPE_ID = null)
        {
            string sp = "pkg_incriment.hr_incr_grade_select";
            try
            {
                var obList = new List<HR_INCR_GRADEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_TYPE_ID", Value = pHR_EMPLOYEE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   HR_INCR_GRADEModel ob = new HR_INCR_GRADEModel();
                   ob.HR_INCR_GRADE_ID = (dr["HR_INCR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_GRADE_ID"]);
                   ob.INCR_GRADE_CODE = (dr["INCR_GRADE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_GRADE_CODE"]);
                   ob.GRADE_DESC = (dr["GRADE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_DESC"]);
                   ob.IS_P_F = (dr["IS_P_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_P_F"]);
                   ob.GRADE_AMT = (dr["GRADE_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_AMT"]);
                   ob.IS_B_G = (dr["IS_B_G"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_B_G"]);
                   ob.ADDL_AMT = (dr["ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_AMT"]);
                   ob.HR_PCT = (dr["HR_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["HR_PCT"]);

                   ob.DESIG_ORDER_LOW = (dr["DESIG_ORDER_LOW"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DESIG_ORDER_LOW"]);
                   ob.DESIG_ORDER_HIGH = (dr["DESIG_ORDER_HIGH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DESIG_ORDER_HIGH"]);

                   obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}