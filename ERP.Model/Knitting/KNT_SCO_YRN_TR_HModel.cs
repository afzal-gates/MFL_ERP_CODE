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
    public class KNT_SCO_YRN_TR_HModel
    {
        public Int64? KNT_SCO_YRN_TR_H_ID { get; set; }
        public Int64? FRM_SUPPLIER_ID { get; set; }
        public Int64? KNT_SCO_CHL_RCV_H_ID { get; set; }
        public Int64? TO_SUPPLIER_ID { get; set; }
        public Int64? KNT_SC_PRG_ISS_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }
        public string FRM_SUP_TRD_NAME_EN { get; set; }
        public string TO_SUP_TRD_NAME_EN { get; set; }
        public string YRN_TRANS_DTL_XML { get; set; }


        public string BatchSave4ScoYrnTransfer()
        {
            const string sp = "pkg_knit_subcontract.knt_sco_yrn_tr_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_YRN_TR_H_ID", Value = ob.KNT_SCO_YRN_TR_H_ID},
                     new CommandParameter() {ParameterName = "pFRM_SUPPLIER_ID", Value = ob.FRM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = ob.KNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pTO_SUPPLIER_ID", Value = ob.TO_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pYRN_TRANS_DTL_XML", Value = ob.YRN_TRANS_DTL_XML},
                     
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



        public object GetTransChlnList(int pageNumber, int pageSize, int? pSCM_SUPPLIER_ID, string pCHALAN_NO, DateTime? pCHALAN_DT, string pSTYLE_NO, string pORDER_NO_LST)
        {
            string sp = "pkg_knit_subcontract.knt_sco_yrn_tr_h_select";
            try
            {                
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SCO_YRN_TR_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = pCHALAN_DT},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_YRN_TR_HModel ob = new KNT_SCO_YRN_TR_HModel();
                    ob.KNT_SCO_YRN_TR_H_ID = (dr["KNT_SCO_YRN_TR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YRN_TR_H_ID"]);
                    ob.FRM_SUPPLIER_ID = (dr["FRM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_SUPPLIER_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.TO_SUPPLIER_ID = (dr["TO_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_SUPPLIER_ID"]);
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.FRM_SUP_TRD_NAME_EN = (dr["FRM_SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FRM_SUP_TRD_NAME_EN"]);
                    ob.TO_SUP_TRD_NAME_EN = (dr["TO_SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_SUP_TRD_NAME_EN"]);

                    
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

        public KNT_SCO_YRN_TR_HModel GetTransChlnHdr(long pKNT_SCO_YRN_TR_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_yrn_tr_h_select";
            try
            {
                var ob = new KNT_SCO_YRN_TR_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_YRN_TR_H_ID", Value = pKNT_SCO_YRN_TR_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_YRN_TR_H_ID = (dr["KNT_SCO_YRN_TR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YRN_TR_H_ID"]);
                    ob.FRM_SUPPLIER_ID = (dr["FRM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_SUPPLIER_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.TO_SUPPLIER_ID = (dr["TO_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_SUPPLIER_ID"]);
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);                   
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