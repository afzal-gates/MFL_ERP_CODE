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
    public class HrShiftPlanModel //: IHrCompanyModel
    {
        public Int64 HR_SHIFT_PLAN_ID { get; set; }
        public string SHIFT_PLAN_CODE { get; set; }

        //[Required]
        //public Int64 HR_SHIFT_TYPE_ID { get; set; }

        [Required]
        public string SHIFT_PLAN_NAME_EN { get; set; }
        [Required]
        public string SHIFT_PLAN_NAME_BN { get; set; }
        public string SHIFT_PLAN_DESC { get; set; }

        [Required]
        public string IS_ROLLING { get; set; }
       
        public Int64 DAY_WORK_HOURS { get; set; }        
        public Int64 WORK_DAYS_WK { get; set; }        
        public Int64 AVG_WK_HOURS { get; set; }

        public string TIME_COVERAGE { get; set; }
        public Int64? ROLLING_CYCLE_ID { get; set; }
        public Int64 ROLLING_RECUR_VALUE { get; set; }
        public string ROLLING_SEQ { get; set; }
        public Int64 TEAMS_REQ { get; set; }

        [Required]
        public string IS_MULTI_SHIFT { get; set; }
        [Required]
        public string IS_ACTIVE { get; set; }

        
        public Int64? RF_CALENDAR_DAY_ID { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }

        public string Save()
        {
            const string sp = "pkg_hr.hr_shift_plan_insert";            
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = ob.HR_SHIFT_PLAN_ID},
                    new CommandParameter() {ParameterName = "pSHIFT_PLAN_CODE", Value = ob.SHIFT_PLAN_CODE},
                    new CommandParameter() {ParameterName = "pSHIFT_PLAN_NAME_EN", Value = ob.SHIFT_PLAN_NAME_EN},
                    new CommandParameter() {ParameterName = "pSHIFT_PLAN_NAME_BN", Value = ob.SHIFT_PLAN_NAME_BN},
                    new CommandParameter() {ParameterName = "pSHIFT_PLAN_DESC", Value = ob.SHIFT_PLAN_DESC},
                    new CommandParameter() {ParameterName = "pIS_ROLLING", Value = ob.IS_ROLLING},
                    new CommandParameter() {ParameterName = "pDAY_WORK_HOURS", Value = ob.DAY_WORK_HOURS},
                    new CommandParameter() {ParameterName = "pWORK_DAYS_WK", Value = ob.WORK_DAYS_WK},
                    new CommandParameter() {ParameterName = "pAVG_WK_HOURS", Value = ob.AVG_WK_HOURS},
                    new CommandParameter() {ParameterName = "pTIME_COVERAGE", Value = ob.TIME_COVERAGE},
                    new CommandParameter() {ParameterName = "pTEAMS_REQ", Value = ob.TEAMS_REQ},                    
                    new CommandParameter() {ParameterName = "pROLLING_CYCLE_ID", Value = ob.ROLLING_CYCLE_ID},
                    new CommandParameter() {ParameterName = "pROLLING_RECUR_VALUE", Value = ob.ROLLING_RECUR_VALUE},
                    new CommandParameter() {ParameterName = "pROLLING_SEQ", Value = ob.ROLLING_SEQ},
                    new CommandParameter() {ParameterName = "pRF_CALENDAR_DAY_ID", Value = ob.RF_CALENDAR_DAY_ID},
                    new CommandParameter() {ParameterName = "pIS_MULTI_SHIFT", Value = ob.IS_MULTI_SHIFT},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

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

        public string Update()
        {
            const string sp = "pkg_hr.hr_shift_plan_update";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = ob.HR_SHIFT_PLAN_ID},
                    new CommandParameter() {ParameterName = "pSHIFT_PLAN_CODE", Value = ob.SHIFT_PLAN_CODE},
                    new CommandParameter() {ParameterName = "pSHIFT_PLAN_NAME_EN", Value = ob.SHIFT_PLAN_NAME_EN},
                    new CommandParameter() {ParameterName = "pSHIFT_PLAN_NAME_BN", Value = ob.SHIFT_PLAN_NAME_BN},
                    new CommandParameter() {ParameterName = "pSHIFT_PLAN_DESC", Value = ob.SHIFT_PLAN_DESC},
                    new CommandParameter() {ParameterName = "pIS_ROLLING", Value = ob.IS_ROLLING},
                    new CommandParameter() {ParameterName = "pDAY_WORK_HOURS", Value = ob.DAY_WORK_HOURS},
                    new CommandParameter() {ParameterName = "pWORK_DAYS_WK", Value = ob.WORK_DAYS_WK},
                    new CommandParameter() {ParameterName = "pAVG_WK_HOURS", Value = ob.AVG_WK_HOURS},
                    new CommandParameter() {ParameterName = "pTIME_COVERAGE", Value = ob.TIME_COVERAGE},
                    new CommandParameter() {ParameterName = "pTEAMS_REQ", Value = ob.TEAMS_REQ},                    
                    new CommandParameter() {ParameterName = "pROLLING_CYCLE_ID", Value = ob.ROLLING_CYCLE_ID},
                    new CommandParameter() {ParameterName = "pROLLING_RECUR_VALUE", Value = ob.ROLLING_RECUR_VALUE},
                    new CommandParameter() {ParameterName = "pROLLING_SEQ", Value = ob.ROLLING_SEQ},
                    new CommandParameter() {ParameterName = "pRF_CALENDAR_DAY_ID", Value = ob.RF_CALENDAR_DAY_ID},
                    new CommandParameter() {ParameterName = "pIS_MULTI_SHIFT", Value = ob.IS_MULTI_SHIFT},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                    

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

        public List<HrShiftPlanModel> SelectAll()
        {
            string sp = "pkg_hr.hr_shift_plan_select";
            try
            {
                var obList = new List<HrShiftPlanModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOption", Value = 3000},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrShiftPlanModel ob = new HrShiftPlanModel();

                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.SHIFT_PLAN_CODE = (dr["SHIFT_PLAN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_PLAN_CODE"]);
                    //ob.HR_SHIFT_TYPE_ID = (dr["HR_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TYPE_ID"]);
                    ob.SHIFT_PLAN_NAME_EN = (dr["SHIFT_PLAN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_PLAN_NAME_EN"]);
                    ob.SHIFT_PLAN_NAME_BN = (dr["SHIFT_PLAN_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_PLAN_NAME_BN"]);
                    ob.SHIFT_PLAN_DESC = (dr["SHIFT_PLAN_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_PLAN_DESC"]);
                    ob.IS_ROLLING = (dr["IS_ROLLING"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ROLLING"]);
                    ob.DAY_WORK_HOURS = (dr["DAY_WORK_HOURS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY_WORK_HOURS"]);
                    ob.WORK_DAYS_WK = (dr["WORK_DAYS_WK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WORK_DAYS_WK"]);
                    ob.AVG_WK_HOURS = (dr["AVG_WK_HOURS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AVG_WK_HOURS"]);
                    ob.TIME_COVERAGE = (dr["TIME_COVERAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIME_COVERAGE"]);
                    ob.ROLLING_CYCLE_ID = (dr["ROLLING_CYCLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ROLLING_CYCLE_ID"]);
                    ob.ROLLING_RECUR_VALUE = (dr["ROLLING_RECUR_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ROLLING_RECUR_VALUE"]);
                    ob.ROLLING_SEQ = (dr["ROLLING_SEQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLLING_SEQ"]);
                    ob.TEAMS_REQ = (dr["TEAMS_REQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TEAMS_REQ"]);                    
                    ob.RF_CALENDAR_DAY_ID = (dr["RF_CALENDAR_DAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_DAY_ID"]);
                    ob.IS_MULTI_SHIFT = (dr["IS_MULTI_SHIFT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_SHIFT"]);
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
