using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Model.Commercial;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ERPSolution.Areas.Commercial.Api
{
    [RoutePrefix("api/Cmr")]
    public class CmrReportController : ApiController
    {
        [Route("CmrReport/PreviewReport")]
        public IHttpActionResult PreviewReport(CmrReportModel ob)
        {
            string vReportCode = ob.REPORT_CODE; //Request.Form["pREPORT_CODE"];

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
                    dsTempReport = ob.ExportPIOne();
                    //dsTempReport.WriteXml("E:\\ReportXML\\DyesChemicalBatchProgramRequisition.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/rptDCBatchRequisition.rpt"));

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
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsCostBreakDownSummary", dt));
        }

        [Route("CmrReport/PreviewReportRDLC")]
        //[HttpGet]
        public IHttpActionResult PreviewReportRDLC(CmrReportModel ob)
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

            ReportViewer rvContainer = new ReportViewer();

            rvContainer.LocalReport.Refresh();
            rvContainer.Reset();
            rvContainer.LocalReport.EnableExternalImages = true;
            rvContainer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

            switch (vReportCode)
            {
                case "RPT-7000": // "RPT-7000" Export PI One
                    ds = ob.ExportPIOne();
                    rds[0] = new ReportDataSource("dsExportPI_H", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("dsExportPI_D", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ob.RPT_PATH_URL);
                    //"~/Areas/Commercial/Reports/rptExportPIOne.rdlc"
                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    //rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-7001": // "RPT-4000" Test Report
                    ds = ob.ExportPIOne();
                    rds[0] = new ReportDataSource("DataSet1", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/Report1.rdlc");
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



    }
}
