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
    public class HR_LEAVE_TYPEModel
    {
        public Int64 HR_LEAVE_TYPE_ID { get; set; }    
   
        [Required(ErrorMessage="Leave Type Code Is Required")]
        public string LEAVE_TYPE_CODE { get; set; }
        [Required(ErrorMessage = "Please Choose Colour Code")]
        public string COLOR_CODE { get; set; }
        [Required(ErrorMessage = "Leave Type Name[EN] is required")]
        public string LV_TYPE_NAME_EN { get; set; }
        [Required(ErrorMessage = "Leave Type Name[BN] is required")]
        public string LV_TYPE_NAME_BN { get; set; }
        public string IS_PAID { get; set; }
        public Int64 LINK_PAY_ELEMENT_ID { get; set; }
        public string IS_FY_CF { get; set; }
        public string IS_ACCURED { get; set; }
        public string IS_ACCURED_AUTO { get; set; }
        public string IS_OFFDAY_INCLUDE { get; set; }
        public string IS_HOLIDAY_INCLUDE { get; set; }
        public string IS_L_P_A { get; set; }
        public Int64 MAX_DAYS_CF { get; set; }
        public Int64 DAYS_NOTIFY { get; set; }
        public Int64 MAX_DAYS_LV { get; set; }
        public Int64 MIN_PERIOD_REQ { get; set; }
        public Int64 HR_PERIOD_TYPE_ID { get; set; }
        public Int64 MIN_DAYS_WORK { get; set; }
        public string IS_FEMALE { get; set; }
        public string IS_CASH { get; set; }
        public string IS_ACTIVE { get; set; }
        public string REMARKS { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string IS_BAL_CHQ { get; set; }

        public string Save()
        {
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ob.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLEAVE_TYPE_CODE", Value = ob.LEAVE_TYPE_CODE},
                    new CommandParameter() {ParameterName = "pCOLOR_CODE", Value = ob.COLOR_CODE},
                    new CommandParameter() {ParameterName = "pLV_TYPE_NAME_EN", Value = ob.LV_TYPE_NAME_EN},
                    new CommandParameter() {ParameterName = "pLV_TYPE_NAME_BN", Value = ob.LV_TYPE_NAME_BN},
                    new CommandParameter() {ParameterName = "pIS_PAID", Value = ob.IS_PAID},
                    new CommandParameter() {ParameterName = "pLINK_PAY_ELEMENT_ID", Value = ob.LINK_PAY_ELEMENT_ID},
                    new CommandParameter() {ParameterName = "pIS_FY_CF", Value = ob.IS_FY_CF},
                    new CommandParameter() {ParameterName = "pIS_ACCURED", Value = ob.IS_ACCURED},
                    new CommandParameter() {ParameterName = "pIS_ACCURED_AUTO", Value = ob.IS_ACCURED_AUTO},
                    new CommandParameter() {ParameterName = "pIS_OFFDAY_INCLUDE", Value = ob.IS_OFFDAY_INCLUDE},
                    new CommandParameter() {ParameterName = "pIS_HOLIDAY_INCLUDE", Value = ob.IS_HOLIDAY_INCLUDE},
                    new CommandParameter() {ParameterName = "pIS_L_P_A", Value = ob.IS_L_P_A},
                    new CommandParameter() {ParameterName = "pMAX_DAYS_CF", Value = ob.MAX_DAYS_CF},
                    new CommandParameter() {ParameterName = "pDAYS_NOTIFY", Value = ob.DAYS_NOTIFY},
                    new CommandParameter() {ParameterName = "pMAX_DAYS_LV", Value = ob.MAX_DAYS_LV},
                    new CommandParameter() {ParameterName = "pMIN_PERIOD_REQ", Value = ob.MIN_PERIOD_REQ},
                    new CommandParameter() {ParameterName = "pHR_PERIOD_TYPE_ID", Value = ob.HR_PERIOD_TYPE_ID},
                    new CommandParameter() {ParameterName = "pMIN_DAYS_WORK", Value = ob.MIN_DAYS_WORK},
                    new CommandParameter() {ParameterName = "pIS_FEMALE", Value = ob.IS_FEMALE},
                    new CommandParameter() {ParameterName = "pIS_CASH", Value = ob.IS_CASH},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pIS_BAL_CHQ", Value = ob.IS_BAL_CHQ},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, "pkg_leave.hr_leave_type_insert");

                string vMsg = "";
                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

                return vMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string Update()
        {
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ob.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLEAVE_TYPE_CODE", Value = ob.LEAVE_TYPE_CODE},
                    new CommandParameter() {ParameterName = "pCOLOR_CODE", Value = ob.COLOR_CODE},
                    new CommandParameter() {ParameterName = "pLV_TYPE_NAME_EN", Value = ob.LV_TYPE_NAME_EN},
                    new CommandParameter() {ParameterName = "pLV_TYPE_NAME_BN", Value = ob.LV_TYPE_NAME_BN},
                    new CommandParameter() {ParameterName = "pIS_PAID", Value = ob.IS_PAID},
                    new CommandParameter() {ParameterName = "pLINK_PAY_ELEMENT_ID", Value = ob.LINK_PAY_ELEMENT_ID},
                    new CommandParameter() {ParameterName = "pIS_FY_CF", Value = ob.IS_FY_CF},
                    new CommandParameter() {ParameterName = "pIS_ACCURED", Value = ob.IS_ACCURED},
                    new CommandParameter() {ParameterName = "pIS_ACCURED_AUTO", Value = ob.IS_ACCURED_AUTO},
                    new CommandParameter() {ParameterName = "pIS_OFFDAY_INCLUDE", Value = ob.IS_OFFDAY_INCLUDE},
                    new CommandParameter() {ParameterName = "pIS_HOLIDAY_INCLUDE", Value = ob.IS_HOLIDAY_INCLUDE},
                    new CommandParameter() {ParameterName = "pIS_L_P_A", Value = ob.IS_L_P_A},
                    new CommandParameter() {ParameterName = "pMAX_DAYS_CF", Value = ob.MAX_DAYS_CF},
                    new CommandParameter() {ParameterName = "pDAYS_NOTIFY", Value = ob.DAYS_NOTIFY},
                    new CommandParameter() {ParameterName = "pMAX_DAYS_LV", Value = ob.MAX_DAYS_LV},
                    new CommandParameter() {ParameterName = "pMIN_PERIOD_REQ", Value = ob.MIN_PERIOD_REQ},
                    new CommandParameter() {ParameterName = "pHR_PERIOD_TYPE_ID", Value = ob.HR_PERIOD_TYPE_ID},
                    new CommandParameter() {ParameterName = "pMIN_DAYS_WORK", Value = ob.MIN_DAYS_WORK},
                    new CommandParameter() {ParameterName = "pIS_FEMALE", Value = ob.IS_FEMALE},
                    new CommandParameter() {ParameterName = "pIS_CASH", Value = ob.IS_CASH},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pIS_BAL_CHQ", Value =ob.IS_BAL_CHQ},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, "pkg_leave.hr_leave_type_update");

                string vMsg = "";
                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

                return vMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Delete()
        {
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ob.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLEAVE_TYPE_CODE", Value = ob.LEAVE_TYPE_CODE},
                    new CommandParameter() {ParameterName = "pCOLOR_CODE", Value = ob.COLOR_CODE},
                    new CommandParameter() {ParameterName = "pLV_TYPE_NAME_EN", Value = ob.LV_TYPE_NAME_EN},
                    new CommandParameter() {ParameterName = "pLV_TYPE_NAME_BN", Value = ob.LV_TYPE_NAME_BN},
                    new CommandParameter() {ParameterName = "pIS_PAID", Value = ob.IS_PAID},
                    new CommandParameter() {ParameterName = "pLINK_PAY_ELEMENT_ID", Value = ob.LINK_PAY_ELEMENT_ID},
                    new CommandParameter() {ParameterName = "pIS_FY_CF", Value = ob.IS_FY_CF},
                    new CommandParameter() {ParameterName = "pIS_ACCURED", Value = ob.IS_ACCURED},
                    new CommandParameter() {ParameterName = "pIS_ACCURED_AUTO", Value = ob.IS_ACCURED_AUTO},
                    new CommandParameter() {ParameterName = "pIS_OFFDAY_INCLUDE", Value = ob.IS_OFFDAY_INCLUDE},
                    new CommandParameter() {ParameterName = "pIS_HOLIDAY_INCLUDE", Value = ob.IS_HOLIDAY_INCLUDE},
                    new CommandParameter() {ParameterName = "pIS_L_P_A", Value = ob.IS_L_P_A},
                    new CommandParameter() {ParameterName = "pMAX_DAYS_CF", Value = ob.MAX_DAYS_CF},
                    new CommandParameter() {ParameterName = "pDAYS_NOTIFY", Value = ob.DAYS_NOTIFY},
                    new CommandParameter() {ParameterName = "pMAX_DAYS_LV", Value = ob.MAX_DAYS_LV},
                    new CommandParameter() {ParameterName = "pMIN_PERIOD_REQ", Value = ob.MIN_PERIOD_REQ},
                    new CommandParameter() {ParameterName = "pHR_PERIOD_TYPE_ID", Value = ob.MIN_PERIOD_REQ},
                    new CommandParameter() {ParameterName = "pMIN_DAYS_WORK", Value = ob.MIN_DAYS_WORK},
                    new CommandParameter() {ParameterName = "pIS_FEMALE", Value = ob.IS_FEMALE},
                    new CommandParameter() {ParameterName = "pIS_CASH", Value = ob.IS_CASH},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value =ob.CREATED_BY},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                    new CommandParameter() {ParameterName = "pLAST_UPDATE_LOGIN", Value =ob.LAST_UPDATE_LOGIN},
                    new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
                     
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, "pkg_leave.hr_leave_type_delete");
                
                string vMsg = "";
                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
                return vMsg;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<HR_LEAVE_TYPEModel> SelectAll()
        {
            const string sp = "pkg_leave.hr_leave_type_select";
            try
            {
                var obList = new List<HR_LEAVE_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TYPEModel ob = new HR_LEAVE_TYPEModel();
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.IS_BAL_CHQ = (dr["IS_BAL_CHQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BAL_CHQ"]);
                    ob.LEAVE_TYPE_CODE = (dr["LEAVE_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_TYPE_CODE"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.LV_TYPE_NAME_EN = (dr["LV_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_EN"]);
                    ob.LV_TYPE_NAME_BN = (dr["LV_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_BN"]);
                    ob.IS_PAID = (dr["IS_PAID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PAID"]);
                    ob.LINK_PAY_ELEMENT_ID = (dr["LINK_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINK_PAY_ELEMENT_ID"]);
                    ob.IS_FY_CF = (dr["IS_FY_CF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FY_CF"]);
                    ob.IS_ACCURED = (dr["IS_ACCURED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACCURED"]);
                    ob.IS_ACCURED_AUTO = (dr["IS_ACCURED_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACCURED_AUTO"]);
                    ob.IS_OFFDAY_INCLUDE = (dr["IS_OFFDAY_INCLUDE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OFFDAY_INCLUDE"]);
                    ob.IS_HOLIDAY_INCLUDE = (dr["IS_HOLIDAY_INCLUDE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_HOLIDAY_INCLUDE"]);
                    ob.IS_L_P_A = (dr["IS_L_P_A"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_L_P_A"]);
                    ob.MAX_DAYS_CF = (dr["MAX_DAYS_CF"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DAYS_CF"]);
                    ob.DAYS_NOTIFY = (dr["DAYS_NOTIFY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_NOTIFY"]);
                    ob.MAX_DAYS_LV = (dr["MAX_DAYS_LV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DAYS_LV"]);
                    ob.MIN_PERIOD_REQ = (dr["MIN_PERIOD_REQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_PERIOD_REQ"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.MIN_DAYS_WORK = (dr["MIN_DAYS_WORK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_DAYS_WORK"]);
                    ob.IS_FEMALE = (dr["IS_FEMALE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FEMALE"]);
                    ob.IS_CASH = (dr["IS_CASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CASH"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
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
        public HR_LEAVE_TYPEModel Select(long ID)
        {
            const string sp = "pkg_leave.hr_leave_type_select";
            try
            {
                var obList = new List<HR_LEAVE_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                var ob = new HR_LEAVE_TYPEModel();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.LEAVE_TYPE_CODE = (dr["LEAVE_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_TYPE_CODE"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.LV_TYPE_NAME_EN = (dr["LV_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_EN"]);
                    ob.LV_TYPE_NAME_BN = (dr["LV_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_BN"]);
                    ob.IS_PAID = (dr["IS_PAID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PAID"]);
                    ob.LINK_PAY_ELEMENT_ID = (dr["LINK_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINK_PAY_ELEMENT_ID"]);
                    ob.IS_FY_CF = (dr["IS_FY_CF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FY_CF"]);
                    ob.IS_ACCURED = (dr["IS_ACCURED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACCURED"]);
                    ob.IS_ACCURED_AUTO = (dr["IS_ACCURED_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACCURED_AUTO"]);
                    ob.IS_OFFDAY_INCLUDE = (dr["IS_OFFDAY_INCLUDE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OFFDAY_INCLUDE"]);
                    ob.IS_HOLIDAY_INCLUDE = (dr["IS_HOLIDAY_INCLUDE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_HOLIDAY_INCLUDE"]);
                    ob.IS_L_P_A = (dr["IS_L_P_A"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_L_P_A"]);
                    ob.MAX_DAYS_CF = (dr["MAX_DAYS_CF"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DAYS_CF"]);
                    ob.DAYS_NOTIFY = (dr["DAYS_NOTIFY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_NOTIFY"]);
                    ob.MAX_DAYS_LV = (dr["MAX_DAYS_LV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DAYS_LV"]);
                    ob.MIN_PERIOD_REQ = (dr["MIN_PERIOD_REQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_PERIOD_REQ"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.MIN_DAYS_WORK = (dr["MIN_DAYS_WORK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_DAYS_WORK"]);
                    ob.IS_FEMALE = (dr["IS_FEMALE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FEMALE"]);
                    ob.IS_CASH = (dr["IS_CASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CASH"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HR_LEAVE_TYPEModel> ProcessableLeaveType()
        {
            const string sp = "pkg_leave.hr_leave_type_select";
            try
            {
                var obList = new List<HR_LEAVE_TYPEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_TYPEModel ob = new HR_LEAVE_TYPEModel();
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.IS_BAL_CHQ = (dr["IS_BAL_CHQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BAL_CHQ"]);
                    ob.LEAVE_TYPE_CODE = (dr["LEAVE_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_TYPE_CODE"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.LV_TYPE_NAME_EN = (dr["LV_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_EN"]);
                    ob.LV_TYPE_NAME_BN = (dr["LV_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_BN"]);
                    ob.IS_PAID = (dr["IS_PAID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PAID"]);
                    ob.LINK_PAY_ELEMENT_ID = (dr["LINK_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINK_PAY_ELEMENT_ID"]);
                    ob.IS_FY_CF = (dr["IS_FY_CF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FY_CF"]);
                    ob.IS_ACCURED = (dr["IS_ACCURED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACCURED"]);
                    ob.IS_ACCURED_AUTO = (dr["IS_ACCURED_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACCURED_AUTO"]);
                    ob.IS_OFFDAY_INCLUDE = (dr["IS_OFFDAY_INCLUDE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OFFDAY_INCLUDE"]);
                    ob.IS_HOLIDAY_INCLUDE = (dr["IS_HOLIDAY_INCLUDE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_HOLIDAY_INCLUDE"]);
                    ob.IS_L_P_A = (dr["IS_L_P_A"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_L_P_A"]);
                    ob.MAX_DAYS_CF = (dr["MAX_DAYS_CF"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DAYS_CF"]);
                    ob.DAYS_NOTIFY = (dr["DAYS_NOTIFY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_NOTIFY"]);
                    ob.MAX_DAYS_LV = (dr["MAX_DAYS_LV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DAYS_LV"]);
                    ob.MIN_PERIOD_REQ = (dr["MIN_PERIOD_REQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_PERIOD_REQ"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.MIN_DAYS_WORK = (dr["MIN_DAYS_WORK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_DAYS_WORK"]);
                    ob.IS_FEMALE = (dr["IS_FEMALE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FEMALE"]);
                    ob.IS_CASH = (dr["IS_CASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CASH"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
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
    }
}