using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Purchase
{

    public class PurReportModel
    {
        public string REPORT_CODE { get; set; }
        public string IS_EXCEL_FORMAT { get; set; }
        public string PAGE_SIZE_NAME { get; set; }

        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public string DYE_BATCH_NO { get; set; }
        public Int64 DYE_STR_REQ_H_ID { get; set; }
        public Int32? RF_BRAND_ID { get; set; }
        public Int32? SCM_STORE_ID { get; set; }
        public Int32? INV_ITEM_CAT_ID { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public Int32? INV_ITEM_ID { get; set; }
        public Int32? LK_LOC_SRC_TYPE_ID { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public string ISS_REF_NO { get; set; }
        public Int32? DYE_STR_TR_ISS_H_ID { get; set; }

        public Int32? DYE_DC_ISS_H_ID { get; set; }

        public Int64? DYE_BATCH_SCDL_ID { get; set; }


        public Int64 MC_BUYER_ID { get; set; }
        public Int64 MC_COLOR_ID { get; set; }
        public Int64 MC_COLOR_GRP_ID { get; set; }
        public Int64 DYE_MACHINE_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64 MC_BYR_ACC_ID { get; set; }
        public Int64 MC_BYR_ACC_GRP_ID { get; set; }
        public Int64 MC_STYLE_H_EXT_ID { get; set; }
                
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public Int64? DF_PROC_TYPE_ID { get; set; }
        public Int64? HR_SCHEDULE_H_ID { get; set; }
        public Int64? DF_MACHINE_ID { get; set; }
        public string PO_NO_IMP { get; set; }
        public Int64? DYE_GSTR_ISS_H_ID { get; set; }
        public Int64? CM_IMP_PO_H_ID { get; set; }
        public string RPT_PATH_URL { get; set; }
        
        public DataSet PurchaseOrder()
        {
            var ob = this;
            string sp = "PKG_PUR_REPORT.PUR_REPORT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 8000}
                }, sp);

            return ds;
        }

        public DataSet PurchaseRequisition()
        {
            var ob = this;
            string sp = "PKG_PUR_REPORT.PUR_REPORT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 8001}
                }, sp);

            return ds;
        }


        public DataSet FundRequisition()
        {
            var ob = this;
            string sp = "PKG_PUR_REPORT.PUR_REPORT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value = ob.SCM_FUND_REQ_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 8002}
                }, sp);

            return ds;
        }
        public DataSet ItemFundRequisition()
        {
            var ob = this;
            string sp = "PKG_PUR_REPORT.PUR_REPORT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value = ob.SCM_FUND_REQ_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 8003}
                }, sp);

            return ds;
        }

        public DataSet InventoryAdjustmentRequisition()
        {
            var ob = this;
            string sp = "PKG_PUR_REPORT.PUR_REPORT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_H_ID", Value = ob.SCM_STR_ITM_ADJ_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 8004}
                }, sp);

            return ds;
        }

        
        public Int64? SCM_PURC_REQ_H_ID { get; set; }
        public Int64? SCM_FUND_REQ_H_ID { get; set; }
        public Int64? SCM_STR_ITM_ADJ_H_ID { get; set; }
    }
}
