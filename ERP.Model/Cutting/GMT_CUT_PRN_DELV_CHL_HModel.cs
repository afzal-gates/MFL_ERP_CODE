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
    public class GMT_CUT_PRN_DELV_CHL_HModel
    {
        public Int64 GMT_CUT_PRN_DELV_CHL_H_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public List<int> RF_GARM_PART_LST { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string IS_TAG { get; set; }
        public Int64 LK_BNDL_MVM_RSN_ID { get; set; }
        public Int64 LK_SUPPLIER_CAT { get; set; }
        public Int64 NO_OF_BAG { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string IS_FINALIZED { get; set; }
        public string GMT_CUT_PRN_DELV_CHL_H_LST { get; set; }
        public string GMT_CUT_PNL_BNK_LST { get; set; }
        public string XML { get; set; }

        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp ="PKG_GMT_CUT_BANK.gmt_cut_prn_delv_chl_h_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_ID", Value = ob.GMT_CUT_PRN_DELV_CHL_H_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pIS_TAG", Value = ob.IS_TAG},
                     new CommandParameter() {ParameterName = "pLK_BNDL_MVM_RSN_ID", Value = ob.LK_BNDL_MVM_RSN_ID},
                     new CommandParameter() {ParameterName = "pNO_OF_BAG", Value = ob.NO_OF_BAG},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_LST", Value = ob.GMT_CUT_PNL_BNK_LST},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_LST", Value = ob.GMT_CUT_PRN_DELV_CHL_H_LST},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_CUT_PRN_DELV_CHL_H_ID", Value =0, Direction = ParameterDirection.Output}
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
        public List<GMT_CUT_PRN_DELV_CHL_HModel> Query(int pOption, String pGMT_CUT_PRN_DELV_CHL_H_LST)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_delv_chl_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PRN_DELV_CHL_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = pOption },
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_LST", Value = pGMT_CUT_PRN_DELV_CHL_H_LST },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_PRN_DELV_CHL_HModel ob = new GMT_CUT_PRN_DELV_CHL_HModel();
                            ob.GMT_CUT_PRN_DELV_CHL_H_ID = (dr["GMT_CUT_PRN_DELV_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PRN_DELV_CHL_H_ID"]);
                            ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                            ob.RF_GARM_PART_LST =Convert.ToString(dr["RF_GARM_PART_LST"]).Split(',').Select(int.Parse).ToList();
                            if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                            {
                                ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                            }
                            
                            ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                            ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                            ob.IS_TAG = (dr["IS_TAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TAG"]);
                            ob.LK_BNDL_MVM_RSN_ID = (dr["LK_BNDL_MVM_RSN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BNDL_MVM_RSN_ID"]);
                            ob.LK_SUPPLIER_CAT = (dr["LK_SUPPLIER_CAT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUPPLIER_CAT"]);
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



        public List<GMT_CUT_PRN_DELV_CHL_HModel> getPrintEmbrDeliveryChallanAutoCompl(string pCHALAN_NO, long? pGMT_CUT_PRN_DELV_CHL_H_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_delv_chl_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PRN_DELV_CHL_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3001 },
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_ID", Value = pGMT_CUT_PRN_DELV_CHL_H_ID },
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = pCHALAN_NO },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_PRN_DELV_CHL_HModel ob = new GMT_CUT_PRN_DELV_CHL_HModel();
                    ob.GMT_CUT_PRN_DELV_CHL_H_ID = (dr["GMT_CUT_PRN_DELV_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PRN_DELV_CHL_H_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.RF_GARM_PART_LST = Convert.ToString(dr["RF_GARM_PART_LST"]).Split(',').Select(int.Parse).ToList();
                    if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                    {
                        ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    }

                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.IS_TAG = (dr["IS_TAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TAG"]);
                    ob.LK_BNDL_MVM_RSN_ID = (dr["LK_BNDL_MVM_RSN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BNDL_MVM_RSN_ID"]);
                    ob.LK_SUPPLIER_CAT = (dr["LK_SUPPLIER_CAT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUPPLIER_CAT"]);
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

        public List<dynamic> GetPrintEmbrDelvChalnList(Int64? pGMT_CUT_PRN_DELV_CHL_H_ID = null, DateTime? pCHALAN_DT = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_ID = null, 
            Int64? pMC_ORDER_H_ID = null, Int64? pMC_COLOR_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pLK_BNDL_MVM_RSN_ID = null, string pIS_TAG = null, string pIS_FINALIZED = null)
        {
            string sp = "pkg_gmt_cut_bank.gmt_cut_prn_delv_chl_h_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_ID", Value =pGMT_CUT_PRN_DELV_CHL_H_ID},
                    new CommandParameter() {ParameterName = "pCHALAN_DT", Value =pCHALAN_DT},

                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pLK_BNDL_MVM_RSN_ID", Value =pLK_BNDL_MVM_RSN_ID},

                    new CommandParameter() {ParameterName = "pIS_TAG", Value =pIS_TAG},
                    new CommandParameter() {ParameterName = "pIS_FINALIZED", Value =pIS_FINALIZED},

                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        CHALLAN_ROW_SPAN = (dr["CHALLAN_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHALLAN_ROW_SPAN"]),
                        CHALLAN_ROW_SL = (dr["CHALLAN_ROW_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHALLAN_ROW_SL"]),
                        CHALLAN_SL = (dr["CHALLAN_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHALLAN_SL"]),
                        GMT_CUT_PRN_DELV_CHL_H_ID = (dr["GMT_CUT_PRN_DELV_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PRN_DELV_CHL_H_ID"]),
                        CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]),
                        BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]),
                        JOB_NO = (dr["JOB_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_NO"]),
                        ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]),
                        ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]),
                        COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]),
                        CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CUTNG_NO"]),
                        STATUS_NAME = (dr["STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STATUS_NAME"]),

                        GMT_PART_NAME = (dr["GMT_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_PART_NAME"]),
                        SERVICE_NAME = (dr["SERVICE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SERVICE_NAME"]),
                        TO_UNIT = (dr["TO_UNIT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_UNIT"]),

                        NO_OF_BAG = (dr["NO_OF_BAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BAG"]),
                        NO_OF_BNDL_SCANNED = (dr["NO_OF_BNDL_SCANNED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_SCANNED"]),
                        FINAL_QTY = (dr["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FINAL_QTY"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string MergeChallan()
        // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_delv_chl_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_ID", Value = ob.GMT_CUT_PRN_DELV_CHL_H_ID},                 
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_LST", Value = ob.GMT_CUT_PRN_DELV_CHL_H_LST},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
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
                    i++;
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