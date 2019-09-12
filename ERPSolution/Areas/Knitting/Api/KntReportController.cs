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




namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    public class KntReportController : ApiController
    {
        [Route("KntReport/PreviewReport")]
        public IHttpActionResult PreviewReport(KntReportModel ob)
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
                case "RPT-3501": // "RPT-3501" Knitting Sub-contract Program Cum Contract Report
                    vReportTitle = "Knitting Sub-contract Program";
                    dsTempReport = ob.KnittingSubContractProgramIssue();
                    //dsTempReport.WriteXml("d:\\KnittingSubContractProgramIssue.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKnittingSubContractProgramIssue.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3502": // "RPT-3502" Knitting Subcontract Yarn Delivery Challan Issue Report
                    vReportTitle = ob.IS_YD == "Y" ? "DELIVERY CHALLAN (Y/D)" : "YARN DELIVERY CHALLAN/GATE PASS";
                    dsTempReport = ob.IS_YD == "Y" ? ob.KntSubContrYrnDelivChlnIssue() : ob.KntYrnDelivChlnIssue();
                    //dsTempReport.WriteXml("E:\\KntSubContrYrnDelivChlnIssue.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntSubContrYrnDelivChlnIssue.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3503": // "RPT-3503" Yarn Receive Statement Report
                    vReportTitle = "Yarn Receive Statement";
                    vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.KntYrnRcvStatement();
                    //dsTempReport.WriteXml("d:\\KntYrnRcvStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntYrnRcvStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3504": // "RPT-3504" Yarn Current Stock Report
                    vReportTitle = "Yarn Current Stock";
                    dsTempReport = ob.KntYrnCurrentStock();
                    //dsTempReport.WriteXml("d:\\KntYrnCurrentStock.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntYrnCurrentStock.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3505": // "RPT-3505" Order Wise Yarn Issue Register Report

                    vReportTitle = "Order Wise Yarn Issue Register";
                    vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.KntOrdWiseYrnIssRegister();
                    //dsTempReport.WriteXml("e:\\KntOrdWiseYrnIssRegister.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntOrdWiseYrnIssRegister.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3507": // "RPT-3507" Machine Loction wise Yarn Requsition
                    vReportTitle = "Yarn Requsition Statement for Knitting Bulk";
                    vReportTitle1 = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.KntMchLocWiseYrnReqSummery();
                    //dsTempReport.WriteXml("d:\\KntMchLocWiseYrnReqSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntMchLocWiseYrnReqSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3508": // "RPT-3508" S/C Grey Fabric Delivery Challan (In House)
                    vReportTitle = "Grey Fabric Delivery and Yarn Return Challan (S/C In-house)";

                    dsTempReport = ob.KntScFabDelvChallan();
                    //dsTempReport.WriteXml("e:\\KntScFabDelvChallan.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntScGreyFabDelvChallan.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3509": // "RPT-3509" Yarn Return Challan (Yarn Test)
                    vReportTitle = "Yarn Return Challan (Yarn Test)";

                    dsTempReport = ob.YarnReturnChallanTest();
                    //dsTempReport.WriteXml("d:\\YarnReturnChallanTest.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnRtnChallanTest.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3510": // "RPT-3510" Yarn Return Challan (Sample)
                    vReportTitle = "Yarn Return Challan ";

                    dsTempReport = ob.YarnReturnChallanSample();
                    //dsTempReport.WriteXml("E:\\YarnReturnChallanSample.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnRtnChallanSmp.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3511": // "RPT-3511" Yarn Test Requisition
                    vReportTitle = "Yarn Test Requisition";

                    dsTempReport = ob.YarnTestReq();
                    //dsTempReport.WriteXml("d:\\YarnTestReq.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnTestReq.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3512": // "RPT-3512" Yarn Requsition for Knitting Sample
                    vReportTitle = "Yarn Requsition Statement for Knitting Sample - Solid (By Date)";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.YarnReq4KnittingSmp();
                    //dsTempReport.WriteXml("d:\\YarnReq4KnittingSmp.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnReq4KnittingSmp.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3514": // "RPT-3514" Party wise ledger(S/C In-house)
                    vReportTitle = "Party wise ledger(S/C In-house)";

                    if (ob.SCM_STORE_ID == null)
                        vReportTitle1 = "Store: All";
                    else
                        vReportTitle1 = "Store: " + ob.STORE_NAME_EN;

                    dsTempReport = ob.GreyStoreLedgerByParty();
                    //dsTempReport.WriteXml("d:\\KntScFabRegByParty.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntGreyStoreLedgerByParty.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3517": // "RPT-3517" Party wise ledger (S/C Out-house)
                    vReportTitle = "Party wise ledger (S/C Out-house)";
                    vReportTitle1 = "S/C Program Issue";

                    dsTempReport = ob.GreyStoreLedgerByPartyOutHouse();
                    //dsTempReport.WriteXml("d:\\KntGreyStoreLedgerByPartyOutHouse.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntGreyStoreLedgerByPartyOutHouse.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                //case "RPT-3519": // "RPT-3519" Daily M/C and Shift wise Knitting Statement
                //    vReportTitle = "Daily M/C and Shift wise Knitting Statement";
                //    vReportTitle1 = "Production Date: " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                //    dsTempReport = ob.OrderColorFabricWiseKntStatement();
                //    //dsTempReport.WriteXml("e:\\OrderColorFabricWiseKntStatement.xml", XmlWriteMode.WriteSchema);
                //    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrderColorFabricWiseKntStatement.rpt"));

                //    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                //    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                //    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                //    break;


                case "RPT-3520": // "RPT-3520" S/C Reject Grey Fabric Delivery Challan (Out House)
                    vReportTitle = "Reject Grey Fabric Delivery Challan (S/C Out-house)";
                    vReportTitle1 = "";

                    dsTempReport = ob.KntScoRejGreyFabRtnChallan();
                    //dsTempReport.WriteXml("d:\\KntScoRejGreyFabRtnChallan.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntScoRejGreyFabRtnChallan.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3521": // "RPT-3521" Knit Card List Report
                    vReportTitle = "Date Wise Knit Card List";
                    vReportTitle1 = "";

                    dsTempReport = ob.DateWisePendingList();
                    // dsTempReport.WriteXml("d:\\DateWisePendingList.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDateWisePendingList.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3522": // "RPT-3522" Challan wise Party Ledger (S/C Out-house)
                    vReportTitle = "Challan wise Party Ledger (S/C Out-house) [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    vReportTitle1 = "S/C Program Issue";

                    dsTempReport = ob.ScoChlnWisePartyLedger();
                    //dsTempReport.WriteXml("d:\\ScoChlnWisePartyLedger.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoChlnWisePartyLedger.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3523": // "RPT-3523" Order and Color wise Statement (S/C Out-house)
                    vReportTitle = "Order and Color wise Statement (S/C Out-house) As on " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    vReportTitle1 = "S/C Program Issue";

                    dsTempReport = ob.ScoOrderStatement();
                    //dsTempReport.WriteXml("d:\\ScoOrderStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoOrderStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3524": // "RPT-3524" Yarn Transfer Challan (S/C Out-house)
                    vReportTitle = "Yarn Transfer Challan for Knitting (S/C Out-house)";
                    vReportTitle1 = "S/C Program Transfer";

                    dsTempReport = ob.ScoProgTransYrnDelivChln();
                    //dsTempReport.WriteXml("d:\\ScoProgTransYrnDelivChln.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntScoProgTransYrnDelivChln.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3525": // "RPT-3525" Order and color wise fabric receive to grey store 
                    vReportTitle = "Order and color wise fabric receive to grey store as on " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    //vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.OrdColWiseFabRcvToGreyStore();
                    //dsTempReport.WriteXml("d:\\OrdColWiseFabRcvToGreyStore.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdColWiseFabRcvToGreyStore.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3526": // "RPT-3526" M/C wise knitting production - capacity vs achievement
                    vReportTitle = "M/C wise monthly knitting production - capacity vs achievement";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.MonthlyProdCapacityVsAchieve();
                    //dsTempReport.WriteXml("d:\\MonthlyProdCapacityVsAchieve.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntMonthlyProdCapacityVsAchieve.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3528": // "RPT-3528" Yarn Dyeing Program
                    vReportTitle = "Yarn Dyeing Program";
                    dsTempReport = ob.YarnDyeingProgramData();
                    // dsTempReport.WriteXml("d:\\YarnDyeingProgramData.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnDyeingProgram.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3531": // "RPT-3531" Order and Color wise Knitting Statement
                    vReportTitle = "Order and Color wise Knitting Statement " + (ob.MONTHOF == null ? "[All Month]" : "for the Month of " + ob.MONTHOF);
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.OrdColWiseProdStatus();
                    //dsTempReport.WriteXml("e:\\OrdColWiseProdStatus.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntOrdColWiseProdStatus.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3532": // "RPT-3532" Lot wise loose yarn statement
                    vReportTitle = "Lot wise loose yarn statement";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.LotWiseLooseYrnStatement();
                    //dsTempReport.WriteXml("d:\\LotWiseLooseYrnStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptLotWiseLooseYrnStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3535": // "RPT-3535" Party wise Knitting Performance by Program (S/C Out-house)
                    vReportTitle = "Party wise Knitting Performance by Program (S/C Out-house)";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.ScoPartyWisePerformance();
                    //dsTempReport.WriteXml("d:\\ScoPartyWisePerformance.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoPartyWisePerformance.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                //case "RPT-3537": // "RPT-3537" Order and Color wise Fabric Production Statement
                //    vReportTitle = "Order and Color wise Fabric Production Statement" + (ob.MONTHOF == null ? "[All Month]" : "for the Month of " + ob.MONTHOF);
                //    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                //    dsTempReport = ob.OrdColWiseFabProd();
                //    //dsTempReport.WriteXml("e:\\OrdColWiseFabProd.xml", XmlWriteMode.WriteSchema);
                //    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntOrdColWiseFabProd.rpt"));

                //    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                //    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                //    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                //    break;

                case "RPT-3540": // "RPT-3540" Party wise Knitting Performance Summary (S/C Out-house)
                    vReportTitle = "Party wise Knitting Performance Summary (S/C Out-house)";
                    vReportTitle1 = "[Top " + ob.NO_OF_COUNT + " List From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.ScoPartyWisePerformanceSummery();
                    //dsTempReport.WriteXml("d:\\ScoPartyWisePerformanceSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoPartyWisePerformanceSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3541": // "RPT-3541" Collar/Cuff Program (S/C Out-house)
                    vReportTitle = "Collar/Cuff Program (S/C Out-house)";
                    vReportTitle1 = ""; //"[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.ScoCollarCuffProg();
                    //dsTempReport.WriteXml("d:\\ScoCollarCuffProg.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoCollarCuffProg.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3542": // "RPT-3542" Daily Roll Labelling Statement (S/C Out-house)
                    vReportTitle = "Daily Roll Labelling Statement (S/C Out-house)";
                    vReportTitle1 = ""; //"[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.ScoRollLabellingStatment();
                    //dsTempReport.WriteXml("d:\\ScoRollLabellingStatment.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoRollLabellingStatment.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3543": // "RPT-3543" M/C and Shift wise Oil Consumption & Unit Cost
                    vReportTitle = "M/C and Shift wise Oil Consumption & Unit Cost";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.McOilConsumpShiftWise();
                    //dsTempReport.WriteXml("d:\\McOilConsumpShiftWise.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptMcOilConsumpShiftWise.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3544": // "RPT-3544" M/C and Operator wise Oil Consumption & Unit Cost
                    vReportTitle = "M/C and Operator wise Oil Consumption & Unit Cost";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.McOilConsumpOperatorWise();
                    //dsTempReport.WriteXml("d:\\McOilConsumpOperatorWise.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptMcOilConsumpOperatorWise.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3545": // "RPT-3545" Daily Collar/Cuff Transfer to QC and Store
                    vReportTitle = "Daily Collar/Cuff Transfer to QC and Store";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.DailyClCfTransToQC();
                    //dsTempReport.WriteXml("d:\\DailyClCfTransToQC.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDailyClCfTransToQC.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3547": // "RPT-3547" Order and Color wise Collar/Cuff Production
                    vReportTitle = "Order and Color wise Collar/Cuff Production";

                    dsTempReport = ob.OrdColorWiseClCfProd();
                    //dsTempReport.WriteXml("d:\\DailyClCfProd.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdColorWiseClCfProd.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3548": // "RPT-3548" Daily Collar/Cuff Production
                    vReportTitle = "Daily Collar/Cuff Production";

                    dsTempReport = ob.DailyCollarCuffProd();
                    //dsTempReport.WriteXml("d:\\DailyCollarCuffProd.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDailyCollarCuffProd.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3549": // "RPT-3549" S/C Party Wise Rate Report
                    vReportTitle = "S/C Party Wise Rate Report";

                    dsTempReport = ob.ScPartyWiseRate();
                    //dsTempReport.WriteXml("E:\\ScPartyWiseRate.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScPartyWiseRate.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3550": // "RPT-3549" Y/D Ledger
                    vReportTitle = "Y/D Ledger";

                    dsTempReport = ob.getDyedYarnLedger();
                    //dsTempReport.WriteXml("E:\\DyedYarnLedger.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDyedYarnLedger.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3551": // "RPT-3551" Excess Yarn Statement
                    vReportTitle = "Excess Yarn Statement [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.ExcessYarnStatement();
                    //dsTempReport.WriteXml("d:\\ExcessYarnStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptExcessYarnStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3552": // "RPT-3552" M/C and Date wise Needle Broken
                    vReportTitle = "M/C and Date wise Needle Broken [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.McNeedleBroken();
                    //dsTempReport.WriteXml("d:\\McNeedleBroken.xml", XmlWriteMode.WriteSchema); //3551
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptMcNeedleBroken.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    foreach (DataRow dr in dsTempReport.Tables[1].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["REG_ADD_EN"]) + "'";
                        break;
                    }
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3554": // "RPT-3554" Yarn Requsition By Requsition (Sample)
                    vReportTitle = "Yarn Requsition for " + (ob.RF_REQ_TYPE_ID == 5 || ob.RF_REQ_TYPE_ID == 6 ? "Sample" : "Balk");

                    dsTempReport = ob.YarnReqSmpByReqNo();
                    //dsTempReport.WriteXml("d:\\YarnReqSmpByReqNo.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnReqSmpByReqNo.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3560": // "RPT-3560" Yarn Requsition Statement for Knitting Sample Y/D (By Date)
                    vReportTitle = "Yarn Requsition Statement for Knitting Sample - Y/D (By Date)";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.YarnReq4KnittingSmpYD();
                    //dsTempReport.WriteXml("e:\\YarnReq4KnittingSmp.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnReq4KnittingSmpYD.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3563": // "RPT-3563" S/C In-house Bill Print
                    vReportTitle = "Knitting S/C In-house Bill/Invoice";
                    //vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.ScInhouseBill();
                    //dsTempReport.WriteXml("e:\\ScInhouseBill.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScInhouseBill.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3564": // "RPT-3564" Multiple Knit Card Print
                    vReportTitle = "Knitting Program";
                    //vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.MultiKnitCard();
                    //dsTempReport.WriteXml("d:\\MultiKnitCard.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptMultiKnitCard.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-3565": // "RPT-3565" Fabric Reject Summery (S/C Out-house)
                    vReportTitle = "Fabric Reject Summery (S/C Out-house)";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.ScoFabricRejectSummery();
                    //dsTempReport.WriteXml("d:\\ScoFabricRejectSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoFabricRejectSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-3566": // "RPT-3566" Knitting Program for Sample
                    vReportTitle = "Knitting Program for Sample";
                    //vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.KnittingProg4Sample();
                    //dsTempReport.WriteXml("d:\\KnittingProg4Sample.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKnittingProg4Sample.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-3569": // "RPT-3569" Order wise Collar/Cuff Stock
                    vReportTitle = "Order wise Collar/Cuff Stock";
                    //vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.OrdWiseCollarCuffStock();
                    //dsTempReport.WriteXml("e:\\OrdWiseCollarCuffStock.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdWiseCollarCuffStock.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    //foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    //{
                    //    //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                    //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    //}
                    break;

                case "RPT-3570": // "RPT-3570" Y/D Statement
                    vReportTitle = "Y/D Statement";
                    //vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.YdStatement();
                    //dsTempReport.WriteXml("d:\\YdStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYdStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    //foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    //{
                    //    //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                    //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    //}
                    break;

                case "RPT-3572": // "RPT-3572" Party Wise Order Statement (S/C In-house)

                    vReportTitle = "Party Wise Order Statement (S/C In-house)";
                    //vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.SciPartyWiseOrderStatement();
                    //dsTempReport.WriteXml("d:\\SciPartyWiseOrderStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptSciPartyWiseOrderStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    //foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    //{
                    //    //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                    //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    //}
                    break;

                case "RPT-3575": // "RPT-3575" Party Wise Order Statement (S/C Out-house)

                    vReportTitle = "Party Wise Order Statement (S/C Out-house)";
                    //vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.ScoPartyWiseOrderStatement();
                    //dsTempReport.WriteXml("e:\\ScoPartyWiseOrderStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoPartyWiseOrderStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    //foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    //{
                    //    //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                    //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    //}
                    break;

                case "RPT-3582": // "RPT-3582" Dyed Yarn Delivery Challan (Transfer to S/C Party)
                    vReportTitle = "DELIVERY CHALLAN (Dyed Yarn Transfer to Party)";
                    dsTempReport = ob.KntDyedYrnDelivChlnTrans2ScParty();
                    //dsTempReport.WriteXml("E:\\KntSubContrYrnDelivChlnIssue.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntDyedYrnDelivChln2ScParty.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-3583": // "RPT-3583" Yarn Requsition Statement for Collar/Cuff
                    vReportTitle = "Yarn Requsition Statement for Collar/Cuff";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    dsTempReport = ob.YarnReq4KntClcf();
                    //dsTempReport.WriteXml("e:\\YarnReq4KntClcf.xml", XmlWriteMode.WriteSchema);
                    rd.Load(HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnReq4KntClcf.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle1"].Text = "'" + vReportTitle1 + "'";
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


        [Route("KntReport/PreviewReportRDLC")]
        //[HttpGet]
        public IHttpActionResult PreviewReportRDLC(KntReportModel ob)
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

            string FilePath = "";

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

                case "RPT-3502": // "RPT-3502" Knitting Subcontract Yarn Delivery Challan Issue Report
                    vReportTitle = ob.IS_YD == "Y" ? "DELIVERY CHALLAN (Y/D)" : "YARN DELIVERY CHALLAN/GATE PASS";
                    ds = ob.IS_YD == "Y" ? ob.KntSubContrYrnDelivChlnIssue() : ob.KntYrnDelivChlnIssue();

                    rds[0] = new ReportDataSource("dsPartyWiseYarnIssueChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntSubContrYrnDelivChlnIssue.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.EnableExternalImages = true;
                    FilePath = @"file:\" + HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/COMPANY_PAD/" + (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() : "MFL") + ".JPG"); //Application.StartupPath is for WinForms, you should try AppDomain.CurrentDomain.BaseDirectory  for .net
                    p[4] = new ReportParameter("CompanyImg", FilePath);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-3506": // "RPT-3506" Yarn Issue Statement
                    ds = ob.KntYrnIssStatement();
                    //vReportTitle = "Yarn Issue Statement";
                    vReportTitle = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    rds[0] = new ReportDataSource("dsKntYrnIssStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntYrnIssStatement.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3513": // "RPT-3513" Yarn Requisition Wise Issue Report
                    ds = ob.YarnReqWithIssue();
                    rds[0] = new ReportDataSource("dsKNT_REQ_ISS", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntYarnRequisitionWiseIssue.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1] });
                    break;

                case "RPT-3515": // "RPT-3515" Grey Fabric Delivery Challan to Store
                    vReportTitle = "Grey Fabric Delivery Challan to Store " + (ob.RF_FAB_PROD_CAT_NAME_LST == null ? "[All]" : "[" + ob.RF_FAB_PROD_CAT_NAME_LST + "]"); // + (ob.SCM_STORE_ID == null ? "[All Store]" : "[" + ob.STORE_NAME_EN + "]");

                    ds = ob.GreyFabDelvChalnToStore();
                    rds[0] = new ReportDataSource("GreyFabDelvChalnToStore", ds.Tables["GREY_FAB_DELV_CHALN_TO_STORE"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptGreyFabDelvChalnToStore.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy"));
                    p[5] = new ReportParameter("vStoreName", ob.STORE_NAME_EN);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-3516": // "RPT-3516" Grey Fabric Roll Inspection
                    vReportTitle = "Grey Fabric Roll Inspection";

                    ds = ob.GreyFabricRollInspection();
                    rds[0] = new ReportDataSource("dsGrayFabricInspection", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptGrayFabricInspection.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", ob.KNT_QC_STS_TYPE_ID == null ? "Grey Fabric Roll Inspection Report" : ob.KNT_QC_STS_TYPE_ID == 1 ? "Passed Grey Fabric Roll Inspection Report" : ob.KNT_QC_STS_TYPE_ID == 3 ? "Hold Grey Fabric Roll Inspection Report" : "Rejected Grey Fabric Roll Inspection Report");
                    //p[3] = new ReportParameter("vReportTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE == null ? null : ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE == null ? null : ob.TO_DATE.ToString());
                    //p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    //p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    p[5] = new ReportParameter("StyleOrder", ob.STYLE_NO);
                    p[6] = new ReportParameter("Color", ob.COLOR_NAME_EN);
                    p[7] = new ReportParameter("StatusType", ob.QC_STS_TYP_NAME);
                    p[8] = new ReportParameter("Shift", ob.SCHEDULE_NAME_EN);
                    p[9] = new ReportParameter("PartyName", ob.SUP_TRD_NAME_EN);
                    p[10] = new ReportParameter("ProductionType", ob.KNT_SC_PRG_ISS_NAME);
                    p[11] = new ReportParameter("FabType", ob.FAB_TYPE_NAME);
                    p[12] = new ReportParameter("RollNo", ob.FAB_ROLL_NO);
                    p[13] = new ReportParameter("BuyerName", ob.BYR_ACC_GRP_NAME_EN);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7], p[8], p[9], p[10], p[11], p[12], p[13] });
                    break;

                case "RPT-3518": // "RPT-3518" Grey Fabric Roll Inspection
                    vReportTitle = "Yarn Stock With Value";

                    ds = ob.YarnStockWithValue();
                    rds[0] = new ReportDataSource("dsYarnStockWithValue", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnStockWithValue.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vProductTitle);
                    p[4] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[5] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3519": // "RPT-3519" Daily M/C and Shift wise Knitting Statement
                    vReportTitle = "Daily M/C and Shift wise Knitting Statement";
                    vReportTitle1 = "Production Date: " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                    ds = ob.OrderColorFabricWiseKntStatement();
                    rds[0] = new ReportDataSource("MC_SHIFT_WISE_KNIT", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("MC_SHIFT_WISE_KNIT1", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rds[2] = new ReportDataSource("MC_SHIFT_WISE_KNIT2", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptMcShiftWiseKntStatement.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-3527": // "RPT-3527" Order And Color Wise Inspection
                    vReportTitle = "Order & Color Wise Inspection";

                    ds = ob.OrderAndColorWiseInspection();
                    rds[0] = new ReportDataSource("dsOrderAndColorWiseInspection", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrderAndColorWiseInspection.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    p[4] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[5] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3529": // "RPT-3528" Date wise Grey Fabric QC Status
                    vReportTitle = "Date wise Grey Fabric QC Status";

                    ds = ob.DailyGrayQCSummary();
                    rds[0] = new ReportDataSource("dsDailyGrayQCSummary", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDailyGrayQCSummary.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    p[4] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[5] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3530": // "RPT-3530" Monthly Out Side Quality Summary
                    vReportTitle = "Monthly Out Side Quality Summary";

                    ds = ob.MonthlyOutSideQualitySummary();
                    rds[0] = new ReportDataSource("dsMonthlyOutSideQualitySummary", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptMonthlyOutSideQualitySummary.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    p[4] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[5] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3533": // "RPT-3533" Supplier Wise Yarn Test Result
                    vReportTitle = "Supplier Wise Yarn Test Result";

                    ds = ob.SupplierWiseYarnTestResult();
                    rds[0] = new ReportDataSource("dsSupplierWiseYarnTestResult", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptSupplierWiseYarnTestResult.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    p[4] = new ReportParameter("FromDate", ob.FROM_DATE == null ? null : ob.FROM_DATE.ToString());
                    p[5] = new ReportParameter("ToDate", ob.TO_DATE == null ? null : ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3534": // "RPT-3534" Lot & Count Wise Loose Yarn Stock
                    vReportTitle = "Lot & Count Wise Loose Yarn Stock";

                    ds = ob.LotCountWiseLooseYarnStock();
                    rds[0] = new ReportDataSource("dsLotCountWiseLooseYarnStock", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptLotCountWiseLooseYarnStock.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    //p[4] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    //p[5] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3536": // "RPT-3536" Yarn Inventory Aging
                    vReportTitle = "Yarn Inventory Aging";

                    ds = ob.YarnInventoryAgeing();
                    rds[0] = new ReportDataSource("dsYarnInventoryAgeing", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnInventoryAgeing.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    //p[4] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    //p[5] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3537": // "RPT-3537" Order and Color wise Fabric Production Statement

                    vReportTitle = "Order and Color wise Fabric Production Statement " + (ob.MONTHOF == null ? "[All Month]" : "for the Month of " + ob.MONTHOF);
                    ds = ob.OrdColWiseFabProd();

                    rds[0] = new ReportDataSource("OrdColWiseFabProd", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntOrdColWiseFabProd.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3538": // "RPT-3538" Monthly Grey QC Summary
                    vReportTitle = "Monthly Grey QC Summary";

                    ds = ob.MonthlyGreyQCSummary();
                    rds[0] = new ReportDataSource("dsDailyGrayQCSummary", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptMonthlyGreyQCSummary.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    p[4] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[5] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3539": // "RPT-3539" Monthly Grey QC Summary With Defect Type
                    vReportTitle = "Monthly Quality Summary Report Before Dyeing";

                    ds = ob.MonthlyGreyQCSummaryWithDefectType();
                    rds[0] = new ReportDataSource("dsMonthlyGreyQCInSummary", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);
                    rds[1] = new ReportDataSource("dsMonthlyGreyQCOutSummary", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);
                    rds[2] = new ReportDataSource("dsMonthlyGreyQCSummary", ds.Tables[2]);
                    rvContainer.LocalReport.DataSources.Add(rds[2]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptMonthlyGreyQCSummaryWithDefectType.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);
                    p[4] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[5] = new ReportParameter("ToDate", ob.TO_DATE.ToString());

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;


                case "RPT-3546": // "RPT-3546" Roll# wise Grey Fabric Delivery Challan to Store
                    vReportTitle = "Roll# wise Grey Fabric Delivery Challan to Store " + (ob.RF_FAB_PROD_CAT_NAME_LST == null ? "[All]" : "[" + ob.RF_FAB_PROD_CAT_NAME_LST + "]"); // + (ob.SCM_STORE_ID == null ? "[All Store]" : "[" + ob.STORE_NAME_EN + "]");

                    ds = ob.RollNoWiseGreyFabDelvChalnToStore();
                    rds[0] = new ReportDataSource("GreyFabDelvChalnToStore", ds.Tables["GREY_FAB_DELV_CHALN_TO_STORE"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptRollNoWiseGreyFabDelvChalnToStore.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy"));
                    p[5] = new ReportParameter("vStoreName", ob.STORE_NAME_EN);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-3553": // "RPT-3553" Needle Requisition Slip
                    vReportTitle = "Needle Requisition Slip";

                    ds = ob.NeedleRequisitionSlip();

                    rds[0] = new ReportDataSource("dsHeader", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("dsDetails", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptNeedleRequisitionSlip.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3555": // "RPT-3555" Needle Stock Taking
                    vReportTitle = "Stock Taking Report";

                    ds = ob.NeedleStockTaking();

                    //ds.WriteXml("E:\\NeedleStockTaking.xml", XmlWriteMode.WriteSchema);

                    rds[0] = new ReportDataSource("dsNeedleStockTaking", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptNeedleStockTaking.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());
                    p[5] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3556": // "RPT-3556" Needel Opening Balance
                    vReportTitle = "Needel Opening Balance Report";

                    ds = ob.NeedelOpeningBalance();

                    rds[0] = new ReportDataSource("dsNeedelOpeningBalance", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptNeedelOpeningBalance.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3557": // "RPT-3557" Oil Opening Balance
                    vReportTitle = "M/C Oil Opening Balance Report";

                    ds = ob.OilOpeningBalance();

                    rds[0] = new ReportDataSource("dsNeedelOpeningBalance", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptNeedelOpeningBalance.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3558": // "RPT-3558" S/C Party Rate
                    vReportTitle = "S/C Party Rate";

                    ds = ob.ScPartyRate();

                    rds[0] = new ReportDataSource("dsSubContPartyRate", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptSubContPartyRate.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-3559": // "RPT-3559" Oil Stock Taking
                    vReportTitle = "Oil Stock Taking Report";

                    ds = ob.OilStockTaking();

                    rds[0] = new ReportDataSource("dsNeedleStockTaking", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOilStockTaking.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());
                    p[5] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3561": // "RPT-3561" Date wise M/C Oil Consumption
                    vReportTitle = "Date wise M/C Oil Consumption";

                    ds = ob.DailyMachineOilConsumption();

                    rds[0] = new ReportDataSource("dsDailyMachineOilConsumption", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDailyMachineOilConsumption.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    //p[3] = new ReportParameter("MonthOf", ob.FROM_DATE.ToString());
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());
                    p[5] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-3562": // "RPT-3562" Order Wise Yarn Requisition
                    vReportTitle = "Order Wise Yarn Requisition";

                    ds = ob.OrderWiseYarnRequisition();

                    rds[0] = new ReportDataSource("dsOrderWiseYarnRequisition", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    //rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnRequirement.rdlc");
                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrderWiseYarnRequisition.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3567": // "RPT-3567" Daily Trims Receive Statement
                    vReportTitle = "Daily Trims Receive Statement";

                    ds = ob.DailyTrimsReceiveStatement();

                    rds[0] = new ReportDataSource("dsDailyTrimsReceiveStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDailyTrimsReceiveStatement.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3568": // "RPT-3568" Daily M/C and Shift wise Needle Statement
                    vReportTitle = "Daily M/C and Shift wise Needle Statement";

                    ds = ob.DailyNeedleBrokenConsumption();

                    rds[0] = new ReportDataSource("dsDailyNeedleBrokenConsumption", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDailyNeedleBrokenConsumption.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.ToString());
                    p[5] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;


                case "RPT-3571": // "RPT-3571" Buffer Yarn Purchase Requisition
                    vReportTitle = "Buffer Yarn Purchase Requisition";

                    ds = ob.BufferYarnPurchaseRequisition();

                    rds[0] = new ReportDataSource("dsBufferYearPurchase", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptBufferYearPurchase.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-3573": // "RPT-3573" Party Wise Bill Statement (S/C In-house)
                    vReportTitle = "Party Wise Bill Statement (S/C In-house)";

                    ds = ob.SciPartyWiseBillStatement();

                    rds[0] = new ReportDataSource("dsSciPartyWiseBillStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptSciPartyWiseBillStatement.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3574": // "RPT-3558" Dyeing S/C Party Rate
                    vReportTitle = "Dyeing S/C Party Rate";

                    ds = ob.ScPartyRate();

                    rds[0] = new ReportDataSource("dsSubContPartyRate", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDyeingSubContPartyRate.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3576": // "RPT-3576" Party Wise Bill Statement (S/C Out-house)
                    vReportTitle = "Party Wise Bill Statement (S/C Out-house)";

                    ds = ob.ScoPartyWiseBillStatement();

                    rds[0] = new ReportDataSource("dsSciPartyWiseBillStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoPartyWiseBillStatement.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3578": // "RPT-3578" Party Wise Summery (S/C In-house)
                    vReportTitle = "Party Wise Summery (S/C In-house)";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.SciPartyWiseSummery();

                    rds[0] = new ReportDataSource("dsSciPartyWiseSummery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptSciPartyWiseSummery.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-3579": // "RPT-3579" Loose Yarn Statement (S/C Out-house)
                    vReportTitle = "Loose Yarn Statement (S/C Out-house)";
                    vReportTitle1 = "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.ScoLooseYarnStatement();

                    rds[0] = new ReportDataSource("dsScoLooseYarnStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoLooseYarnStatement.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-3580": // "RPT-3580" Loose Yarn Statement (S/C Out-house)
                    ds = ob.NeedleReqTypeSummary();
                    rds[0] = new ReportDataSource("dsNeedleReqTypeSummary", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptNeedleReqTypeSummary.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-3581": // "RPT-3581" Comparative Statement
                    ds = ob.ComparativeStatement();
                    rds[0] = new ReportDataSource("dsComparativeStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptComparativeStatement.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;


                case "RPT-3504": // "RPT-3504" Yarn Current Stock Report
                    vReportTitle = "Yarn Current Stock";
                    ds = ob.KntYrnCurrentStock();
                    rds[0] = new ReportDataSource("dsKntYarnCurrentStock", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntYarnCurrentStock.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-3584": // "RPT-3584" Excess Yarn Usage Statement
                    vReportTitle = "Excess Yarn Usage Statement [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    ds = ob.ExcessYrnUsageStatement();

                    rds[0] = new ReportDataSource("ExcessYrnUsageState01", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("ExcessYrnUsageState02", ds.Tables[1]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptExcessYrnUsageState.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-3585": // "RPT-3585" S/C Pre-Treatment Ledger
                    vReportTitle = "S/C Pre-Treatment Ledger";
                    ds = ob.ScPreTreatmentLedger();
                    rds[0] = new ReportDataSource("dsScPtLedger", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScPtLedger.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-3586": // "RPT-3586" Advance Yarn Report
                    vReportTitle = "Advance Yarn Report";
                    ds = ob.YarnAdvanceStatement();
                    rds[0] = new ReportDataSource("dsYarnAdvanceStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYarnAdvanceStatement.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-3587": // "RPT-3587" Advance Yarn Report
                    vReportTitle = "Lot wise Yarn Statement Report";
                    ds = ob.LotWiseYarnStatement();
                    rds[0] = new ReportDataSource("dsLotWiseYarnStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptLotWiseYarnStatement.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });
                    break;

                case "RPT-3589": // "RPT-3589" Party wise Stock Summery (S/C In-house)

                    vReportTitle = "Party wise Stock Summery (S/C In-house) [Store: " + (ob.SCM_STORE_ID > 0 ? ob.STORE_NAME_EN + "]" : "All]"); // ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    ds = ob.SciPartyWiseStkSummery();

                    rds[0] = new ReportDataSource("SciPartyWiseStkSummery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKntSciPartyWiseStkSummery.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3590": // "RPT-3590" Order and Color wise Production Status (S/C Out-house)
                    if (ob.FROM_DATE != null)
                        vReportTitle = "Order and Color wise Production Status (S/C Out-house) [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    else
                        vReportTitle = "Order and Color wise Production Status (S/C Out-house)";

                    vReportTitle1 = (ob.JOB_CRD_NO != null) ? "Knit Card# " + ob.JOB_CRD_NO : "";
                    ds = ob.ScoPartyOrdColWiseRcvRegister();

                    rds[0] = new ReportDataSource("ScoPartyOrdColWiseRcvRegister", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoPartyOrdColWiseRcvRegister.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });
                    break;

                case "RPT-3591": // "RPT-3591" Color wise Yarn Lot Alocation and lot wise Knitting Production
                    vReportTitle = "Color wise Yarn Lot Alocation and lot wise Knitting Production";

                    ds = ob.OrdColFinDiaWiseYrnAllocProd();

                    rds[0] = new ReportDataSource("OrdColFinDiaWiseYrnAllocProd", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdColFinDiaWiseYrnAllocProd.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3592": // "RPT-3592" Party Wise Yarn Issue Challan
                    vReportTitle = "Party Wise Yarn Issue Challan";

                    ds = ob.PartyWiseYarnIssueChallan();

                    rds[0] = new ReportDataSource("dsPartyWiseYarnIssueChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptPartyWiseYarnIssueChallan.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.EnableExternalImages = true;
                    FilePath = @"file:\" + HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/COMPANY_PAD/" + (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() : "MFL") + ".JPG"); //Application.StartupPath is for WinForms, you should try AppDomain.CurrentDomain.BaseDirectory  for .net
                    p[3] = new ReportParameter("CompanyImg", FilePath);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;

                case "RPT-3593": // "RPT-3593" Order and Color wise Summery
                    vReportTitle = "Order and Color wise Summery";

                    ds = ob.OrdColWiseSummery();

                    rds[0] = new ReportDataSource("OrdColWiseSummery", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdColWiseSummery.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;


                case "RPT-3594": // "RPT-3594" Daily Return Challan for Grey QC Deduction Fabric
                    vReportTitle = "Daily Return Challan for Grey QC Deduction Fabric";

                    ds = ob.GreyQcDeductFabric();

                    rds[0] = new ReportDataSource("dsGreyQcDeductFabric", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptGreyQcDeductFabric.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    //p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-3595": // "RPT-3595" Delivery Challan for Yarn Return to Supplier
                    vReportTitle = "Delivery Challan for Yarn Return to Supplier";
                    ds = ob.DeliveryChallanForYarnReturnToSupplier();
                    rds[0] = new ReportDataSource("dsPartyWiseYarnIssueChallan", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    //dsDeliveryChallanForYarnReturnToSupplier

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDeliveryChallanForYarnReturnToSupplier.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.EnableExternalImages = true;
                    FilePath = @"file:\" + HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/COMPANY_PAD/" + (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() != null ? ds.Tables[0].Rows[0]["COMP_SNAME"].ToString() : "MFL") + ".JPG"); //Application.StartupPath is for WinForms, you should try AppDomain.CurrentDomain.BaseDirectory  for .net
                    p[3] = new ReportParameter("CompanyImg", FilePath);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-3596": // "RPT-3596" Party Wise Yarn Issue Statement
                    vReportTitle = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    ds = ob.PartyWiseYrnIssStatement();

                    rds[0] = new ReportDataSource("dsKntYrnIssStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptPartyWiseYrnIssStatement.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-3597": // "RPT-3597" Yarn Issue Return Statement
                    vReportTitle = "From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    ds = ob.DateWiseYrnIssReturnStatement();
                    rds[0] = new ReportDataSource("dsYrnIssReturnStatement", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptYrnIssReturnStatement.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("vReportTitle", vReportTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });

                    break;
                case "RPT-3598": // "RPT-3598" Knit M/C Oil Store Transfer Requisition
                    ds = ob.KnitMcOilStoreTransferRequisition();
                    rds[0] = new ReportDataSource("dsKnitMcOilStoreTransfer", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptKnitMcOilStoreTransfer.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", vProductTitle);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2] });

                    break;

                case "RPT-3599": // "RPT-3599" Collar/Cuff Challan Receive (S/C Out-house)
                    vReportTitle = "Collar/Cuff Challan Receive (S/C Out-house)";

                    ds = ob.ScoClcfRecvChaln();

                    rds[0] = new ReportDataSource("ScoClcfRecvChaln", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptScoClcfRecvChaln.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3600": // "RPT-3600" Order and Color wise Roll Available in Grey Store (S/C In-house)
                    vReportTitle = "Order and Color wise Roll Available in Grey Store (S/C In-house)" + System.Environment.NewLine + "[Store: " + (ob.SCM_STORE_ID > 0 ? ob.STORE_NAME_EN + "]" : "All]");

                    ds = ob.SciRollAvailableGreyStore();

                    rds[0] = new ReportDataSource("SciRollAvailableGreyStore", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptSciRollAvailableGreyStore.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3601": // "RPT-3601" Roll List Without Grey Qc
                    vReportTitle = "Roll List Without Grey Qc";

                    ds = ob.RollListWithoutGreyQc();

                    rds[0] = new ReportDataSource("dsGreyQcDeductFabric", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptRollListWithoutGreyQc.rdlc");

                    p[0] = new ReportParameter("CompanyName", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["COMP_NAME_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["REG_ADD_EN"].ToString() : HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());

                    p[2] = new ReportParameter("vProductTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4] });

                    break;

                case "RPT-3602": // "RPT-3602" Order and Color wise Grey Fabric Stock
                    vReportTitle = "Order and Color wise Grey Fabric Stock" + System.Environment.NewLine + "[Store: " + (ob.SCM_STORE_ID > 0 ? ob.STORE_NAME_EN + "]" : "All]");

                    ds = ob.OrdColWiseGreyFabStk();

                    rds[0] = new ReportDataSource("OrdColWiseGreyFabStk", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdColWiseGreyFabStk.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3603": // "RPT-3603" Daily Order and Color wise Knitting Production
                    vReportTitle = "Daily Order and Color wise Knitting Production" + System.Environment.NewLine + "[Date: " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.DailyOrdColWiseKntProd();

                    rds[0] = new ReportDataSource("DailyOrdColWiseKntProd", ds.Tables["DAILY_ORD_COL_KNT_PROD"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rds[1] = new ReportDataSource("DailyOrdColWiseKntProd1", ds.Tables["DAILY_ORD_COL_KNT_PROD_SUMRY"]);
                    rvContainer.LocalReport.DataSources.Add(rds[1]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptDailyOrdColWiseKntProd.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3604": // "RPT-3604" Order and Color wise Grey Fabric Transfer Statement
                    vReportTitle = "Order and Color wise Grey Fabric Transfer Statement"; //+ System.Environment.NewLine + "[Date: " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.OrdColWiseGfabTransStatement();

                    rds[0] = new ReportDataSource("OrdColWiseGfabTransStatement", ds.Tables["ORD_COL_GFAB_TRANS_STATE"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdColWiseGfabTransStatement.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3605": // "RPT-3605" Order and Color wise Fabric Delivery Status
                    vReportTitle = "Order and Color wise Fabric Delivery Status" + System.Environment.NewLine + "[" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.OrdColWiseFabDelivStatus();

                    rds[0] = new ReportDataSource("OrdColWiseFabDelivStatus", ds.Tables["ORD_COL_WISE_FAB_DELIV_STATUS"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdColWiseFabDelivStatus.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3606": // "RPT-3606" Grey Fabric Stock by Dia and Yarn
                    vReportTitle = "Grey Fabric Stock by Dia and Yarn" + System.Environment.NewLine + "[Store: " + (ob.SCM_STORE_ID > 0 ? ob.STORE_NAME_EN + "]" : "All]");

                    ds = ob.OrdColWiseGreyFabStkByDiaYarn();

                    rds[0] = new ReportDataSource("OrdColWiseGreyFabStk", ds.Tables["ORD_COL_GREY_STK"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptOrdColWiseGreyFabStkByDiaYarn.rdlc");

                    p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-3607": // "RPT-3607" Shift Wise Grey Fabric Roll Inspection
                    vReportTitle = "Shift wise Grey Fabric Roll Inspection";

                    ds = ob.ShiftWiseGreyFabricRollInspection();
                    rds[0] = new ReportDataSource("dsGrayFabricInspection", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptGrayFabricInspection.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vProductTitle", ob.KNT_QC_STS_TYPE_ID == null ? "Grey Fabric Roll Inspection Report" : ob.KNT_QC_STS_TYPE_ID == 1 ? "Passed Grey Fabric Roll Inspection Report" : ob.KNT_QC_STS_TYPE_ID == 3 ? "Hold Grey Fabric Roll Inspection Report" : "Rejected Grey Fabric Roll Inspection Report");
                    //p[3] = new ReportParameter("vReportTitle", vProductTitle);
                    p[3] = new ReportParameter("FromDate", ob.FROM_DATE == null ? null : ob.FROM_DATE.ToString());
                    p[4] = new ReportParameter("ToDate", ob.TO_DATE == null ? null : ob.TO_DATE.ToString());

                    p[5] = new ReportParameter("StyleOrder", ob.STYLE_NO);
                    p[6] = new ReportParameter("Color", ob.COLOR_NAME_EN);
                    p[7] = new ReportParameter("StatusType", ob.QC_STS_TYP_NAME);
                    p[8] = new ReportParameter("Shift", ob.SCHEDULE_NAME_EN);
                    p[9] = new ReportParameter("PartyName", ob.SUP_TRD_NAME_EN);
                    p[10] = new ReportParameter("ProductionType", ob.KNT_SC_PRG_ISS_NAME);
                    p[11] = new ReportParameter("FabType", ob.FAB_TYPE_NAME);
                    p[12] = new ReportParameter("RollNo", ob.FAB_ROLL_NO);
                    p[13] = new ReportParameter("BuyerName", ob.BYR_ACC_GRP_NAME_EN);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5], p[6], p[7], p[8], p[9], p[10], p[11], p[12], p[13] });
                    break;

                case "RPT-3608": // "RPT-3608" Sample Grey Fabric Receive and Delivery Status
                    vReportTitle = "Sample Grey Fabric Receive and Delivery Status";

                    ds = ob.SampleGreyFabricReceiveDeliveryStatus();

                    rds[0] = new ReportDataSource("dsSampleGreyFabricReceiveDeliveryStatus", ds.Tables[0]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/Areas/Knitting/Reports/rptSampleGreyFabricReceiveDeliveryStatus.rdlc");

                    p[0] = new ReportParameter("CompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    p[1] = new ReportParameter("CompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vReportTitle1", vReportTitle1);

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

    }
}