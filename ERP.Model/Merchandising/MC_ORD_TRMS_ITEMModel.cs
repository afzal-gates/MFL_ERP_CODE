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
    public class MC_ORD_TRMS_ITEMModel
    {
        public Int64 MC_ORD_TRMS_ITEM_ID { get; set; }
        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }
        public string MC_ORDER_H_LST { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64? RF_TRM_CAT_ID { get; set; }
        public string MEASUREMENT { get; set; }
        public string THICKNESS { get; set; }
        public string WIDTH { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public string ITEM_SPEC_AUTO { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }

        public string ITEM_NAME_EN { get; set; }
        public Int64? RF_FAB_TYPE_ID { get; set; }











        public Int64 MC_ACCS_PO_D_ITEM_ID { get; set; }
        public string ITEM_SPEC_SHORT { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64? GMT_COLOR_ID { get; set; }
        public Int64? ITEM_COLOR_ID { get; set; }
        public string COMBO_DESC { get; set; }
        public string GMT_COL_DESC { get; set; }
        public string LOT_NO { get; set; }
        public Int64? MC_SIZE_ID { get; set; }
        public Int64? MC_BUYER_BRAND_ID { get; set; }
        public Int64? RF_ITEM_QLTY_ID { get; set; }
        public Int64? RF_YRN_CNT_ID { get; set; }
        public Int64? LK_PLY_TYPE_ID { get; set; }
        public Decimal GROSS_WT { get; set; }
        public Decimal NET_WT { get; set; }
        public string MAIN_MARK { get; set; }
        public string SIDE_MARK { get; set; }
        public string WASH_INSTR { get; set; }
        public string FABRICATION { get; set; }
        public string PART_NO { get; set; }
        public string UVC_NO { get; set; }
        public string UFC_NO { get; set; }
        public string BAR_CODE { get; set; }
        public string SWATCH { get; set; }
        public Int64? NO_PRNT_COLOR { get; set; }
        public Decimal RQD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public decimal? CONF_RATE { get; set; }


        public string PANTON_NO { get; set; }
        public string DESIGN { get; set; }
        public string ANILINE { get; set; }
        public Int64? PRINT_COLOR_ID { get; set; }
        public Int64? TYPE_OF_TAG_ID { get; set; }
        public Int64? TYPE_OF_THREAD_ID { get; set; }
        public Int64? THREAD_COUNT_ID { get; set; }
        public Int64? TYPE_OF_ITEM { get; set; }
        public string SABU_NO { get; set; }
        public Int64? BTN_DYE_COLOR_ID { get; set; }
        public Int64? BTN_DEL_COLOR_ID { get; set; }
        public Int64? LK_DELIVERY_PART { get; set; }
        public string SIZE_RANGE { get; set; }
        public Int64? CARTON_QTY { get; set; }
        public string QUALITY_SPEC { get; set; }


        public string ITEM_SPEC_SHORT_ { get; set; }
        public string MC_ORDER_H_ID_ { get; set; }
        public string GMT_COLOR_ID_ { get; set; }
        public string ITEM_COLOR_ID_ { get; set; }
        public string COMBO_DESC_ { get; set; }
        public string GMT_COL_DESC_ { get; set; }
        public string LOT_NO_ { get; set; }
        public string MC_SIZE_ID_ { get; set; }
        public string MC_BUYER_BRAND_ID_ { get; set; }
        public string RF_ITEM_QLTY_ID_ { get; set; }
        public string RF_YRN_CNT_ID_ { get; set; }
        public string LK_PLY_TYPE_ID_ { get; set; }
        public string GROSS_WT_ { get; set; }
        public string NET_WT_ { get; set; }
        public string MAIN_MARK_ { get; set; }
        public string SIDE_MARK_ { get; set; }
        public string MEASUREMENT_ { get; set; }
        public string THICKNESS_ { get; set; }
        public string WIDTH_ { get; set; }
        public string WASH_INSTR_ { get; set; }
        public string FABRICATION_ { get; set; }
        public string PART_NO_ { get; set; }
        public string UVC_NO_ { get; set; }
        public string UFC_NO_ { get; set; }
        public string BAR_CODE_ { get; set; }
        public string SWATCH_ { get; set; }
        public string NO_PRNT_COLOR_ { get; set; }
        public string RQD_QTY_ { get; set; }
        public string QTY_MOU_ID_ { get; set; }
        public string REMARKS_ { get; set; }
        public string CONF_RATE_ { get; set; }

        public string PANTON_NO_ { get; set; }
        public string DESIGN_ { get; set; }
        public string ANILINE_ { get; set; }
        public string PRINT_COLOR_ID_ { get; set; }
        public string TYPE_OF_TAG_ID_ { get; set; }
        public string TYPE_OF_THREAD_ID_ { get; set; }
        public string THREAD_COUNT_ID_ { get; set; }
        public string TYPE_OF_ITEM_ { get; set; }
        public string SABU_NO_ { get; set; }
        public string BTN_DYE_COLOR_ID_ { get; set; }
        public string BTN_DEL_COLOR_ID_ { get; set; }
        public string LK_DELIVERY_PART_ { get; set; }
        public string SIZE_RANGE_ { get; set; }
        public string CARTON_QTY_ { get; set; }
        public string QUALITY_SPEC_ { get; set; }


        public string Save()
        {
            const string sp = "pkg_inv_trims_rcv.mc_ord_trms_item_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID==0?null:ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID==0?null:ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pPROD_COLOR_NAME_EN", Value = ob.PROD_COLOR_NAME_EN},
                     new CommandParameter() {ParameterName = "pPROD_SIZE_CODE", Value = ob.PROD_SIZE_CODE},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pPARTICULARS", Value = ob.PARTICULARS},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID==0?null:ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value = ob.GMT_COLOR_ID==0?null:ob.GMT_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID==0?null:ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_AUTO", Value = ob.ITEM_SPEC_AUTO},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string Update()
        {
            const string sp = "SP_MC_ORD_TRMS_ITEM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_LST", Value = ob.MC_ORDER_H_LST},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_TRM_CAT_ID", Value = ob.RF_TRM_CAT_ID},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     new CommandParameter() {ParameterName = "pTHICKNESS", Value = ob.THICKNESS},
                     new CommandParameter() {ParameterName = "pWIDTH", Value = ob.WIDTH},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_AUTO", Value = ob.ITEM_SPEC_AUTO},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "SP_MC_ORD_TRMS_ITEM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_LST", Value = ob.MC_ORDER_H_LST},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_TRM_CAT_ID", Value = ob.RF_TRM_CAT_ID},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     new CommandParameter() {ParameterName = "pTHICKNESS", Value = ob.THICKNESS},
                     new CommandParameter() {ParameterName = "pWIDTH", Value = ob.WIDTH},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_AUTO", Value = ob.ITEM_SPEC_AUTO},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<MC_ORD_TRMS_ITEMModel> SelectAll()
        {
            string sp = "pkg_inv_trims_rcv.mc_ord_trms_item_select";
            try
            {
                var obList = new List<MC_ORD_TRMS_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORD_TRMS_ITEMModel ob = new MC_ORD_TRMS_ITEMModel();

                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);

                    ob.PROD_SIZE_CODE = (dr["PROD_SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_SIZE_CODE"]);
                    ob.PROD_COLOR_NAME_EN = (dr["PROD_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_COLOR_NAME_EN"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                    MC_ACCS_PO_TMPLT_CFGModel obj = new MC_ACCS_PO_TMPLT_CFGModel();
                    obj.CNTRL_NAME = "";
                    obj.COL_VAL = "";
                    ob.PARTICULARS_LST = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                    ob.PARTICULARS_LST.Add(obj);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_ORD_TRMS_ITEMModel> SelectByID(Int64? pINV_ITEM_CAT_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_BYR_ACC_ID = null)
        {
            string sp = "pkg_inv_trims_rcv.mc_ord_trms_item_select";
            try
            {
                var obList = new List<MC_ORD_TRMS_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORD_TRMS_ITEMModel ob = new MC_ORD_TRMS_ITEMModel();                    
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);

                    ob.PARTICULARS_VALUE = (dr["PARTICULARS_VALUE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS_VALUE"]);
                    ob.PROD_SIZE_CODE = (dr["PROD_SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_SIZE_CODE"]);
                    ob.PROD_COLOR_NAME_EN = (dr["PROD_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_COLOR_NAME_EN"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                    MC_ACCS_PO_TMPLT_CFGModel obj = new MC_ACCS_PO_TMPLT_CFGModel();
                    obj.CNTRL_NAME = "";
                    obj.COL_VAL = "";
                    ob.PARTICULARS_LST = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                    ob.PARTICULARS_LST.Add(obj);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_ORD_TRMS_ITEMModel> GetTrimsItemListByPordOrdID(Int64? pINV_ITEM_CAT_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            string sp = "pkg_inv_trims_rcv.mc_ord_trms_item_select";
            try
            {
                var obList = new List<MC_ORD_TRMS_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORD_TRMS_ITEMModel ob = new MC_ORD_TRMS_ITEMModel();

                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);

                    ob.PROD_SIZE_CODE = (dr["PROD_SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_SIZE_CODE"]);
                    ob.PROD_COLOR_NAME_EN = (dr["PROD_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_COLOR_NAME_EN"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                    MC_ACCS_PO_TMPLT_CFGModel obj = new MC_ACCS_PO_TMPLT_CFGModel();
                    obj.CNTRL_NAME = "";
                    obj.COL_VAL = "";
                    ob.PARTICULARS_LST = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                    ob.PARTICULARS_LST.Add(obj);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_ORD_TRMS_ITEMModel Select(long ID)
        {
            string sp = "Select_MC_ORD_TRMS_ITEM";
            try
            {
                var ob = new MC_ORD_TRMS_ITEMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);

                    ob.PROD_SIZE_CODE = (dr["PROD_SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_SIZE_CODE"]);
                    ob.PROD_COLOR_NAME_EN = (dr["PROD_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_COLOR_NAME_EN"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                    MC_ACCS_PO_TMPLT_CFGModel obj = new MC_ACCS_PO_TMPLT_CFGModel();
                    obj.CNTRL_NAME = "";
                    obj.COL_VAL = "";
                    ob.PARTICULARS_LST = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                    ob.PARTICULARS_LST.Add(obj);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_ORD_TRMS_ITEMModel> Query(Int64? pSCM_PURC_REQ_D_ID = null)
        {
            string sp = "pkg_acc_booking.mc_ord_trms_item_select";
            try
            {
                var obList = new List<MC_ORD_TRMS_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_ID", Value = pSCM_PURC_REQ_D_ID},
                     //new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = pINV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORD_TRMS_ITEMModel ob = new MC_ORD_TRMS_ITEMModel();

                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);

                    ob.PROD_SIZE_CODE = (dr["PROD_SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_SIZE_CODE"]);
                    ob.PROD_COLOR_NAME_EN = (dr["PROD_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_COLOR_NAME_EN"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                    MC_ACCS_PO_TMPLT_CFGModel obj = new MC_ACCS_PO_TMPLT_CFGModel();
                    obj.CNTRL_NAME = "";
                    obj.COL_VAL = "";
                    ob.PARTICULARS_LST = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                    ob.PARTICULARS_LST.Add(obj);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<MC_ORD_TRMS_ITEMModel> GetTrimsForLDByStyleID(Int64? pMC_STYLE_H_ID = null)
        {
            string sp = "pkg_inv_trims_rcv.mc_ord_trms_item_select";
            try
            {
                var obList = new List<MC_ORD_TRMS_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORD_TRMS_ITEMModel ob = new MC_ORD_TRMS_ITEMModel();

                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);

                    ob.PROD_SIZE_CODE = (dr["PROD_SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_SIZE_CODE"]);
                    ob.PROD_COLOR_NAME_EN = (dr["PROD_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROD_COLOR_NAME_EN"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);
                    MC_ACCS_PO_TMPLT_CFGModel obj = new MC_ACCS_PO_TMPLT_CFGModel();
                    obj.CNTRL_NAME = "";
                    obj.COL_VAL = "";
                    ob.PARTICULARS_LST = new List<MC_ACCS_PO_TMPLT_CFGModel>();
                    ob.PARTICULARS_LST.Add(obj);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Int32? MC_BUYER_ID { get; set; }

        public string PARTICULARS { get; set; }

        public List<MC_ACCS_PO_TMPLT_CFGModel> PARTICULARS_LST { get; set; }

        public object ITEM_CODE { get; set; }

        public object PROD_SIZE_CODE { get; set; }

        public object PROD_COLOR_NAME_EN { get; set; }

        public Int64? MC_BYR_ACC_ID { get; set; }

        public string PARTICULARS_VALUE { get; set; }
    }
}