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




namespace ERPSolution.Areas.Cutting
{
    [RoutePrefix("api/Cutting")]
    public class CutReportController : ApiController
    {
        [Route("CutReport/PreviewReport")]
        public IHttpActionResult PreviewReport(CutReportModel ob)
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

                case "RPT-4500": // "RPT-4500" Bundle Card Report
                    vReportTitle = "Bundle Card";
                    //vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.BundleCard();
                    //dsTempReport.WriteXml("e:\\BundleCard.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleCard.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4501": // "RPT-4501" Bundle Chart Report
                    vReportTitle = "Bundle Chart";
                    //vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.BundleChart();
                    //dsTempReport.WriteXml("e:\\BundleChart.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleChart.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4502": // "RPT-4502" Bundle Card Reprint
                    vReportTitle = "Bundle Card Reprint";
                    //vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.BundleCardReprint();
                    //dsTempReport.WriteXml("d:\\BundleCard.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleCard.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4503": // "RPT-4503" Bundle Card for Piping
                    vReportTitle = "Bundle Card for Piping";
                    //vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.BundleCard4Piping();
                    //dsTempReport.WriteXml("e:\\BundleCard4Piping.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleCard4Piping.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4504": // "RPT-4504" Bundle Card for Full Cake
                    vReportTitle = "Bundle Card for Full Cake";
                    //vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.BundleCard4FullCake();
                    //dsTempReport.WriteXml("e:\\BundleCard4FullCake.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleCard4FullCake.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-4505": // "RPT-4505" Bundle Chart for Full Cake
                    vReportTitle = (ob.BNDL_PRINT_TYP_ID == 7) ? "Bundle Chart for Full Cake Size Wise" : "Bundle Chart for Full Cake Size and Ratio Wise";
                    //vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.BundleChart4FullCake();
                    //dsTempReport.WriteXml("e:\\BundleChart4FullCake.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleChart4FullCake.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;
                
            }

           
            rd.SetDataSource(dsTempReport);

            if (ob.IS_EXCEL_FORMAT == "Y")
            {
                //rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "");
                rd.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, System.Web.HttpContext.Current.Response, false, "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm"));
            }
            else if (ob.IS_MAIL_WITH_ATTACH == "Y")
            {
                string savedFileName = HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm") + ".pdf");
                rd.ExportToDisk(ExportFormatType.PortableDocFormat, savedFileName);

                var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
                var engines = new System.Web.Mvc.ViewEngineCollection();
                engines.Add(new FileSystemRazorViewEngine(viewsPath));
                var emailService = new EmailService(engines);

                KNT_SRT_FAB_REQ_RPTModel ob1 = new KNT_SRT_FAB_REQ_RPTModel();
                var rptData = ob1.GetSrtFabReqRpt(26);

                var email = new SendSrtFabAprovMailService
                {
                    To = rptData.fabList[0].MAIL_ADD_LST,

                    data = rptData //ob.GetSrtFabReqRpt(pKNT_SRT_FAB_REQ_H_ID)

                };


                // Create  the file attachment for this e-mail message.
                Attachment data = new Attachment(savedFileName, MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                System.Net.Mime.ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(savedFileName);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(savedFileName);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(savedFileName);
                // Add the file attachment to this e-mail message.
                email.Attachments.Add(data);

                emailService.Send(email);

                System.IO.File.Delete(savedFileName);
            }
            else
            {
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


        [Route("CutReport/PreviewReportRDLC")]
        //[HttpGet]
        public IHttpActionResult PreviewReportRDLC(CutReportModel ob)
        {
            string vReportCode = ob.REPORT_CODE; //Request.Form["pREPORT_CODE"];
            //vReportCode = vReportCode.Substring(0, vReportCode.Length - 1);

            string vProductTitle = "MultiTex ERP";
            string vReportTitle = "";
            string vReportTitle1 = "";
            string vFloorLine = "";
            DataSet ds;

            ReportDataSource[] rds = new ReportDataSource[10];
            ReportParameter[] p = new ReportParameter[20];

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

                case "RPT-4506": // "RPT-4506" Bundle Chart for Cut Panel Inspection
                                        
                    vReportTitle = "Bundle Chart for Cut Panel Inspection";

                    ds = ob.BundleChart4CutPanelInsp();

                    rds[0] = new ReportDataSource("BundleChart4CutPanelInsp", ds.Tables[0]);
                    rds[1] = new ReportDataSource("BundleChartRatioSource", ds.Tables[1]);

                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleChart4CutPanelInsp.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4507": // "RPT-4507" Date, Order and Color wise Cut Panel Inspection Summery

                    vReportTitle = "Date, Order and Color wise Cut Panel Inspection Summery [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.CutPanelInspSummeryDateOrdWise();

                    rds[0] = new ReportDataSource("CutPanelInspSummery", ds.Tables[0]);
                    rds[1] = new ReportDataSource("CutPanelInspSummery01", ds.Tables[1]);
                    
                    rvContainer.LocalReport.DataSources.Add(rds[0]);                                        
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptCutPanelInspSummeryDateOrdWise.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4508": // "RPT-4508" Order and Color wise Cut Panel Inspection Summery

                    if (ob.FROM_DATE == null || ob.TO_DATE.Value == null)
                    {
                        ob.FROM_DATE = null;
                        ob.TO_DATE = null;

                        vReportTitle = "Order and Color wise Cut Panel Inspection Summery";
                    }
                    else
                    {
                        vReportTitle = "Order and Color wise Cut Panel Inspection Summery [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    }

                    ds = ob.CutPanelInspSummeryOrdColWise();

                    rds[0] = new ReportDataSource("CutPanelInspSummery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptCutPanelInspSummeryOrdColWise.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4509": // "RPT-4509" Daily Table Wise Cutting Production

                    vReportTitle = "Daily Table Wise Cutting Production [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.TableWiseCuttingProd();

                    rds[0] = new ReportDataSource("TableWiseCuttingProd", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    //rvContainer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptTableWiseCuttingProd.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4510": // "RPT-4510" Daily Cutting Target and Achive

                    vReportTitle = "Daily Cutting Target and Achive [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.CuttingProdVsAchive();

                    rds[0] = new ReportDataSource("CuttingProdVsAchive", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptCuttingProdVsAchive.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4511": // "RPT-4511" Order and Color wise Cutting summery

                    vReportTitle = "Order and Color wise Cutting summery [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.CutOrdColorWiseSummery();

                    rds[0] = new ReportDataSource("CutOrdColorWiseSummery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptCutOrdColorWiseSummery.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4512": // "RPT-4512" Order, Color and Cutting# wise Cut Panel Inspection Summery

                    vReportTitle = "Order, Color and Cutting# wise Cut Panel Inspection Summery [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.CutPanelInspSummeryOrdColCutNoWise();

                    rds[0] = new ReportDataSource("CutPanelInspSummery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptCutPanelInspSummeryOrdColCutNoWise.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4513": // "RPT-4512" Date wise Plies

                    vReportTitle = "Date wise Plies [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.CuttingInfoByDate();

                    rds[0] = new ReportDataSource("CuttingInfoByDate01", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("CuttingInfoByDate02", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptCuttingInfoByDate.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                //case "RPT-4514": // "RPT-4514" Garment Item

                //    vReportTitle = "Garment Item";

                //    ds = ob.getGarmentItems();

                //    rds[0] = new ReportDataSource("GARMENT_ITEM", ds.Tables[0]);
                //    rvContainer.LocalReport.DataSources.Add(rds[0]);

                //    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBuyerWiseItem.rdlc");

                //    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                //    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                //    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                //    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                //    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                //    break;

                case "RPT-4514": // "RPT-4514" Sewing Delivery Challan

                    vReportTitle = "Sewing Delivery Challan";

                    ds = ob.SewingDeliveryChallan();

                    rds[0] = new ReportDataSource("SewDelivChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptSewingDeliveryChallan.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4515": // "RPT-4515" Embellishment Delivery Challan

                    vReportTitle = "Embellishment Delivery Challan";

                    ds = ob.EmbellishDeliveryChallan();

                    rds[0] = new ReportDataSource("EmbellishDeliveryChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptEmbellishDeliveryChallan.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4516": // "RPT-4516" Daily Cutting Production

                    vReportTitle = "Daily Cutting Production as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                    ds = ob.DailyCuttingProduction();

                    rds[0] = new ReportDataSource("DailyCuttingProduction", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptDailyCuttingProduction.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4517": // "RPT-4517" Monthly Table Wise Cutting Production

                    vReportTitle = "Monthly Table Wise Cutting Production [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.MonthlyTableWiseCuttingProd();

                    rds[0] = new ReportDataSource("TableWiseCuttingProd", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    //rvContainer.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptMonthlyTableWiseCuttingProd.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4518": // "RPT-4518" Buyer wise Daily Input Balance

                    vReportTitle = "Date wise Cut Panel Inspection Summery [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.CutPanelInspSummeryDateWise();

                    rds[0] = new ReportDataSource("CutPanelInspSummery", ds.Tables[0]);
                    rds[1] = new ReportDataSource("CutPanelInspSummery01", ds.Tables[1]);

                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptCutPanelInspSummeryDateWise.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4519": // "RPT-4519" Buyer wise Daily Input Balance

                    vReportTitle = "Buyer wise Daily Input Balance as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                    ds = ob.ByrWiseInputBal();

                    rds[0] = new ReportDataSource("ByrWiseInputBal", ds.Tables["BYR_WISE_INPUT_BAL"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptByrWiseInputBal.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4520": // "RPT-4520" Date and Cutting# wise Plies

                    vReportTitle = "Date and Cutting# wise Plies [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.CuttingInfoByDateAndCutting();

                    rds[0] = new ReportDataSource("CuttingInfoByDate01", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptCuttingInfoByDateAndCutting.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4521": // "RPT-4521" Bundle Chart for Embellishment

                    vReportTitle = "Bundle Chart for Embellishment";

                    ds = ob.BundleChart4EmbellishDelvChalan();

                    rds[0] = new ReportDataSource("EmbellishDeliveryChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleChart4EmbellishDelvChalan.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-4522": // "RPT-4522" Bundle Chart for Sewing Input

                    vReportTitle = "Bundle Chart for Sewing Input";

                    ds = ob.BundleChart4SewingInput();

                    rds[0] = new ReportDataSource("SewDelivChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Cutting/Reports/rptBundleChart4SewingDelvChalan.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;
                
            }

            string rptType = "PDF";
            if (ob.IS_EXCEL_FORMAT == "Y")
            {
                rptType = "EXCEL";
                //rd.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, System.Web.HttpContext.Current.Response, false, "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm"));
            }

            byte[] bytes = rvContainer.LocalReport.Render(rptType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            System.Web.HttpResponse response = HttpContext.Current.Response;
            try
            {
                response.Clear();
                response.ContentType = mimeType;
                response.Cache.SetCacheability(HttpCacheability.Private);
                response.Expires = -1;
                response.Buffer = true;
                response.BinaryWrite(bytes);
                if (ob.IS_EXCEL_FORMAT == "Y")
                    response.AppendHeader("Content-Disposition", "inline; filename=" + "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm") + ".xls");
                else
                    response.AppendHeader("Content-Disposition", "inline; filename=" + "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm") + ".pdf");

                response.End();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        lastStep:
            return Ok();
        }


        DataTable dt;
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsCostBreakDownSummary", dt));
        }

    }
}