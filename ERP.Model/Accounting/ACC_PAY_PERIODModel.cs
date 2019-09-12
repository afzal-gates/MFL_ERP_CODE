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
    public class ACC_PAY_PERIODModel //: IACC_PAY_PERIODModel
    {
        public Int64 ACC_PAY_PERIOD_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_PERIOD_TYPE_ID { get; set; }
        public Int64 RF_FISCAL_YEAR_ID { get; set; }
        public Int64 RF_CAL_MONTH_ID { get; set; }
        public DateTime PAY_DATE { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public string REMARKS { get; set; }
        public string IS_CLOSED { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string FY_NAME_EN { get; set; }
        public string MONTH_NAME_EN { get; set; }
        public string MONTH_YEAR_NAME { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string PERIOD_TYPE_NAME_EN { get; set; }
        public string IS_SHOW4_RPT { get; set; }



        public List<ACC_PAY_PERIODModel> AllPeriodListData(Int64 pHR_COMPANY_ID, Int64? pHR_PERIOD_TYPE_ID)
        {
            string v_period_type = "";
            try
            {
                if (pHR_PERIOD_TYPE_ID == null)
                {
                    v_period_type = "null";
                }
                else
                {
                    v_period_type = pHR_PERIOD_TYPE_ID.ToString();
                }

                var obList = new List<ACC_PAY_PERIODModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from table(pkg_myvar.get_company_all_periods(" + pHR_COMPANY_ID + "," + v_period_type + ")) order by RF_FISCAL_YEAR_ID desc, RF_CAL_MONTH_ID desc");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_PAY_PERIODModel ob = new ACC_PAY_PERIODModel();
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.PAY_DATE = (dr["PAY_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PAY_DATE"]);
                    ob.START_DATE = (dr["START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DATE"]);
                    ob.END_DATE = (dr["END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ACC_PAY_PERIODModel> OpenPeriodListData(Int64 pHR_COMPANY_ID, Int64? pHR_PERIOD_TYPE_ID)
        {
            string v_period_type = "";
            try
            {
                if (pHR_PERIOD_TYPE_ID == null)
                {
                    v_period_type = "null";
                }
                else
                {
                    v_period_type = pHR_PERIOD_TYPE_ID.ToString();
                }

                var obList = new List<ACC_PAY_PERIODModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from table(pkg_myvar.get_company_open_periods(" + pHR_COMPANY_ID + "," + ((pHR_PERIOD_TYPE_ID == null) ? "null" : pHR_PERIOD_TYPE_ID.ToString()) + ")) order by RF_FISCAL_YEAR_ID desc, RF_CAL_MONTH_ID desc");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_PAY_PERIODModel ob = new ACC_PAY_PERIODModel();
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.PAY_DATE = (dr["PAY_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PAY_DATE"]);
                    ob.START_DATE = (dr["START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DATE"]);
                    ob.END_DATE = (dr["END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public List<ACC_PAY_PERIODModel> ClosePeriodListData(Int64 pHR_COMPANY_ID, Int64? pHR_PERIOD_TYPE_ID)
        {
            string v_period_type = "";
            try
            {
                if (pHR_PERIOD_TYPE_ID == null)
                {
                    v_period_type = "null";
                }
                else
                {
                    v_period_type = pHR_PERIOD_TYPE_ID.ToString();
                }

                var obList = new List<ACC_PAY_PERIODModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteSQLStatement("select * from table(pkg_myvar.get_company_close_periods(" + pHR_COMPANY_ID + "," + pHR_PERIOD_TYPE_ID + ")) order by RF_FISCAL_YEAR_ID desc, RF_CAL_MONTH_ID desc");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_PAY_PERIODModel ob = new ACC_PAY_PERIODModel();
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.PAY_DATE = (dr["PAY_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PAY_DATE"]);
                    ob.START_DATE = (dr["START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DATE"]);
                    ob.END_DATE = (dr["END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }



        public object GetAccPayPeriod(int? pHR_COMPANY_ID = null, int? pHR_PERIOD_TYPE_ID = null, string pIS_CLOSED = null, string pIS_SHOW4_RPT = null)
        {
            string sp = "pkg_common.rf_acc_pay_period_select";
            
            try
            {
                var obList = new List<ACC_PAY_PERIODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_PERIOD_TYPE_ID", Value = pHR_PERIOD_TYPE_ID},
                    new CommandParameter() {ParameterName = "pIS_CLOSED", Value = pIS_CLOSED},
                    new CommandParameter() {ParameterName = "pIS_SHOW4_RPT", Value = pIS_SHOW4_RPT},
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_PAY_PERIODModel ob = new ACC_PAY_PERIODModel();
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.PAY_DATE = (dr["PAY_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PAY_DATE"]);
                    ob.START_DATE = (dr["START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DATE"]);
                    ob.END_DATE = (dr["END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public object GetCompPayPeriodList(Int64 pageNumber, Int64 pageSize, int? pHR_COMPANY_ID = null, int? pHR_PERIOD_TYPE_ID = null, 
            string pIS_CLOSED = null, string pIS_SHOW4_RPT = null)
        {
            string sp = "pkg_common.rf_acc_pay_period_select";

            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<ACC_PAY_PERIODModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_PERIOD_TYPE_ID", Value = pHR_PERIOD_TYPE_ID},
                    new CommandParameter() {ParameterName = "pIS_CLOSED", Value = pIS_CLOSED},
                    new CommandParameter() {ParameterName = "pIS_SHOW4_RPT", Value = pIS_SHOW4_RPT},

                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_PAY_PERIODModel ob = new ACC_PAY_PERIODModel();
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.PAY_DATE = (dr["PAY_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PAY_DATE"]);
                    ob.START_DATE = (dr["START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DATE"]);
                    ob.END_DATE = (dr["END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.PERIOD_TYPE_NAME_EN = (dr["PERIOD_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERIOD_TYPE_NAME_EN"]);
                    ob.IS_SHOW4_RPT = (dr["IS_SHOW4_RPT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SHOW4_RPT"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Save()
        {
            const string sp = "pkg_common.acc_pay_period_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_PERIOD_TYPE_ID", Value = ob.HR_PERIOD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = ob.RF_CAL_MONTH_ID},
                     new CommandParameter() {ParameterName = "pPAY_DATE", Value = ob.PAY_DATE},
                     new CommandParameter() {ParameterName = "pSTART_DATE", Value = ob.START_DATE},
                     new CommandParameter() {ParameterName = "pEND_DATE", Value = ob.END_DATE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value = ob.IS_CLOSED},                     
                     new CommandParameter() {ParameterName = "pIS_SHOW4_RPT", Value = ob.IS_SHOW4_RPT},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                 
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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







        public ACC_PAY_PERIODModel GetCompPayPeriodByID(long pACC_PAY_PERIOD_ID)
        {
            string sp = "pkg_common.rf_acc_pay_period_select";

            try
            {
                ACC_PAY_PERIODModel ob = new ACC_PAY_PERIODModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 3001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {                    
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_PERIOD_TYPE_ID = (dr["HR_PERIOD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PERIOD_TYPE_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.PAY_DATE = (dr["PAY_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PAY_DATE"]);
                    ob.START_DATE = (dr["START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DATE"]);
                    ob.END_DATE = (dr["END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.PERIOD_TYPE_NAME_EN = (dr["PERIOD_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PERIOD_TYPE_NAME_EN"]);
                    ob.IS_SHOW4_RPT = (dr["IS_SHOW4_RPT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SHOW4_RPT"]);                    
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