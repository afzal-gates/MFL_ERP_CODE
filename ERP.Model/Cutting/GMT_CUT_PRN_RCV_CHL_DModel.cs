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
    public class GMT_CUT_PRN_RCV_CHL_DModel
    {
        public Int64 GMT_CUT_PRN_RCV_CHL_D_ID { get; set; }
        public Int64? GMT_CUT_PRN_RCV_CHL_H_ID { get; set; }
        public Int64 GMT_CUT_PRN_DELV_CHL_D_ID { get; set; }
        public Int64 RCV_QTY { get; set; }
        public Int64 SHORT_QTY { get; set; }
        public Int64 SURPLS_QTY { get; set; }
        public string IS_CLOSED { get; set; }
        public Int64 HAS_REJECT { get; set; }
        public Int64 REJECT_QTY { get; set; }
        public Int64 REJECT_QTY_FAB { get; set; }
        public Int32? pOption { get; set; }
        public string BARCODE { get; set; }
        public Int64 RF_GARM_PART_ID { get; set; }
        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_rcv_chl_d_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_RCV_CHL_D_ID", Value = ob.GMT_CUT_PRN_RCV_CHL_D_ID},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_RCV_CHL_H_ID", Value = ob.GMT_CUT_PRN_RCV_CHL_H_ID},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_D_ID", Value = ob.GMT_CUT_PRN_DELV_CHL_D_ID},
                     new CommandParameter() {ParameterName = "pBARCODE", Value = ob.BARCODE},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pSHORT_QTY", Value = ob.SHORT_QTY},
                     new CommandParameter() {ParameterName = "pSURPLS_QTY", Value = ob.SURPLS_QTY},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value = ob.IS_CLOSED},
                     new CommandParameter() {ParameterName = "pHAS_REJECT", Value = ob.HAS_REJECT},

                     new CommandParameter() {ParameterName = "pREJECT_QTY", Value = ob.REJECT_QTY},
                     new CommandParameter() {ParameterName = "pREJECT_QTY_FAB", Value = ob.REJECT_QTY_FAB},

                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption??1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_CUT_PRN_RCV_CHL_D_ID", Value =0, Direction = ParameterDirection.Output}
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
    }
}