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
    public class DYE_DOSE_TMPLT_DModel
    {
        public Int64 DYE_DOSE_TMPLT_D_ID { get; set; }
        public Int64 DYE_DOSE_TMPLT_H_ID { get; set; }
        public Int64 DYE_PRD_PHASE_TYPE_ID { get; set; }
        public Int64 DYE_PROC_OPR_TYPE_ID { get; set; }
        public Int64 DC_AGENT_ID { get; set; }
        public Int64 ALT_AGENT_ID { get; set; }
        public Decimal DOSE_QTY { get; set; }
        public Int64 DOSE_MOU_ID { get; set; }
        public string IS_LABDIP { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string PRD_PHASE_CODE { get; set; }
        public string PRD_PHASE_NAME { get; set; }
        public string PRD_TYPE_SNAME { get; set; }
        public string DYE_MTHD_NAME { get; set; }
        public string DYE_MTHD_SNAME { get; set; }
        public string IS_S_D_PART { get; set; }
        public string PROC_OPR_CODE { get; set; }
        public string PROC_OPR_NAME { get; set; }
        public string PROC_OPR_SNAME { get; set; }
        public string DC_AGENT_NAME { get; set; }
        public string ALT_AGENT_NAME { get; set; }

        public List<INV_ITEMModel> ItemList { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_DOSE_TMPLT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_D_ID", Value = ob.DYE_DOSE_TMPLT_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_PROC_OPR_TYPE_ID", Value = ob.DYE_PROC_OPR_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDC_AGENT_ID", Value = ob.DC_AGENT_ID},
                     new CommandParameter() {ParameterName = "pALT_AGENT_ID", Value = ob.ALT_AGENT_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
                     new CommandParameter() {ParameterName = "pIS_LABDIP", Value = ob.IS_LABDIP},
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
            const string sp = "SP_DYE_DOSE_TMPLT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_D_ID", Value = ob.DYE_DOSE_TMPLT_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_PROC_OPR_TYPE_ID", Value = ob.DYE_PROC_OPR_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDC_AGENT_ID", Value = ob.DC_AGENT_ID},
                     new CommandParameter() {ParameterName = "pALT_AGENT_ID", Value = ob.ALT_AGENT_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
                     new CommandParameter() {ParameterName = "pIS_LABDIP", Value = ob.IS_LABDIP},
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
            const string sp = "SP_DYE_DOSE_TMPLT_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_D_ID", Value = ob.DYE_DOSE_TMPLT_D_ID},
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_PROC_OPR_TYPE_ID", Value = ob.DYE_PROC_OPR_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDC_AGENT_ID", Value = ob.DC_AGENT_ID},
                     new CommandParameter() {ParameterName = "pALT_AGENT_ID", Value = ob.ALT_AGENT_ID},
                     new CommandParameter() {ParameterName = "pDOSE_QTY", Value = ob.DOSE_QTY},
                     new CommandParameter() {ParameterName = "pDOSE_MOU_ID", Value = ob.DOSE_MOU_ID},
                     new CommandParameter() {ParameterName = "pIS_LABDIP", Value = ob.IS_LABDIP},
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

        public List<DYE_DOSE_TMPLT_DModel> SelectAll()
        {
            string sp = "pkg_mc_ld_recipe.dye_dose_tmplt_d_select";
            try
            {
                var obList = new List<DYE_DOSE_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DOSE_TMPLT_DModel ob = new DYE_DOSE_TMPLT_DModel();
                    ob.DYE_DOSE_TMPLT_D_ID = (dr["DYE_DOSE_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_D_ID"]);
                    ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.DYE_PROC_OPR_TYPE_ID = (dr["DYE_PROC_OPR_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PROC_OPR_TYPE_ID"]);
                    ob.DC_AGENT_ID = (dr["DC_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_AGENT_ID"]);
                    ob.ALT_AGENT_ID = (dr["ALT_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_AGENT_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
                    ob.IS_LABDIP = (dr["IS_LABDIP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LABDIP"]);
                    ob.IS_LN_BRK = (dr["IS_LN_BRK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LN_BRK"]);
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

        public List<DYE_DOSE_TMPLT_DModel> Select(Int64? pDYE_DOSE_TMPLT_H_ID, Int64? pMC_LD_RECIPE_H_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_mc_ld_recipe.dye_dose_tmplt_d_select";
            try
            {
                var obList = new List<DYE_DOSE_TMPLT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = pDYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = pMC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption==null?3001:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new DYE_DOSE_TMPLT_DModel();
                    ob.DYE_DOSE_TMPLT_D_ID = (dr["DYE_DOSE_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_D_ID"]);
                    ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.DYE_PROC_OPR_TYPE_ID = (dr["DYE_PROC_OPR_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PROC_OPR_TYPE_ID"]);
                    ob.DC_AGENT_ID = (dr["DC_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_AGENT_ID"]);
                    ob.ALT_AGENT_ID = (dr["ALT_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_AGENT_ID"]);
                    ob.DOSE_QTY = (dr["DOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DOSE_QTY"]);
                    ob.DOSE_MOU_ID = (dr["DOSE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DOSE_MOU_ID"]);
                    ob.IS_LABDIP = (dr["IS_LABDIP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LABDIP"]);
                    ob.IS_LN_BRK = (dr["IS_LN_BRK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LN_BRK"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.STEP_NO = (dr["STEP_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STEP_NO"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);

                    if (dr["INV_ITEM_ID"] != DBNull.Value)
                        ob.INV_ITEM_ID = Convert.ToInt64(dr["INV_ITEM_ID"]);

                    ob.PRD_PHASE_CODE = (dr["PRD_PHASE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_CODE"]);
                    ob.PRD_PHASE_NAME = (dr["PRD_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_NAME"]);
                    ob.PRD_TYPE_SNAME = (dr["PRD_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_TYPE_SNAME"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                    ob.DYE_MTHD_SNAME = (dr["DYE_MTHD_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_SNAME"]);
                    ob.IS_S_D_PART = (dr["IS_S_D_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_S_D_PART"]);
                    ob.PROC_OPR_CODE = (dr["PROC_OPR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROC_OPR_CODE"]);
                    ob.PROC_OPR_NAME = (dr["PROC_OPR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROC_OPR_NAME"]);
                    ob.PROC_OPR_SNAME = (dr["PROC_OPR_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROC_OPR_SNAME"]);
                    ob.DC_AGENT_NAME = (dr["DC_AGENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DC_AGENT_NAME"]);
                    ob.ALT_AGENT_NAME = (dr["ALT_AGENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ALT_AGENT_NAME"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.IS_FINALIZED = "N";
                    ob.IS_WAIT = "X";
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MOU_CODE { get; set; }
        public long STEP_NO { get; set; }
        public long LK_DYE_MTHD_ID { get; set; }
        public string IS_LN_BRK { get; set; }

        public long INV_ITEM_ID { get; set; }

        public string IS_FINALIZED { get; set; }
        public string IS_WAIT { get; set; }
        
    }
}