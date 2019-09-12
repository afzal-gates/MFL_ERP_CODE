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
    public class MC_RATE_SPEC_AOPModel
    {
        public Int64 MC_RATE_SPEC_AOP_ID { get; set; }
        public Int64 MC_FAB_PROC_RATE_ID { get; set; }
        public Int64 LK_AOP_TYPE_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? MC_FAB_PROC_GRP_ID { get; set; }
        public String FAB_PROC_NAME { get; set; }
        public decimal PROC_RATE { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Int64 RATE_MOU_ID { get; set; }
        public string AOP_TYPE_NAME { get; set; }
        public string XML { get; set; }

        public string Save()
        {
            const string sp = "pkg_budget_sheet.mc_rate_spec_aop_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "SP_MC_RATE_SPEC_AOP";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_AOP_ID", Value = ob.MC_RATE_SPEC_AOP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pLK_AOP_TYPE_ID", Value = ob.LK_AOP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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
            const string sp = "SP_MC_RATE_SPEC_AOP";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_AOP_ID", Value = ob.MC_RATE_SPEC_AOP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pLK_AOP_TYPE_ID", Value = ob.LK_AOP_TYPE_ID},
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

        public List<MC_RATE_SPEC_AOPModel> SelectAll()
        {
            string sp = "pkg_budget_sheet.mc_rate_spec_aop_select";
            try
            {
                var obList = new List<MC_RATE_SPEC_AOPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_AOP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_RATE_SPEC_AOPModel ob = new MC_RATE_SPEC_AOPModel();
                            ob.MC_RATE_SPEC_AOP_ID = (dr["MC_RATE_SPEC_AOP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RATE_SPEC_AOP_ID"]);
                            ob.MC_FAB_PROC_GRP_ID = (dr["MC_FAB_PROC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_GRP_ID"]);
                            ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                            ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AOP_TYPE_ID"]);
                            ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);
                            ob.PROC_RATE = (dr["PROC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROC_RATE"]);
                            ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                            ob.RATE_MOU_ID = (dr["RATE_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RATE_MOU_ID"]);
                            ob.AOP_TYPE_NAME = (dr["AOP_TYPE_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["AOP_TYPE_NAME"]);

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_RATE_SPEC_AOPModel Select(long ID)
        {
            string sp = "Select_MC_RATE_SPEC_AOP";
            try
            {
                var ob = new MC_RATE_SPEC_AOPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_RATE_SPEC_AOP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_RATE_SPEC_AOP_ID = (dr["MC_RATE_SPEC_AOP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RATE_SPEC_AOP_ID"]);
                        ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                        ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AOP_TYPE_ID"]);
                        ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                        ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
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