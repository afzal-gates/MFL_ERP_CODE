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
    public class HR_EMP_NOMINEEModel
    {
        public Int64 HR_EMP_NOMINEE_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public string NOM_NAME_EN { get; set; }
        public string NOM_NAME_BN { get; set; }
        public Int64 LK_RELATION_ID { get; set; }
        public string NOM_ADDRESS_EN { get; set; }
        public string NOM_ADDRESS_BN { get; set; }
        public string NOM_MOB_NO { get; set; }
        public Decimal PCT_SHARE { get; set; }
        public string NOM_IMG { get; set; }
        public string RELATION_WITH_NOM { get; set; }

        


        public List<HR_EMP_NOMINEEModel> GetGmtNomineeList(long pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_hr.hr_employee_select";
            try
            {
                var obList = new List<HR_EMP_NOMINEEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMP_NOMINEEModel ob = new HR_EMP_NOMINEEModel();
                    ob.HR_EMP_NOMINEE_ID = (dr["HR_EMP_NOMINEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMP_NOMINEE_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.NOM_NAME_EN = (dr["NOM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOM_NAME_EN"]);
                    ob.NOM_NAME_BN = (dr["NOM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOM_NAME_BN"]);
                    ob.LK_RELATION_ID = (dr["LK_RELATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RELATION_ID"]);
                    ob.NOM_ADDRESS_EN = (dr["NOM_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOM_ADDRESS_EN"]);
                    ob.NOM_ADDRESS_BN = (dr["NOM_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOM_ADDRESS_BN"]);
                    ob.NOM_MOB_NO = (dr["NOM_MOB_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOM_MOB_NO"]);
                    ob.PCT_SHARE = (dr["PCT_SHARE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_SHARE"]);
                    ob.NOM_IMG = (dr["NOM_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOM_IMG"]);

                    ob.RELATION_WITH_NOM = (dr["RELATION_WITH_NOM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RELATION_WITH_NOM"]);

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