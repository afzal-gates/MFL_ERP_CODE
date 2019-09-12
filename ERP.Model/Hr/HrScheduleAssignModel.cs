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
    public class HrScheduleAssignModel //: IHrCompanyModel
    {
        public Int64 HR_SCHEDULE_ASSIGN_ID { get; set; }

        [Required(ErrorMessage = "Please select company")]
        public Int64 HR_COMPANY_ID { get; set; }
        [Required(ErrorMessage = "Please select office")]
        public Int64 HR_OFFICE_ID { get; set; }
        [Required(ErrorMessage = "Please select team")]
        public Int64 HR_SHIFT_TEAM_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }

        [Required(ErrorMessage = "Please select schedule")]
        public Int64? HR_SCHEDULE_H_ID { get; set; }

        [Required(ErrorMessage = "Please input effective date")]
        public DateTime? EFFECTIVE_FROM { get; set; }
        public DateTime? EXPIRED_ON { get; set; }


        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }

        [Required(ErrorMessage = "Please select shift plan")]
        public Int64 HR_SHIFT_PLAN_ID { get; set; }
        public string SHIFT_TEAM_CODE { get; set; }
        public string SHIFT_TEAM_NAME_EN { get; set; }
        public string SHIFT_TEAM_NAME_BN { get; set; }
        public Int64 ROLL_SEQ_NO { get; set; }
        public string SCHEDULE_NAME_EN { get; set; }
        public string SHIFT_PLAN_NAME_EN { get; set; }
        public string IS_ROLLING { get; set; }
        public string IS_EXISTS { get; set; }
        public string EMP_XML { get; set; }
        public string SCHEDULE_ASSIGN_XML { get; set; }



        public string Save()
        {
            const string sp = "pkg_attendance.hr_schedule_assign_Insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_ASSIGN_ID", Value = ob.HR_SCHEDULE_ASSIGN_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},
                    new CommandParameter() {ParameterName = "pEXPIRED_ON", Value = ob.EXPIRED_ON},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 1001},
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
            const string sp = "pkg_attendance.hr_schedule_assign_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_ASSIGN_ID", Value = ob.HR_SCHEDULE_ASSIGN_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},
                    new CommandParameter() {ParameterName = "pEXPIRED_ON", Value = ob.EXPIRED_ON},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
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
                //throw ex;
                vMsg = "MULTI-005" + ex.Message;
            }

            return vMsg;
        }

        public string Delete()
        {
            const string sp = "pkg_attendance.hr_schedule_assign_delete";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_ASSIGN_ID", Value = ob.HR_SCHEDULE_ASSIGN_ID},
                    //new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    //new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    //new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    //new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},
                    //new CommandParameter() {ParameterName = "pEXPIRED_ON", Value = ob.EXPIRED_ON},
                    //new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 4001},
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

        public string ScheduleByRandomPersonBatchSave()
        {
            const string sp = "pkg_attendance.schedule_assign_random_emp";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},                    
                    new CommandParameter() {ParameterName = "pEMP_XML", Value = ob.EMP_XML},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
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

        public string SaveBatch(List<HrScheduleAssignModel> obAssignList)
        {
            string sp = "pkg_attendance.hr_schedule_assign_insert";
            Int64 vOption;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                foreach (var item in obAssignList)
                {
                    if (item.IS_ACTIVE == "Y")
                    {

                        if (item.IS_EXISTS == "N")
                        {
                            sp = "pkg_attendance.hr_schedule_assign_insert";
                            vOption = 1000;
                        }
                        else
                        {
                            sp = "pkg_attendance.hr_schedule_assign_update";
                            vOption = 2000;
                        }

                        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pHR_SCHEDULE_ASSIGN_ID", Value = item.HR_SCHEDULE_ASSIGN_ID},
                            new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = item.HR_COMPANY_ID},
                            new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = item.HR_OFFICE_ID},
                            new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = item.HR_SHIFT_TEAM_ID},
                            new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = item.HR_SCHEDULE_H_ID},
                            new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = item.EFFECTIVE_FROM},
                            new CommandParameter() {ParameterName = "pEXPIRED_ON", Value = item.EXPIRED_ON},
                            new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = item.IS_ACTIVE},
                            new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                            new CommandParameter() {ParameterName = "pOption", Value = vOption},
                            new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                        }, sp);

                        foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                        {
                            vMsg = dr["VALUE"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }

        public string BatchSave4ScheduleAssignByTeam()
        {
            const string sp = "pkg_attendance.schedule_assign_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                    new CommandParameter() {ParameterName = "pSCHEDULE_ASSIGN_XML", Value = ob.SCHEDULE_ASSIGN_XML},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 50, Direction = ParameterDirection.Output}
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

        public List<HrScheduleAssignModel> ShiftTeamListData(long? pHR_SHIFT_PLAN_ID = null)
        {
            string sp = "pkg_attendance.hr_schedule_assign_select";//pkg_hr.hr_shift_team_select";
            try
            {
                var obList = new List<HrScheduleAssignModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = pHR_SHIFT_PLAN_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3003},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrScheduleAssignModel ob = new HrScheduleAssignModel();

                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.SHIFT_TEAM_CODE = (dr["SHIFT_TEAM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_CODE"]);

                    if (dr["HR_SCHEDULE_H_ID"] != DBNull.Value)
                    { ob.HR_SCHEDULE_H_ID = Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]); }

                    ob.SHIFT_TEAM_NAME_EN = (dr["SHIFT_TEAM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_NAME_EN"]);
                    ob.SHIFT_TEAM_NAME_BN = (dr["SHIFT_TEAM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_NAME_BN"]);
                    ob.ROLL_SEQ_NO = (dr["ROLL_SEQ_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ROLL_SEQ_NO"]);

                    if (dr["EFFECTIVE_FROM"] != DBNull.Value)
                    { ob.EFFECTIVE_FROM = Convert.ToDateTime(dr["EFFECTIVE_FROM"]); }

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_ROLLING = (dr["IS_ROLLING"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ROLLING"]);

                    //ob.HR_SCHEDULE_ASSIGN_ID = (dr["HR_SCHEDULE_ASSIGN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_ASSIGN_ID"]);
                    ob.IS_EXISTS = (dr["IS_EXISTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EXISTS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrScheduleAssignModel> ScheduleWiseAssign(Int64 shiftPlaneId)
        {
            string sp = "pkg_attendance.hr_schedule_assign_select";
            try
            {
                var obList = new List<HrScheduleAssignModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = shiftPlaneId},
		            new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrScheduleAssignModel ob = new HrScheduleAssignModel();
                    //ob.HR_SCHEDULE_ASSIGN_ID = (dr["HR_SCHEDULE_ASSIGN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_ASSIGN_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);

                    ob.EFFECTIVE_FROM = (dr["EFFECTIVE_FROM"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["EFFECTIVE_FROM"]);
                    if (dr["EXPIRED_ON"] != DBNull.Value)
                    { ob.EXPIRED_ON = Convert.ToDateTime(dr["EXPIRED_ON"]); }
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.SHIFT_PLAN_NAME_EN = (dr["SHIFT_PLAN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_PLAN_NAME_EN"]);
                    ob.SHIFT_TEAM_NAME_EN = (dr["SHIFT_TEAM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrScheduleAssignModel> ScheduleWiseAssignByPerson(Int64 pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_attendance.hr_schedule_assign_select";
            try
            {
                var obList = new List<HrScheduleAssignModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3005},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrScheduleAssignModel ob = new HrScheduleAssignModel();
                    ob.HR_SCHEDULE_ASSIGN_ID = (dr["HR_SCHEDULE_ASSIGN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_ASSIGN_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);

                    ob.EFFECTIVE_FROM = (dr["EFFECTIVE_FROM"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["EFFECTIVE_FROM"]);
                    if (dr["EXPIRED_ON"] != DBNull.Value)
                    { ob.EXPIRED_ON = Convert.ToDateTime(dr["EXPIRED_ON"]); }
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.SHIFT_PLAN_NAME_EN = (dr["SHIFT_PLAN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_PLAN_NAME_EN"]);
                    ob.SHIFT_TEAM_NAME_EN = (dr["SHIFT_TEAM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrScheduleAssignModel> PersonScheduleAssignListData(Int64 pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_attendance.hr_schedule_assign_select";
            try
            {
                var obList = new List<HrScheduleAssignModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3004},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrScheduleAssignModel ob = new HrScheduleAssignModel();

                    ob.HR_SCHEDULE_ASSIGN_ID = (dr["HR_SCHEDULE_ASSIGN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_ASSIGN_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.EFFECTIVE_FROM = (dr["EFFECTIVE_FROM"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["EFFECTIVE_FROM"]);
                    if (dr["EXPIRED_ON"] != DBNull.Value)
                    { ob.EXPIRED_ON = Convert.ToDateTime(dr["EXPIRED_ON"]); }
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

        public List<HrScheduleAssignModel> GetShiftTeamList(long? pHR_SHIFT_PLAN_ID = null)
        {
            string sp = "pkg_attendance.hr_schedule_assign_select";//pkg_hr.hr_shift_team_select";
            try
            {
                var obList = new List<HrScheduleAssignModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = pHR_SHIFT_PLAN_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3006},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrScheduleAssignModel ob = new HrScheduleAssignModel();

                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.SHIFT_TEAM_CODE = (dr["SHIFT_TEAM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_CODE"]);
                    ob.SHIFT_TEAM_NAME_EN = (dr["SHIFT_TEAM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_NAME_EN"]);
                    ob.SHIFT_TEAM_NAME_BN = (dr["SHIFT_TEAM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_NAME_BN"]);
                    ob.ROLL_SEQ_NO = (dr["ROLL_SEQ_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ROLL_SEQ_NO"]);
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



    public class HR_SHIFT_TEAM_RESTORE_POINTModel
    {
        public Int64 HR_SHIFT_TEAM_ID { get; set; }
        public string SHIFT_TEAM_NAME_EN { get; set; }
        public Int64 BATCH_NO { get; set; }
        public Int64 NO_OF_EMPLOYEE { get; set; }
        public DateTime? EFFECTIVE_FROM { get; set; }
        public Int64? HR_SCHEDULE_H_ID { get; set; }
        public string SCHEDULE_NAME_EN { get; set; }
        public object HR_SCHEDULE
        {
            get
            {
                return new { MC_FAB_PROC_RATE_ID = this.HR_SCHEDULE_H_ID, FAB_PROC_NAME = this.SCHEDULE_NAME_EN ?? "" };
            }
        }



        public List<HR_SHIFT_TEAM_RESTORE_POINTModel> GetShiftTeamRestoreBatchList()
        {
            string sp = "pkg_attendance.hr_schedule_assign_select";//pkg_hr.hr_shift_team_select";
            try
            {
                var obList = new List<HR_SHIFT_TEAM_RESTORE_POINTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3007},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SHIFT_TEAM_RESTORE_POINTModel ob = new HR_SHIFT_TEAM_RESTORE_POINTModel();

                    ob.HR_SHIFT_TEAM_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.SHIFT_TEAM_NAME_EN = (dr["SHIFT_TEAM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TEAM_NAME_EN"]);
                    ob.NO_OF_EMPLOYEE = (dr["NO_OF_EMPLOYEE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_EMPLOYEE"]);
                    ob.BATCH_NO = (dr["BATCH_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BATCH_NO"]);

                    //ob.HR_SCHEDULE_H_ID = (dr["HR_SHIFT_TEAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TEAM_ID"]);
                    ob.SCHEDULE_NAME_EN = "--N/A--";

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string SchdulRestoreByRandomBatchSave()
        {
            const string sp = "pkg_attendance.schedule_restore_random_emp";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                                        
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},                    
                    new CommandParameter() {ParameterName = "pBATCH_NO", Value = ob.BATCH_NO},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},

                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                    
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
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
