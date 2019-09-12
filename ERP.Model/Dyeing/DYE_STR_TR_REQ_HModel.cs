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
    public class DYE_STR_TR_REQ_HModel
    {
        public Int64 DYE_STR_TR_REQ_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public string STR_TR_REQ_NO { get; set; }
        public DateTime STR_TR_REQ_DT { get; set; }
        public string INV_ITEM_CAT_LST { get; set; }
        public Int64 STR_REQ_BY { get; set; }
        public Int64 STR_REQ_TO { get; set; }
        public string REQ_ATTN_MAIL { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 FRM_STORE_ID { get; set; }
        public Int64 TO_STORE_ID { get; set; }
        public string REQ_REMARKS { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string ISS_REMARKS { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public int TOTAL_REC { get; set; }

        public string COMP_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string FROM_ST_NAME { get; set; }
        public string TO_ST_NAME { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string XML_REQ_D { get; set; }

        public string Save()
        {
            const string sp = "pkg_dye_str_tf.dye_str_tr_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ob.DYE_STR_TR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_NO", Value = ob.STR_TR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_DT", Value = ob.STR_TR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFRM_STORE_ID", Value = ob.FRM_STORE_ID},
                     new CommandParameter() {ParameterName = "pTO_STORE_ID", Value = ob.TO_STORE_ID},
                     new CommandParameter() {ParameterName = "pREQ_REMARKS", Value = ob.REQ_REMARKS},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pISS_REMARKS", Value = ob.ISS_REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opDYE_STR_TR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output},
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

        public string SaveIssue()
        {
            const string sp = "pkg_dye_str_tf.dye_str_tr_req_h_issue";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ob.DYE_STR_TR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_NO", Value = ob.STR_TR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_DT", Value = ob.STR_TR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFRM_STORE_ID", Value = ob.FRM_STORE_ID},
                     new CommandParameter() {ParameterName = "pTO_STORE_ID", Value = ob.TO_STORE_ID},
                     new CommandParameter() {ParameterName = "pREQ_REMARKS", Value = ob.REQ_REMARKS},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pISS_REMARKS", Value = ob.ISS_REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opDYE_STR_TR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output},
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
            const string sp = "pkg_dye_str_tf.dye_str_tr_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ob.DYE_STR_TR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_NO", Value = ob.STR_TR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_DT", Value = ob.STR_TR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFRM_STORE_ID", Value = ob.FRM_STORE_ID},
                     new CommandParameter() {ParameterName = "pTO_STORE_ID", Value = ob.TO_STORE_ID},
                     new CommandParameter() {ParameterName = "pREQ_REMARKS", Value = ob.REQ_REMARKS},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pISS_REMARKS", Value = ob.ISS_REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "opDYE_STR_TR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output},
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
            const string sp = "SP_DYE_STR_TR_REQ_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ob.DYE_STR_TR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_NO", Value = ob.STR_TR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_DT", Value = ob.STR_TR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFRM_STORE_ID", Value = ob.FRM_STORE_ID},
                     new CommandParameter() {ParameterName = "pTO_STORE_ID", Value = ob.TO_STORE_ID},
                     new CommandParameter() {ParameterName = "pREQ_REMARKS", Value = ob.REQ_REMARKS},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pISS_REMARKS", Value = ob.ISS_REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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

        public List<DYE_STR_TR_REQ_HModel> SelectAll(int pageNo, int pageSize, string pSTR_TR_REQ_NO = null, string pSTR_TR_REQ_DT = null, string pCOMP_NAME_EN = null,
            string pDEPARTMENT_NAME_EN = null, string pREQ_TYPE_NAME = null, string pFROM_ST_NAME = null, string pTO_ST_NAME = null, string pEVENT_NAME = null,
            string pREQ_REMARKS = null, Int64? pUSER_ID = null)
        {
            string sp = "pkg_dye_str_tf.dye_str_tr_req_h_select";
            try
            {
                var obList = new List<DYE_STR_TR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_NO", Value =pSTR_TR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_TR_REQ_DT", Value =pSTR_TR_REQ_DT},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pDEPARTMENT_NAME_EN", Value =pDEPARTMENT_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value =pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pFROM_ST_NAME", Value =pFROM_ST_NAME},
                     new CommandParameter() {ParameterName = "pTO_ST_NAME", Value =pTO_ST_NAME},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value =pEVENT_NAME},
                     new CommandParameter() {ParameterName = "pREQ_REMARKS", Value =pREQ_REMARKS},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =pUSER_ID},

                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_TR_REQ_HModel ob = new DYE_STR_TR_REQ_HModel();
                    ob.DYE_STR_TR_REQ_H_ID = (dr["DYE_STR_TR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_TR_REQ_NO = (dr["STR_TR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_TR_REQ_NO"]);
                    ob.STR_TR_REQ_DT = (dr["STR_TR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_TR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.FRM_STORE_ID = (dr["FRM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_STORE_ID"]);
                    ob.TO_STORE_ID = (dr["TO_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_STORE_ID"]);
                    ob.REQ_REMARKS = (dr["REQ_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_REMARKS"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.ISS_REMARKS = (dr["ISS_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PERMISSION"]);
                                        
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.FROM_ST_NAME = (dr["FROM_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ST_NAME"]);
                    ob.TO_ST_NAME = (dr["TO_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ST_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.IS_ISSUED = (dr["IS_ISSUED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_ISSUED"]);

                    ob.TTL_RQD = (dr["TTL_RQD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RQD"]);
                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);
                    ob.IS_ISSUED = (int)(ob.TTL_RQD - ob.TTL_ISS_QTY);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_STR_TR_REQ_HModel Select(Int64? ID)
        {
            string sp = "pkg_dye_str_tf.dye_str_tr_req_h_select";
            try
            {
                var ob = new DYE_STR_TR_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_STR_TR_REQ_H_ID = (dr["DYE_STR_TR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_TR_REQ_NO = (dr["STR_TR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_TR_REQ_NO"]);
                    ob.STR_TR_REQ_DT = (dr["STR_TR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_TR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.FRM_STORE_ID = (dr["FRM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_STORE_ID"]);
                    ob.TO_STORE_ID = (dr["TO_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_STORE_ID"]);
                    ob.REQ_REMARKS = (dr["REQ_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_REMARKS"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.ISS_REMARKS = (dr["ISS_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    if (dr["B_DISABLE"] != DBNull.Value)
                        ob.B_DISABLE = (dr["B_DISABLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["B_DISABLE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int IS_ISSUED { get; set; }

        public string REQ_TYPE_NAME { get; set; }


        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public int PERMISSION { get; set; }

        public decimal TTL_RQD { get; set; }

        public decimal TTL_ISS_QTY { get; set; }

        public long B_DISABLE { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }
    }
}