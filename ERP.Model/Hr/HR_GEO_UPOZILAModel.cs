using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HR_GEO_UPOZILAModel //: IHR_GEO_UPOZILAModel
    {
        public Int64 HR_GEO_UPOZILA_ID { get; set; }
        public Int64 HR_GEO_DISTRICT_ID { get; set; }
        public string UPOZILA_CODE { get; set; }
        public string UPOZILA_NAME_EN { get; set; }
        public string UPOZILA_NAME_BN { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public List<HR_GEO_UPOZILAModel> UpozilaListData(long? pHR_GEO_DISTRICT_ID)
        {
            string sp = "pkg_hr.hr_geo_upozila_select";
            try
            {
                var obList = new List<HR_GEO_UPOZILAModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_GEO_DISTRICT_ID", Value = pHR_GEO_DISTRICT_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_GEO_UPOZILAModel ob = new HR_GEO_UPOZILAModel();
                    ob.HR_GEO_UPOZILA_ID = (dr["HR_GEO_UPOZILA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GEO_UPOZILA_ID"]);
                    ob.HR_GEO_DISTRICT_ID = (dr["HR_GEO_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GEO_DISTRICT_ID"]);
                    ob.UPOZILA_CODE = (dr["UPOZILA_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UPOZILA_CODE"]);
                    ob.UPOZILA_NAME_EN = (dr["UPOZILA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UPOZILA_NAME_EN"]);
                    ob.UPOZILA_NAME_BN = (dr["UPOZILA_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UPOZILA_NAME_BN"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
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