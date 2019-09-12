using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Reporting.WebForms;

namespace ERPSolution.Areas.Inventory.Controllers
{
    [SignInCheck]
    public class InvReportController : BaseController
    {
        [HttpPost]
        public void PreviewReport(InvReportModel ob)
        {
            string vReportCode = ob.REPORT_CODE; //Request.Form["pREPORT_CODE"];
            //vReportCode = vReportCode.Substring(0, vReportCode.Length - 1);

            string vProductTitle = "MultiTex ERP";
            string vReportTitle = "";
            string vFloorLine = "";
            DataSet dsTempReport = new DataSet();
            ReportDocument rd = new ReportDocument();

            switch (vReportCode)
            {
                case "RPT-3000": // "RPT-3000" Item List By Login User
                    vReportTitle = "Item List By User";
                    dsTempReport = ob.ItemListByUser();
                    //dsTempReport.WriteXml("d:\\ItemList.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Inventory/Reports/rptItemListByUser.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;                
            }

            //rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
            //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameB"].ToString() + "'";
            //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressB"].ToString() + "'";

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
            string x = "";
        }


        [HttpPost]
        public void PreviewReportRDLC(InvReportModel ob)
        {
            string vReportCode = ob.REPORT_CODE; //Request.Form["pREPORT_CODE"];
            //vReportCode = vReportCode.Substring(0, vReportCode.Length - 1);

            string vProductTitle = "MultiTex ERP";
            string vReportTitle = "";
            string vReportTitle1 = "";
            string vFloorLine = "";
            DataSet ds;

            ReportDataSource[] rds = new ReportDataSource[10];
            ReportParameter[] p = new ReportParameter[10];

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
                
                case "RPT-3000": // "RPT-3000" Item List

                    vReportTitle = "Item List";
                    
                    ds = ob.InvItemList();

                    rds[0] = new ReportDataSource("InvItemList", ds.Tables["INV_ITEM_LIST"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Inventory/Reports/rptInvItemList.rdlc");

                    p[0] = new ReportParameter("vCompanyName",Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", Session["multiCurrCompanyAddressE"].ToString());
                    //foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    //{
                    //    p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                    //    p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));
                    //}
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    
                    //p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));

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
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            try
            {
                response.Clear();
                response.ContentType = mimeType;
                response.Cache.SetCacheability(HttpCacheability.Private);
                response.Expires = -1;
                response.Buffer = true;
                response.BinaryWrite(bytes);
                response.AppendHeader("Content-Disposition", "inline; filename=" + "MultiTex_" + ob.REPORT_CODE + '_' + DateTime.Now.ToString("yyyyMMMdd_HHmm") + ".xls");
                response.End();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        lastStep:
            string x = "";
        }


    }
}