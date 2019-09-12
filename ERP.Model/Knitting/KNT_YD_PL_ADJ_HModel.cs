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
    public class KNT_YD_PL_ADJ_HModel
    {
        public Int64 KNT_YD_PL_ADJ_H_ID { get; set; }
        public string PL_ADJ_MEMO_NO { get; set; }
        public DateTime ADJ_DT { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 KNT_YD_PRG_H_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string PL_ADJ_DTL_XML { get; set; }
        public string IS_FINALIZED { get; set; }
        public string ACTION_USER_TYPE { get; set; }
        public string EVENT_NAME { get; set; }
        public string PRG_REF_NO { get; set; }


        public string BatchSave()
        {
            const string sp = "pkg_knt_yd_prg.knt_yd_pl_adj_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PL_ADJ_H_ID", Value = ob.KNT_YD_PL_ADJ_H_ID},
                     new CommandParameter() {ParameterName = "pPL_ADJ_MEMO_NO", Value = ob.PL_ADJ_MEMO_NO},
                     new CommandParameter() {ParameterName = "pADJ_DT", Value = ob.ADJ_DT},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pPL_ADJ_DTL_XML", Value = ob.PL_ADJ_DTL_XML},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     
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

        public object GetPlAdjList(int pageNumber, int pageSize, Int64? pSCM_SUPPLIER_ID, string pPL_ADJ_MEMO_NO = null, DateTime? pADJ_DT = null)
        {
            string sp = "pkg_knt_yd_prg.knt_yd_pl_adj_h_select";
            try
            {
                
                Int64 vTotalRec = 0;
                var obList = new List<KNT_YD_PL_ADJ_HModel>();
                var obj = new RF_PAGERModel();


                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pPL_ADJ_MEMO_NO", Value =pPL_ADJ_MEMO_NO},
                     new CommandParameter() {ParameterName = "pADJ_DT", Value =pADJ_DT},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_PL_ADJ_HModel ob = new KNT_YD_PL_ADJ_HModel();
                    ob.KNT_YD_PL_ADJ_H_ID = (dr["KNT_YD_PL_ADJ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PL_ADJ_H_ID"]);
                    ob.PL_ADJ_MEMO_NO = (dr["PL_ADJ_MEMO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PL_ADJ_MEMO_NO"]);
                    ob.ADJ_DT = (dr["ADJ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ADJ_DT"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);

                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);

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

        public KNT_YD_PL_ADJ_HModel GetPlAdjHdr(long pKNT_YD_PL_ADJ_H_ID)
        {
            string sp = "pkg_knt_yd_prg.knt_yd_pl_adj_h_select";
            try
            {
                var ob = new KNT_YD_PL_ADJ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PL_ADJ_H_ID", Value =pKNT_YD_PL_ADJ_H_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YD_PL_ADJ_H_ID = (dr["KNT_YD_PL_ADJ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PL_ADJ_H_ID"]);
                    ob.PL_ADJ_MEMO_NO = (dr["PL_ADJ_MEMO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PL_ADJ_MEMO_NO"]);
                    ob.ADJ_DT = (dr["ADJ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ADJ_DT"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

    }



    public class KNT_YDPL_ADJ_USR_LAVELModel
    {
        public string USER_DRAFT_NAME { get; set; }
        public string USER_VERIFIER_NAME { get; set; }


        public KNT_YDPL_ADJ_USR_LAVELModel GetPlAdjUserLavel()
        {
            string sp = "pkg_knt_yd_prg.knt_yd_pl_adj_h_select";
            try
            {
                var ob = new KNT_YDPL_ADJ_USR_LAVELModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.USER_DRAFT_NAME = (dr["USER_DRAFT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DRAFT_NAME"]);
                    ob.USER_VERIFIER_NAME = (dr["USER_VERIFIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_VERIFIER_NAME"]);
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