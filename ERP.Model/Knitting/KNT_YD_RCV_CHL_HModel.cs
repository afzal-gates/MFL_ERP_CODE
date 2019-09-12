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
    public class KNT_YD_RCV_CHL_HModel
    {
        public Int64 KNT_YD_RCV_CHL_H_ID { get; set; }
        public Int64 KNT_YD_PRG_H_ID { get; set; }        
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public string CL_WO_REF_NO { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string INVOICE_NO { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }
        public string IS_TRANSFER { get; set; }
        public Int64? TR_PARTY_ID { get; set; }
        
        public string KNT_YD_RCV_CHL_D_XML { get; set; }


        public string BatchSave()
        {
            const string sp = "pkg_knt_yd_recv_chaln.knt_yd_rcv_chl_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_CHL_H_ID", Value = ob.KNT_YD_RCV_CHL_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = ob.KNT_YD_PRG_H_ID},                     
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pCL_WO_REF_NO", Value = ob.CL_WO_REF_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pINVOICE_NO", Value = ob.INVOICE_NO},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_TRANSFER", Value = ob.IS_TRANSFER},
                     new CommandParameter() {ParameterName = "pTR_PARTY_ID", Value = ob.TR_PARTY_ID},
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_CHL_D_XML", Value = ob.KNT_YD_RCV_CHL_D_XML},
                     
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


        public object GetYdRecvChalnList(Int32 pageNumber, Int32 pageSize, Int64? pSCM_SUPPLIER_ID, string pCHALAN_NO, DateTime? pCHALAN_DT)
        {
            string sp = "pkg_knt_yd_recv_chaln.knt_yd_rcv_chl_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_YD_RCV_CHL_HModel>();
                var obj = new RF_PAGERModel();
                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value =pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value =pCHALAN_DT},
                     
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_RCV_CHL_HModel ob = new KNT_YD_RCV_CHL_HModel();
                    ob.KNT_YD_RCV_CHL_H_ID = (dr["KNT_YD_RCV_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_CHL_H_ID"]);
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CL_WO_REF_NO = (dr["CL_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CL_WO_REF_NO"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_TRANSFER = (dr["IS_TRANSFER"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFER"]);

                    if (dr["TR_PARTY_ID"] != DBNull.Value)
                        ob.TR_PARTY_ID = (dr["TR_PARTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TR_PARTY_ID"]);

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

        public KNT_YD_RCV_CHL_HModel GetYdRecvChalnHdr(long pKNT_YD_RCV_CHL_H_ID)
        {
            string sp = "pkg_knt_yd_recv_chaln.knt_yd_rcv_chl_h_select";
            try
            {
                var ob = new KNT_YD_RCV_CHL_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_CHL_H_ID", Value =pKNT_YD_RCV_CHL_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YD_RCV_CHL_H_ID = (dr["KNT_YD_RCV_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_CHL_H_ID"]);
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CL_WO_REF_NO = (dr["CL_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CL_WO_REF_NO"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_TRANSFER = (dr["IS_TRANSFER"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFER"]);

                    if (dr["TR_PARTY_ID"] != DBNull.Value)
                        ob.TR_PARTY_ID = (dr["TR_PARTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TR_PARTY_ID"]);
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