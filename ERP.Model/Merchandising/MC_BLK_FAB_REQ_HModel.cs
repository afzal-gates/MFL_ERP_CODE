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
    public class MC_BLK_FAB_REQ_HModel
    {
        public Int64? MC_BLK_FAB_REQ_H_ID { get; set; }
        public string BLK_FAB_REQ_NO { get; set; }

        [Required(ErrorMessage = "Please select booking date")]
        public DateTime? BLK_FAB_REQ_DT { get; set; }
        public Int64? BLK_FAB_REQ_BY { get; set; }

        [Required(ErrorMessage = "Please select buyer a/c")]
        public Int64? MC_BYR_ACC_ID { get; set; }

        [Required(ErrorMessage = "Please select style #")]
        public Int64? MC_STYLE_H_EXT_ID { get; set; }

        public Int64? MC_STYLE_H_ID { get; set; }


        [Required(ErrorMessage = "Please select work style #")]
        public string MC_ORDER_H_ID_LST { get; set; }
        [Required(ErrorMessage = "Please select item/model #")]
        public string STYLE_D_ITEM_LST { get; set; }

        //[Range(0,100,ErrorMessage="Cuting wastage should be between {1} and {2}")]
        //[Required(ErrorMessage = "Please input YD wastage %")]
        public Decimal? PYD_WSTG { get; set; }
        public Decimal? PAOP_WSTG { get; set; }
        public Decimal? PCUT_WSTG { get; set; }

        public Int64? REVISION_NO { get; set; }
        public DateTime? REVISION_DT { get; set; }
        public string REV_REASON { get; set; }
        public string IS_BFB_FINALIZED { get; set; }

        public string REMARKS_TOP { get; set; }
        public string REMARKS { get; set; }
        public string FAB_PROD_CAT_SNAME { get; set; }
        public Int64? RF_CURRENCY_ID { get; set; }

        //[Required(ErrorMessage = "Please input currency conversion for local currency")]
        public Decimal? CURR_CONV_LOC { get; set; }
        public Int64? CREATED_BY { get; set; }

        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO_LST { get; set; }
        public string ORDER_NO_LST { get; set; }
        public decimal TOT_ORD_VALUE { get; set; }
        public Int64? TOT_ORD_QTY { get; set; }

        public string BFBK_STATUS_REMARKS { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public Int64? MC_TNA_TASK_STATUS_ID { get; set; }
        public string TASK_STATUS_NAME { get; set; }
        public string ACTION_USER_TYPE { get; set; }
        public string HAS_PLOSS { get; set; }
        public string IS_YD { get; set; }
        public string IS_MULTI_SHIP_DT { get; set; }
        public List<MC_BLK_FAB_REQ_DModel> fabReqDtl { get; set; }
        public string MC_BLK_FAB_REQ_D_XML { get; set; }
        public string MC_BLK_FAB_REQ_CAL_XML { get; set; }
        public string EMPLOYEE_PROFILE { get; set; }
        public Int64? ORD_QTY_MOU { get; set; }
        public List<MC_BLK_FAB_REVISIONModel> REVISION_LIST { get; set; }

        public Int64 MC_STYL_BGT_H_ID { get; set; }
        public Int64? BGT_REVISION_NO { get; set; }
        public DateTime? ACTION_DATE { get; set; }
        public DateTime? SHIP_DT { get; set; }
        public string EMAIL_TO_LST { get; set; }





        public string BatchSaveBulkFabBooking()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pBLK_FAB_REQ_NO", Value = ob.BLK_FAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pBLK_FAB_REQ_DT", Value = ob.BLK_FAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pBLK_FAB_REQ_BY", Value = ob.BLK_FAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCURR_CONV_LOC", Value = ob.CURR_CONV_LOC},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pSTYLE_D_ITEM_LST", Value = ob.STYLE_D_ITEM_LST},
                     new CommandParameter() {ParameterName = "pPCUT_WSTG", Value = ob.PCUT_WSTG},
                     new CommandParameter() {ParameterName = "pPAOP_WSTG", Value = ob.PAOP_WSTG},
                     new CommandParameter() {ParameterName = "pPYD_WSTG", Value = ob.PYD_WSTG},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pREMARKS_TOP", Value = ob.REMARKS_TOP},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_D_XML", Value = ob.MC_BLK_FAB_REQ_D_XML},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_CAL_XML", Value = ob.MC_BLK_FAB_REQ_CAL_XML},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "PMC_BLK_FAB_REQ_H_ID_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "PBLK_FAB_REQ_NO_RTN", Direction = ParameterDirection.Output}
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



        public List<MC_BLK_FAB_REQ_HModel> SelectAll()
        {
            string sp = "Select_MC_BLK_FAB_REQ_H";
            try
            {
                var obList = new List<MC_BLK_FAB_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.BLK_FAB_REQ_NO = (dr["BLK_FAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_FAB_REQ_NO"]);
                    ob.BLK_FAB_REQ_DT = (dr["BLK_FAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BLK_FAB_REQ_DT"]);
                    ob.BLK_FAB_REQ_BY = (dr["BLK_FAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_FAB_REQ_BY"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    ob.PYD_WSTG = (dr["PYD_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PYD_WSTG"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BLK_FAB_REQ_HModel Select(long? ID, string pIS_EMAIL_NOTIF = null)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                var ob = new MC_BLK_FAB_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pIS_EMAIL_NOTIF", Value =pIS_EMAIL_NOTIF},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.BLK_FAB_REQ_NO = (dr["BLK_FAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_FAB_REQ_NO"]);
                    ob.BLK_FAB_REQ_DT = (dr["BLK_FAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BLK_FAB_REQ_DT"]);
                    ob.BLK_FAB_REQ_BY = (dr["BLK_FAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_FAB_REQ_BY"]);
                    ob.EMPLOYEE_PROFILE = (dr["EMPLOYEE_PROFILE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_PROFILE"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);

                    if (dr["PCUT_WSTG"] != DBNull.Value)
                        ob.PCUT_WSTG = ((dr["PCUT_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCUT_WSTG"]));

                    if (dr["PYD_WSTG"] != DBNull.Value)
                        ob.PYD_WSTG = ((dr["PYD_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PYD_WSTG"]));

                    if (dr["PAOP_WSTG"] != DBNull.Value)
                        ob.PAOP_WSTG = ((dr["PAOP_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAOP_WSTG"]));

                    if (dr["REVISION_NO"] != DBNull.Value)
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);

                    ob.IS_BFB_FINALIZED = (dr["IS_BFB_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BFB_FINALIZED"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);

                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REMARKS_TOP = (dr["REMARKS_TOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_TOP"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);
                    ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.HAS_PLOSS = (dr["HAS_PLOSS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_PLOSS"]);

                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    //ob.BGT_REVISION_NO = (dr["BGT_REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BGT_REVISION_NO"]);

                    ob.EMAIL_TO_LST = (dr["EMAIL_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_TO_LST"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BLK_FAB_REQ_HModel SelectForBudgetSheet(long ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                var ob = new MC_BLK_FAB_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.BLK_FAB_REQ_NO = (dr["BLK_FAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_FAB_REQ_NO"]);
                    ob.BLK_FAB_REQ_DT = (dr["BLK_FAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BLK_FAB_REQ_DT"]);
                    ob.BLK_FAB_REQ_BY = (dr["BLK_FAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_FAB_REQ_BY"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.ORD_QTY_MOU = (dr["ORD_QTY_MOU"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORD_QTY_MOU"]);
                    ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_ORD_VALUE"]);

                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    if (dr["PYD_WSTG"] != DBNull.Value)
                        ob.PYD_WSTG = ((dr["PYD_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PYD_WSTG"]));
                    if (dr["REVISION_NO"] != DBNull.Value)
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);

                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_PAGERModel BulkFabBookingList(Int64? pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_ID, Int64? pMC_STYLE_H_EXT_ID, Int64 pageNumber, Int64 pageSize,
                                string pWORK_STYLE_NO_LST = null, string pORDER_NO_LST = null, string pBLK_FAB_REQ_NO = null, Int64? pMC_BYR_ACC_GRP_ID = null)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<MC_BLK_FAB_REQ_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pBLK_FAB_REQ_NO", Value = pBLK_FAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pWORK_STYLE_NO_LST},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO_LST},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.BLK_FAB_REQ_NO = (dr["BLK_FAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_FAB_REQ_NO"]);
                    ob.BLK_FAB_REQ_DT = (dr["BLK_FAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BLK_FAB_REQ_DT"]);
                    ob.BLK_FAB_REQ_BY = (dr["BLK_FAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_FAB_REQ_BY"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    ob.PYD_WSTG = (dr["PYD_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PYD_WSTG"]);
                    ob.PAOP_WSTG = (dr["PAOP_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAOP_WSTG"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO_LST = (dr["WORK_STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO_LST"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);
                    ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);

                    var obRevisionList = new MC_BLK_FAB_REVISIONModel().RevisionList(Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]));
                    ob.REVISION_LIST = (List<MC_BLK_FAB_REVISIONModel>)obRevisionList;

                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.BGT_REVISION_NO = (dr["BGT_REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BGT_REVISION_NO"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_PAGERModel ApprovedBlkFabBkList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_ID, Int64? pMC_STYLE_H_EXT_ID, 
                                string pWORK_STYLE_NO_LST = null, string pORDER_NO_LST = null, string pBLK_FAB_REQ_NO = null, Int64? pMC_BYR_ACC_GRP_ID = null,
            DateTime? pSHIP_FROM_DATE = null, DateTime? pSHIP_TO_DATE = null, DateTime? pFROM_DATE = null, DateTime? pTO_DATE = null)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<MC_BLK_FAB_REQ_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pBLK_FAB_REQ_NO", Value = pBLK_FAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pWORK_STYLE_NO_LST},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO_LST},

                     new CommandParameter() {ParameterName = "pSHIP_FROM_DATE", Value = pSHIP_FROM_DATE},
                     new CommandParameter() {ParameterName = "pSHIP_TO_DATE", Value = pSHIP_TO_DATE},

                     new CommandParameter() {ParameterName = "pFROM_DATE", Value = pFROM_DATE},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value = pTO_DATE},
                     
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3020},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();

                    ob.ACTION_DATE = (dr["ACTION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTION_DATE"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.BLK_FAB_REQ_NO = (dr["BLK_FAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_FAB_REQ_NO"]);
                    ob.BLK_FAB_REQ_DT = (dr["BLK_FAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BLK_FAB_REQ_DT"]);
                    ob.BLK_FAB_REQ_BY = (dr["BLK_FAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_FAB_REQ_BY"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    ob.PYD_WSTG = (dr["PYD_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PYD_WSTG"]);
                    ob.PAOP_WSTG = (dr["PAOP_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAOP_WSTG"]);

                    if (dr["REVISION_NO"] != DBNull.Value)
                    {
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                        ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    }

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO_LST = (dr["WORK_STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO_LST"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);
                    ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);

                    var obRevisionList = new MC_BLK_FAB_REVISIONModel().RevisionList(Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]));
                    ob.REVISION_LIST = (List<MC_BLK_FAB_REVISIONModel>)obRevisionList;

                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.BGT_REVISION_NO = (dr["BGT_REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BGT_REVISION_NO"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ProcessBulkFabBooking()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_fab_req_process";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},                                          
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                                          
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

        public string SaveBulkFabFinalize(long pMC_BLK_FAB_REQ_H_ID)
        {
            const string sp = "pkg_mc_fab_booking.save_blk_fab_finalize";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =  500, Direction = ParameterDirection.Output}
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

        public string Send4BulkBudgetAprvl(long pMC_BLK_FAB_REQ_H_ID)
        {
            const string sp = "pkg_mc_fab_booking.save_send4bulk_budget_aprvl";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =  500, Direction = ParameterDirection.Output}
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

        public object GetBulkFabAprvlPendingList(Int64? pMC_BYR_ACC_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                var obList = new List<MC_BLK_FAB_REQ_HModel>();
                var obAprvr = new List<MC_BFBK_APRVL_VM_Model>();

                Int64 vLoginEmpId = Convert.ToInt64(HttpContext.Current.Session["multiLoginEmpId"]);

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},                    
                     new CommandParameter() {ParameterName = "pBLK_FAB_REQ_BY", Value = vLoginEmpId},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = vLoginEmpId},
                     new CommandParameter() {ParameterName = "pOption", Value = 3019},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.BLK_FAB_REQ_NO = (dr["BLK_FAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_FAB_REQ_NO"]);
                    ob.BLK_FAB_REQ_DT = (dr["BLK_FAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BLK_FAB_REQ_DT"]);
                    ob.BLK_FAB_REQ_BY = (dr["BLK_FAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_FAB_REQ_BY"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    ob.PYD_WSTG = (dr["PYD_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PYD_WSTG"]);
                    ob.PAOP_WSTG = (dr["PAOP_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAOP_WSTG"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO_LST = (dr["WORK_STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO_LST"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);
                    //ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);
                    //ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);

                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.BGT_REVISION_NO = (dr["BGT_REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BGT_REVISION_NO"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.BFBK_STATUS_REMARKS = (dr["BFBK_STATUS_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BFBK_STATUS_REMARKS"]);

                    var obRevisionList = new MC_BLK_FAB_REVISIONModel().RevisionList(Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]));
                    ob.REVISION_LIST = (List<MC_BLK_FAB_REVISIONModel>)obRevisionList;

                    obList.Add(ob);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    MC_BFBK_APRVL_VM_Model ob = new MC_BFBK_APRVL_VM_Model();

                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.STATUS_ORDER = (dr["STATUS_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STATUS_ORDER"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);

                    obAprvr.Add(ob);
                }

                return new { obList, obAprvr };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Send4BulkBudgetAprvl(string pXML, int pLK_BFBK_STATUS_ID)
        {
            const string sp = "pkg_mc_fab_booking.save_send4bulk_budget_aprvl";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                int vOption = 0;

                if (pLK_BFBK_STATUS_ID == 33) //Recomend
                    vOption = 1001;
                else if (pLK_BFBK_STATUS_ID == 35) //Resend
                    vOption = 1002;
                else if (pLK_BFBK_STATUS_ID == 36) //Approve
                    vOption = 1003;
                else if (pLK_BFBK_STATUS_ID == 0) //Clear
                    vOption = 1004;

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = pXML},                     
                     //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value = vOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =  500, Direction = ParameterDirection.Output}
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

        public string MailSendSuccessfully4BulkBudgetAprvl(long? pMC_BLK_FAB_REQ_H_ID, string pIS_MAIL_SEND)
        {
            const string sp = "pkg_mc_fab_booking.save_send4bulk_budget_aprvl";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},                    
                     new CommandParameter() {ParameterName = "pIS_MAIL_SEND", Value = pIS_MAIL_SEND},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1005},
                     new CommandParameter() {ParameterName = "pMsg", Value =  500, Direction = ParameterDirection.Output}
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

        public List<MC_BLK_FAB_REQ_HModel> GetMailSendPendingListBulk()
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                var obList = new List<MC_BLK_FAB_REQ_HModel>();
                

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3021},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);                                       
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    
                    obList.Add(ob);
                }
                
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object getBfbkBudgetNotif(int pageNumber, int pageSize, string pSTYLE_NO = null, string pBYR_ACC_NAME_EN = null, DateTime? pBLK_FAB_REQ_DT = null)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<MC_BLK_FAB_REQ_HModel>();
                var obAprvr = new List<MC_BFBK_APRVL_VM_Model>();
                var obj = new RF_PAGERModel();


                Int64 vLoginEmpId = Convert.ToInt64(HttpContext.Current.Session["multiLoginEmpId"]);

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},                    
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},                     
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value = pBYR_ACC_NAME_EN},
                     new CommandParameter() {ParameterName = "pBLK_FAB_REQ_DT", Value = pBLK_FAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                     new CommandParameter() {ParameterName = "pOption", Value = 3019},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();
                    
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.ACTION_DATE = (dr["ACTION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTION_DATE"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.BLK_FAB_REQ_NO = (dr["BLK_FAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_FAB_REQ_NO"]);
                    ob.BLK_FAB_REQ_DT = (dr["BLK_FAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BLK_FAB_REQ_DT"]);
                    ob.BLK_FAB_REQ_BY = (dr["BLK_FAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_FAB_REQ_BY"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    ob.PYD_WSTG = (dr["PYD_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PYD_WSTG"]);
                    ob.PAOP_WSTG = (dr["PAOP_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAOP_WSTG"]);
                    if (dr["REVISION_NO"] != DBNull.Value)
                    {
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                        ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    }
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO_LST = (dr["WORK_STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO_LST"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);
                    //ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);
                    //ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);

                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.BGT_REVISION_NO = (dr["BGT_REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BGT_REVISION_NO"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);
                    ob.BFBK_STATUS_REMARKS = (dr["BFBK_STATUS_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BFBK_STATUS_REMARKS"]);

                    var obRevisionList = new MC_BLK_FAB_REVISIONModel().RevisionList(Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]));
                    ob.REVISION_LIST = (List<MC_BLK_FAB_REVISIONModel>)obRevisionList;

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    MC_BFBK_APRVL_VM_Model ob = new MC_BFBK_APRVL_VM_Model();

                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.STATUS_ORDER = (dr["STATUS_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STATUS_ORDER"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);

                    obAprvr.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                //obj.obAprvr = obAprvr;
                //return obj;

                return new { obj, obAprvr };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        
    }



    public class MC_BLK_FAB_DATAModel
    {
        public Int64? MC_BLK_FAB_DATA_ID { get; set; }
        public string REQ_FAB_TYPE_NAME { get; set; }
        public string MC_ROW_CAT_NAME { get; set; }
        public Int64? MC_ROW_CAT_ID { get; set; }
        public string MC_BLK_FAB_DISPLAY_ORDER_XML { get; set; }


        public List<MC_BLK_FAB_DATAModel> ReportRowCategoryList(Int64 pMC_BLK_FAB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                var obList = new List<MC_BLK_FAB_DATAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},                                          
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_DATAModel ob = new MC_BLK_FAB_DATAModel();
                    ob.MC_BLK_FAB_DATA_ID = (dr["MC_BLK_FAB_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_DATA_ID"]);
                    ob.REQ_FAB_TYPE_NAME = (dr["REQ_FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_FAB_TYPE_NAME"]);
                    ob.MC_ROW_CAT_NAME = (dr["MC_ROW_CAT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ROW_CAT_NAME"]);
                    ob.MC_ROW_CAT_ID = (dr["MC_ROW_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ROW_CAT_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string BatchSaveBulkFabRptDisplayOrder()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_fab_data_save";
            string jsonStr = "{";
            var ob = this;
            //ob.MC_BLK_COL_REQ_XML = pMC_BLK_COL_REQ_XML;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_DISPLAY_ORDER_XML", Value = ob.MC_BLK_FAB_DISPLAY_ORDER_XML},
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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


    public class MC_BLK_FAB_REVISIONModel
    {
        public Int64? MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64? REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public string REV_REASON { get; set; }


        public string SaveBlkBookingRevision()
        {
            const string sp = "pkg_mc_fab_booking.blk_fab_revision_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;


            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
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

        public List<MC_BLK_FAB_REVISIONModel> RevisionList(long pMC_BLK_FAB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                var obList = new List<MC_BLK_FAB_REVISIONModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REVISIONModel ob = new MC_BLK_FAB_REVISIONModel();
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REV_REASON = (dr["REVISION_NO_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REVISION_NO_NAME"]);

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


    public class MC_BFBK_APRVL_VM_Model
    {
        public Int64? SC_USER_ID { get; set; }
        public Int64? MC_TNA_TASK_STATUS_ID { get; set; }
        public Int64? STATUS_ORDER { get; set; }
        public string ACTION_USER_TYPE { get; set; }


        public object GetBulkFabAprvlLvl4LoginUser()
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_h_select";
            try
            {
                Int64 vLoginEmpId = Convert.ToInt64(HttpContext.Current.Session["multiLoginEmpId"]);

                var ob = new MC_BLK_FAB_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = vLoginEmpId},
                     new CommandParameter() {ParameterName = "pOption", Value =3020},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.BLK_FAB_REQ_NO = (dr["BLK_FAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLK_FAB_REQ_NO"]);
                    ob.BLK_FAB_REQ_DT = (dr["BLK_FAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BLK_FAB_REQ_DT"]);
                    ob.BLK_FAB_REQ_BY = (dr["BLK_FAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BLK_FAB_REQ_BY"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.ORD_QTY_MOU = (dr["ORD_QTY_MOU"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORD_QTY_MOU"]);
                    ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_ORD_VALUE"]);

                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    if (dr["PYD_WSTG"] != DBNull.Value)
                        ob.PYD_WSTG = ((dr["PYD_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PYD_WSTG"]));
                    if (dr["REVISION_NO"] != DBNull.Value)
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);

                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);

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