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
    public class HR_SCHEDULE_HModel //: IHrCompanyModel
    {
        public Int64 HR_SCHEDULE_H_ID { get; set; }

        [Required(ErrorMessage="Please select shift plan")]
        public Int64 HR_SHIFT_PLAN_ID { get; set; }
        [Required(ErrorMessage = "Please select shift type")]
        public Int64 HR_SHIFT_TYPE_ID { get; set; }
        public string SCHEDULE_CODE { get; set; }

        [Required(ErrorMessage="Please input schedule name [E]")]
        public string SCHEDULE_NAME_EN { get; set; }
        [Required(ErrorMessage = "Please input schedule name [B]")]
        public string SCHEDULE_NAME_BN { get; set; }

        public string SCHEDULE_DESC { get; set; }

        [Required]
        public string IS_ACTIVE { get; set; }
        [Required]
        public string IS_MULTI_GRP { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }        
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }



        public string SHIFT_PLAN_NAME_EN { get; set; }
        public string SHIFT_TYPE_NAME_EN { get; set; }

        public Int64 HR_SCHEDULE_D01_ID { get; set; }
        public string WK_GRP_NAME_EN { get; set; }

        public List<HR_SCHEDULE_HModel> ScheduleWithPlanListData()
        {
            string sp = "pkg_hr.hr_schedule_h_select";
            try
            {
                var obList = new List<HR_SCHEDULE_HModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3005},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SCHEDULE_HModel ob = new HR_SCHEDULE_HModel();

                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.HR_SHIFT_TYPE_ID = (dr["HR_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TYPE_ID"]);
                    ob.SCHEDULE_CODE = (dr["SCHEDULE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_CODE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.SCHEDULE_NAME_BN = (dr["SCHEDULE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_BN"]);
                    ob.SCHEDULE_DESC = (dr["SCHEDULE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_DESC"]);
                    ob.SHIFT_PLAN_NAME_EN = (dr["SHIFT_PLAN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_PLAN_NAME_EN"]);
                    //ob.SHIFT_TYPE_NAME_EN = (dr["SHIFT_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TYPE_NAME_EN"]);
                    //ob.HR_SCHEDULE_D01_ID = (dr["HR_SCHEDULE_D01_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_D01_ID"]);
                    //ob.WK_GRP_NAME_EN = (dr["WK_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WK_GRP_NAME_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_MULTI_GRP = (dr["IS_MULTI_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_GRP"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HR_SCHEDULE_HModel> ScheduleListData(Int64 schedulePlanId)
        {
            string sp = "pkg_hr.hr_schedule_h_select";
            try
            {
                var obList = new List<HR_SCHEDULE_HModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = schedulePlanId},
		            new CommandParameter() {ParameterName = "pOption", Value = 3006},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SCHEDULE_HModel ob = new HR_SCHEDULE_HModel();

                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.HR_SHIFT_TYPE_ID = (dr["HR_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TYPE_ID"]);
                    ob.SCHEDULE_CODE = (dr["SCHEDULE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_CODE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.SCHEDULE_NAME_BN = (dr["SCHEDULE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_BN"]);
                    ob.SCHEDULE_DESC = (dr["SCHEDULE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_DESC"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_MULTI_GRP = (dr["IS_MULTI_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_GRP"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HR_SCHEDULE_HModel> ScheduleListDataWithActiveSche(Int64 schedulePlanId)
        {
            string sp = "pkg_hr.hr_schedule_h_select";
            try
            {
                var obList = new List<HR_SCHEDULE_HModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = schedulePlanId},
		            new CommandParameter() {ParameterName = "pOption", Value = 3007},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SCHEDULE_HModel ob = new HR_SCHEDULE_HModel();

                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.HR_SHIFT_TYPE_ID = (dr["HR_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TYPE_ID"]);
                    ob.SCHEDULE_CODE = (dr["SCHEDULE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_CODE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.SCHEDULE_NAME_BN = (dr["SCHEDULE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_BN"]);
                    ob.SCHEDULE_DESC = (dr["SCHEDULE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_DESC"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_SCHE_ACTIVE = (dr["IS_SCHE_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SCHE_ACTIVE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object DataSave()
        {
            const string sp = "pkg_hr.hr_schedule_h_insert";
            Int64 vScheduleId=0;
            Int64 vScheduleD_Id=0;
            string vMsg = "";
            object vRtnObj = new { };

            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = ob.HR_SHIFT_PLAN_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TYPE_ID", Value = ob.HR_SHIFT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pSCHEDULE_CODE", Value = ob.SCHEDULE_CODE},
                    new CommandParameter() {ParameterName = "pSCHEDULE_NAME_EN", Value = ob.SCHEDULE_NAME_EN},
                    new CommandParameter() {ParameterName = "pSCHEDULE_NAME_BN", Value = ob.SCHEDULE_NAME_BN},
                    new CommandParameter() {ParameterName = "pSCHEDULE_DESC", Value = ob.SCHEDULE_DESC},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pIS_MULTI_GRP", Value = ob.IS_MULTI_GRP},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pOutHR_SCHEDULE_H_ID", Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pOutHR_SCHEDULE_D01_ID", Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    if (dr["KEY"].ToString()=="PMSG")
                        vMsg = dr["VALUE"].ToString();
                    else if (dr["KEY"].ToString() == "POUTHR_SCHEDULE_H_ID")
                    {
                        if (dr["VALUE"] != null)
                        {
                            vScheduleId = Convert.ToInt64(dr["VALUE"]);
                        }
                    }
                    else if (dr["KEY"].ToString() == "POUTHR_SCHEDULE_D01_ID")
                    {
                        if (dr["VALUE"] != null)
                            vScheduleD_Id = Convert.ToInt64(dr["VALUE"]);
                    }
                }
                vRtnObj = new { msg = vMsg, HrScheduleId = vScheduleId, HrScheduleD_Id = vScheduleD_Id };                
            }
            catch (Exception ex)
            {
                //throw ex;
                vMsg = "MULTI-005" + ex.Message;
            }
            return vRtnObj = new { msg = vMsg, HrScheduleId = vScheduleId, HrScheduleD_Id = vScheduleD_Id };

        }

        public List<HR_SCHEDULE_HModel> ScheduleListData()
        {
            string sp = "pkg_hr.hr_schedule_h_select";
            try
            {
                var obList = new List<HR_SCHEDULE_HModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3004},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SCHEDULE_HModel ob = new HR_SCHEDULE_HModel();

                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.HR_SHIFT_TYPE_ID = (dr["HR_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TYPE_ID"]);
                    ob.SCHEDULE_CODE = (dr["SCHEDULE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_CODE"]);                    
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.SCHEDULE_NAME_BN = (dr["SCHEDULE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_BN"]);
                    ob.SCHEDULE_DESC = (dr["SCHEDULE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_DESC"]);
                    //ob.SHIFT_PLAN_NAME_EN = (dr["SHIFT_PLAN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_PLAN_NAME_EN"]);
                    //ob.SHIFT_TYPE_NAME_EN = (dr["SHIFT_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_TYPE_NAME_EN"]);
                    ob.HR_SCHEDULE_D01_ID = (dr["HR_SCHEDULE_D01_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_D01_ID"]);
                    ob.WK_GRP_NAME_EN = (dr["WK_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WK_GRP_NAME_EN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_MULTI_GRP = (dr["IS_MULTI_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_GRP"]);
                    
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
            const string sp = "pkg_hr.hr_schedule_h_update";            
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = ob.HR_SHIFT_PLAN_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TYPE_ID", Value = ob.HR_SHIFT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pSCHEDULE_CODE", Value = ob.SCHEDULE_CODE},
                    new CommandParameter() {ParameterName = "pSCHEDULE_NAME_EN", Value = ob.SCHEDULE_NAME_EN},
                    new CommandParameter() {ParameterName = "pSCHEDULE_NAME_BN", Value = ob.SCHEDULE_NAME_BN},
                    new CommandParameter() {ParameterName = "pSCHEDULE_DESC", Value = ob.SCHEDULE_DESC},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pIS_MULTI_GRP", Value = ob.IS_MULTI_GRP},
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

        public object GetKnitScheduleList()
        {
            string sp = "pkg_hr.hr_schedule_h_select";
            try
            {
                var obList = new List<HR_SCHEDULE_HModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3008},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SCHEDULE_HModel ob = new HR_SCHEDULE_HModel();

                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.HR_SHIFT_TYPE_ID = (dr["HR_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TYPE_ID"]);
                    ob.SCHEDULE_CODE = (dr["SCHEDULE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_CODE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.SCHEDULE_NAME_BN = (dr["SCHEDULE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_BN"]);
                    ob.SCHEDULE_DESC = (dr["SCHEDULE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_DESC"]);
                                     
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HR_SCHEDULE_HModel> GetScheduleList(long? pHR_SHIFT_PLAN_ID = null)
        {
            string sp = "pkg_hr.hr_schedule_h_select";
            try
            {
                var obList = new List<HR_SCHEDULE_HModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = pHR_SHIFT_PLAN_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3006},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_SCHEDULE_HModel ob = new HR_SCHEDULE_HModel();

                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.HR_SHIFT_TYPE_ID = (dr["HR_SHIFT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_TYPE_ID"]);
                    ob.SCHEDULE_CODE = (dr["SCHEDULE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_CODE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.SCHEDULE_NAME_BN = (dr["SCHEDULE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_BN"]);
                    ob.SCHEDULE_DESC = (dr["SCHEDULE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_DESC"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_MULTI_GRP = (dr["IS_MULTI_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_GRP"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string IS_SCHE_ACTIVE { get; set; }
    }
}
