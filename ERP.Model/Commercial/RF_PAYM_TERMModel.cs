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
    public class RF_PAYM_TERMModel
    {
        public Int64 RF_PAYM_TERM_ID { get; set; }
        public string PAYM_TERM_CODE { get; set; }
        public string PAYM_TERM_NAME_EN { get; set; }
        public string PAYM_TERM_NAME_BN { get; set; }
        public string PAYM_TERM_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "PKG_CM_REF_TYPE.RF_PAYM_TERM_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_CODE", Value = ob.PAYM_TERM_CODE},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_NAME_EN", Value = ob.PAYM_TERM_NAME_EN},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_NAME_BN", Value = ob.PAYM_TERM_NAME_BN},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_SNAME", Value = ob.PAYM_TERM_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "SP_RF_PAYM_TERM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_CODE", Value = ob.PAYM_TERM_CODE},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_NAME_EN", Value = ob.PAYM_TERM_NAME_EN},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_NAME_BN", Value = ob.PAYM_TERM_NAME_BN},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_SNAME", Value = ob.PAYM_TERM_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
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
            const string sp = "PKG_CM_REF_TYPE.RF_PAYM_TERM_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_CODE", Value = ob.PAYM_TERM_CODE},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_NAME_EN", Value = ob.PAYM_TERM_NAME_EN},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_NAME_BN", Value = ob.PAYM_TERM_NAME_BN},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_SNAME", Value = ob.PAYM_TERM_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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

        public List<RF_PAYM_TERMModel> SelectAll()
        {
            string sp = "PKG_CM_REF_TYPE.RF_PAYM_TERM_SELECT";
            try
            {
                var obList = new List<RF_PAYM_TERMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_PAYM_TERMModel ob = new RF_PAYM_TERMModel();
                    ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                    ob.PAYM_TERM_CODE = (dr["PAYM_TERM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_CODE"]);
                    ob.PAYM_TERM_NAME_EN = (dr["PAYM_TERM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_NAME_EN"]);
                    ob.PAYM_TERM_NAME_BN = (dr["PAYM_TERM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_NAME_BN"]);
                    ob.PAYM_TERM_SNAME = (dr["PAYM_TERM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public RF_PAYM_TERMModel Select(long ID)
        {
            string sp = "Select_RF_PAYM_TERM";
            try
            {
                var ob = new RF_PAYM_TERMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                    ob.PAYM_TERM_CODE = (dr["PAYM_TERM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_CODE"]);
                    ob.PAYM_TERM_NAME_EN = (dr["PAYM_TERM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_NAME_EN"]);
                    ob.PAYM_TERM_NAME_BN = (dr["PAYM_TERM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_NAME_BN"]);
                    ob.PAYM_TERM_SNAME = (dr["PAYM_TERM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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
    }
}