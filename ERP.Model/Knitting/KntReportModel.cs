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
    public class KntReportModel
    {
        public string REPORT_CODE { get; set; }
        public string IS_EXCEL_FORMAT { get; set; }
        public string IS_MAIL_WITH_ATTACH { get; set; }
        public string PAGE_SIZE_NAME { get; set; }

        public Int64? KNT_YRN_STR_REQ_H_ID { get; set; }
        public Int64? KNT_SC_PRG_ISS_ID { get; set; }
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public Int32? RF_YRN_CNT_ID { get; set; }
        public Int32? RF_FIB_COMP_ID { get; set; }
        public Int32? RF_REQ_TYPE_ID { get; set; }
        public Int32? LK_SPN_PRCS_ID { get; set; }
        public Int32? LK_COTN_TYPE_ID { get; set; }
        public string IS_SLUB { get; set; }
        public string IS_MELLANGE { get; set; }
        public Int64? KNT_YRN_LOT_ID { get; set; }
        public string YRN_LOT_NO { get; set; }
        public Int32? RF_BRAND_ID { get; set; }
        public Int32? SCM_STORE_ID { get; set; }
        public string STORE_NAME_EN { get; set; }
        public Int32? MC_BYR_ACC_ID { get; set; }
        public Int32? MC_BYR_ACC_GRP_ID { get; set; }
        public Int32? RF_FAB_PROD_CAT_ID { get; set; }
        public string RF_FAB_PROD_CAT_ID_LST { get; set; }
        public string RF_FAB_PROD_CAT_NAME_LST { get; set; }

        public Int64? SCM_PURC_REQ_H_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? KNT_SC_PRG_RCV_ID { get; set; }
        public string SC_FAB_DELV_CHALAN_NO { get; set; }
        public string ISS_CHALAN_NO { get; set; }
        public string RET_CHALAN_NO { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }
        public Int64? SCM_SC_WO_REF_ID { get; set; }
        public Int64? KNT_YRN_ISS_H_ID { get; set; }
        public Int64? KNT_QC_STS_TYPE_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public Int64? KNT_SCO_GFAB_RET_H_ID { get; set; }
        public string IMP_LC_NO { get; set; }
        public string CHALAN_NO { get; set; }
        public Int64? KNT_SCO_YRN_TR_H_ID { get; set; }
        public Int64? KNT_YD_PRG_H_ID { get; set; }
        public Int64? KNT_YD_RCV_H_ID { get; set; }
        public Int64? KNT_SCO_YD_TR_CHL_H_ID { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public string MONTHOF { get; set; }
        public Int64? NO_OF_COUNT { get; set; }
        public string JOB_CRD_NO { get; set; }
        public Int64? KNT_SCO_CLCF_PRG_H_ID { get; set; }
        public Int64? KNT_SCO_CHL_RCV_H_ID { get; set; }
        public Int64? HR_SCHEDULE_H_ID { get; set; }
        public string FAB_ROLL_NO { get; set; }
        public Int64? RF_FAB_TYPE_ID { get; set; }
        public Int64? KNT_SCI_BILL_H_ID { get; set; }

        public string STYLE_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string QC_STS_TYP_NAME { get; set; }
        public string SCHEDULE_NAME_EN { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string KNT_SC_PRG_ISS_NAME { get; set; }
        public string FAB_TYPE_NAME { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string IS_YD { get; set; }
        public Int64? MC_FAB_PROC_GRP_ID { get; set; }
        public Int64? LK_YRN_TST_TYPE_ID { get; set; }
        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }
        public string MC_BYR_ACC_ID_LST { get; set; }
        public DateTime? FIRSTDATE { get; set; }
        public DateTime? LASTDATE { get; set; }
        public DateTime? pFROM_DATE { get; set; }
        public DateTime? pTO_DATE { get; set; }
        public Int64? SCM_STR_NDL_REQ_H_ID { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }
        public Int64? HR_COMPANY_ID { get; set; }
        public Int64? KNT_MACHINE_ID { get; set; }
        public Int64? KNT_MC_TYPE_ID { get; set; }
        public Int64? HR_OFFICE_ID { get; set; }
        public string KNT_JOB_CRD_H_ID_LST { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? SCM_SC_QUOT_RATE_H_ID { get; set; }
        public string IS_FLAT_CIR { get; set; }





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

        public DataSet KnittingSubContractProgramIssue()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3501}
                }, sp);

            ds.Tables[0].TableName = "KNT_SC_PRG_ISS";

            return ds;
        }

        public DataSet KntDyedYrnDelivChlnTrans2ScParty()
        {
            var ob = this;
            string sp = "PKG_KNT_REPORT.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pKNT_SCO_YD_TR_CHL_H_ID", Value = ob.KNT_SCO_YD_TR_CHL_H_ID},
                    //new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},                    
                    //new CommandParameter() {ParameterName = "pISS_CHALAN_NO", Value = ob.ISS_CHALAN_NO},                                      
                    //new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},

		            new CommandParameter() {ParameterName = "pOption", Value = 3582}
                }, sp);

            ds.Tables[0].TableName = "KNT_SC_YRN_DELIV_CHLN_ISS";

            return ds;
        }

        public DataSet KntSubContrYrnDelivChlnIssue()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
                    
                    new CommandParameter() {ParameterName = "pISS_CHALAN_NO", Value = ob.ISS_CHALAN_NO},                                      
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},

                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},


		            new CommandParameter() {ParameterName = "pOption", Value = 3502}
                }, sp);

            ds.Tables[0].TableName = "KNT_SC_YRN_DELIV_CHLN_ISS";

            return ds;
        }


        public DataSet KntYrnDelivChlnIssue()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
                    
                    new CommandParameter() {ParameterName = "pISS_CHALAN_NO", Value = ob.ISS_CHALAN_NO},                                      
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},

		            new CommandParameter() {ParameterName = "pOption", Value = 3560}
                }, sp);

            ds.Tables[0].TableName = "KNT_SC_YRN_DELIV_CHLN_ISS";

            return ds;
        }

        public DataSet KntYrnRcvStatement()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3503}
                }, sp);

            ds.Tables[0].TableName = "YRN_RCV_STATEMENT";

            return ds;
        }

        public DataSet KntYrnCurrentStock()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                    new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                    new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                    new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                    new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},
                    new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3504}
                }, sp);

            ds.Tables[0].TableName = "YRN_CURRENT_STOCK";

            return ds;
        }

        public DataSet KntOrdWiseYrnIssRegister()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                    new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                    new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                    new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                    new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pIS_FLAT_CIR", Value = ob.IS_FLAT_CIR},

                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3505}
                }, sp);

            ds.Tables[0].TableName = "ORD_WISE_YRN_ISS_REGISTER";

            return ds;
        }

        public DataSet KntYrnIssStatement()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                    new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                    new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                    new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                    new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3506}
                }, sp);

            ds.Tables[0].TableName = "YRN_ISS_STATEMENT";

            return ds;
        }

        public DataSet KntMchLocWiseYrnReqSummery()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                    new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                    new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                    new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                    new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                                        
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = Convert.ToDateTime(ob.FROM_DATE).Date},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = Convert.ToDateTime(ob.TO_DATE).Date},
		            new CommandParameter() {ParameterName = "pOption", Value = 3507}
                }, sp);

            ds.Tables[0].TableName = "MCH_WISE_YRN_REQ_SUMMERY";

            return ds;
        }

        public DataSet KntScFabDelvChallan()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = ob.KNT_SC_PRG_RCV_ID},
                    new CommandParameter() {ParameterName = "pSCM_SC_WO_REF_ID", Value = ob.SCM_SC_WO_REF_ID},
                    new CommandParameter() {ParameterName = "pSC_FAB_DELV_CHALAN_NO", Value = ob.SC_FAB_DELV_CHALAN_NO},                                      
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3508}
                }, sp);

            ds.Tables[0].TableName = "SC_FAB_DELV_CHALLAN";
            ds.Tables[1].TableName = "SCI_YRN_RTN_CHALLAN";

            return ds;
        }

        public DataSet YarnReturnChallanTest()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},                    
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3509}
                }, sp);

            ds.Tables[0].TableName = "YARN_RETURN_CHALLAN_TEST";

            return ds;
        }

        public DataSet YarnReturnChallanSample()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},                    
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3510}
                }, sp);

            ds.Tables[0].TableName = "YARN_RETURN_CHALLAN_SAMPLE";

            return ds;
        }

        public DataSet YarnTestReq()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},                    
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3511}
                }, sp);

            ds.Tables[0].TableName = "YARN_TEST_REQ";

            return ds;
        }

        public DataSet YarnReq4KnittingSmp()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                    new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                    new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                    new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                    new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                                        
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3512}
                }, sp);

            ds.Tables[0].TableName = "YRN_REQ4KNITTING_SMP";

            return ds;
        }

        public DataSet YarnReq4KnittingSmpYD()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                    new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                    new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                    new CommandParameter() {ParameterName = "pIS_SLUB", Value = ob.IS_SLUB},
                    new CommandParameter() {ParameterName = "pIS_MELLANGE", Value = ob.IS_MELLANGE},
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                                        
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3553}
                }, sp);

            ds.Tables[0].TableName = "YRN_REQ4KNITTING_SMP";

            return ds;
        }


        public DataSet YarnReq4KntClcf()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID_LST", Value = ob.RF_FAB_PROD_CAT_ID_LST},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                                        
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3583}
                }, sp);

            ds.Tables[0].TableName = "YRN_REQ4KNT_CLCF";

            return ds;
        }

        public DataSet YarnReqWithIssue()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pKNT_YRN_ISS_H_ID", Value = ob.KNT_YRN_ISS_H_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3513}
                }, sp);

            return ds;
        }


        public DataSet GreyStoreLedgerByParty()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = ob.KNT_SC_PRG_RCV_ID},
                    new CommandParameter() {ParameterName = "pSCM_SC_WO_REF_ID", Value = ob.SCM_SC_WO_REF_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3514}
                }, sp);

            ds.Tables[0].TableName = "SC_FAB_REG_BY_PARTY";
            ds.Tables[1].TableName = "SC_YRN_REG_BY_PARTY";

            return ds;
        }


        public DataSet GreyStoreLedgerByPartyOutHouse()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3517}
                }, sp);

            ds.Tables[0].TableName = "SC_FAB_REG_BY_PARTY";
            ds.Tables[1].TableName = "SC_YRN_REG_BY_PARTY";

            return ds;
        }

        public DataSet GreyFabDelvChalnToStore()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID_LST", Value = ob.RF_FAB_PROD_CAT_ID_LST},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3515}

                }, sp);

            ds.Tables[0].TableName = "GREY_FAB_DELV_CHALN_TO_STORE";

            return ds;
        }

        public DataSet GreyFabricRollInspection()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
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
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value = ob.FAB_ROLL_NO},
                    new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = ob.JOB_CRD_NO},
                    new CommandParameter() {ParameterName = "pGREY_QC_BY", Value = ob.GREY_QC_BY},
                    new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3516}

                }, sp);

            ds.Tables[0].TableName = "GREY_FAB_DELV_CHALN_TO_STORE";

            return ds;
        }


        public DataSet YarnStockWithValue()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},
                    new CommandParameter() {ParameterName = "pIMP_LC_NO", Value = ob.IMP_LC_NO},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3518}

                }, sp);

            ds.Tables[0].TableName = "GREY_FAB_DELV_CHALN_TO_STORE";

            return ds;
        }

        public DataSet OrderColorFabricWiseKntStatement()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},                    
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3519}

                }, sp);

            ds.Tables[0].TableName = "MC_SHIFT_WISE_KNIT";
            ds.Tables[1].TableName = "MC_SHIFT_WISE_KNIT1";
            ds.Tables[2].TableName = "MC_SHIFT_WISE_KNIT2";

            return ds;
        }


        public DataSet KntScoRejGreyFabRtnChallan()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_SCO_GFAB_RET_H_ID", Value = ob.KNT_SCO_GFAB_RET_H_ID},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3520}

                }, sp);

            ds.Tables[0].TableName = "SCO_REJ_GF_RTN_CHALN";

            return ds;
        }


        public DataSet DateWisePendingList()
        {
            var ob = this;
            string sp = "PKG_MC_LOAD_PLAN.KNT_PENDING_KC_select";
            OraDatabase db = new OraDatabase();

            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
            ds.Tables[0].TableName = "PendingKnitCardList";
            return ds;
        }

        public DataSet ScoChlnWisePartyLedger()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3522}

                }, sp);

            ds.Tables[0].TableName = "SCO_CHLN_WISE_PARTY_LGR";

            return ds;
        }

        public DataSet ScoOrderStatement()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3523}

                }, sp);

            ds.Tables[0].TableName = "SCO_ORDER_STATEMENT";

            return ds;
        }

        public DataSet ScoProgTransYrnDelivChln()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pKNT_SCO_YRN_TR_H_ID", Value = ob.KNT_SCO_YRN_TR_H_ID},
                    new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3524}

                }, sp);

            ds.Tables[0].TableName = "SCO_PROG_TRANS_YRN_DELV_CHLN";

            return ds;
        }

        public DataSet OrderAndColorWiseInspection()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                    new CommandParameter() {ParameterName = "pMONTHOF", Value = ob.MONTHOF},
		            new CommandParameter() {ParameterName = "pOption", Value = 3527}

                }, sp);

            ds.Tables[0].TableName = "GREY_FAB_DELV_CHALN_TO_STORE";

            return ds;
        }


        public DataSet OrdColWiseFabRcvToGreyStore()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3525}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_FAB_RCV_TO_STORE";

            return ds;
        }

        public DataSet MonthlyProdCapacityVsAchieve()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3526}

                }, sp);

            ds.Tables[0].TableName = "MONTHLY_PROD_CAPACITY_ACHIEVE";

            return ds;
        }


        public DataSet DailyGrayQCSummary()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
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
		            new CommandParameter() {ParameterName = "pOption", Value = 3529}

                }, sp);

            ds.Tables[0].TableName = "GREY_FAB_DELV_CHALN_TO_STORE";

            return ds;
        }

        public DataSet MonthlyOutSideQualitySummary()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3530}

                }, sp);

            ds.Tables[0].TableName = "MOSQS";

            return ds;
        }

        public DataSet YarnDyeingProgramData()
        {
            var ob = this;
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3011}
                }, sp);

            ds.Tables[0].TableName = "YarnDyeingProgram";

            return ds;
        }

        public DataSet OrdColWiseProdStatus()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMONTHOF", Value = ob.MONTHOF},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3531}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_PROD_STATUS";

            return ds;
        }

        public DataSet LotWiseLooseYrnStatement()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {      
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3532}

                }, sp);

            ds.Tables[0].TableName = "LOT_WISE_LOOSE_YRN_STATEMENT";

            return ds;
        }

        public DataSet SupplierWiseYarnTestResult()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pLK_YRN_TST_TYPE_ID", Value = ob.LK_YRN_TST_TYPE_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3533}

                }, sp);

            ds.Tables[0].TableName = "MOSQS";

            return ds;
        }
        public DataSet LotCountWiseLooseYarnStock()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3534}

                }, sp);

            ds.Tables[0].TableName = "MOSQS";

            return ds;
        }

        public DataSet ScoPartyWisePerformance()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3535}

                }, sp);

            ds.Tables[0].TableName = "SCO_PARTY_WISE_PERFORMANCE";

            return ds;
        }

        public DataSet ScoPartyWisePerformanceSummery()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pNO_OF_COUNT", Value = ob.NO_OF_COUNT},                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3540}

                }, sp);

            ds.Tables[0].TableName = "SCO_PARTY_WISE_PERF_SUMMERY";
            ds.Tables[1].TableName = "SCO_PARTY_WISE_PERF_SUMM_BY_ONTIME_DELV";

            return ds;
        }

        public DataSet OrdColWiseFabProd()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMONTHOF", Value = ob.MONTHOF},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3537}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_PROD_STATUS";

            return ds;
        }

        public DataSet SciPartyWiseStkSummery()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pSCM_SC_WO_REF_ID", Value = ob.SCM_SC_WO_REF_ID},                                       
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3588}

                }, sp);

            ds.Tables[0].TableName = "SCI_PARTY_STK_SUMMERY";

            return ds;
        }

        public DataSet ScoPartyOrdColWiseRcvRegister()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = ob.JOB_CRD_NO},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3589}

                }, sp);

            ds.Tables[0].TableName = "SCO_PARTY_ORD_COL_RCV_REG";

            return ds;
        }

        public DataSet OrdColFinDiaWiseYrnAllocProd()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3590}

                }, sp);

            ds.Tables[0].TableName = "SCO_PARTY_ORD_COL_RCV_REG";

            return ds;
        }

        public DataSet OrdColWiseSummery()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3593}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_SUMMERY";

            return ds;
        }

        public DataSet OrdColWiseGreyFabStk()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3602}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_GREY_STK";

            return ds;
        }

        public DataSet OrdColWiseGreyFabStkByDiaYarn()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3606}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_GREY_STK";

            return ds;
        }

        public DataSet DailyOrdColWiseKntProd()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3603}

                }, sp);

            ds.Tables[0].TableName = "DAILY_ORD_COL_KNT_PROD";
            ds.Tables[1].TableName = "DAILY_ORD_COL_KNT_PROD_SUMRY";

            return ds;
        }

        public DataSet OrdColWiseGfabTransStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                                                      
		            new CommandParameter() {ParameterName = "pOption", Value = 3604}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_GFAB_TRANS_STATE";
            
            return ds;
        }

        public DataSet OrdColWiseFabDelivStatus()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                                      
		            new CommandParameter() {ParameterName = "pOption", Value = 3605}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_WISE_FAB_DELIV_STATUS";

            return ds;
        }

        public DataSet ScoClcfRecvChaln()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {    
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},                                        
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE}, 
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE}, 

		            new CommandParameter() {ParameterName = "pOption", Value = 3599}

                }, sp);

            ds.Tables[0].TableName = "SCO_CLCF_RECV_CHALN";

            return ds;
        }

        public DataSet SciRollAvailableGreyStore()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {    
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pSCM_SC_WO_REF_ID", Value = ob.SCM_SC_WO_REF_ID},                                       
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3600}

                }, sp);

            ds.Tables[0].TableName = "ORD_COL_SUMMERY";

            return ds;
        }

        public DataSet YarnInventoryAgeing()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3536}

                }, sp);

            ds.Tables[0].TableName = "MOSQS";

            return ds;
        }

        public DataSet MonthlyGreyQCSummary()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
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
		            new CommandParameter() {ParameterName = "pOption", Value = 3538}

                }, sp);

            ds.Tables[0].TableName = "GREY_FAB_DELV_CHALN_TO_STORE";

            return ds;
        }


        public DataSet MonthlyGreyQCSummaryWithDefectType()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
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
		            new CommandParameter() {ParameterName = "pOption", Value = 3539}

                }, sp);

            ds.Tables[0].TableName = "AAAA";

            return ds;
        }

        public DataSet ScoCollarCuffProg()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = ob.KNT_SCO_CLCF_PRG_H_ID},                                                                   
		            new CommandParameter() {ParameterName = "pOption", Value = 3541}

                }, sp);

            ds.Tables[0].TableName = "SCO_COLLAR_CUFF_PROG";
            ds.Tables[1].TableName = "SCO_COLLAR_CUFF_YRN_REQ";

            return ds;
        }

        public DataSet ScoRollLabellingStatment()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},                    
                    new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = ob.JOB_CRD_NO},
                    new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = ob.KNT_SCO_CHL_RCV_H_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3542}

                }, sp);

            ds.Tables[0].TableName = "SCO_ROLL_LABELLING_STATEMENT";

            return ds;
        }

        public DataSet McOilConsumpShiftWise()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},                                        
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3543}

                }, sp);

            ds.Tables[0].TableName = "MC_OIL_CONSUMP_SHIFT_WISE";

            return ds;
        }
        public DataSet McOilConsumpOperatorWise()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},                                        
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3544}

                }, sp);

            ds.Tables[0].TableName = "MC_OIL_CONSUMP_OPERATOR_WISE";

            return ds;
        }
        public DataSet DailyClCfTransToQC()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},                                        
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3545}

                }, sp);

            ds.Tables[0].TableName = "CLCF_TRANS_TO_QC";

            return ds;
        }
        public DataSet OrdColorWiseClCfProd()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = 2},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},                                                            
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3547}

                }, sp);

            ds.Tables[0].TableName = "CLCF_REQD";
            ds.Tables[1].TableName = "CLCF_PROD";

            return ds;
        }
        public DataSet DailyCollarCuffProd()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},                                                            
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3548}

                }, sp);

            ds.Tables[0].TableName = "CLCF_PROD";
            ds.Tables[1].TableName = "PROD_SUMMERY";
            ds.Tables[2].TableName = "FT_PROD_SUMMERY";
            ds.Tables[3].TableName = "NDL_BRK_SUMMERY";

            return ds;
        }
        public DataSet RollNoWiseGreyFabDelvChalnToStore()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID_LST", Value = ob.RF_FAB_PROD_CAT_ID_LST},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3546}

                }, sp);

            ds.Tables[0].TableName = "GREY_FAB_DELV_CHALN_TO_STORE";

            return ds;
        }
        public DataSet ScPartyWiseRate()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},                                                            
                    new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3549}

                }, sp);

            ds.Tables[0].TableName = "RATE";

            return ds;
        }

        public DataSet getDyedYarnLedger()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},    
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                    new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},

		            new CommandParameter() { ParameterName = "pOption", Value = 3576}
                }, sp);

            ds.Tables[0].TableName = "YD_LEDGER";
            return ds;
        }

        public DataSet ExcessYarnStatement()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},             
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3550}

                }, sp);

            ds.Tables[0].TableName = "EXCESS_YARN_STATM";

            return ds;
        }

        public DataSet ExcessYrnUsageStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},             
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3584}

                }, sp);

            ds.Tables[0].TableName = "EXCESS_YARN_USAGE_STATE_01";

            return ds;
        }

        public DataSet McNeedleBroken()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},             
                    new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value = ob.KNT_MC_TYPE_ID},             
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},             
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3551}

                }, sp);

            ds.Tables[0].TableName = "MC_NEEDLE_BROKEN";

            return ds;
        }

        public DataSet YarnReqSmpByReqNo()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},                                 
		            new CommandParameter() {ParameterName = "pOption", Value = 3552}

                }, sp);

            ds.Tables[0].TableName = "YRN_REQ_SMP_BY_REQ_NO";

            return ds;
        }




        public DataSet NeedleRequisitionSlip()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3553}

                }, sp);

            ds.Tables[0].TableName = "Req";

            return ds;
        }


        public DataSet NeedleStockTaking()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = Convert.ToDateTime(ob.FROM_DATE).ToString("yyyy-MM-dd")},  
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = Convert.ToDateTime(ob.TO_DATE).ToString("yyyy-MM-dd")},  
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},  
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},  
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},  
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},  
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},  
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value =  HttpContext.Current.Session["multiScUserId"]},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3555}

                }, sp);

            ds.Tables[0].TableName = "Req";

            return ds;
        }

        public DataSet NeedelOpeningBalance()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},  
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3556}

                }, sp);

            ds.Tables[0].TableName = "dsNeedelOpeningBalance";

            return ds;
        }

        public DataSet OilOpeningBalance()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},  
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3557}

                }, sp);

            ds.Tables[0].TableName = "dsNeedelOpeningBalance";

            return ds;
        }


        public DataSet ScPartyRate()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID}, 
                    new CommandParameter() {ParameterName = "pSCM_SC_QUOT_RATE_H_ID", Value = ob.SCM_SC_QUOT_RATE_H_ID},                     
		            new CommandParameter() {ParameterName = "pOption", Value = 3558}

                }, sp);

            ds.Tables[0].TableName = "ScPartyRate";

            return ds;
        }

        public DataSet OilStockTaking()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},  
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},  
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},  
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},  
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},  
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value =  HttpContext.Current.Session["multiScUserId"]},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3559}

                }, sp);

            ds.Tables[0].TableName = "Req";

            return ds;
        }


        public DataSet DailyMachineOilConsumption()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},  
                    new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},  
                    new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value = ob.KNT_MC_TYPE_ID},  
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},   
		            new CommandParameter() {ParameterName = "pOption", Value = 3561}

                }, sp);

            ds.Tables[0].TableName = "Req";

            return ds;
        }

        public DataSet OrderWiseYarnRequisition()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},   
		            new CommandParameter() {ParameterName = "pOption", Value = 3562}

                }, sp);

            ds.Tables[0].TableName = "Req";

            return ds;
        }


        public DataSet ScInhouseBill()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_SCI_BILL_H_ID", Value = ob.KNT_SCI_BILL_H_ID},                   
		            new CommandParameter() {ParameterName = "pOption", Value = 3554}
                }, sp);

            ds.Tables[0].TableName = "SC_INHOUSE_BILL";

            return ds;
        }

        public DataSet MultiKnitCard()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID_LST", Value = ob.KNT_JOB_CRD_H_ID_LST},                   
		            new CommandParameter() {ParameterName = "pOption", Value = 3555}
                }, sp);

            ds.Tables[0].TableName = "MULTI_KNIT_CARD";
            ds.Tables[5].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet ScoFabricRejectSummery()
        {
            var ob = this;
            string sp = "pkg_reports.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},    
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE}, 
		            new CommandParameter() {ParameterName = "pOption", Value = 3556}
                }, sp);

            ds.Tables[0].TableName = "SCO_FAB_REJECT";
            ds.Tables[5].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet KnittingProg4Sample()
        {
            try
            {
                var ob = this;
                string sp = "pkg_reports.knt_rpt_select";
                //DataSet ds = new DataSet();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},                   
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                    new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID_LST", Value = ob.KNT_JOB_CRD_H_ID_LST},
		            new CommandParameter() {ParameterName = "pOption", Value = 3557}
                }, sp);

                ds.Tables[0].TableName = "MULTI_KNIT_CARD_SMP";
                ds.Tables[5].TableName = "COMPANY_INFO";
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet OrdWiseCollarCuffStock()
        {
            try
            {
                var ob = this;
                string sp = "PKG_KNT_REPORT.knt_rpt_select";
                //DataSet ds = new DataSet();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},                   
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3569}
                }, sp);

                ds.Tables[0].TableName = "ORD_COLLAR_CUFF_STOCK";

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet YdStatement()
        {
            try
            {
                var ob = this;
                string sp = "PKG_KNT_REPORT.knt_rpt_select";
                //DataSet ds = new DataSet();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},                   
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},

                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3570}
                }, sp);

                ds.Tables[0].TableName = "YD_STATEMRNT";

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet SciPartyWiseOrderStatement()
        {
            try
            {
                var ob = this;
                string sp = "pkg_reports.knt_rpt_select";
                //DataSet ds = new DataSet();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},                   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pSCM_SC_WO_REF_ID", Value = ob.SCM_SC_WO_REF_ID},
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = ob.KNT_SC_PRG_RCV_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3558}
                }, sp);

                ds.Tables[0].TableName = "SCI_PARTY_ORD_STATEMRNT";

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ScoPartyWiseOrderStatement()
        {
            try
            {
                var ob = this;
                string sp = "pkg_reports.knt_rpt_select";
                //DataSet ds = new DataSet();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},                   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                                     
		            new CommandParameter() {ParameterName = "pOption", Value = 3559}
                }, sp);

                ds.Tables[0].TableName = "SCO_PARTY_ORD_STATEMRNT";

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet DailyTrimsReceiveStatement()
        {
            var ob = this;
            string sp = "PKG_KNT_REPORT.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE}, 
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID}, 
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID}, 
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3567}
                }, sp);

            ds.Tables[0].TableName = "TRIMS";
            return ds;
        }

        public DataSet DailyNeedleBrokenConsumption()
        {
            var ob = this;
            string sp = "PKG_KNT_REPORT.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE}, 
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID}, 
		            new CommandParameter() {ParameterName = "pOption", Value = 3568}
                }, sp);

            ds.Tables[0].TableName = "NDL";
            return ds;
        }


        public DataSet BufferYarnPurchaseRequisition()
        {
            var ob = this;
            string sp = "PKG_KNT_REPORT.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID}, 
		            new CommandParameter() {ParameterName = "pOption", Value = 3571}
                }, sp);

            ds.Tables[0].TableName = "NDL";
            return ds;
        }


        public DataSet SciPartyWiseBillStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3572}
                }, sp);

            ds.Tables[0].TableName = "SCI_PARTY_BILL_STATEMENT";
            ds.Tables[5].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet ScoPartyWiseBillStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3573}
                }, sp);

            ds.Tables[0].TableName = "SCO_PARTY_BILL_STATEMENT";
            ds.Tables[5].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet SciPartyWiseSummery()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3574}
                }, sp);

            ds.Tables[0].TableName = "SCI_PARTY_WISE_SUMMERY";
            ds.Tables[5].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet ScoLooseYarnStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},  
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3575}
                }, sp);

            ds.Tables[0].TableName = "SCO_LOOSE_YARN_STATEMENT";
            ds.Tables[5].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet NeedleReqTypeSummary()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},   
                    new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value = ob.KNT_MC_TYPE_ID},  
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},  
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3580}
                }, sp);

            return ds;
        }

        public DataSet ComparativeStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},   
                    new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3581}
                }, sp);

            return ds;
        }


        public DataSet ScPreTreatmentLedger()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},   
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},  
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},  
		            new CommandParameter() {ParameterName = "pOption", Value = 3585}
                }, sp);

            return ds;
        }

        public DataSet YarnAdvanceStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},   
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},   
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},   
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},   
		            new CommandParameter() {ParameterName = "pOption", Value = 3586}
                }, sp);

            return ds;
        }

        public DataSet LotWiseYarnStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},   
                    new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value = ob.YRN_LOT_NO},   
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},   
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},   
		            new CommandParameter() {ParameterName = "pOption", Value = 3587}
                }, sp);

            return ds;
        }

        public DataSet PartyWiseYarnIssueChallan()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_YRN_CHL_ISS_H_ID", Value = ob.KNT_YRN_CHL_ISS_H_ID},     
		            new CommandParameter() {ParameterName = "pOption", Value = 3592}
                }, sp);

            return ds;
        }


        public DataSet GreyQcDeductFabric()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},     
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},     
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},     
		            new CommandParameter() {ParameterName = "pOption", Value = 3594}
                }, sp);

            return ds;
        }

        public DataSet DeliveryChallanForYarnReturnToSupplier()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pKNT_YRN_RPL_ISS_H_ID", Value = ob.KNT_YRN_RPL_ISS_H_ID},                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3595}
                }, sp);

            return ds;
        }


        public DataSet PartyWiseYrnIssStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3596}
                }, sp);

            ds.Tables[0].TableName = "YRN_ISS_STATEMENT";

            return ds;
        }


        public DataSet DateWiseYrnIssReturnStatement()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
		            new CommandParameter() {ParameterName = "pOption", Value = 3597}
                }, sp);

            ds.Tables[0].TableName = "RTN";

            return ds;
        }
        
        public DataSet KnitMcOilStoreTransferRequisition()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pSCM_STR_OIL_REQ_H_ID", Value = ob.SCM_STR_OIL_REQ_H_ID},
                    new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3598}
                }, sp);

            ds.Tables[0].TableName = "RTN";

            return ds;
        }


        public DataSet RollListWithoutGreyQc()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},     
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},     
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},     
		            new CommandParameter() {ParameterName = "pOption", Value = 3601}
                }, sp);

            return ds;
        }


        public DataSet ShiftWiseGreyFabricRollInspection()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
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
                    new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value = ob.FAB_ROLL_NO},
                    new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = ob.JOB_CRD_NO},
                    new CommandParameter() {ParameterName = "pGREY_QC_BY", Value = ob.GREY_QC_BY},
                    new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                    new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3607}

                }, sp);

            ds.Tables[0].TableName = "GREY_FAB_DELV_CHALN_TO_STORE";

            return ds;
        }

        public DataSet SampleGreyFabricReceiveDeliveryStatus()
        {
            var ob = this;
            string sp = "pkg_knt_report.knt_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},     
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},     
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},     
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},     
		            new CommandParameter() {ParameterName = "pOption", Value = 3608}
                }, sp);

            return ds;
        }

        
        public Int64? KNT_YRN_CHL_ISS_H_ID { get; set; }
        public Int64? KNT_YRN_RPL_ISS_H_ID { get; set; }


        public Int64? SCM_STR_OIL_REQ_H_ID { get; set; }

        public Int64? GREY_QC_BY { get; set; }
        public Int64? LK_QC_GRD_ID { get; set; }


    }
}