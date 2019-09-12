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
    public class HR_AUD_CERT_REGModel
    {
        public Int64 HR_AUD_CERT_REG_ID { get; set; }
        public Int64 RF_AUD_CERT_TYPE_ID { get; set; }
        public Int64 REG_SL_NO { get; set; }
        public string IS_NEW_OR_R { get; set; }
        public string DOC_REF_NO { get; set; }
        public string ORG_DOC_NO { get; set; }
        public DateTime APPLY_DT { get; set; }
        public Int64 APPLY_BY_ID { get; set; }
        public string APP_DOC_PATH { get; set; }
        public string DOC_LOC_INFO { get; set; }
        public DateTime ISSUE_DT { get; set; }
        public DateTime VALID_DT { get; set; }
        public DateTime AUDIT_DT { get; set; }
        public string AUDIT_BY { get; set; }
        public string ISS_DOC_PATH { get; set; }
        public string REMARKS { get; set; }
        public Int64 LK_DOC_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public HttpPostedFileBase ATT_FILE { get; set; }

        public string Save()
        {
            const string sp = "PKG_HR_AUDIT_CERT.HR_AUD_CERT_REG_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value = ob.HR_AUD_CERT_REG_ID},
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value = ob.RF_AUD_CERT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},                     
                     new CommandParameter() {ParameterName = "pREG_SL_NO", Value = ob.REG_SL_NO},
                     new CommandParameter() {ParameterName = "pIS_NEW_OR_R", Value = ob.IS_NEW_OR_R},
                     new CommandParameter() {ParameterName = "pDOC_REF_NO", Value = ob.DOC_REF_NO},
                     new CommandParameter() {ParameterName = "pORG_DOC_NO", Value = ob.ORG_DOC_NO},
                     new CommandParameter() {ParameterName = "pAPPLY_DT", Value = ob.APPLY_DT},
                     new CommandParameter() {ParameterName = "pAPPLY_BY_ID", Value = ob.APPLY_BY_ID},
                     new CommandParameter() {ParameterName = "pAPP_DOC_PATH", Value = ob.APP_DOC_PATH},
                     new CommandParameter() {ParameterName = "pDOC_LOC_INFO", Value = ob.DOC_LOC_INFO},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pVALID_DT", Value = ob.VALID_DT},
                     new CommandParameter() {ParameterName = "pAUDIT_DT", Value = ob.AUDIT_DT},
                     new CommandParameter() {ParameterName = "pAUDIT_BY", Value = ob.AUDIT_BY},
                     new CommandParameter() {ParameterName = "pISS_DOC_PATH", Value = ob.ISS_DOC_PATH},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pLK_DOC_STATUS_ID", Value = ob.LK_DOC_STATUS_ID==0?1:ob.LK_DOC_STATUS_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE==null?"Y":ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pIS_LICENCE", Value = ob.IS_LICENCE==null?"L":ob.IS_LICENCE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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
            const string sp = "PKG_HR_AUDIT_CERT.HR_AUD_CERT_REG_";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value = ob.HR_AUD_CERT_REG_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value = ob.RF_AUD_CERT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREG_SL_NO", Value = ob.REG_SL_NO},
                     new CommandParameter() {ParameterName = "pIS_NEW_OR_R", Value = ob.IS_NEW_OR_R},
                     new CommandParameter() {ParameterName = "pDOC_REF_NO", Value = ob.DOC_REF_NO},
                     new CommandParameter() {ParameterName = "pORG_DOC_NO", Value = ob.ORG_DOC_NO},
                     new CommandParameter() {ParameterName = "pAPPLY_DT", Value = ob.APPLY_DT},
                     new CommandParameter() {ParameterName = "pAPPLY_BY_ID", Value = ob.APPLY_BY_ID},
                     new CommandParameter() {ParameterName = "pAPP_DOC_PATH", Value = ob.APP_DOC_PATH},
                     new CommandParameter() {ParameterName = "pDOC_LOC_INFO", Value = ob.DOC_LOC_INFO},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pVALID_DT", Value = ob.VALID_DT},
                     new CommandParameter() {ParameterName = "pAUDIT_DT", Value = ob.AUDIT_DT},
                     new CommandParameter() {ParameterName = "pAUDIT_BY", Value = ob.AUDIT_BY},
                     new CommandParameter() {ParameterName = "pISS_DOC_PATH", Value = ob.ISS_DOC_PATH},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pIS_LICENCE", Value = ob.IS_LICENCE},
                     
                     new CommandParameter() {ParameterName = "pLK_DOC_STATUS_ID", Value = ob.LK_DOC_STATUS_ID},
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
            const string sp = "PKG_HR_AUDIT_CERT.HR_AUD_CERT_REG_";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value = ob.HR_AUD_CERT_REG_ID},
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value = ob.RF_AUD_CERT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREG_SL_NO", Value = ob.REG_SL_NO},
                     new CommandParameter() {ParameterName = "pIS_NEW_OR_R", Value = ob.IS_NEW_OR_R},
                     new CommandParameter() {ParameterName = "pDOC_REF_NO", Value = ob.DOC_REF_NO},
                     new CommandParameter() {ParameterName = "pORG_DOC_NO", Value = ob.ORG_DOC_NO},
                     new CommandParameter() {ParameterName = "pAPPLY_DT", Value = ob.APPLY_DT},
                     new CommandParameter() {ParameterName = "pAPPLY_BY_ID", Value = ob.APPLY_BY_ID},
                     new CommandParameter() {ParameterName = "pAPP_DOC_PATH", Value = ob.APP_DOC_PATH},
                     new CommandParameter() {ParameterName = "pDOC_LOC_INFO", Value = ob.DOC_LOC_INFO},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pVALID_DT", Value = ob.VALID_DT},
                     new CommandParameter() {ParameterName = "pAUDIT_DT", Value = ob.AUDIT_DT},
                     new CommandParameter() {ParameterName = "pAUDIT_BY", Value = ob.AUDIT_BY},
                     new CommandParameter() {ParameterName = "pISS_DOC_PATH", Value = ob.ISS_DOC_PATH},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pLK_DOC_STATUS_ID", Value = ob.LK_DOC_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<HR_AUD_CERT_REGModel> SelectAll()
        {
            string sp = "PKG_HR_AUDIT_CERT.HR_AUD_CERT_REG_Select";
            try
            {
                var obList = new List<HR_AUD_CERT_REGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_AUD_CERT_REGModel ob = new HR_AUD_CERT_REGModel();
                    ob.HR_AUD_CERT_REG_ID = (dr["HR_AUD_CERT_REG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_AUD_CERT_REG_ID"]);
                    ob.RF_AUD_CERT_TYPE_ID = (dr["RF_AUD_CERT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_AUD_CERT_TYPE_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.REG_SL_NO = (dr["REG_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REG_SL_NO"]);
                    ob.IS_NEW_OR_R = (dr["IS_NEW_OR_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NEW_OR_R"]);
                    ob.DOC_REF_NO = (dr["DOC_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_REF_NO"]);
                    ob.ORG_DOC_NO = (dr["ORG_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORG_DOC_NO"]);
                    ob.APPLY_DT = (dr["APPLY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APPLY_DT"]);
                    ob.APPLY_BY_ID = (dr["APPLY_BY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPLY_BY_ID"]);
                    ob.APP_DOC_PATH = (dr["APP_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APP_DOC_PATH"]);
                    ob.DOC_LOC_INFO = (dr["DOC_LOC_INFO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_LOC_INFO"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.VALID_DT = (dr["VALID_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["VALID_DT"]);
                    ob.AUDIT_DT = (dr["AUDIT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["AUDIT_DT"]);
                    ob.AUDIT_BY = (dr["AUDIT_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUDIT_BY"]);
                    ob.ISS_DOC_PATH = (dr["ISS_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_DOC_PATH"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_DOC_STATUS_ID = (dr["LK_DOC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DOC_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_LICENCE = (dr["IS_LICENCE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_LICENCE"]);
                    
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.ISS_BY_ORG_NAME = (dr["ISS_BY_ORG_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_BY_ORG_NAME"]);

                    ob.LK_AUD_DOC_TYPE_ID = (dr["LK_AUD_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AUD_DOC_TYPE_ID"]);
                    ob.CERT_TYPE_TITLE = (dr["CERT_TYPE_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_TITLE"]);
                    ob.RESP_EMP_ID = (dr["RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RESP_EMP_ID"]);
                    ob.RESP_EMP_NAME = (dr["RESP_EMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_EMP_NAME"]);
                    ob.MC_BUYER_NAME_LST = (dr["MC_BUYER_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_NAME_LST"]);
                    ob.MC_BUYER_ID_LST = (dr["MC_BUYER_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_ID_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_AUD_CERT_REGModel Select(Int32? pHR_AUD_CERT_REG_ID = null)
        {
            string sp = "PKG_HR_AUDIT_CERT.HR_AUD_CERT_REG_Select";
            try
            {
                var ob = new HR_AUD_CERT_REGModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value =pHR_AUD_CERT_REG_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_AUD_CERT_REG_ID = (dr["HR_AUD_CERT_REG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_AUD_CERT_REG_ID"]);
                    ob.RF_AUD_CERT_TYPE_ID = (dr["RF_AUD_CERT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_AUD_CERT_TYPE_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.REG_SL_NO = (dr["REG_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REG_SL_NO"]);
                    ob.IS_NEW_OR_R = (dr["IS_NEW_OR_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NEW_OR_R"]);
                    ob.DOC_REF_NO = (dr["DOC_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_REF_NO"]);
                    ob.ORG_DOC_NO = (dr["ORG_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORG_DOC_NO"]);
                    ob.APPLY_DT = (dr["APPLY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APPLY_DT"]);
                    ob.APPLY_BY_ID = (dr["APPLY_BY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPLY_BY_ID"]);
                    ob.APP_DOC_PATH = (dr["APP_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APP_DOC_PATH"]);
                    ob.DOC_LOC_INFO = (dr["DOC_LOC_INFO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_LOC_INFO"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.VALID_DT = (dr["VALID_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["VALID_DT"]);
                    ob.AUDIT_DT = (dr["AUDIT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["AUDIT_DT"]);
                    ob.AUDIT_BY = (dr["AUDIT_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUDIT_BY"]);
                    ob.ISS_DOC_PATH = (dr["ISS_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_DOC_PATH"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_DOC_STATUS_ID = (dr["LK_DOC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DOC_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_LICENCE = (dr["IS_LICENCE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_LICENCE"]);

                    ob.CURR_ORG_DOC_NO = (dr["ORG_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORG_DOC_NO"]);
                    ob.CURR_ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.CURR_VALID_DT = (dr["VALID_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["VALID_DT"]);
                    ob.CURR_LK_DOC_STATUS_ID = (dr["LK_DOC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DOC_STATUS_ID"]);
                    ob.NOTIFY_BF_DAYS = (dr["NOTIFY_BF_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NOTIFY_BF_DAYS"]);
                    ob.ISS_BY_ORG_NAME = (dr["ISS_BY_ORG_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_BY_ORG_NAME"]);

                    ob.LK_AUD_DOC_TYPE_ID = (dr["LK_AUD_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AUD_DOC_TYPE_ID"]);
                    ob.CERT_TYPE_TITLE = (dr["CERT_TYPE_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_TITLE"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.RESP_EMP_ID = (dr["RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RESP_EMP_ID"]);
                    ob.RESP_EMP_NAME = (dr["RESP_EMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_EMP_NAME"]);
                    ob.MC_BUYER_NAME_LST = (dr["MC_BUYER_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_NAME_LST"]);
                    ob.MC_BUYER_ID_LST = (dr["MC_BUYER_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_ID_LST"]);

                    ob.REMAIN_DAYS = (DateTime.Now - ob.VALID_DT.AddDays(-ob.NOTIFY_BF_DAYS)).Days;

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<HR_AUD_CERT_REGModel> SelectByID(Int32? pHR_COMPANY_ID = null, Int32? pRF_AUD_CERT_TYPE_ID = null)
        {
            string sp = "PKG_HR_AUDIT_CERT.HR_AUD_CERT_REG_SELECT";
            try
            {
                var list = new List<HR_AUD_CERT_REGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value =pRF_AUD_CERT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new HR_AUD_CERT_REGModel();
                    ob.HR_AUD_CERT_REG_ID = (dr["HR_AUD_CERT_REG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_AUD_CERT_REG_ID"]);
                    ob.RF_AUD_CERT_TYPE_ID = (dr["RF_AUD_CERT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_AUD_CERT_TYPE_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.REG_SL_NO = (dr["REG_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REG_SL_NO"]);
                    ob.IS_NEW_OR_R = (dr["IS_NEW_OR_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NEW_OR_R"]);
                    ob.DOC_REF_NO = (dr["DOC_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_REF_NO"]);
                    ob.ORG_DOC_NO = (dr["ORG_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORG_DOC_NO"]);
                    ob.APPLY_DT = (dr["APPLY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["APPLY_DT"]);
                    ob.APPLY_BY_ID = (dr["APPLY_BY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPLY_BY_ID"]);
                    ob.APP_DOC_PATH = (dr["APP_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APP_DOC_PATH"]);
                    ob.DOC_LOC_INFO = (dr["DOC_LOC_INFO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_LOC_INFO"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.VALID_DT = (dr["VALID_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["VALID_DT"]);
                    ob.AUDIT_DT = (dr["AUDIT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["AUDIT_DT"]);
                    ob.AUDIT_BY = (dr["AUDIT_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUDIT_BY"]);
                    ob.ISS_DOC_PATH = (dr["ISS_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_DOC_PATH"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_DOC_STATUS_ID = (dr["LK_DOC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DOC_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_LICENCE = (dr["IS_LICENCE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_LICENCE"]);

                    ob.CURR_ORG_DOC_NO = (dr["ORG_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORG_DOC_NO"]);
                    ob.CURR_ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.CURR_VALID_DT = (dr["VALID_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["VALID_DT"]);
                    ob.CURR_LK_DOC_STATUS_ID = (dr["LK_DOC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DOC_STATUS_ID"]);
                    ob.NOTIFY_BF_DAYS = (dr["NOTIFY_BF_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NOTIFY_BF_DAYS"]);
                    ob.ISS_BY_ORG_NAME = (dr["ISS_BY_ORG_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_BY_ORG_NAME"]);

                    ob.LK_AUD_DOC_TYPE_ID = (dr["LK_AUD_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AUD_DOC_TYPE_ID"]);
                    ob.CERT_TYPE_TITLE = (dr["CERT_TYPE_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_TITLE"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.RESP_EMP_ID = (dr["RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RESP_EMP_ID"]);
                    ob.RESP_EMP_NAME = (dr["RESP_EMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_EMP_NAME"]);
                    ob.MC_BUYER_NAME_LST = (dr["MC_BUYER_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_NAME_LST"]);
                    ob.MC_BUYER_ID_LST = (dr["MC_BUYER_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_ID_LST"]);
                    
                    ob.REMAIN_DAYS = (DateTime.Now - ob.VALID_DT.AddDays(-ob.NOTIFY_BF_DAYS)).Days;

                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long HR_COMPANY_ID { get; set; }

        public string CURR_ORG_DOC_NO { get; set; }

        public DateTime CURR_ISSUE_DT { get; set; }

        public DateTime CURR_VALID_DT { get; set; }

        public long CURR_LK_DOC_STATUS_ID { get; set; }

        public long NOTIFY_BF_DAYS { get; set; }

        public int REMAIN_DAYS { get; set; }

        public long RESP_EMP_ID { get; set; }

        public string RESP_EMP_NAME { get; set; }

        public string MC_BUYER_NAME_LST { get; set; }

        public string MC_BUYER_ID_LST { get; set; }

        public string ISS_BY_ORG_NAME { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string IS_ACTIVE { get; set; }

        public string IS_LICENCE { get; set; }

        public string CERT_TYPE_TITLE { get; set; }

        public long LK_AUD_DOC_TYPE_ID { get; set; }
    }
}