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
    public class MC_RATE_SPEC_DYEModel
    {
        public Int64 MC_RATE_SPEC_DYE_ID { get; set; }
        public Int64 MC_FAB_PROC_RATE_ID { get; set; }
        public Int64 MC_COLOR_GRP_ID { get; set; }
        public Int64? MC_FAB_PROC_GRP_ID { get; set; }
        public String FAB_PROC_NAME { get; set; }
        public Decimal PROC_RATE { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Int64 RATE_MOU_ID { get; set; }
        public String COLOR_GRP_NAME_EN { get; set; }
        public string CURR_NAME_EN { get; set; }
        public string RATE_MOU { get; set; }
        public Int64? LK_DYE_MTHD_ID { get; set; }
        public string DYE_MTHD_NAME { get; set; }
        public Int64 MC_FIB_COMB_TMPLT_ID { get; set; }
        public string FIB_COMB_TMPLT_NAME { get; set; }


        public string Save()
        {
            const string sp = "pkg_budget_sheet.mc_rate_spec_dye_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_DYE_ID", Value = ob.MC_RATE_SPEC_DYE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pPROC_RATE", Value = ob.PROC_RATE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pRATE_MOU_ID", Value = ob.RATE_MOU_ID},
                     new CommandParameter() {ParameterName = "pFAB_PROC_NAME", Value = ob.FAB_PROC_NAME},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pMC_FIB_COMB_TMPLT_ID", Value = ob.MC_FIB_COMB_TMPLT_ID },
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<MC_RATE_SPEC_DYEModel> SelectAll()
        {
            string sp = "pkg_budget_sheet.mc_rate_spec_dye_select";
            try
            {
                var obList = new List<MC_RATE_SPEC_DYEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_DYE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_RATE_SPEC_DYEModel ob = new MC_RATE_SPEC_DYEModel();
                    ob.MC_RATE_SPEC_DYE_ID = (dr["MC_RATE_SPEC_DYE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RATE_SPEC_DYE_ID"]);
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);
                    ob.PROC_RATE = (dr["PROC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROC_RATE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.RATE_MOU_ID = (dr["RATE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RATE_MOU_ID"]);
                    if (dr["LK_DYE_MTHD_ID"] != DBNull.Value)
                    {
                        ob.LK_DYE_MTHD_ID = Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    }
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.DYE_MTHD_NAME = (dr["DYE_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MTHD_NAME"]);
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    ob.RATE_MOU = (dr["RATE_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RATE_MOU"]);
                    ob.MC_FIB_COMB_TMPLT_ID = (dr["MC_FIB_COMB_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FIB_COMB_TMPLT_ID"]);
                    ob.FIB_COMB_TMPLT_NAME = (dr["FIB_COMB_TMPLT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMB_TMPLT_NAME"]);
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