using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.Model.Purchase;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ERPSolution.Areas.Purchase.Api
{
    [RoutePrefix("api/Pur")]
    public class PurReportController : ApiController
    {
        [Route("PurReport/PreviewReport")]
        public IHttpActionResult PreviewReport(PurReportModel ob)
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
                    dsTempReport = ob.PurchaseOrder();
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


        [Route("PurReport/PreviewReportRDLC")]
        //[HttpGet]
        public IHttpActionResult PreviewReportRDLC(PurReportModel ob)
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
                case "RPT-8000": // "RPT-8000" PO
                    ds = ob.PurchaseOrder();
                    rds[0] = new ReportDataSource("dsPO_H", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("dsPO_D", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Purchase/Reports/rptPurchaseOrder.rdlc");
                    //"~/Areas/Commercial/Reports/rptExportPIOne.rdlc"
                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2]});

                    break;
                case "RPT-8001": // "RPT-8001" General Requisition
                    ds = ob.PurchaseRequisition();
                    rds[0] = new ReportDataSource("dsGeneralRequisition", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Purchase/Reports/rptGeneralRequisition.rdlc");
                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2]});
                    break;

                case "RPT-8002": // "RPT-8002" Fund Requisition
                    ds = ob.FundRequisition();
                    rds[0] = new ReportDataSource("dsFundRequisition", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Purchase/Reports/rptFundRequisition.rdlc");
                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2]});
                    break;

                case "RPT-8003": // "RPT-8003" Item Fund Requisition
                    ds = ob.ItemFundRequisition();
                    rds[0] = new ReportDataSource("dsFunReqItemPrint", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Purchase/Reports/rptFunReqItemPrint.rdlc");
                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-8004": // "RPT-8004" Inventory Adjustment Requisition
                    ds = ob.InventoryAdjustmentRequisition();
                    rds[0] = new ReportDataSource("dsInventoryAdjustment", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Purchase/Reports/rptInventoryAdjustment.rdlc");
                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;
                    
                //case "RPT-8001": // "RPT-8001" Test Report
                //    ds = ob.PurchaseOrder();
                //    rds[0] = new ReportDataSource("DataSet1", ds.Tables[0]);
                //    rvContainer.LocalReport.DataSources.Add(rds[0]);
                //    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Dyeing/Reports/Report1.rdlc");
                //    break;

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
