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
    public class HR_EMP_TRNFRModel
    {
        public Int64 HR_EMP_TRNFR_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public string TRNF_REF_NO { get; set; }
        public DateTime TRNF_REF_DT { get; set; }
        public DateTime EFFECT_DT { get; set; }
        public string P_EMPLOYEE_CODE { get; set; }
        public Int64 P_HR_COMPANY_ID { get; set; }
        public Int64 P_HR_OFFICE_ID { get; set; }
        public Int64 P_HR_DEPARTMENT_ID { get; set; }
        public Int64 P_HR_DESIGNATION_ID { get; set; }
        public Int64? P_HR_GRADE_ID { get; set; }
        public Int64? P_HR_PROD_FLR_ID { get; set; }
        public Int64? P_HR_PROD_LINE_ID { get; set; }
        public Decimal? P_BASIC_SALARY { get; set; }
        public Decimal? P_GROSS_SALARY { get; set; }
        public string N_EMPLOYEE_CODE { get; set; }
        public Int64 N_HR_COMPANY_ID { get; set; }
        public Int64 N_HR_OFFICE_ID { get; set; }
        public Int64 N_HR_DEPARTMENT_ID { get; set; }
        public Int64 N_HR_DESIGNATION_ID { get; set; }
        public Int64? N_HR_GRADE_ID { get; set; }
        public Int64? N_HR_PROD_FLR_ID { get; set; }
        public Int64? N_HR_PROD_LINE_ID { get; set; }
        public Decimal? N_BASIC_SALARY { get; set; }
        public Decimal? N_GROSS_SALARY { get; set; }
        public Decimal? LAST_GROSS_SALARY { get; set; }
        public Decimal? LAST_BASIC_SALARY { get; set; }
        public string IS_FINALIZED { get; set; }
        
        public Int64? P_LK_FLOOR_ID { get; set; }
        public Int64? P_LINE_NO { get; set; }
        public string P_FLOOR_DESC_EN { get; set; }
        public Int64? N_LK_FLOOR_ID { get; set; }
        public Int64? N_LINE_NO { get; set; }
        public string N_FLOOR_DESC_EN { get; set; }
        

        public string P_COMP_NAME_EN { get; set; }
        public string P_OFFICE_NAME_EN { get; set; }
        public string P_DEPARTMENT_NAME_EN { get; set; }
        public string P_DESIGNATION_NAME_EN { get; set; }
        public string N_COMP_NAME_EN { get; set; }
        public string N_OFFICE_NAME_EN { get; set; }
        public string N_DEPARTMENT_NAME_EN { get; set; }
        public string N_DESIGNATION_NAME_EN { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }


        public string Save()
        {
            const string sp = "pkg_transfer.hr_emp_trnfr_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EMP_TRNFR_ID", Value = ob.HR_EMP_TRNFR_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pTRNF_REF_NO", Value = ob.TRNF_REF_NO},
                     new CommandParameter() {ParameterName = "pTRNF_REF_DT", Value = ob.TRNF_REF_DT},
                     new CommandParameter() {ParameterName = "pEFFECT_DT", Value = ob.EFFECT_DT},
                     new CommandParameter() {ParameterName = "pP_EMPLOYEE_CODE", Value = ob.P_EMPLOYEE_CODE},
                     new CommandParameter() {ParameterName = "pP_HR_COMPANY_ID", Value = ob.P_HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pP_HR_OFFICE_ID", Value = ob.P_HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pP_HR_DEPARTMENT_ID", Value = ob.P_HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pP_HR_DESIGNATION_ID", Value = ob.P_HR_DESIGNATION_ID},
                     new CommandParameter() {ParameterName = "pP_HR_GRADE_ID", Value = ob.P_HR_GRADE_ID},
                     new CommandParameter() {ParameterName = "pP_HR_PROD_FLR_ID", Value = ob.P_HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pP_HR_PROD_LINE_ID", Value = ob.P_HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pP_BASIC_SALARY", Value = ob.P_BASIC_SALARY},
                     new CommandParameter() {ParameterName = "pP_GROSS_SALARY", Value = ob.P_GROSS_SALARY},
                     new CommandParameter() {ParameterName = "pN_EMPLOYEE_CODE", Value = ob.N_EMPLOYEE_CODE},
                     new CommandParameter() {ParameterName = "pN_HR_COMPANY_ID", Value = ob.N_HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pN_HR_OFFICE_ID", Value = ob.N_HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pN_HR_DEPARTMENT_ID", Value = ob.N_HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pN_HR_DESIGNATION_ID", Value = ob.N_HR_DESIGNATION_ID},
                     new CommandParameter() {ParameterName = "pN_HR_GRADE_ID", Value = ob.N_HR_GRADE_ID},
                     new CommandParameter() {ParameterName = "pN_HR_PROD_FLR_ID", Value = ob.N_HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pN_HR_PROD_LINE_ID", Value = ob.N_HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pN_BASIC_SALARY", Value = ob.N_BASIC_SALARY},
                     new CommandParameter() {ParameterName = "pN_GROSS_SALARY", Value = ob.N_GROSS_SALARY},
                     new CommandParameter() {ParameterName = "pLAST_GROSS_SALARY", Value = ob.LAST_GROSS_SALARY},
                     new CommandParameter() {ParameterName = "pLAST_BASIC_SALARY", Value = ob.LAST_BASIC_SALARY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},

                     new CommandParameter() {ParameterName = "pP_LK_FLOOR_ID", Value = ob.P_LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pP_LINE_NO", Value = ob.P_LINE_NO},
                     new CommandParameter() {ParameterName = "pN_LK_FLOOR_ID", Value = ob.N_LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pN_LINE_NO", Value = ob.N_LINE_NO},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
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



        public object GetTransData(int pageNumber, int pageSize, string pP_EMPLOYEE_CODE = null, string pN_EMPLOYEE_CODE = null, DateTime? pFROM_DT = null, DateTime? pTO_DT = null)
        {
            string sp = "pkg_transfer.hr_emp_trnfr_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<HR_EMP_TRNFRModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pP_EMPLOYEE_CODE", Value = pP_EMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pN_EMPLOYEE_CODE", Value = pN_EMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pFROM_DT", Value = pFROM_DT},
                     new CommandParameter() {ParameterName = "pTO_DT", Value = pTO_DT},
                     
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMP_TRNFRModel ob = new HR_EMP_TRNFRModel();
                    ob.HR_EMP_TRNFR_ID = (dr["HR_EMP_TRNFR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMP_TRNFR_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.TRNF_REF_NO = (dr["TRNF_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRNF_REF_NO"]);
                    ob.TRNF_REF_DT = (dr["TRNF_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRNF_REF_DT"]);
                    ob.EFFECT_DT = (dr["EFFECT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECT_DT"]);
                    ob.P_EMPLOYEE_CODE = (dr["P_EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_EMPLOYEE_CODE"]);
                    ob.P_HR_COMPANY_ID = (dr["P_HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_COMPANY_ID"]);
                    ob.P_HR_OFFICE_ID = (dr["P_HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_OFFICE_ID"]);
                    ob.P_HR_DEPARTMENT_ID = (dr["P_HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_DEPARTMENT_ID"]);
                    ob.P_HR_DESIGNATION_ID = (dr["P_HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_DESIGNATION_ID"]);

                    if (dr["P_HR_GRADE_ID"] != DBNull.Value)
                        ob.P_HR_GRADE_ID = (dr["P_HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_GRADE_ID"]);

                    if (dr["P_HR_PROD_FLR_ID"] != DBNull.Value)
                        ob.P_HR_PROD_FLR_ID = (dr["P_HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_PROD_FLR_ID"]);

                    if (dr["P_HR_PROD_LINE_ID"] != DBNull.Value)
                        ob.P_HR_PROD_LINE_ID = (dr["P_HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_PROD_LINE_ID"]);
                    
                    ob.P_BASIC_SALARY = (dr["P_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["P_BASIC_SALARY"]);
                    ob.P_GROSS_SALARY = (dr["P_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["P_GROSS_SALARY"]);
                    ob.N_EMPLOYEE_CODE = (dr["N_EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_EMPLOYEE_CODE"]);
                    ob.N_HR_COMPANY_ID = (dr["N_HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_COMPANY_ID"]);
                    ob.N_HR_OFFICE_ID = (dr["N_HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_OFFICE_ID"]);
                    ob.N_HR_DEPARTMENT_ID = (dr["N_HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_DEPARTMENT_ID"]);                    
                    ob.N_HR_DESIGNATION_ID = (dr["N_HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_DESIGNATION_ID"]);

                    if (dr["N_HR_GRADE_ID"] != DBNull.Value)
                        ob.N_HR_GRADE_ID = (dr["N_HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_GRADE_ID"]);                    
                    if(dr["N_HR_PROD_FLR_ID"] != DBNull.Value)
                        ob.N_HR_PROD_FLR_ID = (dr["N_HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_PROD_FLR_ID"]);
                    if (dr["N_HR_PROD_LINE_ID"] != DBNull.Value)
                        ob.N_HR_PROD_LINE_ID = (dr["N_HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_PROD_LINE_ID"]);
                    
                    ob.N_BASIC_SALARY = (dr["N_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["N_BASIC_SALARY"]);
                    ob.N_GROSS_SALARY = (dr["N_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["N_GROSS_SALARY"]);
                    ob.LAST_GROSS_SALARY = (dr["LAST_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_GROSS_SALARY"]);
                    ob.LAST_BASIC_SALARY = (dr["LAST_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_BASIC_SALARY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);


                    if (dr["P_LK_FLOOR_ID"] != DBNull.Value)
                        ob.P_LK_FLOOR_ID = (dr["P_LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_LK_FLOOR_ID"]);
                    if (dr["P_LINE_NO"] != DBNull.Value)
                        ob.P_LINE_NO = (dr["P_LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_LINE_NO"]);

                    if (dr["N_LK_FLOOR_ID"] != DBNull.Value)
                        ob.N_LK_FLOOR_ID = (dr["N_LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_LK_FLOOR_ID"]);
                    if (dr["N_LINE_NO"] != DBNull.Value)
                        ob.N_LINE_NO = (dr["N_LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_LINE_NO"]);

                    ob.P_FLOOR_DESC_EN = (dr["P_FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_FLOOR_DESC_EN"]);
                    ob.N_FLOOR_DESC_EN = (dr["N_FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_FLOOR_DESC_EN"]);

                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    
                    ob.P_COMP_NAME_EN = (dr["P_COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_COMP_NAME_EN"]);
                    ob.P_OFFICE_NAME_EN = (dr["P_OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_OFFICE_NAME_EN"]);
                    ob.P_DEPARTMENT_NAME_EN = (dr["P_DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_DEPARTMENT_NAME_EN"]);
                    ob.P_DESIGNATION_NAME_EN = (dr["P_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_DESIGNATION_NAME_EN"]);

                    ob.N_COMP_NAME_EN = (dr["N_COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_COMP_NAME_EN"]);
                    ob.N_OFFICE_NAME_EN = (dr["N_OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_OFFICE_NAME_EN"]);
                    ob.N_DEPARTMENT_NAME_EN = (dr["N_DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_DEPARTMENT_NAME_EN"]);
                    ob.N_DESIGNATION_NAME_EN = (dr["N_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_DESIGNATION_NAME_EN"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }
                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_EMP_TRNFRModel GetTransDataByID(long pHR_EMP_TRNFR_ID)
        {
            string sp = "pkg_transfer.hr_emp_trnfr_select";
            try
            {
                var ob = new HR_EMP_TRNFRModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EMP_TRNFR_ID", Value = pHR_EMP_TRNFR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_EMP_TRNFR_ID = (dr["HR_EMP_TRNFR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMP_TRNFR_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.TRNF_REF_NO = (dr["TRNF_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRNF_REF_NO"]);
                    ob.TRNF_REF_DT = (dr["TRNF_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRNF_REF_DT"]);
                    ob.EFFECT_DT = (dr["EFFECT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECT_DT"]);
                    ob.P_EMPLOYEE_CODE = (dr["P_EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_EMPLOYEE_CODE"]);
                    ob.P_HR_COMPANY_ID = (dr["P_HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_COMPANY_ID"]);
                    ob.P_HR_OFFICE_ID = (dr["P_HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_OFFICE_ID"]);
                    ob.P_HR_DEPARTMENT_ID = (dr["P_HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_DEPARTMENT_ID"]);
                    ob.P_HR_DESIGNATION_ID = (dr["P_HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_DESIGNATION_ID"]);

                    if (dr["P_HR_GRADE_ID"] != DBNull.Value)
                        ob.P_HR_GRADE_ID = (dr["P_HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_GRADE_ID"]);

                    if (dr["P_HR_PROD_FLR_ID"] != DBNull.Value)
                        ob.P_HR_PROD_FLR_ID = (dr["P_HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_PROD_FLR_ID"]);

                    if (dr["P_HR_PROD_LINE_ID"] != DBNull.Value)
                        ob.P_HR_PROD_LINE_ID = (dr["P_HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_HR_PROD_LINE_ID"]);

                    ob.P_BASIC_SALARY = (dr["P_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["P_BASIC_SALARY"]);
                    ob.P_GROSS_SALARY = (dr["P_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["P_GROSS_SALARY"]);
                    ob.N_EMPLOYEE_CODE = (dr["N_EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_EMPLOYEE_CODE"]);
                    ob.N_HR_COMPANY_ID = (dr["N_HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_COMPANY_ID"]);
                    ob.N_HR_OFFICE_ID = (dr["N_HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_OFFICE_ID"]);
                    ob.N_HR_DEPARTMENT_ID = (dr["N_HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_DEPARTMENT_ID"]);
                    ob.N_HR_DESIGNATION_ID = (dr["N_HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_DESIGNATION_ID"]);

                    if (dr["N_HR_GRADE_ID"] != DBNull.Value)
                        ob.N_HR_GRADE_ID = (dr["N_HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_GRADE_ID"]);
                    if (dr["N_HR_PROD_FLR_ID"] != DBNull.Value)
                        ob.N_HR_PROD_FLR_ID = (dr["N_HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_PROD_FLR_ID"]);
                    if (dr["N_HR_PROD_LINE_ID"] != DBNull.Value)
                        ob.N_HR_PROD_LINE_ID = (dr["N_HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_HR_PROD_LINE_ID"]);

                    ob.N_BASIC_SALARY = (dr["N_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["N_BASIC_SALARY"]);
                    ob.N_GROSS_SALARY = (dr["N_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["N_GROSS_SALARY"]);
                    ob.LAST_GROSS_SALARY = (dr["LAST_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_GROSS_SALARY"]);
                    ob.LAST_BASIC_SALARY = (dr["LAST_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_BASIC_SALARY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);


                    if (dr["P_LK_FLOOR_ID"] != DBNull.Value)
                        ob.P_LK_FLOOR_ID = (dr["P_LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_LK_FLOOR_ID"]);
                    if (dr["P_LINE_NO"] != DBNull.Value)
                        ob.P_LINE_NO = (dr["P_LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["P_LINE_NO"]);

                    if (dr["N_LK_FLOOR_ID"] != DBNull.Value)
                        ob.N_LK_FLOOR_ID = (dr["N_LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_LK_FLOOR_ID"]);
                    if (dr["N_LINE_NO"] != DBNull.Value)
                        ob.N_LINE_NO = (dr["N_LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["N_LINE_NO"]);

                    ob.P_FLOOR_DESC_EN = (dr["P_FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_FLOOR_DESC_EN"]);
                    ob.N_FLOOR_DESC_EN = (dr["N_FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_FLOOR_DESC_EN"]);

                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);

                    ob.P_COMP_NAME_EN = (dr["P_COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_COMP_NAME_EN"]);
                    ob.P_OFFICE_NAME_EN = (dr["P_OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_OFFICE_NAME_EN"]);
                    ob.P_DEPARTMENT_NAME_EN = (dr["P_DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_DEPARTMENT_NAME_EN"]);
                    ob.P_DESIGNATION_NAME_EN = (dr["P_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["P_DESIGNATION_NAME_EN"]);

                    ob.N_COMP_NAME_EN = (dr["N_COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_COMP_NAME_EN"]);
                    ob.N_OFFICE_NAME_EN = (dr["N_OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_OFFICE_NAME_EN"]);
                    ob.N_DEPARTMENT_NAME_EN = (dr["N_DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_DEPARTMENT_NAME_EN"]);
                    ob.N_DESIGNATION_NAME_EN = (dr["N_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["N_DESIGNATION_NAME_EN"]);
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