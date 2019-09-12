using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrTimezoneModel //: IHrCompanyModel
    {
        public Int64 HR_TIMEZONE_ID { get; set; }
        public Int64 TIMEZONE_VALUE { get; set; }

        [Required]
        public string TIMEZONE_TEXT { get; set; }
        

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }

        public List<HrTimezoneModel> TimezoneListData()
        {
            string sp = "pkg_hr.hr_timezone_select";
            try
            {
                var obList = new List<HrTimezoneModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3000},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrTimezoneModel ob = new HrTimezoneModel();
                    ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                    ob.TIMEZONE_VALUE = (dr["TIMEZONE_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TIMEZONE_VALUE"]);
                    ob.TIMEZONE_TEXT = (dr["TIMEZONE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIMEZONE_TEXT"]);

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
