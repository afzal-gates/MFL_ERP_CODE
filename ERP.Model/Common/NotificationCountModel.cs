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
    public class NotificationCountModel //: IHR_PAY_ELEMENTModel
    {
        public Int64 ALERT { get; set; }
        public Int64 MESSAGE { get; set; }
        public Int64 EVENT { get; set; }

        public NotificationCountModel getNotificationCount()
        {
            string sp = "pkg_common.rf_notification_select";

            try
            {
                var ob = new NotificationCountModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.EVENT = (dr["EVENT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EVENT"]);
                    ob.MESSAGE = (dr["MESSAGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MESSAGE"]);
                    ob.ALERT = (dr["ALERT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALERT"]);

                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

   
    }
}