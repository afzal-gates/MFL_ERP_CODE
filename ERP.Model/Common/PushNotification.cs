using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ERP.Model
{

    public class PushNotification
    {
        //static int counter = 0;
        //public PushNotification()
        //{
        //    Interlocked.Increment(ref counter);
        //}

        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string PUSH_REGI_ID { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

        public PushNotification PushToAndroidDevice(string registrationid, string title, string message)
        {
            PushNotification notification = new PushNotification();
            try
            {
                var applicationID = "AIzaSyDJkAn4ZsvWmHCSWx_NKvHLR6wbxYCI4tk";

                var SENDER_ID = "626787399256";

                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";

                tRequest.ContentType = "application/x-www-form-urlencoded";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + message + "&data.title=" + title + "&registration_id=" + registrationid + "&data.foreground=true";

                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                notification.Message = sResponseFromServer.Substring(0,5);
                tReader.Close();
                dataStream.Close();
                tResponse.Close();

                notification.Status = true;
            }
            catch (Exception ex)
            {
                notification.Status = false;
                notification.Message = "ERROR DESCRIPTION : " + ex.Message;
            }
            return notification;
        }

        public void PushNotificationCorn(){
            string sp = "pkg_leave.hr_leave_trans_select";
            String PushMessage=string.Empty;
            try
            {
                OraDatabase db = new OraDatabase();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3023}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = this ;
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.PUSH_REGI_ID = (dr["PUSH_REGI_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PUSH_REGI_ID"]);

                    PushMessage = "A leave request from " + ob.EMP_FULL_NAME_EN + ", " + ob.DESIGNATION_NAME_EN + " , " + ob.DEPARTMENT_NAME_EN;

                    if (ob.PUSH_REGI_ID != string.Empty)
                    {
                        this.PushToAndroidDevice(ob.PUSH_REGI_ID, "eLeave", PushMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
