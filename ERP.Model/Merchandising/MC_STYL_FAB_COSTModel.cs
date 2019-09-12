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
    public class MC_STYL_FAB_COSTModel
    {
        public Int64 MC_STYL_FAB_COST_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public string COMBO_NO { get; set; }
        public string GARM_PART { get; set; }
        public Int64 LK_FBR_GRP_ID { get; set; }
        public string FABRIC_DESC { get; set; }
        public Decimal FABRIC_CONS { get; set; }
        public Decimal FABRIC_RATE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "pkg_merchandising.mc_styl_fab_cost_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_FAB_COST_ID", Value = ob.MC_STYL_FAB_COST_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob.COMBO_NO},
                     new CommandParameter() {ParameterName = "pGARM_PART", Value = ob.GARM_PART},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pFABRIC_CONS", Value = ob.FABRIC_CONS},
                     new CommandParameter() {ParameterName = "pFABRIC_RATE", Value = ob.FABRIC_RATE},
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
            const string sp = "pkg_merchandising.mc_styl_fab_cost_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_FAB_COST_ID", Value = ob.MC_STYL_FAB_COST_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob.COMBO_NO},
                     new CommandParameter() {ParameterName = "pGARM_PART", Value = ob.GARM_PART},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pFABRIC_CONS", Value = ob.FABRIC_CONS},
                     new CommandParameter() {ParameterName = "pFABRIC_RATE", Value = ob.FABRIC_RATE},
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
            const string sp = "pkg_merchandising.mc_styl_fab_cost_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_FAB_COST_ID", Value = ob.MC_STYL_FAB_COST_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob.COMBO_NO},
                     new CommandParameter() {ParameterName = "pGARM_PART", Value = ob.GARM_PART},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pFABRIC_CONS", Value = ob.FABRIC_CONS},
                     new CommandParameter() {ParameterName = "pFABRIC_RATE", Value = ob.FABRIC_RATE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<MC_STYL_FAB_COSTModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_styl_fab_cost_select";
            try
            {
                var obList = new List<MC_STYL_FAB_COSTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_FAB_COST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_STYL_FAB_COSTModel ob = new MC_STYL_FAB_COSTModel();
                            ob.MC_STYL_FAB_COST_ID = (dr["MC_STYL_FAB_COST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_FAB_COST_ID"]);
                            ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                            ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                            ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);
                            ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                            ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                            ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                            ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYL_FAB_COSTModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_styl_fab_cost_select";
            try
            {
                var ob = new MC_STYL_FAB_COSTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_FAB_COST_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_STYL_FAB_COST_ID = (dr["MC_STYL_FAB_COST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_FAB_COST_ID"]);
                        ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                        ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                        ob.GARM_PART = (dr["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART"]);
                        ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                        ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                        ob.FABRIC_CONS = (dr["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_CONS"]);
                        ob.FABRIC_RATE = (dr["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FABRIC_RATE"]);
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