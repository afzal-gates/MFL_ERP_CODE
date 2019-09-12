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
    public class HR_PAY_BILLModel //: IHR_PAY_BILLModel
    {
        public Int64 HR_PAY_BILL_ID { get; set; }
        public Int64 ACC_PAY_PERIOD_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
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
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public DateTime PAY_DT { get; set; }
        public Decimal CLK_OUT { get; set; }
        public Decimal PAY_QTY { get; set; }
        public string PAY_UNIT_FLAG { get; set; }
        public Decimal PAY_RATE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }


        public string OtherBillProcess(DateTime? pFromDate, DateTime? pToDate, string pPAY_ELEMENT_CODE, string pOT_TYPES,
                                        Int64 pHR_COMPANY_ID, Int64 pACC_PAY_PERIOD_ID, Int64 pHR_PAY_ELEMENT_ID, string pHR_EMPLOYEE_IDS, Int64? pCORE_DEPT_ID,
                                        Int64? pHR_DEPARTMENT_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pEMPLOYEE_TYPE_ID,
                                        Int64? pHR_OFFICE_ID = null)
        {
            const string sp = "pkg_ot.hr_pay_bill_process";            
            string vMsg = "";
            OraDatabase db = new OraDatabase();

            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DT", Value = pFromDate},
                    new CommandParameter() {ParameterName = "pTO_DT", Value = pToDate},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = pHR_PAY_ELEMENT_ID},
                    new CommandParameter() {ParameterName = "pPAY_ELEMENT_CODE", Value = pPAY_ELEMENT_CODE},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_IDS", Value = pHR_EMPLOYEE_IDS},                    
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = pHR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = pLK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = pLINE_NO},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = pEMPLOYEE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pOT_TYPES", Value = pOT_TYPES},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                    new CommandParameter() {ParameterName = "pOption", Value = 0000},
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

        public string OTBillProcessData(DateTime? pFromDate, DateTime? pToDate, string pPAY_ELEMENT_CODE, string pOT_TYPES,
                                            Int64 pHR_COMPANY_ID, Int64 pACC_PAY_PERIOD_ID, string pHR_EMPLOYEE_IDS, Int64? pCORE_DEPT_ID,
                                            Int64? pHR_DEPARTMENT_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pEMPLOYEE_TYPE_ID)
        {
            const string sp = "pkg_ot.hr_ot_bill_process";            
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pFROM_DT", Value = pFromDate},
                    new CommandParameter() {ParameterName = "pTO_DT", Value = pToDate},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pPAY_ELEMENT_CODE", Value = pPAY_ELEMENT_CODE},
                    new CommandParameter() {ParameterName = "pOT_TYPES", Value = pOT_TYPES},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_IDS", Value = pHR_EMPLOYEE_IDS},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = pHR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = pLK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = pLINE_NO},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = pEMPLOYEE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                    new CommandParameter() {ParameterName = "pOption", Value = 0000},
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