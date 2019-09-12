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
    public class HR_OT_APPROVEModel //: IHR_OT_APPROVEModel
    {
        public Int64 HR_OT_APPROVE_ID { get; set; }
        public Int64 ACC_PAY_PERIOD_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }

        [Required(ErrorMessage = "Please input OT approve date")]
        public DateTime OT_APRV_DATE { get; set; }

        [Required(ErrorMessage = "Please input OT date")]
        public DateTime OT_DATE { get; set; }
        public string OT_APRV_REASON_EN { get; set; }
        public string OT_APRV_REASON_BN { get; set; }

        [Required(ErrorMessage = "Please select an employee")]
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string IN_TIME { get; set; }
        public string OUT_TIME { get; set; }
        public string OT_HR { get; set; }

        public string IS_ATTEN_CORRECTION { get; set; }


        public string Save(HR_TA_PROCESS_DATAModel ob1)
        {
            const string sp = "pkg_hr.hr_ot_approve_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_OT_APPROVE_ID", Value = ob.HR_OT_APPROVE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pOT_APRV_DATE", Value = ob.OT_APRV_DATE},
                    new CommandParameter() {ParameterName = "pOT_DATE", Value = ob.OT_DATE},
                    new CommandParameter() {ParameterName = "pOT_APRV_REASON_EN", Value = ob.OT_APRV_REASON_EN},
                    new CommandParameter() {ParameterName = "pOT_APRV_REASON_BN", Value = ob.OT_APRV_REASON_BN},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pIS_ATTEN_CORRECTION", Value = ob.IS_ATTEN_CORRECTION},

                    new CommandParameter() {ParameterName = "pHR_TA_PROCESS_DATA_ID", Value = ob1.HR_TA_PROCESS_DATA_ID},
                    new CommandParameter() {ParameterName = "pIN_TIME_WT", Value = ob1.IN_TIME_WT},
                    new CommandParameter() {ParameterName = "pOUT_TIME_WT", Value = ob1.OUT_TIME_WT},
                    new CommandParameter() {ParameterName = "pOT_HR", Value = ob1.OT_HR},
                    new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob1.TA_FLAG},

                    new CommandParameter() {ParameterName = "pIS_ATTEN_CORRECTION", Value = ob.IS_ATTEN_CORRECTION},
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
                vMsg = ex.ToString();
            }

            return vMsg;
        }

        public string DeleteBatch(List<HR_OT_APPROVEModel> obList)
        {
            const string sp = "pkg_hr.hr_ot_approve_delete";
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                foreach (HR_OT_APPROVEModel ob in obList)
                {
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                        new CommandParameter() {ParameterName = "pHR_OT_APPROVE_ID", Value = ob.HR_OT_APPROVE_ID},
                        new CommandParameter() {ParameterName = "pOT_DATE", Value = ob.OT_DATE},
                        new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                        new CommandParameter() {ParameterName = "pOption", Value = 4000},
                        new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                    }, sp);

                    foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                    {
                        vMsg = dr["VALUE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }

        public List<HR_OT_APPROVEModel> OtApproveListData(DateTime pOT_APRV_DATE)
        {
            string sp = "pkg_hr.hr_ot_approve_select";
            try
            {
                var obList = new List<HR_OT_APPROVEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOT_APRV_DATE", Value = pOT_APRV_DATE},

                    new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_OT_APPROVEModel ob = new HR_OT_APPROVEModel();
                    ob.HR_OT_APPROVE_ID = (dr["HR_OT_APPROVE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OT_APPROVE_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.OT_APRV_DATE = (dr["OT_APRV_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OT_APRV_DATE"]);
                    ob.OT_DATE = (dr["OT_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OT_DATE"]);
                    ob.OT_APRV_REASON_EN = (dr["OT_APRV_REASON_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_APRV_REASON_EN"]);
                    ob.OT_APRV_REASON_BN = (dr["OT_APRV_REASON_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_APRV_REASON_BN"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    //ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.IN_TIME = (dr["IN_TIME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IN_TIME"]);
                    ob.OUT_TIME = (dr["OUT_TIME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OUT_TIME"]);
                    ob.OT_HR = (dr["OT_HR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_HR"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_OT_APPROVEModel> OtApproveSearchListData(DateTime? pOT_FROM_DATE, DateTime? pOT_TO_DATE, Int64? pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_hr.hr_ot_approve_select";
            try
            {
                var obList = new List<HR_OT_APPROVEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOT_FROM_DATE", Value = pOT_FROM_DATE},
                    new CommandParameter() {ParameterName = "pOT_TO_DATE", Value = pOT_TO_DATE},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},

                    new CommandParameter() {ParameterName = "pOption", Value = 3003},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_OT_APPROVEModel ob = new HR_OT_APPROVEModel();
                    ob.HR_OT_APPROVE_ID = (dr["HR_OT_APPROVE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OT_APPROVE_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.OT_APRV_DATE = (dr["OT_APRV_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OT_APRV_DATE"]);
                    ob.OT_DATE = (dr["OT_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OT_DATE"]);
                    ob.OT_APRV_REASON_EN = (dr["OT_APRV_REASON_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_APRV_REASON_EN"]);
                    ob.OT_APRV_REASON_BN = (dr["OT_APRV_REASON_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_APRV_REASON_BN"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    //ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.IN_TIME = (dr["IN_TIME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IN_TIME"]);
                    ob.OUT_TIME = (dr["OUT_TIME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OUT_TIME"]);
                    ob.OT_HR = (dr["OT_HR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_HR"]);

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