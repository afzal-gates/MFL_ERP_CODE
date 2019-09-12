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
    public class GMT_CUT_INFO_GRDModel
    {
        public Int64 GMT_CUT_INFO_GRD_ID { get; set; }
        public Int64 GMT_CUT_INFO_ID { get; set; }
        public string BUNDLE_NO { get; set; }
        public Int64? MC_ORDER_COL_ID { get; set; }
        public Int64? MC_STYLE_D_ITEM_ID { get; set; }
        public Int64? MC_ORDER_CNTRY_ID { get; set; }
        public Int64? MC_SIZE_ID { get; set; }
        public string DYE_LOT_NO { get; set; }
        public Int64? GRD_SL1 { get; set; }
        public Int64? GRD_SL2 { get; set; }
        public Int64? QTY_IN_BNDL { get; set; }
        public Int64? ROLL_SL { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? MC_ORDER_SHIP_ID { get; set; }     
        public Int64? GMT_COLOR_ID { get; set; }

        private List<MC_ORDER_SHIPModel> _itemsOrdShipList = null;
        public List<MC_ORDER_SHIPModel> itemsOrdShipList
        {
            get
            {
                if (_itemsOrdShipList == null)
                {
                    _itemsOrdShipList = new List<MC_ORDER_SHIPModel>();
                }
                return _itemsOrdShipList;
            }
            set
            {
                _itemsOrdShipList = value;
            }
        }

        private List<MC_COLORModel> _itemsColorList = null;
        public List<MC_COLORModel> itemsColorList
        {
            get
            {
                if (_itemsColorList == null)
                {
                    _itemsColorList = new List<MC_COLORModel>();
                }
                return _itemsColorList;
            }
            set
            {
                _itemsColorList = value;
            }
        }
        private List<MC_ORDER_CNTRYModel> _itemsCountryList = null;
        public List<MC_ORDER_CNTRYModel> itemsCountryList
        {
            get
            {
                if (_itemsCountryList == null)
                {
                    _itemsCountryList = new List<MC_ORDER_CNTRYModel>();
                }
                return _itemsCountryList;
            }
            set
            {
                _itemsCountryList = value;
            }
        }

        private List<MC_STYLE_D_ITEMModel> _itemsStyleItemList = null;
        public List<MC_STYLE_D_ITEMModel> itemsStyleItemList
        {
            get
            {
                if (_itemsStyleItemList == null)
                {
                    _itemsStyleItemList = new List<MC_STYLE_D_ITEMModel>();
                }
                return _itemsStyleItemList;
            }
            set
            {
                _itemsStyleItemList = value;
            }
        }

        private List<MC_SIZEModel> _itemsSizeList = null;
        public List<MC_SIZEModel> itemsSizeList
        {
            get
            {
                if (_itemsSizeList == null)
                {
                    _itemsSizeList = new List<MC_SIZEModel>();
                }
                return _itemsSizeList;
            }
            set
            {
                _itemsSizeList = value;
            }
        }



        public List<GMT_CUT_INFO_GRDModel> GetCutInfoGrdList(Int64 pGMT_CUT_INFO_ID, Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            try
            {
                //var obItemsCountryList = new MC_ORDER_HModel().GetOrdCountryList(pMC_ORDER_H_ID);                
                

                var obList = new List<GMT_CUT_INFO_GRDModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value =pGMT_CUT_INFO_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_INFO_GRDModel ob = new GMT_CUT_INFO_GRDModel();
                    ob.GMT_CUT_INFO_GRD_ID = (dr["GMT_CUT_INFO_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_GRD_ID"]);
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_NO"]);
                    ob.MC_ORDER_COL_ID = (dr["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_COL_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);

                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_ORDER_CNTRY_ID = (dr["MC_ORDER_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_CNTRY_ID"]);
                    
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.GRD_SL1 = (dr["GRD_SL1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GRD_SL1"]);
                    ob.GRD_SL2 = (dr["GRD_SL2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GRD_SL2"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);
                    ob.ROLL_SL = (dr["ROLL_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ROLL_SL"]);
                    ob.COLOR_COMBO_TXT = (dr["COLOR_COMBO_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_COMBO_TXT"]);
                    
                    var obItemsOrdShipList = new MC_ORDER_SHIPModel().GetOrderShipmentList(pMC_STYLE_H_ID, 50);
                    ob.itemsOrdShipList = (List<MC_ORDER_SHIPModel>)obItemsOrdShipList;

                    var obItemsColorList = new MC_COLORModel().OrderOrByerAccWiseColorList(ob.MC_ORDER_H_ID, null);
                    ob.itemsColorList = (List<MC_COLORModel>)obItemsColorList;

                    var obItemsCountryList = new MC_ORDER_HModel().GetOrdCountryList(ob.MC_ORDER_H_ID);
                    ob.itemsCountryList = (List<MC_ORDER_CNTRYModel>)obItemsCountryList;

                    var obItemsStyleItemList = new MC_STYLE_D_ITEMModel().OrderWiseItemList(ob.MC_ORDER_H_ID, null, ob.MC_ORDER_SHIP_ID, "Y"); //.OrderWiseItemListForPln(ob.MC_ORDER_SHIP_ID);                    
                    ob.itemsStyleItemList = (List<MC_STYLE_D_ITEMModel>)obItemsStyleItemList;

                    var obItemsSizeList = new MC_SIZEModel().OrderWiseSizeList(ob.MC_ORDER_H_ID);
                    ob.itemsSizeList = (List<MC_SIZEModel>)obItemsSizeList;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CUT_INFO_GRDModel Select(long ID)
        {
            string sp = "Select_GMT_CUT_INFO_GRD";
            try
            {
                var ob = new GMT_CUT_INFO_GRDModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_GRD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CUT_INFO_GRD_ID = (dr["GMT_CUT_INFO_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_GRD_ID"]);
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.BUNDLE_NO = (dr["BUNDLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUNDLE_NO"]);
                    ob.MC_ORDER_COL_ID = (dr["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_COL_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.MC_ORDER_CNTRY_ID = (dr["MC_ORDER_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_CNTRY_ID"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.GRD_SL1 = (dr["GRD_SL1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GRD_SL1"]);
                    ob.GRD_SL2 = (dr["GRD_SL2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GRD_SL2"]);
                    ob.QTY_IN_BNDL = (dr["QTY_IN_BNDL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_IN_BNDL"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string COLOR_COMBO_TXT { get; set; }
    }
}