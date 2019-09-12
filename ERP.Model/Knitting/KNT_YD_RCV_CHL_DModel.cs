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
    public class KNT_YD_RCV_CHL_DModel
    {
        public Int64 KNT_YD_RCV_CHL_D_ID { get; set; }
        public Int64 KNT_YD_RCV_CHL_H_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 KNT_YDP_D_COL_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Decimal RCV_GYRN_QTY { get; set; }
        public Decimal RCV_YD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string YD_BATCH_NO { get; set; }
        public Int64 RCV_CONE_QTY { get; set; }
        public Int64 RCV_PACK_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public string REMARKS { get; set; }

        public string MC_ORDER_NO_LST { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string YR_SPEC_DESC { get; set; }
        public string YRN_LOT_NO { get; set; }
        public Decimal? ADV_RCV_QTY { get; set; }
        public Decimal? TOT_RCV_YD_QTY { get; set; }


        public List<KNT_YD_RCV_CHL_DModel> GetYdRecvChalnDtl(Int64? pKNT_YD_PRG_H_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pKNT_YD_RCV_CHL_H_ID = null)
        {
            string sp = "pkg_knt_yd_recv_chaln.knt_yd_rcv_chl_h_select";
            try
            {
                var obList = new List<KNT_YD_RCV_CHL_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value =pKNT_YD_PRG_H_ID},                     
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_CHL_H_ID", Value =pKNT_YD_RCV_CHL_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_RCV_CHL_DModel ob = new KNT_YD_RCV_CHL_DModel();
                    ob.KNT_YD_RCV_CHL_D_ID = (dr["KNT_YD_RCV_CHL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_CHL_D_ID"]);
                    ob.KNT_YD_RCV_CHL_H_ID = (dr["KNT_YD_RCV_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_CHL_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.RCV_GYRN_QTY = (dr["RCV_GYRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_GYRN_QTY"]);
                    ob.RCV_YD_QTY = (dr["RCV_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_YD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.YD_BATCH_NO = (dr["YD_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_BATCH_NO"]);
                    ob.RCV_CONE_QTY = (dr["RCV_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_CONE_QTY"]);
                    ob.RCV_PACK_QTY = (dr["RCV_PACK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_PACK_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.ADV_RCV_QTY = (dr["ADV_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_RCV_QTY"]);
                    ob.TOT_RCV_YD_QTY = (dr["TOT_RCV_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_RCV_YD_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YD_RCV_CHL_DModel Select(long ID)
        {
            string sp = "Select_KNT_YD_RCV_CHL_D";
            try
            {
                var ob = new KNT_YD_RCV_CHL_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_CHL_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YD_RCV_CHL_D_ID = (dr["KNT_YD_RCV_CHL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_CHL_D_ID"]);
                    ob.KNT_YD_RCV_CHL_H_ID = (dr["KNT_YD_RCV_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_CHL_H_ID"]);
                    ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.RCV_YD_QTY = (dr["RCV_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_YD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.YD_BATCH_NO = (dr["YD_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_BATCH_NO"]);
                    ob.RCV_CONE_QTY = (dr["RCV_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_CONE_QTY"]);
                    ob.RCV_PACK_QTY = (dr["RCV_PACK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_PACK_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
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