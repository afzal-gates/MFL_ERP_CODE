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
    public class HR_SAL_ADVANCEModel //: IHR_SAL_ADVANCEModel
    {
        public Int64 HR_SAL_ADVANCE_ID { get; set; }        
        public string ADV_REF_NO { get; set; }
        [Required(ErrorMessage = "Please select company")]
        public Int64 HR_COMPANY_ID { get; set; }
        [Required(ErrorMessage = "Please select employee")]
        public Int64 HR_EMPLOYEE_ID { get; set; }
        [Required(ErrorMessage = "Please input request date")]
        public DateTime ADV_RQST_DATE { get; set; }
        [Required(ErrorMessage = "Please input request amount")]
        public Decimal ADV_RQST_AMT { get; set; }        
        public Decimal ADV_APRV_AMT { get; set; }
        public Int64 NO_OF_INSTL { get; set; }
        public Decimal INSTL_AMT { get; set; }
        public DateTime DEDU_ST_DT { get; set; }
        public string REASON_ADV { get; set; }
        public string REMARKS { get; set; }
        
        public Int64 CREATED_BY { get; set; }
        
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 VERSION_NO { get; set; }

        public Int64 HR_OFFICE_ID { get; set; }

        public Int64 HR_DEPARTMENT_ID { get; set; }

        public Int64? LK_FLOOR_ID { get; set; }

        public Int64 LINE_NO { get; set; }

        public Int64 DESIG_ORDER { get; set; }

        public Int64? LK_ADV_STATUS_ID { get; set; }

        public string EMPLOYEE_CODE { get; set; }

        public string EMP_FULL_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string DESIGNATION_NAME_EN { get; set; }

        public DateTime JOINING_DT { get; set; }

        public decimal GROSS_SALARY { get; set; }

        public string LAST_ADV_REF { get; set; }

        public Int64 SC_MENU_ID { get; set; }

        public string LK_ADV_STATUS { get; set; }

        private List<HR_SAL_LN_PAY_SLBModel> _PaySlubModel = null;
        public List<HR_SAL_LN_PAY_SLBModel> items
        {
            get
            {
                if (_PaySlubModel == null)
                {
                    _PaySlubModel = new List<HR_SAL_LN_PAY_SLBModel>();
                }
                return _PaySlubModel;
            }
            set
            {
                _PaySlubModel = value;
            }
        }

        public string REMARKS_APR { get; set; }
        public Int64? HR_SAL_ADV_APRVL_ID { get; set; }
        public Int64 LK_ADV_TYPE_ID { get; set; }
        public Int64? APRV_BY_ID { get; set; }
        public Int64? APPROVER_ID { get; set; }
        public DateTime? ADV_APRV_DATE { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public DateTime? CREATION_DATE { get; set; }

        public Int64? APROVER_LVL_NO { get; set; }

        public string MSG { get; set; }
        public Int64? SALARY_FOR_APPLY { get; set; }

        public string APRV_EMAIL_LIST { get; set; }

        public string LK_ADV_TYPE { get; set; }

        public string APPROVED_BY { get; set; }

        public Int64? MAX_NO_INSTL { get; set; }

        public Int64? TOT_INSTL_PAID { get; set; }

        public decimal? TOT_AMT_PAID { get; set; }

        public List<HR_SAL_ADVANCEModel> SalaryAdvReqesterNotif()
        {
            string sp = "pkg_hr.hr_sal_advance_select";
            try
            {
                var obList = new List<HR_SAL_ADVANCEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])}                    
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SAL_ADVANCEModel ob = new HR_SAL_ADVANCEModel();

                    ob.HR_SAL_ADVANCE_ID = (dr["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADVANCE_ID"]);
                    ob.ADV_REF_NO = (dr["ADV_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADV_REF_NO"]);
                    ob.LK_ADV_STATUS = (dr["LK_ADV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ADV_STATUS"]);
                    ob.LK_ADV_STATUS_ID = (dr["LK_ADV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_STATUS_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    if (dr["ADV_RQST_DATE"] != DBNull.Value) { ob.ADV_RQST_DATE = Convert.ToDateTime(dr["ADV_RQST_DATE"]); }
                    ob.LK_ADV_TYPE = (dr["LK_ADV_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ADV_TYPE"]);
                    ob.APPROVED_BY = (dr["APPROVED_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APPROVED_BY"]);
                    if (dr["CREATION_DATE"] != DBNull.Value) { ob.CREATION_DATE = Convert.ToDateTime(dr["CREATION_DATE"]); }
                    ob.ADV_RQST_AMT = (dr["ADV_RQST_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_RQST_AMT"]);
                    ob.ADV_APRV_AMT = (dr["ADV_APRV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_APRV_AMT"]);
                    ob.NO_OF_INSTL = (dr["NO_OF_INSTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_INSTL"]);
                    ob.INSTL_AMT = (dr["INSTL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INSTL_AMT"]);
                    if (dr["DEDU_ST_DT"] != DBNull.Value) { ob.DEDU_ST_DT = Convert.ToDateTime(dr["DEDU_ST_DT"]); }
                    ob.REASON_ADV = (dr["REASON_ADV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_ADV"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);                            
                    if (dr["LAST_UPDATE_DATE"] != DBNull.Value) { ob.LAST_UPDATE_DATE = Convert.ToDateTime(dr["LAST_UPDATE_DATE"]); }
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Update()
        {
            const string sp = "pkg_hr.hr_sal_advance_update_2";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value =ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pLK_ADV_TYPE_ID", Value = ob.LK_ADV_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pADV_REF_NO", Value = ob.ADV_REF_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value =ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pADV_RQST_DATE", Value = ob.ADV_RQST_DATE.Date},
                    new CommandParameter() {ParameterName = "pADV_RQST_AMT", Value = ob.ADV_RQST_AMT},
                    new CommandParameter() {ParameterName = "pADV_APRV_AMT", Value = ob.ADV_APRV_AMT},
                    new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = ob.NO_OF_INSTL},
                    new CommandParameter() {ParameterName = "pAPRV_BY_ID", Value = ob.APRV_BY_ID==0?null:ob.APRV_BY_ID},
                    new CommandParameter() {ParameterName = "pINSTL_AMT", Value = ob.INSTL_AMT},
                    new CommandParameter() {ParameterName = "pDEDU_ST_DT", Value = ob.DEDU_ST_DT.Date},
                    new CommandParameter() {ParameterName = "pREASON_ADV", Value = ob.REASON_ADV},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pADV_APRV_DATE", Value = ob.ADV_APRV_DATE},
                    new CommandParameter() {ParameterName = "pLK_ADV_STATUS_ID", Value = ob.LK_ADV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 2002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }
        public HR_SAL_ADVANCEModel submitData()
        {
            const string sp = "pkg_hr.hr_sal_advance_update";
            var ob = this;
            try
            {
                var Obj = new HR_SAL_ADVANCEModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pLK_ADV_TYPE_ID", Value = ob.LK_ADV_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pHR_SAL_ADV_APRVL_ID", Value = ob.HR_SAL_ADV_APRVL_ID},
                    new CommandParameter() {ParameterName = "pLK_ADV_STATUS_ID", Value = ob.LK_ADV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pADV_REF_NO", Value = ob.ADV_REF_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pAPROVER_LVL_NO", Value = ob.APROVER_LVL_NO},
                    new CommandParameter() {ParameterName = "pADV_RQST_DATE", Value = ob.ADV_RQST_DATE.Date},
                    new CommandParameter() {ParameterName = "pADV_RQST_AMT", Value = ob.ADV_RQST_AMT},
                    new CommandParameter() {ParameterName = "pADV_APRV_AMT", Value = ob.ADV_APRV_AMT},
                    new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = ob.NO_OF_INSTL},
                    new CommandParameter() {ParameterName = "pINSTL_AMT", Value = ob.INSTL_AMT},
                    new CommandParameter() {ParameterName = "pDEDU_ST_DT", Value = ob.DEDU_ST_DT.Date},
                    new CommandParameter() {ParameterName = "pREASON_ADV", Value = ob.REASON_ADV},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},

                    new CommandParameter() {ParameterName = "pOption", Value = 2001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Obj.APRV_EMAIL_LIST = (dr["APRV_EMAIL_LIST"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["APRV_EMAIL_LIST"]);
                    Obj.LK_ADV_TYPE = (dr["LK_ADV_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ADV_TYPE"]);
                    Obj.ADV_REF_NO = (dr["ADV_REF_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["ADV_REF_NO"]);
                    Obj.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    Obj.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    Obj.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    Obj.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    Obj.ADV_RQST_AMT = (dr["ADV_RQST_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADV_RQST_AMT"]);
                    Obj.ADV_APRV_AMT = (dr["ADV_APRV_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADV_APRV_AMT"]);
                    Obj.APPROVED_BY = (dr["APPROVED_BY"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["APPROVED_BY"]);
                    Obj.DEDU_ST_DT = Convert.ToDateTime(dr["DEDU_ST_DT"]);
                    Obj.NO_OF_INSTL = (dr["NO_OF_INSTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_INSTL"]);
                    Obj.MSG = (dr["MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MSG"]);

                }

                return Obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string saveDataForSectionHead()
        {
            const string sp = "pkg_hr.hr_sal_advance_update_2";
            var ob = this;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SAL_ADV_APRVL_ID", Value = ob.HR_SAL_ADV_APRVL_ID},
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pREMARKS_APR", Value = ob.REMARKS_APR},
                    new CommandParameter() {ParameterName = "pADV_REF_NO", Value = ob.ADV_REF_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pADV_RQST_DATE", Value = ob.ADV_RQST_DATE.Date},
                    new CommandParameter() {ParameterName = "pADV_RQST_AMT", Value = ob.ADV_RQST_AMT},
                    new CommandParameter() {ParameterName = "pADV_APRV_AMT", Value = ob.ADV_APRV_AMT},
                    new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = ob.NO_OF_INSTL},
                    new CommandParameter() {ParameterName = "pINSTL_AMT", Value = ob.INSTL_AMT},
                    new CommandParameter() {ParameterName = "pDEDU_ST_DT", Value = ob.DEDU_ST_DT.Date},
                    new CommandParameter() {ParameterName = "pREASON_ADV", Value = ob.REASON_ADV},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pOption", Value = 2003},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }
        public string ClearFromDashboard(Int64 pOption)
        {
            const string sp = "pkg_hr.hr_sal_advance_update_2";
            var obj = this;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = obj.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pHR_SAL_ADV_APRVL_ID", Value = obj.HR_SAL_ADV_APRVL_ID},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = pOption},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }

        public string fileClose()
        {

            const string sp = "pkg_hr.hr_sal_advance_update_2";
            var ob = this;
            string vMsg = "";


            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SAL_ADV_APRVL_ID", Value = ob.HR_SAL_ADV_APRVL_ID},
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pAPRV_BY_ID", Value = ob.APRV_BY_ID},
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pREMARKS_APR", Value = ob.REMARKS_APR},
                    new CommandParameter() {ParameterName = "pLK_ADV_STATUS_ID", Value = ob.LK_ADV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pADV_REF_NO", Value = ob.ADV_REF_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pADV_RQST_DATE", Value = ob.ADV_RQST_DATE.Date},
                    new CommandParameter() {ParameterName = "pADV_RQST_AMT", Value = ob.ADV_RQST_AMT},
                    new CommandParameter() {ParameterName = "pADV_APRV_AMT", Value = ob.ADV_APRV_AMT},
                    new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = ob.NO_OF_INSTL},
                    new CommandParameter() {ParameterName = "pINSTL_AMT", Value = ob.INSTL_AMT},
                    new CommandParameter() {ParameterName = "pDEDU_ST_DT", Value = ob.DEDU_ST_DT.Date},
                    new CommandParameter() {ParameterName = "pREASON_ADV", Value = ob.REASON_ADV},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pOption", Value = 2001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                if ( ob.items.Count > 0)
                {
                    foreach (HR_SAL_LN_PAY_SLBModel item in ob.items)
                    {
                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {   
                                new CommandParameter() {ParameterName = "pHR_SAL_LN_PAY_SLB_ID", Value = item.HR_SAL_LN_PAY_SLB_ID},
                                new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                                new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = item.NO_OF_INSTL},
                                new CommandParameter() {ParameterName = "pSLAB_NO", Value = item.SLAB_NO},
                                new CommandParameter() {ParameterName = "pINSTL_AMT", Value = item.INSTL_AMT},
                                new CommandParameter() {ParameterName = "pIS_CLOSED", Value = item.IS_CLOSED},
                                new CommandParameter() {ParameterName = "pCAN_BE_DEL", Value = item.CAN_BE_DEL},
                                new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                                new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                                new CommandParameter() {ParameterName = "pOption", Value = 1000},
                                new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                            }, "pkg_hr.hr_sal_ln_pay_slb_insert");
                        foreach (DataRow dr1 in ds1.Tables["OUTPARAM"].Rows)
                        {
                            vMsg = dr1["VALUE"].ToString();
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }
        public String Save()
        {
            const string sp = "pkg_hr.hr_sal_advance_insert";
            var ob = this;
            string vMsg = "";
            var Obj = new HR_SAL_ADVANCEModel();
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pADV_REF_NO", Value = ob.ADV_REF_NO},
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pLK_ADV_TYPE_ID", Value = ob.LK_ADV_TYPE_ID},
                    new CommandParameter() {ParameterName = "pAPRV_BY_ID", Value = ob.APRV_BY_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pADV_RQST_DATE", Value = ob.ADV_RQST_DATE.Date},
                    new CommandParameter() {ParameterName = "pADV_RQST_AMT", Value = ob.ADV_RQST_AMT},
                    new CommandParameter() {ParameterName = "pADV_APRV_AMT", Value = ob.ADV_APRV_AMT},
                    new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = ob.NO_OF_INSTL},
                    new CommandParameter() {ParameterName = "pINSTL_AMT", Value = ob.INSTL_AMT},
                    new CommandParameter() {ParameterName = "pDEDU_ST_DT", Value = ob.DEDU_ST_DT.Date},
                    new CommandParameter() {ParameterName = "pREASON_ADV", Value = ob.REASON_ADV},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Obj.HR_SAL_ADV_APRVL_ID = (dr["HR_SAL_ADV_APRVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADV_APRVL_ID"]);
                    Obj.HR_SAL_ADVANCE_ID = (dr["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADVANCE_ID"]);
                    Obj.LK_ADV_STATUS_ID = (dr["LK_ADV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_STATUS_ID"]);
                    Obj.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    Obj.MSG = (dr["MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MSG"]);
                }

                return vMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HR_SAL_ADVANCEModel SaveAdvData()
        {
            const string sp = "pkg_hr.hr_sal_advance_insert";
            var ob = this;
            try
            {
                var Obj = new HR_SAL_ADVANCEModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pADV_REF_NO", Value = ob.ADV_REF_NO},
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pLK_ADV_TYPE_ID", Value = ob.LK_ADV_TYPE_ID},
                    new CommandParameter() {ParameterName = "pAPRV_BY_ID", Value = ob.APRV_BY_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pADV_RQST_DATE", Value = ob.ADV_RQST_DATE.Date},
                    new CommandParameter() {ParameterName = "pADV_RQST_AMT", Value = ob.ADV_RQST_AMT},
                    new CommandParameter() {ParameterName = "pADV_APRV_AMT", Value = ob.ADV_APRV_AMT},
                    new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = ob.NO_OF_INSTL},
                    new CommandParameter() {ParameterName = "pINSTL_AMT", Value = ob.INSTL_AMT},
                    new CommandParameter() {ParameterName = "pDEDU_ST_DT", Value = ob.DEDU_ST_DT.Date},
                    new CommandParameter() {ParameterName = "pREASON_ADV", Value = ob.REASON_ADV},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Obj.HR_SAL_ADV_APRVL_ID = (dr["HR_SAL_ADV_APRVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADV_APRVL_ID"]);
                    Obj.HR_SAL_ADVANCE_ID = (dr["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADVANCE_ID"]);
                    Obj.LK_ADV_STATUS_ID = (dr["LK_ADV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_STATUS_ID"]);
                    Obj.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    Obj.MSG = (dr["MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MSG"]);
                }
                return Obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HR_SAL_ADVANCEModel SaveManualAdvData()
        {
            const string sp = "pkg_hr.hr_sal_advance_insert";
            var ob = this;
            string vMsg;
            try
            {
                var Obj = new HR_SAL_ADVANCEModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pADV_REF_NO", Value = ob.ADV_REF_NO},
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pLK_ADV_STATUS_ID", Value = ob.LK_ADV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLK_ADV_TYPE_ID", Value = ob.LK_ADV_TYPE_ID},
                    new CommandParameter() {ParameterName = "pAPRV_BY_ID", Value = ob.APRV_BY_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pADV_RQST_DATE", Value = ob.ADV_RQST_DATE.Date},
                    new CommandParameter() {ParameterName = "pADV_RQST_AMT", Value = ob.ADV_RQST_AMT},
                    new CommandParameter() {ParameterName = "pADV_APRV_AMT", Value = ob.ADV_APRV_AMT},
                    new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = ob.NO_OF_INSTL},
                    new CommandParameter() {ParameterName = "pINSTL_AMT", Value = ob.INSTL_AMT},
                    new CommandParameter() {ParameterName = "pDEDU_ST_DT", Value = ob.DEDU_ST_DT.Date},
                    new CommandParameter() {ParameterName = "pADV_APRV_DATE", Value = ob.ADV_APRV_DATE},
                    new CommandParameter() {ParameterName = "pREASON_ADV", Value = ob.REASON_ADV},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pOption", Value = 1002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Obj.HR_SAL_ADV_APRVL_ID = (dr["HR_SAL_ADV_APRVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADV_APRVL_ID"]);
                    Obj.HR_SAL_ADVANCE_ID = (dr["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADVANCE_ID"]);
                    Obj.LK_ADV_STATUS_ID = (dr["LK_ADV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_STATUS_ID"]);
                    Obj.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    Obj.MSG = (dr["MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MSG"]);
                    if (ob.items.Count > 0)
                    {
                        foreach (HR_SAL_LN_PAY_SLBModel item in ob.items)
                        {
                            var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {   
                                new CommandParameter() {ParameterName = "pHR_SAL_LN_PAY_SLB_ID", Value = item.HR_SAL_LN_PAY_SLB_ID},
                                new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = Obj.HR_SAL_ADVANCE_ID},
                                new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = item.NO_OF_INSTL},
                                new CommandParameter() {ParameterName = "pSLAB_NO", Value = item.SLAB_NO},
                                new CommandParameter() {ParameterName = "pINSTL_AMT", Value = item.INSTL_AMT},
                                new CommandParameter() {ParameterName = "pIS_CLOSED", Value = item.IS_CLOSED},
                                new CommandParameter() {ParameterName = "pCAN_BE_DEL", Value = item.CAN_BE_DEL},
                                new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                                new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                                new CommandParameter() {ParameterName = "pOption", Value = 1000},
                                new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                            }, "pkg_hr.hr_sal_ln_pay_slb_insert");
                            foreach (DataRow dr1 in ds1.Tables["OUTPARAM"].Rows)
                            {
                                vMsg = dr1["VALUE"].ToString();
                            }
                        }
                    }

                }
                return Obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HR_SAL_ADVANCEModel submitHrData()
        {
            const string sp = "pkg_hr.hr_sal_advance_insert";
            var SAL_ADV = this;

            try
            {
                var Obj = new HR_SAL_ADVANCEModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_SAL_ADV_APRVL_ID", Value = SAL_ADV.HR_SAL_ADV_APRVL_ID},
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = SAL_ADV.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = SAL_ADV.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = SAL_ADV.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = SAL_ADV.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = SAL_ADV.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = SAL_ADV.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = SAL_ADV.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pREMARKS_APR", Value = SAL_ADV.REMARKS_APR},
                    new CommandParameter() {ParameterName = "pLK_ADV_STATUS_ID", Value = SAL_ADV.LK_ADV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pADV_REF_NO", Value = SAL_ADV.ADV_REF_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = SAL_ADV.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = SAL_ADV.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pADV_RQST_DATE", Value = SAL_ADV.ADV_RQST_DATE.Date},
                    new CommandParameter() {ParameterName = "pADV_RQST_AMT", Value = SAL_ADV.ADV_RQST_AMT},
                    new CommandParameter() {ParameterName = "pADV_APRV_AMT", Value = SAL_ADV.ADV_APRV_AMT},
                    new CommandParameter() {ParameterName = "pNO_OF_INSTL", Value = SAL_ADV.NO_OF_INSTL},
                    new CommandParameter() {ParameterName = "pINSTL_AMT", Value = SAL_ADV.INSTL_AMT},
                    new CommandParameter() {ParameterName = "pDEDU_ST_DT", Value = SAL_ADV.DEDU_ST_DT.Date},
                    new CommandParameter() {ParameterName = "pREASON_ADV", Value = SAL_ADV.REASON_ADV},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = SAL_ADV.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = SAL_ADV.VERSION_NO},
                    new CommandParameter() {ParameterName = "pOption", Value = 1001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Obj.HR_SAL_ADV_APRVL_ID = (dr["HR_SAL_ADV_APRVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADV_APRVL_ID"]);
                    Obj.HR_SAL_ADVANCE_ID = (dr["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADVANCE_ID"]);
                    Obj.LK_ADV_STATUS_ID = (dr["LK_ADV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_STATUS_ID"]);
                    Obj.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    Obj.MSG = (dr["MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MSG"]);
                }
                return Obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HR_SAL_ADVANCEModel SalaryAdvDataById(Int64 HR_SAL_ADVANCE_ID, Int64 HR_SAL_ADV_APRVL_ID)
        {
            string sp = "pkg_hr.hr_sal_advance_select";
            try
            {
                var ob = new HR_SAL_ADVANCEModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pHR_SAL_ADV_APRVL_ID", Value = HR_SAL_ADV_APRVL_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    if (dr["JOINING_DT"] != DBNull.Value)
                    { ob.JOINING_DT = Convert.ToDateTime(dr["JOINING_DT"]); }
                    if (dr["LK_FLOOR_ID"] != DBNull.Value)
                    { ob.LK_FLOOR_ID = Convert.ToInt64(dr["LK_FLOOR_ID"]); }
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.LAST_ADV_REF = (dr["LAST_ADV_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LAST_ADV_REF"]);
                    ob.GROSS_SALARY = (dr["GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_SALARY"]);
                    ob.APRV_BY_ID = (dr["APRV_BY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APRV_BY_ID"]);
                    ob.MAX_NO_INSTL = (dr["MAX_NO_INSTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_NO_INSTL"]);
                    ob.APPROVER_ID = (dr["APPROVER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPROVER_ID"]);
                    ob.LK_ADV_STATUS_ID = (dr["LK_ADV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_STATUS_ID"]);
                    ob.REMARKS_APR = (dr["REMARKS_APR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_APR"]);
                    ob.LK_ADV_TYPE_ID = (dr["LK_ADV_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_TYPE_ID"]);
                    ob.HR_SAL_ADVANCE_ID = (dr["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADVANCE_ID"]);
                    ob.ADV_REF_NO = (dr["ADV_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADV_REF_NO"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    if (dr["ADV_RQST_DATE"] != DBNull.Value) { ob.ADV_RQST_DATE = Convert.ToDateTime(dr["ADV_RQST_DATE"]); }
                    if (dr["ADV_APRV_DATE"] != DBNull.Value) { ob.ADV_APRV_DATE = Convert.ToDateTime(dr["ADV_APRV_DATE"]); }
                    ob.ADV_RQST_AMT = (dr["ADV_RQST_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_RQST_AMT"]);
                    ob.ADV_APRV_AMT = (dr["ADV_APRV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_APRV_AMT"]);
                    ob.NO_OF_INSTL = (dr["NO_OF_INSTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_INSTL"]);
                    ob.INSTL_AMT = (dr["INSTL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INSTL_AMT"]);
                    ob.SALARY_FOR_APPLY = (dr["SALARY_FOR_APPLY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SALARY_FOR_APPLY"]);
                    if (dr["DEDU_ST_DT"] != DBNull.Value) { ob.DEDU_ST_DT = Convert.ToDateTime(dr["DEDU_ST_DT"]); }
                    ob.REASON_ADV = (dr["REASON_ADV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_ADV"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   
                            new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3000},
                            new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                        }, "pkg_hr.hr_sal_ln_pay_slb_select");
                        foreach (DataRow dr1 in ds1.Tables[0].Rows)
                        {
                            HR_SAL_LN_PAY_SLBModel obj = new HR_SAL_LN_PAY_SLBModel();
                            obj.HR_SAL_LN_PAY_SLB_ID = (dr1["HR_SAL_LN_PAY_SLB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_SAL_LN_PAY_SLB_ID"]);
                            obj.HR_SAL_ADVANCE_ID = (dr1["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_SAL_ADVANCE_ID"]);
                            obj.SLAB_NO = (dr1["SLAB_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SLAB_NO"]);
                            obj.NO_OF_INSTL = (dr1["NO_OF_INSTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["NO_OF_INSTL"]);
                            obj.INSTL_AMT = (dr1["INSTL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["INSTL_AMT"]);
                            obj.TOT_INSTL_PAID = (dr1["TOT_INSTL_PAID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TOT_INSTL_PAID"]);
                            obj.IS_CLOSED = (dr1["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_CLOSED"]);
                            obj.CREATED_BY = (dr1["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CREATED_BY"]);
                            obj.LAST_UPDATED_BY = (dr1["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LAST_UPDATED_BY"]);
                            obj.VERSION_NO = (dr1["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["VERSION_NO"]);
                            ob.items.Add(obj);
                        }
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HR_SAL_ADVANCEModel> CurrentSalaryAdvanceList()
        {
            string sp = "pkg_hr.hr_sal_advance_select";

            try
            {
                var obList = new List<HR_SAL_ADVANCEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 3006},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SAL_ADVANCEModel ob = new HR_SAL_ADVANCEModel();
                    ob.HR_SAL_ADVANCE_ID = (dr["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_ADVANCE_ID"]);
                    ob.ADV_REF_NO = (dr["ADV_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADV_REF_NO"]);
                    ob.NO_OF_INSTL = (dr["NO_OF_INSTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_INSTL"]);
                    ob.LK_ADV_TYPE_ID = (dr["LK_ADV_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ADV_TYPE_ID"]);
                    ob.TOT_INSTL_PAID = (dr["TOT_INSTL_PAID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_INSTL_PAID"]);
                    //ob.LAST_AMT_PAID = (dr["LAST_AMT_PAID"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_AMT_PAID"]);
                    ob.TOT_AMT_PAID = (dr["TOT_AMT_PAID"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_AMT_PAID"]);
                    //ob.AMOUNT_LEFT = (dr["AMOUNT_LEFT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AMOUNT_LEFT"]);                            
                    //if (dr["LAST_DT_PAID"] != DBNull.Value) { ob.LAST_DT_PAID = Convert.ToDateTime(dr["LAST_DT_PAID"]); }
                    ob.INSTL_AMT = (dr["INSTL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INSTL_AMT"]);
                    if (dr["ADV_RQST_DATE"] != DBNull.Value) { ob.ADV_RQST_DATE = Convert.ToDateTime(dr["ADV_RQST_DATE"]); }
                    ob.ADV_APRV_AMT = (dr["ADV_APRV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_APRV_AMT"]);
                    ob.LK_ADV_TYPE = (dr["LK_ADV_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ADV_TYPE"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   
                            new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3000},
                            new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                        }, "pkg_hr.hr_sal_ln_pay_slb_select");

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        HR_SAL_LN_PAY_SLBModel obj = new HR_SAL_LN_PAY_SLBModel();
                        obj.HR_SAL_LN_PAY_SLB_ID = (dr1["HR_SAL_LN_PAY_SLB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_SAL_LN_PAY_SLB_ID"]);
                        obj.HR_SAL_ADVANCE_ID = (dr1["HR_SAL_ADVANCE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_SAL_ADVANCE_ID"]);
                        obj.SLAB_NO = (dr1["SLAB_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SLAB_NO"]);
                        obj.NO_OF_INSTL = (dr1["NO_OF_INSTL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["NO_OF_INSTL"]);
                        obj.INSTL_AMT = (dr1["INSTL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["INSTL_AMT"]);
                        obj.TOT_INSTL_PAID = (dr1["TOT_INSTL_PAID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TOT_INSTL_PAID"]);
                        obj.IS_CLOSED = (dr1["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_CLOSED"]);
                        obj.CREATED_BY = (dr1["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CREATED_BY"]);
                        obj.LAST_UPDATED_BY = (dr1["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["LAST_UPDATED_BY"]);
                        obj.VERSION_NO = (dr1["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["VERSION_NO"]);
                        ob.items.Add(obj);
                    }
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HR_SAL_ADVANCEModel> getBoardOfDirectors()
        {
            string sp = "pkg_hr.hr_sal_advance_select";

            try
            {
                var obList = new List<HR_SAL_ADVANCEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SAL_ADVANCEModel ob = new HR_SAL_ADVANCEModel();
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
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