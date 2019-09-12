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
    public class HR_COUNTRYModel
    {
        public Int64 HR_COUNTRY_ID { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string COUNTRY_NAME_EN { get; set; }
        public string COUNTRY_NAME_BN { get; set; }
        public Int64 LK_REGION_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string COUNTRY_SNAME { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.hr_country_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pCOUNTRY_CODE", Value = ob.COUNTRY_CODE},
                     new CommandParameter() {ParameterName = "pCOUNTRY_NAME_EN", Value = ob.COUNTRY_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOUNTRY_NAME_BN", Value = ob.COUNTRY_NAME_BN},
                     new CommandParameter() {ParameterName = "pLK_REGION_ID", Value = ob.LK_REGION_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     //new CommandParameter() {ParameterName = "pCOUNTRY_SNAME", Value = ob.COUNTRY_SNAME},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opHR_COUNTRY_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "SP_HR_COUNTRY";
            string vMsg = "";
            var ob = this;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pCOUNTRY_CODE", Value = ob.COUNTRY_CODE},
                     new CommandParameter() {ParameterName = "pCOUNTRY_NAME_EN", Value = ob.COUNTRY_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOUNTRY_NAME_BN", Value = ob.COUNTRY_NAME_BN},
                     new CommandParameter() {ParameterName = "pLK_REGION_ID", Value = ob.LK_REGION_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pCOUNTRY_SNAME", Value = ob.COUNTRY_SNAME},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                string jsonString = "{";
                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonString += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
                }
                jsonString += "}";
                return jsonString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Delete()
        {
            const string sp = "SP_HR_COUNTRY";
            string vMsg = "";
            var ob = this;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pCOUNTRY_CODE", Value = ob.COUNTRY_CODE},
                     new CommandParameter() {ParameterName = "pCOUNTRY_NAME_EN", Value = ob.COUNTRY_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOUNTRY_NAME_BN", Value = ob.COUNTRY_NAME_BN},
                     new CommandParameter() {ParameterName = "pLK_REGION_ID", Value = ob.LK_REGION_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pCOUNTRY_SNAME", Value = ob.COUNTRY_SNAME},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                string jsonString = "{";
                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonString += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
                }
                jsonString += "}";
                return jsonString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_COUNTRYModel> SelectAll()
        {
            string sp = "pkg_common.hr_country_select";
            try
            {
                var obList = new List<HR_COUNTRYModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = 0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_COUNTRYModel ob = new HR_COUNTRYModel();
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.COUNTRY_CODE = (dr["COUNTRY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_CODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.COUNTRY_NAME_BN = (dr["COUNTRY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_BN"]);
                    ob.REGION_NAME_EN = (dr["REGION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REGION_NAME_EN"]);
                    ob.LK_REGION_ID = (dr["LK_REGION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REGION_ID"]);
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

        public HR_COUNTRYModel Select(long ID)
        {
            string sp = "Select_HR_COUNTRY";
            try
            {
                var ob = new HR_COUNTRYModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.COUNTRY_CODE = (dr["COUNTRY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_CODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.COUNTRY_NAME_BN = (dr["COUNTRY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_BN"]);
                    ob.LK_REGION_ID = (dr["LK_REGION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REGION_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.COUNTRY_SNAME = (dr["COUNTRY_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_SNAME"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string REGION_NAME_EN { get; set; }
    }
}