using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrScheduleD012Model //: IHrCompanyModel
    {
        public Int64 HR_SCHEDULE_D012_ID { get; set; }

        [Required]
        public Int64 HR_SCHEDULE_D01_ID { get; set; }


        [Required]
        public Int64 HR_PUNCH_TYPE_ID { get; set; }

        public DateTime? CLK_START_FROM { get; set; }
        public DateTime? CLK_START_ACT { get; set; }
        public DateTime? CLK_START_TO { get; set; }
        public DateTime? CLK_START_GRACE { get; set; }
        public string IS_CLK_START_SKIP { get; set; }

        public DateTime? CLK_END_FROM { get; set; }
        public DateTime? CLK_END_ACT { get; set; }
        public DateTime? CLK_END_TO { get; set; }
        public DateTime? CLK_END_GRACE { get; set; }
        public string IS_CLK_END_SKIP { get; set; }

        

        public string IS_ACTIVE { get; set; }
        public string PUNCH_TYPE_NAME_EN { get; set; }
        

        //public DateTime? CREATION_DATE { get; set; }
        //public int CREATED_BY { get; set; }
        //public DateTime? LAST_UPDATE_DATE { get; set; }
        //public int LAST_UPDATED_BY { get; set; }        
        //public int LAST_UPDATE_LOGIN { get; set; }
        //public int VERSION_NO { get; set; }

        public List<HrScheduleD012Model> ScheduleClockData(Int64 scheduleD01Id)
        {
            string sp = "pkg_hr.hr_schedule_d012_select";
            try
            {
                var obList = new List<HrScheduleD012Model>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID", Value = scheduleD01Id},
		            new CommandParameter() {ParameterName = "pOption", Value = 3003},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrScheduleD012Model ob = new HrScheduleD012Model();

                    ob.HR_SCHEDULE_D012_ID = (dr["HR_SCHEDULE_D012_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_D012_ID"]);
                    ob.HR_SCHEDULE_D01_ID = (dr["HR_SCHEDULE_D01_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_D01_ID"]);

                    if (dr["CLK_START_FROM"] != DBNull.Value)
                    { ob.CLK_START_FROM = Convert.ToDateTime(dr["CLK_START_FROM"]); }

                    if (dr["CLK_START_ACT"] != DBNull.Value)
                    { ob.CLK_START_ACT = Convert.ToDateTime(dr["CLK_START_ACT"]); }

                    if (dr["CLK_START_TO"] != DBNull.Value)
                    { ob.CLK_START_TO = Convert.ToDateTime(dr["CLK_START_TO"]); }

                    if (dr["CLK_START_GRACE"] != DBNull.Value)
                    { ob.CLK_START_GRACE = Convert.ToDateTime(dr["CLK_START_GRACE"]); }

                    ob.IS_CLK_START_SKIP = (dr["IS_CLK_START_SKIP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLK_START_SKIP"]);

                    if (dr["CLK_END_FROM"] != DBNull.Value)
                    { ob.CLK_END_FROM = Convert.ToDateTime(dr["CLK_END_FROM"]); }

                    if (dr["CLK_END_ACT"] != DBNull.Value)
                    { ob.CLK_END_ACT = Convert.ToDateTime(dr["CLK_END_ACT"]); }

                    if (dr["CLK_END_TO"] != DBNull.Value)
                    { ob.CLK_END_TO = Convert.ToDateTime(dr["CLK_END_TO"]); }

                    if (dr["CLK_END_GRACE"] != DBNull.Value)
                    { ob.CLK_END_GRACE = Convert.ToDateTime(dr["CLK_END_GRACE"]); }

                    ob.IS_CLK_END_SKIP = (dr["IS_CLK_END_SKIP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLK_END_SKIP"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.HR_PUNCH_TYPE_ID = (dr["HR_PUNCH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PUNCH_TYPE_ID"]);
                    ob.PUNCH_TYPE_NAME_EN = (dr["PUNCH_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PUNCH_TYPE_NAME_EN"]);
                    
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
