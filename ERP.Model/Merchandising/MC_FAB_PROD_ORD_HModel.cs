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
    public class MC_FAB_PROD_ORD_HModel
    {
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 LK_FAB_PROD_CAT_ID { get; set; }
        public Int64 MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public string MC_ORDER_H_ID_LST { get; set; }
        public Int64 LK_FAB_QTY_SRC_ID { get; set; }
        public DateTime PROD_ORD_DT { get; set; }
        public DateTime REV_PROD_ORD_DT { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string BUYER_NAME_EN { get; set; }
        public string STYLE_DESC { get; set; }
        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string PRODUCTION_CAT { get; set; }
        public string FAB_PROD_D_XML { get; set; }

        public string ORDER_NO { get; set; }
        public string ORDER_TYPE { get; set; }
        public long TOT_ORD_QTY { get; set; }
        public DateTime SHIP_DT { get; set; }

        public Int64 MC_BYR_ACC_ID { get; set; }
        public string SHIP_DT_STR { get; set; }
        public long RF_FAB_PROD_CAT_ID { get; set; }
        public long TOTAL_REC { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }

        public string MONTHOF { get; set; }
        public DateTime FIRSTDATE { get; set; }
        public DateTime LASTDATE { get; set; }
        public long NO_OF_DTL { get; set; }
        public long KC_TO_DO { get; set; }
        public long KP_TO_DO { get; set; }
        public long HAS_YD { get; set; }
        public string HAS_COL_CUFF { get; set; }
        public string LK_COL_TYPE_ID_LST { get; set; }
        public long? REVISION_NO { get; set; }
        public long MC_STYLE_H_ID { get; set; }
        public string REASON_SHRT { get; set; }
        public string SHARE_SHRT { get; set; }
        public DateTime? LAST_REV_DT { get; set; }
        public long? LAST_REV_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public long MC_BUYER_ID { get; set; }

        private List<STYLE_ITEM_IMAGE> _images = null;
        public List<STYLE_ITEM_IMAGE> images
        {
            get
            {
                if (_images == null)
                {
                    _images = new List<STYLE_ITEM_IMAGE>();
                }
                return _images;
            }
            set
            {
                _images = value;
            }
        }
        
        private List<FAB_BKING_RPTModel> _fabBkingRpt = null;
        public List<FAB_BKING_RPTModel> fabBkingRpt
        {
            get
            {
                if (_fabBkingRpt == null)
                {
                    _fabBkingRpt = new List<FAB_BKING_RPTModel>();
                }
                return _fabBkingRpt;
            }
            set
            {
                _fabBkingRpt = value;
            }
        }


        public List<MC_FAB_PROD_ORD_HModel> SelectAll(
             int pMC_BYR_ACC_ID,
             int pMC_STYLE_H_ID,
             int pRF_FAB_PROD_CAT_ID,
             DateTime? pFIRSTDATE,
             int pageNo,
             int pageSize,
             string pHAS_KP,
             string pHAS_COL_CUFF,
             string pLK_COL_TYPE_ID_LST,
             Int32? pRF_FAB_TYPE_ID
        )
        {
            string sp = "pkg_fab_prod_order.MC_fab_prod_ord_h_select";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value =pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pHAS_KP", Value =pHAS_KP},
                     new CommandParameter() {ParameterName = "pHAS_COL_CUFF", Value =pHAS_COL_CUFF},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID_LST", Value =pLK_COL_TYPE_ID_LST},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_HModel ob = new MC_FAB_PROD_ORD_HModel();
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    if (dr["LAST_REV_NO"] != DBNull.Value)
                    {
                        ob.LAST_REV_NO = Convert.ToInt64(dr["LAST_REV_NO"]);
                    }

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.LK_FAB_QTY_SRC_ID = (dr["LK_FAB_QTY_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_QTY_SRC_ID"]);
                    ob.PROD_ORD_DT = (dr["PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_ORD_DT"]);
                    //ob.REV_PROD_ORD_DT = (dr["REV_PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REV_PROD_ORD_DT"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.PRODUCTION_CAT = (dr["PRODUCTION_CAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCTION_CAT"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_TYPE = (dr["ORDER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    ob.SHIP_DT_STR = (dr["SHIP_DT_STR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_DT_STR"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    ob.NO_OF_DTL = (dr["NO_OF_DTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_DTL"]);
                    ob.KP_TO_DO = (dr["KP_TO_DO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KP_TO_DO"]);
                    ob.KC_TO_DO = (dr["KC_TO_DO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KC_TO_DO"]);

                    ob.HAS_YD = (dr["HAS_YD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HAS_YD"]);

                    ob.HAS_COL_CUFF = pHAS_COL_CUFF;
                    ob.LK_COL_TYPE_ID_LST = pLK_COL_TYPE_ID_LST;

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ob.MC_STYLE_H_ID},
                             new CommandParameter() {ParameterName = "pOption", Value =3003},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        STYLE_ITEM_IMAGE ob1 = new STYLE_ITEM_IMAGE();
                        ob1.STYL_KEY_IMG = (dr1["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STYL_KEY_IMG"]);
                        ob1.ITEM_SNAME = (dr1["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_SNAME"]);
                        ob.images.Add(ob1);
                    }


                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =ob.RF_FAB_PROD_CAT_ID},
                             new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =ob.MC_STYLE_H_EXT_ID},
                             new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value =ob.MC_BLK_FAB_REQ_H_ID},
                             new CommandParameter() {ParameterName = "pOption", Value =3010},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        FAB_BKING_RPTModel ob2 = new FAB_BKING_RPTModel();
                        ob2.FAB_BKING_ID = (dr2["FAB_BKING_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["FAB_BKING_ID"]);
                        ob2.REVISION_NO_NAME = (dr2["REVISION_NO_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["REVISION_NO_NAME"]);
                        if(dr2["REVISION_NO"] != DBNull.Value)
                            ob2.REVISION_NO = (dr2["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr2["REVISION_NO"]);

                        ob.fabBkingRpt.Add(ob2);
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_FAB_PROD_ORD_HModel> SelectShipmentMonth(int? pMC_BYR_ACC_GRP_ID = null, int? pMC_BYR_ACC_ID = null, int? pMC_STYLE_H_ID = null, int? pRF_FAB_PROD_CAT_ID = null)
        {
            string sp = "pkg_fab_prod_order.prod_ord_h_ship_month_select";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID}, 
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_HModel ob = new MC_FAB_PROD_ORD_HModel();

                    ob.MONTHOF = (dr["MONTHOF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTHOF"]);
                    ob.FIRSTDATE = (dr["FIRSTDATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FIRSTDATE"]);
                    ob.LASTDATE = (dr["LASTDATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LASTDATE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_FAB_PROD_ORD_HModel Select(Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            string sp = "pkg_fab_prod_order.MC_fab_prod_ord_h_select";
            try
            {

                MC_FAB_PROD_ORD_HModel ob = new MC_FAB_PROD_ORD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);                    
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    //ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.LK_FAB_QTY_SRC_ID = (dr["LK_FAB_QTY_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_QTY_SRC_ID"]);
                    ob.PROD_ORD_DT = (dr["PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_ORD_DT"]);
                    //ob.REV_PROD_ORD_DT = (dr["REV_PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REV_PROD_ORD_DT"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.PRODUCTION_CAT = (dr["PRODUCTION_CAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCTION_CAT"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_TYPE = (dr["ORDER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    ob.SHIP_DT_STR = (dr["SHIP_DT_STR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_DT_STR"]);

                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MC_FAB_PROD_ORD_HModel GetFabProdOrdHdr(Int64 pRF_FAB_PROD_CAT_ID, Int64 pMC_STYLE_H_EXT_ID)
        {
            string sp = "pkg_fab_prod_order.mc_fab_prod_ord_h_select";
            try
            {

                MC_FAB_PROD_ORD_HModel ob = new MC_FAB_PROD_ORD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    //ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.LK_FAB_QTY_SRC_ID = (dr["LK_FAB_QTY_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_QTY_SRC_ID"]);
                    ob.PROD_ORD_DT = (dr["PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_ORD_DT"]);
                    ob.REASON_SHRT = (dr["REASON_SHRT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_SHRT"]);
                    ob.SHARE_SHRT = (dr["SHARE_SHRT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHARE_SHRT"]);

                    if (dr["LAST_REV_DT"] != DBNull.Value)
                    {
                        ob.LAST_REV_DT = (dr["LAST_REV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_REV_DT"]);
                        ob.LAST_REV_NO = (dr["LAST_REV_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_REV_NO"]);
                    }

                    //ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    //ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    //ob.REV_PROD_ORD_DT = (dr["REV_PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REV_PROD_ORD_DT"]);

                    //ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    //ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    //ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    //ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    //ob.PRODUCTION_CAT = (dr["PRODUCTION_CAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCTION_CAT"]);

                    //ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    //ob.ORDER_TYPE = (dr["ORDER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE"]);

                    //ob.SHIP_DT_STR = (dr["SHIP_DT_STR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_DT_STR"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MC_FAB_PROD_ORD_HModel GetFabProdOrdHdrByStyleId(Int64 pRF_FAB_PROD_CAT_ID, Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_fab_prod_order.mc_fab_prod_ord_h_select";
            try
            {

                MC_FAB_PROD_ORD_HModel ob = new MC_FAB_PROD_ORD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    //ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.LK_FAB_QTY_SRC_ID = (dr["LK_FAB_QTY_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_QTY_SRC_ID"]);
                    ob.PROD_ORD_DT = (dr["PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_ORD_DT"]);
                    ob.REASON_SHRT = (dr["REASON_SHRT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_SHRT"]);
                    ob.SHARE_SHRT = (dr["SHARE_SHRT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHARE_SHRT"]);

                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);

                    if (dr["LAST_REV_DT"] != DBNull.Value)
                    {
                        ob.LAST_REV_DT = (dr["LAST_REV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_REV_DT"]);
                        ob.LAST_REV_NO = (dr["LAST_REV_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_REV_NO"]);
                    }


                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveFabProdOrdMnul()
        {
            const string sp = "PKG_FAB_PROD_ORDER.knt_fab_prod_ord_save_mnul";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},                     
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_QTY_SRC_ID", Value = ob.LK_FAB_QTY_SRC_ID},
                     new CommandParameter() {ParameterName = "pPROD_ORD_DT", Value = ob.PROD_ORD_DT},
                     new CommandParameter() {ParameterName = "pREASON_SHRT", Value = ob.REASON_SHRT},
                     new CommandParameter() {ParameterName = "pSHARE_SHRT", Value = ob.SHARE_SHRT},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pFAB_PROD_D_XML", Value = ob.FAB_PROD_D_XML},                    
                     
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

        public string SaveFabDevKnitOrd()
        {
            const string sp = "PKG_FAB_PROD_ORDER.knt_fab_dev_prod_ord_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},                     
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_QTY_SRC_ID", Value = ob.LK_FAB_QTY_SRC_ID},
                     new CommandParameter() {ParameterName = "pPROD_ORD_DT", Value = ob.PROD_ORD_DT},
                     new CommandParameter() {ParameterName = "pREASON_SHRT", Value = ob.REASON_SHRT},
                     new CommandParameter() {ParameterName = "pSHARE_SHRT", Value = ob.SHARE_SHRT},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pFAB_PROD_D_XML", Value = ob.FAB_PROD_D_XML},                    
                     
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

    public class STYLE_ITEM_IMAGE
    {
        public String STYL_KEY_IMG { get; set; }

        public string ITEM_SNAME { get; set; }
    }

    public class FAB_BKING_RPTModel
    {
        public Int64 FAB_BKING_ID { get; set; }
        public string REVISION_NO_NAME { get; set; }
        public Int32? REVISION_NO { get; set; }
    }


}