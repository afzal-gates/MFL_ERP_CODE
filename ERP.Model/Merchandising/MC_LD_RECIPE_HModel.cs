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
    public class MC_LD_RECIPE_HModel
    {
        public Int64 MC_LD_RECIPE_H_ID { get; set; }
        //public Int64 MC_STYLE_H_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public Int64 MC_BYR_ACC_ID { get; set; }
        public string LD_RECIPE_NO { get; set; }
        public string OPTION_NO { get; set; }
        public DateTime? LD_APROVED_DT { get; set; }
        public Int64 MC_COLOR_ID { get; set; }
        public Int64 MC_COLOR_GRP_ID { get; set; }
        public Int64? MC_STYLE_D_FAB_ID { get; set; }
        public Int64? RF_FAB_TYPE_ID { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public Int64 LQR_RATIO { get; set; }
        public Int64 SMPL_FAB_WT { get; set; }
        public Int64 WT_MOU_ID { get; set; }
        public Int64 LK_DYE_MTHD_ID { get; set; }
        public string RQD_ROOM_TEMP_R { get; set; }
        public Decimal RQD_TIME_MIN_R { get; set; }
        public Decimal RQD_TIME_MIN_D { get; set; }
        public string RQD_ROOM_TEMP_D { get; set; }
        public Int64 RE_CHK_NO { get; set; }
        public DateTime RE_CHK_DT { get; set; }
        public string REASON_DESC { get; set; }
        public string IS_LD_FINALIZED { get; set; }
        public string IS_ACTIVE { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string STYLE_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string STYLE_DESC { get; set; }

        public long MC_STYLE_H_ID { get; set; }
        public string FABRIC_DESC { get; set; }
        public string ORDER_NO { get; set; }
        public string PANTON_NO { get; set; }

        public string XML_LD_ITEM { get; set; }
        public Int64 TOTAL_REC { get; set; }
        public Int64? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? MC_ORD_TRMS_ITEM_ID { get; set; }
        public string FAB_COM_TYPE { get; set; }
        
        public string Save()
        {
            const string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pLD_RECIPE_NO", Value = ob.LD_RECIPE_NO},
                     new CommandParameter() {ParameterName = "pOPTION_NO", Value = ob.OPTION_NO},
                     new CommandParameter() {ParameterName = "pLD_APROVED_DT", Value = ob.LD_APROVED_DT},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID<=0?0:ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID<=0?0:ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID<=0?null:ob.MC_STYLE_D_FAB_ID},                     
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID<=0?null:ob.MC_ORD_TRMS_ITEM_ID},                     
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
                     new CommandParameter() {ParameterName = "pSMPL_FAB_WT", Value = ob.SMPL_FAB_WT},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID<=0?1:ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pRQD_ROOM_TEMP_R", Value = ob.RQD_ROOM_TEMP_R},
                     new CommandParameter() {ParameterName = "pRQD_TIME_MIN_R", Value = ob.RQD_TIME_MIN_R},
                     new CommandParameter() {ParameterName = "pRQD_ROOM_TEMP_D", Value = ob.RQD_ROOM_TEMP_D},
                     new CommandParameter() {ParameterName = "pRQD_TIME_MIN_D", Value = ob.RQD_TIME_MIN_D},
                     new CommandParameter() {ParameterName = "pRE_CHK_NO", Value = ob.RE_CHK_NO},
                     new CommandParameter() {ParameterName = "pRE_CHK_DT", Value = ob.RE_CHK_DT},
                     new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
                     new CommandParameter() {ParameterName = "pIS_LD_FINALIZED", Value = ob.IS_LD_FINALIZED==null?"N":ob.IS_LD_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pFAB_COM_TYPE", Value = ob.FAB_COM_TYPE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = DateTime.Now},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = DateTime.Now},
                     new CommandParameter() {ParameterName = "pXML_LD_ITEM", Value = ob.XML_LD_ITEM},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opMC_LD_RECIPE_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pLD_RECIPE_NO", Value = ob.LD_RECIPE_NO},
                     new CommandParameter() {ParameterName = "pOPTION_NO", Value = ob.OPTION_NO},
                     new CommandParameter() {ParameterName = "pLD_APROVED_DT", Value = ob.LD_APROVED_DT},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID<=0?null:ob.MC_ORD_TRMS_ITEM_ID},                     
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
                     new CommandParameter() {ParameterName = "pSMPL_FAB_WT", Value = ob.SMPL_FAB_WT},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pRQD_ROOM_TEMP_R", Value = ob.RQD_ROOM_TEMP_R},
                     new CommandParameter() {ParameterName = "pRQD_TIME_MIN_R", Value = ob.RQD_TIME_MIN_R},
                     new CommandParameter() {ParameterName = "pRQD_ROOM_TEMP_D", Value = ob.RQD_ROOM_TEMP_D},
                     new CommandParameter() {ParameterName = "pRQD_TIME_MIN_D", Value = ob.RQD_TIME_MIN_D},
                     new CommandParameter() {ParameterName = "pRE_CHK_NO", Value = ob.RE_CHK_NO},
                     new CommandParameter() {ParameterName = "pRE_CHK_DT", Value = ob.RE_CHK_DT},
                     new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
                     new CommandParameter() {ParameterName = "pIS_LD_FINALIZED", Value = ob.IS_LD_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pLD_RECIPE_NO", Value = ob.LD_RECIPE_NO},
                     new CommandParameter() {ParameterName = "pOPTION_NO", Value = ob.OPTION_NO},
                     new CommandParameter() {ParameterName = "pLD_APROVED_DT", Value = ob.LD_APROVED_DT},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
                     new CommandParameter() {ParameterName = "pSMPL_FAB_WT", Value = ob.SMPL_FAB_WT},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pRQD_ROOM_TEMP_R", Value = ob.RQD_ROOM_TEMP_R},
                     new CommandParameter() {ParameterName = "pRQD_TIME_MIN_R", Value = ob.RQD_TIME_MIN_R},
                     new CommandParameter() {ParameterName = "pRQD_ROOM_TEMP_D", Value = ob.RQD_ROOM_TEMP_D},
                     new CommandParameter() {ParameterName = "pRQD_TIME_MIN_D", Value = ob.RQD_TIME_MIN_D},
                     new CommandParameter() {ParameterName = "pRE_CHK_NO", Value = ob.RE_CHK_NO},
                     new CommandParameter() {ParameterName = "pRE_CHK_DT", Value = ob.RE_CHK_DT},
                     new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
                     new CommandParameter() {ParameterName = "pIS_LD_FINALIZED", Value = ob.IS_LD_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<MC_LD_RECIPE_HModel> SelectAll(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_ID = null, string pLD_RECIPE_NO = null)
        {
            string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_select";
            try
            {
                var obList = new List<MC_LD_RECIPE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pLD_RECIPE_NO", Value =pLD_RECIPE_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_RECIPE_HModel ob = new MC_LD_RECIPE_HModel();
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);
                    if (dr["LD_APROVED_DT"] != DBNull.Value)
                        ob.LD_APROVED_DT = Convert.ToDateTime(dr["LD_APROVED_DT"]);
                    //ob.LD_APROVED_DT = (dr["LD_APROVED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_APROVED_DT"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.FAB_COM_TYPE = (dr["FAB_COM_TYPE"] == DBNull.Value) ? "F" : Convert.ToString(dr["FAB_COM_TYPE"]);

                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.SMPL_FAB_WT = (dr["SMPL_FAB_WT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_FAB_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.RQD_ROOM_TEMP_R = (dr["RQD_ROOM_TEMP_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_R"]);
                    ob.RQD_TIME_MIN_R = (dr["RQD_TIME_MIN_R"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_R"]);
                    ob.RQD_ROOM_TEMP_D = (dr["RQD_ROOM_TEMP_D"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_D"]);
                    ob.RQD_TIME_MIN_D = (dr["RQD_TIME_MIN_D"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_D"]);
                    ob.RE_CHK_NO = (dr["RE_CHK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_CHK_NO"]);
                    ob.RE_CHK_DT = (dr["RE_CHK_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RE_CHK_DT"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                    ob.IS_LD_FINALIZED = (dr["IS_LD_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LD_FINALIZED"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_LD_RECIPE_HModel Select(Int64? ID)
        {
            string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_select";
            try
            {
                var ob = new MC_LD_RECIPE_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value =ID==null?0:ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);
                    if (dr["LD_APROVED_DT"] != DBNull.Value)
                        ob.LD_APROVED_DT = Convert.ToDateTime(dr["LD_APROVED_DT"]);
                    //ob.LD_APROVED_DT = (dr["LD_APROVED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_APROVED_DT"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.SMPL_FAB_WT = (dr["SMPL_FAB_WT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_FAB_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.RQD_ROOM_TEMP_R = (dr["RQD_ROOM_TEMP_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_R"]);
                    ob.RQD_TIME_MIN_R = (dr["RQD_TIME_MIN_R"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_R"]);
                    ob.RQD_ROOM_TEMP_D = (dr["RQD_ROOM_TEMP_D"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_D"]);
                    ob.RQD_TIME_MIN_D = (dr["RQD_TIME_MIN_D"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_D"]);
                    ob.RE_CHK_NO = (dr["RE_CHK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_CHK_NO"]);
                    ob.RE_CHK_DT = (dr["RE_CHK_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RE_CHK_DT"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                    ob.IS_LD_FINALIZED = (dr["IS_LD_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LD_FINALIZED"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);

                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.FAB_COM_TYPE = (dr["FAB_COM_TYPE"] == DBNull.Value) ? "F" : Convert.ToString(dr["FAB_COM_TYPE"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MC_LD_RECIPE_HModel SelectForREQ(Int64? ID)
        {
            string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_select";
            try
            {
                var ob = new MC_LD_RECIPE_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value =ID==null?0:ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_LD_RECIPE_HModel SelectLD(Int64? pMC_LD_REQ_D_ID)
        {
            string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_select";
            try
            {
                var ob = new MC_LD_RECIPE_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value =pMC_LD_REQ_D_ID==null?0:pMC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);

                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);
                    if (dr["LD_APROVED_DT"] != DBNull.Value)
                        ob.LD_APROVED_DT = Convert.ToDateTime(dr["LD_APROVED_DT"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetOrderNoByStl(Int64 pMC_STYLE_H_ID)
        {
            string jsonStr = "{";
            string sp = "pkg_mc_style.mc_order_no_by_id_get";
            var i = 1;
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value ="", Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
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
            }
            catch
            {

            }
            return jsonStr;
        }

        public List<MC_LD_RECIPE_HModel> SelectByLabNo(string LD_RECIPE_NO)
        {
            string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_select";
            try
            {
                var obList = new List<MC_LD_RECIPE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLD_RECIPE_NO", Value =LD_RECIPE_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //MC_LD_RECIPE_HModel ob = new MC_LD_RECIPE_HModel();
                    //ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    //ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);

                    MC_LD_RECIPE_HModel ob = new MC_LD_RECIPE_HModel();
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);
                    ob.LD_APROVED_DT = (dr["LD_APROVED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_APROVED_DT"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.SMPL_FAB_WT = (dr["SMPL_FAB_WT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_FAB_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.RQD_ROOM_TEMP_R = (dr["RQD_ROOM_TEMP_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_R"]);
                    ob.RQD_TIME_MIN_R = (dr["RQD_TIME_MIN_R"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_R"]);
                    ob.RE_CHK_NO = (dr["RE_CHK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_CHK_NO"]);
                    ob.RE_CHK_DT = (dr["RE_CHK_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RE_CHK_DT"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                    ob.IS_LD_FINALIZED = (dr["IS_LD_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LD_FINALIZED"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RQD_ROOM_TEMP_D = (dr["RQD_ROOM_TEMP_D"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_D"]);
                    ob.RQD_TIME_MIN_D = (dr["RQD_TIME_MIN_D"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_D"]);

                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.FAB_COM_TYPE = (dr["FAB_COM_TYPE"] == DBNull.Value) ? "F" : Convert.ToString(dr["FAB_COM_TYPE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_LD_RECIPE_HModel> CheckDuplicateBuyerLabNo(Int64 pMC_BUYER_ID, string pLD_RECIPE_NO)
        {
            string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_select";
            try
            {
                var obList = new List<MC_LD_RECIPE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLD_RECIPE_NO", Value =pLD_RECIPE_NO},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_LD_RECIPE_HModel ob = new MC_LD_RECIPE_HModel();
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);
                    ob.LD_APROVED_DT = (dr["LD_APROVED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_APROVED_DT"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.SMPL_FAB_WT = (dr["SMPL_FAB_WT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_FAB_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.RQD_ROOM_TEMP_R = (dr["RQD_ROOM_TEMP_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_R"]);
                    ob.RQD_TIME_MIN_R = (dr["RQD_TIME_MIN_R"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_R"]);
                    ob.RE_CHK_NO = (dr["RE_CHK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_CHK_NO"]);
                    ob.RE_CHK_DT = (dr["RE_CHK_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RE_CHK_DT"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                    ob.IS_LD_FINALIZED = (dr["IS_LD_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LD_FINALIZED"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RQD_ROOM_TEMP_D = (dr["RQD_ROOM_TEMP_D"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_D"]);
                    ob.RQD_TIME_MIN_D = (dr["RQD_TIME_MIN_D"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_D"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_LD_RECIPE_HModel> SelectByOrderStyleColor(string ORDER_NO, Int64 pMC_STYLE_H_ID, Int64 pMC_COLOR_ID, Int64 pMC_BYR_ACC_GRP_ID)
        {
            string sp = "pkg_mc_ld_recipe.mc_ld_recipe_h_select";
            try
            {
                var obList = new List<MC_LD_RECIPE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pORDER_NO", Value =ORDER_NO},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //MC_LD_RECIPE_HModel ob = new MC_LD_RECIPE_HModel();
                    //ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    //ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);

                    MC_LD_RECIPE_HModel ob = new MC_LD_RECIPE_HModel();
                    ob.MC_LD_RECIPE_H_ID = (dr["MC_LD_RECIPE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_RECIPE_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.LD_RECIPE_NO = (dr["LD_RECIPE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_RECIPE_NO"]);
                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);
                    ob.LD_APROVED_DT = (dr["LD_APROVED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LD_APROVED_DT"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.FAB_COM_TYPE = (dr["FAB_COM_TYPE"] == DBNull.Value) ? "F" : Convert.ToString(dr["FAB_COM_TYPE"]);

                    ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
                    ob.SMPL_FAB_WT = (dr["SMPL_FAB_WT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SMPL_FAB_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.RQD_ROOM_TEMP_R = (dr["RQD_ROOM_TEMP_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_R"]);
                    ob.RQD_TIME_MIN_R = (dr["RQD_TIME_MIN_R"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_R"]);
                    ob.RE_CHK_NO = (dr["RE_CHK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_CHK_NO"]);
                    ob.RE_CHK_DT = (dr["RE_CHK_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RE_CHK_DT"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                    ob.IS_LD_FINALIZED = (dr["IS_LD_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LD_FINALIZED"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RQD_ROOM_TEMP_D = (dr["RQD_ROOM_TEMP_D"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_ROOM_TEMP_D"]);
                    ob.RQD_TIME_MIN_D = (dr["RQD_TIME_MIN_D"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_TIME_MIN_D"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.FIB_CMP_DESC = (dr["FIB_CMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_CMP_DESC"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_LD_RECIPE_HModel checkDuplicateRecipe(Int64 pMC_STYLE_H_ID, Int64 pMC_COLOR_ID, Int64 pMC_STYLE_D_FAB_ID)
        {
            string sp = "pkg_mc_ld_recipe.MC_LD_RECIPE_H_CHECK_DUPLICATE";
            try
            {
                var ob = new MC_LD_RECIPE_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value =pMC_STYLE_D_FAB_ID}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string FIB_CMP_DESC { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string FIB_COMP_NAME { get; set; }

        public long KNT_STYL_FAB_ITEM_ID { get; set; }
    }
}
