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
    public class GMT_CUT_PRN_RCV_CHL_HModel
    {
        public Int64 GMT_CUT_PRN_RCV_CHL_H_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string IS_FINALIZED { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }
        public string SERVICE_NAME { get; set; }
        public string PART_NAME_LIST { get; set; }
        public string XML { get; set; }
        public string GMT_CUT_PNL_BNK_LST { get; set; }
        public Int32? pOption { get; set; }
        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_rcv_chl_h_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_LST", Value = ob.GMT_CUT_PNL_BNK_LST},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =ob.pOption ?? 1000},
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

        public List<GMT_CUT_PRN_RCV_CHL_HModel> Query()
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_rcv_chl_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PRN_RCV_CHL_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_RCV_CHL_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_PRN_RCV_CHL_HModel ob = new GMT_CUT_PRN_RCV_CHL_HModel();
                            ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                            ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                            ob.IS_AUTO_CHALLAN_NO = "N";
                            ob.CHALAN_DT = DateTime.Now.Date;
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IS_AUTO_CHALLAN_NO { get; set; }
    }
}