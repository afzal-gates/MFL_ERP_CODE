using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data;

namespace ERP.Model
{
    public class HR_TA_RAW_DATAModel //: IHR_TA_RAW_DATAModel
    {
        public Int64 HR_TA_RAW_DATA_ID { get; set; }
        public Int64 HR_TA_DEVICE_CFG_ID { get; set; }
        public string TA_PROXI_ID { get; set; }
        public DateTime? PUNCH_DATE { get; set; }
        public DateTime? PUNCH_TIME { get; set; }
        public string P_YEAR { get; set; }
        public string P_DAY { get; set; }
        public string P_MONTH { get; set; }
        public string P_HOUR { get; set; }
        public string P_MIN { get; set; }
        public string P_FLAG { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }


        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string TA_DEVICE_CODE { get; set; }

        public string ShortMonthName(Int64 id)
        {
            string vShortMonthName = "";

            switch (id)
            {
                case 1:
                    vShortMonthName = "Jan";
                    break;
                case 2:
                    vShortMonthName = "Feb";
                    break;
                case 3:
                    vShortMonthName = "Mar";
                    break;
                case 4:
                    vShortMonthName = "Apr";
                    break;
                case 5:
                    vShortMonthName = "May";
                    break;
                case 6:
                    vShortMonthName = "Jun";
                    break;
                case 7:
                    vShortMonthName = "Jul";
                    break;
                case 8:
                    vShortMonthName = "Aug";
                    break;
                case 9:
                    vShortMonthName = "Sep";
                    break;
                case 10:
                    vShortMonthName = "Oct";
                    break;
                case 11:
                    vShortMonthName = "Nov";
                    break;
                case 12:
                    vShortMonthName = "Dec";
                    break;

                default:
                    vShortMonthName = "";
                    break;

            }

            return vShortMonthName;
        }

        public string Import(int pIMPORT_TYPE, string readFileName)
        {
            //const string sp = "pkg_hr.hr_employee_insert";            
            string vMsg = "";
            string strSQL = "";

            Int64 vID = 0;

            var ds = new DataSet();
            strSQL = "delete from HR_TA_RAW_DATA_TEMP";

            OraDatabase db = new OraDatabase();
            ds = db.ExecuteSQLStatement(strSQL);

            if (pIMPORT_TYPE == 99)
            {
                //string conns= WebConfigurationManager.ConnectionStrings("").;

                //OleDbConnection conn = new OleDbConnection();
                //string strConn = "Dsn=UNIS;uid=admin;pwd=unisamho"; //System.Configuration.ConfigurationSettings[;   //System.Configuration.ConfigurationSettings.AppSettings["myConnectionString"].ToString();
                //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + @"\punch_data\unis.mdb;Persist Security Info=false;User ID=admin;Password=unisamho";  //"Dsn=UNIS;dbq=D:\\PUNCH_DATA\\UNIS.mdb;driverid=25;fil=MS Access;maxbuffersize=2048;pagetimeout=5;uid=admin;pwd=unisamho";
                //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\\PUNCH_DATA\\UNIS.mdb;User ID=admin;Password=unisamho;Persist Security Info=false";

                //conn.ConnectionString = strConn; //' Configuration.s "Provider=MSDAORA;Data Source=iebms;Persist Security Info=True;User ID=rebstore;Password=rebstore;"
                //conn.Open();

                //Example for web config file
                string constr = ConfigurationManager.ConnectionStrings["UNISConnectionString"].ConnectionString;
                var cn = new OleDbConnection(constr);

                //OdbcConnection cn = new OdbcConnection("DSN=unis;uid=admin;pwd=unisamho;");

                try
                {
                    //OdbcTransaction trans;
                    //trans = conn.BeginTransaction();
                    var cm = new OleDbCommand("select distinct * from tEnter where L_Trans=0", cn);

                    cn.Open();
                    cm.ExecuteReader();
                    cn.Close();

                    var ds1 = new DataSet();
                    var adap = new OleDbDataAdapter(cm);
                    adap.Fill(ds1);
                    cm.Dispose();                    
                    adap.Dispose();

                    foreach (DataRow dr in ds1.Tables[0].Rows)
                    {
                        vID = vID + 1;
                        string x = vID.ToString("{00000}");// string.Format("{0,10:0}", "555");
                        x = dr["L_UID"].ToString();


                        string vYear = dr["C_Date"].ToString().Substring(0, 4);
                        string vMonth = dr["C_Date"].ToString().Substring(4, 2);
                        string vDay = dr["C_Date"].ToString().Substring(6, 2);

                        string vHour = dr["C_Time"].ToString().Substring(0, 2);
                        string vMinute = dr["C_Time"].ToString().Substring(2, 2);
                        string vSecond = dr["C_Time"].ToString().Substring(4, 2);


                        string vShortMonthName = ShortMonthName(Convert.ToInt64(dr["C_Date"].ToString().Substring(4, 2)));
                        string vPunchDate = vYear + "/" + vShortMonthName + "/" + vDay;
                        string vPunchTime = vYear + "/" + vShortMonthName + "/" + vDay + " " + vHour + ":" + vMinute + ":" + vSecond;

                        strSQL = "insert into HR_TA_RAW_DATA_TEMP ( " +
                        " HR_TA_DEVICE_CFG_ID,                                          TA_PROXI_ID,                                                    PUNCH_DATE, " +
                        " PUNCH_TIME,                                                   P_YEAR,                                                         P_DAY, " +
                        " P_MONTH,                                                      P_HOUR,                                                         P_MIN, " +
                        " P_FLAG,                                                       CREATED_BY,                                                     HR_TA_RAW_DATA_ID)" +
                        " values( " +
                        " '" + Convert.ToInt64(dr["L_TID"]) + "',                       '" + Convert.ToInt64(dr["L_UID"]).ToString("0000000000") + "',  to_date('" + vPunchDate + "','rrrr-Mon-dd'), " +
                        " to_timestamp('" + vPunchTime + "','rrrr-Mon-dd HH24:MI:SS'),  '" + vYear + "',                                                '" + vDay + "', " +
                        " '" + vMonth + "',                                             '" + vHour + "',                                                '" + vMinute + "', " +
                        " 'N',                                                          " + HttpContext.Current.Session["multiScUserId"] + ",           " + vID + ")";

                        ds = db.ExecuteSQLStatement(strSQL);
                    }
                    strSQL = "update tEnter set L_Trans=1 where L_Trans=0";
                    var cm1 = new OleDbCommand(strSQL, cn);

                    cn.Open();
                    cm1.ExecuteNonQuery(); //ExecuteReader();
                    cn.Close();
                    cm1.Dispose();

                    vMsg = "MULTI-001" + "Import successfully completed";
                }
                catch (Exception e)
                {
                    vMsg = "MULTI-005" + e.Message;
                }
            }
            else if (pIMPORT_TYPE == 98)
            {
                try
                {
                    //string[] lines = System.IO.File.ReadAllLines(@"D:\PUNCH_DATA\RTA_TEXT.txt");
                    string[] lines = System.IO.File.ReadAllLines(readFileName);

                    foreach (string line in lines)
                    {
                        vID = vID + 1;

                        string[] vResult = line.Split(',');  //vID.ToString("{00000}");// string.Format("{0,10:0}", "555");                

                        string vPROXI_ID = vResult[0].Substring(1, 10);

                        string vYear = vResult[1].Substring(1, 4);
                        string vMonth = vResult[1].Substring(5, 2);
                        string vDay = vResult[1].Substring(7, 2);

                        string vHour = vResult[2].Substring(1, 2);
                        string vMinute = vResult[2].Substring(3, 2);
                        string vSecond = vResult[2].Substring(5, 2);

                        //string[] vResult = line.Split(':');  //vID.ToString("{00000}");// string.Format("{0,10:0}", "555");                

                        //string vPROXI_ID = vResult[2];//.Substring(1, 10);

                        //string vYear = vResult[0].Substring(0, 2);
                        //string vMonth = vResult[0].Substring(2, 2);
                        //string vDay = vResult[0].Substring(4, 2);

                        //string vHour = vResult[1].Substring(0, 2);
                        //string vMinute = vResult[1].Substring(2, 2);
                        //string vSecond = vResult[1].Substring(4, 2);


                        string vShortMonthName = ShortMonthName(Convert.ToInt64(vMonth));
                        string vPunchDate = vYear + "/" + vShortMonthName + "/" + vDay;
                        string vPunchTime = vYear + "/" + vShortMonthName + "/" + vDay + " " + vHour + ":" + vMinute + ":" + vSecond;

                        strSQL = "insert into HR_TA_RAW_DATA_TEMP ( " +
                        " HR_TA_DEVICE_CFG_ID,                                          TA_PROXI_ID,                                                    PUNCH_DATE, " +
                        " PUNCH_TIME,                                                   P_YEAR,                                                         P_DAY, " +
                        " P_MONTH,                                                      P_HOUR,                                                         P_MIN, " +
                        " P_FLAG,                                                       CREATED_BY,                                                     HR_TA_RAW_DATA_ID)" +
                        " values( " +
                        " '" + Convert.ToInt64("1") + "',                              '" + vPROXI_ID + "',                                            to_date('" + vPunchDate + "','rrrr-Mon-dd'), " +
                        " to_timestamp('" + vPunchTime + "','rrrr-Mon-dd HH24:MI:SS'),  '" + vYear + "',                                                '" + vDay + "', " +
                        " '" + vMonth + "',                                             '" + vHour + "',                                                '" + vMinute + "', " +
                        " 'N',                                                          " + HttpContext.Current.Session["multiScUserId"] + ",           " + vID + ")";

                        ds = db.ExecuteSQLStatement(strSQL);

                    }
                    vMsg = "MULTI-001" + "Import successfully completed";
                }
                catch (Exception e)
                {
                    vMsg = "MULTI-005" + e.Message;
                }
            }
            else if (pIMPORT_TYPE == 3)
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(readFileName);

                    foreach (string line in lines)
                    {
                        vID = vID + 1;

                        string[] vResult = line.Split(' ');  //vID.ToString("{00000}");// string.Format("{0,10:0}", "555");                

                        string vPROXI_ID = string.Format("{0:0000000000}", Convert.ToInt64(vResult[0]));

                        string vYear = vResult[1].Substring(6, 4);
                        string vMonth = vResult[1].Substring(3, 2);
                        string vDay = vResult[1].Substring(0, 2);

                        string vHour = vResult[2].Substring(0, 2);
                        string vMinute = vResult[2].Substring(3, 2);
                        string vSecond = "00";


                        string vShortMonthName = ShortMonthName(Convert.ToInt64(vMonth));
                        string vPunchDate = vYear + "/" + vShortMonthName + "/" + vDay;
                        string vPunchTime = vYear + "/" + vShortMonthName + "/" + vDay + " " + vHour + ":" + vMinute + ":" + vSecond;

                        strSQL = "insert into HR_TA_RAW_DATA_TEMP ( " +
                        " HR_TA_DEVICE_CFG_ID,                                          TA_PROXI_ID,                                                    PUNCH_DATE, " +
                        " PUNCH_TIME,                                                   P_YEAR,                                                         P_DAY, " +
                        " P_MONTH,                                                      P_HOUR,                                                         P_MIN, " +
                        " P_FLAG,                                                       CREATED_BY,                                                     HR_TA_RAW_DATA_ID)" +
                        " values( " +
                        " '" + Convert.ToInt64("1") + "',                              '" + vPROXI_ID + "',                                            to_date('" + vPunchDate + "','rrrr-Mon-dd'), " +
                        " to_timestamp('" + vPunchTime + "','rrrr-Mon-dd HH24:MI:SS'),  '" + vYear + "',                                                '" + vDay + "', " +
                        " '" + vMonth + "',                                             '" + vHour + "',                                                '" + vMinute + "', " +
                        " 'N',                                                          " + HttpContext.Current.Session["multiScUserId"] + ",           " + vID + ")";

                        ds = db.ExecuteSQLStatement(strSQL);
                    }
                    vMsg = "MULTI-001" + "Import successfully completed";
                }
                catch (Exception e)
                {
                    vMsg = "MULTI-005" + e.Message;
                }
            }
            else if (pIMPORT_TYPE == 4)
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(readFileName);

                    foreach (string line in lines)
                    {
                        vID = vID + 1;

                        string[] vResult = line.Split(':');  //vID.ToString("{00000}");// string.Format("{0,10:0}", "555");                

                        string vPROXI_ID = vResult[2];

                        string vYear = vResult[0].Substring(0, 2);
                        string vMonth = vResult[0].Substring(2, 2);
                        string vDay = vResult[0].Substring(4, 2);

                        string vHour = vResult[1].Substring(0, 2);
                        string vMinute = vResult[1].Substring(2, 2);
                        string vSecond = vResult[1].Substring(4, 2); ;


                        string vShortMonthName = ShortMonthName(Convert.ToInt64(vMonth));
                        string vPunchDate = vYear + "/" + vShortMonthName + "/" + vDay;
                        string vPunchTime = vYear + "/" + vShortMonthName + "/" + vDay + " " + vHour + ":" + vMinute + ":" + vSecond;

                        strSQL = "insert into HR_TA_RAW_DATA_TEMP ( " +
                        " HR_TA_DEVICE_CFG_ID,                                          TA_PROXI_ID,                                                    PUNCH_DATE, " +
                        " PUNCH_TIME,                                                   P_YEAR,                                                         P_DAY, " +
                        " P_MONTH,                                                      P_HOUR,                                                         P_MIN, " +
                        " P_FLAG,                                                       CREATED_BY,                                                     HR_TA_RAW_DATA_ID)" +
                        " values( " +
                        " '" + Convert.ToInt64("1") + "',                              '" + vPROXI_ID + "',                                            to_date('" + vPunchDate + "','rrrr-Mon-dd'), " +
                        " to_timestamp('" + vPunchTime + "','rr-Mon-dd HH24:MI:SS'),    '" + vYear + "',                                                '" + vDay + "', " +
                        " '" + vMonth + "',                                             '" + vHour + "',                                                '" + vMinute + "', " +
                        " 'N',                                                          " + HttpContext.Current.Session["multiScUserId"] + ",           " + vID + ")";

                        ds = db.ExecuteSQLStatement(strSQL);

                    }
                    vMsg = "MULTI-001" + "Import successfully completed";
                }
                catch (Exception e)
                {
                    vMsg = "MULTI-005" + e.Message;
                }                
            }
            return vMsg;
        }

        public List<HR_TA_RAW_DATAModel> PunchListData()
        {
            string sp = "pkg_attendance.hr_ta_raw_data_select";
            try
            {
                var obList = new List<HR_TA_RAW_DATAModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_TA_RAW_DATAModel ob = new HR_TA_RAW_DATAModel();
                    ob.HR_TA_DEVICE_CFG_ID = (dr["HR_TA_DEVICE_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TA_DEVICE_CFG_ID"]);

                    if (dr["PUNCH_DATE"] != DBNull.Value)
                    { ob.PUNCH_DATE = Convert.ToDateTime(dr["PUNCH_DATE"]); }//(dr["PUNCH_DATE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PUNCH_DATE"]);

                    if (dr["PUNCH_TIME"] != DBNull.Value)
                    { ob.PUNCH_TIME = Convert.ToDateTime(dr["PUNCH_TIME"]); }// (dr["PUNCH_TIME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PUNCH_TIME"]);
                    ob.P_YEAR = (dr["P_YEAR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_YEAR"]);
                    ob.P_MONTH = (dr["P_MONTH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_MONTH"]);
                    ob.P_DAY = (dr["P_DAY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_DAY"]);
                    ob.P_HOUR = (dr["P_HOUR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_HOUR"]);
                    ob.P_MIN = (dr["P_MIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_MIN"]);
                    ob.P_FLAG = (dr["P_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_FLAG"]);
                    ob.TA_PROXI_ID = (dr["TA_PROXI_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_PROXI_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.TA_DEVICE_CODE = (dr["TA_DEVICE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_DEVICE_CODE"]);

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