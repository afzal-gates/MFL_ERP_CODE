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
    public class MC_USR_BYRACCModel
    {
        public Int64 MC_USR_BYRACC_ID { get; set; }
        public Int64 SC_USER_ID { get; set; }
        public Int64 MC_BYR_ACC_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string LOGIN_ID { get; set; }
        public Int64? HR_EMPLOYEE_ID { get; set; }


        public string Save()
        {
            const string sp = "pkg_merchandising.mc_usr_byracc_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_USR_BYRACC_ID", Value = ob.MC_USR_BYRACC_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<MC_USR_BYRACCModel> GetUsersByByrAc(string pMC_BYR_ACC_IDS)
        {
            string sp = "pkg_merchandising.mc_usr_byracc_select";
            try
            {
                var obList = new List<MC_USR_BYRACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_IDS", Value = pMC_BYR_ACC_IDS},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_USR_BYRACCModel ob = new MC_USR_BYRACCModel();
                    
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_USR_BYRACCModel> GetUsersByByrAcLst(Int64 pMC_SMP_REQ_H_ID)
        {
            string sp = "pkg_merchandising.mc_usr_byracc_select";
            try
            {
                var obList = new List<MC_USR_BYRACCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_USR_BYRACCModel ob = new MC_USR_BYRACCModel();

                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);

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