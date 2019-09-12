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
    public class GMT_PLN_STYL_CNG_DEPTModel
    {
        public string RESP_DEPT_NAME { get; set; }
        public int NO_OF_PLAN_CHANGE { get; set; }
        public string PLAN_CNGE_REASON { get; set; }
    }
    public class GMT_PLN_STYL_CNGE_LOGModel
    {
        public Int64 GMT_PLN_STYL_CNGE_LOG_ID { get; set; }
        public Int64 GMT_PLN_LINE_LOAD_ID { get; set; }
        public string PLAN_CNGE_REASON { get; set; }
        public string PLAN_CHANGE_TEXT { get; set; }
        public Int64 PLN_CHANE_NO { get; set; }
        public DateTime? DT_PLN_CHANE { get; set; }
        public Int64 PLN_CHANGE_BY { get; set; }
        public Decimal? AVG_OT_HRS { get; set; }
        public Int64? HR_PROD_LINE_ID { get; set; }
        public DateTime? SEW_START_DT { get; set; }
        public DateTime? SEW_END_DT { get; set; }
        public Int64? ALLOCATED_QTY { get; set; }
        public Int64? MC_ORDER_ITEM_PLN_ID { get; set; }
        public Int64? PLAN_MP { get; set; }
        public Decimal? SMV { get; set; }
        public Int32? pOption { get; set; }
        public string XML { get; set; }
        public List<GMT_PLN_STYL_CNGE_RESModel> items { get; set; }
        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_styl_cnge_log_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_STYL_CNGE_LOG_ID", Value = ob.GMT_PLN_STYL_CNGE_LOG_ID},
                     new CommandParameter() {ParameterName = "pPLAN_CNGE_REASON", Value = ob.PLAN_CNGE_REASON},
                     new CommandParameter() {ParameterName = "pPLAN_CHANGE_TEXT", Value = ob.PLAN_CHANGE_TEXT},
                     new CommandParameter() {ParameterName = "pPLN_CHANE_NO", Value = ob.PLN_CHANE_NO},
                     new CommandParameter() {ParameterName = "pDT_PLN_CHANE", Value = ob.DT_PLN_CHANE},
                     new CommandParameter() {ParameterName = "pPLN_CHANGE_BY", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pAVG_OT_HRS", Value = ob.AVG_OT_HRS},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pSEW_START_DT", Value = ob.SEW_START_DT},
                     new CommandParameter() {ParameterName = "pSEW_END_DT", Value = ob.SEW_END_DT},
                     new CommandParameter() {ParameterName = "pALLOCATED_QTY", Value = ob.ALLOCATED_QTY},
                     new CommandParameter() {ParameterName = "pPLAN_MP", Value = ob.PLAN_MP},
                     new CommandParameter() {ParameterName = "pSMV", Value = ob.SMV},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},

                     new CommandParameter() {ParameterName = "pOTHER_ALLOC_XML", Value = ob.OTHER_ALLOC_XML},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"] },
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID},

                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = ob.MC_ORDER_ITEM_PLN_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption ?? 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_GMT_PLN_STYL_CNGE_LOG_ID", Value =0, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                     jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                     if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     } else  
                      {  
                          jsonStr += "}"; 
                      }  
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GMT_PLN_STYL_CNGE_LOGModel Select(Int64 pGMT_PLN_LINE_LOAD_ID,  Int64 pGMT_PLN_STYL_CNGE_LOG_ID)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_styl_cnge_log_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new GMT_PLN_STYL_CNGE_LOGModel();

                if (pGMT_PLN_STYL_CNGE_LOG_ID > 0)
                {

                            OraDatabase db = new OraDatabase();
                            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                             {
                             new CommandParameter() {ParameterName = "pGMT_PLN_STYL_CNGE_LOG_ID", Value = pGMT_PLN_STYL_CNGE_LOG_ID},
                             new CommandParameter() {ParameterName = "pOption", Value = 3001},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                ob.GMT_PLN_STYL_CNGE_LOG_ID = (dr["GMT_PLN_STYL_CNGE_LOG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_STYL_CNGE_LOG_ID"]);
                                ob.PLAN_CNGE_REASON = (dr["PLAN_CNGE_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CNGE_REASON"]);
                                ob.PLAN_CHANGE_TEXT = (dr["PLAN_CHANGE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CHANGE_TEXT"]);
                                ob.PLN_CHANE_NO = (dr["PLN_CHANE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLN_CHANE_NO"]);
                                ob.DT_PLN_CHANE = (dr["DT_PLN_CHANE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DT_PLN_CHANE"]);
                                ob.PLN_CHANGE_BY = (dr["PLN_CHANGE_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLN_CHANGE_BY"]);
                                ob.AVG_OT_HRS = (dr["AVG_OT_HRS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_OT_HRS"]);
                                ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                                ob.SEW_START_DT = (dr["SEW_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_START_DT"]);
                                ob.SEW_END_DT = (dr["SEW_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SEW_END_DT"]);
                                ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOCATED_QTY"]);
                                ob.PLAN_MP = (dr["PLAN_MP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_MP"]);
                                ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                                ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);
                                ob.items = new List<GMT_PLN_STYL_CNGE_RESModel>();
                                ob.items = new GMT_PLN_STYL_CNGE_RESModel().Query(ob.GMT_PLN_STYL_CNGE_LOG_ID);
                            }

                }
                else
                {
                    ob.DT_PLN_CHANE = DateTime.Today.Date;
                    ob.GMT_PLN_LINE_LOAD_ID = pGMT_PLN_LINE_LOAD_ID;
                    ob.PLN_CHANE_NO = new GMT_PLN_STYL_CNGE_RESModel().FindNoOfPlanChange(pGMT_PLN_LINE_LOAD_ID);
                    ob.items = new List<GMT_PLN_STYL_CNGE_RESModel>();
                    ob.items.Add(
                        new GMT_PLN_STYL_CNGE_RESModel()
                        {
                            GMT_PLN_STYL_CNGE_LOG_ID = -1,
                            PCT_SHARE = 100,
                            PLAN_CNGE_REASON = ""

                        }
                    );

                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string OTHER_ALLOC_XML { get; set; }

        public List<GMT_PLN_STYL_CNGE_LOGModel> getPlanChangeLogData(long pGMT_PLN_LINE_LOAD_ID)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_styl_cnge_log_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_STYL_CNGE_LOGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_STYL_CNGE_LOGModel ob = new GMT_PLN_STYL_CNGE_LOGModel();
                    ob.GMT_PLN_STYL_CNGE_LOG_ID = (dr["GMT_PLN_STYL_CNGE_LOG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_STYL_CNGE_LOG_ID"]);
                    ob.PLAN_CNGE_REASON = (dr["PLAN_CNGE_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CNGE_REASON"]);
                    ob.PLAN_CHANGE_TEXT = (dr["PLAN_CHANGE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CHANGE_TEXT"]);
                    ob.PLAN_CHANGE_BY_TXT = (dr["PLAN_CHANGE_BY_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CHANGE_BY_TXT"]);
                    ob.RES_DEPT_TEXT = (dr["RES_DEPT_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RES_DEPT_TEXT"]);
                    ob.PLN_CHANE_NO = (dr["PLN_CHANE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLN_CHANE_NO"]);
                    ob.DT_PLN_CHANE = (dr["DT_PLN_CHANE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DT_PLN_CHANE"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public List<GMT_PLN_STYL_CNGE_LOGModel> getPlanChangeLogForMail()
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_styl_cnge_log_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_STYL_CNGE_LOGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3005 },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_STYL_CNGE_LOGModel ob = new GMT_PLN_STYL_CNGE_LOGModel();
                    ob.GMT_PLN_STYL_CNGE_LOG_ID = (dr["GMT_PLN_STYL_CNGE_LOG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_STYL_CNGE_LOG_ID"]);
                    ob.PLAN_CNGE_REASON = (dr["PLAN_CNGE_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CNGE_REASON"]);
                    ob.PLAN_CHANGE_TEXT = (dr["PLAN_CHANGE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CHANGE_TEXT"]);
                    ob.PLAN_CHANGE_BY_TXT = (dr["PLAN_CHANGE_BY_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CHANGE_BY_TXT"]);
                    ob.RES_DEPT_TEXT = (dr["RES_DEPT_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RES_DEPT_TEXT"]);
                    ob.PLN_CHANE_NO = (dr["PLN_CHANE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLN_CHANE_NO"]);
                    ob.DT_PLN_CHANE = (dr["DT_PLN_CHANE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DT_PLN_CHANE"]);

                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);

                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ALLOCATED_QTY"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ORDER_QTY"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LEAD_TIME"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<GMT_PLN_STYL_CNG_DEPTModel> getPlanChangeDeptForMail()
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_styl_cnge_log_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_STYL_CNG_DEPTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3006 },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_STYL_CNG_DEPTModel ob = new GMT_PLN_STYL_CNG_DEPTModel();
                    ob.PLAN_CNGE_REASON = (dr["PLAN_CNGE_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_CNGE_REASON"]);
                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);
                    ob.NO_OF_PLAN_CHANGE = (dr["NO_OF_PLAN_CHANGE"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_PLAN_CHANGE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public string PLAN_CHANGE_BY_TXT { get; set; }

        public string RES_DEPT_TEXT { get; set; }

        public string LINE_CODE { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public DateTime SHIP_DT { get; set; }

        public DateTime ORD_CONF_DT { get; set; }

        public int ORDER_QTY { get; set; }

        public string ORDER_NO { get; set; }

        public string STYLE_DESC { get; set; }

        public int LEAD_TIME { get; set; }
    }
}