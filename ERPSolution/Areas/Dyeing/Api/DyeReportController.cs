using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Model.Dyeing;
using ERPSolution.Hubs;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ERPSolution.Areas.Dyeing.Api
{
    [RoutePrefix("api/Dye")]
    [NoCache]
    public class DyeReportController : ApiController
    {
        [Route("DyeReport/PreviewReport")]
        public IHttpActionResult PreviewReport(DyeReportModel ob)
        {
            string vReportCode = ob.REPORT_CODE; //Request.Form["pREPORT_CODE"];
            //vReportCode = vReportCode.Substring(0, vReportCode.Length - 1);

            string vProductTitle = "MultiTex ERP";
            string vReportTitle = "";
            string vReportTitle1 = "";
            string vFloorLine = "";
            DataSet dsTempReport = new DataSet();
            ReportDocument rd = new ReportDocument();

            switch (vReportCode)
            {
                case "RPT-4000": // "RPT-4000" Dyes Chemical Batch Program Requisition Report
                    vReportTitle = "Dyes Chemical Batch Program Requisition";
                    dsTempReport = ob.DyesChemicalBatchProgramRequisition();
                    //dsTempReport.WriteXml("E:\\ReportXML\\DyesChemicalBatchProgramRequisition.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchRequisition.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4001": // "RPT-4001" Dyes Chemical Stock Report
                    if (ob.INV_ITEM_CAT_ID != null)
                        vReportTitle = "DYES & CHEMICAL STOCK [Category: " + ob.ITEM_CAT_NAME_EN + "]";
                    else
                        vReportTitle = "DYES & CHEMICAL STOCK [All Category]";

                    dsTempReport = ob.DyesChemicalStock();
                    //dsTempReport.WriteXml("d:\\DyesChemicalStk.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyesChemicalStock.rpt"));

                    rd.DataDefinition.FormulaFields["vFromDate"].Text = "'" + ob.FROM_DATE.Value.AddDays(-1).ToString("dd/MMM/yyyy") + "'";
                    rd.DataDefinition.FormulaFields["vToDate"].Text = "'" + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "'";

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dsTempReport.Tables[1].Rows[0]["COMP_NAME_EN"]) + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dsTempReport.Tables[1].Rows[0]["ADDRESS_EN"]) + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4002": // "RPT-4002" Dyeing Batch Plan Report

                    vReportTitle = "Dyeing M/C Program";
                    dsTempReport = ob.DyeingMCProgramData();
                    //dsTempReport.WriteXml("d:\\DyeingMCProgramData.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeingProgram.rpt"));

                    rd.DataDefinition.FormulaFields["vFromDate"].Text = "'" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";
                    rd.DataDefinition.FormulaFields["vToDate"].Text = "'" + ob.TO_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    foreach (DataRow dr in dsTempReport.Tables["Table1"].Rows)
                    {
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }

                    break;

                case "RPT-4017": // "RPT-4017" Loan Ledger Summary

                    vReportTitle = "Loan Ledger Summary";
                    dsTempReport = ob.LoanLedgerSummary();
                    //dsTempReport.WriteXml("E:\\LoanLedgerSummary.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptLoanLedgerSummary.rpt"));

                    rd.DataDefinition.FormulaFields["vFromDate"].Text = "'" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";
                    rd.DataDefinition.FormulaFields["vToDate"].Text = "'" + ob.TO_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4018": // "RPT-4018" Loan Ledger Details

                    vReportTitle = "Loan Ledger Details";
                    dsTempReport = ob.LoanLedgerDetails();
                    //dsTempReport.WriteXml("E:\\LoanLedgerDetails.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptLoanLedgerDetails.rpt"));

                    rd.DataDefinition.FormulaFields["vFromDate"].Text = "'" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";
                    rd.DataDefinition.FormulaFields["vToDate"].Text = "'" + ob.TO_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4019": // "RPT-4019" Batch Program

                    vReportTitle = "Dyeing Requisition/Process Sheet";
                    dsTempReport = ob.BatchProgram();
                    //dsTempReport.WriteXml("E:\\BatchProgram.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptBatchProgramFull.rpt"));
                    if (ob.FROM_DATE == null)
                        rd.DataDefinition.FormulaFields["vFromDate"].Text = "";
                    else
                        rd.DataDefinition.FormulaFields["vFromDate"].Text = "'" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";
                    if (ob.TO_DATE == null)
                        rd.DataDefinition.FormulaFields["vToDate"].Text = "";
                    else
                        rd.DataDefinition.FormulaFields["vToDate"].Text = "'" + ob.TO_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dsTempReport.Tables[0].Rows[0]["COMP_NAME_EN"]) + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dsTempReport.Tables[0].Rows[0]["ADDRESS_EN"]) + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4024": // "RPT-4024" Dyeing Batch Plan Report - Sample
                    vReportTitle = "Dyeing M/C Program";
                    dsTempReport = ob.DyeingMCProgramDataSample();
                    // dsTempReport.WriteXml("d:\\DyeingMCProgramDataSample.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeingProgramSample.rpt"));

                    rd.DataDefinition.FormulaFields["vFromDate"].Text = "'" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";
                    rd.DataDefinition.FormulaFields["vToDate"].Text = "'" + ob.TO_DATE.Value.ToString("dd/MMM/yyyy h:mm tt") + "'";

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    foreach (DataRow dr in dsTempReport.Tables["Table1"].Rows)
                    {
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }


                    break;

                case "RPT-6000": // "RPT-6000" Daily Production Report of Dyeing Finishing
                    vReportTitle = "Daily Production Report of Dyeing Finishing";
                    dsTempReport = ob.DailyDyeingFinishingProduction();
                    //dsTempReport.WriteXml("E:\\ReportXML\\DailyDyeingFinishingProduction.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDailyDyeingFinishingProduction.rpt"));

                    rd.DataDefinition.FormulaFields["vFromDate"].Text = "'" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + "'";
                    rd.DataDefinition.FormulaFields["vToDate"].Text = "'" + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "'";

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4029": // "RPT-4029" Dyeing S/C Fabric Booking Sheet
                    vReportTitle = "Dyeing S/C Fabric Booking Sheet";
                    dsTempReport = ob.DyeScFabricBooking();
                    //dsTempReport.WriteXml("D:\\DyeScFabricBooking.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeScFabricBooking.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

            }

            rd.SetDataSource(dsTempReport);

            if (ob.IS_EXCEL_FORMAT == "Y")
            {
                if (ob.REPORT_CODE == "RPT-4019")
                {
                    rd.ExportToHttpResponse(ExportFormatType.Excel, System.Web.HttpContext.Current.Response, true, "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm"));

                }
                else
                    rd.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, System.Web.HttpContext.Current.Response, false, "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm"));
            }
            else
            {
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "");
            }

            rd.Close();
            rd.Dispose();

        lastStep:
            return Ok();
        }

        DataTable dt;
        string dsName = "dsCostBreakDownSummary";

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            //int id = int.Parse(e.Parameters["ID"].Values[0].ToString());            
            //DataTable sdt = dt.AsEnumerable()
            //                .Where(r => r.Field<string>("ID") == id.ToString())
            //                .CopyToDataTable();
            //e.DataSources.Add(new ReportDataSource(dsName, sdt));
            e.DataSources.Add(new ReportDataSource(dsName, dt));
        }

        [Route("DyeReport/PreviewReportRDLC")]
        //[HttpGet]
        public IHttpActionResult PreviewReportRDLC(DyeReportModel ob)
        {
            string FilePath = "";
            string vReportCode = ob.REPORT_CODE; //Request.Form["pREPORT_CODE"];
            //vReportCode = vReportCode.Substring(0, vReportCode.Length - 1);

            string vProductTitle = "MultiTex ERP";
            string vReportTitle = "";
            string vReportTitle1 = "";
            string vFloorLine = "";
            DataSet ds;

            ReportDataSource[] rds = new ReportDataSource[10];
            ReportParameter[] p = new ReportParameter[5];

            string fileName = "abc";
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            ReportViewer rvContainer = new ReportViewer();

            rvContainer.LocalReport.Refresh();
            rvContainer.Reset();
            rvContainer.LocalReport.EnableExternalImages = true;
            rvContainer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

            switch (vReportCode)
            {
                case "RPT-4000": // "RPT-4000" Test Report
                    ds = ob.QueryResult();
                    rds[0] = new ReportDataSource("DataSet1", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/Report1.rdlc");
                    break;
                case "RPT-4001": // "RPT-4000" Dyes Chemical Batch Program Requisition Report
                    ds = ob.DyesChemicalBatchProgramRequisition();
                    rds[0] = new ReportDataSource("HeaderDS", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("PreTreatmentDS", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("dsFabricItem", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    //rds[2] = new ReportDataSource("DyeingDS", ds.Tables[2]);
                    //rvContainer.LocalReport.DataSources.Add(rds[2]);

                    //rds[3] = new ReportDataSource("PostTreatmentDS", ds.Tables[3]);
                    //rvContainer.LocalReport.DataSources.Add(rds[3]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchRequisition.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                case "RPT-4002": // "RPT-4001" Dyes Chemical Stock Report
                    ds = ob.DyesChemicalBatchProgramRequisitionAddition();
                    rds[0] = new ReportDataSource("HeaderDS", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("PreTreatmentDS", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("dsFabricItem", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchRequisitionFull.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-4003": // "RPT-4001" Dyes Chemical Batch Cost Breakdown
                    ds = ob.DyesChemicalBatchCostBreakdown();
                    rds[0] = new ReportDataSource("dsOrginalCostTable", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    //rds[1] = new ReportDataSource("dsCostBreakDownSummary", ds.Tables[1]);
                    dt = ds.Tables[1];
                    rvContainer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchCostBreakdown.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());


                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4004": // "RPT-4001" Dyes Chemical Daily Dyeing Production
                    ds = ob.DyesChemicalDailyDyeingProduction();
                    rds[0] = new ReportDataSource("dsMachineProduction", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rds[1] = new ReportDataSource("dsFabricType", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptMachineWiseDailyProduction.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4005": // "RPT-4001" Dyes Chemical Date Wise Machine Production
                    ds = ob.DyesChemicalDailyDyeingProduction();
                    rds[0] = new ReportDataSource("dsMachineProduction", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDateWiseMachineProduction.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;

                case "RPT-4006": // "RPT-4006" Batch Cost Analysis
                    ds = ob.DyesChemicalBatchCostAnalysis();
                    rds[0] = new ReportDataSource("dsBatchCostAnalysis", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("dsBatchCostAnalysisSummary", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptBatchCostAnalysis.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;


                case "RPT-4007": // "RPT-4007" Daily Production & Costing
                    ds = ob.DyesChemicalDailyProductionCosting();
                    rds[0] = new ReportDataSource("dsDailyProductionCosting", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rds[1] = new ReportDataSource("dsDailyProductionCostingSummary", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDailyProductionCosting.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4008": // "RPT-4008" Store Transfere Delivery Challan
                    ds = ob.StoreTransfereDeliveryChallan();
                    rds[0] = new ReportDataSource("dsStroeTransferDeliveryChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptStroeTransferDeliveryChallan.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;

                case "RPT-4009": // "RPT-4009" Loan Delivery Challan
                    ds = ob.LoanDeliveryChallan();
                    rds[0] = new ReportDataSource("dsLoanDeliveryChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptLoanDeliveryChallan.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;

                case "RPT-4010": // "RPT-4009" Requisition for Machine Wash
                    ds = ob.RequisitionDetails();
                    rds[0] = new ReportDataSource("dsRequisitionDetails", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptMachineWashRequisitionDtl.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;
                case "RPT-4011": // "RPT-4011" Requisition for Dyeing Finishing
                    ds = ob.RequisitionDetails();
                    rds[0] = new ReportDataSource("dsRequisitionDetails", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeingFinishingRequisitionDtl.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[3] = new ReportParameter("vReportTitle", ds.Tables[0].Rows[0]["REQ_TYPE_NAME"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                case "RPT-4012": // "RPT-4012" challan Wise Loan Receive Delivery
                    ds = ob.ChallanWiseLoanReceiveDelivery();
                    rds[0] = new ReportDataSource("dsChallanWiseLoanReceiveDelivery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptChallanWiseLoanReceiveDelivery.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    ob.IS_EXCEL_FORMAT = "Y";

                    break;
                case "RPT-4013": // "RPT-4012" Daily Receive
                    ds = ob.DailyReceive();
                    rds[0] = new ReportDataSource("dsDailyReceive", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDailyReceive.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;
                case "RPT-4014": // "RPT-4014" Stock Report
                    ds = ob.DyesChemicalStockSummary();
                    rds[0] = new ReportDataSource("dsDyesChemicalStock", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyesChemicalStock.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;
                case "RPT-4015": // "RPT-4015"  Batch Wise DC Issue Sheet
                    ds = ob.BatchWiseDCIssueSheet();
                    rds[0] = new ReportDataSource("HeaderDS", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("dsBatchIssueDtl", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptBatchWiseDCIssueSheet.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;
                case "RPT-4016": // "RPT-4016"  Stock Ledger
                    ds = ob.DyesChemicalStockLedger();
                    rds[0] = new ReportDataSource("dsDyesChemicalStockLedger", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyesChemicalStockLedger.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;
                case "RPT-4019":  // "RPT-4019"  Print Batch Receipe Full
                    ds = ob.DyesChemicalBatchProgramRequisitionAddition();
                    rds[0] = new ReportDataSource("HeaderDS", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("PreTreatmentDS", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("dsFabricItem", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchRequisitionFull.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                case "RPT-4020": // "RPT-4020"  Stock Ledger
                    ds = ob.BuyerOrderWithoutLD();
                    rds[0] = new ReportDataSource("dsBuyerOrderWithoutLD", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptBuyerOrderWithoutLD.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;
                case "RPT-4021": // "RPT-4021"  Daily Production Issue
                    ds = ob.DailyProductionIssue();
                    rds[0] = new ReportDataSource("dsDailyProductionIssue", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDailyProductionIssue.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;
                case "RPT-4022": // "RPT-4022"  Batch Card Without Lab-dip Recipe
                    ds = ob.BatchCardWithoutLabdipRecipe();
                    rds[0] = new ReportDataSource("dsBatchCardWithoutLabdip", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptBatchCardWithoutLabdip.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;
                case "RPT-4023": // "RPT-4023"  Month Wise Daily Statement of Reprocess
                    ds = ob.MonthWiseDailyStatementOfReprocess();
                    rds[0] = new ReportDataSource("dsDailyStatementOfBatchProcess", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDailyStatementOfBatchProcess.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4024": // "RPT-4023"  Daily Batch Wise Cost Analysis
                    ds = ob.DailyBatchWiseCostAnalysis();
                    rds[0] = new ReportDataSource("dsDailyBatchCostAnalysis", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDailyBatchWiseCosting.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;
                case "RPT-4025": // "RPT-4025" Buyerwise Production & Costing
                    ds = ob.BuyerWiseProductionCosting();
                    rds[0] = new ReportDataSource("dsBuyerWiseCostBreakDown", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    //rds[1] = new ReportDataSource("dsCostBreakDownSummary", ds.Tables[1]);
                    dt = ds.Tables[1];
                    rvContainer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptBuyerWiseCostBreakDown.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;
                case "RPT-4026": // "RPT-4026" Machine Loading Performance
                    ds = ob.MachineLoadingPerformance();
                    rds[0] = new ReportDataSource("dsMachineLoadingPerformance", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptMachineLoadingPerformance.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4027": // "RPT-4027" Batch Info Without Production
                    ds = ob.BatchInfoWithoutProduction();
                    rds[0] = new ReportDataSource("dsBatchInfoWithoutProduction", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptBatchInfoWithoutProduction.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4028": // "RPT-4028" Delivery Challan for Dyeing & Finishing
                    ds = ob.DfDeliveryChallan();
                    rds[0] = new ReportDataSource("dsDfDeliveryChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDfDeliveryChallan.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;


                case "RPT-4101": //Draft Batch Requisition
                    ds = ob.DyesChemicalBatchProgramRequisition();
                    rds[0] = new ReportDataSource("HeaderDS", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("PreTreatmentDS", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("dsFabricItem", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    //rds[2] = new ReportDataSource("DyeingDS", ds.Tables[2]);
                    //rvContainer.LocalReport.DataSources.Add(rds[2]);

                    //rds[3] = new ReportDataSource("PostTreatmentDS", ds.Tables[3]);
                    //rvContainer.LocalReport.DataSources.Add(rds[3]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchRequisitionDraft.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                case "RPT-4030": // "RPT-4030" Batch Cost Breakdown for PC/CVC
                    ds = ob.DyesChemicalBatchCostBreakdownForPcCvc();
                    rds[0] = new ReportDataSource("dsOrginalCostTable", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchCostBreakdownForPCCVC.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4031": // "RPT-4030" Batch Cost Breakdown for Fiber Composition
                    ds = ob.DyesChemicalBatchCostBreakdownForFiberComp();
                    rds[0] = new ReportDataSource("dsOrginalCostTable", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchCostBreakdownForFiberComp.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4032": // "RPT-4032" Batch Card
                    ds = ob.DyeBatchCard();
                    rds[0] = new ReportDataSource("dsDyeBatchCard", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeBatchCard.rdlc");

                    //p[0] = new ReportParameter("CompanyName", ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    ////p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    //p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    //p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    //rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;
                case "RPT-4033": // "RPT-4033" Duplicate Batch Card
                    ds = ob.DuplicateDyeBatchCard();
                    rds[0] = new ReportDataSource("dsDyeBatchCard", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeBatchCard.rdlc");

                    break;

                case "RPT-4034": // "RPT-4034" Batch Check Roll Status
                    ds = ob.BatchCheckRollStatus();
                    rds[0] = new ReportDataSource("dsBatchCheckRollStatus", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptBatchCheckRollStatus.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;

                case "RPT-4035": // "RPT-4035" Dyeing QC Production Report
                    ds = ob.DyeingQcProduction();
                    rds[0] = new ReportDataSource("dsDyeingQcProduction", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeingQcProduction.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-4036": // "RPT-4036" Dyes/Chemical Stock Taking Report
                    ds = ob.DyeingStockTackingReport();
                    rds[0] = new ReportDataSource("dsDyeingStockTackingReport", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeingStockTackingReport.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4037": // "RPT-4037" Dyes Chemical Daily Dyeing Production With QC
                    ds = ob.DailyDyeingProductionWithQC();
                    rds[0] = new ReportDataSource("dsMachineProduction", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rds[1] = new ReportDataSource("dsFabricType", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDailyDyeingProductionWithQC.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4038": // "RPT-4038" Dyes Chemical Loan Summary
                    ds = ob.DyesChemicalLoanSummary();
                    rds[0] = new ReportDataSource("dsDyesChemicalMonthlyLoan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyesChemicalMonthlyLoan.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;
                case "RPT-4039": // "RPT-4039" Dyeing Reporcess History
                    ds = ob.DyeingReporcessHistory();
                    rds[0] = new ReportDataSource("dsDyeingReprocessHistory", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeingReprocessHistory.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4040": // "RPT-4040" Order and Color wise Dyeing Production Statement
                    ds = ob.OrdColWiseDyeingProd();
                    rds[0] = new ReportDataSource("dsOrdColWiseDyeingProd", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptOrdColWiseDyeingProd.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;
                case "RPT-4041": // "RPT-4041" Sub Batch Card
                    ds = ob.SubDyeBatchCard();
                    rds[0] = new ReportDataSource("dsDyeBatchCard", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeBatchCardSubLot.rdlc");

                    break;
                case "RPT-4042": // "RPT-4042" Manual Batch List
                    ds = ob.ManualBatchList();
                    rds[0] = new ReportDataSource("dsManualBatchList", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptManualBatchList.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-4043": // "RPT-4043" Print Batch Card for DF
                    ds = ob.DyeBatchCard();
                    rds[0] = new ReportDataSource("dsDyeBatchCard", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    if (ob.IS_COMBINE == "Y")
                        rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeBatchCard.rdlc");
                    else
                        rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeBatchCardForDF.rdlc");

                    p[0] = new ReportParameter("IS_DUPLICATE", ob.IS_DUPLICATE);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0]});
                    
                    break;

                case "RPT-4044": // "RPT-4044" Challan wise Dyes Chemical Receive
                    ds = ob.ChallanWiseReceive();
                    rds[0] = new ReportDataSource("dsDailyReceive", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptChallanWiseReceive.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;
                case "RPT-4045": // "RPT-4045" Requisition for Finishing Process
                    ds = ob.RequisitionForFinishingProcess();
                    rds[0] = new ReportDataSource("HeaderDS", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("PreTreatmentDS", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("dsFabricItem", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptRequisitionForFinishingProcess.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-4046": // "RPT-4046" Sample Batch Grey Issue Slip
                    vReportTitle = "Sample Batch Grey Issue Slip";
                    ds = ob.SampleBatchGreyIssueSlip();
                    rds[0] = new ReportDataSource("dsSampleBatchGreyIssueSlip", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptSampleBatchGreyIssueSlip.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                case "RPT-4047": // "RPT-4047" Dyes & Chemical Total Transfer Receive Report
                    vReportTitle = "Dyes & Chemical Total Transfer Receive Report";
                    ds = ob.DyesChemicalTotalReceive();
                    rds[0] = new ReportDataSource("dsDyesChemicalTotalReceive", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyesChemicalTotalReceive.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                case "RPT-4048": // "RPT-4048" Dyes & Chemical Challan Wise Transfer Report
                    vReportTitle = "Dyes & Chemical Challan Wise Transfer Report";
                    ds = ob.DyesChemicalChallanWiseTransfer();
                    rds[0] = new ReportDataSource("dsDyesChemicalChallanWiseTransfer", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyesChemicalChallanWiseTransfer.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;    
                case "RPT-4049": // "RPT-4048" Dyes & Chemical Source Stock Taki Report
                    ds = ob.DyeingSourceStockTackingReport();
                    rds[0] = new ReportDataSource("dsDyeingStockTackingReport", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDyeingSourceStockTackingReport.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    
                    break;    
                    
                case "RPT-6001": // "RPT-6001" Dyeing Finishing S/C Program Delivery Report
                    ds = ob.DfScProgramDelivery();
                    rds[0] = new ReportDataSource("dsDFSCProgramDelivery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDFSCProgramDelivery.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", ds.Tables[1].Rows[0][0].ToString());
                    //p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    //p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-6002": // "RPT-6002" Party wise S/C Dyeing & Dyeing Finishing Production Summary
                    ds = ob.DfScProductionSummary();
                    rds[0] = new ReportDataSource("dsDfScProductionSummary", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDfScProductionSummary.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[3] = new ReportParameter("vReportTitle", ds.Tables[1].Rows[0][0].ToString());
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    //p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                case "RPT-6003": // "RPT-6003" Party wise S/C Dyeing & Dyeing Finishing Production Register
                    ds = ob.DfScProductionRegister();
                    rds[0] = new ReportDataSource("dsDfScProductionRegister", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDfScProductionRegister.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[3] = new ReportParameter("vReportTitle", ds.Tables[1].Rows[0][0].ToString());
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;
                case "RPT-6005": // "RPT-6004" S/C Pre-Treatment Challan
                    ds = ob.ScPreTreatmentChallan();
                    rds[0] = new ReportDataSource("dsScPTChallanRoll", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    dt = ds.Tables[0];
                    dsName = "dsScPTChallanRoll";

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptScPreTreatmentChallan.rdlc");

                    rvContainer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.EnableExternalImages = true;
                    FilePath = @"file:\" + HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/COMPANY_PAD/" + (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() : "MFL") + ".JPG"); //Application.StartupPath is for WinForms, you should try AppDomain.CurrentDomain.BaseDirectory  for .net
                    p[3] = new ReportParameter("CompanyImg", FilePath);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-6006": // "RPT-6006" Finish Fabric Inspection
                    vReportTitle = "Finish Fabric Inspection";

                    ds = ob.FinishFabricInspection();
                    rds[0] = new ReportDataSource("dsFinishFabricInspection", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptFinishFabricInspection.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", "Finish Fabric Inspection Report");

                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE == null ? null : ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE == null ? null : ob.TO_DATE.ToString());

                    //p[5] = new ReportParameter("StyleOrder", ob.STYLE_NO);
                    //p[6] = new ReportParameter("Color", ob.COLOR_NAME_EN);
                    //p[7] = new ReportParameter("StatusType", ob.QC_STS_TYP_NAME);
                    //p[8] = new ReportParameter("Shift", ob.SCHEDULE_NAME_EN);
                    //p[9] = new ReportParameter("PartyName", ob.SUP_TRD_NAME_EN);
                    //p[10] = new ReportParameter("ProductionType", ob.KNT_SC_PRG_ISS_NAME);
                    //p[11] = new ReportParameter("FabType", ob.FAB_TYPE_NAME);
                    //p[12] = new ReportParameter("RollNo", ob.FAB_ROLL_NO);
                    //p[13] = new ReportParameter("BuyerName", ob.BYR_ACC_GRP_NAME_EN);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;
                case "RPT-6000": // "RPT-6000" Daily Production Report of Dyeing Finishing
                    vReportTitle = "Daily Production Report of Dyeing Finishing";
                    ds = ob.DailyDyeingFinishingProduction();
                    rds[0] = new ReportDataSource("dsDailyDyeingFinishingProduction", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rds[1] = new ReportDataSource("dsDailyDyeingFinishingProduction2", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);
                    rds[2] = new ReportDataSource("dsDailyDyeingFinishingProduction3", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);
                    rds[3] = new ReportDataSource("dsDailyDyeingFinishingProduction4", ds.Tables[3]);
                    rvContainer.LocalReport.DataSources.Add(rds[3]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDailyDyeingFinishingProduction.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() != null ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE == null ? null : ob.FROM_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;
                case "RPT-6007": // "RPT-6007" S/C Pre-Treatment Batch Card
                    ds = ob.ScPreTreatmentBatchCard();
                    rds[0] = new ReportDataSource("dsScPtBatchCard", ds.Tables[0]);
                    rds[1] = new ReportDataSource("dsScPtBatchCardParam", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptScPtBatchCard.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() != null ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.EnableExternalImages = true;
                    FilePath = @"file:\" + HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/COMPANY_PAD/" + (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() : "MFL") + ".JPG"); //Application.StartupPath is for WinForms, you should try AppDomain.CurrentDomain.BaseDirectory  for .net
                    p[3] = new ReportParameter("CompanyImg", FilePath);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;
                case "RPT-6008": // "RPT-6008" Collar/Cuff and Trims Requisition Slip for Batch Dyeing
                    ds = ob.CollarCuffTrimsCard();
                    rds[0] = new ReportDataSource("dsCollarCuffTrimsCard", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptCollarCuffTrimsCard.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() != null ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    rvContainer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);

                    break;

                case "RPT-6009": // "RPT-6009" DF S/C Program Challan
                    ds = ob.DfScProgramChallan();
                    rds[0] = new ReportDataSource("dsScPTChallanRoll", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDfScProgramChallan.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.EnableExternalImages = true;
                    FilePath = @"file:\" + HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/COMPANY_PAD/" + (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() : "MFL") + ".JPG"); //Application.StartupPath is for WinForms, you should try AppDomain.CurrentDomain.BaseDirectory  for .net
                    p[3] = new ReportParameter("CompanyImg", FilePath);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;
                case "RPT-6010": // "RPT-6010" Fabric Hold List
                    ds = ob.FabricHoldList();
                    rds[0] = new ReportDataSource("dsFabricHoldList", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("dsFabricHoldSummary", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptFabricHoldList.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;
                case "RPT-6011": // "RPT-6011" GMT Wash Requisition
                    ds = ob.GmtWashRequisition();
                    rds[0] = new ReportDataSource("dsGmtWashReq", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    //rds[1] = new ReportDataSource("dsGmtWashProd", ds.Tables[1]);
                    //rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptGmtWashRequisition.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;
                case "RPT-6012": // "RPT-6011" GMT Wash Requisition
                    ds = ob.ScProgramLedger();
                    rds[0] = new ReportDataSource("dsScProgramLedger", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptScProgramLedger.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;
                case "RPT-6013": // "RPT-6013" Dyeing Finishing Floor Status
                    ds = ob.DfFloorStatus();
                    rds[0] = new ReportDataSource("dsDfFloorStatus", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDfFloorStatus.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                    
            }

            string rType = "PDF";
            if (ob.IS_EXCEL_FORMAT == "Y")
            {
                rType = "EXCEL";
            }

            byte[] bytes = rvContainer.LocalReport.Render(rType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            System.Web.HttpResponse response = HttpContext.Current.Response;
            try
            {
                response.Clear();
                response.ContentType = mimeType;
                response.Cache.SetCacheability(HttpCacheability.Private);
                response.Expires = -1;
                response.Buffer = true;
                response.BinaryWrite(bytes);
                response.End();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        lastStep:
            return Ok();
        }

        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            DataSet ds = new DataSet();
            DyeReportModel obj = new DyeReportModel();
            obj.DF_SC_PT_ISS_H_ID = 24;
            ds = obj.ScPreTreatmentBatchCard();
            ReportParameter[] p = new ReportParameter[5];
            e.DataSources.Add(new ReportDataSource("dsScPtBatchCard", ds.Tables[0]));
        }


    }
}
