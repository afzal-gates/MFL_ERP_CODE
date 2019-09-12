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
    public class MC_FAB_PROD_H_BOMModel
    {
        public Int64 MC_FAB_PROD_H_BOM_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 MC_STYL_BGT_H_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal RQD_QTY { get; set; }
        public Decimal REV_QTY { get; set; }
        public Decimal ADDL_QTY { get; set; }
        public Decimal NET_QTY { get; set; }
        public Decimal BGT_RATE { get; set; }
        public Int64 ACCT_MOU_ID { get; set; }
        public Int64 CONS_MOU_ID { get; set; }
        public Int64 PURCH_MOU_ID { get; set; }
        public Int64 RF_ITEM_SRC_TYPE_ID { get; set; }
        public string IS_PO_CREATED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_FAB_PROD_H_BOM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value = ob.MC_FAB_PROD_H_BOM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = ob.MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pREV_QTY", Value = ob.REV_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pNET_QTY", Value = ob.NET_QTY},
                     new CommandParameter() {ParameterName = "pBGT_RATE", Value = ob.BGT_RATE},
                     new CommandParameter() {ParameterName = "pACCT_MOU_ID", Value = ob.ACCT_MOU_ID},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pRF_ITEM_SRC_TYPE_ID", Value = ob.RF_ITEM_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_PO_CREATED", Value = ob.IS_PO_CREATED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_MC_FAB_PROD_H_BOM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value = ob.MC_FAB_PROD_H_BOM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = ob.MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pREV_QTY", Value = ob.REV_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pNET_QTY", Value = ob.NET_QTY},
                     new CommandParameter() {ParameterName = "pBGT_RATE", Value = ob.BGT_RATE},
                     new CommandParameter() {ParameterName = "pACCT_MOU_ID", Value = ob.ACCT_MOU_ID},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pRF_ITEM_SRC_TYPE_ID", Value = ob.RF_ITEM_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_PO_CREATED", Value = ob.IS_PO_CREATED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_MC_FAB_PROD_H_BOM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value = ob.MC_FAB_PROD_H_BOM_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = ob.MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pREV_QTY", Value = ob.REV_QTY},
                     new CommandParameter() {ParameterName = "pADDL_QTY", Value = ob.ADDL_QTY},
                     new CommandParameter() {ParameterName = "pNET_QTY", Value = ob.NET_QTY},
                     new CommandParameter() {ParameterName = "pBGT_RATE", Value = ob.BGT_RATE},
                     new CommandParameter() {ParameterName = "pACCT_MOU_ID", Value = ob.ACCT_MOU_ID},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pRF_ITEM_SRC_TYPE_ID", Value = ob.RF_ITEM_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_PO_CREATED", Value = ob.IS_PO_CREATED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<MC_FAB_PROD_H_BOMModel> SelectAll()
        {
            string sp = "Select_MC_FAB_PROD_H_BOM";
            try
            {
                var obList = new List<MC_FAB_PROD_H_BOMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_H_BOMModel ob = new MC_FAB_PROD_H_BOMModel();
                    ob.MC_FAB_PROD_H_BOM_ID = (dr["MC_FAB_PROD_H_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_H_BOM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.REV_QTY = (dr["REV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADDL_QTY"]);
                    ob.NET_QTY = (dr["NET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_QTY"]);
                    ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                    ob.ACCT_MOU_ID = (dr["ACCT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCT_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.RF_ITEM_SRC_TYPE_ID = (dr["RF_ITEM_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ITEM_SRC_TYPE_ID"]);
                    ob.IS_PO_CREATED = (dr["IS_PO_CREATED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PO_CREATED"]);
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

        public MC_FAB_PROD_H_BOMModel Select(long ID)
        {
            string sp = "Select_MC_FAB_PROD_H_BOM";
            try
            {
                var ob = new MC_FAB_PROD_H_BOMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_FAB_PROD_H_BOM_ID = (dr["MC_FAB_PROD_H_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_H_BOM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.REV_QTY = (dr["REV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADDL_QTY"]);
                    ob.NET_QTY = (dr["NET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_QTY"]);
                    ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                    ob.ACCT_MOU_ID = (dr["ACCT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCT_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.RF_ITEM_SRC_TYPE_ID = (dr["RF_ITEM_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ITEM_SRC_TYPE_ID"]);
                    ob.IS_PO_CREATED = (dr["IS_PO_CREATED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PO_CREATED"]);
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



        public List<MC_FAB_PROD_H_BOMModel> BOMListForAccBkByID(Int64? pMC_STYLE_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_budget_sheet.mc_style_d_bom_select";
            try
            {
                var obList = new List<MC_FAB_PROD_H_BOMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_H_BOMModel ob = new MC_FAB_PROD_H_BOMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ////ob.MC_STYLE_D_BOM_ID = (dr["MC_STYLE_D_BOM_ID"] == DBNull.Value) ? 1 : Convert.ToInt64(dr["MC_STYLE_D_BOM_ID"]);
                    //ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    //ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    //ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    //ob.CONV_FACTOR = (dr["CONV_FACTOR"] == DBNull.Value) ? 1 : Convert.ToDecimal(dr["CONV_FACTOR"]);
                    //ob.UNIT_QTY_REQ = (dr["UNIT_QTY_REQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["UNIT_QTY_REQ"]);
                    //ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    //ob.SUGGESTED = (dr["SUGGESTED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUGGESTED"]);

                    ob.MC_FAB_PROD_H_BOM_ID = (dr["MC_FAB_PROD_H_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_H_BOM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.REV_QTY = (dr["REV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADDL_QTY"]);
                    ob.NET_QTY = (dr["NET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_QTY"]);
                    ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                    ob.ACCT_MOU_ID = (dr["ACCT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCT_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.RF_ITEM_SRC_TYPE_ID = (dr["RF_ITEM_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ITEM_SRC_TYPE_ID"]);
                    ob.IS_PO_CREATED = (dr["IS_PO_CREATED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PO_CREATED"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_FAB_PROD_H_BOMModel> QueryAccessoriesBooking(string pBLK_BOM_LIST = null, Int64? pBLK_BOM_ACT=null, Int64? pMC_FAB_PROD_ORD_H_ID = null,Int64? pSCM_PURC_REQ_H_ID = null)
        {
            string sp = "pkg_budget_sheet.mc_style_blk_bom_select";
            try
            {
                var obList = new List<MC_FAB_PROD_H_BOMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pBLK_BOM_LIST", Value = pBLK_BOM_LIST },
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID },
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = pSCM_PURC_REQ_H_ID },                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3002 },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_H_BOMModel ob = new MC_FAB_PROD_H_BOMModel();


                    ob.MC_FAB_PROD_H_BOM_ID = (dr["MC_FAB_PROD_H_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_H_BOM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.SCM_PURC_REQ_D_ID = (dr["SCM_PURC_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);


                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    //ob.QTY_EST = (dr["QTY_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_EST"]);
                    //ob.PCT_ADD_QTY = (dr["PCT_ADD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ADD_QTY"]);
                    //ob.RATE_EST = (dr["RATE_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_EST"]);

                    ob.ACCT_MOU_ID = (dr["ACCT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCT_MOU_ID"]);
                    ob.ACCT_MOU_NAME = (dr["ACCT_MOU_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCT_MOU_NAME"]);

                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.PURCH_MOU_NAME = (dr["PURCH_MOU_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURCH_MOU_NAME"]);

                    //ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.IS_TAB_ACT = (pBLK_BOM_ACT ?? 0) == ob.INV_ITEM_ID;
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_ACCS_PO_D_ITEM_ID = (dr["MC_ACCS_PO_D_ITEM_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_ACCS_PO_D_ITEM_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.MC_ACCS_PO_TMPLT_H_ID = (dr["MC_ACCS_PO_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_H_ID"]);
                    ob.MC_ACCS_PO_TMPLT_D_ID = (dr["MC_ACCS_PO_TMPLT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_TMPLT_D_ID"]);
                    

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORD_MOU_CODE = (dr["ORD_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_MOU_CODE"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);

                    if (dr["ITEM_CAT_NAME_EN"] != DBNull.Value)
                        ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    if (dr["BUYER_NAME_EN"] != DBNull.Value)
                        ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.IS_SIZE_COLOR = (dr["IS_SIZE_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SIZE_COLOR"]);

                    //ob.ITEM_SPEC_LIST = new MC_ACCS_PO_D_SPECModel().Query(ob.MC_ACCS_PO_D_ITEM_ID);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ITEM_NAME_EN { get; set; }
        public string SUGGESTED { get; set; }
        public string XML { get; set; }
        public decimal PURCH_PRICE { get; set; }
        public decimal IMP_PRICE { get; set; }
        public decimal CONV_FACTOR { get; set; }
        public Int64 UNIT_QTY_REQ { get; set; }
        public Int64? LK_FAB_QTY_SRC { get; set; }
        public Boolean IS_TAB_ACT { get; set; }
        public string ACCT_MOU_NAME { get; set; }
        public string PURCH_MOU_NAME { get; set; }
        public long? MC_BUYER_ID { get; set; }
        public Int64 MC_ACCS_PO_D_ITEM_ID { get; set; }

        
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string ORD_MOU_CODE { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public Int64 BOOKED_QTY { get; set; }

        public string ITEM_CAT_NAME_EN { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public long SCM_PURC_REQ_D_ID { get; set; }

        public string IS_SIZE_COLOR { get; set; }

        public long MC_ACCS_PO_TMPLT_H_ID { get; set; }

        public long MC_ACCS_PO_TMPLT_D_ID { get; set; }
    }
}