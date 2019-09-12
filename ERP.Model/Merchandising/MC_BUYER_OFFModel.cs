using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.ComponentModel;

namespace ERP.Model
{
    public class MC_BUYER_OFFModel
    {
        public Int64 MC_BUYER_OFF_ID { get; set; }

        [DisplayName("Buyer Office Name[E]")]
        [Required(ErrorMessage = " The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string BOFF_NAME_EN { get; set; }

        [DisplayName("Buyer Office Name[B]")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string BOFF_NAME_BN { get; set; }

        [DisplayName("Office Type")]
        [Required(ErrorMessage = " The field {0} need to be selected")]
        public Int64 LK_BOFF_TYPE_ID { get; set; }

        [DisplayName("Address [E]")]
        [Required(ErrorMessage = " The field {0} is required")]
        [MaxLength(350, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string ADDRESS_EN { get; set; }
        public string ADDRESS_BN { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = " The field {0} need to be selected")]
        public Int64 HR_COUNTRY_ID { get; set; }
        public string POST_CODE { get; set; }
        public string PO_BOX_NO { get; set; }
        public Int64 HR_TIMEZONE_ID { get; set; }
        public string WORK_PHONE { get; set; }
        public string MOB_PHONE { get; set; }
        public string FAX_NO { get; set; }

        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress,ErrorMessage="The field {0} is not a valid email address")]
        public string EMAIL_ID { get; set; }

        [DisplayName("Website")]
        [DataType(DataType.Url, ErrorMessage = "The field {0} is not a valid website address")]
        public string WEB_URL { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string COUNTRY_NAME_EN { get; set; }
        public string LK_BOFF_TYPE { get; set; }

        public string Save()
        {
            const string sp = "pkg_merchandising.mc_buyer_off_insert";
            string jsonStr = "{";
            var ob = this;
            Int64 i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = ob.MC_BUYER_OFF_ID},
                     new CommandParameter() {ParameterName = "pBOFF_NAME_EN", Value = ob.BOFF_NAME_EN},
                     new CommandParameter() {ParameterName = "pBOFF_NAME_BN", Value = ob.BOFF_NAME_BN},
                     new CommandParameter() {ParameterName = "pLK_BOFF_TYPE_ID", Value = ob.LK_BOFF_TYPE_ID},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value = ob.HR_TIMEZONE_ID},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE==null? "Y" : ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_BUYER_OFF_ID", Value =null, Direction = ParameterDirection.Output}
                 }, sp);
               
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

        public string Update()
        {
            const string sp = "pkg_merchandising.mc_buyer_off_update";
            string jsonStr = "{";
            var ob = this;
            Int64 i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = ob.MC_BUYER_OFF_ID},
                     new CommandParameter() {ParameterName = "pBOFF_NAME_EN", Value = ob.BOFF_NAME_EN},
                     new CommandParameter() {ParameterName = "pBOFF_NAME_BN", Value = ob.BOFF_NAME_BN},
                     new CommandParameter() {ParameterName = "pLK_BOFF_TYPE_ID", Value = ob.LK_BOFF_TYPE_ID},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value = ob.HR_TIMEZONE_ID},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE==null? "Y" : ob.IS_ACTIVE },
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
           
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

        public string Delete()
        {
            const string sp = "pkg_merchandising.mc_buyer_off_delete";
            string vMsg = "";
            var ob = this;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = ob.MC_BUYER_OFF_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                 string jsonString = "{";
                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonString += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
                 }
                    jsonString+="}";
                 return jsonString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_BUYER_OFFModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_buyer_off_select";
            try
            {
                var obList = new List<MC_BUYER_OFFModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_BUYER_OFFModel ob = new MC_BUYER_OFFModel();
                            ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                            ob.BOFF_NAME_EN = (dr["BOFF_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOFF_NAME_EN"]);
                            ob.BOFF_NAME_BN = (dr["BOFF_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOFF_NAME_BN"]);
                            ob.LK_BOFF_TYPE_ID = (dr["LK_BOFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BOFF_TYPE_ID"]);
                            ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                            ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                            ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                            ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                            ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                            ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                            ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                            ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                            ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                            ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                            ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                            ob.LK_BOFF_TYPE = (dr["LK_BOFF_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_BOFF_TYPE"]);
                            ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BUYER_OFFModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_buyer_off_select";
            try
            {
                var ob = new MC_BUYER_OFFModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                        ob.BOFF_NAME_EN = (dr["BOFF_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOFF_NAME_EN"]);
                        ob.BOFF_NAME_BN = (dr["BOFF_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOFF_NAME_BN"]);
                        ob.LK_BOFF_TYPE_ID = (dr["LK_BOFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BOFF_TYPE_ID"]);
                        ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                        ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                        ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                        ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                        ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                        ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                        ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                        ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                        ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                        ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                        ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetTimeZoneList()
        {
            try
            {
                var obList = new List<HR_TIMEZONEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("SELECT * FROM HR_TIMEZONE");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_TIMEZONEModel ob = new HR_TIMEZONEModel();
                    ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                    ob.TIMEZONE_VALUE = (dr["TIMEZONE_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TIMEZONE_VALUE"]);
                    ob.TIMEZONE_TEXT = (dr["TIMEZONE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIMEZONE_TEXT"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
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

        public object OfficeDatasByBuyerId(Int64 MC_BUYER_ID)
        {
            string sp = "pkg_merchandising.mc_buyer_off_select";
            try
            {
                var obList = new List<MC_BUYER_OFFModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = MC_BUYER_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new MC_BUYER_OFFModel();
                    ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                    ob.BOFF_NAME_EN = (dr["BOFF_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOFF_NAME_EN"]);
                    ob.BOFF_NAME_BN = (dr["BOFF_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOFF_NAME_BN"]);
                    ob.LK_BOFF_TYPE_ID = (dr["LK_BOFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BOFF_TYPE_ID"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
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

        public object OfficeDatasByUserID()
        {
            string sp = "pkg_merchandising.mc_buyer_off_select";
            try
            {
                var obList = new List<MC_BUYER_OFFModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new MC_BUYER_OFFModel();
                    ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_OFF_ID"]);
                    ob.BOFF_NAME_EN = (dr["BOFF_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOFF_NAME_EN"]);
                    ob.BOFF_NAME_BN = (dr["BOFF_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BOFF_NAME_BN"]);
                    ob.LK_BOFF_TYPE_ID = (dr["LK_BOFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BOFF_TYPE_ID"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
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