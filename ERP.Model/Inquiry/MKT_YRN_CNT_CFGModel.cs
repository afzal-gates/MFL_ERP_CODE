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
    public class MKT_YRN_CNT_CFGModel
    {
        public Int64 MKT_YRN_CNT_CFG_ID { get; set; }
        public Int64 LK_FAB_TYPE_ID { get; set; }
        public Int64 GSM_LOW { get; set; }
        public Int64 GSM_HIGH { get; set; }
        public string RF_YRN_CNT_LST { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp ="pkg_name.mkt_yrn_cnt_cfg_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMKT_YRN_CNT_CFG_ID", Value = ob.MKT_YRN_CNT_CFG_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_TYPE_ID", Value = ob.LK_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pGSM_LOW", Value = ob.GSM_LOW},
                     new CommandParameter() {ParameterName = "pGSM_HIGH", Value = ob.GSM_HIGH},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_LST", Value = ob.RF_YRN_CNT_LST},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_MKT_YRN_CNT_CFG_ID", Value =0, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    // jsonString += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
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

        public string FindSuggestedYarnCount(int pGSM, Int32 pLK_FAB_TYPE_ID)
        {
            string sp = "pkg_inquiry.mkt_inqr_quot_select";
            //pOption=3000=>Select All Data
            try
            {
                var data = "";
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGSM", Value = pGSM},
                     new CommandParameter() {ParameterName = "pLK_FAB_TYPE_ID", Value = pLK_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
               foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   data = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]); ;
               }
              return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}