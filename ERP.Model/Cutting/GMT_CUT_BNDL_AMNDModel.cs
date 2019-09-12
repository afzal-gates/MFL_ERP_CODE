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
    public class GMT_CUT_BNDL_AMNDModel
    {
        public Int64 GMT_CUT_BNDL_AMND_ID { get; set; }
        public Int64? GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64? GMT_BNDL_CRD_PDATA_ID { get; set; }
        public Int64? SHRT_FRM_CUTNG { get; set; }
        public Int64? SHRT_FRM_PRN_EMB { get; set; }
        public Int64? SHRT_DTO_LOST { get; set; }
        public Int64? SRPL_DTO_CUT_BAL { get; set; }
        public Int64? SRPL_DTO_CUT_PNL_RPL { get; set; }
        public Int64? SRPL_DTO_PRN_EMB_BAL { get; set; }
        public string IS_CLOSED { get; set; }

        public string IS_LOK_S_CUTTING { get; set; }
        public string IS_LOK_S_PRNT_EMR { get; set; }
        public string IS_LOK_S_LOST { get; set; }
        public string IS_LOK_SP_CUT_BAL { get; set; }
        public string IS_LOK_SP_CUT_PNL_REP { get; set; }
        public string IS_LOCK_SP_PRNT_EMBR { get; set; }

        public string BUNDLE_BARCODE { get; set; }
        public string BNDL_AMND_XML { get; set; }


        public string SaveBundleCardAmnd()
        {
            const string sp = "pkg_cut_lay_chart.gmt_cut_bndl_amnd_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_BNDL_AMND_ID", Value = ob.GMT_CUT_BNDL_AMND_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pBUNDLE_BARCODE", Value = ob.BUNDLE_BARCODE},
                     new CommandParameter() {ParameterName = "pGMT_BNDL_CRD_PDATA_ID", Value = ob.GMT_BNDL_CRD_PDATA_ID},
                     new CommandParameter() {ParameterName = "pSHRT_FRM_CUTNG", Value = ob.SHRT_FRM_CUTNG},
                     new CommandParameter() {ParameterName = "pSHRT_FRM_PRN_EMB", Value = ob.SHRT_FRM_PRN_EMB},
                     new CommandParameter() {ParameterName = "pSHRT_DTO_LOST", Value = ob.SHRT_DTO_LOST},
                     new CommandParameter() {ParameterName = "pSRPL_DTO_CUT_BAL", Value = ob.SRPL_DTO_CUT_BAL},
                     new CommandParameter() {ParameterName = "pSRPL_DTO_CUT_PNL_RPL", Value = ob.SRPL_DTO_CUT_PNL_RPL},
                     new CommandParameter() {ParameterName = "pSRPL_DTO_PRN_EMB_BAL", Value = ob.SRPL_DTO_PRN_EMB_BAL},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value = ob.IS_CLOSED},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RemoveBundleCardAmnd()
        {
            const string sp = "pkg_cut_lay_chart.gmt_cut_bndl_amnd_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_BNDL_AMND_ID", Value = ob.GMT_CUT_BNDL_AMND_ID},
                                          
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public string FinalizeBundleCardAmnd()
        {
            const string sp = "pkg_cut_lay_chart.gmt_cut_bndl_amnd_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pBNDL_AMND_XML", Value = ob.BNDL_AMND_XML},
                     
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

        public List<GMT_CUT_BNDL_AMNDModel> SelectAll()
        {
            string sp = "Select_GMT_CUT_BNDL_AMND";
            try
            {
                var obList = new List<GMT_CUT_BNDL_AMNDModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_BNDL_AMND_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_BNDL_AMNDModel ob = new GMT_CUT_BNDL_AMNDModel();
                    ob.GMT_CUT_BNDL_AMND_ID = (dr["GMT_CUT_BNDL_AMND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_BNDL_AMND_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.GMT_BNDL_CRD_PDATA_ID = (dr["GMT_BNDL_CRD_PDATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_BNDL_CRD_PDATA_ID"]);
                    ob.SHRT_FRM_CUTNG = (dr["SHRT_FRM_CUTNG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_FRM_CUTNG"]);
                    ob.SHRT_FRM_PRN_EMB = (dr["SHRT_FRM_PRN_EMB"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_FRM_PRN_EMB"]);
                    ob.SHRT_DTO_LOST = (dr["SHRT_DTO_LOST"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_DTO_LOST"]);
                    ob.SRPL_DTO_CUT_BAL = (dr["SRPL_DTO_CUT_BAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_CUT_BAL"]);
                    ob.SRPL_DTO_CUT_PNL_RPL = (dr["SRPL_DTO_CUT_PNL_RPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_CUT_PNL_RPL"]);
                    ob.SRPL_DTO_PRN_EMB_BAL = (dr["SRPL_DTO_PRN_EMB_BAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_PRN_EMB_BAL"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CLOSED"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CUT_BNDL_AMNDModel Select(long ID)
        {
            string sp = "Select_GMT_CUT_BNDL_AMND";
            try
            {
                var ob = new GMT_CUT_BNDL_AMNDModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_BNDL_AMND_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CUT_BNDL_AMND_ID = (dr["GMT_CUT_BNDL_AMND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_BNDL_AMND_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.GMT_BNDL_CRD_PDATA_ID = (dr["GMT_BNDL_CRD_PDATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_BNDL_CRD_PDATA_ID"]);
                    ob.SHRT_FRM_CUTNG = (dr["SHRT_FRM_CUTNG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_FRM_CUTNG"]);
                    ob.SHRT_FRM_PRN_EMB = (dr["SHRT_FRM_PRN_EMB"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_FRM_PRN_EMB"]);
                    ob.SHRT_DTO_LOST = (dr["SHRT_DTO_LOST"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_DTO_LOST"]);
                    ob.SRPL_DTO_CUT_BAL = (dr["SRPL_DTO_CUT_BAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_CUT_BAL"]);
                    ob.SRPL_DTO_CUT_PNL_RPL = (dr["SRPL_DTO_CUT_PNL_RPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_CUT_PNL_RPL"]);
                    ob.SRPL_DTO_PRN_EMB_BAL = (dr["SRPL_DTO_PRN_EMB_BAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_PRN_EMB_BAL"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CLOSED"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



    public class GMT_BNDL_CARD_AMND_VIEWModel
    {
        public Int64 GMT_CUT_BNDL_AMND_ID { get; set; }
        public Int64 GMT_CUT_INFO_ID { get; set; }
        public Int64 SL { get; set; }        
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string COUNTRY_NAME_EN { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }
        public Int64 CUTNG_NO { get; set; }
        public Int64 BUNDLE_NO { get; set; }
        public string BUNDLE_BARCODE { get; set; }
        public string SL_RANGE_TXT { get; set; }
        public Int64 QTY_IN_BNDL { get; set; }
        public string DYE_LOT_NO { get; set; }
        public Int64? TOT_REC { get; set; }
        public Int64? SHRT_FRM_CUTNG { get; set; }
        public Int64? SHRT_FRM_PRN_EMB { get; set; }
        public Int64? SHRT_DTO_LOST { get; set; }
        public Int64? SRPL_DTO_CUT_BAL { get; set; }
        public Int64? SRPL_DTO_CUT_PNL_RPL { get; set; }
        public Int64? SRPL_DTO_PRN_EMB_BAL { get; set; }
        public string IS_LOK_S_CUTTING { get; set; }
        public string IS_LOK_S_PRNT_EMR { get; set; }
        public string IS_LOK_S_LOST { get; set; }
        public string IS_LOK_SP_CUT_BAL { get; set; }
        public string IS_LOK_SP_CUT_PNL_REP { get; set; }
        public string IS_LOCK_SP_PRNT_EMBR { get; set; }
        public string IS_DATA_ITEM_CHANGED { get; set; }
        public List<GMT_BNDL_CARD_AMND_VIEWModel> bndlAmndDtl { get; set; }


        public GMT_BNDL_CARD_AMND_VIEWModel GetLastBndlAmndInfo(Int64 pGMT_CUT_BNDL_AMND_ID)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            
            try
            {
                var ob = new GMT_BNDL_CARD_AMND_VIEWModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_BNDL_AMND_ID", Value = pGMT_CUT_BNDL_AMND_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CUT_BNDL_AMND_ID = (dr["GMT_CUT_BNDL_AMND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_BNDL_AMND_ID"]);                    
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_BNDL_CARD_AMND_VIEWModel> GetBndlAmndList(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";

            try
            {
                var obList = new List<GMT_BNDL_CARD_AMND_VIEWModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID}, 
                     //new CommandParameter() {ParameterName = "pIS_CLOSED", Value = pIS_CLOSED},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3010},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_AMND_VIEWModel ob = new GMT_BNDL_CARD_AMND_VIEWModel();

                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                    
                    ob.TOT_REC = (dr["TOT_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_REC"]);
                    
                    var obBndlAmndDtl = new GMT_BNDL_CARD_AMND_VIEWModel().GetBndlAmndDtlList(ob.GMT_CUT_INFO_ID, pGMT_PROD_PLN_CLNDR_ID);
                    ob.bndlAmndDtl = (List<GMT_BNDL_CARD_AMND_VIEWModel>)obBndlAmndDtl;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_BNDL_CARD_AMND_VIEWModel> GetBndlAmndDtlList(Int64 pGMT_CUT_INFO_ID, Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            Int64 vSL = 0;

            try
            {
                var obList = new List<GMT_BNDL_CARD_AMND_VIEWModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pOption", Value = 3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_AMND_VIEWModel ob = new GMT_BNDL_CARD_AMND_VIEWModel();

                    vSL++;
                    ob.SL = vSL;

                    ob.GMT_CUT_BNDL_AMND_ID = (dr["GMT_CUT_BNDL_AMND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_BNDL_AMND_ID"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    ob.SHRT_FRM_CUTNG = (dr["SHRT_FRM_CUTNG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_FRM_CUTNG"]);
                    ob.SHRT_FRM_PRN_EMB = (dr["SHRT_FRM_PRN_EMB"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_FRM_PRN_EMB"]);
                    ob.SHRT_DTO_LOST = (dr["SHRT_DTO_LOST"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_DTO_LOST"]);
                    ob.SRPL_DTO_CUT_BAL = (dr["SRPL_DTO_CUT_BAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_CUT_BAL"]);
                    ob.SRPL_DTO_CUT_PNL_RPL = (dr["SRPL_DTO_CUT_PNL_RPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_CUT_PNL_RPL"]);
                    ob.SRPL_DTO_PRN_EMB_BAL = (dr["SRPL_DTO_PRN_EMB_BAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_PRN_EMB_BAL"]);

                    ob.IS_LOK_S_CUTTING = (dr["IS_LOK_S_CUTTING"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_S_CUTTING"]);
                    ob.IS_LOK_S_PRNT_EMR = (dr["IS_LOK_S_PRNT_EMR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_S_PRNT_EMR"]);
                    ob.IS_LOK_S_LOST = (dr["IS_LOK_S_LOST"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_S_LOST"]);
                    ob.IS_LOK_SP_CUT_BAL = (dr["IS_LOK_SP_CUT_BAL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_SP_CUT_BAL"]);
                    ob.IS_LOK_SP_CUT_PNL_REP = (dr["IS_LOK_SP_CUT_PNL_REP"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_SP_CUT_PNL_REP"]);
                    ob.IS_LOCK_SP_PRNT_EMBR = (dr["IS_LOCK_SP_PRNT_EMBR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOCK_SP_PRNT_EMBR"]);

                    ob.IS_DATA_ITEM_CHANGED = (dr["IS_DATA_ITEM_CHANGED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_DATA_ITEM_CHANGED"]);                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_BNDL_CARD_AMND_VIEWModel> GetBndlAmndList4Finalize(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            Int64 vSL = 0;

            try
            {
                var obList = new List<GMT_BNDL_CARD_AMND_VIEWModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pOption", Value = 3012},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_AMND_VIEWModel ob = new GMT_BNDL_CARD_AMND_VIEWModel();

                    vSL++;
                    ob.SL = vSL;

                    ob.GMT_CUT_BNDL_AMND_ID = (dr["GMT_CUT_BNDL_AMND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_BNDL_AMND_ID"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    ob.SHRT_FRM_CUTNG = (dr["SHRT_FRM_CUTNG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_FRM_CUTNG"]);
                    ob.SHRT_FRM_PRN_EMB = (dr["SHRT_FRM_PRN_EMB"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_FRM_PRN_EMB"]);
                    ob.SHRT_DTO_LOST = (dr["SHRT_DTO_LOST"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHRT_DTO_LOST"]);
                    ob.SRPL_DTO_CUT_BAL = (dr["SRPL_DTO_CUT_BAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_CUT_BAL"]);
                    ob.SRPL_DTO_CUT_PNL_RPL = (dr["SRPL_DTO_CUT_PNL_RPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_CUT_PNL_RPL"]);
                    ob.SRPL_DTO_PRN_EMB_BAL = (dr["SRPL_DTO_PRN_EMB_BAL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SRPL_DTO_PRN_EMB_BAL"]);

                    ob.IS_LOK_S_CUTTING = (dr["IS_LOK_S_CUTTING"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_S_CUTTING"]);
                    ob.IS_LOK_S_PRNT_EMR = (dr["IS_LOK_S_PRNT_EMR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_S_PRNT_EMR"]);
                    ob.IS_LOK_S_LOST = (dr["IS_LOK_S_LOST"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_S_LOST"]);
                    ob.IS_LOK_SP_CUT_BAL = (dr["IS_LOK_SP_CUT_BAL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_SP_CUT_BAL"]);
                    ob.IS_LOK_SP_CUT_PNL_REP = (dr["IS_LOK_SP_CUT_PNL_REP"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOK_SP_CUT_PNL_REP"]);
                    ob.IS_LOCK_SP_PRNT_EMBR = (dr["IS_LOCK_SP_PRNT_EMBR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LOCK_SP_PRNT_EMBR"]);

                    ob.IS_DATA_ITEM_CHANGED = (dr["IS_DATA_ITEM_CHANGED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_DATA_ITEM_CHANGED"]); 

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