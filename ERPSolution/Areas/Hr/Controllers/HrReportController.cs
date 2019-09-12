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


//using Microsoft.Office.Interop.Word;
//using Microsoft.Office.Core;
//using System.Reflection;
//using Word = Microsoft.Office.Interop.Word;
//using System.IO;
//using System.Diagnostics;
//using System.Drawing;




namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrReportController : BaseController
    {
                
        [HttpPost]
        public void PreviewReport(HrReportModel ob)
        {            
            string vReportCode = ob.REPORT_CODE; //Request.Form["pREPORT_CODE"];
            //vReportCode = vReportCode.Substring(0, vReportCode.Length - 1);

            String vFormDate;
            string vProductTitle = "MultiTex ERP";
            string vReportTitle = "";
            string vReportTitleShort = "";
            string vFloorLine = "";
            string vOtType = "";
            string pPRJ_SERVER_PATH = Server.MapPath("~/UPLOAD_DOCS");

            DataSet dsTempReport = new DataSet();
            ReportDocument rd = new ReportDocument();

            switch (vReportCode)
            {
                case "RPT-1000": // Employee Job Card Orginal
                    if(ob.IS_PREVIOUS_MONTH!=null && (ob.HR_EMPLOYEE_ID!=null || ob.HR_EMPLOYEE_ID!=0))
                    {
                        if (ob.IS_PREVIOUS_MONTH == "N")
                        {
                            ob.FROM_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            ob.TO_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                        }
                        else if (ob.IS_PREVIOUS_MONTH == "Y")
                        {
                            ob.FROM_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                            ob.TO_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
                        }
                    }
                    else if (ob.IS_PREVIOUS_MONTH != null && (ob.HR_EMPLOYEE_ID == null || ob.HR_EMPLOYEE_ID == 0))
                    {
                        goto lastStep;
                    }


                    vReportTitle = "JOBCARD REPORT FROM " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " TO " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + (ob.HR_SHIFT_TEAM_ID>0?"":"");

                    dsTempReport = ob.JobCard();
                    //dsTempReport.WriteXml("d:\\JobCard.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmployeeJobCardOrg.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    //if (ob.HR_COMPANY_ID != null)
                    //{
                    //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + ob.COMP_NAME_EN + "'";
                    //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    //}
                    //else
                    //{
                    //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    //}
                    break;

                case "RPT-1001":
                    vReportTitle = "Daily Attendance & Allocation By Floor & Line as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.DailyAttendanceSummery();
                    //dsTempReport.WriteXml("d:\\DailyAttendanceSummery.xml", XmlWriteMode.WriteSchema);

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyAttendanceFloorWiseSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1002":
                    vReportTitle = "Daily Attendance as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.DailyAttendance();
                    //dsTempReport.WriteXml("d:\\DailyAttendance.xml", XmlWriteMode.WriteSchema);

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyAttendance.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1003": //Daily Attendance Report (By Section)
                    vReportTitle = "Daily Attendance (By Section) as On " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " [Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO + "]";
                    dsTempReport = ob.AttenBySection();
                    //dsTempReport.WriteXml("d:\\AttenBySection.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptAttenBySection.rpt"));

                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1004":
                    vReportTitle = "Daily Attendance as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.DailyAttendance();
                    //dsTempReport.WriteXml("d:\\DailyAttendance.xml", XmlWriteMode.WriteSchema);

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyAttendanceByTeam.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1005": //Loan/Salary Advance Application
                    vReportTitle = "Loan/Salary Advance Application";

                    dsTempReport = ob.SalaryAdvanceApplication();
                    //dsTempReport.WriteXml("d:\\rptSalaryAdvanceApplication.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryAdvanceApplication1.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrGroupNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1006":
                    vReportTitle = "";

                    dsTempReport = ob.MaternityLeaveData();
                    //dsTempReport.WriteXml("d:\\rptMaternityLeaveApplication.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptMaternityLeaveApplication.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameB"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressB"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1007":
                    vReportTitle = "";

                    dsTempReport = ob.OffdayLeaveApplicationData();
                    //dsTempReport.WriteXml("d:\\rptOffdayLeaveApplication.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptOffdayLeaveApplication.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameB"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressB"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;
                case "RPT-1008":
                    vReportTitle = "Leave Application";
                    //Daily
                    dsTempReport = ob.LeaveApplication();
                    //dsTempReport.WriteXml("d:\\rptOffdayLeaveApplication.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptLeaveApplication.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameB"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressB"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;
                case "RPT-1009":
                    vReportTitle = "Leave Application";
                    //Hourly
                    dsTempReport = ob.LeaveApplicationHr();
                    //dsTempReport.WriteXml("d:\\rptOffdayLeaveApplicationHr.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptLeaveApplicationHr.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameB"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressB"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        rd.DataDefinition.FormulaFields["vPrjServerPath"].Text = "'" + pPRJ_SERVER_PATH + "'";

                        rd.DataDefinition.FormulaFields["vCompanyID"].Text = "'" + Convert.ToString(dr["HR_COMPANY_ID"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1010":
                    vReportTitle = "Unassign Proxy ID List";

                    dsTempReport = ob.UnassignProxy();
                    //dsTempReport.WriteXml("d:\\UnassignProxy.xml", XmlWriteMode.WriteSchema);

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptUnassignProxy.rpt"));
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1011":

                    vReportTitle = "Online Leave Application";
                    dsTempReport = ob.onlineLeaveApplication();
                    //dsTempReport.WriteXml("d:\\onlineLeaveApplication.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptOnlineLeaveApplication.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameB"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressB"].ToString() + "'";
                    break;

                case "RPT-1012":
                    vReportTitle = "Daily Floor Wise Attendance Summary as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"); ;
                    dsTempReport = ob.DailyFloorWiseAtten();
                    //dsTempReport.WriteXml("d:\\DailyFloorWiseAtten.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyFloorWiseAtten.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1013":  // Monthly Attendance Summary
                    vReportTitle = "Monthly Attendance Summary From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.MonthlyAttenSummery();
                    //dsTempReport.WriteXml("d:\\MonthlyAttenSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptMonthlyAttenSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1014":
                    vReportTitle = "OT Summary From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.OtSummery();
                    //dsTempReport.WriteXml("d:\\OtSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptOtSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1015":
                    vReportTitle = "Leave Summary Sheet of " + ob.FY_NAME_EN;
                    dsTempReport = ob.LeaveSummarySheet();
                    //dsTempReport.WriteXml("d:\\LeaveSummarySheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptLeaveSummarySheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-1016":
                    vReportTitle = "Leave Detail Report";
                    dsTempReport = ob.LeaveDetailReport();
                    //dsTempReport.WriteXml("d:\\LeaveDetailReport.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptLeaveDetailReport.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-1017": // Tiffin Bill Report
                    vReportTitle = "Tiffin Bill From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                       ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.TiffinBill();
                    //dsTempReport.WriteXml("d:\\TIFFIN_BILL.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptTiffinBill.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1018": // Night Bill Report
                    vReportTitle = "Night Bill From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.NigthBill();
                    //dsTempReport.WriteXml("E:\\NightBill.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptNightBill.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";                    
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {                        
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1019": // OT Bill Report
                    vOtType="";

                    if(ob.HR_OT_TYPE_ID==2)
                    {
                        vOtType="Weekend ";
                    }
                    else if (ob.HR_OT_TYPE_ID == 3)
                    {
                        vOtType = "Holiday ";
                    }
                    else if (ob.HR_OT_TYPE_ID == 4)
                    {
                        vOtType = "Festival ";
                    }

                    vReportTitle = vOtType + "OT Bill From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.OvertimeBill();
                    //dsTempReport.WriteXml("d:\\OVERTIME_BILL.xml", XmlWriteMode.WriteSchema);

                    if (ob.HR_OT_TYPE_ID == 4)
                    {
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptFestivalOvertimeBill.rpt"));
                    }
                    else
                    {
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptOvertimeBill.rpt"));
                    }

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1020": // Salary Sheet Report
                    vFormDate = String.Format("{0:MMMM-yyyy}", ob.FROM_DATE);

                    vReportTitle = "Employee Salary/Worker`s Wages and Overtime for " + vFormDate;
                    vReportTitleShort = "Wages and OT for " + vFormDate;
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.SalarySheet();
                    //dsTempReport.WriteXml("e:\\SalarySheet.xml", XmlWriteMode.WriteSchema);

                    if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 1)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetStaff.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 2)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetStaffCp.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 3)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetStaffCp_1.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 1)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetWorker.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 2)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetWorkerCp.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 3)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetWorkerCp_1.rpt"));
                    else
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetWorker.rpt"));

                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitleShort"].Text = "'" + vReportTitleShort + "'";
                    rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1021": // Salary Pay Slip Report
                    vReportTitle = "Pay Slip";
                    dsTempReport = ob.PaySlip();
                    //dsTempReport.WriteXml("e:\\PaySlip.xml", XmlWriteMode.WriteSchema); 
                    
                    if (ob.REPORT_TYPE_ID == 2)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlipCp.rpt"));
                    else if (ob.REPORT_TYPE_ID == 3)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlipCp1.rpt"));                   
                    else
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlip.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1022": // Daily Attendance & Allocation Summary Report
                    vReportTitle = "Daily Attendance & Allocation Summary By Floor as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.DailyAttenAllocSummery();
                    //dsTempReport.WriteXml("d:\\DailyAttenAllocSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyAttenAllocSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1023": // Partial Salary Report
                    vReportTitle = "Partial Salary From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.PartialSalary();
                    //dsTempReport.WriteXml("d:\\PartialSalary.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPartialSalary.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1024": // Bonus Sheet Report
                    vReportTitle = "Employee/Worker`s Bonus Sheet For " + ob.BONUS_TYPE_NAME + " as on " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.BonusSheet();
                    //dsTempReport.WriteXml("d:\\BonusSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptBonusSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1025": // Salary Top Sheet By Section
                    vReportTitle = "Employee/Worker`s Wages and Overtime Top Sheet For " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.SalaryTopSheetBySection();
                    //dsTempReport.WriteXml("d:\\SalaryTopSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryTopSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1026": // ID Card Report
                    vReportTitle = "MULTIFABS LIMITED";                    
                    dsTempReport = ob.IDCardPrint(pPRJ_SERVER_PATH);
                    //dsTempReport.WriteXml("e:\\IDCardPrint.xml", XmlWriteMode.WriteSchema);

                    if(ob.IS_SINGLE_PAGE=="Y" && ob.CARD_PRINT_OPTION_ID==1)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmpIDCardFrontBangla.rpt"));

                    else if (ob.IS_SINGLE_PAGE == "Y" && ob.CARD_PRINT_OPTION_ID == 2)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmpIDCardFront.rpt"));
                    else
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmpIDCardFrontMultiEmp.rpt"));

                    //rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    //foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    //{
                    //    //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                    //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    //}
                    break;

                case "RPT-1027": // Shifting Daily Attendenca Summary
                    vReportTitle = "Daily Attendenca Summary (Textile) as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.TextileDailyAttenSummery();
                    //dsTempReport.WriteXml("d:\\TextileDailyAttenSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptTextileDailyAttenSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1028": // Daily Estimate Manpower
                    vReportTitle = "Daily Estimate Manpower as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.DailyEstimateManpower();
                    //dsTempReport.WriteXml("d:\\DailyEstimateManpower.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyEstimateManpower.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1029": // Night Bill Top Sheet
                    vReportTitle = "Night Bill Top Sheet From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.NightBillTopSheet();
                    //dsTempReport.WriteXml("d:\\NightBillTopSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptNightBillTopSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1030": // Bonus Pay Slip
                    vReportTitle = "Pay Slip For " + ob.BONUS_TYPE_NAME + " as on " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.BonusPaySlip();
                    //dsTempReport.WriteXml("d:\\BonusPaySlip.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptBonusPaySlip.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1031": // Bonus Bank Statement
                    vReportTitle = "Employee/Worker`s Bonus Bank Statement for " + ob.BONUS_TYPE_NAME + " as on " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.BonusBankStatement();
                    //dsTempReport.WriteXml("d:\\BonusBankStatment.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptBonusBankStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1032": // Pay Slip Partial Salary
                    vReportTitle = "Partial Salary From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.PartialSalaryPaySlip();
                    //dsTempReport.WriteXml("d:\\PartialSalaryPaySlip.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPartialSalaryPaySlip.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1033": // Salary Bank Statement
                    vReportTitle = "Employee/Worker`s Salary/Wages Bank Statement for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.SalaryBankStatement();
                    //dsTempReport.WriteXml("d:\\SalaryBankStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryBankStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1034": // Salary Cash Statement
                    vReportTitle = "Employee/Worker`s Salary/Wages Cash Statement for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.SalaryBankStatement();
                    //dsTempReport.WriteXml("d:\\SalaryBankStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryCashStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1035": // Daily Movement
                    vReportTitle = "Daily Movement as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.DailyMovement();
                    //dsTempReport.WriteXml("d:\\DailyMovement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyMovement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1036": // LoanAdvStatement
                    vReportTitle = "Loan/Advance from Salary Statement";
                    dsTempReport = ob.LoanAdvStatement();
                    //dsTempReport.WriteXml("d:\\LoanAdvStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptLoanAdvStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-1037": // Absent Summary
                    vReportTitle = "Absent Summary From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.AbsentSummery();
                    //dsTempReport.WriteXml("e:\\AbsentSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptAbsentSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1038": // Employee List
                    if (ob.FROM_DATE != null && ob.TO_DATE != null)
                    {
                        vReportTitle = "Employee List Joining From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                            ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    }
                    else// if (ob.FROM_DATE != null && ob.TO_DATE != null)
                    {
                        vReportTitle = "Employee List";
                    }
                    dsTempReport = ob.EmployeeList();
                    //dsTempReport.WriteXml("d:\\EmployeeList.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmployeeList.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1039": // Monthly Lunch Summary
                    vReportTitle = "Monthly Lunch Movement Summary From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                            ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.MonthlyLunchSummery();
                    //dsTempReport.WriteXml("d:\\MonthlyLunchSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptMonthlyLunchSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1040": // Allocation Summary By Line Type
                    vReportTitle = "Manpower Allocation & Attendance Summary By Line Type as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.DailyAttenAllocSummeryByLine();
                    //dsTempReport.WriteXml("d:\\DailyAttenAllocSummeryByLine.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyAttenAllocSummeryByLine.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1041": // POS Memo
                    //vReportTitle = "Manpower Allocation & Attendance Summary By Line Type as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");
                    dsTempReport = ob.POSMemoPrint();
                    //dsTempReport.WriteXml("d:\\POSMemoPrint.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPOSMemoPrint.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    break;

                case "RPT-1042": // Employee Job Card - Dual Print
                    if (ob.IS_PREVIOUS_MONTH != null && (ob.HR_EMPLOYEE_ID != null || ob.HR_EMPLOYEE_ID != 0))
                    {
                        if (ob.IS_PREVIOUS_MONTH == "N")
                        {
                            ob.FROM_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            ob.TO_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                        }
                        else if (ob.IS_PREVIOUS_MONTH == "Y")
                        {
                            ob.FROM_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                            ob.TO_DATE = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
                        }
                    }
                    else if (ob.IS_PREVIOUS_MONTH != null && (ob.HR_EMPLOYEE_ID == null || ob.HR_EMPLOYEE_ID == 0))
                    {
                        goto lastStep;
                    }


                    vReportTitle = "JOBCARD REPORT FROM " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " TO " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + (ob.HR_SHIFT_TEAM_ID > 0 ? "" : "");

                    dsTempReport = ob.JobCard();
                    //dsTempReport.WriteXml("d:\\JobCard.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmployeeJobCard.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1043": // Incriment Proposal List
                    vReportTitle = "Incriment Proposal List [Memo #: " + ob.MEMO_NO + "]";
                    dsTempReport = ob.IncrProposalList();
                    //dsTempReport.WriteXml("d:\\IncrProposalList.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptIncrProposalList.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1044": // Other Bill Summery from Atten.
                    vReportTitle = "Other Bill Summery from Atten. [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " TO " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    dsTempReport = ob.OtherBillSummery();
                    //dsTempReport.WriteXml("d:\\OtherBillSummery.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptOtherBillSummery.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1045": // Employee Profile
                    vReportTitle = "EMPLOYEE PROFILE";                                       
                    dsTempReport = ob.EmployeeProfile(pPRJ_SERVER_PATH);
                    //dsTempReport.WriteXml("d:\\EmployeeProfile.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmployeeProfile.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1046": // EL Encashment Sheet
                    vReportTitle = "Earn Leave Encashment [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " TO " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.ElEncashmentSheet();
                    //dsTempReport.WriteXml("d:\\ElEncashmentSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptElEncashmentSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1047": // EL Encashment Pay Slip
                    vReportTitle = "Earn Leave Encashment [" + ob.FROM_DATE.Value.ToString("dd/MMM/yy") + " TO " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yy") + "]";
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.ElEncashmentSheet();
                    //dsTempReport.WriteXml("d:\\ElEncashmentSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptElEncashmentPaySlip.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1048": // EL Encashment Bank Statement
                    vReportTitle = "Employee/Worker`s EL Encashment Bank Statement for " + ob.FROM_DATE.Value.ToString("yyyy");;
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.ElEncashmentBankStatement();
                    //dsTempReport.WriteXml("d:\\ElEncashmentBankStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptElEncashmentBankStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1049": // Daily Operator/Helper Status
                    if (ob.FROM_DATE == null)
                    {
                        ob.FROM_DATE = DateTime.Today;                        
                    }
                    ob.CORE_DEPT_ID = 60;

                    vReportTitle = "Daily Operator/Helper Status as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yy");
                    //vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.DailyOperatorHelperStatus();
                    //dsTempReport.WriteXml("e:\\DailyOperatorHelperStatus.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyOperatorHelperStatus.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1050": // Employee List By Disciplinary Action

                    vReportTitle = "Employee List By Disciplinary Action [" + ob.FROM_DATE.Value.ToString("dd/MMM/yy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yy") + "]";
                    //vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.EmpListByDiscpActType();
                    //dsTempReport.WriteXml("d:\\EmpListByDiscpActType.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmpListByDiscpActType.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1051": // 70% Salary Sheet Report for Staff
                    vFormDate = String.Format("{0:MMMM-yyyy}", ob.FROM_DATE);

                    vReportTitle = "Employee Salary/Worker`s Wages and Overtime for " + vFormDate;
                    vReportTitleShort = "Wages and OT for " + vFormDate;
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    dsTempReport = ob.SalarySheet();
                    //dsTempReport.WriteXml("d:\\SalarySheet.xml", XmlWriteMode.WriteSchema);

                    if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 1)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetStaffPart1Pct.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 2)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetStaffPart1Pct.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 3)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetStaffPart1Pct.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 1)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetWorker.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 2)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetWorkerCp.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 3)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetWorkerCp_1.rpt"));
                    else
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetStaffPart1Pct.rpt"));
                    
                    
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitleShort"].Text = "'" + vReportTitleShort + "'";
                    rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1052": // 70% Salary Pay Slip Report for Staff
                    vReportTitle = "Pay Slip";
                    dsTempReport = ob.PaySlip();
                    //dsTempReport.WriteXml("d:\\PaySlip.xml", XmlWriteMode.WriteSchema); 
                    
                    if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 1)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlipPart1Pct.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 2)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlipPart1Pct.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 2 && ob.REPORT_TYPE_ID == 3)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlipPart1Pct.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 1)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlip.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 2)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlipCp.rpt"));
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8 && ob.REPORT_TYPE_ID == 3)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlipCp.rpt"));
                    else
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPaySlipPart1Pct.rpt"));
                    

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1053": // 30% Salary Cash Statement for Staff
                    vReportTitle = "Employee/Worker`s Salary/Wages Cash Statement for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;

                    dsTempReport = ob.SalaryCashStatPart2Pct();
                    //dsTempReport.WriteXml("d:\\SalaryCashStatPart2Pct.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryCashStatementPart2Pct.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1054": // 70% Salary Bank Statement for Staff
                    vReportTitle = "Employee/Worker`s Salary/Wages Bank Statement for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.SalaryBankStatPart1Pct();
                    //dsTempReport.WriteXml("d:\\SalaryBankStatPart1Pct.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryBankStatPart1Pct.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1055": // Salary Loan/Advance Deduction Statemtnt
                    vReportTitle = "Salary Loan/Advance Deduction Statemtnt for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.SalaryLoanAdvDeductStatement();
                    //dsTempReport.WriteXml("d:\\SalaryLoanAdvDeductStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryLoanAdvDeductStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1056": // Increment Proposal List

                    if(ob.HR_MANAGEMENT_TYPE_ID==2)
                        vReportTitle = "Staff`s Increment Proposal List for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    else if (ob.HR_MANAGEMENT_TYPE_ID == 8)
                        vReportTitle = "Worker`s Increment Proposal List for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    else
                    vReportTitle = "Increment Proposal List for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");

                    dsTempReport = ob.IncrementProposalList();
                    //dsTempReport.WriteXml("d:\\IncrProposalList.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptIncrProposalList.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";// +" [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1057": // Increment Letter
                    vReportTitle = "Increment Letter for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");

                    dsTempReport = ob.IncrementLetter(pPRJ_SERVER_PATH);
                    //dsTempReport.WriteXml("d:\\IncrLetter.xml", XmlWriteMode.WriteSchema);

                    if (ob.HR_MANAGEMENT_TYPE_ID==8)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptIncrLetterWorker.rpt"));
                    else
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptIncrLetterStaff.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";// +" [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1058": // Salary Top Sheet 
                    vReportTitle = "Employee/Worker`s Wages and Overtime Top Sheet For " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.SalaryTopSheet01();
                    //dsTempReport.WriteXml("d:\\SalaryTopSheet01.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryTopSheet01.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1059": // Salary Analysis
                    vReportTitle = "Employee/Worker`s Salary Analysis";// For " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.SalaryAnalysis();
                    //dsTempReport.WriteXml("d:\\SalaryAnalysis.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryAnalysis.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1060": // Fairshop Daily Sales Report
                  
                    dsTempReport = ob.POSDailySales();

                    foreach (DataRow dr in dsTempReport.Tables["STORE_INFO"].Rows)
                    {
                        vReportTitle = "'" + Convert.ToString(dr["STORE_NAME_EN"]) + "'" + " + chr(10) + " + "'" + " Sales Report [" + ob.FROM_DATE.Value.ToString("dd/MMM/yy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yy") + "]" + "'";
 
                    }
                    
                    //dsTempReport.WriteXml("d:\\POSDailySales.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPOSDailySales.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "" + vReportTitle + "";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";

                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }

                    break;

                case "RPT-1062": // Increment Top Sheet
                    vReportTitle = "Increment Top Sheet for the month of " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.IncrTopSheet();
                    //dsTempReport.WriteXml("d:\\IncrTopSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptIncrTopSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1063": // Salary Deduction Sheet
                    
                    vReportTitle = "Salary Deduction Sheet for the month of " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;

                    dsTempReport = ob.SalarySheet();
                    //dsTempReport.WriteXml("d:\\SalarySheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryDeductionSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1064": // Increment Check Sheet
                    vReportTitle = "Increment Check Sheet for the month of " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    dsTempReport = ob.IncrCheckSheet();
                    //dsTempReport.WriteXml("d:\\IncrCheckSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptIncrCheckSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1065": // Maternity Benefit Bill
                    vReportTitle = "Maternity Benefit Bill";
                    dsTempReport = ob.MaternityBenefitBill();
                    //dsTempReport.WriteXml("e:\\MaternityBenefitBill.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptMaternityBenefitBill.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1066": // Bonus Top Sheet
                    vReportTitle = "Employee/Worker`s Bonus Top Sheet For " + ob.BONUS_TYPE_NAME + " as on " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;

                    dsTempReport = ob.BonusTopSheet();
                    //dsTempReport.WriteXml("d:\\BonusTopSheet.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptBonusTopSheet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1067": // Employee Tax Pay Statment
                    vReportTitle = "Employee Tax Pay Statment From " + ob.FROM_DATE.Value.ToString("MMMM,yyyy") + " To " + ob.TO_DATE.Value.ToString("MMMM,yyyy");
                    
                    dsTempReport = ob.EmpTaxPayStatement();
                    //dsTempReport.WriteXml("d:\\EmpTaxPayStatement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmpTaxPayStatement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1068": // A/C Head wise Salary Deduction

                    vReportTitle = "A/C Head wise Salary Deduction for the month of " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;

                    dsTempReport = ob.AcHeadWiseSalaryDeduct();
                    //dsTempReport.WriteXml("d:\\AcHeadWiseSalaryDeduct.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptAcHeadWiseSalaryDeduct.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1069": // Salary Top Sheet By Section (For Cash)

                    vReportTitle = "Statement of Salary/Wages and OT for the month of " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;

                    dsTempReport = ob.SalaryTopSheetBySection4Cash(pPRJ_SERVER_PATH);
                    //dsTempReport.WriteXml("d:\\SalaryTopSheetBySection4Cash.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptSalaryTopSheetBySection4Cash.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1070": // Fairshop Sales Summery

                    dsTempReport = ob.POSSalesSummeryDateWise();
                    //dsTempReport.WriteXml("d:\\POSSalesSummeryDateWise.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptPOSSalesSummeryDateWise.rpt"));


                    foreach (DataRow dr in dsTempReport.Tables["STORE_INFO"].Rows)
                    {
                        vReportTitle = "'" + Convert.ToString(dr["STORE_NAME_EN"]) + "'" + " + chr(10) + " + "'" + " Sales Summery [" + ob.FROM_DATE.Value.ToString("dd/MMM/yy") + " To " +
                            ob.TO_DATE.Value.ToString("dd/MMM/yy") + "] [Item Category: " + (ob.INV_ITEM_CAT_ID == null ? "All" : ob.ITEM_CAT_NAME_EN) + "]'";

                    }
                    //string vStrSQL = ((char)34).ToString(); 
                    //vStrSQL= Chr(34) & gvCompanyName & Chr(34) & " & Chr(10) & " & Chr(34) & "JOURNAL VOUCHER" & Chr(34) & " & Chr(10) & " & Chr(34) & "Date: """ & Chr(34) & Date.Today.ToString("dd-MMM-yyyy") & Chr(34)

                    //vReportTitle = "'" + "Test" + "'" + " + chr(10) + " + "'" + "Hello" + "'";

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "" + vReportTitle + "";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }

                    break;

                case "RPT-1072": // Terminal Wise Movement
                    vReportTitle = "Terminal Wise Movement [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    dsTempReport = ob.TerminalWiseMovement();
                    //dsTempReport.WriteXml("d:\\TerminalWiseMovement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptTerminalWiseMovement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1075": // Extra OT Bill Report
                    vOtType = "";

                    vReportTitle = vOtType + "Extra OT Bill From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO + "[" + ob.OT_HR_TIME + "]";
                    dsTempReport = ob.ExtraOvertimeBill4Cp();
                    //dsTempReport.WriteXml("e:\\OVERTIME_BILL.xml", XmlWriteMode.WriteSchema);

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptExtraOvertimeBill4Cp.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    rd.DataDefinition.FormulaFields["vFloorLine"].Text = "'" + vFloorLine + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1077": // Increment Letter for New Gejet
                    vReportTitle = "Increment Letter for " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");

                    dsTempReport = ob.IncrementLetter(pPRJ_SERVER_PATH);
                    //dsTempReport.WriteXml("e:\\IncrLetter.xml", XmlWriteMode.WriteSchema);

                    if (ob.HR_MANAGEMENT_TYPE_ID == 8)
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptIncrLetterWorker4NewGejet.rpt"));
                    else
                        rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptIncrLetterWorker4NewGejet.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + "'";// +" [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1078": // Floor Wise Movement
                    vReportTitle = "Floor Movement [" + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";
                    dsTempReport = ob.FloorWiseMovement();
                    //dsTempReport.WriteXml("d:\\FloorWiseMovement.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptFloorWiseMovement.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

                case "RPT-1079": // Employee Job Card by Virtual OT
                    
                    vReportTitle = "JOBCARD REPORT FROM " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " TO " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + (ob.HR_SHIFT_TEAM_ID > 0 ? "" : "");

                    dsTempReport = ob.JobCardByVirtualOT();
                    //dsTempReport.WriteXml("d:\\JobCard.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmployeeJobCardOrg.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";

                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }                    
                    break;

                case "RPT-1082": // Employee List - General
                    if (ob.FROM_DATE != null && ob.TO_DATE != null)
                    {
                        vReportTitle = "Employee List - General Joining From " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " +
                            ob.TO_DATE.Value.ToString("dd/MMM/yyyy");
                    }
                    else// if (ob.FROM_DATE != null && ob.TO_DATE != null)
                    {
                        vReportTitle = "Employee List - General";
                    }
                    dsTempReport = ob.EmployeeList();
                    //dsTempReport.WriteXml("d:\\EmployeeList.xml", XmlWriteMode.WriteSchema);
                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmployeeListGeneral.rpt"));

                    rd.DataDefinition.FormulaFields["vProductTitle"].Text = "'" + vProductTitle + "'";
                    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
                    //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
                    //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";
                    foreach (DataRow dr in dsTempReport.Tables["COMPANY_INFO"].Rows)
                    {
                        //ob.HR_GEO_DIVISION_ID = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                        rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]" + "'";
                        rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Convert.ToString(dr["ADDRESS_EN"]) + "'";
                    }
                    break;

            }

            //rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
            //rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameB"].ToString() + "'";
            //rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressB"].ToString() + "'";

            rd.SetDataSource(dsTempReport);

            if (ob.IS_EXCEL_FORMAT == "Y")
            {                
                //rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "");
                rd.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, System.Web.HttpContext.Current.Response, false, "MultiTex_"+ob.REPORT_CODE+'_'+DateTime.Now.ToString("yyyyMMMdd_HHmm"));
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
        public void PreviewReportRDLC(HrReportModel ob)
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
                case "RPT-1020": // Salary Sheet Report
                   
                    vReportTitle = "Employee Salary/Worker`s Wages and Overtime for " + ob.FROM_DATE.Value.ToString("MMMM-yyyy");
                    vReportTitle1 = "Wages and OT for " + ob.FROM_DATE.Value.ToString("MMMM-yyyy");
                    vFloorLine = "Floor: " + ob.FLOOR_NAME + ", Line: " + ob.LINE_NO;
                    
                    ds = ob.SalarySheet();
                    rds[0] = new ReportDataSource("SalarySheet", ds.Tables["SALARY_SHEET"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    if (ob.HR_MANAGEMENT_TYPE_ID==2)
                        rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetStaff.rdlc");
                    else
                        rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptSalarySheetWorker.rdlc");
                    
                    foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    {
                        p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                        p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));                        
                    }
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    p[4] = new ReportParameter("vFloorLine", vFloorLine);
                    p[5] = new ReportParameter("vReportTitle1", vReportTitle1);

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3], p[4], p[5] });
                    break;

                case "RPT-1061": // "RPT-1061" Salary Top Sheet (For Cash)
                    vReportTitle = "Salary Top Sheet (For Cash) Month of " + ob.FROM_DATE.Value.ToString("MMMM,yyyy");

                    ds = ob.SalaryTopSheet4Cash();
                    rds[0] = new ReportDataSource("SalaryTopSheet4Cash", ds.Tables["SALARY_TOP_SHEET4CASH"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptSalaryTopSheet4Cash.rdlc");

                    //p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    {
                        p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                        p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));                        
                    }
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-1071": // "RPT-1071" Bkash Payment

                    if (ob.HR_PAY_ELEMENT_ID==12)
                        vReportTitle = "Bkash Payment for Tiffin Bill";
                    else if (ob.HR_PAY_ELEMENT_ID == 13)
                    {
                        vReportTitle = "Bkash Payment for Night Bill";
                        //ds = ob.BkashData4Payment();
                    }
                    else if (ob.HR_PAY_ELEMENT_ID == 1)
                    {
                        vReportTitle = "Salary Payment for the month of " + String.Format("{0:MMMM-yyyy}", ob.FROM_DATE);
                        //ds = ob.BkashData4Payment();
                    }


                    ds = ob.BkashData4Payment();
                    
                    rds[0] = new ReportDataSource("BkashData4Pay", ds.Tables["BKASH_DATA4PAY"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptBkashData4Pay.rdlc");

                    //p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    {
                        p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                        p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));
                    }
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-1073": // "RPT-1073" Daily Out Punch wise Manpower
                    vReportTitle = "Daily Out Punch wise Manpower Summery for the date of " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                    ds = ob.DailyOutPunchWiseMP();
                    rds[0] = new ReportDataSource("DailyOutPunchWiseMP", ds.Tables["DAILY_OUTPUNCH_WISE_MP"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptDailyOutPunchWiseMP.rdlc");

                    //p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    {
                        p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                        p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));                        
                    }
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-1074": // "RPT-1074" Fairshop Advance Deduction Detail
                    vReportTitle = "Fairshop Advance Deduction Detail for the Month of " + String.Format("{0:MMMM-yyyy}", ob.FROM_DATE);

                    ds = ob.FairshopAdvDeducDtl();
                    rds[0] = new ReportDataSource("HrDs", ds.Tables["FS_ADV_DEDUC_DTL"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptFairshopAdvDeducDtl.rdlc");

                    //p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    {
                        p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                        p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));
                    }
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-1076": // "RPT-1076" Item wise Sales Summery
                    
                    ds = ob.POSItemWiseSalesSummery();

                    foreach (DataRow dr in ds.Tables["STORE_INFO"].Rows)
                    {
                        vReportTitle = Convert.ToString(dr["STORE_NAME_EN"]) + System.Environment.NewLine + " Item wise Sales Summery [" + ob.FROM_DATE.Value.ToString("dd/MMM/yy") + " To " +
                        ob.TO_DATE.Value.ToString("dd/MMM/yy") + "]";

                    }

                    rds[0] = new ReportDataSource("FS_ITEM_SALES_SUMMERY", ds.Tables["FS_ITEM_SALES_SUMMERY"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptPOSItemWiseSalesSummery.rdlc");

                    //p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    {
                        p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                        p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));
                    }
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-1080": // "RPT-1080" Employee List by Joining Date for BKMEA
                    vReportTitle = "Employee List" + System.Environment.NewLine + "[Joining from " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " To " + ob.TO_DATE.Value.ToString("dd/MMM/yyyy") + "]";

                    ds = ob.EmployeeList4BKMEA();
                    rds[0] = new ReportDataSource("EmployeeList4BKMEA", ds.Tables["EmployeeList4BKMEA"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptEmployeeList4BKMEA.rdlc");

                    //p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    {
                        p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                        p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));
                    }
                    p[2] = new ReportParameter("vReportTitle", vReportTitle);
                    p[3] = new ReportParameter("vProductTitle", vProductTitle);
                    //p[4] = new ReportParameter("vDelvDate", ob.FROM_DATE.Value.ToString("dd/MMM/yyyy"));

                    rvContainer.LocalReport.SetParameters(new ReportParameter[] { p[0], p[1], p[2], p[3] });
                    break;

                case "RPT-1081": // "RPT-1081" Employee List from Monthly Salary
                    vReportTitle = "Employee List from Monthly Salary [Month: " + ob.FROM_DATE.Value.ToString("MMMM-yyyy") + "]";

                    ds = ob.EmployeeList4MonthlySalary();
                    rds[0] = new ReportDataSource("EmployeeList4MonthlySalary", ds.Tables["EmployeeList4MonthlySalary"]);
                    rvContainer.LocalReport.DataSources.Add(rds[0]);

                    rvContainer.LocalReport.ReportPath = Server.MapPath("~/Areas/Hr/Reports/rptEmployeeList4MonthlySalary.rdlc");

                    //p[0] = new ReportParameter("vCompanyName", HttpContext.Current.Session["multiCurrCompanyNameE"].ToString());
                    //p[1] = new ReportParameter("vCompanyAddress", HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString());
                    foreach (DataRow dr in ds.Tables["COMPANY_INFO"].Rows)
                    {
                        p[0] = new ReportParameter("vCompanyName", Convert.ToString(dr["COMP_NAME_EN"]) + " [" + Convert.ToString(dr["OFFICE_NAME_EN"]) + "]");
                        p[1] = new ReportParameter("vCompanyAddress", Convert.ToString(dr["ADDRESS_EN"]));
                    }
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
            string x = "";
        }


        //public void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceText)
        //{ 
        //    object matchCase = true;
        //    object matchWholeWord = true;
        //    object matchWildCards = false;
        //    object matchSoundsLike = false;
        //    object matchAllWordForms = false;
        //    object forward = true;            
        //    object format = false;
        //    object matchKashida = false;
        //    object matchDiacritics = false;
        //    object matchAlefHamza = false;
        //    object matchControl = false;

        //    object read_only = false;
        //    object visable = true;
        //    object replace = 2;
        //    object wrap = 1;


        //    wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
        //        ref matchAllWordForms, ref forward, ref wrap, format, ref replaceText, ref replace, ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

        //}

        //public void CreateDocument(object fileName, object saveAs)
        //{
        //    object missing = Missing.Value;
        //    string tempPath=null;

        //    Word.Application wordApp = new Word.Application();
        //    Word.Document aDoc = null;

        //    if (System.IO.File.Exists((string)fileName))
        //    {
        //        object readOnly = false;
        //        object isVisable = false;

        //        wordApp.Visible = false;

        //        aDoc = wordApp.Documents.Open(ref fileName, ref missing, ref readOnly,
        //            ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing);
                

        //        aDoc.Activate();

        //        this.FindAndReplace(wordApp, "<name>", "Md. Maruf Al Farid");

        //   }
            
        //    aDoc.SaveAs2(ref saveAs, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            

        //    aDoc.Close(false, ref missing, ref missing);
        //    wordApp.Quit(ref missing, ref missing, ref missing);

        //}


        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult ReportParams()
        {
            return PartialView();
        }


        public JsonResult ReadTemplate()
        {
            var x = new { vMsg = "Just for test" };
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        


        // Start Temp Block =====================================================
        //[HttpPost]
        //public void Report(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_LEAVE_TYPE_ID, Int64 HR_EMPLOYEE_ID)
        //{

        //    DataSet dsTempReport = new DataSet();
        //    ReportDocument rd = new ReportDocument();

        //    dsTempReport = ob.LeaveApplication(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID, HR_EMPLOYEE_ID);
        //    //dsTempReport.WriteXml("d:\\LeaveApp.xml", XmlWriteMode.WriteSchema);


        //    string vReportTitle = "Leave Application";


        //    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptLeaveApplication.rpt"));

        //    rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
        //    rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
        //    rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";

        //    rd.SetDataSource(dsTempReport);
        //    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

        //}
    }
}