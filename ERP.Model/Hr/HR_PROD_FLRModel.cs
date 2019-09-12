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
    public class HR_PROD_FLRModel
    {
        public Int64 HR_PROD_FLR_ID { get; set; }
        public Int64 HR_PROD_BLDNG_ID { get; set; }
        public string FLOOR_NO { get; set; }
        public string FLOOR_CODE { get; set; }
        public string FLOOR_DESC_EN { get; set; }
        public string FLOOR_DESC_BN { get; set; }
        public Int64 LK_FLOOR_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public string BLDNG_CODE { get; set; }
        public string BLDNG_DESC_EN { get; set; }

        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LK_PFLR_TYP_ID { get; set; }
        public string OFFICE_NAME_EN { get; set; }
        public string OFFICE_SNAME { get; set; }
        public long HR_OFFICE_ID { get; set; }


        private List<HR_PROD_LINEModel> _items = null;
        public List<HR_PROD_LINEModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<HR_PROD_LINEModel>();
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
            const string sp = "pkg_hr.hr_prod_flr_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_BLDNG_ID", Value = ob.HR_PROD_BLDNG_ID},
                     new CommandParameter() {ParameterName = "pFLOOR_NO", Value = ob.FLOOR_NO},
                     new CommandParameter() {ParameterName = "pFLOOR_CODE", Value = ob.FLOOR_CODE},
                     new CommandParameter() {ParameterName = "pFLOOR_DESC_EN", Value = ob.FLOOR_DESC_EN},
                     new CommandParameter() {ParameterName = "pFLOOR_DESC_BN", Value = ob.FLOOR_DESC_BN},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLK_PFLR_TYP_ID", Value = ob.LK_PFLR_TYP_ID},
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



        public List<HR_PROD_FLRModel> getBuildingData(Int64 pHR_COMPANY_ID, Int16 pLK_PFLR_TYP_ID)
        {
            string sp = "PKG_MC_LOAD_PLAN.RESOURCE_SELECT";
            try
            {
                var obList = new List<HR_PROD_FLRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pLK_PFLR_TYP_ID", Value = pLK_PFLR_TYP_ID},
                      new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_FLRModel ob = new HR_PROD_FLRModel();
                    ob.HR_PROD_BLDNG_ID = (dr["HR_PROD_BLDNG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_BLDNG_ID"]);
                    ob.BLDNG_CODE = (dr["BLDNG_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_CODE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<HR_PROD_FLRModel> GetFloor4IncrProp()
        {
            string sp = "pkg_incriment.hr_yr_incr_h_select";
            try
            {
                int vOption = 3004;
                if (Convert.ToString(HttpContext.Current.Session["multiUserType"]) == "B")
                    vOption = 3005;

                var obList = new List<HR_PROD_FLRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = vOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_FLRModel ob = new HR_PROD_FLRModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_BLDNG_ID = (dr["HR_PROD_BLDNG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_BLDNG_ID"]);
                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                    ob.FLOOR_DESC_BN = (dr["FLOOR_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_BN"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
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

        public List<HR_PROD_FLRModel> GetProdFloorList(Int16? pHR_COMPANY_ID, Int16? pLK_PFLR_TYP_ID, Int64? pOption)
        {
            string sp = "pkg_hr.hr_prod_flr_select";
            try
            {
                var obList = new List<HR_PROD_FLRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pLK_PFLR_TYP_ID", Value = pLK_PFLR_TYP_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_FLRModel ob = new HR_PROD_FLRModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_BLDNG_ID = (dr["HR_PROD_BLDNG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_BLDNG_ID"]);
                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                    ob.FLOOR_DESC_BN = (dr["FLOOR_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_BN"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.BLDNG_DESC_EN = (dr["BLDNG_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_DESC_EN"]);
                    ob.BLDNG_CODE = (dr["BLDNG_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_CODE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_PROD_FLRModel> select(Int64? pHR_PROD_BLDNG_ID = null, Int64? pHR_PROD_FLR_ID = null)
        {
            string sp = "pkg_hr.hr_prod_flr_select";
            try
            {
                var obList = new List<HR_PROD_FLRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_BLDNG_ID", Value = pHR_PROD_BLDNG_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_FLRModel ob = new HR_PROD_FLRModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_BLDNG_ID = (dr["HR_PROD_BLDNG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_BLDNG_ID"]);
                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                    ob.FLOOR_DESC_BN = (dr["FLOOR_DESC_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_BN"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);

                    ob.BLDNG_DESC_EN = (dr["BLDNG_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_DESC_EN"]);
                    ob.BLDNG_CODE = (dr["BLDNG_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_CODE"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.OFFICE_SNAME = (dr["OFFICE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_SNAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<HR_PROD_FLRModel> getFloorByProdCategory(Int64 pGMT_PRODUCT_TYP_ID)
        {
            string sp = "pkg_hr.hr_prod_flr_select";
            try
            {
                var obList = new List<HR_PROD_FLRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = pGMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_FLRModel ob = new HR_PROD_FLRModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_BLDNG_ID = (dr["HR_PROD_BLDNG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_BLDNG_ID"]);
                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);

                    ob.BLDNG_DESC_EN = (dr["BLDNG_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_DESC_EN"]);
                    ob.BLDNG_CODE = (dr["BLDNG_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_CODE"]);

                    ob.items = new HR_PROD_LINEModel().getLineByProdType(pGMT_PRODUCT_TYP_ID, ob.HR_PROD_FLR_ID);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_PROD_FLRModel> GetPlnRsurcAccessFlrLnByUser(Int64 pLK_PFLR_TYP_ID, Int64 pSC_USER_ID)
        {
            string sp = "pkg_hr.hr_prod_flr_select";
            try
            {
                var obList = new List<HR_PROD_FLRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_PFLR_TYP_ID", Value = pLK_PFLR_TYP_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_PROD_FLRModel ob = new HR_PROD_FLRModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.HR_PROD_BLDNG_ID = (dr["HR_PROD_BLDNG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_BLDNG_ID"]);
                    ob.FLOOR_NO = (dr["FLOOR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NO"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);

                    ob.BLDNG_DESC_EN = (dr["BLDNG_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_DESC_EN"]);
                    ob.BLDNG_CODE = (dr["BLDNG_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BLDNG_CODE"]);

                    ob.items = new HR_PROD_LINEModel().GetPlnRsurcAccessLnByFlrUsr(pSC_USER_ID, ob.HR_PROD_FLR_ID);

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