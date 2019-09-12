using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class HR_EL_BILLModel
    {
        public Int64 HR_EL_BILL_ID { get; set; }
        public Int64 ACC_PAY_PERIOD_ID { get; set; }
        public Int64 EL_PAY_YEAR_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public Decimal LAST_GROSS_SALARY { get; set; }
        public Int64 DAYS_PRESENT { get; set; }
        public Int64 GROSS_EL_DAYS { get; set; }
        public Int64 EL_ENJOYED { get; set; }
        public Int64 YRLY_HOLI_DAYS { get; set; }
        public Int64 NET_EL_DAYS { get; set; }
        public Decimal EL_RATE { get; set; }
        public Decimal EL_AMT { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }



        public string Save()
        {
            const string sp = "SP_HR_EL_BILL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EL_BILL_ID", Value = ob.HR_EL_BILL_ID},
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     new CommandParameter() {ParameterName = "pEL_PAY_YEAR_ID", Value = ob.EL_PAY_YEAR_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},
                     new CommandParameter() {ParameterName = "pLAST_GROSS_SALARY", Value = ob.LAST_GROSS_SALARY},
                     new CommandParameter() {ParameterName = "pDAYS_PRESENT", Value = ob.DAYS_PRESENT},
                     new CommandParameter() {ParameterName = "pGROSS_EL_DAYS", Value = ob.GROSS_EL_DAYS},
                     new CommandParameter() {ParameterName = "pEL_ENJOYED", Value = ob.EL_ENJOYED},
                     new CommandParameter() {ParameterName = "pYRLY_HOLI_DAYS", Value = ob.YRLY_HOLI_DAYS},
                     new CommandParameter() {ParameterName = "pNET_EL_DAYS", Value = ob.NET_EL_DAYS},
                     new CommandParameter() {ParameterName = "pEL_RATE", Value = ob.EL_RATE},
                     new CommandParameter() {ParameterName = "pEL_AMT", Value = ob.EL_AMT},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Update()
        {
            const string sp = "SP_HR_EL_BILL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EL_BILL_ID", Value = ob.HR_EL_BILL_ID},
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     new CommandParameter() {ParameterName = "pEL_PAY_YEAR_ID", Value = ob.EL_PAY_YEAR_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},
                     new CommandParameter() {ParameterName = "pLAST_GROSS_SALARY", Value = ob.LAST_GROSS_SALARY},
                     new CommandParameter() {ParameterName = "pDAYS_PRESENT", Value = ob.DAYS_PRESENT},
                     new CommandParameter() {ParameterName = "pGROSS_EL_DAYS", Value = ob.GROSS_EL_DAYS},
                     new CommandParameter() {ParameterName = "pEL_ENJOYED", Value = ob.EL_ENJOYED},
                     new CommandParameter() {ParameterName = "pYRLY_HOLI_DAYS", Value = ob.YRLY_HOLI_DAYS},
                     new CommandParameter() {ParameterName = "pNET_EL_DAYS", Value = ob.NET_EL_DAYS},
                     new CommandParameter() {ParameterName = "pEL_RATE", Value = ob.EL_RATE},
                     new CommandParameter() {ParameterName = "pEL_AMT", Value = ob.EL_AMT},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Delete()
        {
            const string sp = "SP_HR_EL_BILL";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EL_BILL_ID", Value = ob.HR_EL_BILL_ID},
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     new CommandParameter() {ParameterName = "pEL_PAY_YEAR_ID", Value = ob.EL_PAY_YEAR_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},
                     new CommandParameter() {ParameterName = "pLAST_GROSS_SALARY", Value = ob.LAST_GROSS_SALARY},
                     new CommandParameter() {ParameterName = "pDAYS_PRESENT", Value = ob.DAYS_PRESENT},
                     new CommandParameter() {ParameterName = "pGROSS_EL_DAYS", Value = ob.GROSS_EL_DAYS},
                     new CommandParameter() {ParameterName = "pEL_ENJOYED", Value = ob.EL_ENJOYED},
                     new CommandParameter() {ParameterName = "pYRLY_HOLI_DAYS", Value = ob.YRLY_HOLI_DAYS},
                     new CommandParameter() {ParameterName = "pNET_EL_DAYS", Value = ob.NET_EL_DAYS},
                     new CommandParameter() {ParameterName = "pEL_RATE", Value = ob.EL_RATE},
                     new CommandParameter() {ParameterName = "pEL_AMT", Value = ob.EL_AMT},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_EL_BILLModel> SelectAll()
        {
            string sp = "Select_HR_EL_BILL";
            try
            {
                var obList = new List<HR_EL_BILLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EL_BILL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EL_BILLModel ob = new HR_EL_BILLModel();
                    ob.HR_EL_BILL_ID = (dr["HR_EL_BILL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EL_BILL_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.EL_PAY_YEAR_ID = (dr["EL_PAY_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EL_PAY_YEAR_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.LAST_GROSS_SALARY = (dr["LAST_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_GROSS_SALARY"]);
                    ob.DAYS_PRESENT = (dr["DAYS_PRESENT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_PRESENT"]);
                    ob.GROSS_EL_DAYS = (dr["GROSS_EL_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GROSS_EL_DAYS"]);
                    ob.EL_ENJOYED = (dr["EL_ENJOYED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EL_ENJOYED"]);
                    ob.YRLY_HOLI_DAYS = (dr["YRLY_HOLI_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRLY_HOLI_DAYS"]);
                    ob.NET_EL_DAYS = (dr["NET_EL_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NET_EL_DAYS"]);
                    ob.EL_RATE = (dr["EL_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["EL_RATE"]);
                    ob.EL_AMT = (dr["EL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["EL_AMT"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_EL_BILLModel Select(long ID)
        {
            string sp = "Select_HR_EL_BILL";
            try
            {
                var ob = new HR_EL_BILLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EL_BILL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_EL_BILL_ID = (dr["HR_EL_BILL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EL_BILL_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.EL_PAY_YEAR_ID = (dr["EL_PAY_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EL_PAY_YEAR_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.LAST_GROSS_SALARY = (dr["LAST_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_GROSS_SALARY"]);
                    ob.DAYS_PRESENT = (dr["DAYS_PRESENT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAYS_PRESENT"]);
                    ob.GROSS_EL_DAYS = (dr["GROSS_EL_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GROSS_EL_DAYS"]);
                    ob.EL_ENJOYED = (dr["EL_ENJOYED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EL_ENJOYED"]);
                    ob.YRLY_HOLI_DAYS = (dr["YRLY_HOLI_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRLY_HOLI_DAYS"]);
                    ob.NET_EL_DAYS = (dr["NET_EL_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NET_EL_DAYS"]);
                    ob.EL_RATE = (dr["EL_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["EL_RATE"]);
                    ob.EL_AMT = (dr["EL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["EL_AMT"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ElEncashmentProcess(Int64? pHR_COMPANY_ID, Int64? pACC_PAY_PERIOD_ID, Int64? pACC_PAY_MONTH_ID, Int64? pHR_DEPARTMENT_ID, Int64? pHR_DESIGNATION_ID,
            Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID, Int64? pEMPLOYEE_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pCORE_DEPT_ID,
            string pHR_DESIGNATION_GRP_IDS, DateTime? pJOINING_DT, decimal? pPAY_PCT)
        {
            const string sp = "pkg_ot.el_encashment_process";
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_MONTH_ID", Value = pACC_PAY_MONTH_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = pHR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = pHR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = pEMPLOYEE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = pLK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pPAY_PCT", Value = ((pPAY_PCT==null)?1:pPAY_PCT)},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = pLINE_NO},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},                    
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_IDS", Value = pHR_DESIGNATION_GRP_IDS}, 
                    new CommandParameter() {ParameterName = "pJOINING_DT", Value = pJOINING_DT},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 0000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }
    }
}