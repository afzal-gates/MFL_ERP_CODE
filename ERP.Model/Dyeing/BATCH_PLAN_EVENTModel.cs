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
    public class BATCH_PLAN_EVENTModel
    {

        public Int64 id {get;set;}
        public string barColor { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public decimal pct_completion { get; set; }
        public Int64  resource { get; set; }
        public string status { get; set; }
        public string text {get;set;}

        public TimeSpan span { get; set; }
        //public Int64 join { get; set; }
        //public string backColor { get; set; }
        //public string barBackColor { get; set; }

        //Event
        public bool clickDisabled { get; set; }
        public bool resizeDisabled { get; set; }
        public bool moveDisabled { get; set; }
        public bool rightClickDisabled { get; set; }

        public Int64 DYE_BATCH_SCDL_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 LK_DYE_MTHD_ID { get; set; }
        public Int64 PRIORITY_NO { get; set; }
        public Int64 RQD_PRD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 REVISION_NO { get; set; }
        public string REV_REASON { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_BATCH_PLAN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_PLAN_ID", Value = ob.id},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value = ob.DYE_BATCH_SCDL_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.resource},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pPRIORITY_NO", Value = ob.PRIORITY_NO},
                     new CommandParameter() {ParameterName = "pRQD_PRD_QTY", Value = ob.RQD_PRD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.start},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.end},
                     //new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     //new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<BATCH_PLAN_EVENTModel> QueryData(DateTime? pSTART_DT, DateTime? pEND_DT)
        {
            string sp = "PKG_DYE_BATCH_PLAN.GET_EVENT_DATA";
            try
            {
                var obList = new List<BATCH_PLAN_EVENTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSTART_DT", Value =pSTART_DT},
                     new CommandParameter() {ParameterName = "pEND_DT", Value =pEND_DT},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BATCH_PLAN_EVENTModel ob = new BATCH_PLAN_EVENTModel();
                    ob.id = (dr["DYE_BATCH_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_PLAN_ID"]);
                    ob.barColor = (dr["RGB_COL_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.start = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.end = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.span = ob.end - ob.start;

                    ob.status = "";
                    ob.text = (dr["TEXT"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["TEXT"]);
                    ob.resource = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);


                    ob.clickDisabled = (dr["IS_EVENT_CLICK"] == DBNull.Value) ? true : Convert.ToString(dr["IS_EVENT_CLICK"]) != "Y";
                    ob.moveDisabled = (dr["IS_EVENT_MOVE"] == DBNull.Value) ? true : Convert.ToString(dr["IS_EVENT_MOVE"]) != "Y";
                    ob.rightClickDisabled = (dr["IS_EVENT_RIGHT_CLICK"] == DBNull.Value) ? true : Convert.ToString(dr["IS_EVENT_RIGHT_CLICK"]) != "Y";
                    ob.resizeDisabled = (dr["IS_EVENT_RESIZE"] == DBNull.Value) ? true : Convert.ToString(dr["IS_EVENT_RESIZE"]) != "Y";

                    ob.DYE_BATCH_SCDL_ID = (dr["DYE_BATCH_SCDL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_SCDL_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    ob.PRIORITY_NO = (dr["PRIORITY_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRIORITY_NO"]);
                    ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PRD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.DURATION_STR = String.Format("{0} days, {1} hours, {2} minutes",ob.span.Days, ob.span.Hours, ob.span.Minutes);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BATCH_PLAN_EVENTModel> loadEventDataRpt(long? pDYE_BATCH_SCDL_ID)
        {
            string sp = "PKG_DYE_BATCH_PLAN.GET_EVENT_DATA";
            try
            {
                var obList = new List<BATCH_PLAN_EVENTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value =pDYE_BATCH_SCDL_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BATCH_PLAN_EVENTModel ob = new BATCH_PLAN_EVENTModel();

                    ob.start = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.end = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);



                    ob.SC_START = (dr["SC_START"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_START"]);
                    ob.SC_END = (dr["SC_END"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_END"]);
                    ob.SC_DURATION = (ob.SC_END - ob.SC_START).Days;

                    ob.span = ob.end - ob.start;

                    ob.status = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);

                    ob.DYE_MACHINE_ID_SL = (dr["DYE_MACHINE_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID_SL"]);
                    ob.DYE_MACHINE_ID_SPAN = (dr["DYE_MACHINE_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID_SPAN"]);

                    ob.RQD_PRD_QTY = (dr["RQD_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PRD_QTY"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.DYE_BATCH_NO_LST = (dr["DYE_BATCH_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO_LST"]);
                    ob.DURATION_STR = String.Format("{0} D, {1} H, {2} M", ob.span.Days, ob.span.Hours, ob.span.Minutes);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string XMLDataSaving(string pXML, Int64? pOption)
        {
            const string sp = "pkg_mc_load_plan.XMLDataSaving";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = pXML},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
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


        public string ORDER_NO_LIST { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public string WORK_STYLE_NO { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public List<BATCH_PLAN_EVENTModel> getDataForDropDown(int pOption)
        {
            string sp = "PKG_DYE_BATCH_PLAN.get_batch_plan_drop_down";
            try
            {
                var obList = new List<BATCH_PLAN_EVENTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BATCH_PLAN_EVENTModel ob = new BATCH_PLAN_EVENTModel();
                    ob.id = (dr["ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ID"]);
                    ob.text = (dr["TEXT"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["TEXT"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DURATION_STR { get; set; }



        public long DYE_MACHINE_ID_SL { get; set; }

        public long DYE_MACHINE_ID_SPAN { get; set; }

        public string DYE_MACHINE_NO { get; set; }

        public DateTime SC_START { get; set; }

        public DateTime SC_END { get; set; }

        public int SC_DURATION { get; set; }
        public string DYE_BATCH_NO_LST { get; set; }
    }
}