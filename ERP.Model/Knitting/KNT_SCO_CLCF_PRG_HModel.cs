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
    public class KNT_SCO_CLCF_PRG_HModel
    {
        public Int64 KNT_SCO_CLCF_PRG_H_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public string SCO_PRG_NO { get; set; }
        public DateTime SCO_PRG_DT { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 LK_SC_PRG_STATUS_ID { get; set; }
        public Int64? CPI { get; set; }
        public Int64? NO_PLY { get; set; }
        public string REMARKS { get; set; }
        public Int64? RF_FAB_TYPE_ID { get; set; }
        public string RF_GARM_PART_LST { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string SCO_CLCF_PRG_D_XML { get; set; }
        private List<KNT_SCO_CLCF_PRG_DModel> _clcfDtl = null;
        public List<KNT_SCO_CLCF_PRG_DModel> clcfDtl
        {
            get
            {
                if (_clcfDtl == null)
                {
                    _clcfDtl = new List<KNT_SCO_CLCF_PRG_DModel>();
                }
                return _clcfDtl;
            }
            set
            {
                _clcfDtl = value;
            }
        }


        public string ScoCollarCuffBatchSave()
        {
            const string sp = "pkg_knit_subcontract.knt_sco_clcf_prg_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = ob.KNT_SCO_CLCF_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSCO_PRG_NO", Value = ob.SCO_PRG_NO},
                     new CommandParameter() {ParameterName = "pSCO_PRG_DT", Value = ob.SCO_PRG_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_SC_PRG_STATUS_ID", Value = ob.LK_SC_PRG_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCPI", Value = ob.CPI},
                     new CommandParameter() {ParameterName = "pNO_PLY", Value = ob.NO_PLY},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pSCO_CLCF_PRG_D_XML", Value = ob.SCO_CLCF_PRG_D_XML},
                     
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

        public string ScoCollarCuffProgFinalize()
        {
            const string sp = "pkg_knit_subcontract.knt_sco_clcf_prg_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = ob.KNT_SCO_CLCF_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSCO_PRG_NO", Value = ob.SCO_PRG_NO},
                     new CommandParameter() {ParameterName = "pSCO_PRG_DT", Value = ob.SCO_PRG_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_SC_PRG_STATUS_ID", Value = ob.LK_SC_PRG_STATUS_ID},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string ScoCollarCuffProgCancel()
        {
            const string sp = "pkg_knit_subcontract.knt_sco_clcf_prg_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = ob.KNT_SCO_CLCF_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSCO_PRG_NO", Value = ob.SCO_PRG_NO},
                     new CommandParameter() {ParameterName = "pSCO_PRG_DT", Value = ob.SCO_PRG_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_SC_PRG_STATUS_ID", Value = ob.LK_SC_PRG_STATUS_ID},
                     new CommandParameter() {ParameterName = "pSCO_CLCF_PRG_D_XML", Value = ob.SCO_CLCF_PRG_D_XML},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1002},
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

        public object GetScoCollarCuffProgList(Int64 pageNumber, Int64 pageSize, Int64? pSCM_SUPPLIER_ID = null, Int64? pKNT_SCO_CLCF_PRG_H_ID = null, string pSCO_PRG_NO = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_clcf_prg_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SCO_CLCF_PRG_HModel>();
                var obj = new RF_PAGERModel();
                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = pKNT_SCO_CLCF_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pSCO_PRG_NO", Value = pSCO_PRG_NO},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CLCF_PRG_HModel ob = new KNT_SCO_CLCF_PRG_HModel();
                    ob.KNT_SCO_CLCF_PRG_H_ID = (dr["KNT_SCO_CLCF_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.SCO_PRG_NO = (dr["SCO_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCO_PRG_NO"]);
                    ob.SCO_PRG_DT = (dr["SCO_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SCO_PRG_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                    if (dr["CPI"] != DBNull.Value)
                        ob.CPI = (dr["CPI"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CPI"]);
                    if (dr["NO_PLY"] != DBNull.Value)
                        ob.NO_PLY = (dr["NO_PLY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_PLY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    
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

        public KNT_SCO_CLCF_PRG_HModel GetScoCollarCuffHdr(Int64 pKNT_SCO_CLCF_PRG_H_ID, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_clcf_prg_h_select";
            try
            {
                var ob = new KNT_SCO_CLCF_PRG_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = pKNT_SCO_CLCF_PRG_H_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_CLCF_PRG_H_ID = (dr["KNT_SCO_CLCF_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.SCO_PRG_NO = (dr["SCO_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCO_PRG_NO"]);
                    ob.SCO_PRG_DT = (dr["SCO_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SCO_PRG_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                    if (dr["CPI"] != DBNull.Value)
                        ob.CPI = (dr["CPI"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CPI"]);
                    if (dr["NO_PLY"] != DBNull.Value)
                        ob.NO_PLY = (dr["NO_PLY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_PLY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                    
                    //var obScoClcfDtlList = new KNT_SCO_CLCF_PRG_DModel().GetScoCollarCuffDtl(pKNT_SCO_CLCF_PRG_H_ID, pMC_FAB_PROD_ORD_H_ID);
                    //ob.clcfDtl = obScoClcfDtlList;
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