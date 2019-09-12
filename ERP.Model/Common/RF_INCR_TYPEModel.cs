using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class RF_INCR_TYPEModel
    {
        public Int64 RF_INCR_TYPE_ID { get; set; }
        public string INCR_TYPE_CODE { get; set; }
        public string INCR_TYPE_NAME_EN { get; set; }
        public string INCR_TYPE_NAME_BN { get; set; }
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        



        public List<RF_INCR_TYPEModel> GetIncrimentType()
        {
            string sp = "pkg_common.rf_incr_type_select";
            try
            {
                var obList = new List<RF_INCR_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_INCR_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                    RF_INCR_TYPEModel ob = new RF_INCR_TYPEModel();
                    ob.RF_INCR_TYPE_ID = (dr["RF_INCR_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCR_TYPE_ID"]);
                    ob.INCR_TYPE_CODE = (dr["INCR_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_TYPE_CODE"]);
                    ob.INCR_TYPE_NAME_EN = (dr["INCR_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_TYPE_NAME_EN"]);
                    ob.INCR_TYPE_NAME_BN = (dr["INCR_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_TYPE_NAME_BN"]);
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
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