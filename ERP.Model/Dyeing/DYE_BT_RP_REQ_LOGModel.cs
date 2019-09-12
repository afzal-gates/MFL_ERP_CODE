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
    public class DYE_BT_RP_REQ_LOGModel
    {
        public Int64 DYE_BT_RP_REQ_LOG_ID { get; set; }
        public DateTime BT_RP_REQ_DT { get; set; }
        public Int64 DYE_STR_REQ_H_ID { get; set; }
        public Int64 LK_RP_SRC_TYPE_ID { get; set; }
        public Int64 DYE_SUG_RP_TYPE_ID { get; set; }
        public string IS_RPROC_DONE { get; set; }
        public Int64 NXT_STR_REQ_H_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_BT_RP_REQ_LOG";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_ID", Value = ob.DYE_BT_RP_REQ_LOG_ID},
                     new CommandParameter() {ParameterName = "pBT_RP_REQ_DT", Value = ob.BT_RP_REQ_DT},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pLK_RP_SRC_TYPE_ID", Value = ob.LK_RP_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_SUG_RP_TYPE_ID", Value = ob.DYE_SUG_RP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_RPROC_DONE", Value = ob.IS_RPROC_DONE},
                     new CommandParameter() {ParameterName = "pNXT_STR_REQ_H_ID", Value = ob.NXT_STR_REQ_H_ID},
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
            const string sp = "SP_DYE_BT_RP_REQ_LOG";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_ID", Value = ob.DYE_BT_RP_REQ_LOG_ID},
                     new CommandParameter() {ParameterName = "pBT_RP_REQ_DT", Value = ob.BT_RP_REQ_DT},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pLK_RP_SRC_TYPE_ID", Value = ob.LK_RP_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_SUG_RP_TYPE_ID", Value = ob.DYE_SUG_RP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_RPROC_DONE", Value = ob.IS_RPROC_DONE},
                     new CommandParameter() {ParameterName = "pNXT_STR_REQ_H_ID", Value = ob.NXT_STR_REQ_H_ID},
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
            const string sp = "SP_DYE_BT_RP_REQ_LOG";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_ID", Value = ob.DYE_BT_RP_REQ_LOG_ID},
                     new CommandParameter() {ParameterName = "pBT_RP_REQ_DT", Value = ob.BT_RP_REQ_DT},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pLK_RP_SRC_TYPE_ID", Value = ob.LK_RP_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDYE_SUG_RP_TYPE_ID", Value = ob.DYE_SUG_RP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_RPROC_DONE", Value = ob.IS_RPROC_DONE},
                     new CommandParameter() {ParameterName = "pNXT_STR_REQ_H_ID", Value = ob.NXT_STR_REQ_H_ID},
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

        public List<DYE_BT_RP_REQ_LOGModel> SelectAll()
        {
            string sp = "Select_DYE_BT_RP_REQ_LOG";
            try
            {
                var obList = new List<DYE_BT_RP_REQ_LOGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            DYE_BT_RP_REQ_LOGModel ob = new DYE_BT_RP_REQ_LOGModel();
                            ob.DYE_BT_RP_REQ_LOG_ID = (dr["DYE_BT_RP_REQ_LOG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_RP_REQ_LOG_ID"]);
                            ob.BT_RP_REQ_DT = (dr["BT_RP_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BT_RP_REQ_DT"]);
                            ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                            ob.LK_RP_SRC_TYPE_ID = (dr["LK_RP_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_SRC_TYPE_ID"]);
                            ob.DYE_SUG_RP_TYPE_ID = (dr["DYE_SUG_RP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SUG_RP_TYPE_ID"]);
                            ob.IS_RPROC_DONE = (dr["IS_RPROC_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RPROC_DONE"]);
                            ob.NXT_STR_REQ_H_ID = (dr["NXT_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NXT_STR_REQ_H_ID"]);
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

        public DYE_BT_RP_REQ_LOGModel Select(long ID)
        {
            string sp = "Select_DYE_BT_RP_REQ_LOG";
            try
            {
                var ob = new DYE_BT_RP_REQ_LOGModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DYE_BT_RP_REQ_LOG_ID = (dr["DYE_BT_RP_REQ_LOG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_RP_REQ_LOG_ID"]);
                        ob.BT_RP_REQ_DT = (dr["BT_RP_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BT_RP_REQ_DT"]);
                        ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                        ob.LK_RP_SRC_TYPE_ID = (dr["LK_RP_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_SRC_TYPE_ID"]);
                        ob.DYE_SUG_RP_TYPE_ID = (dr["DYE_SUG_RP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SUG_RP_TYPE_ID"]);
                        ob.IS_RPROC_DONE = (dr["IS_RPROC_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RPROC_DONE"]);
                        ob.NXT_STR_REQ_H_ID = (dr["NXT_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NXT_STR_REQ_H_ID"]);
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