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
    public class MC_RATE_SPEC_DFINModel
    {
        public Int64 MC_RATE_SPEC_DFIN_ID { get; set; }
        public Int64 MC_FAB_PROC_RATE_ID { get; set; }
        public Int64? MC_FAB_PROC_GRP_ID { get; set; }
        public String FAB_PROC_NAME { get; set; }
        public decimal PROC_RATE { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Int64 RATE_MOU_ID { get; set; }
        public string XML { get; set; }
        public Int64 MC_FIB_COMB_TMPLT_ID { get; set; }
        public string FIB_COMB_TMPLT_NAME { get; set; }
        public string Save()
        {
            const string sp = "pkg_budget_sheet.mc_rate_spec_dfin_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
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
        public List<MC_RATE_SPEC_DFINModel> SelectAll()
        {
            string sp = "pkg_budget_sheet.mc_rate_spec_dfin_select";
            try
            {
                var obList = new List<MC_RATE_SPEC_DFINModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_DFIN_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_RATE_SPEC_DFINModel ob = new MC_RATE_SPEC_DFINModel();
                    ob.MC_FAB_PROC_GRP_ID = (dr["MC_FAB_PROC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_GRP_ID"]);
                    ob.MC_RATE_SPEC_DFIN_ID = (dr["MC_RATE_SPEC_DFIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RATE_SPEC_DFIN_ID"]);
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);
                    ob.PROC_RATE = (dr["PROC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROC_RATE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.RATE_MOU_ID = (dr["RATE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RATE_MOU_ID"]);
                    ob.MC_FIB_COMB_TMPLT_ID = (dr["MC_FIB_COMB_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FIB_COMB_TMPLT_ID"]);
                    ob.FIB_COMB_TMPLT_NAME = (dr["FIB_COMB_TMPLT_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FIB_COMB_TMPLT_NAME"]);
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