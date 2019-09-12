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
    public class GMT_IE_TARGET_HModel
    {
        public DateTime RF_CALENDAR_DATE { get; set; }
        public Int32 HR_PROD_FLR_ID { get; set; }
        public string XML { get; set; }

        public Int32 HOURLY_TARGET { get; set; }
        public Int32 DAILY_TARGET { get; set; }
        public Int64 INPUT_MIN { get; set; }
        public Int64 PRODUCED_MIN { get; set; }
        public Int32? H1_HR { get; set; }
        public Int32? H2_HR { get; set; }
        public Int32? H3_HR { get; set; }
        public Int32? H4_HR { get; set; }
        public Int32? H5_HR { get; set; }
        public Int32? H6_HR { get; set; }
        public Int32? H7_HR { get; set; }
        public Int32? H8_HR { get; set; }
        public Int32? ACHV_QTY_GWH { get; set; }
        public Int32? ACHV_QTY_OTWH { get; set; }
        public Int64 NPT { get; set; }
        public Int64 WIP { get; set; }
        public List<GMT_IE_TARGET_LNModel> lines { get; set; }
        public List<GMT_IE_TARGETModel> items { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64 GMT_PLN_CLNDR_MN_ID { get; set; }
        public Int32? pOption { get; set; }

        public string Save()
        // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_IE.gmt_ie_target_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},

                     new CommandParameter() {ParameterName = "pXML_PROD", Value = ob.XML_PROD},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pGMT_PLN_CLNDR_MN_ID", Value = ob.GMT_PLN_CLNDR_MN_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =ob.pOption ?? 1000},
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

        public string SaveOutOfPlanProd()
        // pOption=1000=>General Form Save
        {
            const string sp = "PKG_GMT_IE.gmt_ie_target_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = ob.GMT_PLN_LINE_LOAD_ID },
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID },
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID },
                     new CommandParameter() {ParameterName = "pGMT_PLN_CLNDR_MN_ID", Value = ob.GMT_PLN_CLNDR_MN_ID },
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID },
                     new CommandParameter() {ParameterName = "pPROD_QTY", Value = ob.PROD_QTY },
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 1003 },
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

        public string XML_PROD { get; set; }

        public string FLOOR_CODE { get; set; }

        public decimal TTL_VALUE { get; set; }

        public long TARGET_QTY_OTWH { get; set; }

        public decimal OTWH { get; set; }

        public int? ACHV_QTY { get; set; }

        public int TTL_TARFET { get; set; }

        public int TTL_ACHIEVE { get; set; }

        public int CUR_OPERATOR { get; set; }

        public int CUR_HELPER { get; set; }

        public long USED_OP { get; set; }

        public long USED_HLP { get; set; }

        public int PLAN_OP { get; set; }

        public int PLAN_HP { get; set; }

        public int DAY_TARGET { get; set; }
        public Int32 HR_PROD_LINE_ID { get; set; }
        public Int32 GMT_PLN_LINE_LOAD_ID { get; set; }
        public Int32 PROD_QTY { get; set; }
    }
    public class GMT_IE_TARGET_LNModel
    {
        public Int32 HR_PROD_LINE_ID { get; set; }
        public string LINE_CODE { get; set; }
        public Int32 TTL_ACTV_POINT { get; set; }
        public Int32 HOURLY_TARGET { get; set; }
        public Int32 DAILY_TARGET { get; set; }
        public Int64 INPUT_MIN { get; set; }
        public Int64 PRODUCED_MIN { get; set; }
        public Int32? H1_HR { get; set; }
        public Int32? H2_HR { get; set; }
        public Int32? H3_HR { get; set; }
        public Int32? H4_HR { get; set; }
        public Int32? H5_HR { get; set; }
        public Int32? H6_HR { get; set; }
        public Int32? H7_HR { get; set; }
        public Int32? H8_HR { get; set; }
        public Int32? ACHV_QTY_GWH { get; set; }
        public Int32? ACHV_QTY_OTWH { get; set; }
        public Int64 NPT { get; set; }
        public Int64 WIP { get; set; }
        public List<GMT_IE_TARGETModel> items { get; set; }
        public long GMT_PROD_PLN_CLNDR_ID { get; set; }
        public string MC_ORDER_STYL_LST { get; set; }
        public decimal PERFORMANCE { get; set; }
        public decimal EFFICIENCY { get; set; }
        public int RWH { get; set; }
        public long? HOURLY_PROD { get; set; }
        public long HRLY_TARGET { get; set; }

        public long H15_P_WH_ADD { get; set; }

        public long H15_P_MP_ADD { get; set; }

        public long H15_MP_ADD { get; set; }

        public long H14_MP_ADD { get; set; }

        public long H13_MP_ADD { get; set; }

        public long H12_MP_ADD { get; set; }

        public long H11_MP_ADD { get; set; }

        public long H10_MP_ADD { get; set; }
        public long H9_MP_ADD { get; set; }
        public long USED_MP { get; set; }
        public int OTWH { get; set; }
        public int ACHV_QTY { get; set; }
        public int DAY_TARGET { get; set; }

        public long GMT_PLN_CLNDR_MN_ID { get; set; }

        public int TTL_TARFET { get; set; }

        public int TTL_ACHIEVE { get; set; }

        public long GMT_PROD_PLN_CLNDR_ID_C { get; set; }

        public int RF_PFLT_RSN_TYPE_ID { get; set; }

        public string RSN_TYPE_NAME_EN { get; set; }

        public string IS_IN_PLAN { get; set; }
    }
    public class GMT_SEW_PRODModel
    {
        public Int64 GMT_SEW_PROD_ID { get; set; }
        public Int64 GMT_IE_TARGET_ID { get; set; }
        public Int64 HOUR_NO { get; set; }
        public Int64 PROD_QTY { get; set; }
        public Int64 NPT { get; set; }
        public Int64 RF_RESP_DEPT_ID { get; set; }
        public Int64 GMT_IE_NPT_REASON_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string REASON_CODE { get; set; }
        public string COLR_CODE { get; set; }
        public string REASON_DESC_EN { get; set; }
        public bool IS_LOCKED { get; set; }
    }
    public class GMT_IE_TARGETModel
    {
        public Int64 GMT_IE_TARGET_ID { get; set; }
        public Int64 GMT_PLN_LINE_LOAD_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public Decimal TARGET_EFF { get; set; }
        public Int64 DAY_NO { get; set; }
        public Int64 USED_OP { get; set; }
        public Int64 USED_HLP { get; set; }
        public Int64 TARGET_WH { get; set; }
        public Int64 GWH { get; set; }
        public Int64 TARGET_QTY_GWH { get; set; }
        public Int32? ACHV_QTY_GWH { get; set; }
        public Decimal OTWH { get; set; }
        public Int64 TARGET_QTY_OTWH { get; set; }
        public Int32? ACHV_QTY_OTWH { get; set; }
        public Int32 HRLY_TARGET { get; set; }
        public Int32 DAY_TARGET { get; set; }
        public Int64 R_BEGIN_HR { get; set; }
        public Int64 R_END_HR { get; set; }
        public Int64 GMT_IE_NPT_REASON_ID { get; set; }
        public string REASON_OF_EOT { get; set; }
        public string IS_CLOSED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public Int32? H1_HR { get; set; }
        public Int32? H2_HR { get; set; }
        public Int32? H3_HR { get; set; }
        public Int32? H4_HR { get; set; }
        public Int32? H5_HR { get; set; }
        public Int32? H6_HR { get; set; }
        public Int32? H7_HR { get; set; }
        public Int32? H8_HR { get; set; }
        public Int32? H9_HR { get; set; }
        public Int32? H10_HR { get; set; }
        public Int32? H11_HR { get; set; }
        public Int32? H12_HR { get; set; }
        public Int32? H13_HR { get; set; }
        public Int32? H14_HR { get; set; }
        public Int32? H15_HR { get; set; }
        public Int32? H16_HR { get; set; }
        public Int32? H17_HR { get; set; }
        public Int32? H18_HR { get; set; }
        public Int32? H19_HR { get; set; }
        public Int32? H20_HR { get; set; }
        public Int32? H21_HR { get; set; }
        public string IS_TRGT_LOCK { get; set; }
        public GMT_IE_TARGET_LNModel line { get; set; }

        public GMT_IE_TARGET_HModel GetDataForTargetSetting(DateTime pRF_CALENDAR_DATE, Int32 pHR_PROD_FLR_ID)
        {
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new GMT_IE_TARGET_HModel();
                ob.RF_CALENDAR_DATE = pRF_CALENDAR_DATE;
                ob.HR_PROD_FLR_ID = pHR_PROD_FLR_ID;
                ob.lines = new List<GMT_IE_TARGET_LNModel>();
                ob.lines = this.QueryPmSewPlanLines(pRF_CALENDAR_DATE, pHR_PROD_FLR_ID);
                ob.ACHV_QTY_GWH = ob.lines.Sum(x => x.ACHV_QTY_GWH);
                ob.ACHV_QTY_OTWH = ob.lines.Sum(x => x.ACHV_QTY_OTWH);
                ob.HOURLY_TARGET = (Int32)ob.lines.Average(x => x.HOURLY_TARGET);
                ob.DAILY_TARGET = ob.lines.Sum(x => x.DAILY_TARGET);
                ob.INPUT_MIN = ob.lines.Sum(x => x.INPUT_MIN);
                ob.PRODUCED_MIN = ob.lines.Sum(x => x.PRODUCED_MIN);
                ob.DAY_TARGET = ob.lines.Sum(x => x.DAY_TARGET);
                ob.HOURLY_TARGET = (Int32)ob.lines.Sum(x => x.HOURLY_TARGET);

                ob.WIP = ob.lines.Sum(x => x.WIP);
                ob.H1_HR = ob.lines.Sum(x => x.H1_HR);
                ob.H2_HR = ob.lines.Sum(x => x.H2_HR);
                ob.H3_HR = ob.lines.Sum(x => x.H3_HR);
                ob.H4_HR = ob.lines.Sum(x => x.H4_HR);
                ob.H5_HR = ob.lines.Sum(x => x.H5_HR);
                ob.H6_HR = ob.lines.Sum(x => x.H6_HR);
                ob.H7_HR = ob.lines.Sum(x => x.H7_HR);
                ob.H8_HR = ob.lines.Sum(x => x.H8_HR);


                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<GMT_IE_TARGET_HModel> GetDataForTargetSettingBoard(DateTime pRF_CALENDAR_DATE, string pHR_PROD_FLR_LST)
        {
            string sp = "PKG_GMT_IE.gmt_ie_target_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_IE_TARGET_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_CALENDAR_DATE", Value =pRF_CALENDAR_DATE},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_LST", Value =  pHR_PROD_FLR_LST},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_IE_TARGET_HModel ob = new GMT_IE_TARGET_HModel();

                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_FLR_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.GMT_PLN_CLNDR_MN_ID = (dr["GMT_PLN_CLNDR_MN_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GMT_PLN_CLNDR_MN_ID"]);
                    ob.FLOOR_CODE = (dr["FLOOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_CODE"]);
                    ob.items = new List<GMT_IE_TARGETModel>();
                    ob.items = this.QueryPmSewPlanDBoard(ob.GMT_PROD_PLN_CLNDR_ID, ob.HR_PROD_FLR_ID, ob.GMT_PLN_CLNDR_MN_ID);
                    ob.ACHV_QTY_GWH = ob.items.Sum(x => x.ACHV_QTY_GWH);
                    ob.ACHV_QTY_OTWH = ob.items.Sum(x => x.ACHV_QTY_OTWH);

                    ob.ACHV_QTY = ob.items.Sum(x => x.ACHV_QTY_GWH + x.ACHV_QTY_OTWH);

                    ob.TARGET_QTY_OTWH = ob.items.Sum(x => x.TARGET_QTY_OTWH);
                    ob.DAILY_TARGET = ob.items.Sum(x => x.DAY_TARGET);
                    ob.OTWH = ob.items.Sum(x => x.OTWH);
                    ob.TTL_VALUE = ob.items.Sum(x => x.TTL_VALUE);

                    ob.INPUT_MIN = ob.items.Sum(x => x.line.INPUT_MIN);
                    ob.PRODUCED_MIN = ob.items.Sum(x => x.line.PRODUCED_MIN);

                    ob.TTL_TARFET = ob.items.Sum(x => x.line.TTL_TARFET);
                    ob.TTL_ACHIEVE = ob.items.Sum(x => x.line.TTL_ACHIEVE);

                    ob.CUR_OPERATOR = ob.items.Where(y => y.HR_PROD_LINE_ID_SL == 1).Sum(x => x.CUR_OPERATOR);
                    ob.CUR_HELPER = ob.items.Where(y => y.HR_PROD_LINE_ID_SL == 1).Sum(x => x.CUR_HELPER);

                    ob.USED_OP = ob.items.Where(y => y.HR_PROD_LINE_ID_SL == 1).Sum(x => x.USED_OP);
                    ob.USED_HLP = ob.items.Where(y => y.HR_PROD_LINE_ID_SL == 1).Sum(x => x.USED_HLP);

                    ob.PLAN_OP = ob.items.Where(y => y.HR_PROD_LINE_ID_SL == 1).Sum(x => x.PLAN_OP);
                    ob.PLAN_HP = ob.items.Where(y => y.HR_PROD_LINE_ID_SL == 1).Sum(x => x.PLAN_HP);

                    ob.WIP = ob.items.Sum(x => x.WIP);
                    ob.H1_HR = ob.items.Sum(x => x.H1_HR);
                    ob.H2_HR = ob.items.Sum(x => x.H2_HR);
                    ob.H3_HR = ob.items.Sum(x => x.H3_HR);
                    ob.H4_HR = ob.items.Sum(x => x.H4_HR);
                    ob.H5_HR = ob.items.Sum(x => x.H5_HR);
                    ob.H6_HR = ob.items.Sum(x => x.H6_HR);
                    ob.H7_HR = ob.items.Sum(x => x.H7_HR);
                    ob.H8_HR = ob.items.Sum(x => x.H8_HR);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<GMT_IE_TARGET_LNModel> QueryPmSewPlanLines(DateTime pRF_CALENDAR_DATE, Int32 pHR_PROD_FLR_ID)
        {
            string sp = "PKG_GMT_IE.gmt_ie_target_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_IE_TARGET_LNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_CALENDAR_DATE", Value =pRF_CALENDAR_DATE},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =  pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_IE_TARGET_LNModel ob = new GMT_IE_TARGET_LNModel();


                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_LINE_ID"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.TTL_ACTV_POINT = (dr["TTL_ACTV_POINT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TTL_ACTV_POINT"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);

                    ob.GMT_PROD_PLN_CLNDR_ID_C = (dr["GMT_PROD_PLN_CLNDR_ID_C"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID_C"]);

                    ob.GMT_PLN_CLNDR_MN_ID = (dr["GMT_PLN_CLNDR_MN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_CLNDR_MN_ID"]);

                    ob.MC_ORDER_STYL_LST = (dr["MC_ORDER_STYL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_STYL_LST"]);

                    ob.RF_PFLT_RSN_TYPE_ID = (dr["RF_PFLT_RSN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_PFLT_RSN_TYPE_ID"]);
                    ob.RSN_TYPE_NAME_EN = (dr["RSN_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_NAME_EN"]);

                    ob.items = new List<GMT_IE_TARGETModel>();
                    ob.items = this.QueryPmSewPlanD(ob.GMT_PROD_PLN_CLNDR_ID, ob.HR_PROD_LINE_ID, ob.GMT_PROD_PLN_CLNDR_ID_C);
                    //ob.IS_TRGT_LOCK = ob.items.Any(x => x.IS_TRGT_LOCK == "Y") ? "Y" : "N";
                    ob.ACHV_QTY_GWH = ob.items.Sum(x => x.ACHV_QTY_GWH);
                    ob.ACHV_QTY_OTWH = ob.items.Sum(x => x.ACHV_QTY_OTWH);

                    ob.NPT = (dr["NPT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NPT"]);

                    if (ob.items.Sum(x => x.TARGET_WH) > 0)
                    {
                        ob.HRLY_TARGET = ob.items.Sum(x => x.TARGET_WH) > 0 ? ob.items.Sum(x => x.HRLY_TARGET) / ob.items.Sum(x => x.TARGET_WH) : 0;
                    }
                    else
                    {
                        ob.HRLY_TARGET =  0;
                    }
                    
                    ob.DAY_TARGET = ob.items.Sum(x => x.DAY_TARGET);
                  
                    ob.RWH = (dr["R_WH"] == DBNull.Value) ? 1 : Convert.ToInt32(dr["R_WH"]);
                    
                    ob.USED_MP = (dr["USED_MP"] == DBNull.Value) ? 1 : Convert.ToInt64(dr["USED_MP"]);
                    ob.H9_MP_ADD = ((dr["H9_MP_ADD"] == DBNull.Value) || ob.RWH <= 8 ) ? 0 : Convert.ToInt64(dr["H9_MP_ADD"]) * 60;
                    ob.H10_MP_ADD = ((dr["H10_MP_ADD"] == DBNull.Value) || ob.RWH <= 9 )  ? 0 : Convert.ToInt64(dr["H10_MP_ADD"])  * 60;
                    ob.H11_MP_ADD =((dr["H11_MP_ADD"] == DBNull.Value) || ob.RWH <= 10 )  ? 0 : Convert.ToInt64(dr["H11_MP_ADD"])  * 60;
                    ob.H12_MP_ADD = ((dr["H12_MP_ADD"] == DBNull.Value) || ob.RWH <= 11 )  ? 0 : Convert.ToInt64(dr["H12_MP_ADD"]) * 60;
                    ob.H13_MP_ADD = ((dr["H13_MP_ADD"] == DBNull.Value) || ob.RWH <= 12 )  ? 0 : Convert.ToInt64(dr["H13_MP_ADD"]) * 60;
                    ob.H14_MP_ADD = ((dr["H14_MP_ADD"] == DBNull.Value) || ob.RWH <= 13 )  ? 0 : Convert.ToInt64(dr["H14_MP_ADD"]) * 60;
                    ob.H15_MP_ADD = ((dr["H15_MP_ADD"] == DBNull.Value) || ob.RWH <= 14 )  ? 0 : Convert.ToInt64(dr["H15_MP_ADD"])  * 60;
                    ob.H15_P_MP_ADD = ((dr["H15_P_MP_ADD"] == DBNull.Value) || ob.RWH <= 15) ? 0 : Convert.ToInt64(dr["H15_P_MP_ADD"]) * (ob.RWH - 15) * 60;
                    ob.INPUT_MIN = (((ob.RWH < 8) ? ob.RWH: 8) * ob.USED_MP * 60) + ob.H9_MP_ADD + ob.H10_MP_ADD + ob.H11_MP_ADD + ob.H12_MP_ADD + ob.H13_MP_ADD + ob.H14_MP_ADD + ob.H15_MP_ADD + ob.H15_P_MP_ADD;
                    ob.PRODUCED_MIN = ob.items.Sum(x => x.PRODUCED_MIN);
                    ob.EFFICIENCY = ob.INPUT_MIN > 0 ? (ob.PRODUCED_MIN / ob.INPUT_MIN) * 100 : 0;
                    ob.PERFORMANCE = ob.INPUT_MIN > 0 ? (ob.PRODUCED_MIN / (ob.INPUT_MIN - ob.NPT)) * 100 : 0;
                   

                    ob.HOURLY_PROD = ob.RWH > 0 ? (ob.ACHV_QTY_GWH + ob.ACHV_QTY_OTWH) / ob.RWH : 0; ;

                    ob.WIP = ob.items.Sum(x => x.WIP);
                    ob.H1_HR = ob.items.Sum(x => x.H1_HR);
                    ob.H2_HR = ob.items.Sum(x => x.H2_HR);
                    ob.H3_HR = ob.items.Sum(x => x.H3_HR);
                    ob.H4_HR = ob.items.Sum(x => x.H4_HR);
                    ob.H5_HR = ob.items.Sum(x => x.H5_HR);
                    ob.H6_HR = ob.items.Sum(x => x.H6_HR);
                    ob.H7_HR = ob.items.Sum(x => x.H7_HR);
                    ob.H8_HR = ob.items.Sum(x => x.H8_HR);

                    if (ob.items.Count == 1)
                    {
                        ob.items[0].TARGET_WH = ob.items[0].GMT_IE_TARGET_ID < 1 ? 11 : ob.items[0].TARGET_WH;
                        ob.items[0].R_BEGIN_HR = ob.items[0].GMT_IE_TARGET_ID < 1 ? 1 : ob.items[0].R_BEGIN_HR;
                        ob.items[0].R_END_HR = ob.items[0].GMT_IE_TARGET_ID < 1 ? 11 : ob.items[0].R_END_HR;
                    }
                    else
                    {
                        foreach (var itm in  ob.items)
                        {
                            itm.TARGET_WH = itm.GMT_IE_TARGET_ID < 1 ? 0 : itm.TARGET_WH;
                            itm.R_BEGIN_HR = itm.GMT_IE_TARGET_ID < 1 ? 0 : itm.R_BEGIN_HR;
                            itm.R_END_HR = itm.GMT_IE_TARGET_ID < 1 ? 0 : itm.R_END_HR;
                        }

                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<GMT_IE_TARGETModel> QueryPmSewPlanD(Int64 pGMT_PROD_PLN_CLNDR_ID, Int32 pHR_PROD_LINE_ID, Int64 pGMT_PROD_PLN_CLNDR_ID_C)
        {
            string sp = "PKG_GMT_IE.gmt_ie_target_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_IE_TARGETModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID_C", Value =pGMT_PROD_PLN_CLNDR_ID_C},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =  pHR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_IE_TARGETModel ob = new GMT_IE_TARGETModel();

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.GMT_IE_TARGET_ID = (dr["GMT_IE_TARGET_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_TARGET_ID"]);
                    ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = pGMT_PROD_PLN_CLNDR_ID;
                    ob.HR_PROD_LINE_ID = pHR_PROD_LINE_ID;
                    ob.TARGET_EFF = (dr["TARGET_EFF"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TARGET_EFF"]);
                    ob.DAY_NO = (dr["DAY_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DAY_NO"]);
                    ob.PLAN_OP = (dr["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_OP"]);
                    ob.PLAN_HP = (dr["PLAN_HP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_HP"]);

                    ob.USED_OP = (dr["USED_OP"] == DBNull.Value) ? ob.PLAN_OP : Convert.ToInt32(dr["USED_OP"]);
                    ob.USED_HLP = (dr["USED_HLP"] == DBNull.Value) ? ob.PLAN_HP : Convert.ToInt32(dr["USED_HLP"]);

                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.TARGET_WH = (dr["TARGET_WH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TARGET_WH"]);
                    ob.HRLY_TARGET = (dr["HRLY_TARGET"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HRLY_TARGET"]);
                    ob.DAY_TARGET = (dr["DAY_TARGET"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DAY_TARGET"]);

                    ob.R_BEGIN_HR = (dr["R_BEGIN_HR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["R_BEGIN_HR"]);
                    ob.R_END_HR = (dr["R_END_HR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["R_END_HR"]);

                    ob.GWH = (dr["GWH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GWH"]);
                    ob.TARGET_QTY_GWH = (dr["TARGET_QTY_GWH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TARGET_QTY_GWH"]);
                    ob.IS_TRGT_LOCK = (dr["IS_TRGT_LOCK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRGT_LOCK"]);
                    ob.OTWH = (dr["OTWH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["OTWH"]);
                    ob.TARGET_QTY_OTWH = (dr["TARGET_QTY_OTWH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TARGET_QTY_OTWH"]);
                    ob.REASON_OF_EOT = (dr["REASON_OF_EOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_OF_EOT"]);


                    ob.WIP = (dr["WIP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["WIP"]);

                    if (dr["H1_HR"] != DBNull.Value)
                    {
                        ob.H1_HR = Convert.ToInt32(dr["H1_HR"]);
                    }
                    if (dr["H2_HR"] != DBNull.Value)
                    {
                        ob.H2_HR = Convert.ToInt32(dr["H2_HR"]);
                    }
                    if (dr["H3_HR"] != DBNull.Value)
                    {
                        ob.H3_HR = Convert.ToInt32(dr["H3_HR"]);
                    }
                    if (dr["H4_HR"] != DBNull.Value)
                    {
                        ob.H4_HR = Convert.ToInt32(dr["H4_HR"]);
                    }
                    if (dr["H5_HR"] != DBNull.Value)
                    {
                        ob.H5_HR = Convert.ToInt32(dr["H5_HR"]);
                    }
                    if (dr["H6_HR"] != DBNull.Value)
                    {
                        ob.H6_HR = Convert.ToInt32(dr["H6_HR"]);
                    }
                    if (dr["H7_HR"] != DBNull.Value)
                    {
                        ob.H7_HR = Convert.ToInt32(dr["H7_HR"]);
                    }
                    if (dr["H8_HR"] != DBNull.Value)
                    {
                        ob.H8_HR = Convert.ToInt32(dr["H8_HR"]);
                    }
                    if (dr["H9_HR"] != DBNull.Value)
                    {
                        ob.H9_HR = Convert.ToInt32(dr["H9_HR"]);
                    }
                    if (dr["H10_HR"] != DBNull.Value)
                    {
                        ob.H10_HR = Convert.ToInt32(dr["H10_HR"]);
                    }
                    if (dr["H11_HR"] != DBNull.Value)
                    {
                        ob.H11_HR = Convert.ToInt32(dr["H11_HR"]);
                    }
                    if (dr["H12_HR"] != DBNull.Value)
                    {
                        ob.H12_HR = Convert.ToInt32(dr["H12_HR"]);
                    }
                    if (dr["H13_HR"] != DBNull.Value)
                    {
                        ob.H13_HR = Convert.ToInt32(dr["H13_HR"]);
                    }

                    if (dr["H14_HR"] != DBNull.Value)
                    {
                        ob.H14_HR = Convert.ToInt32(dr["H14_HR"]);
                    }
                    if (dr["H15_HR"] != DBNull.Value)
                    {
                        ob.H15_HR = Convert.ToInt32(dr["H15_HR"]);
                    }
                    if (dr["H16_HR"] != DBNull.Value)
                    {
                        ob.H16_HR = Convert.ToInt32(dr["H16_HR"]);
                    }
                    if (dr["H17_HR"] != DBNull.Value)
                    {
                        ob.H17_HR = Convert.ToInt32(dr["H17_HR"]);
                    }
                    if (dr["H18_HR"] != DBNull.Value)
                    {
                        ob.H18_HR = Convert.ToInt32(dr["H18_HR"]);
                    }
                    if (dr["H19_HR"] != DBNull.Value)
                    {
                        ob.H19_HR = Convert.ToInt32(dr["H19_HR"]);
                    }
                    if (dr["H20_HR"] != DBNull.Value)
                    {
                        ob.H20_HR = Convert.ToInt32(dr["H20_HR"]);
                    }
                    if (dr["H21_HR"] != DBNull.Value)
                    {
                        ob.H21_HR = Convert.ToInt32(dr["H21_HR"]);
                    }



                    ob.ACHV_QTY_GWH = (ob.H1_HR ?? 0) + (ob.H2_HR ?? 0) + (ob.H3_HR ?? 0) + (ob.H4_HR ?? 0) + (ob.H5_HR ?? 0) + (ob.H6_HR ?? 0) + (ob.H7_HR ?? 0) + (ob.H8_HR ?? 0);
                    ob.ACHV_QTY_OTWH = (ob.H9_HR ?? 0) + (ob.H10_HR ?? 0) + (ob.H11_HR ?? 0) + (ob.H12_HR ?? 0) + (ob.H13_HR ?? 0) + (ob.H14_HR ?? 0) + (ob.H15_HR ?? 0) + (ob.H16_HR ?? 0) +
                        (ob.H17_HR ?? 0) + (ob.H18_HR ?? 0) + (ob.H19_HR ?? 0) + (ob.H20_HR ?? 0) + (ob.H21_HR ?? 0);
                    ob.PRODUCED_MIN = Convert.ToInt64((ob.ACHV_QTY_GWH + ob.ACHV_QTY_OTWH) * ob.SMV);
                    ob.IS_IN_PLAN = (dr["IS_IN_PLAN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_IN_PLAN"]);
                    ob.prods = new List<GMT_SEW_PRODModel>();
                    ob.prods = this.QueryPmSewPlanProd(ob.GMT_IE_TARGET_ID, ob.HRLY_TARGET);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<GMT_IE_TARGETModel> QueryPmSewPlanDBoard(Int64 pGMT_PROD_PLN_CLNDR_ID, Int32 pHR_PROD_FLR_ID, Int64 pGMT_PLN_CLNDR_MN_ID)
        {
            string sp = "PKG_GMT_IE.gmt_ie_target_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_IE_TARGETModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =  pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_IE_TARGETModel ob = new GMT_IE_TARGETModel();

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);


                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);

                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_LINE_ID"]);

                    ob.CUR_OPERATOR = (dr["CUR_OPERATOR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CUR_OPERATOR"]);
                    ob.CUR_HELPER = (dr["CUR_HELPER"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CUR_HELPER"]);

                    

                    ob.HR_PROD_LINE_ID_SPAN = (dr["HR_PROD_LINE_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_LINE_ID_SPAN"]);
                    ob.HR_PROD_LINE_ID_SL = (dr["HR_PROD_LINE_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_PROD_LINE_ID_SL"]);

                    ob.RF_PFLT_RSN_TYPE_ID = (dr["RF_PFLT_RSN_TYPE_ID"] == DBNull.Value) ? -1 : Convert.ToInt32(dr["RF_PFLT_RSN_TYPE_ID"]);
                    ob.RSN_TYPE_NAME_EN = (dr["RSN_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RSN_TYPE_NAME_EN"]);


                    ob.line = new GMT_IE_TARGET_LNModel();
                    if (ob.HR_PROD_LINE_ID_SL == 1)
                    {
                        ob.line = this.QueryPmSewPlanLinesBoard(pGMT_PROD_PLN_CLNDR_ID, ob.HR_PROD_LINE_ID, pGMT_PLN_CLNDR_MN_ID);
                    }

                    ob.STYLE_NO_S = ob.STYLE_NO.Count() > 8 ? "[.] " + ob.STYLE_NO.Substring(ob.STYLE_NO.Count() - 8) : ob.STYLE_NO;
                    ob.ORDER_NO_S = ob.ORDER_NO.Count() > 8 ? "[.] " + ob.ORDER_NO.Substring(ob.ORDER_NO.Count() - 8) : ob.ORDER_NO;

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.GMT_IE_TARGET_ID = (dr["GMT_IE_TARGET_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_TARGET_ID"]);
                    ob.GMT_PLN_LINE_LOAD_ID = (dr["GMT_PLN_LINE_LOAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_LINE_LOAD_ID"]);

                    ob.TARGET_EFF = (dr["TARGET_EFF"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TARGET_EFF"]);
                    ob.DAY_NO = (dr["DAY_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DAY_NO"]);
                    ob.PLAN_OP = (dr["PLAN_OP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_OP"]);
                    ob.PLAN_HP = (dr["PLAN_HP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PLAN_HP"]);

                    ob.USED_OP = (dr["USED_OP"] == DBNull.Value) ? ob.PLAN_OP : Convert.ToInt32(dr["USED_OP"]);
                    ob.USED_HLP = (dr["USED_HLP"] == DBNull.Value) ? ob.PLAN_HP : Convert.ToInt32(dr["USED_HLP"]);

                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.TARGET_WH = (dr["TARGET_WH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TARGET_WH"]);
                    ob.HRLY_TARGET = (dr["HRLY_TARGET"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HRLY_TARGET"]);
                    ob.DAY_TARGET = (dr["DAY_TARGET"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DAY_TARGET"]);

                    ob.R_BEGIN_HR = (dr["R_BEGIN_HR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["R_BEGIN_HR"]);
                    ob.R_END_HR = (dr["R_END_HR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["R_END_HR"]);

                    ob.GWH = (dr["GWH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GWH"]);
                    ob.TARGET_QTY_GWH = (dr["TARGET_QTY_GWH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TARGET_QTY_GWH"]);
                    ob.IS_TRGT_LOCK = (dr["IS_TRGT_LOCK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRGT_LOCK"]);
                    ob.OTWH = (dr["OTWH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["OTWH"]);
                    ob.TARGET_QTY_OTWH = (dr["TARGET_QTY_OTWH"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TARGET_QTY_OTWH"]);
                    ob.REASON_OF_EOT = (dr["REASON_OF_EOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_OF_EOT"]);

                    ob.IS_IN_PLAN = (dr["IS_IN_PLAN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_IN_PLAN"]);

                    ob.WIP = (dr["WIP"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["WIP"]);

                    if (dr["H1_HR"] != DBNull.Value)
                    {
                        ob.H1_HR =  Convert.ToInt32(dr["H1_HR"]);
                    }
                    if (dr["H2_HR"] != DBNull.Value)
                    {
                        ob.H2_HR = Convert.ToInt32(dr["H2_HR"]);
                    }
                    if (dr["H3_HR"] != DBNull.Value)
                    {
                        ob.H3_HR = Convert.ToInt32(dr["H3_HR"]);
                    }
                    if (dr["H4_HR"] != DBNull.Value)
                    {
                        ob.H4_HR = Convert.ToInt32(dr["H4_HR"]);
                    }
                    if (dr["H5_HR"] != DBNull.Value)
                    {
                        ob.H5_HR = Convert.ToInt32(dr["H5_HR"]);
                    }
                    if (dr["H6_HR"] != DBNull.Value)
                    {
                        ob.H6_HR = Convert.ToInt32(dr["H6_HR"]);
                    }
                    if (dr["H7_HR"] != DBNull.Value)
                    {
                        ob.H7_HR = Convert.ToInt32(dr["H7_HR"]);
                    }
                    if (dr["H8_HR"] != DBNull.Value)
                    {
                        ob.H8_HR = Convert.ToInt32(dr["H8_HR"]);
                    }
                    if (dr["H9_HR"] != DBNull.Value)
                    {
                        ob.H9_HR = Convert.ToInt32(dr["H9_HR"]);
                    }
                    if (dr["H10_HR"] != DBNull.Value)
                    {
                        ob.H10_HR = Convert.ToInt32(dr["H10_HR"]);
                    }
                    if (dr["H11_HR"] != DBNull.Value)
                    {
                        ob.H11_HR = Convert.ToInt32(dr["H11_HR"]);
                    }
                    if (dr["H12_HR"] != DBNull.Value)
                    {
                        ob.H12_HR = Convert.ToInt32(dr["H12_HR"]);
                    }
                    if (dr["H13_HR"] != DBNull.Value)
                    {
                        ob.H13_HR = Convert.ToInt32(dr["H13_HR"]);
                    }

                    if (dr["H14_HR"] != DBNull.Value)
                    {
                        ob.H14_HR = Convert.ToInt32(dr["H14_HR"]);
                    }
                    if (dr["H15_HR"] != DBNull.Value)
                    {
                        ob.H15_HR = Convert.ToInt32(dr["H15_HR"]);
                    }
                    if (dr["H16_HR"] != DBNull.Value)
                    {
                        ob.H16_HR = Convert.ToInt32(dr["H16_HR"]);
                    }
                    if (dr["H17_HR"] != DBNull.Value)
                    {
                        ob.H17_HR = Convert.ToInt32(dr["H17_HR"]);
                    }
                    if (dr["H18_HR"] != DBNull.Value)
                    {
                        ob.H18_HR = Convert.ToInt32(dr["H18_HR"]);
                    }
                    if (dr["H19_HR"] != DBNull.Value)
                    {
                        ob.H19_HR = Convert.ToInt32(dr["H19_HR"]);
                    }
                    if (dr["H20_HR"] != DBNull.Value)
                    {
                        ob.H20_HR = Convert.ToInt32(dr["H20_HR"]);
                    }
                    if (dr["H21_HR"] != DBNull.Value)
                    {
                        ob.H21_HR = Convert.ToInt32(dr["H21_HR"]);
                    }



                    ob.ACHV_QTY_GWH = (ob.H1_HR ?? 0) + (ob.H2_HR ?? 0) + (ob.H3_HR ?? 0) + (ob.H4_HR ?? 0) + (ob.H5_HR ?? 0) + (ob.H6_HR ?? 0) + (ob.H7_HR ?? 0) + (ob.H8_HR ?? 0);
                    ob.ACHV_QTY_OTWH = (ob.H9_HR ?? 0) + (ob.H10_HR ?? 0) + (ob.H11_HR ?? 0) + (ob.H12_HR ?? 0) + (ob.H13_HR ?? 0) + (ob.H14_HR ?? 0) + (ob.H15_HR ?? 0) + (ob.H16_HR ?? 0) +
                        (ob.H17_HR ?? 0) + (ob.H18_HR ?? 0) + (ob.H19_HR ?? 0) + (ob.H20_HR ?? 0) + (ob.H21_HR ?? 0);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);

                    ob.TTL_VALUE = (decimal) (ob.ACHV_QTY_GWH + ob.ACHV_QTY_OTWH) * ob.UNIT_PRICE;

                    ob.prods = new List<GMT_SEW_PRODModel>();
                    ob.prods = this.QueryPmSewPlanProd(ob.GMT_IE_TARGET_ID, ob.HRLY_TARGET);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private GMT_IE_TARGET_LNModel QueryPmSewPlanLinesBoard(Int64 pGMT_PROD_PLN_CLNDR_ID, Int64 pHR_PROD_LINE_ID, Int64 pGMT_PLN_CLNDR_MN_ID)
        {
            string sp = "PKG_GMT_IE.gmt_ie_target_select";
            //pOption=3000=>Select All Data
            try
            {
                OraDatabase db = new OraDatabase();
                GMT_IE_TARGET_LNModel ob = new GMT_IE_TARGET_LNModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =pGMT_PROD_PLN_CLNDR_ID},
                      new CommandParameter() {ParameterName = "pGMT_PLN_CLNDR_MN_ID", Value =pGMT_PLN_CLNDR_MN_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value =  pHR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   
                    ob.TTL_ACTV_POINT = (dr["TTL_ACTV_POINT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TTL_ACTV_POINT"]);
                    ob.NPT = (dr["NPT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NPT"]);
                    ob.RWH = (dr["R_WH"] == DBNull.Value) ? 1 : Convert.ToInt32(dr["R_WH"]);
                    ob.USED_MP = (dr["USED_MP"] == DBNull.Value) ? 1 : Convert.ToInt64(dr["USED_MP"]);

                    ob.OTWH = (ob.RWH < 9) ? 0 : ob.RWH-8;

                    ob.ACHV_QTY = (dr["ACHV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ACHV_QTY"]);
                    ob.HRLY_TARGET = (dr["HRLY_TARGET"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HRLY_TARGET"]);

                    ob.TTL_TARFET = (dr["TTL_TARFET"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TTL_TARFET"]);
                    ob.TTL_ACHIEVE = (dr["TTL_ACHIEVE"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TTL_ACHIEVE"]);

                    ob.DAILY_TARGET = (int) (ob.HRLY_TARGET * ob.RWH);

                    ob.H9_MP_ADD = ((dr["H9_MP_ADD"] == DBNull.Value) || ob.RWH <= 8) ? 0 : Convert.ToInt64(dr["H9_MP_ADD"]) * 60;
                    ob.H10_MP_ADD = ((dr["H10_MP_ADD"] == DBNull.Value) || ob.RWH <= 9) ? 0 : Convert.ToInt64(dr["H10_MP_ADD"]) * 60;
                    ob.H11_MP_ADD = ((dr["H11_MP_ADD"] == DBNull.Value) || ob.RWH <= 10) ? 0 : Convert.ToInt64(dr["H11_MP_ADD"]) * 60;
                    ob.H12_MP_ADD = ((dr["H12_MP_ADD"] == DBNull.Value) || ob.RWH <= 11) ? 0 : Convert.ToInt64(dr["H12_MP_ADD"]) * 60;
                    ob.H13_MP_ADD = ((dr["H13_MP_ADD"] == DBNull.Value) || ob.RWH <= 12) ? 0 : Convert.ToInt64(dr["H13_MP_ADD"]) * 60;
                    ob.H14_MP_ADD = ((dr["H14_MP_ADD"] == DBNull.Value) || ob.RWH <= 13) ? 0 : Convert.ToInt64(dr["H14_MP_ADD"]) * 60;
                    ob.H15_MP_ADD = ((dr["H15_MP_ADD"] == DBNull.Value) || ob.RWH <= 14) ? 0 : Convert.ToInt64(dr["H15_MP_ADD"]) * 60;
                    ob.H15_P_MP_ADD = ((dr["H15_P_MP_ADD"] == DBNull.Value) || ob.RWH <= 15) ? 0 : Convert.ToInt64(dr["H15_P_MP_ADD"]) * (ob.RWH - 15) * 60;
                    ob.INPUT_MIN = (((ob.RWH < 8) ? ob.RWH : 8) * ob.USED_MP * 60) + ob.H9_MP_ADD + ob.H10_MP_ADD + ob.H11_MP_ADD + ob.H12_MP_ADD + ob.H13_MP_ADD + ob.H14_MP_ADD + ob.H15_MP_ADD + ob.H15_P_MP_ADD;
                    ob.PRODUCED_MIN = (dr["PRODUCED_MIN"] == DBNull.Value) ? 1 : Convert.ToInt64(dr["PRODUCED_MIN"]);
                    ob.EFFICIENCY = (decimal) ob.INPUT_MIN > 0 ? (ob.PRODUCED_MIN / ob.INPUT_MIN) * 100 : 0;
                    ob.PERFORMANCE = (decimal) ob.INPUT_MIN > 0 ? (ob.PRODUCED_MIN / (ob.INPUT_MIN - ob.NPT)) * 100 : 0;
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<GMT_SEW_PRODModel> QueryPmSewPlanProd(Int64 pGMT_IE_TARGET_ID, Int32 pHRLY_TARGET)
        {
            string sp = "PKG_GMT_IE.gmt_ie_target_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_SEW_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_IE_TARGET_ID", Value = pGMT_IE_TARGET_ID},
                     new CommandParameter() {ParameterName = "pHRLY_TARGET", Value = pHRLY_TARGET},
                     new CommandParameter() {ParameterName = "pOption", Value =  3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SEW_PRODModel ob = new GMT_SEW_PRODModel();
                    ob.GMT_SEW_PROD_ID = (dr["GMT_SEW_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_SEW_PROD_ID"]);
                    ob.GMT_IE_TARGET_ID = (dr["GMT_IE_TARGET_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_TARGET_ID"]);
                    ob.HOUR_NO = (dr["HOUR_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOUR_NO"]);
                    ob.PROD_QTY = (dr["PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_QTY"]);
                    ob.NPT = (dr["NPT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NPT"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.GMT_IE_NPT_REASON_ID = (dr["GMT_IE_NPT_REASON_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_IE_NPT_REASON_ID"]);

                    ob.COLR_CODE = (dr["COLR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLR_CODE"]);
                    ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);
                    ob.REASON_CODE = (dr["REASON_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_CODE"]);
                    ob.IS_LOCKED = (dr["IS_LOCKED"] == DBNull.Value) ? true : Convert.ToString(dr["IS_LOCKED"]) == "Y";
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_SEW_PRODModel> SewPlanProd4OutOfPlan(Int64 pGMT_PLN_LINE_LOAD_ID, Int32 pGMT_PROD_PLN_CLNDR_ID, Int32 pHR_PROD_LINE_ID)
        {
            string sp = "PKG_GMT_IE.gmt_ie_target_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_SEW_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PLN_LINE_LOAD_ID", Value = pGMT_PLN_LINE_LOAD_ID },
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID },
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = pHR_PROD_LINE_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =  3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_SEW_PRODModel ob = new GMT_SEW_PRODModel();
                    ob.HOUR_NO = (dr["HOUR_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOUR_NO"]);
                    ob.PROD_QTY = (dr["PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_QTY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int PLAN_OP { get; set; }
        public int PLAN_HP { get; set; }
        public decimal SMV { get; set; }
        public Int64 INPUT_MIN { get; set; }
        public int WIP { get; set; }
        public long PRODUCED_MIN { get; set; }
        public List<GMT_SEW_PRODModel> prods { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string STYLE_NO_S { get; set; }
        public string ORDER_NO_S { get; set; }
        public int HR_PROD_LINE_ID_SPAN { get; set; }
        public int HR_PROD_LINE_ID_SL { get; set; }

        public string LINE_CODE { get; set; }

        public int CUR_OPERATOR { get; set; }

        public int CUR_HELPER { get; set; }

        public decimal UNIT_PRICE { get; set; }

        public decimal TTL_VALUE { get; set; }

        public int RF_PFLT_RSN_TYPE_ID { get; set; }

        public string RSN_TYPE_NAME_EN { get; set; }

        public string IS_IN_PLAN { get; set; }
    }
}