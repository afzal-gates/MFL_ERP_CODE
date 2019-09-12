using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrCompanyGrpModel
    {
        public Int64 HR_COMPANY_GRP_ID { get; set; }
        public string COMP_GRP_CODE { get; set; }

        [Required]
        public string COMP_GRP_NAME_EN { get; set; }
        [Required]
        public string COMP_GRP_NAME_BN { get; set; }

        public string COMP_GRP_DESC { get; set; }

        public string IS_MULTI_COMPANY { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }        
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }

        public List<HrCompanyGrpModel> CompanyGroupListData()
        {
            string sp = "pkg_hr.hr_company_grp_select";
            try
            {
                var obList = new List<HrCompanyGrpModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrCompanyGrpModel ob = new HrCompanyGrpModel();
                    ob.HR_COMPANY_GRP_ID = (dr["HR_COMPANY_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_GRP_ID"]);
                    ob.COMP_GRP_CODE = (dr["COMP_GRP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_GRP_CODE"]);
                    ob.COMP_GRP_NAME_EN = (dr["COMP_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_GRP_NAME_EN"]);
                    ob.COMP_GRP_NAME_BN = (dr["COMP_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_GRP_NAME_BN"]);
                    ob.COMP_GRP_DESC = (dr["COMP_GRP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_GRP_DESC"]);
                    ob.IS_MULTI_COMPANY = (dr["IS_MULTI_COMPANY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_COMPANY"]);
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
