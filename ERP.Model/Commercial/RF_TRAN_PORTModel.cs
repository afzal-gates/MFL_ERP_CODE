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
    public class RF_TRAN_PORTModel
    {
        public Int64 RF_TRAN_PORT_ID { get; set; }
        public string TRAN_PORT_CODE { get; set; }
        public string TRAN_PORT_NAME_EN { get; set; }
        public string TRAN_PORT_NAME_BN { get; set; }
        public string TRAN_PORT_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "PKG_CM_REF_TYPE.RF_TRAN_PORT_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TRAN_PORT_ID", Value = ob.RF_TRAN_PORT_ID},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_CODE", Value = ob.TRAN_PORT_CODE},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_NAME_EN", Value = ob.TRAN_PORT_NAME_EN},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_NAME_BN", Value = ob.TRAN_PORT_NAME_BN},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_SNAME", Value = ob.TRAN_PORT_SNAME},
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
            const string sp = "SP_RF_TRAN_PORT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TRAN_PORT_ID", Value = ob.RF_TRAN_PORT_ID},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_CODE", Value = ob.TRAN_PORT_CODE},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_NAME_EN", Value = ob.TRAN_PORT_NAME_EN},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_NAME_BN", Value = ob.TRAN_PORT_NAME_BN},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_SNAME", Value = ob.TRAN_PORT_SNAME},
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
            const string sp = "PKG_CM_REF_TYPE.RF_TRAN_PORT_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TRAN_PORT_ID", Value = ob.RF_TRAN_PORT_ID},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_CODE", Value = ob.TRAN_PORT_CODE},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_NAME_EN", Value = ob.TRAN_PORT_NAME_EN},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_NAME_BN", Value = ob.TRAN_PORT_NAME_BN},
                     new CommandParameter() {ParameterName = "pTRAN_PORT_SNAME", Value = ob.TRAN_PORT_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
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

        public List<RF_TRAN_PORTModel> SelectAll()
        {
            string sp = "PKG_CM_REF_TYPE.RF_TRAN_PORT_SELECT";
            try
            {
                var obList = new List<RF_TRAN_PORTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TRAN_PORT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_TRAN_PORTModel ob = new RF_TRAN_PORTModel();
                    ob.RF_TRAN_PORT_ID = (dr["RF_TRAN_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_TRAN_PORT_ID"]);
                    ob.TRAN_PORT_CODE = (dr["TRAN_PORT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_PORT_CODE"]);
                    ob.TRAN_PORT_NAME_EN = (dr["TRAN_PORT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_PORT_NAME_EN"]);
                    ob.TRAN_PORT_NAME_BN = (dr["TRAN_PORT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_PORT_NAME_BN"]);
                    ob.TRAN_PORT_SNAME = (dr["TRAN_PORT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_PORT_SNAME"]);
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

        public RF_TRAN_PORTModel Select(long ID)
        {
            string sp = "Select_RF_TRAN_PORT";
            try
            {
                var ob = new RF_TRAN_PORTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TRAN_PORT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_TRAN_PORT_ID = (dr["RF_TRAN_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_TRAN_PORT_ID"]);
                    ob.TRAN_PORT_CODE = (dr["TRAN_PORT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_PORT_CODE"]);
                    ob.TRAN_PORT_NAME_EN = (dr["TRAN_PORT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_PORT_NAME_EN"]);
                    ob.TRAN_PORT_NAME_BN = (dr["TRAN_PORT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_PORT_NAME_BN"]);
                    ob.TRAN_PORT_SNAME = (dr["TRAN_PORT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRAN_PORT_SNAME"]);
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