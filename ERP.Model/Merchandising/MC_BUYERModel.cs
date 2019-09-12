  using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.ComponentModel;

namespace ERP.Model
{
    public class MC_BUYERModel
    {
        public MC_BUYERModel()
        {
            OraDatabase db = new OraDatabase();
        }
        public Int64 MC_BUYER_ID { get; set; }
        public string BUYER_CODE { get; set; }

        [DisplayName("Buyer Name[E]")]
        [Required(ErrorMessage=" The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string BUYER_NAME_EN { get; set; }
        [DisplayName("Buyer Name[B]")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string BUYER_NAME_BN { get; set; }

        [DisplayName("Buyer Short Name")]
        [Required(ErrorMessage = " The field {0} is required")]
        [MaxLength(20, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string BUYER_SNAME { get; set; }
        public string BUYER_DESC { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = " The field {0} is required")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string ADDRESS_PI { get; set; }
        public string BUYER_CP_REF { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = " The field {0} need to be selected")]
        public Int64 HR_COUNTRY_ID { get; set; }
        public DateTime REG_PERIOD { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public Int64 MC_BU_COL_REF_ID { get; set; }

        public string IS_ACTIVE { get; set; }

        public string COLOR_REF { get; set; }

        public string COUNTRY_NAME_EN { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public Int64? MC_BYR_ACC_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string XML { get; set; }


        private List<MC_BU_COL_REFModel> _items = null;
        public List<MC_BU_COL_REFModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<MC_BU_COL_REFModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public string Save()
        {
            const string sp = "pkg_merchandising.mc_buyer_insert";
            string jsonStr = "{";
            var ob = this;
            Int64 i = 1;

            OraDatabase db = new OraDatabase();

            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pBUYER_CODE", Value = ob.BUYER_CODE},
                     new CommandParameter() {ParameterName = "pBUYER_NAME_EN", Value = ob.BUYER_NAME_EN},
                     new CommandParameter() {ParameterName = "pBUYER_NAME_BN", Value = ob.BUYER_NAME_BN},
                     new CommandParameter() {ParameterName = "pBUYER_SNAME", Value = ob.BUYER_SNAME},
                     new CommandParameter() {ParameterName = "pBUYER_DESC", Value = ob.BUYER_DESC},
                     new CommandParameter() {ParameterName = "pADDRESS_PI", Value = ob.ADDRESS_PI},
                     new CommandParameter() {ParameterName = "pBUYER_CP_REF", Value = ob.BUYER_CP_REF},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pREG_PERIOD", Value = ob.REG_PERIOD},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "v_mc_buyer_id", Value =500, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_merchandising.mc_buyer_update";
            string jsonStr = "{";
            Int64 i = 1;
            var ob = this;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pBUYER_CODE", Value = ob.BUYER_CODE},
                     new CommandParameter() {ParameterName = "pBUYER_NAME_EN", Value = ob.BUYER_NAME_EN},
                     new CommandParameter() {ParameterName = "pBUYER_NAME_BN", Value = ob.BUYER_NAME_BN},
                     new CommandParameter() {ParameterName = "pBUYER_SNAME", Value = ob.BUYER_SNAME},
                     new CommandParameter() {ParameterName = "pBUYER_DESC", Value = ob.BUYER_DESC},
                     new CommandParameter() {ParameterName = "pADDRESS_PI", Value = ob.ADDRESS_PI},
                     new CommandParameter() {ParameterName = "pBUYER_CP_REF", Value = ob.BUYER_CP_REF},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pREG_PERIOD", Value = ob.REG_PERIOD},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "pkg_merchandising.mc_buyer_delete";
            string vMsg = "";
            var ob = this;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
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

        public List<MC_BUYERModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_buyer_select";
            try
            {
                var obList = new List<MC_BUYERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BUYERModel ob = new MC_BUYERModel();
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.BUYER_CODE = (dr["BUYER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CODE"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BUYER_NAME_BN = (dr["BUYER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_BN"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.BUYER_DESC = (dr["BUYER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_DESC"]);
                    ob.ADDRESS_PI = (dr["ADDRESS_PI"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_PI"]);
                    ob.BUYER_CP_REF = (dr["BUYER_CP_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CP_REF"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.REG_PERIOD = (dr["REG_PERIOD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_PERIOD"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public MC_BUYERModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_buyer_select";
            try
            {
                var ob = new MC_BUYERModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.BUYER_CODE = (dr["BUYER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CODE"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BUYER_NAME_BN = (dr["BUYER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_BN"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.BUYER_DESC = (dr["BUYER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_DESC"]);
                    ob.ADDRESS_PI = (dr["ADDRESS_PI"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_PI"]);
                    ob.BUYER_CP_REF = (dr["BUYER_CP_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CP_REF"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.REG_PERIOD = (dr["REG_PERIOD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_PERIOD"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public List<MC_BUYERModel> BuyerDatasByColourId(Int64 MC_COLOR_ID)
        {
            string sp = "pkg_merchandising.mc_buyer_select";
            try
            {
                var obList = new List<MC_BUYERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BUYERModel ob = new MC_BUYERModel();
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.BUYER_CODE = (dr["BUYER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CODE"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BUYER_NAME_BN = (dr["BUYER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_BN"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.BUYER_DESC = (dr["BUYER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_DESC"]);
                    ob.ADDRESS_PI = (dr["ADDRESS_PI"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_PI"]);
                    ob.BUYER_CP_REF = (dr["BUYER_CP_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CP_REF"]);

                    ob.COLOR_REF = (dr["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_REF"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.MC_BU_COL_REF_ID = (dr["MC_BU_COL_REF_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_BU_COL_REF_ID"]);

                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.REG_PERIOD = (dr["REG_PERIOD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_PERIOD"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
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

        public List<MC_BUYERModel> BuyerByUserList()
        {
            string sp = "pkg_merchandising.mc_buyer_select";
            try
            {
                var obList = new List<MC_BUYERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BUYERModel ob = new MC_BUYERModel();
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.BUYER_CODE = (dr["BUYER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CODE"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BUYER_NAME_BN = (dr["BUYER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_BN"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.BUYER_DESC = (dr["BUYER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_DESC"]);
                    ob.ADDRESS_PI = (dr["ADDRESS_PI"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_PI"]);
                    ob.BUYER_CP_REF = (dr["BUYER_CP_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CP_REF"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.REG_PERIOD = (dr["REG_PERIOD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_PERIOD"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_BUYERModel> BuyerListByStyleHID(Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_merchandising.mc_buyer_select";
            try
            {
                var obList = new List<MC_BUYERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BUYERModel ob = new MC_BUYERModel();
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.BUYER_CODE = (dr["BUYER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CODE"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BUYER_NAME_BN = (dr["BUYER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_BN"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.BUYER_DESC = (dr["BUYER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_DESC"]);
                    ob.ADDRESS_PI = (dr["ADDRESS_PI"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_PI"]);
                    ob.BUYER_CP_REF = (dr["BUYER_CP_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CP_REF"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.REG_PERIOD = (dr["REG_PERIOD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REG_PERIOD"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    //ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    //ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_BUYERModel> MappedBuyerList(Int64 pOption)
        {
            string sp = "pkg_merchandising.mc_buyer_select";
            try
            {
                var obList = new List<MC_BUYERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BUYERModel ob = new MC_BUYERModel();
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.BUYER_CODE = (dr["BUYER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CODE"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BUYER_NAME_BN = (dr["BUYER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_BN"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.BUYER_DESC = (dr["BUYER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_DESC"]);
                    ob.ADDRESS_PI = (dr["ADDRESS_PI"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_PI"]);
                    ob.BUYER_CP_REF = (dr["BUYER_CP_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CP_REF"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveSampleMappedData()
        {
            const string sp = "pkg_merchandising.mc_buyer_insert";
            string jsonStr = "{";
            var ob = this;
            Int64 i = 1;

            OraDatabase db = new OraDatabase();

            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "v_mc_buyer_id", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]}
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

        public List<MC_BUYERModel> getBuyerDropdownList(Int64? MC_BYR_ACC_ID)
        {
            string sp = "pkg_merchandising.mc_buyer_select";
            try
            {
                var obList = new List<MC_BUYERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = MC_BYR_ACC_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BUYERModel ob = new MC_BUYERModel();
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.BUYER_CODE = (dr["BUYER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CODE"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_BUYERModel> getBuyerDdForCostApp()
        {
            string sp = "pkg_merchandising.mc_buyer_select";
            try
            {
                var obList = new List<MC_BUYERModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = MC_BYR_ACC_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BUYERModel ob = new MC_BUYERModel();
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.BUYER_CODE = (dr["BUYER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_CODE"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BUYER_SNAME = (dr["BUYER_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_SNAME"]);
                    ob.MC_BUYER_OFF_ID = (dr["MC_BUYER_OFF_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BUYER_OFF_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int MC_BUYER_OFF_ID { get; set; }
    }
}