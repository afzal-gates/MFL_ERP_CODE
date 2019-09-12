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
    public class GMT_CUT_INFOModel
    {
        public Int64 GMT_CUT_INFO_ID { get; set; }
        public Int64 GMT_MRKR_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64? GMT_PROD_PLN_CLNDR_ID { get; set; }
        public DateTime LAY_ST_TIME { get; set; }
        public DateTime? LAY_END_TIME { get; set; }
        public Int64 GMT_CUT_TABLE_ID { get; set; }
        public Int64? CUTNG_NO { get; set; }
        public Int64 LK_CUT_TYPE_ID { get; set; }
        public string IS_CUTTING_PROD { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Int64 LK_STCKR_OPT_ID { get; set; }
        public Int64? MAX_QTY_IN_BNDL { get; set; }
        public Int64? NO_BNDL_SET { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZED { get; set; }
        public string HAS_GRADING { get; set; }
        public string IS_SC_CUTTING { get; set; }

        public Int64 LK_BNDL_DIV_TYP_ID { get; set; }
        public Int64? MAX_RATIO_QTY { get; set; }

        public string GMT_CUT_INFO_SZ_RTO_XML { get; set; }
        private List<GMT_CUT_INFO_SZ_RTO_ITEMModel> _itemsCutInfoSzRtoItm = null;
        public List<GMT_CUT_INFO_SZ_RTO_ITEMModel> itemsCutInfoSzRtoItm
        {
            get
            {
                if (_itemsCutInfoSzRtoItm == null)
                {
                    _itemsCutInfoSzRtoItm = new List<GMT_CUT_INFO_SZ_RTO_ITEMModel>();
                }
                return _itemsCutInfoSzRtoItm;
            }
            set
            {
                _itemsCutInfoSzRtoItm = value;
            }
        }

        public string GMT_CUT_INFO_PLY_XML { get; set; }
        private List<GMT_CUT_INFO_PLYModel> _itemsCutInfoPly = null;
        public List<GMT_CUT_INFO_PLYModel> itemsCutInfoPly
        {
            get
            {
                if (_itemsCutInfoPly == null)
                {
                    _itemsCutInfoPly = new List<GMT_CUT_INFO_PLYModel>();
                }
                return _itemsCutInfoPly;
            }
            set
            {
                _itemsCutInfoPly = value;
            }
        }

        public string GMT_CUT_INFO_GRD_XML { get; set; }
        private List<GMT_CUT_INFO_GRDModel> _itemsCutInfoGrd = null;
        public List<GMT_CUT_INFO_GRDModel> itemsCutInfoGrd
        {
            get
            {
                if (_itemsCutInfoGrd == null)
                {
                    _itemsCutInfoGrd = new List<GMT_CUT_INFO_GRDModel>();
                }
                return _itemsCutInfoGrd;
            }
            set
            {
                _itemsCutInfoGrd = value;
            }
        }


        public Int64? TTL_CUT_QTY { get; set; }
        public string RF_GARM_PART_LST { get; set; }
        public string MRKR_SH_DESC { get; set; }
        public string TABLE_NO { get; set; }
        public long? MC_BYR_ACC_GRP_ID { get; set; }
        public long? MC_STYLE_H_EXT_ID { get; set; }
        public long? MC_ORDER_H_ID { get; set; }
        public long? GMT_COLOR_ID { get; set; }


        public string LayChartBatchSave()
        {
            const string sp = "pkg_cut_lay_chart.gmt_cut_info_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pGMT_MRKR_ID", Value = ob.GMT_MRKR_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID", Value = ob.GMT_PROD_PLN_CLNDR_ID},
                     new CommandParameter() {ParameterName = "pLAY_ST_TIME", Value = ob.LAY_ST_TIME},
                     new CommandParameter() {ParameterName = "pLAY_END_TIME", Value = ob.LAY_END_TIME},
                     new CommandParameter() {ParameterName = "pGMT_CUT_TABLE_ID", Value = ob.GMT_CUT_TABLE_ID},
                     new CommandParameter() {ParameterName = "pCUTNG_NO", Value = ob.CUTNG_NO},
                     new CommandParameter() {ParameterName = "pLK_CUT_TYPE_ID", Value = ob.LK_CUT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_CUTTING_PROD", Value = ob.IS_CUTTING_PROD},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_STCKR_OPT_ID", Value = ob.LK_STCKR_OPT_ID},
                     new CommandParameter() {ParameterName = "pMAX_QTY_IN_BNDL", Value = ob.MAX_QTY_IN_BNDL},
                     new CommandParameter() {ParameterName = "pNO_BNDL_SET", Value = ob.NO_BNDL_SET},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value = ob.GMT_COLOR_ID},
                     new CommandParameter() {ParameterName = "pLK_BNDL_DIV_TYP_ID", Value = ob.LK_BNDL_DIV_TYP_ID},
                     new CommandParameter() {ParameterName = "pMAX_RATIO_QTY", Value = ob.MAX_RATIO_QTY},
                     new CommandParameter() {ParameterName = "pHAS_GRADING", Value = ob.HAS_GRADING},
                     new CommandParameter() {ParameterName = "pIS_SC_CUTTING", Value = ob.IS_SC_CUTTING},

                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_SZ_RTO_XML", Value = ob.GMT_CUT_INFO_SZ_RTO_XML},
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_PLY_XML", Value = ob.GMT_CUT_INFO_PLY_XML},
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_GRD_XML", Value = ob.GMT_CUT_INFO_GRD_XML},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ProcBndlCard()
        {
            const string sp = "pkg_cut_lay_chart.bndl_card_generate";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                                          
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveMarkAsCuttingProd()
        {
            const string sp = "pkg_cut_lay_chart.gmt_cut_info_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                                          
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_CUT_INFOModel> GetCuttingList(Int64 pMC_ORDER_H_ID, Int64? pMC_ORDER_SHIP_ID = null, Int64? pGMT_COLOR_ID = null)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            try
            {
                var obList = new List<GMT_CUT_INFOModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value =pMC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value =pGMT_COLOR_ID},

                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_INFOModel ob = new GMT_CUT_INFOModel();
                    
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);

                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    
                    ob.LAY_ST_TIME = (dr["LAY_ST_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAY_ST_TIME"]);                 
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);

                    ob.IS_CUTTING_PROD = (dr["IS_CUTTING_PROD"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CUTTING_PROD"]);

                    ob.GMT_MRKR_ID = (dr["GMT_MRKR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_ID"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                    ob.MRKR_SH_DESC = (dr["MRKR_SH_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_SH_DESC"]);
                    ob.TABLE_NO = (dr["TABLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TABLE_NO"]);

                    ob.TTL_CUT_QTY = (dr["TTL_CUT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_CUT_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CUT_INFOModel GetCutInfoHdr(long pGMT_CUT_INFO_ID)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            try
            {
                var ob = new GMT_CUT_INFOModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value =pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.GMT_MRKR_ID = (dr["GMT_MRKR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_ID"]);                                        
                    ob.GMT_PROD_PLN_CLNDR_ID = (dr["GMT_PROD_PLN_CLNDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PROD_PLN_CLNDR_ID"]);
                    ob.LAY_ST_TIME = (dr["LAY_ST_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAY_ST_TIME"]);
                    
                    if (dr["LAY_END_TIME"] != DBNull.Value)
                        ob.LAY_END_TIME = (dr["LAY_END_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAY_END_TIME"]);

                    ob.GMT_CUT_TABLE_ID = (dr["GMT_CUT_TABLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_TABLE_ID"]);
                    ob.CUTNG_NO = (dr["CUTNG_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUTNG_NO"]);
                    ob.LK_CUT_TYPE_ID = (dr["LK_CUT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CUT_TYPE_ID"]);
                    ob.IS_CUTTING_PROD = (dr["IS_CUTTING_PROD"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_CUTTING_PROD"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.LK_STCKR_OPT_ID = (dr["LK_STCKR_OPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STCKR_OPT_ID"]);
                    
                    if(dr["MAX_QTY_IN_BNDL"] != DBNull.Value)
                        ob.MAX_QTY_IN_BNDL = (dr["MAX_QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_QTY_IN_BNDL"]);

                    if (dr["NO_BNDL_SET"] != DBNull.Value)
                        ob.NO_BNDL_SET = (dr["NO_BNDL_SET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_BNDL_SET"]);
                    
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.HAS_GRADING = (dr["HAS_GRADING"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_GRADING"]);
                    ob.IS_SC_CUTTING = (dr["IS_SC_CUTTING"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC_CUTTING"]);
                                        
                    ob.LK_BNDL_DIV_TYP_ID = (dr["LK_BNDL_DIV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BNDL_DIV_TYP_ID"]);

                    if (dr["MAX_RATIO_QTY"] != DBNull.Value)
                        ob.MAX_RATIO_QTY = (dr["MAX_RATIO_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_RATIO_QTY"]);

                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    //ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    //ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    //ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);

                    var obItemsCutInfoPly = new GMT_CUT_INFO_PLYModel().GetCutInfoPlyList(ob.GMT_CUT_INFO_ID, ob.MC_STYLE_H_ID);
                    ob.itemsCutInfoPly = (List<GMT_CUT_INFO_PLYModel>)obItemsCutInfoPly;

                    if (ob.itemsCutInfoPly.Count == 0)
                    {
                        List<GMT_CUT_INFO_PLYModel> obCutIntoPlyDummyList = new List<GMT_CUT_INFO_PLYModel>();

                        GMT_CUT_INFO_PLYModel obCutInfoPlyDummy = new GMT_CUT_INFO_PLYModel();
                        obCutInfoPlyDummy.GMT_CUT_INFO_PLY_ID = 0;
                        obCutInfoPlyDummy.ROLL_SL = 0;
                        obCutInfoPlyDummy.MC_ORDER_CNTRY_ID = null;
                        obCutInfoPlyDummy.PLY_QTY = null;
                        obCutInfoPlyDummy.FAB_ROLL_WT = null;
                        obCutInfoPlyDummy.DYE_LOT_NO = null;

                        var obItemsOrdShipList = new MC_ORDER_SHIPModel().GetOrderShipmentList(ob.MC_STYLE_H_ID, 50);
                        obCutInfoPlyDummy.itemsOrdShipList = (List<MC_ORDER_SHIPModel>)obItemsOrdShipList;

                        if (obCutInfoPlyDummy.itemsOrdShipList.Count > 0)
                        {
                            obCutInfoPlyDummy.MC_ORDER_SHIP_ID = obCutInfoPlyDummy.itemsOrdShipList[0].MC_ORDER_SHIP_ID;

                            var obItemsColorList = new MC_COLORModel().OrderOrByerAccWiseColorList(obCutInfoPlyDummy.itemsOrdShipList[0].MC_ORDER_H_ID, null);
                            obCutInfoPlyDummy.itemsColorList = (List<MC_COLORModel>)obItemsColorList;

                            if (obCutInfoPlyDummy.itemsColorList.Count > 0)
                                obCutInfoPlyDummy.GMT_COLOR_ID = obCutInfoPlyDummy.itemsColorList[0].MC_COLOR_ID;

                            var obItemsCountryList = new MC_ORDER_HModel().GetOrdCountryList(obCutInfoPlyDummy.itemsOrdShipList[0].MC_ORDER_H_ID);
                            obCutInfoPlyDummy.itemsCountryList = (List<MC_ORDER_CNTRYModel>)obItemsCountryList;

                            if (obCutInfoPlyDummy.itemsCountryList.Count > 0)
                                obCutInfoPlyDummy.MC_ORDER_CNTRY_ID = obCutInfoPlyDummy.itemsCountryList[0].MC_ORDER_CNTRY_ID;
                        }


                        obCutIntoPlyDummyList.Add(obCutInfoPlyDummy);

                        ob.itemsCutInfoPly = obCutIntoPlyDummyList;
                    }



                    var obItemsCutInfoGrd = new GMT_CUT_INFO_GRDModel().GetCutInfoGrdList(ob.GMT_CUT_INFO_ID, ob.MC_STYLE_H_ID);
                    ob.itemsCutInfoGrd = (List<GMT_CUT_INFO_GRDModel>)obItemsCutInfoGrd;

                    if (ob.itemsCutInfoGrd.Count == 0)
                    {
                        List<GMT_CUT_INFO_GRDModel> obCutIntoGrdDummyList = new List<GMT_CUT_INFO_GRDModel>();

                        GMT_CUT_INFO_GRDModel obCutInfoGrdDummy = new GMT_CUT_INFO_GRDModel();
                        obCutInfoGrdDummy.GMT_CUT_INFO_GRD_ID = 0;
                        obCutInfoGrdDummy.MC_ORDER_CNTRY_ID = null;
                        obCutInfoGrdDummy.MC_STYLE_D_ITEM_ID = null;
                        obCutInfoGrdDummy.MC_SIZE_ID = null;
                        obCutInfoGrdDummy.BUNDLE_NO = null;
                        obCutInfoGrdDummy.GRD_SL1 = null;
                        obCutInfoGrdDummy.GRD_SL2 = null;
                        obCutInfoGrdDummy.QTY_IN_BNDL = null;                        
                        obCutInfoGrdDummy.DYE_LOT_NO = null;

                        var obItemsOrdShipList = new MC_ORDER_SHIPModel().GetOrderShipmentList(ob.MC_STYLE_H_ID, 50);
                        obCutInfoGrdDummy.itemsOrdShipList = (List<MC_ORDER_SHIPModel>)obItemsOrdShipList;

                        if (obCutInfoGrdDummy.itemsOrdShipList.Count > 0)
                        {
                            obCutInfoGrdDummy.MC_ORDER_SHIP_ID = obCutInfoGrdDummy.itemsOrdShipList[0].MC_ORDER_SHIP_ID;

                            var obItemsColorList = new MC_COLORModel().OrderOrByerAccWiseColorList(obCutInfoGrdDummy.itemsOrdShipList[0].MC_ORDER_H_ID, null);
                            obCutInfoGrdDummy.itemsColorList = (List<MC_COLORModel>)obItemsColorList;

                            if (obCutInfoGrdDummy.itemsColorList.Count > 0)
                                obCutInfoGrdDummy.GMT_COLOR_ID = obCutInfoGrdDummy.itemsColorList[0].MC_COLOR_ID;

                            var obItemsCountryList = new MC_ORDER_HModel().GetOrdCountryList(obCutInfoGrdDummy.itemsOrdShipList[0].MC_ORDER_H_ID);
                            obCutInfoGrdDummy.itemsCountryList = (List<MC_ORDER_CNTRYModel>)obItemsCountryList;

                            if (obCutInfoGrdDummy.itemsCountryList.Count > 0)
                                obCutInfoGrdDummy.MC_ORDER_CNTRY_ID = obCutInfoGrdDummy.itemsCountryList[0].MC_ORDER_CNTRY_ID;

                            var obItemsStyleItemList = new MC_ORDER_HModel().GetOrdItmList(obCutInfoGrdDummy.itemsOrdShipList[0].MC_ORDER_H_ID, ob.GMT_COLOR_ID);
                            obCutInfoGrdDummy.itemsStyleItemList = (List<MC_STYLE_D_ITEMModel>)obItemsStyleItemList;

                            if (obCutInfoGrdDummy.itemsStyleItemList.Count > 0)
                                obCutInfoGrdDummy.MC_STYLE_D_ITEM_ID = obCutInfoGrdDummy.itemsStyleItemList[0].MC_STYLE_D_ITEM_ID;

                            var obItemsSizeList = new MC_SIZEModel().OrderWiseSizeList(obCutInfoGrdDummy.itemsOrdShipList[0].MC_ORDER_H_ID);
                            obCutInfoGrdDummy.itemsSizeList = (List<MC_SIZEModel>)obItemsSizeList;

                            if (obCutInfoGrdDummy.itemsSizeList.Count > 0)
                                obCutInfoGrdDummy.MC_SIZE_ID = obCutInfoGrdDummy.itemsSizeList[0].MC_SIZE_ID;

                        }
                        obCutIntoGrdDummyList.Add(obCutInfoGrdDummy);

                        ob.itemsCutInfoGrd = obCutIntoGrdDummyList;
                    }
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> GetCutInfoUserLavel()
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]}, 
                    new CommandParameter() {ParameterName = "pOption", Value = 3013},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]),
                        USER_LEVEL = (dr["USER_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_LEVEL"]),                        
                    });
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