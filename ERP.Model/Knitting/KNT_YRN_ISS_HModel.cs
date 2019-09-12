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
    public class KNT_YRN_ISS_HModel
    {
        public Int64 KNT_YRN_ISS_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 ITEM_ISS_BY { get; set; }
        public Int64 KNT_YRN_STR_REQ_H_ID { get; set; }
        public string ISS_CHALAN_NO { get; set; }
        public DateTime ISS_CHALAN_DT { get; set; }
        public Int64 LK_REQ_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public string TRUCK_NO { get; set; }
        public string DRIVER_NAME { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string XML_ISS_D { get; set; }
       
        public string Save()
        {
            const string sp = "pkg_knit_yarn_issue.knt_yrn_iss_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_H_ID", Value = ob.KNT_YRN_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_ISS_BY", Value = ob.ITEM_ISS_BY},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pISS_CHALAN_NO", Value = ob.ISS_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pISS_CHALAN_DT", Value = ob.ISS_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED==null?"N": ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pTRUCK_NO", Value = ob.TRUCK_NO},
                     new CommandParameter() {ParameterName = "pDRIVER_NAME", Value = ob.DRIVER_NAME},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pXML_ISS_D", Value = ob.XML_ISS_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opKNT_YRN_ISS_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "SP_KNT_YRN_ISS_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_H_ID", Value = ob.KNT_YRN_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_ISS_BY", Value = ob.ITEM_ISS_BY},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pISS_CHALAN_NO", Value = ob.ISS_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pISS_CHALAN_DT", Value = ob.ISS_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pLK_REQ_STATUS_ID", Value = ob.LK_REQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_KNT_YRN_ISS_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_H_ID", Value = ob.KNT_YRN_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_ISS_BY", Value = ob.ITEM_ISS_BY},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pISS_CHALAN_NO", Value = ob.ISS_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pISS_CHALAN_DT", Value = ob.ISS_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pLK_REQ_STATUS_ID", Value = ob.LK_REQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<KNT_YRN_ISS_HModel> SelectAll(int pageNo, int pageSize, string pISS_CHALAN_NO = null, string pISS_CHALAN_DT = null, string pCOMP_NAME_EN = null,
            string pSTR_REQ_NO = null, string pSTORE_NAME_EN = null, string pREQ_STATUS = null, string pREMARKS = null, Int64? pRF_REQ_TYPE_ID=null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_iss_h_select";
            try
            {
                var obList = new List<KNT_YRN_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pISS_CHALAN_NO", Value =pISS_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pISS_CHALAN_DT", Value =pISS_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value =pSTR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTORE_NAME_EN", Value =pSTORE_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_STATUS", Value =pREQ_STATUS},
                     new CommandParameter() {ParameterName = "pREMARKS", Value =pREMARKS},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},

                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_ISS_HModel ob = new KNT_YRN_ISS_HModel();
                    ob.KNT_YRN_ISS_H_ID = (dr["KNT_YRN_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.ISS_CHALAN_NO = (dr["ISS_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_CHALAN_NO"]);
                    ob.ISS_CHALAN_DT = (dr["ISS_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_CHALAN_DT"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    //ob.REQ_STATUS = (dr["REQ_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_STATUS"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YRN_ISS_HModel Select(Int64? ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_iss_h_select";
            try
            {
                var ob = new KNT_YRN_ISS_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_ISS_H_ID = (dr["KNT_YRN_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.ISS_CHALAN_NO = (dr["ISS_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_CHALAN_NO"]);
                    ob.ISS_CHALAN_DT = (dr["ISS_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_CHALAN_DT"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.TRUCK_NO = (dr["TRUCK_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRUCK_NO"]);
                    ob.DRIVER_NAME = (dr["DRIVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRIVER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    //ob.REQ_STATUS = (dr["REQ_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_STATUS"]);

                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_ISS_HModel> SelectByReqID(Int64? pKNT_YRN_STR_REQ_H_ID = null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_iss_h_select";
            try
            {
                var obList = new List<KNT_YRN_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = pKNT_YRN_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_ISS_HModel ob = new KNT_YRN_ISS_HModel();
                    ob.KNT_YRN_ISS_H_ID = (dr["KNT_YRN_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.ISS_CHALAN_NO = (dr["ISS_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_CHALAN_NO"]);
                    ob.ISS_CHALAN_DT = (dr["ISS_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_CHALAN_DT"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    //ob.REQ_STATUS = (dr["REQ_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_STATUS"]);
                    //ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object YarnIssueChallanAuto(string pCHALAN_NO)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_iss_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_YRN_ISS_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = pCHALAN_NO},                     

                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_ISS_HModel ob = new KNT_YRN_ISS_HModel();
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.ISS_CHALAN_NO = (dr["ISS_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_CHALAN_NO"]);
                    ob.ISS_CHALAN_DT = (dr["ISS_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_CHALAN_DT"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string STR_REQ_NO { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string REQ_STATUS { get; set; }
        public int TOTAL_REC { get; set; }
        public DateTime STR_REQ_DT { get; set; }
        public string USER_NAME_EN { get; set; }
        public string REQ_TYPE_NAME { get; set; }
        public string IS_FINALIZED { get; set; }

        public long RF_REQ_TYPE_ID { get; set; }
    }
}