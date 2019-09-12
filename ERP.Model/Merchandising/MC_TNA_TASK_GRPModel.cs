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
    public class MC_TNA_TASK_GRPModel
    {
        public Int64 MC_TNA_TASK_GRP_ID { get; set; }
        public string TA_GRP_NAME_EN { get; set; }
        public string TA_GRP_NAME_BN { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_START_TASK { get; set; }
        public string IS_END_TASK { get; set; }
        public string IS_PRODUCTION { get; set; }




        public string Save()
        {
            const string sp = "pkg_tna.mc_tna_task_grp_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value = ob.MC_TNA_TASK_GRP_ID},
                     new CommandParameter() {ParameterName = "pTA_GRP_NAME_EN", Value = ob.TA_GRP_NAME_EN},
                     new CommandParameter() {ParameterName = "pTA_GRP_NAME_BN", Value = ob.TA_GRP_NAME_BN},
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
                         i++ ;
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
            const string sp = "pkg_tna.mc_tna_task_grp_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value = ob.MC_TNA_TASK_GRP_ID},
                     new CommandParameter() {ParameterName = "pTA_GRP_NAME_EN", Value = ob.TA_GRP_NAME_EN},
                     new CommandParameter() {ParameterName = "pTA_GRP_NAME_BN", Value = ob.TA_GRP_NAME_BN},
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
                         i++ ;
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
            const string sp = "pkg_tna.mc_tna_task_grp_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value = ob.MC_TNA_TASK_GRP_ID},
                     new CommandParameter() {ParameterName = "pTA_GRP_NAME_EN", Value = ob.TA_GRP_NAME_EN},
                     new CommandParameter() {ParameterName = "pTA_GRP_NAME_BN", Value = ob.TA_GRP_NAME_BN},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TASK_GRPModel> SelectAll(Int64 MC_TNA_TMPLT_H_ID)
        {
            string sp = "pkg_tna.mc_tna_task_grp_select";
            try
            {
                var obList = new List<MC_TNA_TASK_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value =MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_TNA_TASK_GRPModel ob = new MC_TNA_TASK_GRPModel();
                            ob.MC_TNA_TASK_GRP_ID = (dr["MC_TNA_TASK_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_GRP_ID"]);
                            ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                            ob.TA_GRP_NAME_EN = (dr["TA_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_EN"]);
                            ob.TA_GRP_NAME_BN = (dr["TA_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_BN"]);
                            ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);                  
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                            ob.IS_START_TASK = (dr["IS_START_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_START_TASK"]);
                            ob.IS_END_TASK = (dr["IS_END_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_END_TASK"]);
                            ob.IS_PRODUCTION = (dr["IS_PRODUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRODUCTION"]);

                            ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_TNA_TASK_GRPModel Select(Int64 ID)
        {
            string sp = "pkg_tna.mc_tna_task_grp_select";
            try
            {
                var ob = new MC_TNA_TASK_GRPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_TNA_TASK_GRP_ID = (dr["MC_TNA_TASK_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_GRP_ID"]);
                        ob.TA_GRP_NAME_EN = (dr["TA_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_EN"]);
                        ob.TA_GRP_NAME_BN = (dr["TA_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_BN"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string TA_TASK_NAME_EN { get; set; }

        public long MC_TNA_TASK_ID { get; set; }

        public List<MC_TNA_TASK_GRPModel> parentTasksList(Int64 MC_TNA_TMPLT_H_ID)
        {
            string sp = "pkg_tna.mc_tna_task_grp_select";
            try
            {
                var obList = new List<MC_TNA_TASK_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value =MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TASK_GRPModel ob = new MC_TNA_TASK_GRPModel();
                    ob.MC_TNA_TASK_GRP_ID = (dr["MC_TNA_TASK_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_GRP_ID"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.TA_GRP_NAME_EN = (dr["TA_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_EN"]);
                    ob.TA_GRP_NAME_BN = (dr["TA_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_BN"]);
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.IS_START_TASK = (dr["IS_START_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_START_TASK"]);
                    ob.IS_END_TASK = (dr["IS_END_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_END_TASK"]);
                    ob.IS_PRODUCTION = (dr["IS_PRODUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRODUCTION"]);

                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SelectAll()
        {
            string sp = "pkg_tna.mc_tna_task_grp_select";
            try
            {
                var obList = new List<MC_TNA_TASK_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TASK_GRPModel ob = new MC_TNA_TASK_GRPModel();
                    ob.MC_TNA_TASK_GRP_ID = (dr["MC_TNA_TASK_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_GRP_ID"]);
                    ob.TA_GRP_NAME_EN = (dr["TA_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_EN"]);
                    ob.TA_GRP_NAME_BN = (dr["TA_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getDeSelectTnaTaskList(long pMC_STYLE_H_ID)
        {
            const string sp = "pkg_tna.GET_TNA_TASK_LIST";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "TNA_TASK_LIST", Value =500, Direction = ParameterDirection.Output}
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
    }
}