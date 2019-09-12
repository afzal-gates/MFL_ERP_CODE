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
    public class SCM_STR_OIL_REQ_HModel
    {
        public Int64 SCM_STR_OIL_REQ_H_ID { get; set; }
        public Int64? HR_COMPANY_ID { get; set; }
        public Int64? HR_DEPARTMENT_ID { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }
        public string STR_REQ_NO { get; set; }
        public DateTime STR_REQ_DT { get; set; }
        public Int64? STR_REQ_BY { get; set; }
        public Int64? STR_REQ_TO { get; set; }
        public string REQ_ATTN_MAIL { get; set; }
        public Int64? RF_REQ_TYPE_ID { get; set; }
        public Int64? RCV_STORE_ID { get; set; }
        public Int64? ISS_STORE_ID { get; set; }
        public Int64? RF_ACTN_STATUS_ID { get; set; }
        public string REQ_REMARKS { get; set; }
        public string ISS_REMARKS { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string USER_VERIFIER_NAME { get; set; }
        public string USER_ISSUER_NAME { get; set; }
        public string ISS_STORE_NAME_EN { get; set; }
        public string RCV_STORE_NAME_EN { get; set; }
        public decimal? RQD_QTY { get; set; }
        public decimal? ISS_QTY { get; set; }
        public string IS_FINALIZE { get; set; }
        public string OIL_REQ_D_XML { get; set; }



        
        public string BatchSave()
        {
            const string sp = "pkg_scm_str.scm_str_oil_req_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_OIL_REQ_H_ID", Value = ob.SCM_STR_OIL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRCV_STORE_ID", Value = ob.RCV_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREQ_REMARKS", Value = ob.REQ_REMARKS},
                     new CommandParameter() {ParameterName = "pISS_REMARKS", Value = ob.ISS_REMARKS},
                     new CommandParameter() {ParameterName = "pOIL_REQ_D_XML", Value = ob.OIL_REQ_D_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
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

        public string SubmitRequisition()
        {
            const string sp = "pkg_scm_str.scm_str_oil_req_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_OIL_REQ_H_ID", Value = ob.SCM_STR_OIL_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1001},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
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

        public string IssueRequisition()
        {
            const string sp = "pkg_scm_str.scm_str_oil_req_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_OIL_REQ_H_ID", Value = ob.SCM_STR_OIL_REQ_H_ID},  
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRCV_STORE_ID", Value = ob.RCV_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},

                     new CommandParameter() {ParameterName = "pISS_REMARKS", Value = ob.ISS_REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZE", Value = ob.IS_FINALIZE},
                     new CommandParameter() {ParameterName = "pOIL_REQ_D_XML", Value = ob.OIL_REQ_D_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1002},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
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

        public List<SCM_STR_OIL_REQ_HModel> SelectAll()
        {
            string sp = "Select_SCM_STR_OIL_REQ_H";
            try
            {
                var obList = new List<SCM_STR_OIL_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_OIL_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_OIL_REQ_HModel ob = new SCM_STR_OIL_REQ_HModel();
                    ob.SCM_STR_OIL_REQ_H_ID = (dr["SCM_STR_OIL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_OIL_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RCV_STORE_ID = (dr["RCV_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REQ_REMARKS = (dr["REQ_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_REMARKS"]);
                    ob.ISS_REMARKS = (dr["ISS_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REMARKS"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_OIL_REQ_HModel GetReqByHdrID(Int64 pSCM_STR_OIL_REQ_H_ID)
        {
            string sp = "pkg_scm_str.scm_str_oil_req_h_select";
            try
            {
                var ob = new SCM_STR_OIL_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pSCM_STR_OIL_REQ_H_ID", Value = pSCM_STR_OIL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_OIL_REQ_H_ID = (dr["SCM_STR_OIL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_OIL_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    //ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RCV_STORE_ID = (dr["RCV_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REQ_REMARKS = (dr["REQ_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_REMARKS"]);
                    ob.ISS_REMARKS = (dr["ISS_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REMARKS"]);

                    ob.USER_VERIFIER_NAME = (dr["USER_VERIFIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_VERIFIER_NAME"]);
                    ob.USER_ISSUER_NAME = (dr["USER_ISSUER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_ISSUER_NAME"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetReqList(Int64 pageNumber, Int64 pageSize)
        {
            string sp = "pkg_scm_str.scm_str_oil_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<SCM_STR_OIL_REQ_HModel>();
                var obj = new RF_PAGERModel();
            
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]}, 
                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_OIL_REQ_HModel ob = new SCM_STR_OIL_REQ_HModel();
                    ob.SCM_STR_OIL_REQ_H_ID = (dr["SCM_STR_OIL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_OIL_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    //ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RCV_STORE_ID = (dr["RCV_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REQ_REMARKS = (dr["REQ_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_REMARKS"]);
                    ob.ISS_REMARKS = (dr["ISS_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REMARKS"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.USER_VERIFIER_NAME = (dr["USER_VERIFIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_VERIFIER_NAME"]);
                    ob.USER_ISSUER_NAME = (dr["USER_ISSUER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_ISSUER_NAME"]);

                    ob.ISS_STORE_NAME_EN = (dr["ISS_STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_STORE_NAME_EN"]);
                    ob.RCV_STORE_NAME_EN = (dr["RCV_STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RCV_STORE_NAME_EN"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    
                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetItemStockByID(long pISS_STORE_ID, long pRCV_STORE_ID, long pINV_ITEM_ID)
        {
            string sp = "pkg_scm_str.scm_str_oil_req_h_select";
            try
            {                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = pISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pRCV_STORE_ID", Value = pRCV_STORE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = pINV_ITEM_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                    
                    var ob = new {
                        CENTRAL_STR_STOCK = (dr["CENTRAL_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CENTRAL_STR_STOCK"]),
                        SUB_STR_STOCK = (dr["SUB_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SUB_STR_STOCK"])
                    };

                    return ob;
                }
                return new { CENTRAL_STR_STOCK = 0, SUB_STR_STOCK = 0 };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string INV_ITEM_CAT_LST { get; set; }
    }
}