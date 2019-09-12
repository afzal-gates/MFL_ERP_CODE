using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ERPSolution.Areas.Inventory.Api
{
    [RoutePrefix("api/Inv")]
    public class InvReportController : ApiController
    {
        [Route("InvReport/PreviewReport")]
        public IHttpActionResult PreviewReport(InvReportModel ob)
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
                    dsTempReport = ob.GoodsReceiveNote();
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


        [Route("InvReport/PreviewReportRDLC")]
        //[HttpGet]
        public IHttpActionResult PreviewReportRDLC(InvReportModel ob)
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
                case "RPT-3000": // "RPT-9000" Goods Receive Note
                    ds = ob.GoodsReceiveNote();
                    rds[0] = new ReportDataSource("dsReceiveGoodsNote", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Inventory/Reports/rptReceiveGoodsNote.rdlc");
                    //"~/Areas/Commercial/Reports/rptExportPIOne.rdlc"
                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2]});

                    break;
                case "RPT-3001": // "RPT-9001" General Stock Taking 
                    ds = ob.GeneralStockTaking();
                    vReportTitle = "General Stock Taking Report";
                    rds[0] = new ReportDataSource("dsGeneralStockTaking", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Inventory/Reports/rptGeneralStockTaking.rdlc");
                    //"~/Areas/Commercial/Reports/rptExportPIOne.rdlc"
                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-3002": // "RPT-3002" General Current Stock
                    ds = ob.GeneralCurrentStock();
                    vReportTitle = "Current Stock Report";
                    rds[0] = new ReportDataSource("dsGeneralItemCurrentStock", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Inventory/Reports/rptGeneralItemCurrentStock.rdlc");
                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-3003": // "RPT-3003" General Item Requisition Slip
                    ds = ob.GenItemReqSlip();
                    vReportTitle = "General Item Requisition Slip";
                    rds[0] = new ReportDataSource("dsGenItemReqSlip", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Inventory/Reports/rptGenItemReqSlip.rdlc");
                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-3004": // "RPT-3004"  Stock Ledger
                    ds = ob.GeneralStockLedger();
                    rds[0] = new ReportDataSource("dsGeneralStockLedger", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Inventory/Reports/rptGeneralStockLedger.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[2] = new ReportParameter("RequisitionType", ob.REQ_TYPE_NAME);
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

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
