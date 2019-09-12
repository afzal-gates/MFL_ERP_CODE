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
    public class MC_COST_HEADModel
    {
        public Int64 MC_COST_HEAD_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public string COST_CODE { get; set; }
        public string COST_NAME_EN { get; set; }
        public string COST_NAME_BN { get; set; }
        public Int64 LEVEL_NO { get; set; }
        public string IS_LEAF { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_PCT { get; set; }
        public decimal? PCT_COST { get; set; }
        public decimal? HEAD_COST { get; set; }
        public Int64 PURCH_MOU_ID { get; set; }
        public Decimal? RATE_EST { get; set; }
        public Int64 ACCT_MOU_ID { get; set; }
        public Decimal? PCT_ADD_QTY { get; set; }
        public Decimal? QTY_EST { get; set; }
        public Int64? MC_STYL_BGT_COST_ID { get; set; }

        private List<MC_COST_HEADModel> _items = null;
        public List<MC_COST_HEADModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<MC_COST_HEADModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public string IS_QTY { get; set; }


        public string Save()
        {
            const string sp = "pkg_merchandising.mc_cost_head_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value = ob.MC_COST_HEAD_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pCOST_CODE", Value = ob.COST_CODE},
                     new CommandParameter() {ParameterName = "pCOST_NAME_EN", Value = ob.COST_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOST_NAME_BN", Value = ob.COST_NAME_BN},
                     new CommandParameter() {ParameterName = "pLEVEL_NO", Value = ob.LEVEL_NO},
                     new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
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
            const string sp = "pkg_merchandising.mc_cost_head_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value = ob.MC_COST_HEAD_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pCOST_CODE", Value = ob.COST_CODE},
                     new CommandParameter() {ParameterName = "pCOST_NAME_EN", Value = ob.COST_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOST_NAME_BN", Value = ob.COST_NAME_BN},
                     new CommandParameter() {ParameterName = "pLEVEL_NO", Value = ob.LEVEL_NO},
                     new CommandParameter() {ParameterName = "pIS_LEAF", Value = ob.IS_LEAF},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
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
            const string sp = "pkg_merchandising.mc_cost_head_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value = ob.MC_COST_HEAD_ID},
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

        public List<MC_COST_HEADModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_cost_head_select";
            try
            {
                var obList = new List<MC_COST_HEADModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_COST_HEADModel ob = new MC_COST_HEADModel();
                            ob.MC_COST_HEAD_ID = (dr["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COST_HEAD_ID"]);
                            ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                            ob.COST_CODE = (dr["COST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_CODE"]);
                            ob.COST_NAME_EN = (dr["COST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_NAME_EN"]);
                            ob.COST_NAME_BN = (dr["COST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_NAME_BN"]);
                            ob.LEVEL_NO = (dr["LEVEL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEVEL_NO"]);
                            ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                            ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_COST_HEADModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_cost_head_select";
            try
            {
                var ob = new MC_COST_HEADModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_COST_HEAD_ID = (dr["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COST_HEAD_ID"]);
                        ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                        ob.COST_CODE = (dr["COST_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_CODE"]);
                        ob.COST_NAME_EN = (dr["COST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_NAME_EN"]);
                        ob.COST_NAME_BN = (dr["COST_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_NAME_BN"]);
                        ob.LEVEL_NO = (dr["LEVEL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEVEL_NO"]);
                        ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                        ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_COST_HEADModel> FindCostHeadForBlkFab(Int64 MC_BLK_FAB_REQ_H_ID, Int64 MC_STYL_BGT_H_ID)
        {
            string sp = "pkg_merchandising.mc_cost_head_select";
            try
            {
                var obList = new List<MC_COST_HEADModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    MC_COST_HEADModel root = new MC_COST_HEADModel();
                    root.MC_COST_HEAD_ID = (ds.Tables[0].Rows[i]["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_COST_HEAD_ID"]);
                    root.PARENT_ID = (ds.Tables[0].Rows[i]["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
                    root.COST_NAME_EN = (ds.Tables[0].Rows[i]["COST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["COST_NAME_EN"]);
                    root.IS_PCT = (ds.Tables[0].Rows[i]["IS_PCT"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_PCT"]);
                    root.IS_QTY = (ds.Tables[0].Rows[i]["IS_QTY"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_QTY"]);



                    if (ds.Tables[0].Rows[i]["PCT_COST"] != DBNull.Value)
                    {
                        root.PCT_COST = Convert.ToDecimal(ds.Tables[0].Rows[i]["PCT_COST"]);
                    }

                    if (ds.Tables[0].Rows[i]["HEAD_COST"] != DBNull.Value)
                    {
                        root.HEAD_COST = Convert.ToDecimal(ds.Tables[0].Rows[i]["HEAD_COST"]);
                    }

                    if (ds.Tables[0].Rows[i]["MC_STYL_BGT_COST_ID"] != DBNull.Value)
                    {
                        root.MC_STYL_BGT_COST_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYL_BGT_COST_ID"]);
                    }

                    if (ds.Tables[0].Rows[i]["QTY_EST"] != DBNull.Value)
                    {
                        root.QTY_EST = Convert.ToDecimal(ds.Tables[0].Rows[i]["QTY_EST"]);
                    }

                    if (ds.Tables[0].Rows[i]["PCT_ADD_QTY"] != DBNull.Value)
                    {
                        root.PCT_ADD_QTY = Convert.ToDecimal(ds.Tables[0].Rows[i]["PCT_ADD_QTY"]);
                    }

                    if (ds.Tables[0].Rows[i]["ACCT_MOU_ID"] != DBNull.Value)
                    {
                        root.ACCT_MOU_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ACCT_MOU_ID"]);
                    }
                    else
                    {
                        root.ACCT_MOU_ID = 5;
                    }

                    if (ds.Tables[0].Rows[i]["RATE_EST"] != DBNull.Value)
                    {
                        root.RATE_EST = Convert.ToDecimal(ds.Tables[0].Rows[i]["RATE_EST"]);
                    }

                    if (ds.Tables[0].Rows[i]["PURCH_MOU_ID"] != DBNull.Value)
                    {
                        root.PURCH_MOU_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["PURCH_MOU_ID"]);
                    }
                    else
                    {
                        root.PURCH_MOU_ID = 5;
                    }

                    CreateTreeNode(root, MC_BLK_FAB_REQ_H_ID,MC_STYL_BGT_H_ID);
                    obList.Add(root);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateTreeNode(MC_COST_HEADModel node, Int64 MC_BLK_FAB_REQ_H_ID, Int64 MC_STYL_BGT_H_ID)
        {
            string sp = "pkg_merchandising.mc_cost_head_select";
            var obList = new List<MC_COST_HEADModel>();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = node.MC_COST_HEAD_ID},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MC_COST_HEADModel tnode = new MC_COST_HEADModel();

                tnode.MC_COST_HEAD_ID = (ds.Tables[0].Rows[i]["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_COST_HEAD_ID"]);
                tnode.PARENT_ID = (ds.Tables[0].Rows[i]["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
                tnode.COST_NAME_EN = (ds.Tables[0].Rows[i]["COST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["COST_NAME_EN"]);
                tnode.IS_PCT = (ds.Tables[0].Rows[i]["IS_PCT"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_PCT"]);


                tnode.IS_QTY = (ds.Tables[0].Rows[i]["IS_QTY"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["IS_QTY"]);

                if (ds.Tables[0].Rows[i]["PCT_COST"] != DBNull.Value)
                {
                    tnode.PCT_COST = Convert.ToDecimal(ds.Tables[0].Rows[i]["PCT_COST"]);
                }

                if (ds.Tables[0].Rows[i]["HEAD_COST"] != DBNull.Value)
                {
                    tnode.HEAD_COST = Convert.ToDecimal(ds.Tables[0].Rows[i]["HEAD_COST"]);
                }

                if (ds.Tables[0].Rows[i]["MC_STYL_BGT_COST_ID"] != DBNull.Value)
                {
                    tnode.MC_STYL_BGT_COST_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYL_BGT_COST_ID"]);
                }

                if (ds.Tables[0].Rows[i]["MC_STYL_BGT_COST_ID"] != DBNull.Value)
                {
                    tnode.MC_STYL_BGT_COST_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYL_BGT_COST_ID"]);
                }

                if (ds.Tables[0].Rows[i]["QTY_EST"] != DBNull.Value)
                {
                    tnode.QTY_EST = Convert.ToDecimal(ds.Tables[0].Rows[i]["QTY_EST"]);
                }

                if (ds.Tables[0].Rows[i]["PCT_ADD_QTY"] != DBNull.Value)
                {
                    tnode.PCT_ADD_QTY = Convert.ToDecimal(ds.Tables[0].Rows[i]["PCT_ADD_QTY"]);
                }

                if (ds.Tables[0].Rows[i]["ACCT_MOU_ID"] != DBNull.Value)
                {
                    tnode.ACCT_MOU_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["ACCT_MOU_ID"]);
                }
                else
                {
                    tnode.ACCT_MOU_ID = 5;
                }

                if (ds.Tables[0].Rows[i]["RATE_EST"] != DBNull.Value)
                {
                    tnode.RATE_EST = Convert.ToDecimal(ds.Tables[0].Rows[i]["RATE_EST"]);
                }

                if (ds.Tables[0].Rows[i]["PURCH_MOU_ID"] != DBNull.Value)
                {
                    tnode.PURCH_MOU_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["PURCH_MOU_ID"]);
                }
                else
                {
                    tnode.PURCH_MOU_ID = 5;
                }


                node.items.Add(tnode);
                CreateTreeNode(tnode, MC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID);
            }

        }

    }
}
