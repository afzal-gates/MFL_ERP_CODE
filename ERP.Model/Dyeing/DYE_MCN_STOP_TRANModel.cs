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
    public class DYE_MCN_STOP_TRANModel
    {
        public Int64 DYE_MCN_STOP_TRAN_ID { get; set; }
        public DateTime TRAN_DATE { get; set; }
        public DateTime REPORT_TIME { get; set; }
        public Int64 REPORT_BY { get; set; }
        public Int64 DYE_MACHINE_ID { get; set; }
        public DateTime? PRB_ST_TIME { get; set; }
        public DateTime? PRB_END_TIME { get; set; }
        public Int64 LK_MC_MAINT_TYP_ID { get; set; }
        public Int64? MAINT_BY { get; set; }
        public string REMARKS { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_UPDATED { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 RF_RESP_DEPT_ID { get; set; }

        
        public string Save()
        {
            const string sp = "PKG_DYE_MC_MAINTENANCE.dye_mcn_stop_tran_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_TRAN_ID", Value = ob.DYE_MCN_STOP_TRAN_ID},
                     new CommandParameter() {ParameterName = "pTRAN_DATE", Value = ob.TRAN_DATE},
                     new CommandParameter() {ParameterName = "pREPORT_TIME", Value = ob.REPORT_TIME},
                     new CommandParameter() {ParameterName = "pREPORT_BY", Value = ob.REPORT_BY},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pPRB_ST_TIME", Value = ob.PRB_ST_TIME},
                     new CommandParameter() {ParameterName = "pPRB_END_TIME", Value = ob.PRB_END_TIME},
                     new CommandParameter() {ParameterName = "pLK_MC_MAINT_TYP_ID", Value = ob.LK_MC_MAINT_TYP_ID},
                     new CommandParameter() {ParameterName = "pMAINT_BY", Value = ob.MAINT_BY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pIS_UPDATED", Value = ob.IS_UPDATED},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
                     
                     new CommandParameter() {ParameterName = "pXML_MCN", Value = ob.XML_MCN},
                     new CommandParameter() {ParameterName = "pXML_DFCT", Value = ob.XML_DFCT},

                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "SP_DYE_MCN_STOP_TRAN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_TRAN_ID", Value = ob.DYE_MCN_STOP_TRAN_ID},
                     new CommandParameter() {ParameterName = "pTRAN_DATE", Value = ob.TRAN_DATE},
                     new CommandParameter() {ParameterName = "pREPORT_TIME", Value = ob.REPORT_TIME},
                     new CommandParameter() {ParameterName = "pREPORT_BY", Value = ob.REPORT_BY},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pPRB_ST_TIME", Value = ob.PRB_ST_TIME},
                     new CommandParameter() {ParameterName = "pPRB_END_TIME", Value = ob.PRB_END_TIME},
                     new CommandParameter() {ParameterName = "pLK_MC_MAINT_TYP_ID", Value = ob.LK_MC_MAINT_TYP_ID},
                     new CommandParameter() {ParameterName = "pMAINT_BY", Value = ob.MAINT_BY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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
            const string sp = "SP_DYE_MCN_STOP_TRAN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_TRAN_ID", Value = ob.DYE_MCN_STOP_TRAN_ID},
                     new CommandParameter() {ParameterName = "pTRAN_DATE", Value = ob.TRAN_DATE},
                     new CommandParameter() {ParameterName = "pREPORT_TIME", Value = ob.REPORT_TIME},
                     new CommandParameter() {ParameterName = "pREPORT_BY", Value = ob.REPORT_BY},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pPRB_ST_TIME", Value = ob.PRB_ST_TIME},
                     new CommandParameter() {ParameterName = "pPRB_END_TIME", Value = ob.PRB_END_TIME},
                     new CommandParameter() {ParameterName = "pLK_MC_MAINT_TYP_ID", Value = ob.LK_MC_MAINT_TYP_ID},
                     new CommandParameter() {ParameterName = "pMAINT_BY", Value = ob.MAINT_BY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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

        public List<DYE_MCN_STOP_TRANModel> SelectAll(int pageNo, int pageSize)
        {
            string sp = "PKG_DYE_MC_MAINTENANCE.DYE_MCN_STOP_TRAN_SELECT";
            try
            {
                var obList = new List<DYE_MCN_STOP_TRANModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_MCN_STOP_TRANModel ob = new DYE_MCN_STOP_TRANModel();
                    ob.DYE_MCN_STOP_TRAN_ID = (dr["DYE_MCN_STOP_TRAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MCN_STOP_TRAN_ID"]);
                    ob.TRAN_DATE = (dr["TRAN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRAN_DATE"]);
                    ob.REPORT_TIME = (dr["REPORT_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORT_TIME"]);
                    ob.REPORT_BY = (dr["REPORT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REPORT_BY"]);
                    //ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.PRB_ST_TIME = (dr["PRB_ST_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRB_ST_TIME"]);
                    if (dr["PRB_END_TIME"] != DBNull.Value)
                        ob.PRB_END_TIME = Convert.ToDateTime(dr["PRB_END_TIME"]);
                    ob.LK_MC_MAINT_TYP_ID = (dr["LK_MC_MAINT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_MAINT_TYP_ID"]);
                    ob.MAINT_BY = (dr["MAINT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAINT_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.DFCT_TYPE_NAME_EN = (dr["DFCT_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_NAME_EN"]);
                    ob.REPORT_BY_NAME = (dr["REPORT_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REPORT_BY_NAME"]);
                    ob.MAINT_BY_NAME = (dr["MAINT_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAINT_BY_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PERMISSION"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    
                    ob.HR_COMPANY_ID = 1;
                    //ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["D_PRD_CAPACITY"]);
                    //ob.IS_DMY_MCHN = (dr["IS_DMY_MCHN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DMY_MCHN"]);
                    //ob.IS_SMP_BLK = (dr["IS_SMP_BLK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_BLK"]);
                    //ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_RPM"]);
                    //ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    //ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    //ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PCT_EFFCNCY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_MCN_STOP_TRANModel Select(Int64 pDYE_MCN_STOP_TRAN_ID)
        {
            string sp = "PKG_DYE_MC_MAINTENANCE.DYE_MCN_STOP_TRAN_SELECT";
            try
            {
                var ob = new DYE_MCN_STOP_TRANModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_TRAN_ID", Value =pDYE_MCN_STOP_TRAN_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_MCN_STOP_TRAN_ID = (dr["DYE_MCN_STOP_TRAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MCN_STOP_TRAN_ID"]);
                    ob.TRAN_DATE = (dr["TRAN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRAN_DATE"]);
                    ob.REPORT_TIME = (dr["REPORT_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORT_TIME"]);
                    ob.REPORT_BY = (dr["REPORT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REPORT_BY"]);
                    //ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    ob.PRB_ST_TIME = (dr["PRB_ST_TIME"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["PRB_ST_TIME"]);
                    if (dr["PRB_END_TIME"] != DBNull.Value)
                        ob.PRB_END_TIME = Convert.ToDateTime(dr["PRB_END_TIME"]);
                    ob.LK_MC_MAINT_TYP_ID = (dr["LK_MC_MAINT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_MAINT_TYP_ID"]);
                    //ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.MAINT_BY = (dr["MAINT_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAINT_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);

                    ob.XML_DFCT = (dr["XML_DFCT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["XML_DFCT"]);
                    ob.XML_MCN = (dr["XML_MCN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["XML_MCN"]);

                    ob.HR_COMPANY_ID = 1;
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int PERMISSION { get; set; }

        public int TOTAL_REC { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public string MAINT_BY_NAME { get; set; }

        public string REPORT_BY_NAME { get; set; }

        public string DFCT_TYPE_NAME_EN { get; set; }

        public string DYE_MACHINE_NO { get; set; }

        public long DYE_STR_REQ_H_ID { get; set; }

        public string XML_MCN { get; set; }

        public string XML_DFCT { get; set; }
    }
}