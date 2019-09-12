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
    public class RF_FAB_TYPEModel
    {
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public string FAB_TYPE_CODE { get; set; }
        public string FAB_TYPE_NAME { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_FLAT_CIR { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }

        public string LK_YFAB_PART_LST { get; set; }
        public string IS_FBP_VISIBLE { get; set; }
        public string IS_DRP_NDL { get; set; }
        public string RF_GARM_PART_LST { get; set; }




        public string Save()
        {
            const string sp = "pkg_common.rf_fab_type_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFAB_TYPE_CODE", Value = ob.FAB_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pFAB_TYPE_NAME", Value = ob.FAB_TYPE_NAME},
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
            const string sp = "pkg_common.rf_fab_type_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFAB_TYPE_CODE", Value = ob.FAB_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pFAB_TYPE_NAME", Value = ob.FAB_TYPE_NAME},
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
            const string sp = "pkg_common.rf_fab_type_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
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

        public List<RF_FAB_TYPEModel> SelectAll(Int64? pINV_ITEM_CAT_ID, string pIS_FLAT_CIR)
        {
            string sp = "pkg_common.rf_fab_type_select";
            try
            {
                var obList = new List<RF_FAB_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pIS_FLAT_CIR", Value =pIS_FLAT_CIR},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FAB_TYPEModel ob = new RF_FAB_TYPEModel();
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FAB_TYPE_CODE = (dr["FAB_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_CODE"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.IS_FBP_VISIBLE = (dr["IS_FBP_VISIBLE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FBP_VISIBLE"]);
                    ob.LK_YFAB_PART_LST = (dr["LK_YFAB_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YFAB_PART_LST"]);
                    ob.IS_DRP_NDL = (dr["IS_DRP_NDL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DRP_NDL"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_FAB_TYPEModel> getFabTypeByFabProdOrder(String pMC_FAB_PROD_ORD_H_LST)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            try
            {
                var obList = new List<RF_FAB_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {    
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value =pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3010},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FAB_TYPEModel ob = new RF_FAB_TYPEModel();
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.IS_FBP_VISIBLE = (dr["IS_FBP_VISIBLE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FBP_VISIBLE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public RF_FAB_TYPEModel Select(long ID)
        {
            string sp = "pkg_common.rf_fab_type_select";
            try
            {
                var ob = new RF_FAB_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FAB_TYPE_CODE = (dr["FAB_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_CODE"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);

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