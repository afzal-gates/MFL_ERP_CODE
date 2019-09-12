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
    public class PlanReportModel
    {
        public string REPORT_CODE { get; set; }
        public string IS_EXCEL_FORMAT { get; set; }
        public string IS_EXPORT_TO_DISK { get; set; }
        public string PAGE_SIZE_NAME { get; set; }

        public Int32? HR_PROD_FLR_ID { get; set; }
        public Int32? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? MC_ORDER_SHIP_ID { get; set; }
        public Int64? MC_STYLE_D_ITEM_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public string HR_PROD_LINE_LST { get; set; }
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public DateTime? PROD_DT { get; set; }
        public Int32? RF_FISCAL_YEAR_ID { get; set; }

        public Int32? HR_COMPANY_ID { get; set; }
        public Int32? HR_OFFICE_ID { get; set; }
        public string OFFICE_NAME_EN { get; set; }
        public string COMP_NAME_EN { get; set; }
        public DateTime? FROM_MONTH { get; set; }
        public DateTime? TO_MONTH { get; set; }


        public DataSet getEfficiencyReportDatas()
        {
            var ob = this;
            string sp = "pkg_hourly_prod.gmt_ln_load_plan_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT}, 
		            new CommandParameter() {ParameterName = "pOption", Value = 3004}
                }, sp);

            return ds;
        }

        public DataSet Next3DaysSwInputStatus()
        {
            var ob = this;
            string sp = "pkg_gmt_report.pln_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID}, 
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE}, 
		            new CommandParameter() {ParameterName = "pOption", Value = 5000}
                }, sp);

            return ds;
        }

        public DataSet ProdPlan()
        {
            var ob = this;
            string sp = "pkg_gmt_report.pln_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID}, 
                    new CommandParameter() {ParameterName = "pHR_PROD_LINE_LST", Value = ob.HR_PROD_LINE_LST},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID}, 
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE}, 
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE}, 
		            new CommandParameter() {ParameterName = "pOption", Value = 5001}
                }, sp);

            return ds;
        }

        public DataSet OrderWisePlanExecution()
        {
            var ob = this;
            string sp = "pkg_gmt_report.pln_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                       
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID}, 
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = ob.MC_ORDER_SHIP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = ob.MC_STYLE_D_ITEM_ID},
                                        
		            new CommandParameter() {ParameterName = "pOption", Value = 5004}
                }, sp);

            return ds;
        }

        public DataSet ProdPlanSummery()
        {
            var ob = this;
            string sp = "pkg_gmt_report.pln_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                                           
                    new CommandParameter() {ParameterName = "pFROM_MONTH", Value = ob.FROM_MONTH},
                    new CommandParameter() {ParameterName = "pTO_MONTH", Value = ob.TO_MONTH},
                                        
		            new CommandParameter() {ParameterName = "pOption", Value = 5005}
                }, sp);

            return ds;
        }

        public DataSet OrdWiseFabMonitor()
        {
            var ob = this;
            string sp = "pkg_gmt_report.pln_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                                           
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID}, 
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                                        
		            new CommandParameter() {ParameterName = "pOption", Value = 5005}
                }, sp);

            return ds;
        }

        public DataSet DailyLinePlan()
        {
            var ob = this;
            string sp = "pkg_gmt_report.pln_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID}, 
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},                     
		            new CommandParameter() {ParameterName = "pOption", Value = 5002}
                }, sp);

            return ds;
        }

        public DataSet ByrWiseCapctyAlloc()
        {
            var ob = this;
            string sp = "pkg_gmt_report.pln_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                        
                    new CommandParameter() {ParameterName = "pFROM_MONTH", Value = ob.FROM_MONTH},                     
		            new CommandParameter() {ParameterName = "pOption", Value = 5006}
                }, sp);

            return ds;
        }

        public DataSet getMonthlyCapacityBooking()
        {
            var ob = this;
            string sp = "pkg_gmt_report.pln_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID}, 
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID}, 
                    new CommandParameter() {ParameterName = "pFROM_MONTH", Value = ob.FROM_MONTH},
                    new CommandParameter() {ParameterName = "pTO_MONTH", Value = ob.TO_MONTH},
		            new CommandParameter() {ParameterName = "pOption", Value = 5003}
                }, sp);

            return ds;
        }
    }
}