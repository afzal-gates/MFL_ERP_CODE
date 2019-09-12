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
    public class RF_MAC_PROD_STSModel
    {
        public Int64 RF_MAC_PROD_STS_ID { get; set; }
        public string MAC_PROD_STS_CODE { get; set; }
        public string MAC_PROD_STS_EN_NAME { get; set; }
        public string MAC_PROD_STS_BN_NAME { get; set; }
        public string MAC_PROD_STS_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_RF_MAC_PROD_STS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value = ob.RF_MAC_PROD_STS_ID},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_CODE", Value = ob.MAC_PROD_STS_CODE},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_EN_NAME", Value = ob.MAC_PROD_STS_EN_NAME},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_BN_NAME", Value = ob.MAC_PROD_STS_BN_NAME},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_SNAME", Value = ob.MAC_PROD_STS_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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

        public string Update()
        {
            const string sp = "SP_RF_MAC_PROD_STS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value = ob.RF_MAC_PROD_STS_ID},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_CODE", Value = ob.MAC_PROD_STS_CODE},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_EN_NAME", Value = ob.MAC_PROD_STS_EN_NAME},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_BN_NAME", Value = ob.MAC_PROD_STS_BN_NAME},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_SNAME", Value = ob.MAC_PROD_STS_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
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
            const string sp = "SP_RF_MAC_PROD_STS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value = ob.RF_MAC_PROD_STS_ID},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_CODE", Value = ob.MAC_PROD_STS_CODE},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_EN_NAME", Value = ob.MAC_PROD_STS_EN_NAME},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_BN_NAME", Value = ob.MAC_PROD_STS_BN_NAME},
                     new CommandParameter() {ParameterName = "pMAC_PROD_STS_SNAME", Value = ob.MAC_PROD_STS_SNAME},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
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

        public List<RF_MAC_PROD_STSModel> SelectAll()
        {
            string sp = "pkg_dye_common.rf_mac_prod_sts_select";
            try
            {
                var obList = new List<RF_MAC_PROD_STSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_MAC_PROD_STSModel ob = new RF_MAC_PROD_STSModel();
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);
                    ob.MAC_PROD_STS_CODE = (dr["MAC_PROD_STS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_CODE"]);
                    ob.MAC_PROD_STS_EN_NAME = (dr["MAC_PROD_STS_EN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_EN_NAME"]);
                    ob.MAC_PROD_STS_BN_NAME = (dr["MAC_PROD_STS_BN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_BN_NAME"]);
                    ob.MAC_PROD_STS_SNAME = (dr["MAC_PROD_STS_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public RF_MAC_PROD_STSModel Select(long ID)
        {
            string sp = "pkg_dye_common.rf_mac_prod_sts_select";
            try
            {
                var ob = new RF_MAC_PROD_STSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);
                    ob.MAC_PROD_STS_CODE = (dr["MAC_PROD_STS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_CODE"]);
                    ob.MAC_PROD_STS_EN_NAME = (dr["MAC_PROD_STS_EN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_EN_NAME"]);
                    ob.MAC_PROD_STS_BN_NAME = (dr["MAC_PROD_STS_BN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_BN_NAME"]);
                    ob.MAC_PROD_STS_SNAME = (dr["MAC_PROD_STS_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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