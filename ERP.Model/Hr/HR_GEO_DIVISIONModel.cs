using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HR_GEO_DIVISIONModel //: IHR_GEO_DIVISIONModel
    {
        public Int64 HR_GEO_DIVISION_ID { get; set; }
        public string DIVISION_CODE { get; set; }
        public string DIVISION_NAME_EN { get; set; }
        public string DIVISION_NAME_BN { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public List<HR_GEO_DIVISIONModel> SelectAll()
        {
            string sp = "pkg_hr.hr_geo_division_select";
            try
            {
                var obList = new List<HR_GEO_DIVISIONModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3000},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_GEO_DIVISIONModel ob = new HR_GEO_DIVISIONModel();
                    ob.HR_GEO_DIVISION_ID = (dr["HR_GEO_DIVISION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GEO_DIVISION_ID"]);
                    ob.DIVISION_CODE = (dr["DIVISION_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIVISION_CODE"]);
                    ob.DIVISION_NAME_EN = (dr["DIVISION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIVISION_NAME_EN"]);
                    ob.DIVISION_NAME_BN = (dr["DIVISION_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIVISION_NAME_BN"]);
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