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
    public class CM_UD_HModel
    {
        public Int64 CM_UD_H_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public string UD_NO { get; set; }
        public string AMND_NO { get; set; }
        public DateTime UD_DT { get; set; }
        public Int64 RF_GMT_ASN_ID { get; set; }
        public Int64? CM_EXP_LC_H_ID { get; set; }
        public Int64? CM_IMP_LC_H_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }

        public int TOTAL_REC { get; set; }
        public string IMP_LC_NO { get; set; }
        public string EXP_LCSC_NO { get; set; }
        public string GMT_ASN_SNAME { get; set; }
        public string GMT_ASN_NAME_EN { get; set; }
        public string COMP_NAME_EN { get; set; }
        public Int64 MC_BUYER_ID { get; set; }

        public string IS_FINALIZED { get; set; }
        public string XML_UD_D { get; set; }


        public string Save()
        {
            const string sp = "PKG_CM_UD.CM_UD_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value = ob.CM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID==0?null:ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     
                     new CommandParameter() {ParameterName = "pUD_NO", Value = ob.UD_NO},
                     new CommandParameter() {ParameterName = "pAMND_NO", Value = ob.AMND_NO},
                     new CommandParameter() {ParameterName = "pUD_DT", Value = ob.UD_DT},
                     new CommandParameter() {ParameterName = "pRF_GMT_ASN_ID", Value = ob.RF_GMT_ASN_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML_UD_D", Value = ob.XML_UD_D},
                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_CM_UD.CM_UD_H_UPDATE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value = ob.CM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID==0?null:ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pUD_NO", Value = ob.UD_NO},
                     new CommandParameter() {ParameterName = "pAMND_NO", Value = ob.AMND_NO},
                     new CommandParameter() {ParameterName = "pUD_DT", Value = ob.UD_DT},
                     new CommandParameter() {ParameterName = "pRF_GMT_ASN_ID", Value = ob.RF_GMT_ASN_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML_UD_D", Value = ob.XML_UD_D},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_CM_UD.CM_UD_H_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value = ob.CM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pUD_NO", Value = ob.UD_NO},
                     new CommandParameter() {ParameterName = "pAMND_NO", Value = ob.AMND_NO},
                     new CommandParameter() {ParameterName = "pUD_DT", Value = ob.UD_DT},
                     new CommandParameter() {ParameterName = "pRF_GMT_ASN_ID", Value = ob.RF_GMT_ASN_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
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

        public List<CM_UD_HModel> SelectAll(Int64? pSCM_SUPPLIER_ID = null, Int64? pMC_BUYER_ID = null, string pLC_NO = null, string pUD_NO = null)
        {
            string sp = "PKG_CM_UD.CM_UD_H_SELECT";
            try
            {
                var obList = new List<CM_UD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pLC_NO", Value =pLC_NO},
                     new CommandParameter() {ParameterName = "pUD_NO", Value =pUD_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_UD_HModel ob = new CM_UD_HModel();
                    ob.CM_UD_H_ID = (dr["CM_UD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_UD_H_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.UD_NO = (dr["UD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UD_NO"]);
                    ob.AMND_NO = (dr["AMND_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AMND_NO"]);
                    ob.UD_DT = (dr["UD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UD_DT"]);
                    ob.RF_GMT_ASN_ID = (dr["RF_GMT_ASN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GMT_ASN_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.GMT_ASN_NAME_EN = (dr["GMT_ASN_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_NAME_EN"]);
                    ob.GMT_ASN_SNAME = (dr["GMT_ASN_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_ASN_SNAME"]);
                    ob.EXP_LCSC_NO = (dr["EXP_LCSC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXP_LCSC_NO"]);
                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CM_UD_HModel Select(Int64? pCM_UD_H_ID = null)
        {
            string sp = "PKG_CM_UD.CM_UD_H_SELECT";
            try
            {
                var ob = new CM_UD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value =pCM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_UD_H_ID = (dr["CM_UD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_UD_H_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.UD_NO = (dr["UD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UD_NO"]);
                    ob.AMND_NO = (dr["AMND_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AMND_NO"]);
                    ob.UD_DT = (dr["UD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UD_DT"]);
                    ob.RF_GMT_ASN_ID = (dr["RF_GMT_ASN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GMT_ASN_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

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