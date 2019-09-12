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
    public class MC_TRMS_DY_PRODModel
    {
        public Int64 MC_TRMS_DY_PROD_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 MC_ORD_TRMS_ITEM_ID { get; set; }
        public Int64 MC_COLOR_ID { get; set; }
        public Decimal RQD_DY_QTY { get; set; }
        public Decimal REV_DY_QTY { get; set; }
        public Decimal NET_DY_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string DY_NOTES { get; set; }
        
        //Extra Added
        public string COLOR_REF { get; set; }
        public Decimal STK_BALANCE { get; set; }
        public string ITEM_SPEC_AUTO { get; set; }
        public string MOU_CODE { get; set; }
        public long MC_FAB_PROD_ORD_H_SL { get; set; }
        public long MC_FAB_PROD_ORD_H_SPAN { get; set; }
        public string ORDER_NO_LIST { get; set; }
        public decimal TTL_BT { get; set; }
        public decimal LEFT_TO_BT { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public string QTY_MOU_CODE { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string DTL_XML { get; set; }

        public bool IS_DISABLE { get; set; }
        public object MC_COLOR
        {
            get
            {
                return new { MC_COLOR_ID = this.MC_COLOR_ID, COLOR_NAME_EN = this.COLOR_NAME_EN ?? "" };
            }
        }


        public string BatchSaveTrmsDyBooking()
        {
            const string sp = "pkg_mc_fab_booking.mc_trms_dy_prod_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_TRMS_DY_PROD_ID", Value = ob.MC_TRMS_DY_PROD_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORD_TRMS_ITEM_ID", Value = ob.MC_ORD_TRMS_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = ob.MC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRQD_DY_QTY", Value = ob.RQD_DY_QTY},
                     new CommandParameter() {ParameterName = "pREV_DY_QTY", Value = ob.REV_DY_QTY},
                     new CommandParameter() {ParameterName = "pNET_DY_QTY", Value = ob.NET_DY_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pDY_NOTES", Value = ob.DY_NOTES},
                     new CommandParameter() {ParameterName = "pDTL_XML", Value = ob.DTL_XML},
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

        public List<MC_TRMS_DY_PRODModel> QueryStockBalance(String pMC_FAB_PROD_ORD_H_LST, Int64 pMC_COLOR_ID, Int64 pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_dye_batch_plan.mc_item_dy_prod_select";
            try
            {
                var obList = new List<MC_TRMS_DY_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value =pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TRMS_DY_PRODModel ob = new MC_TRMS_DY_PRODModel();
                    ob.MC_TRMS_DY_PROD_ID = (dr["MC_TRMS_DY_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TRMS_DY_PROD_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_SPAN = (dr["MC_FAB_PROD_ORD_H_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SPAN"]);
                    ob.MC_FAB_PROD_ORD_H_SL = (dr["MC_FAB_PROD_ORD_H_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SL"]);

                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);

                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.DY_NOTES = (dr["DY_NOTES"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DY_NOTES"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NET_DY_QTY = (dr["NET_DY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_DY_QTY"]);
                    ob.STK_BALANCE = (dr["STK_BALANCE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_BALANCE"]);
                    ob.TTL_BT = (dr["TTL_BT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_BT"]);
                    ob.RQD_QTY_YDS = (dr["RQD_QTY_YDS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY_YDS"]);
                    ob.RQD_QTY_KG = (dr["RQD_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY_KG"]);

                    ob.LEFT_TO_BT = ob.NET_DY_QTY - ob.TTL_BT;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TRMS_DY_PRODModel> QueryStockBalanceScP(String pMC_FAB_PROD_ORD_H_LST, Int64 pMC_COLOR_ID, Int64 pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_dye_batch_plan.mc_item_dy_prod_select";
            try
            {
                var obList = new List<MC_TRMS_DY_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value =pMC_FAB_PROD_ORD_H_LST},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TRMS_DY_PRODModel ob = new MC_TRMS_DY_PRODModel();
                    ob.MC_TRMS_DY_PROD_ID = (dr["MC_TRMS_DY_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TRMS_DY_PROD_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_SPAN = (dr["MC_FAB_PROD_ORD_H_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SPAN"]);
                    ob.MC_FAB_PROD_ORD_H_SL = (dr["MC_FAB_PROD_ORD_H_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_SL"]);

                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.DY_NOTES = (dr["DY_NOTES"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DY_NOTES"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NET_DY_QTY = (dr["NET_DY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_DY_QTY"]);
                    ob.STK_BALANCE = (dr["STK_BALANCE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_BALANCE"]);
                    ob.TTL_BT = (dr["TTL_BT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_BT"]);
                    ob.LEFT_TO_BT = ob.NET_DY_QTY - ob.TTL_BT;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_TRMS_DY_PRODModel> FabReqStockBalance(Int64 pMC_FAB_PROD_ORD_H_ID, Int64 pMC_COLOR_ID, Int64? pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_dye_batch_plan.mc_item_dy_prod_select";
            try
            {
                var obList = new List<MC_TRMS_DY_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TRMS_DY_PRODModel ob = new MC_TRMS_DY_PRODModel();
                    ob.MC_TRMS_DY_PROD_ID = (dr["MC_TRMS_DY_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TRMS_DY_PROD_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);

                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.DY_NOTES = (dr["DY_NOTES"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DY_NOTES"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NET_DY_QTY = (dr["NET_DY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_DY_QTY"]);
                    ob.STK_BALANCE = (dr["STK_BALANCE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STK_BALANCE"]);
                    ob.TTL_BT = (dr["TTL_BT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_BT"]);
                    ob.LEFT_TO_BT = ob.NET_DY_QTY - ob.TTL_BT;
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_TRMS_DY_PRODModel> GetTrmsDyProdList(Int64 pMC_FAB_PROD_ORD_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_trms_dy_prod_select";
            try
            {
                var obList = new List<MC_TRMS_DY_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_TRMS_DY_PRODModel ob = new MC_TRMS_DY_PRODModel();

                    ob.MC_TRMS_DY_PROD_ID = (dr["MC_TRMS_DY_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TRMS_DY_PROD_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.RQD_DY_QTY = (dr["RQD_DY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_DY_QTY"]);
                    ob.REV_DY_QTY = (dr["REV_DY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_DY_QTY"]);
                    ob.NET_DY_QTY = (dr["NET_DY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_DY_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.DY_NOTES = (dr["DY_NOTES"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DY_NOTES"]);

                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_REF = (dr["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_REF"]);
                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                    
                    ob.IS_DISABLE = true;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public string DYE_BATCH_NO { get; set; }

        public string DYE_LOT_NO { get; set; }

        public decimal RQD_QTY_YDS { get; set; }

        public decimal RQD_QTY_KG { get; set; }

        public string ITEM_NAME_EN { get; set; }
    }

    public class TRIM_ISSUE_VMModel
    {
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 DYE_BT_CARD_D_TRMS_ID { get; set; }
        public Int64 MC_ORD_TRMS_ITEM_ID { get; set; }
        public Int64 RQD_QTY_YDS { get; set; }
        public Int64 RQD_QTY_KG { get; set; }
        public Int64 DYE_GSTR_BT_ISS_D3_ID { get; set; }
        public Int64 ISS_QTY { get; set; }
        public Int64 ISS_QTY_YDS { get; set; }
        public string ITEM_SPEC_AUTO { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public string XML { get; set; }
        public Int64 DYE_GSTR_ISS_H_ID { get; set; }

        public string Save()
        {
            const string sp = "PKG_DYE_GFAB_REQ.knt_btc_roll_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = ob.DYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pOption", Value =1003},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
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
        public List<TRIM_ISSUE_VMModel> Query(Int64 pDYE_BT_CARD_H_ID, Int64 pDYE_GSTR_ISS_H_ID)
        {
            string sp = "PKG_DYE_BATCH_PLAN.mc_item_dy_prod_select";
            try
            {
                var obList = new List<TRIM_ISSUE_VMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = pDYE_GSTR_ISS_H_ID},

                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TRIM_ISSUE_VMModel ob = new TRIM_ISSUE_VMModel();
                    ob.DYE_BT_CARD_D_TRMS_ID = (dr["DYE_BT_CARD_D_TRMS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_D_TRMS_ID"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);
                    ob.RQD_QTY_YDS = (dr["RQD_QTY_YDS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_YDS"]);
                    ob.RQD_QTY_KG = (dr["RQD_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_KG"]);
                    ob.DYE_GSTR_BT_ISS_D3_ID = (dr["DYE_GSTR_BT_ISS_D3_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_BT_ISS_D3_ID"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.ISS_QTY_YDS = (dr["ISS_QTY_YDS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_YDS"]);

                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_ISS_QTY"]);
                    ob.TTL_ISS_QTY_YDS = (dr["TTL_ISS_QTY_YDS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TTL_ISS_QTY_YDS"]);

                    ob.STK_BALANCE = (dr["STK_BALANCE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STK_BALANCE"]);
                    
                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long TTL_ISS_QTY { get; set; }
        public long TTL_ISS_QTY_YDS { get; set; }

        public long STK_BALANCE { get; set; }
    }

}