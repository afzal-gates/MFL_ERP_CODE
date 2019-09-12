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
    public class INV_ITEMModel
    {
        public Int64? INV_ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string BAR_CODE { get; set; }
        public string PART_NO { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }

        [Required(ErrorMessage = "Please input item name")]
        public string ITEM_NAME_EN { get; set; }
        public string ITEM_NAME_BN { get; set; }
        public string ITEM_SPEC_1 { get; set; }
        public string ITEM_SPEC_2 { get; set; }
        public string ITEM_SPEC_3 { get; set; }
        public string ITEM_SPEC_IMP { get; set; }
        public string ITEM_SPEC_BND { get; set; }
        public Int64? LK_ITEM_TYPE_ID { get; set; }
        public Int64? RF_MAKER_ID { get; set; }
        public Int64? LK_LOC_SRC_TYPE_ID { get; set; }
        public Int64? RF_BRAND_ID { get; set; }
        [Required(ErrorMessage = "Please select purchase unit")]
        public Int64? PURCH_MOU_ID { get; set; }

        //[Required(ErrorMessage = "Please select country of orgin")]
        public Int64? HR_COUNTRY_ID { get; set; }

        [Required(ErrorMessage = "Please select consumption unit")]
        public Int64? CONS_MOU_ID { get; set; }
        public string CONS_MOU_CODE { get; set; }
        public string HS_CODE { get; set; }
        public Int64? BIN_NO { get; set; }
        public Decimal? RSV_QTY { get; set; }
        public Decimal? REORDER_QTY { get; set; }
        public Int64? PCT_ADDL_PRICE { get; set; }
        public Decimal? COST_PRICE { get; set; }
        public Decimal? PURCH_PRICE { get; set; }
        public Decimal? SALES_PRICE { get; set; }
        public Int64? RF_CURRENCY_ID { get; set; }
        public string MARKS { get; set; }
        public Decimal? GROSS_WT { get; set; }
        public Decimal? NET_WT { get; set; }
        public Decimal? ACT_WT { get; set; }
        public Int64? WT_MOU_ID { get; set; }

        //[Required(ErrorMessage = "Please select packing unit")]
        public Int64? PACK_MOU_ID { get; set; }
        
        public string MEASUREMENT { get; set; }
        public string TAG_NO { get; set; }
        public string IS_WT_MACHINE { get; set; }
        public Decimal? PACK_RATIO { get; set; }
        public Int64? PACK_RATIO_MOU_ID { get; set; }

        [Required(ErrorMessage = "Please select item status")]
        public Int64? LK_ITEM_STATUS_ID { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64? CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64? LAST_UPDATED_BY { get; set; }
        
        public string MOU_CODE { get; set; }
        public string MOU_NAME_EN { get; set; }
        public string ITEM_CODE_NAME { get; set; }
        public string IS_COMMON { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string IS_RD_CHEM { get; set; }
        


        public string Save()
        {
            const string sp = "pkg_inventory.inv_item_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pBAR_CODE", Value = ob.BAR_CODE},
                     new CommandParameter() {ParameterName = "pPART_NO", Value = ob.PART_NO},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_NAME_BN", Value = ob.ITEM_NAME_BN},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_1", Value = ob.ITEM_SPEC_1},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_2", Value = ob.ITEM_SPEC_2},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_3", Value = ob.ITEM_SPEC_3},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_IMP", Value = ob.ITEM_SPEC_IMP},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_BND", Value = ob.ITEM_SPEC_BND},
                     new CommandParameter() {ParameterName = "pLK_ITEM_TYPE_ID", Value = ob.LK_ITEM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_MAKER_ID", Value = ob.RF_MAKER_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pHS_CODE", Value = ob.HS_CODE},
                     new CommandParameter() {ParameterName = "pBIN_NO", Value = ob.BIN_NO},
                     new CommandParameter() {ParameterName = "pRSV_QTY", Value = ob.RSV_QTY},
                     new CommandParameter() {ParameterName = "pREORDER_QTY", Value = ob.REORDER_QTY},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pPURCH_PRICE", Value = ob.PURCH_PRICE},
                     new CommandParameter() {ParameterName = "pSALES_PRICE", Value = ob.SALES_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pMARKS", Value = ob.MARKS},
                     new CommandParameter() {ParameterName = "pGROSS_WT", Value = ob.GROSS_WT},
                     new CommandParameter() {ParameterName = "pNET_WT", Value = ob.NET_WT},
                     new CommandParameter() {ParameterName = "pACT_WT", Value = ob.ACT_WT},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     new CommandParameter() {ParameterName = "pTAG_NO", Value = ob.TAG_NO},
                     new CommandParameter() {ParameterName = "pIS_WT_MACHINE", Value = ob.IS_WT_MACHINE},

                     new CommandParameter() {ParameterName = "pPACK_RATIO", Value = ob.PACK_RATIO},
                     new CommandParameter() {ParameterName = "pPACK_RATIO_MOU_ID", Value = ob.PACK_RATIO_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCT_ADDL_PRICE", Value = ob.PCT_ADDL_PRICE},

                     new CommandParameter() {ParameterName = "pIS_RD_CHEM", Value = ob.IS_RD_CHEM},
                     new CommandParameter() {ParameterName = "pIS_COMMON", Value = ob.IS_COMMON},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "pkg_inventory.inv_item_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pBAR_CODE", Value = ob.BAR_CODE},
                     new CommandParameter() {ParameterName = "pPART_NO", Value = ob.PART_NO},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_NAME_BN", Value = ob.ITEM_NAME_BN},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_1", Value = ob.ITEM_SPEC_1},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_2", Value = ob.ITEM_SPEC_2},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_3", Value = ob.ITEM_SPEC_3},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_IMP", Value = ob.ITEM_SPEC_IMP},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_BND", Value = ob.ITEM_SPEC_BND},
                     new CommandParameter() {ParameterName = "pLK_ITEM_TYPE_ID", Value = ob.LK_ITEM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_MAKER_ID", Value = ob.RF_MAKER_ID},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID", Value = ob.HR_COUNTRY_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pHS_CODE", Value = ob.HS_CODE},
                     new CommandParameter() {ParameterName = "pBIN_NO", Value = ob.BIN_NO},
                     new CommandParameter() {ParameterName = "pRSV_QTY", Value = ob.RSV_QTY},
                     new CommandParameter() {ParameterName = "pREORDER_QTY", Value = ob.REORDER_QTY},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pPURCH_PRICE", Value = ob.PURCH_PRICE},
                     new CommandParameter() {ParameterName = "pSALES_PRICE", Value = ob.SALES_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pMARKS", Value = ob.MARKS},
                     new CommandParameter() {ParameterName = "pGROSS_WT", Value = ob.GROSS_WT},
                     new CommandParameter() {ParameterName = "pNET_WT", Value = ob.NET_WT},
                     new CommandParameter() {ParameterName = "pACT_WT", Value = ob.ACT_WT},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     new CommandParameter() {ParameterName = "pTAG_NO", Value = ob.TAG_NO},
                     new CommandParameter() {ParameterName = "pIS_WT_MACHINE", Value = ob.IS_WT_MACHINE},

                     new CommandParameter() {ParameterName = "pPACK_RATIO", Value = ob.PACK_RATIO},
                     new CommandParameter() {ParameterName = "pPACK_RATIO_MOU_ID", Value = ob.PACK_RATIO_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCT_ADDL_PRICE", Value = ob.PCT_ADDL_PRICE},

                     new CommandParameter() {ParameterName = "pIS_RD_CHEM", Value = ob.IS_RD_CHEM},
                     new CommandParameter() {ParameterName = "pIS_COMMON", Value = ob.IS_COMMON},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_INV_ITEM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value = ob.ITEM_CODE},
                     new CommandParameter() {ParameterName = "pPART_NO", Value = ob.PART_NO},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pITEM_NAME_EN", Value = ob.ITEM_NAME_EN},
                     new CommandParameter() {ParameterName = "pITEM_NAME_BN", Value = ob.ITEM_NAME_BN},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_1", Value = ob.ITEM_SPEC_1},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_2", Value = ob.ITEM_SPEC_2},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_3", Value = ob.ITEM_SPEC_3},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_IMP", Value = ob.ITEM_SPEC_IMP},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_BND", Value = ob.ITEM_SPEC_BND},
                     new CommandParameter() {ParameterName = "pLK_ITEM_TYPE_ID", Value = ob.LK_ITEM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_MAKER_ID", Value = ob.RF_MAKER_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPURCH_MOU_ID", Value = ob.PURCH_MOU_ID},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pHS_CODE", Value = ob.HS_CODE},
                     new CommandParameter() {ParameterName = "pBIN_NO", Value = ob.BIN_NO},
                     new CommandParameter() {ParameterName = "pRSV_QTY", Value = ob.RSV_QTY},
                     new CommandParameter() {ParameterName = "pREORDER_QTY", Value = ob.REORDER_QTY},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pPURCH_PRICE", Value = ob.PURCH_PRICE},
                     new CommandParameter() {ParameterName = "pSALES_PRICE", Value = ob.SALES_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pMARKS", Value = ob.MARKS},
                     new CommandParameter() {ParameterName = "pGROSS_WT", Value = ob.GROSS_WT},
                     new CommandParameter() {ParameterName = "pNET_WT", Value = ob.NET_WT},
                     new CommandParameter() {ParameterName = "pACT_WT", Value = ob.ACT_WT},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     new CommandParameter() {ParameterName = "pTAG_NO", Value = ob.TAG_NO},
                     new CommandParameter() {ParameterName = "pLK_ITEM_STATUS_ID", Value = ob.LK_ITEM_STATUS_ID},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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

        public List<INV_ITEMModel> SelectAll()
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);
                    ob.PART_NO = (dr["PART_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_NO"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    ob.ITEM_SPEC_1 = (dr["ITEM_SPEC_1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_1"]);
                    ob.ITEM_SPEC_2 = (dr["ITEM_SPEC_2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_2"]);
                    ob.ITEM_SPEC_3 = (dr["ITEM_SPEC_3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_3"]);
                    ob.ITEM_SPEC_IMP = (dr["ITEM_SPEC_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_IMP"]);
                    ob.ITEM_SPEC_BND = (dr["ITEM_SPEC_BND"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_BND"]);
                    ob.LK_ITEM_TYPE_ID = (dr["LK_ITEM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_TYPE_ID"]);
                    ob.RF_MAKER_ID = (dr["RF_MAKER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAKER_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);
                    ob.BIN_NO = (dr["BIN_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BIN_NO"]);
                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]);
                    ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]);
                    ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                    ob.TAG_NO = (dr["TAG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TAG_NO"]);
                    ob.IS_WT_MACHINE = (dr["IS_WT_MACHINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_WT_MACHINE"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                    ob.IS_RD_CHEM = (dr["IS_RD_CHEM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RD_CHEM"]);
                    
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public INV_ITEMModel Select(long ID)
        {
            string sp = "Select_INV_ITEM";
            try
            {
                var ob = new INV_ITEMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);
                    ob.PART_NO = (dr["PART_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_NO"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    ob.ITEM_SPEC_1 = (dr["ITEM_SPEC_1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_1"]);
                    ob.ITEM_SPEC_2 = (dr["ITEM_SPEC_2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_2"]);
                    ob.ITEM_SPEC_3 = (dr["ITEM_SPEC_3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_3"]);
                    ob.ITEM_SPEC_IMP = (dr["ITEM_SPEC_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_IMP"]);
                    ob.ITEM_SPEC_BND = (dr["ITEM_SPEC_BND"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_BND"]);
                    ob.LK_ITEM_TYPE_ID = (dr["LK_ITEM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_TYPE_ID"]);
                    ob.RF_MAKER_ID = (dr["RF_MAKER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAKER_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);
                    ob.BIN_NO = (dr["BIN_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BIN_NO"]);
                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]);
                    ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]);
                    ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                    ob.TAG_NO = (dr["TAG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TAG_NO"]);
                    ob.IS_WT_MACHINE = (dr["IS_WT_MACHINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_WT_MACHINE"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                    ob.IS_RD_CHEM = (dr["IS_RD_CHEM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RD_CHEM"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<INV_ITEMModel> ItemList(int? pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);
                    ob.PART_NO = (dr["PART_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_NO"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    ob.ITEM_SPEC_1 = (dr["ITEM_SPEC_1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_1"]);
                    ob.ITEM_SPEC_2 = (dr["ITEM_SPEC_2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_2"]);
                    ob.ITEM_SPEC_3 = (dr["ITEM_SPEC_3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_3"]);
                    ob.ITEM_SPEC_IMP = (dr["ITEM_SPEC_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_IMP"]);
                    ob.ITEM_SPEC_BND = (dr["ITEM_SPEC_BND"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_BND"]);
                    if ((dr["LK_ITEM_TYPE_ID"] != DBNull.Value))
                        ob.LK_ITEM_TYPE_ID = Convert.ToInt64(dr["LK_ITEM_TYPE_ID"]);

                    if ((dr["RF_MAKER_ID"] != DBNull.Value))
                        ob.RF_MAKER_ID = Convert.ToInt64(dr["RF_MAKER_ID"]);

                    if ((dr["HR_COUNTRY_ID"] != DBNull.Value))
                        ob.HR_COUNTRY_ID = Convert.ToInt64(dr["HR_COUNTRY_ID"]);

                    if ((dr["LK_LOC_SRC_TYPE_ID"] != DBNull.Value))
                        ob.LK_LOC_SRC_TYPE_ID = Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);

                    if ((dr["RF_BRAND_ID"] != DBNull.Value))
                    {
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);
                        ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    }
                    
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);

                    if ((dr["BIN_NO"] != DBNull.Value))
                        ob.BIN_NO = Convert.ToInt64(dr["BIN_NO"]);

                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);

                    if ((dr["RF_CURRENCY_ID"] != DBNull.Value))
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);

                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    if (dr["GROSS_WT"] != DBNull.Value)
                    { ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]); }
                    if (dr["NET_WT"] != DBNull.Value)
                    { ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]); }
                    if (dr["ACT_WT"] != DBNull.Value)
                    { ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]); }

                    if ((dr["WT_MOU_ID"] != DBNull.Value))
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);

                    if ((dr["PACK_MOU_ID"] != DBNull.Value))
                        ob.PACK_MOU_ID = Convert.ToInt64(dr["PACK_MOU_ID"]);

                    if ((dr["PACK_RATIO"] != DBNull.Value))
                        ob.PACK_RATIO = Convert.ToInt64(dr["PACK_RATIO"]);

                    if ((dr["PACK_RATIO_MOU_ID"] != DBNull.Value))
                        ob.PACK_RATIO_MOU_ID = Convert.ToInt64(dr["PACK_RATIO_MOU_ID"]);

                    ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                    ob.TAG_NO = (dr["TAG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TAG_NO"]);
                    ob.IS_WT_MACHINE = (dr["IS_WT_MACHINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_WT_MACHINE"]);

                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    ob.IS_COMMON = (dr["IS_COMMON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_COMMON"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.MOU_NAME_EN = (dr["MOU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_NAME_EN"]);
                    ob.IS_RD_CHEM = (dr["IS_RD_CHEM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RD_CHEM"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<INV_ITEMModel> ItemListWithUserID()
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);
                    ob.PART_NO = (dr["PART_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_NO"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    ob.ITEM_SPEC_1 = (dr["ITEM_SPEC_1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_1"]);
                    ob.ITEM_SPEC_2 = (dr["ITEM_SPEC_2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_2"]);
                    ob.ITEM_SPEC_3 = (dr["ITEM_SPEC_3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_3"]);
                    ob.ITEM_SPEC_IMP = (dr["ITEM_SPEC_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_IMP"]);
                    ob.ITEM_SPEC_BND = (dr["ITEM_SPEC_BND"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_BND"]);
                    if ((dr["LK_ITEM_TYPE_ID"] != DBNull.Value))
                        ob.LK_ITEM_TYPE_ID = Convert.ToInt64(dr["LK_ITEM_TYPE_ID"]);

                    if ((dr["RF_MAKER_ID"] != DBNull.Value))
                        ob.RF_MAKER_ID = Convert.ToInt64(dr["RF_MAKER_ID"]);

                    if ((dr["HR_COUNTRY_ID"] != DBNull.Value))
                        ob.HR_COUNTRY_ID = Convert.ToInt64(dr["HR_COUNTRY_ID"]);

                    if ((dr["LK_LOC_SRC_TYPE_ID"] != DBNull.Value))
                        ob.LK_LOC_SRC_TYPE_ID = Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);

                    if ((dr["RF_BRAND_ID"] != DBNull.Value))
                    {
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);
                        ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    }

                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);

                    if ((dr["BIN_NO"] != DBNull.Value))
                        ob.BIN_NO = Convert.ToInt64(dr["BIN_NO"]);

                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);

                    if ((dr["RF_CURRENCY_ID"] != DBNull.Value))
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);

                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    if (dr["GROSS_WT"] != DBNull.Value)
                    { ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]); }
                    if (dr["NET_WT"] != DBNull.Value)
                    { ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]); }
                    if (dr["ACT_WT"] != DBNull.Value)
                    { ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]); }

                    if ((dr["WT_MOU_ID"] != DBNull.Value))
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);

                    if ((dr["PACK_MOU_ID"] != DBNull.Value))
                        ob.PACK_MOU_ID = Convert.ToInt64(dr["PACK_MOU_ID"]);

                    if ((dr["PACK_RATIO"] != DBNull.Value))
                        ob.PACK_RATIO = Convert.ToInt64(dr["PACK_RATIO"]);

                    if ((dr["PACK_RATIO_MOU_ID"] != DBNull.Value))
                        ob.PACK_RATIO_MOU_ID = Convert.ToInt64(dr["PACK_RATIO_MOU_ID"]);

                    ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                    ob.TAG_NO = (dr["TAG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TAG_NO"]);
                    ob.IS_WT_MACHINE = (dr["IS_WT_MACHINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_WT_MACHINE"]);

                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    ob.IS_COMMON = (dr["IS_COMMON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_COMMON"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.MOU_NAME_EN = (dr["MOU_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_NAME_EN"]);
                    ob.IS_RD_CHEM = (dr["IS_RD_CHEM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RD_CHEM"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<INV_ITEMModel> ItemAutoDataList(string pITEM_CODE)
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pITEM_CODE", Value =pITEM_CODE},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    ob.ITEM_CODE_NAME = (dr["ITEM_CODE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE_NAME"]);

                    if (dr["PACK_MOU_ID"] != DBNull.Value)
                    {
                        ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);                        
                    }

                    if (dr["CONS_MOU_ID"] != DBNull.Value)
                    {
                        ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                        ob.CONS_MOU_CODE = (dr["CONS_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONS_MOU_CODE"]);
                    }

                    if (dr["PACK_RATIO_MOU_ID"] != DBNull.Value)
                        ob.PACK_RATIO_MOU_ID = (dr["PACK_RATIO_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_RATIO_MOU_ID"]);

                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<INV_ITEMModel> ItemDataListForLD(Int64? pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    //ob.ITEM_CODE_NAME = (dr["ITEM_CODE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE_NAME"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);

                    if ((dr["HR_COUNTRY_ID"] != DBNull.Value))
                        ob.HR_COUNTRY_ID = Convert.ToInt64(dr["HR_COUNTRY_ID"]);

                    if ((dr["RF_BRAND_ID"] != DBNull.Value))
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);

                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);

                    if ((dr["MOU_CODE"] != DBNull.Value))
                        ob.MOU_CODE = Convert.ToString(dr["MOU_CODE"]);

                    if ((dr["BIN_NO"] != DBNull.Value))
                        ob.BIN_NO = Convert.ToInt64(dr["BIN_NO"]);

                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);                    
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);

                    if ((dr["RF_CURRENCY_ID"] != DBNull.Value))
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);

                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    if (dr["GROSS_WT"] != DBNull.Value)
                    { ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]); }
                    if (dr["NET_WT"] != DBNull.Value)
                    { ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]); }
                    if (dr["ACT_WT"] != DBNull.Value)
                    { ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]); }

                    if ((dr["WT_MOU_ID"] != DBNull.Value))
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);

                    if ((dr["PACK_MOU_ID"] != DBNull.Value))
                        ob.PACK_MOU_ID = Convert.ToInt64(dr["PACK_MOU_ID"]);

                    if ((dr["PACK_RATIO"] != DBNull.Value))
                        ob.PACK_RATIO = Convert.ToInt64(dr["PACK_RATIO"]);

                    if ((dr["PACK_RATIO_MOU_ID"] != DBNull.Value))
                        ob.PACK_RATIO_MOU_ID = Convert.ToInt64(dr["PACK_RATIO_MOU_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<INV_ITEMModel> ItemDataListByCatList(string pINV_ITEM_CAT_LST)
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value =pINV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    //ob.ITEM_CODE_NAME = (dr["ITEM_CODE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE_NAME"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);

                    if ((dr["HR_COUNTRY_ID"] != DBNull.Value))
                        ob.HR_COUNTRY_ID = Convert.ToInt64(dr["HR_COUNTRY_ID"]);

                    if ((dr["RF_BRAND_ID"] != DBNull.Value))
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);

                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);

                    if ((dr["MOU_CODE"] != DBNull.Value))
                        ob.MOU_CODE = Convert.ToString(dr["MOU_CODE"]);

                    if ((dr["BIN_NO"] != DBNull.Value))
                        ob.BIN_NO = Convert.ToInt64(dr["BIN_NO"]);

                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);

                    if ((dr["RF_CURRENCY_ID"] != DBNull.Value))
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);

                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    if (dr["GROSS_WT"] != DBNull.Value)
                    { ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]); }
                    if (dr["NET_WT"] != DBNull.Value)
                    { ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]); }
                    if (dr["ACT_WT"] != DBNull.Value)
                    { ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]); }

                    if ((dr["WT_MOU_ID"] != DBNull.Value))
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);

                    if ((dr["PACK_MOU_ID"] != DBNull.Value))
                        ob.PACK_MOU_ID = Convert.ToInt64(dr["PACK_MOU_ID"]);

                    if ((dr["PACK_RATIO"] != DBNull.Value))
                        ob.PACK_RATIO = Convert.ToInt64(dr["PACK_RATIO"]);

                    if ((dr["PACK_RATIO_MOU_ID"] != DBNull.Value))
                        ob.PACK_RATIO_MOU_ID = Convert.ToInt64(dr["PACK_RATIO_MOU_ID"]);

                    if ((dr["IS_RD_CHEM"] != DBNull.Value))
                        ob.IS_RD_CHEM = Convert.ToString(dr["IS_RD_CHEM"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        public string GetPantonNoByStlClr(Int64 pMC_STYLE_H_ID, Int64 pMC_COLOR_ID)
        {
            string jsonStr = "{";
            string sp = "pkg_mc_style.mc_bu_col_ref_panton_no_get";
            var i = 1;
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pPANTON_NO", Value ="", Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
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
            }
            catch
            {

            }
            return jsonStr;
        }

        public List<INV_ITEMModel> GetKnitSTKItemListByCatID(Int64? pINV_ITEM_CAT_ID, Int64? pRF_REQ_TYPE_ID = null)
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    //ob.ITEM_CODE_NAME = (dr["ITEM_CODE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE_NAME"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);

                    if ((dr["HR_COUNTRY_ID"] != DBNull.Value))
                        ob.HR_COUNTRY_ID = Convert.ToInt64(dr["HR_COUNTRY_ID"]);

                    if ((dr["RF_BRAND_ID"] != DBNull.Value))
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);

                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);

                    if ((dr["BIN_NO"] != DBNull.Value))
                        ob.BIN_NO = Convert.ToInt64(dr["BIN_NO"]);

                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);

                    if ((dr["RF_CURRENCY_ID"] != DBNull.Value))
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);

                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    if (dr["GROSS_WT"] != DBNull.Value)
                    { ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]); }
                    if (dr["NET_WT"] != DBNull.Value)
                    { ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]); }
                    if (dr["ACT_WT"] != DBNull.Value)
                    { ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]); }

                    if ((dr["WT_MOU_ID"] != DBNull.Value))
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);

                    if ((dr["PACK_MOU_ID"] != DBNull.Value))
                        ob.PACK_MOU_ID = Convert.ToInt64(dr["PACK_MOU_ID"]);

                    if ((dr["PACK_RATIO"] != DBNull.Value))
                        ob.PACK_RATIO = Convert.ToInt64(dr["PACK_RATIO"]);

                    if ((dr["PACK_RATIO_MOU_ID"] != DBNull.Value))
                        ob.PACK_RATIO_MOU_ID = Convert.ToInt64(dr["PACK_RATIO_MOU_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<INV_ITEMModel> GetDyeSTKItemListByCatID(Int64? pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    //ob.ITEM_CODE_NAME = (dr["ITEM_CODE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE_NAME"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);

                    if ((dr["HR_COUNTRY_ID"] != DBNull.Value))
                        ob.HR_COUNTRY_ID = Convert.ToInt64(dr["HR_COUNTRY_ID"]);

                    if ((dr["RF_BRAND_ID"] != DBNull.Value))
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);

                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);

                    if ((dr["BIN_NO"] != DBNull.Value))
                        ob.BIN_NO = Convert.ToInt64(dr["BIN_NO"]);

                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);

                    if ((dr["RF_CURRENCY_ID"] != DBNull.Value))
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);

                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    if (dr["GROSS_WT"] != DBNull.Value)
                    { ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]); }
                    if (dr["NET_WT"] != DBNull.Value)
                    { ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]); }
                    if (dr["ACT_WT"] != DBNull.Value)
                    { ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]); }

                    if ((dr["WT_MOU_ID"] != DBNull.Value))
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);

                    if ((dr["PACK_MOU_ID"] != DBNull.Value))
                        ob.PACK_MOU_ID = Convert.ToInt64(dr["PACK_MOU_ID"]);

                    if ((dr["PACK_RATIO"] != DBNull.Value))
                        ob.PACK_RATIO = Convert.ToInt64(dr["PACK_RATIO"]);

                    if ((dr["PACK_RATIO_MOU_ID"] != DBNull.Value))
                        ob.PACK_RATIO_MOU_ID = Convert.ToInt64(dr["PACK_RATIO_MOU_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<INV_ITEMModel> ItemDataListByStoreNCatList(string pINV_ITEM_CAT_LST, Int64? pSCM_STORE_ID=null)
        {
            string sp = "pkg_inventory.inv_item_select";
            try
            {
                var obList = new List<INV_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value =pINV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value =pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_ITEMModel ob = new INV_ITEMModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);

                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_NAME_BN = (dr["ITEM_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_BN"]);
                    //ob.ITEM_CODE_NAME = (dr["ITEM_CODE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE_NAME"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);

                    if ((dr["HR_COUNTRY_ID"] != DBNull.Value))
                        ob.HR_COUNTRY_ID = Convert.ToInt64(dr["HR_COUNTRY_ID"]);

                    if ((dr["RF_BRAND_ID"] != DBNull.Value))
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);

                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);

                    if ((dr["MOU_CODE"] != DBNull.Value))
                        ob.MOU_CODE = Convert.ToString(dr["MOU_CODE"]);

                    if ((dr["BIN_NO"] != DBNull.Value))
                        ob.BIN_NO = Convert.ToInt64(dr["BIN_NO"]);

                    ob.RSV_QTY = (dr["RSV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RSV_QTY"]);
                    ob.REORDER_QTY = (dr["REORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REORDER_QTY"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_ADDL_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.PURCH_PRICE = (dr["PURCH_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PURCH_PRICE"]);
                    ob.SALES_PRICE = (dr["SALES_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SALES_PRICE"]);

                    if ((dr["RF_CURRENCY_ID"] != DBNull.Value))
                        ob.RF_CURRENCY_ID = Convert.ToInt64(dr["RF_CURRENCY_ID"]);

                    ob.MARKS = (dr["MARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKS"]);
                    if (dr["GROSS_WT"] != DBNull.Value)
                    { ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]); }
                    if (dr["NET_WT"] != DBNull.Value)
                    { ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]); }
                    if (dr["ACT_WT"] != DBNull.Value)
                    { ob.ACT_WT = (dr["ACT_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_WT"]); }

                    if ((dr["WT_MOU_ID"] != DBNull.Value))
                        ob.WT_MOU_ID = Convert.ToInt64(dr["WT_MOU_ID"]);

                    if ((dr["PACK_MOU_ID"] != DBNull.Value))
                        ob.PACK_MOU_ID = Convert.ToInt64(dr["PACK_MOU_ID"]);

                    if ((dr["PACK_RATIO"] != DBNull.Value))
                        ob.PACK_RATIO = Convert.ToInt64(dr["PACK_RATIO"]);

                    if ((dr["PACK_RATIO_MOU_ID"] != DBNull.Value))
                        ob.PACK_RATIO_MOU_ID = Convert.ToInt64(dr["PACK_RATIO_MOU_ID"]);

                    if ((dr["IS_RD_CHEM"] != DBNull.Value))
                        ob.IS_RD_CHEM = Convert.ToString(dr["IS_RD_CHEM"]);

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