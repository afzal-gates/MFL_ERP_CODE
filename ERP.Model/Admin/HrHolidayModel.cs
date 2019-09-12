using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class HrHolidayModel //: IHrHolidayModel
    {
        public Int64 HR_HOLIDAY_ID { get; set; }
        public Int64 HR_DAY_TYPE_ID { get; set; }
        public string HOLIDAY_NAME_EN { get; set; }
        public string HOLIDAY_NAME_BN { get; set; }
        public string HOLIDAY_DESC { get; set; }
        public DateTime MONTH_DAY { get; set; }
        public string IS_COMMON { get; set; }
        public Int64 LK_HOLIDAY_TYPE_ID { get; set; }
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }        
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }

        public List<HrHolidayModel> HolidayList()
        {
            try
            {
                var obList = new List<HrHolidayModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from HR_HOLIDAY order by HR_HOLIDAY_ID");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrHolidayModel ob = new HrHolidayModel();

                    ob.HR_HOLIDAY_ID = (dr["HR_HOLIDAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_HOLIDAY_ID"]);
                    ob.HOLIDAY_NAME_EN = (dr["HOLIDAY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOLIDAY_NAME_EN"]);
                    ob.HOLIDAY_NAME_BN = (dr["HOLIDAY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOLIDAY_NAME_BN"]);
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
