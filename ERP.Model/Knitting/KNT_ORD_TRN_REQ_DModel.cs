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
    public class KNT_ORD_TRN_REQ_DModel
    {
        public Int64 KNT_ORD_TRN_REQ_D_ID { get; set; }
        public Int64 KNT_ORD_TRN_REQ_H_ID { get; set; }
        public Int64 FAB_PROD_ORD_H_ID1 { get; set; }
        public Int64 FAB_COLOR_ID1 { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID1 { get; set; }
        public Decimal TRN_FAB_QTY { get; set; }
        public Int64 FAB_PROD_ORD_H_ID2 { get; set; }
        public Int64 FAB_COLOR_ID2 { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID2 { get; set; }
        public Decimal RCV_FAB_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }

        public string FROM_BYR_ACC_GRP_NAME_EN { get; set; }
        public string FROM_STYLE_NO { get; set; }
        public string FROM_ORDER_NO { get; set; }
        public string FROM_COLOR_NAME_EN { get; set; }
        public string FROM_FAB_ITEM_DESC { get; set; }

        public Int64? MC_FAB_PROD_ORD_D_ID2 { get; set; }
        public string TO_BYR_ACC_GRP_NAME_EN { get; set; }
        public string TO_STYLE_NO { get; set; }
        public string TO_ORDER_NO { get; set; }
        public string TO_COLOR_NAME_EN { get; set; }
        public string TO_FAB_ITEM_DESC { get; set; }
        


        public List<KNT_ORD_TRN_REQ_DModel> GetFabTransDtl(long pKNT_ORD_TRN_REQ_H_ID)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_h_select";
            try
            {
                var obList = new List<KNT_ORD_TRN_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ORD_TRN_REQ_H_ID", Value = pKNT_ORD_TRN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_ORD_TRN_REQ_DModel ob = new KNT_ORD_TRN_REQ_DModel();
                    ob.KNT_ORD_TRN_REQ_D_ID = (dr["KNT_ORD_TRN_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ORD_TRN_REQ_D_ID"]);
                    ob.KNT_ORD_TRN_REQ_H_ID = (dr["KNT_ORD_TRN_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ORD_TRN_REQ_H_ID"]);
                    ob.FAB_PROD_ORD_H_ID1 = (dr["FAB_PROD_ORD_H_ID1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_PROD_ORD_H_ID1"]);
                    ob.FAB_COLOR_ID1 = (dr["FAB_COLOR_ID1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID1"]);
                    ob.KNT_STYL_FAB_ITEM_ID1 = (dr["KNT_STYL_FAB_ITEM_ID1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID1"]);
                    ob.TRN_FAB_QTY = (dr["TRN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TRN_FAB_QTY"]);
                    ob.FAB_PROD_ORD_H_ID2 = (dr["FAB_PROD_ORD_H_ID2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_PROD_ORD_H_ID2"]);
                    ob.MC_FAB_PROD_ORD_D_ID2 = (dr["MC_FAB_PROD_ORD_D_ID2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID2"]);
                    ob.FAB_COLOR_ID2 = (dr["FAB_COLOR_ID2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID2"]);
                    ob.KNT_STYL_FAB_ITEM_ID2 = (dr["KNT_STYL_FAB_ITEM_ID2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID2"]);
                    ob.RCV_FAB_QTY = (dr["RCV_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.FROM_BYR_ACC_GRP_NAME_EN = (dr["FROM_BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_BYR_ACC_GRP_NAME_EN"]);
                    ob.FROM_STYLE_NO = (dr["FROM_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_STYLE_NO"]);
                    ob.FROM_ORDER_NO = (dr["FROM_ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ORDER_NO"]);
                    ob.FROM_COLOR_NAME_EN = (dr["FROM_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_COLOR_NAME_EN"]);
                    ob.FROM_FAB_ITEM_DESC = (dr["FROM_FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_FAB_ITEM_DESC"]);

                    ob.TO_BYR_ACC_GRP_NAME_EN = (dr["TO_BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_BYR_ACC_GRP_NAME_EN"]);
                    ob.TO_STYLE_NO = (dr["TO_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_STYLE_NO"]);
                    ob.TO_ORDER_NO = (dr["TO_ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ORDER_NO"]);
                    ob.TO_COLOR_NAME_EN = (dr["TO_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_COLOR_NAME_EN"]);
                    ob.TO_FAB_ITEM_DESC = (dr["TO_FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_FAB_ITEM_DESC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_ORD_TRN_REQ_DModel Select(long ID)
        {
            string sp = "Select_KNT_ORD_TRN_REQ_D";
            try
            {
                var ob = new KNT_ORD_TRN_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ORD_TRN_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_ORD_TRN_REQ_D_ID = (dr["KNT_ORD_TRN_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ORD_TRN_REQ_D_ID"]);
                    ob.KNT_ORD_TRN_REQ_H_ID = (dr["KNT_ORD_TRN_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ORD_TRN_REQ_H_ID"]);
                    ob.FAB_PROD_ORD_H_ID1 = (dr["FAB_PROD_ORD_H_ID1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_PROD_ORD_H_ID1"]);
                    ob.FAB_COLOR_ID1 = (dr["FAB_COLOR_ID1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID1"]);
                    ob.KNT_STYL_FAB_ITEM_ID1 = (dr["KNT_STYL_FAB_ITEM_ID1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID1"]);
                    ob.TRN_FAB_QTY = (dr["TRN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TRN_FAB_QTY"]);
                    ob.FAB_PROD_ORD_H_ID2 = (dr["FAB_PROD_ORD_H_ID2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_PROD_ORD_H_ID2"]);
                    ob.FAB_COLOR_ID2 = (dr["FAB_COLOR_ID2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID2"]);
                    ob.KNT_STYL_FAB_ITEM_ID2 = (dr["KNT_STYL_FAB_ITEM_ID2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID2"]);
                    ob.RCV_FAB_QTY = (dr["RCV_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

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