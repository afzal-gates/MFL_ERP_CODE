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
using System.Text.RegularExpressions;




namespace ERPSolution.Areas.Planning.Api
{
    [RoutePrefix("api/pln")]
    public class PlnReportController : ApiController
    {
        [Route("PlnReport/PreviewReport")]
        public IHttpActionResult PreviewReport(PlanReportModel ob)
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
                case "RPT-1500": // "RPT-1500" Sewing M/C Specification

                    break;
            }

          

            rd.SetDataSource(dsTempReport);

            if (ob.IS_EXCEL_FORMAT == "Y")
            {
                //rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "");
                rd.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, System.Web.HttpContext.Current.Response, false, "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm"));
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

        [Route("PlnReport/PreviewReportRDLC")]        
        public IHttpActionResult PreviewReportRDLC(PlanReportModel ob)
        {
            string vReportCode = ob.REPORT_CODE;
            string vProductTitle = "MultiTex ERP";
            string vReportTitle = "";
            DataSet ds;

            ReportDataSource[] rds = new ReportDataSource[10];
            ReportParameter[] p = new ReportParameter[20];
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
                case "RPT-9000": // "RPT-9000" Daily TGT Production &  Efficiency Report)
                    vReportTitle = "Daily TGT Production &  Efficiency Report";
                    ds = ob.getEfficiencyReportDatas();

                    rds[0] = new ReportDataSource("dsEfficiency", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptEfficiencyReport.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    p[4] = new ReportParameter("V_PROD_DT", ob.PROD_DT.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-5000": // "RPT-5000" Next 3 Days Sewing Input Status
                    vReportTitle = "Next 3 Days Sewing Input Status From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    ds = ob.Next3DaysSwInputStatus();

                    rds[0] = new ReportDataSource("Next3DaysSwInputStatus", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptNext3DaysSwInputStatus.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-5001": // "RPT-5001" Production Plan (Max 7 Days)
                    vReportTitle = "Production Plan [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    ds = ob.ProdPlan();

                    rds[0] = new ReportDataSource("WeeklyProdPlan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptWeeklyProdPlan.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-5002": // "RPT-5002" Daily Line Plan
                    vReportTitle = "Daily Line Plan as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    ds = ob.DailyLinePlan();

                    rds[0] = new ReportDataSource("DailyLinePlan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("DailyLinePlanOtSummery", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("DailyLinePlanSummery", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    rds[3] = new ReportDataSource("DailyLinePlanFlrWiseExOtCost", ds.Tables[3]);
                    rvContainer.LocalReport.DataSources.Add(rds[3]);

                    rds[4] = new ReportDataSource("DailyLinePlanDeptWiseExOtCost", ds.Tables[4]);
                    rvContainer.LocalReport.DataSources.Add(rds[4]);

                    rds[5] = new ReportDataSource("DailyLinePlanByrWiseLineSummery", ds.Tables[5]);
                    rvContainer.LocalReport.DataSources.Add(rds[5]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptDailyLinePlan.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-5003": // "RPT-5003" Monthly Capacity Booking
                    vReportTitle = "Order Booking Status";
                    if ( !String.IsNullOrEmpty(ob.COMP_NAME_EN) )
                    {
                        vReportTitle += " Company: " + ob.COMP_NAME_EN; 
                    }

                    if (!String.IsNullOrEmpty(ob.OFFICE_NAME_EN))
                    {
                        vReportTitle += " Unit: " + ob.OFFICE_NAME_EN;
                    }

                    ds = ob.getMonthlyCapacityBooking();
                    rds[0] = new ReportDataSource("MONTHLY_CAP_BOOK", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rds[1] = new ReportDataSource("MONTHLY_CAP_BOOK_1", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rpMonthlyCapBooking.rdlc");
                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    var str = "MonthlyCapBooking";
                    vReportFilePrefix = Regex.Replace(str, "[^a-zA-Z0-9_]+", "_", RegexOptions.Compiled);

                    break;

                case "RPT-5004": // "RPT-5004" Production Plan (Maximum 16 Days)
                    vReportTitle = "Production Plan [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    ds = ob.ProdPlan();

                    rds[0] = new ReportDataSource("WeeklyProdPlan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptWeeklyProdPlan416Days.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-5005": // "RPT-5005" Order Wise Plan Execution
                    vReportTitle = "Order Wise Plan Execution";
                    ds = ob.OrderWisePlanExecution();

                    rds[0] = new ReportDataSource("OrderWisePlanExecution", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptOrderWisePlanExecution.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-5006": // "RPT-5006" Production Plan Summery
                    vReportTitle = "Production Plan Summery";
                    ds = ob.ProdPlanSummery();

                    rds[0] = new ReportDataSource("ProdPlanSummery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptProdPlanSummery.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-5007": // "RPT-5007" Order Wise Fabric Monitoring
                    vReportTitle = "Order Wise Fabric Monitoring";
                    ds = ob.OrdWiseFabMonitor();

                    rds[0] = new ReportDataSource("OrdWiseFabMonitor", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptOrdWiseFabMonitor.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-5008": // "RPT-5008" Buyer wise Capacity Allocation
                    vReportTitle = "Buyer wise Capacity Allocation as on " + ob.FROM_MONTH.Value.ToString("MMMM/yyyy");
                    ds = ob.ByrWiseCapctyAlloc();

                    rds[0] = new ReportDataSource("ByrWiseCapctyAlloc", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Planning/Reports/rptByrWiseCapctyAlloc.rdlc");

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
            }

            byte[] bytes = rvContainer.LocalReport.Render(rptType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

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

                if (ob.REPORT_CODE == "RPT-5003") //For Order Booking Status (Ordered + FabBooked+ Sewn Qty )
                {
                    ERPSolution.Areas.Security.Controllers.ScUserController.AutoMail4CapBk(savedFileName);
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