using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.IO;

namespace ERP.Model
{
    public class SMS_BROADCAST2EMPModel
    {
        public Int32? SMS_TYPE_ID { get; set; }
        public Int32? PARAM_SPEC_TYPE_ID { get; set; }
        public string MOB_NO_PRS { get; set; }
        public string SMS_TEXT { get; set; }
        public string HR_COMPANY_ID_LST { get; set; }
        public string HR_OFFICE_ID_LST { get; set; }
        public string HR_DEPARTMENT_ID_LST { get; set; }
        public string HR_SHIFT_TEAM_ID_LST { get; set; }
        public string LK_FLOOR_ID_LST { get; set; }
        public string LINE_NO_LST { get; set; }
        public string HR_MANAGEMENT_TYPE_ID_LST { get; set; }
        public string HR_EMPLOYEE_ID_LST { get; set; }
        public Int32? HR_COMPANY_ID { get; set; }
        public Int32? ACC_PAY_PERIOD_ID { get; set; }


       
        public string SendSms()
        {
            Int32 vOption = 3000;

            const string sp = "pkg_hr.sms_broadcust2emp_select";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                string savedFileName = HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/sms.txt"); // Path.Combine(Server.MapPath("~/UPLOAD_DOCS/PUNCH_DATA"), Path.GetFileName("ATTEN_FILE.txt"));

                if (ob.SMS_TYPE_ID == 2)
                {
                    vOption = 3001;
                }                

                var obList = new List<SMS_BROADCAST2EMPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSMS_TYPE_ID", Value = ob.SMS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPARAM_SPEC_TYPE_ID", Value = ob.PARAM_SPEC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSMS_TEXT", Value = ob.SMS_TEXT},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID_LST", Value = ob.HR_COMPANY_ID_LST},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID_LST", Value = ob.HR_OFFICE_ID_LST},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID_LST", Value = ob.HR_DEPARTMENT_ID_LST},
                     new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID_LST", Value = ob.HR_SHIFT_TEAM_ID_LST},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID_LST", Value = ob.LK_FLOOR_ID_LST},
                     new CommandParameter() {ParameterName = "pLINE_NO_LST", Value = ob.LINE_NO_LST},
                     new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID_LST", Value = ob.HR_MANAGEMENT_TYPE_ID_LST},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID_LST", Value = ob.HR_EMPLOYEE_ID_LST},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = vOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                using (System.IO.StreamWriter file = new System.IO.StreamWriter(savedFileName))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string line = "";

                        if (ob.SMS_TYPE_ID == 1) // For Bulk
                            line = Convert.ToString(dr["MOB_NO_PRS"]);
                        else if (ob.SMS_TYPE_ID != 1) // For Individual
                            line = Convert.ToString(dr["MOB_NO_PRS"]) + "|" + Convert.ToString(dr["SMS_TEXT"]);

                        file.WriteLine(line);
                    }

                }
               
                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');

                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_COUNTRYModel Select(long ID)
        {
            string sp = "Select_HR_COUNTRY";
            try
            {
                var ob = new HR_COUNTRYModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.COUNTRY_CODE = (dr["COUNTRY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_CODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.COUNTRY_NAME_BN = (dr["COUNTRY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_BN"]);
                    ob.LK_REGION_ID = (dr["LK_REGION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REGION_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.COUNTRY_SNAME = (dr["COUNTRY_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_SNAME"]);

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