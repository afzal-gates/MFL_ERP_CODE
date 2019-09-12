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
    public class MC_PROV_FAB_BK_DModel
    {
        public Int64 MC_PROV_FAB_BK_D_ID { get; set; }
        public Int64 MC_PROV_FAB_BK_H_ID { get; set; }
        public string RF_GARM_PART_LST { get; set; }
        public Int64 LK_FBR_GRP_ID { get; set; }
        public Int64 MC_STYLE_D_FAB_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public string FIN_GSM { get; set; }
        public string FIN_DIA { get; set; }
        public Int64 DIA_MOU_ID { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public Int64 LK_COL_TYPE_ID { get; set; }
        public string COLOR_SPEC { get; set; }
        public string IS_CONTRAST { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public Int64 LK_YD_TYPE_ID { get; set; }
        public Int64 RQD_FAB_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_PROV_FAB_BK_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_D_ID", Value = ob.MC_PROV_FAB_BK_D_ID},
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFIN_GSM", Value = ob.FIN_GSM},
                     new CommandParameter() {ParameterName = "pFIN_DIA", Value = ob.FIN_DIA},
                     new CommandParameter() {ParameterName = "pDIA_MOU_ID", Value = ob.DIA_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value = ob.LK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCOLOR_SPEC", Value = ob.COLOR_SPEC},
                     new CommandParameter() {ParameterName = "pIS_CONTRAST", Value = ob.IS_CONTRAST},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = ob.LK_YD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRQD_FAB_QTY", Value = ob.RQD_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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

        public string Update()
        {
            const string sp = "SP_MC_PROV_FAB_BK_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_D_ID", Value = ob.MC_PROV_FAB_BK_D_ID},
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFIN_GSM", Value = ob.FIN_GSM},
                     new CommandParameter() {ParameterName = "pFIN_DIA", Value = ob.FIN_DIA},
                     new CommandParameter() {ParameterName = "pDIA_MOU_ID", Value = ob.DIA_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value = ob.LK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCOLOR_SPEC", Value = ob.COLOR_SPEC},
                     new CommandParameter() {ParameterName = "pIS_CONTRAST", Value = ob.IS_CONTRAST},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = ob.LK_YD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRQD_FAB_QTY", Value = ob.RQD_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_MC_PROV_FAB_BK_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_D_ID", Value = ob.MC_PROV_FAB_BK_D_ID},
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFIN_GSM", Value = ob.FIN_GSM},
                     new CommandParameter() {ParameterName = "pFIN_DIA", Value = ob.FIN_DIA},
                     new CommandParameter() {ParameterName = "pDIA_MOU_ID", Value = ob.DIA_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value = ob.LK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCOLOR_SPEC", Value = ob.COLOR_SPEC},
                     new CommandParameter() {ParameterName = "pIS_CONTRAST", Value = ob.IS_CONTRAST},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = ob.LK_YD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRQD_FAB_QTY", Value = ob.RQD_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<MC_PROV_FAB_BK_DModel> SelectAll()
        {
            string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_d_select";
            try
            {
                var obList = new List<MC_PROV_FAB_BK_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PROV_FAB_BK_DModel ob = new MC_PROV_FAB_BK_DModel();
                    ob.MC_PROV_FAB_BK_D_ID = (dr["MC_PROV_FAB_BK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PROV_FAB_BK_D_ID"]);
                    ob.MC_PROV_FAB_BK_H_ID = (dr["MC_PROV_FAB_BK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PROV_FAB_BK_H_ID"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_PROV_FAB_BK_DModel> Select(Int64? pMC_PROV_FAB_BK_H_ID = null)
        {
            string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_d_select";
            try
            {
                var obList = new List<MC_PROV_FAB_BK_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value =pMC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PROV_FAB_BK_DModel ob = new MC_PROV_FAB_BK_DModel();
                    ob.MC_PROV_FAB_BK_D_ID = (dr["MC_PROV_FAB_BK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PROV_FAB_BK_D_ID"]);
                    ob.MC_PROV_FAB_BK_H_ID = (dr["MC_PROV_FAB_BK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PROV_FAB_BK_H_ID"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
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