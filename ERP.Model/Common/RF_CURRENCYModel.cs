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
    public class RF_CURRENCYModel
    {
        public Int64 RF_CURRENCY_ID { get; set; }
        public string CURR_CODE { get; set; }
        public string CURR_NAME_EN { get; set; }
        public string CURR_NAME_BN { get; set; }
        public Int64 HR_COUNTRY_ID { get; set; }
        public string IS_BASE { get; set; }
        public string CURR_SYMBOL { get; set; }
        public Decimal EXCHANGE_RATE { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.rf_currency_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCURR_CODE", Value = ob.CURR_CODE},
                     new CommandParameter() {ParameterName = "pCURR_NAME_EN", Value = ob.CURR_NAME_EN},
                     new CommandParameter() {ParameterName = "pCURR_NAME_BN", Value = ob.CURR_NAME_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pIS_BASE", Value = ob.IS_BASE},
                     new CommandParameter() {ParameterName = "pCURR_SYMBOL", Value = ob.CURR_SYMBOL},
                     new CommandParameter() {ParameterName = "pEXCHANGE_RATE", Value = ob.EXCHANGE_RATE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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
                         i++ ;
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
            const string sp = "pkg_common.rf_currency_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCURR_CODE", Value = ob.CURR_CODE},
                     new CommandParameter() {ParameterName = "pCURR_NAME_EN", Value = ob.CURR_NAME_EN},
                     new CommandParameter() {ParameterName = "pCURR_NAME_BN", Value = ob.CURR_NAME_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pIS_BASE", Value = ob.IS_BASE},
                     new CommandParameter() {ParameterName = "pCURR_SYMBOL", Value = ob.CURR_SYMBOL},
                     new CommandParameter() {ParameterName = "pEXCHANGE_RATE", Value = ob.EXCHANGE_RATE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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
                         i++ ;
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
            const string sp = "pkg_common.rf_currency_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCURR_CODE", Value = ob.CURR_CODE},
                     new CommandParameter() {ParameterName = "pCURR_NAME_EN", Value = ob.CURR_NAME_EN},
                     new CommandParameter() {ParameterName = "pCURR_NAME_BN", Value = ob.CURR_NAME_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pIS_BASE", Value = ob.IS_BASE},
                     new CommandParameter() {ParameterName = "pCURR_SYMBOL", Value = ob.CURR_SYMBOL},
                     new CommandParameter() {ParameterName = "pEXCHANGE_RATE", Value = ob.EXCHANGE_RATE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_CURRENCYModel> SelectAll()
        {
            string sp = "pkg_common.rf_currency_select";
            try
            {
                var obList = new List<RF_CURRENCYModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_CURRENCYModel ob = new RF_CURRENCYModel();
                            ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                            ob.CURR_CODE = (dr["CURR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_CODE"]);
                            ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                            ob.CURR_NAME_BN = (dr["CURR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_BN"]);
                            ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                            ob.IS_BASE = (dr["IS_BASE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BASE"]);
                            ob.CURR_SYMBOL = (dr["CURR_SYMBOL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_SYMBOL"]);
                            ob.EXCHANGE_RATE = (dr["EXCHANGE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["EXCHANGE_RATE"]);
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

        public RF_CURRENCYModel Select(Int64 ID)
        {
            string sp = "pkg_common.rf_currency_select";
            try
            {
                var ob = new RF_CURRENCYModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                        ob.CURR_CODE = (dr["CURR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_CODE"]);
                        ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                        ob.CURR_NAME_BN = (dr["CURR_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_BN"]);
                        ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                        ob.IS_BASE = (dr["IS_BASE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BASE"]);
                        ob.CURR_SYMBOL = (dr["CURR_SYMBOL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_SYMBOL"]);
                        ob.EXCHANGE_RATE = (dr["EXCHANGE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["EXCHANGE_RATE"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

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