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
    public class MC_STYLE_ITEMModel
    {
        public Int64 MC_STYLE_ITEM_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 MC_STYLE_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64 ITEM_GRP_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_EXT_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string ITEM_SNAME { get; set; }
        public string MATERIAL_SPEC { get; set; }
        public Int64? LK_GARM_TYPE_ID { get; set; }
        public Int64? LK_GARM_DEPT_ID { get; set; }
        public Int64? LK_SLV_TYPE_ID { get; set; }
        public Int64? LK_NECK_TYPE_ID { get; set; }
        public Int64 PCS_PER_PACK { get; set; }
        public Decimal CAL_PRICE { get; set; }
        public Decimal QUOTE_PRICE { get; set; }
        public Int64? LK_ITEM_STATUS_ID { get; set; }
        public string KEY_IMAGE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string COMBO_NO { get; set; }

        public Int64 MC_STYL_FAB_COST_ID { get; set; }

        public String GARM_PART { get; set; }
        public string FABRIC_DESC{get;set;}

        public Decimal FABRIC_CONS { get; set; }

        public Decimal FABRIC_RATE { get; set; }

        public string SEGMENT_FLAG { get; set; }

        public string OPTION_NO { get; set; }


        public Decimal FAB_COST { get; set; }
        public Decimal TRIM_ACC { get; set; }

        public Decimal PRINT_COST { get; set; }

        public Decimal COMMERCIAL { get; set; }

        public Decimal SERV { get; set; }
        public Decimal FOB { get; set; }
        public Decimal FAB_COST_D { get; set; }
        public decimal CM { get; set; }

        public Int64? LK_FBR_GRP_ID { get; set; }

        public string YC_DIA { get; set; }

        



        private List<MC_STYLE_ITEMModel> _items = null;
        public List<MC_STYLE_ITEMModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<MC_STYLE_ITEMModel>();
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
            const string sp = "pkg_merchandising.mc_style_item_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value = ob.MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_GRP_ID", Value = ob.ITEM_GRP_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pITEM_EXT_NO", Value = ob.ITEM_EXT_NO},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob.ITEM_SNAME},
                     new CommandParameter() {ParameterName = "pMATERIAL_SPEC", Value = ob.MATERIAL_SPEC},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob.LK_GARM_DEPT_ID},
                     new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob.LK_SLV_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob.LK_NECK_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pCAL_PRICE", Value = ob.CAL_PRICE},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     new CommandParameter() {ParameterName = "pKEY_IMAGE", Value = ob.KEY_IMAGE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob.COMBO_NO},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_STYLE_ITEM_ID", Value =null, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_merchandising.mc_style_item_update";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value = ob.MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_GRP_ID", Value = ob.ITEM_GRP_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pITEM_EXT_NO", Value = ob.ITEM_EXT_NO},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob.ITEM_SNAME},
                     new CommandParameter() {ParameterName = "pMATERIAL_SPEC", Value = ob.MATERIAL_SPEC},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob.LK_GARM_DEPT_ID},
                     new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob.LK_SLV_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob.LK_NECK_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pCAL_PRICE", Value = ob.CAL_PRICE},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     new CommandParameter() {ParameterName = "pKEY_IMAGE", Value = ob.KEY_IMAGE},
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
            const string sp = "pkg_merchandising.mc_style_item_delete";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
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

        public List<MC_STYLE_ITEMModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYLE_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_STYLE_ITEMModel ob = new MC_STYLE_ITEMModel();
                            ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);

                            if (dr["PARENT_ID"] != DBNull.Value)
                            {
                                ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                            }

                            ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);
                            ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                            ob.ITEM_GRP_ID = (dr["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_GRP_ID"]);
                            ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                            ob.ITEM_EXT_NO = (dr["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_EXT_NO"]);
                            ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                            ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                            ob.MATERIAL_SPEC = (dr["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MATERIAL_SPEC"]);

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
                            ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                            ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                            ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                            ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                            ob.KEY_IMAGE = (dr["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KEY_IMAGE"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYLE_ITEMModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var ob = new MC_STYLE_ITEMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);

                   if (dr["PARENT_ID"] != DBNull.Value)
                   {
                       ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                   }

                   ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);
                   ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                   ob.ITEM_GRP_ID = (dr["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_GRP_ID"]);
                   ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                   ob.ITEM_EXT_NO = (dr["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_EXT_NO"]);
                   ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                   ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                   ob.MATERIAL_SPEC = (dr["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MATERIAL_SPEC"]);

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
                   ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                   ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                   ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                   ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                   ob.KEY_IMAGE = (dr["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KEY_IMAGE"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object InvItemByParent(Int64 ID)
        {
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                var ds = new DataSet();
                OraDatabase db = new OraDatabase();
                if (ID == 0)
                {
                    ds = db.ExecuteSQLStatement("select * from INV_ITEM_CAT where ITEM_CAT_PREFIX='GM' and  LENGTH(ITEM_CAT_CODE)=6");
                }
                else
                {
                    ds = db.ExecuteSQLStatement("select * from INV_ITEM_CAT where PARENT_ID=" + ID + " and ITEM_CAT_CODE!='GM03' order by INV_ITEM_CAT_ID");
                }


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.ITEM_CAT_PREFIX = (dr["ITEM_CAT_PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_PREFIX"]);
                    ob.ITEM_CAT_CODE = (dr["ITEM_CAT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_CODE"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_CAT_NAME_BN = (dr["ITEM_CAT_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_BN"]);
                    ob.ITEM_CAT_SNAME = (dr["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_SNAME"]);
                    ob.ITEM_CAT_LEVEL = (dr["ITEM_CAT_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_LEVEL"]);
                    ob.ITEM_CAT_ORDER = (dr["ITEM_CAT_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_CAT_ORDER"]);
                    ob.IS_LEAF = (dr["IS_LEAF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LEAF"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_M_P = (dr["IS_M_P"] == DBNull.Value) ? "M" : Convert.ToString(dr["IS_M_P"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object InvItemByLine(Int64 pPARENT_ID, Int64 pHR_PROD_LINE_ID)
        {
            try
            {
                var obList = new List<INV_ITEM_CATModel>();
                var ds = new DataSet();
                var ds1 = new DataSet();
                OraDatabase db = new OraDatabase();
                ds = db.ExecuteSQLStatement("select INV_ITEM_CAT_ID,ITEM_CAT_NAME_EN,ITEM_CAT_SNAME from INV_ITEM_CAT where PARENT_ID=" + pPARENT_ID + " and ITEM_CAT_CODE!='GM03' order by INV_ITEM_CAT_ID");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_CATModel ob = new INV_ITEM_CATModel();
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_CAT_SNAME = (dr["ITEM_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_SNAME"]);

                    ds1 = db.ExecuteSQLStatement("select X.ITEM_CAT_NAME_EN,X.INV_ITEM_CAT_ID, nvl2(y.GMT_PRDTYP_PRDLN_ID,'Y','N') AS IS_ACTIVE from INV_ITEM_CAT x left join GMT_PRDTYP_PRDLN y on X.INV_ITEM_CAT_ID = y.INV_ITEM_CAT_ID and y.HR_PROD_LINE_ID = " + pHR_PROD_LINE_ID + " where x.PARENT_ID=" + ob.INV_ITEM_CAT_ID + " and x.ITEM_CAT_CODE!='GM03' order by x.INV_ITEM_CAT_ID");

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        INV_ITEM_CATModel ob1 = new INV_ITEM_CATModel();
                        ob1.INV_ITEM_CAT_ID = (dr1["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_CAT_ID"]);
                        ob1.ITEM_CAT_NAME_EN = (dr1["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_CAT_NAME_EN"]);
                        ob1.IS_ACTIVE = (dr1["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["IS_ACTIVE"]);
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




        //Item List for other with Combo
        public object ItemByStyleID(Int64 MC_STYLE_ID)
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYLE_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEMModel ob = new MC_STYLE_ITEMModel();
                    ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_GRP_ID = (dr["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_GRP_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_EXT_NO = (dr["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_EXT_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.MATERIAL_SPEC = (dr["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MATERIAL_SPEC"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);

                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? "1" : Convert.ToString(dr["OPTION_NO"]);

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
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                    ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                    ob.KEY_IMAGE = (dr["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KEY_IMAGE"]);

                    ob.ITEM_GROUP_NAME = (dr["ITEM_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_GROUP_NAME"]);
                   



                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3003},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {

                        MC_STYLE_ITEMModel ob1 = new MC_STYLE_ITEMModel();
                        ob1.MC_STYLE_ITEM_ID = (dr1["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_ITEM_ID"]);

                        if (dr1["PARENT_ID"] != DBNull.Value)
                        {
                            ob1.PARENT_ID = Convert.ToInt64(dr1["PARENT_ID"]);
                        }

                        ob1.MC_STYLE_ID = (dr1["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_ID"]);
                        ob1.INV_ITEM_CAT_ID = (dr1["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_CAT_ID"]);
                        ob1.ITEM_GRP_ID = (dr1["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ITEM_GRP_ID"]);
                        ob1.ITEM_CODE = (dr1["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_CODE"]);
                        ob1.ITEM_EXT_NO = (dr1["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_EXT_NO"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.ITEM_SNAME = (dr1["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_SNAME"]);
                        ob1.MATERIAL_SPEC = (dr1["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MATERIAL_SPEC"]);
                        ob1.COMBO_NO = (dr1["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COMBO_NO"]);
                        ob1.ITEM_NAME = (dr1["ITEM_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME"]);

                        if (dr1["LK_GARM_TYPE_ID"] != DBNull.Value)
                        {
                            ob1.LK_GARM_TYPE_ID = Convert.ToInt64(dr1["LK_GARM_TYPE_ID"]);
                        }

                        if (dr1["LK_GARM_DEPT_ID"] != DBNull.Value)
                        {
                            ob1.LK_GARM_DEPT_ID = Convert.ToInt64(dr1["LK_GARM_DEPT_ID"]);
                        }

                        if (dr1["LK_SLV_TYPE_ID"] != DBNull.Value)
                        {
                            ob1.LK_SLV_TYPE_ID = Convert.ToInt64(dr1["LK_SLV_TYPE_ID"]);
                        }

                        if (dr1["LK_NECK_TYPE_ID"] != DBNull.Value)
                        {
                            ob1.LK_NECK_TYPE_ID = Convert.ToInt64(dr1["LK_NECK_TYPE_ID"]);
                        }

                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob1.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3004},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                            {
                                MC_STYLE_ITEMModel ob2 = new MC_STYLE_ITEMModel();
                                ob2.MC_STYLE_ITEM_ID = (dr2["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_STYLE_ITEM_ID"]);

                                if (dr2["PARENT_ID"] != DBNull.Value)
                                {
                                    ob2.PARENT_ID = Convert.ToInt64(dr2["PARENT_ID"]);
                                }


                                ob2.MC_STYL_FAB_COST_ID = (dr2["MC_STYL_FAB_COST_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr2["MC_STYL_FAB_COST_ID"]);

                                ob2.LK_FBR_GRP_ID = (dr2["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["LK_FBR_GRP_ID"]);
                                ob2.YC_DIA = (dr2["YC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["YC_DIA"]);


                                ob2.MC_STYLE_ID = (dr2["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_STYLE_ID"]);
                                ob2.INV_ITEM_CAT_ID = (dr2["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["INV_ITEM_CAT_ID"]);
                                ob2.ITEM_GRP_ID = (dr2["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["ITEM_GRP_ID"]);
                                ob2.ITEM_CODE = (dr2["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_CODE"]);
                                ob2.ITEM_EXT_NO = (dr2["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_EXT_NO"]);

                                ob2.GARM_PART = (dr2["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["GARM_PART"]);

                                ob2.ITEM_NAME_EN = (dr2["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_NAME_EN"]);
                                ob2.ITEM_SNAME = (dr2["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_SNAME"]);
                                ob2.MATERIAL_SPEC = (dr2["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MATERIAL_SPEC"]);

                                if (dr2["LK_GARM_TYPE_ID"] != DBNull.Value)
                                {
                                    ob2.LK_GARM_TYPE_ID = Convert.ToInt64(dr2["LK_GARM_TYPE_ID"]);
                                }

                                if (dr2["LK_GARM_DEPT_ID"] != DBNull.Value)
                                {
                                    ob2.LK_GARM_DEPT_ID = Convert.ToInt64(dr2["LK_GARM_DEPT_ID"]);
                                }

                                if (dr2["LK_SLV_TYPE_ID"] != DBNull.Value)
                                {
                                    ob2.LK_SLV_TYPE_ID = Convert.ToInt64(dr2["LK_SLV_TYPE_ID"]);
                                }

                                if (dr2["LK_NECK_TYPE_ID"] != DBNull.Value)
                                {
                                    ob2.LK_NECK_TYPE_ID = Convert.ToInt64(dr2["LK_NECK_TYPE_ID"]);
                                }
                                ob2.PCS_PER_PACK = (dr2["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PCS_PER_PACK"]);
                                ob2.CAL_PRICE = (dr2["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["CAL_PRICE"]);
                                ob2.QUOTE_PRICE = (dr2["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["QUOTE_PRICE"]);
                                ob2.LK_ITEM_STATUS_ID = (dr2["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["LK_ITEM_STATUS_ID"]);
                                ob2.KEY_IMAGE = (dr2["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["KEY_IMAGE"]);

                                ob2.FABRIC_CONS = (dr2["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["FABRIC_CONS"]);
                                ob2.FABRIC_RATE = (dr2["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["FABRIC_RATE"]);
                                ob2.FABRIC_DESC = (dr2["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["FABRIC_DESC"]);


                                ob1.items.Add(ob2);
                            }

                        }
                        else
                        {
                            MC_STYLE_ITEMModel ob3 = new MC_STYLE_ITEMModel();

                            ob3.MC_STYL_FAB_COST_ID = -1;
                            ob3.LK_FBR_GRP_ID = 192;
                            ob3.GARM_PART = "Body";
                            ob1.items.Add(ob3);
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


        public List<MC_STYLE_ITEMModel> ItemByStyleID2Level(Int64 MC_STYLE_ID)
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYLE_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEMModel ob = new MC_STYLE_ITEMModel();
                    ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_GRP_ID = (dr["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_GRP_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_EXT_NO = (dr["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_EXT_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.MATERIAL_SPEC = (dr["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MATERIAL_SPEC"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);

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
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                    ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                    ob.KEY_IMAGE = (dr["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KEY_IMAGE"]);

                    ob.ITEM_GROUP_NAME = (dr["ITEM_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_GROUP_NAME"]);


                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3004},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            MC_STYLE_ITEMModel ob2 = new MC_STYLE_ITEMModel();
                            ob2.MC_STYLE_ITEM_ID = (dr2["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_STYLE_ITEM_ID"]);

                            if (dr2["PARENT_ID"] != DBNull.Value)
                            {
                                ob2.PARENT_ID = Convert.ToInt64(dr2["PARENT_ID"]);
                            }


                            ob2.MC_STYL_FAB_COST_ID = (dr2["MC_STYL_FAB_COST_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr2["MC_STYL_FAB_COST_ID"]);

                            ob2.LK_FBR_GRP_ID = (dr2["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["LK_FBR_GRP_ID"]);
                            ob2.YC_DIA = (dr2["YC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["YC_DIA"]);



                            ob2.MC_STYLE_ID = (dr2["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_STYLE_ID"]);
                            ob2.INV_ITEM_CAT_ID = (dr2["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["INV_ITEM_CAT_ID"]);
                            ob2.ITEM_GRP_ID = (dr2["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["ITEM_GRP_ID"]);
                            ob2.ITEM_CODE = (dr2["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_CODE"]);
                            ob2.ITEM_EXT_NO = (dr2["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_EXT_NO"]);

                            ob2.GARM_PART = (dr2["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["GARM_PART"]);

                            ob2.ITEM_NAME_EN = (dr2["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_NAME_EN"]);
                            ob2.ITEM_SNAME = (dr2["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_SNAME"]);
                            ob2.MATERIAL_SPEC = (dr2["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MATERIAL_SPEC"]);

                            if (dr2["LK_GARM_TYPE_ID"] != DBNull.Value)
                            {
                                ob2.LK_GARM_TYPE_ID = Convert.ToInt64(dr2["LK_GARM_TYPE_ID"]);
                            }

                            if (dr2["LK_GARM_DEPT_ID"] != DBNull.Value)
                            {
                                ob2.LK_GARM_DEPT_ID = Convert.ToInt64(dr2["LK_GARM_DEPT_ID"]);
                            }

                            if (dr2["LK_SLV_TYPE_ID"] != DBNull.Value)
                            {
                                ob2.LK_SLV_TYPE_ID = Convert.ToInt64(dr2["LK_SLV_TYPE_ID"]);
                            }

                            if (dr2["LK_NECK_TYPE_ID"] != DBNull.Value)
                            {
                                ob2.LK_NECK_TYPE_ID = Convert.ToInt64(dr2["LK_NECK_TYPE_ID"]);
                            }
                            ob2.PCS_PER_PACK = (dr2["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PCS_PER_PACK"]);
                            ob2.CAL_PRICE = (dr2["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["CAL_PRICE"]);
                            ob2.QUOTE_PRICE = (dr2["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["QUOTE_PRICE"]);
                            ob2.LK_ITEM_STATUS_ID = (dr2["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["LK_ITEM_STATUS_ID"]);
                            ob2.KEY_IMAGE = (dr2["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["KEY_IMAGE"]);

                            ob2.FABRIC_CONS = (dr2["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["FABRIC_CONS"]);
                            ob2.FABRIC_RATE = (dr2["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["FABRIC_RATE"]);
                            ob2.FABRIC_DESC = (dr2["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["FABRIC_DESC"]);


                            ob.items.Add(ob2);
                        }

                    }
                    else
                    {
                        MC_STYLE_ITEMModel ob3 = new MC_STYLE_ITEMModel();

                        ob3.MC_STYL_FAB_COST_ID = -1;
                        ob3.GARM_PART = "Body";
                        ob3.LK_FBR_GRP_ID = 192;
                        ob.items.Add(ob3);
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



        //public object ItemByStyleID(Int64 MC_STYLE_ID)
        //{
        //    string sp = "pkg_merchandising.mc_style_item_select";
        //    try
        //    {
        //        var obList = new List<MC_STYLE_ITEMModel>();
        //        OraDatabase db = new OraDatabase();
        //        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {
        //             new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =MC_STYLE_ID},
        //             new CommandParameter() {ParameterName = "pOption", Value =3002},
        //             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
        //         }, sp);


        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            MC_STYLE_ITEMModel root = new MC_STYLE_ITEMModel();
        //            root.MC_STYLE_ITEM_ID = (ds.Tables[0].Rows[i]["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYLE_ITEM_ID"]);

        //            if (ds.Tables[0].Rows[i]["PARENT_ID"] != DBNull.Value)
        //            {
        //                root.PARENT_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
        //            }

        //            root.MC_STYLE_ID = (ds.Tables[0].Rows[i]["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYLE_ID"]);
        //            root.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);
        //            root.ITEM_GRP_ID = (ds.Tables[0].Rows[i]["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_GRP_ID"]);
        //            root.ITEM_CODE = (ds.Tables[0].Rows[i]["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CODE"]);
        //            root.BUYER_ITEM_NO = (ds.Tables[0].Rows[i]["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BUYER_ITEM_NO"]);
        //            root.ITEM_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_NAME_EN"]);
        //            root.ITEM_SNAME = (ds.Tables[0].Rows[i]["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_SNAME"]);
        //            root.MATERIAL_SPEC = (ds.Tables[0].Rows[i]["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MATERIAL_SPEC"]);
        //            root.ITEM_GROUP_NAME = (ds.Tables[0].Rows[i]["ITEM_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_GROUP_NAME"]);
        //            root.ITEM_NAME = (ds.Tables[0].Rows[i]["ITEM_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_NAME"]);

        //            if (ds.Tables[0].Rows[i]["LK_GARM_TYPE_ID"] != DBNull.Value)
        //            {
        //                root.LK_GARM_TYPE_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["LK_GARM_TYPE_ID"]);
        //            }

        //            if (ds.Tables[0].Rows[i]["LK_GARM_DEPT_ID"] != DBNull.Value)
        //            {
        //                root.LK_GARM_DEPT_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["LK_GARM_DEPT_ID"]);
        //            }

        //            if (ds.Tables[0].Rows[i]["LK_SLV_TYPE_ID"] != DBNull.Value)
        //            {
        //                root.LK_SLV_TYPE_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["LK_SLV_TYPE_ID"]);
        //            }

        //            if (ds.Tables[0].Rows[i]["LK_NECK_TYPE_ID"] != DBNull.Value)
        //            {
        //                root.LK_NECK_TYPE_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["LK_NECK_TYPE_ID"]);
        //            }
        //            root.PCS_PER_PACK = (ds.Tables[0].Rows[i]["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PCS_PER_PACK"]);
        //            root.CAL_PRICE = (ds.Tables[0].Rows[i]["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["CAL_PRICE"]);
        //            root.QUOTE_PRICE = (ds.Tables[0].Rows[i]["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["QUOTE_PRICE"]);
        //            root.LK_ITEM_STATUS_ID = (ds.Tables[0].Rows[i]["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["LK_ITEM_STATUS_ID"]);
        //            root.KEY_IMAGE = (ds.Tables[0].Rows[i]["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["KEY_IMAGE"]);
        //            CreateNode(root);
        //            obList.Add(root);
        //        }
        //      return obList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void CreateNode(MC_STYLE_ITEMModel node)
        //{
        //    string sp = "pkg_merchandising.mc_style_item_select";
        //    var obList = new List<MC_STYLE_ITEMModel>();
        //    OraDatabase db = new OraDatabase();
        //    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {
        //             new CommandParameter() {ParameterName = "pPARENT_ID", Value = node.MC_STYLE_ITEM_ID},
        //             new CommandParameter() {ParameterName = "pOption", Value =3003},
        //             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
        //         }, sp);

        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        return;
        //    }
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        MC_STYLE_ITEMModel tnode = new MC_STYLE_ITEMModel();
        //        tnode.MC_STYLE_ITEM_ID = (ds.Tables[0].Rows[i]["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYLE_ITEM_ID"]);

        //        if (ds.Tables[0].Rows[i]["PARENT_ID"] != DBNull.Value)
        //        {
        //            tnode.PARENT_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["PARENT_ID"]);
        //        }

        //        tnode.MC_STYLE_ID = (ds.Tables[0].Rows[i]["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYLE_ID"]);
        //        tnode.INV_ITEM_CAT_ID = (ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["INV_ITEM_CAT_ID"]);
        //        tnode.ITEM_GRP_ID = (ds.Tables[0].Rows[i]["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["ITEM_GRP_ID"]);
        //        tnode.ITEM_CODE = (ds.Tables[0].Rows[i]["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_CODE"]);
        //        tnode.BUYER_ITEM_NO = (ds.Tables[0].Rows[i]["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["BUYER_ITEM_NO"]);
        //        tnode.ITEM_NAME_EN = (ds.Tables[0].Rows[i]["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_NAME_EN"]);
        //        tnode.ITEM_SNAME = (ds.Tables[0].Rows[i]["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_SNAME"]);
        //        tnode.MATERIAL_SPEC = (ds.Tables[0].Rows[i]["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["MATERIAL_SPEC"]);
        //        tnode.ITEM_GROUP_NAME = (ds.Tables[0].Rows[i]["ITEM_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_GROUP_NAME"]);
        //        tnode.ITEM_NAME = (ds.Tables[0].Rows[i]["ITEM_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["ITEM_NAME"]);

        //        if (ds.Tables[0].Rows[i]["LK_GARM_TYPE_ID"] != DBNull.Value)
        //        {
        //            tnode.LK_GARM_TYPE_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["LK_GARM_TYPE_ID"]);
        //        }

        //        if (ds.Tables[0].Rows[i]["LK_GARM_DEPT_ID"] != DBNull.Value)
        //        {
        //            tnode.LK_GARM_DEPT_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["LK_GARM_DEPT_ID"]);
        //        }

        //        if (ds.Tables[0].Rows[i]["LK_SLV_TYPE_ID"] != DBNull.Value)
        //        {
        //            tnode.LK_SLV_TYPE_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["LK_SLV_TYPE_ID"]);
        //        }

        //        if (ds.Tables[0].Rows[i]["LK_NECK_TYPE_ID"] != DBNull.Value)
        //        {
        //            tnode.LK_NECK_TYPE_ID = Convert.ToInt64(ds.Tables[0].Rows[i]["LK_NECK_TYPE_ID"]);
        //        }
        //        tnode.PCS_PER_PACK = (ds.Tables[0].Rows[i]["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["PCS_PER_PACK"]);
        //        tnode.CAL_PRICE = (ds.Tables[0].Rows[i]["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["CAL_PRICE"]);
        //        tnode.QUOTE_PRICE = (ds.Tables[0].Rows[i]["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["QUOTE_PRICE"]);
        //        tnode.LK_ITEM_STATUS_ID = (ds.Tables[0].Rows[i]["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["LK_ITEM_STATUS_ID"]);
        //        tnode.KEY_IMAGE = (ds.Tables[0].Rows[i]["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["KEY_IMAGE"]);
        //        tnode.GARM_PART = (ds.Tables[0].Rows[i]["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["GARM_PART"]);
        //        tnode.FABRIC_DESC = (ds.Tables[0].Rows[i]["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(ds.Tables[0].Rows[i]["FABRIC_DESC"]);

        //        tnode.MC_STYL_FAB_COST_ID = (ds.Tables[0].Rows[i]["MC_STYL_FAB_COST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(ds.Tables[0].Rows[i]["MC_STYL_FAB_COST_ID"]);
        //        tnode.FABRIC_CONS = (ds.Tables[0].Rows[i]["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["FABRIC_CONS"]);
        //        tnode.FABRIC_RATE = (ds.Tables[0].Rows[i]["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["FABRIC_RATE"]);


        //        node.items.Add(tnode);
        //        CreateNode(tnode);
        //    }

        //}



        public string ITEM_NAME { get; set; }

        public string ITEM_GROUP_NAME { get; set; }
        public Int64? REMOVE { get; set; }





        public string saveStyleItemFabData()
        {
            const string sp = "pkg_merchandising.mc_style_item_update_1";
            Int64 V_MC_STYLE_ITEM_ID = 0;
            Int64 V_PARENT_ID = 0;
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value = ob.MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_GRP_ID", Value = ob.ITEM_GRP_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pITEM_EXT_NO", Value = ob.ITEM_EXT_NO},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob.ITEM_SNAME},
                     new CommandParameter() {ParameterName = "pMATERIAL_SPEC", Value = ob.MATERIAL_SPEC},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob.LK_GARM_DEPT_ID},
                     new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob.LK_SLV_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob.LK_NECK_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pCAL_PRICE", Value = ob.CAL_PRICE},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     new CommandParameter() {ParameterName = "pKEY_IMAGE", Value = ob.KEY_IMAGE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob.COMBO_NO},

                     new CommandParameter() {ParameterName = "pSEGMENT_FLAG", Value = ob.SEGMENT_FLAG},
                     new CommandParameter() {ParameterName = "pOPTION_NO", Value = ob.OPTION_NO},

                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pREMOVE", Value =ob.REMOVE},
                     new CommandParameter() {ParameterName = "V_MC_STYLE_ITEM_ID", Value =null, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    V_PARENT_ID = (dr["VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VALUE"]);
                }





                if (ob.items.Count > 0)
                   {
                        foreach (var ob1 in ob.items)
                        {

                            var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob1.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.MC_STYLE_ITEM_ID < 0  ?V_PARENT_ID:ob1.PARENT_ID},
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value = ob1.MC_STYLE_ID},
                                 new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob1.INV_ITEM_CAT_ID},
                                 new CommandParameter() {ParameterName = "pITEM_GRP_ID", Value = ob1.ITEM_GRP_ID},
                                 new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob1.ITEM_CODE},
                                 new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob1.COMBO_NO},
                                 new CommandParameter() {ParameterName = "pITEM_EXT_NO", Value = ob1.ITEM_EXT_NO},
                                 new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob1.ITEM_NAME_EN},
                                 new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob1.ITEM_SNAME},
                                 new CommandParameter() {ParameterName = "pMATERIAL_SPEC", Value = ob1.MATERIAL_SPEC},
                                 new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob1.LK_GARM_TYPE_ID},
                                 new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob1.LK_GARM_DEPT_ID},
                                 new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob1.LK_SLV_TYPE_ID},
                                 new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob1.LK_NECK_TYPE_ID},
                                 new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob1.PCS_PER_PACK},
                                 new CommandParameter() {ParameterName = "pCAL_PRICE", Value = ob1.CAL_PRICE},
                                 new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob1.QUOTE_PRICE},
                                 new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob1.LK_ITEM_STATUS_ID},
                                 new CommandParameter() {ParameterName = "pKEY_IMAGE", Value = ob1.KEY_IMAGE},

                                 new CommandParameter() {ParameterName = "pSEGMENT_FLAG", Value = ob1.SEGMENT_FLAG},
                                 new CommandParameter() {ParameterName = "pOPTION_NO", Value = ob1.OPTION_NO},


                                 new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                                 new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                                 new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                                 new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                                 new CommandParameter() {ParameterName = "pOption", Value =2000},
                                 new CommandParameter() {ParameterName = "pREMOVE", Value =ob1.REMOVE},
                                 
                                 new CommandParameter() {ParameterName = "V_MC_STYLE_ITEM_ID", Value =null, Direction = ParameterDirection.Output}
                             }, sp);



                            foreach (DataRow dr1 in ds1.Tables["OUTPARAM"].Rows)
                            {
                                V_MC_STYLE_ITEM_ID = (dr1["VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["VALUE"]);
                                //jsonStr = Convert.ToString(dr1["VALUE"]);

                            }

                            if (V_MC_STYLE_ITEM_ID > 0 && ob1.items.Count > 0)
                            {

                              foreach(var ob2 in ob1.items) {
                               var ds2= db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = V_MC_STYLE_ITEM_ID},                                         
                                         new CommandParameter() {ParameterName = "pMC_STYL_FAB_COST_ID", Value = ob2.MC_STYL_FAB_COST_ID},

                                         new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob2.LK_FBR_GRP_ID},
                                         new CommandParameter() {ParameterName = "pYC_DIA", Value = ob2.YC_DIA},

                                         new CommandParameter() {ParameterName = "pGARM_PART", Value = ob2.GARM_PART},
                                         new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob2.FABRIC_DESC},
                                         new CommandParameter() {ParameterName = "pITEM_GRP_ID", Value = ob2.ITEM_GRP_ID},
                                         new CommandParameter() {ParameterName = "pFABRIC_CONS", Value = ob2.FABRIC_CONS},
                                         new CommandParameter() {ParameterName = "pFABRIC_RATE", Value = ob2.FABRIC_RATE},
                                         new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob2.ITEM_NAME_EN},
                                         new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob2.ITEM_SNAME},
                                         new CommandParameter() {ParameterName = "pMATERIAL_SPEC", Value = ob2.MATERIAL_SPEC},
                                         new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob2.LK_GARM_TYPE_ID},
                                         new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob2.LK_GARM_DEPT_ID},
                                         new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob2.LK_SLV_TYPE_ID},
                                         new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob2.LK_NECK_TYPE_ID},
                                         new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob2.PCS_PER_PACK},
                                         new CommandParameter() {ParameterName = "pCAL_PRICE", Value = ob2.CAL_PRICE},
                                         new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob2.QUOTE_PRICE},
                                         new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob2.LK_ITEM_STATUS_ID},
                                         new CommandParameter() {ParameterName = "pKEY_IMAGE", Value = ob2.KEY_IMAGE},
                                         new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob2.CREATION_DATE},
                                         new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                                         new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                                         new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                                         new CommandParameter() {ParameterName = "pOption", Value =2001},
                                         new CommandParameter() {ParameterName = "pREMOVE", Value =ob2.REMOVE},
                                         new CommandParameter() {ParameterName = "V_MC_STYLE_ITEM_ID", Value =null, Direction = ParameterDirection.Output}
                                     }, sp);



                               foreach (DataRow dr2 in ds2.Tables["OUTPARAM"].Rows)
                                {
                                    //V_MC_STYLE_ITEM_ID = (dr1["VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["VALUE"]);
                                    jsonStr = Convert.ToString(dr2["VALUE"]);

                                }

                                }
                            }


                        }
                   }

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
        public string saveStyleItemFabData2L()
        {
            const string sp = "pkg_merchandising.mc_style_item_update_1";
            Int64 V_MC_STYLE_ITEM_ID = 0;
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value = ob.MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_GRP_ID", Value = ob.ITEM_GRP_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pITEM_EXT_NO", Value = ob.ITEM_EXT_NO},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob.ITEM_SNAME},
                     new CommandParameter() {ParameterName = "pMATERIAL_SPEC", Value = ob.MATERIAL_SPEC},
                     new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob.LK_GARM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob.LK_GARM_DEPT_ID},
                     new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob.LK_SLV_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob.LK_NECK_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pCAL_PRICE", Value = ob.CAL_PRICE},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     new CommandParameter() {ParameterName = "pKEY_IMAGE", Value = ob.KEY_IMAGE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pCOMBO_NO", Value = ob.COMBO_NO},

                     new CommandParameter() {ParameterName = "pSEGMENT_FLAG", Value = ob.SEGMENT_FLAG},
                     new CommandParameter() {ParameterName = "pOPTION_NO", Value = ob.OPTION_NO},

                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pREMOVE", Value =ob.REMOVE},
                     new CommandParameter() {ParameterName = "V_MC_STYLE_ITEM_ID", Value =null, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    V_MC_STYLE_ITEM_ID = (dr["VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VALUE"]);
                }


                if (V_MC_STYLE_ITEM_ID > 0 && ob.items.Count > 0)
                {

                    foreach (var ob2 in ob.items)
                    {
                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = V_MC_STYLE_ITEM_ID},                                         
                                         new CommandParameter() {ParameterName = "pMC_STYL_FAB_COST_ID", Value = ob2.MC_STYL_FAB_COST_ID},

                                         new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob2.LK_FBR_GRP_ID},
                                         new CommandParameter() {ParameterName = "pYC_DIA", Value = ob2.YC_DIA},

                                         new CommandParameter() {ParameterName = "pGARM_PART", Value = ob2.GARM_PART},
                                         new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob2.FABRIC_DESC},
                                         new CommandParameter() {ParameterName = "pITEM_GRP_ID", Value = ob2.ITEM_GRP_ID},
                                         new CommandParameter() {ParameterName = "pFABRIC_CONS", Value = ob2.FABRIC_CONS},
                                         new CommandParameter() {ParameterName = "pFABRIC_RATE", Value = ob2.FABRIC_RATE},
                                         new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob2.ITEM_NAME_EN},
                                         new CommandParameter() {ParameterName = "pITEM_SNAME", Value = ob2.ITEM_SNAME},
                                         new CommandParameter() {ParameterName = "pMATERIAL_SPEC", Value = ob2.MATERIAL_SPEC},
                                         new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = ob2.LK_GARM_TYPE_ID},
                                         new CommandParameter() {ParameterName = "pLK_GARM_DEPT_ID", Value = ob2.LK_GARM_DEPT_ID},
                                         new CommandParameter() {ParameterName = "pLK_SLV_TYPE_ID", Value = ob2.LK_SLV_TYPE_ID},
                                         new CommandParameter() {ParameterName = "pLK_NECK_TYPE_ID", Value = ob2.LK_NECK_TYPE_ID},
                                         new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob2.PCS_PER_PACK},
                                         new CommandParameter() {ParameterName = "pCAL_PRICE", Value = ob2.CAL_PRICE},
                                         new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob2.QUOTE_PRICE},
                                         new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob2.LK_ITEM_STATUS_ID},
                                         new CommandParameter() {ParameterName = "pKEY_IMAGE", Value = ob2.KEY_IMAGE},
                                         new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob2.CREATION_DATE},
                                         new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                                         new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                                         new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                                         new CommandParameter() {ParameterName = "pOption", Value =2001},
                                         new CommandParameter() {ParameterName = "pREMOVE", Value =ob2.REMOVE},
                                         new CommandParameter() {ParameterName = "V_MC_STYLE_ITEM_ID", Value =null, Direction = ParameterDirection.Output}
                                     }, sp);



                        foreach (DataRow dr2 in ds2.Tables["OUTPARAM"].Rows)
                        {
                            //V_MC_STYLE_ITEM_ID = (dr1["VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["VALUE"]);
                            jsonStr = Convert.ToString(dr2["VALUE"]);

                        }

                    }
                }




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
        public List<MC_STYLE_ITEMModel> ItemByStyleIdForSetCombo(long ID)
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYLE_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEMModel ob = new MC_STYLE_ITEMModel();
                    ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_GRP_ID = (dr["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_GRP_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_EXT_NO = (dr["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_EXT_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.MATERIAL_SPEC = (dr["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MATERIAL_SPEC"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);

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
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                    ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                    ob.KEY_IMAGE = (dr["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KEY_IMAGE"]);

                    ob.ITEM_GROUP_NAME = (dr["ITEM_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_GROUP_NAME"]);




                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3003},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {

                        MC_STYLE_ITEMModel ob1 = new MC_STYLE_ITEMModel();
                        ob1.MC_STYLE_ITEM_ID = (dr1["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_ITEM_ID"]);

                        if (dr1["PARENT_ID"] != DBNull.Value)
                        {
                            ob1.PARENT_ID = Convert.ToInt64(dr1["PARENT_ID"]);
                        }

                        ob1.MC_STYLE_ID = (dr1["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_ID"]);
                        ob1.INV_ITEM_CAT_ID = (dr1["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["INV_ITEM_CAT_ID"]);
                        ob1.ITEM_GRP_ID = (dr1["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ITEM_GRP_ID"]);
                        ob1.ITEM_CODE = (dr1["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_CODE"]);
                        ob1.ITEM_EXT_NO = (dr1["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_EXT_NO"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.ITEM_SNAME = (dr1["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_SNAME"]);
                        ob1.MATERIAL_SPEC = (dr1["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MATERIAL_SPEC"]);
                        ob1.COMBO_NO = (dr1["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COMBO_NO"]);

                        ob1.OPTION_NO = (dr1["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["OPTION_NO"]);

                        ob1.ITEM_NAME = (dr1["ITEM_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME"]);

                        if (dr1["LK_GARM_TYPE_ID"] != DBNull.Value)
                        {
                            ob1.LK_GARM_TYPE_ID = Convert.ToInt64(dr1["LK_GARM_TYPE_ID"]);
                        }

                        if (dr1["LK_GARM_DEPT_ID"] != DBNull.Value)
                        {
                            ob1.LK_GARM_DEPT_ID = Convert.ToInt64(dr1["LK_GARM_DEPT_ID"]);
                        }

                        if (dr1["LK_SLV_TYPE_ID"] != DBNull.Value)
                        {
                            ob1.LK_SLV_TYPE_ID = Convert.ToInt64(dr1["LK_SLV_TYPE_ID"]);
                        }

                        if (dr1["LK_NECK_TYPE_ID"] != DBNull.Value)
                        {
                            ob1.LK_NECK_TYPE_ID = Convert.ToInt64(dr1["LK_NECK_TYPE_ID"]);
                        }

                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob1.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3004},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in ds2.Tables[0].Rows)
                            {
                                MC_STYLE_ITEMModel ob2 = new MC_STYLE_ITEMModel();
                                ob2.MC_STYLE_ITEM_ID = (dr2["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_STYLE_ITEM_ID"]);

                                if (dr2["PARENT_ID"] != DBNull.Value)
                                {
                                    ob2.PARENT_ID = Convert.ToInt64(dr2["PARENT_ID"]);
                                }


                                ob2.MC_STYL_FAB_COST_ID = (dr2["MC_STYL_FAB_COST_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr2["MC_STYL_FAB_COST_ID"]);

                                ob2.LK_FBR_GRP_ID = (dr2["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["LK_FBR_GRP_ID"]);
                                ob2.YC_DIA = (dr2["YC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["YC_DIA"]);


                                ob2.MC_STYLE_ID = (dr2["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["MC_STYLE_ID"]);
                                ob2.INV_ITEM_CAT_ID = (dr2["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["INV_ITEM_CAT_ID"]);
                                ob2.ITEM_GRP_ID = (dr2["ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["ITEM_GRP_ID"]);
                                ob2.ITEM_CODE = (dr2["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_CODE"]);
                                ob2.ITEM_EXT_NO = (dr2["ITEM_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_EXT_NO"]);

                                ob2.GARM_PART = (dr2["GARM_PART"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["GARM_PART"]);

                                ob2.ITEM_NAME_EN = (dr2["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_NAME_EN"]);
                                ob2.ITEM_SNAME = (dr2["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["ITEM_SNAME"]);
                                ob2.MATERIAL_SPEC = (dr2["MATERIAL_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["MATERIAL_SPEC"]);

                                if (dr2["LK_GARM_TYPE_ID"] != DBNull.Value)
                                {
                                    ob2.LK_GARM_TYPE_ID = Convert.ToInt64(dr2["LK_GARM_TYPE_ID"]);
                                }

                                if (dr2["LK_GARM_DEPT_ID"] != DBNull.Value)
                                {
                                    ob2.LK_GARM_DEPT_ID = Convert.ToInt64(dr2["LK_GARM_DEPT_ID"]);
                                }

                                if (dr2["LK_SLV_TYPE_ID"] != DBNull.Value)
                                {
                                    ob2.LK_SLV_TYPE_ID = Convert.ToInt64(dr2["LK_SLV_TYPE_ID"]);
                                }

                                if (dr2["LK_NECK_TYPE_ID"] != DBNull.Value)
                                {
                                    ob2.LK_NECK_TYPE_ID = Convert.ToInt64(dr2["LK_NECK_TYPE_ID"]);
                                }
                                ob2.PCS_PER_PACK = (dr2["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["PCS_PER_PACK"]);
                                ob2.CAL_PRICE = (dr2["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["CAL_PRICE"]);
                                ob2.QUOTE_PRICE = (dr2["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["QUOTE_PRICE"]);
                                ob2.LK_ITEM_STATUS_ID = (dr2["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr2["LK_ITEM_STATUS_ID"]);
                                ob2.KEY_IMAGE = (dr2["KEY_IMAGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["KEY_IMAGE"]);

                                ob2.FABRIC_CONS = (dr2["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["FABRIC_CONS"]);
                                ob2.FABRIC_RATE = (dr2["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr2["FABRIC_RATE"]);
                                ob2.FABRIC_DESC = (dr2["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr2["FABRIC_DESC"]);


                                ob1.items.Add(ob2);
                            }

                        }
                        else
                        {
                            MC_STYLE_ITEMModel ob3 = new MC_STYLE_ITEMModel();

                            ob3.MC_STYL_FAB_COST_ID = -1;
                            ob3.GARM_PART = "Body";
                            ob3.LK_FBR_GRP_ID = 192;
                            ob1.items.Add(ob3);
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






        public List<MC_STYLE_ITEMModel> ItemListForSetWithNoCombo(Int64 MC_STYLE_ID)
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYLE_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3010},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEMModel ob = new MC_STYLE_ITEMModel();
                    ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.FAB_COST = (dr["FAB_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_COST"]);
                    ob.TRIM_ACC = (dr["TRIM_ACC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TRIM_ACC"]);
                    ob.PRINT_COST = (dr["PRINT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PRINT_COST"]);
                    ob.CM = (dr["CM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CM"]);
                    ob.COMMERCIAL = (dr["COMMERCIAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COMMERCIAL"]);
                    ob.FOB = (dr["FOB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FOB"]);
                    ob.SERV = (dr["SERV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SERV"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3011},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {

                        MC_STYLE_ITEMModel ob1 = new MC_STYLE_ITEMModel();
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.FABRIC_DESC = (dr1["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["FABRIC_DESC"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.FABRIC_CONS = (dr1["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FABRIC_CONS"]);
                        ob1.FABRIC_RATE = (dr1["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FABRIC_RATE"]);
                        ob1.FAB_COST_D = (dr1["FAB_COST_D"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FAB_COST_D"]);
                        ob1.YC_DIA = (dr1["YC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["YC_DIA"]);

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
        public List<MC_STYLE_ITEMModel> ItemListForSetWithComboOpt(Int64 MC_STYLE_ID)
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYLE_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =MC_STYLE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3014},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEMModel ob = new MC_STYLE_ITEMModel();
                    ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);

                    ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.FAB_COST = (dr["FAB_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_COST"]);
                    ob.TRIM_ACC = (dr["TRIM_ACC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TRIM_ACC"]);
                    ob.PRINT_COST = (dr["PRINT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PRINT_COST"]);
                    ob.CM = (dr["CM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CM"]);
                    ob.COMMERCIAL = (dr["COMMERCIAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COMMERCIAL"]);
                    ob.FOB = (dr["FOB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FOB"]);
                    ob.SERV = (dr["SERV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SERV"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3011},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {

                        MC_STYLE_ITEMModel ob1 = new MC_STYLE_ITEMModel();
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.FABRIC_DESC = (dr1["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["FABRIC_DESC"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.FABRIC_CONS = (dr1["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FABRIC_CONS"]);
                        ob1.FABRIC_RATE = (dr1["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FABRIC_RATE"]);
                        ob1.FAB_COST_D = (dr1["FAB_COST_D"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FAB_COST_D"]);
                        ob1.YC_DIA = (dr1["YC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["YC_DIA"]);

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
        public List<MC_STYLE_ITEMModel> ItemListForOtherWithNoCombo(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYLE_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3012},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEMModel ob = new MC_STYLE_ITEMModel();
                    ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.FAB_COST = (dr["FAB_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_COST"]);
                    ob.TRIM_ACC = (dr["TRIM_ACC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TRIM_ACC"]);
                    ob.PRINT_COST = (dr["PRINT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PRINT_COST"]);
                    ob.CM = (dr["CM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CM"]);
                    ob.COMMERCIAL = (dr["COMMERCIAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COMMERCIAL"]);
                    ob.FOB = (dr["FOB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FOB"]);
                    ob.SERV = (dr["SERV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SERV"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YC_DIA = (dr["YC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YC_DIA"]);
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

        public object ItemListForSetWithCombo(long ID)
        {
            string sp = "pkg_merchandising.mc_style_item_select";
            try
            {
                var obList = new List<MC_STYLE_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3016},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEMModel ob = new MC_STYLE_ITEMModel();
                    ob.MC_STYLE_ITEM_ID = (dr["MC_STYLE_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);

                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);


                    ob.CAL_PRICE = (dr["CAL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.FAB_COST = (dr["FAB_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_COST"]);
                    ob.TRIM_ACC = (dr["TRIM_ACC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TRIM_ACC"]);
                    ob.PRINT_COST = (dr["PRINT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PRINT_COST"]);
                    ob.CM = (dr["CM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CM"]);
                    ob.COMMERCIAL = (dr["COMMERCIAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COMMERCIAL"]);
                    ob.FOB = (dr["FOB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FOB"]);
                    ob.SERV = (dr["SERV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SERV"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {
                                 new CommandParameter() {ParameterName = "pMC_STYLE_ITEM_ID", Value = ob.MC_STYLE_ITEM_ID},
                                 new CommandParameter() {ParameterName = "pOption", Value =3011},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                             }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {

                        MC_STYLE_ITEMModel ob1 = new MC_STYLE_ITEMModel();
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.FABRIC_DESC = (dr1["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["FABRIC_DESC"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.FABRIC_CONS = (dr1["FABRIC_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FABRIC_CONS"]);
                        ob1.FABRIC_RATE = (dr1["FABRIC_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FABRIC_RATE"]);
                        ob1.FAB_COST_D = (dr1["FAB_COST_D"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["FAB_COST_D"]);
                        ob1.YC_DIA = (dr1["YC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["YC_DIA"]);

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
    }
}