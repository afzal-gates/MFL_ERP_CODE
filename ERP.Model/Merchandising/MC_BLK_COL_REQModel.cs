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
    public class MC_BLK_COL_REQModel
    {
        public Int64? MC_BLK_COL_REQ_ID { get; set; }
        public Int64 MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64? RF_GARM_PART_ID { get; set; }
        public Int64 MC_SIZE_ID { get; set; }
        public string SIZE_CODE { get; set; }
        public string MEASUREMENT { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        //public Int64 LAST_UPDATED_BY { get; set; }
        public string MC_BLK_COL_REQ_XML { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_BLK_COL_REQ";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_COL_REQ_ID", Value = ob.MC_BLK_COL_REQ_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_MC_BLK_COL_REQ";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_COL_REQ_ID", Value = ob.MC_BLK_COL_REQ_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_MC_BLK_COL_REQ";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_COL_REQ_ID", Value = ob.MC_BLK_COL_REQ_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<MC_BLK_COL_REQModel> SelectAll()
        {
            string sp = "Select_MC_BLK_COL_REQ";
            try
            {
                var obList = new List<MC_BLK_COL_REQModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_COL_REQ_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_BLK_COL_REQModel ob = new MC_BLK_COL_REQModel();
                            ob.MC_BLK_COL_REQ_ID = (dr["MC_BLK_COL_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_COL_REQ_ID"]);
                            ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                            ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                            ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                            ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                            //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                            ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                            //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                            //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BLK_COL_REQModel Select(long ID)
        {
            string sp = "Select_MC_BLK_COL_REQ";
            try
            {
                var ob = new MC_BLK_COL_REQModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_COL_REQ_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_BLK_COL_REQ_ID = (dr["MC_BLK_COL_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_COL_REQ_ID"]);
                        ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                        ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                        ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                        ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                        //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                        ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                        //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                        //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object CollarCuffMeasList(long pMC_BLK_FAB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_col_req_select";
            try
            {
                var obList = new List<MC_BLK_COL_REQModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_COL_REQModel ob = new MC_BLK_COL_REQModel();
                    ob.MC_BLK_COL_REQ_ID = (dr["MC_BLK_COL_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_COL_REQ_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    if (dr["RF_GARM_PART_ID"] != DBNull.Value)
                        ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);

                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string BatchSaveBulkFabCollarCuff()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_col_req_save";
            string jsonStr = "{";
            var ob = this;
            //ob.MC_BLK_COL_REQ_XML = pMC_BLK_COL_REQ_XML;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pMC_BLK_COL_REQ_ID", Value = ob.MC_BLK_COL_REQ_ID},
                     //new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     //new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                     //new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     //new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMC_BLK_COL_REQ_XML", Value = ob.MC_BLK_COL_REQ_XML},
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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