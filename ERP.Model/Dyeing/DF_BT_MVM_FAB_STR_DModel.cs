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
    public class DF_BT_MVM_FAB_STR_DModel
    {
        public Int64 DF_BT_MVM_FAB_STR_D_ID { get; set; }
        public Int64 DF_BT_MVM_FAB_STR_H_ID { get; set; }
        public DateTime ISSUE_DT { get; set; }
        public Int64 DELV_NO_ROLL { get; set; }
        public Decimal DELV_FAB_QTY { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_BT_MVM_FAB_STR_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_FAB_STR_D_ID", Value = ob.DF_BT_MVM_FAB_STR_D_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_FAB_STR_H_ID", Value = ob.DF_BT_MVM_FAB_STR_H_ID},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pDELV_NO_ROLL", Value = ob.DELV_NO_ROLL},
                     new CommandParameter() {ParameterName = "pDELV_FAB_QTY", Value = ob.DELV_FAB_QTY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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
            const string sp = "SP_DF_BT_MVM_FAB_STR_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_FAB_STR_D_ID", Value = ob.DF_BT_MVM_FAB_STR_D_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_FAB_STR_H_ID", Value = ob.DF_BT_MVM_FAB_STR_H_ID},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pDELV_NO_ROLL", Value = ob.DELV_NO_ROLL},
                     new CommandParameter() {ParameterName = "pDELV_FAB_QTY", Value = ob.DELV_FAB_QTY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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
            const string sp = "SP_DF_BT_MVM_FAB_STR_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_FAB_STR_D_ID", Value = ob.DF_BT_MVM_FAB_STR_D_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_FAB_STR_H_ID", Value = ob.DF_BT_MVM_FAB_STR_H_ID},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pDELV_NO_ROLL", Value = ob.DELV_NO_ROLL},
                     new CommandParameter() {ParameterName = "pDELV_FAB_QTY", Value = ob.DELV_FAB_QTY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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

        public List<DF_BT_MVM_FAB_STR_DModel> SelectAll()
        {
            string sp = "Select_DF_BT_MVM_FAB_STR_D";
            try
            {
                var obList = new List<DF_BT_MVM_FAB_STR_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_FAB_STR_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            DF_BT_MVM_FAB_STR_DModel ob = new DF_BT_MVM_FAB_STR_DModel();
                            ob.DF_BT_MVM_FAB_STR_D_ID = (dr["DF_BT_MVM_FAB_STR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_MVM_FAB_STR_D_ID"]);
                            ob.DF_BT_MVM_FAB_STR_H_ID = (dr["DF_BT_MVM_FAB_STR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_MVM_FAB_STR_H_ID"]);
                            ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                            ob.DELV_NO_ROLL = (dr["DELV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_NO_ROLL"]);
                            ob.DELV_FAB_QTY = (dr["DELV_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_FAB_QTY"]);
                            ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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

        public DF_BT_MVM_FAB_STR_DModel Select(long ID)
        {
            string sp = "Select_DF_BT_MVM_FAB_STR_D";
            try
            {
                var ob = new DF_BT_MVM_FAB_STR_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_FAB_STR_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DF_BT_MVM_FAB_STR_D_ID = (dr["DF_BT_MVM_FAB_STR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_MVM_FAB_STR_D_ID"]);
                        ob.DF_BT_MVM_FAB_STR_H_ID = (dr["DF_BT_MVM_FAB_STR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_MVM_FAB_STR_H_ID"]);
                        ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                        ob.DELV_NO_ROLL = (dr["DELV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_NO_ROLL"]);
                        ob.DELV_FAB_QTY = (dr["DELV_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_FAB_QTY"]);
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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