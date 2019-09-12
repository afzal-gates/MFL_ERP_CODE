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
    public class StyleOrderViewModel
    {
        public Int64? MC_STYLE_H_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64? MC_BUYER_ID { get; set; }
        public Int64? MC_INQR_H_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string STYL_EXT_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string MATERIAL_DESC { get; set; }
        public Int64? LK_STYL_DEV_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public string ORDER_NO { get; set; }
        public Int64? LK_ORD_TYPE_ID { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string HR_COUNTRY_ID_LST { get; set; }

        public DateTime? ORD_CONF_DT { get; set; }
        public DateTime? SHIP_DT { get; set; }
        public Int64? LEAD_TIME { get; set; }
        public Decimal? UNIT_PRICE { get; set; }
        public Int64? RF_CURRENCY_ID { get; set; }
        public Int64? LK_ORD_STATUS_ID { get; set; }
        public Int64? TOT_ORD_QTY { get; set; }
        public Decimal? TOT_ORD_VALUE { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public Int64? CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64? LAST_UPDATED_BY { get; set; }

        public Int64? MC_TNA_TMPLT_H_ID { get; set; }
        public string JOB_TRAC_NO { get; set; }

        public string IS_TNA_FINALIZED { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public long? LK_STYL_DEV_TYP_ID { get; set; }
        public Int64? MC_SMP_REQ_H_ID { get; set; }
        public string IS_N_R { get; set; }
        public string HAS_EXT { get; set; }
        public Int64? MC_STYLE_H_EXT_ID_ORD { get; set; }
        public string IS_MULTI_SHIP_DT { get; set; }
        public string MC_SIZE_LST { get; set; }
        public List<MC_ORDER_SHIPModel> itmsOrdShipDT { get; set; }


        private List<MC_STYLE_D_ITEMModel> _items = null;
        public List<MC_STYLE_D_ITEMModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<MC_STYLE_D_ITEMModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public string EXT_XML { get; set; }

        public string ALL_ITEM_LIST { get; set; }

        public string ITEM_LIST { get; set; }

        public string HAS_MULTI_COL_PACK { get; set; }

        public string HAS_MODEL { get; set; }

        public string HAS_COMBO { get; set; }

        public string HAS_SET { get; set; }

        public string SZ_RANGE { get; set; }

        public string SEASON_REF { get; set; }

        public Int64? LK_SEASON_ID { get; set; }

        public Int64? PCS_PER_PACK { get; set; }

        public Int64? NO_OF_SET { get; set; }

        public Int64? NO_OF_REV { get; set; }

        public Int64? MANUF_ID { get; set; }

        public Int64? ORIGIN_ID { get; set; }

        public string COMP_STYLE_NO { get; set; }

        public string REF_STYLE_NO { get; set; }

        public Int64? QTY_MOU_ID_ST { get; set; }
        public string MKT_XML { get; set; }
        public string IS_OC { get; set; }

        public string IS_EXT_AUTO { get; set; }
        public Decimal? CURR_CONV_LOC { get; set;}

        public Int64? HR_COMPANY_ID { get; set; }
        public Int64? PROD_UNIT_ID { get; set; }
        public Int64? MC_ORDER_SHIP_ID { get; set; }
        public List<MC_GMT_CAP_ITEMModel> cap_itms { get; set; }

        public string IS_PROV { get; set; }
        public DateTime? TGT_ORD_DOC_RCV_DT { get; set; }
        
        public string IS_OTP_SEND { get; set; }
        public string OTP_ORDR_BKNG { get; set; }
        public string OTP_ORDR_BKNG_CHK { get; set; }
        
        


        public string Update()
        {
            const string sp = "pkg_merchandising.mc_order_style_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                var xmlOrdShipDT = new System.Text.StringBuilder();
                xmlOrdShipDT.Append("<trans>");

                foreach (var itmOrdShipDT in ob.itmsOrdShipDT)
                {
                    xmlOrdShipDT.AppendLine();
                    xmlOrdShipDT.Append(" <row ");

                    xmlOrdShipDT.Append(" MC_ORDER_SHIP_ID=\"" + itmOrdShipDT.MC_ORDER_SHIP_ID + "\"");
                    xmlOrdShipDT.Append(" SHIP_DT=\"" + string.Format("{0:yyyy-MMM-dd}", itmOrdShipDT.SHIP_DT) + "\"");
                    xmlOrdShipDT.Append(" SHIP_DESC=\"" + itmOrdShipDT.SHIP_DESC + "\"");
                    xmlOrdShipDT.Append(" CAP_ITEM_XML=\"" + itmOrdShipDT.CapItmXML + "\"");
                    xmlOrdShipDT.Append(" />");
                }

                xmlOrdShipDT.AppendLine();
                xmlOrdShipDT.AppendLine("</trans>");

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_INQR_H_ID", Value = ob.MC_INQR_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pSTYL_EXT_NO", Value = ob.STYL_EXT_NO},
                     new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = ob.STYLE_DESC},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID}, 
                     new CommandParameter() {ParameterName = "pMATERIAL_DESC", Value = ob.MATERIAL_DESC}, 
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = ob.LK_STYL_DEV_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pSZ_RANGE", Value = ob.SZ_RANGE},

                     new CommandParameter() {ParameterName = "pORIGIN_ID", Value = ob.ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pMANUF_ID", Value = ob.MANUF_ID},
                     new CommandParameter() {ParameterName = "pHAS_SET", Value = ob.HAS_SET},
                     new CommandParameter() {ParameterName = "pHAS_COMBO", Value = ob.HAS_COMBO},
                     new CommandParameter() {ParameterName = "pNO_OF_SET", Value = ob.NO_OF_SET},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID_ST", Value = ob.QTY_MOU_ID_ST},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pHAS_MODEL", Value = ob.HAS_MODEL},
                     new CommandParameter() {ParameterName = "pHAS_MULTI_COL_PACK", Value = ob.HAS_MULTI_COL_PACK},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},

                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = ob.ORDER_NO},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value = ob.WORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pORD_CONF_DT", Value = ob.ORD_CONF_DT},
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value = ob.SHIP_DT},
                     new CommandParameter() {ParameterName = "pLEAD_TIME", Value = ob.LEAD_TIME},
                     new CommandParameter() {ParameterName = "pTOT_ORD_QTY", Value = ob.TOT_ORD_QTY},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = ob.LK_ORD_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pIS_N_R", Value = ob.IS_N_R},
                     new CommandParameter() {ParameterName = "pHAS_EXT", Value = ob.HAS_EXT},
                     new CommandParameter() {ParameterName = "pIS_MULTI_SHIP_DT", Value = ob.IS_MULTI_SHIP_DT },
                     
                     new CommandParameter() {ParameterName = "pCURR_CONV_LOC", Value = ob.CURR_CONV_LOC ?? 0 },
                     new CommandParameter() {ParameterName = "pMKT_XML", Value = ob.MKT_XML },

                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID_LST", Value = ob.HR_COUNTRY_ID_LST }, 
                     new CommandParameter() {ParameterName = "pMC_SHIPMENT_DATE_DTL", Value = xmlOrdShipDT},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "V_MC_ORDER_H_ID", Value =null, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_STYLE_H_ID", Value =null, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pIS_OC", Value = ob.IS_OC },
                     new CommandParameter() {ParameterName = "pMC_SIZE_LST", Value = ob.MC_SIZE_LST },
                     new CommandParameter() {ParameterName = "pIS_EXT_AUTO", Value = ob.IS_EXT_AUTO },
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pIS_PROV", Value = ob.IS_PROV},
                     new CommandParameter() {ParameterName = "pTGT_ORD_DOC_RCV_DT", Value = ob.TGT_ORD_DOC_RCV_DT}

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


        public string SaveOrder()
        {
            const string sp = "pkg_merchandising.order_confirmation_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {

                var xmlOrdShipDT = new System.Text.StringBuilder();
                xmlOrdShipDT.Append("<trans>");

                foreach (var itmOrdShipDT in ob.itmsOrdShipDT)
                {                    
                    xmlOrdShipDT.AppendLine();
                    xmlOrdShipDT.Append(" <row ");
                    
                    xmlOrdShipDT.Append(" MC_ORDER_SHIP_ID=\"" + itmOrdShipDT.MC_ORDER_SHIP_ID + "\"");
                    xmlOrdShipDT.Append(" SHIP_DT=\"" + string.Format("{0:yyyy-MMM-dd}", itmOrdShipDT.SHIP_DT) + "\"");
                    xmlOrdShipDT.Append(" SHIP_DESC=\"" + itmOrdShipDT.SHIP_DESC + "\"");
                    xmlOrdShipDT.Append(" CAP_ITEM_XML=\"" + itmOrdShipDT.CapItmXML + "\"");
                    xmlOrdShipDT.Append(" />");
                }

                xmlOrdShipDT.AppendLine();
                xmlOrdShipDT.AppendLine("</trans>");

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = ob.ORDER_NO},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value = ob.WORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pORD_CONF_DT", Value = ob.ORD_CONF_DT},
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value = ob.SHIP_DT},
                     new CommandParameter() {ParameterName = "pLEAD_TIME", Value = ob.LEAD_TIME},
                     new CommandParameter() {ParameterName = "pTOT_ORD_QTY", Value = ob.TOT_ORD_QTY},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = ob.LK_ORD_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pIS_MULTI_SHIP_DT", Value = ob.IS_MULTI_SHIP_DT },                   
                     new CommandParameter() {ParameterName = "pCURR_CONV_LOC", Value = ob.CURR_CONV_LOC ?? 0 },
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID_LST", Value = ob.HR_COUNTRY_ID_LST }, 
                     new CommandParameter() {ParameterName = "pMC_SHIPMENT_DATE_DTL", Value = xmlOrdShipDT},

                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},

                     new CommandParameter() {ParameterName = "pIS_PROV", Value = ob.IS_PROV},
                     new CommandParameter() {ParameterName = "pTGT_ORD_DOC_RCV_DT", Value = ob.TGT_ORD_DOC_RCV_DT},

                     new CommandParameter() {ParameterName = "pIS_OTP_SEND", Value = ob.IS_OTP_SEND},
                     new CommandParameter() {ParameterName = "pOTP_ORDR_BKNG", Value = ob.OTP_ORDR_BKNG},
                     new CommandParameter() {ParameterName = "pOTP_ORDR_BKNG_CHK", Value = ob.OTP_ORDR_BKNG_CHK},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opMC_ORDER_H_ID", Value =null, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opJOB_TRAC_NO", Value =null, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pRTN_LK_ORD_STATUS_ID", Value =null, Direction = ParameterDirection.Output},

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

        public string SameOrderTnASave()
        {
            const string sp = "pkg_tna.ord_tna_save4related_ord";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},                    
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =HttpContext.Current.Session["multiScUserId"]},
                                                       
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

        public StyleOrderViewModel SelectOrder(long ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var ob = new StyleOrderViewModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                    {
                        ob.ORD_CONF_DT = Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    }

                    if (dr["SHIP_DT"] != DBNull.Value)
                    {
                        ob.SHIP_DT = Convert.ToDateTime(dr["SHIP_DT"]);
                    }

                    if (dr["TGT_ORD_DOC_RCV_DT"] != DBNull.Value)
                    {
                        ob.TGT_ORD_DOC_RCV_DT = Convert.ToDateTime(dr["TGT_ORD_DOC_RCV_DT"]);
                    }


                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                    {
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    }

                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);

                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);


                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.NO_GMT_ITEM_ENTRY = this.FindNoOfGmtItemsByStyle(ob.MC_STYLE_H_ID);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TNA_FINALIZED"]);

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PROV"]);

                    ob.MC_STYLE_H_EXT_ID_ORD = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    if (dr["LK_STYL_DEV_TYP_ID"] != DBNull.Value)
                        ob.LK_STYL_DEV_TYP_ID = (dr["LK_STYL_DEV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_TYP_ID"]);

                    ob.MC_SMP_REQ_H_ID = (dr["MC_SMP_REQ_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_SMP_REQ_H_ID"]);

                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_EXT"]);

                    ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);

                    ob.IS_ORD_FINALIZED = (dr["IS_ORD_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ORD_FINALIZED"]);

                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);

                    ob.HR_COUNTRY_ID_LST = (dr["HR_COUNTRY_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_COUNTRY_ID_LST"]);
                    ob.cap_itms = new List<MC_GMT_CAP_ITEMModel>();

                    if (dr["MC_ORDER_SHIP_ID"] != DBNull.Value)
                    {
                        ob.cap_itms = new MC_GMT_CAP_ITEMModel().Query(Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]));

                    }

                    var obItemsOrdShipDt = new MC_ORDER_HModel().GetOrdShipList(ob.MC_ORDER_H_ID);
                    ob.itmsOrdShipDT = (List<MC_ORDER_SHIPModel>)obItemsOrdShipDt;

                    if (ob.itmsOrdShipDT.Count == 0)
                    {
                        List<MC_ORDER_SHIPModel> obShipDummyList = new List<MC_ORDER_SHIPModel>();

                        MC_ORDER_SHIPModel obShipDummy = new MC_ORDER_SHIPModel();
                        obShipDummy.MC_ORDER_SHIP_ID = 0;
                        obShipDummy.MC_ORDER_H_ID = ob.MC_ORDER_H_ID;
                        obShipDummy.SHIP_DT = ob.SHIP_DT;
                        obShipDummy.SHIP_DESC = "Shipment-1";
                        obShipDummy.SHIP_QTY = 0;
                        obShipDummy.IS_ACTIVE = true;
                        obShipDummy.cap_itms = new MC_GMT_CAP_ITEMModel().Query(0);
                        obShipDummyList.Add(obShipDummy);

                        ob.itmsOrdShipDT = obShipDummyList;
                    }

                    if (dr["HR_COMPANY_ID"] != DBNull.Value)
                    {
                        ob.HR_COMPANY_ID = Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    }
                    if (dr["PROD_UNIT_ID"] != DBNull.Value)
                    {
                        ob.PROD_UNIT_ID = Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    }
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int64 FindNoOfGmtItemsByStyle(long? MC_STYLE_H_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                Int64 ret = 0;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3017},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ret = (dr["NO_GMT_ITEM_ENTRY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_GMT_ITEM_ENTRY"]);
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<StyleOrderViewModel> SelectOrdersByStyleId(long MC_STYLE_H_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<StyleOrderViewModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new StyleOrderViewModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                    {
                        ob.ORD_CONF_DT = Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    }

                    if (dr["SHIP_DT"] != DBNull.Value)
                    {
                        ob.SHIP_DT = Convert.ToDateTime(dr["SHIP_DT"]);
                    }

                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                    {
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    }

                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);

                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);


                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TNA_FINALIZED"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_N_R"]);
                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_EXT"]);
                    ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);
                    ob.CURR_CONV_LOC = (dr["CURR_CONV_LOC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_LOC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StyleOrderViewModel SelectStyle(long ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var ob = new StyleOrderViewModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.MC_INQR_H_ID = (dr["MC_INQR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_INQR_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.REF_STYLE_NO = (dr["REF_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_STYLE_NO"]);
                    ob.COMP_STYLE_NO = (dr["COMP_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORIGIN_ID = (dr["ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORIGIN_ID"]);
                    ob.MANUF_ID = (dr["MANUF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MANUF_ID"]);

                    if (dr["NO_OF_REV"] != DBNull.Value)
                    {
                        ob.NO_OF_REV = Convert.ToInt64(dr["NO_OF_REV"]);
                    }

                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    {
                        ob.MC_BYR_ACC_ID = Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    }

                    ob.NO_OF_SET = (dr["NO_OF_SET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_SET"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                    {
                        ob.QTY_MOU_ID = Convert.ToInt64(dr["QTY_MOU_ID"]);
                    }

                    if (dr["PCS_PER_PACK"] != DBNull.Value)
                    {
                        ob.PCS_PER_PACK = Convert.ToInt64(dr["PCS_PER_PACK"]);
                    }


                    if (dr["LK_SEASON_ID"] != DBNull.Value)
                    {
                        ob.LK_SEASON_ID = Convert.ToInt64(dr["LK_SEASON_ID"]);
                    }
                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MODEL = (dr["HAS_MODEL"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MODEL"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? "N" : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);

                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);


                    ob.ITEM_LIST = (dr["ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_LIST"]);
                    ob.ALL_ITEM_LIST = (dr["ALL_ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ALL_ITEM_LIST"]);

                    ob.EXT_XML = (dr["EXT_XML"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXT_XML"]);

                    ob.MATERIAL_DESC = (dr["MATERIAL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MATERIAL_DESC"]);

                    ob.MC_SIZE_LST = (dr["MC_SIZE_LST"] == DBNull.Value) ? "N" : Convert.ToString(dr["MC_SIZE_LST"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =ob.MC_STYLE_H_ID},
                                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);


                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        MC_STYLE_D_ITEMModel ob1 = new MC_STYLE_D_ITEMModel();
                        ob1.MC_STYLE_D_ITEM_ID = (dr1["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_STYLE_D_ITEM_ID"]);
                        ob1.ITEM_NAME_EN = (dr1["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ITEM_NAME_EN"]);
                        ob1.MODEL_NO = (dr1["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MODEL_NO"]);
                        ob1.COMBO_NO = (dr1["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COMBO_NO"]);
                        ob1.SEGMENT_FLAG = (dr1["SEGMENT_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SEGMENT_FLAG"]);
                        ob.items.Add(ob1);

                    }
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object getTargetOrdDocRecv(long pageNumber, long pageSize, string pTGT_ORD_DOC_RCV_DT, string pSTYLE_NO, int? pFILTER_TYPE)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<dynamic>();
                int total_rec = 0;
                OraDatabase db = new OraDatabase();
                var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                                     new CommandParameter() {ParameterName = "pTGT_ORD_DOC_RCV_DT", Value =pTGT_ORD_DOC_RCV_DT},
                                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value =pSTYLE_NO},
                                     new CommandParameter() {ParameterName = "pFILTER_TYPE", Value =pFILTER_TYPE},
                                     new CommandParameter() {ParameterName = "pOption", Value =3016},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);

                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {


                    if (total_rec < 1)
                    {
                        total_rec = Convert.ToInt32(dr1["TOTAL_REC"]);
                    }
                    obList.Add(new
                    {
                        BYR_ACC_NAME_EN = (dr1["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["BYR_ACC_NAME_EN"]),
                        STYLE_NO = (dr1["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STYLE_NO"]),
                        ORDER_NO = (dr1["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ORDER_NO"]),
                        ORD_CONF_DT = (dr1["ORD_CONF_DT"] == DBNull.Value) ? null : Convert.ToString(dr1["ORD_CONF_DT"]),
                        SHIP_DT = (dr1["SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["SHIP_DT"]),
                        ORD_DOC_RCV_DT = (dr1["ORD_DOC_RCV_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["ORD_DOC_RCV_DT"]),
                        TGT_ORD_DOC_RCV_DT = (dr1["TGT_ORD_DOC_RCV_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["TGT_ORD_DOC_RCV_DT"]),
                        FILTER_TYPE = (dr1["FILTER_TYPE"] == DBNull.Value) ? 0 : Convert.ToInt32(dr1["FILTER_TYPE"])
                    });
                }
                return new  {
                    total = total_rec,
                    data = obList
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long NO_GMT_ITEM_ENTRY { get; set; }

        public string IS_ORD_FINALIZED { get; set; }
    }
}