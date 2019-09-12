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
    public class RF_BANKModel
    {
        public Int64 RF_BANK_ID { get; set; }
        public string BANK_CODE { get; set; }
        public string BANK_NAME_EN { get; set; }
        public string BANK_NAME_BN { get; set; }
        public string BANK_PREFIX { get; set; }
        public string IS_LOCAL { get; set; }
        public Int64 HR_COUNTRY_ID { get; set; }
        public string IS_TREASURY { get; set; }
        public string SWIFT_CODE { get; set; }
        public string IBN_NO { get; set; }
        public string CALL_CENTER_NO { get; set; }
        public string WEB_URL { get; set; }
        public string IS_ACTIVE { get; set; }        
        public Int64 CREATED_BY { get; set; }        
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.rf_bank_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pBANK_CODE", Value = ob.BANK_CODE},
                     new CommandParameter() {ParameterName = "pBANK_NAME_EN", Value = ob.BANK_NAME_EN},
                     new CommandParameter() {ParameterName = "pBANK_NAME_BN", Value = ob.BANK_NAME_BN},
                     new CommandParameter() {ParameterName = "pBANK_PREFIX", Value = ob.BANK_PREFIX},
                     new CommandParameter() {ParameterName = "pIS_LOCAL", Value = ob.IS_LOCAL},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pIS_TREASURY", Value = ob.IS_TREASURY},
                     new CommandParameter() {ParameterName = "pSWIFT_CODE", Value = ob.SWIFT_CODE},
                     new CommandParameter() {ParameterName = "pIBN_NO", Value = ob.IBN_NO},
                     new CommandParameter() {ParameterName = "pCALL_CENTER_NO", Value = ob.CALL_CENTER_NO},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                    
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opRF_BANK_ID", Value =0,Direction = ParameterDirection.Output},
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
            const string sp = "pkg_common.rf_bank_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pBANK_CODE", Value = ob.BANK_CODE},
                     new CommandParameter() {ParameterName = "pBANK_NAME_EN", Value = ob.BANK_NAME_EN},
                     new CommandParameter() {ParameterName = "pBANK_NAME_BN", Value = ob.BANK_NAME_BN},
                     new CommandParameter() {ParameterName = "pBANK_PREFIX", Value = ob.BANK_PREFIX},
                     new CommandParameter() {ParameterName = "pIS_LOCAL", Value = ob.IS_LOCAL},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pIS_TREASURY", Value = ob.IS_TREASURY},
                     new CommandParameter() {ParameterName = "pSWIFT_CODE", Value = ob.SWIFT_CODE},
                     new CommandParameter() {ParameterName = "pIBN_NO", Value = ob.IBN_NO},
                     new CommandParameter() {ParameterName = "pCALL_CENTER_NO", Value = ob.CALL_CENTER_NO},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},                     
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
            const string sp = "SP_RF_BANK";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pBANK_CODE", Value = ob.BANK_CODE},
                     new CommandParameter() {ParameterName = "pBANK_NAME_EN", Value = ob.BANK_NAME_EN},
                     new CommandParameter() {ParameterName = "pBANK_NAME_BN", Value = ob.BANK_NAME_BN},
                     new CommandParameter() {ParameterName = "pBANK_PREFIX", Value = ob.BANK_PREFIX},
                     new CommandParameter() {ParameterName = "pIS_LOCAL", Value = ob.IS_LOCAL},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pIS_TREASURY", Value = ob.IS_TREASURY},
                     new CommandParameter() {ParameterName = "pSWIFT_CODE", Value = ob.SWIFT_CODE},
                     new CommandParameter() {ParameterName = "pIBN_NO", Value = ob.IBN_NO},
                     new CommandParameter() {ParameterName = "pCALL_CENTER_NO", Value = ob.CALL_CENTER_NO},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},                     
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

        public List<RF_BANKModel> SelectAll()
        {
            string sp = "pkg_common.rf_bank_select";
            try
            {
                var obList = new List<RF_BANKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_BANKModel ob = new RF_BANKModel();
                            ob.RF_BANK_ID = (dr["RF_BANK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_ID"]);
                            ob.BANK_CODE = (dr["BANK_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_CODE"]);
                            ob.BANK_NAME_EN = (dr["BANK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_NAME_EN"]);
                            ob.BANK_NAME_BN = (dr["BANK_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_NAME_BN"]);
                            ob.BANK_PREFIX = (dr["BANK_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_PREFIX"]);
                            ob.IS_LOCAL = (dr["IS_LOCAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL"]);
                            ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                            ob.IS_TREASURY = (dr["IS_TREASURY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TREASURY"]);
                            ob.SWIFT_CODE = (dr["SWIFT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWIFT_CODE"]);
                            ob.IBN_NO = (dr["IBN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IBN_NO"]);
                            ob.CALL_CENTER_NO = (dr["CALL_CENTER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CALL_CENTER_NO"]);
                            ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            
                            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);                            
                            ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_BANKModel Select(long ID)
        {
            string sp = "pkg_common.rf_bank_select";
            try
            {
                var ob = new RF_BANKModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_BANK_ID = (dr["RF_BANK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_ID"]);
                        ob.BANK_CODE = (dr["BANK_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_CODE"]);
                        ob.BANK_NAME_EN = (dr["BANK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_NAME_EN"]);
                        ob.BANK_NAME_BN = (dr["BANK_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_NAME_BN"]);
                        ob.BANK_PREFIX = (dr["BANK_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_PREFIX"]);
                        ob.IS_LOCAL = (dr["IS_LOCAL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL"]);
                        ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                        ob.IS_TREASURY = (dr["IS_TREASURY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TREASURY"]);
                        ob.SWIFT_CODE = (dr["SWIFT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWIFT_CODE"]);
                        ob.IBN_NO = (dr["IBN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IBN_NO"]);
                        ob.CALL_CENTER_NO = (dr["CALL_CENTER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CALL_CENTER_NO"]);
                        ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                        
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);                        
                        ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

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