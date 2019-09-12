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
    public class MC_INQR_HModel
    {
        public Int64 MC_INQR_H_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public string INQR_NO { get; set; }
        public DateTime INQR_DT { get; set; }
        public string QUOTE_NO { get; set; }
        public DateTime QUOTE_DT { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public Int64 LK_INCOTERM_ID { get; set; }
        public Int64 LK_LCTERM_ID { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Decimal CURR_CONV_TK { get; set; }
        public Int64 CONS_MOU_ID { get; set; }
        public Int64 QTY_PER_MOU { get; set; }
        public Int64 WT_MOU_ID { get; set; }
        public Int64 MIN_ORD_QTY1 { get; set; }
        public Int64 MIN_ORD_QTY2 { get; set; }
        public string ORD_QTY_DESC { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 PCS_PER_PACK { get; set; }
        public string SHIPMENT_DT { get; set; }
        public string DELIVERY_WK { get; set; }
        public string OPD_DEAD_LN { get; set; }
        public Int64 MC_LD_PORT_ID { get; set; }
        public Decimal AVG_PRICE { get; set; }
        public Decimal QUOTE_PRICE { get; set; }
        public Int64? EXECUTED_BY { get; set; }
        public Int64 LK_INQ_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string LK_INCOTERM { get; set; }
        public string LK_LCTERM { get; set; }

        public string CURR_NAME_EN { get; set; }

        public string CONS_MOU { get; set; }

        public string WT_MOU { get; set; }

        public string QTY_MOU { get; set; }

        public string CURR_SYMBOL { get; set; }

        public string STYLE_NO { get; set; }

        public string STYLE_DESC { get; set; }

        public string STYL_EXT_NO { get; set; }
        public string REF_STYLE_NO { get; set; }
        public Int64? MC_STYLE_ID { get; set; }

        public string Save()
        {
            const string sp = "pkg_merchandising.mc_inqr_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value = ob.MC_INQR_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pINQR_NO", Value = ob.INQR_NO},
                     new CommandParameter() {ParameterName = "pINQR_DT", Value = ob.INQR_DT},
                     new CommandParameter() {ParameterName = "pQUOTE_NO", Value = ob.QUOTE_NO},
                     new CommandParameter() {ParameterName = "pQUOTE_DT", Value = ob.QUOTE_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pLK_INCOTERM_ID", Value = ob.LK_INCOTERM_ID},
                     new CommandParameter() {ParameterName = "pLK_LCTERM_ID", Value = ob.LK_LCTERM_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCURR_CONV_TK", Value = ob.CURR_CONV_TK},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_MOU", Value = ob.QTY_PER_MOU},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pMIN_ORD_QTY1", Value = ob.MIN_ORD_QTY1},
                     new CommandParameter() {ParameterName = "pMIN_ORD_QTY2", Value = ob.MIN_ORD_QTY2},
                     new CommandParameter() {ParameterName = "pORD_QTY_DESC", Value = ob.ORD_QTY_DESC},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pSHIPMENT_DT", Value = ob.SHIPMENT_DT},
                     new CommandParameter() {ParameterName = "pDELIVERY_WK", Value = ob.DELIVERY_WK},
                     new CommandParameter() {ParameterName = "pOPD_DEAD_LN", Value = ob.OPD_DEAD_LN},
                     new CommandParameter() {ParameterName = "pMC_LD_PORT_ID", Value = ob.MC_LD_PORT_ID},
                     new CommandParameter() {ParameterName = "pAVG_PRICE", Value = ob.AVG_PRICE},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                     new CommandParameter() {ParameterName = "pEXECUTED_BY", Value = ob.EXECUTED_BY},
                     new CommandParameter() {ParameterName = "pLK_INQ_STATUS_ID", Value = ob.LK_INQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_INQR_H_ID", Value =null, Direction = ParameterDirection.Output}
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

        public string Update()
        {
            const string sp = "pkg_merchandising.mc_inqr_h_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value = ob.MC_INQR_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pINQR_NO", Value = ob.INQR_NO},
                     new CommandParameter() {ParameterName = "pINQR_DT", Value = ob.INQR_DT},
                     new CommandParameter() {ParameterName = "pQUOTE_NO", Value = ob.QUOTE_NO},
                     new CommandParameter() {ParameterName = "pQUOTE_DT", Value = ob.QUOTE_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pLK_INCOTERM_ID", Value = ob.LK_INCOTERM_ID},
                     new CommandParameter() {ParameterName = "pLK_LCTERM_ID", Value = ob.LK_LCTERM_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCURR_CONV_TK", Value = ob.CURR_CONV_TK},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_MOU", Value = ob.QTY_PER_MOU},
                     new CommandParameter() {ParameterName = "pWT_MOU_ID", Value = ob.WT_MOU_ID},
                     new CommandParameter() {ParameterName = "pMIN_ORD_QTY1", Value = ob.MIN_ORD_QTY1},
                     new CommandParameter() {ParameterName = "pMIN_ORD_QTY2", Value = ob.MIN_ORD_QTY2},
                     new CommandParameter() {ParameterName = "pORD_QTY_DESC", Value = ob.ORD_QTY_DESC},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pSHIPMENT_DT", Value = ob.SHIPMENT_DT},
                     new CommandParameter() {ParameterName = "pDELIVERY_WK", Value = ob.DELIVERY_WK},
                     new CommandParameter() {ParameterName = "pOPD_DEAD_LN", Value = ob.OPD_DEAD_LN},
                     new CommandParameter() {ParameterName = "pMC_LD_PORT_ID", Value = ob.MC_LD_PORT_ID},
                     new CommandParameter() {ParameterName = "pAVG_PRICE", Value = ob.AVG_PRICE},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE", Value = ob.QUOTE_PRICE},
                     new CommandParameter() {ParameterName = "pEXECUTED_BY", Value = ob.EXECUTED_BY},
                     new CommandParameter() {ParameterName = "pLK_INQ_STATUS_ID", Value = ob.LK_INQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "pkg_merchandising.mc_inqr_h_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value = ob.MC_INQR_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<MC_INQR_HModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_inqr_h_select";
            try
            {
                var obList = new List<MC_INQR_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_INQR_HModel ob = new MC_INQR_HModel();
                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.INQR_NO = (dr["INQR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INQR_NO"]);
                    ob.INQR_DT = (dr["INQR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INQR_DT"]);
                    ob.QUOTE_NO = (dr["QUOTE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUOTE_NO"]);
                    ob.QUOTE_DT = (dr["QUOTE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QUOTE_DT"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.LK_INCOTERM_ID = (dr["LK_INCOTERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INCOTERM_ID"]);
                    ob.LK_LCTERM_ID = (dr["LK_LCTERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LCTERM_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_TK = (dr["CURR_CONV_TK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_TK"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.QTY_PER_MOU = (dr["QTY_PER_MOU"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_PER_MOU"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.MIN_ORD_QTY1 = (dr["MIN_ORD_QTY1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_ORD_QTY1"]);
                    ob.MIN_ORD_QTY2 = (dr["MIN_ORD_QTY2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_ORD_QTY2"]);
                    ob.ORD_QTY_DESC = (dr["ORD_QTY_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_QTY_DESC"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                    ob.SHIPMENT_DT = (dr["SHIPMENT_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIPMENT_DT"]);
                    ob.DELIVERY_WK = (dr["DELIVERY_WK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELIVERY_WK"]);
                    ob.OPD_DEAD_LN = (dr["OPD_DEAD_LN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPD_DEAD_LN"]);
                    ob.MC_LD_PORT_ID = (dr["MC_LD_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_PORT_ID"]);
                    ob.AVG_PRICE = (dr["AVG_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);
                    ob.EXECUTED_BY = (dr["EXECUTED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXECUTED_BY"]);
                    ob.LK_INQ_STATUS_ID = (dr["LK_INQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INQ_STATUS_ID"]);

                    ob.CONS_MOU = (dr["CONS_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONS_MOU"]);
                    ob.WT_MOU = (dr["WT_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WT_MOU"]);
                    ob.QTY_MOU = (dr["QTY_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU"]);
                    ob.CONS_MOU = (dr["CONS_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONS_MOU"]);
                    ob.LK_INCOTERM = (dr["LK_INCOTERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_INCOTERM"]);
                    ob.LK_LCTERM = (dr["LK_LCTERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_LCTERM"]);

                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    ob.CURR_SYMBOL = (dr["CURR_SYMBOL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_SYMBOL"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_INQR_HModel> InquiryDataForList()
        {
            string sp = "pkg_merchandising.mc_inqr_h_select";
            try
            {
                var obList = new List<MC_INQR_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_INQR_HModel ob = new MC_INQR_HModel();
                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.INQR_NO = (dr["INQR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INQR_NO"]);
                    ob.INQR_DT = (dr["INQR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INQR_DT"]);
                    ob.QUOTE_NO = (dr["QUOTE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUOTE_NO"]);
                    ob.QUOTE_DT = (dr["QUOTE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QUOTE_DT"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.LK_INQ_STATUS = (dr["LK_INQ_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_INQ_STATUS"]);
                    ob.LK_INCOTERM = (dr["LK_INCOTERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_INCOTERM"]);
                    ob.LK_LCTERM = (dr["LK_LCTERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_LCTERM"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.REF_STYLE_NO = (dr["REF_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_STYLE_NO"]);
                    ob.MC_STYLE_ID = (dr["MC_STYLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MC_INQR_HModel Select(Int64 ID)
        {
            string sp = "pkg_merchandising.mc_inqr_h_select";
            try
            {
                var ob = new MC_INQR_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.INQR_NO = (dr["INQR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INQR_NO"]);
                    ob.INQR_DT = (dr["INQR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INQR_DT"]);
                    ob.QUOTE_NO = (dr["QUOTE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUOTE_NO"]);
                    ob.QUOTE_DT = (dr["QUOTE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QUOTE_DT"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.LK_INCOTERM_ID = (dr["LK_INCOTERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INCOTERM_ID"]);
                    ob.LK_LCTERM_ID = (dr["LK_LCTERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LCTERM_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.CURR_CONV_TK = (dr["CURR_CONV_TK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_TK"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);

                    ob.QTY_PER_MOU = (dr["QTY_PER_MOU"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_PER_MOU"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.MIN_ORD_QTY1 = (dr["MIN_ORD_QTY1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_ORD_QTY1"]);
                    ob.MIN_ORD_QTY2 = (dr["MIN_ORD_QTY2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_ORD_QTY2"]);
                    ob.ORD_QTY_DESC = (dr["ORD_QTY_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_QTY_DESC"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                    ob.SHIPMENT_DT = (dr["SHIPMENT_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIPMENT_DT"]);
                    ob.DELIVERY_WK = (dr["DELIVERY_WK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELIVERY_WK"]);
                    ob.OPD_DEAD_LN = (dr["OPD_DEAD_LN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPD_DEAD_LN"]);
                    ob.MC_LD_PORT_ID = (dr["MC_LD_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_PORT_ID"]);
                    ob.AVG_PRICE = (dr["AVG_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_PRICE"]);
                    ob.QUOTE_PRICE = (dr["QUOTE_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE"]);


                    ob.CONS_MOU = (dr["CONS_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONS_MOU"]);
                    ob.WT_MOU = (dr["WT_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WT_MOU"]);
                    ob.QTY_MOU = (dr["QTY_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU"]);
                    ob.CONS_MOU = (dr["CONS_MOU"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONS_MOU"]);
                    ob.LK_INCOTERM = (dr["LK_INCOTERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_INCOTERM"]);
                    ob.LK_LCTERM = (dr["LK_LCTERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_LCTERM"]);

                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    ob.CURR_SYMBOL = (dr["CURR_SYMBOL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_SYMBOL"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);


                    if (dr["EXECUTED_BY"] != DBNull.Value)
                    {
                        ob.EXECUTED_BY = Convert.ToInt64(dr["EXECUTED_BY"]);
                    }

                    ob.LK_INQ_STATUS_ID = (dr["LK_INQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INQ_STATUS_ID"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LK_INQ_STATUS { get; set; }
    }
}