using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data;

namespace ERP.Model
{
    public class ATTN_CORR_DATA_VM
    {

        public Int64? HR_TA_PROCESS_DATA_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string TA_PROXI_ID { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string CORRECTION_REASON_OTHER { get; set; }
        public string TA_FLAG { get; set; }
        public DateTime? ATTEN_DATE { get; set; }
        public DateTime? IN_TIME_WT { get; set; }
        public DateTime? OUT_TIME_WT { get; set; }
        public DateTime? OT_HR { get; set; }
        public Int64? HR_DAY_TYPE_ID { get; set; }
        public Int64 PUNCH_TYPE { get; set; }

        public Int64? LK_PN_COR_REASON_ID { get; set; }

        public string LK_PN_CORR_STATUS { get; set; }

        public string OT_HR_STRING { get; set; }
        public string IS_SELF { get; set; }

        public List<ATTN_CORR_DATA_VM> LoadTaData(ATTN_CORR_VM ob)
        {
            const string sp = "pkg_attendance.attn_corr_load_ta_data";
            try
            {
                var obList = new List<ATTN_CORR_DATA_VM>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = (ob.CORRECTED_STATUS == "H" || ob.CORRECTED_STATUS == "W" || ob.CORRECTED_STATUS == "F")? 3001 : 3000},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pMONTH_VALUE", Value = ob.MONTH_VALUE},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},

                    new CommandParameter() {ParameterName = "pCORRECTION_TYPE", Value = ob.CORRECTION_TYPE},
                    new CommandParameter() {ParameterName = "pLK_PN_COR_REASON_ID", Value = ob.LK_PN_COR_REASON_ID},
                    new CommandParameter() {ParameterName = "pIS_CORRECTION_REASON_OTHER", Value = ob.IS_CORRECTION_REASON_OTHER},
                    new CommandParameter() {ParameterName = "pCORRECTION_REASON_OTHER", Value = ob.CORRECTION_REASON_OTHER},
                    new CommandParameter() {ParameterName = "pPUNCH_TYPE", Value = ob.PUNCH_TYPE},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pCORRECTED_STATUS", Value = ob.CORRECTED_STATUS},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value =ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pOT_HR", Value = ob.OT_HR},
                    new CommandParameter() {ParameterName = "pFLOOR", Value = ob.FLOOR},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_IDS", Value = ob.HR_EMPLOYEE_IDS},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.EMPLOYEE_TYPE_ID},                    
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.HR_DAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ATTN_CORR_DATA_VM obRead = new ATTN_CORR_DATA_VM();
                    obRead.HR_TA_PROCESS_DATA_ID = (dr["HR_TA_PROCESS_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_TA_PROCESS_DATA_ID"]);
                    obRead.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    obRead.PUNCH_TYPE = ob.PUNCH_TYPE;
                    obRead.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    obRead.TA_PROXI_ID = (dr["TA_PROXI_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_PROXI_ID"]);
                    obRead.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    obRead.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    obRead.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    obRead.TA_FLAG = (dr["TA_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_FLAG"]);
                    obRead.ATTEN_DATE = (dr["ATTEN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ATTEN_DATE"]);

                    if (dr["IN_TIME_WT"] != DBNull.Value)
                    {
                        obRead.IN_TIME_WT = Convert.ToDateTime(dr["IN_TIME_WT"]);
                    }

                    if (dr["OUT_TIME_WT"] != DBNull.Value)
                    {
                        obRead.OUT_TIME_WT = Convert.ToDateTime(dr["OUT_TIME_WT"]);
                    }

                    obRead.OT_HR = (dr["OT_HR"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OT_HR"]);
                    obRead.OT_HR_STRING = String.Format("{0:HH:mm}", obRead.OT_HR);
                    if (dr["LK_PN_COR_REASON_ID"] != DBNull.Value)
                    {
                        obRead.LK_PN_COR_REASON_ID = Convert.ToInt64(dr["LK_PN_COR_REASON_ID"]);
                    }
                    obRead.LK_PN_CORR_STATUS = (dr["LK_PN_CORR_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PN_CORR_STATUS"]);
                    obRead.IS_SELF = (dr["IS_SELF"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SELF"]);
                    if (dr["HR_DAY_TYPE_ID"] != DBNull.Value)
                    {
                        obRead.HR_DAY_TYPE_ID = Convert.ToInt64(dr["HR_DAY_TYPE_ID"]);
                    }
                    obList.Add(obRead);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UupdateAttnCorrData(List<ATTN_CORR_DATA_VM> obList)
        {
            const string sp = "pkg_attendance.update_attn_corr_data";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            foreach (var ob in obList)
            {
                try
                {
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pHR_TA_PROCESS_DATA_ID", Value = ob.HR_TA_PROCESS_DATA_ID},
                            new CommandParameter() {ParameterName = "pLK_PN_COR_REASON_ID", Value = ob.LK_PN_COR_REASON_ID},
                            new CommandParameter() {ParameterName = "pPUNCH_TYPE", Value = ob.PUNCH_TYPE},
                            new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                            new CommandParameter() {ParameterName = "pCORRECTION_REASON_OTHER", Value = ob.CORRECTION_REASON_OTHER},
                            new CommandParameter() {ParameterName = "pATTEN_DATE", Value = ob.ATTEN_DATE},
                            new CommandParameter() {ParameterName = "pIN_TIME_WT", Value = ob.IN_TIME_WT},
                            new CommandParameter() {ParameterName = "pOUT_TIME_WT", Value = ob.OUT_TIME_WT},
                            new CommandParameter() {ParameterName = "pOT_HR", Value = ob.OT_HR},
                            new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                            new CommandParameter() {ParameterName = "pOption", Value = 2000},
                            new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.HR_DAY_TYPE_ID},
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
        public string saveBatchData(List<ATTN_CORR_DATA_VM> obList)
        {
            const string sp = "pkg_attendance.attn_corr_update_batch_data";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            foreach (var ob in obList)
            {
                try
                {
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pHR_TA_PROCESS_DATA_ID", Value = ob.HR_TA_PROCESS_DATA_ID},
                            new CommandParameter() {ParameterName = "pLK_PN_COR_REASON_ID", Value = ob.LK_PN_COR_REASON_ID},
                            new CommandParameter() {ParameterName = "pOUT_TIME_WT", Value = ob.OUT_TIME_WT},
                            new CommandParameter() {ParameterName = "pPUNCH_TYPE", Value = ob.PUNCH_TYPE},
                            new CommandParameter() {ParameterName = "pIN_TIME_WT", Value = ob.IN_TIME_WT},
                            new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.HR_DAY_TYPE_ID},
                            new CommandParameter() {ParameterName = "pCORRECTION_REASON_OTHER", Value = ob.CORRECTION_REASON_OTHER},
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
                    throw ex;
                }

            }
            return vMsg;
        }




       
    }
}