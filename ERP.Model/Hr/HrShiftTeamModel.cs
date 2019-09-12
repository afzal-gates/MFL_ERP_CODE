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
    public class HrShiftTeamModel //: IHrCompanyModel
    {
        public Int64 HR_SHIFT_TEAM_ID { get; set; }
        public string SHIFT_TEAM_CODE { get; set; }


        [Required]
        public Int64 HR_SHIFT_PLAN_ID { get; set; }
        [Required]
        public Int64 HR_COMPANY_ID { get; set; }
        [Required]
        public string SHIFT_TEAM_NAME_EN { get; set; }
        [Required]
        public string SHIFT_TEAM_NAME_BN { get; set; }
        [Required(ErrorMessage="Please input roll sequence#.")]
        public Int64 ROLL_SEQ_NO { get; set; }
        [Required]
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }        
        public Int64 HR_SCHEDULE_ASSIGN_ID { get; set; }

        public List<HrShiftTeamModel> ShiftTeamListData(Int64? pHR_SHIFT_PLAN_ID)
        {
            string sp = "pkg_hr.hr_shift_team_select";
            try
            {
                var obList = new List<HrShiftTeamModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = (pHR_SHIFT_PLAN_ID != null)?pHR_SHIFT_PLAN_ID:null},
		            new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrShiftTeamModel ob = new HrShiftTeamModel();
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

        public string Save()
        {
            const string sp = "pkg_hr.hr_shift_team_save";
            
            string jsonStr = "{";
            string vMsg = "";
            var ob = this;
            var i = 1;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = ob.HR_SHIFT_PLAN_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pSHIFT_TEAM_CODE", Value = ob.SHIFT_TEAM_CODE},
                    new CommandParameter() {ParameterName = "pSHIFT_TEAM_NAME_EN", Value = ob.SHIFT_TEAM_NAME_EN},
                    new CommandParameter() {ParameterName = "pSHIFT_TEAM_NAME_BN", Value = ob.SHIFT_TEAM_NAME_BN},
                    new CommandParameter() {ParameterName = "pROLL_SEQ_NO", Value = ob.ROLL_SEQ_NO},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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

        public string Update()
        {
            const string sp = "pkg_hr.hr_shift_team_update";            
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_PLAN_ID", Value = ob.HR_SHIFT_PLAN_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pSHIFT_TEAM_CODE", Value = ob.SHIFT_TEAM_CODE},
                    new CommandParameter() {ParameterName = "pSHIFT_TEAM_NAME_EN", Value = ob.SHIFT_TEAM_NAME_EN},
                    new CommandParameter() {ParameterName = "pSHIFT_TEAM_NAME_BN", Value = ob.SHIFT_TEAM_NAME_BN},
                    new CommandParameter() {ParameterName = "pROLL_SEQ_NO", Value = ob.ROLL_SEQ_NO},
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

        public List<HrShiftTeamModel> SelectAll()
        {
            string sp = "pkg_hr.hr_shift_team_select";
            try
            {
                var obList = new List<HrShiftTeamModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3000},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrShiftTeamModel ob = new HrShiftTeamModel();

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
}
