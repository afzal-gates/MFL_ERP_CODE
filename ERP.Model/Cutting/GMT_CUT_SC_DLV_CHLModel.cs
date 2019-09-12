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
    public class GMT_CUT_SC_DLV_CHLModel
    {
        public Int64 GMT_CUT_SC_DLV_CHL_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string IS_TAG { get; set; }
        public Int64 LK_BNDL_MVM_RSN_ID { get; set; }
        public Int64 NO_OF_BAG { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string IS_FINALIZED { get; set; }
        public string GMT_CUT_PNL_BNK_LST { get; set; }
        public int? pOption { get; set; }

        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp ="PKG_GMT_CUT_BANK.gmt_cut_sc_dlv_chl_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_SC_DLV_CHL_ID", Value = ob.GMT_CUT_SC_DLV_CHL_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pIS_TAG", Value = ob.IS_TAG},
                     new CommandParameter() {ParameterName = "pLK_BNDL_MVM_RSN_ID", Value = ob.LK_BNDL_MVM_RSN_ID},
                     new CommandParameter() {ParameterName = "pNO_OF_BAG", Value = ob.NO_OF_BAG},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_LST", Value = ob.GMT_CUT_PNL_BNK_LST},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption??1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_CUT_SC_DLV_CHL_ID", Value =0, Direction = ParameterDirection.Output}
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
        public List<GMT_CUT_SC_DLV_CHLModel> Query(int pOption)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_sc_dlv_chl_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_SC_DLV_CHLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_SC_DLV_CHL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_SC_DLV_CHLModel ob = new GMT_CUT_SC_DLV_CHLModel();
                            ob.GMT_CUT_SC_DLV_CHL_ID = (dr["GMT_CUT_SC_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SC_DLV_CHL_ID"]);
                            ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                            if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                            {
                                ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                            }

                            ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                            ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                            ob.IS_TAG = (dr["IS_TAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TAG"]);
                            ob.LK_BNDL_MVM_RSN_ID = (dr["LK_BNDL_MVM_RSN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BNDL_MVM_RSN_ID"]);
                            ob.NO_OF_BAG = (dr["NO_OF_BAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BAG"]);
                            ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                            ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                            ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CUT_SC_DLV_CHLModel Select(Int64 pGMT_CUT_SC_DLV_CHL_ID, int pOption)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_sc_dlv_chl_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new GMT_CUT_SC_DLV_CHLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_SC_DLV_CHL_ID", Value = pGMT_CUT_SC_DLV_CHL_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.GMT_CUT_SC_DLV_CHL_ID = (dr["GMT_CUT_SC_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SC_DLV_CHL_ID"]);
                        ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                        

                        if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                        {
                            ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                        }
                        ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                        ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                        ob.IS_TAG = (dr["IS_TAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TAG"]);
                        ob.LK_BNDL_MVM_RSN_ID = (dr["LK_BNDL_MVM_RSN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BNDL_MVM_RSN_ID"]);
                        ob.NO_OF_BAG = (dr["NO_OF_BAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BAG"]);
                        ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                        ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_SC_DLV_CHLModel> getSewingScDeliveryAutoCompl(string pCHALAN_NO, long? pGMT_CUT_SC_DLV_CHL_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_sc_dlv_chl_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_SC_DLV_CHLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_SC_DLV_CHL_ID", Value = pGMT_CUT_SC_DLV_CHL_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_SC_DLV_CHLModel ob = new GMT_CUT_SC_DLV_CHLModel();
                    ob.GMT_CUT_SC_DLV_CHL_ID = (dr["GMT_CUT_SC_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SC_DLV_CHL_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                    {
                        ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    }

                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.IS_TAG = (dr["IS_TAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TAG"]);
                    ob.LK_BNDL_MVM_RSN_ID = (dr["LK_BNDL_MVM_RSN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BNDL_MVM_RSN_ID"]);
                    ob.NO_OF_BAG = (dr["NO_OF_BAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BAG"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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