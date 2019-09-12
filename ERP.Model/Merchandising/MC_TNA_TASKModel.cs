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
    public class MC_TNA_TASKModel
    {
        public Int64 MC_TNA_TASK_ID { get; set; }
        public Int64 MC_TNA_TASK_GRP_ID { get; set; }
        public Int64 TA_TASK_SL { get; set; }
        public string TA_TASK_NAME_EN { get; set; }
        public string TA_TASK_NAME_BN { get; set; }
        public string IS_ACTIVE { get; set; }
        public Int64? RF_SMPL_TYPE_ID { get; set; }
        public string IS_SMPL_TASK { get; set; }
        public string TA_TASK_SNAME { get; set; }
        public string PAGE_URL { get; set; }

        public string Save()
        {
            const string sp = "pkg_tna.mc_tna_task_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = ob.MC_TNA_TASK_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value = ob.MC_TNA_TASK_GRP_ID},
                     new CommandParameter() {ParameterName = "pTA_TASK_SL", Value = ob.TA_TASK_SL},
                     new CommandParameter() {ParameterName = "pTA_TASK_NAME_EN", Value = ob.TA_TASK_NAME_EN},
                     new CommandParameter() {ParameterName = "pTA_TASK_NAME_BN", Value = ob.TA_TASK_NAME_BN},
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_SMPL_TASK", Value = ob.IS_SMPL_TASK},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_TNA_TASK_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_tna.mc_tna_task_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = ob.MC_TNA_TASK_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value = ob.MC_TNA_TASK_GRP_ID},
                     new CommandParameter() {ParameterName = "pTA_TASK_SL", Value = ob.TA_TASK_SL},
                     new CommandParameter() {ParameterName = "pTA_TASK_NAME_EN", Value = ob.TA_TASK_NAME_EN},
                     new CommandParameter() {ParameterName = "pTA_TASK_NAME_BN", Value = ob.TA_TASK_NAME_BN},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pRF_SMPL_TYPE_ID", Value = ob.RF_SMPL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_SMPL_TASK", Value = ob.IS_SMPL_TASK},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<MC_TNA_TASKModel> SelectAll(string IS_ORDER_EXEC)
        {
            string sp = "pkg_tna.mc_tna_task_select";
            try
            {
                var obList = new List<MC_TNA_TASKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pIS_ORDER_EXEC", Value = IS_ORDER_EXEC},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TASKModel ob = new MC_TNA_TASKModel();
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.MC_TNA_TASK_GRP_ID = (dr["MC_TNA_TASK_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_GRP_ID"]);

                    if (dr["RF_SMPL_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_SMPL_TYPE_ID = Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    }
                    ob.IS_SMPL_TASK = (dr["IS_SMPL_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMPL_TASK"]);
                    ob.TA_TASK_SL = (dr["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TA_TASK_SL"]);
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.TA_TASK_NAME_BN = (dr["TA_TASK_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_BN"]);
                    ob.TA_GRP_NAME_EN = (dr["TA_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_GRP_NAME_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.TA_TASK_SNAME = (dr["TA_TASK_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_SNAME"]);
                    ob.PAGE_URL = (dr["PAGE_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAGE_URL"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 
        public MC_TNA_TASKModel Select(Int64 ID)
        {
            string sp = "pkg_tna.mc_tna_task_select";
            try
            {
                var ob = new MC_TNA_TASKModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.MC_TNA_TASK_GRP_ID = (dr["MC_TNA_TASK_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_GRP_ID"]);
                    ob.TA_TASK_SL = (dr["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TA_TASK_SL"]);
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.TA_TASK_NAME_BN = (dr["TA_TASK_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_SMPL_TASK = (dr["IS_SMPL_TASK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMPL_TASK"]);
                    if (dr["RF_SMPL_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_SMPL_TYPE_ID = Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
                    }
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TASKModel> ListData(Int64 MC_TNA_TASK_GRP_ID)
        {
            string sp = "pkg_tna.mc_tna_task_select";
            try
            {
                var obList = new List<MC_TNA_TASKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value =MC_TNA_TASK_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TASKModel ob = new MC_TNA_TASKModel();
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.MC_TNA_TASK_GRP_ID = (dr["MC_TNA_TASK_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_GRP_ID"]);
                    ob.TA_TASK_SL = (dr["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TA_TASK_SL"]);
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.TA_TASK_NAME_BN = (dr["TA_TASK_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    if (dr["RF_SMPL_TYPE_ID"] != DBNull.Value)
                    {
                        ob.RF_SMPL_TYPE_ID = Convert.ToInt64(dr["RF_SMPL_TYPE_ID"]);
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

        public string TA_GRP_NAME_EN { get; set; }

        public List<MC_TNA_TASKModel> SelectTnaStatus()
        {
            string sp = "pkg_tna.mc_tna_task_select";
            try
            {
                var obList = new List<MC_TNA_TASKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_GRP_ID", Value =MC_TNA_TASK_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TASKModel ob = new MC_TNA_TASKModel();
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.MC_TNA_TASK_GRP_ID = (dr["MC_TNA_TASK_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_GRP_ID"]);
                    ob.TA_TASK_SL = (dr["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TA_TASK_SL"]);
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.TA_TASK_NAME_BN = (dr["TA_TASK_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_BN"]);
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

    }
}