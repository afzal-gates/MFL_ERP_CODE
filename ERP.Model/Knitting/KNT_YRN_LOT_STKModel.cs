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
    public class KNT_YRN_LOT_STKModel
    {
        public Int64 KNT_YRN_LOT_STK_ID { get; set; }
        public Int64 KNT_YRN_RCV_H_ID { get; set; }
        public Int64 KNT_SC_YRN_RCV_H_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Decimal ISSUE_QTY { get; set; }
        public Decimal ALOC_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal COST_PRICE { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public string IS_TEST_OK { get; set; }
        public string IS_SUB_CONTR { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public long LK_YRN_COLR_GRP_ID { get; set; }
        public long PACK_MOU_ID { get; set; }
        public long RF_BRAND_ID { get; set; }
        public decimal QTY_PER_PACK { get; set; }
        public string YRN_LOT_NO { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string YRN_COLR_GRP { get; set; }
        public string MOU_CODE { get; set; }
        public Decimal STOCK_QTY { get; set; }


        public string YR_COUNT_NO { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public decimal ADJ_QTY { get; set; }
        public string QTY_MOU_CODE { get; set; }
        public string REMARKS { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string SPN_PROCESS_NAME { get; set; }
        public string COTTON_TYPE_NAME { get; set; }
        public string IS_SLUB { get; set; }
        public string IS_MELLANGE { get; set; }



        public string Save()
        {
            const string sp = "SP_KNT_YRN_LOT_STK";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_STK_ID", Value = ob.KNT_YRN_LOT_STK_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_RCV_H_ID", Value = ob.KNT_YRN_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_H_ID", Value = ob.KNT_SC_YRN_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
                     new CommandParameter() {ParameterName = "pALOC_QTY", Value = ob.ALOC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pIS_TEST_OK", Value = ob.IS_TEST_OK},
                     new CommandParameter() {ParameterName = "pIS_SUB_CONTR", Value = ob.IS_SUB_CONTR},
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
            const string sp = "SP_KNT_YRN_LOT_STK";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_STK_ID", Value = ob.KNT_YRN_LOT_STK_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_RCV_H_ID", Value = ob.KNT_YRN_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_H_ID", Value = ob.KNT_SC_YRN_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
                     new CommandParameter() {ParameterName = "pALOC_QTY", Value = ob.ALOC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pIS_TEST_OK", Value = ob.IS_TEST_OK},
                     new CommandParameter() {ParameterName = "pIS_SUB_CONTR", Value = ob.IS_SUB_CONTR},
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
            const string sp = "SP_KNT_YRN_LOT_STK";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_STK_ID", Value = ob.KNT_YRN_LOT_STK_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_RCV_H_ID", Value = ob.KNT_YRN_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_H_ID", Value = ob.KNT_SC_YRN_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pISSUE_QTY", Value = ob.ISSUE_QTY},
                     new CommandParameter() {ParameterName = "pALOC_QTY", Value = ob.ALOC_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pIS_TEST_OK", Value = ob.IS_TEST_OK},
                     new CommandParameter() {ParameterName = "pIS_SUB_CONTR", Value = ob.IS_SUB_CONTR},
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
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_LOT_STKModel> SelectAll()
        {
            string sp = "Select_KNT_YRN_LOT_STK";
            try
            {
                var obList = new List<KNT_YRN_LOT_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_STK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_STKModel ob = new KNT_YRN_LOT_STKModel();
                    ob.KNT_YRN_LOT_STK_ID = (dr["KNT_YRN_LOT_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_STK_ID"]);
                    ob.KNT_YRN_RCV_H_ID = (dr["KNT_YRN_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_RCV_H_ID"]);
                    ob.KNT_SC_YRN_RCV_H_ID = (dr["KNT_SC_YRN_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_YRN_RCV_H_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.IS_TEST_OK = (dr["IS_TEST_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TEST_OK"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
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

        public List<KNT_YRN_LOT_STKModel> ItemLotStockByID(Int64 pYARN_ITEM_ID, Int64 pSCM_STORE_ID, Int64 pRF_REQ_TYPE_ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_lot_stk_select";
            try
            {
                var obList = new List<KNT_YRN_LOT_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value =pYARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value =pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_STKModel ob = new KNT_YRN_LOT_STKModel();
                    ob.KNT_YRN_LOT_STK_ID = (dr["KNT_YRN_LOT_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_STK_ID"]);
                    //ob.KNT_YRN_RCV_H_ID = (dr["KNT_YRN_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_RCV_H_ID"]);
                    //ob.KNT_SC_YRN_RCV_H_ID = (dr["KNT_SC_YRN_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_YRN_RCV_H_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    //ob.IS_TEST_OK = (dr["IS_TEST_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TEST_OK"]);
                    //ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STOCK_QTY"]);

                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_COLR_GRP = (dr["YRN_COLR_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YRN_LOT_STKModel Select(long ID)
        {
            string sp = "Select_KNT_YRN_LOT_STK";
            try
            {
                var ob = new KNT_YRN_LOT_STKModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_STK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_LOT_STK_ID = (dr["KNT_YRN_LOT_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_STK_ID"]);
                    ob.KNT_YRN_RCV_H_ID = (dr["KNT_YRN_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_RCV_H_ID"]);
                    ob.KNT_SC_YRN_RCV_H_ID = (dr["KNT_SC_YRN_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_YRN_RCV_H_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.IS_TEST_OK = (dr["IS_TEST_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TEST_OK"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
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


        public object GetYarnLotStockList(int pageNumber, int pageSize, int? pRF_YRN_CNT_ID, int? pRF_FIB_COMP_ID, int? pLK_SPN_PRCS_ID,
            int? pLK_COTN_TYPE_ID, string pIS_SLUB, string pIS_MELLANGE, Int64? pKNT_YRN_LOT_ID, int? pRF_BRAND_ID, int? pSCM_STORE_ID)
        {
            string sp = "pkg_knit_common.knt_yrn_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_YRN_LOT_STKModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = pRF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = pRF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = pLK_SPN_PRCS_ID},
                     new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = pLK_COTN_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pIS_SLUB", Value = pIS_SLUB},
                     new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = pIS_MELLANGE},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = pKNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = pRF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},

                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_STKModel ob = new KNT_YRN_LOT_STKModel();

                    ob.YR_COUNT_NO = (dr["YR_COUNT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_NO"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);
                    ob.ADJ_QTY = (dr["ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_QTY"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);

                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    ob.CUR_STOCK_QTY = (dr["CUR_STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CUR_STOCK_QTY"]);

                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.SPN_PROCESS_NAME = (dr["SPN_PROCESS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SPN_PROCESS_NAME"]);
                    ob.COTTON_TYPE_NAME = (dr["COTTON_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COTTON_TYPE_NAME"]);
                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SLUB"]);
                    ob.IS_MELLANGE = (dr["IS_MELLANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MELLANGE"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_LOT_STKModel> GetYarnForTest(Int64? pRF_BRAND_ID = null, Int64? pRF_YR_CAT_ID = null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_lot_stk_select";
            try
            {
                var obList = new List<KNT_YRN_LOT_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_STK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pRF_YR_CAT_ID", Value =pRF_YR_CAT_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value =pRF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_STKModel ob = new KNT_YRN_LOT_STKModel();
                    ob.KNT_YRN_LOT_STK_ID = (dr["LK_YRN_TST_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_TST_TYPE_ID"]);
                    ob.KNT_YRN_RCV_H_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_COLR_GRP = (dr["YRN_COLR_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_LOT_STKModel> GetYarnForTestReq(string pFROM_DATE = null, string pTO_DATE = null, string pIMP_LC_NO = null, string pYRN_LOT_NO = null, Int64? pRF_BRAND_ID = null, Int64? pRF_YRN_CNT_ID = null)
        {
            string sp = "pkg_knt_yarn_test.knt_yrn_lot_test_select";
            try
            {
                var obList = new List<KNT_YRN_LOT_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =pFROM_DATE},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =pTO_DATE},
                     new CommandParameter() {ParameterName = "pIMP_LC_NO", Value =pIMP_LC_NO},
                     new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value =pYRN_LOT_NO},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value =pRF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value =pRF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_LOT_STKModel ob = new KNT_YRN_LOT_STKModel();
                    ob.YRN_MRR_NO = (dr["YRN_MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_MRR_NO"]);
                    ob.YRN_MRR_DT = (dr["YRN_MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["YRN_MRR_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.RF_YRN_CNT_ID = (dr["RF_YRN_CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_COLR_GRP = (dr["YRN_COLR_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YR_COUNT_NO = (dr["YR_COUNT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_NO"]);
                    ob.REQ_DOC_NO = (dr["REQ_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_DOC_NO"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string YRN_MRR_NO { get; set; }

        public DateTime YRN_MRR_DT { get; set; }

        public long SCM_SUPPLIER_ID { get; set; }

        public string REQ_DOC_NO { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public long RF_YRN_CNT_ID { get; set; }

        public long RF_FIB_COMP_ID { get; set; }

        public decimal CUR_STOCK_QTY { get; set; }
    }
}