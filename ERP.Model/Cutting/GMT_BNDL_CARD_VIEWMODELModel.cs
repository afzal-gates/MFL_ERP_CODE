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
    public class GMT_BNDL_CARD_VIEWMODELModel
    {

        public Int64 GMT_CUT_PNL_INSPTN_H_ID { get; set; }
        public Int64 GMT_CUT_INFO_ID { get; set; }
        public Int64 SL { get; set; }
        public Int32 RF_GARM_PART_ID { get; set; }
        public string GARM_PART_NAME { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string COUNTRY_NAME_EN { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }
        public Int64 CUTNG_NO { get; set; }
        public Int64 BUNDLE_NO { get; set; }
        public string SL_RANGE_TXT { get; set; }
        public Int64 QTY_IN_BNDL { get; set; }
        public long TTL_BNDL_CUTTING { get; set; }
        public long TTL_BNDL_INSPECTED { get; set; }
        public string BUNDLE_BARCODE { get; set; }
        public long TTL_BNDL_OK { get; set; }
        public int HAS_RECUT_PROB { get; set; }
        public Int64 GMT_CUT_PNL_RECUT_ID { get; set; }
        public string DYE_LOT_NO { get; set; }
        public int TTL_BNDL_PENDING { get; set; }
        public long TTL_BNDL_HAVING_REJ_PNL { get; set; }
        public long TTL_BNDL_REPLACED { get; set; }
        public int TTL_BNDL_HV_RECUT_PROB { get; set; }
        public int TTL_BNDL_HV_NO_RECUT_PROB { get; set; }
        public string OK_CUT_PNL_RECUT_LST { get; set; }

        //public List<Int32> RF_GARM_PART_LST { get; set; }

        public long GMT_BNDL_CRD_PDATA_ID { get; set; }



        public List<GMT_BNDL_CARD_VIEWMODELModel> details { get; set; }
        public List<GMT_CUT_PNL_INSPTN_DModel> defects { get; set; }
        public List<GMT_CUT_PNL_RECUTModel> recuts { get; set; }

        public List<GMT_BNDL_CARD_VIEWMODELModel> Query(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_BNDL_CARD_VIEWMODELModel ob = new GMT_BNDL_CARD_VIEWMODELModel();
                            ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                            ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                            ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                            ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                            ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_GARM_PART_ID"]);
                            ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);

                            ob.TTL_BNDL_CUTTING = (dr["TTL_BNDL_CUTTING"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_BNDL_CUTTING"]);
                            ob.TTL_BNDL_INSPECTED = (dr["TTL_BNDL_INSPECTED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_BNDL_INSPECTED"]);
                            ob.TTL_BNDL_OK = (dr["TTL_BNDL_OK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_BNDL_OK"]);

                            ob.details = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                            ob.details = this.QueryDetials(ob.GMT_CUT_INFO_ID, ob.RF_GARM_PART_ID, pGMT_PROD_PLN_CLNDR_ID);
                            ob.TTL_BNDL_PENDING = ob.details.Count();
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<GMT_BNDL_CARD_VIEWMODELModel> QueryDetials(Int64 pGMT_CUT_INFO_ID, Int32 pRF_GARM_PART_ID, Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = pRF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_VIEWMODELModel ob = new GMT_BNDL_CARD_VIEWMODELModel();
                    ob.GMT_CUT_PNL_INSPTN_H_ID = (dr["GMT_CUT_PNL_INSPTN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_INSPTN_H_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_BNDL_CARD_VIEWMODELModel> SummaryDataForRecut(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_VIEWMODELModel ob = new GMT_BNDL_CARD_VIEWMODELModel();
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_GARM_PART_ID"]);
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);

    
               
                    ob.details = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                    ob.details = this.QueryDetialsForRecut(ob.GMT_CUT_INFO_ID, ob.RF_GARM_PART_ID, pGMT_PROD_PLN_CLNDR_ID);
                    ob.TTL_BNDL_HV_RECUT_PROB = ob.details.Where(x => x.HAS_RECUT_PROB ==1).Count();
                    ob.TTL_BNDL_HV_NO_RECUT_PROB = ob.details.Where(x => x.HAS_RECUT_PROB == 0).Count();
                    ob.OK_CUT_PNL_RECUT_LST = string.Join(",", ob.details.Where(x => x.HAS_RECUT_PROB == 0).Select(x => x.GMT_CUT_PNL_RECUT_ID).ToArray());

                    ob.TTL_BNDL_HAVING_REJ_PNL = (dr["TTL_BNDL_HAVING_REJ_PNL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_BNDL_HAVING_REJ_PNL"]);
                    ob.TTL_BNDL_REPLACED = (dr["TTL_BNDL_REPLACED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_BNDL_REPLACED"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        

        public List<GMT_BNDL_CARD_VIEWMODELModel> QueryDetialsForRecut(Int64 pGMT_CUT_INFO_ID, Int32 pRF_GARM_PART_ID, Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = pRF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_VIEWMODELModel ob = new GMT_BNDL_CARD_VIEWMODELModel();
                    ob.GMT_CUT_PNL_RECUT_ID = (dr["GMT_CUT_PNL_RECUT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_RECUT_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    ob.HAS_RECUT_PROB = (dr["HAS_RECUT_PROB"] == DBNull.Value) ? -1 : Convert.ToInt32(dr["HAS_RECUT_PROB"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<GMT_BNDL_CARD_VIEWMODELModel> GetBundleListOfRejectedPanel(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_VIEWMODELModel ob = new GMT_BNDL_CARD_VIEWMODELModel();
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_GARM_PART_ID"]);
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.GMT_CUT_PNL_INSPTN_H_ID = (dr["GMT_CUT_PNL_INSPTN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_INSPTN_H_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);


                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    ob.defects = new List<GMT_CUT_PNL_INSPTN_DModel>();
                    ob.defects = new GMT_CUT_PNL_INSPTN_DModel().Query(ob.GMT_CUT_PNL_INSPTN_H_ID);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public GMT_BNDL_CARD_VIEWMODELModel Select(Int64 pGMT_CUT_PNL_INSPTN_H_ID, Int32? pOption)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new GMT_BNDL_CARD_VIEWMODELModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_INSPTN_H_ID", Value = pGMT_CUT_PNL_INSPTN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption ?? 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                //3001 => For CutPanel Inspection
                //3014 => For Sewing Production
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.GMT_CUT_PNL_INSPTN_H_ID = (dr["GMT_CUT_PNL_INSPTN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_INSPTN_H_ID"]);
                        ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                        ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                        ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                        ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                        ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                        ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                        ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                        ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                        ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                        ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                        ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                        ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                        ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                        ob.FINAL_QTY = (dr["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FINAL_QTY"]);

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<GMT_BNDL_CARD_VIEWMODELModel> getBundlesOfRecutModal(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_CUT_PLN_INS.gmt_cut_pnl_insptn_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_BNDL_CARD_VIEWMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3010},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_BNDL_CARD_VIEWMODELModel ob = new GMT_BNDL_CARD_VIEWMODELModel();
                    ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
    
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.BUNDLE_BARCODE = (dr["BUNDLE_BARCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_BARCODE"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]);
                    ob.SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);

                    ob.GMT_BNDL_CRD_PDATA_ID = (dr["GMT_BNDL_CRD_PDATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_BNDL_CRD_PDATA_ID"]);

                    ob.recuts = new List<GMT_CUT_PNL_RECUTModel>();
                    ob.recuts = new GMT_CUT_PNL_RECUTModel().Query(ob.GMT_BNDL_CRD_PDATA_ID);

                    ob.ALLOWED_SHRT_QTY = ob.recuts.First().ALLOWED_SHRT_QTY;
                    ob.IS_DATA_CHANGE = false;

                 //   ob.RF_GARM_PART_LST = Convert.ToString(dr["RF_GARM_PART_LST"]).Split(',').Select(Int32.Parse).ToList(); ;


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        public int ALLOWED_SHRT_QTY { get; set; }

        public bool IS_DATA_CHANGE { get; set; }

        public long FINAL_QTY { get; set; }
        public string LINE_CODE { get; set; }
        public long TTL_INPUTED { get; set; }
        public long TTL_SEW_OUTPUT { get; set; }
        public int HR_PROD_LINE_ID { get; set; }
        public long MC_ORDER_SHIP_ID { get; set; }
        public long MC_COLOR_ID { get; set; }
        public long GMT_SEW_PROD_SCAN_ID { get; set; }
        public long TTL_SEW_REJECTED { get; set; }
        public int LINE_CODE_SL { get; set; }
        public int LINE_CODE_SPAN { get; set; }
        public long LINE_WISE_PROD_QTY { get; set; }

        public long HAS_REJECT { get; set; }
    }
}