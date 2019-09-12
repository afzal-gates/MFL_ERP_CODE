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
    public class GMT_CUT_PNL_BNKModel
    {
        public Int64 GMT_CUT_PNL_BNK_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64 GMT_CUT_SEW_DLV_CHL_ID { get; set; }
        public Int64 GMT_CUT_SC_DLV_CHL_ID { get; set; }
        public Int64 GMT_BNDL_CRD_PDATA_ID { get; set; }
        public Int64 FINAL_QTY { get; set; }
        public Int64 LK_CPNL_STATUS_ID { get; set; }
        public string IS_CLOSED { get; set; }
        public string GMT_CUT_PNL_BNK_LST { get; set; }
        public int? pOption { get; set; }
        public string BARCODE { get; set; }
        public string XML { get; set; }

        public string SaveAndOrDelete()
         // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_ID", Value = ob.GMT_CUT_PNL_BNK_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pGMT_BNDL_CRD_PDATA_ID", Value = ob.GMT_BNDL_CRD_PDATA_ID},
                     new CommandParameter() {ParameterName = "pBARCODE", Value = ob.BARCODE},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_LST", Value = ob.GMT_CUT_PNL_BNK_LST},
                     new CommandParameter() {ParameterName = "pLK_CPNL_STATUS_ID", Value = ob.LK_CPNL_STATUS_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value = ob.IS_CLOSED},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption??1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_CUT_PNL_BNK_ID", Value =0, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_CUT_PRN_DELV_CHL_H_LST", Value =0, Direction = ParameterDirection.Output}
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
        public List<GMT_CUT_PNL_BNKModel> Query(int pOption)
        {
            string sp = "pkg_name.gmt_cut_pnl_bnk_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PNL_BNKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_PNL_BNKModel ob = new GMT_CUT_PNL_BNKModel();
                            ob.GMT_CUT_PNL_BNK_ID = (dr["GMT_CUT_PNL_BNK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_BNK_ID"]);
                            ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                            ob.GMT_CUT_SEW_DLV_CHL_ID = (dr["GMT_CUT_SEW_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SEW_DLV_CHL_ID"]);
                            ob.GMT_CUT_SC_DLV_CHL_ID = (dr["GMT_CUT_SC_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SC_DLV_CHL_ID"]);
                            ob.GMT_BNDL_CRD_PDATA_ID = (dr["GMT_BNDL_CRD_PDATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_BNDL_CRD_PDATA_ID"]);
                            ob.FINAL_QTY = (dr["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FINAL_QTY"]);
                            ob.LK_CPNL_STATUS_ID = (dr["LK_CPNL_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CPNL_STATUS_ID"]);
                            ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GMT_CUT_PNL_BNKModel Select(Int64 pGMT_CUT_PNL_BNK_ID, int pOption)
        {
            string sp = "pkg_name.gmt_cut_pnl_bnk_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new GMT_CUT_PNL_BNKModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_ID", Value = pGMT_CUT_PNL_BNK_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.GMT_CUT_PNL_BNK_ID = (dr["GMT_CUT_PNL_BNK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_BNK_ID"]);
                        ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                        ob.GMT_CUT_SEW_DLV_CHL_ID = (dr["GMT_CUT_SEW_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SEW_DLV_CHL_ID"]);
                        ob.GMT_CUT_SC_DLV_CHL_ID = (dr["GMT_CUT_SC_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SC_DLV_CHL_ID"]);
                        ob.GMT_BNDL_CRD_PDATA_ID = (dr["GMT_BNDL_CRD_PDATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_BNDL_CRD_PDATA_ID"]);
                        ob.FINAL_QTY = (dr["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FINAL_QTY"]);
                        ob.LK_CPNL_STATUS_ID = (dr["LK_CPNL_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CPNL_STATUS_ID"]);
                        ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);

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