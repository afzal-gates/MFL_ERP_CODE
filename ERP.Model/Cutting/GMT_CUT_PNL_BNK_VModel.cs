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
    public class GMT_CUT_PNL_BNK_VModel
    {
        public Int64 GMT_CUT_INFO_ID { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string COUNTRY_NAME_EN { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }
        public Int64 CUTNG_NO { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64 MC_COLOR_ID { get; set; }
        public Int64 NO_OF_BNDL_CUTTING { get; set; }
        public Int64 NO_OF_BNDL_INSPECTED { get; set; }
        public Int64 NO_OF_BNDL_STORE_RECV { get; set; }
        public string GMT_CUT_PNL_BNK_LST { get; set; }
        public Int64 CUTTING_QTY { get; set; }
        public Int64 SHORT_QTY { get; set; }
        public Int64 FINAL_QTY { get; set; }

        public Int64 TTL_NO_OF_BNDL_CUTTING { get; set; }
        public Int64 TTL_NO_OF_BNDL_INSPECTED { get; set; }
        public Int64 TTL_NO_OF_BNDL_STORE_RECV { get; set; }

        public Int64 TTL_CUTTING_QTY { get; set; }
        public Int64 TTL_SHORT_QTY { get; set; }
        public Int64 TTL_FINAL_QTY { get; set; }
        public long ITEM_SPAN { get; set; }
        public long ITEM_SL { get; set; }
        public long COUNTRY_SPAN { get; set; }
        public long COUNTRY_SL { get; set; }
        public long NO_OF_BNDL_SCANNED { get; set; }
        public long TTL_NO_OF_BNDL_SCANNED { get; set; }
        public long BUNDLE_NO { get; set; }
        public string SL_RANGE_TXT { get; set; }
        public long QTY_IN_BNDL { get; set; }
        public long GMT_CUT_PNL_BNK_ID { get; set; }
 
        public List<GMT_CUT_PNL_BNK_VModel> details { get; set; }
        public List<GMT_CUT_PNL_BNK_VModel> Query(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PNL_BNK_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_CUT_PNL_BNK_VModel ob = new GMT_CUT_PNL_BNK_VModel();
                            ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                            ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                            ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);

                            ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                            ob.GMT_CUT_PNL_BNK_LST = (dr["GMT_CUT_PNL_BNK_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_CUT_PNL_BNK_LST"]);

                            ob.GMT_BNDL_CRD_PDATA_LST = (dr["GMT_BNDL_CRD_PDATA_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_BNDL_CRD_PDATA_LST"]);
                            ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                            ob.details = new List<GMT_CUT_PNL_BNK_VModel>();
                            ob.details = this.QueryDetials(pGMT_PROD_PLN_CLNDR_ID, ob.GMT_CUT_INFO_ID, ob.MC_ORDER_H_ID,ob.MC_COLOR_ID);
                            ob.TTL_NO_OF_BNDL_CUTTING = ob.details.Sum(x => x.NO_OF_BNDL_CUTTING);
                            ob.TTL_NO_OF_BNDL_INSPECTED = ob.details.Sum(x => x.NO_OF_BNDL_INSPECTED);
                            ob.TTL_NO_OF_BNDL_STORE_RECV = ob.details.Sum(x => x.NO_OF_BNDL_STORE_RECV);
                            ob.TTL_NO_OF_BNDL_SCANNED = ob.details.Sum(x => x.NO_OF_BNDL_SCANNED);


                            ob.TTL_CUTTING_QTY = ob.details.Sum(x => x.CUTTING_QTY);
                            ob.TTL_CUTTING_QTY_CUR = ob.details.Sum(x => x.CUTTING_QTY_CUR);
                            ob.TTL_SHORT_QTY = ob.details.Sum(x => x.SHORT_QTY);
                            ob.TTL_FINAL_QTY = ob.details.Sum(x => x.FINAL_QTY);
                            

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_PNL_BNK_VModel> getSewingDeliveryChallan(Int64 pGMT_CUT_SEW_DLV_CHL_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PNL_BNK_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value = pGMT_CUT_SEW_DLV_CHL_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_PNL_BNK_VModel ob = new GMT_CUT_PNL_BNK_VModel();
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);

                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                                        
                    ob.GMT_CUT_PNL_BNK_LST = (dr["GMT_CUT_PNL_BNK_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_CUT_PNL_BNK_LST"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);



                    ob.details = new List<GMT_CUT_PNL_BNK_VModel>();
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                            new CommandParameter() {ParameterName = "pGMT_CUT_SEW_DLV_CHL_ID", Value = pGMT_CUT_SEW_DLV_CHL_ID},
                            new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                            new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3004},
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        GMT_CUT_PNL_BNK_VModel ob1 = new GMT_CUT_PNL_BNK_VModel();

                        ob1.GMT_CUT_PNL_BNK_LST = (dr1["GMT_CUT_PNL_BNK_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["GMT_CUT_PNL_BNK_LST"]);

                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.COUNTRY_NAME_EN = (dr1["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COUNTRY_NAME_EN"]);
                        ob1.SIZE_CODE = (dr1["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SIZE_CODE"]);
  
                        ob1.ITEM_SPAN = (dr1["ITEM_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ITEM_SPAN"]);
                        ob1.ITEM_SL = (dr1["ITEM_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ITEM_SL"]);
                        ob1.COUNTRY_SPAN = (dr1["COUNTRY_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["COUNTRY_SPAN"]);
                        ob1.COUNTRY_SL = (dr1["COUNTRY_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["COUNTRY_SL"]);

                        ob1.NO_OF_BNDL_SCANNED = (dr1["NO_OF_BNDL_SCANNED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["NO_OF_BNDL_SCANNED"]);
                        ob1.CUTTING_QTY = (dr1["CUTTING_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CUTTING_QTY"]);
                        ob1.FINAL_QTY = (dr1["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["FINAL_QTY"]);
                        ob.details.Add(ob1);
                    }
                    ob.TTL_CUTTING_QTY = ob.details.Sum(x => x.CUTTING_QTY);
                    ob.TTL_NO_OF_BNDL_SCANNED = ob.details.Sum(x => x.NO_OF_BNDL_SCANNED);
                    ob.TTL_FINAL_QTY = ob.details.Sum(x => x.FINAL_QTY);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_PNL_BNK_VModel> GetRescanData4SewInput()
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PNL_BNK_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3009},
                     
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_PNL_BNK_VModel ob = new GMT_CUT_PNL_BNK_VModel();
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);

                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    ob.GMT_CUT_PNL_BNK_LST = (dr["GMT_CUT_PNL_BNK_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_CUT_PNL_BNK_LST"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);



                    ob.details = new List<GMT_CUT_PNL_BNK_VModel>();
                    ob.details = this.GetRescanData4SewInputDetials(ob.GMT_CUT_INFO_ID, ob.MC_ORDER_H_ID, ob.MC_COLOR_ID);
                    ob.TTL_NO_OF_BNDL_CUTTING = ob.details.Sum(x => x.NO_OF_BNDL_CUTTING);
                    ob.TTL_NO_OF_BNDL_INSPECTED = ob.details.Sum(x => x.NO_OF_BNDL_INSPECTED);
                    ob.TTL_NO_OF_BNDL_STORE_RECV = ob.details.Sum(x => x.NO_OF_BNDL_STORE_RECV);
                    ob.TTL_NO_OF_BNDL_SCANNED = ob.details.Sum(x => x.NO_OF_BNDL_SCANNED);


                    ob.TTL_CUTTING_QTY = ob.details.Sum(x => x.CUTTING_QTY);
                    ob.TTL_SHORT_QTY = ob.details.Sum(x => x.SHORT_QTY);
                    ob.TTL_FINAL_QTY = ob.details.Sum(x => x.FINAL_QTY);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_PNL_BNK_VModel> GetRescanData4SewInputDetials(Int64 pGMT_CUT_INFO_ID, Int64 pMC_ORDER_H_ID, Int64 pMC_COLOR_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";

            try
            {
                var obList = new List<GMT_CUT_PNL_BNK_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3010},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_PNL_BNK_VModel ob = new GMT_CUT_PNL_BNK_VModel();

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.NO_OF_BNDL_CUTTING = (dr["NO_OF_BNDL_CUTTING"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_CUTTING"]);
                    ob.NO_OF_BNDL_INSPECTED = (dr["NO_OF_BNDL_INSPECTED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_INSPECTED"]);
                    ob.NO_OF_BNDL_STORE_RECV = (dr["NO_OF_BNDL_STORE_RECV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_STORE_RECV"]);
                    ob.NO_OF_BNDL_SCANNED = (dr["NO_OF_BNDL_SCANNED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_SCANNED"]);

                    ob.ITEM_SPAN = (dr["ITEM_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_SPAN"]);
                    ob.ITEM_SL = (dr["ITEM_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_SL"]);
                    ob.COUNTRY_SPAN = (dr["COUNTRY_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNTRY_SPAN"]);
                    ob.COUNTRY_SL = (dr["COUNTRY_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNTRY_SL"]);

                    ob.CUTTING_QTY = (dr["CUTTING_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTTING_QTY"]);
                    ob.SHORT_QTY = (dr["SHORT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHORT_QTY"]);
                    ob.FINAL_QTY = (dr["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FINAL_QTY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_PNL_BNK_VModel> QueryDetials(Int64 pGMT_PROD_PLN_CLNDR_ID, Int64 pGMT_CUT_INFO_ID, Int64 pMC_ORDER_H_ID, Int64 pMC_COLOR_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";
            
            try
            {
                var obList = new List<GMT_CUT_PNL_BNK_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = pGMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_PNL_BNK_VModel ob = new GMT_CUT_PNL_BNK_VModel();

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);

                   

                    ob.NO_OF_BNDL_CUTTING = (dr["NO_OF_BNDL_CUTTING"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_CUTTING"]);
                    ob.NO_OF_BNDL_INSPECTED = (dr["NO_OF_BNDL_INSPECTED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_INSPECTED"]);
                    ob.NO_OF_BNDL_STORE_RECV = (dr["NO_OF_BNDL_STORE_RECV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_STORE_RECV"]);
                    ob.NO_OF_BNDL_SCANNED = (dr["NO_OF_BNDL_SCANNED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_BNDL_SCANNED"]);

                    ob.ITEM_SPAN = (dr["ITEM_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_SPAN"]);
                    ob.ITEM_SL = (dr["ITEM_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_SL"]);
                    ob.COUNTRY_SPAN = (dr["COUNTRY_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNTRY_SPAN"]);
                    ob.COUNTRY_SL = (dr["COUNTRY_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNTRY_SL"]);

                    ob.CUTTING_QTY = (dr["CUTTING_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTTING_QTY"]);

                    ob.CUTTING_QTY_CUR = (dr["CUTTING_QTY_CUR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTTING_QTY_CUR"]);


                    ob.SHORT_QTY = (dr["SHORT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHORT_QTY"]);
                    ob.FINAL_QTY = (dr["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FINAL_QTY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GMT_CUT_PNL_BNK_VModel Select(Int64 pGMT_CUT_PNL_BNK_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";
            
            try
            {
                var ob = new GMT_CUT_PNL_BNK_VModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_PNL_BNK_ID", Value = pGMT_CUT_PNL_BNK_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CUT_PNL_BNK_ID = (dr["GMT_CUT_PNL_BNK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_PNL_BNK_ID"]);
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

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string RF_GARM_PART_LST { get; set; }

        public List<GMT_CUT_PNL_BNK_VModel> getSewingScDeliveryChallan(long pGMT_CUT_SC_DLV_CHL_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PNL_BNK_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pGMT_CUT_SC_DLV_CHL_ID", Value = pGMT_CUT_SC_DLV_CHL_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_PNL_BNK_VModel ob = new GMT_CUT_PNL_BNK_VModel();
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);

                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    ob.GMT_CUT_PNL_BNK_LST = (dr["GMT_CUT_PNL_BNK_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_CUT_PNL_BNK_LST"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);



                    ob.details = new List<GMT_CUT_PNL_BNK_VModel>();
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                            new CommandParameter() {ParameterName = "pGMT_CUT_SC_DLV_CHL_ID", Value = pGMT_CUT_SC_DLV_CHL_ID},
                            new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                            new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3006},
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        GMT_CUT_PNL_BNK_VModel ob1 = new GMT_CUT_PNL_BNK_VModel();

                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.COUNTRY_NAME_EN = (dr1["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COUNTRY_NAME_EN"]);
                        ob1.SIZE_CODE = (dr1["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SIZE_CODE"]);

                        ob1.ITEM_SPAN = (dr1["ITEM_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ITEM_SPAN"]);
                        ob1.ITEM_SL = (dr1["ITEM_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ITEM_SL"]);
                        ob1.COUNTRY_SPAN = (dr1["COUNTRY_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["COUNTRY_SPAN"]);
                        ob1.COUNTRY_SL = (dr1["COUNTRY_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["COUNTRY_SL"]);

                        ob1.NO_OF_BNDL_SCANNED = (dr1["NO_OF_BNDL_SCANNED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["NO_OF_BNDL_SCANNED"]);
                        ob1.CUTTING_QTY = (dr1["CUTTING_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CUTTING_QTY"]);
                        ob1.FINAL_QTY = (dr1["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["FINAL_QTY"]);
                        ob.details.Add(ob1);
                    }
                    ob.TTL_CUTTING_QTY = ob.details.Sum(x => x.CUTTING_QTY);
                    ob.TTL_NO_OF_BNDL_SCANNED = ob.details.Sum(x => x.NO_OF_BNDL_SCANNED);
                    ob.TTL_FINAL_QTY = ob.details.Sum(x => x.FINAL_QTY);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_PNL_BNK_VModel> getPrintEmbrDelChallanSumData(long pGMT_CUT_PRN_DELV_CHL_H_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_CUT_PNL_BNK_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3007},
                     new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_ID", Value = pGMT_CUT_PRN_DELV_CHL_H_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_PNL_BNK_VModel ob = new GMT_CUT_PNL_BNK_VModel();
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);

                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    
                    ob.GMT_CUT_PNL_BNK_LST = (dr["GMT_CUT_PNL_BNK_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_CUT_PNL_BNK_LST"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);



                    ob.details = new List<GMT_CUT_PNL_BNK_VModel>();
                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                            new CommandParameter() {ParameterName = "pGMT_CUT_PRN_DELV_CHL_H_ID", Value = pGMT_CUT_PRN_DELV_CHL_H_ID},
                            new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                            new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3008},
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        GMT_CUT_PNL_BNK_VModel ob1 = new GMT_CUT_PNL_BNK_VModel();

                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.COUNTRY_NAME_EN = (dr1["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COUNTRY_NAME_EN"]);
                        ob1.SIZE_CODE = (dr1["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SIZE_CODE"]);

                        ob1.ITEM_SPAN = (dr1["ITEM_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ITEM_SPAN"]);
                        ob1.ITEM_SL = (dr1["ITEM_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ITEM_SL"]);
                        ob1.COUNTRY_SPAN = (dr1["COUNTRY_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["COUNTRY_SPAN"]);
                        ob1.COUNTRY_SL = (dr1["COUNTRY_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["COUNTRY_SL"]);

                        ob1.NO_OF_BNDL_SCANNED = (dr1["NO_OF_BNDL_SCANNED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["NO_OF_BNDL_SCANNED"]);
                        ob1.CUTTING_QTY = (dr1["CUTTING_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["CUTTING_QTY"]);
                        ob1.FINAL_QTY = (dr1["FINAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["FINAL_QTY"]);
                        ob.details.Add(ob1);
                    }
                    ob.TTL_CUTTING_QTY = ob.details.Sum(x => x.CUTTING_QTY);
                    ob.TTL_NO_OF_BNDL_SCANNED = ob.details.Sum(x => x.NO_OF_BNDL_SCANNED);
                    ob.TTL_FINAL_QTY = ob.details.Sum(x => x.FINAL_QTY);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetData4BndlStatus(Int64 pGMT_CUT_INFO_ID)
        {
            string sp = "PKG_GMT_CUT_BANK.gmt_cut_pnl_bnk_select";
            
            try
            {
                object ob = new {};
                var statusDtl = this.GetData4BndlStatusDtl(pGMT_CUT_INFO_ID);

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3011},
                     
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                   
                    ob = new
                    {
                        BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]),
                        STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]),
                        WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]),
                        ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]),
                        COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]),
                        CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]),

                        GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]),
                        MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]),
                        MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]),

                        GMT_CUT_PNL_BNK_LST = (dr["GMT_CUT_PNL_BNK_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_CUT_PNL_BNK_LST"]),
                        RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]),

                        itemDtl = statusDtl,

                        TTL_CUTTING_QTY = statusDtl.Sum(x => Convert.ToInt32(x.CUTTING_QTY)),
                        TTL_IS_REJ_PNL = statusDtl.Sum(x => Convert.ToInt32(x.IS_REJ_PNL)),
                        TTL_IS_RECUT = statusDtl.Sum(x => Convert.ToInt32(x.IS_RECUT)),
                        TTL_IS_STR_RCV = statusDtl.Sum(x => Convert.ToInt32(x.IS_STR_RCV)),
                        TTL_IS_PRNT_SENT = statusDtl.Sum(x => Convert.ToInt32(x.IS_PRNT_SENT)),
                        TTL_IS_PRNT_RECV = statusDtl.Sum(x => Convert.ToInt32(x.IS_PRNT_RECV)),
                        TTL_IS_CUT_BNK_WTH_TAG = statusDtl.Sum(x => Convert.ToInt32(x.IS_CUT_BNK_WTH_TAG)),
                        TTL_IS_CUT_BNK_WTHOT_TAG = statusDtl.Sum(x => Convert.ToInt32(x.IS_CUT_BNK_WTHOT_TAG)),
                        TTL_IS_SC = statusDtl.Sum(x => Convert.ToInt32(x.IS_SC)),
                        TTL_IS_SEW_INPUT = statusDtl.Sum(x => Convert.ToInt32(x.IS_SEW_INPUT)),
                        TTL_IS_SEW_OUTPUT = statusDtl.Sum(x => Convert.ToInt32(x.IS_SEW_OUTPUT)),
                        TTL_SEW_INPUT_QTY = statusDtl.Sum(x => Convert.ToInt32(x.SEW_INPUT_QTY)),
                        TTL_SEW_OUTPUT_QTY = statusDtl.Sum(x => Convert.ToInt32(x.SEW_OUTPUT_QTY))
                    };
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> GetData4BndlStatusDtl(Int64 pGMT_CUT_INFO_ID)
        {
            string sp = "pkg_gmt_cut_bank.gmt_cut_pnl_bnk_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value =pGMT_CUT_INFO_ID},

                    new CommandParameter() {ParameterName = "pOption", Value = 3012},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        ITEM_ROW_SPAN = (dr["ITEM_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ROW_SPAN"]),
                        ITEM_ROW_SL = (dr["ITEM_ROW_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ROW_SL"]),
                        COUNTRY_ROW_SPAN = (dr["COUNTRY_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNTRY_ROW_SPAN"]),
                        COUNTRY_ROW_SL = (dr["COUNTRY_ROW_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COUNTRY_ROW_SL"]),
                        SIZE_ROW_SPAN = (dr["SIZE_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SIZE_ROW_SPAN"]),
                        SIZE_ROW_SL = (dr["SIZE_ROW_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SIZE_ROW_SL"]),

                        ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]),
                        COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]),
                        SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]),
                        BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BUNDLE_NO"]),
                        DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]),
                        SL_RANGE_TXT = (dr["SL_RANGE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_RANGE_TXT"]),
                        CUTTING_QTY = (dr["CUTTING_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTTING_QTY"]),
                        
                        IS_REJ_PNL = (dr["IS_REJ_PNL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_REJ_PNL"]),
                        IS_RECUT = (dr["IS_RECUT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_RECUT"]),
                        IS_STR_RCV = (dr["IS_STR_RCV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_STR_RCV"]),                        
                        IS_PRNT_SENT = (dr["IS_PRNT_SENT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_PRNT_SENT"]),
                        IS_PRNT_RECV = (dr["IS_PRNT_RECV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_PRNT_RECV"]),
                        IS_CUT_BNK_WTH_TAG = (dr["IS_CUT_BNK_WTH_TAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_CUT_BNK_WTH_TAG"]),
                        IS_CUT_BNK_WTHOT_TAG = (dr["IS_CUT_BNK_WTHOT_TAG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_CUT_BNK_WTHOT_TAG"]),
                        IS_SC = (dr["IS_SC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_SC"]),
                        IS_SEW_INPUT = (dr["IS_SEW_INPUT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_SEW_INPUT"]),
                        IS_SEW_OUTPUT = (dr["IS_SEW_OUTPUT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_SEW_OUTPUT"]),
                        SEW_INPUT_QTY = (dr["SEW_INPUT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEW_INPUT_QTY"]),
                        SEW_OUTPUT_QTY = (dr["SEW_OUTPUT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEW_OUTPUT_QTY"]),                                                
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string GMT_BNDL_CRD_PDATA_LST { get; set; }

        public long CUTTING_QTY_CUR { get; set; }

        public long TTL_CUTTING_QTY_CUR { get; set; }
    }
}