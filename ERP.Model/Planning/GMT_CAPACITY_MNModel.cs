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
    public class GMT_CAPACITY_WKModel
    {
        public Int64 GMT_CAPACITY_WK_ID { get; set; }
        public Int64 PLAN_PROD_QTY { get; set; }
        public Int64 PLAN_PROD_PCS { get; set; }
        public Int64 TRNSFR_PROD_PCS { get; set; }
        public Int64 ALLOW_SC_CAPACITY { get; set; }
        public Int64 ADDL_PLAN_QTY { get; set; }


        public int YR_WK_NO { get; set; }

        public DateTime WK_END_DATE { get; set; }

        public DateTime WK_START_DATE { get; set; }
    }
    public class GMT_CAPACITY_MNModel
    {
        public Int64 GMT_CAPACITY_MN_ID { get; set; }
        public Int64 GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64? HR_COMPANY_ID { get; set; }
        public Int64 PROD_UNIT_ID { get; set; }
        public Int64? GMT_PRODUCT_TYP_ID { get; set; }
        public Int64 CORE_DEPT_ID { get; set; }
        public Int64 LK_CALC_MTHD_ID { get; set; }
        public Int64? NO_LINE_OR_WS { get; set; }
        public Int64? WORK_DAYS_IN_MN { get; set; }
        public Int64? PLAN_MP { get; set; }
        public Int64 AVG_WORK_HR { get; set; }
        public Decimal? AVG_SMV { get; set; }
        public Decimal? PCT_ABSENCE { get; set; }
        public Decimal? FACT_EFICNCY { get; set; }
        public Decimal? PCT_BACKLOG { get; set; }
        public Int64 PLAN_PROD_QTY { get; set; }
        public Int64? BOOKED_PROD_QTY { get; set; }
        public Int64? TRNSFR_PROD_QTY { get; set; }
        public Int64? PLAN_PROD_PCS { get; set; }
        public Int64? BOOKED_PROD_PCS { get; set; }
        public Int64? TRNSFR_PROD_PCS { get; set; }
        public Int64? ALLOW_SC_CAPACITY { get; set; }
        public Int64? RF_FISCAL_YEAR_ID { get; set; }
        public string GMT_PROD_PLN_CLNDR_ID_LST { get; set; }

        public decimal? AVG_CM { get; set; }
        public decimal? AVG_FOB { get; set; }
        public Int64? TTL_TRG_CM { get; set; }
        public Int64? TTL_TRG_FOB { get; set; }
        public List<GMT_CAP_TRNFERModel> items { get; set; }

        public List<GMT_CAPACITY_WKModel> wk_caps { get; set; }

        public string XML { get; set; }
        



        public string Save()
        {
            const string sp = "pkg_planning_common.gmt_capacity_mn_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CAPACITY_MN_ID", Value = ob.GMT_CAPACITY_MN_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = ob.GMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                     new CommandParameter() {ParameterName = "pLK_CALC_MTHD_ID", Value = ob.LK_CALC_MTHD_ID},
                     new CommandParameter() {ParameterName = "pNO_LINE_OR_WS", Value = ob.NO_LINE_OR_WS},
                     new CommandParameter() {ParameterName = "pWORK_DAYS_IN_MN", Value = ob.WORK_DAYS_IN_MN},
                     new CommandParameter() {ParameterName = "pPLAN_MP", Value = ob.PLAN_MP},
                     new CommandParameter() {ParameterName = "pAVG_WORK_HR", Value = ob.AVG_WORK_HR},
                     new CommandParameter() {ParameterName = "pAVG_SMV", Value = ob.AVG_SMV},
                     new CommandParameter() {ParameterName = "pPCT_ABSENCE", Value = ob.PCT_ABSENCE},
                     new CommandParameter() {ParameterName = "pFACT_EFICNCY", Value = ob.FACT_EFICNCY},
                     new CommandParameter() {ParameterName = "pPCT_BACKLOG", Value = ob.PCT_BACKLOG},
                     new CommandParameter() {ParameterName = "pPLAN_PROD_QTY", Value = ob.PLAN_PROD_QTY},
                     new CommandParameter() {ParameterName = "pBOOKED_PROD_QTY", Value = ob.BOOKED_PROD_QTY},
                     new CommandParameter() {ParameterName = "pTRNSFR_PROD_QTY", Value = ob.TRNSFR_PROD_QTY},
                     new CommandParameter() {ParameterName = "pPLAN_PROD_PCS", Value = ob.PLAN_PROD_PCS},
                     new CommandParameter() {ParameterName = "pBOOKED_PROD_PCS", Value = ob.BOOKED_PROD_PCS},
                     new CommandParameter() {ParameterName = "pTRNSFR_PROD_PCS", Value = ob.TRNSFR_PROD_PCS},
                     new CommandParameter() {ParameterName = "pALLOW_SC_CAPACITY", Value = ob.ALLOW_SC_CAPACITY},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID_LST", Value = ob.GMT_PROD_PLN_CLNDR_ID_LST},

                     new CommandParameter() {ParameterName = "pAVG_CM", Value = ob.AVG_CM},
                     new CommandParameter() {ParameterName = "pTTL_TRG_CM", Value = ob.TTL_TRG_CM},
                     new CommandParameter() {ParameterName = "pAVG_FOB", Value = ob.AVG_FOB},
                     new CommandParameter() {ParameterName = "pTTL_TRG_FOB", Value = ob.TTL_TRG_FOB},

                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},

                      new CommandParameter() {ParameterName = "pXML_WK_CAP", Value = ob.XML_WK_CAP},


                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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


        public List<GMT_CAPACITY_MNModel> SelectAll()
        {
            string sp = "Select_GMT_CAPACITY_MN";
            try
            {
                var obList = new List<GMT_CAPACITY_MNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CAPACITY_MN_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CAPACITY_MNModel ob = new GMT_CAPACITY_MNModel();
                    ob.GMT_CAPACITY_MN_ID = (dr["GMT_CAPACITY_MN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CAPACITY_MN_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);

                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);

                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);
                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.LK_CALC_MTHD_ID = (dr["LK_CALC_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CALC_MTHD_ID"]);
                    ob.NO_LINE_OR_WS = (dr["NO_LINE_OR_WS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_LINE_OR_WS"]);
                    ob.WORK_DAYS_IN_MN = (dr["WORK_DAYS_IN_MN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WORK_DAYS_IN_MN"]);
                    ob.PLAN_MP = (dr["PLAN_MP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_MP"]);
                    ob.AVG_WORK_HR = (dr["AVG_WORK_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AVG_WORK_HR"]);
                    ob.AVG_SMV = (dr["AVG_SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_SMV"]);
                    ob.PCT_ABSENCE = (dr["PCT_ABSENCE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ABSENCE"]);
                    ob.FACT_EFICNCY = (dr["FACT_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FACT_EFICNCY"]);
                    ob.PLAN_PROD_QTY = (dr["PLAN_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_PROD_QTY"]);
                    ob.BOOKED_PROD_QTY = (dr["BOOKED_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BOOKED_PROD_QTY"]);
                    ob.TRNSFR_PROD_QTY = (dr["TRNSFR_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRNSFR_PROD_QTY"]);
                    ob.PLAN_PROD_PCS = (dr["PLAN_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_PROD_PCS"]);
                    ob.BOOKED_PROD_PCS = (dr["BOOKED_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BOOKED_PROD_PCS"]);
                    ob.TRNSFR_PROD_PCS = (dr["TRNSFR_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRNSFR_PROD_PCS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CAPACITY_MNModel GetCapctiMonById(long pGMT_CAPACITY_MN_ID)
        {
            string sp = "pkg_planning_common.gmt_capacity_mn_select";
            try
            {
                var ob = new GMT_CAPACITY_MNModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CAPACITY_MN_ID", Value = pGMT_CAPACITY_MN_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CAPACITY_MN_ID = (dr["GMT_CAPACITY_MN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CAPACITY_MN_ID"]);
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    if (dr["GMT_PRODUCT_TYP_ID"] != DBNull.Value)
                        ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);

                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.LK_CALC_MTHD_ID = (dr["LK_CALC_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CALC_MTHD_ID"]);

                    if (dr["NO_LINE_OR_WS"] != DBNull.Value)
                        ob.NO_LINE_OR_WS = (dr["NO_LINE_OR_WS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_LINE_OR_WS"]);
                    if (dr["WORK_DAYS_IN_MN"] != DBNull.Value)
                        ob.WORK_DAYS_IN_MN = (dr["WORK_DAYS_IN_MN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WORK_DAYS_IN_MN"]);
                    if (dr["PLAN_MP"] != DBNull.Value)
                        ob.PLAN_MP = (dr["PLAN_MP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_MP"]);

                    ob.AVG_WORK_HR = (dr["AVG_WORK_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AVG_WORK_HR"]);

                    if (dr["AVG_SMV"] != DBNull.Value)
                        ob.AVG_SMV = (dr["AVG_SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_SMV"]);
                    if (dr["PCT_ABSENCE"] != DBNull.Value)
                        ob.PCT_ABSENCE = (dr["PCT_ABSENCE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ABSENCE"]);
                    if (dr["FACT_EFICNCY"] != DBNull.Value)
                        ob.FACT_EFICNCY = (dr["FACT_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FACT_EFICNCY"]);
                    if (dr["PCT_BACKLOG"] != DBNull.Value)
                        ob.PCT_BACKLOG = (dr["PCT_BACKLOG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_BACKLOG"]);
                    
                    ob.PLAN_PROD_QTY = (dr["PLAN_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_PROD_QTY"]);

                    if (dr["BOOKED_PROD_QTY"] != DBNull.Value)
                        ob.BOOKED_PROD_QTY = (dr["BOOKED_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BOOKED_PROD_QTY"]);
                    if (dr["TRNSFR_PROD_QTY"] != DBNull.Value)
                        ob.TRNSFR_PROD_QTY = (dr["TRNSFR_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRNSFR_PROD_QTY"]);
                    if (dr["PLAN_PROD_PCS"] != DBNull.Value)
                        ob.PLAN_PROD_PCS = (dr["PLAN_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_PROD_PCS"]);
                    if (dr["BOOKED_PROD_PCS"] != DBNull.Value)
                        ob.BOOKED_PROD_PCS = (dr["BOOKED_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BOOKED_PROD_PCS"]);
                    if (dr["TRNSFR_PROD_PCS"] != DBNull.Value)
                        ob.TRNSFR_PROD_PCS = (dr["TRNSFR_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRNSFR_PROD_PCS"]);
                    if (dr["ALLOW_SC_CAPACITY"] != DBNull.Value)
                        ob.ALLOW_SC_CAPACITY = (dr["ALLOW_SC_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOW_SC_CAPACITY"]);

                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);

                    if (dr["AVG_CM"] != DBNull.Value)
                        ob.AVG_CM = (dr["AVG_CM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_CM"]);
                    if (dr["AVG_FOB"] != DBNull.Value)
                        ob.AVG_FOB = (dr["AVG_FOB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_FOB"]);

                    if (dr["TTL_TRG_CM"] != DBNull.Value)
                        ob.TTL_TRG_CM = (dr["TTL_TRG_CM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_TRG_CM"]);
                    if (dr["TTL_TRG_FOB"] != DBNull.Value)
                        ob.TTL_TRG_FOB = (dr["TTL_TRG_FOB"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_TRG_FOB"]);

                    ob.items = new List<GMT_CAP_TRNFERModel>();
                    ob.items = new GMT_CAP_TRNFERModel().Query(ob.GMT_CAPACITY_MN_ID, ob.GMT_PRODUCT_TYP_ID);

                    ob.wk_caps = new List<GMT_CAPACITY_WKModel>();

                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pGMT_CAPACITY_MN_ID", Value = ob.GMT_CAPACITY_MN_ID},
                             new CommandParameter() {ParameterName = "pOption", Value = 3004},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                        foreach (DataRow dr1 in ds1.Tables[0].Rows)
                      {
                          GMT_CAPACITY_WKModel ob1 = new GMT_CAPACITY_WKModel();
                          ob1.GMT_CAPACITY_WK_ID = (dr1["GMT_CAPACITY_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["GMT_CAPACITY_WK_ID"]);
                          ob1.PLAN_PROD_QTY = (dr1["PLAN_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PLAN_PROD_QTY"]);
                          ob1.PLAN_PROD_PCS = (dr1["PLAN_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PLAN_PROD_PCS"]);
                          ob1.TRNSFR_PROD_PCS = (dr1["TRNSFR_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TRNSFR_PROD_PCS"]);
                          ob1.ALLOW_SC_CAPACITY = (dr1["ALLOW_SC_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ALLOW_SC_CAPACITY"]);
                          ob1.ADDL_PLAN_QTY = (dr1["ADDL_PLAN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ADDL_PLAN_QTY"]);
                          ob1.YR_WK_NO = (dr1["YR_WK_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr1["YR_WK_NO"]);
                          ob1.WK_START_DATE = (dr1["WK_START_DATE"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr1["WK_START_DATE"]);
                          ob1.WK_END_DATE = (dr1["WK_END_DATE"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr1["WK_END_DATE"]);
                          ob.wk_caps.Add(ob1);
                      }
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string XML_WK_CAP { get; set; }
    }

    public class GMT_CAPACITY_MN_ListModel
    {
        public string MONTH_NAME_EN { get; set; }
        public Int64 CUTTING_ID { get; set; }
        public Int64 PRINTING_ID { get; set; }
        public Int64 EMBROIDERY_ID { get; set; }
        public Int64 SEWING_ID { get; set; }
        public Int64 GMT_FINISHING_ID { get; set; }
        public long GMT_PROD_PLN_CLNDR_ID { get; set; }


        public object GetCapctyProdMonthList(long pPROD_UNIT_ID, long pRF_FISCAL_YEAR_ID, Int64? pGMT_PRODUCT_TYP_ID = null)
        {
            string sp = "pkg_planning_common.gmt_capacity_mn_select";
            try
            {                
                var obList = new List<GMT_CAPACITY_MN_ListModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = pPROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = pGMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = pRF_FISCAL_YEAR_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CAPACITY_MN_ListModel ob = new GMT_CAPACITY_MN_ListModel();

                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    if (dr["CUTTING_ID"] != DBNull.Value)
                        ob.CUTTING_ID = (dr["CUTTING_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTTING_ID"]);

                    if (dr["PRINTING_ID"] != DBNull.Value)
                        ob.PRINTING_ID = (dr["PRINTING_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRINTING_ID"]);

                    if (dr["EMBROIDERY_ID"] != DBNull.Value)
                        ob.EMBROIDERY_ID = (dr["EMBROIDERY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EMBROIDERY_ID"]);

                    if (dr["SEWING_ID"] != DBNull.Value)
                        ob.SEWING_ID = (dr["SEWING_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEWING_ID"]);

                    if (dr["GMT_FINISHING_ID"] != DBNull.Value)
                        ob.GMT_FINISHING_ID = (dr["GMT_FINISHING_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_FINISHING_ID"]);

                    if (dr["GMT_PROD_PLN_CLNDR_ID"] != DBNull.Value)
                        ob.GMT_PROD_PLN_CLNDR_ID =  Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);

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