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
    public class RF_NOTIFICATIONModel 
    {
        public Int64 RF_NOTIFICATION_ID { get; set; }
        public Int64 LK_NOTIF_TYPE_ID { get; set; }
        public string NOTIFICATION_TITLE { get; set; }
        public string NOTIFICATION_DESC_EN { get; set; }
        public Int64 NOTIFICATION_FOR_ID { get; set; }
        public string IS_REDIRECT { get; set; }
        public string REDIRECT_BTN_LBL { get; set; }
        public string REDIRECT_URL { get; set; }
        public string IS_AUTO_EXPIRE { get; set; }
        public DateTime EXPIRED_ON { get; set; }
        public string IS_REPEAT { get; set; }
        public Int64 REPEAT_DAY_AFTER_EXP { get; set; }
        public string COLOR_CODE { get; set; }
        public string IS_CLR_BY_USER { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_SEEN { get; set; }
        public string IS_STICKY { get; set; }

        public List<RF_NOTIFICATIONModel> getMessageData()
        {
            string sp = "pkg_common.rf_notification_select";

            try
            {
                var obList = new List<RF_NOTIFICATIONModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pLK_NOTIF_TYPE_ID", Value = 182},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new RF_NOTIFICATIONModel();
                    ob.RF_NOTIFICATION_ID = (dr["RF_NOTIFICATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_NOTIFICATION_ID"]);
                    ob.LK_NOTIF_TYPE_ID = (dr["LK_NOTIF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NOTIF_TYPE_ID"]);
                    ob.NOTIFICATION_TITLE = (dr["NOTIFICATION_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTIFICATION_TITLE"]);
                    ob.NOTIFICATION_DESC_EN = (dr["NOTIFICATION_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTIFICATION_DESC_EN"]);
                    ob.IS_REDIRECT = (dr["IS_REDIRECT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REDIRECT"]);
                    ob.REDIRECT_BTN_LBL = (dr["REDIRECT_BTN_LBL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REDIRECT_BTN_LBL"]);
                    ob.REDIRECT_URL = (dr["REDIRECT_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REDIRECT_URL"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["CREATION_DATE"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_NOTIFICATIONModel> getAlertData()
        {
            string sp = "pkg_common.rf_notification_select";

            try
            {
                var obList = new List<RF_NOTIFICATIONModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pLK_NOTIF_TYPE_ID", Value = 181},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_NOTIFICATIONModel ob = new RF_NOTIFICATIONModel();
                    ob.RF_NOTIFICATION_ID = (dr["RF_NOTIFICATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_NOTIFICATION_ID"]);
                    ob.LK_NOTIF_TYPE_ID = (dr["LK_NOTIF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NOTIF_TYPE_ID"]);
                    ob.NOTIFICATION_TITLE = (dr["NOTIFICATION_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTIFICATION_TITLE"]);
                    ob.NOTIFICATION_DESC_EN = (dr["NOTIFICATION_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTIFICATION_DESC_EN"]);
                    ob.IS_REDIRECT = (dr["IS_REDIRECT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REDIRECT"]);
                    ob.REDIRECT_BTN_LBL = (dr["REDIRECT_BTN_LBL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REDIRECT_BTN_LBL"]);
                    ob.REDIRECT_URL = (dr["REDIRECT_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REDIRECT_URL"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["CREATION_DATE"]);
                    obList.Add(ob);

                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_NOTIFICATIONModel> getEventData()
        {
            string sp = "pkg_common.rf_notification_select";
            try
            {
                var obList = new List<RF_NOTIFICATIONModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                { 
                    new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pLK_NOTIF_TYPE_ID", Value = 183},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_NOTIFICATIONModel ob = new RF_NOTIFICATIONModel();
                    ob.RF_NOTIFICATION_ID = (dr["RF_NOTIFICATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_NOTIFICATION_ID"]);
                    ob.LK_NOTIF_TYPE_ID = (dr["LK_NOTIF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NOTIF_TYPE_ID"]);
                    ob.NOTIFICATION_TITLE = (dr["NOTIFICATION_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTIFICATION_TITLE"]);
                    ob.NOTIFICATION_DESC_EN = (dr["NOTIFICATION_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTIFICATION_DESC_EN"]);
                    ob.IS_REDIRECT = (dr["IS_REDIRECT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REDIRECT"]);
                    ob.REDIRECT_BTN_LBL = (dr["REDIRECT_BTN_LBL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REDIRECT_BTN_LBL"]);
                    ob.REDIRECT_URL = (dr["REDIRECT_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REDIRECT_URL"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["CREATION_DATE"]);
                    obList.Add(ob);

                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string seenNotification(Int64 RF_NOTIFICATION_ID)
        {
            const string sp = "pkg_common.rf_notification_update";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_NOTIFICATION_ID", Value = RF_NOTIFICATION_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vMsg;

        }
        public RF_NOTIFICATIONModel getNotification()
        {
            string sp = "pkg_common.rf_notification_select";
            try
            {
                var ob = this;

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_NOTIFICATION_ID = (dr["RF_NOTIFICATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_NOTIFICATION_ID"]);
                    ob.NOTIFICATION_TITLE = (dr["NOTIFICATION_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTIFICATION_TITLE"]);
                    ob.NOTIFICATION_DESC_EN = (dr["NOTIFICATION_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTIFICATION_DESC_EN"]);
                    ob.COLOR_CODE = (dr["COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_CODE"]);
                    ob.IS_CLR_BY_USER = (dr["IS_CLR_BY_USER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLR_BY_USER"]);
                    ob.IS_STICKY = (dr["IS_STICKY"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_STICKY"]);
                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string seenNotificationByList(string RF_NOTIFICATION_ID_LIST)
        {
            const string sp = "pkg_common.rf_notification_update";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_NOTIFICATION_ID_LIST", Value = RF_NOTIFICATION_ID_LIST},
                    new CommandParameter() {ParameterName = "pOption", Value = 2001},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vMsg;
        }
    }
}