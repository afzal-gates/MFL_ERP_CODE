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
    public class MC_STYLE_D_ITEMModel
    {
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string COMBO_NO { get; set; }
        public string BUYER_ITEM_NO { get; set; }
        public string FABRICATION { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public string TECH_SPEC { get; set; }
        public Int64? LK_GARM_TYPE_ID { get; set; }
        public Int64? LK_GARM_DEPT_ID { get; set; }
        public Int64? LK_SLV_TYPE_ID { get; set; }
        public Int64? LK_NECK_TYPE_ID { get; set; }
        public string LK_WASH_TYPE_ID { get; set; }
        public string LK_FINIS_TYPE_ID { get; set; }        
        public Decimal UNIT_PRICE { get; set; }
        public Int64? RF_CURRENCY_ID { get; set; }
        public string IS_YD { get; set; }
        public string IS_AOP { get; set; }
        public string IS_EMB { get; set; }
        public string IS_PRINT { get; set; }
        public string STYL_KEY_IMG { get; set; }
        public HttpPostedFileBase STYL_KEY_IMG_FILE { get; set; }
        public string STYL_ALT_IMG { get; set; }
        public HttpPostedFileBase STYL_ALT_IMG_FILE { get; set; }
        public Int64 LK_ITEM_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string HAS_SET { get; set; }
        public string ITEM_LIST { get; set; }

        public string STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        

        public string IS_GMT_WASH { get; set; }
        public string LK_PRN_TYPE_ID { get; set; }
        public string LK_EMB_TYPE_ID { get; set; }
        public string LK_GMT_WASH_TYPE_ID { get; set; }
        public string LK_AOP_TYPE_ID { get; set; }
        public Int64? LK_YD_TYPE_ID { get; set; }

        public string ITEM_SNAME { get; set; }

        public string SEGMENT_FLAG { get; set; }
        public string NatureOfWork { get; set; }
        public string pLK_PRN_TYPE_LST { get; set; } 
        public List<LookupDataModel> LK_PRN_TYPE_LST { get; set; }
        public List<MC_STYLE_D_ITEMModel> CHILD_ITEMS { get; set; }
        public string IS_SOLID { get;set; }
        public Int64 NO_MACHINE { get; set; }
        public Int64 EST_PROD_CAPACTY_HR { get; set; }

        public string MODEL_NO { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public int? RF_FAB_CLASS_ID { get; set; }
        public int NO_OF_OPR { get; set; }
        public int NO_OF_HLPR { get; set; }
        public decimal? SMV { get; set; }
        public decimal? PLAN_EFICNCY { get; set; }
        public long MC_ORDER_STYL_ID { get; set; }
        public string PRODUCT_TYP_NAME_EN { get; set; }
        public string FAB_CLASS_NAME_EN { get; set; }
        public long ALLOCATED_QTY { get; set; }
        public Int64 ORDER_QTY { get; set; }
        public decimal? PCT_COMPLETION { get; set; }
        public List<GMT_PLN_LINE_LOADModel> ln_loads_items { get; set; }
        public string IS_INCLUDE_4PLN { get; set; }
        public int GMT_PRODUCT_TYP_ID { get; set; }
        public DateTime? LOAD_START_DT { get; set; }
        public DateTime? LOAD_END_DT { get; set; }
        public string IS_USED { get; set; }
        public string Save()
        {
            const string sp = "pkg_merchandising.mc_style_d_item_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob.COMBO_NO},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pBUYER_ITEM_NO", Value = ob.BUYER_ITEM_NO},
                     new CommandParameter() {ParameterName = "pFABRICATION", Value = ob.FABRICATION},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
                     new CommandParameter() {ParameterName = "pTECH_SPEC", Value = ob.TECH_SPEC},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob.LK_GARM_DEPT_ID},
                     new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob.LK_SLV_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob.LK_NECK_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_WASH_TYPE_ID", Value = ob.LK_WASH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FINIS_TYPE_ID", Value = ob.LK_FINIS_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pIS_YD", Value = ob.IS_YD},
                     new CommandParameter() {ParameterName = "pIS_AOP", Value = ob.IS_AOP},
                     new CommandParameter() {ParameterName = "pIS_EMB", Value = ob.IS_EMB},
                     new CommandParameter() {ParameterName = "pIS_GMT_WASH", Value = ob.IS_GMT_WASH},
                      new CommandParameter() {ParameterName = "pIS_HS_SINZING", Value = ob.IS_HS_SINZING},
                     new CommandParameter() {ParameterName = "pIS_PRINT", Value = ob.IS_PRINT},
                     new CommandParameter() {ParameterName = "pLK_PRN_TYPE_ID", Value = ob.LK_PRN_TYPE_ID},

                     new CommandParameter() {ParameterName = "pLK_EMB_TYPE_ID", Value = ob.LK_EMB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_GMT_WASH_TYPE_ID", Value = ob.LK_GMT_WASH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_AOP_TYPE_ID", Value = ob.LK_AOP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = ob.LK_YD_TYPE_ID},

                     new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob.ITEM_SNAME},
                     new CommandParameter() {ParameterName = "pSEGMENT_FLAG", Value = ob.SEGMENT_FLAG},

                     new CommandParameter() {ParameterName = "pIS_SOLID", Value = ob.IS_SOLID},
                     new CommandParameter() {ParameterName = "pNO_MACHINE", Value = ob.NO_MACHINE},
                     new CommandParameter() {ParameterName = "pEST_PROD_CAPACTY_HR", Value = ob.EST_PROD_CAPACTY_HR},

                     new CommandParameter() {ParameterName = "pSTYL_KEY_IMG", Value = ob.STYL_KEY_IMG},
                     new CommandParameter() {ParameterName = "pSTYL_ALT_IMG", Value = ob.STYL_ALT_IMG},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_STYLE_D_ITEM_ID", Value =null, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_merchandising.mc_style_d_item_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob.COMBO_NO},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pBUYER_ITEM_NO", Value = ob.BUYER_ITEM_NO},
                     new CommandParameter() {ParameterName = "pFABRICATION", Value = ob.FABRICATION},
                     new CommandParameter() {ParameterName = "pSP_INSTRUCTION", Value = ob.SP_INSTRUCTION},
                     new CommandParameter() {ParameterName = "pTECH_SPEC", Value = ob.TECH_SPEC},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob.LK_GARM_DEPT_ID},
                     new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob.LK_SLV_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob.LK_NECK_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_WASH_TYPE_ID", Value = ob.LK_WASH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_FINIS_TYPE_ID", Value = ob.LK_FINIS_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pIS_YD", Value = ob.IS_YD},
                     new CommandParameter() {ParameterName = "pIS_AOP", Value = ob.IS_AOP},
                     new CommandParameter() {ParameterName = "pIS_EMB", Value = ob.IS_EMB},
                     new CommandParameter() {ParameterName = "pIS_PRINT", Value = ob.IS_PRINT},
                     new CommandParameter() {ParameterName = "pIS_GMT_WASH", Value = ob.IS_GMT_WASH},
                     new CommandParameter() {ParameterName = "pLK_PRN_TYPE_ID", Value = ob.LK_PRN_TYPE_ID},

                     new CommandParameter() {ParameterName = "pLK_EMB_TYPE_ID", Value = ob.LK_EMB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_GMT_WASH_TYPE_ID", Value = ob.LK_GMT_WASH_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_AOP_TYPE_ID", Value = ob.LK_AOP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = ob.LK_YD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob.ITEM_SNAME},
                     new CommandParameter() {ParameterName = "pSEGMENT_FLAG", Value = ob.SEGMENT_FLAG},

                     new CommandParameter() {ParameterName = "pIS_SOLID", Value = ob.IS_SOLID},
                     new CommandParameter() {ParameterName = "pNO_MACHINE", Value = ob.NO_MACHINE},
                     new CommandParameter() {ParameterName = "pEST_PROD_CAPACTY_HR", Value = ob.EST_PROD_CAPACTY_HR},

                     new CommandParameter() {ParameterName = "pSTYL_KEY_IMG", Value = ob.STYL_KEY_IMG},
                     new CommandParameter() {ParameterName = "pIS_HS_SINZING", Value = ob.IS_HS_SINZING},
                     new CommandParameter() {ParameterName = "pSTYL_ALT_IMG", Value = ob.STYL_ALT_IMG},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "pkg_merchandising.mc_style_d_item_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
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

        public List<MC_STYLE_D_ITEMModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                            ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);

                            if (dr["PARENT_ID"] != DBNull.Value)
                            {
                                ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                            }
                          
                            if (dr["LK_GARM_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_GARM_TYPE_ID = Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                            }

                            if (dr["LK_GARM_DEPT_ID"] != DBNull.Value)
                            {
                                ob.LK_GARM_DEPT_ID = Convert.ToInt64(dr["LK_GARM_DEPT_ID"]);
                            }

                            if (dr["LK_SLV_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_SLV_TYPE_ID = Convert.ToInt64(dr["LK_SLV_TYPE_ID"]);
                            }

                            if (dr["LK_NECK_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_NECK_TYPE_ID = Convert.ToInt64(dr["LK_NECK_TYPE_ID"]);
                            }

                            if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_YD_TYPE_ID = Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                            }

                            ob.LK_PRN_TYPE_ID = (dr["LK_PRN_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PRN_TYPE_ID"]);
                            ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                            ob.SEGMENT_FLAG = (dr["SEGMENT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEGMENT_FLAG"]);
                            ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                            ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                            ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                            ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                            ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                            ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);

                            ob.BUYER_ITEM_NO = (dr["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_ITEM_NO"]);
                            ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                            ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                            ob.TECH_SPEC = (dr["TECH_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC"]);
                            ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                            ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);                            
                            ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                            ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                            ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                            ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                            ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                            ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                            ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);
                            ob.STYL_ALT_IMG = (dr["STYL_ALT_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_ALT_IMG"]);
                            ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);

                            ob.LK_EMB_TYPE_ID = (dr["LK_EMB_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_EMB_TYPE_ID"]);
                            ob.LK_GMT_WASH_TYPE_ID = (dr["LK_GMT_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_GMT_WASH_TYPE_ID"]);
                            ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_AOP_TYPE_ID"]);

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYLE_D_ITEMModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var ob = new MC_STYLE_D_ITEMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);

                        if (dr["PARENT_ID"] != DBNull.Value)
                        {
                            ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                        }


                        if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                        {
                            ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                        }


                        ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                        ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                        ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                        ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                        ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                        ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                        ob.BUYER_ITEM_NO = (dr["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_ITEM_NO"]);
                        ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                        ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                        ob.TECH_SPEC = (dr["TECH_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC"]);
                        ob.LK_PRN_TYPE_ID = (dr["LK_PRN_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PRN_TYPE_ID"]);
                        
                        
                        if (dr["LK_GARM_TYPE_ID"] != DBNull.Value)
                        {
                            ob.LK_GARM_TYPE_ID = Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                        }

                        if (dr["LK_GARM_DEPT_ID"] != DBNull.Value)
                        {
                            ob.LK_GARM_DEPT_ID = Convert.ToInt64(dr["LK_GARM_DEPT_ID"]);
                        }

                        if (dr["LK_SLV_TYPE_ID"] != DBNull.Value)
                        {
                            ob.LK_SLV_TYPE_ID = Convert.ToInt64(dr["LK_SLV_TYPE_ID"]);
                        }

                        if (dr["LK_NECK_TYPE_ID"] != DBNull.Value)
                        {
                            ob.LK_NECK_TYPE_ID = Convert.ToInt64(dr["LK_NECK_TYPE_ID"]);
                        }

                        if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                        {
                            ob.LK_YD_TYPE_ID = Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                        }

                        ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                        ob.SEGMENT_FLAG = (dr["SEGMENT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEGMENT_FLAG"]);

                        ob.IS_SOLID = (dr["IS_SOLID"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SOLID"]);
                        ob.NO_MACHINE = (dr["NO_MACHINE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_MACHINE"]);
                        ob.EST_PROD_CAPACTY_HR = (dr["EST_PROD_CAPACTY_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EST_PROD_CAPACTY_HR"]);

                        ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                        ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);                        
                        ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                        ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                        ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                        ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                        ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                        ob.IS_GMT_WASH = (dr["IS_GMT_WASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GMT_WASH"]);
                        ob.IS_USED = (dr["IS_USED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_USED"]);

                        ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);
                        ob.STYL_ALT_IMG = (dr["STYL_ALT_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_ALT_IMG"]);
                        ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);

                        ob.LK_EMB_TYPE_ID = (dr["LK_EMB_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_EMB_TYPE_ID"]);
                        ob.LK_GMT_WASH_TYPE_ID = (dr["LK_GMT_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_GMT_WASH_TYPE_ID"]);
                        ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_AOP_TYPE_ID"]);

                        ob.IS_HS_SINZING = (dr["IS_HS_SINZING"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_HS_SINZING"]);
                        string spPrint = "PKG_Print_Strike.mc_prnso_req_h_select";
                        ob.pLK_PRN_TYPE_LST = (dr["LK_PRN_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PRN_TYPE_ID"]);
                        if (ob.pLK_PRN_TYPE_LST != "")
                        {
                            List<LookupDataModel> obLkList = new List<LookupDataModel>();
                            var LkDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pLK_PRN_TYPE_LST", Value =  ob.pLK_PRN_TYPE_LST},
                             new CommandParameter() {ParameterName = "pOption", Value = 3006},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, spPrint);
                            foreach (DataRow drlk in LkDs.Tables[0].Rows)
                            {
                                LookupDataModel obLk = new LookupDataModel();
                                obLk.LOOKUP_DATA_ID = (drlk["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drlk["LOOKUP_DATA_ID"]);
                                obLk.LK_DATA_NAME_EN = (drlk["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drlk["LK_DATA_NAME_EN"]);
                                obLkList.Add(obLk);
                            }
                            ob.LK_PRN_TYPE_LST = obLkList;
                        }

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_ITEMModel> StyleDtlParentItemList(Int64? pMC_STYLE_H_ID, string pIS_GET_CHILD)
        {
            Int64 vOption = 0;
            if (pIS_GET_CHILD == "Y")
                vOption = 3004;
            else
                vOption = 3003;

            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = vOption},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.NO_MACHINE = (dr["NO_MACHINE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_MACHINE"]);
                    ob.EST_PROD_CAPACTY_HR = (dr["EST_PROD_CAPACTY_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EST_PROD_CAPACTY_HR"]);


                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.BUYER_ITEM_NO = (dr["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_ITEM_NO"]);
                    ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.TECH_SPEC = (dr["TECH_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_GARM_DEPT_ID = (dr["LK_GARM_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_DEPT_ID"]);
                    ob.LK_SLV_TYPE_ID = (dr["LK_SLV_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SLV_TYPE_ID"]);
                    ob.LK_NECK_TYPE_ID = (dr["LK_NECK_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NECK_TYPE_ID"]);
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);                    
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);
                    ob.STYL_ALT_IMG = (dr["STYL_ALT_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_ALT_IMG"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);

                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_STYLE_D_ITEMModel> StyleDtlItemList(Int64? pMC_STYLE_H_ID, string pIS_GET_CHILD)
        {
            Int64 vOption = 0;
            if (pIS_GET_CHILD == "Y")
                vOption = 3005;
            else if (pIS_GET_CHILD == "N")
                vOption = 3003;
            else
                vOption = 3007;

            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = vOption},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }


                    if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                    {
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    
                    }

                    ob.NO_MACHINE = (dr["NO_MACHINE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_MACHINE"]);
                    ob.EST_PROD_CAPACTY_HR = (dr["EST_PROD_CAPACTY_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EST_PROD_CAPACTY_HR"]);


                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);

                    ob.BUYER_ITEM_NO = (dr["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_ITEM_NO"]);
                    ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.TECH_SPEC = (dr["TECH_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC"]);
                    ob.LK_PRN_TYPE_ID = (dr["LK_PRN_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PRN_TYPE_ID"]);


                    if (dr["LK_GARM_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_GARM_TYPE_ID = Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    }

                    if (dr["LK_GARM_DEPT_ID"] != DBNull.Value)
                    {
                        ob.LK_GARM_DEPT_ID = Convert.ToInt64(dr["LK_GARM_DEPT_ID"]);
                    }

                    if (dr["LK_SLV_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_SLV_TYPE_ID = Convert.ToInt64(dr["LK_SLV_TYPE_ID"]);
                    }

                    if (dr["LK_NECK_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_NECK_TYPE_ID = Convert.ToInt64(dr["LK_NECK_TYPE_ID"]);
                    }

                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_YD_TYPE_ID = Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    }
                    
                    ob.SEGMENT_FLAG = (dr["SEGMENT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEGMENT_FLAG"]);
                                        
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);                    
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    ob.IS_GMT_WASH = (dr["IS_GMT_WASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GMT_WASH"]);
                    ob.IS_SOLID = (dr["IS_SOLID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SOLID"]);


                    ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);
                    ob.STYL_ALT_IMG = (dr["STYL_ALT_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_ALT_IMG"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);

                    ob.LK_EMB_TYPE_ID = (dr["LK_EMB_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_EMB_TYPE_ID"]);
                    ob.LK_GMT_WASH_TYPE_ID = (dr["LK_GMT_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_GMT_WASH_TYPE_ID"]);
                    ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_AOP_TYPE_ID"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);

                    ob.CHILD_ITEMS = new List<MC_STYLE_D_ITEMModel> { };

                    if (ob.HAS_SET == "Y")
                    {
                        var obItemsList = new List<MC_STYLE_D_ITEMModel>();
                        var dsItems = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {                             
                             new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                             new CommandParameter() {ParameterName = "pOption", Value = 3004},
                             new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                         }, sp);
                        foreach (DataRow drItems in dsItems.Tables[0].Rows)
                        {
                            MC_STYLE_D_ITEMModel obItems = new MC_STYLE_D_ITEMModel();
                            if (drItems["PARENT_ID"] != DBNull.Value)
                            {
                                obItems.PARENT_ID = Convert.ToInt64(drItems["PARENT_ID"]);
                            }

                            obItems.MC_STYLE_D_ITEM_ID = (drItems["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drItems["MC_STYLE_D_ITEM_ID"]);

                            obItems.MC_STYLE_H_ID = (drItems["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drItems["MC_STYLE_H_ID"]);
                            obItems.INV_ITEM_CAT_ID = (drItems["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drItems["INV_ITEM_CAT_ID"]);
                            obItems.ITEM_CODE = (drItems["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drItems["ITEM_CODE"]);
                            obItems.ITEM_NAME_EN = (drItems["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drItems["ITEM_NAME_EN"]);
                            obItems.ITEM_SNAME = (drItems["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drItems["ITEM_SNAME"]);
                            obItems.MODEL_NO = (drItems["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drItems["MODEL_NO"]);
                            obItems.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drItems["LK_ITEM_STATUS_ID"]);

                            obItemsList.Add(obItems);
                        }
                        ob.CHILD_ITEMS = obItemsList;
                    }
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object ChildItemListByStyle(Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    //ob.ITEM_LIST = (dr["ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_LIST"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);

                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_FAB_CLASS_ID"]);

                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_GARM_TYPE_ID"]);

                    ob.NO_OF_OPR = (dr["NO_OF_OPR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_OPR"]);
                    ob.NO_OF_HLPR = (dr["NO_OF_HLPR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_HLPR"]);
                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.PLAN_EFICNCY = (dr["PLAN_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_EFICNCY"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);                    
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ParentItemList(long ID)
        {
            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    //ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.ITEM_LIST = (dr["ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_LIST"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_ITEMModel> OrderWiseItemList(Int64? pMC_ORDER_H_ID, string pMC_ORDER_H_IDS, Int64? pMC_ORDER_SHIP_ID = null, string pEXCLUDE_PARENT_ID = "N")
        {            
            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},   
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_IDS", Value = pMC_ORDER_H_IDS},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = pMC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pEXCLUDE_PARENT_ID", Value = pEXCLUDE_PARENT_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]) + " " + ob.MODEL_NO;
                    ob.BUYER_ITEM_NO = (dr["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_ITEM_NO"]);
                    ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.TECH_SPEC = (dr["TECH_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_GARM_DEPT_ID = (dr["LK_GARM_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_DEPT_ID"]);
                    ob.LK_SLV_TYPE_ID = (dr["LK_SLV_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SLV_TYPE_ID"]);
                    ob.LK_NECK_TYPE_ID = (dr["LK_NECK_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NECK_TYPE_ID"]);
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);
                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    { ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]); }
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    //ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);
                    //ob.STYL_ALT_IMG = (dr["STYL_ALT_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_ALT_IMG"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);

                    //ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_ITEMModel> StyleWiseItemList(Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.BUYER_ITEM_NO = (dr["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_ITEM_NO"]);
                    ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.TECH_SPEC = (dr["TECH_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_GARM_DEPT_ID = (dr["LK_GARM_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_DEPT_ID"]);
                    ob.LK_SLV_TYPE_ID = (dr["LK_SLV_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SLV_TYPE_ID"]);
                    ob.LK_NECK_TYPE_ID = (dr["LK_NECK_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NECK_TYPE_ID"]);
                    ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);
                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    { ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]); }
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    //ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);
                    //ob.STYL_ALT_IMG = (dr["STYL_ALT_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_ALT_IMG"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);

                    //ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.NatureOfWork = (dr["NatureOfWork"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NatureOfWork"]);

                    string spPrint = "PKG_Print_Strike.mc_prnso_req_h_select";
                    ob.pLK_PRN_TYPE_LST = (dr["LK_PRN_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PRN_TYPE_ID"]);
                    if (ob.pLK_PRN_TYPE_LST != "")
                    {
                        List<LookupDataModel> obLkList = new List<LookupDataModel>();
                        var LkDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pLK_PRN_TYPE_LST", Value =  ob.pLK_PRN_TYPE_LST},
                             new CommandParameter() {ParameterName = "pOption", Value = 3006},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, spPrint);
                        foreach (DataRow drlk in LkDs.Tables[0].Rows)
                        {
                            LookupDataModel obLk = new LookupDataModel();
                            obLk.LOOKUP_DATA_ID = (drlk["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drlk["LOOKUP_DATA_ID"]);
                            obLk.LK_DATA_NAME_EN = (drlk["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drlk["LK_DATA_NAME_EN"]);
                            obLkList.Add(obLk);
                        }
                        ob.LK_PRN_TYPE_LST = obLkList;
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_ITEMModel> OrderWiseItemListForPln(Int64? pMC_ORDER_SHIP_ID, int? pOption=null, Int64? pMC_ORDER_STYL_ID = null )
        {
            string sp = "pkg_merchandising.mc_style_d_item_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = pMC_ORDER_SHIP_ID},   
                     new CommandParameter() {ParameterName = "pMC_ORDER_STYL_ID", Value = pMC_ORDER_STYL_ID},   
                     new CommandParameter() {ParameterName = "pOption", Value = 3010},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_ORDER_STYL_ID = (dr["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_STYL_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);

                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_FAB_CLASS_ID"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["INV_ITEM_CAT_ID"]);

                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GMT_PRODUCT_TYP_ID"]);

                    ob.PRODUCT_TYP_NAME_EN = (dr["PRODUCT_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_NAME_EN"]);
                    ob.FAB_CLASS_NAME_EN = (dr["FAB_CLASS_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_NAME_EN"]);
                    

                    ob.NO_OF_OPR = (dr["NO_OF_OPR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_OPR"]);
                    ob.NO_OF_HLPR = (dr["NO_OF_HLPR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_HLPR"]);
                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.PLAN_EFICNCY = (dr["PLAN_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_EFICNCY"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);
                   
                    
                    ob.IS_INCLUDE_4PLN = "Y";
                    
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    ob.IS_GMT_WASH = (dr["IS_GMT_WASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GMT_WASH"]);
                    ob.IS_SOLID = (dr["IS_SOLID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SOLID"]);
                   
                    ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);

                    if (dr["LOAD_START_DT"] != DBNull.Value)
                    {
                        ob.LOAD_START_DT = Convert.ToDateTime(dr["LOAD_START_DT"]);
                    }

                    if (dr["LOAD_END_DT"] != DBNull.Value)
                    {
                        ob.LOAD_END_DT = Convert.ToDateTime(dr["LOAD_END_DT"]);
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_STYLE_D_ITEMModel> GmtItemListForPln(Int64 pMC_STYLE_H_ID, string pMC_ORDER_ITEM_PLN_LST)
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.get_list_for_planning";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},   
                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_LST", Value = pMC_ORDER_ITEM_PLN_LST},   
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_ORDER_ITEM_PLN_ID = (dr["MC_ORDER_ITEM_PLN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_ITEM_PLN_ID"]);

                    ob.MC_STYLE_H_ID = pMC_STYLE_H_ID;

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_FAB_CLASS_ID"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["INV_ITEM_CAT_ID"]);

                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GMT_PRODUCT_TYP_ID"]);

                    ob.PRODUCT_TYP_NAME_EN = (dr["PRODUCT_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_NAME_EN"]);
                    ob.FAB_CLASS_NAME_EN = (dr["FAB_CLASS_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_CLASS_NAME_EN"]);

                    ob.NO_OF_OPR = (dr["NO_OF_OPR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_OPR"]);
                    ob.NO_OF_HLPR = (dr["NO_OF_HLPR"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_HLPR"]);
                    ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);
                    ob.PLAN_EFICNCY = (dr["PLAN_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_EFICNCY"]);
                    ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);


                    ob.IS_INCLUDE_4PLN = "Y";

                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    ob.IS_GMT_WASH = (dr["IS_GMT_WASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GMT_WASH"]);
                    ob.IS_SOLID = (dr["IS_SOLID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SOLID"]);

                    ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);

                    if (dr["LOAD_START_DT"] != DBNull.Value)
                    {
                        ob.LOAD_START_DT = Convert.ToDateTime(dr["LOAD_START_DT"]);
                    }

                    if (dr["LOAD_END_DT"] != DBNull.Value)
                    {
                        ob.LOAD_END_DT = Convert.ToDateTime(dr["LOAD_END_DT"]);
                    }

                    if (dr["SHIP_DT"] != DBNull.Value)
                    {
                        ob.SHIP_DT = Convert.ToDateTime(dr["SHIP_DT"]).ToString("dd-MMM-yyyy");
                    }

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                    {
                        ob.ORD_CONF_DT = Convert.ToDateTime(dr["ORD_CONF_DT"]).ToString("dd-MMM-yyyy"); ;
                    }


                    ob.ln_loads_items = new GMT_PLN_LINE_LOADModel().getLoadedDateByOrderItem(ob.MC_ORDER_ITEM_PLN_ID);
                    if (ob.ln_loads_items.Count == 0)
                    {
                        ob.ln_loads_items.Add(new GMT_PLN_LINE_LOADModel()
                        {
                            GMT_PLN_LINE_LOAD_ID = -1,
                            PLAN_MP = (ob.NO_OF_OPR + ob.NO_OF_HLPR),
                            PLAN_OP = ob.NO_OF_OPR,
                            PLAN_HP = ob.NO_OF_HLPR,
                            PLAN_WH = 11,
                            IS_LRN_CRV_APP = "Y",
                            ALLOCATED_QTY = ob.ORDER_QTY,
                            SEW_START_DT = DateTime.Today.Date.AddHours(8),
                            SEW_END_DT = DateTime.Today.Date.AddHours(8).AddHours(23),
                            GMT_PLN_STRIPE_PHASE_ID = 1,
                            PROD_QTY = 0,
                            HR_PROD_LINE_LST = new List<int>() { -1 }
                        });
                        ob.ALLOCATED_QTY = 0;
                        ob.PCT_COMPLETION = 0;
                        ob.TOT_PROD = 0;
                    }
                    else
                    {
                        ob.ALLOCATED_QTY = ob.ln_loads_items.Sum(x => x.ALLOCATED_QTY);
                        ob.PCT_COMPLETION = (ob.ALLOCATED_QTY / ob.ORDER_QTY) * 100;
                        ob.TOT_PROD = ob.ln_loads_items.Sum(x => x.TOT_PROD);
                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string IS_HS_SINZING { get; set; }

        public long TOT_PROD { get; set; }

        public long MC_ORDER_ITEM_PLN_ID { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string ORDER_NO { get; set; }

        public string SHIP_DT { get; set; }

        public string ORD_CONF_DT { get; set; }
    }
}