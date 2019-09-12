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
    public class CutReportModel
    {
        public string REPORT_CODE { get; set; }
        public string IS_EXCEL_FORMAT { get; set; }
        public string IS_MAIL_WITH_ATTACH { get; set; }
        public string PAGE_SIZE_NAME { get; set; }

        public DateTime? FROM_MONTH { get; set; }
        public DateTime? TO_MONTH { get; set; }
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public Int64? GMT_CUT_INFO_ID { get; set; }
        public string RF_GARM_PART_LST { get; set; }
        public string RF_GARM_PART_NAME_LST { get; set; }
        public string ADDNL_SERVICE_NAME { get; set; }
        public Int64? MC_ORDER_SIZE_ID { get; set; }
        public string BUNDLE_NO_LST { get; set; }
        public Int64? GRD_SL { get; set; }
        public Int64? BNDL_PRINT_TYP_ID { get; set; }
        public Int64? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public Int64? GMT_CUT_TABLE_ID { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? GMT_CUT_SEW_DLV_CHL_ID { get; set; }
        public Int64? GMT_CUT_PRN_DELV_CHL_H_ID { get; set; }
        
        


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

        public DataSet BundleCard()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                    new CommandParameter() {ParameterName = "pADDNL_SERVICE_NAME", Value = ob.ADDNL_SERVICE_NAME},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4000}
                }, sp);

            ds.Tables[0].TableName = "BUNDLE_CARD";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet BundleCardReprint()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                    new CommandParameter() {ParameterName = "pADDNL_SERVICE_NAME", Value = ob.ADDNL_SERVICE_NAME},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},

                    new CommandParameter() {ParameterName = "pMC_ORDER_SIZE_ID", Value = ob.MC_ORDER_SIZE_ID},
                    new CommandParameter() {ParameterName = "pBUNDLE_NO_LST", Value = ob.BUNDLE_NO_LST},
                    new CommandParameter() {ParameterName = "pGRD_SL", Value = ob.GRD_SL},

		            new CommandParameter() {ParameterName = "pOption", Value = 4002}
                }, sp);

            ds.Tables[0].TableName = "BUNDLE_CARD";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet BundleChart()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},   
                    new CommandParameter() {ParameterName = "pADDNL_SERVICE_NAME", Value = ob.ADDNL_SERVICE_NAME},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
		            new CommandParameter() {ParameterName = "pOption", Value = 4001}
                }, sp);

            ds.Tables[0].TableName = "BUNDLE_CHART";
            ds.Tables[1].TableName = "BNDL_SIZE_RATIO";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet BundleCard4Piping()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                    new CommandParameter() {ParameterName = "pADDNL_SERVICE_NAME", Value = ob.ADDNL_SERVICE_NAME},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_NAME_LST", Value = ob.RF_GARM_PART_NAME_LST},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4003}
                }, sp);

            ds.Tables[0].TableName = "BUNDLE_CARD";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet BundleCard4FullCake()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                    new CommandParameter() {ParameterName = "pADDNL_SERVICE_NAME", Value = ob.ADDNL_SERVICE_NAME},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
             
		            new CommandParameter() {ParameterName = "pOption", Value = ob.BNDL_PRINT_TYP_ID==5 ? 4004 : 4005}
                }, sp);

            ds.Tables[0].TableName = "BUNDLE_CARD";
            ds.Tables[1].TableName = "BNDL_SIZE_RATIO";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet BundleChart4FullCake()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                    new CommandParameter() {ParameterName = "pADDNL_SERVICE_NAME", Value = ob.ADDNL_SERVICE_NAME},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
             
		            new CommandParameter() {ParameterName = "pOption", Value = ob.BNDL_PRINT_TYP_ID==7 ? 4006 : 4007}
                }, sp);

            ds.Tables[0].TableName = "BUNDLE_CHART";
            ds.Tables[1].TableName = "BNDL_SIZE_RATIO";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet BundleChart4CutPanelInsp()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                    new CommandParameter() {ParameterName = "pADDNL_SERVICE_NAME", Value = ob.ADDNL_SERVICE_NAME},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4008}
                }, sp);

            ds.Tables[0].TableName = "BUNDLE_CHART";
            ds.Tables[1].TableName = "BUNDLE_CHART_RATIO_SRC";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet CutPanelInspSummeryDateOrdWise()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	     
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4009}
                }, sp);

            ds.Tables[0].TableName = "CUT_PANEL_INSP_SUMMERY";
            ds.Tables[1].TableName = "CUT_PANEL_INSP_SUMMERY01";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet CutPanelInspSummeryDateWise()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	     
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4021}
                }, sp);

            ds.Tables[0].TableName = "CUT_PANEL_INSP_SUMMERY";
            ds.Tables[1].TableName = "CUT_PANEL_INSP_SUMMERY01";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet CutPanelInspSummeryOrdColWise()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    
                    //new CommandParameter() {ParameterName = "pADDNL_SERVICE_NAME", Value = ob.ADDNL_SERVICE_NAME},
                    //new CommandParameter() {ParameterName = "pRF_GARM_PART_NAME_LST", Value = ob.RF_GARM_PART_NAME_LST},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4010}
                }, sp);

            ds.Tables[0].TableName = "CUT_PANEL_INSP_SUMMERY_ORD_COL";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet CutPanelInspSummeryOrdColCutNoWise()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 4014}
                }, sp);

            ds.Tables[0].TableName = "CUT_PANEL_INSP_SUMMERY_ORD_COL_CUT";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet CuttingInfoByDate()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4015}
                }, sp);

            ds.Tables[0].TableName = "CUT_INFO_01";
            ds.Tables[1].TableName = "CUT_INFO_02";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet CuttingInfoByDateAndCutting()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4023}
                }, sp);

            ds.Tables[0].TableName = "CUT_INFO_01";
            
            return ds;
        }

        public DataSet TableWiseCuttingProd()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4011}
                }, sp);

            ds.Tables[0].TableName = "TABLE_WISE_CUT_PROD";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet CuttingProdVsAchive()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    //new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    //new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    //new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    //new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pGMT_CUT_TABLE_ID", Value = ob.GMT_CUT_TABLE_ID},
                                   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4012}
                }, sp);

            ds.Tables[0].TableName = "CUT_PROD_VS_ACHIVE";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet CutOrdColorWiseSummery()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4013}
                }, sp);

            ds.Tables[0].TableName = "CUT_ORD_COLOR_SUMMERY";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet getGarmentItems()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	  
		            new CommandParameter() {ParameterName = "pOption", Value = 4016}
                }, sp);

            ds.Tables[0].TableName = "GARMENT_ITEM";

            return ds;
        }

        public DataSet SewingDeliveryChallan()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value = ob.GMT_CUT_SEW_DLV_CHL_ID},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 4017}
                }, sp);

            ds.Tables[0].TableName = "SewingDeliveryChallan";
            
            return ds;
        }

        public DataSet EmbellishDeliveryChallan()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_ID", Value = ob.GMT_CUT_PRN_DELV_CHL_H_ID},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 4018}
                }, sp);

            ds.Tables[0].TableName = "EmbellishDeliveryChallan";

            return ds;
        }
                
        public DataSet DailyCuttingProduction()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 4019}
                }, sp);

            ds.Tables[0].TableName = "DailyCuttingProduction";

            return ds;
        }

        public DataSet MonthlyTableWiseCuttingProd()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_MONTH", Value = ob.FROM_MONTH},
                    new CommandParameter() {ParameterName = "pTO_MONTH", Value = ob.TO_MONTH},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4020}
                }, sp);

            ds.Tables[0].TableName = "TABLE_WISE_CUT_PROD";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet ByrWiseInputBal()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
             
		            new CommandParameter() {ParameterName = "pOption", Value = 4022}
                }, sp);

            ds.Tables[0].TableName = "BYR_WISE_INPUT_BAL";
            ds.Tables[5].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet BundleChart4EmbellishDelvChalan()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_ID", Value = ob.GMT_CUT_PRN_DELV_CHL_H_ID},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 4024}
                }, sp);

            ds.Tables[0].TableName = "BundleChart4EmbellishDelvChalan";

            return ds;
        }

        public DataSet BundleChart4SewingInput()
        {
            var ob = this;
            string sp = "pkg_cut_report.cut_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value = ob.GMT_CUT_SEW_DLV_CHL_ID},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 4025}
                }, sp);

            ds.Tables[0].TableName = "SewingDeliveryChallan";

            return ds;
        }
    }
}