using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.ComponentModel;
using System.IO;

namespace ERP.Model
{
    public class MrcReportModel
    {
        public DateTime? TO_MONTH { get; set; }
        public DateTime? FROM_MONTH { get; set; }
        public string REPORT_CODE { get; set; }
        public string IS_EXCEL_FORMAT { get; set; }
        public string IS_EXPORT_TO_DISK { get; set; }
        public string PAGE_SIZE_NAME { get; set; }
        public Int64? MC_SMP_REQ_H_ID { get; set; }
        public Int64? MC_SMP_PROD_ID { get; set; }
        public Int32? NO_OF_DELV_QTY_PRINT { get; set; }
        public Int32? SMPL_REV_NO { get; set; }
        public Int32? SMPL_OPT_NO { get; set; }
        public string SMPL_ACT_FAB_GSM { get; set; }
        public Int32? SMPL_FMT_ID { get; set; }
        public Int64? MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64? MC_BLK_REVISION_NO { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public string IS_MULTI_SHIP_DT { get; set; }
        public Int64? MC_LD_REQ_H_ID { get; set; }
        public Int64? REVISION_NO { get; set; }
        public Int64? MC_STYL_BGT_H_ID { get; set; }
        public string IS_BGT_FINALIZED { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public Int64? MC_STYLE_H_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? MC_PROV_FAB_BK_H_ID { get; set; }
        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }
        public string RES_TITLE { get; set; }

        public Int64? HR_DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }

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

        public DataSet SampleFabricBooking()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = ob.MC_SMP_REQ_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);

            ds.Tables[0].TableName = "SAM_FAB_BOOKING_H";
            ds.Tables[1].TableName = "SAM_FAB_BOOKING_SAM";
            ds.Tables[2].TableName = "SAM_FAB_BOOKING_FAB";
            ds.Tables[3].TableName = "SAM_FAB_BOOKING_TNA";
            ds.Tables[4].TableName = "SAM_FAB_BOOKING_CLCF";
            return ds;
        }

        public DataSet BulkFabricBooking()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pMC_BLK_REVISION_NO", Value = ob.MC_BLK_REVISION_NO},
		            new CommandParameter() {ParameterName = "pOption", Value = 3001}
                }, sp);

            ds.Tables[0].TableName = "BULK_FABRIC_BOOKING";
            ds.Tables[1].TableName = "BULK_FABRIC_BOOKING_CONS";
            ds.Tables[2].TableName = "BULK_FABRIC_BOOKING_FAB";

            if (ob.IS_MULTI_SHIP_DT == "Y")
            {
                ds.Tables[3].TableName = "BULK_FAB_BK_ORDER_QTY_MULTI_SP";
            }
            else
            {
                ds.Tables[3].TableName = "BULK_FAB_BK_ORDER_QTY";
            }

            ds.Tables[4].TableName = "BULK_FABRIC_BOOKING_TNA";

            return ds;
        }

        public DataSet YarnList()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                }, sp);

            ds.Tables[0].TableName = "Yarn List";
            return ds;
        }

        public DataSet BudgetSheet()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	    
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value =ob.MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BGT_REVISION_NO", Value =ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3003}
                }, sp);

            ds.Tables[0].TableName = "Header- BS";
            ds.Tables[1].TableName = "Main- BS";
            ds.Tables[2].TableName = "TOP_BS";
            return ds;
        }

        public DataSet OrderSheet()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3004}
                }, sp);

            ds.Tables[0].TableName = "ORDER_SHEET";
            return ds;
        }

        public DataSet LapdibProgram()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	    
                     new CommandParameter() {ParameterName = "pMC_LD_REQ_H_ID", Value =ob.MC_LD_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                }, sp);

            ds.Tables[0].TableName = "Labdip";
            return ds;
        }

        public DataSet SamplProduction()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3006}
                }, sp);

            ds.Tables[0].TableName = "SMPL_PRODUCTION";
            ds.Tables[1].TableName = "SMPL_GROUP_PROD";
            return ds;
        }

        public DataSet SampleCard()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	            
                    new CommandParameter() {ParameterName = "pMC_SMP_PROD_ID", Value = ob.MC_SMP_PROD_ID},
                    new CommandParameter() {ParameterName = "pNO_OF_DELV_QTY_PRINT", Value = ob.NO_OF_DELV_QTY_PRINT},
                    new CommandParameter() {ParameterName = "pSMPL_REV_NO", Value = ob.SMPL_REV_NO},
                    new CommandParameter() {ParameterName = "pSMPL_OPT_NO", Value = ob.SMPL_OPT_NO},
                    new CommandParameter() {ParameterName = "pSMPL_ACT_FAB_GSM", Value = ob.SMPL_ACT_FAB_GSM},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3007}
                }, sp);

            ds.Tables[0].TableName = "SMPL_CARD";            
            return ds;
        }

        public DataSet getTnATaskListByOrderID()
        {
            var ob = this;
            string sp = "pkg_tna.mc_tna_task_select_for_rpt";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	            
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);

            ds.Tables[0].TableName = "TNA_TASK_LIST";
            return ds;
        }


        public DataSet ProjectionBooking()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	            
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3009}
                }, sp);

            ds.Tables[0].TableName = "PB";
            return ds;
        }


        public DataSet DyeingOrd4Trims()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	            
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3010}
                }, sp);

            ds.Tables[0].TableName = "DYEING_ORD4TRIMS";
            return ds;
        }
        
        public DataSet DFTnaFailReport()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	            
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3011}
                }, sp);

            ds.Tables[0].TableName = "DfTnaFailReport";
            return ds;
        }

        public DataSet DeptWiseTnaFailReport()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3014}
                }, sp);

            ds.Tables[0].TableName = "DEPT_TNA_FAIL_RPT";
            return ds;
        }

        public DataSet DeptByrWiseTnaFailReport()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3015}
                }, sp);

            ds.Tables[0].TableName = "DEPT_BYR_TNA_FAIL";
            return ds;
        }

        public DataSet DeptStyleWiseTnaFailReport()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3016}
                }, sp);

            ds.Tables[0].TableName = "DEPT_STYLE_TNA_FAIL";
            return ds;
        }


        
        public DataSet getInquiryList()
        {
            var ob = this;
            string sp = "pkg_inquiry.mkt_inqr_quot_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	            
                    new CommandParameter() {ParameterName = "pLK_INQ_STATUS_ID", Value = ob.LK_INQ_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLK_ITEM_GRP_ID", Value = ob.LK_ITEM_GRP_ID},
                    new CommandParameter() {ParameterName = "pINQR_NO", Value = ob.INQR_NO},
                    new CommandParameter() {ParameterName = "pINQR_DT_FROM", Value = ob.INQR_DT_FROM},
                    new CommandParameter() {ParameterName = "pINQR_DT_TO", Value = ob.INQR_DT_TO},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3006}
                }, sp);

            ds.Tables[0].TableName = "RPT_INQUIRY";
            ds.Tables[1].TableName = "COMPANY_INFO";
            return ds;
        }



        public DataSet AccPurchaseRequisition()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	            
                    new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3012}
                }, sp);

            ds.Tables[0].TableName = "BOM";
            return ds;
        }


        public DataSet dsOrderWiseAccStatus()
        {
            var ob = this;
            string sp = "pkg_reports.mrc_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	            
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3013}
                }, sp);

            ds.Tables[0].TableName = "ACC";
            return ds;
        }
        public DataSet getInquiryDetailsList()
        {
            var ob = this;
            string sp = "pkg_inquiry.mkt_inqr_quot_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pLK_INQ_STATUS_ID", Value = ob.LK_INQ_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLK_ITEM_GRP_ID", Value = ob.LK_ITEM_GRP_ID},
                    new CommandParameter() {ParameterName = "pINQR_NO", Value = ob.INQR_NO},
                    new CommandParameter() {ParameterName = "pINQR_DT_FROM", Value = ob.INQR_DT_FROM},
                    new CommandParameter() {ParameterName = "pINQR_DT_TO", Value = ob.INQR_DT_TO},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},

                    new CommandParameter() {ParameterName = "pMKT_INQR_QUOT_ID", Value = ob.MKT_INQR_QUOT_ID},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3007}
                }, sp);

            ds.Tables[0].TableName = "RPT_INQ_DETAILS";
            ds.Tables[1].TableName = "COMPANY_INFO";
            return ds;

        }



        public Int32? LK_INQ_STATUS_ID { get; set; }
        public Int32? LK_ITEM_GRP_ID { get; set; }
        public DateTime? INQR_DT_FROM { get; set; }
        public DateTime? INQR_DT_TO { get; set; }
        public Int32? SC_USER_ID { get; set; }
        public Int32? MC_BUYER_ID { get; set; }
        public string INQR_NO { get; set; }

        public Int32? SCM_PURC_REQ_H_ID { get; set; }

        public Int32? CM_IMP_PO_H_ID { get; set; }
        public Int64? MKT_INQR_QUOT_ID { get; set; }
    }
}