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
    public class MC_USR_TNA_TASKModel
    {
        public Int64 MC_USR_TNA_TASK_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public Int64 MC_TNA_TASK_ID { get; set; }
        public string CRUD_FLAG { get; set; }
        public string IS_ACTIVE { get; set; }
        public Int64 TA_TASK_SL { get; set; }
        public String TA_TASK_NAME_EN { get; set; }
        public String XML { get; set; }
        public String LOGIN_ID { get; set; }
        public String USER_NAME_EN { get; set; }
        public String USER_EMAIL { get; set; }
        


        public string Save()
        {
            const string sp = "pkg_tna.mc_tna_tmplt_h_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2001},
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

        public List<MC_USR_TNA_TASKModel> FindTaskListByUser(Int64? SC_USER_ID)
        {
            string sp = "pkg_tna.mc_tna_tmplt_h_select";
            try
            {
                var obList = new List<MC_USR_TNA_TASKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =SC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_USR_TNA_TASKModel ob = new MC_USR_TNA_TASKModel();
                            ob.MC_USR_TNA_TASK_ID = (dr["MC_USR_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_USR_TNA_TASK_ID"]);
                            ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                            ob.TA_TASK_SL = (dr["TA_TASK_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TA_TASK_SL"]);
                            ob.CRUD_FLAG = (dr["CRUD_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CRUD_FLAG"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_USR_TNA_TASKModel> FindTnaMappedUserList()
        {
            string sp = "pkg_tna.mc_tna_tmplt_h_select";
            try
            {
                var obList = new List<MC_USR_TNA_TASKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_USR_TNA_TASKModel ob = new MC_USR_TNA_TASKModel();
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);

                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
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