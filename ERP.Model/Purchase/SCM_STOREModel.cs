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
    public class SCM_STOREModel
    {
        public Int64 SCM_STORE_ID { get; set; }
        public string STORE_CODE { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string STORE_NAME_BN { get; set; }
        public Int64 LK_WH_TYPE_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public string ADDRESS_EN { get; set; }
        public string OFFICE_NAME_EN { get; set; }
        public string STORE_TYPE_NAME { get; set; }
        public string INV_ITEM_CAT_LST { get; set; }



        public string Save()
        {
            const string sp = "pkg_scm.scm_store_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSTORE_CODE", Value = ob.STORE_CODE},
                     new CommandParameter() {ParameterName = "pSTORE_NAME_EN", Value = ob.STORE_NAME_EN},
                     new CommandParameter() {ParameterName = "pSTORE_NAME_BN", Value = ob.STORE_NAME_BN},
                     new CommandParameter() {ParameterName = "pLK_WH_TYPE_ID", Value = ob.LK_WH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},                     
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "opSCM_STORE_ID", Value =0, Direction = ParameterDirection.Output},
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

        public string Delete()
        {
            const string sp = "pkg_scm.scm_store_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSTORE_CODE", Value = ob.STORE_CODE},
                     new CommandParameter() {ParameterName = "pSTORE_NAME_EN", Value = ob.STORE_NAME_EN},
                     new CommandParameter() {ParameterName = "pSTORE_NAME_BN", Value = ob.STORE_NAME_BN},
                     new CommandParameter() {ParameterName = "pLK_WH_TYPE_ID", Value = ob.LK_WH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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



        public List<SCM_STOREModel> SelectAll(string pINV_ITEM_CAT_LST = null, Int64? pSC_USER_ID = null)
        {
            string sp = "pkg_scm.scm_store_select";
            try
            {
                var obList = new List<SCM_STOREModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = pINV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STOREModel ob = new SCM_STOREModel();
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.STORE_CODE = (dr["STORE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_CODE"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.STORE_NAME_BN = (dr["STORE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_BN"]);
                    ob.LK_WH_TYPE_ID = (dr["LK_WH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_WH_TYPE_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    if (dr["HR_COMPANY_ID"] != DBNull.Value)
                        ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.STORE_TYPE_NAME = (dr["STORE_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_TYPE_NAME"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STOREModel Select(long ID)
        {
            string sp = "Select_SCM_STORE";
            try
            {
                var ob = new SCM_STOREModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.STORE_CODE = (dr["STORE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_CODE"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.STORE_NAME_BN = (dr["STORE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_BN"]);
                    ob.LK_WH_TYPE_ID = (dr["LK_WH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_WH_TYPE_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SCM_STOREModel> StoreListByOfcComCatID(Int64? pHR_OFFICE_ID = null, Int64? pHR_COMPANY_ID = null, string pINV_ITEM_CAT_LST = null)
        {
            string sp = "pkg_scm.scm_store_select";
            try
            {
                var obList = new List<SCM_STOREModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = pINV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STOREModel ob = new SCM_STOREModel();
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.STORE_CODE = (dr["STORE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_CODE"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.STORE_NAME_BN = (dr["STORE_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_BN"]);
                    ob.LK_WH_TYPE_ID = (dr["LK_WH_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_WH_TYPE_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                    ob.STORE_TYPE_NAME = (dr["STORE_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_TYPE_NAME"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long HR_COMPANY_ID { get; set; }
    }
}