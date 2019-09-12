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
    public class MC_STYLEModel
    {
        public Int64 MC_STYLE_ID { get; set; }
        public Int64? REF_STYLE_ID { get; set; }
        public Int64? MC_BUYER_ID { get; set; }
        public Int64 MC_INQR_H_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string COMP_STYLE_NO { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public string STYLE_DESC { get; set; }
        public Int64 NO_OF_ITEM { get; set; }
        public string INSTRUCTION { get; set; }

        public string HAS_COMBO { get; set; }

        public string HAS_SET { get; set; }

        public string HAS_OPTION { get; set; }

        public string STYL_EXT_NO { get; set; }

        public string CONCEPT_NAME { get; set; }
        public string BRAND_NAME { get; set; }
        public string SEASON_REF { get; set; }
        public string SZ_RANGE { get; set; }
        public Decimal AVG_PRICE { get; set; }
        public Decimal QUOTE_PRICE { get; set; }
        public string IS_DEVELOPED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "pkg_merchandising.mc_style_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value = ob.MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pREF_STYLE_ID", Value = ob.REF_STYLE_ID},
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value = ob.MC_INQR_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pCOMP_STYLE_NO", Value = ob.COMP_STYLE_NO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = ob.STYLE_DESC},
                     new CommandParameter() {ParameterName = "pNO_OF_ITEM", Value = ob.NO_OF_ITEM},
                     new CommandParameter() {ParameterName = "pINSTRUCTION", Value = ob.INSTRUCTION},
                     new CommandParameter() {ParameterName = "pCONCEPT_NAME", Value = ob.CONCEPT_NAME},
                     new CommandParameter() {ParameterName = "pBRAND_NAME", Value = ob.BRAND_NAME},
                     new CommandParameter() {ParameterName = "pSEASON_REF", Value = ob.SEASON_REF},
                     new CommandParameter() {ParameterName = "pSZ_RANGE", Value = ob.SZ_RANGE},
                     new CommandParameter() {ParameterName = "pAVG_PRICE", Value = ob.AVG_PRICE},
                     new CommandParameter() {ParameterName = "pHAS_COMBO", Value = ob.HAS_COMBO},
                     new CommandParameter() {ParameterName = "pHAS_SET", Value = ob.HAS_SET},
                     new CommandParameter() {ParameterName = "pHAS_OPTION", Value = ob.HAS_OPTION},
                     new CommandParameter() {ParameterName = "pSTYL_EXT_NO", Value = ob.STYL_EXT_NO},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                     new CommandParameter() {ParameterName = "pIS_DEVELOPED", Value = ob.IS_DEVELOPED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_STYLE_ID", Value =null, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_merchandising.mc_style_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value = ob.MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pREF_STYLE_ID", Value = ob.REF_STYLE_ID},
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value = ob.MC_INQR_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pCOMP_STYLE_NO", Value = ob.COMP_STYLE_NO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = ob.STYLE_DESC},
                     new CommandParameter() {ParameterName = "pNO_OF_ITEM", Value = ob.NO_OF_ITEM},
                     new CommandParameter() {ParameterName = "pINSTRUCTION", Value = ob.INSTRUCTION},
                     new CommandParameter() {ParameterName = "pCONCEPT_NAME", Value = ob.CONCEPT_NAME},
                     new CommandParameter() {ParameterName = "pBRAND_NAME", Value = ob.BRAND_NAME},
                     new CommandParameter() {ParameterName = "pSEASON_REF", Value = ob.SEASON_REF},
                     new CommandParameter() {ParameterName = "pHAS_COMBO", Value = ob.HAS_COMBO},
                     new CommandParameter() {ParameterName = "pHAS_OPTION", Value = ob.HAS_OPTION},

                     new CommandParameter() {ParameterName = "pHAS_SET", Value = ob.HAS_SET},
                     new CommandParameter() {ParameterName = "pSTYL_EXT_NO", Value = ob.STYL_EXT_NO},

                     new CommandParameter() {ParameterName = "pSZ_RANGE", Value = ob.SZ_RANGE},
                     new CommandParameter() {ParameterName = "pAVG_PRICE", Value = ob.AVG_PRICE},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                     new CommandParameter() {ParameterName = "pIS_DEVELOPED", Value = ob.IS_DEVELOPED},
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
            const string sp = "pkg_merchandising.mc_style_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value = ob.MC_STYLE_ID},
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

        public List<MC_STYLEModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_style_select";
            try
            {
                var obList = new List<MC_STYLEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_STYLEModel ob = new MC_STYLEModel();
                            ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);
                            if (dr["REF_STYLE_ID"] != DBNull.Value)
                            {
                                ob.REF_STYLE_ID = Convert.ToInt64(dr["REF_STYLE_ID"]);
                            }
                            ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                            ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                            ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                            ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                            ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                            ob.NO_OF_ITEM = (dr["NO_OF_ITEM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ITEM"]);
                            ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                            ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                            ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);

                            ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                            ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                            ob.HAS_OPTION = (dr["HAS_OPTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_OPTION"]);

                            ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                            ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                            ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                            ob.AVG_PRICE = (dr["AVG_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_PRICE"]);
                            ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                            ob.IS_DEVELOPED = (dr["IS_DEVELOPED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEVELOPED"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYLEModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_style_select";
            try
            {
                var ob = new MC_STYLEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);

                        if (dr["REF_STYLE_ID"] != DBNull.Value)
                        {
                            ob.REF_STYLE_ID = Convert.ToInt64(dr["REF_STYLE_ID"]);
                        }

                        ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                        ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                        ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                        ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                        ob.NO_OF_ITEM = (dr["NO_OF_ITEM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ITEM"]);
                        ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                        ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                        ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                        ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                        ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                        ob.AVG_PRICE = (dr["AVG_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_PRICE"]);
                        ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                        ob.IS_DEVELOPED = (dr["IS_DEVELOPED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEVELOPED"]);
                        ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                        ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N": Convert.ToString(dr["HAS_SET"]);
                        ob.HAS_OPTION = (dr["HAS_OPTION"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_OPTION"]);
                        ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYLEModel getStyleDataByInqID(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_style_select";
            try
            {
                var ob = new MC_STYLEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);

                    if (dr["REF_STYLE_ID"] != DBNull.Value)
                    {
                        ob.REF_STYLE_ID = Convert.ToInt64(dr["REF_STYLE_ID"]);
                    }

                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);

                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.NO_OF_ITEM = (dr["NO_OF_ITEM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ITEM"]);
                    ob.INSTRUCTION = (dr["INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSTRUCTION"]);
                    ob.CONCEPT_NAME = (dr["CONCEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONCEPT_NAME"]);
                    ob.BRAND_NAME = (dr["BRAND_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME"]);
                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.AVG_PRICE = (dr["AVG_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.IS_DEVELOPED = (dr["IS_DEVELOPED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEVELOPED"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);

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