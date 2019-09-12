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
    public class KNT_YD_PL_ADJ_DModel
    {
        public Int64 KNT_YD_PL_ADJ_D_ID { get; set; }
        public Int64 KNT_YD_PL_ADJ_H_ID { get; set; }
        public Int64 YRN_COLOR_ID { get; set; }
        public Int64? RF_BRAND_ID { get; set; }
        public Int64? KNT_YRN_LOT_ID { get; set; }
        public Int64 PL_ADJ_QTY { get; set; }

        public Int64? KNT_YD_PRG_H_ID { get; set; }
        public string PRG_REF_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string YARN_DETAIL { get; set; }


        public List<KNT_YD_PL_ADJ_DModel> GetPlAdjDtl(Int64? pKNT_YD_PL_ADJ_H_ID)
        {
            string sp = "pkg_knt_yd_prg.knt_yd_pl_adj_h_select";
            try
            {
                var obList = new List<KNT_YD_PL_ADJ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PL_ADJ_H_ID", Value =pKNT_YD_PL_ADJ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_PL_ADJ_DModel ob = new KNT_YD_PL_ADJ_DModel();
                    
                    ob.KNT_YD_PL_ADJ_D_ID = (dr["KNT_YD_PL_ADJ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PL_ADJ_D_ID"]);
                    ob.KNT_YD_PL_ADJ_H_ID = (dr["KNT_YD_PL_ADJ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PL_ADJ_H_ID"]);
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);

                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.YARN_DETAIL = (dr["YARN_DETAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAIL"]);

                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PL_ADJ_QTY = (dr["PL_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PL_ADJ_QTY"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YD_PL_ADJ_DModel Select(long ID)
        {
            string sp = "Select_KNT_YD_PL_ADJ_D";
            try
            {
                var ob = new KNT_YD_PL_ADJ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PL_ADJ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YD_PL_ADJ_D_ID = (dr["KNT_YD_PL_ADJ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PL_ADJ_D_ID"]);
                    ob.KNT_YD_PL_ADJ_H_ID = (dr["KNT_YD_PL_ADJ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PL_ADJ_H_ID"]);
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PL_ADJ_QTY = (dr["PL_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PL_ADJ_QTY"]);
                   
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