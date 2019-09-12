using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class RF_ACTION_CFG_LVLModel
    {
        public Int64 RF_ACTION_CFG_LVL_ID { get; set; }
        public Int64 RF_ACTION_CFG_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64? LK_FLOOR_ID { get; set; }
        public Int64? LINE_NO { get; set; }
        public Int64 APROVER_LVL_NO { get; set; }
        public string IS_LEAF { get; set; }
        public Int64 DESIG_ORDER { get; set; }
        public Int64 APPROVER_ID { get; set; }
        public string IS_AUTO_APRVD { get; set; }
        public string IS_NOTIFY_EMAIL { get; set; }
        //public string EMAIL_ID { get; set; }
        public Int64 LK_ACTION_STATUS_ID { get; set; }
        public string AUTO_GEN_MSG { get; set; }
        public Int64? EXEC_BY_ID { get; set; }

        public string EXEC_BY_CODE { get; set; }

        public string APPROVER_CODE { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string OFFICE_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public Int64? RF_ACTION_CFG_D11_ID { get; set; }

        public Int64? RF_ACTION_CFG_D1_ID { get; set; }

        public string LK_ACTION_STATUS { get; set; }

        public Int64? ALT_APPROVER_ID { get; set; }

        public string ALT_APPROVER_CODE { get; set; }

        public Int64? SECTION_ID { get; set; }

        public List<RF_ACTION_CFG_LVLModel> getLvlData(Int64 RF_ACTION_CFG_H_ID)
        {
            string sp = "pkg_admin.rf_action_cfg_d1_select";
            try
            {
                var obList = new List<RF_ACTION_CFG_LVLModel>();
                OraDatabase db = new OraDatabase();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_H_ID", Value = RF_ACTION_CFG_H_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTION_CFG_LVLModel ob = new RF_ACTION_CFG_LVLModel();
                    ob.RF_ACTION_CFG_LVL_ID = (dr["RF_ACTION_CFG_LVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTION_CFG_LVL_ID"]);
                    ob.RF_ACTION_CFG_H_ID = (dr["RF_ACTION_CFG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTION_CFG_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    if (dr["LK_FLOOR_ID"] != DBNull.Value)
                    {
                        ob.LK_FLOOR_ID = Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    }
                    if (dr["LINE_NO"] != DBNull.Value)
                    {
                        ob.LINE_NO = Convert.ToInt64(dr["LINE_NO"]);
                    }

                    if (dr["SECTION_ID"] != DBNull.Value)
                    {
                        ob.SECTION_ID = Convert.ToInt64(dr["SECTION_ID"]);
                    }

                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.APPROVER_ID = (dr["APPROVER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPROVER_ID"]);
                    ob.IS_AUTO_APRVD = (dr["IS_AUTO_APRVD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AUTO_APRVD"]);

                    ob.LK_ACTION_STATUS = (dr["LK_ACTION_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ACTION_STATUS"]);

                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);
                    ob.LK_ACTION_STATUS_ID = (dr["LK_ACTION_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACTION_STATUS_ID"]);
                    ob.AUTO_GEN_MSG = (dr["AUTO_GEN_MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTO_GEN_MSG"]);
                    if (dr["EXEC_BY_ID"] != DBNull.Value)
                    {
                        ob.EXEC_BY_ID = Convert.ToInt64(dr["EXEC_BY_ID"]);
                    }
                    ob.APPROVER_CODE = (dr["APPROVER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APPROVER_CODE"]);
                    ob.EXEC_BY_CODE = (dr["EXEC_BY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXEC_BY_CODE"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_ACTION_CFG_LVLModel> getApproverLvlData(long RF_ACTION_CFG_H_ID, long RF_ACTION_CFG_D1_ID)
        {
            string sp = "pkg_admin.rf_action_cfg_d1_select";
            try
            {
                var obList = new List<RF_ACTION_CFG_LVLModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_H_ID", Value = RF_ACTION_CFG_H_ID},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_D1_ID", Value = RF_ACTION_CFG_D1_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTION_CFG_LVLModel ob = new RF_ACTION_CFG_LVLModel();
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    if (dr["LK_FLOOR_ID"] != DBNull.Value)
                    {
                        ob.LK_FLOOR_ID = Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    }
                    if (dr["LINE_NO"] != DBNull.Value)
                    {
                        ob.LINE_NO = Convert.ToInt64(dr["LINE_NO"]);
                    }
                    if (dr["ALT_APPROVER_ID"] != DBNull.Value)
                    {
                        ob.ALT_APPROVER_ID = Convert.ToInt64(dr["ALT_APPROVER_ID"]);
                    }
                    ob.ALT_APPROVER_CODE = (dr["ALT_APPROVER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ALT_APPROVER_CODE"]);
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.APPROVER_ID = (dr["APPROVER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPROVER_ID"]);
                    ob.IS_AUTO_APRVD = (dr["IS_AUTO_APRVD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AUTO_APRVD"]);
                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);
                    ob.LK_ACTION_STATUS_ID = (dr["LK_ACTION_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACTION_STATUS_ID"]);
                    ob.AUTO_GEN_MSG = (dr["AUTO_GEN_MSG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTO_GEN_MSG"]);
                    ob.LK_ACTION_STATUS = (dr["LK_ACTION_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ACTION_STATUS"]);
                    if (dr["EXEC_BY_ID"] != DBNull.Value)
                    {
                        ob.EXEC_BY_ID = Convert.ToInt64(dr["EXEC_BY_ID"]);
                    }
                    if (dr["RF_ACTION_CFG_D11_ID"] != DBNull.Value)
                    {
                        ob.RF_ACTION_CFG_D11_ID = Convert.ToInt64(dr["RF_ACTION_CFG_D11_ID"]);
                    }
                    ob.APPROVER_CODE = (dr["APPROVER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APPROVER_CODE"]);
                    ob.EXEC_BY_CODE = (dr["EXEC_BY_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXEC_BY_CODE"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string submitLvlData(List<RF_ACTION_CFG_LVLModel> obList)
        {
            const string sp = "pkg_admin.rf_action_cfg_lvl_insert";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            foreach (var ob in obList)
            {
                try
                {
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pRF_ACTION_CFG_H_ID", Value = ob.RF_ACTION_CFG_H_ID},
                            new CommandParameter() {ParameterName = "pRF_ACTION_CFG_D1_ID", Value = ob.RF_ACTION_CFG_D1_ID},
                            new CommandParameter() {ParameterName = "pRF_ACTION_CFG_D11_ID", Value = ob.RF_ACTION_CFG_D11_ID},
                            new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                            new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                            new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                            new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                            new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                            new CommandParameter() {ParameterName = "pAPROVER_LVL_NO", Value = ob.APROVER_LVL_NO},
                            new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                            new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                            new CommandParameter() {ParameterName = "pAPPROVER_ID", Value = ob.APPROVER_ID},
                            new CommandParameter() {ParameterName = "pIS_AUTO_APRVD", Value = ob.IS_AUTO_APRVD},
                            new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
                            new CommandParameter() {ParameterName = "pALT_APPROVER_ID", Value = ob.ALT_APPROVER_ID},
                            new CommandParameter() {ParameterName = "pLK_ACTION_STATUS_ID", Value = ob.LK_ACTION_STATUS_ID},
                            new CommandParameter() {ParameterName = "pAUTO_GEN_MSG", Value = ob.AUTO_GEN_MSG},
                            new CommandParameter() {ParameterName = "pEXEC_BY_ID", Value = ob.EXEC_BY_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 1001},
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
            }
            return vMsg;
        }

        public string LvlUpdate()
        {
            const string sp = "pkg_admin.rf_action_cfg_lvl_update";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_LVL_ID", Value = ob.RF_ACTION_CFG_LVL_ID},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_H_ID", Value = ob.RF_ACTION_CFG_H_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pSECTION_ID", Value = ob.SECTION_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pAPROVER_LVL_NO", Value = ob.APROVER_LVL_NO},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pAPPROVER_ID", Value = ob.APPROVER_ID},
                    new CommandParameter() {ParameterName = "pIS_AUTO_APRVD", Value = ob.IS_AUTO_APRVD},
                    new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
                    new CommandParameter() {ParameterName = "pLK_ACTION_STATUS_ID", Value = ob.LK_ACTION_STATUS_ID},
                    new CommandParameter() {ParameterName = "pAUTO_GEN_MSG", Value = ob.AUTO_GEN_MSG},
                    new CommandParameter() {ParameterName = "pEXEC_BY_ID", Value = ob.EXEC_BY_ID},
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
                throw ex;
            }
            return vMsg;
        }

        public string LvlSave()
        {
            const string sp = "pkg_admin.rf_action_cfg_lvl_insert";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_LVL_ID", Value = ob.RF_ACTION_CFG_LVL_ID},
                    new CommandParameter() {ParameterName = "pRF_ACTION_CFG_H_ID", Value = ob.RF_ACTION_CFG_H_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},

                     new CommandParameter() {ParameterName = "pSECTION_ID", Value = ob.SECTION_ID},

                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pAPROVER_LVL_NO", Value = ob.APROVER_LVL_NO},
                    new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                    new CommandParameter() {ParameterName = "pDESIG_ORDER", Value = ob.DESIG_ORDER},
                    new CommandParameter() {ParameterName = "pAPPROVER_ID", Value = ob.APPROVER_ID},
                    new CommandParameter() {ParameterName = "pIS_AUTO_APRVD", Value = ob.IS_AUTO_APRVD},
                    new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
                    new CommandParameter() {ParameterName = "pLK_ACTION_STATUS_ID", Value = ob.LK_ACTION_STATUS_ID},
                    new CommandParameter() {ParameterName = "pAUTO_GEN_MSG", Value = ob.AUTO_GEN_MSG},
                    new CommandParameter() {ParameterName = "pEXEC_BY_ID", Value = ob.EXEC_BY_ID},
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