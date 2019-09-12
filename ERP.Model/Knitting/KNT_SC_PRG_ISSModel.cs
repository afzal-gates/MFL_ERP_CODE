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
    public class KNT_SC_PRG_ISSModel
    {
        public Int64 KNT_SC_PRG_ISS_ID { get; set; }
        public string PRG_ISS_NO { get; set; }
        public DateTime SC_PRG_DT { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 LK_SC_PRG_STATUS_ID { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string SC_PRG_STATUS_NAME { get; set; }
        public string IS_REQS_DONE { get; set; }
        public long CNT_KNT_CRD { get; set; }
        public object FRM_SUPPLIER_ID { get; set; }
        public object IS_TI_TE { get; set; }

        public string FAB_PROD_CAT_SNAME { get; set; }

        private string _IS_YD = null;
        public string IS_YD { 
            get {
                if (_IS_YD == null)
                {
                    _IS_YD = "N";
                }
                return _IS_YD;
            }
            set
            {
                _IS_YD = value ?? "N";
            }
        }
       
      

        public string Save()
        {
            const string sp = "pkg_knit_plan.knt_sc_prg_iss_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pPRG_ISS_NO", Value = ob.PRG_ISS_NO},
                     new CommandParameter() {ParameterName = "pIS_YD", Value = ob.IS_YD},
                     new CommandParameter() {ParameterName = "pIS_TI_TE", Value = ob.IS_TI_TE},
                     new CommandParameter() {ParameterName = "pFRM_SUPPLIER_ID", Value = ob.FRM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSC_PRG_DT", Value = ob.SC_PRG_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_SC_PRG_STATUS_ID", Value = ob.LK_SC_PRG_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_SC_PRG_ISS_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_knit_plan.knt_sc_prg_iss_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
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

        public RF_PAGERModel SelectAll(Int64 pageNumber, Int64 pageSize, Int64? pRF_FAB_PROD_CAT_ID, Int64? pSCM_SUPPLIER_ID, Int64? pMC_BYR_ACC_ID, Int64? pMC_BUYER_ID, Int64? pMC_STYLE_H_EXT_ID, string pPRG_ISS_NO, string pSC_PRG_DT,
            string pSUP_TRD_NAME_EN, string pSC_PRG_STATUS_NAME, Int64? pKNT_SC_PRG_ISS_ID = null, string pIS_TI_TE = null, string pIS_YD = null, string pRF_FAB_PROD_CAT_ID_LST = null)
        {
            string sp = "pkg_knit_plan.knt_sc_prg_iss_select_paginate";
            try
            {
                var obList = new List<KNT_SC_PRG_ISSModel>();
                Int64 vTotalRec = 0;
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pPRG_ISS_NO", Value =pPRG_ISS_NO},
                     new CommandParameter() {ParameterName = "pSC_PRG_DT", Value =pSC_PRG_DT},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_EN", Value =pSUP_TRD_NAME_EN},
                     new CommandParameter() {ParameterName = "pSC_PRG_STATUS_NAME", Value = pSC_PRG_STATUS_NAME},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = pKNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pIS_TI_TE", Value = pIS_TI_TE },
                     new CommandParameter() {ParameterName = "pIS_YD", Value = pIS_YD},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID_LST", Value = pRF_FAB_PROD_CAT_ID_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_PRG_ISSModel ob = new KNT_SC_PRG_ISSModel();
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.PRG_ISS_NO = (dr["PRG_ISS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ISS_NO"]);
                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                    ob.SC_PRG_STATUS_NAME = (dr["SC_PRG_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_STATUS_NAME"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.IS_REQS_DONE = (dr["IS_REQS_DONE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_REQS_DONE"]);

                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }
                    ob.CNT_KNT_CRD = (dr["CNT_KNT_CRD"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CNT_KNT_CRD"]);

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

        public KNT_SC_PRG_ISSModel Select(Int64? pKNT_SC_PRG_ISS_ID)
        {
            string sp = "pkg_knit_plan.knt_sc_prg_iss_select";
            try
            {
                var ob = new KNT_SC_PRG_ISSModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value =pKNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.PRG_ISS_NO = (dr["PRG_ISS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ISS_NO"]);
                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.IS_REQS_DONE = (dr["IS_REQS_DONE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_REQS_DONE"]);
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