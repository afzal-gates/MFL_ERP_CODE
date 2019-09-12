using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrCountryModel //: IHrCompanyModel
    {
        public Int64 HR_COUNTRY_ID { get; set; }
        public string COUNTRY_CODE { get; set; }
               
        [Required]
        public string COUNTRY_NAME_EN { get; set; }
        [Required]
        public string COUNTRY_NAME_BN { get; set; }
        [Required]
        public Int64 LK_REGION_ID { get; set; }


        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }

        public List<HrCountryModel> CountryListData()
        {
            string sp = "pkg_hr.hr_country_select";

            try
            {
                var obList = new List<HrCountryModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrCountryModel ob = new HrCountryModel();
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.COUNTRY_CODE = (dr["COUNTRY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_CODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.COUNTRY_NAME_BN = (dr["COUNTRY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_BN"]);
                    ob.LK_REGION_ID = (dr["LK_REGION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REGION_ID"]);
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
