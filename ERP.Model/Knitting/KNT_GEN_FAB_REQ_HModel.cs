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
    public class KNT_GEN_FAB_REQ_HModel
    {
        public Int64 KNT_GEN_FAB_REQ_H_ID { get; set; }
        public string GFAB_REQ_NO { get; set; }
        public DateTime GFAB_REQ_DT { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 GFAB_REQ_BY { get; set; }
        public Int64 GFAB_REQ_TO { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        
        public Int64? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public string MC_ORDER_H_ID_LST { get; set; }

        public string GEN_FAB_REQ_DTL_XML { get; set; }

        public string REQ_BY_NAME { get; set; }
        public string REQ_TO_EMP_CODE { get; set; }
        public string REQ_TO_EMP_PROFILE { get; set; }
        public string IS_FINALIZE { get; set; }
        public string REQ_TYPE_NAME { get; set; }
        public string ACTN_STATUS_NAME { get; set; }



        public string BatchSave()
        {
            const string sp = "PKG_FAB_PROD_ORDER.knt_gen_fab_req_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_GEN_FAB_REQ_H_ID", Value = ob.KNT_GEN_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pGFAB_REQ_NO", Value = ob.GFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pGFAB_REQ_DT", Value = ob.GFAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pGFAB_REQ_BY", Value = ob.GFAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pGFAB_REQ_TO", Value = ob.GFAB_REQ_TO},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pGEN_FAB_REQ_DTL_XML", Value = ob.GEN_FAB_REQ_DTL_XML},
                     new CommandParameter() {ParameterName = "pIS_FINALIZE", Value = ob.IS_FINALIZE},
                     
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



        public object GetFabReqList(int pageNumber, int pageSize, string pGFAB_REQ_NO = null, DateTime? pFROM_DT = null, DateTime? pTO_DT = null)
        {
            string sp = "PKG_FAB_PROD_ORDER.knt_gen_fab_req_h_select";
            try
            {                
                Int64 vTotalRec = 0;
                var obList = new List<KNT_GEN_FAB_REQ_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                     new CommandParameter() {ParameterName = "pFROM_DT", Value =pFROM_DT},
                     new CommandParameter() {ParameterName = "pTO_DT", Value =pTO_DT},
                     new CommandParameter() {ParameterName = "pGFAB_REQ_NO", Value =pGFAB_REQ_NO},

                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_GEN_FAB_REQ_HModel ob = new KNT_GEN_FAB_REQ_HModel();
                    ob.KNT_GEN_FAB_REQ_H_ID = (dr["KNT_GEN_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_GEN_FAB_REQ_H_ID"]);
                    ob.GFAB_REQ_NO = (dr["GFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GFAB_REQ_NO"]);
                    ob.GFAB_REQ_DT = (dr["GFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["GFAB_REQ_DT"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.GFAB_REQ_BY = (dr["GFAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GFAB_REQ_BY"]);
                    ob.GFAB_REQ_TO = (dr["GFAB_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GFAB_REQ_TO"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);

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

        public KNT_GEN_FAB_REQ_HModel GetFabReqHdr(Int64 pKNT_GEN_FAB_REQ_H_ID)
        {
            string sp = "PKG_FAB_PROD_ORDER.knt_gen_fab_req_h_select";
            try
            {
                var ob = new KNT_GEN_FAB_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_GEN_FAB_REQ_H_ID", Value =pKNT_GEN_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_GEN_FAB_REQ_H_ID = (dr["KNT_GEN_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_GEN_FAB_REQ_H_ID"]);
                    ob.GFAB_REQ_NO = (dr["GFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GFAB_REQ_NO"]);
                    ob.GFAB_REQ_DT = (dr["GFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["GFAB_REQ_DT"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.GFAB_REQ_BY = (dr["GFAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GFAB_REQ_BY"]);
                    ob.GFAB_REQ_TO = (dr["GFAB_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GFAB_REQ_TO"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    if (dr["MC_BYR_ACC_GRP_ID"] != DBNull.Value)
                        ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);

                    if (dr["MC_STYLE_H_EXT_ID"] != DBNull.Value)
                        ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);

                    ob.REQ_BY_NAME = (dr["REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_BY_NAME"]);
                    ob.REQ_TO_EMP_CODE = (dr["REQ_TO_EMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TO_EMP_CODE"]);
                    ob.REQ_TO_EMP_PROFILE = (dr["REQ_TO_EMP_PROFILE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TO_EMP_PROFILE"]);
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