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
    public class GMT_EBR_SENT_VMODELModel
    {
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public Int64 CUTNG_NO { get; set; }
        public Int64 GMT_CUT_INFO_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64 MC_COLOR_ID { get; set; }
        public string PART_NAME_LIST { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string SERVICE_NAME { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string COUNTRY_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }
        public Int64 BNDL_STORE_RECV { get; set; }
        public Int64 BNDL_SENT_QTY { get; set; }
        public Int64 BNDL_RECV_QTY { get; set; }
        public Int64 BNDL_CUTTING { get; set; }
        public Int64 PCS_CUTTING { get; set; }
        public Int64 PCS_STORE_RECV { get; set; }
        public Int64 PCS_SENT_QTY { get; set; }
        public Int64 PCS_RECV_QTY { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string BUNDLE_BARCODE { get; set; }
        public long BUNDLE_NO { get; set; }
        public string SL_RANGE_TXT { get; set; }
        public long QTY_IN_BNDL { get; set; }
        public long GMT_CUT_PRN_RCV_CHL_D_ID { get; set; }
        public long SL { get; set; }
        public Int64 PCS_SHORT_ADDI { get; set; }
        public Int64 PCS_REJECT_QTY { get; set; }
        public List<GMT_EBR_SENT_VMODELModel> details { get; set; }

        public List<GMT_EBR_SENT_VMODELModel> Query(Int32? pOption)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_rcv_chl_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_EBR_SENT_VMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = pOption??3000},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_EBR_SENT_VMODELModel ob = new GMT_EBR_SENT_VMODELModel();
                            ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                            ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                            ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                            ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                            ob.details = new List<GMT_EBR_SENT_VMODELModel>();
                            if (pOption==null)
                            {
                                ob.details = this.getDetailsData(ob.GMT_CUT_INFO_ID, ob.MC_COLOR_ID, ob.MC_ORDER_H_ID, 3001);
                            }
                            if (pOption == 3006)
                            {
                                ob.details = this.getDetailsData(ob.GMT_CUT_INFO_ID, ob.MC_COLOR_ID, ob.MC_ORDER_H_ID, 3007);
                            }
                            ob.GMT_CUT_PNL_BNK_LST = string.Join(",", ob.details.Select(x => x.GMT_CUT_PNL_BNK_LST));
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<GMT_EBR_SENT_VMODELModel> getDetailsData(Int64 pGMT_CUT_INFO_ID, Int64 pMC_COLOR_ID, Int64 pMC_ORDER_H_ID, Int32 pOption)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_rcv_chl_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_EBR_SENT_VMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value =pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_EBR_SENT_VMODELModel ob = new GMT_EBR_SENT_VMODELModel();

                    ob.PART_NAME_LIST = (dr["PART_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_NAME_LIST"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.SERVICE_NAME = (dr["SERVICE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SERVICE_NAME"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);

                    ob.BNDL_CUTTING = (dr["BNDL_CUTTING"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BNDL_CUTTING"]);
                    ob.BNDL_STORE_RECV = (dr["BNDL_STORE_RECV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BNDL_STORE_RECV"]);
                    ob.BNDL_SENT_QTY = (dr["BNDL_SENT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BNDL_SENT_QTY"]);
                    ob.BNDL_RECV_QTY = (dr["BNDL_RECV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BNDL_RECV_QTY"]);

                    ob.PCS_CUTTING = (dr["PCS_CUTTING"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_CUTTING"]);
                    ob.PCS_STORE_RECV = (dr["PCS_STORE_RECV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_STORE_RECV"]);
                    ob.PCS_SENT_QTY = (dr["PCS_SENT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_SENT_QTY"]);
                    ob.PCS_RECV_QTY = (dr["PCS_RECV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_RECV_QTY"]);
                    ob.PCS_SHORT_ADDI = (dr["PCS_SHORT_ADDI"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_SHORT_ADDI"]);
                    ob.PCS_REJECT_QTY = (dr["PCS_REJECT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_REJECT_QTY"]);
                    ob.GMT_CUT_PNL_BNK_LST = (dr["GMT_CUT_PNL_BNK_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_CUT_PNL_BNK_LST"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public dynamic GetCountDataAtStoreRecvFromPrint()
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_rcv_chl_h_select";
           
            //pOption=3000=>Select All Data
            try
            {
                OraDatabase db = new OraDatabase();
                dynamic ob77 = null;
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ob77 = new
                        { OK_NEXT_ACT = (dr["OK_NEXT_ACT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OK_NEXT_ACT"]),
                            COUNT_OK = (dr["COUNT_OK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNT_OK"]),
                            COUNT_NOT_OK = (dr["COUNT_NOT_OK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNT_NOT_OK"])
                        };
                    }
                }
                else
                {
                        ob77 = new
                        {
                            OK_NEXT_ACT = 0,
                            COUNT_OK = 0,
                            COUNT_NOT_OK = 0
                        };
                }


                return ob77; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GMT_EBR_SENT_VMODELModel> getRejectedPrintEmbrData()
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_prn_rcv_chl_h_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_EBR_SENT_VMODELModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_EBR_SENT_VMODELModel ob = new GMT_EBR_SENT_VMODELModel();
                    ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);
                    ob.GMT_CUT_PNL_BNK_ID = (dr["GMT_CUT_PNL_BNK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_BNK_ID"]);

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
                    ob.GMT_CUT_PRN_RCV_CHL_D_ID = (dr["GMT_CUT_PRN_RCV_CHL_D_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["GMT_CUT_PRN_RCV_CHL_D_ID"]);

                    ob.details = new List<GMT_EBR_SENT_VMODELModel>();

                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                                new CommandParameter() {ParameterName = "pOption", Value = 3004},
                                new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_ID", Value = ob.GMT_CUT_PNL_BNK_ID},
                                new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                                new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);
                        foreach (DataRow dr1 in ds1.Tables[0].Rows)
                        {
                            GMT_EBR_SENT_VMODELModel ob1 = new GMT_EBR_SENT_VMODELModel();
                            ob1.GMT_CUT_PRN_RCV_CHL_D_ID = (dr1["GMT_CUT_PRN_RCV_CHL_D_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["GMT_CUT_PRN_RCV_CHL_D_ID"]);
                            ob1.PART_NAME_LIST = (dr1["PART_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["PART_NAME_LIST"]);
                            ob1.SUP_TRD_NAME_EN = (dr1["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SUP_TRD_NAME_EN"]);
                            ob1.SERVICE_NAME = (dr1["SERVICE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SERVICE_NAME"]);

                            ob1.SHORT_QTY = (dr1["SHORT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SHORT_QTY"]);
                            ob1.SURPLS_QTY = (dr1["SURPLS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["SURPLS_QTY"]);
                            ob1.REJECT_QTY = (dr1["REJECT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["REJECT_QTY"]);
                            ob1.REJECT_QTY_FAB = (dr1["REJECT_QTY_FAB"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["REJECT_QTY_FAB"]);
                            ob1.QTY_IN_BNDL = (dr1["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["QTY_IN_BNDL"]);
                            ob1.PAIRED_QTY = (dr1["PAIRED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PAIRED_QTY"]);
                            ob1.ADJUST_QTY = (dr1["ADJUST_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ADJUST_QTY"]);

                            ob1.IS_ACTIVE = ob.GMT_CUT_PRN_RCV_CHL_D_ID == ob1.GMT_CUT_PRN_RCV_CHL_D_ID;
                            ob.details.Add(ob1);
                        }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public long GMT_CUT_PNL_BNK_ID { get; set; }

        public long SHORT_QTY { get; set; }

        public long SURPLS_QTY { get; set; }

        public long REJECT_QTY { get; set; }

        public bool IS_ACTIVE { get; set; }

        public long PAIRED_QTY { get; set; }

        public long ADJUST_QTY { get; set; }

        public string GMT_CUT_PNL_BNK_LST { get; set; }
        public long REJECT_QTY_FAB { get; set; }
    }
}