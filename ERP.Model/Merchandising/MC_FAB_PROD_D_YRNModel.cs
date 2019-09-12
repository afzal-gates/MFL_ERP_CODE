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
    public class MC_FAB_PROD_D_YRNModel
    {
        public Int64 MC_FAB_PROD_D_YRN_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_D_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64? KNT_YRN_LOT_ID { get; set; }
        public Int64? LK_YFAB_PART_ID { get; set; }
        public Decimal STITCH_LEN { get; set; }
        public Decimal? PCT_ELA_QTY { get; set; }
        public Decimal RATIO_QTY { get; set; }
        public Decimal RQD_YRN_QTY { get; set; }
        public string YR_SPEC_DESC { get; set; }
        public Int64? RF_BRAND_ID { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string YRN_LOT_NO { get; set; }
        public string FAB_PART_GRP_NAME_EN { get; set; }
        public Int64? LK_YRN_COLR_GRP_ID { get; set; }
        public string COLOR_GRP_NAME_EN { get; set; }
        public object YR_SPEC
        {
            get
            {
                return new { YARN_ITEM_ID = this.YARN_ITEM_ID, YR_SPEC_DESC = this.YR_SPEC_DESC ?? "" };
            }
        }
        public object CAT_BRAND
        {
            get
            {
                return new { RF_BRAND_ID = this.RF_BRAND_ID, BRAND_NAME_EN = this.BRAND_NAME_EN ?? "" };
            }
        }
        public object FAB_PART
        {
            get
            {
                return new { LK_YFAB_PART_ID = this.LK_YFAB_PART_ID, FAB_PART_GRP_NAME_EN = this.FAB_PART_GRP_NAME_EN ?? "" };
            }
        }
        public object COLOR_GRP
        {
            get
            {
                return new { LK_YRN_COLR_GRP_ID = this.LK_YRN_COLR_GRP_ID, COLOR_GRP_NAME_EN = this.COLOR_GRP_NAME_EN ?? "" };
            }
        }
        public bool editMode { get; set; }






        public List<MC_FAB_PROD_D_YRNModel> GetYrnListByFabOrd(Int64 pMC_FAB_PROD_ORD_D_ID)
        {
            string sp = "PKG_FAB_PROD_ORDER.mc_fab_prod_d_yrn_select";
            try
            {
                var obList = new List<MC_FAB_PROD_D_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_D_ID", Value = pMC_FAB_PROD_ORD_D_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_D_YRNModel ob = new MC_FAB_PROD_D_YRNModel();
                    ob.MC_FAB_PROD_D_YRN_ID = (dr["MC_FAB_PROD_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_D_YRN_ID"]);
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    if (dr["LK_YFAB_PART_ID"] != DBNull.Value)
                        ob.LK_YFAB_PART_ID = (dr["LK_YFAB_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YFAB_PART_ID"]);
                    ob.STITCH_LEN = (dr["STITCH_LEN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STITCH_LEN"]);
                    ob.PCT_ELA_QTY = (dr["PCT_ELA_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ELA_QTY"]);
                    ob.RATIO_QTY = (dr["RATIO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATIO_QTY"]);
                    ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY"]);

                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    if (dr["LK_YRN_COLR_GRP_ID"] != DBNull.Value)
                        ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.FAB_PART_GRP_NAME_EN = (dr["FAB_PART_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PART_GRP_NAME_EN"]);

                    ob.editMode = false;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public object GetYrnByScOrdRefID(long pSCM_SC_WO_REF_ID)
        {
            string sp = "PKG_FAB_PROD_ORDER.mc_fab_prod_d_yrn_select";
            try
            {
                var obList = new List<MC_FAB_PROD_D_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SC_WO_REF_ID", Value = pSCM_SC_WO_REF_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_D_YRNModel ob = new MC_FAB_PROD_D_YRNModel();
                    //ob.MC_FAB_PROD_D_YRN_ID = (dr["MC_FAB_PROD_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_D_YRN_ID"]);
                    //ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    //ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    
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