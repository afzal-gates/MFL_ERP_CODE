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
    public class KNT_YD_RCV_DModel
    {
        public Int64 KNT_YD_RCV_D_ID { get; set; }
        public Int64 KNT_YD_RCV_H_ID { get; set; }
        public Int64 YRN_COLOR_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Decimal RCV_YD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string YD_BATCH_NO { get; set; }
        public Int64 RCV_CONE_QTY { get; set; }
        public Int64 RCV_PACK_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal RCV_GYRN_QTY { get; set; }
        public string REMARKS { get; set; }

        public string COLOR_NAME_EN { get; set; }
        public string YR_SPEC_DESC { get; set; }
        public decimal RQD_YD_QTY { get; set; }
        public decimal TTL_RECV { get; set; }



        public List<KNT_YD_RCV_DModel> SelectAll(Int64 pKNT_YD_PRG_H_ID, Int64? pKNT_YD_RCV_H_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_rcv_h_select";
            try
            {
                var obList = new List<KNT_YD_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_H_ID", Value = pKNT_YD_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = pKNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},

                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_RCV_DModel ob = new KNT_YD_RCV_DModel();
                    ob.KNT_YD_RCV_D_ID = (dr["KNT_YD_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_D_ID"]);
                    ob.KNT_YD_RCV_H_ID = (dr["KNT_YD_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_H_ID"]);
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.RCV_YD_QTY = (dr["RCV_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_YD_QTY"]);
                    ob.RCV_YD_QTY_ORI = ob.RCV_YD_QTY;
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.YD_BATCH_NO = (dr["YD_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_BATCH_NO"]);
                    ob.RCV_CONE_QTY = (dr["RCV_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_CONE_QTY"]);
                    ob.RCV_PACK_QTY = (dr["RCV_PACK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_PACK_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.RCV_GYRN_QTY = (dr["RCV_GYRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_GYRN_QTY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);

                    ob.RQD_YD_QTY = (dr["RQD_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YD_QTY"]);
                    ob.TTL_RECV = (dr["TTL_RECV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RECV"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YD_RCV_DModel Select(long ID)
        {
            string sp = "Select_KNT_YD_RCV_D";
            try
            {
                var ob = new KNT_YD_RCV_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_RCV_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YD_RCV_D_ID = (dr["KNT_YD_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_D_ID"]);
                    ob.KNT_YD_RCV_H_ID = (dr["KNT_YD_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_RCV_H_ID"]);
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.RCV_YD_QTY = (dr["RCV_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_YD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.YD_BATCH_NO = (dr["YD_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_BATCH_NO"]);
                    ob.RCV_CONE_QTY = (dr["RCV_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_CONE_QTY"]);
                    ob.RCV_PACK_QTY = (dr["RCV_PACK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_PACK_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_YD_RCV_DModel> SearchApprovedBatchNo(string pYD_BATCH_NO, Int32? pOption)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_rcv_h_select";
            try
            {
                var obList = new List<KNT_YD_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pYD_BATCH_NO", Value = pYD_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption ?? 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_RCV_DModel ob = new KNT_YD_RCV_DModel();
                    ob.YD_BATCH_NO = (dr["YD_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_BATCH_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal RCV_YD_QTY_ORI { get; set; }
    }
}