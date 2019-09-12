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
    public class KNT_YDP_D_YRNModel
    {
        public Int64 KNT_YDP_D_YRN_ID { get; set; }
        public Int64 KNT_YDP_D_COL_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64? RF_BRAND_ID { get; set; }
        public Int64? KNT_YRN_LOT_ID { get; set; }
        public Int64? LK_YFAB_PART_ID { get; set; }
        public Decimal RATIO_QTY { get; set; }
        public Int64 RQD_YD_QTY { get; set; }
        public Int64 RQD_CONE_QTY { get; set; }
        public Decimal QTY_PER_CONE { get; set; }
        public string REMARKS { get; set; }
        public string KNT_YRN_LOT_ID_LST { get; set; }

        public Int64? KNT_YD_PRG_H_ID { get; set; }
        public string PRG_REF_NO { get; set; }
        public Int64? YRN_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string YARN_DETAIL { get; set; }


        public string Save()
        {
            const string sp = "SP_KNT_YDP_D_YRN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_YRN_ID", Value = ob.KNT_YDP_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_COL_ID", Value = ob.KNT_YDP_D_COL_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pLK_YFAB_PART_ID", Value = ob.LK_YFAB_PART_ID},
                     new CommandParameter() {ParameterName = "pRATIO_QTY", Value = ob.RATIO_QTY},
                     new CommandParameter() {ParameterName = "pRQD_YD_QTY", Value = ob.RQD_YD_QTY},
                     new CommandParameter() {ParameterName = "pRQD_CONE_QTY", Value = ob.RQD_CONE_QTY},
                     new CommandParameter() {ParameterName = "pQTY_PER_CONE", Value = ob.QTY_PER_CONE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<KNT_YDP_D_YRNModel> queryData(Int64? pKNT_YDP_D_COL_ID = null)
        {
            string sp = "PKG_KNT_YD_PRG.knt_ydp_d_yrn_select";
            try
            {
                var obList = new List<KNT_YDP_D_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YDP_D_COL_ID", Value =pKNT_YDP_D_COL_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        KNT_YDP_D_YRNModel ob = new KNT_YDP_D_YRNModel();
                        ob.KNT_YDP_D_YRN_ID = (dr["KNT_YDP_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_YRN_ID"]);
                        ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);
                        ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);




                        ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);


                        if (dr["RF_BRAND_ID"] != DBNull.Value)
                        {
                            ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);
                        }

                        if (dr["KNT_YRN_LOT_ID"] != DBNull.Value)
                        {
                            ob.KNT_YRN_LOT_ID = Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                        }


                        if (dr["LK_YFAB_PART_ID"] != DBNull.Value)
                        {
                            ob.LK_YFAB_PART_ID = Convert.ToInt64(dr["LK_YFAB_PART_ID"]);
                        }

                        ob.KNT_YRN_LOT_ID_LST = (dr["KNT_YRN_LOT_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_ID_LST"]);

                        ob.RATIO_QTY = (dr["RATIO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATIO_QTY"]);
                        ob.RQD_YD_QTY = (dr["RQD_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_YD_QTY"]);
                        ob.RQD_CONE_QTY = (dr["RQD_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_CONE_QTY"]);
                        ob.QTY_PER_CONE = (dr["QTY_PER_CONE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_CONE"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        obList.Add(ob);
                    }
                }
                else
                {
                    KNT_YDP_D_YRNModel ob = new KNT_YDP_D_YRNModel()
                    {
                        KNT_YDP_D_YRN_ID = -1
                    };

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YDP_D_YRNModel> GetColorYrnByPrg(long pKNT_YD_PRG_H_ID)
        {
            string sp = "PKG_KNT_YD_PRG.knt_ydp_d_yrn_select";
            try
            {
                var obList = new List<KNT_YDP_D_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value =pKNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YDP_D_YRNModel ob = new KNT_YDP_D_YRNModel();

                    ob.KNT_YD_PRG_H_ID = (dr["KNT_YD_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PRG_H_ID"]);
                    ob.PRG_REF_NO = (dr["PRG_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_REF_NO"]);
                    ob.YRN_COLOR_ID = (dr["YRN_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YRN_COLOR_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);

                    if (dr["RF_BRAND_ID"] != DBNull.Value)
                    {
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);
                    }

                    if (dr["KNT_YRN_LOT_ID"] != DBNull.Value)
                    {
                        ob.KNT_YRN_LOT_ID = Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    }

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.YARN_DETAIL = (dr["YARN_DETAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAIL"]);

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




    public class KNT_YDP_YRN_REQModel
    {
        public Int64 KNT_YRN_STR_REQ_D_ID { get; set; }
        public Int64 KNT_YRN_STR_REQ_H_ID { get; set; }

        public Int64? ORD_ROW_SPAN { get; set; }
        public Int64? ORD_SL { get; set; }
        public Int64? COLOR_ROW_SPAN { get; set; }
        public Int64? COLOR_SL { get; set; }
        public Int64? KNT_YDP_D_COL_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public string YD_COLOR_NAME { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64? RF_BRAND_ID { get; set; }
        public Int64? KNT_YRN_LOT_ID { get; set; }
        public string YR_SPEC_DESC { get; set; }

        public decimal? RQD_YD_QTY { get; set; }
        public decimal? REQ_YRN_QTY_DONE { get; set; }
        public decimal? RQD_YRN_QTY { get; set; }                
        public decimal? RQD_CONE_QTY { get; set; }        

        private List<RF_BRANDModel> _itemsBrand = null;
        public List<RF_BRANDModel> itemsBrand
        {
            get
            {
                if (_itemsBrand == null)
                {
                    _itemsBrand = new List<RF_BRANDModel>();
                }
                return _itemsBrand;
            }
            set
            {
                _itemsBrand = value;
            }
        }



        public List<KNT_YDP_YRN_REQModel> GetYrnRqdList4Yd(Int64 pKNT_YD_PRG_H_ID, Int64? pKNT_YRN_STR_REQ_H_ID)
        {
            string sp = "pkg_knt_yd_prg.knt_ydp_d_yrn_select";
            try
            {
                var obList = new List<KNT_YDP_YRN_REQModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = pKNT_YD_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = pKNT_YRN_STR_REQ_H_ID},

                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YDP_YRN_REQModel ob = new KNT_YDP_YRN_REQModel();
                    ob.KNT_YRN_STR_REQ_D_ID = (dr["KNT_YRN_STR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_D_ID"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);

                    ob.ORD_ROW_SPAN = (dr["ORD_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORD_ROW_SPAN"]);
                    ob.ORD_SL = (dr["ORD_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORD_SL"]);

                    ob.COLOR_ROW_SPAN = (dr["COLOR_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COLOR_ROW_SPAN"]);                    
                    ob.COLOR_SL = (dr["COLOR_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COLOR_SL"]);

                    ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);

                    ob.YD_COLOR_NAME = (dr["YD_COLOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_COLOR_NAME"]);

                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
                    
                    if (dr["RF_BRAND_ID"] != DBNull.Value)
                        ob.RF_BRAND_ID = Convert.ToInt64(dr["RF_BRAND_ID"]);


                    if (dr["KNT_YRN_LOT_ID"] != DBNull.Value)
                        ob.KNT_YRN_LOT_ID = Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    if (dr["RQD_YD_QTY"] != DBNull.Value)
                        ob.RQD_YD_QTY = (dr["RQD_YD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_YD_QTY"]);

                    ob.REQ_YRN_QTY_DONE = (dr["REQ_YRN_QTY_DONE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_YRN_QTY_DONE"]);

                    if (dr["RQD_YRN_QTY"] != DBNull.Value)
                        ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY"]);

                    if (dr["RQD_CONE_QTY"] != DBNull.Value)
                        ob.RQD_CONE_QTY = (dr["RQD_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_CONE_QTY"]);

                    var obItemsBrand = new RF_BRANDModel().CategoryWiseBrandList(2, 3003, null, "S", ob.YARN_ITEM_ID);
                    ob.itemsBrand = (List<RF_BRANDModel>)obItemsBrand;

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