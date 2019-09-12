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
    public class GMT_CUT_PNL_RECUTModel
    {
        public Int64 GMT_CUT_PNL_RECUT_ID { get; set; }
        public string GMT_CUT_PNL_RECUT_LST { get; set; }
        public Int64 ACT_TYPE { get; set; }
        public Int32 NO_OF_PANEL_REJ { get; set; }
        public Int32 NO_OF_PNL_RECUT { get; set; }
        public Int32 ALLOWED_SHRT_QTY { get; set; }
        public string GARM_PART_NAME { get; set; }
        public Int32 RF_GARM_PART_ID { get; set; }

        public string Save()
        // 0=>Clear From List, 1=>Mark As OK, 2=> Mark As Not OK
        {
            const string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_recut_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_RECUT_LST", Value = ob.GMT_CUT_PNL_RECUT_LST},
                     new CommandParameter() {ParameterName = "pACT_TYPE", Value = ob.ACT_TYPE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                     jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     }  
                     else  
                      {  
                          jsonStr += "}"; 
                      }  
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GMT_CUT_PNL_RECUTModel> Query(Int64 pGMT_BNDL_CRD_PDATA_ID)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PNL_RECUTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_BNDL_CRD_PDATA_ID", Value = pGMT_BNDL_CRD_PDATA_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_PNL_RECUTModel ob = new GMT_CUT_PNL_RECUTModel();
                            ob.GMT_CUT_PNL_RECUT_ID = (dr["GMT_CUT_PNL_RECUT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_RECUT_ID"]);
                            ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                            ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_GARM_PART_ID"]);
                            ob.NO_OF_PANEL_REJ = (dr["NO_OF_PANEL_REJ"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_PANEL_REJ"]);
                            ob.NO_OF_PNL_RECUT = (dr["NO_OF_PNL_RECUT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_PNL_RECUT"]);
                            ob.ALLOWED_SHRT_QTY = (dr["ALLOWED_SHRT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ALLOWED_SHRT_QTY"]);
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