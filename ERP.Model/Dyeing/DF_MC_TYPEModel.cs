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




    public class DF_MC_TYPEModel
    {
        public Int64 DF_MC_TYPE_ID { get; set; }
        public string DF_MC_TYPE_CODE { get; set; }
        public string DF_MC_TYPE_NAME_EN { get; set; }
        public string DF_MC_TYPE_NAME_BN { get; set; }
        public Int64 DF_PROC_TYPE_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LK_PROC_SUB_GRP_ID { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_MC_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_ID", Value = ob.DF_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_CODE", Value = ob.DF_MC_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_NAME_EN", Value = ob.DF_MC_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_NAME_BN", Value = ob.DF_MC_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
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
            const string sp = "SP_DF_MC_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_ID", Value = ob.DF_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_CODE", Value = ob.DF_MC_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_NAME_EN", Value = ob.DF_MC_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_NAME_BN", Value = ob.DF_MC_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
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
            const string sp = "SP_DF_MC_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_ID", Value = ob.DF_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_CODE", Value = ob.DF_MC_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_NAME_EN", Value = ob.DF_MC_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_NAME_BN", Value = ob.DF_MC_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
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

        public List<DF_MC_TYPEModel> SelectAll()
        {
            string sp = "PKG_DYE_COMMON.DF_MC_TYPE_SELECT";
            try
            {
                var obList = new List<DF_MC_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_MC_TYPEModel ob = new DF_MC_TYPEModel();
                    ob.DF_MC_TYPE_ID = (dr["DF_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MC_TYPE_ID"]);
                    ob.DF_MC_TYPE_CODE = (dr["DF_MC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_TYPE_CODE"]);
                    ob.DF_MC_TYPE_NAME_EN = (dr["DF_MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_TYPE_NAME_EN"]);
                    ob.DF_MC_TYPE_NAME_BN = (dr["DF_MC_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_TYPE_NAME_BN"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
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

        public DF_MC_TYPEModel Select(Int64? ID)
        {
            string sp = "PKG_DYE_COMMON.DF_MC_TYPE_SELECT";
            try
            {
                var ob = new DF_MC_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_MC_TYPE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_MC_TYPE_ID = (dr["DF_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MC_TYPE_ID"]);
                    ob.DF_MC_TYPE_CODE = (dr["DF_MC_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_TYPE_CODE"]);
                    ob.DF_MC_TYPE_NAME_EN = (dr["DF_MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_TYPE_NAME_EN"]);
                    ob.DF_MC_TYPE_NAME_BN = (dr["DF_MC_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_MC_TYPE_NAME_BN"]);
                    ob.DF_PROC_TYPE_ID = (dr["DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_TYPE_ID"]);
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