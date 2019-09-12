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

    public class GMT_SEW_PROD_SCAN_HR
    {
        public Int32 HOUR_NO { get; set; }
        public bool ACTIVE_HR { get; set; }
        public List<GMT_BNDL_CARD_VIEWMODELModel> items { get; set; }
        public Int64 QTY_IN_BNDL { get; set; }
    }

    public class GMT_SEW_PROD_SCAN_DModel
    {
        public Int64 GMT_SEW_PROD_SCAN_D_ID { get; set; }
        public Int64 GMT_SEW_PROD_SCAN_ID { get; set; }
        public Int64 RF_FB_DFCT_TYPE_ID { get; set; }
        public Int64 NO_OF_DFCT { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public List<GMT_SEW_PROD_SCAN_DModel> Query(Int64 pGMT_SEW_PROD_SCAN_ID)
        {
            string sp = "PKG_GMT_SEW_PROD.gmt_sew_prod_scan_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_SEW_PROD_SCAN_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_SEW_PROD_SCAN_ID", Value = pGMT_SEW_PROD_SCAN_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SEW_PROD_SCAN_DModel ob = new GMT_SEW_PROD_SCAN_DModel();
                    ob.GMT_SEW_PROD_SCAN_D_ID = (dr["GMT_SEW_PROD_SCAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SEW_PROD_SCAN_D_ID"]);
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.NO_OF_DFCT = (dr["NO_OF_DFCT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_DFCT"]);
                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DISPLAY_ORDER"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DISPLAY_ORDER { get; set; }

        public string FB_DFCT_TYPE_NAME { get; set; }
    }
    public class GMT_SEW_PROD_SCANModel
    {
        public Int64 GMT_SEW_PROD_SCAN_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64 HOUR_NO { get; set; }
        public Int64 GMT_CUT_PNL_BNK_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public Int64 REJECT_A { get; set; }
        public Int64 REJECT_B { get; set; }
        public Int64 REJECT_C { get; set; }
        public Int64 SEW_OUTPUT { get; set; }
        public Int64 HAS_REJECT { get; set; }
        public string IS_CLOSED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public List<GMT_SEW_PROD_SCAN_DModel> defects { get; set; }
        public string XML { get; set; }

        public Int32? pOption { get; set; }

        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_SEW_PROD.gmt_sew_prod_scan_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHAS_REJECT", Value = ob.HAS_REJECT},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pBARCODE", Value = ob.BARCODE},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption ?? 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_SEW_PROD_SCAN_ID", Value =0, Direction = ParameterDirection.Output}
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


        public List<GMT_SEW_PROD_SCAN_HR> FinSewProdScanHrlyData(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_SEW_PROD.gmt_sew_prod_scan_select";
            //pOption=3001=>Select Single Data
            try
            {
                var obList = new List<GMT_SEW_PROD_SCAN_HR>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SEW_PROD_SCAN_HR ob = new GMT_SEW_PROD_SCAN_HR();
                    ob.HOUR_NO = (dr["HOUR_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HOUR_NO"]);
                    ob.ACTIVE_HR = ob.HOUR_NO == Convert.ToInt32(dr["CUR_HOUR_NO"]);
                    ob.items = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                                         new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                                         new CommandParameter() {ParameterName = "pHOUR_NO", Value = ob.HOUR_NO },
                                         new CommandParameter() {ParameterName = "pOption", Value = 3003},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        GMT_BNDL_CARD_VIEWMODELModel ob2 = new GMT_BNDL_CARD_VIEWMODELModel();
                        ob2.LINE_CODE = (dr2["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["LINE_CODE"]);

                        ob2.LINE_CODE_SL = (dr2["LINE_CODE_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr2["LINE_CODE_SL"]);
                        ob2.LINE_CODE_SPAN = (dr2["LINE_CODE_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr2["LINE_CODE_SPAN"]);

                        ob2.BYR_ACC_GRP_NAME_EN = (dr2["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["BYR_ACC_GRP_NAME_EN"]);
                        ob2.STYLE_NO = (dr2["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["STYLE_NO"]);
                        ob2.WORK_STYLE_NO = (dr2["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["WORK_STYLE_NO"]);
                        ob2.ORDER_NO = (dr2["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ORDER_NO"]);
                        ob2.COLOR_NAME_EN = (dr2["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["COLOR_NAME_EN"]);
                        ob2.ITEM_NAME_EN = (dr2["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_NAME_EN"]);
                        ob2.QTY_IN_BNDL = (dr2["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["QTY_IN_BNDL"]);
                        ob2.LINE_WISE_PROD_QTY = (dr2["LINE_WISE_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["LINE_WISE_PROD_QTY"]);
                        ob.items.Add(ob2);
                    }
                    ob.QTY_IN_BNDL = ob.items.Sum(x => x.QTY_IN_BNDL);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<GMT_BNDL_CARD_VIEWMODELModel> GetSewProdScanSummery(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_SEW_PROD.gmt_sew_prod_scan_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_VIEWMODELModel ob = new GMT_BNDL_CARD_VIEWMODELModel();

                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_LINE_ID"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.TTL_INPUTED = (dr["TTL_INPUTED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_INPUTED"]);
                    ob.TTL_SEW_OUTPUT = (dr["TTL_SEW_OUTPUT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_SEW_OUTPUT"]);

                    ob.TTL_SEW_REJECTED = (dr["TTL_SEW_REJECTED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_SEW_REJECTED"]);

                    ob.TTL_BNDL_OK = (dr["TTL_BNDL_OK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_BNDL_OK"]);

                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    
                    ob.details = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                    ob.details = this.GetSewProdDetailsData(ob.HR_PROD_LINE_ID, ob.MC_ORDER_SHIP_ID, ob.MC_COLOR_ID, pGMT_PROD_PLN_CLNDR_ID);
                    ob.TTL_BNDL_PENDING = ob.details.Where(x => x.HAS_REJECT == 1).Count();
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<GMT_BNDL_CARD_VIEWMODELModel> GetSewProdDetailsData(Int32 pHR_PROD_LINE_ID, Int64 pMC_ORDER_SHIP_ID, Int64 pMC_COLOR_ID, Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_SEW_PROD.gmt_sew_prod_scan_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = pHR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = pMC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_VIEWMODELModel ob = new GMT_BNDL_CARD_VIEWMODELModel();
                    ob.GMT_SEW_PROD_SCAN_ID = (dr["GMT_SEW_PROD_SCAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SEW_PROD_SCAN_ID"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    ob.HAS_REJECT = (dr["HAS_REJECT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HAS_REJECT"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object BARCODE { get; set; }


        public List<GMT_SEW_PROD_SCANModel> GetSewProdDefectData(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_SEW_PROD.gmt_sew_prod_scan_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_SEW_PROD_SCANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SEW_PROD_SCANModel ob = new GMT_SEW_PROD_SCANModel();

                    ob.GMT_SEW_PROD_SCAN_ID = (dr["GMT_SEW_PROD_SCAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SEW_PROD_SCAN_ID"]);
                    ob.REJECT_A = (dr["REJECT_A"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_A"]);
                    ob.REJECT_B = (dr["REJECT_B"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_B"]);
                    ob.REJECT_C = (dr["REJECT_C"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_C"]);

                    ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                    ob.SEW_OUTPUT = (dr["SEW_OUTPUT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEW_OUTPUT"]);
                    ob.defects = new List<GMT_SEW_PROD_SCAN_DModel>();
                    ob.defects = new GMT_SEW_PROD_SCAN_DModel().Query(ob.GMT_SEW_PROD_SCAN_ID);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long SL { get; set; }

        public string SL_RANGE_TXT { get; set; }

        public string LINE_CODE { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string WORK_STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public long CUTNG_NO { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public string DYE_LOT_NO { get; set; }

        public string BUNDLE_BARCODE { get; set; }

        public string COUNTRY_NAME_EN { get; set; }

        public string SIZE_CODE { get; set; }

        public long BUNDLE_NO { get; set; }
    }
}