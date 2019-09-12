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
    public class HR_YR_INCR_DModel
    {
        public Int64 HR_YR_INCR_D_ID { get; set; }
        public Int64 HR_YR_INCR_H_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public string IS_PROMOTED { get; set; }
        public Int64? HR_DESIGNATION_ID { get; set; }
        public Int64? HR_INCR_GRADE_ID { get; set; }
        public Int64? ADDL_AMT { get; set; }
        public Int64? INCR_AMT { get; set; }
        public DateTime? EFFECTIVE_DT { get; set; }
        public string IS_APPROVED { get; set; }
        public string REMARKS { get; set; }        
        public string IS_EFFECTED { get; set; }
        public decimal? INCR_PCT { get; set; }


        public decimal? GRADE_PCT { get; set; }
        public decimal? GRADE_HR_PCT { get; set; }
        public Int64? GRADE_ADDL_AMT { get; set; }
        public string IS_B_G { get; set; }


        public Int64? CORE_DEPT_ID { get; set; }
        public Int64? DSG_GRP_ORDER { get; set; }
        public Int64? DESIG_ORDER { get; set; }
        public string IS_EFF_DT_CHANGED { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string OLD_DESIGNATION_NAME_EN { get; set; }
        public string NEW_DESIGNATION_NAME_EN { get; set; }
        public DateTime? JOINING_DT { get; set; }
        public Int64? PRE_GROSS_SALARY { get; set; }
        public Int64? PRE_BASIC_SALARY { get; set; }
        public Int64? PRE_HR_DESIGNATION_ID { get; set; }
        public string PRE_DESIGNATION_NAME_EN { get; set; }
        public Int64? NEW_GROSS { get; set; }
        public DateTime? LAST_INCR_DT { get; set; }
        public Int64? LAST_INCR_AMT { get; set; }
        public string INCR_GRADE_CODE { get; set; }
        public bool PROMOTED_DISABLED { get; set; }
        public bool INCR_AMT_DISABLED { get; set; }
        public bool GRADE_DISABLED { get; set; }
        public bool APPROVE_DISABLED { get; set; }
        public object HR_DESIGNATION
        {
            get
            {
                return new { HR_DESIGNATION_ID = this.HR_DESIGNATION_ID, DESIGNATION_NAME_EN = this.DESIGNATION_NAME_EN ?? "" };
            }
        }
        public object HR_GRADE
        {
            get
            {
                return new { HR_INCR_GRADE_ID = this.HR_INCR_GRADE_ID, INCR_GRADE_CODE = this.INCR_GRADE_CODE ?? "" };
            }
        }


         
        //public string Save()
        //{
        //    const string sp = "SP_HR_YR_INCR_D";
        //    string jsonStr="{";
        //    var ob = this;
        //    var i = 1;
        //    try
        //     {
        //        OraDatabase db = new OraDatabase();
        //        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {
        //             new CommandParameter() {ParameterName = "pHR_YR_INCR_D_ID", Value = ob.HR_YR_INCR_D_ID},
        //             new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = ob.HR_YR_INCR_H_ID},
        //             new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
        //             new CommandParameter() {ParameterName = "pIS_PROMOTED", Value = ob.IS_PROMOTED},
        //             new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = ob.HR_DESIGNATION_ID},
        //             new CommandParameter() {ParameterName = "pHR_INCR_GRADE_ID", Value = ob.HR_INCR_GRADE_ID},
        //             new CommandParameter() {ParameterName = "pADDL_AMT", Value = ob.ADDL_AMT},
        //             new CommandParameter() {ParameterName = "pINCR_AMT", Value = ob.INCR_AMT},
        //             new CommandParameter() {ParameterName = "pEFFECTIVE_DT", Value = ob.EFFECTIVE_DT},
        //             new CommandParameter() {ParameterName = "pIS_APPROVED", Value = ob.IS_APPROVED},
        //             new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
        //             new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
        //             new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
        //             new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
        //             new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
        //             new CommandParameter() {ParameterName = "pVERSION_NO", Value = ob.VERSION_NO},
        //             new CommandParameter() {ParameterName = "pIS_EFFECTED", Value = ob.IS_EFFECTED},
        //             new CommandParameter() {ParameterName = "pOption", Value =1000},
        //             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
        //         }, sp);

        //         foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
        //         {
        //            jsonString += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
        //            if (i < ds.Tables["OUTPARAM"].Rows.Count) 
        //             { 
        //              jsonStr += ",";
        //             }  
        //             else  
        //              {  
        //                  jsonStr += "}"; 
        //              }  
        //                 i++ ;
        //         }
        //         return jsonStr;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public object GetEmpSearch4SpecialIncr(string pEMPLOYEE_CODE, Int64 pHR_INCR_MEMO_ID, Int64 pHR_YR_INCR_H_ID)
        {
            string sp = "pkg_incriment.hr_yr_incr_h_select";
            try
            {
                string vUserLevel = "";
                Int32 vUserApproverLvlNo = 0;

                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                                         
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},                     
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},                     

                     new CommandParameter() {ParameterName = "pOption", Value = 3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    vUserLevel = (dr["USER_LEVEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["USER_LEVEL"]);
                    vUserApproverLvlNo = (dr["USER_APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["USER_APROVER_LVL_NO"]);
                }
                //if (Convert.ToString(HttpContext.Current.Session["multiUserType"]) == "B")
                //{
                //    vIsProposer = "Y";
                //    vIsApprover = "Y";
                //}

                int vIncrCurrStatusId = 0;
                int vIncrCurrAprvlLvl = 0;

                var obList = new List<HR_YR_INCR_DModel>();
                
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = pEMPLOYEE_CODE},
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = pHR_INCR_MEMO_ID},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_YR_INCR_DModel ob = new HR_YR_INCR_DModel();
                    ob.HR_YR_INCR_D_ID = (dr["HR_YR_INCR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_D_ID"]);
                    ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.IS_PROMOTED = (dr["IS_PROMOTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PROMOTED"]);

                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.DSG_GRP_ORDER = (dr["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DSG_GRP_ORDER"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);

                    if (dr["HR_DESIGNATION_ID"] != DBNull.Value)
                        ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    if (dr["HR_INCR_GRADE_ID"] != DBNull.Value)
                        ob.HR_INCR_GRADE_ID = (dr["HR_INCR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_GRADE_ID"]);
                    ob.INCR_GRADE_CODE = (dr["INCR_GRADE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_GRADE_CODE"]);

                    ob.GRADE_PCT = (dr["GRADE_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PCT"]);
                    ob.GRADE_HR_PCT = (dr["GRADE_HR_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_HR_PCT"]);
                    ob.GRADE_ADDL_AMT = (dr["GRADE_ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GRADE_ADDL_AMT"]);
                    ob.IS_B_G = (dr["IS_B_G"] == DBNull.Value) ? "G" : Convert.ToString(dr["IS_B_G"]);

                    //if (dr["ADDL_AMT"] != DBNull.Value)
                    ob.ADDL_AMT = (dr["ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_AMT"]);
                    //if (dr["INCR_AMT"] != DBNull.Value)
                    ob.INCR_AMT = (dr["INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INCR_AMT"]);
                    //if (dr["INCR_PCT"] != DBNull.Value)
                    ob.INCR_PCT = (dr["INCR_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INCR_PCT"]);

                    if (dr["EFFECTIVE_DT"] != DBNull.Value)
                        ob.EFFECTIVE_DT = (dr["EFFECTIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_DT"]);

                    ob.IS_APPROVED = (dr["IS_APPROVED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_APPROVED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.IS_EFFECTED = (dr["IS_EFFECTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_EFFECTED"]);
                    ob.IS_EFF_DT_CHANGED = (dr["IS_EFF_DT_CHANGED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_EFF_DT_CHANGED"]);

                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    if (dr["JOINING_DT"] != DBNull.Value)
                        ob.JOINING_DT = (dr["JOINING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["JOINING_DT"]);

                    ob.PRE_GROSS_SALARY = (dr["PRE_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_GROSS_SALARY"]);
                    ob.PRE_BASIC_SALARY = (dr["PRE_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_BASIC_SALARY"]);
                    ob.PRE_HR_DESIGNATION_ID = (dr["PRE_HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_HR_DESIGNATION_ID"]);
                    ob.PRE_DESIGNATION_NAME_EN = (dr["PRE_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_DESIGNATION_NAME_EN"]);
                    ob.NEW_GROSS = (dr["NEW_GROSS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NEW_GROSS"]);

                    vIncrCurrStatusId = (dr["LK_INCR_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["LK_INCR_STATUS_ID"]);
                    vIncrCurrAprvlLvl = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["APROVER_LVL_NO"]);

                    if (dr["LAST_INCR_DT"] != DBNull.Value)
                        ob.LAST_INCR_DT = (dr["LAST_INCR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_INCR_DT"]);

                    ob.LAST_INCR_AMT = (dr["LAST_INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_INCR_AMT"]);

                    

                    ob.PROMOTED_DISABLED = true;



                    if (ob.IS_PROMOTED == "Y")
                        ob.GRADE_DISABLED = true;
                    else if (vUserLevel == "PROPOSER" && vIncrCurrStatusId > 636)
                        ob.GRADE_DISABLED = true;
                    else if ((vUserLevel == "APPROVER" && vIncrCurrStatusId == 537))
                        ob.GRADE_DISABLED = true;
                    else
                        ob.GRADE_DISABLED = false;

                    ob.INCR_AMT_DISABLED = true;


                    if (vUserLevel == "APPROVER")
                        ob.APPROVE_DISABLED = false;
                    else
                        ob.APPROVE_DISABLED = true;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetSpecialIncrDtl(Int64 pHR_YR_INCR_H_ID)
        {
            string sp = "pkg_incriment.hr_yr_incr_h_select";
            try
            {
                string vUserLevel = "";
                Int32 vUserApproverLvlNo = 0;

                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                                         
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},                     
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},                     

                     new CommandParameter() {ParameterName = "pOption", Value = 3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    vUserLevel = (dr["USER_LEVEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["USER_LEVEL"]);
                    vUserApproverLvlNo = (dr["USER_APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["USER_APROVER_LVL_NO"]);
                }
                //if (Convert.ToString(HttpContext.Current.Session["multiUserType"]) == "B")
                //{
                //    vIsProposer = "Y";
                //    vIsApprover = "Y";
                //}

                int vIncrCurrStatusId = 0;
                int vIncrCurrAprvlLvl = 0;

                var obList = new List<HR_YR_INCR_DModel>();
                
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3010},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_YR_INCR_DModel ob = new HR_YR_INCR_DModel();
                    ob.HR_YR_INCR_D_ID = (dr["HR_YR_INCR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_D_ID"]);
                    ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.IS_PROMOTED = (dr["IS_PROMOTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PROMOTED"]);

                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.DSG_GRP_ORDER = (dr["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DSG_GRP_ORDER"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);

                    if (dr["HR_DESIGNATION_ID"] != DBNull.Value)
                        ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    if (dr["HR_INCR_GRADE_ID"] != DBNull.Value)
                        ob.HR_INCR_GRADE_ID = (dr["HR_INCR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_GRADE_ID"]);
                    ob.INCR_GRADE_CODE = (dr["INCR_GRADE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_GRADE_CODE"]);

                    ob.GRADE_PCT = (dr["GRADE_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PCT"]);
                    ob.GRADE_HR_PCT = (dr["GRADE_HR_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_HR_PCT"]);
                    ob.GRADE_ADDL_AMT = (dr["GRADE_ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GRADE_ADDL_AMT"]);
                    ob.IS_B_G = (dr["IS_B_G"] == DBNull.Value) ? "G" : Convert.ToString(dr["IS_B_G"]);

                    //if (dr["ADDL_AMT"] != DBNull.Value)
                    ob.ADDL_AMT = (dr["ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_AMT"]);
                    //if (dr["INCR_AMT"] != DBNull.Value)
                    ob.INCR_AMT = (dr["INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INCR_AMT"]);
                    //if (dr["INCR_PCT"] != DBNull.Value)
                    ob.INCR_PCT = (dr["INCR_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INCR_PCT"]);

                    if (dr["EFFECTIVE_DT"] != DBNull.Value)
                        ob.EFFECTIVE_DT = (dr["EFFECTIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_DT"]);

                    ob.IS_APPROVED = (dr["IS_APPROVED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_APPROVED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.IS_EFFECTED = (dr["IS_EFFECTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_EFFECTED"]);
                    ob.IS_EFF_DT_CHANGED = (dr["IS_EFF_DT_CHANGED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_EFF_DT_CHANGED"]);

                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    if (dr["JOINING_DT"] != DBNull.Value)
                        ob.JOINING_DT = (dr["JOINING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["JOINING_DT"]);

                    ob.PRE_GROSS_SALARY = (dr["PRE_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_GROSS_SALARY"]);
                    ob.PRE_BASIC_SALARY = (dr["PRE_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_BASIC_SALARY"]);
                    ob.PRE_HR_DESIGNATION_ID = (dr["PRE_HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_HR_DESIGNATION_ID"]);
                    ob.PRE_DESIGNATION_NAME_EN = (dr["PRE_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_DESIGNATION_NAME_EN"]);
                    ob.NEW_GROSS = (dr["NEW_GROSS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NEW_GROSS"]);

                    vIncrCurrStatusId = (dr["LK_INCR_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["LK_INCR_STATUS_ID"]);
                    vIncrCurrAprvlLvl = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["APROVER_LVL_NO"]);

                    if (dr["LAST_INCR_DT"] != DBNull.Value)
                        ob.LAST_INCR_DT = (dr["LAST_INCR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_INCR_DT"]);

                    ob.LAST_INCR_AMT = (dr["LAST_INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_INCR_AMT"]);
                    
                    ob.PROMOTED_DISABLED = true;
                    
                    if (ob.IS_PROMOTED == "Y")
                        ob.GRADE_DISABLED = true;
                    else if (vUserLevel == "PROPOSER" && vIncrCurrStatusId > 636)
                        ob.GRADE_DISABLED = true;
                    else if ((vUserLevel == "APPROVER" && vIncrCurrStatusId == 537))
                        ob.GRADE_DISABLED = true;
                    else
                        ob.GRADE_DISABLED = false;

                    ob.INCR_AMT_DISABLED = true;


                    if (vUserLevel == "APPROVER")
                        ob.APPROVE_DISABLED = false;
                    else
                        ob.APPROVE_DISABLED = true;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetEmp4IncrProposal(Int64 pageNumber, Int64 pageSize, Int64? pHR_INCR_MEMO_ID, Int32? pEMPLOYEE_TYPE_ID, Int64? pHR_YR_INCR_H_ID, Int64? pHR_DEPARTMENT_ID, Int32? pLK_FLOOR_ID)
        {
            string sp = "pkg_incriment.hr_yr_incr_h_select";
            try
            {
                string vUserLevel = "";
                Int32 vUserApproverLvlNo = 0;

                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = pHR_INCR_MEMO_ID}, 
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = (pLK_FLOOR_ID<1?null:pLK_FLOOR_ID)},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},                     

                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    vUserLevel = (dr["USER_LEVEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["USER_LEVEL"]);
                    vUserApproverLvlNo = (dr["USER_APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["USER_APROVER_LVL_NO"]);
                }
                //if (Convert.ToString(HttpContext.Current.Session["multiUserType"]) == "B")
                //{
                //    vIsProposer = "Y";
                //    vIsApprover = "Y";
                //}

                int vIncrCurrStatusId=0;
                int vIncrCurrAprvlLvl=0;

                Int64 vTotalRec = 0;
                var obList = new List<HR_YR_INCR_DModel>();
                var obj = new RF_PAGERModel();
                                
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = pHR_INCR_MEMO_ID},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = pEMPLOYEE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = pLK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_YR_INCR_DModel ob = new HR_YR_INCR_DModel();
                    ob.HR_YR_INCR_D_ID = (dr["HR_YR_INCR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_D_ID"]);
                    ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.IS_PROMOTED = (dr["IS_PROMOTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PROMOTED"]);

                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.DSG_GRP_ORDER = (dr["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DSG_GRP_ORDER"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    
                    if (dr["HR_DESIGNATION_ID"] != DBNull.Value)
                        ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    if (dr["HR_INCR_GRADE_ID"] != DBNull.Value)
                        ob.HR_INCR_GRADE_ID = (dr["HR_INCR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_GRADE_ID"]);
                    ob.INCR_GRADE_CODE = (dr["INCR_GRADE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_GRADE_CODE"]);

                    ob.GRADE_PCT = (dr["GRADE_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PCT"]);
                    ob.GRADE_HR_PCT = (dr["GRADE_HR_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_HR_PCT"]);
                    ob.GRADE_ADDL_AMT = (dr["GRADE_ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GRADE_ADDL_AMT"]);
                    ob.IS_B_G = (dr["IS_B_G"] == DBNull.Value) ? "G" : Convert.ToString(dr["IS_B_G"]);

                    //if (dr["ADDL_AMT"] != DBNull.Value)
                        ob.ADDL_AMT = (dr["ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_AMT"]);
                    //if (dr["INCR_AMT"] != DBNull.Value)
                        ob.INCR_AMT = (dr["INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INCR_AMT"]);
                    //if (dr["INCR_PCT"] != DBNull.Value)
                        ob.INCR_PCT = (dr["INCR_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INCR_PCT"]);

                    if (dr["EFFECTIVE_DT"] != DBNull.Value)
                        ob.EFFECTIVE_DT = (dr["EFFECTIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_DT"]);

                    ob.IS_APPROVED = (dr["IS_APPROVED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_APPROVED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.IS_EFFECTED = (dr["IS_EFFECTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_EFFECTED"]);
                    ob.IS_EFF_DT_CHANGED = (dr["IS_EFF_DT_CHANGED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_EFF_DT_CHANGED"]);
                    
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    if (dr["JOINING_DT"] != DBNull.Value)
                        ob.JOINING_DT = (dr["JOINING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["JOINING_DT"]);
                    
                    ob.PRE_GROSS_SALARY = (dr["PRE_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_GROSS_SALARY"]);
                    ob.PRE_BASIC_SALARY = (dr["PRE_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_BASIC_SALARY"]);
                    ob.PRE_HR_DESIGNATION_ID = (dr["PRE_HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_HR_DESIGNATION_ID"]);
                    ob.PRE_DESIGNATION_NAME_EN = (dr["PRE_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRE_DESIGNATION_NAME_EN"]);
                    ob.NEW_GROSS = (dr["NEW_GROSS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NEW_GROSS"]); 
                    
                    vIncrCurrStatusId=(dr["LK_INCR_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["LK_INCR_STATUS_ID"]);
                    vIncrCurrAprvlLvl=(dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["APROVER_LVL_NO"]);
                    
                    if (dr["LAST_INCR_DT"] != DBNull.Value)
                        ob.LAST_INCR_DT = (dr["LAST_INCR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_INCR_DT"]);
                    
                    ob.LAST_INCR_AMT = (dr["LAST_INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_INCR_AMT"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    ob.PROMOTED_DISABLED = true;

                    //if (pEMPLOYEE_TYPE_ID == 2)
                    //{
                    //    ob.GRADE_DISABLED = true;
                    //    ob.INCR_AMT_DISABLED = false;
                    //}
                    //else
                    //{
                    //    if(ob.IS_PROMOTED=="Y")
                    //        ob.GRADE_DISABLED = true;
                    //    else if (vUserLevel == "PROPOSER" && vIncrCurrStatusId>506)                        
                    //        ob.GRADE_DISABLED = true;                        
                    //    else if ((vUserLevel == "VERIFIER" && vIncrCurrStatusId>507) || (vUserLevel == "VERIFIER" && vIncrCurrAprvlLvl != vUserApproverLvlNo))                        
                    //        ob.GRADE_DISABLED = true;                        
                    //    else if ((vUserLevel == "APPROVER" && vIncrCurrStatusId == 510))                        
                    //        ob.GRADE_DISABLED = true;                        
                    //    else                        
                    //        ob.GRADE_DISABLED = false;                        

                    //    ob.INCR_AMT_DISABLED = true;
                    //}

                    if (ob.IS_PROMOTED == "Y")
                        ob.GRADE_DISABLED = true;
                    else if (vUserLevel == "PROPOSER" && vIncrCurrStatusId > 506)
                        ob.GRADE_DISABLED = true;
                    else if ((vUserLevel == "VERIFIER" && vIncrCurrStatusId > 507) || (vUserLevel == "VERIFIER" && vIncrCurrAprvlLvl != vUserApproverLvlNo))
                        ob.GRADE_DISABLED = true;
                    else if ((vUserLevel == "APPROVER" && vIncrCurrStatusId == 510))
                        ob.GRADE_DISABLED = true;
                    else
                        ob.GRADE_DISABLED = false;

                    ob.INCR_AMT_DISABLED = true;


                    if (vUserLevel == "APPROVER")
                        ob.APPROVE_DISABLED = false;
                    else
                        ob.APPROVE_DISABLED = true;

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

        public HR_YR_INCR_DModel Select(long ID)
        {
            string sp = "Select_HR_YR_INCR_D";
            try
            {
                var ob = new HR_YR_INCR_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   ob.HR_YR_INCR_D_ID = (dr["HR_YR_INCR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_D_ID"]);
                   ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                   ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                   ob.IS_PROMOTED = (dr["IS_PROMOTED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROMOTED"]);
                   ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                   ob.HR_INCR_GRADE_ID = (dr["HR_INCR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_GRADE_ID"]);
                   ob.ADDL_AMT = (dr["ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_AMT"]);
                   ob.INCR_AMT = (dr["INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INCR_AMT"]);
                   ob.EFFECTIVE_DT = (dr["EFFECTIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_DT"]);
                   ob.IS_APPROVED = (dr["IS_APPROVED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_APPROVED"]);
                   ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                   ob.IS_EFFECTED = (dr["IS_EFFECTED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EFFECTED"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object GetEmpIncrHistory(Int64? pHR_EMPLOYEE_ID = null)
        {
            string sp = "pkg_incriment.hr_yr_incr_h_select";
            try
            {                
                var obList = new List<HR_YR_INCR_DModel>();                

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_YR_INCR_DModel ob = new HR_YR_INCR_DModel();
                    ob.HR_YR_INCR_D_ID = (dr["HR_YR_INCR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_D_ID"]);
                    ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.IS_PROMOTED = (dr["IS_PROMOTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PROMOTED"]);
                    if (dr["HR_DESIGNATION_ID"] != DBNull.Value)
                        ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    if (dr["HR_INCR_GRADE_ID"] != DBNull.Value)
                        ob.HR_INCR_GRADE_ID = (dr["HR_INCR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_GRADE_ID"]);
                    ob.INCR_GRADE_CODE = (dr["INCR_GRADE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_GRADE_CODE"]);

                    ob.ADDL_AMT = (dr["ADDL_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_AMT"]);
                    ob.INCR_AMT = (dr["INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INCR_AMT"]);
                    ob.INCR_PCT = (dr["INCR_PCT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INCR_PCT"]);

                    if (dr["EFFECTIVE_DT"] != DBNull.Value)
                        ob.EFFECTIVE_DT = (dr["EFFECTIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EFFECTIVE_DT"]);

                    ob.IS_APPROVED = (dr["IS_APPROVED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_APPROVED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.IS_EFFECTED = (dr["IS_EFFECTED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_EFFECTED"]);

                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    
                    if (dr["JOINING_DT"] != DBNull.Value)
                        ob.JOINING_DT = (dr["JOINING_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["JOINING_DT"]);

                    ob.OLD_DESIGNATION_NAME_EN = (dr["OLD_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OLD_DESIGNATION_NAME_EN"]);
                    ob.NEW_DESIGNATION_NAME_EN = (dr["NEW_DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NEW_DESIGNATION_NAME_EN"]);

                    ob.PRE_GROSS_SALARY = (dr["PRE_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_GROSS_SALARY"]);
                    ob.PRE_BASIC_SALARY = (dr["PRE_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_BASIC_SALARY"]);
                    if (dr["LAST_INCR_DT"] != DBNull.Value)
                        ob.LAST_INCR_DT = (dr["LAST_INCR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_INCR_DT"]);

                    ob.LAST_INCR_AMT = (dr["LAST_INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_INCR_AMT"]);

                    
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }











        
    }
}