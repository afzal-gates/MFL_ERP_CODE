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
    public class KNT_SC_YRN_RCV_DModel
    {
        public Int64 KNT_SC_YRN_RCV_D_ID { get; set; }
        public Int64 KNT_SC_YRN_RCV_H_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 LK_YRN_COLR_GRP_ID { get; set; }
        public Decimal CTN_RCV_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal QTY_PER_CTN { get; set; }
        public Decimal LOOSE_QTY { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public Decimal LOC_UNIT_PRICE { get; set; }
        public string SP_NOTE { get; set; }

        public Int64 RF_BRAND_ID { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string YRN_COLR_GRP_NAME { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string YRN_LOT_NO { get; set; }
        public string QTY_MOU_CODE { get; set; }
        public string PACK_MOU_CODE { get; set; }
        public Int64 SCM_SC_WO_REF_ID { get; set; }
        public string SC_WO_REF_NO { get; set; }
        public string YR_SPEC_DESC { get; set; }
        public Decimal RQD_FAB_QTY { get; set; }
        public Decimal CUMU_RCV_YRN_QTY { get; set; }
        public Decimal BAL_YRN_QTY { get; set; }
        

        public object GetScYrnRcvDtlListByHrdID(long pKNT_SC_YRN_RCV_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sc_prg_rcv_select";
            try
            {
                var obList = new List<KNT_SC_YRN_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_H_ID", Value = pKNT_SC_YRN_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                    
                    KNT_SC_YRN_RCV_DModel ob = new KNT_SC_YRN_RCV_DModel();
                    ob.KNT_SC_YRN_RCV_D_ID = (dr["KNT_SC_YRN_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_YRN_RCV_D_ID"]);
                    ob.KNT_SC_YRN_RCV_H_ID = (dr["KNT_SC_YRN_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_YRN_RCV_H_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.CTN_RCV_QTY = (dr["CTN_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CTN_RCV_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_CTN = (dr["QTY_PER_CTN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_CTN"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.LOC_UNIT_PRICE = (dr["LOC_UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_UNIT_PRICE"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);

                    ob.SCM_SC_WO_REF_ID = (dr["SCM_SC_WO_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SC_WO_REF_ID"]);
                    ob.SC_WO_REF_NO = (dr["SC_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_WO_REF_NO"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);

                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YRN_COLR_GRP_NAME = (dr["YRN_COLR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP_NAME"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);

                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FAB_QTY"]);
                    ob.CUMU_RCV_YRN_QTY = (dr["CUMU_RCV_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CUMU_RCV_YRN_QTY"]);
                    ob.BAL_YRN_QTY = (dr["BAL_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_YRN_QTY"]);

                    obList.Add(ob);
                }
                                    
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetYrnSummery(long? pSCM_SC_WO_REF_ID, long? pYARN_ITEM_ID, long? pKNT_YRN_LOT_ID, 
            long? pLK_YFAB_PART_ID, long? pKNT_SC_YRN_RCV_D_ID, DateTime? pCHALAN_DT)
        {
            string sp = "pkg_knit_subcontract.knt_sc_prg_rcv_select";
            try
            {                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SC_WO_REF_ID", Value = pSCM_SC_WO_REF_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = pYARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = pKNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pLK_YFAB_PART_ID", Value = pLK_YFAB_PART_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = pCHALAN_DT},
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_D_ID", Value = pKNT_SC_YRN_RCV_D_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                KNT_SC_YRN_RCV_DModel ob = new KNT_SC_YRN_RCV_DModel();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                                        
                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FAB_QTY"]);
                    ob.CUMU_RCV_YRN_QTY = (dr["CUMU_RCV_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CUMU_RCV_YRN_QTY"]);
                    ob.BAL_YRN_QTY = (dr["BAL_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_YRN_QTY"]);                    
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