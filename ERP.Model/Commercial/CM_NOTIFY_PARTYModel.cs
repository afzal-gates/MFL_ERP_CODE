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
    public class CM_NOTIFY_PARTYModel
    {
        public Int64 CM_NOTIFY_PARTY_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public string NTF_PARTY_CODE { get; set; }
        public string NTF_PARTY_NAME_EN { get; set; }
        public string NTF_PARTY_NAME_BN { get; set; }
        public string ADDRESS_EN { get; set; }
        public string ADDRESS_BN { get; set; }
        public Int64? HR_COUNTRY_ID { get; set; }
        public string POST_CODE { get; set; }
        public string PO_BOX_NO { get; set; }
        public string WORK_PHONE { get; set; }
        public string MOB_PHONE { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL_ID { get; set; }
        public string WEB_URL { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "PKG_CM_COMMON.CM_NOTIFY_PARTY_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_ID", Value = ob.CM_NOTIFY_PARTY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_CODE", Value = ob.NTF_PARTY_CODE},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_NAME_EN", Value = ob.NTF_PARTY_NAME_EN},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_NAME_BN", Value = ob.NTF_PARTY_NAME_BN},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opCM_NOTIFY_PARTY_ID", Value =0, Direction = ParameterDirection.Output},
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

        public string Update()
        {
            const string sp = "SP_CM_NOTIFY_PARTY";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_ID", Value = ob.CM_NOTIFY_PARTY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_CODE", Value = ob.NTF_PARTY_CODE},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_NAME_EN", Value = ob.NTF_PARTY_NAME_EN},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_NAME_BN", Value = ob.NTF_PARTY_NAME_BN},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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
            const string sp = "PKG_CM_COMMON.CM_NOTIFY_PARTY_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_ID", Value = ob.CM_NOTIFY_PARTY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_CODE", Value = ob.NTF_PARTY_CODE},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_NAME_EN", Value = ob.NTF_PARTY_NAME_EN},
                     new CommandParameter() {ParameterName = "pNTF_PARTY_NAME_BN", Value = ob.NTF_PARTY_NAME_BN},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<CM_NOTIFY_PARTYModel> SelectAll()
        {
            string sp = "PKG_CM_COMMON.CM_NOTIFY_PARTY_SELECT";
            try
            {
                var obList = new List<CM_NOTIFY_PARTYModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_NOTIFY_PARTYModel ob = new CM_NOTIFY_PARTYModel();
                    ob.CM_NOTIFY_PARTY_ID = (dr["CM_NOTIFY_PARTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_NOTIFY_PARTY_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.NTF_PARTY_CODE = (dr["NTF_PARTY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_CODE"]);
                    ob.NTF_PARTY_NAME_EN = (dr["NTF_PARTY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_NAME_EN"]);
                    ob.NTF_PARTY_NAME_BN = (dr["NTF_PARTY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_NAME_BN"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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

        public List<CM_NOTIFY_PARTYModel> SelectByID(Int64? pMC_BUYER_ID = null)
        {
            string sp = "PKG_CM_COMMON.CM_NOTIFY_PARTY_SELECT";
            try
            {
                var obList = new List<CM_NOTIFY_PARTYModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_NOTIFY_PARTYModel ob = new CM_NOTIFY_PARTYModel();
                    ob.CM_NOTIFY_PARTY_ID = (dr["CM_NOTIFY_PARTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_NOTIFY_PARTY_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.NTF_PARTY_CODE = (dr["NTF_PARTY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_CODE"]);
                    ob.NTF_PARTY_NAME_EN = (dr["NTF_PARTY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_NAME_EN"]);
                    ob.NTF_PARTY_NAME_BN = (dr["NTF_PARTY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_NAME_BN"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
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

        public CM_NOTIFY_PARTYModel Select(long ID)
        {
            string sp = "Select_CM_NOTIFY_PARTY";
            try
            {
                var ob = new CM_NOTIFY_PARTYModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_NOTIFY_PARTY_ID = (dr["CM_NOTIFY_PARTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_NOTIFY_PARTY_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.NTF_PARTY_CODE = (dr["NTF_PARTY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_CODE"]);
                    ob.NTF_PARTY_NAME_EN = (dr["NTF_PARTY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_NAME_EN"]);
                    ob.NTF_PARTY_NAME_BN = (dr["NTF_PARTY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NTF_PARTY_NAME_BN"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BUYER_NAME_EN { get; set; }

        public string COUNTRY_NAME_EN { get; set; }
    }
}