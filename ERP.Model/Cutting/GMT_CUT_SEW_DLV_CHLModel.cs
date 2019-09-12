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
    public class GMT_CUT_SEW_DLV_CHLModel
    {
        public Int64 GMT_CUT_SEW_DLV_CHL_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64? HR_PROD_LINE_ID { get; set; }
        public string IS_TAG { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public Int64 NO_OF_BAG { get; set; }
       
        public string IS_FINALIZED { get; set; }
        public string GMT_CUT_PNL_BNK_LST { get; set; }
        public int? pOption { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp ="PKG_GMT_CUT_BANK.gmt_cut_sew_dlv_chl_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value = ob.GMT_CUT_SEW_DLV_CHL_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pIS_TAG", Value = ob.IS_TAG},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_LST", Value = ob.GMT_CUT_PNL_BNK_LST},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pNO_OF_BAG", Value = ob.NO_OF_BAG},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption??1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_CUT_SEW_DLV_CHL_ID", Value =0, Direction = ParameterDirection.Output}
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
        public List<GMT_CUT_SEW_DLV_CHLModel> Query(int pOption)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_sew_dlv_chl_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_SEW_DLV_CHLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_SEW_DLV_CHLModel ob = new GMT_CUT_SEW_DLV_CHLModel();
                            ob.GMT_CUT_SEW_DLV_CHL_ID = (dr["GMT_CUT_SEW_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SEW_DLV_CHL_ID"]);
                            ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                            if (dr["HR_PROD_LINE_ID"] != DBNull.Value)
                            {
                                ob.HR_PROD_LINE_ID = Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                            }
                            ob.IS_TAG = (dr["IS_TAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TAG"]);
                            ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                            ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                            ob.NO_OF_BAG = (dr["NO_OF_BAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BAG"]);
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

        public GMT_CUT_SEW_DLV_CHLModel Select(Int64 pGMT_CUT_SEW_DLV_CHL_ID, int pOption)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_sew_dlv_chl_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new GMT_CUT_SEW_DLV_CHLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value = pGMT_CUT_SEW_DLV_CHL_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.GMT_CUT_SEW_DLV_CHL_ID = (dr["GMT_CUT_SEW_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SEW_DLV_CHL_ID"]);
                        ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                        if (dr["HR_PROD_LINE_ID"] != DBNull.Value)
                        {
                            ob.HR_PROD_LINE_ID = Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                        }
                        ob.IS_TAG = (dr["IS_TAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TAG"]);
                        ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                        ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                        ob.NO_OF_BAG = (dr["NO_OF_BAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BAG"]);
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_SEW_DLV_CHLModel> getSewingDeliveryAutoCompl(string pCHALAN_NO, Int64? pGMT_CUT_SEW_DLV_CHL_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_sew_dlv_chl_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_SEW_DLV_CHLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value = pGMT_CUT_SEW_DLV_CHL_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_SEW_DLV_CHLModel ob = new GMT_CUT_SEW_DLV_CHLModel();
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.GMT_CUT_SEW_DLV_CHL_ID = (dr["GMT_CUT_SEW_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SEW_DLV_CHL_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    if (dr["HR_PROD_LINE_ID"] != DBNull.Value)
                    {
                        ob.HR_PROD_LINE_ID = Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                    }
                    ob.IS_TAG = (dr["IS_TAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TAG"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.NO_OF_BAG = (dr["NO_OF_BAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BAG"]);
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

        public List<dynamic> GetSewDelvChallanList(Int64? pGMT_CUT_SEW_DLV_CHL_ID = null, DateTime? pCHALAN_DT = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_ORDER_H_ID = null,
            Int64? pMC_COLOR_ID = null, Int64? pHR_PROD_LINE_ID = null, string pIS_TAG = null, string pIS_FINALIZED = null)
        {
            string sp = "pkg_gmt_cut_bank.gmt_cut_sew_dlv_chl_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value =pGMT_CUT_SEW_DLV_CHL_ID},
                    new CommandParameter() {ParameterName = "pCHALAN_DT", Value =pCHALAN_DT},

                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =pHR_PROD_LINE_ID},

                    new CommandParameter() {ParameterName = "pIS_TAG", Value =pIS_TAG},
                    new CommandParameter() {ParameterName = "pIS_FINALIZED", Value =pIS_FINALIZED},

                    new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {                        
                        CHALLAN_ROW_SPAN = (dr["CHALLAN_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHALLAN_ROW_SPAN"]),
                        CHALLAN_ROW_SL = (dr["CHALLAN_ROW_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHALLAN_ROW_SL"]),
                        CHALLAN_SL = (dr["CHALLAN_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CHALLAN_SL"]),
                        GMT_CUT_SEW_DLV_CHL_ID = (dr["GMT_CUT_SEW_DLV_CHL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_SEW_DLV_CHL_ID"]),
                        CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]),
                        BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]),
                        JOB_NO = (dr["JOB_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_NO"]),
                        ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]),
                        ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]),
                        COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]),
                        LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CUTNG_NO"]),
                        CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CUTNG_NO"]),
                        STATUS_NAME = (dr["STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STATUS_NAME"]),

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



    }




    
}