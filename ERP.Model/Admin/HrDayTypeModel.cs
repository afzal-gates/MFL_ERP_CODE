using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class HrDayTypeModel //: IHrDayTypeModel
    {
        public Int64 HR_DAY_TYPE_ID { get; set; }
        public string DAY_TYPE_NAME_EN { get; set; }
        public string DAY_TYPE_NAME_BN { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        //public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        //public int LAST_UPDATED_BY { get; set; }        
        //public int LAST_UPDATE_LOGIN { get; set; }
        //public int VERSION_NO { get; set; }

        public List<HrDayTypeModel> DayTypeList()
        {
            try
            {
                var obList = new List<HrDayTypeModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from HR_DAY_TYPE order by HR_DAY_TYPE_ID");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrDayTypeModel ob = new HrDayTypeModel();

                    ob.HR_DAY_TYPE_ID = (dr["HR_DAY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DAY_TYPE_ID"]);
                    ob.DAY_TYPE_NAME_EN = (dr["DAY_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DAY_TYPE_NAME_EN"]);
                    ob.DAY_TYPE_NAME_BN = (dr["DAY_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DAY_TYPE_NAME_BN"]);
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
