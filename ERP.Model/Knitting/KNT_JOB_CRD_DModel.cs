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
    public class KNT_JOB_CRD_DModel
    {
        public Int64 KNT_JOB_CRD_D_ID { get; set; }
        public Int64 KNT_JOB_CRD_H_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 LK_YFAB_PART_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Decimal RQD_YRN_QTY_JC { get; set; }

        public Decimal STITCH_LEN { get; set; }
        public Decimal RQD_YRN_QTY_KP { get; set; }
        public Decimal RATIO_QTY { get; set; }
        public string YRN_NOTE { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string LK_YFAB_PART_NAME { get; set; }
        public string YRN_LOT_NO { get; set; }


        public string KNIT_CARD_D { get; set; }
        public Int32 LOC_ROW_SPAN { get; set; }
        public Int32 LOC_ROW_NUM { get; set; }
        public Int32 MC_ROW_SPAN { get; set; }
        public Int32 MC_ROW_NUM { get; set; }
        public Int32 YARN_ROW_SPAN { get; set; }
        public Int32 YARN_ROW_NUM { get; set; }


        public string LOCATION_NAME { get; set; }
        public string KNT_MACHINE_NO { get; set; }


        public string YARN_DETAILS { get; set; }


        public Decimal RQSTD_QTY { get; set; }
        public Decimal CONSMD_QTY { get; set; }
        public DateTime START_DT { get; set; }
        public DateTime END_DT { get; set; }






        public List<KNT_JOB_CRD_DModel> QueryData(Int64? pKNT_PLAN_H_ID, Int64? pKNT_JOB_CRD_H_ID)
        {
            string sp = "pkg_knit_plan.knt_job_crd_d_select";
            try
            {
                var obList = new List<KNT_JOB_CRD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value =pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value =pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_JOB_CRD_DModel ob = new KNT_JOB_CRD_DModel();
                    ob.KNT_JOB_CRD_D_ID = (dr["KNT_JOB_CRD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_D_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.LK_YFAB_PART_ID = (dr["LK_YFAB_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YFAB_PART_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.KNT_PLAN_D_ID = (dr["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_D_ID"]);

                    ob.RQD_YRN_QTY_JC = (dr["RQD_YRN_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY_JC"]);
                    ob.RQD_YRN_QTY_KP = (dr["RQD_YRN_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY_KP"]);
                    ob.STITCH_LEN = (dr["STITCH_LEN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STITCH_LEN"]);
                    ob.RATIO_QTY = (dr["RATIO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATIO_QTY"]);
                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);

                    ob.LK_YFAB_PART_NAME = (dr["LK_YFAB_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YFAB_PART_NAME"]);

                    ob.RQD_CONE_QTY = (dr["RQD_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RQD_CONE_QTY"]);
                    //ob.KNT_YRN_STR_REQ_D_ID = (dr["KNT_YRN_STR_REQ_D_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_D_ID"]);
                    ob.NO_OF_FDR = (dr["NO_OF_FDR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_FDR"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic getYarnDetailsData(Int64? pKNT_PLAN_H_ID, Int64? pKNT_JOB_CRD_H_ID)
        {
            string sp = "pkg_knit_plan.knt_job_crd_d_select";
            try
            {
                var obList = new List<KNT_JOB_CRD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value =pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value =pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_JOB_CRD_DModel ob = new KNT_JOB_CRD_DModel();

                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    ob.RQD_YRN_QTY_JC = (dr["RQD_YRN_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY_JC"]);
                    ob.RQD_YRN_QTY_KP = (dr["RQD_YRN_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY_KP"]);

                    ob.REQ_DN_YRN_QTY_JC = (dr["REQ_DN_YRN_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_DN_YRN_QTY_JC"]);
                    ob.REQ_DN_YRN_QTY_KP = (dr["REQ_DN_YRN_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_DN_YRN_QTY_KP"]);
                    ob.ISSUE_QTY_JC = (dr["ISSUE_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY_JC"]);
                    ob.ISSUE_QTY_KP = (dr["ISSUE_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY_KP"]);

                    ob.ADV_ISSUE_QTY = (dr["ADV_ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_ISSUE_QTY"]);
                    ob.YARN_STOCK = (dr["YARN_STOCK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["YARN_STOCK"]);


                    ob.ISSUE_RET_QTY_KP = (dr["ISSUE_RET_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_RET_QTY_KP"]);
                    ob.ISSUE_RET_QTY_JC = (dr["ISSUE_RET_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_RET_QTY_JC"]);


                    ob.ALOC_QTY = (dr["ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALOC_QTY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);

                    obList.Add(ob);
                }
                return new {
                    yarn_details = obList,
                    TTL_RQD_YRN_QTY_KP = obList.Sum(x => x.REQ_DN_YRN_QTY_KP),
                    TTL_REQ_DN_YRN_QTY_KP = obList.Sum(x => x.REQ_DN_YRN_QTY_KP),
                    TTL_ISSUE_QTY_KP = obList.Sum(x => x.ISSUE_QTY_KP),
                    TTL_RQD_YRN_QTY_JC = obList.Sum(x => x.RQD_YRN_QTY_JC),
                    TTL_REQ_DN_YRN_QTY_JC = obList.Sum(x => x.REQ_DN_YRN_QTY_JC),
                    TTL_ISSUE_QTY_JC = obList.Sum(x => x.ISSUE_QTY_JC),
                    TTL_ISSUE_RET_QTY_KP = obList.Sum(x => x.ISSUE_RET_QTY_KP),
                    TTL_ISSUE_RET_QTY_JC = obList.Sum(x => x.ISSUE_RET_QTY_JC)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<KNT_JOB_CRD_DModel> GetYarnRequisitionData(string pKNT_JOB_CRD_H_LIST, string pKNT_PLAN_H_LST)
        {
            string sp = "PKG_MC_LOAD_PLAN.YARN_REQ_IN_HOUSE_PROD";
            try
            {
                var obList = new List<KNT_JOB_CRD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_LIST", Value =pKNT_JOB_CRD_H_LIST},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_LST", Value =pKNT_PLAN_H_LST},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_JOB_CRD_DModel ob = new KNT_JOB_CRD_DModel();
                    ob.KNT_JOB_CRD_D_ID = (dr["KNT_JOB_CRD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_D_ID"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);

                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    ob.LOC_ROW_SPAN = (dr["LOC_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LOC_ROW_SPAN"]);
                    ob.LOC_ROW_NUM = (dr["LOC_ROW_NUM"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LOC_ROW_NUM"]);
                    ob.MC_ROW_SPAN = (dr["MC_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_ROW_SPAN"]);
                    ob.MC_ROW_NUM = (dr["MC_ROW_NUM"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_ROW_NUM"]);
                    ob.YARN_ROW_SPAN = (dr["YARN_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["YARN_ROW_SPAN"]);
                    ob.YARN_ROW_NUM = (dr["YARN_ROW_NUM"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["YARN_ROW_NUM"]);


                    ob.START_DT = (dr["START_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["START_DT"]);
                    ob.END_DT = (dr["END_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["END_DT"]);

  

                    ob.RQD_YRN_QTY_JC = (dr["RQD_YRN_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY_JC"]);
                    ob.RQD_YRN_QTY_KP = (dr["RQD_YRN_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY_KP"]);

                    ob.REQ_DN_YRN_QTY_JC = (dr["REQ_DN_YRN_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_DN_YRN_QTY_JC"]);
                    ob.REQ_DN_YRN_QTY_KP = (dr["REQ_DN_YRN_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_DN_YRN_QTY_KP"]);

                    ob.ISSUE_QTY_KP = (dr["ISSUE_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY_KP"]);

                    ob.ADV_ISSUE_QTY = (dr["ADV_ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_ISSUE_QTY"]);
                    ob.ISSUE_RET_QTY_KP = (dr["ISSUE_RET_QTY_KP"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_RET_QTY_KP"]);

                    ob.KNIT_CARD_D = (dr["KNIT_CARD_D"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNIT_CARD_D"]);
                    ob.LOCATION_NAME = (dr["LOCATION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOCATION_NAME"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.YARN_DETAILS = (dr["YARN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAILS"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.NEW_RQSTD_QTY = (ob.RQD_YRN_QTY_KP - ob.REQ_DN_YRN_QTY_KP + ob.ISSUE_RET_QTY_KP) > ob.RQD_YRN_QTY_JC ? ob.RQD_YRN_QTY_JC : (ob.RQD_YRN_QTY_KP - ob.REQ_DN_YRN_QTY_KP + ob.ISSUE_RET_QTY_KP);
                    ob.MAX_RQSTD_QTY = ob.NEW_RQSTD_QTY; 

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_JOB_CRD_DModel Select(long ID)
        {
            string sp = "Select_KNT_JOB_CRD_D";
            try
            {
                var ob = new KNT_JOB_CRD_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_JOB_CRD_D_ID = (dr["KNT_JOB_CRD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_D_ID"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.LK_YFAB_PART_ID = (dr["LK_YFAB_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YFAB_PART_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal RQD_YRN_QTY { get; set; }

        public long KNT_PLAN_D_ID { get; set; }

        public decimal RCVD_YRN_QTY { get; set; }

        public int RQD_CONE_QTY { get; set; }

        public Int64 KNT_YRN_STR_REQ_D_ID { get; set; }

        public long MC_FAB_PROD_ORD_H_ID { get; set; }

        public long NO_OF_FDR { get; set; }

        public decimal ALOC_QTY { get; set; }

        public decimal REQ_DN_YRN_QTY_JC { get; set; }

        public decimal REQ_DN_YRN_QTY_KP { get; set; }

        public decimal ISSUE_QTY_JC { get; set; }

        public decimal ISSUE_QTY_KP { get; set; }

        public decimal ADV_ISSUE_QTY { get; set; }

        public decimal YARN_STOCK { get; set; }

        public decimal ISSUE_RET_QTY_KP { get; set; }

        public decimal ISSUE_RET_QTY_JC { get; set; }

        public decimal NEW_RQSTD_QTY { get; set; }

        public decimal MAX_RQSTD_QTY { get; set; }
    }
}