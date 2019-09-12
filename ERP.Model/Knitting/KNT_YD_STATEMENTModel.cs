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
    public class KNT_YD_STATEMENTModel
    {
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 KNT_YD_PRG_H_ID { get; set; }
        public Int64 KNT_YD_RCV_H_ID { get; set; }
        public Int64 YRN_COLOR_ID { get; set; }
        public Int64 ORDH_ID_COLOR_SPAN { get; set; }
        public Int64 ORDH_ID_COLOR_SL { get; set; }
        public Int64 ORDH_ID_SPAN { get; set; }
        public Int64 ORDH_ID_SL { get; set; }
        public Int64 ORDH_ID_SUP_SPAN { get; set; }
        public Int64 ORDH_ID_SUP_SL { get; set; }
        public Int64 ORDH_ID_PROG_SPAN { get; set; }
        public Int64 ORDH_ID_PROG_SL { get; set; }
        public Int64 ORDH_ID_RECV_SPAN { get; set; }
        public Int64 ORDH_ID_RECV_SL { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public DateTime SHIP_DT { get; set; }
        public DateTime PROD_ORD_DT { get; set; }
        public string PRG_REF_NO { get; set; }
        public DateTime PRG_ISS_DT { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string CL_WO_REF_NO { get; set; }
        public string YD_COLOR_NAME { get; set; }
        public string YD_BATCH_NO { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public string PACK_MOU { get; set; }
        public Int64 RQD_YD_QTY { get; set; }
        public Int64 RCV_YD_QTY { get; set; }
        public Int64 RCV_PACK_QTY { get; set; }
        public Int64 TOT_RCV_YD_QTY { get; set; }

        

        private List<dynamic> _yarns = null;
        public List<dynamic> yarns
        {
            get
            {
                if (_yarns == null)
                {
                    _yarns = new List<dynamic>();
                }
                return _yarns;
            }
            set
            {
                this._yarns = value;
            }
        }

        public RF_PAGERModel Query(
             Int32 pageNumber,
             Int32 pageSize,
             Int64? pRF_FAB_PROD_CAT_ID,
             Int64? pSCM_SUPPLIER_ID,
             Int64? pMC_FAB_PROD_ORD_H_ID,
             Int64? pMC_BYR_ACC_ID,
             DateTime? pFIRSTDATE,
             DateTime? pLASTDATE,
             string pPCT_DONE_CODE,
             Int64? pKNT_YD_PRG_H_ID

            )
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_prg_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<KNT_YD_STATEMENTModel>();
                OraDatabase db = new OraDatabase();
                Int64 vTotalRec = 0;
                var obj = new RF_PAGERModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},
                     new CommandParameter() {ParameterName = "pPCT_DONE_CODE", Value = pPCT_DONE_CODE},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = pKNT_YD_PRG_H_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3014},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_STATEMENTModel ob = new KNT_YD_STATEMENTModel();
                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.KNT_YD_RCV_H_ID = (dr["KNT_YD_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_H_ID"]);
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                    ob.ORDH_ID_COLOR_SL = (dr["ORDH_ID_COLOR_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_COLOR_SL"]);
                    ob.ORDH_ID_COLOR_SPAN = (dr["ORDH_ID_COLOR_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_COLOR_SPAN"]);

                    ob.ORDH_ID_SPAN = (dr["ORDH_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_SPAN"]);
                    ob.ORDH_ID_SL = (dr["ORDH_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_SL"]);
                    ob.ORDH_ID_SUP_SPAN = (dr["ORDH_ID_SUP_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_SUP_SPAN"]);
                    ob.ORDH_ID_SUP_SL = (dr["ORDH_ID_SUP_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_SUP_SL"]);
                    ob.ORDH_ID_PROG_SPAN = (dr["ORDH_ID_PROG_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_PROG_SPAN"]);
                    ob.ORDH_ID_PROG_SL = (dr["ORDH_ID_PROG_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_PROG_SL"]);
                    ob.ORDH_ID_RECV_SPAN = (dr["ORDH_ID_RECV_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_RECV_SPAN"]);
                    ob.ORDH_ID_RECV_SL = (dr["ORDH_ID_RECV_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDH_ID_RECV_SL"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.PROD_ORD_DT = (dr["PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_ORD_DT"]);
                    ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);
                    ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.CL_WO_REF_NO = (dr["CL_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CL_WO_REF_NO"]);
                    ob.YD_COLOR_NAME = (dr["YD_COLOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_COLOR_NAME"]);
                    ob.YD_BATCH_NO = (dr["YD_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_BATCH_NO"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.PACK_MOU = (dr["PACK_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU"]);
                    ob.RQD_YD_QTY = (dr["RQD_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_YD_QTY"]);
                    ob.RCV_YD_QTY = (dr["RCV_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_YD_QTY"]);
                    ob.RCV_PACK_QTY = (dr["RCV_PACK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_PACK_QTY"]);

                    ob.TOT_RCV_YD_QTY = (dr["TOT_RCV_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_RCV_YD_QTY"]);
                    

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                        new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                                        new CommandParameter() {ParameterName = "pYRN_COLOR_ID", Value = ob.YRN_COLOR_ID},
                                        new CommandParameter() {ParameterName = "pOption", Value = 3015},
                                        new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                    }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr1["YARN"] != DBNull.Value)
                        {
                            ob.yarns.Add(
                                   new
                                   {
                                       YARN = Convert.ToString(dr1["YARN"])
                                   }
                            );
                        }

                    }
                    obList.Add(ob);
                }
                obj.data = obList;
                obj.total = vTotalRec;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}