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
    public class HR_SAL_ADV_APRVLModel
    {
        public long DUE_INSTL;
        public Int64 HR_SAL_ADV_APRVL_ID { get; set; }
        public Int64 HR_SAL_ADVANCE_ID { get; set; }
        public Int64 APPROVER_ID { get; set; }
        public DateTime ACTION_DATE { get; set; }
        public Int64 LK_ADV_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 APROVER_LVL_NO { get; set; }
        public string IS_VISIBLE { get; set; }

        public string LK_ADV_STATUS { get; set; }

        public string ADV_REF_NO { get; set; }

        public string APPROVED_BY { get; set; }

        public Int64 HR_COMPANY_ID { get; set; }

        public Int64 HR_OFFICE_ID { get; set; }

        public Int64 HR_DEPARTMENT_ID { get; set; }

        public Int64 LK_FLOOR_ID { get; set; }

        public Int64 LINE_NO { get; set; }

        public Int64 SC_MENU_ID { get; set; }

        public Int64 DESIG_ORDER { get; set; }

        public string EMPLOYEE_CODE { get; set; }

        public string EMP_FULL_NAME_EN { get; set; }

        public string DESIGNATION_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string LK_ADV_TYPE { get; set; }

        public Int64 HR_EMPLOYEE_ID { get; set; }

        public Int64 ADV_RQST_AMT { get; set; }

        public string REASON_ADV { get; set; }

        public long NO_OF_INSTL { get; set; }

        public decimal INSTL_AMT { get; set; }

        public DateTime DEDU_ST_DT { get; set; }

        public List<HR_SAL_ADV_APRVLModel> SalaryAdvApproverNotif()
        {
            string sp = "pkg_hr.hr_sal_advance_select";
            try
            {
                var obList = new List<HR_SAL_ADV_APRVLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])}
                }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SAL_ADV_APRVLModel ob = new HR_SAL_ADV_APRVLModel();
                    ob.ADV_REF_NO = (dr["ADV_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADV_REF_NO"]);
                    ob.HR_SAL_ADV_APRVL_ID = (dr["HR_SAL_ADV_APRVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADV_APRVL_ID"]);
                    ob.HR_SAL_ADVANCE_ID = (dr["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADVANCE_ID"]);
                    ob.APPROVER_ID = (dr["APPROVER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPROVER_ID"]);
                    if (dr["ACTION_DATE"] != DBNull.Value) { ob.ACTION_DATE = Convert.ToDateTime(dr["ACTION_DATE"]); }
                    ob.LK_ADV_STATUS_ID = (dr["LK_ADV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_STATUS_ID"]);
                    ob.ADV_RQST_AMT = (dr["ADV_RQST_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADV_RQST_AMT"]);
                    ob.NO_OF_INSTL = (dr["NO_OF_INSTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_INSTL"]);
                    ob.INSTL_AMT = (dr["INSTL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INSTL_AMT"]);
                    if (dr["DEDU_ST_DT"] != DBNull.Value) { ob.DEDU_ST_DT = Convert.ToDateTime(dr["DEDU_ST_DT"]); }
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REASON_ADV = (dr["REASON_ADV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_ADV"]);
                    ob.LK_ADV_TYPE = (dr["LK_ADV_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ADV_TYPE"]);
                    ob.LK_ADV_STATUS = (dr["LK_ADV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ADV_STATUS"]);
                    if (dr["CREATION_DATE"] != DBNull.Value) { ob.CREATION_DATE = Convert.ToDateTime(dr["CREATION_DATE"]); }
                    if (dr["LAST_UPDATE_DATE"] != DBNull.Value) { ob.LAST_UPDATE_DATE = Convert.ToDateTime(dr["LAST_UPDATE_DATE"]); }
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.IS_VISIBLE = (dr["IS_VISIBLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_VISIBLE"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.SC_MENU_ID = (dr["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
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
        public List<HR_SAL_ADV_APRVLModel> SalAdvNotif(Int64 HR_SAL_ADVANCE_ID)
        {
            string sp = "pkg_hr.hr_sal_advance_select";
            try
            {
                var obList = new List<HR_SAL_ADV_APRVLModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3005},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SAL_ADV_APRVLModel ob = new HR_SAL_ADV_APRVLModel();
                    if (dr["ACTION_DATE"] != DBNull.Value) { ob.ACTION_DATE = Convert.ToDateTime(dr["ACTION_DATE"]); }
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_ADV_STATUS = (dr["LK_ADV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ADV_STATUS"]);
                    if (dr["CREATION_DATE"] != DBNull.Value) { ob.CREATION_DATE = Convert.ToDateTime(dr["CREATION_DATE"]); }
                    ob.APPROVED_BY = (dr["APPROVED_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APPROVED_BY"]);
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.IS_VISIBLE = (dr["IS_VISIBLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_VISIBLE"]);
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