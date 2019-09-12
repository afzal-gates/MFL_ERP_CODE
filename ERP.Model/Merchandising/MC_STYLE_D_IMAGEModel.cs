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
    public class MC_STYLE_D_IMAGEModel
    {
        public Int64 MC_STYLE_D_IMAGE_ID { get; set; }
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public string STYL_IMAGE { get; set; }
        public string STYL_IMG_DESC { get; set; }
        public Int64 IMG_SEQ { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_STYLE_D_IMAGE";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_IMAGE_ID", Value = ob.MC_STYLE_D_IMAGE_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSTYL_IMAGE", Value = ob.STYL_IMAGE},
                     new CommandParameter() {ParameterName = "pSTYL_IMG_DESC", Value = ob.STYL_IMG_DESC},
                     new CommandParameter() {ParameterName = "pIMG_SEQ", Value = ob.IMG_SEQ},
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
            const string sp = "SP_MC_STYLE_D_IMAGE";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_IMAGE_ID", Value = ob.MC_STYLE_D_IMAGE_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSTYL_IMAGE", Value = ob.STYL_IMAGE},
                     new CommandParameter() {ParameterName = "pSTYL_IMG_DESC", Value = ob.STYL_IMG_DESC},
                     new CommandParameter() {ParameterName = "pIMG_SEQ", Value = ob.IMG_SEQ},
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
            const string sp = "SP_MC_STYLE_D_IMAGE";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_IMAGE_ID", Value = ob.MC_STYLE_D_IMAGE_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSTYL_IMAGE", Value = ob.STYL_IMAGE},
                     new CommandParameter() {ParameterName = "pSTYL_IMG_DESC", Value = ob.STYL_IMG_DESC},
                     new CommandParameter() {ParameterName = "pIMG_SEQ", Value = ob.IMG_SEQ},
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

        public List<MC_STYLE_D_IMAGEModel> SelectAll()
        {
            string sp = "Select_MC_STYLE_D_IMAGE";
            try
            {
                var obList = new List<MC_STYLE_D_IMAGEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_IMAGE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_STYLE_D_IMAGEModel ob = new MC_STYLE_D_IMAGEModel();
                            ob.MC_STYLE_D_IMAGE_ID = (dr["MC_STYLE_D_IMAGE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_IMAGE_ID"]);
                            ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                            ob.STYL_IMAGE = (dr["STYL_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_IMAGE"]);
                            ob.STYL_IMG_DESC = (dr["STYL_IMG_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_IMG_DESC"]);
                            ob.IMG_SEQ = (dr["IMG_SEQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IMG_SEQ"]);
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

        public MC_STYLE_D_IMAGEModel Select(long ID)
        {
            string sp = "Select_MC_STYLE_D_IMAGE";
            try
            {
                var ob = new MC_STYLE_D_IMAGEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_IMAGE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_STYLE_D_IMAGE_ID = (dr["MC_STYLE_D_IMAGE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_IMAGE_ID"]);
                        ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                        ob.STYL_IMAGE = (dr["STYL_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_IMAGE"]);
                        ob.STYL_IMG_DESC = (dr["STYL_IMG_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_IMG_DESC"]);
                        ob.IMG_SEQ = (dr["IMG_SEQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IMG_SEQ"]);
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