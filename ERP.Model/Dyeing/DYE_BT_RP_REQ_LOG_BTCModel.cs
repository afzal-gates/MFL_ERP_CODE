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
    public class DYE_BT_RP_REQ_LOG_BTCModel
    {
        public Int64 DYE_BT_RP_REQ_LOG_BTC_ID { get; set; }
        public Int64 DYE_BT_RP_REQ_LOG_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 DF_BT_SUB_LOT_ID { get; set; }
        public Int64 DYE_DOSE_TMPLT_H_ID { get; set; }
        public Int64 RP_BATCH_QTY { get; set; }
        public Int64 ADDL_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 LQR_RATIO { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_BT_RP_REQ_LOG_BTC";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_BTC_ID", Value = ob.DYE_BT_RP_REQ_LOG_BTC_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_ID", Value = ob.DYE_BT_RP_REQ_LOG_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pRP_BATCH_QTY", Value = ob.RP_BATCH_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
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
            const string sp = "SP_DYE_BT_RP_REQ_LOG_BTC";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_BTC_ID", Value = ob.DYE_BT_RP_REQ_LOG_BTC_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_ID", Value = ob.DYE_BT_RP_REQ_LOG_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pRP_BATCH_QTY", Value = ob.RP_BATCH_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
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
            const string sp = "SP_DYE_BT_RP_REQ_LOG_BTC";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_BTC_ID", Value = ob.DYE_BT_RP_REQ_LOG_BTC_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_ID", Value = ob.DYE_BT_RP_REQ_LOG_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pRP_BATCH_QTY", Value = ob.RP_BATCH_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLQR_RATIO", Value = ob.LQR_RATIO},
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

        public List<DYE_BT_RP_REQ_LOG_BTCModel> SelectAll()
        {
            string sp = "Select_DYE_BT_RP_REQ_LOG_BTC";
            try
            {
                var obList = new List<DYE_BT_RP_REQ_LOG_BTCModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_BTC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            DYE_BT_RP_REQ_LOG_BTCModel ob = new DYE_BT_RP_REQ_LOG_BTCModel();
                            ob.DYE_BT_RP_REQ_LOG_BTC_ID = (dr["DYE_BT_RP_REQ_LOG_BTC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_RP_REQ_LOG_BTC_ID"]);
                            ob.DYE_BT_RP_REQ_LOG_ID = (dr["DYE_BT_RP_REQ_LOG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_RP_REQ_LOG_ID"]);
                            ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                            ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                            ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                            ob.RP_BATCH_QTY = (dr["RP_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_BATCH_QTY"]);
                            ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                            ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
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

        public DYE_BT_RP_REQ_LOG_BTCModel Select(long ID)
        {
            string sp = "Select_DYE_BT_RP_REQ_LOG_BTC";
            try
            {
                var ob = new DYE_BT_RP_REQ_LOG_BTCModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_RP_REQ_LOG_BTC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DYE_BT_RP_REQ_LOG_BTC_ID = (dr["DYE_BT_RP_REQ_LOG_BTC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_RP_REQ_LOG_BTC_ID"]);
                        ob.DYE_BT_RP_REQ_LOG_ID = (dr["DYE_BT_RP_REQ_LOG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_RP_REQ_LOG_ID"]);
                        ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                        ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                        ob.DYE_DOSE_TMPLT_H_ID = (dr["DYE_DOSE_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DOSE_TMPLT_H_ID"]);
                        ob.RP_BATCH_QTY = (dr["RP_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_BATCH_QTY"]);
                        ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                        ob.LQR_RATIO = (dr["LQR_RATIO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LQR_RATIO"]);
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