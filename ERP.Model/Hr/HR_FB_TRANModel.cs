using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data;
using System.Data.Common;

namespace ERP.Model
{
    public class HR_FB_TRANModel //: IHR_FB_TRANModel
    {
        public Int64 HR_FB_TRAN_ID { get; set; }
        public Int64 ACC_PAY_PERIOD_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public DateTime FB_LIMIT_DT { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 HR_DESIGNATION_ID { get; set; }
        public Int64 HR_GRADE_ID { get; set; }
        public Int64 LK_FLOOR_ID { get; set; }
        public Int64 LINE_NO { get; set; }
        public Decimal GROSS_SALARY { get; set; }
        public Decimal BASIC_SALARY { get; set; }
        public Decimal SERV_LEN_YR { get; set; }
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public Int64 LK_FB_TYPE_ID { get; set; }
        public Decimal PAY_AMT { get; set; }
        public string IS_FB_OFF { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }

        public string BonusProcess(Int64? pHR_COMPANY_ID, Int64? pACC_PAY_PERIOD_ID, Int64? pHR_DEPARTMENT_ID,
            Int64? pHR_DESIGNATION_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID,
            Int64? pHR_MANAGEMENT_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pCORE_DEPT_ID, Int64? pLK_FB_TYPE_ID,
            DateTime? pFB_LIMIT_DT)
        {
            const string sp = "pkg_salary.hr_bonus_process";            
            string vMsg = "";
            
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = pHR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = pHR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = pLK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value =pLINE_NO},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = pHR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pLK_FB_TYPE_ID", Value = pLK_FB_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFB_LIMIT_DT", Value = pFB_LIMIT_DT},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }
    }
}