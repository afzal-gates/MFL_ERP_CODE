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
    public class SCM_FUND_REQ_DModel
    {
        public Int64 SCM_FUND_REQ_D_ID { get; set; }
        public Int64 SCM_FUND_REQ_H_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 SCM_PURC_REQ_H_ID { get; set; }
        public Decimal RQD_AMT { get; set; }
        public Decimal APRV_AMT { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_SCM_FUND_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_D_ID", Value = ob.SCM_FUND_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value = ob.SCM_FUND_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRQD_AMT", Value = ob.RQD_AMT},
                     new CommandParameter() {ParameterName = "pAPRV_AMT", Value = ob.APRV_AMT},
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
            const string sp = "SP_SCM_FUND_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_D_ID", Value = ob.SCM_FUND_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value = ob.SCM_FUND_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRQD_AMT", Value = ob.RQD_AMT},
                     new CommandParameter() {ParameterName = "pAPRV_AMT", Value = ob.APRV_AMT},
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
            const string sp = "SP_SCM_FUND_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_D_ID", Value = ob.SCM_FUND_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value = ob.SCM_FUND_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRQD_AMT", Value = ob.RQD_AMT},
                     new CommandParameter() {ParameterName = "pAPRV_AMT", Value = ob.APRV_AMT},
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

        public List<SCM_FUND_REQ_DModel> SelectAll()
        {
            string sp = "Select_SCM_FUND_REQ_D";
            try
            {
                var obList = new List<SCM_FUND_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_FUND_REQ_DModel ob = new SCM_FUND_REQ_DModel();
                    ob.SCM_FUND_REQ_D_ID = (dr["SCM_FUND_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_FUND_REQ_D_ID"]);
                    ob.SCM_FUND_REQ_H_ID = (dr["SCM_FUND_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_FUND_REQ_H_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.RQD_AMT = (dr["RQD_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_AMT"]);
                    ob.APRV_AMT = (dr["APRV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["APRV_AMT"]);
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


        public List<SCM_FUND_REQ_DModel> SelectByID(Int64? pSCM_FUND_REQ_H_ID = null)
        {
            string sp = "PKG_SCM_PURCHASE.SCM_FUND_REQ_D_Select";
            try
            {
                var obList = new List<SCM_FUND_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value =pSCM_FUND_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_FUND_REQ_DModel ob = new SCM_FUND_REQ_DModel();
                    ob.SCM_FUND_REQ_D_ID = (dr["SCM_FUND_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_FUND_REQ_D_ID"]);
                    ob.SCM_FUND_REQ_H_ID = (dr["SCM_FUND_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_FUND_REQ_H_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.RQD_AMT = (dr["RQD_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_AMT"]);
                    ob.APRV_AMT = (dr["APRV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["APRV_AMT"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    ob.ITEM_NAME_LIST = (dr["ITEM_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_LIST"]);

                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    //ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SCM_FUND_REQ_DModel Select(long ID)
        {
            string sp = "Select_SCM_FUND_REQ_D";
            try
            {
                var ob = new SCM_FUND_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_FUND_REQ_D_ID = (dr["SCM_FUND_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_FUND_REQ_D_ID"]);
                    ob.SCM_FUND_REQ_H_ID = (dr["SCM_FUND_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_FUND_REQ_H_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.RQD_AMT = (dr["RQD_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_AMT"]);
                    ob.APRV_AMT = (dr["APRV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["APRV_AMT"]);
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

        public string PURC_REQ_NO { get; set; }

        public DateTime PURC_REQ_DT { get; set; }

        public string ITEM_NAME_LIST { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }
    }
}