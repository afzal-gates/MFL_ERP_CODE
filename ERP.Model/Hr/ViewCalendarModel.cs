using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ERP.Model
{
    public class ViewCalendarModel
    {
        public Int64 TaskId { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public Int64 OwnerID { get; set; }
        public Boolean IsAllDay { get; set; }

        public List<ViewCalendarModel> SelectViewCalender(Int64 pHR_COMPANY_ID, Int64 pRF_FISCAL_YEAR_ID)
        {
            string sp = "pkg_hr.hr_yrly_calndr_select";
            try
            {
                var obList = new List<ViewCalendarModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = pRF_FISCAL_YEAR_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3001}                    
                    //new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ViewCalendarModel ob = new ViewCalendarModel();

                    ob.TaskId = Convert.ToInt64(dr[0]); //Convert.ToInt64(dr["HR_YRLY_CALNDR_ID"]);
                    ob.Title = dr["Title"].ToString();
                    ob.Start = Convert.ToDateTime(dr["Start"]);
                    ob.End = Convert.ToDateTime(dr["End"]);
                    ob.Description = dr["Description"].ToString();
                    ob.OwnerID = Convert.ToInt64(dr["OwnerID"]);
                    ob.IsAllDay = true;

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