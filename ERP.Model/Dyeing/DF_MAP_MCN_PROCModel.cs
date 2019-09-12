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
    public class DF_MAP_MCN_PROCModel
    {
        public Int64 DF_MAP_MCN_PROC_ID { get; set; }
        public Int64 DF_MACHINE_ID { get; set; }
        public Int64 DF_PROC_TYPE_ID { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string DF_MACHINE_NO { get; set; }
        public string DF_MC_NAME { get; set; }
        public string DF_PROC_TYPE_NAME { get; set; }
        public Int64 PROC_SEQ { get; set; }

        public Int64 DYE_BT_STS_TYPE_ID { get; set; }
        public string NXT_BT_STS_TYPE_LST { get; set; }
        public string IS_DEFA_FINIS { get; set; }

        public string Save()
        {
            const string sp = "pkg_df_production.df_map_mcn_proc_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MAP_MCN_PROC_ID", Value = ob.DF_MAP_MCN_PROC_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPROC_SEQ", Value = ob.PROC_SEQ},
                     new CommandParameter() {ParameterName = "pDYE_BT_STS_TYPE_ID", Value = ob.DYE_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pNXT_BT_STS_TYPE_LST", Value = ob.NXT_BT_STS_TYPE_LST},
                     new CommandParameter() {ParameterName = "pIS_DEFA_FINIS", Value = ob.IS_DEFA_FINIS},                     
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
            const string sp = "pkg_df_production.df_map_mcn_proc_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MAP_MCN_PROC_ID", Value = ob.DF_MAP_MCN_PROC_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPROC_SEQ", Value = ob.PROC_SEQ},
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
            const string sp = "pkg_df_production.df_map_mcn_proc_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MAP_MCN_PROC_ID", Value = ob.DF_MAP_MCN_PROC_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPROC_SEQ", Value = ob.PROC_SEQ},
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

        public List<DF_MAP_MCN_PROCModel> SelectAll()
        {
            string sp = "pkg_df_production.df_map_mcn_proc_select";
            try
            {
                var obList = new List<DF_MAP_MCN_PROCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MAP_MCN_PROC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_MAP_MCN_PROCModel ob = new DF_MAP_MCN_PROCModel();
                    ob.DF_MAP_MCN_PROC_ID = (dr["DF_MAP_MCN_PROC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MAP_MCN_PROC_ID"]);
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.PROC_SEQ = (dr["PROC_SEQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROC_SEQ"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.NXT_BT_STS_TYPE_LST = (dr["NXT_BT_STS_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_BT_STS_TYPE_LST"]);
                    ob.IS_DEFA_FINIS = (dr["IS_DEFA_FINIS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFA_FINIS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_MAP_MCN_PROCModel> SelectByID(Int64? pDF_MACHINE_ID = null, Int64? pDF_PROC_TYPE_ID = null, Int64? pLK_DIA_TYPE_ID = null)
        {
            string sp = "pkg_df_production.df_map_mcn_proc_select";
            try
            {
                var obList = new List<DF_MAP_MCN_PROCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value =pLK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value =pDF_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value =pDF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_MAP_MCN_PROCModel ob = new DF_MAP_MCN_PROCModel();
                    ob.DF_MAP_MCN_PROC_ID = (dr["DF_MAP_MCN_PROC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MAP_MCN_PROC_ID"]);
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.PROC_SEQ = (dr["PROC_SEQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROC_SEQ"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.NXT_BT_STS_TYPE_LST = (dr["NXT_BT_STS_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_BT_STS_TYPE_LST"]);
                    ob.IS_DEFA_FINIS = (dr["IS_DEFA_FINIS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFA_FINIS"]);

                    ob.DF_MACHINE_NO = (dr["DF_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MACHINE_NO"]);
                    ob.DF_MC_NAME = (dr["DF_MC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_NAME"]);
                    ob.DF_PROC_TYPE_NAME = (dr["DF_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_MAP_MCN_PROCModel Select(long ID)
        {
            string sp = "pkg_df_production.df_map_mcn_proc_select";
            try
            {
                var ob = new DF_MAP_MCN_PROCModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MAP_MCN_PROC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_MAP_MCN_PROC_ID = (dr["DF_MAP_MCN_PROC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MAP_MCN_PROC_ID"]);
                    ob.DF_MACHINE_ID = (dr["DF_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MACHINE_ID"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.PROC_SEQ = (dr["PROC_SEQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROC_SEQ"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_BT_STS_TYPE_ID = (dr["DYE_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_STS_TYPE_ID"]);
                    ob.NXT_BT_STS_TYPE_LST = (dr["NXT_BT_STS_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_BT_STS_TYPE_LST"]);
                    ob.IS_DEFA_FINIS = (dr["IS_DEFA_FINIS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFA_FINIS"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DIA_TYPE_NAME { get; set; }
    }
}