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
    public class RF_GMT_ASNModel
    {
        public Int64 RF_GMT_ASN_ID { get; set; }
        public string GMT_ASN_CODE { get; set; }
        public string GMT_ASN_NAME_EN { get; set; }
        public string GMT_ASN_NAME_BN { get; set; }
        public string GMT_ASN_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "PKG_CM_REF_TYPE.RF_GMT_ASN_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GMT_ASN_ID", Value = ob.RF_GMT_ASN_ID},
                     new CommandParameter() {ParameterName = "pGMT_ASN_CODE", Value = ob.GMT_ASN_CODE},
                     new CommandParameter() {ParameterName = "pGMT_ASN_NAME_EN", Value = ob.GMT_ASN_NAME_EN},
                     new CommandParameter() {ParameterName = "pGMT_ASN_NAME_BN", Value = ob.GMT_ASN_NAME_BN},
                     new CommandParameter() {ParameterName = "pGMT_ASN_SNAME", Value = ob.GMT_ASN_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value  = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value  = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "SP_RF_GMT_ASN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GMT_ASN_ID", Value = ob.RF_GMT_ASN_ID},
                     new CommandParameter() {ParameterName = "pGMT_ASN_CODE", Value = ob.GMT_ASN_CODE},
                     new CommandParameter() {ParameterName = "pGMT_ASN_NAME_EN", Value = ob.GMT_ASN_NAME_EN},
                     new CommandParameter() {ParameterName = "pGMT_ASN_NAME_BN", Value = ob.GMT_ASN_NAME_BN},
                     new CommandParameter() {ParameterName = "pGMT_ASN_SNAME", Value = ob.GMT_ASN_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value  = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value  = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_CM_REF_TYPE.RF_GMT_ASN_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GMT_ASN_ID", Value = ob.RF_GMT_ASN_ID},
                     new CommandParameter() {ParameterName = "pGMT_ASN_CODE", Value = ob.GMT_ASN_CODE},
                     new CommandParameter() {ParameterName = "pGMT_ASN_NAME_EN", Value = ob.GMT_ASN_NAME_EN},
                     new CommandParameter() {ParameterName = "pGMT_ASN_NAME_BN", Value = ob.GMT_ASN_NAME_BN},
                     new CommandParameter() {ParameterName = "pGMT_ASN_SNAME", Value = ob.GMT_ASN_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value  = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value  = HttpContext.Current.Session["multiScUserId"]},
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

        public List<RF_GMT_ASNModel> SelectAll()
        {
            string sp = "PKG_CM_REF_TYPE.RF_GMT_ASN_SELECT";
            try
            {
                var obList = new List<RF_GMT_ASNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GMT_ASN_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_GMT_ASNModel ob = new RF_GMT_ASNModel();
                    ob.RF_GMT_ASN_ID = (dr["RF_GMT_ASN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GMT_ASN_ID"]);
                    ob.GMT_ASN_CODE = (dr["GMT_ASN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_CODE"]);
                    ob.GMT_ASN_NAME_EN = (dr["GMT_ASN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_NAME_EN"]);
                    ob.GMT_ASN_NAME_BN = (dr["GMT_ASN_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_NAME_BN"]);
                    ob.GMT_ASN_SNAME = (dr["GMT_ASN_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.ADDRESS = (dr["ADDRESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_GMT_ASNModel Select(long ID)
        {
            string sp = "Select_RF_GMT_ASN";
            try
            {
                var ob = new RF_GMT_ASNModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GMT_ASN_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_GMT_ASN_ID = (dr["RF_GMT_ASN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GMT_ASN_ID"]);
                    ob.GMT_ASN_CODE = (dr["GMT_ASN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_CODE"]);
                    ob.GMT_ASN_NAME_EN = (dr["GMT_ASN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_NAME_EN"]);
                    ob.GMT_ASN_NAME_BN = (dr["GMT_ASN_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_NAME_BN"]);
                    ob.GMT_ASN_SNAME = (dr["GMT_ASN_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_SNAME"]);
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

        public string ADDRESS { get; set; }
    }
}