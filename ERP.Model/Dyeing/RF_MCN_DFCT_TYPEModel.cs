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
    public class RF_MCN_DFCT_TYPEModel
    {
        public Int64 RF_MCN_DFCT_TYPE_ID { get; set; }
        public string DFCT_TYPE_CODE { get; set; }
        public string DFCT_TYPE_NAME_EN { get; set; }
        public string DFCT_TYPE_NAME_BN { get; set; }
        public string DFCT_TYPE_SNAME { get; set; }
        public Int64 LK_MC_MAINT_TYP_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_RF_MCN_DFCT_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MCN_DFCT_TYPE_ID", Value = ob.RF_MCN_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_CODE", Value = ob.DFCT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_NAME_EN", Value = ob.DFCT_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_NAME_BN", Value = ob.DFCT_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_SNAME", Value = ob.DFCT_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pLK_MC_MAINT_TYP_ID", Value = ob.LK_MC_MAINT_TYP_ID},
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
            const string sp = "SP_RF_MCN_DFCT_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MCN_DFCT_TYPE_ID", Value = ob.RF_MCN_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_CODE", Value = ob.DFCT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_NAME_EN", Value = ob.DFCT_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_NAME_BN", Value = ob.DFCT_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_SNAME", Value = ob.DFCT_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pLK_MC_MAINT_TYP_ID", Value = ob.LK_MC_MAINT_TYP_ID},
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
            const string sp = "SP_RF_MCN_DFCT_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MCN_DFCT_TYPE_ID", Value = ob.RF_MCN_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_CODE", Value = ob.DFCT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_NAME_EN", Value = ob.DFCT_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_NAME_BN", Value = ob.DFCT_TYPE_NAME_BN},
                     new CommandParameter() {ParameterName = "pDFCT_TYPE_SNAME", Value = ob.DFCT_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pLK_MC_MAINT_TYP_ID", Value = ob.LK_MC_MAINT_TYP_ID},
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

        public List<RF_MCN_DFCT_TYPEModel> SelectAll(string pRF_RESP_DEPT_LST = null)
        {
            string sp = "PKG_DYE_MC_MAINTENANCE.RF_MCN_DFCT_TYPE_SELECT";
            try
            {
                var obList = new List<RF_MCN_DFCT_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MCN_DFCT_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_LST", Value =pRF_RESP_DEPT_LST},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_MCN_DFCT_TYPEModel ob = new RF_MCN_DFCT_TYPEModel();
                    ob.RF_MCN_DFCT_TYPE_ID = (dr["RF_MCN_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MCN_DFCT_TYPE_ID"]);
                    ob.DFCT_TYPE_CODE = (dr["DFCT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_CODE"]);
                    ob.DFCT_TYPE_NAME_EN = (dr["DFCT_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_NAME_EN"]);
                    ob.DFCT_TYPE_NAME_BN = (dr["DFCT_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_NAME_BN"]);
                    ob.DFCT_TYPE_SNAME = (dr["DFCT_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_SNAME"]);
                    //ob.LK_MC_MAINT_TYP_ID = (dr["LK_MC_MAINT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_MAINT_TYP_ID"]);
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

        public RF_MCN_DFCT_TYPEModel Select(long ID)
        {
            string sp = "Select_RF_MCN_DFCT_TYPE";
            try
            {
                var ob = new RF_MCN_DFCT_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_MCN_DFCT_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_MCN_DFCT_TYPE_ID = (dr["RF_MCN_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MCN_DFCT_TYPE_ID"]);
                    ob.DFCT_TYPE_CODE = (dr["DFCT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_CODE"]);
                    ob.DFCT_TYPE_NAME_EN = (dr["DFCT_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_NAME_EN"]);
                    ob.DFCT_TYPE_NAME_BN = (dr["DFCT_TYPE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_NAME_BN"]);
                    ob.DFCT_TYPE_SNAME = (dr["DFCT_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_SNAME"]);
                    ob.LK_MC_MAINT_TYP_ID = (dr["LK_MC_MAINT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_MAINT_TYP_ID"]);
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