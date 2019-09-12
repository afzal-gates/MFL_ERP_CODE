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
    public class SC_MAP_STR_USRModel
    {
        public Int64 SC_MAP_STR_USR_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }

        public string STORE_CODE { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string STORE_NAME_BN { get; set; }
        public Int64 LK_WH_TYPE_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public string ADDRESS_EN { get; set; }
        public string OFFICE_NAME_EN { get; set; }
        public string STORE_TYPE_NAME { get; set; }

        public string XML_MAP_D { get; set; }

        public string Save()
        {
            const string sp = "PKG_SCM_USER_MAP.SC_MAP_STR_USR_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_STR_USR_ID", Value = ob.SC_MAP_STR_USR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pXML_MAP_D", Value = ob.XML_MAP_D},
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
            const string sp = "SP_SC_MAP_STR_USR";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_STR_USR_ID", Value = ob.SC_MAP_STR_USR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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
            const string sp = "SP_SC_MAP_STR_USR";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_STR_USR_ID", Value = ob.SC_MAP_STR_USR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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

        public List<SC_MAP_STR_USRModel> SelectAll()
        {
            string sp = "Select_SC_MAP_STR_USR";
            try
            {
                var obList = new List<SC_MAP_STR_USRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_STR_USR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SC_MAP_STR_USRModel ob = new SC_MAP_STR_USRModel();
                    ob.SC_MAP_STR_USR_ID = (dr["SC_MAP_STR_USR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_STR_USR_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SC_MAP_STR_USRModel> SelectByID(Int64? pSC_USER_ID = null)
        {
            string sp = "PKG_SCM_USER_MAP.SC_MAP_STR_USR_SELECT";
            try
            {
                var obList = new List<SC_MAP_STR_USRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SC_MAP_STR_USRModel ob = new SC_MAP_STR_USRModel();
                    ob.SC_MAP_STR_USR_ID = (dr["SC_MAP_STR_USR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_STR_USR_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);

                    ob.STORE_CODE = (dr["STORE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_CODE"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.STORE_NAME_BN = (dr["STORE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_BN"]);
                    //ob.LK_WH_TYPE_ID = (dr["LK_WH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_WH_TYPE_ID"]);
                    //ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.STORE_TYPE_NAME = (dr["STORE_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_TYPE_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SC_MAP_STR_USRModel Select(Int64? pSCM_USER_ID = null)
        {
            string sp = "PKG_SCM_USER_MAP.SC_MAP_STR_USR_SELECT";
            try
            {
                var ob = new SC_MAP_STR_USRModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_MAP_STR_USR_ID", Value =pSCM_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SC_MAP_STR_USR_ID = (dr["SC_MAP_STR_USR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MAP_STR_USR_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);

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