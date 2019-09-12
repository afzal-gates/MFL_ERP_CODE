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
    public class MC_STYLE_D_FABModel
    {
        public Int64 MC_STYLE_D_FAB_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public string FAB_ITEM_CODE { get; set; }
        public string GARM_PART { get; set; }
        public Int64? RF_FAB_TYPE_ID { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public string FIB_CMP_DESC { get; set; }
        public Int64? LK_SPN_PRCS_ID { get; set; }
        public Int64? LK_COTN_TYPE_ID { get; set; }
        public string IS_SLUB { get; set; }
        public Int64? FB_WT_MIN { get; set; }
        public Int64? FB_WT_MAX { get; set; }
        public Int64? WT_MOU_ID { get; set; }
        public Int64? LK_MAC_GG_ID { get; set; }
        public string LK_WASH_TYPE_ID { get; set; }
        public string LK_FINIS_TYPE_ID { get; set; }
        public Int64? LK_FBR_GRP_ID { get; set; }
        public string FABRIC_DESC { get; set; }
        public string OTHER_SPEC { get; set; }
        public string RF_YRN_CNT_LST { get; set; }
        public string RF_YRN_CNT_LST_FULL { get; set; }
        public Int64? RF_YR_CAT_ID { get; set; }
        public Int64? LK_FEDER_TYPE_ID { get; set; }
        public Int64? RF_ELA_CNT_ID { get; set; }
        public string GSM_TOLLA { get; set; }
        public Decimal FABRIC_CONS { get; set; }
        public Decimal FABRIC_RATE { get; set; }
        public Int64? RF_CURRENCY_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string STYLE_D_ITEM_LST { get; set; }
        public Int64? LK_ELA_BRAND_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string IS_MELLANGE { get; set; }
        public string FAB_CATEGORY { get; set; }
        public string FABRIC_SNAME { get; set; }
        public string IS_ELA_MXD { get; set; }
        public string FAB_TYPE_SNAME { get; set; }

        public Int64? YR_FIB_COMP_ID { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string LK_FIB_TYPE_LST { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public Int64? INV_ITEM_CAT_ID_SUB { get; set; }
        public String IS_M_P { get; set; }
        public String IS_CHILD_EXIST { get; set; }
        public string IS_MSG_GEN { get; set; }
        public Int64? LK_FK_DGN_TYP_ID { get; set; }
        public string DROP_NDL_DESC { get; set; }

        private List<COUNT_LIST> _YRN_CNT_LST = null;
        public List<COUNT_LIST> YRN_CNT_LST
        {
            get
            {
                if (_YRN_CNT_LST == null)
                {
                    _YRN_CNT_LST = new List<COUNT_LIST>();
                }
                return _YRN_CNT_LST;
            }
            set
            {
                _YRN_CNT_LST = value;
            }
        }

        private string _IS_SP_WASH;

        public string IS_SP_WASH
        {
            get
            {
                if (this.LK_WASH_TYPE_ID == String.Empty)
                {
                    _IS_SP_WASH = "Y";
                }
                else
                {
                    _IS_SP_WASH = "N";
                }
                return _IS_SP_WASH;

            }
        }

        private string _IS_SP_FINISH;

        public string IS_SP_FINISH
        {
            get
            {
                if (this.LK_FINIS_TYPE_ID == String.Empty)
                {
                    _IS_SP_FINISH = "Y";
                }
                else
                {
                    _IS_SP_FINISH = "N";
                }
                return _IS_SP_FINISH;
            }
        }
        public String IS_USED { get; set; }

        public string Save()
        {
            const string sp = "pkg_merchandising.mc_style_d_fab_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pFAB_ITEM_CODE", Value = ob.FAB_ITEM_CODE},
                     new CommandParameter() {ParameterName = "pGARM_PART", Value = ob.GARM_PART},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFIB_CMP_DESC", Value = ob.FIB_CMP_DESC},
                     new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                     new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                     new CommandParameter() {ParameterName = "pFB_WT_MIN", Value = ob.FB_WT_MIN},
                     new CommandParameter() {ParameterName = "pFB_WT_MAX", Value = ob.FB_WT_MAX},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_MAC_GG_ID", Value = ob.LK_MAC_GG_ID},
                     new CommandParameter() {ParameterName = "pLK_WASH_TYPE_ID", Value = ob.LK_WASH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FINIS_TYPE_ID", Value = ob.LK_FINIS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ELA_CNT_ID", Value = ob.RF_ELA_CNT_ID},
                     new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pOTHER_SPEC", Value = ob.OTHER_SPEC},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_LST", Value = ob.RF_YRN_CNT_LST},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_LST_FULL", Value = ob.RF_YRN_CNT_LST_FULL},
                     new CommandParameter() {ParameterName = "pRF_YR_CAT_ID", Value = ob.RF_YR_CAT_ID},
                     new CommandParameter() {ParameterName = "pLK_FEDER_TYPE_ID", Value = ob.LK_FEDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pGSM_TOLLA", Value = ob.GSM_TOLLA},
                     new CommandParameter() {ParameterName = "pFABRIC_CONS", Value = ob.FABRIC_CONS},
                     new CommandParameter() {ParameterName = "pLK_ELA_BRAND_ID", Value = ob.LK_ELA_BRAND_ID},
                     new CommandParameter() {ParameterName = "pFABRIC_RATE", Value = ob.FABRIC_RATE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pSTYLE_D_ITEM_LST", Value = ob.STYLE_D_ITEM_LST},
                     new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                     new CommandParameter() {ParameterName = "pYR_FIB_COMP_ID", Value = ob.YR_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFIB_COMP_NAME", Value = ob.FIB_COMP_NAME},
                     new CommandParameter() {ParameterName = "pLK_FIB_TYPE_LST", Value = ob.LK_FIB_TYPE_LST},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pIS_MSG_GEN", Value = ob.IS_MSG_GEN??"N"},

                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID_SUB", Value = ob.INV_ITEM_CAT_ID_SUB},
                     new CommandParameter() {ParameterName = "pIS_M_P", Value = ob.IS_M_P},
                     new CommandParameter() {ParameterName = "pLK_FK_DGN_TYP_ID", Value = ob.LK_FK_DGN_TYP_ID},
                    
                     new CommandParameter() {ParameterName = "pDROP_NDL_DESC", Value = ob.DROP_NDL_DESC},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_STYLE_D_FAB_ID", Value =null, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_merchandising.mc_style_d_fab_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pFAB_ITEM_CODE", Value = ob.FAB_ITEM_CODE},
                     new CommandParameter() {ParameterName = "pGARM_PART", Value = ob.GARM_PART},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFIB_CMP_DESC", Value = ob.FIB_CMP_DESC},
                     new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                     new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                     new CommandParameter() {ParameterName = "pFB_WT_MIN", Value = ob.FB_WT_MIN},
                     new CommandParameter() {ParameterName = "pFB_WT_MAX", Value = ob.FB_WT_MAX},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_MAC_GG_ID", Value = ob.LK_MAC_GG_ID},
                     new CommandParameter() {ParameterName = "pLK_WASH_TYPE_ID", Value = ob.LK_WASH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FINIS_TYPE_ID", Value = ob.LK_FINIS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ELA_CNT_ID", Value = ob.RF_ELA_CNT_ID},
                     new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pOTHER_SPEC", Value = ob.OTHER_SPEC},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_LST", Value = ob.RF_YRN_CNT_LST},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_LST_FULL", Value = ob.RF_YRN_CNT_LST_FULL},
                     new CommandParameter() {ParameterName = "pRF_YR_CAT_ID", Value = ob.RF_YR_CAT_ID},
                     new CommandParameter() {ParameterName = "pLK_FEDER_TYPE_ID", Value = ob.LK_FEDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pGSM_TOLLA", Value = ob.GSM_TOLLA},
                     new CommandParameter() {ParameterName = "pFABRIC_CONS", Value = ob.FABRIC_CONS},
                     new CommandParameter() {ParameterName = "pFABRIC_RATE", Value = ob.FABRIC_RATE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_ELA_BRAND_ID", Value = ob.LK_ELA_BRAND_ID},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pSTYLE_D_ITEM_LST", Value = ob.STYLE_D_ITEM_LST},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},

                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID_SUB", Value = ob.INV_ITEM_CAT_ID_SUB},
                     new CommandParameter() {ParameterName = "pIS_M_P", Value = ob.IS_M_P},
                     new CommandParameter() {ParameterName = "pLK_FK_DGN_TYP_ID", Value = ob.LK_FK_DGN_TYP_ID},

                     new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                     new CommandParameter() {ParameterName = "pIS_MSG_GEN", Value = ob.IS_MSG_GEN??"N"},
                     new CommandParameter() {ParameterName = "pYR_FIB_COMP_ID", Value = ob.YR_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFIB_COMP_NAME", Value = ob.FIB_COMP_NAME},
                     new CommandParameter() {ParameterName = "pLK_FIB_TYPE_LST", Value = ob.LK_FIB_TYPE_LST},

                     new CommandParameter() {ParameterName = "pDROP_NDL_DESC", Value = ob.DROP_NDL_DESC},

                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string UpdateFromKnitting()
        {
            const string sp = "pkg_merchandising.mc_style_d_fab_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                     new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                     new CommandParameter() {ParameterName = "pFB_WT_MIN", Value = ob.FB_WT_MIN},
                     new CommandParameter() {ParameterName = "pFB_WT_MAX", Value = ob.FB_WT_MAX},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pLK_FEDER_TYPE_ID", Value = ob.LK_FEDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_FK_DGN_TYP_ID", Value = ob.LK_FK_DGN_TYP_ID},
                     

                     new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                     new CommandParameter() {ParameterName = "pYR_FIB_COMP_ID", Value = ob.YR_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pDROP_NDL_DESC", Value = ob.DROP_NDL_DESC},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_D_ID", Value = ob.MC_FAB_PROD_ORD_D_ID},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2001},
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

        public List<MC_STYLE_D_FABModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_style_d_fab_select";
            try
            {
                var obList = new List<MC_STYLE_D_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_FABModel ob = new MC_STYLE_D_FABModel();
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.FAB_ITEM_CODE = (dr["FAB_ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_CODE"]);
                    ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);

                    ob.FIB_CMP_DESC = (dr["FIB_CMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_CMP_DESC"]);
                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);


                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    if (dr["RF_FAB_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_FAB_TYPE_ID = Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    }

                    if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                    {
                        ob.LK_SPN_PRCS_ID = Convert.ToInt64(dr["LK_SPN_PRCS_ID"]);
                    }

                    if (dr["LK_ELA_BRAND_ID"] != DBNull.Value)
                    {
                        ob.LK_ELA_BRAND_ID = Convert.ToInt64(dr["LK_ELA_BRAND_ID"]);
                    }

                    if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_COTN_TYPE_ID = Convert.ToInt64(dr["LK_COTN_TYPE_ID"]);
                    }

                    if (dr["RF_ELA_CNT_ID"] != DBNull.Value)
                    {
                        ob.RF_ELA_CNT_ID = Convert.ToInt64(dr["RF_ELA_CNT_ID"]);
                    }

                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);


                    ob.FB_WT_MIN = (dr["FB_WT_MIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MIN"]);
                    ob.FB_WT_MAX = (dr["FB_WT_MAX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MAX"]);

                    if (dr["WT_MOU_ID"] != DBNull.Value)
                    {
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);
                    }
                    if (dr["LK_MAC_GG_ID"] != DBNull.Value)
                    {
                        ob.LK_MAC_GG_ID = Convert.ToInt64(dr["LK_MAC_GG_ID"]);
                    }
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);

                    if (dr["LK_FBR_GRP_ID"] != DBNull.Value)
                    {
                        ob.LK_FBR_GRP_ID = Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    }

                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);


                    if (dr["RF_YR_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_YR_CAT_ID = Convert.ToInt64(dr["RF_YR_CAT_ID"]);
                    }

                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }

                    ob.GSM_TOLLA = (dr["GSM_TOLLA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GSM_TOLLA"]);

                    ob.IS_MELLANGE = (dr["IS_MELLANGE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MELLANGE"]);

                    ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                    ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);

                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);

                    if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                    {
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    }
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["INV_ITEM_ID"] != DBNull.Value)
                    {
                        ob.INV_ITEM_ID = Convert.ToInt64(dr["INV_ITEM_ID"]);
                    }
                    if (dr["INV_ITEM_CAT_ID_SUB"] != DBNull.Value)
                    {
                        ob.INV_ITEM_CAT_ID_SUB = Convert.ToInt64(dr["INV_ITEM_CAT_ID_SUB"]);
                    }
                    ob.IS_M_P = (dr["IS_M_P"] == DBNull.Value) ? "M" : Convert.ToString(dr["IS_M_P"]);

                    if (dr["LK_FK_DGN_TYP_ID"] != DBNull.Value)
                    {
                        ob.LK_FK_DGN_TYP_ID = Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    }
                    ob.IS_CHILD_EXIST = (dr["IS_CHILD_EXIST"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CHILD_EXIST"]);
                    ob.DROP_NDL_DESC = (dr["DROP_NDL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DROP_NDL_DESC"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYLE_D_FABModel Select(long ID)
        {
            string sp = "pkg_merchandising.mc_style_d_fab_select";
            try
            {
                var ob = new MC_STYLE_D_FABModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.FAB_ITEM_CODE = (dr["FAB_ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_CODE"]);
                    ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);
                    ob.FIB_CMP_DESC = (dr["FIB_CMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_CMP_DESC"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    ob.IS_MELLANGE = (dr["IS_MELLANGE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MELLANGE"]);

                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    if (dr["RF_FAB_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_FAB_TYPE_ID = Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    }


                    if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                    {
                        ob.LK_SPN_PRCS_ID = Convert.ToInt64(dr["LK_SPN_PRCS_ID"]);
                    }

                    if (dr["LK_ELA_BRAND_ID"] != DBNull.Value)
                    {
                        ob.LK_ELA_BRAND_ID = Convert.ToInt64(dr["LK_ELA_BRAND_ID"]);
                    }

                    if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_COTN_TYPE_ID = Convert.ToInt64(dr["LK_COTN_TYPE_ID"]);
                    }

                    if (dr["RF_ELA_CNT_ID"] != DBNull.Value)
                    {
                        ob.RF_ELA_CNT_ID = Convert.ToInt64(dr["RF_ELA_CNT_ID"]);
                    }
                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);


                    ob.FB_WT_MIN = (dr["FB_WT_MIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MIN"]);
                    ob.FB_WT_MAX = (dr["FB_WT_MAX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MAX"]);

                    if (dr["WT_MOU_ID"] != DBNull.Value)
                    {
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);
                    }
                    if (dr["LK_MAC_GG_ID"] != DBNull.Value)
                    {
                        ob.LK_MAC_GG_ID = Convert.ToInt64(dr["LK_MAC_GG_ID"]);
                    }
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);

                    if (dr["LK_FBR_GRP_ID"] != DBNull.Value)
                    {
                        ob.LK_FBR_GRP_ID = Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    }

                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);

                    if (dr["RF_YR_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_YR_CAT_ID = Convert.ToInt64(dr["RF_YR_CAT_ID"]);
                    }

                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }

                    ob.GSM_TOLLA = (dr["GSM_TOLLA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GSM_TOLLA"]);
                    ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                    ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);

                    if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                    {
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    }

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["INV_ITEM_ID"] != DBNull.Value)
                    {
                        ob.INV_ITEM_ID = Convert.ToInt64(dr["INV_ITEM_ID"]);
                    }
                    if (dr["INV_ITEM_CAT_ID_SUB"] != DBNull.Value)
                    {
                        ob.INV_ITEM_CAT_ID_SUB = Convert.ToInt64(dr["INV_ITEM_CAT_ID_SUB"]);
                    }
                    ob.IS_M_P = (dr["IS_M_P"] == DBNull.Value) ? "M" : Convert.ToString(dr["IS_M_P"]);

                    ob.IS_USED = (dr["IS_USED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_USED"]);

                    if (dr["LK_FK_DGN_TYP_ID"] != DBNull.Value)
                    {
                        ob.LK_FK_DGN_TYP_ID = Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    }
                    ob.IS_CHILD_EXIST = (dr["IS_CHILD_EXIST"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CHILD_EXIST"]);

                    ob.DROP_NDL_DESC = (dr["DROP_NDL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DROP_NDL_DESC"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value =ob.MC_STYLE_D_FAB_ID},
                             new CommandParameter() {ParameterName = "pOption", Value =3000},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "PKG_FABRICATION.mc_style_d_fab_yrn_select");
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        COUNT_LIST ob1 = new COUNT_LIST();
                        ob1.PART_ID = (dr1["PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PART_ID"]);
                        if (dr1["CNT_ID"] != DBNull.Value)
                        {
                            ob1.CNT_ID = Convert.ToInt64(dr1["CNT_ID"]);
                        }
                        ob1.RATIO = (dr1["RATIO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["RATIO"]);
                        ob.YRN_CNT_LST.Add(ob1);
                    }


                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_FABModel> SelectFabByStyleID(long ID, Int16? pLK_FBR_GRP_ID, string pIS_ACTIVE)
        {
            string sp = "pkg_merchandising.mc_style_d_fab_select";
            try
            {
                var obList = new List<MC_STYLE_D_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = pLK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = pIS_ACTIVE},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_FABModel ob = new MC_STYLE_D_FABModel();
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    if (dr["INV_ITEM_ID"] != DBNull.Value)
                        ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.FAB_ITEM_CODE = (dr["FAB_ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_CODE"]);
                    ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);
                    ob.FIB_CMP_DESC = (dr["FIB_CMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_CMP_DESC"]);

                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    if (dr["RF_FAB_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_FAB_TYPE_ID = Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    }

                    if (dr["FAB_TYPE_SNAME"] != DBNull.Value)
                    {
                        ob.FAB_TYPE_SNAME = Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    }

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]);


                    if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                    {
                        ob.LK_SPN_PRCS_ID = Convert.ToInt64(dr["LK_SPN_PRCS_ID"]);
                    }

                    if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_COTN_TYPE_ID = Convert.ToInt64(dr["LK_COTN_TYPE_ID"]);
                    }

                    if (dr["RF_ELA_CNT_ID"] != DBNull.Value)
                    {
                        ob.RF_ELA_CNT_ID = Convert.ToInt64(dr["RF_ELA_CNT_ID"]);
                    }
                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);


                    ob.FB_WT_MIN = (dr["FB_WT_MIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MIN"]);
                    ob.FB_WT_MAX = (dr["FB_WT_MAX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MAX"]);

                    if (dr["WT_MOU_ID"] != DBNull.Value)
                    {
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);
                    }
                    if (dr["LK_MAC_GG_ID"] != DBNull.Value)
                    {
                        ob.LK_MAC_GG_ID = Convert.ToInt64(dr["LK_MAC_GG_ID"]);
                    }
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);

                    if (dr["LK_FBR_GRP_ID"] != DBNull.Value)
                    {
                        ob.LK_FBR_GRP_ID = Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    }
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);

                    if (dr["RF_YR_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_YR_CAT_ID = Convert.ToInt64(dr["RF_YR_CAT_ID"]);
                    }

                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }

                    ob.GSM_TOLLA = (dr["GSM_TOLLA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GSM_TOLLA"]);
                    ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                    ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);

                    if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                    {
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    }
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    //ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.IS_CHILD_EXIST = (dr["IS_CHILD_EXIST"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CHILD_EXIST"]);
                    ob.DROP_NDL_DESC = (dr["DROP_NDL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DROP_NDL_DESC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_FABModel> FabDataByItemId(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_style_d_fab_select";
            try
            {
                var obList = new List<MC_STYLE_D_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_FABModel ob = new MC_STYLE_D_FABModel();
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.FAB_ITEM_CODE = (dr["FAB_ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_CODE"]);
                    ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);

                    ob.FIB_CMP_DESC = (dr["FIB_CMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_CMP_DESC"]);

                    ob.FAB_CATEGORY = (dr["FAB_CATEGORY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CATEGORY"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);

                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    if (dr["RF_FAB_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_FAB_TYPE_ID = Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    }


                    if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                    {
                        ob.LK_SPN_PRCS_ID = Convert.ToInt64(dr["LK_SPN_PRCS_ID"]);
                    }

                    if (dr["LK_ELA_BRAND_ID"] != DBNull.Value)
                    {
                        ob.LK_ELA_BRAND_ID = Convert.ToInt64(dr["LK_ELA_BRAND_ID"]);
                    }


                    if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_COTN_TYPE_ID = Convert.ToInt64(dr["LK_COTN_TYPE_ID"]);
                    }
                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);


                    ob.FB_WT_MIN = (dr["FB_WT_MIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MIN"]);
                    ob.FB_WT_MAX = (dr["FB_WT_MAX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MAX"]);

                    if (dr["WT_MOU_ID"] != DBNull.Value)
                    {
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);
                    }
                    if (dr["LK_MAC_GG_ID"] != DBNull.Value)
                    {
                        ob.LK_MAC_GG_ID = Convert.ToInt64(dr["LK_MAC_GG_ID"]);
                    }
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);

                    if (dr["LK_FBR_GRP_ID"] != DBNull.Value)
                    {
                        ob.LK_FBR_GRP_ID = Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    }

                    ob.LK_FBR_GRP_TXT = (dr["LK_FBR_GRP_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FBR_GRP_TXT"]);

                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);

                    if (dr["RF_YR_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_YR_CAT_ID = Convert.ToInt64(dr["RF_YR_CAT_ID"]);
                    }

                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }
                    ob.GSM_TOLLA = (dr["GSM_TOLLA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GSM_TOLLA"]);
                    ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                    ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);

                    if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                    {
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    }

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.IS_MELLANGE = (dr["IS_MELLANGE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MELLANGE"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value =ob.MC_STYLE_D_FAB_ID},
                             new CommandParameter() {ParameterName = "pOption", Value =3000},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "PKG_FABRICATION.mc_style_d_fab_yrn_select");
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        COUNT_LIST ob1 = new COUNT_LIST();
                        ob1.PART_ID = (dr1["PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PART_ID"]);
                        if (dr1["CNT_ID"] != DBNull.Value)
                        {
                            ob1.CNT_ID = Convert.ToInt64(dr1["CNT_ID"]);
                        }
                        ob1.RATIO = (dr1["RATIO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["RATIO"]);
                        ob.YRN_CNT_LST.Add(ob1);
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


        public List<MC_STYLE_D_FABModel> FabDataByBuyerId(long pMC_BUYER_ID)
        {
            string sp = "pkg_merchandising.mc_style_d_fab_select";
            try
            {
                var obList = new List<MC_STYLE_D_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_FABModel ob = new MC_STYLE_D_FABModel();
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    //ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.FAB_ITEM_CODE = (dr["FAB_ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_CODE"]);
                    ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);
                    ob.FIB_CMP_DESC = (dr["FIB_CMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_CMP_DESC"]);

                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    if (dr["RF_FAB_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_FAB_TYPE_ID = Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    }

                    if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                    {
                        ob.LK_SPN_PRCS_ID = Convert.ToInt64(dr["LK_SPN_PRCS_ID"]);
                    }

                    if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_COTN_TYPE_ID = Convert.ToInt64(dr["LK_COTN_TYPE_ID"]);
                    }
                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);


                    ob.FB_WT_MIN = (dr["FB_WT_MIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MIN"]);
                    ob.FB_WT_MAX = (dr["FB_WT_MAX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MAX"]);

                    if (dr["WT_MOU_ID"] != DBNull.Value)
                    {
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);
                    }
                    if (dr["LK_MAC_GG_ID"] != DBNull.Value)
                    {
                        ob.LK_MAC_GG_ID = Convert.ToInt64(dr["LK_MAC_GG_ID"]);
                    }
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);

                    if (dr["LK_FBR_GRP_ID"] != DBNull.Value)
                    {
                        ob.LK_FBR_GRP_ID = Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    }

                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);

                    if (dr["RF_YR_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_YR_CAT_ID = Convert.ToInt64(dr["RF_YR_CAT_ID"]);
                    }

                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }

                    ob.GSM_TOLLA = (dr["GSM_TOLLA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GSM_TOLLA"]);
                    ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                    ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);

                    if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                    {
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    }

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    //ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_FABModel> FabDataByBuyerAccId(Int64? pMC_BYR_ACC_ID)
        {
            string sp = "pkg_merchandising.mc_style_d_fab_select";
            try
            {
                var obList = new List<MC_STYLE_D_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_FABModel ob = new MC_STYLE_D_FABModel();
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    //ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.FAB_ITEM_CODE = (dr["FAB_ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_CODE"]);
                    ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);
                    ob.FIB_CMP_DESC = (dr["FIB_CMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_CMP_DESC"]);

                    if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                    {
                        ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    }

                    if (dr["RF_FAB_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_FAB_TYPE_ID = Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    }


                    if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                    {
                        ob.LK_SPN_PRCS_ID = Convert.ToInt64(dr["LK_SPN_PRCS_ID"]);
                    }

                    if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_COTN_TYPE_ID = Convert.ToInt64(dr["LK_COTN_TYPE_ID"]);
                    }
                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);


                    ob.FB_WT_MIN = (dr["FB_WT_MIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MIN"]);
                    ob.FB_WT_MAX = (dr["FB_WT_MAX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MAX"]);

                    if (dr["WT_MOU_ID"] != DBNull.Value)
                    {
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);
                    }
                    if (dr["LK_MAC_GG_ID"] != DBNull.Value)
                    {
                        ob.LK_MAC_GG_ID = Convert.ToInt64(dr["LK_MAC_GG_ID"]);
                    }

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);

                    if (dr["LK_FBR_GRP_ID"] != DBNull.Value)
                    {
                        ob.LK_FBR_GRP_ID = Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    }

                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);

                    if (dr["RF_YR_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_YR_CAT_ID = Convert.ToInt64(dr["RF_YR_CAT_ID"]);
                    }

                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }

                    ob.GSM_TOLLA = (dr["GSM_TOLLA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GSM_TOLLA"]);
                    ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                    ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);

                    if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                    {
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    }
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    //ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string Delete(Int64 pMC_STYLE_D_FAB_ID)
        {
            const string sp = "pkg_merchandising.mc_style_d_fab_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = pMC_STYLE_D_FAB_ID},                    
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
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

        public List<MC_STYLE_D_FABModel> SelectForKnitProdOrder(Int64 pMC_FAB_PROD_ORD_H_ID)
        {
            string sp = "PKG_FAB_PROD_ORDER.MC_STYLE_D_FAB_ORDER_SELECT";
            try
            {
                var list = new List<MC_STYLE_D_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new MC_STYLE_D_FABModel();
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.FAB_ITEM_CODE = (dr["FAB_ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_CODE"]);
                    ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);
                    ob.FIB_CMP_DESC = (dr["FIB_CMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_CMP_DESC"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    ob.IS_MELLANGE = (dr["IS_MELLANGE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MELLANGE"]);

                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.OTHER_SPEC = (dr["OTHER_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTHER_SPEC"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);

                    ob.GSM_TOLLA = (dr["GSM_TOLLA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GSM_TOLLA"]);
                    ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                    ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DROP_NDL_DESC = (dr["DROP_NDL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DROP_NDL_DESC"]);
                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string makeActiveToggle(long pMC_STYLE_D_FAB_ID)
        {
            const string sp = "pkg_merchandising.mc_style_d_fab_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = pMC_STYLE_D_FAB_ID},                    
                     new CommandParameter() {ParameterName = "pOption", Value =4001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
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

        public string IS_ACTIVE { get; set; }
        public string LK_FBR_GRP_TXT { get; set; }
        public Int64? MC_FAB_PROD_ORD_D_ID { get; set; }
    }
}