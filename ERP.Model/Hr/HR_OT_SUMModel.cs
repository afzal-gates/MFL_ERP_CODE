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
    public class HR_OT_SUMModel
    {
        public Int64 HR_OT_SUM_ID { get; set; }
        public Int64 ACC_PAY_PERIOD_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public Int64 HR_OT_TYPE_ID { get; set; }
        public Int64 OT_QTY { get; set; }
        public string OT_UNIT_FLAG { get; set; }

        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }


        public string Save()
        {
            const string sp = "pkg_hr.hr_ot_sum_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_OT_SUM_ID", Value = ob.HR_OT_SUM_ID},
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOT_QTY", Value = ob.OT_QTY},
                     new CommandParameter() {ParameterName = "pOT_UNIT_FLAG", Value = ob.OT_UNIT_FLAG},                     
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

        public string Remove()
        {
            const string sp = "pkg_hr.hr_ot_sum_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_OT_SUM_ID", Value = ob.HR_OT_SUM_ID},
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOT_QTY", Value = ob.OT_QTY},
                     new CommandParameter() {ParameterName = "pOT_UNIT_FLAG", Value = ob.OT_UNIT_FLAG},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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



        public List<HR_OT_SUMModel> GetOtSummeryList(int pACC_PAY_PERIOD_ID)
        {
            string sp = "pkg_hr.hr_ot_sum_select";
            try
            {
                var obList = new List<HR_OT_SUMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_OT_SUMModel ob = new HR_OT_SUMModel();
                    ob.HR_OT_SUM_ID = (dr["HR_OT_SUM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OT_SUM_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_OT_TYPE_ID = (dr["HR_OT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OT_TYPE_ID"]);
                    ob.OT_QTY = (dr["OT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_QTY"]);
                    ob.OT_UNIT_FLAG = (dr["OT_UNIT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_UNIT_FLAG"]);

                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    
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