using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Purchase
{
    public class SCM_SUP_ADDRESSModel
    {
        public Int64 SCM_SUP_ADDRESS_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 LK_OFF_TYPE_ID { get; set; }


        [Required(ErrorMessage = "Please insert office name")]
        [MaxLength(350, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string SUP_OFFICE_NAME { get; set; }

        [Required(ErrorMessage = "Please insert address")]
        [MaxLength(350, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string ADDRESS_EN { get; set; }

        [MaxLength(350, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string ADDRESS_BN { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = " The field {0} need to be selected")]
        public Int64 HR_COUNTRY_ID { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string CITY_NAME { get; set; }

        [Required(ErrorMessage = "Please insert post code")]
        [MaxLength(20, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string POST_CODE { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string PO_BOX_NO { get; set; }

        [MaxLength(80, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string WORK_PHONE { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string MOB_PHONE { get; set; }

        [MaxLength(30, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string FAX_NO { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string EMAIL_ID { get; set; }

        [MaxLength(30, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string WEB_URL { get; set; }



        public string IS_DEFAULT { get; set; }
        public string IS_ACTIVE { get; set; }

        public DateTime CREATION_DATE { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }

        public Int64 CREATED_BY { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? INV_ITEM_CORE_CAT_ID { get; set; }
        public Int64? SC_USER_ID { get; set; }


        public string XML_STR { get; set; }
        public string PARENT_NAME { get; set; }
        public string text { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }

        public int TranMode { get; set; }
        public Int64 TOT_OQ { get; set; }
        public String IS_M_P { get; set; }

        //public Int64 SCM_SUP_ADDRESS_ID { get; set; }
        //public Int64 SCM_SUPPLIER_ID { get; set; }
        //public Int64 LK_OFF_TYPE_ID { get; set; }
        //public string ADDRESS_EN { get; set; }
        //public string ADDRESS_BN { get; set; }
        //public string CITY_NAME { get; set; }
        //public string POST_CODE { get; set; }
        //public string PO_BOX_NO { get; set; }
        //public Int64 HR_COUNTRY_ID { get; set; }
        //public string WORK_PHONE { get; set; }
        //public string MOB_PHONE { get; set; }
        //public string FAX_NO { get; set; }
        //public string EMAIL_ID { get; set; }
        //public string WEB_URL { get; set; }
        //public string IS_DEFAULT { get; set; }
        //public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        //public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        //public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_SCM_SUP_ADDRESS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUP_ADDRESS_ID", Value = ob.SCM_SUP_ADDRESS_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_OFF_TYPE_ID", Value = ob.LK_OFF_TYPE_ID},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pCITY_NAME", Value = ob.CITY_NAME},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_DEFAULT", Value = ob.IS_DEFAULT},
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
            const string sp = "SP_SCM_SUP_ADDRESS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUP_ADDRESS_ID", Value = ob.SCM_SUP_ADDRESS_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_OFF_TYPE_ID", Value = ob.LK_OFF_TYPE_ID},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pCITY_NAME", Value = ob.CITY_NAME},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_DEFAULT", Value = ob.IS_DEFAULT},
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
            const string sp = "SP_SCM_SUP_ADDRESS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUP_ADDRESS_ID", Value = ob.SCM_SUP_ADDRESS_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_OFF_TYPE_ID", Value = ob.LK_OFF_TYPE_ID},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pCITY_NAME", Value = ob.CITY_NAME},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_DEFAULT", Value = ob.IS_DEFAULT},
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

        public List<SCM_SUP_ADDRESSModel> SelectAll()
        {
            string sp = "pkg_pur_supplier.pur_supplier_address_select";
            try
            {
                var obList = new List<SCM_SUP_ADDRESSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUP_ADDRESS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SUP_ADDRESSModel ob = new SCM_SUP_ADDRESSModel();
                    ob.SCM_SUP_ADDRESS_ID = (dr["SCM_SUP_ADDRESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUP_ADDRESS_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_OFF_TYPE_ID = (dr["LK_OFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_OFF_TYPE_ID"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_EN = (dr["SUP_OFFICE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_OFFICE_NAME"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.CITY_NAME = (dr["CITY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CITY_NAME"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                    ob.IS_DEFAULT = (dr["IS_DEFAULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFAULT"]);
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

        public List<SCM_SUP_ADDRESSModel> Select(Int64? ID)
        {
            string sp = "pkg_pur_supplier.pur_supplier_address_select";
            try
            {
                var ob = new SCM_SUP_ADDRESSModel();
                var list = new List<SCM_SUP_ADDRESSModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =ID==null?0:ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob = new SCM_SUP_ADDRESSModel();
                    ob.SCM_SUP_ADDRESS_ID = (dr["SCM_SUP_ADDRESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUP_ADDRESS_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_OFF_TYPE_ID = (dr["LK_OFF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_OFF_TYPE_ID"]);
                    ob.SUP_OFFICE_NAME = (dr["SUP_OFFICE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_OFFICE_NAME"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                    ob.CITY_NAME = (dr["CITY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CITY_NAME"]);
                    ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                    ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                    ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                    ob.IS_DEFAULT = (dr["IS_DEFAULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFAULT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    list.Add(ob);

                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
