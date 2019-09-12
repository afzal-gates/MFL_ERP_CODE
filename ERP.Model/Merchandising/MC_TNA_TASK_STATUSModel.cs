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
    public class MC_TNA_TASK_STATUSModel
    {
        public Int64 MC_TNA_TASK_STATUS_ID { get; set; }
        public Int64 MC_TNA_TASK_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string EVENT_NAME { get; set; }
        public string TASK_STATUS_NAME { get; set; }
        public Int64 STATUS_ORDER { get; set; }
        public string IS_REPEAT { get; set; }
        public string ST_END_FLAG { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? PARENT_ID { get; set; }
        public string IS_FB_FRM_BUYER { get; set; }
        
        public string Save()
        {
            const string sp = "pkg_tna.mc_tna_task_status_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = ob.MC_TNA_TASK_ID},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value = ob.EVENT_NAME},
                     new CommandParameter() {ParameterName = "pTASK_STATUS_NAME", Value = ob.TASK_STATUS_NAME},
                     new CommandParameter() {ParameterName = "pSTATUS_ORDER", Value = ob.STATUS_ORDER},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},                     
                     new CommandParameter() {ParameterName = "pST_END_FLAG", Value = ob.ST_END_FLAG},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pIS_FB_FRM_BUYER", Value = ob.IS_FB_FRM_BUYER},
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
            const string sp = "SP_MC_TNA_TASK_STATUS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = ob.MC_TNA_TASK_ID},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value = ob.EVENT_NAME},
                     new CommandParameter() {ParameterName = "pTASK_STATUS_NAME", Value = ob.TASK_STATUS_NAME},
                     new CommandParameter() {ParameterName = "pSTATUS_ORDER", Value = ob.STATUS_ORDER},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pST_END_FLAG", Value = ob.ST_END_FLAG},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pIS_FB_FRM_BUYER", Value = ob.IS_FB_FRM_BUYER},
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
            const string sp = "SP_MC_TNA_TASK_STATUS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = ob.MC_TNA_TASK_ID},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value = ob.EVENT_NAME},
                     new CommandParameter() {ParameterName = "pTASK_STATUS_NAME", Value = ob.TASK_STATUS_NAME},
                     new CommandParameter() {ParameterName = "pSTATUS_ORDER", Value = ob.STATUS_ORDER},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pST_END_FLAG", Value = ob.ST_END_FLAG},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pIS_FB_FRM_BUYER", Value = ob.IS_FB_FRM_BUYER},
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

        public List<MC_TNA_TASK_STATUSModel> SelectAll()
        {
            string sp = "Select_MC_TNA_TASK_STATUS";
            try
            {
                var obList = new List<MC_TNA_TASK_STATUSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_TNA_TASK_STATUSModel ob = new MC_TNA_TASK_STATUSModel();
                            ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                            ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                            ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                            ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                            ob.STATUS_ORDER = (dr["STATUS_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STATUS_ORDER"]);
                            ob.IS_REPEAT = (dr["IS_REPEAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPEAT"]);
                            ob.ST_END_FLAG = (dr["ST_END_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ST_END_FLAG"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                            ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                            ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                            ob.IS_FB_FRM_BUYER = (dr["IS_FB_FRM_BUYER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FB_FRM_BUYER"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TASK_STATUSModel> SelectByTnaID(Int64? ID)
        {
            string sp = "pkg_tna.mc_tna_task_status_select";
            try
            {
                var obList = new List<MC_TNA_TASK_STATUSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TASK_STATUSModel ob = new MC_TNA_TASK_STATUSModel();
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.STATUS_ORDER = (dr["STATUS_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STATUS_ORDER"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);                    
                    ob.IS_REPEAT = (dr["IS_REPEAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPEAT"]);
                    ob.ST_END_FLAG = (dr["ST_END_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ST_END_FLAG"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_FB_FRM_BUYER = (dr["IS_FB_FRM_BUYER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FB_FRM_BUYER"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_TNA_TASK_STATUSModel Select(long ID)
        {
            string sp = "PKG_TNA.Select_MC_TNA_TASK_STATUS";
            try
            {
                var ob = new MC_TNA_TASK_STATUSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                        ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                        ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                        ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                        ob.STATUS_ORDER = (dr["STATUS_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STATUS_ORDER"]);
                        ob.IS_REPEAT = (dr["IS_REPEAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPEAT"]);
                        ob.ST_END_FLAG = (dr["ST_END_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ST_END_FLAG"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                        ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                        ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                        ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                        ob.IS_FB_FRM_BUYER = (dr["IS_FB_FRM_BUYER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FB_FRM_BUYER"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TNA_TASK_STATUSModel> SelectApprovRejectStatus(int? pMC_TNA_TASK_ID = null, int? pPARENT_ID = null, string pIS_FB_FRM_BUYER = null,int? pHR_DEPARTMENT_ID=null)
        {
            string sp = "PKG_TNA.mc_tna_task_status_select";
            try
            {
                var obList = new List<MC_TNA_TASK_STATUSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value =pMC_TNA_TASK_ID},
                    new CommandParameter() {ParameterName = "pPARENT_ID", Value = pPARENT_ID},
                    new CommandParameter() {ParameterName = "pIS_FB_FRM_BUYER", Value = pIS_FB_FRM_BUYER},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value =0},
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TNA_TASK_STATUSModel ob = new MC_TNA_TASK_STATUSModel();
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.MC_TNA_TASK_ID = (dr["MC_TNA_TASK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_ID"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.TASK_STATUS_NAME = (dr["TASK_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TASK_STATUS_NAME"]);
                    ob.STATUS_ORDER = (dr["STATUS_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STATUS_ORDER"]);
                    ob.IS_REPEAT = (dr["IS_REPEAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPEAT"]);
                    ob.ST_END_FLAG = (dr["ST_END_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ST_END_FLAG"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_FB_FRM_BUYER = (dr["IS_FB_FRM_BUYER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FB_FRM_BUYER"]);
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