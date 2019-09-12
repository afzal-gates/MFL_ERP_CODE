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
    public class RF_RESP_DEPTModel
    {
        public Int64 RF_RESP_DEPT_ID { get; set; }
        public string RESP_DEPT_CODE { get; set; }
        public string RESP_DEPT_NAME { get; set; }
        public Int64? LNK_HR_DEPT_ID { get; set; }
        public string IS_ACTIVE { get; set; }


        public string Save()
        {
            const string sp = "pkg_knit_common.rf_resp_dept_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
                     //new CommandParameter() {ParameterName = "pRESP_DEPT_CODE", Value = ob.RESP_DEPT_CODE},
                     new CommandParameter() {ParameterName = "pRESP_DEPT_NAME", Value = ob.RESP_DEPT_NAME},
                     new CommandParameter() {ParameterName = "pLNK_HR_DEPT_ID", Value = ob.LNK_HR_DEPT_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                
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

        public List<RF_RESP_DEPTModel> getRespDeptList()
        {
            string sp = "pkg_knit_common.rf_resp_dept_select";
            try
            {
                var obList = new List<RF_RESP_DEPTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_RESP_DEPTModel ob = new RF_RESP_DEPTModel();
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.RESP_DEPT_CODE = (dr["RESP_DEPT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_CODE"]);
                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);
                    ob.LNK_HR_DEPT_ID = (dr["LNK_HR_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LNK_HR_DEPT_ID"]);
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


        public List<RF_RESP_DEPTModel> getRespDeptListByID(string pMULTI_DEPT_LST = null)
        {
            string sp = "pkg_knit_common.rf_resp_dept_select";
            try
            {
                var obList = new List<RF_RESP_DEPTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pMULTI_DEPT_LST", Value =pMULTI_DEPT_LST},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_RESP_DEPTModel ob = new RF_RESP_DEPTModel();
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.RESP_DEPT_CODE = (dr["RESP_DEPT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_CODE"]);
                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);
                    ob.LNK_HR_DEPT_ID = (dr["LNK_HR_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LNK_HR_DEPT_ID"]);
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

        public RF_RESP_DEPTModel Select(long ID)
        {
            string sp = "Select_RF_RESP_DEPT";
            try
            {
                var ob = new RF_RESP_DEPTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.RESP_DEPT_CODE = (dr["RESP_DEPT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_CODE"]);
                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);
                    ob.LNK_HR_DEPT_ID = (dr["LNK_HR_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LNK_HR_DEPT_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}