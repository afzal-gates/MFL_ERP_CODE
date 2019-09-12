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
    public class HR_INCR_MEMOModel
    {
        public Int64 HR_INCR_MEMO_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 RF_INCR_TYPE_ID { get; set; }
        public string AUTH_NO { get; set; }
        public string MEMO_NO { get; set; }
        public DateTime MEMO_DT { get; set; }
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public long ACC_PAY_PERIOD_ID { get; set; }
        public DateTime EFFECTIVE_DT { get; set; }
        public string IS_BY_ADMIN { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }

        public string INCR_TYPE_NAME_EN { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string MONTH_YEAR_NAME { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public Int64? HR_YR_INCR_H_ID { get; set; }



        public string Save()
        {
            const string sp = "pkg_incriment.hr_incr_memo_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = ob.HR_INCR_MEMO_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     new CommandParameter() {ParameterName = "pRF_INCR_TYPE_ID", Value = ob.RF_INCR_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMEMO_NO", Value = ob.MEMO_NO},
                     new CommandParameter() {ParameterName = "pAUTH_NO", Value = ob.AUTH_NO},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value = ob.MEMO_DT},
                     new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                     
                     new CommandParameter() {ParameterName = "pIS_BY_ADMIN", Value = ob.IS_BY_ADMIN},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pAUTH_NO_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID_RTN", Direction = ParameterDirection.Output},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MemoFinalize()
        {
            const string sp = "pkg_incriment.hr_incr_memo_finalize";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = ob.HR_INCR_MEMO_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pRF_INCR_TYPE_ID", Value = ob.RF_INCR_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMEMO_NO", Value = ob.MEMO_NO},
                     new CommandParameter() {ParameterName = "pMEMO_DT", Value = ob.MEMO_DT},
                     new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                     //new CommandParameter() {ParameterName = "pEFFECTIVE_DT", Value = ob.EFFECTIVE_DT},
                     new CommandParameter() {ParameterName = "pIS_BY_ADMIN", Value = ob.IS_BY_ADMIN},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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



        public object GetIncrMemoList(Int64 pageNumber, Int64 pageSize, string pAUTH_NO = null, string pRF_INCR_TYPE_ID_LST = null, string pINCR_TYPE_NAME_EN = null, string pCOMP_NAME_EN = null,
            string pMEMO_NO = null, string pIS_FINALIZED = null, string pIS_CLOSED = null)
        {
            string sp = "pkg_incriment.hr_incr_memo_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<HR_INCR_MEMOModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value =0},
                     new CommandParameter() {ParameterName = "pAUTH_NO", Value =pAUTH_NO},
                     new CommandParameter() {ParameterName = "pRF_INCR_TYPE_ID_LST", Value =pRF_INCR_TYPE_ID_LST},
                     new CommandParameter() {ParameterName = "pINCR_TYPE_NAME_EN", Value =pINCR_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pMEMO_NO", Value =pMEMO_NO},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value =pIS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value =pIS_CLOSED},

                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_INCR_MEMOModel ob = new HR_INCR_MEMOModel();
                    ob.HR_INCR_MEMO_ID = (dr["HR_INCR_MEMO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_MEMO_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_INCR_TYPE_ID = (dr["RF_INCR_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCR_TYPE_ID"]);
                    ob.AUTH_NO = (dr["AUTH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTH_NO"]);
                    ob.MEMO_NO = (dr["MEMO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEMO_NO"]);
                    ob.MEMO_DT = (dr["MEMO_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MEMO_DT"]);
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);

                    ob.IS_BY_ADMIN = (dr["IS_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BY_ADMIN"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.INCR_TYPE_NAME_EN = (dr["INCR_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_TYPE_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);
                    ob.START_DATE = (dr["START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DATE"]);
                    ob.END_DATE = (dr["END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DATE"]);

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
        
        public string IncrEffectProcess()
        {
            const string sp = "pkg_incriment.incriment_effected";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                     new CommandParameter() {ParameterName = "pSTART_DATE", Value = ob.START_DATE},
                     new CommandParameter() {ParameterName = "pEND_DATE", Value = ob.END_DATE},
                     //new CommandParameter() {ParameterName = "pRF_INCR_TYPE_ID", Value = ob.RF_INCR_TYPE_ID},
                     
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


        public object GetSpecialIncrMemoList(string pAUTH_NO = null, string pRF_INCR_TYPE_ID_LST = null, string pINCR_TYPE_NAME_EN = null, string pCOMP_NAME_EN = null,
            string pMEMO_NO = null, string pIS_FINALIZED = null, string pIS_CLOSED = null)
        {
            string sp = "pkg_incriment.hr_incr_memo_select";
            try
            {                
                var obList = new List<HR_INCR_MEMOModel>();
                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value =0},
                     new CommandParameter() {ParameterName = "pAUTH_NO", Value =pAUTH_NO},
                     new CommandParameter() {ParameterName = "pRF_INCR_TYPE_ID_LST", Value =pRF_INCR_TYPE_ID_LST},
                     new CommandParameter() {ParameterName = "pINCR_TYPE_NAME_EN", Value =pINCR_TYPE_NAME_EN},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pMEMO_NO", Value =pMEMO_NO},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value =pIS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value =pIS_CLOSED},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_INCR_MEMOModel ob = new HR_INCR_MEMOModel();
                    ob.HR_INCR_MEMO_ID = (dr["HR_INCR_MEMO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_INCR_MEMO_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_INCR_TYPE_ID = (dr["RF_INCR_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCR_TYPE_ID"]);
                    ob.AUTH_NO = (dr["AUTH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUTH_NO"]);
                    ob.MEMO_NO = (dr["MEMO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEMO_NO"]);
                    ob.MEMO_DT = (dr["MEMO_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MEMO_DT"]);
                    ob.HR_MANAGEMENT_TYPE_ID = (dr["HR_MANAGEMENT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MANAGEMENT_TYPE_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);

                    if (dr["HR_YR_INCR_H_ID"] != DBNull.Value)
                        ob.HR_YR_INCR_H_ID = (dr["HR_YR_INCR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_YR_INCR_H_ID"]);

                    ob.IS_BY_ADMIN = (dr["IS_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BY_ADMIN"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.INCR_TYPE_NAME_EN = (dr["INCR_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INCR_TYPE_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.MONTH_YEAR_NAME = (dr["MONTH_YEAR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_YEAR_NAME"]);
                    ob.START_DATE = (dr["START_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DATE"]);
                    ob.END_DATE = (dr["END_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DATE"]);                   

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