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
    public class GMT_CUT_INFO_PLYModel
    {
        public Int64 GMT_CUT_INFO_PLY_ID { get; set; }
        public Int64 GMT_CUT_INFO_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64? MC_ORDER_SHIP_ID { get; set; }
        public Int64? MC_ORDER_CNTRY_ID { get; set; }
        public Int64? GMT_COLOR_ID { get; set; }
        public Int64? ROLL_SL { get; set; }
        public Int64? KNT_FAB_ROLL_ID { get; set; }
        public Decimal? FAB_ROLL_WT { get; set; }
        public Int64? PLY_QTY { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string COLOR_COMBO_TXT { get; set; }

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


        public string Save()
        {
            const string sp = "SP_GMT_CUT_INFO_PLY";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_PLY_ID", Value = ob.GMT_CUT_INFO_PLY_ID},
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value = ob.GMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_CNTRY_ID", Value = ob.MC_ORDER_CNTRY_ID},
                     new CommandParameter() {ParameterName = "pROLL_SL", Value = ob.ROLL_SL},
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pFAB_ROLL_WT", Value = ob.FAB_ROLL_WT},
                     new CommandParameter() {ParameterName = "pPLY_QTY", Value = ob.PLY_QTY},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
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


        public List<GMT_CUT_INFO_PLYModel> GetCutInfoPlyList(Int64 pGMT_CUT_INFO_ID, Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            try
            {                
                var obList = new List<GMT_CUT_INFO_PLYModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value =pGMT_CUT_INFO_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CUT_INFO_PLYModel ob = new GMT_CUT_INFO_PLYModel();
                    ob.GMT_CUT_INFO_PLY_ID = (dr["GMT_CUT_INFO_PLY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_PLY_ID"]);
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);

                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_ORDER_CNTRY_ID = (dr["MC_ORDER_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_CNTRY_ID"]);
                    
                    ob.ROLL_SL = (dr["ROLL_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ROLL_SL"]);
                    
                    if (dr["KNT_FAB_ROLL_ID"] != DBNull.Value)
                        ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);

                    if (dr["FAB_ROLL_WT"] != DBNull.Value)
                        ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);

                    ob.PLY_QTY = (dr["PLY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLY_QTY"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    ob.COLOR_COMBO_TXT = (dr["COLOR_COMBO_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_COMBO_TXT"]);


                    var obItemsOrdShipList = new MC_ORDER_SHIPModel().GetOrderShipmentList(pMC_STYLE_H_ID, 50);
                    ob.itemsOrdShipList = (List<MC_ORDER_SHIPModel>)obItemsOrdShipList;

                    var obItemsColorList = new MC_COLORModel().OrderOrByerAccWiseColorList(ob.MC_ORDER_H_ID, null);
                    ob.itemsColorList = (List<MC_COLORModel>)obItemsColorList;

                    var obItemsCountryList = new MC_ORDER_HModel().GetOrdCountryList(ob.MC_ORDER_H_ID);
                    ob.itemsCountryList = (List<MC_ORDER_CNTRYModel>)obItemsCountryList;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CUT_INFO_PLYModel Select(long ID)
        {
            string sp = "Select_GMT_CUT_INFO_PLY";
            try
            {
                var ob = new GMT_CUT_INFO_PLYModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CUT_INFO_PLY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CUT_INFO_PLY_ID = (dr["GMT_CUT_INFO_PLY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_PLY_ID"]);
                    ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.MC_ORDER_CNTRY_ID = (dr["MC_ORDER_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_CNTRY_ID"]);
                    ob.ROLL_SL = (dr["ROLL_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ROLL_SL"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.PLY_QTY = (dr["PLY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLY_QTY"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

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