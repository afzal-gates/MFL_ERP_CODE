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
    public class HR_LEAVE_TRANSModel
    {
        public Int64 HR_LEAVE_TRANS_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }

        [Required(ErrorMessage = "Please select Company")]
        public Int64? HR_COMPANY_ID { get; set; }

        [Required(ErrorMessage = "Please select Fiscal Year")]
        public Int64? RF_FISCAL_YEAR_ID { get; set; }

        public string LEAVE_REF_NO { get; set; }

        [Required(ErrorMessage = "Please select Employee ")]
        public Int64? HR_EMPLOYEE_ID { get; set; }

        [Required(ErrorMessage = "Please select Leave Type ")]
        public Int64? HR_LEAVE_TYPE_ID { get; set; }
        public DateTime? APPLY_DATE { get; set; }

        public DateTime? APPROVE_DATE { get; set; }

        public String LV_TYPE_NAME_EN { get; set; } //Display

        [Required]
        public string IS_DAY_OR_HR { get; set; }

        [Required(ErrorMessage = "Please Choose Start Date")]
        public DateTime? FROM_DATE { get; set; }
        public DateTime? EDD_DT { get; set; }

        [Required(ErrorMessage = "Please Choose To Date")]
        public DateTime? TO_DATE { get; set; }
        public Int64? NO_DAYS_APPL { get; set; }
        public Decimal? NO_HRS_APPL { get; set; }
        public string REASON_DESC_EN { get; set; }
        public string REASON_DESC_BN { get; set; }
        public string LV_TIME_ADD_EN { get; set; }
        public string LV_TIME_ADD_BN { get; set; }

        public Int64? ON_DUTY_EMP_ID { get; set; }
        public string IS_CONFIRM_OD { get; set; }
        public DateTime NEXT_JOIN_DATE { get; set; }
        public string REMARKS { get; set; }
        public String LK_DATA_NAME_EN { get; set; } //Display


        public DateTime? CREATION_DATE { get; set; }
        public Int64? CREATED_BY { get; set; }
        //public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64? LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string EMP_FULL_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string DESIGNATION_NAME_EN { get; set; }

        public string EMPLOYEE_CODE2 { get; set; }

        public Int32? RF_CAL_MONTH_ID { get; set; }

        public DateTime? ATTEN_DATE { get; set; }

        public string NAME_OF_DAY { get; set; }

        public string IS_OL_ADJUSTED { get; set; }

        public DateTime? CLK_IN_WT { get; set; }

        public DateTime? CLK_OUT_WT { get; set; }

        public Int64 HR_LEAVE_APRVL_ID { get; set; }
        public Int64 APPROVER_ID { get; set; }
        public Int64 EXEC_BY_ID { get; set; }
        public DateTime? ACTION_DATE { get; set; }
        public Int64? LK_LV_STATUS_ID { get; set; }
        public Int64 APROVER_LVL_NO { get; set; }

        public string LK_LV_STATUS { get; set; }

        public string IS_VISIBLE { get; set; }

        public string IS_ONLINE { get; set; }

        public string MSG { get; set; }

        public Int64 HR_OFFICE_ID { get; set; }

        public Int64 HR_DEPARTMENT_ID { get; set; }

        public Int64 DESIG_ORDER { get; set; }

        public Int64? LK_FLOOR_ID { get; set; }

        public Int64? LINE_NO { get; set; }

        public Int64 SC_MENU_ID { get; set; }

        public string REMARKS_APR { get; set; }

        public string APPROVED_BY { get; set; }

        public DateTime? LAST_LV_FROM_DATE { get; set; }

        public DateTime? LAST_LV_TO_DATE { get; set; }

        public Int64? LAST_LV_ENJOY { get; set; }
        public String DOC_NAME_EN { get; set; }
        public HttpPostedFileBase ATT_FILE { get; set; }


        private List<HR_SL_DOCSModel> _SlDocsModel = null;
        public List<HR_SL_DOCSModel> items
        {
            get
            {
                if (_SlDocsModel == null)
                {
                    _SlDocsModel = new List<HR_SL_DOCSModel>();
                }
                return _SlDocsModel;
            }
            set
            {
                _SlDocsModel = value;
            }
        }


        public string FILE_TYPE { get; set; }

        public string NEXT_SL_FILE { get; set; }

        public string DOC_PATH_REF { get; set; }

        public string IS_CONFIRM_JOIN { get; set; }

        public Int64? HR_SL_DOCS_ID { get; set; }

        public string TA_FLAG { get; set; }

        public string APRV_EMAIL_LIST { get; set; }


        public string PUSH_REGI_ID { get; set; }

        public string Message { get; set; }


        public List<HR_LEAVE_TRANSModel> OnlineLeaveReqesterNotif()
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_LEAVE_TRANSModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3014},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TRANSModel ob = new HR_LEAVE_TRANSModel();
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.LK_LV_STATUS_ID = (dr["LK_LV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LV_STATUS_ID"]);
                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);
                    ob.LK_LV_STATUS = (dr["LK_LV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_LV_STATUS"]);
                    ob.APPROVED_BY = (dr["APPROVED_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APPROVED_BY"]);
                    if (dr["APPLY_DATE"] != DBNull.Value) { ob.APPLY_DATE = Convert.ToDateTime(dr["APPLY_DATE"]); }
                    if (dr["CREATION_DATE"] != DBNull.Value) { ob.CREATION_DATE = Convert.ToDateTime(dr["CREATION_DATE"]); }
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_LEAVE_TRANSModel LeaveDataById(Int64 HR_LEAVE_TRANS_ID, Int64? HR_LEAVE_APRVL_ID, Int64? SC_USER_ID)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var ob = new HR_LEAVE_TRANSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {   new CommandParameter() {ParameterName = "pOption", Value = 3015},
                            new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = HR_LEAVE_TRANS_ID},
                            new CommandParameter() {ParameterName = "pHR_LEAVE_APRVL_ID", Value = HR_LEAVE_APRVL_ID},
                            new CommandParameter() {ParameterName = "pSC_USER_ID", Value =SC_USER_ID==null?Convert.ToInt64(HttpContext.Current.Session["multiScUserId"]):Convert.ToInt64(SC_USER_ID)},
                            new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                        }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);

                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);

                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);

                    ob.LV_TYPE_NAME_EN = (dr["LV_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_EN"]);

                    ob.NEXT_SL_FILE = (dr["NEXT_SL_FILE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NEXT_SL_FILE"]);

                    ob.APPLY_DATE = (dr["APPLY_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APPLY_DATE"]);
                    ob.APPROVE_DATE = (dr["APPROVE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APPROVE_DATE"]);
                    ob.IS_DAY_OR_HR = (dr["IS_DAY_OR_HR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OR_HR"]);
                    ob.EDD_DT = (dr["EDD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EDD_DT"]);
                    ob.FROM_DATE = Convert.ToDateTime(dr["FROM_DATE"]);
                    ob.TO_DATE = Convert.ToDateTime(dr["TO_DATE"]);

                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);

                    ob.NO_DAYS_APPL = (dr["NO_DAYS_APPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS_APPL"]);
                    ob.NO_HRS_APPL = (dr["NO_HRS_APPL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NO_HRS_APPL"]);
                    ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);
                    ob.REASON_DESC_BN = (dr["REASON_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_BN"]);
                    ob.LV_TIME_ADD_EN = (dr["LV_TIME_ADD_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TIME_ADD_EN"]);
                    ob.LV_TIME_ADD_BN = (dr["LV_TIME_ADD_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TIME_ADD_BN"]);

                    if ((dr["ON_DUTY_EMP_ID"] != DBNull.Value))
                    {
                        ob.ON_DUTY_EMP_ID = Convert.ToInt64(dr["ON_DUTY_EMP_ID"]);
                    }

                    ob.IS_CONFIRM_OD = (dr["IS_CONFIRM_OD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONFIRM_OD"]);
                    ob.NEXT_JOIN_DATE = (dr["NEXT_JOIN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NEXT_JOIN_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_LV_STATUS_ID = (dr["LK_LV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LV_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);

                    if (dr["LAST_LV_FROM_DATE"] != DBNull.Value)
                    {
                        ob.LAST_LV_FROM_DATE = Convert.ToDateTime(dr["LAST_LV_FROM_DATE"]);
                    }

                    if (dr["LAST_LV_TO_DATE"] != DBNull.Value)
                    {
                        ob.LAST_LV_TO_DATE = Convert.ToDateTime(dr["LAST_LV_TO_DATE"]);
                    }
                    ob.LAST_LV_ENJOY = (dr["LAST_LV_ENJOY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_LV_ENJOY"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                    ob.IS_ONLINE = (dr["IS_ONLINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ONLINE"]);

                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMPLOYEE_CODE2 = (dr["EMPLOYEE_CODE2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE2"]);

                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.HR_DEPARTMENT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);

                    ob.HR_LEAVE_APRVL_ID = (dr["HR_LEAVE_APRVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_APRVL_ID"]);
                    ob.REMARKS_APR = (dr["REMARKS_APR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_APR"]);

                    ob.IS_CONFIRM_JOIN = (dr["IS_CONFIRM_JOIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONFIRM_JOIN"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {   new CommandParameter() {ParameterName = "pOption", Value = 3021},
                        new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = ob.HR_LEAVE_TRANS_ID},
                        new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                       
                    }, "pkg_leave.hr_leave_trans_select");

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        HR_SL_DOCSModel obj = new HR_SL_DOCSModel();
                        obj.HR_SL_DOCS_ID = (dr1["HR_SL_DOCS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_SL_DOCS_ID"]);
                        obj.HR_LEAVE_TRANS_ID = (dr1["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["HR_LEAVE_TRANS_ID"]);
                        obj.DOC_ORDER = (dr1["DOC_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["DOC_ORDER"]);
                        obj.DOC_NAME_EN = (dr1["DOC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DOC_NAME_EN"]);
                        obj.DOC_NAME_BN = (dr1["DOC_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DOC_NAME_BN"]);
                        obj.DOC_PATH_REF = (dr1["DOC_PATH_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["DOC_PATH_REF"]);
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

        public HR_LEAVE_TRANSModel saveDataForEleave(HR_LEAVE_TRANSModel ob, Int64 pOption)
        {
            string sp = "pkg_leave.hr_leave_trans_update";
            string vMsg;
            
            OraDatabase db = new OraDatabase();
            try
            {
                var Obj = new HR_LEAVE_TRANSModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                 {
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = ob.HR_LEAVE_TRANS_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pIS_CONFIRM_JOIN", Value = ob.IS_CONFIRM_JOIN==null ? "Y" :ob.IS_CONFIRM_JOIN},
                    new CommandParameter() {ParameterName = "pAPROVER_LVL_NO", Value = ob.APROVER_LVL_NO},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pLEAVE_REF_NO", Value = ob.LEAVE_REF_NO},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ob.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pAPPLY_DATE", Value = ob.APPLY_DATE},
                    new CommandParameter() {ParameterName = "pAPPROVE_DATE", Value = ob.APPROVE_DATE},
                    new CommandParameter() {ParameterName = "pIS_DAY_OR_HR", Value = ob.IS_DAY_OR_HR},
                    new CommandParameter() {ParameterName = "pEDD_DT", Value = ob.EDD_DT},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pNO_DAYS_APPL", Value = ob.NO_DAYS_APPL},
                    new CommandParameter() {ParameterName = "pNO_HRS_APPL", Value = ob.NO_HRS_APPL},
                    new CommandParameter() {ParameterName = "pREASON_DESC_EN", Value = ob.REASON_DESC_EN},
                    new CommandParameter() {ParameterName = "pREASON_DESC_BN", Value = ob.REASON_DESC_BN},
                    new CommandParameter() {ParameterName = "pLV_TIME_ADD_EN", Value = ob.LV_TIME_ADD_EN},
                    new CommandParameter() {ParameterName = "pLV_TIME_ADD_BN", Value = ob.LV_TIME_ADD_BN},
                    new CommandParameter() {ParameterName = "pON_DUTY_EMP_ID", Value = ob.ON_DUTY_EMP_ID},
                    new CommandParameter() {ParameterName = "pIS_CONFIRM_OD", Value = ob.IS_CONFIRM_OD},
                    new CommandParameter() {ParameterName = "pNEXT_JOIN_DATE", Value = ob.NEXT_JOIN_DATE.Date},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pLK_LV_STATUS_ID", Value = ob.LK_LV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pDOC_PATH_REF", Value = ob.DOC_PATH_REF},
                    new CommandParameter() {ParameterName = "pFILE_TYPE", Value = ob.FILE_TYPE},
                    new CommandParameter() {ParameterName = "pDOC_NAME_EN", Value = ob.DOC_NAME_EN},
                    new CommandParameter() {ParameterName = "pHR_SL_DOCS_ID", Value = ob.HR_SL_DOCS_ID},
                    new CommandParameter() {ParameterName = "pNEXT_SL_FILE", Value = ob.NEXT_SL_FILE},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_APRVL_ID", Value = ob.HR_LEAVE_APRVL_ID},
                    new CommandParameter() {ParameterName = "pREMARKS_APR", Value = ob.REMARKS_APR},
                    new CommandParameter() {ParameterName = "pIS_ONLINE", Value = "Y"},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY == null?Convert.ToInt64(HttpContext.Current.Session["multiScUserId"]): ob.CREATED_BY},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY == null?Convert.ToInt64(HttpContext.Current.Session["multiScUserId"]): ob.LAST_UPDATED_BY},
                    new CommandParameter() {ParameterName = "pOption", Value = pOption},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                    }, sp);


                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Obj.HR_LEAVE_APRVL_ID = (dr["HR_LEAVE_APRVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_APRVL_ID"]);

                    Obj.APRV_EMAIL_LIST = (dr["APRV_EMAIL_LIST"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["APRV_EMAIL_LIST"]);

                    Obj.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);

                    Obj.PUSH_REGI_ID = (dr["PUSH_REGI_ID"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["PUSH_REGI_ID"]);


                    if (ob.items.Count > 0)
                    {
                        Int64 i = 0;
                        foreach (HR_SL_DOCSModel item in ob.items)
                        {
                            try
                            {
                                i += 1;
                                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                            {
                                                new CommandParameter() {ParameterName = "pHR_SL_DOCS_ID", Value = item.HR_SL_DOCS_ID},
                                                new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = Obj.HR_LEAVE_TRANS_ID},
                                                new CommandParameter() {ParameterName = "pDOC_ORDER", Value = i},
                                                new CommandParameter() {ParameterName = "pDOC_NAME_EN", Value = item.DOC_NAME_EN},
                                                new CommandParameter() {ParameterName = "pDOC_PATH_REF", Value = item.DOC_PATH_REF},
                                                new CommandParameter() {ParameterName = "pREMOVE", Value = item.REMOVE},
                                                new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                                                new CommandParameter() {ParameterName = "pOption", Value = 1000},
                                                new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                                            }, "pkg_leave.hr_sl_docs_insert");

                                foreach (DataRow dr1 in ds1.Tables["OUTPARAM"].Rows)
                                {
                                    vMsg = dr1["VALUE"].ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    Obj.LK_LV_STATUS_ID = (dr["LK_LV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LV_STATUS_ID"]);
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

        public List<HR_LEAVE_TRANSModel> SelectAll()
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_LEAVE_TRANSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TRANSModel ob = new HR_LEAVE_TRANSModel();
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.APPLY_DATE = (dr["APPLY_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APPLY_DATE"]);
                    ob.APPROVE_DATE = (dr["APPROVE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APPROVE_DATE"]);
                    ob.IS_DAY_OR_HR = (dr["IS_DAY_OR_HR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OR_HR"]);
                    ob.FROM_DATE = Convert.ToDateTime(dr["FROM_DATE"]);
                    ob.TO_DATE = Convert.ToDateTime(dr["TO_DATE"]);
                    ob.NO_DAYS_APPL = (dr["NO_DAYS_APPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS_APPL"]);
                    //ob.NO_DAYS_APRV = (dr["NO_DAYS_APRV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS_APRV"]);
                    ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);
                    ob.REASON_DESC_BN = (dr["REASON_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_BN"]);
                    ob.LV_TIME_ADD_EN = (dr["LV_TIME_ADD_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TIME_ADD_EN"]);
                    ob.LV_TIME_ADD_BN = (dr["LV_TIME_ADD_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TIME_ADD_BN"]);
                    if (dr["ON_DUTY_EMP_ID"] != DBNull.Value)
                    {
                        ob.ON_DUTY_EMP_ID = Convert.ToInt64(dr["ON_DUTY_EMP_ID"]);
                    }
                    ob.IS_CONFIRM_OD = (dr["IS_CONFIRM_OD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONFIRM_OD"]);
                    ob.NEXT_JOIN_DATE = (dr["NEXT_JOIN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NEXT_JOIN_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_LV_STATUS_ID = (dr["LK_LV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LV_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
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

        public List<HR_LEAVE_TRANSModel> OffDayLeaveData(HR_LEAVE_TRANSModel obj)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_LEAVE_TRANSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3012},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = obj.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = obj.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = obj.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = obj.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TRANSModel ob = new HR_LEAVE_TRANSModel();
                    ob.ATTEN_DATE = Convert.ToDateTime(dr["ATTEN_DATE"]);
                    ob.NAME_OF_DAY = (dr["NAME_OF_DAY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NAME_OF_DAY"]);
                    ob.IS_OL_ADJUSTED = (dr["IS_OL_ADJUSTED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OL_ADJUSTED"]);
                    if (dr["CLK_IN_WT"] != DBNull.Value)
                    {
                        ob.CLK_IN_WT = Convert.ToDateTime(dr["CLK_IN_WT"]);
                    }

                    if (dr["CLK_OUT_WT"] != DBNull.Value)
                    {
                        ob.CLK_OUT_WT = Convert.ToDateTime(dr["CLK_OUT_WT"]);
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

        public List<HR_LEAVE_TRANSModel> GetLeaveData(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_EMPLOYEE_ID)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_LEAVE_TRANSModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TRANSModel ob = new HR_LEAVE_TRANSModel();

                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.LV_TYPE_NAME_EN = (dr["LV_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_EN"]);
                    ob.IS_DAY_OR_HR = (dr["IS_DAY_OR_HR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OR_HR"]);
                    ob.FROM_DATE = Convert.ToDateTime(dr["FROM_DATE"]);
                    ob.TO_DATE = Convert.ToDateTime(dr["TO_DATE"]);
                    ob.NO_DAYS_APPL = (dr["NO_DAYS_APPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS_APPL"]);
                    ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);
                    ob.REASON_DESC_BN = (dr["REASON_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_BN"]);
                    ob.LV_TIME_ADD_EN = (dr["LV_TIME_ADD_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TIME_ADD_EN"]);
                    ob.LV_TIME_ADD_BN = (dr["LV_TIME_ADD_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TIME_ADD_BN"]);
                    ob.ON_DUTY_EMP_ID = (dr["ON_DUTY_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ON_DUTY_EMP_ID"]);
                    ob.IS_CONFIRM_OD = (dr["IS_CONFIRM_OD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONFIRM_OD"]);
                    ob.NEXT_JOIN_DATE = (dr["NEXT_JOIN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NEXT_JOIN_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_LV_STATUS_ID = (dr["LK_LV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LV_STATUS_ID"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
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
            const string sp = "pkg_leave.hr_leave_trans_update_manual";
            string jsonStr = "{";
            var i = 1;
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = ob.HR_LEAVE_TRANS_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pLEAVE_REF_NO", Value = ob.LEAVE_REF_NO},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ob.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pAPPLY_DATE", Value = ob.APPLY_DATE},
                    new CommandParameter() {ParameterName = "pAPPROVE_DATE", Value = ob.APPROVE_DATE},
                    new CommandParameter() {ParameterName = "pIS_DAY_OR_HR", Value = ob.IS_DAY_OR_HR},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pNO_DAYS_APPL", Value = ob.NO_DAYS_APPL},
                    new CommandParameter() {ParameterName = "pNO_HRS_APPL", Value = ob.NO_HRS_APPL},
                    new CommandParameter() {ParameterName = "pEDD_DT", Value = ob.EDD_DT},
                    new CommandParameter() {ParameterName = "pREASON_DESC_EN", Value = ob.REASON_DESC_EN},
                    new CommandParameter() {ParameterName = "pREASON_DESC_BN", Value = ob.REASON_DESC_BN},
                    new CommandParameter() {ParameterName = "pLV_TIME_ADD_EN", Value = ob.LV_TIME_ADD_EN},
                    new CommandParameter() {ParameterName = "pLV_TIME_ADD_BN", Value = ob.LV_TIME_ADD_BN},
                    new CommandParameter() {ParameterName = "pON_DUTY_EMP_ID", Value = ob.ON_DUTY_EMP_ID},
                    new CommandParameter() {ParameterName = "pIS_CONFIRM_OD", Value = ob.IS_CONFIRM_OD},
                    new CommandParameter() {ParameterName = "pNEXT_JOIN_DATE", Value = ob.NEXT_JOIN_DATE.Date},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pLK_LV_STATUS_ID", Value = ob.LK_LV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "OP_HR_LEAVE_TRANS_ID", Value = 0, Direction = ParameterDirection.Output}
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

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonStr;
        }

        public string Save(HR_LEAVE_TRANSModel ob)
        {
            const string sp = "pkg_leave.hr_leave_trans_insert";
            string jsonStr = "{";
            var i = 1;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = ob.HR_LEAVE_TRANS_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pLEAVE_REF_NO", Value = ob.LEAVE_REF_NO},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ob.HR_LEAVE_TYPE_ID > 0?ob.HR_LEAVE_TYPE_ID:null},
                    new CommandParameter() {ParameterName = "pAPPLY_DATE", Value = ob.APPLY_DATE},
                    new CommandParameter() {ParameterName = "pAPPROVE_DATE", Value = ob.APPROVE_DATE},
                    new CommandParameter() {ParameterName = "pIS_DAY_OR_HR", Value = ob.IS_DAY_OR_HR},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pNO_DAYS_APPL", Value = ob.NO_DAYS_APPL},
                    new CommandParameter() {ParameterName = "pNO_HRS_APPL", Value = ob.NO_HRS_APPL},
                    new CommandParameter() {ParameterName = "pREASON_DESC_EN", Value = ob.REASON_DESC_EN},
                    new CommandParameter() {ParameterName = "pREASON_DESC_BN", Value = ob.REASON_DESC_BN},
                    new CommandParameter() {ParameterName = "pLV_TIME_ADD_EN", Value = ob.LV_TIME_ADD_EN},
                    new CommandParameter() {ParameterName = "pLV_TIME_ADD_BN", Value = ob.LV_TIME_ADD_BN},
                    new CommandParameter() {ParameterName = "pON_DUTY_EMP_ID", Value = ob.ON_DUTY_EMP_ID},
                    new CommandParameter() {ParameterName = "pIS_CONFIRM_OD", Value = ob.IS_CONFIRM_OD},
                    new CommandParameter() {ParameterName = "pNEXT_JOIN_DATE", Value = ob.NEXT_JOIN_DATE.Date},
                    new CommandParameter() {ParameterName = "pEDD_DT", Value = ob.EDD_DT},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pLK_LV_STATUS_ID", Value = ob.LK_LV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "OP_HR_LEAVE_TRANS_ID", Value = 0, Direction = ParameterDirection.Output}
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

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonStr;
        }

        public string ProcessLeaveReset(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_LEAVE_TYPE_ID)
        {
            const string sp = "pkg_leave.leave_balance_process";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vMsg;
        }

        public List<HR_LEAVE_TRANSModel> loadLeaveTransData(HR_LEAVE_TRANSModel obj)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_LEAVE_TRANSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3011},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = obj.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value =obj.RF_FISCAL_YEAR_ID },
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = obj.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = obj.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_LV_STATUS_ID", Value = obj.LK_LV_STATUS_ID},
                    new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = obj.RF_CAL_MONTH_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TRANSModel ob = new HR_LEAVE_TRANSModel();
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    if (dr["APPLY_DATE"] != DBNull.Value)
                    {
                        ob.APPLY_DATE = Convert.ToDateTime(dr["APPLY_DATE"]);
                    }
                    if (dr["APPROVE_DATE"] != DBNull.Value)
                    {
                        ob.APPROVE_DATE = Convert.ToDateTime(dr["APPROVE_DATE"]);
                    }
                    if (dr["EDD_DT"] != DBNull.Value)
                    {
                        ob.EDD_DT = Convert.ToDateTime(dr["EDD_DT"]);
                    }

                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.LV_TYPE_NAME_EN = (dr["LV_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_EN"]);
                    ob.EMPLOYEE_CODE2 = (dr["EMPLOYEE_CODE2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE2"]);
                    ob.IS_DAY_OR_HR = (dr["IS_DAY_OR_HR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OR_HR"]);
                    ob.FROM_DATE = Convert.ToDateTime(dr["FROM_DATE"]);
                    ob.TO_DATE = Convert.ToDateTime(dr["TO_DATE"]);
                    ob.NO_DAYS_APPL = (dr["NO_DAYS_APPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS_APPL"]);
                    ob.NO_HRS_APPL = (dr["NO_HRS_APPL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NO_HRS_APPL"]);
                    ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);
                    ob.REASON_DESC_BN = (dr["REASON_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_BN"]);
                    ob.LV_TIME_ADD_EN = (dr["LV_TIME_ADD_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TIME_ADD_EN"]);
                    ob.LV_TIME_ADD_BN = (dr["LV_TIME_ADD_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TIME_ADD_BN"]);
                    if (dr["ON_DUTY_EMP_ID"] != DBNull.Value)
                    {
                        ob.ON_DUTY_EMP_ID = Convert.ToInt64(dr["ON_DUTY_EMP_ID"]);
                    }
                    ob.IS_CONFIRM_OD = (dr["IS_CONFIRM_OD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONFIRM_OD"]);

                    if (dr["NEXT_JOIN_DATE"] != DBNull.Value)
                    {
                        ob.NEXT_JOIN_DATE = Convert.ToDateTime(dr["NEXT_JOIN_DATE"]);
                    }
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_LV_STATUS_ID = (dr["LK_LV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LV_STATUS_ID"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
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

        public List<HR_LEAVE_TRANSModel> Notifications(Int64 HR_LEAVE_TRANS_ID)
        {

            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_LEAVE_TRANSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3016},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = HR_LEAVE_TRANS_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TRANSModel ob = new HR_LEAVE_TRANSModel();
                    if (dr["ACTION_DATE"] != DBNull.Value) { ob.ACTION_DATE = Convert.ToDateTime(dr["ACTION_DATE"]); }
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_LV_STATUS = (dr["LK_LV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_LV_STATUS"]);
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


        public List<HR_LEAVE_TRANSModel> AttendanceStatus(Int64 HR_EMPLOYEE_ID, DateTime FROM_DATE, DateTime TO_DATE)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_LEAVE_TRANSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3022},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = TO_DATE},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TRANSModel ob = new HR_LEAVE_TRANSModel();
                    ob.ATTEN_DATE = Convert.ToDateTime(dr["ATTEN_DATE"]);
                    ob.NAME_OF_DAY = (dr["NAME_OF_DAY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NAME_OF_DAY"]);
                    ob.TA_FLAG = (dr["TA_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_FLAG"]);
                    ob.CLK_IN_WT = Convert.ToDateTime(dr["CLK_IN_WT"]);
                    ob.CLK_OUT_WT = Convert.ToDateTime(dr["CLK_OUT_WT"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ClearFromDashboard(HR_LEAVE_TRANSModel obj, Int64 pOption)
        {
            const string sp = "pkg_leave.hr_leave_trans_update_manual";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = obj.HR_LEAVE_TRANS_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_APRVL_ID", Value = obj.HR_LEAVE_APRVL_ID},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = obj.LAST_UPDATED_BY},
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
                throw ex;
            }
            return vMsg;
        }








    }
}