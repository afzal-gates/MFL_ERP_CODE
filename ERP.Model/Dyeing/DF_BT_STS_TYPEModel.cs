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
    public class DF_BT_STS_TYPEModel
    {
        public Int64 DF_BT_STS_TYPE_ID { get; set; }
        public string BT_STS_TYP_CODE { get; set; }
        public string BT_STS_TYP_NAME { get; set; }
        public string RGB_COL_CODE { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_BT_STS_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBT_STS_TYP_CODE", Value = ob.BT_STS_TYP_CODE},
                     new CommandParameter() {ParameterName = "pBT_STS_TYP_NAME", Value = ob.BT_STS_TYP_NAME},
                     new CommandParameter() {ParameterName = "pRGB_COL_CODE", Value = ob.RGB_COL_CODE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_DF_BT_STS_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBT_STS_TYP_CODE", Value = ob.BT_STS_TYP_CODE},
                     new CommandParameter() {ParameterName = "pBT_STS_TYP_NAME", Value = ob.BT_STS_TYP_NAME},
                     new CommandParameter() {ParameterName = "pRGB_COL_CODE", Value = ob.RGB_COL_CODE},
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
            const string sp = "SP_DF_BT_STS_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBT_STS_TYP_CODE", Value = ob.BT_STS_TYP_CODE},
                     new CommandParameter() {ParameterName = "pBT_STS_TYP_NAME", Value = ob.BT_STS_TYP_NAME},
                     new CommandParameter() {ParameterName = "pRGB_COL_CODE", Value = ob.RGB_COL_CODE},
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

        public List<DF_BT_STS_TYPEModel> SelectAll()
        {
            string sp = "PKG_DYE_COMMON.DF_BT_STS_TYPE_SELECT";
            try
            {
                var obList = new List<DF_BT_STS_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_STS_TYPEModel ob = new DF_BT_STS_TYPEModel();
                    ob.DF_BT_STS_TYPE_ID = (dr["DF_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_STS_TYPE_ID"]);
                    ob.BT_STS_TYP_CODE = (dr["BT_STS_TYP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_CODE"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
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

        public DF_BT_STS_TYPEModel Select(long ID)
        {
            string sp = "PKG_DYE_COMMON.DF_BT_STS_TYPE_SELECT";
            try
            {
                var ob = new DF_BT_STS_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_STS_TYPE_ID = (dr["DF_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_STS_TYPE_ID"]);
                    ob.BT_STS_TYP_CODE = (dr["BT_STS_TYP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_CODE"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
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