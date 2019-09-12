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
    public class MC_BLK_FAB_PLOSSModel
    {
        public Int64 MC_BLK_FAB_PLOSS_ID { get; set; }
        public Int64 MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public Decimal PLOSS_PCT { get; set; }
        public Decimal? PLOSS_PCT_FKNIT { get; set; }
        public Decimal? YD_PLOSS_PCT { get; set; }
        public Decimal? AOP_PLOSS_PCT { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string COLOR_NAME_EN { get; set; }
        public string MC_BLK_FAB_PLOSS_XML { get; set; }
            

        public string Save()
        {
            const string sp = "SP_MC_BLK_FAB_PLOSS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_PLOSS_ID", Value = ob.MC_BLK_FAB_PLOSS_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pPLOSS_PCT", Value = ob.PLOSS_PCT},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     //new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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
            const string sp = "SP_MC_BLK_FAB_PLOSS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_PLOSS_ID", Value = ob.MC_BLK_FAB_PLOSS_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pPLOSS_PCT", Value = ob.PLOSS_PCT},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     //new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     //new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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
            const string sp = "SP_MC_BLK_FAB_PLOSS";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_PLOSS_ID", Value = ob.MC_BLK_FAB_PLOSS_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pPLOSS_PCT", Value = ob.PLOSS_PCT},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     //new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     //new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
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

        public List<MC_BLK_FAB_PLOSSModel> SelectAll()
        {
            string sp = "Select_MC_BLK_FAB_PLOSS";
            try
            {
                var obList = new List<MC_BLK_FAB_PLOSSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_PLOSS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_BLK_FAB_PLOSSModel ob = new MC_BLK_FAB_PLOSSModel();
                            ob.MC_BLK_FAB_PLOSS_ID = (dr["MC_BLK_FAB_PLOSS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_PLOSS_ID"]);
                            ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                            ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                            ob.PLOSS_PCT = (dr["PLOSS_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLOSS_PCT"]);
                            //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                            //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                            //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                            //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                            //ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BLK_FAB_PLOSSModel Select(long ID)
        {
            string sp = "Select_MC_BLK_FAB_PLOSS";
            try
            {
                var ob = new MC_BLK_FAB_PLOSSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_PLOSS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_BLK_FAB_PLOSS_ID = (dr["MC_BLK_FAB_PLOSS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_PLOSS_ID"]);
                        ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                        ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                        ob.PLOSS_PCT = (dr["PLOSS_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLOSS_PCT"]);
                        //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                        //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                        //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                        //ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_BLK_FAB_PLOSSModel> ProcessLossColorList(long pMC_BLK_FAB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_ploss_select";
            try
            {
                var obList = new List<MC_BLK_FAB_PLOSSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_PLOSSModel ob = new MC_BLK_FAB_PLOSSModel();

                    ob.MC_BLK_FAB_PLOSS_ID = (dr["MC_BLK_FAB_PLOSS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_PLOSS_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    if (dr["PLOSS_PCT"] != DBNull.Value)
                    { ob.PLOSS_PCT = (dr["PLOSS_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLOSS_PCT"]); }
                    if (dr["PLOSS_PCT_FKNIT"] != DBNull.Value)
                    { ob.PLOSS_PCT_FKNIT = (dr["PLOSS_PCT_FKNIT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLOSS_PCT_FKNIT"]); }
                    if (dr["YD_PLOSS_PCT"] != DBNull.Value)
                    { ob.YD_PLOSS_PCT = (dr["YD_PLOSS_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["YD_PLOSS_PCT"]); }
                    if (dr["AOP_PLOSS_PCT"] != DBNull.Value)
                    { ob.AOP_PLOSS_PCT = (dr["AOP_PLOSS_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AOP_PLOSS_PCT"]); }
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                                        
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string BatchSaveBulkFabPL()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_fab_ploss_save";
            string jsonStr = "{";
            var ob = this;
            ob.MC_BLK_FAB_PLOSS_XML = ob.MC_BLK_FAB_PLOSS_XML.Replace("null","");
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     //new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_PLOSS_XML", Value = ob.MC_BLK_FAB_PLOSS_XML},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}