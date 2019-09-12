using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class RfCalendarDayModel //: IHrCompanyModel
    {
        public Int64 RF_CALENDAR_DAY_ID { get; set; }


        [Required]
        public string CALENDAR_DAY_NAME_EN { get; set; }
        [Required]
        public string CALENDAR_DAY_NAME_BN { get; set; }
        [Required]
        public string CALENDAR_DAY_SNAME { get; set; }
       

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }


        public Int64 HR_SCHEDULE_D01_ID { get; set; }
        public Int64 HR_SCHEDULE_D011_ID { get; set; }
        public string IS_ACTIVE { get; set; }

        public List<RfCalendarDayModel> DayListData()
        {
            string sp = "pkg_admin.rf_calendar_day_select";
            try
            {
                var obList = new List<RfCalendarDayModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pOption", Value = 3000},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RfCalendarDayModel ob = new RfCalendarDayModel();
                    ob.RF_CALENDAR_DAY_ID = (dr["RF_CALENDAR_DAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_DAY_ID"]);
                    ob.CALENDAR_DAY_NAME_EN = (dr["CALENDAR_DAY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CALENDAR_DAY_NAME_EN"]);
                    ob.CALENDAR_DAY_NAME_BN = (dr["CALENDAR_DAY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CALENDAR_DAY_NAME_BN"]);
                    ob.CALENDAR_DAY_SNAME = (dr["CALENDAR_DAY_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CALENDAR_DAY_SNAME"]);

                    obList.Add(ob);
                }
                return obList;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RfCalendarDayModel> ScheduleDayData(Int64 scheduleId, Int64? scheduleD01Id)
        {
            string sp = "pkg_hr.hr_schedule_h_select";
            try
            {
                var obList = new List<RfCalendarDayModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = scheduleId},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID", Value = scheduleD01Id},
		            new CommandParameter() {ParameterName = "pOption", Value = 3003},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RfCalendarDayModel ob = new RfCalendarDayModel();
                    ob.RF_CALENDAR_DAY_ID = (dr["RF_CALENDAR_DAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_DAY_ID"]);
                    ob.HR_SCHEDULE_D01_ID = (dr["HR_SCHEDULE_D01_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_D01_ID"]);
                    ob.HR_SCHEDULE_D011_ID = (dr["HR_SCHEDULE_D011_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_D011_ID"]);
                    ob.CALENDAR_DAY_NAME_EN = (dr["CALENDAR_DAY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CALENDAR_DAY_NAME_EN"]);
                    //ob.CALENDAR_DAY_NAME_BN = (dr["CALENDAR_DAY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CALENDAR_DAY_NAME_BN"]);
                    //ob.CALENDAR_DAY_SNAME = (dr["CALENDAR_DAY_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CALENDAR_DAY_SNAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

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
