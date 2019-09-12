using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Dyeing
{
    public class DyeReportModel
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



        private void LoadImage(DataRow objDataRow, string strImageField)
        {
            try
            {
                //FileStream fs = new FileStream(FilePath,
                //           System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //byte[] Image = new byte[objDataRow[""].Length];
                //fs.Read(Image, 0, Convert.ToInt32(fs.Length));
                //fs.Close();
                //objDataRow[strImageField] = Image;
            }
            catch (Exception ex)
            {

            }
        }

        public DataSet QueryResult()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 4000}
                }, sp);

            return ds;
        }

        public DataSet DyesChemicalBatchProgramRequisition()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4000}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalBatchProgramRequisitionAddition()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4002}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalBatchCostBreakdown()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    //new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_LST", Value = ob.MC_BYR_ACC_GRP_LST},
		            new CommandParameter() {ParameterName = "pOption", Value = 4003}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalDailyDyeingProduction()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 4004}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalBatchCostAnalysis()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    //new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4005}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalDailyProductionCosting()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    //new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4006}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalStock()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4001}
                }, sp);

            ds.Tables[0].TableName = "DC_STOCK";

            return ds;
        }


        public DataSet StoreTransfereDeliveryChallan()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_STR_TR_ISS_H_ID", Value = ob.DYE_STR_TR_ISS_H_ID},
                    new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ob.DYE_STR_TR_REQ_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 4007}

                }, sp);

            return ds;
        }

        public DataSet LoanDeliveryChallan()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_DC_ISS_H_ID", Value = ob.DYE_DC_ISS_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 4008}

                }, sp);

            return ds;
        }

        public DataSet RequisitionDetails()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 4009}

                }, sp);

            return ds;
        }

        public DataSet ChallanWiseLoanReceiveDelivery()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4010}

                }, sp);

            return ds;
        }

        public DataSet DailyReceive()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
	                new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
	                new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},                    
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4011}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalStockSummary()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 4014}

                }, sp);

            return ds;
        }

        public DataSet BatchWiseDCIssueSheet()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 4015}

                }, sp);

            return ds;
        }


        public DataSet DyesChemicalStockLedger()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 4016}

                }, sp);

            return ds;
        }

        public DataSet BuyerOrderWithoutLD()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},                   
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},                   
		            new CommandParameter() {ParameterName = "pOption", Value = 4020}

                }, sp);

            return ds;
        }

        public DataSet DailyProductionIssue()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE.Value.AddHours(6)},                   
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE.Value.AddHours(6)},                   
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},                   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},                   
		            new CommandParameter() {ParameterName = "pOption", Value = 4021}

                }, sp);

            return ds;
        }

        public DataSet DyeingMCProgramData()
        {
            var ob = this;
            string sp = "PKG_DYE_BATCH_PLAN.GET_EVENT_DATA";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value =ob.DYE_BATCH_SCDL_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
            return ds;
        }

        public DataSet LoanLedgerSummary()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},                    
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4017}

                }, sp);

            return ds;
        }

        public DataSet LoanLedgerDetails()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},                    
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4018}

                }, sp);

            return ds;
        }

        public DataSet BatchProgram()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},               
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},               
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},               
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},          
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},              
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},               
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},               
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},               
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4019}

                }, sp);

            return ds;
        }

        public DataSet BatchCardWithoutLabdipRecipe()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},                   
		            new CommandParameter() {ParameterName = "pOption", Value = 4022}

                }, sp);

            return ds;
        }



        public DataSet MonthWiseDailyStatementOfReprocess()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},                   
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},                   
		            new CommandParameter() {ParameterName = "pOption", Value = 4023}

                }, sp);

            return ds;
        }

        public DataSet DailyBatchWiseCostAnalysis()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},               
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},               
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},               
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},               
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},               
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},               
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},                   
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},                   
		            new CommandParameter() {ParameterName = "pOption", Value = 4024}

                }, sp);

            return ds;
        }

        public DataSet BuyerWiseProductionCosting()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    //new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4025}

                }, sp);

            return ds;
        }


        public DataSet DyeingMCProgramDataSample()
        {
            var ob = this;
            string sp = "PKG_DYE_BATCH_PLAN.GET_EVENT_DATA";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =ob.FROM_DATE},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =ob.TO_DATE},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
            return ds;
        }


        public DataSet MachineLoadingPerformance()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    //new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},

                    //new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    //new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    //new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4026}

                }, sp);

            return ds;
        }

        public DataSet BatchInfoWithoutProduction()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4027}

                }, sp);

            return ds;
        }



        public DataSet DailyDyeingFinishingProduction()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                    new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value = ob.DF_PROC_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pDF_MACHINE_ID", Value = ob.DF_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 6000}

                }, sp);

            return ds;
        }


        public DataSet DfScProgramDelivery()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 6001}

                }, sp);

            return ds;
        }

        public DataSet DfScProductionSummary()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 6002 }
                }, sp);

            return ds;
        }
        public DataSet DfScProductionRegister()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 6003 }
                }, sp);

            return ds;
        }

        public DataSet DfDeliveryChallan()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = ob.DYE_GSTR_ISS_H_ID },
		            new CommandParameter() {ParameterName = "pOption", Value = 6004}

                }, sp);

            return ds;
        }

        public DataSet ScPreTreatmentChallan()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = ob.DYE_GSTR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value = ob.DF_SC_PT_CHL_ISS_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 6005}

                }, sp);

            return ds;
        }

        public DataSet DyeScFabricBooking()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = ob.DYE_SC_PRG_ISS_ID },
		            new CommandParameter() {ParameterName = "pOption", Value = 4029}

                }, sp);

            ds.Tables[0].TableName = "SC_BOOKING_HDR";
            ds.Tables[1].TableName = "SC_BOOKING_FAB_DTL";
            ds.Tables[2].TableName = "SC_BOOKING_DIA_DTL";

            return ds;
        }


        public DataSet DyesChemicalBatchCostBreakdownForPcCvc()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    //new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_LST", Value = ob.MC_BYR_ACC_GRP_LST},
		            new CommandParameter() {ParameterName = "pOption", Value = 4030}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalBatchCostBreakdownForFiberComp()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    //new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4031}

                }, sp);

            return ds;
        }



        public DataSet DyeBatchCard()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},                    
                    new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},                    
                    new CommandParameter() {ParameterName = "pLK_FBR_GRP_LST", Value = ob.LK_FBR_GRP_LST},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 4032}

                }, sp);

            return ds;
        }

        public DataSet DuplicateDyeBatchCard()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID}, 
                    new CommandParameter() {ParameterName = "pLK_FBR_GRP_LST", Value = ob.LK_FBR_GRP_LST},                     
		            new CommandParameter() {ParameterName = "pOption", Value = 4033}

                }, sp);

            return ds;
        }

        public DataSet BatchCheckRollStatus()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},                  
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},                  
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},                  
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},           
		            new CommandParameter() {ParameterName = "pOption", Value = 4034}

                }, sp);

            return ds;
        }


        public DataSet DyeingQcProduction()
        {
            var ob = this;
            string sp = "PKG_DYE_REPORT.DYE_RPT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},                  
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},                  
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},                  
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},                  
                    new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = ob.LK_CHQ_RL_STS_ID},       
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},       
                    new CommandParameter() {ParameterName = "pSTYLE_ORDER_NO", Value = ob.STYLE_ORDER_NO},       
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},           
                    new CommandParameter() {ParameterName = "pDT_TYPE_ID", Value = ob.DT_TYPE_ID},           
		            new CommandParameter() {ParameterName = "pOption", Value = 4035}

                }, sp);

            return ds;
        }


        public DataSet DyeingStockTackingReport()
        {
            var ob = this;
            string sp = "PKG_DYE_REPORT.DYE_RPT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {        
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},           
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
                    
                    new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},

		            new CommandParameter() {ParameterName = "pOption", Value = 4036}

                }, sp);

            return ds;
        }


        public DataSet FinishFabricInspection()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                    //new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value = ob.FAB_ROLL_NO},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},

                    new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                    new CommandParameter() {ParameterName = "pSUB_LOT_NO", Value = ob.SUB_LOT_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 6006}

                }, sp);

            return ds;
        }


        public DataSet ScPreTreatmentBatchCard()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSC_PRG_NO", Value = ob.SC_PRG_NO},
                    new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 6007}

                }, sp);

            return ds;
        }


        public DataSet CollarCuffTrimsCard()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = ob.DYE_GSTR_REQ_H_ID},  
                    new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},  
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 6008}

                }, sp);

            return ds;
        }


        public DataSet DailyDyeingProductionWithQC()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
                    new CommandParameter() {ParameterName = "pIS_ROLL_OK", Value = ob.IS_ROLL_OK},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 4037}

                }, sp);

            return ds;
        }


        public DataSet DyesChemicalLoanSummary()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 4038}

                }, sp);

            return ds;
        }

        public DataSet DyeingReporcessHistory()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 4039}

                }, sp);

            return ds;
        }


        public DataSet DfScProgramChallan()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDF_SC_BT_CHL_ISS_H_ID", Value = ob.DF_SC_BT_CHL_ISS_H_ID},
                    new CommandParameter() {ParameterName = "pDF_SC_PRG_ISS_H_ID", Value = ob.DF_SC_PRG_ISS_H_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 6009}

                }, sp);

            return ds;
        }


        public DataSet OrdColWiseDyeingProd()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMONTHOF", Value = ob.MONTHOF},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 4040}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_PROD_STATUS";

            return ds;
        }


        public DataSet SubDyeBatchCard()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSUB_BATCH_NO", Value = ob.SUB_BATCH_NO},
                    //new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID}, 
                    //new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},                     
		            new CommandParameter() {ParameterName = "pOption", Value = 4041}

                }, sp);

            return ds;
        }


        public DataSet ManualBatchList()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                    new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4042}

                }, sp);

            return ds;
        }


        public DataSet FabricHoldList()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =ob.MC_COLOR_ID},                    
                    new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value =ob.RF_RESP_DEPT_ID},                    
                    new CommandParameter() {ParameterName = "pOption", Value = 6010}

                }, sp);

            return ds;
        }

        public DataSet GmtWashRequisition()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                      
                    new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value =ob.GMT_DF_WASH_REQ_ID},                    
                    new CommandParameter() {ParameterName = "pOption", Value = 6011}

                }, sp);

            return ds;
        }



        public DataSet ScProgramLedger()
        {
            var ob = this;
            string sp = "pkg_df_report.df_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},  
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},  
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
		            new CommandParameter() {ParameterName = "pOption", Value = 6012}
                }, sp);

            return ds;
        }

        public DataSet DfFloorStatus()
        {
            var ob = this;
            string sp = "PKG_DF_PRODUCTION.dye_bt_prod_dashboard_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
		            new CommandParameter() {ParameterName = "pOption", Value = 3008}
                }, sp);

            return ds;
        }


        public DataSet ChallanWiseReceive()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},                    
	                new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
	                new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
	                new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
	                new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
	                new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4044}

                }, sp);

            return ds;
        }

        public DataSet RequisitionForFinishingProcess()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = ob.DYE_BATCH_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 4045}

                }, sp);

            return ds;
        }


        public DataSet SampleBatchGreyIssueSlip()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4046}
                }, sp);

            return ds;
        }


        public DataSet DyesChemicalTotalReceive()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},                    
	                new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
	                new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
	                new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4047}

                }, sp);

            return ds;
        }

        public DataSet DyesChemicalChallanWiseTransfer()
        {
            var ob = this;
            string sp = "pkg_dye_report.dye_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},                    
	                new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
	                new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
	                new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4048}

                }, sp);

            return ds;
        }


        public DataSet DyeingStockTackingWithSourceReport()
        {
            var ob = this;
            string sp = "PKG_DYE_REPORT.DYE_RPT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {        
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},           
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
                    
                    new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},

		            new CommandParameter() {ParameterName = "pOption", Value = 4036}

                }, sp);

            return ds;
        }


        public DataSet DyeingSourceStockTackingReport()
        {
            var ob = this;
            string sp = "PKG_DYE_REPORT.DYE_RPT_SELECT";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {        
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},           
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
                    
                    new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},

		            new CommandParameter() {ParameterName = "pOption", Value = 4049}

                }, sp);

            return ds;
        }

        public Int64? DYE_BT_CARD_H_ID { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public Int64? DF_PROC_TYPE_ID { get; set; }
        public Int64? HR_SCHEDULE_H_ID { get; set; }
        public Int64? DF_MACHINE_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public Int64? DYE_GSTR_ISS_H_ID { get; set; }
        public Int64? DYE_SC_PRG_ISS_ID { get; set; }
        public Int64? HR_COMPANY_ID { get; set; }

        public string MC_BYR_ACC_GRP_LST { get; set; }

        public string LK_FBR_GRP_LST { get; set; }

        public object DYE_GSTR_REQ_H_ID { get; set; }

        public object DF_SC_PT_ISS_H_ID { get; set; }

        public Int64? KNT_QC_STS_TYPE_ID { get; set; }

        public Int64? RF_FAB_TYPE_ID { get; set; }

        public string FAB_ROLL_NO { get; set; }

        public Int64? SUB_LOT_NO { get; set; }

        public Int64? DF_FAB_QC_RPT_ID { get; set; }

        public Int64? DF_SC_PT_CHL_ISS_H_ID { get; set; }

        public string SC_PRG_NO { get; set; }

        public string IS_ROLL_OK { get; set; }
        public string IS_COMBINE { get; set; }

        public Int64? DYE_BT_CARD_GRP_ID { get; set; }

        public Int64? LK_CHQ_RL_STS_ID { get; set; }

        public string STYLE_ORDER_NO { get; set; }

        public Int64? SC_USER_ID { get; set; }

        public string DT_TYPE_ID { get; set; }

        public Int64? DF_SC_BT_CHL_ISS_H_ID { get; set; }

        public Int64? RF_FAB_PROD_CAT_ID { get; set; }

        public string MONTHOF { get; set; }

        public Int64? DF_BT_SUB_LOT_ID { get; set; }

        public string SUB_BATCH_NO { get; set; }

        public Int64? DF_SC_PRG_ISS_H_ID { get; set; }
        public Int64? RF_RESP_DEPT_ID { get; set; }
        public Int64? GMT_DF_WASH_REQ_ID { get; set; }
        public Int64? RF_PAY_MTHD_ID { get; set; }
        public Int64? DYE_STR_TR_REQ_H_ID { get; set; }



        public string IS_DUPLICATE { get; set; }
    }
}
