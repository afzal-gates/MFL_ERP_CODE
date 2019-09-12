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
    public class KNT_YRN_LOT_TEST_R1Model
    {
        public Int64 KNT_YRN_LOT_TEST_R1_ID { get; set; }
        public Int64 KNT_YRN_LOT_TEST_D1_ID { get; set; }
        public Int64 RF_YRN_TST_PARAM_ID { get; set; }
        public string TST_PARAM_VAL { get; set; }
        public string COMMENTS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string XML_TEST_R1 { get; set; }


        public string Save()
        {
            const string sp = "pkg_knt_yarn_test.knt_yrn_lot_test_r1_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_R1_ID", Value = ob.KNT_YRN_LOT_TEST_R1_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value = ob.KNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pRF_YRN_TST_PARAM_ID", Value = ob.RF_YRN_TST_PARAM_ID},
                     new CommandParameter() {ParameterName = "pTST_PARAM_VAL", Value = ob.TST_PARAM_VAL},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
                     new CommandParameter() {ParameterName = "pXML_TEST_R1", Value = ob.XML_TEST_R1},
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
            const string sp = "PKG_KNT_YARN_TEST.SP_KNT_YRN_LOT_TEST_R1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_R1_ID", Value = ob.KNT_YRN_LOT_TEST_R1_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value = ob.KNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pRF_YRN_TST_PARAM_ID", Value = ob.RF_YRN_TST_PARAM_ID},
                     new CommandParameter() {ParameterName = "pTST_PARAM_VAL", Value = ob.TST_PARAM_VAL},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "PKG_KNT_YARN_TEST.SP_KNT_YRN_LOT_TEST_R1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_R1_ID", Value = ob.KNT_YRN_LOT_TEST_R1_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value = ob.KNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pRF_YRN_TST_PARAM_ID", Value = ob.RF_YRN_TST_PARAM_ID},
                     new CommandParameter() {ParameterName = "pTST_PARAM_VAL", Value = ob.TST_PARAM_VAL},
                     new CommandParameter() {ParameterName = "pCOMMENTS", Value = ob.COMMENTS},
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

        public List<KNT_YRN_LOT_TEST_R1Model> SelectAll()
        {
            string sp = "PKG_KNT_YARN_TEST.KNT_YRN_LOT_TEST_R1_Select";
            try
            {
                var obList = new List<KNT_YRN_LOT_TEST_R1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_R1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_TEST_R1Model ob = new KNT_YRN_LOT_TEST_R1Model();
                    ob.KNT_YRN_LOT_TEST_R1_ID = (dr["KNT_YRN_LOT_TEST_R1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_R1_ID"]);
                    ob.KNT_YRN_LOT_TEST_D1_ID = (dr["KNT_YRN_LOT_TEST_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D1_ID"]);
                    ob.RF_YRN_TST_PARAM_ID = (dr["RF_YRN_TST_PARAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_TST_PARAM_ID"]);
                    ob.TST_PARAM_VAL = (dr["TST_PARAM_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TST_PARAM_VAL"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
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
        public List<KNT_YRN_LOT_TEST_R1Model> SelectByID(Int64? pKNT_YRN_LOT_TEST_D1_ID = null)
        {
            string sp = "PKG_KNT_YARN_TEST.KNT_YRN_LOT_TEST_R1_Select";
            try
            {
                var obList = new List<KNT_YRN_LOT_TEST_R1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_D1_ID", Value =pKNT_YRN_LOT_TEST_D1_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_TEST_R1Model ob = new KNT_YRN_LOT_TEST_R1Model();
                    ob.KNT_YRN_LOT_TEST_R1_ID = (dr["KNT_YRN_LOT_TEST_R1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_R1_ID"]);
                    ob.KNT_YRN_LOT_TEST_D1_ID = (dr["KNT_YRN_LOT_TEST_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D1_ID"]);
                    ob.RF_YRN_TST_PARAM_ID = (dr["RF_YRN_TST_PARAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_TST_PARAM_ID"]);
                    ob.TST_PARAM_VAL = (dr["TST_PARAM_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TST_PARAM_VAL"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
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

        public KNT_YRN_LOT_TEST_R1Model Select(long ID)
        {
            string sp = "PKG_KNT_YARN_TEST.KNT_YRN_LOT_TEST_R1_Select";
            try
            {
                var ob = new KNT_YRN_LOT_TEST_R1Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_R1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_LOT_TEST_R1_ID = (dr["KNT_YRN_LOT_TEST_R1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_R1_ID"]);
                    ob.KNT_YRN_LOT_TEST_D1_ID = (dr["KNT_YRN_LOT_TEST_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_TEST_D1_ID"]);
                    ob.RF_YRN_TST_PARAM_ID = (dr["RF_YRN_TST_PARAM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_TST_PARAM_ID"]);
                    ob.TST_PARAM_VAL = (dr["TST_PARAM_VAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TST_PARAM_VAL"]);
                    ob.COMMENTS = (dr["COMMENTS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMMENTS"]);
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
    }
}