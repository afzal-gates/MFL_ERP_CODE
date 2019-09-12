using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class HrOfficeModel
    {
        public Int64 HR_OFFICE_ID { get; set; }        
        public string OFFICE_CODE { get; set; }


        [Required]
        public Int64 HR_COMPANY_GRP_ID { get; set; }
        [Required]
        public string OFFICE_NAME_EN { get; set; }
        [Required]
        public string OFFICE_NAME_BN { get; set; }
        [Required]
        public string OFFICE_SNAME { get; set; }

        public string OFFICE_DESC { get; set; }
        public Int64? LK_OFF_TYPE_ID { get; set; }

        [Required]
        public string ADDRESS_EN { get; set; }
        [Required]
        public string ADDRESS_BN { get; set; }
        [Required]
        public string POST_CODE { get; set; }

        public string PO_NAME_EN { get; set; }
        public string PO_NAME_BN { get; set; }
        public string PO_BOX_NO { get; set; }

        [Required]
        public Int64 HR_TIMEZONE_ID { get; set; }
        public Int64 HR_GEO_DISTRICT_ID { get; set; }

        [Required]
        public Int64 HR_COUNTRY_ID { get; set; }

        public string PHONE_NO { get; set; }
        public string PHONE_EXT { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL_ID { get; set; }
        
        public string IS_ACTIVE { get; set; }

        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }        
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }
        public string OFFICE_TYPE_NAME_EN { get; set; }
        public string XML_COMP { get; set; }
        
        

        public List<HrOfficeModel> OfficeListData()
        {
            string sp = "pkg_hr.hr_office_select";
            try
            {
                var obList = new List<HrOfficeModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pOption", Value = 3002}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrOfficeModel ob = new HrOfficeModel();
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.OFFICE_CODE = (dr["OFFICE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_CODE"]);
                    ob.HR_COMPANY_GRP_ID = (dr["HR_COMPANY_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_GRP_ID"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.OFFICE_NAME_BN = (dr["OFFICE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_BN"]);
                    ob.OFFICE_SNAME = (dr["OFFICE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_SNAME"]);
                    ob.OFFICE_DESC = (dr["OFFICE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_DESC"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.LK_OFF_TYPE_ID = (dr["LK_OFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_OFF_TYPE_ID"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_NAME_EN = (dr["PO_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_NAME_EN"]);
                    ob.PO_NAME_BN = (dr["PO_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_NAME_BN"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                    ob.HR_GEO_DISTRICT_ID = (dr["HR_GEO_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GEO_DISTRICT_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.PHONE_NO = (dr["PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_NO"]);
                    ob.PHONE_EXT = (dr["PHONE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_EXT"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.OFFICE_TYPE_NAME_EN = (dr["OFFICE_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_TYPE_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<HrOfficeModel> GetOfficeList(Int32? pHR_COMPANY_ID = null, Int32? pOption = null)
        {
            string sp = "pkg_hr.hr_office_select";
            try
            {
                var obList = new List<HrOfficeModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value = pOption??3003}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrOfficeModel ob = new HrOfficeModel();
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.OFFICE_CODE = (dr["OFFICE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_CODE"]);
                    ob.HR_COMPANY_GRP_ID = (dr["HR_COMPANY_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_GRP_ID"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.OFFICE_NAME_BN = (dr["OFFICE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_BN"]);
                    ob.OFFICE_SNAME = (dr["OFFICE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_SNAME"]);
                    ob.OFFICE_DESC = (dr["OFFICE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_DESC"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.LK_OFF_TYPE_ID = (dr["LK_OFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_OFF_TYPE_ID"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_NAME_EN = (dr["PO_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_NAME_EN"]);
                    ob.PO_NAME_BN = (dr["PO_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_NAME_BN"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                    ob.HR_GEO_DISTRICT_ID = (dr["HR_GEO_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GEO_DISTRICT_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.PHONE_NO = (dr["PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_NO"]);
                    ob.PHONE_EXT = (dr["PHONE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_EXT"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.OFFICE_TYPE_NAME_EN = (dr["OFFICE_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_TYPE_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string Update()
        {
            const string sp = "pkg_hr.hr_office_update";            
            string vMsg = "";

            var ob = this;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_GRP_ID", Value = ob.HR_COMPANY_GRP_ID},
                    new CommandParameter() {ParameterName = "pOFFICE_CODE", Value = ob.OFFICE_CODE},
                    new CommandParameter() {ParameterName = "pOFFICE_NAME_EN", Value = ob.OFFICE_NAME_EN},
                    new CommandParameter() {ParameterName = "pOFFICE_NAME_BN", Value = ob.OFFICE_NAME_BN},
                    new CommandParameter() {ParameterName = "pOFFICE_SNAME", Value = ob.OFFICE_SNAME},
                    new CommandParameter() {ParameterName = "pOFFICE_DESC", Value = ob.OFFICE_DESC},
                    new CommandParameter() {ParameterName = "pLK_OFF_TYPE_ID", Value = ob.LK_OFF_TYPE_ID},
                    new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                    new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                    new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                    new CommandParameter() {ParameterName = "pPO_NAME_EN", Value = ob.PO_NAME_EN},
                    new CommandParameter() {ParameterName = "pPO_NAME_BN", Value = ob.PO_NAME_BN},
                    new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                    new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value = ob.HR_TIMEZONE_ID},
                    new CommandParameter() {ParameterName = "pHR_GEO_DISTRICT_ID", Value = ob.HR_GEO_DISTRICT_ID},
                    new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                    new CommandParameter() {ParameterName = "pPHONE_NO", Value = ob.PHONE_NO},
                    new CommandParameter() {ParameterName = "pPHONE_EXT", Value = ob.PHONE_EXT},
                    new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                    new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pXML_COMP", Value = ob.XML_COMP},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }                
            }
            catch (Exception ex)
            {
                //throw ex;
                vMsg = "MULTI-005" + ex.Message;
            }

            return vMsg;
        }

        public string Save()
        {
            const string sp = "pkg_hr.hr_office_insert";            
            string vMsg = "";
            var ob = this;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_GRP_ID", Value = ob.HR_COMPANY_GRP_ID},
                    new CommandParameter() {ParameterName = "pOFFICE_CODE", Value = ob.OFFICE_CODE},
                    new CommandParameter() {ParameterName = "pOFFICE_NAME_EN", Value = ob.OFFICE_NAME_EN},
                    new CommandParameter() {ParameterName = "pOFFICE_NAME_BN", Value = ob.OFFICE_NAME_BN},
                    new CommandParameter() {ParameterName = "pOFFICE_SNAME", Value = ob.OFFICE_SNAME},
                    new CommandParameter() {ParameterName = "pOFFICE_DESC", Value = ob.OFFICE_DESC},
                    new CommandParameter() {ParameterName = "pLK_OFF_TYPE_ID", Value = ob.LK_OFF_TYPE_ID},
                    new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                    new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                    new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                    new CommandParameter() {ParameterName = "pPO_NAME_EN", Value = ob.PO_NAME_EN},
                    new CommandParameter() {ParameterName = "pPO_NAME_BN", Value = ob.PO_NAME_BN},
                    new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                    new CommandParameter() {ParameterName = "pHR_TIMEZONE_ID", Value = ob.HR_TIMEZONE_ID},
                    new CommandParameter() {ParameterName = "pHR_GEO_DISTRICT_ID", Value = ob.HR_GEO_DISTRICT_ID},
                    new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                    new CommandParameter() {ParameterName = "pPHONE_NO", Value = ob.PHONE_NO},
                    new CommandParameter() {ParameterName = "pPHONE_EXT", Value = ob.PHONE_EXT},
                    new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                    new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pXML_COMP", Value = ob.XML_COMP},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }                
            }
            catch (Exception ex)
            {
                //throw ex;
                vMsg = "MULTI-005" + ex.Message;
            }

            return vMsg;
        }


    }
}
