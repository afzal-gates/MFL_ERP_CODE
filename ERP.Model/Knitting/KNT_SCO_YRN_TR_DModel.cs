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
    public class KNT_SCO_YRN_TR_DModel
    {
        public Int64 KNT_SCO_YRN_TR_D_ID { get; set; }
        public Int64 KNT_SCO_YRN_TR_H_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Decimal PACK_TR_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal QTY_PER_PACK { get; set; }
        public Decimal LOOSE_QTY { get; set; }
        public Decimal TR_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string SP_NOTE { get; set; }
        public decimal INTACT_QTY { get; set; }
        public string PACK_TR_QTY_DESC { get; set; }
        public string YARN_DETAIL { get; set; }
        public string PACK_MOU_CODE { get; set; }
        public decimal? DUE_TR_QTY { get; set; }



        public List<KNT_SCO_YRN_TR_DModel> GetTransChlnYrnDtl(Int64 pKNT_SCO_YRN_TR_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_yrn_tr_h_select";
            try
            {
                var obList = new List<KNT_SCO_YRN_TR_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_YRN_TR_H_ID", Value = pKNT_SCO_YRN_TR_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_YRN_TR_DModel ob = new KNT_SCO_YRN_TR_DModel();
                    ob.KNT_SCO_YRN_TR_D_ID = (dr["KNT_SCO_YRN_TR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YRN_TR_D_ID"]);
                    ob.KNT_SCO_YRN_TR_H_ID = (dr["KNT_SCO_YRN_TR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YRN_TR_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_TR_QTY = (dr["PACK_TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_TR_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.TR_QTY = (dr["TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TR_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);

                    ob.YARN_DETAIL = (dr["YARN_DETAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAIL"]);
                    ob.PACK_TR_QTY_DESC = (dr["PACK_TR_QTY_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_TR_QTY_DESC"]);
                    ob.INTACT_QTY = (dr["INTACT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["INTACT_QTY"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                    ob.DUE_TR_QTY = (dr["DUE_TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DUE_TR_QTY"]);
          
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCO_YRN_TR_DModel Select(long ID)
        {
            string sp = "Select_KNT_SCO_YRN_TR_D";
            try
            {
                var ob = new KNT_SCO_YRN_TR_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_YRN_TR_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_YRN_TR_D_ID = (dr["KNT_SCO_YRN_TR_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YRN_TR_D_ID"]);
                    ob.KNT_SCO_YRN_TR_H_ID = (dr["KNT_SCO_YRN_TR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YRN_TR_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_TR_QTY = (dr["PACK_TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_TR_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.TR_QTY = (dr["TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TR_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);                   

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