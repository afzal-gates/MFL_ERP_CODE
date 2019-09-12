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
    public class RF_FISCAL_YEARModel
    {
        public Int64 RF_FISCAL_YEAR_ID { get; set; }
        public string FY_NAME_EN { get; set; }
        public string FY_NAME_BN { get; set; }
        public Int64 FY_VALUE_1 { get; set; }
        public Int64 FY_VALUE_2 { get; set; }
        public DateTime FY_START_DATE { get; set; }
        public DateTime FY_END_DATE { get; set; }
        public DateTime CY_START_DATE { get; set; }
        public DateTime CY_END_DATE { get; set; }
        public string IS_CLOSED { get; set; }        
        public Int64 CREATED_BY { get; set; }
        public string IS_ACTIVE { get; set; }
   



        public string Save()
        {
            const string sp = "pkg_common.rf_fiscal_year_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pFY_NAME_EN", Value = ob.FY_NAME_EN},
                     new CommandParameter() {ParameterName = "pFY_NAME_BN", Value = ob.FY_NAME_BN},
                     new CommandParameter() {ParameterName = "pFY_VALUE_1", Value = ob.FY_VALUE_1},
                     new CommandParameter() {ParameterName = "pFY_VALUE_2", Value = ob.FY_VALUE_2},
                     new CommandParameter() {ParameterName = "pFY_START_DATE", Value = ob.FY_START_DATE},
                     new CommandParameter() {ParameterName = "pFY_END_DATE", Value = ob.FY_END_DATE},
                     new CommandParameter() {ParameterName = "pCY_START_DATE", Value = ob.CY_START_DATE},
                     new CommandParameter() {ParameterName = "pCY_END_DATE", Value = ob.CY_END_DATE},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value = ob.IS_CLOSED},                     
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


        public List<RF_FISCAL_YEARModel> FiscalYearData(string pIS_CLOSED = null)
        {
            string sp = "pkg_common.rf_fiscal_year_select";
            try
            {
                var obList = new List<RF_FISCAL_YEARModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pIS_CLOSED", Value = pIS_CLOSED},
                    new CommandParameter() {ParameterName = "pOption", Value = 3000},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FISCAL_YEARModel ob = new RF_FISCAL_YEARModel();
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    ob.FY_NAME_BN = (dr["FY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_BN"]);
                    ob.FY_VALUE_1 = (dr["FY_VALUE_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FY_VALUE_1"]);
                    ob.FY_VALUE_2 = (dr["FY_VALUE_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FY_VALUE_2"]);
                    ob.FY_START_DATE = (dr["FY_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FY_START_DATE"]);
                    ob.FY_END_DATE = (dr["FY_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FY_END_DATE"]);
                    ob.CY_START_DATE = (dr["CY_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CY_START_DATE"]);
                    ob.CY_END_DATE = (dr["CY_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CY_END_DATE"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_FISCAL_YEARModel> FiscalYearDataAll()
        {
            string sp = "pkg_common.rf_fiscal_year_select";
            try
            {
                var obList = new List<RF_FISCAL_YEARModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FISCAL_YEARModel ob = new RF_FISCAL_YEARModel();
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.FY_NAME_EN = (dr["FY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_EN"]);
                    ob.FY_NAME_BN = (dr["FY_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FY_NAME_BN"]);
                    ob.FY_VALUE_1 = (dr["FY_VALUE_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FY_VALUE_1"]);
                    ob.FY_VALUE_2 = (dr["FY_VALUE_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FY_VALUE_2"]);
                    ob.FY_START_DATE = (dr["FY_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FY_START_DATE"]);
                    ob.FY_END_DATE = (dr["FY_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FY_END_DATE"]);
                    ob.CY_START_DATE = (dr["CY_START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CY_START_DATE"]);
                    ob.CY_END_DATE = (dr["CY_END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CY_END_DATE"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    
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