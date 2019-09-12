using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class HrCompanyModel
    {
        public Int64 HR_COMPANY_ID { get; set; }
        public string COMP_CODE { get; set; }

        [Required(ErrorMessage = "Please input company name [E].")]
        public string COMP_NAME_EN { get; set; }
        [Required]
        public string COMP_NAME_BN { get; set; }

        public string COMP_DESC { get; set; }
        public Int64 HR_COMPANY_GRP_ID { get; set; }

        [Required]
        public string COMP_SNAME { get; set; }
        public Int64? LK_COMP_TYPE_ID { get; set; }
        public Int64? LK_BUS_TYPE_ID { get; set; }

        [Required]
        public string VAT_REG_NO { get; set; }
        public Int64 YEAR_ESTD { get; set; }
        public string OLD_COMP_CODE { get; set; }
        public string COMP_WEB { get; set; }
        //string COMP_LOGO {get;set;}
        public string IS_ACTIVE { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public int LAST_UPDATE_LOGIN { get; set; }
        public int VERSION_NO { get; set; }


        public string COMP_TYPE_NAME { get; set; }
        public string COMP_BUS_TYPE_NAME { get; set; }
        public Int64? HR_OFFICE_ID { get; set; }
        public string OFFICE_NAME_EN { get; set; }



        public List<HrCompanyModel> CompanyListData()
        {
            string sp = "pkg_hr.hr_company_select";
            try
            {
                var obList = new List<HrCompanyModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrCompanyModel ob = new HrCompanyModel();
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.HR_COMPANY_GRP_ID = (dr["HR_COMPANY_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_GRP_ID"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_NAME_BN = (dr["COMP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_BN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);
                    ob.COMP_DESC = (dr["COMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_DESC"]);
                    ob.COMP_WEB = (dr["COMP_WEB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_WEB"]);
                    ob.LK_COMP_TYPE_ID = (dr["LK_COMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COMP_TYPE_ID"]);
                    ob.LK_BUS_TYPE_ID = (dr["LK_BUS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BUS_TYPE_ID"]);
                    ob.OLD_COMP_CODE = (dr["OLD_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OLD_COMP_CODE"]);
                    ob.VAT_REG_NO = (dr["VAT_REG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VAT_REG_NO"]);
                    ob.YEAR_ESTD = (dr["YEAR_ESTD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YEAR_ESTD"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.COMP_TYPE_NAME = (dr["COMP_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_TYPE_NAME"]);
                    ob.COMP_BUS_TYPE_NAME = (dr["COMP_BUS_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_BUS_TYPE_NAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrCompanyModel> CompanyListDataForKnit()
        {
            string sp = "pkg_hr.hr_company_select";
            try
            {
                var obList = new List<HrCompanyModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrCompanyModel ob = new HrCompanyModel();
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.HR_COMPANY_GRP_ID = (dr["HR_COMPANY_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_GRP_ID"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_NAME_BN = (dr["COMP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_BN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);
                    ob.COMP_DESC = (dr["COMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_DESC"]);
                    ob.COMP_WEB = (dr["COMP_WEB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_WEB"]);
                    ob.LK_COMP_TYPE_ID = (dr["LK_COMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COMP_TYPE_ID"]);
                    ob.LK_BUS_TYPE_ID = (dr["LK_BUS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BUS_TYPE_ID"]);
                    ob.OLD_COMP_CODE = (dr["OLD_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OLD_COMP_CODE"]);
                    ob.VAT_REG_NO = (dr["VAT_REG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VAT_REG_NO"]);
                    ob.YEAR_ESTD = (dr["YEAR_ESTD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YEAR_ESTD"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.COMP_TYPE_NAME = (dr["COMP_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_TYPE_NAME"]);
                    ob.COMP_BUS_TYPE_NAME = (dr["COMP_BUS_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_BUS_TYPE_NAME"]);

                    if ((dr["HR_OFFICE_ID"] != DBNull.Value))
                        ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Save()
        {
            const string sp = "pkg_hr.hr_company_insert";
            var ob = this;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_GRP_ID", Value = ob.HR_COMPANY_GRP_ID},
                    new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                    new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value = ob.COMP_NAME_EN},
                    new CommandParameter() {ParameterName = "pCOMP_NAME_BN", Value = ob.COMP_NAME_BN},
                    new CommandParameter() {ParameterName = "pCOMP_SNAME", Value = ob.COMP_SNAME},
                    new CommandParameter() {ParameterName = "pCOMP_DESC", Value = ob.COMP_DESC},
                    new CommandParameter() {ParameterName = "pCOMP_WEB", Value = ob.COMP_WEB},
                    new CommandParameter() {ParameterName = "pVAT_REG_NO", Value = ob.VAT_REG_NO},
                    new CommandParameter() {ParameterName = "pYEAR_ESTD", Value = ob.YEAR_ESTD},
                    new CommandParameter() {ParameterName = "pLK_COMP_TYPE_ID", Value = (ob.LK_COMP_TYPE_ID != 0 || ob.LK_COMP_TYPE_ID == null) ? ob.LK_COMP_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pLK_BUS_TYPE_ID", Value =(ob.LK_BUS_TYPE_ID != 0 || ob.LK_BUS_TYPE_ID == null) ? ob.LK_BUS_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }

        public string Update()
        {
            const string sp = "pkg_hr.hr_company_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_GRP_ID", Value = ob.HR_COMPANY_GRP_ID},
                    new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                    new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value = ob.COMP_NAME_EN},
                    new CommandParameter() {ParameterName = "pCOMP_NAME_BN", Value = ob.COMP_NAME_BN},
                    new CommandParameter() {ParameterName = "pCOMP_SNAME", Value = ob.COMP_SNAME},
                    new CommandParameter() {ParameterName = "pCOMP_DESC", Value = ob.COMP_DESC},
                    new CommandParameter() {ParameterName = "pCOMP_WEB", Value = ob.COMP_WEB},
                    new CommandParameter() {ParameterName = "pVAT_REG_NO", Value = ob.VAT_REG_NO},
                    new CommandParameter() {ParameterName = "pYEAR_ESTD", Value = ob.YEAR_ESTD},
                    new CommandParameter() {ParameterName = "pLK_COMP_TYPE_ID", Value = (ob.LK_COMP_TYPE_ID != 0 || ob.LK_COMP_TYPE_ID == null) ? ob.LK_COMP_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pLK_BUS_TYPE_ID", Value =(ob.LK_BUS_TYPE_ID != 0 || ob.LK_BUS_TYPE_ID == null) ? ob.LK_BUS_TYPE_ID : null},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE == null ? "N" : "Y"},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }

        public HrCompanyModel Select(long ID)
        {
            string sp = "pkg_hr.hr_company_select";

            try
            {
                var ob = new HrCompanyModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ID}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.HR_COMPANY_GRP_ID = (dr["HR_COMPANY_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_GRP_ID"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_NAME_BN = (dr["COMP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_BN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);
                    ob.COMP_DESC = (dr["COMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_DESC"]);
                    ob.COMP_WEB = (dr["COMP_WEB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_WEB"]);
                    ob.LK_COMP_TYPE_ID = (dr["LK_COMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COMP_TYPE_ID"]);
                    ob.LK_BUS_TYPE_ID = (dr["LK_BUS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BUS_TYPE_ID"]);
                    ob.OLD_COMP_CODE = (dr["OLD_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OLD_COMP_CODE"]);
                    ob.VAT_REG_NO = (dr["VAT_REG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VAT_REG_NO"]);
                    ob.YEAR_ESTD = (dr["YEAR_ESTD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YEAR_ESTD"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrCompanyModel> SelectAll(Int64? pSC_USER_ID = null)
        {
            string sp = "pkg_hr.hr_company_select";

            try
            {
                var obList = new List<HrCompanyModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HrCompanyModel ob = new HrCompanyModel();
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.HR_COMPANY_GRP_ID = (dr["HR_COMPANY_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_GRP_ID"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_NAME_BN = (dr["COMP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_BN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);
                    ob.COMP_DESC = (dr["COMP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_DESC"]);
                    ob.COMP_WEB = (dr["COMP_WEB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_WEB"]);
                    ob.LK_COMP_TYPE_ID = (dr["LK_COMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COMP_TYPE_ID"]);
                    ob.LK_BUS_TYPE_ID = (dr["LK_BUS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BUS_TYPE_ID"]);
                    ob.OLD_COMP_CODE = (dr["OLD_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OLD_COMP_CODE"]);
                    ob.VAT_REG_NO = (dr["VAT_REG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VAT_REG_NO"]);
                    ob.YEAR_ESTD = (dr["YEAR_ESTD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YEAR_ESTD"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.REG_ADD_EN = (dr["REG_ADD_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REG_ADD_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public string REG_ADD_EN { get; set; }
    }
}
