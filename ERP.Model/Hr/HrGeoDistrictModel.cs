using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrGeoDistrictModel //: IHrCompanyModel
    {
        public Int64 HR_GEO_DISTRICT_ID { get; set; }        
        public string DISTRICT_CODE { get; set; }


        [Required]
        public Int64 HR_GEO_DIVISION_ID { get; set; }
        [Required]
        public string DISTRICT_NAME_EN { get; set; }
        [Required]
        public string DISTRICT_NAME_BN { get; set; }
        

        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }

        public List<HrGeoDistrictModel> DistrictListData(Int64? pHR_GEO_DIVISION_ID)
        {

            string sp = "pkg_hr.hr_geo_district_select";
            try
            {
                var obList = new List<HrGeoDistrictModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pHR_GEO_DIVISION_ID", Value = pHR_GEO_DIVISION_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3002},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrGeoDistrictModel ob = new HrGeoDistrictModel();
                    ob.HR_GEO_DISTRICT_ID = (dr["HR_GEO_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GEO_DISTRICT_ID"]);
                    ob.HR_GEO_DIVISION_ID = (dr["HR_GEO_DIVISION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GEO_DIVISION_ID"]);
                    ob.DISTRICT_CODE = (dr["DISTRICT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DISTRICT_CODE"]);
                    ob.DISTRICT_NAME_EN = (dr["DISTRICT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DISTRICT_NAME_EN"]);
                    ob.DISTRICT_NAME_BN = (dr["DISTRICT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DISTRICT_NAME_BN"]);
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
