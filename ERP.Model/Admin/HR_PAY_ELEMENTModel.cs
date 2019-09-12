using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HR_PAY_ELEMENTModel //: IHR_PAY_ELEMENTModel
    {
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public string PAY_ELEMENT_NAME_EN { get; set; }
        public string PAY_ELEMENT_NAME_BN { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_EFFECT_SALARY { get; set; }
        public Int64 LK_PAY_SLIP_GRP_ID { get; set; }
        public Int64 LK_PAY_ELM_TYPE_ID { get; set; }
        public Int64 LK_ROUND_MTHD_ID { get; set; }
        public Int64 LK_SP_FLAG_ID { get; set; }
        public Int64 ACC_COST_CENTER_ID { get; set; }
        public Int64 HR_PAY_CALC_MTHD_ID { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string PAY_ELEMENT_CODE { get; set; }
        public Decimal PAY_AMT { get; set; }
        public string IS_OTH_BILL { get; set; }
        public string IS_CORE_SAL_PART { get; set; }
        public string PAY_ELM_TYPE_NAME_EN { get; set; }

        public List<HR_PAY_ELEMENTModel> BillTypeListData()
        {
            string sp = "pkg_salary.hr_pay_element_select";

            try
            {
                var obList = new List<HR_PAY_ELEMENTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PAY_ELEMENTModel ob = new HR_PAY_ELEMENTModel();
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.PAY_ELEMENT_NAME_EN = (dr["PAY_ELEMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_NAME_EN"]);
                    ob.PAY_ELEMENT_NAME_BN = (dr["PAY_ELEMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_EFFECT_SALARY = (dr["IS_EFFECT_SALARY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EFFECT_SALARY"]);
                    ob.LK_PAY_SLIP_GRP_ID = (dr["LK_PAY_SLIP_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PAY_SLIP_GRP_ID"]);
                    ob.LK_PAY_ELM_TYPE_ID = (dr["LK_PAY_ELM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PAY_ELM_TYPE_ID"]);
                    ob.LK_ROUND_MTHD_ID = (dr["LK_ROUND_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ROUND_MTHD_ID"]);
                    ob.LK_SP_FLAG_ID = (dr["LK_SP_FLAG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SP_FLAG_ID"]);
                    ob.ACC_COST_CENTER_ID = (dr["ACC_COST_CENTER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_COST_CENTER_ID"]);
                    ob.HR_PAY_CALC_MTHD_ID = (dr["HR_PAY_CALC_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_CALC_MTHD_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.PAY_ELEMENT_CODE = (dr["PAY_ELEMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_CODE"]);
                    ob.PAY_AMT = (dr["PAY_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_AMT"]);
                    ob.IS_OTH_BILL = (dr["IS_OTH_BILL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OTH_BILL"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_PAY_ELEMENTModel> PayElementListData(string pIS_ACTIVE = null, string pIS_CORE_SAL_PART = null, Int64? pLK_PAY_ELM_TYPE_ID = null)
        {
            string sp = "pkg_salary.hr_pay_element_select";
            try
            {
                var obList = new List<HR_PAY_ELEMENTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = pIS_ACTIVE},
                    new CommandParameter() {ParameterName = "pIS_CORE_SAL_PART", Value = pIS_CORE_SAL_PART},
                    new CommandParameter() {ParameterName = "pLK_PAY_ELM_TYPE_ID", Value = pLK_PAY_ELM_TYPE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3004},                  
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PAY_ELEMENTModel ob = new HR_PAY_ELEMENTModel();
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.PAY_ELEMENT_NAME_EN = (dr["PAY_ELEMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_NAME_EN"]);
                    ob.PAY_ELEMENT_NAME_BN = (dr["PAY_ELEMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_EFFECT_SALARY = (dr["IS_EFFECT_SALARY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EFFECT_SALARY"]);
                    ob.LK_PAY_SLIP_GRP_ID = (dr["LK_PAY_SLIP_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PAY_SLIP_GRP_ID"]);
                    ob.LK_PAY_ELM_TYPE_ID = (dr["LK_PAY_ELM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PAY_ELM_TYPE_ID"]);
                    ob.LK_ROUND_MTHD_ID = (dr["LK_ROUND_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ROUND_MTHD_ID"]);
                    ob.LK_SP_FLAG_ID = (dr["LK_SP_FLAG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SP_FLAG_ID"]);
                    ob.ACC_COST_CENTER_ID = (dr["ACC_COST_CENTER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_COST_CENTER_ID"]);
                    ob.HR_PAY_CALC_MTHD_ID = (dr["HR_PAY_CALC_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_CALC_MTHD_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.PAY_ELEMENT_CODE = (dr["PAY_ELEMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_CODE"]);
                    ob.PAY_AMT = (dr["PAY_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_AMT"]);
                    ob.IS_OTH_BILL = (dr["IS_OTH_BILL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OTH_BILL"]);
                    ob.IS_CORE_SAL_PART = (dr["IS_CORE_SAL_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORE_SAL_PART"]);
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

        public List<HR_PAY_ELEMENTModel> SalaryBreakupListData(Int64? pGROSS_SALARY)
        {
            string sp = "pkg_salary.salary_breakup_select";
            try
            {
                var obList = new List<HR_PAY_ELEMENTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pGROSS_SALARY", Value = pGROSS_SALARY},
		            new CommandParameter() {ParameterName = "pOption", Value = 3000},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PAY_ELEMENTModel ob = new HR_PAY_ELEMENTModel();
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.PAY_ELEMENT_NAME_EN = (dr["PAY_ELEMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_NAME_EN"]);
                    //ob.PAY_ELEMENT_NAME_BN = (dr["PAY_ELEMENT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_EFFECT_SALARY = (dr["IS_EFFECT_SALARY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EFFECT_SALARY"]);
                    ob.IS_CORE_SAL_PART = (dr["IS_CORE_SAL_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORE_SAL_PART"]);
                    //ob.LK_PAY_SLIP_GRP_ID = (dr["LK_PAY_SLIP_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PAY_SLIP_GRP_ID"]);
                    //ob.LK_PAY_ELM_TYPE_ID = (dr["LK_PAY_ELM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PAY_ELM_TYPE_ID"]);
                    //ob.LK_ROUND_MTHD_ID = (dr["LK_ROUND_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ROUND_MTHD_ID"]);
                    //ob.LK_SP_FLAG_ID = (dr["LK_SP_FLAG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SP_FLAG_ID"]);
                    //ob.ACC_COST_CENTER_ID = (dr["ACC_COST_CENTER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_COST_CENTER_ID"]);
                    //ob.HR_PAY_CALC_MTHD_ID = (dr["HR_PAY_CALC_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_CALC_MTHD_ID"]);
                    //ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    //ob.PAY_ELEMENT_CODE = (dr["PAY_ELEMENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_ELEMENT_CODE"]);
                    ob.PAY_AMT = (dr["PAY_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_AMT"]);
                    //ob.IS_OTH_BILL = (dr["IS_OTH_BILL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OTH_BILL"]);
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