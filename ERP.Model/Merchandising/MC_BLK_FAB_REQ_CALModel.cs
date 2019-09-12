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
    public class MC_BLK_FAB_REQ_CALModel
    {
        public Int64? MC_BLK_FAB_REQ_CAL_ID { get; set; }
        public Int64? MC_BLK_FAB_REQ_D_ID { get; set; }
        public Int64? FAB_COLOR_ID { get; set; }
        public Int64? MC_SIZE_ID { get; set; }
        public Int64? FIN_FAB_QTY { get; set; }
        public Decimal? PLOSS_QTY { get; set; }
        public Int64? GREY_FAB_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        //public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        //public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? GMT_QTY { get; set; }


        public string ITEM_CAT_NAME_EN { get; set; }
        public string FAB_COLOR_NAME { get; set; }
        public string GMT_PART_NAME { get; set; }
        public string SIZE_RANGE { get; set; }
        public string FIN_DIA_DESC { get; set; }
        public string COLOR_GRP_NAME_EN { get; set; }
        

        public string Save()
        {
            const string sp = "SP_MC_BLK_FAB_REQ_CAL";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_CAL_ID", Value = ob.MC_BLK_FAB_REQ_CAL_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_D_ID", Value = ob.MC_BLK_FAB_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pFIN_FAB_QTY", Value = ob.FIN_FAB_QTY},
                     new CommandParameter() {ParameterName = "pPLOSS_QTY", Value = ob.PLOSS_QTY},
                     new CommandParameter() {ParameterName = "pGREY_FAB_QTY", Value = ob.GREY_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},                     
                     new CommandParameter() {ParameterName = "pGMT_QTY", Value = ob.GMT_QTY},
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
            const string sp = "SP_MC_BLK_FAB_REQ_CAL";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_CAL_ID", Value = ob.MC_BLK_FAB_REQ_CAL_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_D_ID", Value = ob.MC_BLK_FAB_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pFIN_FAB_QTY", Value = ob.FIN_FAB_QTY},
                     new CommandParameter() {ParameterName = "pPLOSS_QTY", Value = ob.PLOSS_QTY},
                     new CommandParameter() {ParameterName = "pGREY_FAB_QTY", Value = ob.GREY_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},                    
                     new CommandParameter() {ParameterName = "pGMT_QTY", Value = ob.GMT_QTY},
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
            const string sp = "SP_MC_BLK_FAB_REQ_CAL";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_CAL_ID", Value = ob.MC_BLK_FAB_REQ_CAL_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_D_ID", Value = ob.MC_BLK_FAB_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pFIN_FAB_QTY", Value = ob.FIN_FAB_QTY},
                     new CommandParameter() {ParameterName = "pPLOSS_QTY", Value = ob.PLOSS_QTY},
                     new CommandParameter() {ParameterName = "pGREY_FAB_QTY", Value = ob.GREY_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                    
                     new CommandParameter() {ParameterName = "pGMT_QTY", Value = ob.GMT_QTY},
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

        public List<MC_BLK_FAB_REQ_CALModel> BulkFabProcessConsList(Int64? pMC_BLK_FAB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_cal_select";
            try
            {
                var obList = new List<MC_BLK_FAB_REQ_CALModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REQ_CALModel ob = new MC_BLK_FAB_REQ_CALModel();
                    ob.MC_BLK_FAB_REQ_CAL_ID = (dr["MC_BLK_FAB_REQ_CAL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_CAL_ID"]);
                    //ob.MC_BLK_FAB_REQ_D_ID = (dr["MC_BLK_FAB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_D_ID"]);

                    //if (dr["FAB_COLOR_ID"] != DBNull.Value)
                    //    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);

                    //if (dr["MC_SIZE_ID"] != DBNull.Value)
                    //    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);

                    ob.FIN_FAB_QTY = (dr["FIN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_FAB_QTY"]);
                    //ob.PLOSS_QTY = (dr["PLOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLOSS_QTY"]);
                    ob.GREY_FAB_QTY = (dr["GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GREY_FAB_QTY"]);

                    //if (dr["QTY_MOU_ID"] != DBNull.Value)
                    //    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    //ob.GMT_QTY = (dr["GMT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_QTY"]);

                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.FAB_COLOR_NAME = (dr["FAB_COLOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_COLOR_NAME"]);
                    ob.GMT_PART_NAME = (dr["GMT_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_PART_NAME"]);
                    ob.SIZE_RANGE = (dr["SIZE_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_RANGE"]);                    
                    ob.FIN_DIA_DESC = (dr["FIN_DIA_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_DESC"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_BLK_FAB_REQ_CALModel> SelectAll()
        {
            string sp = "Select_MC_BLK_FAB_REQ_CAL";
            try
            {
                var obList = new List<MC_BLK_FAB_REQ_CALModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_CAL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_BLK_FAB_REQ_CALModel ob = new MC_BLK_FAB_REQ_CALModel();
                            ob.MC_BLK_FAB_REQ_CAL_ID = (dr["MC_BLK_FAB_REQ_CAL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_CAL_ID"]);
                            ob.MC_BLK_FAB_REQ_D_ID = (dr["MC_BLK_FAB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_D_ID"]);
                            ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                            ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                            ob.FIN_FAB_QTY = (dr["FIN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_FAB_QTY"]);
                            ob.PLOSS_QTY = (dr["PLOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLOSS_QTY"]);
                            ob.GREY_FAB_QTY = (dr["GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GREY_FAB_QTY"]);

                            if (dr["QTY_MOU_ID"] != DBNull.Value)
                                ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                            
                            ob.GMT_QTY = (dr["GMT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_QTY"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BLK_FAB_REQ_CALModel Select(long ID)
        {
            string sp = "Select_MC_BLK_FAB_REQ_CAL";
            try
            {
                var ob = new MC_BLK_FAB_REQ_CALModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_CAL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_BLK_FAB_REQ_CAL_ID = (dr["MC_BLK_FAB_REQ_CAL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_CAL_ID"]);
                        ob.MC_BLK_FAB_REQ_D_ID = (dr["MC_BLK_FAB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_D_ID"]);
                        ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                        ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                        ob.FIN_FAB_QTY = (dr["FIN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_FAB_QTY"]);
                        ob.PLOSS_QTY = (dr["PLOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLOSS_QTY"]);
                        ob.GREY_FAB_QTY = (dr["GREY_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GREY_FAB_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                      
                        ob.GMT_QTY = (dr["GMT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_QTY"]);

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