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
    public class MC_STYL_ITM_COSTModel
    {
        public Int64 MC_STYL_ITM_COST_ID { get; set; }
        public Int64 MC_STYLE_ITEM_ID { get; set; }
        public Int64 MC_COST_HEAD_ID { get; set; }
        public string IS_PCT { get; set; }

        public string COST_NAME_EN {get;set;}
        public Decimal? PCT_COST { get; set; }
        public Decimal? HEAD_COST { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string ITEM_NAME_EN { get; set; }

        public string MATERIAL_SPEC { get; set; }

        public Int64? COMBO_NO { get; set; }

        public Int64? REMOVE { get; set; }

        public string ITEM_GROUP_NAME { get; set; }

        public Decimal FOB { get; set; }
        public Decimal CAL_PRICE { get; set; }
        public Decimal QUOTE_PRICE { get; set; }
        public string ITEM_NAME { get; set; }
        public string HEAD_COST_DESC { get; set; }
        public decimal FAB_COST { get; set; }


        private List<MC_STYL_ITM_COSTModel> _items = null;
        public List<MC_STYL_ITM_COSTModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<MC_STYL_ITM_COSTModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public string Save()
        {
            const string sp = "pkg_merchandising.mc_styl_itm_cost_insert";
            string jsonStr="{}";
            var ob = this;
            try
             {
                OraDatabase db = new OraDatabase();


                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pFOB", Value = ob.FOB},
                                 new CommandParameter() {ParameterName = "pCAL_PRICE", Value = ob.CAL_PRICE},
                                 new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                                 new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.CREATED_BY},
                                 new CommandParameter() {ParameterName = "pOption", Value =2001},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, "pkg_merchandising.mc_style_item_update");

                foreach (DataRow dr1 in ds1.Tables["OUTPARAM"].Rows)
                {
                    jsonStr = Convert.ToString(dr1["VALUE"]);

                }


                if (ob.items.Count > 0)
                {

                    foreach (var item in ob.items)
                    {
                        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYL_ITM_COST_ID", Value = item.MC_STYL_ITM_COST_ID},
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value = item.MC_COST_HEAD_ID},
                                 new CommandParameter() {ParameterName = "pIS_PCT", Value = item.IS_PCT},
                                 new CommandParameter() {ParameterName = "pPCT_COST", Value = item.PCT_COST},
                                 new CommandParameter() {ParameterName = "pHEAD_COST", Value = item.HEAD_COST},
                                 new CommandParameter() {ParameterName = "pCREATION_DATE", Value = item.CREATION_DATE},

                                 new CommandParameter() {ParameterName = "pREMOVE", Value = item.REMOVE},
                                 new CommandParameter() {ParameterName = "pHEAD_COST_DESC", Value = item.HEAD_COST_DESC},
                               
                                 new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                                 new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.CREATED_BY},
                                 new CommandParameter() {ParameterName = "pOption", Value =1000},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                        foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                        {
                          jsonStr = Convert.ToString(dr["VALUE"]);

                        }


                    }

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
            const string sp = "pkg_merchandising.mc_styl_itm_cost_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_ITM_COST_ID", Value = ob.MC_STYL_ITM_COST_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value = ob.MC_COST_HEAD_ID},
                     new CommandParameter() {ParameterName = "pIS_PCT", Value = ob.IS_PCT},
                     new CommandParameter() {ParameterName = "pPCT_COST", Value = ob.PCT_COST},
                     new CommandParameter() {ParameterName = "pHEAD_COST", Value = ob.HEAD_COST},
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
            const string sp = "pkg_merchandising.mc_styl_itm_cost_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_ITM_COST_ID", Value = ob.MC_STYL_ITM_COST_ID},
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

        public List<MC_STYL_ITM_COSTModel> SelectAll(Int64 MC_STYLE_ITEM_ID, Int64 pOption)
        {


            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYL_ITM_COSTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value =MC_STYLE_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYL_ITM_COSTModel ob = new MC_STYL_ITM_COSTModel();
                    ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);                    
                    ob.FAB_COST = (dr["FAB_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_COST"]);
                    ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                    ob.FOB = (dr["FOB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FOB"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3002},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, "pkg_merchandising.mc_styl_itm_cost_select");


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_STYL_ITM_COSTModel ob1 = new MC_STYL_ITM_COSTModel();
                        ob1.MC_STYL_ITM_COST_ID = (dr1["MC_STYL_ITM_COST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYL_ITM_COST_ID"]);
                        ob1.MC_STYLE_ITEM_ID = (dr1["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_ITEM_ID"]);
                        ob1.MC_COST_HEAD_ID = (dr1["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_COST_HEAD_ID"]);

                        if (ob1.MC_COST_HEAD_ID == 6)
                        {
                            ob1.HEAD_COST = ob.FAB_COST;
                        }
                        else
                        {
                            if (dr1["HEAD_COST"] != DBNull.Value)
                            {
                                ob1.HEAD_COST = Convert.ToDecimal(dr1["HEAD_COST"]);
                            }

                        }


                        ob1.IS_PCT = (dr1["IS_PCT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_PCT"]);
                        ob1.HEAD_COST_DESC = (dr1["HEAD_COST_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["HEAD_COST_DESC"]);                       
                        ob1.COST_NAME_EN = (dr1["COST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COST_NAME_EN"]);
                        

                        if (dr1["PCT_COST"] != DBNull.Value)
                        {
                            ob1.PCT_COST = Convert.ToDecimal(dr1["PCT_COST"]);
                        }


                        ob.items.Add(ob1);
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

        public MC_STYL_ITM_COSTModel Select(long ID)
        {
            string sp = "pkg_merchandising.mc_styl_itm_cost_select";
            try
            {
                var ob = new MC_STYL_ITM_COSTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYL_ITM_COST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_STYL_ITM_COST_ID = (dr["MC_STYL_ITM_COST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_ITM_COST_ID"]);
                        ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);
                        ob.MC_COST_HEAD_ID = (dr["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COST_HEAD_ID"]);
                        ob.IS_PCT = (dr["IS_PCT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PCT"]);
                        ob.PCT_COST = (dr["PCT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_COST"]);
                        ob.HEAD_COST = (dr["HEAD_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["HEAD_COST"]);
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