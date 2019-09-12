using CrystalDecisions.CrystalReports.Engine;
using ERP.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CrystalDecisions.Shared;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WebForms;

using System.Web.Hosting;
//using System.Web.Mvc;
using Postal;
using ERPSolution.Controllers;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;



namespace ERPSolution.Areas.Merchandising.Api
{
    [RoutePrefix("api/mrc")]
    public class MrcReportController : ApiController
    {
        [Route("MrcReport/PreviewReport")]
        public IHttpActionResult PreviewReport(MrcReportModel ob)
        {
            string vReportCode = ob.REPORT_CODE; //Request.Form["pREPORT_CODE"];
            //vReportCode = vReportCode.Substring(0, vReportCode.Length - 1);

            string vReportFilePrefix = "";
            string vProductTitle = "MultiTex ERP";
            string vReportTitle = "";
            string vFloorLine = "";
            DataSet dsTempReport = new DataSet();
            ReportDocument rd = new ReportDocument();


            // Declare variables and get the export options.
            //ExportOptions exportOpts = new ExportOptions();
            //ExcelFormatOptions excelFormatOpts = new ExcelFormatOptions();
            //DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
            //exportOpts = rd.ExportOptions;
            // Set the excel format options.
            //excelFormatOpts.ExcelUseConstantColumnWidth = true;
            //exportOpts.ExportFormatType = ExportFormatType.Excel;
            //exportOpts.FormatOptions = excelFormatOpts;




            switch (vReportCode)
            {
                //case "RPT-2000": // "RPT-2000" Sample Fabric Booking    
                //    vReportFilePrefix = "SFB_";
                //    vReportTitle = "SAMPLE PROGRAM & FABRICS BOOKING SHEET";
                //    dsTempReport = ob.SampleFabricBooking();
                //    //dsTempReport.WriteXml("e:\\SampleFabricBooking.xml", XmlWriteMode.WriteSchema);
                //    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptSampleFabricBooking.rpt"));

                //    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                //    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                //    break;

                case "RPT-2001": // "RPT-2001" Bulk Fabric Booking
                    //vReportTitle = "Absent Summary From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                    //    ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    
                    vReportTitle = "BULK FABRICS BOOKING SHEET";
                    dsTempReport = ob.BulkFabricBooking();
                    //dsTempReport.WriteXml("e:\\BulkFabricBooking.xml", XmlWriteMode.WriteSchema);

                    if (ob.IS_MULTI_SHIP_DT == "Y" && ob.PAGE_SIZE_NAME == "A4")
                    {
                        rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptBulkFabricBkMultiSp.rpt"));
                    }
                    else if (ob.IS_MULTI_SHIP_DT == "Y" && ob.PAGE_SIZE_NAME == "Legal")
                    {
                        rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptBulkFabricBkMultiSpLP.rpt"));
                    }
                    else if (ob.IS_MULTI_SHIP_DT == "N" && ob.PAGE_SIZE_NAME == "A4")
                    {
                        rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptBulkFabricBooking.rpt"));
                    }
                    else if (ob.IS_MULTI_SHIP_DT == "N" && ob.PAGE_SIZE_NAME == "Legal")
                    {
                        rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptBulkFabricBookingLP.rpt"));
                    }
                    else if (ob.IS_MULTI_SHIP_DT == "N" && ob.PAGE_SIZE_NAME == "Legal-1")
                    {
                        rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptBulkFabricBookingLP1.rpt"));
                    }
                    else
                    {
                        rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptBulkFabricBookingLP1.rpt"));
                    }

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    try
                    {
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    }
                    catch(Exception e)
                    {
                        var ex = e.Message;
                    }


                    foreach (DataRow dr in dsTempReport.Tables["BULK_FABRIC_BOOKING"].Rows)
                    {
                        var str = "BFB_" + Convert.ToString(dr["STYLE_NO"]) + "_" + Convert.ToString(dr["ORDER_NO"]);
                        vReportFilePrefix = Regex.Replace(str, "[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);                   
                    }

                    break;

                case "RPT-2002": // "RPT-2002" Yarn List
                    vReportTitle = "Yarn List";
                    dsTempReport = ob.YarnList();
                    //dsTempReport.WriteXml("E:\\ReportXML\\YarnList.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptYarnList.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-2003": // "RPT-2003" Budget Sheet
                    vReportTitle = "Budget Sheet";
                    dsTempReport = ob.BudgetSheet();
                    //dsTempReport.WriteXml("E:\\ReportXML\\BudgetSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptBudgetSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["V_IS_BGT_FINALIZED"].Text = "'" + (ob.IS_BGT_FINALIZED ?? "N") + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-2004": // "RPT-2004" Order Sheet
                    vReportTitle = "Order Sheet";
                    dsTempReport = ob.OrderSheet();
                    //dsTempReport.WriteXml("d:\\OrderSheet.xml", XmlWriteMode.WriteSchema);

                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptOrderSheet.rpt"));

                    //rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-2005": // "RPT-2005" Lapdib Program
                    vReportTitle = "Lapdib Program";
                    dsTempReport = ob.LapdibProgram();
                    //dsTempReport.WriteXml("E:\\ReportXML\\LapDibProgram.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptLapdibProgram.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-2006": // "RPT-2006" Sample Production
                    vReportTitle = "Sample Production on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.SamplProduction();
                    //dsTempReport.WriteXml("d:\\SamplProduction.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptSamplProduction.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-2007": // "RPT-2006" Sample Card
                    vReportTitle = "Sample Card " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.SampleCard();
                    //dsTempReport.WriteXml("e:\\SampleCard.xml", XmlWriteMode.WriteSchema);

                    if (ob.SMPL_FMT_ID==1)
                        rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptSampleCardLindex.rpt"));
                    else
                        rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptSampleCard.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-2008": // "RPT-2008" TnA Task List By Order H
                    vReportTitle = "Time & Action Calander";
                    dsTempReport = ob.getTnATaskListByOrderID();

                    //dsTempReport.WriteXml("d:\\TnATASK_LIST_ORD_BY.xml", XmlWriteMode.WriteSchema);

                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptTnATaskByOrder.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;
            }

            //rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
            //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameB"].ToString() + "'";
            //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressB"].ToString() + "'";

            rd.SetDataSource(dsTempReport);

            if (ob.IS_EXCEL_FORMAT == "Y")
            {
                rd.ExportToHttpResponse(ExportFormatType.Excel, System.Web.HttpContext.Current.Response, false, "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm"));
            }
            else if (ob.IS_EXPORT_TO_DISK == "Y")
            {
                string savedFileName = HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/" + vReportFilePrefix.Replace("/","_") + ".pdf");
                rd.ExportToDisk(ExportFormatType.PortableDocFormat, savedFileName);

                if (ob.REPORT_CODE == "RPT-2001") 
                { 
                    ERPSolution.Areas.Merchandising.Controllers.MrcController.SendBulkFabAprovMail(ob.MC_BLK_FAB_REQ_H_ID, savedFileName);                   
                }
            }
            else
            {
                rd.SummaryInfo.ReportTitle = ob.RES_TITLE;
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "");
            }


            rd.Close();
            rd.Dispose();


            //return view("/CrystalReportViewer.aspx");

            //CrystalDecisions.Web.CrystalReportViewer cv = new CrystalDecisions.Web.CrystalReportViewer();
        //cv.RefreshReport();
        //cv.Visible = true;
        //cv.ReportSource = rd;
        //cv.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
        lastStep:
            return Ok();
        }


        [Route("MrcReport/PreviewReportRDLC")]
        //[HttpGet]
        public IHttpActionResult PreviewReportRDLC(MrcReportModel ob)
        {
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
            string vReportFilePrefix = "";

            ReportViewer rvContainer = new ReportViewer();

            rvContainer.LocalReport.Refresh();
            rvContainer.Reset();
            rvContainer.LocalReport.EnableExternalImages = true;
            rvContainer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

            switch (vReportCode)
            {

                case "RPT-2000": // "RPT-2000" Sample Fabric Booking    
                    vReportFilePrefix = "SFB_";
                    vReportTitle = "SAMPLE PROGRAM & FABRICS BOOKING SHEET";
                    ds = ob.SampleFabricBooking();

                    rds[0] = new ReportDataSource("SAM_FAB_BOOKING_H", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("SAM_FAB_BOOKING_SAM", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("SAM_FAB_BOOKING_FAB", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    rds[3] = new ReportDataSource("SAM_FAB_BOOKING_TNA", ds.Tables[3]);
                    rvContainer.LocalReport.DataSources.Add(rds[3]);

                    rds[4] = new ReportDataSource("SAM_FAB_BOOKING_CLCF", ds.Tables[4]);
                    rvContainer.LocalReport.DataSources.Add(rds[4]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptSampleFabricBooking.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    foreach (DataRow dr in ds.Tables["SAM_FAB_BOOKING_H"].Rows)
                    {
                        var str = "SFB_" + Convert.ToString(dr["STYLE_NO_LST"]) + "_" + Convert.ToString(dr["ORDER_NO_LST"]);
                        vReportFilePrefix = Regex.Replace(str, "[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);
                    }

                    break;

                case "RPT-2003": // "RPT-2003" Budget Sheet
                    vReportTitle = "Budget Sheet";
                    ds = ob.BudgetSheet();
                    rds[0] = new ReportDataSource("dsBudgetSheet1", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("dsBudgetSheet2", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("dsBudgetSheet3", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptBudgetSheet.rdlc");

                    //p[0] = new ReportParameter("vCompany", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("vCompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    //p[4] = new ReportParameter("V_IS_BGT_FINALIZED", (ob.IS_BGT_FINALIZED ?? "N"));

                    //rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-2009": // "RPT-2009" Projection Booking
                    vReportTitle = "Projection Booking";
                    ds = ob.ProjectionBooking();
                    rds[0] = new ReportDataSource("dsProjectionBooking", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptProjectionBooking.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        var str = "PBK_" + Convert.ToString(dr["STYLE_NO"]) + "_" + Convert.ToString(dr["ORDER_NO"]);
                        vReportFilePrefix = Regex.Replace(str, "[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);
                    }

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;

                case "RPT-2010": // "RPT-2010" Dyeing Order for Trims
                    vReportTitle = "Dyeing Order for Trims";
                    ds = ob.DyeingOrd4Trims();
                    rds[0] = new ReportDataSource("dsDyeingOrd4Trims", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptDyeingOrd4Trims.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                 case "RPT-2011": // "RPT-2011" Dyeing Finishing T&A Fail Report
                    vReportTitle = ob.DEPARTMENT_NAME_EN + " Fail";

                    if (ob.FROM_MONTH != null)
                    {
                        ob.FROM_DATE = new DateTime(Convert.ToDateTime(ob.FROM_MONTH).Year, Convert.ToDateTime(ob.FROM_MONTH).Month, 1);
                        ob.TO_DATE = new DateTime(Convert.ToDateTime(ob.FROM_MONTH).Year, Convert.ToDateTime(ob.FROM_MONTH).Month+1, 1).AddDays(-1);
                    }

                    ds = ob.DFTnaFailReport();
                    rds[0] = new ReportDataSource("DfTnaFailReport", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptDyeingFinishingTnAFail.rdlc");
                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3]});

                    break;
                 case "RPT-2012": // "RPT-2012" Accessories Purchase Requisition
                    vReportTitle = "Purchase Order";
                    ds = ob.AccPurchaseRequisition();
                    rds[0] = new ReportDataSource("dsAccPurchaseRequisition", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptAccPurchaseRequisition.rdlc");
                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                    
                 case "RPT-2013": // "RPT-2013" Inquiry Report
                    vReportTitle = "inquiry List";
                    ds = ob.getInquiryList();
                    rds[0] = new ReportDataSource("RPT_INQUIRY", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptInquiryList.rdlc");

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            p[0] = new ReportParameter("CompanyName", Convert.ToString(dr["COMP_NAME_EN"]));
                            p[1] = new ReportParameter("CompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));

                        }
                    }
                    else
                    {
                        p[0] = new ReportParameter("CompanyName", "");
                        p[1] = new ReportParameter("CompanyAddress", "" );

                    }
                    
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                 case "RPT-2014": // "RPT-2014" Order Wise Accessories Status Report
                    vReportTitle = "Order Wise Accessories Status Report";
                    ds = ob.dsOrderWiseAccStatus();
                    rds[0] = new ReportDataSource("dsOrderWiseAccStatus", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptOrderWiseAccStatus.rdlc");
                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                 case "RPT-2015": // "RPT-2015" Inquiry Details
                    vReportTitle = "Inquiry Details";
                    ds = ob.getInquiryDetailsList();
                    rds[0] = new ReportDataSource("RPT_INQ_DETAILS", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptInquiryDetails.rdlc");

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            p[0] = new ReportParameter("CompanyName", Convert.ToString(dr["COMP_NAME_EN"]));
                            p[1] = new ReportParameter("CompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));

                        }
                    }
                    else
                    {
                        p[0] = new ReportParameter("CompanyName", "");
                        p[1] = new ReportParameter("CompanyAddress", "");

                    }

                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;
                 case "RPT-2016": // "RPT-2016" Dept Wise T&A Task Fail
                    vReportTitle = "Dept Wise T&A Task Fail";
                    if (ob.FROM_MONTH != null)
                    {
                        ob.FROM_DATE = new DateTime(Convert.ToDateTime(ob.FROM_MONTH).Year, Convert.ToDateTime(ob.FROM_MONTH).Month, 1);
                        ob.TO_DATE = new DateTime(Convert.ToDateTime(ob.FROM_MONTH).Year, Convert.ToDateTime(ob.FROM_MONTH).Month + 1, 1).AddDays(-1);
                    }

                    ds = ob.DeptWiseTnaFailReport();
                    rds[0] = new ReportDataSource("DEPT_TNA_FAIL_RPT", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptDeptWiseTnAFail.rdlc");
                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                 case "RPT-2017": // "RPT-2017" Byr Dept Wise T&A Task Fail
                    vReportTitle = "Buyer-Dept Wise T&A Task Fail";
                    if (ob.FROM_MONTH != null)
                    {
                        ob.FROM_DATE = new DateTime(Convert.ToDateTime(ob.FROM_MONTH).Year, Convert.ToDateTime(ob.FROM_MONTH).Month, 1);
                        ob.TO_DATE = new DateTime(Convert.ToDateTime(ob.FROM_MONTH).Year, Convert.ToDateTime(ob.FROM_MONTH).Month + 1, 1).AddDays(-1);
                    }

                    ds = ob.DeptByrWiseTnaFailReport();
                    rds[0] = new ReportDataSource("DEPT_BYR_TNA_FAIL", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptByrDeptWiseTnAFail.rdlc");
                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                 case "RPT-2018": // "RPT-2017" Style-Dept Wise T&A Task Fail
                    vReportTitle = "Buyer-Dept Wise T&A Task Fail";
                    if (ob.FROM_MONTH != null)
                    {
                        ob.FROM_DATE = new DateTime(Convert.ToDateTime(ob.FROM_MONTH).Year, Convert.ToDateTime(ob.FROM_MONTH).Month, 1);
                        ob.TO_DATE = new DateTime(Convert.ToDateTime(ob.FROM_MONTH).Year, Convert.ToDateTime(ob.FROM_MONTH).Month + 1, 1).AddDays(-1);
                    }
                    ds = ob.DeptStyleWiseTnaFailReport();
                    rds[0] = new ReportDataSource("DEPT_STYLE_TNA_FAIL", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Merchandising/Reports/rptStyleDeptWiseTnAFail.rdlc");
                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;



            }

            string rType = "PDF";
            if (ob.IS_EXCEL_FORMAT == "Y")
            {
                rType = "EXCEL";
            }
            

            byte[] bytes = rvContainer.LocalReport.Render(rType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            if (ob.IS_EXCEL_FORMAT == "Y")
            {                
                string ExcelFileName = "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm");
                System.Web.HttpResponse response = HttpContext.Current.Response;
                
                response.Clear();
                response.ContentType = mimeType;
                response.Cache.SetCacheability(HttpCacheability.Private);
                response.Expires = -1;
                response.Buffer = true;
                response.AddHeader("content-disposition", "attachment; filename=" + ExcelFileName + ".xls");
                response.BinaryWrite(bytes);
                response.End();
            }
            else if (ob.IS_EXPORT_TO_DISK == "Y")
            {

                string savedFileName = HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/" + vReportFilePrefix.Replace("/", "_") + ".pdf");

                using (FileStream fs = new FileStream(savedFileName, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }

                if (ob.REPORT_CODE == "RPT-2000") //For sample program
                {
                    ERPSolution.Areas.Merchandising.Controllers.MrcController.SendSmplProgAprovMail(ob.MC_SMP_REQ_H_ID, savedFileName);
                }
                else if (ob.REPORT_CODE == "RPT-2009")
                {
                    ERPSolution.Areas.Merchandising.Controllers.MrcController.SendProjectionMail(ob.MC_PROV_FAB_BK_H_ID, savedFileName);
                }
            }
            else
            {
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
            }
        lastStep:
            return Ok();
        }

    }
}