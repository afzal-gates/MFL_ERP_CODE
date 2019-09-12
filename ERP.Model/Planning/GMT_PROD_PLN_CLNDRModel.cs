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
    public class GMT_PROD_PLN_CLNDRModel
    {
        public Int64? GMT_PROD_PLN_CLNDR_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64? RF_FISCAL_YEAR_ID { get; set; }
        public DateTime? YR_START_DATE { get; set; }
        public DateTime? YR_END_DATE { get; set; }
        public Int64? RF_CAL_MONTH_ID { get; set; }
        public DateTime? MN_START_DATE { get; set; }
        public DateTime? MN_END_DATE { get; set; }
        public Int64? RF_CALENDAR_WK_ID { get; set; }
        public DateTime? WK_START_DATE { get; set; }
        public DateTime? WK_END_DATE { get; set; }
        public Int64? DAY_NO { get; set; }
        public DateTime? CALENDAR_DT { get; set; }
        public string FY_NAME_EN { get; set; }
        public string MONTH_NAME_EN { get; set; }
        public Int64? YR_WK_NO { get; set; }
        public Int64? MN_WK_NO { get; set; }
        public Boolean IS_TAB_ACT { get; set; }
        public string CAL_WK_CODE { get; set; }

        public string CALENDAR_DT_TXT { get; set; }
        public string ProdClndrProcess()
        {
            const string sp = "pkg_planning_common.prod_pln_clndr_clndr_process";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pYR_START_DATE", Value = ob.YR_START_DATE},
                     new CommandParameter() {ParameterName = "pYR_END_DATE", Value = ob.YR_END_DATE},
                     
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

        public object GetClndrYearList(string pFY_NAME_EN = null)
        {
            string sp = "pkg_planning_common.gmt_prod_pln_clndr_select";
            try
            {
                var obList = new List<GMT_PROD_PLN_CLNDRModel>();
                //Int64 vTotalRec = 0;                
                //var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pFY_NAME_EN", Value = pFY_NAME_EN},

                     //new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     //new CommandParameter() {ParameterName = "pageSize", Value = pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PROD_PLN_CLNDRModel ob = new GMT_PROD_PLN_CLNDRModel();
                    
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    
                    //if (vTotalRec < 1)
                    //{
                    //    vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    //}

                    obList.Add(ob);

                }
                //obj.total = vTotalRec;
                //obj.data = obList;
                //return obj;
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetClndrMonthList(Int64 pRF_FISCAL_YEAR_ID, string pMONTH_NAME_EN = null)
        {
            string sp = "pkg_planning_common.gmt_prod_pln_clndr_select";
            try
            {
                var obList = new List<GMT_PROD_PLN_CLNDRModel>();
                //Int64 vTotalRec = 0;                
                //var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = pRF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pMONTH_NAME_EN", Value = pMONTH_NAME_EN},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PROD_PLN_CLNDRModel ob = new GMT_PROD_PLN_CLNDRModel();

                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);

                    ob.IS_TAB_ACT = (dr["IS_ACTIVE"] == DBNull.Value) ? false : Convert.ToString(dr["IS_ACTIVE"])=="Y";
                    //if (vTotalRec < 1)
                    //{
                    //    vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    //}

                    obList.Add(ob);

                }
                //obj.total = vTotalRec;
                //obj.data = obList;
                //return obj;
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetCapctiMonList4Copy(
            Int64 pRF_FISCAL_YEAR_ID, Int64 pHR_COMPANY_ID, Int64 pPROD_UNIT_ID, 
            Int64? pGMT_PRODUCT_TYP_ID, Int32? pGMT_PROD_PLN_CLNDR_ID = null, Int64? pCORE_DEPT_ID = null,
            Int64? pLK_CALC_MTHD_ID =  null
        )
        {
            string sp = "pkg_planning_common.gmt_prod_pln_clndr_select";
            try
            {
                var obList = new List<GMT_PROD_PLN_CLNDRModel>();
                //Int64 vTotalRec = 0;                
                //var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = pRF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = pPROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = pGMT_PRODUCT_TYP_ID},

                     new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},
                     new CommandParameter() {ParameterName = "pLK_CALC_MTHD_ID", Value = pLK_CALC_MTHD_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PROD_PLN_CLNDRModel ob = new GMT_PROD_PLN_CLNDRModel();

                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);

                    //if (vTotalRec < 1)
                    //{
                    //    vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    //}

                    obList.Add(ob);

                }
                //obj.total = vTotalRec;
                //obj.data = obList;
                //return obj;
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetClndrWkList(Int64? pRF_FISCAL_YEAR_ID = null, Int64? pRF_CAL_MONTH_ID = null, Int64? pPARENT_ID = null)
        {
            string sp = "pkg_planning_common.gmt_prod_pln_clndr_select";
            try
            {
                var obList = new List<GMT_PROD_PLN_CLNDRModel>();
                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = pRF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = pRF_CAL_MONTH_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = pPARENT_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PROD_PLN_CLNDRModel ob = new GMT_PROD_PLN_CLNDRModel();

                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.YR_START_DATE = (dr["YR_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["YR_START_DATE"]);
                    ob.YR_END_DATE = (dr["YR_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["YR_END_DATE"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.MN_START_DATE = (dr["MN_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MN_START_DATE"]);
                    ob.MN_END_DATE = (dr["MN_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MN_END_DATE"]);
                    ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);
                    ob.WK_START_DATE = (dr["WK_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["WK_START_DATE"]);
                    ob.WK_END_DATE = (dr["WK_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["WK_END_DATE"]);
                    ob.DAY_NO = (dr["DAY_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY_NO"]);
                    ob.CALENDAR_DT = (dr["CALENDAR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CALENDAR_DT"]);

                    ob.YR_WK_NO = (dr["YR_WK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YR_WK_NO"]);
                    ob.MN_WK_NO = (dr["MN_WK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MN_WK_NO"]);
                    ob.CAL_WK_CODE = (dr["CAL_WK_CODE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["CAL_WK_CODE"]);

                    obList.Add(ob);
                }                
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public GMT_PROD_PLN_CLNDRModel Select(long ID)
        {
            string sp = "Select_GMT_PROD_PLN_CLNDR";
            try
            {
                var ob = new GMT_PROD_PLN_CLNDRModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.YR_START_DATE = (dr["YR_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["YR_START_DATE"]);
                    ob.YR_END_DATE = (dr["YR_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["YR_END_DATE"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.MN_START_DATE = (dr["MN_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MN_START_DATE"]);
                    ob.MN_END_DATE = (dr["MN_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MN_END_DATE"]);
                    ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);
                    ob.WK_START_DATE = (dr["WK_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["WK_START_DATE"]);
                    ob.WK_END_DATE = (dr["WK_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["WK_END_DATE"]);
                    ob.DAY_NO = (dr["DAY_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY_NO"]);
                    ob.CALENDAR_DT = (dr["CALENDAR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CALENDAR_DT"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_PROD_PLN_CLNDRModel> getCurrentProdCalendarList()
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            try
            {
                var obList = new List<GMT_PROD_PLN_CLNDRModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3013},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PROD_PLN_CLNDRModel ob = new GMT_PROD_PLN_CLNDRModel();

                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.CALENDAR_DT_TXT = (dr["CALENDAR_DT"] == DBNull.Value) ? String.Empty : Convert.ToDateTime(dr["CALENDAR_DT"]).ToString("dd-MMM-yyyy");
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