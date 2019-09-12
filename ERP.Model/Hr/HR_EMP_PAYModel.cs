using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HR_EMP_PAYModel //: IHR_EMP_PAYModel
    {
        public Int64 HR_EMP_PAY_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public Decimal PAY_AMT { get; set; }
        public string IS_EFFECT_SALARY { get; set; }
        public string REMARKS { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string PAY_ELEMENT_NAME_EN { get; set; }
        public string PAY_ELM_TYPE_NAME_EN { get; set; }
        public string IS_CORE_SAL_PART { get; set; }

        public List<HR_EMP_PAYModel> EmpPayListData(Int64? pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_hr.hr_emp_pay_select";
            try
            {
                var obList = new List<HR_EMP_PAYModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMP_PAYModel ob = new HR_EMP_PAYModel();
                    ob.HR_EMP_PAY_ID = (dr["HR_EMP_PAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMP_PAY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.PAY_AMT = (dr["PAY_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_AMT"]);
                    ob.IS_EFFECT_SALARY = (dr["IS_EFFECT_SALARY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EFFECT_SALARY"]);
                    ob.IS_CORE_SAL_PART = (dr["IS_CORE_SAL_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORE_SAL_PART"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.PAY_ELEMENT_NAME_EN = (dr["PAY_ELEMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_NAME_EN"]);
                    ob.PAY_ELM_TYPE_NAME_EN = (dr["PAY_ELM_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELM_TYPE_NAME_EN"]);
                    
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