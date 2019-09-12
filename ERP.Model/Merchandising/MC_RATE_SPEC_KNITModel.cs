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
    public class MC_RATE_SPEC_KNITModel
    {
        public Int64 MC_RATE_SPEC_KNIT_ID { get; set; }
        public Int64 MC_FAB_PROC_RATE_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 LK_COL_TYPE_ID { get; set; }
        public Int64? LK_YD_TYPE_ID { get; set; }
        public Int64? LK_FEDER_TYPE_ID { get; set; }
        public Int64? MC_FAB_PROC_GRP_ID { get; set; }
        public String FAB_PROC_NAME { get; set; }
        public Decimal PROC_RATE { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Int64 RATE_MOU_ID { get; set; }
        public string FAB_TYPE_NAME { get; set; }

        public string COL_TYPE_NAME { get; set; }

        public string YD_TYPE_NAME { get; set; }

        public string FEDER_TYPE_NAME { get; set; }

        public string CURR_NAME_EN { get; set; }

        public string RATE_MOU { get; set; }

        public Int64? LK_MAC_GG_ID { get; set; }
        public string IS_SLUB { get; set; }
        public Int64? LK_FK_DGN_TYP_ID { get; set; }
        public Int64 MC_FIB_COMB_TMPLT_ID { get; set; }
        public string MAC_GG_NAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string FIB_COMB_TMPLT_NAME { get; set; }
        public string IS_ACTIVE { get; set; }
        public Decimal FACT_PROD_RATE { get; set; }



        public string Save()
        {
            const string sp = "pkg_budget_sheet.mc_rate_spec_knit_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_KNIT_ID", Value = ob.MC_RATE_SPEC_KNIT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value = ob.LK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = ob.LK_YD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FEDER_TYPE_ID", Value = ob.LK_FEDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPROC_RATE", Value = ob.PROC_RATE},
                     new CommandParameter() {ParameterName = "pFACT_PROD_RATE", Value = ob.FACT_PROD_RATE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pRATE_MOU_ID", Value = ob.RATE_MOU_ID},
                     new CommandParameter() {ParameterName = "pFAB_PROC_NAME", Value = ob.FAB_PROC_NAME},
                     new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pLK_MAC_GG_ID", Value = ob.LK_MAC_GG_ID},
                     new CommandParameter() {ParameterName = "pLK_FK_DGN_TYP_ID", Value = ob.LK_FK_DGN_TYP_ID},
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

        public List<MC_RATE_SPEC_KNITModel> SelectAll()
        {
            string sp = "pkg_budget_sheet.mc_rate_spec_knit_select";
            try
            {
                var obList = new List<MC_RATE_SPEC_KNITModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_KNIT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_RATE_SPEC_KNITModel ob = new MC_RATE_SPEC_KNITModel();
                    ob.MC_RATE_SPEC_KNIT_ID = (dr["MC_RATE_SPEC_KNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RATE_SPEC_KNIT_ID"]);
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_YD_TYPE_ID = Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    }

                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }

                    if (dr["LK_FK_DGN_TYP_ID"] != DBNull.Value)
                    {
                        ob.LK_FK_DGN_TYP_ID = Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    }

                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SLUB"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);

                    ob.MAC_GG_NAME = (dr["MAC_GG_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_GG_NAME"]);

                    ob.PROC_RATE = (dr["PROC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROC_RATE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.RATE_MOU_ID = (dr["RATE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RATE_MOU_ID"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.COL_TYPE_NAME = (dr["COL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_TYPE_NAME"]);
                    ob.YD_TYPE_NAME = (dr["YD_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_TYPE_NAME"]);
                    ob.FEDER_TYPE_NAME = (dr["FEDER_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FEDER_TYPE_NAME"]);
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    ob.RATE_MOU = (dr["RATE_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RATE_MOU"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.MC_FIB_COMB_TMPLT_ID = (dr["MC_FIB_COMB_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FIB_COMB_TMPLT_ID"]);
                    ob.FIB_COMB_TMPLT_NAME = (dr["FIB_COMB_TMPLT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMB_TMPLT_NAME"]);
                    ob.FACT_PROD_RATE = (dr["FACT_PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FACT_PROD_RATE"]);
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