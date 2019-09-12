using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class HrScheduleD01Model //: IHrCompanyModel
    {
        public Int64 HR_SCHEDULE_D01_ID { get; set; }

        [Required]
        public Int64 HR_SCHEDULE_H_ID { get; set; }

       
        [Required(ErrorMessage="Please input week group name")]
        public string WK_GRP_NAME_EN { get; set; }

        public string IS_DAY_OVERLAP { get; set; }

        [Required]
        public string IS_ACTIVE { get; set; }

        public decimal DAY_WORK_HOURS { get; set; }


        //public DateTime? CREATION_DATE { get; set; }
        //public int CREATED_BY { get; set; }
        //public DateTime? LAST_UPDATE_DATE { get; set; }
        //public int LAST_UPDATED_BY { get; set; }        
        //public int LAST_UPDATE_LOGIN { get; set; }
        //public int VERSION_NO { get; set; }

        public object DataBatchSave(List<HrScheduleD011Model> obDay, List<HrScheduleD012Model> obClock)
        {
            string sp = "pkg_hr.hr_schedule_d011_update";
            Int64 v_action = 0;
            Int64 vOption;
            Int64 v_schedule_id = 0;
            Int64 v_schedule_d1_id = 0;

            string vMsg = "";
            object vRtnObj = new { };
            var obWeek = this;

            OraDatabase db = new OraDatabase();
            try
            {                               
                /////////////// Start For Week Group 
                if (obWeek.HR_SCHEDULE_D01_ID < 1 || obWeek.HR_SCHEDULE_D01_ID == null)
                {
                    v_action = 0;
                    sp = "pkg_hr.hr_schedule_d01_insert";
                    vOption = 1000;
                }
                else
                {
                    v_action = 1;
                    sp = "pkg_hr.hr_schedule_d01_update";
                    vOption = 2000;
                }

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = obWeek.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID", Value = obWeek.HR_SCHEDULE_D01_ID},
                    new CommandParameter() {ParameterName = "pWK_GRP_NAME_EN", Value = obWeek.WK_GRP_NAME_EN},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = obWeek.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pIS_DAY_OVERLAP", Value = obWeek.IS_DAY_OVERLAP},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                    new CommandParameter() {ParameterName = "pOption", Value = vOption},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID_1", Direction = ParameterDirection.Output}
                }, sp);

                v_schedule_id = obWeek.HR_SCHEDULE_H_ID;
                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    if (dr["KEY"].ToString() == "PHR_SCHEDULE_D01_ID_1")
                    {
                        if (v_action == 0)
                            v_schedule_d1_id = Convert.ToInt64(dr["VALUE"]);
                        else
                            v_schedule_d1_id = obWeek.HR_SCHEDULE_D01_ID;
                    }
                }                
                /////////////// End For Week Group 


                /////////////// Start For Day Group 
                foreach (var ob in obDay)
                {
                    if ((ob.HR_SCHEDULE_D011_ID < 1 || ob.HR_SCHEDULE_D011_ID == null) && ob.IS_ACTIVE == "Y")
                    {
                        sp = "pkg_hr.hr_schedule_d011_insert";
                        vOption = 1000;
                    }
                    else if (ob.IS_ACTIVE == "Y")
                    {
                        sp = "pkg_hr.hr_schedule_d011_update";
                        vOption = 2000;
                    }
                    else
                    {
                        sp = "pkg_hr.hr_schedule_d011_delete";
                        vOption = 4000;
                    }

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                        new CommandParameter() {ParameterName = "pHR_SCHEDULE_D011_ID", Value = ob.HR_SCHEDULE_D011_ID},
                        new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID", Value = v_schedule_d1_id},
                        new CommandParameter() {ParameterName = "pRF_CALENDAR_DAY_ID", Value = ob.RF_CALENDAR_DAY_ID},                       
                        new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                        new CommandParameter() {ParameterName = "pOption", Value = vOption},
                        new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}                        
                    }, sp);
                    
                    foreach (DataRow dr in ds1.Tables["OUTPARAM"].Rows)
                    {
                        vMsg = dr["VALUE"].ToString();
                    } 
                }
                /////////////// End For Day Group 

                /////////////// Start For Schedule Clock 
                List<HrScheduleD012Model> obList = new List<HrScheduleD012Model>();
                foreach (var ob in obClock)
                {
                    if ((ob.HR_SCHEDULE_D012_ID < 1 || ob.HR_SCHEDULE_D012_ID == null))
                    {
                        sp = "pkg_hr.hr_schedule_d012_insert";
                        vOption = 1000;
                    }
                    else
                    {
                        sp = "pkg_hr.hr_schedule_d012_update";
                        vOption = 2000;
                    }

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                        new CommandParameter() {ParameterName = "pHR_SCHEDULE_D012_ID", Value = ob.HR_SCHEDULE_D012_ID},
                        new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID", Value = v_schedule_d1_id},
                        new CommandParameter() {ParameterName = "pHR_PUNCH_TYPE_ID", Value = ob.HR_PUNCH_TYPE_ID},                       
                        new CommandParameter() {ParameterName = "pCLK_START_FROM", Value = ob.CLK_START_FROM},
                        new CommandParameter() {ParameterName = "pCLK_START_ACT", Value = ob.CLK_START_ACT},
                        new CommandParameter() {ParameterName = "pCLK_START_TO", Value = ob.CLK_START_TO},
                        new CommandParameter() {ParameterName = "pCLK_START_GRACE", Value = ob.CLK_START_GRACE},
                        new CommandParameter() {ParameterName = "pIS_CLK_START_SKIP", Value = ob.IS_CLK_START_SKIP},
                        new CommandParameter() {ParameterName = "pCLK_END_FROM", Value = ob.CLK_END_FROM},
                        new CommandParameter() {ParameterName = "pCLK_END_ACT", Value = ob.CLK_END_ACT},
                        new CommandParameter() {ParameterName = "pCLK_END_TO", Value = ob.CLK_END_TO},
                        new CommandParameter() {ParameterName = "pCLK_END_GRACE", Value = ob.CLK_END_GRACE},
                        new CommandParameter() {ParameterName = "pIS_CLK_END_SKIP", Value = ob.IS_CLK_END_SKIP},
                        new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                        new CommandParameter() {ParameterName = "pOption", Value = vOption},
                        new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}                        
                    }, sp);

                    foreach (DataRow dr in ds1.Tables["OUTPARAM"].Rows)
                    {
                        vMsg = dr["VALUE"].ToString();
                    } 
                    
                    obList = new HrScheduleD012Model().ScheduleClockData(v_schedule_d1_id);
                    //vRtnObj = new { msg = vMsg, HrScheduleId = cmd.Parameters["pOutHR_SCHEDULE_H_ID"].Value };
                }
                /////////////// End For Schedule Clock 

                vRtnObj = new { msg = vMsg, scheduleClockData = obList };


            }
            catch (Exception ex)
            {
                //throw ex;
                vMsg = "MULTI-005" + ex.Message;
            }
            return vRtnObj;

        }

        public HrScheduleD01Model Select(long ID)
        {
            string sp = "pkg_hr.hr_schedule_d01_select";
            try
            {
                var ob = this;

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID", Value = ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3001},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_SCHEDULE_D01_ID = (dr["HR_SCHEDULE_D01_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_D01_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.WK_GRP_NAME_EN = (dr["WK_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WK_GRP_NAME_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_DAY_OVERLAP = (dr["IS_DAY_OVERLAP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OVERLAP"]);
                    ob.DAY_WORK_HOURS = (dr["DAY_WORK_HOURS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DAY_WORK_HOURS"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Update()
        {
            const string sp = "pkg_hr.hr_schedule_d01_update";            
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID", Value = ob.HR_SCHEDULE_D01_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pWK_GRP_NAME_EN", Value = ob.WK_GRP_NAME_EN},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pIS_DAY_OVERLAP", Value = ob.IS_DAY_OVERLAP},
                    
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
                //throw ex;
                vMsg = "MULTI-005" + ex.Message;
            }

            return vMsg;
        }

        public string Save()
        {
            const string sp = "pkg_hr.hr_schedule_d01_insert";
            var ob = this;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_D01_ID", Value = ob.HR_SCHEDULE_D01_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pWK_GRP_NAME_EN", Value = ob.WK_GRP_NAME_EN},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pIS_DAY_OVERLAP", Value = ob.IS_DAY_OVERLAP},
                    
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
                //throw ex;
                vMsg = "MULTI-005" + ex.Message;
            }

            return vMsg;
        }

        public List<HrScheduleD01Model> ScheduleWiseWeekGrpListData(Int64 ScheduleId)
        {
            string sp = "pkg_hr.hr_schedule_d01_select";
            try
            {
                var obList = new List<HrScheduleD01Model>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ScheduleId},
		            new CommandParameter() {ParameterName = "pOption", Value = 3001},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrScheduleD01Model ob = new HrScheduleD01Model();
                    ob.HR_SCHEDULE_D01_ID = (dr["HR_SCHEDULE_D01_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_D01_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.WK_GRP_NAME_EN = (dr["WK_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WK_GRP_NAME_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_DAY_OVERLAP = (dr["IS_DAY_OVERLAP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OVERLAP"]);
                    ob.DAY_WORK_HOURS = (dr["DAY_WORK_HOURS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DAY_WORK_HOURS"]);

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
