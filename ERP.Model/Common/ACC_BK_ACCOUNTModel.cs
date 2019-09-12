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
    public class ACC_BK_ACCOUNTModel
    {
        public Int64? ACC_BK_ACCOUNT_ID { get; set; }
        [Required(ErrorMessage="Please input account number")]
        public string BK_ACC_NO { get; set; }
        [Required(ErrorMessage = "Please input account title")]
        public string BK_ACC_TITLE { get; set; }
        [Required(ErrorMessage = "Please select bank")]
        public Int64? RF_BANK_ID { get; set; }
        [Required(ErrorMessage = "Please select bank branch")]
        public Int64? RF_BANK_BRANCH_ID { get; set; }
        [Required(ErrorMessage = "Please select account type")]
        public Int64? LK_ACC_TYPE_ID { get; set; }
        public string IS_INT_ACC { get; set; }
        public string IS_AP_ACC { get; set; }
        public string IS_AR_ACC { get; set; }
        public string IS_EMP_ACC { get; set; }
        [Required(ErrorMessage = "Please select currence")]
        public Int64? RF_CURRENCY_ID { get; set; }
        public DateTime? EFFECTIVE_FROM { get; set; }
        public DateTime? EXPIRED_ON { get; set; }
        public string IS_ACTIVE { get; set; }
        
        public Int64? CREATED_BY { get; set; }        
        public Int64? LAST_UPDATED_BY { get; set; }
        public Int64? LAST_UPDATE_LOGIN { get; set; }
        public Int64? VERSION_NO { get; set; }

        public Int64? HR_EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }

        public Int64 SCM_SUP_BK_ACC_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }

        

        public string Save()
        {
            const string sp = "pkg_common.acc_bk_account_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_BK_ACCOUNT_ID", Value = ob.ACC_BK_ACCOUNT_ID==null?0:ob.ACC_BK_ACCOUNT_ID},
                     new CommandParameter() {ParameterName = "pBK_ACC_NO", Value = ob.BK_ACC_NO},
                     new CommandParameter() {ParameterName = "pBK_ACC_TITLE", Value = ob.BK_ACC_TITLE},
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pRF_BANK_BRANCH_ID", Value = ob.RF_BANK_BRANCH_ID},
                     new CommandParameter() {ParameterName = "pLK_ACC_TYPE_ID", Value = ob.LK_ACC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_INT_ACC", Value = ob.IS_INT_ACC},
                     new CommandParameter() {ParameterName = "pIS_AP_ACC", Value = ob.IS_AP_ACC},
                     new CommandParameter() {ParameterName = "pIS_AR_ACC", Value = ob.IS_AR_ACC},
                     new CommandParameter() {ParameterName = "pIS_EMP_ACC", Value = ob.IS_EMP_ACC},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},
                     new CommandParameter() {ParameterName = "pEXPIRED_ON", Value = ob.EXPIRED_ON},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},                     
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_LOGIN", Value = ob.LAST_UPDATE_LOGIN},
                     //new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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
            const string sp = "SP_ACC_BK_ACCOUNT";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_BK_ACCOUNT_ID", Value = ob.ACC_BK_ACCOUNT_ID},
                     new CommandParameter() {ParameterName = "pBK_ACC_NO", Value = ob.BK_ACC_NO},
                     new CommandParameter() {ParameterName = "pBK_ACC_TITLE", Value = ob.BK_ACC_TITLE},
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pRF_BANK_BRANCH_ID", Value = ob.RF_BANK_BRANCH_ID},
                     new CommandParameter() {ParameterName = "pLK_ACC_TYPE_ID", Value = ob.LK_ACC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_INT_ACC", Value = ob.IS_INT_ACC},
                     new CommandParameter() {ParameterName = "pIS_AP_ACC", Value = ob.IS_AP_ACC},
                     new CommandParameter() {ParameterName = "pIS_AR_ACC", Value = ob.IS_AR_ACC},
                     new CommandParameter() {ParameterName = "pIS_EMP_ACC", Value = ob.IS_EMP_ACC},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},
                     new CommandParameter() {ParameterName = "pEXPIRED_ON", Value = ob.EXPIRED_ON},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},                     
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_LOGIN", Value = ob.LAST_UPDATE_LOGIN},
                     new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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
            const string sp = "SP_ACC_BK_ACCOUNT";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_BK_ACCOUNT_ID", Value = ob.ACC_BK_ACCOUNT_ID},
                     new CommandParameter() {ParameterName = "pBK_ACC_NO", Value = ob.BK_ACC_NO},
                     new CommandParameter() {ParameterName = "pBK_ACC_TITLE", Value = ob.BK_ACC_TITLE},
                     new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                     new CommandParameter() {ParameterName = "pRF_BANK_BRANCH_ID", Value = ob.RF_BANK_BRANCH_ID},
                     new CommandParameter() {ParameterName = "pLK_ACC_TYPE_ID", Value = ob.LK_ACC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_INT_ACC", Value = ob.IS_INT_ACC},
                     new CommandParameter() {ParameterName = "pIS_AP_ACC", Value = ob.IS_AP_ACC},
                     new CommandParameter() {ParameterName = "pIS_AR_ACC", Value = ob.IS_AR_ACC},
                     new CommandParameter() {ParameterName = "pIS_EMP_ACC", Value = ob.IS_EMP_ACC},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},
                     new CommandParameter() {ParameterName = "pEXPIRED_ON", Value = ob.EXPIRED_ON},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},                     
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_LOGIN", Value = ob.LAST_UPDATE_LOGIN},
                     new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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

        public List<ACC_BK_ACCOUNTModel> SelectAll()
        {
            string sp = "Select_ACC_BK_ACCOUNT";
            try
            {
                var obList = new List<ACC_BK_ACCOUNTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_BK_ACCOUNT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            ACC_BK_ACCOUNTModel ob = new ACC_BK_ACCOUNTModel();
                            ob.ACC_BK_ACCOUNT_ID = (dr["ACC_BK_ACCOUNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_BK_ACCOUNT_ID"]);
                            ob.BK_ACC_NO = (dr["BK_ACC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BK_ACC_NO"]);
                            ob.BK_ACC_TITLE = (dr["BK_ACC_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BK_ACC_TITLE"]);
                            ob.RF_BANK_ID = (dr["RF_BANK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_ID"]);
                            ob.RF_BANK_BRANCH_ID = (dr["RF_BANK_BRANCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_BRANCH_ID"]);
                            ob.LK_ACC_TYPE_ID = (dr["LK_ACC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACC_TYPE_ID"]);
                            ob.IS_INT_ACC = (dr["IS_INT_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_INT_ACC"]);
                            ob.IS_AP_ACC = (dr["IS_AP_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AP_ACC"]);
                            ob.IS_AR_ACC = (dr["IS_AR_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AR_ACC"]);
                            ob.IS_EMP_ACC = (dr["IS_EMP_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMP_ACC"]);
                            ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                            ob.EFFECTIVE_FROM = (dr["EFFECTIVE_FROM"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_FROM"]);
                            ob.EXPIRED_ON = (dr["EXPIRED_ON"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRED_ON"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            
                            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);                            
                            ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                            ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                            ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_BK_ACCOUNTModel Select(long ID)
        {
            string sp = "Select_ACC_BK_ACCOUNT";
            try
            {
                var ob = new ACC_BK_ACCOUNTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_BK_ACCOUNT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.ACC_BK_ACCOUNT_ID = (dr["ACC_BK_ACCOUNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_BK_ACCOUNT_ID"]);
                        ob.BK_ACC_NO = (dr["BK_ACC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BK_ACC_NO"]);
                        ob.BK_ACC_TITLE = (dr["BK_ACC_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BK_ACC_TITLE"]);
                        ob.RF_BANK_ID = (dr["RF_BANK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_ID"]);
                        ob.RF_BANK_BRANCH_ID = (dr["RF_BANK_BRANCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_BRANCH_ID"]);
                        ob.LK_ACC_TYPE_ID = (dr["LK_ACC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACC_TYPE_ID"]);
                        ob.IS_INT_ACC = (dr["IS_INT_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_INT_ACC"]);
                        ob.IS_AP_ACC = (dr["IS_AP_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AP_ACC"]);
                        ob.IS_AR_ACC = (dr["IS_AR_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AR_ACC"]);
                        ob.IS_EMP_ACC = (dr["IS_EMP_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMP_ACC"]);
                        ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                        ob.EFFECTIVE_FROM = (dr["EFFECTIVE_FROM"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_FROM"]);
                        ob.EXPIRED_ON = (dr["EXPIRED_ON"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRED_ON"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                        
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);                        
                        ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                        ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                        ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ACC_BK_ACCOUNTModel> BankAccountAutoList(string pIS_EMP_ACC, string pBK_ACC_NO, int? pRF_BANK_ID)
        {
            string sp = "pkg_common.acc_bk_account_select";
            try
            {
                var obList = new List<ACC_BK_ACCOUNTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = pRF_BANK_ID},
                    new CommandParameter() {ParameterName = "pBK_ACC_NO", Value = pBK_ACC_NO},
                    new CommandParameter() {ParameterName = "pOption", Value =3002},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_BK_ACCOUNTModel ob = new ACC_BK_ACCOUNTModel();
                    ob.ACC_BK_ACCOUNT_ID = (dr["ACC_BK_ACCOUNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_BK_ACCOUNT_ID"]);
                    ob.BK_ACC_NO = (dr["BK_ACC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BK_ACC_NO"]);
                    ob.BK_ACC_TITLE = (dr["BK_ACC_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BK_ACC_TITLE"]);
                    ob.RF_BANK_ID = (dr["RF_BANK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_ID"]);
                    ob.RF_BANK_BRANCH_ID = (dr["RF_BANK_BRANCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BANK_BRANCH_ID"]);
                    ob.LK_ACC_TYPE_ID = (dr["LK_ACC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACC_TYPE_ID"]);
                    ob.IS_INT_ACC = (dr["IS_INT_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_INT_ACC"]);
                    ob.IS_AP_ACC = (dr["IS_AP_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AP_ACC"]);
                    ob.IS_AR_ACC = (dr["IS_AR_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AR_ACC"]);
                    ob.IS_EMP_ACC = (dr["IS_EMP_ACC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMP_ACC"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.EFFECTIVE_FROM = (dr["EFFECTIVE_FROM"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_FROM"]);
                    ob.EXPIRED_ON = (dr["EXPIRED_ON"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRED_ON"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);

                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
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