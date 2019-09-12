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
    public class KNT_YRN_LOT_TEST_HModel
    {
        public Int64 KNT_YRN_LOT_TEST_H_ID { get; set; }
        public string TEST_WO_NO { get; set; }
        public DateTime TEST_WO_DT { get; set; }
        public Int64 TEST_WO_BY { get; set; }
        public string ATTN_TO { get; set; }
        public Int64? KNT_YRN_STR_REQ_H_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_FINALIZED { get; set; }
        public string XML_TEST_D { get; set; }
        

        public string Save()
        {
            const string sp = "PKG_KNT_YARN_TEST.KNT_YRN_LOT_TEST_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_H_ID", Value = ob.KNT_YRN_LOT_TEST_H_ID},
                     new CommandParameter() {ParameterName = "pTEST_WO_NO", Value = ob.TEST_WO_NO},
                     new CommandParameter() {ParameterName = "pTEST_WO_DT", Value = ob.TEST_WO_DT},
                     new CommandParameter() {ParameterName = "pTEST_WO_BY", Value = ob.TEST_WO_BY},
                     new CommandParameter() {ParameterName = "pATTN_TO", Value = ob.ATTN_TO},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID==0?null:ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pXML_TEST_D", Value = ob.XML_TEST_D},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     
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
            const string sp = "PKG_KNT_YARN_TEST.KNT_YRN_LOT_TEST_H_UPDATE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_H_ID", Value = ob.KNT_YRN_LOT_TEST_H_ID},
                     new CommandParameter() {ParameterName = "pTEST_WO_NO", Value = ob.TEST_WO_NO},
                     new CommandParameter() {ParameterName = "pTEST_WO_DT", Value = ob.TEST_WO_DT},
                     new CommandParameter() {ParameterName = "pTEST_WO_BY", Value = ob.TEST_WO_BY},
                     new CommandParameter() {ParameterName = "pATTN_TO", Value = ob.ATTN_TO},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID==0?null:ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pXML_TEST_D", Value = ob.XML_TEST_D},
                     
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

        public string Delete()
        {
            const string sp = "SP_KNT_YRN_LOT_TEST_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_H_ID", Value = ob.KNT_YRN_LOT_TEST_H_ID},
                     new CommandParameter() {ParameterName = "pTEST_WO_NO", Value = ob.TEST_WO_NO},
                     new CommandParameter() {ParameterName = "pTEST_WO_DT", Value = ob.TEST_WO_DT},
                     new CommandParameter() {ParameterName = "pTEST_WO_BY", Value = ob.TEST_WO_BY},
                     new CommandParameter() {ParameterName = "pATTN_TO", Value = ob.ATTN_TO},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
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
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<KNT_YRN_LOT_TEST_HModel> SelectAll(int pageNo, int pageSize, string pTEST_WO_NO = null, string pTEST_WO_DT = null)
        {
            string sp = "pkg_knt_yarn_test.KNT_YRN_LOT_TEST_H_SELECT";
            try
            {
                var obList = new List<KNT_YRN_LOT_TEST_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pTEST_WO_NO", Value =pTEST_WO_NO},
                     new CommandParameter() {ParameterName = "pTEST_WO_DT", Value =pTEST_WO_DT},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_TEST_HModel ob = new KNT_YRN_LOT_TEST_HModel();
                    ob.KNT_YRN_LOT_TEST_H_ID = (dr["KNT_YRN_LOT_TEST_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_H_ID"]);
                    ob.TEST_WO_NO = (dr["TEST_WO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_WO_NO"]);
                    ob.TEST_WO_DT = (dr["TEST_WO_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TEST_WO_DT"]);
                    ob.TEST_WO_BY = (dr["TEST_WO_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TEST_WO_BY"]);
                    ob.ATTN_TO = (dr["ATTN_TO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ATTN_TO"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.TEST_WO_BY_NAME = (dr["TEST_WO_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_WO_BY_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    //ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.YARN_ITEM_INFO = (dr["YARN_ITEM_INFO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_ITEM_INFO"]);

                    ob.KNT_JOB_CRD_H_LST = new KNT_JOB_CRD_HModel().getYarnTestKP(ob.KNT_YRN_LOT_TEST_H_ID, 3002);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YRN_LOT_TEST_HModel Select(Int64? pKNT_YRN_LOT_TEST_H_ID = null)
        {
            string sp = "pkg_knt_yarn_test.KNT_YRN_LOT_TEST_H_SELECT";
            try
            {
                var ob = new KNT_YRN_LOT_TEST_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_H_ID", Value =pKNT_YRN_LOT_TEST_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_LOT_TEST_H_ID = (dr["KNT_YRN_LOT_TEST_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_H_ID"]);
                    ob.TEST_WO_NO = (dr["TEST_WO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_WO_NO"]);
                    ob.TEST_WO_DT = (dr["TEST_WO_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TEST_WO_DT"]);
                    ob.TEST_WO_BY = (dr["TEST_WO_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TEST_WO_BY"]);
                    ob.ATTN_TO = (dr["ATTN_TO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ATTN_TO"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int TOTAL_REC { get; set; }

        public string TEST_WO_BY_NAME { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }


        public long KNT_JOB_CRD_H_ID { get; set; }

        public string YARN_ITEM_INFO { get; set; }

        public List<KNT_JOB_CRD_HModel> KNT_JOB_CRD_H_LST { get; set; }
    }
}