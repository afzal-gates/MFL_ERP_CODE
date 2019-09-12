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
    public class MC_FAB_PROD_ORD_DModel
    {
        public Int64 MC_FAB_PROD_ORD_D_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public Int64? MC_STYLE_D_FAB_ID { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public string COLOR_SPEC { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public string FIN_DIA { get; set; }
        public Int64? DIA_MOU_ID { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public string FIN_GSM { get; set; }
        public Int64? LK_COL_TYPE_ID { get; set; }
        public Int64? LK_COL_TYPE_ID4YD { get; set; }
        public Int64? LK_FK_DGN_TYP_ID { get; set; }
        public Int64? LK_YD_TYPE_ID { get; set; }
        public Int64? LK_FEDER_TYPE_ID { get; set; }
        public string FIB_COMBINATION_ID { get; set; }
        public Int64? MC_COLOR_GRP_ID { get; set; }
        public Int64? LK_DYE_MTHD_ID { get; set; }
        public Int64? LK_AOP_TYPE_ID { get; set; }
        public string WASH_TYPE_LIST { get; set; }
        public string FINISH_TYPE_LIST { get; set; }
        public Decimal RQD_GFAB_QTY { get; set; }
        public Decimal REV_GFAB_QTY { get; set; }
        public Decimal NET_GFAB_QTY { get; set; }
        public Decimal RQD_FFAB_QTY { get; set; }
        public Decimal REV_FFAB_QTY { get; set; }
        public Decimal NET_FFAB_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string SC_PRG_SN { get; set; }
        public string IS_SC_PRG_SN_AUTO { get; set; }
        public string HAS_YD_ITEM { get; set; }

        public string SC_BUYER_NAME { get; set; }
        public string SC_ORDER_NO { get; set; }
        public Int64? SCM_SC_WO_REF_ID { get; set; }
        public string SC_WO_REF_NO { get; set; }
        public DateTime? SC_START_DT { get; set; }
        public DateTime? SC_END_DT { get; set; }
        public Int64? KNT_MC_DIA_ID { get; set; }
        public Int64? LK_MC_GG_ID { get; set; }
        public Decimal? PROD_RATE { get; set; }
        public List<MC_FAB_PROD_D_YRNModel> fabProdYrn { get; set; }


        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }


        public string FAB_TYPE_SNAME { get; set; }
        public string FAB_TYPE_NAME { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string COLOR_GRP_NAME_EN { get; set; }
        public string FABRIC_SNAME { get; set; }
        public string FABRIC_DESC { get; set; }

        public string DIA_TYPE { get; set; }
        public string FIN_DIA_N_DIA_TYPE { get; set; }
        public string MC_DIA_N_GG { get; set; }
        public string COLOR_TYPE { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string NET_FFAB_QTY_NAME { get; set; }
        public string DYE_MTHD_NAME { get; set; }
        public string ORDER_NO_LIST { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string KNT_PLAN_NO { get; set; }
        public long RF_FAB_PROD_CAT_ID { get; set; }
        public long KNT_PLAN_H_ID { get; set; }
        public decimal BAL_GFAB_QTY { get; set; }
        public decimal KC_DONE { get; set; }
        public string IS_FLAT_CIR { get; set; }
        public decimal REQ_PENDING { get; set; }
        public string FEDER_TYPE_NAME { get; set; }
        public string YD_TYPE_NAME { get; set; }
        public string LD_RECIPE_NO { get; set; }
        public long BATCH_GFAB_QTY { get; set; }
        public DateTime ORD_CONF_DT { get; set; }
        public DateTime SHIP_DT { get; set; }
        public string COL_TYPE_NAME { get; set; }
        public string ORDER_NO_LIST_CON { get; set; }
        public DateTime? PLAN_START_DT { get; set; }
        public DateTime? PLAN_END_DT { get; set; }
        public string FAB_PROD_CAT_NAME { get; set; }
        public string IS_ACTIVE { get; set; }
        public string IS_BKING_APPROVE { get; set; }
        private List<KNT_PLAN_DModel> _items = null;
        public List<KNT_PLAN_DModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<KNT_PLAN_DModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }
        public long MC_STYLE_H_EXT_ID { get; set; }
        public long MC_BYR_ACC_GRP_ID { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string IS_ELA_MXD { get; set; }



        public List<MC_FAB_PROD_ORD_DModel> SelectByID(Int64? pMC_FAB_PROD_ORD_H_ID, String pONLY_COLLAR, Int32? pOption, string pLK_COL_TYPE_ID_LST, Int64? pMC_FAB_PROD_ORD_D_ID, string pHAS_YD = null, Int32? pRF_FAB_TYPE_ID = null)
        {
            string sp = "pkg_fab_prod_order.MC_fab_prod_ord_d_select";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pONLY_COLLAR", Value =pONLY_COLLAR},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID_LST", Value =pLK_COL_TYPE_ID_LST},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_D_ID", Value =pMC_FAB_PROD_ORD_D_ID},
                     new CommandParameter() {ParameterName = "pHAS_YD", Value =pHAS_YD},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value =pRF_FAB_TYPE_ID},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_DModel ob = new MC_FAB_PROD_ORD_DModel();
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);

                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    ob.LK_FK_DGN_TYP_ID = (dr["LK_FK_DGN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    ob.LK_FEDER_TYPE_ID = (dr["LK_FEDER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);


                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    // ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AOP_TYPE_ID"]);
                    ob.WASH_TYPE_LIST = (dr["WASH_TYPE_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_TYPE_LIST"]);
                    ob.FINISH_TYPE_LIST = (dr["FINISH_TYPE_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FINISH_TYPE_LIST"]);

                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);



                    ob.BAL_GFAB_QTY = (dr["BAL_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_GFAB_QTY"]);

                    ob.KC_DONE = (dr["KC_DONE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["KC_DONE"]);


                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.DIA_TYPE = (dr["DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.COLOR_TYPE = (dr["COLOR_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_TYPE"]);


                    ob.FEDER_TYPE_NAME = (dr["FEDER_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FEDER_TYPE_NAME"]);
                    ob.YD_TYPE_NAME = (dr["YD_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_TYPE_NAME"]);
                    ob.KNT_PLAN_NO = (dr["KNT_PLAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_PLAN_NO"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);

                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);

                    ob.REQ_PENDING = (dr["REQ_PENDING"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_PENDING"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_BKING_APPROVE = (dr["IS_BKING_APPROVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_BKING_APPROVE"]);

                    ob.HAS_YD_ITEM = (dr["HAS_YD_ITEM"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_YD_ITEM"]);
                    ob.LK_COL_TYPE_ID4YD = (dr["LK_COL_TYPE_ID4YD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID4YD"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.GFAB_ADJ_QTY = (dr["GFAB_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GFAB_ADJ_QTY"]);

                    ob.LK_FBR_GRP_TXT = (dr["LK_FBR_GRP_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FBR_GRP_TXT"]);

                    if (pOption == 3003 && ob.KNT_PLAN_H_ID > 0)
                    {
                        ob.items = new KNT_PLAN_DModel().QueryData(ob.KNT_PLAN_H_ID);
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


        public string MoveToYDKP()
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
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},              
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 1001},
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


        public MC_FAB_PROD_ORD_DModel Select(long ID)
        {
            string sp = "Select_MC_FAB_PROD_ORD_D";
            try
            {
                var ob = new MC_FAB_PROD_ORD_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    ob.LK_FK_DGN_TYP_ID = (dr["LK_FK_DGN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    ob.LK_FEDER_TYPE_ID = (dr["LK_FEDER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    ob.FIB_COMBINATION_ID = (dr["FIB_COMBINATION_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMBINATION_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    //ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AOP_TYPE_ID"]);
                    ob.WASH_TYPE_LIST = (dr["WASH_TYPE_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_TYPE_LIST"]);
                    ob.FINISH_TYPE_LIST = (dr["FINISH_TYPE_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FINISH_TYPE_LIST"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.REV_GFAB_QTY = (dr["REV_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_GFAB_QTY"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);
                    ob.RQD_FFAB_QTY = (dr["RQD_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FFAB_QTY"]);
                    ob.REV_FFAB_QTY = (dr["REV_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_FFAB_QTY"]);
                    ob.NET_FFAB_QTY = (dr["NET_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_FFAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public RF_PAGERModel getFabOrderDataForBatchProgram(
                Int64 pageNumber,
                Int64 pageSize,
                Int64? pMC_BYR_ACC_ID,
                string pRF_FAB_PROD_CAT_ID_LST,
                DateTime? pFIRSTDATE,
                DateTime? pLASTDATE,
                Int64? pMC_FAB_PROD_ORD_H_ID,
                Int64? pLK_COL_TYPE_ID,
                Int64? pMC_COLOR_ID
            )
        {
            string sp = "pkg_fab_prod_order.MC_FAB_PROD_ORD_H_SELECT";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_DModel>();
                Int64 vTotalRec = 0;
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize },
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID },
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID_LST", Value =pRF_FAB_PROD_CAT_ID_LST },
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID },                     
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value =pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pLASTDATE", Value =pLASTDATE},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value =pLK_COL_TYPE_ID },
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_DModel ob = new MC_FAB_PROD_ORD_DModel();

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NET_GFAB_QTY"]);
                    ob.BATCH_GFAB_QTY = (dr["BATCH_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_GFAB_QTY"]);

                    ob.KNT_FAB_QTY = (dr["KNT_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_QTY"]);

                    ob.GFAB_ADJ_QTY = (dr["GFAB_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GFAB_ADJ_QTY"]);
                    ob.GFAB_RCV_QTY = (dr["GFAB_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GFAB_RCV_QTY"]);

                    ob.BAL_GFAB_QTY = (ob.NET_GFAB_QTY - ob.GFAB_ADJ_QTY - ob.BATCH_GFAB_QTY);
                    
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);

                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);
                    
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.COL_TYPE_NAME = (dr["COL_TYPE_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["COL_TYPE_NAME"]);
                    ob.FAB_PROD_CAT_NAME = (dr["FAB_PROD_CAT_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_PROD_CAT_NAME"]);

                    ob.ORDER_NO_LIST_CON = "Order# :" + Convert.ToString(dr["ORDER_NO_LIST"]) + " [ Shipment : " + ob.SHIP_DT.ToString("dd-MMM-yyyy") + "]";

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    if (dr["PLAN_START_DT"] != DBNull.Value)
                    {
                        ob.PLAN_START_DT = Convert.ToDateTime(dr["PLAN_START_DT"]);
                    }

                    if (dr["PLAN_END_DT"] != DBNull.Value)
                    {
                        ob.PLAN_END_DT = Convert.ToDateTime(dr["PLAN_END_DT"]);

                        ob.ORDER_NO_LIST_CON += " [ T&A : " + Convert.ToDateTime(dr["PLAN_START_DT"]).ToString("dd-MMM-yyyy") + " ~ " + Convert.ToDateTime(dr["PLAN_END_DT"]).ToString("dd-MMM-yyyy") + "]";

                    }
                    ob.MC_BLK_REVISION_NO = (dr["MC_BLK_REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_REVISION_NO"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

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


        public RF_PAGERModel getFabOrderDataForSampleBatchProgram(
            Int64 pageNumber,
            Int64 pageSize,
            Int64? pMC_BYR_ACC_ID,
            Int64? pMC_BYR_ACC_GRP_ID,
            string pRF_FAB_PROD_CAT_ID_LST,
            DateTime? pFIRSTDATE,
            DateTime? pLASTDATE,
            Int64? pMC_FAB_PROD_ORD_H_ID,
            Int64? pLK_COL_TYPE_ID,
            Int64? pMC_COLOR_ID,
            string pJOB_CRD_NO,
            Int64? pOption
        )
        {
            string sp = "pkg_fab_prod_order.MC_FAB_PROD_ORD_H_SELECT";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_DModel>();
                Int64 vTotalRec = 0;
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize },
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID },
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID },
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID_LST", Value =pRF_FAB_PROD_CAT_ID_LST },
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID },                     
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value =pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pLASTDATE", Value =pLASTDATE},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value =pLK_COL_TYPE_ID },
                     new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value =pJOB_CRD_NO },
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3014:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_DModel ob = new MC_FAB_PROD_ORD_DModel();

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    //ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.COL_TYPE_NAME = (dr["COL_TYPE_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["COL_TYPE_NAME"]);
                    ob.FAB_PROD_CAT_NAME = (dr["FAB_PROD_CAT_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_PROD_CAT_NAME"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA_N_GG = (dr["MC_DIA_N_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA_N_GG"]);
                    ob.DIA_TYPE = (dr["DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    if(pOption==null)
                        ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);
                    
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);
                    ob.ISSUED_QTY = (dr["ISSUED_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUED_QTY"]);
                    
                    ob.ORDER_NO_LIST_CON = "Order# :" + Convert.ToString(dr["ORDER_NO_LIST"]) + " [ Shipment : " + ob.SHIP_DT.ToString("dd-MMM-yyyy") + "]";

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


        public RF_PAGERModel getFabOrderDataForScProgram(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_GRP_ID, Int64? pMC_STYLE_H_EXT_ID = null, string pRF_FAB_PROD_CAT_ID_LST = null, DateTime? pFIRSTDATE = null, DateTime? pLASTDATE = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_COLOR_ID = null
            )
        {
            string sp = "pkg_fab_prod_order.MC_FAB_PROD_ORD_H_SELECT";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_DModel>();
                Int64 vTotalRec = 0;
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize },
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID },
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID_LST", Value =pRF_FAB_PROD_CAT_ID_LST },
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID },                     
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value =pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pLASTDATE", Value =pLASTDATE},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID },
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_DModel ob = new MC_FAB_PROD_ORD_DModel();

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NET_GFAB_QTY"]);
                    ob.BATCH_GFAB_QTY = (dr["BATCH_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_GFAB_QTY"]);

                    

                    ob.KNT_FAB_QTY = (dr["KNT_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_QTY"]);
                    ob.SC_FAB_QTY = (dr["SC_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_FAB_QTY"]);
                    ob.BAL_GFAB_QTY = (ob.NET_GFAB_QTY - (ob.BATCH_GFAB_QTY+ob.SC_FAB_QTY));

                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);

                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);

                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.COL_TYPE_NAME = (dr["COL_TYPE_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["COL_TYPE_NAME"]);
                    ob.FAB_PROD_CAT_NAME = (dr["FAB_PROD_CAT_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_PROD_CAT_NAME"]);

                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);

                    ob.ORDER_NO_LIST_CON = "Order# :" + Convert.ToString(dr["ORDER_NO_LIST"]) + " [ Shipment : " + ob.SHIP_DT.ToString("dd-MMM-yyyy") + "]";

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


        public List<MC_FAB_PROD_ORD_DModel> GetFabOrdListByID(Int64 pMC_FAB_PROD_ORD_H_ID)
        {
            string sp = "pkg_fab_prod_order.MC_fab_prod_ord_d_select";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_DModel ob = new MC_FAB_PROD_ORD_DModel();
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    //ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);

                    if (dr["MC_STYLE_D_FAB_ID"] != DBNull.Value)
                        ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);

                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);

                    if (dr["DIA_MOU_ID"] != DBNull.Value)
                        ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    if (dr["LK_DIA_TYPE_ID"] != DBNull.Value)
                        ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);

                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);

                    if (dr["KNT_MC_DIA_ID"] != DBNull.Value)
                        ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                    if (dr["LK_MC_GG_ID"] != DBNull.Value)
                        ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);

                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    //ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    //ob.LK_FK_DGN_TYP_ID = (dr["LK_FK_DGN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    //ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    //ob.LK_FEDER_TYPE_ID = (dr["LK_FEDER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);

                    //ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    //ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    //ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AOP_TYPE_ID"]);
                    //ob.WASH_TYPE_LIST = (dr["WASH_TYPE_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_TYPE_LIST"]);
                    //ob.FINISH_TYPE_LIST = (dr["FINISH_TYPE_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FINISH_TYPE_LIST"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.REV_GFAB_QTY = (dr["REV_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_GFAB_QTY"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);
                    ob.RQD_FFAB_QTY = (dr["RQD_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FFAB_QTY"]);
                    ob.REV_FFAB_QTY = (dr["REV_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_FFAB_QTY"]);
                    ob.NET_FFAB_QTY = (dr["NET_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_FFAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    //ob.SC_PRG_SN = (dr["SC_PRG_SN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_SN"]);
                    //ob.IS_SC_PRG_SN_AUTO = (dr["IS_SC_PRG_SN_AUTO"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC_PRG_SN_AUTO"]);

                    //ob.SC_START_DT = (dr["SC_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_START_DT"]);
                    //ob.SC_END_DT = (dr["SC_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_END_DT"]);
                    //ob.SCM_SC_WO_REF_ID = (dr["SCM_SC_WO_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SC_WO_REF_ID"]);
                    //ob.SC_BUYER_NAME = (dr["SC_BUYER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_BUYER_NAME"]);
                    //ob.SC_WO_REF_NO = (dr["SC_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_WO_REF_NO"]);
                    //ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    //ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    //ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    //ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    //ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    //ob.DIA_TYPE = (dr["DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.MC_DIA_N_GG = (dr["MC_DIA_N_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA_N_GG"]);
                    //ob.COLOR_TYPE = (dr["COLOR_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_TYPE"]);
                    //ob.RQD_FFAB_QTY = (dr["RQD_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FFAB_QTY"]);
                    ob.NET_FFAB_QTY_NAME = (dr["NET_FFAB_QTY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NET_FFAB_QTY_NAME"]);

                    //var obFabProdYrnList = new MC_FAB_PROD_D_YRNModel().GetYrnListByFabOrd(ob.MC_FAB_PROD_ORD_D_ID);
                    //ob.fabProdYrn = (List<MC_FAB_PROD_D_YRNModel>)obFabProdYrnList;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_FAB_PROD_ORD_DModel> GetColorWiseFabOrdListByID(Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            string sp = "pkg_fab_prod_order.MC_fab_prod_ord_d_select";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_DModel ob = new MC_FAB_PROD_ORD_DModel();
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    if (dr["MC_STYLE_D_FAB_ID"] != DBNull.Value)
                        ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);

                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.REV_GFAB_QTY = (dr["REV_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_GFAB_QTY"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);
                    ob.RQD_FFAB_QTY = (dr["RQD_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FFAB_QTY"]);
                    ob.REV_FFAB_QTY = (dr["REV_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_FFAB_QTY"]);
                    ob.NET_FFAB_QTY = (dr["NET_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_FFAB_QTY"]);

                    ob.YD_PLOSS_QTY = (dr["YD_PLOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["YD_PLOSS_QTY"]);
                    ob.AOP_PLOSS_QTY = (dr["AOP_PLOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AOP_PLOSS_QTY"]);
                    ob.MKT_RATE = (dr["MKT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MKT_RATE"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_FAB_PROD_ORD_DModel> GetFinDiaByProdOrdId(Int64 pRF_FAB_PROD_CAT_ID, Int64 pMC_STYLE_H_EXT_ID)
        {
            string sp = "pkg_fab_prod_order.MC_FAB_PROD_ORD_D_SELECT";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},     
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_DModel ob = new MC_FAB_PROD_ORD_DModel();

                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public decimal RQD_QTY { get; set; }

        public long MC_BUYER_ID { get; set; }

        public string ORDER_NO { get; set; }

        public decimal YD_PLOSS_QTY { get; set; }

        public decimal AOP_PLOSS_QTY { get; set; }

        public decimal MKT_RATE { get; set; }

        public long KNT_FAB_QTY { get; set; }

        public long KNT_STYL_FAB_ITEM_ID { get; set; }

        public long MC_BLK_FAB_REQ_H_ID { get; set; }

        public long MC_BLK_REVISION_NO { get; set; }

        public decimal GFAB_ADJ_QTY { get; set; }

        public decimal STOCK_QTY { get; set; }

        public string KNT_YRN_LOT_LST { get; set; }

        public long SC_FAB_QTY { get; set; }

        public string JOB_CRD_NO { get; set; }

        public string DYE_BATCH_NO { get; set; }

        public decimal ISSUED_QTY { get; set; }

        public long MC_SMP_REQ_H_ID { get; set; }

        public decimal GFAB_RCV_QTY { get; set; }

        public string STYL_KEY_IMG { get; set; }

        public string LK_FBR_GRP_TXT { get; set; }
    }
}