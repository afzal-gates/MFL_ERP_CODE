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
    public class MC_FAB_PROC_GRPModel
    {
        public Int64 MC_FAB_PROC_GRP_ID { get; set; }
        public string FAB_PROC_GRP_NAME { get; set; }
        public Int64 MC_COST_HEAD_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        private List<MC_BLK_FAB_COSTModel> _items = null;
        public List<MC_BLK_FAB_COSTModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<MC_BLK_FAB_COSTModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }



        public string Save()
        {
            const string sp = "SP_MC_FAB_PROC_GRP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pFAB_PROC_GRP_NAME", Value = ob.FAB_PROC_GRP_NAME},
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value = ob.MC_COST_HEAD_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_MC_FAB_PROC_GRP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pFAB_PROC_GRP_NAME", Value = ob.FAB_PROC_GRP_NAME},
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value = ob.MC_COST_HEAD_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "SP_MC_FAB_PROC_GRP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pFAB_PROC_GRP_NAME", Value = ob.FAB_PROC_GRP_NAME},
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value = ob.MC_COST_HEAD_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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

        public List<MC_FAB_PROC_GRPModel> getFabProcessCost(Int64 MC_BLK_FAB_REQ_H_ID, Int64 MC_STYL_BGT_H_ID)
        {
            string sp = "pkg_budget_sheet.mc_fab_proc_grp_select";
            try
            {
                var obList = new List<MC_FAB_PROC_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROC_GRPModel ob = new MC_FAB_PROC_GRPModel();
                    ob.MC_FAB_PROC_GRP_ID = (dr["MC_FAB_PROC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_GRP_ID"]);
                    ob.FAB_PROC_GRP_NAME = (dr["FAB_PROC_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_GRP_NAME"]);
                    ob.MC_COST_HEAD_ID = (dr["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COST_HEAD_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);


                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value =ob.MC_FAB_PROC_GRP_ID},
                                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = MC_BLK_FAB_REQ_H_ID},
                                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = MC_STYL_BGT_H_ID},
                                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_BLK_FAB_COSTModel ob1 = new MC_BLK_FAB_COSTModel();
                        ob1.MC_BLK_FAB_COST_ID = (dr1["MC_BLK_FAB_COST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_BLK_FAB_COST_ID"]);
                        ob1.MC_FAB_PROC_RATE_ID = (dr1["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_FAB_PROC_RATE_ID"]);
                        ob1.FAB_QTY = (dr1["FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FAB_QTY"]);
                        ob1.PROC_RATE = (dr1["PROC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["PROC_RATE"]);
                        ob1.FAB_PROC_NAME = (dr1["FAB_PROC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["FAB_PROC_NAME"]);
                        ob1.IS_SAVED = (dr1["IS_SAVED"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_SAVED"]);
                        ob1.LK_WASH_TYPE_ID = (dr1["LK_WASH_TYPE_ID"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["LK_WASH_TYPE_ID"]);
                        ob1.LK_FINIS_TYPE_ID = (dr1["LK_FINIS_TYPE_ID"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["LK_FINIS_TYPE_ID"]);
                        ob1.ACCT_MOU_ID = (dr1["ACCT_MOU_ID"] == DBNull.Value) ? 3 : Convert.ToInt64(dr1["ACCT_MOU_ID"]);
                        ob1.PURCH_MOU_ID = (dr1["PURCH_MOU_ID"] == DBNull.Value) ? 3 : Convert.ToInt64(dr1["PURCH_MOU_ID"]);
                        ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["REMARKS"]);
                        ob.items.Add(ob1);
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

        public List<MC_FAB_PROC_GRPModel> Select(Int64? pMC_FAB_PROC_GRP_ID = null)
        {
            string sp = "pkg_df_production.mc_fab_proc_grp_select";
            try
            {
                var obList = new List<MC_FAB_PROC_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value =pMC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new MC_FAB_PROC_GRPModel();

                    ob.MC_FAB_PROC_GRP_ID = (dr["MC_FAB_PROC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_GRP_ID"]);
                    ob.FAB_PROC_GRP_NAME = (dr["FAB_PROC_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_GRP_NAME"]);
                    ob.MC_COST_HEAD_ID = (dr["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COST_HEAD_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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