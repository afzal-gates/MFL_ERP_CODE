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
    public class HR_YR_INCR_HModel
    {
        public Int64 HR_YR_INCR_H_ID { get; set; }
        public Int64 HR_INCR_MEMO_ID { get; set; }
        public Int64? HR_DEPARTMENT_ID { get; set; }
        public Int64? HR_PROD_FLR_ID { get; set; }
        public Int64? PROPOSE_BY { get; set; }
        public string AUTHORIZE_BY_LST { get; set; }
        public string BATCH_NO { get; set; }
        public string IS_P_F { get; set; }
        public string REASON_DESC { get; set; }
        public Int64? LK_INCR_STATUS_ID { get; set; }
        public string BATCH_APVRL_STATUS { get; set; }
        public Int32? APROVER_LVL_NO { get; set; }
        
        
        public Int64? EMPLOYEE_TYPE_ID { get; set; }
        public string USER_LEVEL { get; set; }
        public Int32? USER_APROVER_LVL_NO { get; set; }
        public string SECTION_NAME { get; set; }
        public string FLOOR_DESC_EN { get; set; }
        public string BATCH_STATUS { get; set; }
        public Int64? TOTAL_EMPLOYEE { get; set; }
        public decimal? TOTAL_INCR_AMT { get; set; }

        public Int64? CORE_DEPT_ID { get; set; }
        public Int64? LK_FLOOR_ID { get; set; }
        public DateTime? MEMO_DT { get; set; }
        public string INCR_DTL_XML { get; set; }

        

        public List<HR_YR_INCR_HModel> SelectAll()
        {
            string sp = "Select_HR_YR_INCR_H";
            try
            {
                var obList = new List<HR_YR_INCR_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            HR_YR_INCR_HModel ob = new HR_YR_INCR_HModel();
                            ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                            ob.HR_INCR_MEMO_ID = (dr["HR_INCR_MEMO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_MEMO_ID"]);
                            ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                            ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                            ob.PROPOSE_BY = (dr["PROPOSE_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROPOSE_BY"]);
                            ob.AUTHORIZE_BY_LST = (dr["AUTHORIZE_BY_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTHORIZE_BY_LST"]);
                            ob.BATCH_NO = (dr["BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BATCH_NO"]);
                            ob.IS_P_F = (dr["IS_P_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_P_F"]);
                            ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                            
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_YR_INCR_HModel GetIncrHdr(Int64? pHR_YR_INCR_H_ID = null, Int64? pHR_INCR_MEMO_ID = null, Int64? pHR_DEPARTMENT_ID = null, Int64? pLK_FLOOR_ID = null, Int64? pPROPOSE_BY = null)
        {
            string sp = "pkg_incriment.hr_yr_incr_h_select";
            try
            {
                string vUserLevel = "";
                Int32 vUserApproverLvlNo=0;
                //Int64 vProposeBy = 0;

                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = pHR_INCR_MEMO_ID},                     
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = (pLK_FLOOR_ID<1?null:pLK_FLOOR_ID)},
                     new CommandParameter() {ParameterName = "pPROPOSE_BY", Value = pPROPOSE_BY},
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

                var ob = new HR_YR_INCR_HModel();                
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = pHR_INCR_MEMO_ID},                     
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = (pLK_FLOOR_ID<1?null:pLK_FLOOR_ID)},
                     new CommandParameter() {ParameterName = "pPROPOSE_BY", Value = pPROPOSE_BY},

                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                    ob.HR_INCR_MEMO_ID = (dr["HR_INCR_MEMO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_MEMO_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);

                    if (dr["HR_PROD_FLR_ID"] != DBNull.Value)
                        ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);

                    ob.PROPOSE_BY = (dr["PROPOSE_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROPOSE_BY"]);
                    ob.AUTHORIZE_BY_LST = (dr["AUTHORIZE_BY_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTHORIZE_BY_LST"]);
                    ob.BATCH_NO = (dr["BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BATCH_NO"]);
                    ob.IS_P_F = (dr["IS_P_F"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_P_F"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                    ob.LK_INCR_STATUS_ID = (dr["LK_INCR_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INCR_STATUS_ID"]);
                    ob.BATCH_APVRL_STATUS = (dr["BATCH_APVRL_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BATCH_APVRL_STATUS"]);
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["APROVER_LVL_NO"]);
                }

                if (ob.PROPOSE_BY < 1 || ob.PROPOSE_BY==null)
                    ob.PROPOSE_BY = Convert.ToInt64(HttpContext.Current.Session["multiLoginEmpId"]);

                ob.USER_LEVEL = vUserLevel;
                ob.USER_APROVER_LVL_NO = vUserApproverLvlNo;

                return ob;
            }            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IncrBatchSave()
        {
            const string sp = "pkg_incriment.hr_yr_incr_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = ob.HR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = ob.HR_INCR_MEMO_ID},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value = ob.MEMO_DT},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pPROPOSE_BY", Value = ob.PROPOSE_BY},// HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pAUTHORIZE_BY_LST", Value = ob.AUTHORIZE_BY_LST},
                     new CommandParameter() {ParameterName = "pBATCH_NO", Value = ob.BATCH_NO},
                     new CommandParameter() {ParameterName = "pIS_P_F", Value = ob.IS_P_F},
                     new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
                     new CommandParameter() {ParameterName = "pINCR_DTL_XML", Value = ob.INCR_DTL_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pPROPOSER_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pUSER_LEVEL", Value = ob.USER_LEVEL},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pIS_P_F_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID_RTN", Direction = ParameterDirection.Output}                     
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

        public string IncrProposeSave()
        {
            const string sp = "pkg_incriment.hr_yr_incr_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = ob.HR_YR_INCR_H_ID},   
                     //new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},   
                     //new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},   
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pPROPOSER_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pIS_P_F_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID_RTN", Direction = ParameterDirection.Output}                     
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

        public string IncrVeryficationSave()
        {
            const string sp = "pkg_incriment.hr_yr_incr_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = ob.HR_YR_INCR_H_ID},    
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pPROPOSER_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pIS_P_F_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID_RTN", Direction = ParameterDirection.Output}                     
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

        public string IncrFinalizeSave()
        {
            const string sp = "pkg_incriment.hr_yr_incr_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = ob.HR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pINCR_DTL_XML", Value = ob.INCR_DTL_XML},
                     new CommandParameter() {ParameterName = "pPROPOSER_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pIS_P_F_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID_RTN", Direction = ParameterDirection.Output}                     
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

        public List<HR_YR_INCR_HModel> GetIncrBatchList(Int64? pHR_INCR_MEMO_ID)
        {
            string sp = "pkg_incriment.hr_yr_incr_h_select";
            try
            {
                var obList = new List<HR_YR_INCR_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = pHR_INCR_MEMO_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_YR_INCR_HModel ob = new HR_YR_INCR_HModel();
                    ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                    ob.HR_INCR_MEMO_ID = (dr["HR_INCR_MEMO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_MEMO_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);

                    if (dr["LK_FLOOR_ID"] != DBNull.Value)
                        ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);

                    ob.EMPLOYEE_TYPE_ID = (dr["EMPLOYEE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EMPLOYEE_TYPE_ID"]);
                    ob.SECTION_NAME = (dr["SECTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_NAME"]);
                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                    ob.LK_INCR_STATUS_ID = (dr["LK_INCR_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INCR_STATUS_ID"]);
                    ob.BATCH_STATUS = (dr["BATCH_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BATCH_STATUS"]);
                    ob.TOTAL_EMPLOYEE = (dr["TOTAL_EMPLOYEE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_EMPLOYEE"]);
                    ob.TOTAL_INCR_AMT = (dr["TOTAL_INCR_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_INCR_AMT"]);
                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SpecialIncrBatchSave()
        {
            const string sp = "pkg_incriment.hr_yr_incr_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = ob.HR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = ob.HR_INCR_MEMO_ID},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value = ob.MEMO_DT},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pPROPOSE_BY", Value = ob.PROPOSE_BY},// HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pAUTHORIZE_BY_LST", Value = ob.AUTHORIZE_BY_LST},
                     new CommandParameter() {ParameterName = "pBATCH_NO", Value = ob.BATCH_NO},
                     new CommandParameter() {ParameterName = "pIS_P_F", Value = ob.IS_P_F},
                     new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
                     new CommandParameter() {ParameterName = "pINCR_DTL_XML", Value = ob.INCR_DTL_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pPROPOSER_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pUSER_LEVEL", Value = ob.USER_LEVEL},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pIS_P_F_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID_RTN", Direction = ParameterDirection.Output}                     
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

        public HR_YR_INCR_HModel GetSpecialIncrHdr(Int64? pHR_YR_INCR_H_ID = null, Int64? pHR_INCR_MEMO_ID = null, Int64? pPROPOSE_BY = null)
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
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = pHR_INCR_MEMO_ID},                     
                     
                     new CommandParameter() {ParameterName = "pPROPOSE_BY", Value = pPROPOSE_BY},
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
                
                var ob = new HR_YR_INCR_HModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = pHR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = pHR_INCR_MEMO_ID},                                        
                     new CommandParameter() {ParameterName = "pPROPOSE_BY", Value = pPROPOSE_BY},

                     new CommandParameter() {ParameterName = "pOption", Value = 3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);
                    ob.HR_INCR_MEMO_ID = (dr["HR_INCR_MEMO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_MEMO_ID"]);
                    //ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    //ob.CORE_DEPT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    //ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);

                    if (dr["HR_PROD_FLR_ID"] != DBNull.Value)
                        ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);

                    ob.PROPOSE_BY = (dr["PROPOSE_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROPOSE_BY"]);
                    ob.AUTHORIZE_BY_LST = (dr["AUTHORIZE_BY_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTHORIZE_BY_LST"]);
                    ob.BATCH_NO = (dr["BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BATCH_NO"]);
                    ob.IS_P_F = (dr["IS_P_F"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_P_F"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                    ob.LK_INCR_STATUS_ID = (dr["LK_INCR_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INCR_STATUS_ID"]);
                    ob.BATCH_APVRL_STATUS = (dr["BATCH_APVRL_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BATCH_APVRL_STATUS"]);
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["APROVER_LVL_NO"]);
                }

                if (ob.PROPOSE_BY < 1 || ob.PROPOSE_BY == null)
                    ob.PROPOSE_BY = Convert.ToInt64(HttpContext.Current.Session["multiLoginEmpId"]);

                ob.USER_LEVEL = vUserLevel;
                ob.USER_APROVER_LVL_NO = vUserApproverLvlNo;

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SpecialIncrProposeSave()
        {
            const string sp = "pkg_incriment.hr_yr_incr_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = ob.HR_YR_INCR_H_ID},                        
                     new CommandParameter() {ParameterName = "pPROPOSER_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pIS_P_F_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID_RTN", Direction = ParameterDirection.Output}                     
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

        public string SpecialIncrFinalizeSave()
        {
            const string sp = "pkg_incriment.hr_yr_incr_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID", Value = ob.HR_YR_INCR_H_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pPROPOSER_EMPLOYEE_ID", Value = HttpContext.Current.Session["multiLoginEmpId"]},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pIS_P_F_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pHR_YR_INCR_H_ID_RTN", Direction = ParameterDirection.Output}                     
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

    }




    
}