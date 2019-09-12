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
    public class INV_ITEM_YRN_SPECModel
    {
        public Int64 INV_ITEM_YRN_SPEC_ID { get; set; }

        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public Int64 RF_YRN_CNT_ID { get; set; }
        public Int64? LK_SPN_PRCS_ID { get; set; }
        public Int64? LK_COTN_TYPE_ID { get; set; }
        public string IS_SLUB { get; set; }
        public string IS_MELLANGE { get; set; }
        public Int64? RF_YR_CAT_ID { get; set; }
        public string FANCY_DESC { get; set; }
        public string YR_SPEC_DESC { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string FIB_COMP_NAME { get; set; }
        //public string YR_CAT_NAME { get; set; }
        public string YR_COUNT_NO { get; set; }
        public string SPN_PRCS { get; set; }
        public string COTN_TYPE { get; set; }
        public string ITEM_CODE { get; set; }

        public string Save()
        {
            const string sp = "pkg_inventory.inv_item_yrn_spec_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_YRN_SPEC_ID", Value = ob.INV_ITEM_YRN_SPEC_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                     new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                     new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pRF_YR_CAT_ID", Value = ob.RF_YR_CAT_ID},
                     new CommandParameter() {ParameterName = "pFANCY_DESC", Value = ob.FANCY_DESC},
                     new CommandParameter() {ParameterName = "pYR_SPEC_DESC", Value = ob.YR_SPEC_DESC},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string Update()
        {
            const string sp = "pkg_inventory.inv_item_yrn_spec_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_YRN_SPEC_ID", Value = ob.INV_ITEM_YRN_SPEC_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                     new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                     new CommandParameter() {ParameterName = "pRF_YR_CAT_ID", Value = ob.RF_YR_CAT_ID},
                     new CommandParameter() {ParameterName = "pFANCY_DESC", Value = ob.FANCY_DESC},
                     new CommandParameter() {ParameterName = "pYR_SPEC_DESC", Value = ob.YR_SPEC_DESC},
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
                    i++;
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
            const string sp = "pkg_inventory.inv_item_yrn_spec_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_YRN_SPEC_ID", Value = ob.INV_ITEM_YRN_SPEC_ID},
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
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<INV_ITEM_YRN_SPECModel> SelectAll()
        {
            string sp = "pkg_inventory.inv_item_yrn_spec_select";
            try
            {
                var obList = new List<INV_ITEM_YRN_SPECModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_YRN_SPEC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEM_YRN_SPECModel ob = new INV_ITEM_YRN_SPECModel();
                    ob.INV_ITEM_YRN_SPEC_ID = (dr["INV_ITEM_YRN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_YRN_SPEC_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_COTN_TYPE_ID = Convert.ToInt64(dr["LK_COTN_TYPE_ID"]);
                    }

                    if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                    {
                        ob.LK_SPN_PRCS_ID = Convert.ToInt64(dr["LK_SPN_PRCS_ID"]);
                    }

                    if (dr["RF_YRN_CNT_ID"] != DBNull.Value)
                    {
                        ob.RF_YRN_CNT_ID = Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                    }

                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);
                    ob.IS_MELLANGE = (dr["IS_MELLANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MELLANGE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    if (dr["RF_YR_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_YR_CAT_ID = Convert.ToInt64(dr["RF_YR_CAT_ID"]);
                    }

                    ob.FANCY_DESC = (dr["FANCY_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FANCY_DESC"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);

                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    //ob.YR_CAT_NAME = (dr["YR_CAT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_CAT_NAME"]);
                    ob.YR_COUNT_NO = (dr["YR_COUNT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_NO"]);
                    ob.SPN_PRCS = (dr["SPN_PRCS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SPN_PRCS"]);
                    ob.COTN_TYPE = (dr["COTN_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COTN_TYPE"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public INV_ITEM_YRN_SPECModel Select(long ID)
        {
            string sp = "pkg_inventory.inv_item_yrn_spec_select";
            try
            {
                var ob = new INV_ITEM_YRN_SPECModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_YRN_SPEC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.INV_ITEM_YRN_SPEC_ID = (dr["INV_ITEM_YRN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_YRN_SPEC_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_COTN_TYPE_ID = Convert.ToInt64(dr["LK_COTN_TYPE_ID"]);
                    }

                    if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                    {
                        ob.LK_SPN_PRCS_ID = Convert.ToInt64(dr["LK_SPN_PRCS_ID"]);
                    }

                    if (dr["RF_YRN_CNT_ID"] != DBNull.Value)
                    {
                        ob.RF_YRN_CNT_ID = Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                    }

                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);
                    ob.IS_MELLANGE = (dr["IS_MELLANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MELLANGE"]);
                    //ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    if (dr["RF_YR_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_YR_CAT_ID = Convert.ToInt64(dr["RF_YR_CAT_ID"]);
                    }
                    ob.FANCY_DESC = (dr["FANCY_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FANCY_DESC"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
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

        public string YrnParamForFabrication(Int64 RF_FIB_COMP_ID)
        {
            const string sp = "pkg_inventory.yrn_param_fabri_select";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},

                     new CommandParameter() {ParameterName = "opLK_SPN_PRCS_ID_LST", Value ="", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opLK_COTN_TYPE_ID_LST", Value ="", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opIS_SLUB_LST", Value ="", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opIS_MELLANGE_LST", Value ="", Direction = ParameterDirection.Output},                  
                     new CommandParameter() {ParameterName = "pMsg", Value ="", Direction = ParameterDirection.Output}

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


    public class YRN_SPEC_DtlModel
    {
        public long? INV_ITEM_YRN_SPEC_ID { get; set; }
        public long? INV_ITEM_ID { get; set; }
        public long? YARN_ITEM_ID { get; set; }
        public string YARN_DETAIL { get; set; }
        public string YR_SPEC_DESC { get; set; }
        public string YR_COUNT_NO { get; set; }
        public long? KNT_YRN_LOT_ID { get; set; }
        public string YRN_LOT_NO { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public int? STITCH_LEN { get; set; }


        public List<YRN_SPEC_DtlModel> GetYarnListByProdOrdHdr(long? pMC_FAB_PROD_ORD_H_ID = null)
        {
            string sp = "pkg_inventory.inv_item_yrn_spec_select";
            try
            {
                var obList = new List<YRN_SPEC_DtlModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    YRN_SPEC_DtlModel ob = new YRN_SPEC_DtlModel();
                    ob.INV_ITEM_YRN_SPEC_ID = (dr["INV_ITEM_YRN_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_YRN_SPEC_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.YARN_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    ob.YARN_DETAIL = (dr["YARN_DETAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAIL"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
                    ob.YR_COUNT_NO = (dr["YR_COUNT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_NO"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.STITCH_LEN = (dr["STITCH_LEN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STITCH_LEN"]);
                    
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