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
    public class RF_YRN_CNTModel
    {
        public Int64 RF_YRN_CNT_ID { get; set; }
        public string YR_COUNT_NO { get; set; }
        public string IS_SINGLE { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public String YR_CNT_SYS { get; set; }
        public String YR_COUNT_DESC { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.rf_yrn_cnt_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pYR_COUNT_NO", Value = ob.YR_COUNT_NO},
                     new CommandParameter() {ParameterName = "pIS_SINGLE", Value = ob.IS_SINGLE},
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

        public string Update()
        {
            const string sp = "pkg_common.rf_yrn_cnt_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pYR_COUNT_NO", Value = ob.YR_COUNT_NO},
                     new CommandParameter() {ParameterName = "pIS_SINGLE", Value = ob.IS_SINGLE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "pkg_common.rf_yrn_cnt_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
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

        public List<RF_YRN_CNTModel> SelectAll()
        {
            string sp = "pkg_common.rf_yrn_cnt_select";
            try
            {
                var obList = new List<RF_YRN_CNTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            RF_YRN_CNTModel ob = new RF_YRN_CNTModel();
                            ob.RF_YRN_CNT_ID = (dr["RF_YRN_CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                            ob.YR_COUNT_NO = (dr["YR_COUNT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_NO"]);
                            ob.IS_SINGLE = (dr["IS_SINGLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SINGLE"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            ob.YR_CNT_SYS = (dr["YR_CNT_SYS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_CNT_SYS"]);
                            ob.YR_COUNT_DESC = (dr["YR_COUNT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_DESC"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_YRN_CNTModel Select(long ID)
        {
            string sp = "pkg_common.rf_yrn_cnt_insert";
            try
            {
                var ob = new RF_YRN_CNTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.RF_YRN_CNT_ID = (dr["RF_YRN_CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                        ob.YR_COUNT_NO = (dr["YR_COUNT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_NO"]);
                        ob.IS_SINGLE = (dr["IS_SINGLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SINGLE"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                        ob.YR_CNT_SYS = (dr["YR_CNT_SYS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_CNT_SYS"]);
                        ob.YR_COUNT_DESC = (dr["YR_COUNT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_DESC"]);
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