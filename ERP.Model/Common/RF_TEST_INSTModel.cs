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
    public class RF_TEST_INSTModel
    {
        public Int64 RF_TEST_INST_ID { get; set; }
        public string TEST_INST_NAME_EN { get; set; }
        public string TEST_INST_NAME_BN { get; set; }
        public string ADDRESS_EN { get; set; }
        public string ADDRESS_BN { get; set; }
        public Int64 HR_COUNTRY_ID { get; set; }
        public string POST_CODE { get; set; }
        public string PO_BOX_NO { get; set; }
        public string WORK_PHONE { get; set; }
        public string MOB_PHONE { get; set; }
        public string FAX_NO { get; set; }
        public string EMAIL_ID { get; set; }
        public string WEB_URL { get; set; }
        public string IS_ACTIVE { get; set; }


        public string Save()
        {
            const string sp = "SP_RF_TEST_INST";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TEST_INST_ID", Value = ob.RF_TEST_INST_ID},
                     new CommandParameter() {ParameterName = "pTEST_INST_NAME_EN", Value = ob.TEST_INST_NAME_EN},
                     new CommandParameter() {ParameterName = "pTEST_INST_NAME_BN", Value = ob.TEST_INST_NAME_BN},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
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
            const string sp = "SP_RF_TEST_INST";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TEST_INST_ID", Value = ob.RF_TEST_INST_ID},
                     new CommandParameter() {ParameterName = "pTEST_INST_NAME_EN", Value = ob.TEST_INST_NAME_EN},
                     new CommandParameter() {ParameterName = "pTEST_INST_NAME_BN", Value = ob.TEST_INST_NAME_BN},
                     new CommandParameter() {ParameterName = "pADDRESS_EN", Value = ob.ADDRESS_EN},
                     new CommandParameter() {ParameterName = "pADDRESS_BN", Value = ob.ADDRESS_BN},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pPOST_CODE", Value = ob.POST_CODE},
                     new CommandParameter() {ParameterName = "pPO_BOX_NO", Value = ob.PO_BOX_NO},
                     new CommandParameter() {ParameterName = "pWORK_PHONE", Value = ob.WORK_PHONE},
                     new CommandParameter() {ParameterName = "pMOB_PHONE", Value = ob.MOB_PHONE},
                     new CommandParameter() {ParameterName = "pFAX_NO", Value = ob.FAX_NO},
                     new CommandParameter() {ParameterName = "pEMAIL_ID", Value = ob.EMAIL_ID},
                     new CommandParameter() {ParameterName = "pWEB_URL", Value = ob.WEB_URL},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     
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

        

        public List<RF_TEST_INSTModel> SelectAll()
        {
            string sp = "Select_RF_TEST_INST";
            try
            {
                var obList = new List<RF_TEST_INSTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TEST_INST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_TEST_INSTModel ob = new RF_TEST_INSTModel();
                            ob.RF_TEST_INST_ID = (dr["RF_TEST_INST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_TEST_INST_ID"]);
                            ob.TEST_INST_NAME_EN = (dr["TEST_INST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_INST_NAME_EN"]);
                            ob.TEST_INST_NAME_BN = (dr["TEST_INST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_INST_NAME_BN"]);
                            ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                            ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                            ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                            ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                            ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                            ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                            ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                            ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                            ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                            ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
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

        public RF_TEST_INSTModel Select(long ID)
        {
            string sp = "Select_RF_TEST_INST";
            try
            {
                var ob = new RF_TEST_INSTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_TEST_INST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_TEST_INST_ID = (dr["RF_TEST_INST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_TEST_INST_ID"]);
                        ob.TEST_INST_NAME_EN = (dr["TEST_INST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_INST_NAME_EN"]);
                        ob.TEST_INST_NAME_BN = (dr["TEST_INST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TEST_INST_NAME_BN"]);
                        ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);
                        ob.ADDRESS_BN = (dr["ADDRESS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_BN"]);
                        ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                        ob.POST_CODE = (dr["POST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_CODE"]);
                        ob.PO_BOX_NO = (dr["PO_BOX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_BOX_NO"]);
                        ob.WORK_PHONE = (dr["WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_PHONE"]);
                        ob.MOB_PHONE = (dr["MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOB_PHONE"]);
                        ob.FAX_NO = (dr["FAX_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAX_NO"]);
                        ob.EMAIL_ID = (dr["EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_ID"]);
                        ob.WEB_URL = (dr["WEB_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEB_URL"]);
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