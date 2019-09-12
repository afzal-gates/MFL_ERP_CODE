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
    public class DF_SC_PT_CHL_ISS_D_TRMSModel
    {
        public Int64 DF_SC_PT_CHL_ISS_D_TRMS_ID { get; set; }
        public Int64 DF_SC_PT_CHL_ISS_H_ID { get; set; }
        public Int64 DF_SC_PT_ISS_D1_TRMS_ID { get; set; }
        public Int64 MC_ORD_TRMS_ITEM_ID { get; set; }
        public Int64 ISS_QTY_YDS { get; set; }
        public Int64 ISS_QTY_KG { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_SC_PT_CHL_ISS_D_TRMS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_TRMS_ID", Value = ob.DF_SC_PT_CHL_ISS_D_TRMS_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value = ob.DF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_TRMS_ID", Value = ob.DF_SC_PT_ISS_D1_TRMS_ID},
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_YDS", Value = ob.ISS_QTY_YDS},
                     new CommandParameter() {ParameterName = "pISS_QTY_KG", Value = ob.ISS_QTY_KG},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
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
            const string sp = "SP_DF_SC_PT_CHL_ISS_D_TRMS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_TRMS_ID", Value = ob.DF_SC_PT_CHL_ISS_D_TRMS_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value = ob.DF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_TRMS_ID", Value = ob.DF_SC_PT_ISS_D1_TRMS_ID},
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_YDS", Value = ob.ISS_QTY_YDS},
                     new CommandParameter() {ParameterName = "pISS_QTY_KG", Value = ob.ISS_QTY_KG},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
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
            const string sp = "SP_DF_SC_PT_CHL_ISS_D_TRMS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_TRMS_ID", Value = ob.DF_SC_PT_CHL_ISS_D_TRMS_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value = ob.DF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_TRMS_ID", Value = ob.DF_SC_PT_ISS_D1_TRMS_ID},
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_YDS", Value = ob.ISS_QTY_YDS},
                     new CommandParameter() {ParameterName = "pISS_QTY_KG", Value = ob.ISS_QTY_KG},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
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

        public List<DF_SC_PT_CHL_ISS_D_TRMSModel> SelectAll()
        {
            string sp = "Select_DF_SC_PT_CHL_ISS_D_TRMS";
            try
            {
                var obList = new List<DF_SC_PT_CHL_ISS_D_TRMSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_TRMS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            DF_SC_PT_CHL_ISS_D_TRMSModel ob = new DF_SC_PT_CHL_ISS_D_TRMSModel();
                            ob.DF_SC_PT_CHL_ISS_D_TRMS_ID = (dr["DF_SC_PT_CHL_ISS_D_TRMS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_D_TRMS_ID"]);
                            ob.DF_SC_PT_CHL_ISS_H_ID = (dr["DF_SC_PT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_H_ID"]);
                            ob.DF_SC_PT_ISS_D1_TRMS_ID = (dr["DF_SC_PT_ISS_D1_TRMS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_TRMS_ID"]);
                            ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                            ob.ISS_QTY_YDS = (dr["ISS_QTY_YDS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_YDS"]);
                            ob.ISS_QTY_KG = (dr["ISS_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_KG"]);
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

        public DF_SC_PT_CHL_ISS_D_TRMSModel Select(long ID)
        {
            string sp = "Select_DF_SC_PT_CHL_ISS_D_TRMS";
            try
            {
                var ob = new DF_SC_PT_CHL_ISS_D_TRMSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_TRMS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DF_SC_PT_CHL_ISS_D_TRMS_ID = (dr["DF_SC_PT_CHL_ISS_D_TRMS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_D_TRMS_ID"]);
                        ob.DF_SC_PT_CHL_ISS_H_ID = (dr["DF_SC_PT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_H_ID"]);
                        ob.DF_SC_PT_ISS_D1_TRMS_ID = (dr["DF_SC_PT_ISS_D1_TRMS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_TRMS_ID"]);
                        ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                        ob.ISS_QTY_YDS = (dr["ISS_QTY_YDS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_YDS"]);
                        ob.ISS_QTY_KG = (dr["ISS_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_KG"]);
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