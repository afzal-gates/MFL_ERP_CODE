using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class ScSecurityLevelModel //: IHrCompany
    {
        public Int64 SC_SECURITY_LEVEL_ID { get; set; }
        public string SCR_LEVEL_NAME { get; set; }
        public Int64 NOTIFICATION_MODE { get; set; }
        public Int64 AUTHENTICATION_MODE { get; set; }        
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }        
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }

        public List<ScSecurityLevelModel> ScSecurityLevelData()
        {
            string sp = "pkg_security.sc_system_select";
            try
            {
                var obList = new List<ScSecurityLevelModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScSecurityLevelModel ob = new ScSecurityLevelModel();
                    ob.SC_SECURITY_LEVEL_ID = (dr["SC_SECURITY_LEVEL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_SECURITY_LEVEL_ID"]);
                    ob.SCR_LEVEL_NAME = (dr["SCR_LEVEL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCR_LEVEL_NAME"]);
                    ob.NOTIFICATION_MODE = (dr["NOTIFICATION_MODE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NOTIFICATION_MODE"]);
                    ob.AUTHENTICATION_MODE = (dr["AUTHENTICATION_MODE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AUTHENTICATION_MODE"]);
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
