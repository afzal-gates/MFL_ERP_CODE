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
    public class GMT_PLN_STYL_CNGE_RESModel
    {
        public Int64 GMT_PLN_STYL_CNGE_RES_ID { get; set; }
        public Int64 GMT_PLN_STYL_CNGE_LOG_ID { get; set; }
        public Int64? RF_RESP_DEPT_ID { get; set; }
        public Int64 PCT_SHARE { get; set; }
        public string PLAN_CNGE_REASON { get; set; }

        public List<GMT_PLN_STYL_CNGE_RESModel> Query(Int64 pGMT_PLN_STYL_CNGE_LOG_ID)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_styl_cnge_log_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_STYL_CNGE_RESModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "GMT_PLN_STYL_CNGE_LOG_ID", Value = pGMT_PLN_STYL_CNGE_LOG_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_PLN_STYL_CNGE_RESModel ob = new GMT_PLN_STYL_CNGE_RESModel();
                            ob.GMT_PLN_STYL_CNGE_RES_ID = (dr["GMT_PLN_STYL_CNGE_RES_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_STYL_CNGE_RES_ID"]);
                            ob.GMT_PLN_STYL_CNGE_LOG_ID = (dr["GMT_PLN_STYL_CNGE_LOG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_STYL_CNGE_LOG_ID"]);
                            ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                            ob.PCT_SHARE = (dr["PCT_SHARE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_SHARE"]);
                            ob.PLAN_CNGE_REASON = (dr["PLAN_CNGE_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CNGE_REASON"]);

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 FindNoOfPlanChange(Int64 pGMT_PLN_LINE_LOAD_ID)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_styl_cnge_log_select";
            //pOption=3000=>Select All Data
            try
            {
                Int32 ret_number = 1;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ret_number = (dr["NO_OF_PLN_CHANGE"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_PLN_CHANGE"]);
                }
                return ret_number;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}