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
    public class KNT_MAP_USR_COMPModel
    {
        public Int64 KNT_MAP_USR_COMP_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public string OFFICE_CODE { get; set; }
        public Int64 HR_COMPANY_GRP_ID { get; set; }
        public string OFFICE_NAME_EN { get; set; }
        public string OFFICE_NAME_BN { get; set; }
        public string OFFICE_SNAME { get; set; }
        public string OFFICE_DESC { get; set; }
        public Int64? LK_OFF_TYPE_ID { get; set; }
        public string ADDRESS_EN { get; set; }
        public string ADDRESS_BN { get; set; }
        public string POST_CODE { get; set; }
        public string PO_NAME_EN { get; set; }
        public string PO_NAME_BN { get; set; }
        public string PO_BOX_NO { get; set; }
        public Int64 HR_TIMEZONE_ID { get; set; }
        public Int64 HR_GEO_DISTRICT_ID { get; set; }
        public Int64 HR_COUNTRY_ID { get; set; }
        public string PHONE_NO { get; set; }
        public string PHONE_EXT { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public int VERSION_NO { get; set; }
        public string OFFICE_TYPE_NAME_EN { get; set; }
        
        public string XML_MAP_D { get; set; }


        public string Save()
        {
            const string sp = "PKG_SCM_USER_MAP.KNT_MAP_USR_COMP_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MAP_USR_COMP_ID", Value = ob.KNT_MAP_USR_COMP_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
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
            const string sp = "SP_KNT_MAP_USR_COMP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MAP_USR_COMP_ID", Value = ob.KNT_MAP_USR_COMP_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
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
            const string sp = "SP_KNT_MAP_USR_COMP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MAP_USR_COMP_ID", Value = ob.KNT_MAP_USR_COMP_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
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

        public List<KNT_MAP_USR_COMPModel> SelectAll()
        {
            string sp = "Select_KNT_MAP_USR_COMP";
            try
            {
                var obList = new List<KNT_MAP_USR_COMPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MAP_USR_COMP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MAP_USR_COMPModel ob = new KNT_MAP_USR_COMPModel();
                    ob.KNT_MAP_USR_COMP_ID = (dr["KNT_MAP_USR_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MAP_USR_COMP_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_MAP_USR_COMPModel> SelectByID(Int64? pSC_USER_ID=null)
        {
            string sp = "PKG_SCM_USER_MAP.KNT_MAP_USR_COMP_Select";
            try
            {
                var obList = new List<KNT_MAP_USR_COMPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MAP_USR_COMPModel ob = new KNT_MAP_USR_COMPModel();
                    ob.KNT_MAP_USR_COMP_ID = (dr["KNT_MAP_USR_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MAP_USR_COMP_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.OFFICE_CODE = (dr["OFFICE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_CODE"]);
                    ob.HR_COMPANY_GRP_ID = (dr["HR_COMPANY_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_GRP_ID"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.OFFICE_NAME_BN = (dr["OFFICE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_BN"]);
                    ob.OFFICE_SNAME = (dr["OFFICE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_SNAME"]);
                    ob.OFFICE_DESC = (dr["OFFICE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_DESC"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.LK_OFF_TYPE_ID = (dr["LK_OFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_OFF_TYPE_ID"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_NAME_EN = (dr["PO_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_NAME_EN"]);
                    ob.PO_NAME_BN = (dr["PO_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_NAME_BN"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.HR_TIMEZONE_ID = (dr["HR_TIMEZONE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TIMEZONE_ID"]);
                    ob.HR_GEO_DISTRICT_ID = (dr["HR_GEO_DISTRICT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GEO_DISTRICT_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.PHONE_NO = (dr["PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_NO"]);
                    ob.PHONE_EXT = (dr["PHONE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_EXT"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_MAP_USR_COMPModel Select(long ID)
        {
            string sp = "Select_KNT_MAP_USR_COMP";
            try
            {
                var ob = new KNT_MAP_USR_COMPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MAP_USR_COMP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_MAP_USR_COMP_ID = (dr["KNT_MAP_USR_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MAP_USR_COMP_ID"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string COMP_NAME_EN { get; set; }
    }
}