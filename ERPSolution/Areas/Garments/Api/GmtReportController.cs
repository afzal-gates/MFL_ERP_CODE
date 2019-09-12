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




namespace ERPSolution.Areas.Garments.Api
{
    [RoutePrefix("api/Gmt")]
    public class GmtReportController : ApiController
    {
        [Route("GmtReport/PreviewReport")]
        public IHttpActionResult PreviewReport(GmtReportModel ob)
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
                    vReportTitle = "Sewing M/C Specification";

                    dsTempReport = ob.SwMcnSpec();
                    //dsTempReport.WriteXml("d:\\SwMcnSpec.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Garments/Reports/rptSwMcnSpec.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

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



    }
}