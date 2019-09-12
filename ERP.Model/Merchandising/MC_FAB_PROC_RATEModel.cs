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
    public class MC_FAB_PROC_RATEModel
    {
        public Int64 MC_FAB_PROC_RATE_ID { get; set; }
        public Int64 MC_FAB_PROC_GRP_ID { get; set; }
        public string FAB_PROC_NAME { get; set; }
        public Decimal PROC_RATE { get; set; }        
        public string IS_ACTIVE { get; set; }
        public decimal FACT_PROD_RATE { get; set; }



        public string Save()
        {
            const string sp = "SP_MC_FAB_PROC_RATE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pFAB_PROC_NAME", Value = ob.FAB_PROC_NAME},
                     new CommandParameter() {ParameterName = "pPROC_RATE", Value = ob.PROC_RATE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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

        public List<MC_FAB_PROC_RATEModel> getRateSuggestion(
            Int64? pMC_FAB_PROC_GRP_ID, 
            Int64? pRF_FIB_COMP_GRP_ID, 
            Int64? pRF_FAB_TYPE_ID, 
            Int64? pLK_COL_TYPE_ID, 
            Int64? pLK_YD_TYPE_ID,
            Int64? pLK_FEDER_TYPE_ID, 
            Int64? pMC_COLOR_GRP_ID, 
            Int64? pLK_AOP_TYPE_ID, 
            Int64? pLK_FK_DGN_TYP_ID
        )
        {
            string sp = "pkg_budget_sheet.mc_fab_proc_rate_select";
            try
            {
                var obList = new List<MC_FAB_PROC_RATEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = pMC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_GRP_ID", Value = pRF_FIB_COMP_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value = pLK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = pLK_YD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FEDER_TYPE_ID", Value = pLK_FEDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = pMC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_AOP_TYPE_ID", Value = pLK_AOP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FK_DGN_TYP_ID", Value = pLK_FK_DGN_TYP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROC_RATEModel ob = new MC_FAB_PROC_RATEModel();
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);
                    ob.PROC_RATE = (dr["PROC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROC_RATE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_FAB_PROC_RATEModel Select(long ID)
        {
            string sp = "Select_MC_FAB_PROC_RATE";
            try
            {
                var ob = new MC_FAB_PROC_RATEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.MC_FAB_PROC_GRP_ID = (dr["MC_FAB_PROC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_GRP_ID"]);
                    ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);
                    ob.PROC_RATE = (dr["PROC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROC_RATE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MC_FAB_PROC_RATEModel> GetProcRateListByParty(Int32? pSCM_SUPPLIER_ID = null, Int64? pRF_FAB_TYPE_ID = null, Int64? pLK_FK_DGN_TYP_ID = null)
        {
            string sp = "pkg_knit_common.knt_fab_proc_rate_select";
            try
            {
                var obList = new List<MC_FAB_PROC_RATEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {    
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},                     
                    new CommandParameter() {ParameterName = "pLK_FK_DGN_TYP_ID", Value = pLK_FK_DGN_TYP_ID},
                    new CommandParameter() {ParameterName = "pOption", Value =3000},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROC_RATEModel ob = new MC_FAB_PROC_RATEModel();
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);
                    ob.PROC_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    public List<MC_FAB_PROC_RATEModel> KntRateData(
            string pFIB_COMBINATION_ID,
            Int64? pRF_FAB_TYPE_ID,
            Int64? pLK_COL_TYPE_ID,
            Int64? pLK_YD_TYPE_ID,
            Int64? pLK_FEDER_TYPE_ID,
            Int64? pLK_FK_DGN_TYP_ID
        )
        {
            string sp = "PKG_PROCESS_RATE.knitting_rate_select";
            try
            {
                var obList = new List<MC_FAB_PROC_RATEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pFIB_COMBINATION_ID", Value = pFIB_COMBINATION_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value = pLK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = pLK_YD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FEDER_TYPE_ID", Value = pLK_FEDER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FK_DGN_TYP_ID", Value = pLK_FK_DGN_TYP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000 },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROC_RATEModel ob = new MC_FAB_PROC_RATEModel();
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);
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