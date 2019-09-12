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
    public class RF_INSURN_COMPModel
    {
        public Int64 RF_INSURN_COMP_ID { get; set; }
        public string INS_COMP_CODE { get; set; }
        public string INS_COMP_NAME_EN { get; set; }
        public string INS_COMP_NAME_BN { get; set; }
        public string INSUR_CVN_NO { get; set; }
        public DateTime INSUR_DT { get; set; }
        public string ADDRESS_EN { get; set; }
        public string ADDRESS_BN { get; set; }
        public string PHONE_NO { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.RF_INSURN_COMP_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_INSURN_COMP_ID", Value = ob.RF_INSURN_COMP_ID},
                     new CommandParameter() {ParameterName = "pINS_COMP_CODE", Value = ob.INS_COMP_CODE},
                     new CommandParameter() {ParameterName = "pINS_COMP_NAME_EN", Value = ob.INS_COMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pINS_COMP_NAME_BN", Value = ob.INS_COMP_NAME_BN},
                     new CommandParameter() {ParameterName = "pINSUR_CVN_NO", Value = ob.INSUR_CVN_NO},
                     new CommandParameter() {ParameterName = "pINSUR_DT", Value = ob.INSUR_DT},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pPHONE_NO", Value = ob.PHONE_NO},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
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
            const string sp = "pkg_common.RF_INSURN_COMP_";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_INSURN_COMP_ID", Value = ob.RF_INSURN_COMP_ID},
                     new CommandParameter() {ParameterName = "pINS_COMP_CODE", Value = ob.INS_COMP_CODE},
                     new CommandParameter() {ParameterName = "pINS_COMP_NAME_EN", Value = ob.INS_COMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pINS_COMP_NAME_BN", Value = ob.INS_COMP_NAME_BN},
                     new CommandParameter() {ParameterName = "pINSUR_CVN_NO", Value = ob.INSUR_CVN_NO},
                     new CommandParameter() {ParameterName = "pINSUR_DT", Value = ob.INSUR_DT},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pPHONE_NO", Value = ob.PHONE_NO},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
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
            const string sp = "pkg_common.RF_INSURN_COMP_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_INSURN_COMP_ID", Value = ob.RF_INSURN_COMP_ID},
                     new CommandParameter() {ParameterName = "pINS_COMP_CODE", Value = ob.INS_COMP_CODE},
                     new CommandParameter() {ParameterName = "pINS_COMP_NAME_EN", Value = ob.INS_COMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pINS_COMP_NAME_BN", Value = ob.INS_COMP_NAME_BN},
                     new CommandParameter() {ParameterName = "pINSUR_CVN_NO", Value = ob.INSUR_CVN_NO},
                     new CommandParameter() {ParameterName = "pINSUR_DT", Value = ob.INSUR_DT},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pPHONE_NO", Value = ob.PHONE_NO},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
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

        public List<RF_INSURN_COMPModel> SelectAll()
        {
            string sp = "pkg_common.RF_INSURN_COMP_Select";
            try
            {
                var obList = new List<RF_INSURN_COMPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_INSURN_COMP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_INSURN_COMPModel ob = new RF_INSURN_COMPModel();
                    ob.RF_INSURN_COMP_ID = (dr["RF_INSURN_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INSURN_COMP_ID"]);
                    ob.INS_COMP_CODE = (dr["INS_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INS_COMP_CODE"]);
                    ob.INS_COMP_NAME_EN = (dr["INS_COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INS_COMP_NAME_EN"]);
                    ob.INS_COMP_NAME_BN = (dr["INS_COMP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INS_COMP_NAME_BN"]);
                    ob.INSUR_CVN_NO = (dr["INSUR_CVN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSUR_CVN_NO"]);
                    ob.INSUR_DT = (dr["INSUR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INSUR_DT"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.PHONE_NO = (dr["PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_NO"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
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

        public RF_INSURN_COMPModel Select(long ID)
        {
            string sp = "pkg_common.RF_INSURN_COMP_Select";
            try
            {
                var ob = new RF_INSURN_COMPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_INSURN_COMP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_INSURN_COMP_ID = (dr["RF_INSURN_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INSURN_COMP_ID"]);
                    ob.INS_COMP_CODE = (dr["INS_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INS_COMP_CODE"]);
                    ob.INS_COMP_NAME_EN = (dr["INS_COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INS_COMP_NAME_EN"]);
                    ob.INS_COMP_NAME_BN = (dr["INS_COMP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INS_COMP_NAME_BN"]);
                    ob.INSUR_CVN_NO = (dr["INSUR_CVN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSUR_CVN_NO"]);
                    ob.INSUR_DT = (dr["INSUR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INSUR_DT"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.PHONE_NO = (dr["PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_NO"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
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