using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Dynamic;

namespace ERP.Model
{
    public class GMT_PLN_LINE_LD_CPModel
    {
        public Int64 GMT_PLN_LINE_LD_CP_ID { get; set; }
        public Int64 GMT_PLN_LINE_LOAD_ID { get; set; }
        public Int64 PARENT_TNA_TASK_D_ID { get; set; }
        public Int64 MC_TNA_TASK_D_ID { get; set; }
        public DateTime PLAN_DT { get; set; }
        public DateTime REQ_DT_AFTER_DELAY { get; set; }
        public DateTime? ACTUAL_DT { get; set; }
        public Decimal TNA_FAIL_SCORE { get; set; }
        public string TA_TASK_NAME_EN { get; set; }
        public string XML { get; set; }
        public long PRIOR_TNA_TASK_D_ID { get; set; }
        public long STD_DAYS { get; set; }
        public DateTime? EXCEPTED_DATE { get; set; }
        public long DAYS_DELAY_EARLIER { get; set; }
        public string REMARKS { get; set; }
        public DateTime? REF_PLAN_DATE { get; set; }
        public string IS_SD_RD { get; set; }
        public long STD_DAYS_ORI { get; set; }

        public List<GMT_PLN_LINE_LD_CPModel> Query(Int64 pGMT_PLN_LINE_LOAD_ID)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_line_ld_cp_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_LINE_LD_CPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value =pGMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_PLN_LINE_LD_CPModel ob = new GMT_PLN_LINE_LD_CPModel();
                            ob.GMT_PLN_LINE_LD_CP_ID = (dr["GMT_PLN_LINE_LD_CP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LD_CP_ID"]);
                            ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);

                           
                           
                            if (dr["PARENT_TNA_TASK_D_ID"] != DBNull.Value)
                            {
                                ob.PARENT_TNA_TASK_D_ID = Convert.ToInt64(dr["PARENT_TNA_TASK_D_ID"]);
                            }

                            if (dr["PRIOR_TNA_TASK_D_ID"] != DBNull.Value)
                            {
                                ob.PRIOR_TNA_TASK_D_ID = Convert.ToInt64(dr["PRIOR_TNA_TASK_D_ID"]);
                            }

                            ob.MC_TNA_TASK_D_ID = (dr["MC_TNA_TASK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_D_ID"]);
                            ob.PLAN_DT = (dr["PLAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PLAN_DT"]);
                            ob.REQ_DT_AFTER_DELAY = (dr["REQ_DT_AFTER_DELAY"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REQ_DT_AFTER_DELAY"]);
                            if (dr["ACTUAL_DT"] != DBNull.Value)
                            {
                                ob.ACTUAL_DT =  Convert.ToDateTime(dr["ACTUAL_DT"]);
                            }
                            ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                            ob.TNA_FAIL_SCORE = (dr["TNA_FAIL_SCORE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TNA_FAIL_SCORE"]);

                            ob.STD_DAYS = (dr["STD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_DAYS"]);

                            ob.STD_DAYS_ORI = ob.STD_DAYS;

                            if (dr["EXCEPTED_DATE"] != DBNull.Value)
                            {
                                ob.EXCEPTED_DATE = Convert.ToDateTime(dr["EXCEPTED_DATE"]);
                            }

                            if (dr["REF_PLAN_DATE"] != DBNull.Value)
                            {
                                ob.REF_PLAN_DATE = Convert.ToDateTime(dr["REF_PLAN_DATE"]);
                            }

                            ob.DAYS_DELAY_EARLIER = (dr["DAYS_DELAY_EARLIER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_DELAY_EARLIER"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.IS_SD_RD = "N";

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateLineLoadCpData()
        {
            const string sp = "pkg_gmt_pln_line_load.update_line_load_cp_data";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
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



        public object GetCpSummeryData(long pGMT_PLN_LINE_LOAD_ID)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_line_ld_cp_select";
            //pOption=3000=>Select All Data
            try
            {
                Object obj = new { };
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value =pGMT_PLN_LINE_LOAD_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obj = new
                    {
                        TNA_1_NAME = (dr["TNA_1_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_1_NAME"]),
                        TNA_1_VALUE = (dr["TNA_1_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TNA_1_VALUE"]),

                        TNA_2_NAME = (dr["TNA_2_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_2_NAME"]),
                        TNA_2_VALUE = (dr["TNA_2_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TNA_2_VALUE"]),

                        TNA_3_NAME = (dr["TNA_3_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_3_NAME"]),
                        TNA_3_VALUE = (dr["TNA_3_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TNA_3_VALUE"]),

                        TNA_4_NAME = (dr["TNA_4_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TNA_4_NAME"]),
                        TNA_4_VALUE = (dr["TNA_4_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TNA_4_VALUE"])
                    };
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public dynamic FindProdMonitoringData(long pMC_ORDER_ITEM_PLN_ID)
        {
            dynamic ob = new ExpandoObject();
            ob.datas = new List<dynamic>();
            ob.lines = new List<dynamic>();

            string sp = "PKG_GMT_PLN_LINE_LOAD.select_prod_monitoring_data";
            //pOption=3000=>Select All Data
            try
            {
                OraDatabase db = new OraDatabase();

                var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr3 in ds3.Tables[0].Rows)
                {
                    ob.STYLE_NO = (dr3["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["STYLE_NO"]);
                    ob.ORDER_NO = (dr3["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["ORDER_NO"]);
                    ob.BYR_ACC_NAME_EN = (dr3["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["BYR_ACC_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr3["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr3["ITEM_NAME_EN"]);
                    ob.ORDER_QTY = (dr3["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["ORDER_QTY"]);
                    ob.ORD_CONF_DT = (dr3["ORD_CONF_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr3["ORD_CONF_DT"]);
                    ob.SHIP_DT = (dr3["SHIP_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr3["SHIP_DT"]);
                    ob.INITIAL_PROD_QTY = (dr3["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["INITIAL_PROD_QTY"]);
                    ob.ALLOCATED_QTY = (dr3["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["ALLOCATED_QTY"]);
                    ob.TOT_PROD = (dr3["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr3["TOT_PROD"]);
                    ob.TTL_PROD = ob.INITIAL_PROD_QTY + ob.TOT_PROD;
                }


                var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                {
                    ob.lines.Add(new {
                        INITIAL_PROD_QTY = (dr2["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["INITIAL_PROD_QTY"]),
                        TOT_PROD = (dr2["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["TOT_PROD"]),
                        ALLOCATED_QTY = (dr2["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["ALLOCATED_QTY"]),
                        LINE_CODE = (dr2["LINE_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["LINE_CODE"]),
                        OUT_OF_PLAN_PROD_REC = (dr2["OUT_OF_PLAN_PROD_REC"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["OUT_OF_PLAN_PROD_REC"]),
                        TTL_PROD =  ((dr2["INITIAL_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["INITIAL_PROD_QTY"]))+  ((dr2["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["TOT_PROD"])),
                        HR_PROD_LINE_ID = (dr2["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["HR_PROD_LINE_ID"])
                    });
                }
                

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    IDictionary<string, object> obObj = new ExpandoObject();
                    obObj["GMT_PROD_PLN_CLNDR_ID"] = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    obObj["CALENDAR_DT"] = (dr["CALENDAR_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr["CALENDAR_DT"]);

                    foreach ( dynamic line in  ob.lines) {

                        obObj[line.LINE_CODE] = new ExpandoObject();
                            var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                    new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = line.HR_PROD_LINE_ID },
                                    new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value = pMC_ORDER_ITEM_PLN_ID},
                                    new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =   obObj["GMT_PROD_PLN_CLNDR_ID"] },
                                    new CommandParameter() {ParameterName = "pOption", Value = 3001},
                                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                                {

                                    obObj[line.LINE_CODE] = new
                                    {
                                        REQ_EFF = (dr1["REQ_EFF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["REQ_EFF"]),
                                        RF_PLAN_EFF = (dr1["RF_PLAN_EFF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["RF_PLAN_EFF"]),
                                        REQ_PROD_QTY = (dr1["REQ_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["REQ_PROD_QTY"]),
                                        TOT_PROD = (dr1["TOT_PROD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TOT_PROD"]),
                                        REQ_PER_HR = (dr1["REQ_PER_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["REQ_PER_HR"]),
                                        STS_FLAG = (dr1["STS_FLAG"] == DBNull.Value) ? "U" : Convert.ToString(dr1["STS_FLAG"])
                                    };
                                }

                            }
                            else
                            {

                                obObj[line.LINE_CODE] = new
                                {
                                    REQ_EFF = -1,
                                    RF_PLAN_EFF = -1,
                                    REQ_PROD_QTY = -1,
                                    TOT_PROD = -1,
                                    REQ_PER_HR = -1,
                                    STS_FLAG = -1
                                };
                            }
                       
                    }
                    ob.datas.Add(obObj);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}