using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HR_OT_TYPEModel //: IHR_OT_TYPEModel
    {
        public Int64 HR_OT_TYPE_ID { get; set; }
        public string OT_TYPE_CODE { get; set; }
        public string OT_TYPE_NAME_EN { get; set; }
        public string OT_TYPE_NAME_BN { get; set; }
        public Int64 HR_DAY_TYPE_ID { get; set; }
        public string IS_OTH_BILL { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public List<HR_OT_TYPEModel> OtTypeListData()
        {
            string sp = "pkg_ot.hr_ot_type_select";
            try
            {
                var obList = new List<HR_OT_TYPEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_OT_TYPEModel ob = new HR_OT_TYPEModel();
                    ob.HR_OT_TYPE_ID = (dr["HR_OT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OT_TYPE_ID"]);
                    ob.OT_TYPE_CODE = (dr["OT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_TYPE_CODE"]);
                    ob.OT_TYPE_NAME_EN = (dr["OT_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_TYPE_NAME_EN"]);
                    ob.OT_TYPE_NAME_BN = (dr["OT_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OT_TYPE_NAME_BN"]);
                    ob.HR_DAY_TYPE_ID = (dr["HR_DAY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DAY_TYPE_ID"]);
                    ob.IS_OTH_BILL = (dr["IS_OTH_BILL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_OTH_BILL"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
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