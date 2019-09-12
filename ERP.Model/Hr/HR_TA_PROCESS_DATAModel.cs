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
    public class HR_TA_PROCESS_DATAModel //: IHR_EMP_PAYModel
    {
        public Int64 HR_TA_PROCESS_DATA_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public DateTime ATTEN_DATE { get; set; }
        public Int64 HR_SCHEDULE_ASSIGN_ID { get; set; }
        public Int64 HR_SHIFT_TEAM_ID { get; set; }
        public Int64 DAY_WORK_HOURS { get; set; }
        public DateTime? CLK_IN_WT { get; set; }
        public DateTime? CLK_IN_WT_GRACE { get; set; }
        public DateTime? CLK_OUT_WT { get; set; }
        public DateTime? CLK_OUT_WT_GRACE { get; set; }
        public DateTime? CLK_IN_BT { get; set; }
        public DateTime? CLK_OUT_BT { get; set; }
        public DateTime? CLK_IN_OT1 { get; set; }
        public DateTime? CLK_OUT_OT1 { get; set; }
        public DateTime? CLK_IN_OT2 { get; set; }
        public DateTime? CLK_OUT_OT2 { get; set; }
        public DateTime? IN_TIME_WT { get; set; }
        public DateTime? OUT_TIME_WT { get; set; }
        public DateTime? IN_TIME_BT { get; set; }
        public DateTime? OUT_TIME_BT { get; set; }
        public DateTime? IN_TIME_OT1 { get; set; }
        public DateTime? OUT_TIME_OT1 { get; set; }
        public DateTime? IN_TIME_OT2 { get; set; }
        public DateTime? OUT_TIME_OT2 { get; set; }
        public DateTime? LATE_HR { get; set; }
        public DateTime? OT_HR { get; set; }
        public DateTime? EARLY_HR { get; set; }
        public Int64 WORK_OFF_DAY { get; set; }
        public Int64 WORK_NIGHT { get; set; }
        public Int64 MISS_PUNCH_BT { get; set; }
        public string TA_FLAG { get; set; }
        public string REMARKS { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LK_PN_CORR_STATUS_ID { get; set; }
        public Decimal LATE_HR_BT { get; set; }
        public Int64 WORK_OVERSTAY { get; set; }
        public Int64 LK_PN_COR_REASON_ID { get; set; }


        public string AttendanceProcess(DateTime? pFromDate, DateTime? pToDate, Int64? pHR_DEPARTMENT_ID,
            Int64? pHR_DESIGNATION_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID,
            Int64? pHR_MANAGEMENT_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pHR_DAY_TYPE_ID, Int64? pHR_COMPANY_ID, Int64? pHR_OFFICE_ID)
        {
            const string sp = "pkg_attendance.hr_attendance_process";            
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pFromDate", Value = pFromDate},
                    new CommandParameter() {ParameterName = "pToDate", Value = pToDate},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = pHR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = pHR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = pLK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = pLINE_NO},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = pHR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = pHR_DAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},                    
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
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

        public List<HR_TA_PROCESS_DATAModel> AttenDataList()
        {
            const string sp = "pkg_attendance.hr_ta_process_data_select";
            try
            {
                var ob = this;
                var obList = new List<HR_TA_PROCESS_DATAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pATTEN_DATE", Value = ob.ATTEN_DATE},
                    new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_TA_PROCESS_DATAModel obRead = new HR_TA_PROCESS_DATAModel();
                    obRead.HR_TA_PROCESS_DATA_ID = (dr["HR_TA_PROCESS_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TA_PROCESS_DATA_ID"]);
                    obRead.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);                                        
                    obRead.TA_FLAG = (dr["TA_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_FLAG"]);
                    obRead.ATTEN_DATE = (dr["ATTEN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ATTEN_DATE"]);
                    obRead.CLK_IN_WT = (dr["CLK_IN_WT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CLK_IN_WT"]);
                    obRead.CLK_OUT_WT = (dr["CLK_OUT_WT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CLK_OUT_WT"]);
                    obRead.IN_TIME_WT = (dr["IN_TIME_WT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["IN_TIME_WT"]);
                    obRead.OUT_TIME_WT = (dr["OUT_TIME_WT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OUT_TIME_WT"]);
                    obRead.OT_HR = (dr["OT_HR"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OT_HR"]);
                    //obRead.OT_HR_STRING = String.Format("{0:HH:mm}", obRead.OT_HR);
                    
                    obList.Add(obRead);
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