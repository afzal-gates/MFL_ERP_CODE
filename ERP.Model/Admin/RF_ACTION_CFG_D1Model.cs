using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class RF_ACTION_CFG_D1Model
    {
        public Int64 RF_ACTION_CFG_D1_ID { get; set; }
        public Int64 RF_ACTION_CFG_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64? LK_FLOOR_ID { get; set; }
        public string FLOOR_NAME { get; set; }
        public Int64? LINE_NO { get; set; }
        public Int64? DESIG_ORDER_LOW { get; set; }
        public Int64? DESIG_ORDER_HIGH { get; set; }
        public string IS_MULTI_LEVEL { get; set; }
        public string IS_NOTIFY_EMAIL { get; set; }
        public Int64 LK_ACTION_STATUS_ID { get; set; }
        public string AUTO_GEN_MSG { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string OFFICE_NAME_EN { get; set; }

        public Int64? HR_EMPLOYEE_ID { get; set; }

        public string EMAIL_ID { get; set; }

        public string EMPLOYEE_CODE { get; set; }

        public object APRV_EMAIL_LIST { get; set; }
        public Int64? SECTION_ID { get; set; }
        public string SECTION_NAME { get; set; }


        public List<RF_ACTION_CFG_D1Model> SelectAll()
        {
            string sp = "pkg_admin.rf_action_cfg_d1_select";
            try
            {
                var obList = new List<RF_ACTION_CFG_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_D1_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTION_CFG_D1Model ob = new RF_ACTION_CFG_D1Model();
                    ob.RF_ACTION_CFG_D1_ID = (dr["RF_ACTION_CFG_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTION_CFG_D1_ID"]);
                    ob.RF_ACTION_CFG_H_ID = (dr["RF_ACTION_CFG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTION_CFG_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.DESIG_ORDER_LOW = (dr["DESIG_ORDER_LOW"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER_LOW"]);
                    ob.DESIG_ORDER_HIGH = (dr["DESIG_ORDER_HIGH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER_HIGH"]);
                    ob.IS_MULTI_LEVEL = (dr["IS_MULTI_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_LEVEL"]);
                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);
                    ob.LK_ACTION_STATUS_ID = (dr["LK_ACTION_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACTION_STATUS_ID"]);
                    ob.AUTO_GEN_MSG = (dr["AUTO_GEN_MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTO_GEN_MSG"]);
                    if (dr["CREATION_DATE"] != DBNull.Value)
                    {
                        ob.CREATION_DATE = Convert.ToDateTime(dr["CREATION_DATE"]);
                    }
                    if (dr["LAST_UPDATE_DATE"] != DBNull.Value)
                    {
                        ob.CREATION_DATE = Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    }
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
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

        public List<RF_ACTION_CFG_D1Model> ActionConfigData(Int64 RF_ACTION_CFG_H_ID)
        {
            string sp = "pkg_admin.rf_action_cfg_d1_select";
            try
            {
                var obList = new List<RF_ACTION_CFG_D1Model>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_H_ID", Value = RF_ACTION_CFG_H_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTION_CFG_D1Model ob = new RF_ACTION_CFG_D1Model();
                    ob.RF_ACTION_CFG_D1_ID = (dr["RF_ACTION_CFG_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTION_CFG_D1_ID"]);
                    ob.RF_ACTION_CFG_H_ID = (dr["RF_ACTION_CFG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTION_CFG_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.FLOOR_NAME = (dr["FLOOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_NAME"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.DESIG_ORDER_LOW = (dr["DESIG_ORDER_LOW"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER_LOW"]);
                    ob.DESIG_ORDER_HIGH = (dr["DESIG_ORDER_HIGH"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER_HIGH"]);
                    ob.IS_MULTI_LEVEL = (dr["IS_MULTI_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_LEVEL"]);
                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);
                    ob.LK_ACTION_STATUS_ID = (dr["LK_ACTION_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACTION_STATUS_ID"]);
                    if (dr["HR_EMPLOYEE_ID"] != DBNull.Value)
                    {
                        ob.HR_EMPLOYEE_ID = Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    }

                    if (dr["SECTION_ID"] != DBNull.Value)
                    {
                        ob.SECTION_ID = Convert.ToInt64(dr["SECTION_ID"]);
                    }

                    ob.APRV_EMAIL_LIST = (dr["APRV_EMAIL_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APRV_EMAIL_LIST"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.AUTO_GEN_MSG = (dr["AUTO_GEN_MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTO_GEN_MSG"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.SECTION_NAME = (dr["SECTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_NAME"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Update()
        {
            const string sp = "pkg_admin.rf_action_cfg_d1_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_D1_ID", Value = ob.RF_ACTION_CFG_D1_ID},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_H_ID", Value = ob.RF_ACTION_CFG_H_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pSECTION_ID", Value = ob.SECTION_ID},

                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER_LOW", Value = ob.DESIG_ORDER_LOW},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER_HIGH", Value = ob.DESIG_ORDER_HIGH},
                    new CommandParameter() {ParameterName = "pIS_MULTI_LEVEL", Value = ob.IS_MULTI_LEVEL},
                    new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pAPRV_EMAIL_LIST", Value = ob.APRV_EMAIL_LIST},
                    new CommandParameter() {ParameterName = "pLK_ACTION_STATUS_ID", Value = ob.LK_ACTION_STATUS_ID},
                    new CommandParameter() {ParameterName = "pAUTO_GEN_MSG", Value = ob.AUTO_GEN_MSG},
                    new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(System.Web.HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
                return vMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Save()
        {
            const string sp = "pkg_admin.rf_action_cfg_d1_insert";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_D1_ID", Value = ob.RF_ACTION_CFG_D1_ID},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_H_ID", Value = ob.RF_ACTION_CFG_H_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pSECTION_ID", Value = ob.SECTION_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER_LOW", Value = ob.DESIG_ORDER_LOW},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER_HIGH", Value = ob.DESIG_ORDER_HIGH},
                    new CommandParameter() {ParameterName = "pIS_MULTI_LEVEL", Value = ob.IS_MULTI_LEVEL},
                    new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
                    new CommandParameter() {ParameterName = "pLK_ACTION_STATUS_ID", Value = ob.LK_ACTION_STATUS_ID},
                    new CommandParameter() {ParameterName = "pAPRV_EMAIL_LIST", Value = ob.APRV_EMAIL_LIST},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pAUTO_GEN_MSG", Value = ob.AUTO_GEN_MSG},
                    new CommandParameter() {ParameterName = "pCREATION_DATE", Value = DateTime.Today},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(System.Web.HttpContext.Current.Session["multiScUserId"])},

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
                throw ex;
            }
            return vMsg;
        }
    }
}