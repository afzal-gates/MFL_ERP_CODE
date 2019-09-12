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
    public class GMT_CUT_INFO_SZ_RTO_ITEMModel
    {
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public string ITEM_NAME_EN { get; set; }
        private List<GMT_CUT_INFO_SZ_RTOModel> _itemsOrdSizeRatio = null;
        public List<GMT_CUT_INFO_SZ_RTOModel> itemsOrdSizeRatio
        {
            get
            {
                if (_itemsOrdSizeRatio == null)
                {
                    _itemsOrdSizeRatio = new List<GMT_CUT_INFO_SZ_RTOModel>();
                }
                return _itemsOrdSizeRatio;
            }
            set
            {
                _itemsOrdSizeRatio = value;
            }
        }


        public List<GMT_CUT_INFO_SZ_RTO_ITEMModel> GetCutInfoSzRto(Int64 pGMT_MRKR_ID, Int64? pGMT_CUT_INFO_ID = null)
        {
            string sp = "pkg_cut_lay_chart.gmt_cut_info_select";
            try
            {
                var ratioGrpList = new GMT_MRKR_RATIO_GRPModel().GetMrkrRatioGrpList();

                OraDatabase db = new OraDatabase();
                var obItemList = new List<GMT_CUT_INFO_SZ_RTO_ITEMModel>();
                var dsItm = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pGMT_MRKR_ID", Value =pGMT_MRKR_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow drItm in dsItm.Tables[0].Rows)
                {
                    GMT_CUT_INFO_SZ_RTO_ITEMModel obItem = new GMT_CUT_INFO_SZ_RTO_ITEMModel();
                    obItem.MC_STYLE_D_ITEM_ID = (drItm["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drItm["MC_STYLE_D_ITEM_ID"]);
                    obItem.ITEM_NAME_EN = (drItm["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drItm["ITEM_NAME_EN"]);

                    var obList = new List<GMT_CUT_INFO_SZ_RTOModel>();
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pGMT_MRKR_ID", Value =pGMT_MRKR_ID},
                         new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value =obItem.MC_STYLE_D_ITEM_ID},
                         new CommandParameter() {ParameterName = "pGMT_CUT_INFO_ID", Value =pGMT_CUT_INFO_ID},

                         new CommandParameter() {ParameterName = "pOption", Value =3007},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     }, sp);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        GMT_CUT_INFO_SZ_RTOModel ob = new GMT_CUT_INFO_SZ_RTOModel();

                        ob.GMT_CUT_INFO_SZ_RTO_ID = (dr["GMT_CUT_INFO_SZ_RTO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_SZ_RTO_ID"]);

                        if ((dr["GMT_CUT_INFO_ID"] != DBNull.Value))
                            ob.GMT_CUT_INFO_ID = (dr["GMT_CUT_INFO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CUT_INFO_ID"]);

                        ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                        ob.GMT_MRKR_RATIO_GRP_ID = (dr["GMT_MRKR_RATIO_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_RATIO_GRP_ID"]);
                        ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                        ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);

                        if ((dr["RATIO_QTY"] != DBNull.Value))
                            ob.RATIO_QTY = (dr["RATIO_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RATIO_QTY"]);

                        ob.itemsRatioGrpList = (List<GMT_MRKR_RATIO_GRPModel>)ratioGrpList;

                        obList.Add(ob);
                    }
                    obItem.itemsOrdSizeRatio = obList;
                    obItemList.Add(obItem);
                }
                return obItemList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class GMT_CUT_INFO_SZ_RTOModel
    {
        public Int64 GMT_CUT_INFO_SZ_RTO_ID { get; set; }
        public Int64 GMT_CUT_INFO_ID { get; set; }
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public Int64 GMT_MRKR_RATIO_GRP_ID { get; set; }
        public Int64 MC_SIZE_ID { get; set; }
        public Int64 RATIO_QTY { get; set; }

        public string ITEM_NAME_EN { get; set; }
        public string SIZE_CODE { get; set; }

        private List<GMT_MRKR_RATIO_GRPModel> _itemsRatioGrpList = null;
        public List<GMT_MRKR_RATIO_GRPModel> itemsRatioGrpList
        {
            get
            {
                if (_itemsRatioGrpList == null)
                {
                    _itemsRatioGrpList = new List<GMT_MRKR_RATIO_GRPModel>();
                }
                return _itemsRatioGrpList;
            }
            set
            {
                _itemsRatioGrpList = value;
            }
        }

    }
}