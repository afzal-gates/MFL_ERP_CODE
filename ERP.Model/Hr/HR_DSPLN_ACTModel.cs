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
    public class HR_DSPLN_ACTModel //: IHR_DSPLN_ACTModel
    {
        public Int64 HR_DSPLN_ACT_ID { get; set; }
        public string DISP_ACT_REF_NO { get; set; }

        //[Required(ErrorMessage="Please select effective period")]
        public Int64 RF_FISCAL_YEAR_ID { get; set; }
        //[Required(ErrorMessage="Please select a month")]
        public Int64 RF_CAL_MONTH_ID { get; set; }
        [Required(ErrorMessage="Please select a company")]
        public Int64 HR_COMPANY_ID { get; set; }
        [Required(ErrorMessage="Please select an employee")]
        public Int64 HR_EMPLOYEE_ID { get; set; }
        [Required(ErrorMessage="Please select proposed date")]
        public DateTime PROPOSED_DT { get; set; }

        [Required(ErrorMessage="Please select disciplinary action type")]
        public Int64 HR_DSPLN_ACT_TYPE_ID { get; set; }
        public string ACTION_DESC_EN { get; set; }
        public string ACTION_DESC_BN { get; set; }
        public Int64? LK_DPA_REASON_TYPE_ID { get; set; }
        public string REASON_DESC_EN { get; set; }
        public string REASON_DESC_BN { get; set; }

        //[Required(ErrorMessage = "Please select effective period")]
        public DateTime EFFECTIVE_DT { get; set; }

        //[Required(ErrorMessage="Please select deduction on")]
        public string IS_B_OR_G { get; set; }

        public string IS_DAY_OR_AMT { get; set; }
        public Int64? NO_DAYS { get; set; }
        public Decimal? DEDU_AMT { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        [Required(ErrorMessage="Please select proposed by")]
        public Int64 ACTION_BY_ID { get; set; }
        //public Int64 DEDUCTION_BASE_ID { get; set; }
        public string ABSENT_DAYS { get; set; }


        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string PROPOSED_EMPLOYEE_CODE { get; set; }
        public string PROPOSED_EMP_FULL_NAME_EN { get; set; }
        public string MONTH_YEAR_NAME { get; set; }
        public string DA_TYPE_NAME_EN { get; set; }
        public string LK_DATA_NAME_EN { get; set; }
        public string IS_EFFECT_SALARY { get; set; }

        public string IS_DEDUCT_SALARY4FS { get; set; }


        public string Save()
        {
            const string sp = "pkg_hr.hr_dspln_act_insert";            
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_DSPLN_ACT_ID", Value = ob.HR_DSPLN_ACT_ID},
                    new CommandParameter() {ParameterName = "pDISP_ACT_REF_NO", Value = ob.DISP_ACT_REF_NO},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = ob.RF_CAL_MONTH_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPROPOSED_DT", Value = ob.PROPOSED_DT},
                    new CommandParameter() {ParameterName = "pHR_DSPLN_ACT_TYPE_ID", Value = ob.HR_DSPLN_ACT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pACTION_DESC_EN", Value = ob.ACTION_DESC_EN},
                    new CommandParameter() {ParameterName = "pACTION_DESC_BN", Value = ob.ACTION_DESC_BN},
                    new CommandParameter() {ParameterName = "pLK_DPA_REASON_TYPE_ID", Value = ob.LK_DPA_REASON_TYPE_ID},
                    new CommandParameter() {ParameterName = "pREASON_DESC_EN", Value = ob.REASON_DESC_EN},
                    new CommandParameter() {ParameterName = "pREASON_DESC_BN", Value = ob.REASON_DESC_BN},
                    new CommandParameter() {ParameterName = "pEFFECTIVE_DT", Value = ob.EFFECTIVE_DT},
                    new CommandParameter() {ParameterName = "pIS_B_OR_G", Value = ob.IS_B_OR_G},
                    new CommandParameter() {ParameterName = "pIS_DAY_OR_AMT", Value = ob.IS_DAY_OR_AMT},
                    new CommandParameter() {ParameterName = "pNO_DAYS", Value = ob.NO_DAYS},
                    new CommandParameter() {ParameterName = "pDEDU_AMT", Value = ob.DEDU_AMT},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pACTION_BY_ID", Value = ob.ACTION_BY_ID},
                    new CommandParameter() {ParameterName = "pABSENT_DAYS", Value = ob.ABSENT_DAYS},

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

        public string Update()
        {
            const string sp = "pkg_hr.hr_dspln_act_update";            
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_DSPLN_ACT_ID", Value = ob.HR_DSPLN_ACT_ID},
                    new CommandParameter() {ParameterName = "pDISP_ACT_REF_NO", Value = ob.DISP_ACT_REF_NO},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = ob.RF_CAL_MONTH_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPROPOSED_DT", Value = ob.PROPOSED_DT},
                    new CommandParameter() {ParameterName = "pHR_DSPLN_ACT_TYPE_ID", Value = ob.HR_DSPLN_ACT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pACTION_DESC_EN", Value = ob.ACTION_DESC_EN},
                    new CommandParameter() {ParameterName = "pACTION_DESC_BN", Value = ob.ACTION_DESC_BN},
                    new CommandParameter() {ParameterName = "pLK_DPA_REASON_TYPE_ID", Value = ob.LK_DPA_REASON_TYPE_ID},
                    new CommandParameter() {ParameterName = "pREASON_DESC_EN", Value = ob.REASON_DESC_EN},
                    new CommandParameter() {ParameterName = "pREASON_DESC_BN", Value = ob.REASON_DESC_BN},
                    new CommandParameter() {ParameterName = "pEFFECTIVE_DT", Value = ob.EFFECTIVE_DT},
                    new CommandParameter() {ParameterName = "pIS_B_OR_G", Value = ob.IS_B_OR_G},
                    new CommandParameter() {ParameterName = "pIS_DAY_OR_AMT", Value = ob.IS_DAY_OR_AMT},
                    new CommandParameter() {ParameterName = "pNO_DAYS", Value = ob.NO_DAYS},
                    new CommandParameter() {ParameterName = "pDEDU_AMT", Value = ob.DEDU_AMT},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pACTION_BY_ID", Value = ob.ACTION_BY_ID},
                    new CommandParameter() {ParameterName = "pABSENT_DAYS", Value = ob.ABSENT_DAYS},

                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
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

        public long DeductAmountData()
        {
            string sp = "pkg_hr.hr_dspln_act_select";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                Int64 vDeductAmount = 0;

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pIS_B_OR_G", Value = ob.IS_B_OR_G},
                    new CommandParameter() {ParameterName = "pNO_DAYS", Value = ob.NO_DAYS},
                    new CommandParameter() {ParameterName = "pEFFECTIVE_DT", Value = ob.EFFECTIVE_DT},

                    new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pMsg", Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    vDeductAmount = (dr["DEDU_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DEDU_AMT"]);
                }
                
                return vDeductAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_DSPLN_ACTModel> DisciplinActionListData(Int64? pHR_COMPANY_ID, Int64? pRF_FISCAL_YEAR_ID, Int64? pRF_CAL_MONTH_ID,
                                        Int64? pHR_EMPLOYEE_ID, string pDISP_ACT_REF_NO)
        {
            string sp = "pkg_hr.hr_dspln_act_select";
            try
            {
                var obList = new List<HR_DSPLN_ACTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = pRF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = pRF_CAL_MONTH_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pDISP_ACT_REF_NO", Value = pDISP_ACT_REF_NO},

                    new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_DSPLN_ACTModel ob = new HR_DSPLN_ACTModel();
                    ob.HR_DSPLN_ACT_ID = (dr["HR_DSPLN_ACT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DSPLN_ACT_ID"]);
                    ob.DISP_ACT_REF_NO = (dr["DISP_ACT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DISP_ACT_REF_NO"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.PROPOSED_DT = (dr["PROPOSED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROPOSED_DT"]);
                    ob.HR_DSPLN_ACT_TYPE_ID = (dr["HR_DSPLN_ACT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DSPLN_ACT_TYPE_ID"]);
                    ob.ACTION_DESC_EN = (dr["ACTION_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_DESC_EN"]);
                    ob.ACTION_DESC_BN = (dr["ACTION_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_DESC_BN"]);
                    ob.LK_DPA_REASON_TYPE_ID = (dr["LK_DPA_REASON_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DPA_REASON_TYPE_ID"]);
                    ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);
                    ob.REASON_DESC_BN = (dr["REASON_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_BN"]);
                    ob.EFFECTIVE_DT = (dr["EFFECTIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_DT"]);
                    ob.IS_B_OR_G = (dr["IS_B_OR_G"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_B_OR_G"]);
                    ob.IS_DAY_OR_AMT = (dr["IS_DAY_OR_AMT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OR_AMT"]);
                    ob.NO_DAYS = (dr["NO_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS"]);
                    ob.DEDU_AMT = (dr["DEDU_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DEDU_AMT"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    //ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    //ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);

                    ob.ACTION_BY_ID = (dr["ACTION_BY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACTION_BY_ID"]);
                    ob.PROPOSED_EMPLOYEE_CODE = (dr["PROPOSED_EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROPOSED_EMPLOYEE_CODE"]);
                    ob.PROPOSED_EMP_FULL_NAME_EN = (dr["PROPOSED_EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROPOSED_EMP_FULL_NAME_EN"]);

                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);
                    ob.DA_TYPE_NAME_EN = (dr["DA_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DA_TYPE_NAME_EN"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.ABSENT_DAYS = (dr["ABSENT_DAYS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ABSENT_DAYS"]);
                    ob.IS_EFFECT_SALARY = (dr["IS_EFFECT_SALARY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EFFECT_SALARY"]);

                    obList.Add(ob);                    
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public List<HR_DSPLN_ACTModel> AutoSearchDisciplinActionListData(string pDISP_ACT_REF_NO)
        {
            string sp = "pkg_hr.hr_dspln_act_select";
            try
            {
                var obList = new List<HR_DSPLN_ACTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDISP_ACT_REF_NO", Value = pDISP_ACT_REF_NO},

                    new CommandParameter() {ParameterName = "pOption", Value = 3003},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_DSPLN_ACTModel ob = new HR_DSPLN_ACTModel();
                    ob.HR_DSPLN_ACT_ID = (dr["HR_DSPLN_ACT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DSPLN_ACT_ID"]);
                    ob.DISP_ACT_REF_NO = (dr["DISP_ACT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DISP_ACT_REF_NO"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.PROPOSED_DT = (dr["PROPOSED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROPOSED_DT"]);
                    ob.HR_DSPLN_ACT_TYPE_ID = (dr["HR_DSPLN_ACT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DSPLN_ACT_TYPE_ID"]);
                    ob.ACTION_DESC_EN = (dr["ACTION_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_DESC_EN"]);
                    ob.ACTION_DESC_BN = (dr["ACTION_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_DESC_BN"]);
                    ob.LK_DPA_REASON_TYPE_ID = (dr["LK_DPA_REASON_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DPA_REASON_TYPE_ID"]);
                    ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);
                    ob.REASON_DESC_BN = (dr["REASON_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_BN"]);
                    ob.EFFECTIVE_DT = (dr["EFFECTIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_DT"]);
                    ob.IS_B_OR_G = (dr["IS_B_OR_G"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_B_OR_G"]);
                    ob.IS_DAY_OR_AMT = (dr["IS_DAY_OR_AMT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OR_AMT"]);
                    ob.NO_DAYS = (dr["NO_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS"]);
                    ob.DEDU_AMT = (dr["DEDU_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DEDU_AMT"]);
                    
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