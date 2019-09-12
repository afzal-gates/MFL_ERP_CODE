using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Model.Common;
using System.Data;
using System.Web;
//using ERPSolution.Common;


namespace ERP.Model
{
    public class HrYrlyCalndrModel //: IHrYrlyCalndrModel
    {
        public Int64 HR_YRLY_CALNDR_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 RF_FISCAL_YEAR_ID { get; set; }

        [Required(ErrorMessage="Please select day type")]
        public Int64? HR_DAY_TYPE_ID { get; set; }
        public Int64? HR_HOLIDAY_ID { get; set; }
        public DateTime CALNDR_DATE { get; set; }
        public string REMARKS { get; set; }
        //public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }


        public string DAY_TYPE_NAME_EN { get; set; }
        public string HOLIDAY_NAME_EN { get; set; }

        

        [Required(ErrorMessage = "Please select from date")]
        [DateRange(ErrorMessage = "Form date should be grater than or equal to current date")]
        public DateTime? FROM_DATE { get; set; }
        [Required(ErrorMessage = "Please select to date")]
        public DateTime? TO_DATE { get; set; }

        public string IS_GOVT_HOLIDAY { get; set; }

        public string Update()
        {
            const string sp = "pkg_hr.hr_yrly_calndr_update";            
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_YRLY_CALNDR_ID", Value = ob.HR_YRLY_CALNDR_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.HR_DAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_HOLIDAY_ID", Value = ob.HR_HOLIDAY_ID},
                    new CommandParameter() {ParameterName = "pCALNDR_DATE", Value = ob.CALNDR_DATE},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},

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
                vMsg = ex.ToString();
            }
            return vMsg;
        }

        public List<HrYrlyCalndrModel> SelectYrCalender(DateTime? showMonth)
        {
            string sp = "pkg_hr.hr_yrly_calndr_select";
            try
            {
                var obList = new List<HrYrlyCalndrModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = 1},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = 1},
                    new CommandParameter() {ParameterName = "pShowMonth", Value = showMonth},
		            new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrYrlyCalndrModel ob = new HrYrlyCalndrModel();

                    ob.HR_YRLY_CALNDR_ID = (dr["HR_YRLY_CALNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YRLY_CALNDR_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.HR_DAY_TYPE_ID = (dr["HR_DAY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DAY_TYPE_ID"]);
                    ob.HR_HOLIDAY_ID = (dr["HR_HOLIDAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_HOLIDAY_ID"]);
                    ob.CALNDR_DATE = Convert.ToDateTime(dr["CALNDR_DATE"]); //== DBNull.Value) ? DateTime.db : Convert.ToString(dr["CALNDR_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.DAY_TYPE_NAME_EN = (dr["DAY_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DAY_TYPE_NAME_EN"]);
                    ob.HOLIDAY_NAME_EN = (dr["HOLIDAY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOLIDAY_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string HolidayCalendarProcess(Int64 pHR_COMPANY_ID, Int64 pRF_FISCAL_YEAR_ID)
        {
            const string sp = "pkg_hr.hr_yrly_calndr_process";            
            string vMsg = "";
            
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = pRF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                    //new CommandParameter() {ParameterName = "pOption", Value = 1000},
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

        public string UpdateDateRange()
        {
            const string sp = "pkg_hr.hr_yrly_calndr_update_range";            
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTo_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.HR_DAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_HOLIDAY_ID", Value = ob.HR_HOLIDAY_ID},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                    //new CommandParameter() {ParameterName = "pOption", Value = 1000},
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



        public string getNoOfWorkingDay(int pHR_COMPANY_ID,DateTime? pFROM_DT, DateTime? pTO_DT)
        {
            const string sp = "pkg_hr.get_no_of_working_day";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pFROM_DT", Value = pFROM_DT.Value.Date},
                     new CommandParameter() {ParameterName = "pTO_DT", Value = pTO_DT.Value.Date},

                     new CommandParameter() {ParameterName = "V_NO_OF_DAYS", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_NO_OF_HOLI_DAYS", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     
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
