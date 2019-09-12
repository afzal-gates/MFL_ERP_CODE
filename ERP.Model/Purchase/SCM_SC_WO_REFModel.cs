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
    public class SCM_SC_WO_REFModel
    {
        public Int64? SCM_SC_WO_REF_ID { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }
        public string SC_BUYER_NAME { get; set; }
        public string SC_ORDER_NO { get; set; }
        public string SC_STYLE_NO { get; set; }
        public string SC_WO_REF_NO { get; set; }


        public string Save()
        {
            const string sp = "pkg_knit_subcontract.scm_sc_wo_ref_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SC_WO_REF_ID", Value = ob.SCM_SC_WO_REF_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSC_BUYER_NAME", Value = ob.SC_BUYER_NAME},
                     new CommandParameter() {ParameterName = "pSC_ORDER_NO", Value = ob.SC_ORDER_NO},
                     new CommandParameter() {ParameterName = "pSC_STYLE_NO", Value = ob.SC_STYLE_NO},
                     new CommandParameter() {ParameterName = "pSC_WO_REF_NO", Value = ob.SC_WO_REF_NO},
                   
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

        public object GetScOrdRefByPartyID(Int64 pageNumber, Int64 pageSize, Int64? pSCM_SUPPLIER_ID, Int64? pKNT_SC_PRG_RCV_ID, string pSC_WO_REF_NO)
        {            
            string sp = "pkg_knit_subcontract.scm_sc_wo_ref_select";
            
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<SCM_SC_WO_REFModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = pKNT_SC_PRG_RCV_ID},
                     new CommandParameter() {ParameterName = "pSC_WO_REF_NO", Value = pSC_WO_REF_NO},

                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SC_WO_REFModel ob = new SCM_SC_WO_REFModel();
                    ob.SCM_SC_WO_REF_ID = (dr["SCM_SC_WO_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SC_WO_REF_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SC_BUYER_NAME = (dr["SC_BUYER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_BUYER_NAME"]);
                    ob.SC_ORDER_NO = (dr["SC_ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_ORDER_NO"]);
                    ob.SC_STYLE_NO = (dr["SC_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_STYLE_NO"]);
                    ob.SC_WO_REF_NO = (dr["SC_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_WO_REF_NO"]);

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



        public object GetScOrdRefByPrgID(long pKNT_SC_PRG_RCV_ID)
        {
            string sp = "pkg_knit_subcontract.scm_sc_wo_ref_select";
            try
            {                
                var obList = new List<SCM_SC_WO_REFModel>();
               
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = pKNT_SC_PRG_RCV_ID},                   
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SC_WO_REFModel ob = new SCM_SC_WO_REFModel();
                    ob.SCM_SC_WO_REF_ID = (dr["SCM_SC_WO_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SC_WO_REF_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SC_BUYER_NAME = (dr["SC_BUYER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_BUYER_NAME"]);
                    ob.SC_ORDER_NO = (dr["SC_ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_ORDER_NO"]);
                    ob.SC_STYLE_NO = (dr["SC_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_STYLE_NO"]);
                    ob.SC_WO_REF_NO = (dr["SC_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_WO_REF_NO"]);
                    
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