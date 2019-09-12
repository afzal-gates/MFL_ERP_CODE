using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERP.Model.Purchase
{
    public class SCM_SUPPLIERModel
    {
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string SUP_CODE { get; set; }

        [Required(ErrorMessage = "Please insert category name [En]")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string SUP_TRD_NAME_EN { get; set; }
        public Int64 LK_SUP_TYPE_ID { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = " The field {0} need to be selected")]
        public Int64 HR_COUNTRY_ID { get; set; }

        public string SUP_TRD_NAME_BN { get; set; }


        [Required(ErrorMessage = "Please insert Owner Name")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string SUP_OWNER_NAME { get; set; }


        [Required(ErrorMessage = "Please insert short name")]
        [MaxLength(20, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string SUP_SNAME { get; set; }


        //[Required(ErrorMessage = "Please insert default address")]
        [MaxLength(350, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string ADDRESS_DEFA { get; set; }

        //[Required(ErrorMessage = "Please insert default contact person")]
        [MaxLength(350, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string CP_DEFA { get; set; }
        public string SUP_OFFICE_NAME { get; set; }
        public string SUPPLIER_GROUP { get; set; }
        
        public string IS_LOCAL { get; set; }
        public string VAT_REG_NO { get; set; }
        public string TIN_NO { get; set; }
        public string IRC_NO { get; set; }
        public string ERC_NO { get; set; }
        public string INSR_COVG_NO { get; set; }       
        public Decimal PCT_CREDIT_LMT { get; set; }       


        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }

        public Int64 CREATED_BY { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? INV_ITEM_CORE_CAT_ID { get; set; }
        public Int64? SC_USER_ID { get; set; }


        public string XML_STR_CERT { get; set; }
        public string XML_STR_Contact { get; set; }
        public string XML_STR_Address { get; set; }
        public string XML_STR_Brand { get; set; }
        public string PARENT_NAME { get; set; }
        public string INV_ITEM_CAT_ID_LST { get; set; }
        public string spriteCssClass { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
        
        public int TranMode { get; set; }
        public Int64 TOT_OQ { get; set; }
        public String IS_M_P { get; set; }

        //public SCM_SUP_ADDRESSModel SUP_ADDRESSModel { get; set; }
        //public SCM_SUP_CPModel SUP_CPModel { get; set; } 

        private List<SCM_SUP_ADDRESSModel> _SupplierAddressModel = null;
        public List<SCM_SUP_ADDRESSModel> addressList
        {
            get
            {
                if (_SupplierAddressModel == null)
                {
                    _SupplierAddressModel = new List<SCM_SUP_ADDRESSModel>();
                }
                return _SupplierAddressModel;
            }
            set
            {
                _SupplierAddressModel = value;
            }
        }


        private List<SCM_SUP_CPModel> _SupplierContactPerson = null;
        public List<SCM_SUP_CPModel> contactList
        {
            get
            {
                if (_SupplierContactPerson == null)
                {
                    _SupplierContactPerson = new List<SCM_SUP_CPModel>();
                }
                return _SupplierContactPerson;
            }
            set
            {
                _SupplierContactPerson = value;
            }
        }

        public string SaveHrdData()
        {
            const string sp = "pkg_pur_supplier.scm_supplier_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSUP_CODE", Value = ob.SUP_CODE},
                     new CommandParameter() {ParameterName = "pLK_SUP_TYPE_ID", Value = ob.LK_SUP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_EN", Value = ob.SUP_TRD_NAME_EN},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_BN", Value = ob.SUP_TRD_NAME_BN},
                     new CommandParameter() {ParameterName = "pSUP_OWNER_NAME", Value = ob.SUP_OWNER_NAME},
                     new CommandParameter() {ParameterName = "pSUP_SNAME", Value = ob.SUP_SNAME},
                     new CommandParameter() {ParameterName = "pADDRESS_DEFA", Value = ob.ADDRESS_DEFA==null?"N/A":ob.ADDRESS_DEFA},
                     new CommandParameter() {ParameterName = "pCP_DEFA", Value = ob.CP_DEFA==null?"N/A":ob.CP_DEFA},
             
                     new CommandParameter() {ParameterName = "pIS_LOCAL", Value = ob.IS_LOCAL},
                     new CommandParameter() {ParameterName = "pVAT_REG_NO", Value = ob.VAT_REG_NO},
                     new CommandParameter() {ParameterName = "pTIN_NO", Value = ob.TIN_NO},
                     new CommandParameter() {ParameterName = "pIRC_NO", Value = ob.IRC_NO},
                     new CommandParameter() {ParameterName = "pERC_NO", Value = ob.ERC_NO},
                     new CommandParameter() {ParameterName = "pINSR_COVG_NO", Value = ob.INSR_COVG_NO},
                     new CommandParameter() {ParameterName = "pPCT_CREDIT_LMT", Value = ob.PCT_CREDIT_LMT},

                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE==null?"Y":"N"},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = DateTime.Now},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = DateTime.Now},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pXML_ADD",  Value = ob.XML_STR_Address},
                     new CommandParameter() {ParameterName = "pXML_CONT", Value = ob.XML_STR_Contact},
                     new CommandParameter() {ParameterName = "pXML_BRAND", Value = ob.XML_STR_Brand},
                     new CommandParameter() {ParameterName = "pXML_STR_CERT", Value = ob.XML_STR_CERT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID_LST", Value = ob.INV_ITEM_CAT_ID_LST},                     
                     new CommandParameter() {ParameterName = "pOption", Value =1000},                     
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opSCM_SUPPLIER_ID", Value =0, Direction = ParameterDirection.Output}
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

        public string Save()
        {
            const string sp = "pkg_pur_supplier.scm_supplier_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSUP_CODE", Value = ob.SUP_CODE},
                     new CommandParameter() {ParameterName = "pLK_SUP_TYPE_ID", Value = ob.LK_SUP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_EN", Value = ob.SUP_TRD_NAME_EN},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_BN", Value = ob.SUP_TRD_NAME_BN},
                     new CommandParameter() {ParameterName = "pSUP_OWNER_NAME", Value = ob.SUP_OWNER_NAME},
                     new CommandParameter() {ParameterName = "pSUP_SNAME", Value = ob.SUP_SNAME},
                     new CommandParameter() {ParameterName = "pADDRESS_DEFA", Value = ob.ADDRESS_DEFA},
                     new CommandParameter() {ParameterName = "pCP_DEFA", Value = ob.CP_DEFA},
                     new CommandParameter() {ParameterName = "pIS_LOCAL", Value = ob.IS_LOCAL},
                     new CommandParameter() {ParameterName = "pVAT_REG_NO", Value = ob.VAT_REG_NO},
                     new CommandParameter() {ParameterName = "pTIN_NO", Value = ob.TIN_NO},
                     new CommandParameter() {ParameterName = "pIRC_NO", Value = ob.IRC_NO},
                     new CommandParameter() {ParameterName = "pERC_NO", Value = ob.ERC_NO},
                     new CommandParameter() {ParameterName = "pINSR_COVG_NO", Value = ob.INSR_COVG_NO},
                     new CommandParameter() {ParameterName = "pPCT_CREDIT_LMT", Value = ob.PCT_CREDIT_LMT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pXML_ADD", Value =1000},
                     new CommandParameter() {ParameterName = "pXML_CONT", Value =1000},
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
            const string sp = "SP_SCM_SUPPLIER";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSUP_CODE", Value = ob.SUP_CODE},
                     new CommandParameter() {ParameterName = "pLK_SUP_TYPE_ID", Value = ob.LK_SUP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_EN", Value = ob.SUP_TRD_NAME_EN},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_BN", Value = ob.SUP_TRD_NAME_BN},
                     new CommandParameter() {ParameterName = "pSUP_OWNER_NAME", Value = ob.SUP_OWNER_NAME},
                     new CommandParameter() {ParameterName = "pSUP_SNAME", Value = ob.SUP_SNAME},
                     new CommandParameter() {ParameterName = "pADDRESS_DEFA", Value = ob.ADDRESS_DEFA},
                     new CommandParameter() {ParameterName = "pCP_DEFA", Value = ob.CP_DEFA},
                     new CommandParameter() {ParameterName = "pIS_LOCAL", Value = ob.IS_LOCAL},
                     new CommandParameter() {ParameterName = "pVAT_REG_NO", Value = ob.VAT_REG_NO},
                     new CommandParameter() {ParameterName = "pTIN_NO", Value = ob.TIN_NO},
                     new CommandParameter() {ParameterName = "pIRC_NO", Value = ob.IRC_NO},
                     new CommandParameter() {ParameterName = "pERC_NO", Value = ob.ERC_NO},
                     new CommandParameter() {ParameterName = "pINSR_COVG_NO", Value = ob.INSR_COVG_NO},
                     new CommandParameter() {ParameterName = "pPCT_CREDIT_LMT", Value = ob.PCT_CREDIT_LMT},
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
            const string sp = "SP_SCM_SUPPLIER";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSUP_CODE", Value = ob.SUP_CODE},
                     new CommandParameter() {ParameterName = "pLK_SUP_TYPE_ID", Value = ob.LK_SUP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_EN", Value = ob.SUP_TRD_NAME_EN},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_BN", Value = ob.SUP_TRD_NAME_BN},
                     new CommandParameter() {ParameterName = "pSUP_OWNER_NAME", Value = ob.SUP_OWNER_NAME},
                     new CommandParameter() {ParameterName = "pSUP_SNAME", Value = ob.SUP_SNAME},
                     new CommandParameter() {ParameterName = "pADDRESS_DEFA", Value = ob.ADDRESS_DEFA},
                     new CommandParameter() {ParameterName = "pCP_DEFA", Value = ob.CP_DEFA},
                     new CommandParameter() {ParameterName = "pIS_LOCAL", Value = ob.IS_LOCAL},
                     new CommandParameter() {ParameterName = "pVAT_REG_NO", Value = ob.VAT_REG_NO},
                     new CommandParameter() {ParameterName = "pTIN_NO", Value = ob.TIN_NO},
                     new CommandParameter() {ParameterName = "pIRC_NO", Value = ob.IRC_NO},
                     new CommandParameter() {ParameterName = "pERC_NO", Value = ob.ERC_NO},
                     new CommandParameter() {ParameterName = "pINSR_COVG_NO", Value = ob.INSR_COVG_NO},
                     new CommandParameter() {ParameterName = "pPCT_CREDIT_LMT", Value = ob.PCT_CREDIT_LMT},
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

        public List<SCM_SUPPLIERModel> SelectAll()
        {
            string sp = "pkg_pur_supplier.scm_supplier_select";
            try
            {
                var obList = new List<SCM_SUPPLIERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SUPPLIERModel ob = new SCM_SUPPLIERModel();
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_CODE = (dr["SUP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_CODE"]);
                    ob.LK_SUP_TYPE_ID = (dr["LK_SUP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUP_TYPE_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.SUP_TRD_NAME_BN = (dr["SUP_TRD_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_BN"]);
                    ob.SUP_OWNER_NAME = (dr["SUP_OWNER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_OWNER_NAME"]);
                    ob.SUP_SNAME = (dr["SUP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_SNAME"]);
                    ob.INV_ITEM_CAT_ID_LST = (dr["INV_ITEM_CAT_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_ID_LST"]);
                    ob.ADDRESS_DEFA = (dr["ADDRESS_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_DEFA"]);
                    ob.CP_DEFA = (dr["CP_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_DEFA"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_LOCAL = (dr["IS_LOCAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL"]);
                    ob.VAT_REG_NO = (dr["VAT_REG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VAT_REG_NO"]);
                    ob.TIN_NO = (dr["TIN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIN_NO"]);
                    ob.IRC_NO = (dr["IRC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IRC_NO"]);
                    ob.ERC_NO = (dr["ERC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ERC_NO"]);
                    ob.SUPPLIER_GROUP = (dr["SUPPLIER_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUPPLIER_GROUP"]);                    
                    ob.INSR_COVG_NO = (dr["INSR_COVG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSR_COVG_NO"]);
                    ob.PCT_CREDIT_LMT = (dr["PCT_CREDIT_LMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_CREDIT_LMT"]);
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

        public List<SCM_SUPPLIERModel> SelectAllByCat(string INV_ITEM_CAT_LST)
        {
            string sp = "pkg_pur_supplier.scm_supplier_select";
            try
            {
                var obList = new List<SCM_SUPPLIERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value =INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SUPPLIERModel ob = new SCM_SUPPLIERModel();
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_CODE = (dr["SUP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_CODE"]);
                    ob.LK_SUP_TYPE_ID = (dr["LK_SUP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUP_TYPE_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.SUP_TRD_NAME_BN = (dr["SUP_TRD_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_BN"]);
                    ob.SUP_OWNER_NAME = (dr["SUP_OWNER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_OWNER_NAME"]);
                    ob.SUP_SNAME = (dr["SUP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_SNAME"]);
                    //ob.INV_ITEM_CAT_ID_LST = (dr["INV_ITEM_CAT_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_ID_LST"]);
                    ob.ADDRESS_DEFA = (dr["ADDRESS_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_DEFA"]);
                    ob.CP_DEFA = (dr["CP_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_DEFA"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_LOCAL = (dr["IS_LOCAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL"]);
                    ob.VAT_REG_NO = (dr["VAT_REG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VAT_REG_NO"]);
                    ob.TIN_NO = (dr["TIN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIN_NO"]);
                    ob.IRC_NO = (dr["IRC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IRC_NO"]);
                    ob.ERC_NO = (dr["ERC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ERC_NO"]);
                    //ob.SUPPLIER_GROUP = (dr["SUPPLIER_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUPPLIER_GROUP"]);
                    //ob.INSR_COVG_NO = (dr["INSR_COVG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSR_COVG_NO"]);
                    //ob.PCT_CREDIT_LMT = (dr["PCT_CREDIT_LMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_CREDIT_LMT"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_SUPPLIERModel Select(Int64? ID)
        {
            string sp = "pkg_pur_supplier.scm_supplier_select";
            try
            {
                if (ID == null)
                    ID = 0;
                var ob = new SCM_SUPPLIERModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_CODE = (dr["SUP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_CODE"]);
                    ob.LK_SUP_TYPE_ID = (dr["LK_SUP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUP_TYPE_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.SUP_TRD_NAME_BN = (dr["SUP_TRD_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_BN"]);
                    ob.SUP_OWNER_NAME = (dr["SUP_OWNER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_OWNER_NAME"]);
                    ob.SUP_SNAME = (dr["SUP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_SNAME"]);
                    ob.ADDRESS_DEFA = (dr["ADDRESS_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_DEFA"]);
                    ob.CP_DEFA = (dr["CP_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_DEFA"]);
                    ob.IS_LOCAL = (dr["IS_LOCAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL"]);
                    ob.VAT_REG_NO = (dr["VAT_REG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VAT_REG_NO"]);
                    ob.TIN_NO = (dr["TIN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIN_NO"]);
                    ob.IRC_NO = (dr["IRC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IRC_NO"]);
                    ob.ERC_NO = (dr["ERC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ERC_NO"]);
                    ob.INSR_COVG_NO = (dr["INSR_COVG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSR_COVG_NO"]);
                    ob.PCT_CREDIT_LMT = (dr["PCT_CREDIT_LMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_CREDIT_LMT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.INV_ITEM_CAT_ID_LST = (dr["INV_ITEM_CAT_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_ID_LST"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetTypeWiseSuplier(int? pLK_SUP_TYPE_ID)
        {
            string sp = "pkg_pur_supplier.scm_supplier_select";
            try
            {
                var obList = new List<SCM_SUPPLIERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_SUP_TYPE_ID", Value = pLK_SUP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SUPPLIERModel ob = new SCM_SUPPLIERModel();
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_CODE = (dr["SUP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_CODE"]);
                    ob.LK_SUP_TYPE_ID = (dr["LK_SUP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUP_TYPE_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.SUP_TRD_NAME_BN = (dr["SUP_TRD_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_BN"]);
                    ob.SUP_OWNER_NAME = (dr["SUP_OWNER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_OWNER_NAME"]);
                    ob.SUP_SNAME = (dr["SUP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_SNAME"]);
                    //ob.INV_ITEM_CAT_ID_LST = (dr["INV_ITEM_CAT_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_ID_LST"]);
                    //ob.ADDRESS_DEFA = (dr["ADDRESS_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_DEFA"]);
                    //ob.CP_DEFA = (dr["CP_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_DEFA"]);
                    //ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.IS_LOCAL = (dr["IS_LOCAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL"]);
                    //ob.VAT_REG_NO = (dr["VAT_REG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VAT_REG_NO"]);
                    //ob.TIN_NO = (dr["TIN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TIN_NO"]);
                    //ob.IRC_NO = (dr["IRC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IRC_NO"]);
                    //ob.ERC_NO = (dr["ERC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ERC_NO"]);
                    //ob.SUPPLIER_GROUP = (dr["SUPPLIER_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUPPLIER_GROUP"]);
                    //ob.INSR_COVG_NO = (dr["INSR_COVG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSR_COVG_NO"]);
                    //ob.PCT_CREDIT_LMT = (dr["PCT_CREDIT_LMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_CREDIT_LMT"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
