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
    public class DF_FSTR_REQ_DModel
    {
        public Int64 DF_FSTR_REQ_D_ID { get; set; }
        public Int64 DF_FSTR_REQ_H_ID { get; set; }
        public Int64 MC_ORDER_SHIP_ID { get; set; }
        public Int64 REQ_ROLL_QTY { get; set; }
        public Int64 REQ_FAB_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_FSTR_REQ_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FSTR_REQ_D_ID", Value = ob.DF_FSTR_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pDF_FSTR_REQ_H_ID", Value = ob.DF_FSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = ob.MC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pREQ_ROLL_QTY", Value = ob.REQ_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pREQ_FAB_QTY", Value = ob.REQ_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_FSTR_REQ_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FSTR_REQ_D_ID", Value = ob.DF_FSTR_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pDF_FSTR_REQ_H_ID", Value = ob.DF_FSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = ob.MC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pREQ_ROLL_QTY", Value = ob.REQ_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pREQ_FAB_QTY", Value = ob.REQ_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_FSTR_REQ_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FSTR_REQ_D_ID", Value = ob.DF_FSTR_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pDF_FSTR_REQ_H_ID", Value = ob.DF_FSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = ob.MC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pREQ_ROLL_QTY", Value = ob.REQ_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pREQ_FAB_QTY", Value = ob.REQ_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<DF_FSTR_REQ_DModel> SelectAll()
        {
            string sp = "Select_DF_FSTR_REQ_D";
            try
            {
                var obList = new List<DF_FSTR_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FSTR_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            DF_FSTR_REQ_DModel ob = new DF_FSTR_REQ_DModel();
                            ob.DF_FSTR_REQ_D_ID = (dr["DF_FSTR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FSTR_REQ_D_ID"]);
                            ob.DF_FSTR_REQ_H_ID = (dr["DF_FSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FSTR_REQ_H_ID"]);
                            ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                            ob.REQ_ROLL_QTY = (dr["REQ_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_ROLL_QTY"]);
                            ob.REQ_FAB_QTY = (dr["REQ_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_FAB_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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

        public DF_FSTR_REQ_DModel Select(long ID)
        {
            string sp = "Select_DF_FSTR_REQ_D";
            try
            {
                var ob = new DF_FSTR_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FSTR_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DF_FSTR_REQ_D_ID = (dr["DF_FSTR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FSTR_REQ_D_ID"]);
                        ob.DF_FSTR_REQ_H_ID = (dr["DF_FSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FSTR_REQ_H_ID"]);
                        ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                        ob.REQ_ROLL_QTY = (dr["REQ_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_ROLL_QTY"]);
                        ob.REQ_FAB_QTY = (dr["REQ_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_FAB_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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