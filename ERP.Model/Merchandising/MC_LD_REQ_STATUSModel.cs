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
    public class MC_LD_REQ_STATUSModel
    {
        public Int64 MC_LD_REQ_STATUS_ID { get; set; }
        public Int64 MC_LD_REQ_D_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime ACTION_DATE { get; set; }
        public Int64 ACTION_BY { get; set; }
        public string IS_VISIBLE { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_LD_REQ_STATUS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_STATUS_ID", Value = ob.MC_LD_REQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pACTION_DATE", Value = ob.ACTION_DATE},
                     new CommandParameter() {ParameterName = "pACTION_BY", Value = ob.ACTION_BY},
                     new CommandParameter() {ParameterName = "pIS_VISIBLE", Value = ob.IS_VISIBLE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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

        public string Update()
        {
            const string sp = "SP_MC_LD_REQ_STATUS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_STATUS_ID", Value = ob.MC_LD_REQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pACTION_DATE", Value = ob.ACTION_DATE},
                     new CommandParameter() {ParameterName = "pACTION_BY", Value = ob.ACTION_BY},
                     new CommandParameter() {ParameterName = "pIS_VISIBLE", Value = ob.IS_VISIBLE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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
            const string sp = "SP_MC_LD_REQ_STATUS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_STATUS_ID", Value = ob.MC_LD_REQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_D_ID", Value = ob.MC_LD_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pACTION_DATE", Value = ob.ACTION_DATE},
                     new CommandParameter() {ParameterName = "pACTION_BY", Value = ob.ACTION_BY},
                     new CommandParameter() {ParameterName = "pIS_VISIBLE", Value = ob.IS_VISIBLE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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

        public List<MC_LD_REQ_STATUSModel> SelectAll()
        {
            string sp = "PKG_Labdip_action.mc_ld_req_status_select";
            try
            {
                var obList = new List<MC_LD_REQ_STATUSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_STATUS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_LD_REQ_STATUSModel ob = new MC_LD_REQ_STATUSModel();
                            ob.MC_LD_REQ_STATUS_ID = (dr["MC_LD_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_STATUS_ID"]);
                            ob.MC_LD_REQ_D_ID = (dr["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_D_ID"]);
                            ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                            ob.ACTION_DATE = (dr["ACTION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTION_DATE"]);
                            ob.ACTION_BY = (dr["ACTION_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACTION_BY"]);
                            ob.IS_VISIBLE = (dr["IS_VISIBLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_VISIBLE"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                            ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
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

        public MC_LD_REQ_STATUSModel Select(long ID)
        {
            string sp = "Select_MC_LD_REQ_STATUS";
            try
            {
                var ob = new MC_LD_REQ_STATUSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_STATUS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_LD_REQ_STATUS_ID = (dr["MC_LD_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_STATUS_ID"]);
                        ob.MC_LD_REQ_D_ID = (dr["MC_LD_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_REQ_D_ID"]);
                        ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                        ob.ACTION_DATE = (dr["ACTION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTION_DATE"]);
                        ob.ACTION_BY = (dr["ACTION_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACTION_BY"]);
                        ob.IS_VISIBLE = (dr["IS_VISIBLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_VISIBLE"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
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