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
    public class RF_BANK_BRANCHModel
    {
        public Int64 RF_BANK_BRANCH_ID { get; set; }
        public Int64 RF_BANK_ID { get; set; }
        public string BANK_BRANCH_CODE { get; set; }
        public string BANK_BRANCH_NAME_EN { get; set; }
        public string BANK_BRANCH_NAME_BN { get; set; }
        public string SWIFT_CODE_EXT { get; set; }
        public string ROUTING_NO { get; set; }
        public string BRANCH_ADDRESS_EN { get; set; }
        public string BRANCH_ADDRESS_BN { get; set; }
        public Int64 HR_COUNTRY_ID { get; set; }
        public string PHONE_NO { get; set; }
        public string PHONE_EXT { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL_ID { get; set; }
        public string IS_CORPORATE_L { get; set; }
        public string IS_CORPORATE_F { get; set; }
        public string IS_ACTIVE { get; set; }        
        public Int64 CREATED_BY { get; set; }        
        public Int64 LAST_UPDATED_BY { get; set; }


        public string BANK_NAME_EN { get; set; }
        public string SWIFT_CODE { get; set; }        

        

        public string Save()
        {
            const string sp = "pkg_common.rf_bank_branch_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_BRANCH_ID", Value = ob.RF_BANK_BRANCH_ID},
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_CODE", Value = ob.BANK_BRANCH_CODE},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_NAME_EN", Value = ob.BANK_BRANCH_NAME_EN},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_NAME_BN", Value = ob.BANK_BRANCH_NAME_BN},
                     new CommandParameter() {ParameterName = "pSWIFT_CODE_EXT", Value = ob.SWIFT_CODE_EXT},
                     new CommandParameter() {ParameterName = "pROUTING_NO", Value = ob.ROUTING_NO},
                     new CommandParameter() {ParameterName = "pBRANCH_ADDRESS_EN", Value = ob.BRANCH_ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pBRANCH_ADDRESS_BN", Value = ob.BRANCH_ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPHONE_NO", Value = ob.PHONE_NO},
                     new CommandParameter() {ParameterName = "pPHONE_EXT", Value = ob.PHONE_EXT},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pIS_CORPORATE_L", Value = ob.IS_CORPORATE_L},
                     new CommandParameter() {ParameterName = "pIS_CORPORATE_F", Value = ob.IS_CORPORATE_F},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]}, 
                     
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]}, 
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opRF_BANK_BRANCH_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "SP_RF_BANK_BRANCH";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_BRANCH_ID", Value = ob.RF_BANK_BRANCH_ID},
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_CODE", Value = ob.BANK_BRANCH_CODE},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_NAME_EN", Value = ob.BANK_BRANCH_NAME_EN},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_NAME_BN", Value = ob.BANK_BRANCH_NAME_BN},
                     new CommandParameter() {ParameterName = "pSWIFT_CODE_EXT", Value = ob.SWIFT_CODE_EXT},
                     new CommandParameter() {ParameterName = "pROUTING_NO", Value = ob.ROUTING_NO},
                     new CommandParameter() {ParameterName = "pBRANCH_ADDRESS_EN", Value = ob.BRANCH_ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pBRANCH_ADDRESS_BN", Value = ob.BRANCH_ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPHONE_NO", Value = ob.PHONE_NO},
                     new CommandParameter() {ParameterName = "pPHONE_EXT", Value = ob.PHONE_EXT},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pIS_CORPORATE_L", Value = ob.IS_CORPORATE_L},
                     new CommandParameter() {ParameterName = "pIS_CORPORATE_F", Value = ob.IS_CORPORATE_F},
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
            const string sp = "SP_RF_BANK_BRANCH";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_BRANCH_ID", Value = ob.RF_BANK_BRANCH_ID},
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_CODE", Value = ob.BANK_BRANCH_CODE},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_NAME_EN", Value = ob.BANK_BRANCH_NAME_EN},
                     new CommandParameter() {ParameterName = "pBANK_BRANCH_NAME_BN", Value = ob.BANK_BRANCH_NAME_BN},
                     new CommandParameter() {ParameterName = "pSWIFT_CODE_EXT", Value = ob.SWIFT_CODE_EXT},
                     new CommandParameter() {ParameterName = "pROUTING_NO", Value = ob.ROUTING_NO},
                     new CommandParameter() {ParameterName = "pBRANCH_ADDRESS_EN", Value = ob.BRANCH_ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pBRANCH_ADDRESS_BN", Value = ob.BRANCH_ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPHONE_NO", Value = ob.PHONE_NO},
                     new CommandParameter() {ParameterName = "pPHONE_EXT", Value = ob.PHONE_EXT},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pIS_CORPORATE_L", Value = ob.IS_CORPORATE_L},
                     new CommandParameter() {ParameterName = "pIS_CORPORATE_F", Value = ob.IS_CORPORATE_F},
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

        public List<RF_BANK_BRANCHModel> SelectAll()
        {
            string sp = "pkg_common.rf_bank_branch_select";
            try
            {
                var obList = new List<RF_BANK_BRANCHModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_BRANCH_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_BANK_BRANCHModel ob = new RF_BANK_BRANCHModel();
                    ob.RF_BANK_BRANCH_ID = (dr["RF_BANK_BRANCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_BRANCH_ID"]);
                    ob.RF_BANK_ID = (dr["RF_BANK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_ID"]);
                    ob.BANK_BRANCH_CODE = (dr["BANK_BRANCH_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_CODE"]);
                    ob.BANK_BRANCH_NAME_EN = (dr["BANK_BRANCH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_NAME_EN"]);
                    ob.BANK_BRANCH_NAME_BN = (dr["BANK_BRANCH_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_NAME_BN"]);
                    ob.SWIFT_CODE_EXT = (dr["SWIFT_CODE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWIFT_CODE_EXT"]);
                    ob.ROUTING_NO = (dr["ROUTING_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROUTING_NO"]);
                    ob.BRANCH_ADDRESS_EN = (dr["BRANCH_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRANCH_ADDRESS_EN"]);
                    ob.BRANCH_ADDRESS_BN = (dr["BRANCH_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRANCH_ADDRESS_BN"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.PHONE_NO = (dr["PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_NO"]);
                    ob.PHONE_EXT = (dr["PHONE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_EXT"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.IS_CORPORATE_L = (dr["IS_CORPORATE_L"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORPORATE_L"]);
                    ob.IS_CORPORATE_F = (dr["IS_CORPORATE_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORPORATE_F"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.BANK_NAME_EN = (dr["BANK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_NAME_EN"]);
                    ob.SWIFT_CODE = (dr["SWIFT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWIFT_CODE"]);

                            
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

        public RF_BANK_BRANCHModel Select(long ID)
        {
            string sp = "Select_RF_BANK_BRANCH";
            try
            {
                var ob = new RF_BANK_BRANCHModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_BRANCH_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_BANK_BRANCH_ID = (dr["RF_BANK_BRANCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_BRANCH_ID"]);
                        ob.RF_BANK_ID = (dr["RF_BANK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_ID"]);
                        ob.BANK_BRANCH_CODE = (dr["BANK_BRANCH_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_CODE"]);
                        ob.BANK_BRANCH_NAME_EN = (dr["BANK_BRANCH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_NAME_EN"]);
                        ob.BANK_BRANCH_NAME_BN = (dr["BANK_BRANCH_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_NAME_BN"]);
                        ob.SWIFT_CODE_EXT = (dr["SWIFT_CODE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWIFT_CODE_EXT"]);
                        ob.ROUTING_NO = (dr["ROUTING_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROUTING_NO"]);
                        ob.BRANCH_ADDRESS_EN = (dr["BRANCH_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRANCH_ADDRESS_EN"]);
                        ob.BRANCH_ADDRESS_BN = (dr["BRANCH_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRANCH_ADDRESS_BN"]);
                        ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                        ob.PHONE_NO = (dr["PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_NO"]);
                        ob.PHONE_EXT = (dr["PHONE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_EXT"]);
                        ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                        ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                        ob.IS_CORPORATE_L = (dr["IS_CORPORATE_L"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORPORATE_L"]);
                        ob.IS_CORPORATE_F = (dr["IS_CORPORATE_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORPORATE_F"]);
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

        public List<RF_BANK_BRANCHModel> BankBranchDataList(int? pRF_BANK_ID)
        {
            string sp = "pkg_common.rf_bank_branch_select";
            try
            {                
                var obList = new List<RF_BANK_BRANCHModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value =pRF_BANK_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_BANK_BRANCHModel ob = new RF_BANK_BRANCHModel();
                    ob.RF_BANK_BRANCH_ID = (dr["RF_BANK_BRANCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_BRANCH_ID"]);
                    ob.RF_BANK_ID = (dr["RF_BANK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_ID"]);
                    ob.BANK_BRANCH_CODE = (dr["BANK_BRANCH_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_CODE"]);
                    ob.BANK_BRANCH_NAME_EN = (dr["BANK_BRANCH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_NAME_EN"]);
                    ob.BANK_BRANCH_NAME_BN = (dr["BANK_BRANCH_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BANK_BRANCH_NAME_BN"]);
                    ob.SWIFT_CODE_EXT = (dr["SWIFT_CODE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWIFT_CODE_EXT"]);
                    ob.SWIFT_CODE = (dr["SWIFT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWIFT_CODE"]);
                    ob.ROUTING_NO = (dr["ROUTING_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROUTING_NO"]);
                    ob.BRANCH_ADDRESS_EN = (dr["BRANCH_ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRANCH_ADDRESS_EN"]);
                    ob.BRANCH_ADDRESS_BN = (dr["BRANCH_ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRANCH_ADDRESS_BN"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.PHONE_NO = (dr["PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_NO"]);
                    ob.PHONE_EXT = (dr["PHONE_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PHONE_EXT"]);
                    ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                    ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                    ob.IS_CORPORATE_L = (dr["IS_CORPORATE_L"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORPORATE_L"]);
                    ob.IS_CORPORATE_F = (dr["IS_CORPORATE_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CORPORATE_F"]);
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

    }
}