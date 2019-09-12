using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP.Model;

namespace ERPSolution
{
    public partial class CrystalReportViewer : System.Web.UI.Page    
    {              

        public void Page_Load(object sender, EventArgs e)
        {            
            //string x = HttpContext.Current.Request.QueryString[0].ToString();
            

            ReportDocument rd = new ReportDocument();
            HrReportModel ob = new HrReportModel();
            DataSet dsTempReport = new DataSet();

            string vReportTitle = "";

            ob.REPORT_CODE = Request.QueryString.Get("REPORT_CODE");
            if (Request.QueryString.Get("HR_COMPANY_ID") != null && Request.QueryString.Get("HR_COMPANY_ID") != string.Empty) { ob.HR_COMPANY_ID = Convert.ToInt64(Request.QueryString.Get("HR_COMPANY_ID")); }
            if (Request.QueryString.Get("HR_DEPARTMENT_ID") != null && Request.QueryString.Get("HR_DEPARTMENT_ID") != "") { ob.HR_DEPARTMENT_ID = Convert.ToInt64(Request.QueryString.Get("HR_DEPARTMENT_ID")); }
            if (Request.QueryString.Get("HR_MANAGEMENT_TYPE_ID") != null && Request.QueryString.Get("HR_MANAGEMENT_TYPE_ID") != "") { ob.HR_MANAGEMENT_TYPE_ID = Convert.ToInt64(Request.QueryString.Get("HR_MANAGEMENT_TYPE_ID")); }
            if (Request.QueryString.Get("HR_SHIFT_TEAM_ID") != null && Request.QueryString.Get("HR_SHIFT_TEAM_ID") != "") { ob.HR_SHIFT_TEAM_ID = Convert.ToInt64(Request.QueryString.Get("HR_SHIFT_TEAM_ID")); }
            if (Request.QueryString.Get("LK_FLOOR_ID") != null && Request.QueryString.Get("LK_FLOOR_ID") != "") { ob.LK_FLOOR_ID = Convert.ToInt64(Request.QueryString.Get("LK_FLOOR_ID")); }
            if (Request.QueryString.Get("LINE_NO") != null && Request.QueryString.Get("LINE_NO") != "") { ob.LINE_NO = Convert.ToInt64(Request.QueryString.Get("LINE_NO")); }
            if (Request.QueryString.Get("HR_EMPLOYEE_ID") != null && Request.QueryString.Get("HR_EMPLOYEE_ID") != "") { ob.HR_EMPLOYEE_ID = Convert.ToInt64(Request.QueryString.Get("HR_EMPLOYEE_ID")); }

            if (Request.QueryString.Get("FROM_DATE") != null) { ob.FROM_DATE = Convert.ToDateTime(Request.QueryString.Get("FROM_DATE")); }
            if (Request.QueryString.Get("TO_DATE") != null) { ob.TO_DATE = Convert.ToDateTime(Request.QueryString.Get("TO_DATE")); }

            switch (ob.REPORT_CODE)
            {
                case "RPT-1000":
                    vReportTitle = "JOBCARD REPORT FROM " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy") + " TO " +
                                   ob.TO_DATE.Value.ToString("dd/MMM/yyyy");

                    dsTempReport = ob.JobCard();
                    //dsTempReport.WriteXml("d:\\JobCard.xml", XmlWriteMode.WriteSchema);                    

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmployeeJobCard.rpt"));

                    break;

                case "RPT-1001":
                    vReportTitle = "Daily Attendance Summery (By Floor & Line) as on " + ob.FROM_DATE.Value.ToString("dd/MMM/yyyy");

                    //dsTempReport = ob.DailyAttendanceSummery(ob);
                    //dsTempReport.WriteXml("d:\\DailyAttendanceSummery.xml", XmlWriteMode.WriteSchema);

                    rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptDailyAttendanceFloorWiseSummery.rpt"));
                    break;

                case "LEAVE APPLICATION":
                    break;

            }

            rd.DataDefinition.FormulaFields["vReportTitle"].Text = "'" + vReportTitle + "'";
            rd.DataDefinition.FormulaFields["vCompany"].Text = "'" + Session["multiCurrCompanyNameE"].ToString() + "'";
            rd.DataDefinition.FormulaFields["vCompanyAddress"].Text = "'" + Session["multiCurrCompanyAddressE"].ToString() + "'";

            rd.SetDataSource(dsTempReport);

            CrystalReportViewer1.RefreshReport();
            CrystalReportViewer1.ReportSource = rd;
            CrystalReportViewer1.HasPrintButton = true;
            CrystalReportViewer1.HasRefreshButton = true;
            //CrystalReportViewer1.Visible = true;
            //CrystalReportViewer1.HasToggleGroupTreeButton=true;
            //CrystalReportViewer1.PrintMode = PrintMode.ActiveX;
            



            //CrystalReportViewer1.Visible = true;
            
            //dsTempReport.ReadXml(Server.MapPath("~/App_Data/rss.xml"));

            //ReportModel ob = new ReportModel();
            //ob.FROM_DATE = DateTime.Today.AddDays(-25);
            //ob.TO_DATE = DateTime.Today;

            //dsTempReport = obCommonBLL.GetData();
            //dsTempReport = ob.JobCard(ob);


            
            //rd.Load(Server.MapPath("~/Areas/Hr/Reports/CrystalReport1.rpt"));
            //rd.Load("D:\\Projects\\MultiTex\\ERPSolution\\ERPSolution\\Areas\\Hr\\Reports\\rptEmployeeJobCard.rpt");
            //rd.Load(Server.MapPath("~/Areas/Hr/Reports/rptEmployeeJobCard.rpt"));
            //rd.SetDataSource(dsTempReport.Tables[0]);

            //CrystalReportViewer1.RefreshReport();
            //CrystalReportViewer1.ReportSource = rd;
            //CrystalReportViewer1.Visible = true;
            //CrystalReportViewer1.RefreshReport();

        }
       

    }
}