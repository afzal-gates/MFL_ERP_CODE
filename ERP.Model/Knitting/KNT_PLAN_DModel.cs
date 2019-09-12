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
    public class KNT_PLAN_DModel
    {
        public string KNT_YRN_LOT_ID_LST { get; set; }
        public Int64 KNT_PLAN_D_ID { get; set; }
        public Int64? KNT_PLAN_H_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public string IS_SKIP_LOT { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Int64 LK_YFAB_PART_ID { get; set; }
        public Decimal STITCH_LEN { get; set; }
        public Decimal RQD_YRN_QTY { get; set; }
        
        public Decimal RATIO_QTY { get; set; }
        public string YRN_NOTE { get; set; }


        public string BRAND_NAME_EN { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string LK_YFAB_PART_NAME { get; set; }
        public string YRN_LOT_NO { get; set; }


        public List<KNT_PLAN_DModel> QueryData(Int64? pKNT_PLAN_H_ID, Int64? pKNT_YRN_STR_REQ_H_ID = null, Int64? pLK_COL_TYPE_ID=null)
        {
            string sp = "pkg_knit_plan.knt_plan_d_select";
            try
            {
                var obList = new List<KNT_PLAN_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = pKNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pIS_SOLID", Value = (pLK_COL_TYPE_ID == 360 ? "Y" : "S")},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        KNT_PLAN_DModel ob = new KNT_PLAN_DModel();
                        ob.KNT_PLAN_D_ID = (dr["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_D_ID"]);
                        ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                        ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                        ob.IS_SKIP_LOT = (dr["IS_SKIP_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SKIP_LOT"]);
                        ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                        ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                        ob.LK_YFAB_PART_ID = (dr["LK_YFAB_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YFAB_PART_ID"]);
                        ob.STITCH_LEN = (dr["STITCH_LEN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STITCH_LEN"]);
                        
                        ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY"]);
                        ob.RQSTD_QTY = (dr["RQSTD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQSTD_QTY"]);                        
                        ob.RQD_YRN_QTY_OLD = ob.RQD_YRN_QTY;

                        ob.RATIO_QTY = (dr["RATIO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATIO_QTY"]);
                        ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);

                        ob.YRN_NOTE = (dr["YRN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_NOTE"]);

                        ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                        ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                        ob.LK_YFAB_PART_NAME = (dr["LK_YFAB_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YFAB_PART_NAME"]);
                        ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                        ob.KNT_YRN_LOT_ID_LST = (dr["KNT_YRN_LOT_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_ID_LST"]);
                                                
                        ob.RQSTD_NEW = (dr["RQSTD_NEW"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQSTD_NEW"]);
                        //ob.KNT_YRN_STR_REQ_D_ID = (dr["KNT_YRN_STR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_D_ID"]);
                        ob.RQD_CONE_QTY = (dr["RQD_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_CONE_QTY"]);

                        ob.RF_YRN_CNT_ID = (dr["RF_YRN_CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_CNT_ID"]);

                        ob.IS_DBL_PLY = (dr["IS_DBL_PLY"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_DBL_PLY"]);
                        ob.IS_USE_WIP = (dr["IS_USE_WIP"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_USE_WIP"]);
                        ob.IS_SOLID = (dr["IS_SOLID"] == DBNull.Value) ? "S" : Convert.ToString(dr["IS_SOLID"]);

                        ob.NO_OF_FDR = (dr["NO_OF_FDR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_FDR"]);
                        ob.MIN_KP_ASSIGN_KGS = (dr["MIN_KP_ASSIGN_KGS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_KP_ASSIGN_KGS"]);

                        obList.Add(ob);
                    }
                }
                else
                {

                        KNT_PLAN_DModel ob = new KNT_PLAN_DModel();
                        ob.KNT_PLAN_D_ID = -1;
                        ob.KNT_PLAN_H_ID = pKNT_PLAN_H_ID;
                        ob.IS_DBL_PLY = "N";
                        ob.IS_USE_WIP = "N";
                        ob.IS_SOLID = pLK_COL_TYPE_ID == 360 ? "Y" : "S";
                        ob.MIN_KP_ASSIGN_KGS = 0;
                        ob.guid = -1;
                        obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_PLAN_DModel Select(long ID)
        {
            string sp = "Select_KNT_PLAN_D";
            try
            {
                var ob = new KNT_PLAN_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.KNT_PLAN_D_ID = (dr["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_D_ID"]);
                        ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                        ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                        ob.IS_SKIP_LOT = (dr["IS_SKIP_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SKIP_LOT"]);
                        ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                        ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                        ob.LK_YFAB_PART_ID = (dr["LK_YFAB_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YFAB_PART_ID"]);
                        ob.STITCH_LEN = (dr["STITCH_LEN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STITCH_LEN"]);
                        ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY"]);
                        ob.YRN_NOTE = (dr["YRN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_NOTE"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal RQD_YRN_QTY_OLD { get; set; }

        public decimal ALOC_QTY { get; set; }
        public decimal RQSTD_QTY { get; set; }
        public decimal RQSTD_NEW { get; set; }
        public long KNT_YRN_STR_REQ_D_ID { get; set; }
        public long RQD_CONE_QTY { get; set; }
        public string IS_DBL_PLY { get; set; }
        public long RF_YRN_CNT_ID { get; set; }
        public string IS_USE_WIP { get; set; }
        public string IS_SOLID { get; set; }

        public long NO_OF_FDR { get; set; }

        public int? guid { get; set; }
        public long? MIN_KP_ASSIGN_KGS { get; set; }
    }
}