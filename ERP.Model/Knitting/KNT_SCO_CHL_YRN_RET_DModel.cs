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
    public class KNT_SCO_CHL_YRN_RET_DModel
    {
        public Int64 KNT_SCO_CHL_YRN_RET_D_ID { get; set; }
        public Int64 KNT_SCO_CHL_RCV_H_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 LK_YRN_RET_TYPE_ID { get; set; }
        public Int64 LK_YRN_RET_COZ_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Decimal? PACK_RET_QTY { get; set; }
        public Int64? PACK_MOU_ID { get; set; }
        public Decimal? QTY_PER_PACK { get; set; }
        public Decimal? LOOSE_QTY { get; set; }
        public Decimal? INTACT_QTY { get; set; }
        public Decimal RET_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public string IS_TRANSFER { get; set; }
        public string SP_NOTE { get; set; }
        public string RETURN_TYPE_DESC { get; set; }
        public string RETURN_COZ_DESC { get; set; }
        public string STYLE_NO { get; set; }
        public string YARN_DETAIL { get; set; }
        public string PACK_MOU_CODE { get; set; }
        public string PACK_TR_QTY_DESC { get; set; }
        public long? PACK_TR_QTY { get; set; }
        public long? TR_QTY { get; set; }
        public decimal? DUE_TR_QTY { get; set; }



        public List<KNT_SCO_CHL_YRN_RET_DModel> GetYrnRcvList(Int64 pKNT_SCO_CHL_RCV_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_yrn_ret_d_select";
            try
            {
                var obList = new List<KNT_SCO_CHL_YRN_RET_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = pKNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_YRN_RET_DModel ob = new KNT_SCO_CHL_YRN_RET_DModel();
                    ob.KNT_SCO_CHL_YRN_RET_D_ID = (dr["KNT_SCO_CHL_YRN_RET_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_YRN_RET_D_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.LK_YRN_RET_TYPE_ID = (dr["LK_YRN_RET_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_RET_TYPE_ID"]);
                    ob.LK_YRN_RET_COZ_ID = (dr["LK_YRN_RET_COZ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_RET_COZ_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_RET_QTY = (dr["PACK_RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RET_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RET_QTY = (dr["RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RET_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.IS_TRANSFER = (dr["IS_TRANSFER"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFER"]);

                    ob.RETURN_TYPE_DESC = (dr["RETURN_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RETURN_TYPE_DESC"]);
                    ob.RETURN_COZ_DESC = (dr["RETURN_COZ_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RETURN_COZ_DESC"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.YARN_DETAIL = (dr["YARN_DETAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAIL"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCO_CHL_YRN_RET_DModel Select(long ID)
        {
            string sp = "Select_KNT_SCO_CHL_YRN_RET_D";
            try
            {
                var ob = new KNT_SCO_CHL_YRN_RET_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_YRN_RET_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_CHL_YRN_RET_D_ID = (dr["KNT_SCO_CHL_YRN_RET_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_YRN_RET_D_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.LK_YRN_RET_TYPE_ID = (dr["LK_YRN_RET_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_RET_TYPE_ID"]);
                    ob.LK_YRN_RET_COZ_ID = (dr["LK_YRN_RET_COZ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_RET_COZ_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_RET_QTY = (dr["PACK_RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RET_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RET_QTY = (dr["RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RET_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.IS_TRANSFER = (dr["IS_TRANSFER"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFER"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_SCO_CHL_YRN_RET_DModel> GetChlnWiseTransferedYrn(Int64 pKNT_SCO_CHL_RCV_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_yrn_ret_d_select";
            try
            {
                var obList = new List<KNT_SCO_CHL_YRN_RET_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = pKNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_YRN_RET_DModel ob = new KNT_SCO_CHL_YRN_RET_DModel();
                    //ob.KNT_SCO_CHL_YRN_RET_D_ID = (dr["KNT_SCO_CHL_YRN_RET_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_YRN_RET_D_ID"]);
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    //ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    //ob.LK_YRN_RET_TYPE_ID = (dr["LK_YRN_RET_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_RET_TYPE_ID"]);
                    //ob.LK_YRN_RET_COZ_ID = (dr["LK_YRN_RET_COZ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_RET_COZ_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_RET_QTY = (dr["PACK_RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RET_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.INTACT_QTY = (dr["INTACT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INTACT_QTY"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RET_QTY = (dr["RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RET_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    //ob.IS_TRANSFER = (dr["IS_TRANSFER"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFER"]);
                    
                    //ob.RETURN_TYPE_DESC = (dr["RETURN_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RETURN_TYPE_DESC"]);
                    //ob.RETURN_COZ_DESC = (dr["RETURN_COZ_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RETURN_COZ_DESC"]);
                    //ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.YARN_DETAIL = (dr["YARN_DETAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAIL"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);

                    ob.PACK_TR_QTY_DESC = (dr["PACK_RET_QTY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_RET_QTY"]) + " " + Convert.ToString(dr["PACK_MOU_CODE"]);
                    ob.PACK_TR_QTY = (dr["PACK_RET_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_RET_QTY"]);
                    ob.TR_QTY = (dr["RET_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RET_QTY"]);

                    ob.DUE_TR_QTY = (dr["RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RET_QTY"]);


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