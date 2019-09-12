using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data;

namespace ERP.Model
{
    public class HR_EMPLOYEEModel //: IHR_EMPLOYEEModel
    {
        public Int64 HR_EMPLOYEE_ID { get; set; }

        //[Required(ErrorMessage="Please input employee code")]
        public string EMPLOYEE_CODE { get; set; }
        
        public string OLD_EMP_CODE { get; set; }
        public Int64? REPORTING_MGR_ID { get; set; }

        //[MinLength(10, ErrorMessage = "Please input minimum 10 charecter for proxi ID")]
        //[MaxLength(10,ErrorMessage="Please input maximum 10 charecter for proxi ID")]        
        public string TA_PROXI_ID { get; set; }

        [MaxLength(10, ErrorMessage = "Please input maximum 10 charecter for card#")]        
        public string CARD_NO { get; set; }
        [MaxLength(10, ErrorMessage = "Please input maximum 10 charecter for employee title")]
        
        public string EMP_TITLE { get; set; }

        [MaxLength(50, ErrorMessage = "Please input maximum 50 charecter for employee first name [E]")]
        public string EMP_FIRST_NAME_EN { get; set; }
        [MaxLength(50, ErrorMessage = "Please input maximum 50 charecter for employee first name [B]")]
        public string EMP_FIRST_NAME_BN { get; set; }
        [MaxLength(50, ErrorMessage = "Please input maximum 50 charecter for employee last name [E]")]
        public string EMP_LAST_NAME_EN { get; set; }
        [MaxLength(50, ErrorMessage = "Please input maximum 50 charecter for employee last name [B]")]
        public string EMP_LAST_NAME_BN { get; set; }

        [Required(ErrorMessage = "Please input full name [E]")]
        [MaxLength(100, ErrorMessage = "Please input maximum 100 charecter for full name [E]")]
        public string EMP_FULL_NAME_EN { get; set; }

        //[Required(ErrorMessage = "Please input full name [B]")]
        [MaxLength(100, ErrorMessage = "Please input maximum 100 charecter for full name [B]")]
        public string EMP_FULL_NAME_BN { get; set; }

        [MaxLength(20, ErrorMessage = "Please input maximum 20 charecter for nick name [E]")]
        public string EMP_NICKNM_EN { get; set; }
        [MaxLength(20, ErrorMessage = "Please input maximum 20 charecter for nick name [B]")]
        public string EMP_NICKNM_BN { get; set; }

        [Required(ErrorMessage = "Please select company")]
        public Int64 HR_COMPANY_ID { get; set; }
        [Required(ErrorMessage="Please select office")]
        public Int64 HR_OFFICE_ID { get; set; }

        public Int64? SAL_COMPANY_ID { get; set; }

        [Required(ErrorMessage="Please select core department")]
        public Int64 CORE_DEPT_ID { get; set; }

        [Required(ErrorMessage = "Please select section")]
        public Int64 HR_DEPARTMENT_ID { get; set; }

        [Required(ErrorMessage="Please select designation")]
        public Int64 HR_DESIGNATION_ID { get; set; }
        public Int64? HR_GRADE_ID { get; set; }


        public DateTime? BIRTH_DT { get; set; }

        //[MaxLength(2, ErrorMessage = "Please input maximum 2 charecter")]
        public Int64? LK_FLOOR_ID { get; set; }
        //[Range(1, 99, ErrorMessage = "Please input between 1 & 50 for line#")]
       // [MaxLength(2, ErrorMessage = "Please input maximum 2 charecter")]
        public Int64? LINE_NO { get; set; }

        public Int64? LK_APNT_STATUS_ID { get; set; }

        [Required(ErrorMessage="Please select job status")]
        public Int64? LK_JOB_STATUS_ID { get; set; }
        public Int64? LK_EMP_TYPE_ID { get; set; }
        public Int64? LK_MNG_LVL_ID { get; set; }
        public Int64? LK_JOB_CATG_ID { get; set; }
        public DateTime? CONFIRM_DT { get; set; }

        [Required(ErrorMessage = "Please select joining date")]
        public DateTime? JOINING_DT { get; set; }
        public DateTime? APNT_ISSUE_DT { get; set; }
        public Int64? PROBATION_PERIOD { get; set; }
        public Int64? PR_PERIOD_TYPE_ID { get; set; }
        public DateTime? PROBATION_DT { get; set; }
        public DateTime? OFFER_DT { get; set; }

        //public string INTERVIEW_HELD { get; set; }

        //public DateTime INTERVIEW_DT { get; set; }
        public DateTime? REPORTING_DT { get; set; }

        public Int64? CONTRACT_PERIOD { get; set; }
        public Int64? CT_PERIOD_TYPE_ID { get; set; }
        public Int64? DUTY_HOUR { get; set; }
        public Int64? ALLOWED_LEAVE_DAY { get; set; }
        public Decimal? GROSS_SALARY { get; set; }
        public Int64? ACC_BK_ACCOUNT_ID { get; set; }

        [Required(ErrorMessage = "Please input present address [E]")]
        public string PRE_ADDRESS_EN { get; set; }
        //[Required(ErrorMessage = "Please input present address [B]")]
        public string PRE_ADDRESS_BN { get; set; }
        [Required(ErrorMessage = "Please select present division")]
        public Int64 PRE_DIVISION_ID { get; set; }
        [Required(ErrorMessage = "Please select present district")]
        public Int64 PRE_DISTRICT_ID { get; set; }
        [Required(ErrorMessage = "Please select present thana")]
        public Int64 PRE_UPOZILA_ID { get; set; }
        [Required(ErrorMessage = "Please input present postal code")]
        public string PRE_POSTAL_CODE { get; set; }

        
        public string PERM_ADDRESS_EN { get; set; }        
        public string PERM_ADDRESS_BN { get; set; }
        public Int64? PERM_DIVISION_ID { get; set; }
        public Int64? PERM_DISTRICT_ID { get; set; }
        public Int64? PERM_UPOZILA_ID { get; set; }
        public string PERM_POSTAL_CODE { get; set; }


        [Required(ErrorMessage = "Please select country")]
        public Int64? HR_COUNTRY_ID { get; set; }

        public string WORK_PHONE { get; set; }
        public Int64? WORK_PHONE_EXT { get; set; }
        public string HOME_PHONE { get; set; }
        public Int64? HOME_PHONE_EXT { get; set; }
        public string MOB_NO_OFF { get; set; }
        public string MOB_NO_PRS { get; set; }
        

        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$",ErrorMessage="Please input valid offical e-mail id")]
        public string EMAIL_ID_OFF { get; set; }
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$", ErrorMessage = "Please input valid personal e-mail id")]
        public string EMAIL_ID_PRS { get; set; }
        public string FATHER_NAME_EN { get; set; }
        public string FATHER_NAME_BN { get; set; }
        public string MOTHER_NAME_EN { get; set; }
        public string MOTHER_NAME_BN { get; set; }
        public string GUARDIAN_NAME_EN { get; set; }
        public string GUARDIAN_NAME_BN { get; set; }
        public Int64? LK_RELATION_ID { get; set; }
        public Int64? LK_RELIGION_ID { get; set; }
        public Int64? LK_GENDER_ID { get; set; }
        public Int64? LK_MRTL_STATUS_ID { get; set; }
        
        public Int64? LK_NTLTY_ID { get; set; }
        public Int64? LK_BLD_GRP_ID { get; set; }
        public string NATIONAL_ID { get; set; }
        public string PASSPORT_NO { get; set; }
        public DateTime? PASS_ISSUE_DT { get; set; }
        public DateTime? PASS_EXPIRE_DT { get; set; }
        public string PASS_ISS_PLACE { get; set; }
        public string DRIV_LIC_NO { get; set; }
        public DateTime? DRIV_LIC_EXPIRE_DT { get; set; }
        public string TIN_NO { get; set; }
        
        public byte[] EMP_PHOTO { get; set; }        
        public HttpPostedFileBase ATT_FILE { get; set; }
        public string EMP_PHOTO_PREVIEW { get; set; }

        public string EMP_SIGN { get; set; }
        public Int64? LK_CITIZENSHP_ID { get; set; }
        public string VISA_NO { get; set; }
        public DateTime? VISA_EXPIRE_DT { get; set; }
        public Int64? HR_PAY_POLICY_ID { get; set; }
        public Int64? HR_SHIFT_TEAM_ID { get; set; }
        public string IS_OT_HRLY { get; set; }
        public string IS_TRANSPORT { get; set; }
        public string IS_HOUSING { get; set; }
        public Int64? SC_USER_ID { get; set; }
        public Int32? NO_OF_CHILD { get; set; }

        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }


        public string EMP_PHOTO_PATH { get; set; }
        public string EMP_SIGN_PATH { get; set; }


        //[Required(ErrorMessage = "Please select shift plan")]
        public Int64? HR_SHIFT_PLAN_ID { get; set; }

        [Required(ErrorMessage="Please select department")]
        public Int64 PARENT_ID { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string CORE_DEPARTMENT_NAME { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string OFFICE_NAME_EN { get; set; }
        public string FLOOR_NAME_EN { get; set; }
        public string LAST_ADV_REF { get; set; }
        public Int64 DESIG_ORDER { get; set; }
        public string OT_UNIT_FLAG { get; set; }
        public List<HR_EMP_PAYModel> EMP_PAY_LIST { get; set; }
        public string IS_DEDUCT_SALARY4FS { get; set; }
        public long? EMPLOYEE_TYPE_ID { get; set; }
        public long? HR_PROD_FLR_ID { get; set; }
        public long? HR_PROD_LINE_ID { get; set; }
        public string SECTION_NAME { get; set; }
        public string EMP_NOM_DTL_XML { get; set; }
        public string NOM_IMG1 { get; set; }
        public string NOM_IMG2 { get; set; }
        public string NOM_IMG3 { get; set; }
        //public HttpPostedFileBase NOM_IMG_FILE { get; set; }

        




        public List<HR_EMPLOYEEModel> EmployeeAutoData(Int64? LK_GENDER_ID, string pEMPLOYEE_CODE)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_EMPLOYEEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3010},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = pEMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = LK_GENDER_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMPLOYEEModel ob = new HR_EMPLOYEEModel();
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.TA_PROXI_ID = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_PROXI_ID"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);

                    if (dr["LK_GENDER_ID"] != DBNull.Value)
                        ob.LK_GENDER_ID = (dr["LK_GENDER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GENDER_ID"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_EMPLOYEEModel> EmployeeAutoData(string pEMPLOYEE_CODE, string pLK_JOB_STATUS_ID, Int64? pHR_COMPANY_ID)
        {
            string sp = "pkg_hr.hr_employee_select";

            try
            {
                var obList = new List<HR_EMPLOYEEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = pEMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = pLK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMPLOYEEModel ob = new HR_EMPLOYEEModel();
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.TA_PROXI_ID = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_PROXI_ID"]);
                    
                    if (dr["CORE_DEPT_ID"] != DBNull.Value)
                        ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    
                    ob.CORE_DEPARTMENT_NAME = (dr["CORE_DEPARTMENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CORE_DEPARTMENT_NAME"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.LK_JOB_STATUS_ID = (dr["LK_JOB_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_STATUS_ID"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);

                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    //ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);                    
                    ob.SECTION_NAME = (dr["SECTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_NAME"]);
                    ob.EMPLOYEE_TYPE_ID = (dr["EMPLOYEE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EMPLOYEE_TYPE_ID"]);


                    if (dr["LK_FLOOR_ID"] != DBNull.Value)
                    {
                        ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                        ob.FLOOR_NAME_EN = (dr["FLOOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NAME"]);
                    }
                    if (dr["LINE_NO"] != DBNull.Value)
                        ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);

                    ob.IS_DEDUCT_SALARY4FS = (dr["IS_DEDUCT_SALARY4FS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEDUCT_SALARY4FS"]);

                    if (dr["JOINING_DT"] != DBNull.Value)
                    {
                        ob.JOINING_DT = Convert.ToDateTime(dr["JOINING_DT"]);
                    }
                    ob.GROSS_SALARY = (dr["GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_SALARY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Save()
        {
            string jsonStr = "{";
            var i = 1;
            string sp = "pkg_hr.hr_employee_insert";

            Int64 vEMP_ID = 0;
            var ob = this;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                if (ob.ATT_FILE != null)
                {
                    ob.ATT_FILE.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"\UPLOAD_DOCS\EMP_PHOTOS\" + ob.EMPLOYEE_CODE + ".jpg");
                }

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = ob.EMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pOLD_EMP_CODE", Value = ob.OLD_EMP_CODE},
                    new CommandParameter() {ParameterName = "pREPORTING_MGR_ID", Value = (ob.REPORTING_MGR_ID != null && ob.REPORTING_MGR_ID > 0)? ob.REPORTING_MGR_ID : null},
                    new CommandParameter() {ParameterName = "pTA_PROXI_ID", Value = ob.TA_PROXI_ID},
                    new CommandParameter() {ParameterName = "pCARD_NO", Value = ob.CARD_NO},
                    new CommandParameter() {ParameterName = "pEMP_TITLE", Value = ob.EMP_TITLE},
                    new CommandParameter() {ParameterName = "pEMP_FIRST_NAME_EN", Value = ob.EMP_FIRST_NAME_EN},
                    new CommandParameter() {ParameterName = "pEMP_FIRST_NAME_BN", Value = ob.EMP_FIRST_NAME_BN},
                    new CommandParameter() {ParameterName = "pEMP_LAST_NAME_EN", Value = ob.EMP_LAST_NAME_EN},
                    new CommandParameter() {ParameterName = "pEMP_LAST_NAME_BN", Value = ob.EMP_LAST_NAME_BN},
                    new CommandParameter() {ParameterName = "pEMP_FULL_NAME_EN", Value = ob.EMP_FULL_NAME_EN},
                    new CommandParameter() {ParameterName = "pEMP_FULL_NAME_BN", Value = ob.EMP_FULL_NAME_BN},
                    new CommandParameter() {ParameterName = "pEMP_NICKNM_EN", Value = ob.EMP_NICKNM_EN},
                    new CommandParameter() {ParameterName = "pEMP_NICKNM_BN", Value = ob.EMP_NICKNM_BN},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pSAL_COMPANY_ID", Value =(ob.SAL_COMPANY_ID != null && ob.SAL_COMPANY_ID > 0)?ob.SAL_COMPANY_ID:null},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = ob.HR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value =(ob.HR_GRADE_ID != null && ob.HR_GRADE_ID > 0)? ob.HR_GRADE_ID : null},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value =(ob.LK_FLOOR_ID != null && ob.LK_FLOOR_ID > 0)? ob.LK_FLOOR_ID : null},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = (ob.LINE_NO != null && ob.LINE_NO > 0) ? ob.LINE_NO : null},
                    new CommandParameter() {ParameterName = "pLK_APNT_STATUS_ID", Value =(ob.LK_APNT_STATUS_ID != null && ob.LK_APNT_STATUS_ID > 0) ? ob.LK_APNT_STATUS_ID : null},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = (ob.LK_JOB_STATUS_ID != null && ob.LK_JOB_STATUS_ID > 0) ? ob.LK_JOB_STATUS_ID : null},
                    new CommandParameter() {ParameterName = "pLK_EMP_TYPE_ID", Value = (ob.LK_EMP_TYPE_ID != null && ob.LK_EMP_TYPE_ID > 0) ? ob.LK_EMP_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pLK_MNG_LVL_ID", Value =(ob.LK_MNG_LVL_ID != null && ob.LK_MNG_LVL_ID > 0) ? ob.LK_MNG_LVL_ID : null},
                    new CommandParameter() {ParameterName = "pLK_JOB_CATG_ID", Value = (ob.LK_JOB_CATG_ID != null && ob.LK_JOB_CATG_ID > 0) ? ob.LK_JOB_CATG_ID : null},
                    new CommandParameter() {ParameterName = "pCONFIRM_DT", Value = ob.CONFIRM_DT},
                    new CommandParameter() {ParameterName = "pJOINING_DT", Value = ob.JOINING_DT},
                    new CommandParameter() {ParameterName = "pAPNT_ISSUE_DT", Value = ob.APNT_ISSUE_DT},
                    new CommandParameter() {ParameterName = "pPROBATION_PERIOD", Value = (ob.PROBATION_PERIOD != null && ob.PROBATION_PERIOD > 0) ? ob.PROBATION_PERIOD : null},
                    new CommandParameter() {ParameterName = "pPR_PERIOD_TYPE_ID", Value = (ob.PR_PERIOD_TYPE_ID != null && ob.PR_PERIOD_TYPE_ID > 0) ? ob.PR_PERIOD_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pPROBATION_DT", Value = ob.PROBATION_DT},
                    new CommandParameter() {ParameterName = "pOFFER_DT", Value = ob.OFFER_DT},
                    new CommandParameter() {ParameterName = "pREPORTING_DT", Value = ob.REPORTING_DT},
                    new CommandParameter() {ParameterName = "pCONTRACT_PERIOD", Value =(ob.CONTRACT_PERIOD != null && ob.CONTRACT_PERIOD > 0) ? ob.CONTRACT_PERIOD : null},
                    new CommandParameter() {ParameterName = "pCT_PERIOD_TYPE_ID", Value = (ob.CT_PERIOD_TYPE_ID != null && ob.CT_PERIOD_TYPE_ID > 0) ? ob.CT_PERIOD_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pDUTY_HOUR", Value = (ob.DUTY_HOUR != null && ob.DUTY_HOUR > 0) ? ob.DUTY_HOUR : null},
                    new CommandParameter() {ParameterName = "pALLOWED_LEAVE_DAY", Value = (ob.ALLOWED_LEAVE_DAY != null && ob.ALLOWED_LEAVE_DAY > 0) ? ob.ALLOWED_LEAVE_DAY : null},
                    new CommandParameter() {ParameterName = "pGROSS_SALARY", Value = (ob.GROSS_SALARY != null && ob.GROSS_SALARY > 0) ? ob.GROSS_SALARY : null},
                    new CommandParameter() {ParameterName = "pHR_EMP_BANK_ACC_ID", Value = (ob.ACC_BK_ACCOUNT_ID != null && ob.ACC_BK_ACCOUNT_ID > 0) ? ob.ACC_BK_ACCOUNT_ID : null},
                    new CommandParameter() {ParameterName = "pPRE_ADDRESS_EN", Value = ob.PRE_ADDRESS_EN},
                    new CommandParameter() {ParameterName = "pPRE_ADDRESS_BN", Value = ob.PRE_ADDRESS_BN},
                    new CommandParameter() {ParameterName = "pPRE_DISTRICT_ID", Value = ob.PRE_DISTRICT_ID},
                    new CommandParameter() {ParameterName = "pPRE_UPOZILA_ID", Value = ob.PRE_UPOZILA_ID},
                    new CommandParameter() {ParameterName = "pPRE_POSTAL_CODE", Value = ob.PRE_POSTAL_CODE},
                    new CommandParameter() {ParameterName = "pPERM_ADDRESS_EN", Value = ob.PERM_ADDRESS_EN},
                    new CommandParameter() {ParameterName = "pPERM_ADDRESS_BN", Value = ob.PERM_ADDRESS_BN},
                    new CommandParameter() {ParameterName = "pPERM_DISTRICT_ID", Value = (ob.PERM_DISTRICT_ID != null && ob.PERM_DISTRICT_ID > 0) ? ob.PERM_DISTRICT_ID : null},
                    new CommandParameter() {ParameterName = "pPERM_UPOZILA_ID", Value = ob.PERM_UPOZILA_ID},
                    new CommandParameter() {ParameterName = "pPERM_POSTAL_CODE", Value = ob.PERM_POSTAL_CODE},
                    new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = (ob.HR_COUNTRY_ID != null && ob.HR_COUNTRY_ID > 0) ? ob.HR_COUNTRY_ID : null},
                    new CommandParameter() {ParameterName = "pMOB_NO_OFF", Value = ob.MOB_NO_OFF},
                    new CommandParameter() {ParameterName = "pMOB_NO_PRS", Value = ob.MOB_NO_PRS},
                    new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                    new CommandParameter() {ParameterName = "pWORK_PHONE_EXT", Value = (ob.WORK_PHONE_EXT != null && ob.WORK_PHONE_EXT > 0) ? ob.WORK_PHONE_EXT : null},
                    new CommandParameter() {ParameterName = "pHOME_PHONE", Value = ob.HOME_PHONE},
                    new CommandParameter() {ParameterName = "pHOME_PHONE_EXT", Value = (ob.HOME_PHONE_EXT != null && ob.HOME_PHONE_EXT > 0) ? ob.HOME_PHONE_EXT : null},
                    new CommandParameter() {ParameterName = "pEMAIL_ID_OFF", Value = ob.EMAIL_ID_OFF},
                    new CommandParameter() {ParameterName = "pEMAIL_ID_PRS", Value = ob.EMAIL_ID_PRS},
                    new CommandParameter() {ParameterName = "pFATHER_NAME_EN", Value = ob.FATHER_NAME_EN},
                    new CommandParameter() {ParameterName = "pFATHER_NAME_BN", Value = ob.FATHER_NAME_BN},
                    new CommandParameter() {ParameterName = "pMOTHER_NAME_EN", Value = ob.MOTHER_NAME_EN},
                    new CommandParameter() {ParameterName = "pMOTHER_NAME_BN", Value = ob.MOTHER_NAME_BN},
                    new CommandParameter() {ParameterName = "pGUARDIAN_NAME_EN", Value = ob.GUARDIAN_NAME_EN},
                    new CommandParameter() {ParameterName = "pGUARDIAN_NAME_BN", Value = ob.GUARDIAN_NAME_BN},
                    new CommandParameter() {ParameterName = "pLK_RELATION_ID", Value = (ob.LK_RELATION_ID != null && ob.LK_RELATION_ID > 0) ? ob.LK_RELATION_ID : null},
                    new CommandParameter() {ParameterName = "pLK_RELIGION_ID", Value = (ob.LK_RELIGION_ID != null && ob.LK_RELIGION_ID > 0) ? ob.LK_RELIGION_ID : null},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = (ob.LK_GENDER_ID != null && ob.LK_GENDER_ID > 0) ? ob.LK_GENDER_ID : null},
                    new CommandParameter() {ParameterName = "pLK_MRTL_STATUS_ID", Value = (ob.LK_MRTL_STATUS_ID != null && ob.LK_MRTL_STATUS_ID > 0) ? ob.LK_MRTL_STATUS_ID : null},
                    new CommandParameter() {ParameterName = "pBIRTH_DT", Value = ob.BIRTH_DT},
                    new CommandParameter() {ParameterName = "pLK_NTLTY_ID", Value = (ob.LK_NTLTY_ID != null && ob.LK_NTLTY_ID > 0) ? ob.LK_NTLTY_ID : null},
                    new CommandParameter() {ParameterName = "pLK_BLD_GRP_ID", Value = (ob.LK_BLD_GRP_ID != null && ob.LK_BLD_GRP_ID > 0) ? ob.LK_BLD_GRP_ID : null},
                    new CommandParameter() {ParameterName = "pNATIONAL_ID", Value = ob.NATIONAL_ID},
                    new CommandParameter() {ParameterName = "pPASSPORT_NO", Value = ob.PASSPORT_NO},
                    new CommandParameter() {ParameterName = "pPASS_ISSUE_DT", Value = ob.PASS_ISSUE_DT},
                    new CommandParameter() {ParameterName = "pPASS_EXPIRE_DT", Value = ob.PASS_EXPIRE_DT},
                    new CommandParameter() {ParameterName = "pPASS_ISS_PLACE", Value = ob.PASS_ISS_PLACE},
                    new CommandParameter() {ParameterName = "pDRIV_LIC_NO", Value = ob.DRIV_LIC_NO},
                    new CommandParameter() {ParameterName = "pDRIV_LIC_EXPIRE_DT", Value = ob.DRIV_LIC_EXPIRE_DT},
                    new CommandParameter() {ParameterName = "pTIN_NO", Value = ob.TIN_NO},
                    new CommandParameter() {ParameterName = "pLK_CITIZENSHP_ID", Value = (ob.LK_CITIZENSHP_ID != null && ob.LK_CITIZENSHP_ID > 0) ? ob.LK_CITIZENSHP_ID : null},
                    new CommandParameter() {ParameterName = "pVISA_NO", Value = ob.VISA_NO},
                    new CommandParameter() {ParameterName = "pVISA_EXPIRE_DT", Value = ob.VISA_EXPIRE_DT},
                    new CommandParameter() {ParameterName = "pHR_PAY_POLICY_ID", Value = (ob.HR_PAY_POLICY_ID != null && ob.HR_PAY_POLICY_ID > 0) ? ob.HR_PAY_POLICY_ID : null},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = (ob.HR_SHIFT_TEAM_ID != null && ob.HR_SHIFT_TEAM_ID > 0) ? ob.HR_SHIFT_TEAM_ID : null},
                    new CommandParameter() {ParameterName = "pIS_OT_HRLY", Value = ob.IS_OT_HRLY},
                    new CommandParameter() {ParameterName = "pIS_TRANSPORT", Value = ob.IS_TRANSPORT},
                    new CommandParameter() {ParameterName = "pIS_HOUSING", Value = ob.IS_HOUSING},
                    new CommandParameter() {ParameterName = "pNO_OF_CHILD", Value = ob.NO_OF_CHILD},

                    new CommandParameter() {ParameterName = "pEMP_NOM_DTL_XML", Value = ob.EMP_NOM_DTL_XML},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = 1},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pEMP_ID", Value = 16, Direction = ParameterDirection.Output}
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
                    /////////////////////////////////////////////
                    if (dr["KEY"].ToString() == "PMSG")
                    {
                        vMsg = dr["VALUE"].ToString();
                    }
                    if (dr["KEY"].ToString() == "PEMP_ID")
                    {
                        if (dr["VALUE"] != null)
                        { vEMP_ID = dr["VALUE"] == DBNull.Value ? 0 : Convert.ToInt64(dr["VALUE"]); }
                    }
                    ////////////////////////////////////////////
                }
                if (vMsg.Substring(0, 9) == "MULTI-001")
                {
                    sp = "pkg_hr.hr_emp_pay_insert";
                    if (ob.EMP_PAY_LIST != null)
                    {
                        foreach (var item in ob.EMP_PAY_LIST)
                        {
                            var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = item.HR_PAY_ELEMENT_ID},
                                new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = vEMP_ID},
                                new CommandParameter() {ParameterName = "pPAY_AMT", Value = item.PAY_AMT},
                                new CommandParameter() {ParameterName = "pIS_EFFECT_SALARY", Value = "Y"},
                                new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = "Y"},
                                new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                                new CommandParameter() {ParameterName = "pOption", Value = 1000},
                                new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                            }, sp);

                            foreach (DataRow dr1 in ds1.Tables["OUTPARAM"].Rows)
                            {
                                vMsg = dr1["VALUE"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //vMsg = "MULTI-005" + ex.Message;
                jsonStr += Convert.ToString('"') + "ERR" + Convert.ToString('"') + ":" + Convert.ToString('"') + ("MULTI-005" + ex.Message.ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
            }
            return jsonStr;
        }

        public string Update()
        {
            string sp = "pkg_hr.hr_employee_update";
            var ob = this;
            string vMsg = "";


            OraDatabase db = new OraDatabase();
            try
            {
                    if (ob.ATT_FILE != null)
                    {
                        ob.ATT_FILE.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"\UPLOAD_DOCS\EMP_PHOTOS\" + ob.EMPLOYEE_CODE + ".jpg");
                    }


                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                                        new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = ob.EMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pOLD_EMP_CODE", Value = ob.OLD_EMP_CODE},
                    new CommandParameter() {ParameterName = "pREPORTING_MGR_ID", Value = (ob.REPORTING_MGR_ID != null && ob.REPORTING_MGR_ID > 0)? ob.REPORTING_MGR_ID : null},
                    new CommandParameter() {ParameterName = "pTA_PROXI_ID", Value = ob.TA_PROXI_ID},
                    new CommandParameter() {ParameterName = "pCARD_NO", Value = ob.CARD_NO},
                    new CommandParameter() {ParameterName = "pEMP_TITLE", Value = ob.EMP_TITLE},
                    new CommandParameter() {ParameterName = "pEMP_FIRST_NAME_EN", Value = ob.EMP_FIRST_NAME_EN},
                    new CommandParameter() {ParameterName = "pEMP_FIRST_NAME_BN", Value = ob.EMP_FIRST_NAME_BN},
                    new CommandParameter() {ParameterName = "pEMP_LAST_NAME_EN", Value = ob.EMP_LAST_NAME_EN},
                    new CommandParameter() {ParameterName = "pEMP_LAST_NAME_BN", Value = ob.EMP_LAST_NAME_BN},
                    new CommandParameter() {ParameterName = "pEMP_FULL_NAME_EN", Value = ob.EMP_FULL_NAME_EN},
                    new CommandParameter() {ParameterName = "pEMP_FULL_NAME_BN", Value = ob.EMP_FULL_NAME_BN},
                    new CommandParameter() {ParameterName = "pEMP_NICKNM_EN", Value = ob.EMP_NICKNM_EN},
                    new CommandParameter() {ParameterName = "pEMP_NICKNM_BN", Value = ob.EMP_NICKNM_BN},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pSAL_COMPANY_ID", Value =(ob.SAL_COMPANY_ID != null && ob.SAL_COMPANY_ID > 0)?ob.SAL_COMPANY_ID:null},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = ob.HR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value =(ob.HR_GRADE_ID != null && ob.HR_GRADE_ID > 0)? ob.HR_GRADE_ID : null},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value =(ob.LK_FLOOR_ID != null && ob.LK_FLOOR_ID > 0)? ob.LK_FLOOR_ID : null},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = (ob.LINE_NO != null && ob.LINE_NO > 0) ? ob.LINE_NO : null},
                    new CommandParameter() {ParameterName = "pLK_APNT_STATUS_ID", Value =(ob.LK_APNT_STATUS_ID != null && ob.LK_APNT_STATUS_ID > 0) ? ob.LK_APNT_STATUS_ID : null},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = (ob.LK_JOB_STATUS_ID != null && ob.LK_JOB_STATUS_ID > 0) ? ob.LK_JOB_STATUS_ID : null},
                    new CommandParameter() {ParameterName = "pLK_EMP_TYPE_ID", Value = (ob.LK_EMP_TYPE_ID != null && ob.LK_EMP_TYPE_ID > 0) ? ob.LK_EMP_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pLK_MNG_LVL_ID", Value =(ob.LK_MNG_LVL_ID != null && ob.LK_MNG_LVL_ID > 0) ? ob.LK_MNG_LVL_ID : null},
                    new CommandParameter() {ParameterName = "pLK_JOB_CATG_ID", Value = (ob.LK_JOB_CATG_ID != null && ob.LK_JOB_CATG_ID > 0) ? ob.LK_JOB_CATG_ID : null},
                    new CommandParameter() {ParameterName = "pCONFIRM_DT", Value = ob.CONFIRM_DT},
                    new CommandParameter() {ParameterName = "pJOINING_DT", Value = ob.JOINING_DT},
                    new CommandParameter() {ParameterName = "pAPNT_ISSUE_DT", Value = ob.APNT_ISSUE_DT},
                    new CommandParameter() {ParameterName = "pPROBATION_PERIOD", Value = (ob.PROBATION_PERIOD != null && ob.PROBATION_PERIOD > 0) ? ob.PROBATION_PERIOD : null},
                    new CommandParameter() {ParameterName = "pPR_PERIOD_TYPE_ID", Value = (ob.PR_PERIOD_TYPE_ID != null && ob.PR_PERIOD_TYPE_ID > 0) ? ob.PR_PERIOD_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pPROBATION_DT", Value = ob.PROBATION_DT},
                    new CommandParameter() {ParameterName = "pOFFER_DT", Value = ob.OFFER_DT},
                    new CommandParameter() {ParameterName = "pREPORTING_DT", Value = ob.REPORTING_DT},
                    new CommandParameter() {ParameterName = "pCONTRACT_PERIOD", Value =(ob.CONTRACT_PERIOD != null && ob.CONTRACT_PERIOD > 0) ? ob.CONTRACT_PERIOD : null},
                    new CommandParameter() {ParameterName = "pCT_PERIOD_TYPE_ID", Value = (ob.CT_PERIOD_TYPE_ID != null && ob.CT_PERIOD_TYPE_ID > 0) ? ob.CT_PERIOD_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pDUTY_HOUR", Value = (ob.DUTY_HOUR != null && ob.DUTY_HOUR > 0) ? ob.DUTY_HOUR : null},
                    new CommandParameter() {ParameterName = "pALLOWED_LEAVE_DAY", Value = (ob.ALLOWED_LEAVE_DAY != null && ob.ALLOWED_LEAVE_DAY > 0) ? ob.ALLOWED_LEAVE_DAY : null},
                    new CommandParameter() {ParameterName = "pGROSS_SALARY", Value = (ob.GROSS_SALARY != null && ob.GROSS_SALARY > 0) ? ob.GROSS_SALARY : null},
                    new CommandParameter() {ParameterName = "pHR_EMP_BANK_ACC_ID", Value = (ob.ACC_BK_ACCOUNT_ID != null && ob.ACC_BK_ACCOUNT_ID > 0) ? ob.ACC_BK_ACCOUNT_ID : null},
                    new CommandParameter() {ParameterName = "pPRE_ADDRESS_EN", Value = ob.PRE_ADDRESS_EN},
                    new CommandParameter() {ParameterName = "pPRE_ADDRESS_BN", Value = ob.PRE_ADDRESS_BN},
                    new CommandParameter() {ParameterName = "pPRE_DISTRICT_ID", Value = ob.PRE_DISTRICT_ID},
                    new CommandParameter() {ParameterName = "pPRE_UPOZILA_ID", Value = ob.PRE_UPOZILA_ID},
                    new CommandParameter() {ParameterName = "pPRE_POSTAL_CODE", Value = ob.PRE_POSTAL_CODE},
                    new CommandParameter() {ParameterName = "pPERM_ADDRESS_EN", Value = ob.PERM_ADDRESS_EN},
                    new CommandParameter() {ParameterName = "pPERM_ADDRESS_BN", Value = ob.PERM_ADDRESS_BN},
                    new CommandParameter() {ParameterName = "pPERM_DISTRICT_ID", Value = (ob.PERM_DISTRICT_ID != null && ob.PERM_DISTRICT_ID > 0) ? ob.PERM_DISTRICT_ID : null},
                    new CommandParameter() {ParameterName = "pPERM_UPOZILA_ID", Value = ob.PERM_UPOZILA_ID},
                    new CommandParameter() {ParameterName = "pPERM_POSTAL_CODE", Value = ob.PERM_POSTAL_CODE},
                    new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = (ob.HR_COUNTRY_ID != null && ob.HR_COUNTRY_ID > 0) ? ob.HR_COUNTRY_ID : null},
                    new CommandParameter() {ParameterName = "pMOB_NO_OFF", Value = ob.MOB_NO_OFF},
                    new CommandParameter() {ParameterName = "pMOB_NO_PRS", Value = ob.MOB_NO_PRS},
                    new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                    new CommandParameter() {ParameterName = "pWORK_PHONE_EXT", Value = (ob.WORK_PHONE_EXT != null && ob.WORK_PHONE_EXT > 0) ? ob.WORK_PHONE_EXT : null},
                    new CommandParameter() {ParameterName = "pHOME_PHONE", Value = ob.HOME_PHONE},
                    new CommandParameter() {ParameterName = "pHOME_PHONE_EXT", Value = (ob.HOME_PHONE_EXT != null && ob.HOME_PHONE_EXT > 0) ? ob.HOME_PHONE_EXT : null},
                    new CommandParameter() {ParameterName = "pEMAIL_ID_OFF", Value = ob.EMAIL_ID_OFF},
                    new CommandParameter() {ParameterName = "pEMAIL_ID_PRS", Value = ob.EMAIL_ID_PRS},
                    new CommandParameter() {ParameterName = "pFATHER_NAME_EN", Value = ob.FATHER_NAME_EN},
                    new CommandParameter() {ParameterName = "pFATHER_NAME_BN", Value = ob.FATHER_NAME_BN},
                    new CommandParameter() {ParameterName = "pMOTHER_NAME_EN", Value = ob.MOTHER_NAME_EN},
                    new CommandParameter() {ParameterName = "pMOTHER_NAME_BN", Value = ob.MOTHER_NAME_BN},
                    new CommandParameter() {ParameterName = "pGUARDIAN_NAME_EN", Value = ob.GUARDIAN_NAME_EN},
                    new CommandParameter() {ParameterName = "pGUARDIAN_NAME_BN", Value = ob.GUARDIAN_NAME_BN},
                    new CommandParameter() {ParameterName = "pLK_RELATION_ID", Value = (ob.LK_RELATION_ID != null && ob.LK_RELATION_ID > 0) ? ob.LK_RELATION_ID : null},
                    new CommandParameter() {ParameterName = "pLK_RELIGION_ID", Value = (ob.LK_RELIGION_ID != null && ob.LK_RELIGION_ID > 0) ? ob.LK_RELIGION_ID : null},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = (ob.LK_GENDER_ID != null && ob.LK_GENDER_ID > 0) ? ob.LK_GENDER_ID : null},
                    new CommandParameter() {ParameterName = "pLK_MRTL_STATUS_ID", Value = (ob.LK_MRTL_STATUS_ID != null && ob.LK_MRTL_STATUS_ID > 0) ? ob.LK_MRTL_STATUS_ID : null},
                    new CommandParameter() {ParameterName = "pBIRTH_DT", Value = ob.BIRTH_DT},
                    new CommandParameter() {ParameterName = "pLK_NTLTY_ID", Value = (ob.LK_NTLTY_ID != null && ob.LK_NTLTY_ID > 0) ? ob.LK_NTLTY_ID : null},
                    new CommandParameter() {ParameterName = "pLK_BLD_GRP_ID", Value = (ob.LK_BLD_GRP_ID != null && ob.LK_BLD_GRP_ID > 0) ? ob.LK_BLD_GRP_ID : null},
                    new CommandParameter() {ParameterName = "pNATIONAL_ID", Value = ob.NATIONAL_ID},
                    new CommandParameter() {ParameterName = "pPASSPORT_NO", Value = ob.PASSPORT_NO},
                    new CommandParameter() {ParameterName = "pPASS_ISSUE_DT", Value = ob.PASS_ISSUE_DT},
                    new CommandParameter() {ParameterName = "pPASS_EXPIRE_DT", Value = ob.PASS_EXPIRE_DT},
                    new CommandParameter() {ParameterName = "pPASS_ISS_PLACE", Value = ob.PASS_ISS_PLACE},
                    new CommandParameter() {ParameterName = "pDRIV_LIC_NO", Value = ob.DRIV_LIC_NO},
                    new CommandParameter() {ParameterName = "pDRIV_LIC_EXPIRE_DT", Value = ob.DRIV_LIC_EXPIRE_DT},
                    new CommandParameter() {ParameterName = "pTIN_NO", Value = ob.TIN_NO},
                    new CommandParameter() {ParameterName = "pLK_CITIZENSHP_ID", Value = (ob.LK_CITIZENSHP_ID != null && ob.LK_CITIZENSHP_ID > 0) ? ob.LK_CITIZENSHP_ID : null},
                    new CommandParameter() {ParameterName = "pVISA_NO", Value = ob.VISA_NO},
                    new CommandParameter() {ParameterName = "pVISA_EXPIRE_DT", Value = ob.VISA_EXPIRE_DT},
                    new CommandParameter() {ParameterName = "pHR_PAY_POLICY_ID", Value = (ob.HR_PAY_POLICY_ID != null && ob.HR_PAY_POLICY_ID > 0) ? ob.HR_PAY_POLICY_ID : null},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = (ob.HR_SHIFT_TEAM_ID != null && ob.HR_SHIFT_TEAM_ID > 0) ? ob.HR_SHIFT_TEAM_ID : null},
                    new CommandParameter() {ParameterName = "pIS_OT_HRLY", Value = ob.IS_OT_HRLY},
                    new CommandParameter() {ParameterName = "pIS_TRANSPORT", Value = ob.IS_TRANSPORT},
                    new CommandParameter() {ParameterName = "pIS_HOUSING", Value = ob.IS_HOUSING},
                    new CommandParameter() {ParameterName = "pNO_OF_CHILD", Value = ob.NO_OF_CHILD},
                    
                    new CommandParameter() {ParameterName = "pEMP_NOM_DTL_XML", Value = ob.EMP_NOM_DTL_XML},
                    new CommandParameter() {ParameterName = "pNOM_IMG1", Value = ob.NOM_IMG1},
                    new CommandParameter() {ParameterName = "pNOM_IMG2", Value = ob.NOM_IMG2},
                    new CommandParameter() {ParameterName = "pNOM_IMG3", Value = ob.NOM_IMG3},

                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATE_LOGIN", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = 1},
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }


                string isInsertEmpPay;

                if (ob.EMP_PAY_LIST != null && vMsg.Substring(0, 9) == "MULTI-001")
                {
                    foreach (var item in ob.EMP_PAY_LIST)
                    {
                        if (item.HR_EMP_PAY_ID > 0)
                        {
                            isInsertEmpPay = "N";
                            sp = "pkg_hr.hr_emp_pay_update";
                        }
                        else
                        {
                            isInsertEmpPay = "Y";
                            sp = "pkg_hr.hr_emp_pay_insert";
                        }


                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                    new CommandParameter() {ParameterName = "pHR_EMP_PAY_ID", Value = item.HR_EMP_PAY_ID},
                                    new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = item.HR_PAY_ELEMENT_ID},
                                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                                    new CommandParameter() {ParameterName = "pPAY_AMT", Value = item.PAY_AMT},
                                    new CommandParameter() {ParameterName = "pIS_EFFECT_SALARY", Value = item.IS_EFFECT_SALARY},
                                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = item.IS_ACTIVE},
                                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                                    new CommandParameter() {ParameterName = "pOption", Value = (isInsertEmpPay == "Y")? 1000 : 2000},
                                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                                }, sp);

                                foreach (DataRow dr1 in ds1.Tables["OUTPARAM"].Rows)
                                {
                                    vMsg = dr1["VALUE"].ToString();
                                }
                    }
                }



            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }

        public List<HR_EMPLOYEEModel> SelectAll()
        {
            string sp = "pkg_hr.hr_employee_select";
            try
            {
                var obList = new List<HR_EMPLOYEEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMPLOYEEModel ob = new HR_EMPLOYEEModel();
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.OLD_EMP_CODE = (dr["OLD_EMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OLD_EMP_CODE"]);
                    ob.REPORTING_MGR_ID = (dr["REPORTING_MGR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REPORTING_MGR_ID"]);
                    ob.TA_PROXI_ID = (dr["TA_PROXI_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_PROXI_ID"]);
                    ob.CARD_NO = (dr["CARD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARD_NO"]);
                    ob.EMP_TITLE = (dr["EMP_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_TITLE"]);
                    ob.EMP_FIRST_NAME_EN = (dr["EMP_FIRST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FIRST_NAME_EN"]);
                    ob.EMP_FIRST_NAME_BN = (dr["EMP_FIRST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FIRST_NAME_BN"]);
                    ob.EMP_LAST_NAME_EN = (dr["EMP_LAST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_LAST_NAME_EN"]);
                    ob.EMP_LAST_NAME_BN = (dr["EMP_LAST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_LAST_NAME_BN"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.EMP_FULL_NAME_BN = (dr["EMP_FULL_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_BN"]);
                    ob.EMP_NICKNM_EN = (dr["EMP_NICKNM_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_NICKNM_EN"]);
                    ob.EMP_NICKNM_BN = (dr["EMP_NICKNM_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_NICKNM_BN"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.SAL_COMPANY_ID = (dr["SAL_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SAL_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    if (ob.LK_FLOOR_ID != null)
                    { ob.LK_FLOOR_ID = Convert.ToInt64(dr["LK_FLOOR_ID"]); }
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.LK_APNT_STATUS_ID = (dr["LK_APNT_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_APNT_STATUS_ID"]);
                    ob.LK_JOB_STATUS_ID = (dr["LK_JOB_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_STATUS_ID"]);
                    ob.LK_EMP_TYPE_ID = (dr["LK_EMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_EMP_TYPE_ID"]);
                    ob.LK_MNG_LVL_ID = (dr["LK_MNG_LVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MNG_LVL_ID"]);
                    ob.LK_JOB_CATG_ID = (dr["LK_JOB_CATG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_CATG_ID"]);
                    ob.CONFIRM_DT = (dr["CONFIRM_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CONFIRM_DT"]);
                    ob.JOINING_DT = (dr["JOINING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["JOINING_DT"]);
                    ob.APNT_ISSUE_DT = (dr["APNT_ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APNT_ISSUE_DT"]);
                    ob.PROBATION_PERIOD = (dr["PROBATION_PERIOD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROBATION_PERIOD"]);
                    ob.PR_PERIOD_TYPE_ID = (dr["PR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PR_PERIOD_TYPE_ID"]);
                    ob.PROBATION_DT = (dr["PROBATION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROBATION_DT"]);
                    ob.OFFER_DT = (dr["OFFER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OFFER_DT"]);
                    ob.REPORTING_DT = (dr["REPORTING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORTING_DT"]);
                    ob.CONTRACT_PERIOD = (dr["CONTRACT_PERIOD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONTRACT_PERIOD"]);
                    ob.CT_PERIOD_TYPE_ID = (dr["CT_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CT_PERIOD_TYPE_ID"]);
                    ob.DUTY_HOUR = (dr["DUTY_HOUR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DUTY_HOUR"]);
                    ob.ALLOWED_LEAVE_DAY = (dr["ALLOWED_LEAVE_DAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOWED_LEAVE_DAY"]);
                    ob.GROSS_SALARY = (dr["GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_SALARY"]);
                    ob.ACC_BK_ACCOUNT_ID = (dr["ACC_BK_ACCOUNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_BK_ACCOUNT_ID"]);
                    ob.PRE_ADDRESS_EN = (dr["PRE_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_ADDRESS_EN"]);
                    ob.PRE_ADDRESS_BN = (dr["PRE_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_ADDRESS_BN"]);
                    ob.PRE_DISTRICT_ID = (dr["PRE_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_DISTRICT_ID"]);
                    ob.PRE_POSTAL_CODE = (dr["PRE_POSTAL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_POSTAL_CODE"]);
                    ob.PERM_ADDRESS_EN = (dr["PERM_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_ADDRESS_EN"]);
                    ob.PERM_ADDRESS_BN = (dr["PERM_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_ADDRESS_BN"]);
                    ob.PERM_DISTRICT_ID = (dr["PERM_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERM_DISTRICT_ID"]);
                    ob.PERM_POSTAL_CODE = (dr["PERM_POSTAL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_POSTAL_CODE"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.MOB_NO_OFF = (dr["MOB_NO_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_NO_OFF"]);
                    ob.MOB_NO_PRS = (dr["MOB_NO_PRS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_NO_PRS"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.WORK_PHONE_EXT = (dr["WORK_PHONE_EXT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WORK_PHONE_EXT"]);
                    ob.HOME_PHONE = (dr["HOME_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOME_PHONE"]);
                    ob.HOME_PHONE_EXT = (dr["HOME_PHONE_EXT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOME_PHONE_EXT"]);
                    ob.EMAIL_ID_OFF = (dr["EMAIL_ID_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID_OFF"]);
                    ob.EMAIL_ID_PRS = (dr["EMAIL_ID_PRS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID_PRS"]);
                    ob.FATHER_NAME_EN = (dr["FATHER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FATHER_NAME_EN"]);
                    ob.FATHER_NAME_BN = (dr["FATHER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FATHER_NAME_BN"]);
                    ob.MOTHER_NAME_EN = (dr["MOTHER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOTHER_NAME_EN"]);
                    ob.MOTHER_NAME_BN = (dr["MOTHER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOTHER_NAME_BN"]);
                    ob.GUARDIAN_NAME_EN = (dr["GUARDIAN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GUARDIAN_NAME_EN"]);
                    ob.GUARDIAN_NAME_BN = (dr["GUARDIAN_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GUARDIAN_NAME_BN"]);
                    ob.LK_RELATION_ID = (dr["LK_RELATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RELATION_ID"]);
                    ob.LK_RELIGION_ID = (dr["LK_RELIGION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RELIGION_ID"]);
                    ob.LK_GENDER_ID = (dr["LK_GENDER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GENDER_ID"]);
                    ob.LK_MRTL_STATUS_ID = (dr["LK_MRTL_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MRTL_STATUS_ID"]);
                    ob.BIRTH_DT = (dr["BIRTH_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BIRTH_DT"]);
                    ob.LK_NTLTY_ID = (dr["LK_NTLTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NTLTY_ID"]);
                    ob.LK_BLD_GRP_ID = (dr["LK_BLD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BLD_GRP_ID"]);
                    ob.NATIONAL_ID = (dr["NATIONAL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NATIONAL_ID"]);
                    ob.PASSPORT_NO = (dr["PASSPORT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASSPORT_NO"]);
                    ob.PASS_ISSUE_DT = (dr["PASS_ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PASS_ISSUE_DT"]);
                    ob.PASS_EXPIRE_DT = (dr["PASS_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PASS_EXPIRE_DT"]);
                    ob.PASS_ISS_PLACE = (dr["PASS_ISS_PLACE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASS_ISS_PLACE"]);
                    ob.DRIV_LIC_NO = (dr["DRIV_LIC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRIV_LIC_NO"]);
                    ob.DRIV_LIC_EXPIRE_DT = (dr["DRIV_LIC_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DRIV_LIC_EXPIRE_DT"]);
                    ob.TIN_NO = (dr["TIN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIN_NO"]);
                    ob.EMP_SIGN = (dr["EMP_SIGN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_SIGN"]);
                    ob.LK_CITIZENSHP_ID = (dr["LK_CITIZENSHP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CITIZENSHP_ID"]);
                    ob.VISA_NO = (dr["VISA_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VISA_NO"]);
                    ob.VISA_EXPIRE_DT = (dr["VISA_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["VISA_EXPIRE_DT"]);
                    ob.HR_PAY_POLICY_ID = (dr["HR_PAY_POLICY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_POLICY_ID"]);
                    ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.IS_OT_HRLY = (dr["IS_OT_HRLY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OT_HRLY"]);
                    ob.IS_TRANSPORT = (dr["IS_TRANSPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRANSPORT"]);
                    ob.IS_HOUSING = (dr["IS_HOUSING"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_HOUSING"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                    ob.OT_UNIT_FLAG = (dr["OT_UNIT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_UNIT_FLAG"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_EMPLOYEEModel GetEmpDataByID(long pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_hr.hr_employee_select";
            try
            {
                var ob = this;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);

                    //ob.OLD_EMP_CODE = (dr["OLD_EMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OLD_EMP_CODE"]);
                    //ob.REPORTING_MGR_ID = (dr["REPORTING_MGR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REPORTING_MGR_ID"]);
                    //ob.TA_PROXI_ID = (dr["TA_PROXI_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_PROXI_ID"]);
                    //ob.CARD_NO = (dr["CARD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARD_NO"]);
                    //ob.EMP_TITLE = (dr["EMP_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_TITLE"]);
                    //ob.EMP_FIRST_NAME_EN = (dr["EMP_FIRST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FIRST_NAME_EN"]);
                    //ob.EMP_FIRST_NAME_BN = (dr["EMP_FIRST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FIRST_NAME_BN"]);
                    //ob.EMP_LAST_NAME_EN = (dr["EMP_LAST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_LAST_NAME_EN"]);
                    //ob.EMP_LAST_NAME_BN = (dr["EMP_LAST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_LAST_NAME_BN"]);
                    //ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    //ob.EMP_FULL_NAME_BN = (dr["EMP_FULL_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_BN"]);
                    //ob.EMP_NICKNM_EN = (dr["EMP_NICKNM_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_NICKNM_EN"]);
                    //ob.EMP_NICKNM_BN = (dr["EMP_NICKNM_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_NICKNM_BN"]);
                    //ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    //ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    //ob.SAL_COMPANY_ID = (dr["SAL_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SAL_COMPANY_ID"]);
                    //ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    //ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    //ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    //if (ob.LK_FLOOR_ID != null)
                    //{ ob.LK_FLOOR_ID = Convert.ToInt64(dr["LK_FLOOR_ID"]); }
                    //ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    //ob.LK_APNT_STATUS_ID = (dr["LK_APNT_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_APNT_STATUS_ID"]);
                    //ob.LK_JOB_STATUS_ID = (dr["LK_JOB_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_STATUS_ID"]);
                    //ob.LK_EMP_TYPE_ID = (dr["LK_EMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_EMP_TYPE_ID"]);
                    //ob.LK_MNG_LVL_ID = (dr["LK_MNG_LVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MNG_LVL_ID"]);
                    //ob.LK_JOB_CATG_ID = (dr["LK_JOB_CATG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_CATG_ID"]);
                    //ob.CONFIRM_DT = (dr["CONFIRM_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CONFIRM_DT"]);
                    //ob.JOINING_DT = (dr["JOINING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["JOINING_DT"]);
                    //ob.APNT_ISSUE_DT = (dr["APNT_ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APNT_ISSUE_DT"]);
                    //ob.PROBATION_PERIOD = (dr["PROBATION_PERIOD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROBATION_PERIOD"]);
                    //ob.PR_PERIOD_TYPE_ID = (dr["PR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PR_PERIOD_TYPE_ID"]);
                    //ob.PROBATION_DT = (dr["PROBATION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROBATION_DT"]);
                    //ob.OFFER_DT = (dr["OFFER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OFFER_DT"]);
                    //ob.REPORTING_DT = (dr["REPORTING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORTING_DT"]);
                    //ob.CONTRACT_PERIOD = (dr["CONTRACT_PERIOD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONTRACT_PERIOD"]);
                    //ob.CT_PERIOD_TYPE_ID = (dr["CT_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CT_PERIOD_TYPE_ID"]);
                    //ob.DUTY_HOUR = (dr["DUTY_HOUR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DUTY_HOUR"]);
                    //ob.ALLOWED_LEAVE_DAY = (dr["ALLOWED_LEAVE_DAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOWED_LEAVE_DAY"]);
                    //ob.GROSS_SALARY = (dr["GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_SALARY"]);
                    //ob.ACC_BK_ACCOUNT_ID = (dr["ACC_BK_ACCOUNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_BK_ACCOUNT_ID"]);
                    //ob.PRE_ADDRESS_EN = (dr["PRE_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_ADDRESS_EN"]);
                    //ob.PRE_ADDRESS_BN = (dr["PRE_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_ADDRESS_BN"]);
                    //ob.PRE_DISTRICT_ID = (dr["PRE_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_DISTRICT_ID"]);
                    //ob.PRE_POSTAL_CODE = (dr["PRE_POSTAL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_POSTAL_CODE"]);
                    //ob.PERM_ADDRESS_EN = (dr["PERM_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_ADDRESS_EN"]);
                    //ob.PERM_ADDRESS_BN = (dr["PERM_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_ADDRESS_BN"]);
                    //ob.PERM_DISTRICT_ID = (dr["PERM_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERM_DISTRICT_ID"]);
                    //ob.PERM_POSTAL_CODE = (dr["PERM_POSTAL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_POSTAL_CODE"]);
                    //ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    //ob.MOB_NO_OFF = (dr["MOB_NO_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_NO_OFF"]);
                    //ob.MOB_NO_PRS = (dr["MOB_NO_PRS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_NO_PRS"]);
                    //ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    //ob.WORK_PHONE_EXT = (dr["WORK_PHONE_EXT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WORK_PHONE_EXT"]);
                    //ob.HOME_PHONE = (dr["HOME_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOME_PHONE"]);
                    //ob.HOME_PHONE_EXT = (dr["HOME_PHONE_EXT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOME_PHONE_EXT"]);
                    //ob.EMAIL_ID_OFF = (dr["EMAIL_ID_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID_OFF"]);
                    //ob.EMAIL_ID_PRS = (dr["EMAIL_ID_PRS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID_PRS"]);
                    //ob.FATHER_NAME_EN = (dr["FATHER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FATHER_NAME_EN"]);
                    //ob.FATHER_NAME_BN = (dr["FATHER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FATHER_NAME_BN"]);
                    //ob.MOTHER_NAME_EN = (dr["MOTHER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOTHER_NAME_EN"]);
                    //ob.MOTHER_NAME_BN = (dr["MOTHER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOTHER_NAME_BN"]);
                    //ob.GUARDIAN_NAME_EN = (dr["GUARDIAN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GUARDIAN_NAME_EN"]);
                    //ob.GUARDIAN_NAME_BN = (dr["GUARDIAN_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GUARDIAN_NAME_BN"]);
                    //ob.LK_RELATION_ID = (dr["LK_RELATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RELATION_ID"]);
                    //ob.LK_RELIGION_ID = (dr["LK_RELIGION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RELIGION_ID"]);
                    //ob.LK_GENDER_ID = (dr["LK_GENDER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GENDER_ID"]);
                    //ob.LK_MRTL_STATUS_ID = (dr["LK_MRTL_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MRTL_STATUS_ID"]);
                    //ob.BIRTH_DT = (dr["BIRTH_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BIRTH_DT"]);
                    //ob.LK_NTLTY_ID = (dr["LK_NTLTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NTLTY_ID"]);
                    //ob.LK_BLD_GRP_ID = (dr["LK_BLD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BLD_GRP_ID"]);
                    //ob.NATIONAL_ID = (dr["NATIONAL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NATIONAL_ID"]);
                    //ob.PASSPORT_NO = (dr["PASSPORT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASSPORT_NO"]);
                    //ob.PASS_ISSUE_DT = (dr["PASS_ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PASS_ISSUE_DT"]);
                    //ob.PASS_EXPIRE_DT = (dr["PASS_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PASS_EXPIRE_DT"]);
                    //ob.PASS_ISS_PLACE = (dr["PASS_ISS_PLACE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASS_ISS_PLACE"]);
                    //ob.DRIV_LIC_NO = (dr["DRIV_LIC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRIV_LIC_NO"]);
                    //ob.DRIV_LIC_EXPIRE_DT = (dr["DRIV_LIC_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DRIV_LIC_EXPIRE_DT"]);
                    //ob.TIN_NO = (dr["TIN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIN_NO"]);
                    //ob.EMP_SIGN = (dr["EMP_SIGN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_SIGN"]);
                    //ob.LK_CITIZENSHP_ID = (dr["LK_CITIZENSHP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CITIZENSHP_ID"]);
                    //ob.VISA_NO = (dr["VISA_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VISA_NO"]);
                    //ob.VISA_EXPIRE_DT = (dr["VISA_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["VISA_EXPIRE_DT"]);
                    //ob.HR_PAY_POLICY_ID = (dr["HR_PAY_POLICY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_POLICY_ID"]);
                    //ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    //ob.IS_OT_HRLY = (dr["IS_OT_HRLY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OT_HRLY"]);
                    //ob.IS_TRANSPORT = (dr["IS_TRANSPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRANSPORT"]);
                    //ob.IS_HOUSING = (dr["IS_HOUSING"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_HOUSING"]);
                    //ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    //ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    //ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                    //ob.OT_UNIT_FLAG = (dr["OT_UNIT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_UNIT_FLAG"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_EMPLOYEEModel> EmployeeListData(Int64? pHR_DEPARTMENT_ID, Int64? pLK_JOB_STATUS_ID, string pEMPLOYEE_CODE, string pOLD_EMP_CODE)
        {
            string sp = "pkg_hr.hr_employee_select";
            try
            {
                var obList = new List<HR_EMPLOYEEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = pLK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pOLD_EMP_CODE", Value = pOLD_EMP_CODE},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = pEMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMPLOYEEModel ob = new HR_EMPLOYEEModel();
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.OLD_EMP_CODE = (dr["OLD_EMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OLD_EMP_CODE"]);
                    ob.REPORTING_MGR_ID = (dr["REPORTING_MGR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REPORTING_MGR_ID"]);
                    ob.TA_PROXI_ID = (dr["TA_PROXI_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_PROXI_ID"]);
                    ob.CARD_NO = (dr["CARD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARD_NO"]);
                    ob.EMP_TITLE = (dr["EMP_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_TITLE"]);
                    ob.EMP_FIRST_NAME_EN = (dr["EMP_FIRST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FIRST_NAME_EN"]);
                    ob.EMP_FIRST_NAME_BN = (dr["EMP_FIRST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FIRST_NAME_BN"]);
                    ob.EMP_LAST_NAME_EN = (dr["EMP_LAST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_LAST_NAME_EN"]);
                    ob.EMP_LAST_NAME_BN = (dr["EMP_LAST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_LAST_NAME_BN"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.EMP_FULL_NAME_BN = (dr["EMP_FULL_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_BN"]);
                    ob.EMP_NICKNM_EN = (dr["EMP_NICKNM_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_NICKNM_EN"]);
                    ob.EMP_NICKNM_BN = (dr["EMP_NICKNM_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_NICKNM_BN"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.SAL_COMPANY_ID = (dr["SAL_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SAL_COMPANY_ID"]);
                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    //if (dr["LK_FLOOR_ID"] != DBNull.Value) LK_DATA_NAME_EN
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.FLOOR_NAME_EN = (dr["FLOOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NAME_EN"]);

                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.LK_APNT_STATUS_ID = (dr["LK_APNT_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_APNT_STATUS_ID"]);
                    ob.LK_JOB_STATUS_ID = (dr["LK_JOB_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_STATUS_ID"]);
                    ob.LK_EMP_TYPE_ID = (dr["LK_EMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_EMP_TYPE_ID"]);
                    ob.LK_MNG_LVL_ID = (dr["LK_MNG_LVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MNG_LVL_ID"]);
                    ob.LK_JOB_CATG_ID = (dr["LK_JOB_CATG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_CATG_ID"]);

                    if (dr["CONFIRM_DT"] != DBNull.Value)
                    { ob.CONFIRM_DT = Convert.ToDateTime(dr["CONFIRM_DT"]); }// ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CONFIRM_DT"]);
                    if (dr["JOINING_DT"] != DBNull.Value)
                    { ob.JOINING_DT = Convert.ToDateTime(dr["JOINING_DT"]); }//(dr["JOINING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["JOINING_DT"]);
                    if (dr["APNT_ISSUE_DT"] != DBNull.Value)
                    { ob.APNT_ISSUE_DT = Convert.ToDateTime(dr["APNT_ISSUE_DT"]); } //(dr["APNT_ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APNT_ISSUE_DT"]);

                    ob.PROBATION_PERIOD = (dr["PROBATION_PERIOD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROBATION_PERIOD"]);
                    ob.PR_PERIOD_TYPE_ID = (dr["PR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PR_PERIOD_TYPE_ID"]);

                    if (dr["PROBATION_DT"] != DBNull.Value)
                    { ob.PROBATION_DT = Convert.ToDateTime(dr["PROBATION_DT"]); } //(dr["PROBATION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROBATION_DT"]);
                    if (dr["OFFER_DT"] != DBNull.Value)
                    { ob.OFFER_DT = Convert.ToDateTime(dr["OFFER_DT"]); }//(dr["OFFER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OFFER_DT"]);

                    //ob.INTERVIEW_HELD = (dr["INTERVIEW_HELD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INTERVIEW_HELD"]);

                    //if (dr["INTERVIEW_DT"] != DBNull.Value)
                    //{ ob.INTERVIEW_DT = Convert.ToDateTime(dr["INTERVIEW_DT"]); }//DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INTERVIEW_DT"]);
                    if (dr["REPORTING_DT"] != DBNull.Value)
                    { ob.REPORTING_DT = Convert.ToDateTime(dr["REPORTING_DT"]); }// DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORTING_DT"]);

                    ob.CONTRACT_PERIOD = (dr["CONTRACT_PERIOD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONTRACT_PERIOD"]);
                    ob.CT_PERIOD_TYPE_ID = (dr["CT_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CT_PERIOD_TYPE_ID"]);
                    ob.DUTY_HOUR = (dr["DUTY_HOUR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DUTY_HOUR"]);
                    ob.ALLOWED_LEAVE_DAY = (dr["ALLOWED_LEAVE_DAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOWED_LEAVE_DAY"]);
                    ob.GROSS_SALARY = (dr["GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_SALARY"]);
                    ob.ACC_BK_ACCOUNT_ID = (dr["ACC_BK_ACCOUNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_BK_ACCOUNT_ID"]);
                    ob.PRE_ADDRESS_EN = (dr["PRE_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_ADDRESS_EN"]);
                    ob.PRE_ADDRESS_BN = (dr["PRE_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_ADDRESS_BN"]);
                    ob.PRE_DIVISION_ID = (dr["PRE_DIVISION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_DIVISION_ID"]);
                    ob.PRE_DISTRICT_ID = (dr["PRE_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_DISTRICT_ID"]);
                    ob.PRE_UPOZILA_ID = (dr["PRE_UPOZILA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_UPOZILA_ID"]);
                    ob.PRE_POSTAL_CODE = (dr["PRE_POSTAL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_POSTAL_CODE"]);
                    ob.PERM_ADDRESS_EN = (dr["PERM_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_ADDRESS_EN"]);
                    ob.PERM_ADDRESS_BN = (dr["PERM_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_ADDRESS_BN"]);
                    ob.PERM_DIVISION_ID = (dr["PERM_DIVISION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERM_DIVISION_ID"]);
                    ob.PERM_DISTRICT_ID = (dr["PERM_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERM_DISTRICT_ID"]);
                    ob.PERM_UPOZILA_ID = (dr["PERM_UPOZILA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERM_UPOZILA_ID"]);
                    ob.PERM_POSTAL_CODE = (dr["PERM_POSTAL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERM_POSTAL_CODE"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.MOB_NO_OFF = (dr["MOB_NO_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_NO_OFF"]);
                    ob.MOB_NO_PRS = (dr["MOB_NO_PRS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_NO_PRS"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.WORK_PHONE_EXT = (dr["WORK_PHONE_EXT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WORK_PHONE_EXT"]);
                    ob.HOME_PHONE = (dr["HOME_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOME_PHONE"]);
                    ob.HOME_PHONE_EXT = (dr["HOME_PHONE_EXT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOME_PHONE_EXT"]);
                    ob.EMAIL_ID_OFF = (dr["EMAIL_ID_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID_OFF"]);
                    ob.EMAIL_ID_PRS = (dr["EMAIL_ID_PRS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID_PRS"]);
                    ob.FATHER_NAME_EN = (dr["FATHER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FATHER_NAME_EN"]);
                    ob.FATHER_NAME_BN = (dr["FATHER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FATHER_NAME_BN"]);
                    ob.MOTHER_NAME_EN = (dr["MOTHER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOTHER_NAME_EN"]);
                    ob.MOTHER_NAME_BN = (dr["MOTHER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOTHER_NAME_BN"]);
                    ob.GUARDIAN_NAME_EN = (dr["GUARDIAN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GUARDIAN_NAME_EN"]);
                    ob.GUARDIAN_NAME_BN = (dr["GUARDIAN_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GUARDIAN_NAME_BN"]);
                    ob.LK_RELATION_ID = (dr["LK_RELATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RELATION_ID"]);
                    ob.LK_RELIGION_ID = (dr["LK_RELIGION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RELIGION_ID"]);
                    ob.LK_GENDER_ID = (dr["LK_GENDER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GENDER_ID"]);
                    ob.LK_MRTL_STATUS_ID = (dr["LK_MRTL_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MRTL_STATUS_ID"]);

                    if (dr["NO_OF_CHILD"] != DBNull.Value)
                        ob.NO_OF_CHILD = (dr["NO_OF_CHILD"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_CHILD"]);

                    if (dr["BIRTH_DT"] != DBNull.Value)
                    { ob.BIRTH_DT = Convert.ToDateTime(dr["BIRTH_DT"]); }

                    // (dr["BIRTH_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BIRTH_DT"]);
                    //ob.BIRTH_DT =  (dr["BIRTH_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BIRTH_DT"]);
                    //ob.BIRTH_DT = Convert.ToDateTime(dr["BIRTH_DT"]);

                    ob.LK_NTLTY_ID = (dr["LK_NTLTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NTLTY_ID"]);
                    ob.LK_BLD_GRP_ID = (dr["LK_BLD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BLD_GRP_ID"]);
                    ob.NATIONAL_ID = (dr["NATIONAL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NATIONAL_ID"]);
                    ob.PASSPORT_NO = (dr["PASSPORT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASSPORT_NO"]);

                    if (dr["PASS_ISSUE_DT"] != DBNull.Value)
                    { ob.PASS_ISSUE_DT = Convert.ToDateTime(dr["PASS_ISSUE_DT"]); } //(dr["PASS_ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PASS_ISSUE_DT"]);
                    if (dr["PASS_EXPIRE_DT"] != DBNull.Value)
                    { ob.PASS_EXPIRE_DT = Convert.ToDateTime(dr["PASS_EXPIRE_DT"]); } //(dr["PASS_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PASS_EXPIRE_DT"]);

                    ob.PASS_ISS_PLACE = (dr["PASS_ISS_PLACE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASS_ISS_PLACE"]);
                    ob.DRIV_LIC_NO = (dr["DRIV_LIC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRIV_LIC_NO"]);

                    if (dr["DRIV_LIC_EXPIRE_DT"] != DBNull.Value)
                    { ob.DRIV_LIC_EXPIRE_DT = Convert.ToDateTime(dr["DRIV_LIC_EXPIRE_DT"]); } //(dr["DRIV_LIC_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DRIV_LIC_EXPIRE_DT"]);

                    ob.TIN_NO = (dr["TIN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIN_NO"]);

                    if (dr["EMP_PHOTO"] != DBNull.Value)
                    {
                        ob.EMP_PHOTO = (byte[])dr["EMP_PHOTO"];
                        ob.EMP_PHOTO_PREVIEW = Convert.ToBase64String(ob.EMP_PHOTO);
                    }//(dr["EMP_PHOTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_PHOTO"]);
                    //ob.EMP_SIGN = (dr["EMP_SIGN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_SIGN"]);
                    //FileContentResult img = null;
                    //img = ob.EMP_PHOTO;
                    ob.LK_CITIZENSHP_ID = (dr["LK_CITIZENSHP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CITIZENSHP_ID"]);
                    ob.VISA_NO = (dr["VISA_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VISA_NO"]);

                    if (dr["VISA_EXPIRE_DT"] != DBNull.Value)
                    { ob.VISA_EXPIRE_DT = Convert.ToDateTime(dr["VISA_EXPIRE_DT"]); } //(dr["VISA_EXPIRE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["VISA_EXPIRE_DT"]);

                    ob.HR_PAY_POLICY_ID = (dr["HR_PAY_POLICY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_POLICY_ID"]);
                    ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.IS_OT_HRLY = (dr["IS_OT_HRLY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OT_HRLY"]);
                    ob.IS_TRANSPORT = (dr["IS_TRANSPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRANSPORT"]);
                    ob.IS_HOUSING = (dr["IS_HOUSING"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_HOUSING"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    //ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    //ob.EMP_PHOTO_PATH = (dr["EMP_PHOTO_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_PHOTO_PATH"]);

                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);

                    ob.OT_UNIT_FLAG = (dr["OT_UNIT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_UNIT_FLAG"]);
                    
                    //HR_EMP_PAYDAL obEmpPayDAL = new HR_EMP_PAYDAL();
                    //ob.EMP_PAY_LIST = obEmpPayDAL.EmpPayListData(ob.HR_EMPLOYEE_ID);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExchangeProxyUpdate(Int64 pHR_EMPLOYEE_ID, string pTA_PROXI_ID)
        {
            const string sp = "pkg_hr.hr_employee_update";
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pTA_PROXI_ID", Value = pTA_PROXI_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 2001},
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



        public object GetEmployeeList(long? pHR_COMPANY_ID, long? pHR_DESIGNATION_GRP_ID, int? pLK_JOB_STATUS_ID, Int32? pCORE_DEPT_ID, string pEMPLOYEE_CODE)
        {
            string sp = "pkg_hr.hr_employee_select";
            try
            {
                var obList = new List<HR_EMPLOYEEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_ID", Value = pHR_DESIGNATION_GRP_ID},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = pLK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = pEMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMPLOYEEModel ob = new HR_EMPLOYEEModel();
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                                        
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    //ob.EMP_FULL_NAME_BN = (dr["EMP_FULL_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_BN"]);                    
                    ob.FLOOR_NAME_EN = (dr["FLOOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NAME_EN"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);

                    

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








    public class HR_EMP_GROSS_UPDATEModel
    {
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public decimal GROSS_SALARY { get; set; }

        public string EmployeeGrossUpdate()
        {
            const string sp = "pkg_hr.hr_emp_gross_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pGROSS_SALARY", Value = ob.GROSS_SALARY},                     
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
    }

}