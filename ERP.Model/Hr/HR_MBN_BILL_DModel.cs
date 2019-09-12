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
    public class HR_MBN_BILL_DModel
    {
        public Int64 HR_MBN_BILL_D_ID { get; set; }
        public Int64 HR_MBN_BILL_H_ID { get; set; }
        public string MBN_BILL_NO { get; set; }
        public Int64 PHASE_NO { get; set; }
        public DateTime NOTICE_DT { get; set; }
        public DateTime PAYMENT_DT { get; set; }
        public Decimal PAY_AMT { get; set; }
        public Decimal OT_RATE { get; set; }
        public Decimal LAST_GROSS_SALARY { get; set; }
        public Decimal LAST_BASIC_SALARY { get; set; }
        public Boolean IS_EDIT_ENABLED { get; set; }

        public string MbnBillPhaseUpdate()
        {
            const string sp = "pkg_leave.hr_mbn_bill_d_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_MBN_BILL_D_ID", Value = ob.HR_MBN_BILL_D_ID},
                     new CommandParameter() {ParameterName = "pHR_MBN_BILL_H_ID", Value = ob.HR_MBN_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pMBN_BILL_NO", Value = ob.MBN_BILL_NO},
                     new CommandParameter() {ParameterName = "pPHASE_NO", Value = ob.PHASE_NO},
                     new CommandParameter() {ParameterName = "pNOTICE_DT", Value = ob.NOTICE_DT},
                     new CommandParameter() {ParameterName = "pPAYMENT_DT", Value = ob.PAYMENT_DT},
                     new CommandParameter() {ParameterName = "pPAY_AMT", Value = ob.PAY_AMT},
                     new CommandParameter() {ParameterName = "pOT_RATE", Value = ob.OT_RATE},
                     new CommandParameter() {ParameterName = "pLAST_GROSS_SALARY", Value = ob.LAST_GROSS_SALARY},
                     new CommandParameter() {ParameterName = "pLAST_BASIC_SALARY", Value = ob.LAST_BASIC_SALARY},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 2000},
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



        

        public HR_MBN_BILL_DModel Select(long ID)
        {
            string sp = "Select_HR_MBN_BILL_D";
            try
            {
                var ob = new HR_MBN_BILL_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_MBN_BILL_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_MBN_BILL_D_ID = (dr["HR_MBN_BILL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MBN_BILL_D_ID"]);
                    ob.HR_MBN_BILL_H_ID = (dr["HR_MBN_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MBN_BILL_H_ID"]);
                    ob.MBN_BILL_NO = (dr["MBN_BILL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MBN_BILL_NO"]);
                    ob.PHASE_NO = (dr["PHASE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PHASE_NO"]);
                    ob.NOTICE_DT = (dr["NOTICE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NOTICE_DT"]);
                    ob.PAYMENT_DT = (dr["PAYMENT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PAYMENT_DT"]);
                    ob.PAY_AMT = (dr["PAY_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_AMT"]);
                    ob.OT_RATE = (dr["OT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["OT_RATE"]);
                    ob.LAST_GROSS_SALARY = (dr["LAST_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_GROSS_SALARY"]);
                    ob.LAST_BASIC_SALARY = (dr["LAST_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_BASIC_SALARY"]);                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}