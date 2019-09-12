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
    public class MC_STYL_BGT_COSTModel
    {
        public Int64 MC_STYL_BGT_COST_ID { get; set; }
        public Int64 MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64 MC_COST_HEAD_ID { get; set; }
        public string IS_PCT { get; set; }
        public Decimal PCT_COST { get; set; }
        public Decimal HEAD_COST { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public String XML {get;set;}
        public Int64 MC_STYL_BGT_H_ID { get; set; }

        public string IS_BGT_FINALIZED { get; set; }

        public string Save()
        {
            const string sp = "pkg_budget_sheet.mc_styl_bgt_cost_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pIS_BGT_FINALIZED", Value = ob.IS_BGT_FINALIZED},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = ob.MC_STYL_BGT_H_ID},
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
        public List<MC_STYL_BGT_COSTModel> SelectAll()
        {
            string sp = "Select_MC_STYL_BGT_COST";
            try
            {
                var obList = new List<MC_STYL_BGT_COSTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_COST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_STYL_BGT_COSTModel ob = new MC_STYL_BGT_COSTModel();
                            ob.MC_STYL_BGT_COST_ID = (dr["MC_STYL_BGT_COST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_COST_ID"]);
                            ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                            ob.MC_COST_HEAD_ID = (dr["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COST_HEAD_ID"]);
                            ob.IS_PCT = (dr["IS_PCT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PCT"]);
                            ob.PCT_COST = (dr["PCT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_COST"]);
                            ob.HEAD_COST = (dr["HEAD_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["HEAD_COST"]);
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

        public MC_STYL_BGT_COSTModel Select(long ID)
        {
            string sp = "Select_MC_STYL_BGT_COST";
            try
            {
                var ob = new MC_STYL_BGT_COSTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_COST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_STYL_BGT_COST_ID = (dr["MC_STYL_BGT_COST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_COST_ID"]);
                        ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                        ob.MC_COST_HEAD_ID = (dr["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COST_HEAD_ID"]);
                        ob.IS_PCT = (dr["IS_PCT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PCT"]);
                        ob.PCT_COST = (dr["PCT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_COST"]);
                        ob.HEAD_COST = (dr["HEAD_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["HEAD_COST"]);
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