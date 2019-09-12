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
    public class MC_ORDER_HModel
    {
        public Int64? MC_ORDER_H_ID { get; set; }

        //[Required(ErrorMessage = "Please select supplier")]
        public Int64? HR_COMPANY_ID { get; set; }

        //[Required(ErrorMessage = "Please select production unit")]
        public Int64? PROD_UNIT_ID { get; set; }

        //[Required(ErrorMessage="Please input order #")]
        public string ORDER_NO { get; set; }
        public string ORDER_LOT_NO { get; set; }

        [Required(ErrorMessage = "Please select buyer")]
        public Int64? MC_BUYER_ID { get; set; }

        [Required(ErrorMessage = "Please select buyer a/c #")]
        public Int64? MC_BYR_ACC_ID { get; set; }

        public Int64? MAIN_MERCH_ID { get; set; }
        public Int64? ALT_MERCH_ID { get; set; }
        public string IS_PROV { get; set; }
        public Int64? LK_ORD_TYPE_ID { get; set; }

        //[Required(ErrorMessage = "Please select order date")]
        public DateTime? ORDER_DT { get; set; }

        //[Required(ErrorMessage = "Please select document receive date")]
        public DateTime? ORD_DOC_RCV_DT { get; set; }
        public DateTime? ART_WRK_RCV_DT { get; set; }

        public DateTime? ORD_DOC_RCV_DT_P { get; set; }
        public DateTime? ART_WRK_RCV_DT_P { get; set; }


        [Required(ErrorMessage = "Please select order confirm date")]
        public DateTime? ORD_CONF_DT { get; set; }

        [Required(ErrorMessage = "Please select shipment date")]
        public DateTime? SHIP_DT { get; set; }

        public string IS_MULTI_SHIP_DT { get; set; }

        public DateTime? BOOKING_PRD { get; set; }
        public Int64? RF_CALENDAR_WK_ID { get; set; }

        //[Required(ErrorMessage = "Please select final destination")]
        public string HR_COUNTRY_ID_LST { get; set; }
        public string SEASON_REF { get; set; }
        public Int64? LEAD_TIME { get; set; }

        public Int64? QTY_MOU_ID { get; set; }
        public Decimal? UNIT_PRICE { get; set; }


        [Required(ErrorMessage = "Please select currency")]
        public Int64? RF_CURRENCY_ID { get; set; }
        public Int64? LK_ORD_STATUS_ID { get; set; }


        public Int64? MC_STYLE_H_ID { get; set; }

        [Required(ErrorMessage = "Please select style")]
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? LK_STYL_DEV_TYP_ID { get; set; }
        public Int64? MC_BLK_FAB_REQ_H_ID { get; set; }

        public Int64? REVISION_NO { get; set; }
        public DateTime? REVISION_DT { get; set; }
        public string REV_REASON { get; set; }
        public string IS_ORD_FINALIZED { get; set; }

        public Int64? PCS_PER_PACK { get; set; }
        public Int64? TOTAL_PCS_QTY { get; set; }
        
        
        public string HAS_MULTI_COL_PACK { get; set; }
        public string HAS_SET { get; set; }
        public string HAS_COMBO { get; set; }
        public string STYLE_NO { get; set; }
        public string STYLE_EXT_NO { get; set; }
        public string JOB_TRAC_NO { get; set; }

        //[Required(ErrorMessage = "Please input work style#")]
        public string WORK_STYLE_NO { get; set; }
        public string IS_TNA_FINALIZED { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_DESC { get; set; }
        public string GMT_TYPE_NAME { get; set; }
        public string PREPARED_BY { get; set; }
        public string MOU_CODE { get; set; }
        public string WEEK_NUM_DATE_RANGE { get; set; }
        public string CURR_NAME_EN { get; set; }
        public Int64? TOT_ORD_QTY { get; set; }
        public Decimal? TOT_ORD_VALUE { get; set; }

        public Int64? MC_TNA_TMPLT_H_ID { get; set; }
        public DateTime? PLAN_START_DT { get; set; }
        public string ORDER_TYPE_NAME_EN { get; set; }
        public string SZ_RANGE { get; set; }
        public Int32 TNA_REVISION_NO { get; set; }

        public string NATURE_OF_ORDER { get; set; }
        public int TOTAL_REC { get; set; }
        public string IS_FINALIZED { get; set; }
        public string OTP_ORDR_BKNG { get; set; }
        

        public string MC_ORDER_SIZE_DTL { get; set; }
        public List<MC_ORDER_SHIPModel> itmsOrdShipDT { get; set; }
        public List<MC_STYLE_H_EXTModel> buyerWiseStyleList { get; set; }
        public List<MC_BUYERModel> accWiseBuyerList { get; set; }
        //public List<LookupDataModel> orderStatusList { get; set; }

        public int STYLE_ORD_SPAN { get; set; }
        public int STYLE_ORD_SL { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public string FIB_COMP_CODE_FULL { get; set; }
        public long TOT_ORD_QTY_COL { get; set; }
        public string IS_YD { get; set; }
        public string IS_AOP { get; set; }
        public string IS_EMB { get; set; }
        public string IS_PRINT { get; set; }
        public string IS_GMT_WASH { get; set; }

        public int? LAST_REV_NO { get; set; }
        public int RF_FAB_PROD_CAT_ID { get; set; }
        public int? LAST_REV_NO_BGT { get; set; }
        public long MC_STYL_BGT_H_ID { get; set; }
        public DateTime? ACT_SHIP_DATE { get; set; }


        public DateTime? TRIM_BOOKING_DATE { get; set; }
        public DateTime? TRIM_INHOUSE_DATE { get; set; }
        public DateTime? COL_STD_RECV_DATE { get; set; }
        public DateTime? LD_SEND_DATE { get; set; }
        public DateTime? LD_APPROVE_DATE { get; set; }
        public DateTime? YRN_INH_DATE { get; set; }
        public DateTime? YRN_BK_DATE { get; set; }

        public DateTime? TRIM_BOOKING_DATE_P { get; set; }
        public DateTime? TRIM_INHOUSE_DATE_P { get; set; }
        public DateTime? COL_STD_RECV_DATE_P { get; set; }
        public DateTime? LD_SEND_DATE_P { get; set; }
        public DateTime? LD_APPROVE_DATE_P { get; set; }
        public DateTime? YRN_INH_DATE_P { get; set; }
        public DateTime? YRN_BK_DATE_P { get; set; }
        public int? TODO_CNT { get; set; }
        public DateTime? PLAN_END_DT { get; set; }
        public DateTime? TGT_ORD_DOC_RCV_DT { get; set; }

        public string STYLE_ORDER_TXT
        {
            get
            {
                return this.WORK_STYLE_NO + " " + (this.ORDER_NO == string.Empty ? "" : "/") + this.ORDER_NO;
            }
        }



        public string SaveDevOrder()
        {
            const string sp = "pkg_mc_order.mc_order_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = ob.ORDER_NO},
                     new CommandParameter() {ParameterName = "pORDER_LOT_NO", Value = ob.ORDER_LOT_NO},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMAIN_MERCH_ID", Value = ob.MAIN_MERCH_ID},
                     new CommandParameter() {ParameterName = "pALT_MERCH_ID", Value = ob.ALT_MERCH_ID},
                     new CommandParameter() {ParameterName = "pIS_PROV", Value = ob.IS_PROV},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pORDER_DT", Value = ob.ORDER_DT},
                     new CommandParameter() {ParameterName = "pORD_DOC_RCV_DT", Value = ob.ORD_DOC_RCV_DT},
                     new CommandParameter() {ParameterName = "pART_WRK_RCV_DT", Value = ob.ART_WRK_RCV_DT},
                     new CommandParameter() {ParameterName = "pORD_CONF_DT", Value = ob.ORD_CONF_DT},
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value = ob.SHIP_DT},
                     new CommandParameter() {ParameterName = "pIS_MULTI_SHIP_DT", Value = ob.IS_MULTI_SHIP_DT},
                     new CommandParameter() {ParameterName = "pBOOKING_PRD", Value = ob.BOOKING_PRD},
                     new CommandParameter() {ParameterName = "pRF_CALENDAR_WK_ID", Value = ob.RF_CALENDAR_WK_ID},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID_LST", Value = ob.HR_COUNTRY_ID_LST},
                     new CommandParameter() {ParameterName = "pSEASON_REF", Value = ob.SEASON_REF},
                     new CommandParameter() {ParameterName = "pLEAD_TIME", Value = ob.LEAD_TIME},
                     new CommandParameter() {ParameterName = "pTOT_ORD_QTY", Value = ob.TOT_ORD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},//ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = ob.LK_ORD_STATUS_ID},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_TYP_ID", Value = ob.LK_STYL_DEV_TYP_ID},

                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_EXT_NO", Value = ob.STYLE_EXT_NO},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value = ob.WORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pJOB_TRAC_NO", Value = ob.JOB_TRAC_NO},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SIZE_DTL", Value = ob.MC_ORDER_SIZE_DTL},

                     new CommandParameter() {ParameterName = "pOption", Value =1001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pJOB_TRAC_NO_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE_RTN", Direction = ParameterDirection.Output}
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


        public string BatchSaveOrder()
        {
            const string sp = "pkg_mc_order.mc_order_h_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            var vCntryList = ob.HR_COUNTRY_ID_LST.Split(',');
            
            Int64 vSpDtIndex = 0;
            Int64 vItemIndex = 0;
            Int64 vColorIndex = 0;
            Int64 vSizeIndex = 0;

            var xmlOrdShipDT = new System.Text.StringBuilder();
            xmlOrdShipDT.Append("<trans>");
            var xmItmStyle = new System.Text.StringBuilder();
            xmItmStyle.Append("<trans>");
            var xmlItmColor = new System.Text.StringBuilder();
            xmlItmColor.Append("<trans>");
            var xmlItmSize = new System.Text.StringBuilder();
            xmlItmSize.Append("<trans>");

            foreach (var itmOrdShipDT in ob.itmsOrdShipDT)
            {
                vSpDtIndex = vSpDtIndex + 1;
                
                xmlOrdShipDT.AppendLine();
                xmlOrdShipDT.Append(" <row ");
                xmlOrdShipDT.Append(" SHIP_DT_INDEX=\"" + vSpDtIndex + "\"");
                xmlOrdShipDT.Append(" MC_ORDER_SHIP_ID=\"" + itmOrdShipDT.MC_ORDER_SHIP_ID + "\"");
                xmlOrdShipDT.Append(" SHIP_DT=\"" + string.Format("{0:yyyy-MMM-dd}", itmOrdShipDT.SHIP_DT) + "\"");
                xmlOrdShipDT.Append(" SHIP_DESC=\"" + itmOrdShipDT.SHIP_DESC + "\"");
                xmlOrdShipDT.Append(" />");

                foreach (var itmStyle in itmOrdShipDT.itemsStyle)
                {
                    vItemIndex = vItemIndex + 1;

                    xmItmStyle.AppendLine();
                    xmItmStyle.Append(" <row ");
                    xmItmStyle.Append(" SHIP_DT_INDEX=\"" + vSpDtIndex + "\"");
                    xmItmStyle.Append(" ITEM_INDEX=\"" + vItemIndex + "\"");
                    xmItmStyle.Append(" MC_ORDER_STYL_ID=\"" + itmStyle.MC_ORDER_STYL_ID + "\"");
                    xmItmStyle.Append(" STYLE_EXT_NO=\"" + itmStyle.STYLE_EXT_NO + "\"");
                    xmItmStyle.Append(" MC_ORDER_SHIP_ID=\"" + itmStyle.MC_ORDER_SHIP_ID + "\"");
                    xmItmStyle.Append(" MC_STYLE_D_ITEM_ID=\"" + itmStyle.MC_STYLE_D_ITEM_ID + "\"");
                    xmItmStyle.Append(" PARENT_ITEM_ID=\"" + itmStyle.PARENT_ITEM_ID + "\"");
                    xmItmStyle.Append(" ORDER_QTY=\"" + itmStyle.ORDER_QTY + "\"");
                    xmItmStyle.Append(" QTY_MOU_ID=\"" + itmStyle.QTY_MOU_ID + "\"");
                    xmItmStyle.Append(" UNIT_PRICE=\"" + itmStyle.UNIT_PRICE + "\"");
                    xmItmStyle.Append(" RF_CURRENCY_ID=\"" + itmStyle.RF_CURRENCY_ID + "\"");
                    xmItmStyle.Append(" />");

                    if (itmStyle.childItems.Count > 0)
                    {
                        foreach (var childItm in itmStyle.childItems)
                        {
                            vItemIndex = vItemIndex + 1;

                            xmItmStyle.AppendLine();
                            xmItmStyle.Append(" <row ");
                            xmItmStyle.Append(" SHIP_DT_INDEX=\"" + vSpDtIndex + "\"");
                            xmItmStyle.Append(" ITEM_INDEX=\"" + vItemIndex + "\"");
                            xmItmStyle.Append(" MC_ORDER_STYL_ID=\"" + childItm.MC_ORDER_STYL_ID + "\"");
                            xmItmStyle.Append(" STYLE_EXT_NO=\"" + childItm.STYLE_EXT_NO + "\"");
                            xmItmStyle.Append(" MC_ORDER_SHIP_ID=\"" + childItm.MC_ORDER_SHIP_ID + "\"");
                            xmItmStyle.Append(" MC_STYLE_D_ITEM_ID=\"" + childItm.MC_STYLE_D_ITEM_ID + "\"");
                            xmItmStyle.Append(" PARENT_ITEM_ID=\"" + childItm.PARENT_ITEM_ID + "\"");
                            xmItmStyle.Append(" ORDER_QTY=\"" + childItm.ORDER_QTY + "\"");
                            xmItmStyle.Append(" QTY_MOU_ID=\"" + childItm.QTY_MOU_ID + "\"");
                            xmItmStyle.Append(" UNIT_PRICE=\"" + childItm.UNIT_PRICE + "\"");
                            xmItmStyle.Append(" RF_CURRENCY_ID=\"" + childItm.RF_CURRENCY_ID + "\"");
                            xmItmStyle.Append(" />");

                            //vColorIndex = 0;
                            foreach (var childItmColor in childItm.itemsColor)
                            {
                                vColorIndex = vColorIndex + 1;

                                xmlItmColor.AppendLine();
                                xmlItmColor.Append(" <row ");
                                xmlItmColor.Append(" SHIP_DT_INDEX=\"" + vSpDtIndex + "\"");
                                xmlItmColor.Append(" ITEM_INDEX=\"" + vItemIndex + "\"");
                                xmlItmColor.Append(" COLOR_INDEX=\"" + vColorIndex + "\"");
                                xmlItmColor.Append(" MC_ORDER_COL_ID=\"" + childItmColor.MC_ORDER_COL_ID + "\"");
                                xmlItmColor.Append(" MC_ORDER_STYL_ID=\"" + childItm.MC_ORDER_STYL_ID + "\"");
                                xmlItmColor.Append(" MC_COLOR_ID=\"" + childItmColor.MC_COLOR_ID + "\"");
                                xmlItmColor.Append(" COLOR_REF=\"" + childItmColor.COLOR_REF + "\"");
                                xmlItmColor.Append(" COMBO_NO=\"" + childItmColor.COMBO_NO + "\"");
                                xmlItmColor.Append(" PACK_NO=\"" + childItmColor.PACK_NO + "\"");

                                if (vCntryList.Length > 1)
                                    xmlItmColor.Append(" HR_COUNTRY_ID=\"" + childItmColor.HR_COUNTRY_ID + "\"");
                                else
                                    xmlItmColor.Append(" HR_COUNTRY_ID=\"" + vCntryList[0] + "\"");

                                xmlItmColor.Append(" />");

                                vSizeIndex = 0;
                                foreach (var childSiz in childItmColor.itemsSize)
                                {
                                    vSizeIndex = vSizeIndex + 1;

                                    xmlItmSize.AppendLine();
                                    xmlItmSize.Append(" <row ");
                                    xmItmStyle.Append(" SHIP_DT_INDEX=\"" + vSpDtIndex + "\"");
                                    xmlItmSize.Append(" ITEM_INDEX=\"" + vItemIndex + "\"");
                                    xmlItmSize.Append(" COLOR_INDEX=\"" + vColorIndex + "\"");
                                    xmlItmSize.Append(" SIZE_INDEX=\"" + vSizeIndex + "\"");
                                    xmlItmSize.Append(" MC_ORDER_SIZE_ID=\"" + childSiz.MC_ORDER_SIZE_ID + "\"");
                                    xmlItmSize.Append(" MC_ORDER_COL_ID=\"" + childSiz.MC_ORDER_COL_ID + "\"");
                                                                            
                                    xmlItmSize.Append(" MC_SIZE_ID=\"" + childSiz.MC_SIZE_ID + "\"");                                    
                                    xmlItmSize.Append(" UNIT_PRICE=\"" + childSiz.UNIT_PRICE + "\"");
                                    xmlItmSize.Append(" SIZE_QTY=\"" + (childSiz.SIZE_QTY == null ? 0 : childSiz.SIZE_QTY) + "\"");

                                    xmlItmSize.Append(" />");
                                }
                            }
                        }
                    }
                    else
                    {
                        //vColorIndex = 0;
                        foreach (var itmColor in itmStyle.itemsColor)
                        {
                            vColorIndex = vColorIndex + 1;

                            xmlItmColor.AppendLine();
                            xmlItmColor.Append(" <row ");
                            xmlItmColor.Append(" SHIP_DT_INDEX=\"" + vSpDtIndex + "\"");
                            xmlItmColor.Append(" ITEM_INDEX=\"" + vItemIndex + "\"");
                            xmlItmColor.Append(" COLOR_INDEX=\"" + vColorIndex + "\"");                            
                            xmlItmColor.Append(" MC_ORDER_COL_ID=\"" + itmColor.MC_ORDER_COL_ID + "\"");
                            xmlItmColor.Append(" MC_ORDER_STYL_ID=\"" + itmStyle.MC_ORDER_STYL_ID + "\"");
                            xmlItmColor.Append(" MC_COLOR_ID=\"" + itmColor.MC_COLOR_ID + "\"");
                            xmlItmColor.Append(" COLOR_REF=\"" + itmColor.COLOR_REF + "\"");
                            xmlItmColor.Append(" COMBO_NO=\"" + itmColor.COMBO_NO + "\"");
                            xmlItmColor.Append(" PACK_NO=\"" + itmColor.PACK_NO + "\"");

                            if (vCntryList.Length > 1)
                                xmlItmColor.Append(" HR_COUNTRY_ID=\"" + itmColor.HR_COUNTRY_ID + "\"");
                            else
                                xmlItmColor.Append(" HR_COUNTRY_ID=\"" + vCntryList[0] + "\"");

                            xmlItmColor.Append(" />");

                            vSizeIndex = 0;
                            foreach (var siz in itmColor.itemsSize)
                            {
                                vSizeIndex = vSizeIndex + 1;

                                xmlItmSize.AppendLine();
                                xmlItmSize.Append(" <row ");
                                xmlItmSize.Append(" SHIP_DT_INDEX=\"" + vSpDtIndex + "\"");
                                xmlItmSize.Append(" ITEM_INDEX=\"" + vItemIndex + "\"");
                                xmlItmSize.Append(" COLOR_INDEX=\"" + vColorIndex + "\"");
                                xmlItmSize.Append(" SIZE_INDEX=\"" + vSizeIndex + "\"");
                                xmlItmSize.Append(" MC_ORDER_SIZE_ID=\"" + siz.MC_ORDER_SIZE_ID + "\"");
                                xmlItmSize.Append(" MC_ORDER_COL_ID=\"" + siz.MC_ORDER_COL_ID + "\"");

                                xmlItmSize.Append(" MC_SIZE_ID=\"" + siz.MC_SIZE_ID + "\"");
                                xmlItmSize.Append(" UNIT_PRICE=\"" + siz.UNIT_PRICE + "\"");
                                xmlItmSize.Append(" SIZE_QTY=\"" + (siz.SIZE_QTY == null ? 0 : siz.SIZE_QTY) + "\"");

                                xmlItmSize.Append(" />");
                            }
                        }

                    }
                }
            }

            xmlOrdShipDT.AppendLine();
            xmlOrdShipDT.AppendLine("</trans>");
            xmItmStyle.AppendLine();
            xmItmStyle.AppendLine("</trans>");
            xmlItmColor.AppendLine();
            xmlItmColor.AppendLine("</trans>");
            xmlItmSize.AppendLine();
            xmlItmSize.AppendLine("</trans>");


            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = ob.ORDER_NO},
                     new CommandParameter() {ParameterName = "pORDER_LOT_NO", Value = ob.ORDER_LOT_NO},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMAIN_MERCH_ID", Value = ob.MAIN_MERCH_ID},
                     new CommandParameter() {ParameterName = "pALT_MERCH_ID", Value = ob.ALT_MERCH_ID},
                     new CommandParameter() {ParameterName = "pIS_PROV", Value = ob.IS_PROV},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pORDER_DT", Value = ob.ORDER_DT},
                     new CommandParameter() {ParameterName = "pORD_DOC_RCV_DT", Value = ob.ORD_DOC_RCV_DT},
                     new CommandParameter() {ParameterName = "pART_WRK_RCV_DT", Value = ob.ART_WRK_RCV_DT},
                     new CommandParameter() {ParameterName = "pORD_CONF_DT", Value = ob.ORD_CONF_DT},
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value = ob.SHIP_DT},
                     new CommandParameter() {ParameterName = "pIS_MULTI_SHIP_DT", Value = ob.IS_MULTI_SHIP_DT},
                     new CommandParameter() {ParameterName = "pBOOKING_PRD", Value = ob.BOOKING_PRD},
                     new CommandParameter() {ParameterName = "pRF_CALENDAR_WK_ID", Value = ob.RF_CALENDAR_WK_ID},
                     new CommandParameter() {ParameterName = "pHR_COUNTRY_ID_LST", Value = ob.HR_COUNTRY_ID_LST},
                     new CommandParameter() {ParameterName = "pSEASON_REF", Value = ob.SEASON_REF},
                     new CommandParameter() {ParameterName = "pLEAD_TIME", Value = ob.LEAD_TIME},
                     new CommandParameter() {ParameterName = "pTOT_ORD_QTY", Value = ob.TOT_ORD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},//ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = ob.LK_ORD_STATUS_ID},
                     //new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_EXT_NO", Value = ob.STYLE_EXT_NO},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value = ob.WORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pPCS_PER_PACK", Value = ob.PCS_PER_PACK},
                     new CommandParameter() {ParameterName = "pJOB_TRAC_NO", Value = ob.JOB_TRAC_NO},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     
                     new CommandParameter() {ParameterName = "pMC_SHIPMENT_DATE_DTL", Value = xmlOrdShipDT},
                     new CommandParameter() {ParameterName = "pMC_ORDER_STYL_DTL", Value = xmItmStyle},
                     new CommandParameter() {ParameterName = "pMC_ORDER_COLOR_DTL", Value = xmlItmColor},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SIZE_DTL", Value = xmlItmSize},

                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pJOB_TRAC_NO_RTN", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE_RTN", Direction = ParameterDirection.Output}
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

        public List<dynamic> GetGmtCapacityBookedData(Int64 pMC_BYR_ACC_ID, DateTime pSHIP_DT, Int64? pMC_ORDER_H_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pSHIP_DT", Value = pSHIP_DT},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3027},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]),
                        PLAN_PROD_PCS = (dr["PLAN_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_PROD_PCS"]),
                        PCS_BOOKED = (dr["PCS_BOOKED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_BOOKED"]),
                        CARRY_ORVERED_QTY = (dr["CARRY_ORVERED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CARRY_ORVERED_QTY"]),
                        MN_FREE_CAP_PCS4BYR_ALLOC = (dr["MN_FREE_CAP_PCS4BYR_ALLOC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MN_FREE_CAP_PCS4BYR_ALLOC"]),
                        IS_SC_ALLOWED = (dr["IS_SC_ALLOWED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC_ALLOWED"]),
                        SC_GMT_CAP_WITH_PERMISN_PCS = (dr["SC_GMT_CAP_WITH_PERMISN_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_GMT_CAP_WITH_PERMISN_PCS"])                        
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<GMT_WEEKLY_CAP_BKING_RPTModel> GetGmtCapBkData4OTPModal(Int64 pMC_BYR_ACC_ID, DateTime pSHIP_DT, Int64? pMC_ORDER_H_ID, int pOption = 3028)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pSHIP_DT", Value = pSHIP_DT},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = pOption},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                
                var dataList = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_WEEKLY_CAP_BKING_RPTModel ob = new GMT_WEEKLY_CAP_BKING_RPTModel();

                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);
                    ob.GMT_PRODUCT_TYP_NAME = (dr["GMT_PRODUCT_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_PRODUCT_TYP_NAME"]);

                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.RF_CAL_MONTH_ID = (dr["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CAL_MONTH_ID"]);
                    ob.MONTH_NAME_EN = (dr["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTH_NAME_EN"]);
                    
                    ob.WK_NAME_EN = (dr["WK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WK_NAME_EN"]);
                    ob.MN_WK_NO = (dr["MN_WK_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MN_WK_NO"]);
                    ob.PLAN_PROD_PCS = (dr["PLAN_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PLAN_PROD_PCS"]);
                    ob.PCS_BOOKED = (dr["PCS_BOOKED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_BOOKED"]);
                    ob.CARRY_ORVERED_PCS = (dr["CARRY_ORVERED_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CARRY_ORVERED_PCS"]);
                    ob.IS_SC_ALLOWED = (dr["IS_SC_ALLOWED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC_ALLOWED"]);
                    ob.BK_WITH_CO_PCS = ob.PCS_BOOKED + ob.CARRY_ORVERED_PCS;
                    ob.BAL_PCS = ob.PLAN_PROD_PCS - ob.BK_WITH_CO_PCS;

                    dataList.Add(ob);
                }

                var weekHdrList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                var obWeekHdrList = new List<WEEK_LIST_CAP_BKING_RPTModel>();
                foreach (GMT_WEEKLY_CAP_BKING_RPTModel obWeek in weekHdrList)
                {
                    WEEK_LIST_CAP_BKING_RPTModel ob2 = new WEEK_LIST_CAP_BKING_RPTModel();
                    ob2.GMT_PRODUCT_TYP_ID = obWeek.GMT_PRODUCT_TYP_ID;
                    ob2.RF_FISCAL_YEAR_ID = obWeek.RF_FISCAL_YEAR_ID;
                    ob2.RF_CAL_MONTH_ID = obWeek.RF_CAL_MONTH_ID;
                    ob2.WK_NAME_EN = obWeek.WK_NAME_EN;

                    ob2.PLAN_PROD_PCS = dataList.Where(a => a.WK_NAME_EN == ob2.WK_NAME_EN).ToList().Sum(a=> a.PLAN_PROD_PCS);
                    ob2.PCS_BOOKED = dataList.Where(a => a.WK_NAME_EN == ob2.WK_NAME_EN).ToList().Sum(a => a.PCS_BOOKED);
                    ob2.CARRY_ORVERED_QTY = dataList.Where(a => a.WK_NAME_EN == ob2.WK_NAME_EN).ToList().Sum(a => a.CARRY_ORVERED_PCS);
                    ob2.BK_WITH_CO_PCS = dataList.Where(a => a.WK_NAME_EN == ob2.WK_NAME_EN).ToList().Sum(a => a.BK_WITH_CO_PCS);
                    ob2.BAL_PCS = dataList.Where(a => a.WK_NAME_EN == ob2.WK_NAME_EN).ToList().Sum(a => a.BAL_PCS);

                    obWeekHdrList.Add(ob2);
                }


                var monthHdrList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.MONTH_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                var obMonthHdrList = new List<MONTH_LIST_CAP_BKING_RPTModel>();
                foreach (GMT_WEEKLY_CAP_BKING_RPTModel obMonth in monthHdrList)
                {
                    MONTH_LIST_CAP_BKING_RPTModel ob2 = new MONTH_LIST_CAP_BKING_RPTModel();
                    ob2.RF_FISCAL_YEAR_ID = obMonth.RF_FISCAL_YEAR_ID;
                    ob2.RF_CAL_MONTH_ID = obMonth.RF_CAL_MONTH_ID;
                    ob2.MONTH_NAME_EN = obMonth.MONTH_NAME_EN;

                    ob2.MONTH_COL_SPAN = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().Where(a => a.RF_FISCAL_YEAR_ID == ob2.RF_FISCAL_YEAR_ID && a.RF_CAL_MONTH_ID==ob2.RF_CAL_MONTH_ID).ToList().Count();

                    obMonthHdrList.Add(ob2);
                }

                
                var obProdTypList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.GMT_PRODUCT_TYP_ID)).ToList();
                
                foreach(GMT_WEEKLY_CAP_BKING_RPTModel obProd in obProdTypList)
                {
                    GMT_WEEKLY_CAP_BKING_RPTModel ob = new GMT_WEEKLY_CAP_BKING_RPTModel();
                    ob.GMT_PRODUCT_TYP_ID = obProd.GMT_PRODUCT_TYP_ID;
                    ob.GMT_PRODUCT_TYP_NAME = obProd.GMT_PRODUCT_TYP_NAME;

                    ob.CAP_MONTH_HDR_LIST = (List<MONTH_LIST_CAP_BKING_RPTModel>)obMonthHdrList;
                    ob.CAP_WEEK_HDR_LIST = (List<WEEK_LIST_CAP_BKING_RPTModel>)obWeekHdrList;

                    //var obWkList = new List<WEEK_LIST_CAP_BKING_RPTModel>();   
                    var obWeekList = dataList.Where(a => a.GMT_PRODUCT_TYP_ID == ob.GMT_PRODUCT_TYP_ID).ToList().Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                    //var obWeekList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().Where(a => a.GMT_PRODUCT_TYP_ID == ob.GMT_PRODUCT_TYP_ID).ToList();//.OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                    ob.CAP_WEEK_DATA_LIST = (List<GMT_WEEKLY_CAP_BKING_RPTModel>)obWeekList;
                    
                    obList.Add(ob);
                }
                
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CAP_BKING4OTP_VM_Model GetData4SendOTP(Int64 pMC_ORDER_H_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                CAP_BKING4OTP_VM_Model ob = new CAP_BKING4OTP_VM_Model();
                var obProdTypWiseDtlList = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();
                var obCapBkingDtlList = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();
                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {               
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3029},
                     new CommandParameter() {ParameterName = "pResult", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pResult1", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pResult2", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MAIL_ADD_LST = (dr["MAIL_ADD_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIL_ADD_LST"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
                    ob.REDIRECT_URL = "/Merchandising/Mrc4AutoMail/FireMailSendOTP4OrderConfirm?pMC_ORDER_H_ID=" + pMC_ORDER_H_ID;

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.OTP_ORDR_BKNG = (dr["OTP_ORDR_BKNG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTP_ORDR_BKNG"]);
                                        
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.ORD_QTY_IN_PCS = (dr["ORD_QTY_IN_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORD_QTY_IN_PCS"]);

                    foreach (DataRow dr1 in ds.Tables[1].Rows)
                    {
                        GMT_WEEKLY_CAP_BKING_RPTModel ob1 = new GMT_WEEKLY_CAP_BKING_RPTModel();
                        ob1.GMT_PRODUCT_TYP_ID = (dr1["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["GMT_PRODUCT_TYP_ID"]);
                        ob1.GMT_PRODUCT_TYP_NAME = (dr1["GMT_PRODUCT_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["GMT_PRODUCT_TYP_NAME"]);
                        ob1.PCS_BOOKED = (dr1["PCS_BOOKED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PCS_BOOKED"]);

                        obProdTypWiseDtlList.Add(ob1);
                    }

                    
                    //========== Start Capacity Booking Detail ============
                    var dataList = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();
                    foreach (DataRow drCap1 in ds.Tables[2].Rows)
                    {
                        GMT_WEEKLY_CAP_BKING_RPTModel obCap1 = new GMT_WEEKLY_CAP_BKING_RPTModel();

                        obCap1.GMT_PRODUCT_TYP_ID = (drCap1["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["GMT_PRODUCT_TYP_ID"]);
                        obCap1.GMT_PRODUCT_TYP_NAME = (drCap1["GMT_PRODUCT_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap1["GMT_PRODUCT_TYP_NAME"]);

                        obCap1.RF_FISCAL_YEAR_ID = (drCap1["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["RF_FISCAL_YEAR_ID"]);
                        obCap1.RF_CAL_MONTH_ID = (drCap1["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["RF_CAL_MONTH_ID"]);
                        obCap1.MONTH_NAME_EN = (drCap1["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap1["MONTH_NAME_EN"]);

                        obCap1.WK_NAME_EN = (drCap1["WK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap1["WK_NAME_EN"]);
                        obCap1.MN_WK_NO = (drCap1["MN_WK_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(drCap1["MN_WK_NO"]);
                        obCap1.PLAN_PROD_PCS = (drCap1["PLAN_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["PLAN_PROD_PCS"]);
                        obCap1.PCS_BOOKED = (drCap1["PCS_BOOKED"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["PCS_BOOKED"]);
                        obCap1.CARRY_ORVERED_PCS = (drCap1["CARRY_ORVERED_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["CARRY_ORVERED_PCS"]);
                        obCap1.BK_WITH_CO_PCS = obCap1.PCS_BOOKED + obCap1.CARRY_ORVERED_PCS;
                        obCap1.BAL_PCS = obCap1.PLAN_PROD_PCS - obCap1.BK_WITH_CO_PCS;

                        obCap1.OVER_BOOKED_PCT = Math.Round(((decimal)obCap1.BK_WITH_CO_PCS / obCap1.PLAN_PROD_PCS) * 100, 2);
                        obCap1.NEW_ORD_PCS = obProdTypWiseDtlList.Where(a => a.GMT_PRODUCT_TYP_ID == obCap1.GMT_PRODUCT_TYP_ID).Sum(a => a.PCS_BOOKED);
                        obCap1.NEW_OVER_BOOKED_PCT = Math.Round(((decimal)(obCap1.BK_WITH_CO_PCS + obCap1.NEW_ORD_PCS) / obCap1.PLAN_PROD_PCS) * 100, 2);
                            
                        dataList.Add(obCap1);
                    }

                    var weekHdrList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                    var obWeekHdrList = new List<WEEK_LIST_CAP_BKING_RPTModel>();
                    foreach (GMT_WEEKLY_CAP_BKING_RPTModel obWeek in weekHdrList)
                    {
                        WEEK_LIST_CAP_BKING_RPTModel obCap2 = new WEEK_LIST_CAP_BKING_RPTModel();
                        obCap2.GMT_PRODUCT_TYP_ID = obWeek.GMT_PRODUCT_TYP_ID;
                        obCap2.RF_FISCAL_YEAR_ID = obWeek.RF_FISCAL_YEAR_ID;
                        obCap2.RF_CAL_MONTH_ID = obWeek.RF_CAL_MONTH_ID;
                        obCap2.WK_NAME_EN = obWeek.WK_NAME_EN;

                        obCap2.PLAN_PROD_PCS = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.PLAN_PROD_PCS);
                        obCap2.PCS_BOOKED = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.PCS_BOOKED);
                        obCap2.CARRY_ORVERED_QTY = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.CARRY_ORVERED_PCS);
                        obCap2.BK_WITH_CO_PCS = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.BK_WITH_CO_PCS);
                        obCap2.BAL_PCS = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.BAL_PCS);
                        obCap2.OVER_BOOKED_PCT = Math.Round(((decimal)obCap2.BK_WITH_CO_PCS / obCap2.PLAN_PROD_PCS) * 100, 2);
                        obCap2.NEW_ORD_PCS = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.NEW_ORD_PCS);
                        obCap2.NEW_OVER_BOOKED_PCT = Math.Round(((decimal)(obCap2.BK_WITH_CO_PCS + obCap2.NEW_ORD_PCS) / obCap2.PLAN_PROD_PCS) * 100, 2);
                        
                        obWeekHdrList.Add(obCap2);
                    }


                    var monthHdrList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.MONTH_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                    var obMonthHdrList = new List<MONTH_LIST_CAP_BKING_RPTModel>();
                    foreach (GMT_WEEKLY_CAP_BKING_RPTModel obMonth in monthHdrList)
                    {
                        MONTH_LIST_CAP_BKING_RPTModel obCap2 = new MONTH_LIST_CAP_BKING_RPTModel();
                        obCap2.RF_FISCAL_YEAR_ID = obMonth.RF_FISCAL_YEAR_ID;
                        obCap2.RF_CAL_MONTH_ID = obMonth.RF_CAL_MONTH_ID;
                        obCap2.MONTH_NAME_EN = obMonth.MONTH_NAME_EN;

                        obCap2.MONTH_COL_SPAN = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().Where(a => a.RF_FISCAL_YEAR_ID == obCap2.RF_FISCAL_YEAR_ID && a.RF_CAL_MONTH_ID == obCap2.RF_CAL_MONTH_ID).ToList().Count();

                        obMonthHdrList.Add(obCap2);
                    }


                    var obProdTypList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.GMT_PRODUCT_TYP_ID)).ToList();

                    foreach (GMT_WEEKLY_CAP_BKING_RPTModel obProd in obProdTypList)
                    {
                        GMT_WEEKLY_CAP_BKING_RPTModel obCap = new GMT_WEEKLY_CAP_BKING_RPTModel();
                        obCap.GMT_PRODUCT_TYP_ID = obProd.GMT_PRODUCT_TYP_ID;
                        obCap.GMT_PRODUCT_TYP_NAME = obProd.GMT_PRODUCT_TYP_NAME;

                        obCap.CAP_MONTH_HDR_LIST = (List<MONTH_LIST_CAP_BKING_RPTModel>)obMonthHdrList;
                        obCap.CAP_WEEK_HDR_LIST = (List<WEEK_LIST_CAP_BKING_RPTModel>)obWeekHdrList;

                        //var obWkList = new List<WEEK_LIST_CAP_BKING_RPTModel>();   
                        var obWeekList = dataList.Where(a => a.GMT_PRODUCT_TYP_ID == obCap.GMT_PRODUCT_TYP_ID).ToList().Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                        //var obWeekList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().Where(a => a.GMT_PRODUCT_TYP_ID == ob.GMT_PRODUCT_TYP_ID).ToList();//.OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                        obCap.CAP_WEEK_DATA_LIST = (List<GMT_WEEKLY_CAP_BKING_RPTModel>)obWeekList;

                        obCapBkingDtlList.Add(obCap);
                    }
                    //========== End Capacity Booking Detail ==============

                    ob.PROD_TYP_WISE_DTL = obProdTypWiseDtlList;
                    ob.CAP_BKING_DTL = obCapBkingDtlList;                  
                }
                
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CAP_BKING4OTP_VM_Model GetCapBkData4MailAutoSend()
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                CAP_BKING4OTP_VM_Model ob = new CAP_BKING4OTP_VM_Model();
                var obCapBkingMonthlyDtlList = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();
                var obCapBkingDtlList = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                                    
                     new CommandParameter() {ParameterName = "pOption", Value = 3030},
                     new CommandParameter() {ParameterName = "pResult", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pResult1", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pResult2", Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MAIL_ADD_LST = (dr["MAIL_ADD_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIL_ADD_LST"]);
                    
                    //========== Start Weekly Capacity Booking Detail ============
                    //var dataList = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();
                    //foreach (DataRow drCap1 in ds.Tables[1].Rows)
                    //{
                    //    GMT_WEEKLY_CAP_BKING_RPTModel obCap1 = new GMT_WEEKLY_CAP_BKING_RPTModel();

                    //    obCap1.GMT_PRODUCT_TYP_ID = (drCap1["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["GMT_PRODUCT_TYP_ID"]);
                    //    obCap1.GMT_PRODUCT_TYP_NAME = (drCap1["GMT_PRODUCT_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap1["GMT_PRODUCT_TYP_NAME"]);

                    //    obCap1.RF_FISCAL_YEAR_ID = (drCap1["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["RF_FISCAL_YEAR_ID"]);
                    //    obCap1.RF_CAL_MONTH_ID = (drCap1["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["RF_CAL_MONTH_ID"]);
                    //    obCap1.MONTH_NAME_EN = (drCap1["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap1["MONTH_NAME_EN"]);

                    //    obCap1.WK_NAME_EN = (drCap1["WK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap1["WK_NAME_EN"]);
                    //    obCap1.MN_WK_NO = (drCap1["MN_WK_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(drCap1["MN_WK_NO"]);
                    //    obCap1.PLAN_PROD_PCS = (drCap1["PLAN_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["PLAN_PROD_PCS"]);
                    //    obCap1.PCS_BOOKED = (drCap1["PCS_BOOKED"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["PCS_BOOKED"]);
                    //    obCap1.CARRY_ORVERED_PCS = (drCap1["CARRY_ORVERED_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap1["CARRY_ORVERED_PCS"]);
                    //    obCap1.BK_WITH_CO_PCS = obCap1.PCS_BOOKED + obCap1.CARRY_ORVERED_PCS;
                    //    obCap1.BAL_PCS = obCap1.PLAN_PROD_PCS - obCap1.BK_WITH_CO_PCS;

                    //    obCap1.OVER_BOOKED_PCT = Math.Round(((decimal)obCap1.BK_WITH_CO_PCS / obCap1.PLAN_PROD_PCS) * 100, 2);
                    //    obCap1.NEW_ORD_PCS = 0;
                    //    obCap1.NEW_OVER_BOOKED_PCT = Math.Round(((decimal)(obCap1.BK_WITH_CO_PCS + obCap1.NEW_ORD_PCS) / obCap1.PLAN_PROD_PCS) * 100, 2);

                    //    dataList.Add(obCap1);
                    //}

                    //var weekHdrList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                    //var obWeekHdrList = new List<WEEK_LIST_CAP_BKING_RPTModel>();
                    //foreach (GMT_WEEKLY_CAP_BKING_RPTModel obWeek in weekHdrList)
                    //{
                    //    WEEK_LIST_CAP_BKING_RPTModel obCap2 = new WEEK_LIST_CAP_BKING_RPTModel();
                    //    obCap2.GMT_PRODUCT_TYP_ID = obWeek.GMT_PRODUCT_TYP_ID;
                    //    obCap2.RF_FISCAL_YEAR_ID = obWeek.RF_FISCAL_YEAR_ID;
                    //    obCap2.RF_CAL_MONTH_ID = obWeek.RF_CAL_MONTH_ID;
                    //    obCap2.WK_NAME_EN = obWeek.WK_NAME_EN;

                    //    obCap2.PLAN_PROD_PCS = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.PLAN_PROD_PCS);
                    //    obCap2.PCS_BOOKED = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.PCS_BOOKED);
                    //    obCap2.CARRY_ORVERED_QTY = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.CARRY_ORVERED_PCS);
                    //    obCap2.BK_WITH_CO_PCS = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.BK_WITH_CO_PCS);
                    //    obCap2.BAL_PCS = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.BAL_PCS);
                    //    obCap2.OVER_BOOKED_PCT = Math.Round(((decimal)obCap2.BK_WITH_CO_PCS / obCap2.PLAN_PROD_PCS) * 100, 2);
                    //    obCap2.NEW_ORD_PCS = dataList.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.NEW_ORD_PCS);
                    //    obCap2.NEW_OVER_BOOKED_PCT = Math.Round(((decimal)(obCap2.BK_WITH_CO_PCS + obCap2.NEW_ORD_PCS) / obCap2.PLAN_PROD_PCS) * 100, 2);

                    //    obWeekHdrList.Add(obCap2);
                    //}

                    //var monthHdrList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.MONTH_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                    //var obMonthHdrList = new List<MONTH_LIST_CAP_BKING_RPTModel>();
                    //foreach (GMT_WEEKLY_CAP_BKING_RPTModel obMonth in monthHdrList)
                    //{
                    //    MONTH_LIST_CAP_BKING_RPTModel obCap2 = new MONTH_LIST_CAP_BKING_RPTModel();
                    //    obCap2.RF_FISCAL_YEAR_ID = obMonth.RF_FISCAL_YEAR_ID;
                    //    obCap2.RF_CAL_MONTH_ID = obMonth.RF_CAL_MONTH_ID;
                    //    obCap2.MONTH_NAME_EN = obMonth.MONTH_NAME_EN;

                    //    obCap2.MONTH_COL_SPAN = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().Where(a => a.RF_FISCAL_YEAR_ID == obCap2.RF_FISCAL_YEAR_ID && a.RF_CAL_MONTH_ID == obCap2.RF_CAL_MONTH_ID).ToList().Count();

                    //    obMonthHdrList.Add(obCap2);
                    //}

                    //var obProdTypList = dataList.Distinct(new GmtWklyCapBkingRptComparer(a => a.GMT_PRODUCT_TYP_ID)).ToList();

                    //foreach (GMT_WEEKLY_CAP_BKING_RPTModel obProd in obProdTypList)
                    //{
                    //    GMT_WEEKLY_CAP_BKING_RPTModel obCap = new GMT_WEEKLY_CAP_BKING_RPTModel();
                    //    obCap.GMT_PRODUCT_TYP_ID = obProd.GMT_PRODUCT_TYP_ID;
                    //    obCap.GMT_PRODUCT_TYP_NAME = obProd.GMT_PRODUCT_TYP_NAME;

                    //    obCap.CAP_MONTH_HDR_LIST = (List<MONTH_LIST_CAP_BKING_RPTModel>)obMonthHdrList;
                    //    obCap.CAP_WEEK_HDR_LIST = (List<WEEK_LIST_CAP_BKING_RPTModel>)obWeekHdrList;
                                                
                    //    var obWeekList = dataList.Where(a => a.GMT_PRODUCT_TYP_ID == obCap.GMT_PRODUCT_TYP_ID).ToList().Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();                        
                    //    obCap.CAP_WEEK_DATA_LIST = (List<GMT_WEEKLY_CAP_BKING_RPTModel>)obWeekList;

                    //    obCapBkingDtlList.Add(obCap);
                    //}
                    //========== End Weekly Capacity Booking Detail ============


                    //========== Start Monthly Capacity Booking Detail ============
                    //var dataList1 = new List<GMT_WEEKLY_CAP_BKING_RPTModel>();
                    //foreach (DataRow drCap2 in ds.Tables[2].Rows)
                    //{
                    //    GMT_WEEKLY_CAP_BKING_RPTModel obCap2 = new GMT_WEEKLY_CAP_BKING_RPTModel();

                    //    obCap2.GMT_PRODUCT_TYP_ID = (drCap2["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap2["GMT_PRODUCT_TYP_ID"]);
                    //    obCap2.GMT_PRODUCT_TYP_NAME = (drCap2["GMT_PRODUCT_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap2["GMT_PRODUCT_TYP_NAME"]);

                    //    obCap2.RF_FISCAL_YEAR_ID = (drCap2["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap2["RF_FISCAL_YEAR_ID"]);
                    //    obCap2.RF_CAL_MONTH_ID = (drCap2["RF_CAL_MONTH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap2["RF_CAL_MONTH_ID"]);
                    //    obCap2.MONTH_NAME_EN = (drCap2["MONTH_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap2["MONTH_NAME_EN"]);

                    //    obCap2.WK_NAME_EN = (drCap2["WK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drCap2["WK_NAME_EN"]);
                    //    obCap2.MN_WK_NO = (drCap2["MN_WK_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(drCap2["MN_WK_NO"]);
                    //    obCap2.PLAN_PROD_PCS = (drCap2["PLAN_PROD_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap2["PLAN_PROD_PCS"]);
                    //    obCap2.PCS_BOOKED = (drCap2["PCS_BOOKED"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap2["PCS_BOOKED"]);
                    //    obCap2.CARRY_ORVERED_PCS = (drCap2["CARRY_ORVERED_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(drCap2["CARRY_ORVERED_PCS"]);
                    //    obCap2.BK_WITH_CO_PCS = obCap2.PCS_BOOKED + obCap2.CARRY_ORVERED_PCS;
                    //    obCap2.BAL_PCS = obCap2.PLAN_PROD_PCS - obCap2.BK_WITH_CO_PCS;

                    //    obCap2.OVER_BOOKED_PCT = Math.Round(((decimal)obCap2.BK_WITH_CO_PCS / obCap2.PLAN_PROD_PCS) * 100, 2);
                    //    obCap2.NEW_ORD_PCS = 0;
                    //    obCap2.NEW_OVER_BOOKED_PCT = Math.Round(((decimal)(obCap2.BK_WITH_CO_PCS + obCap2.NEW_ORD_PCS) / obCap2.PLAN_PROD_PCS) * 100, 2);

                    //    dataList1.Add(obCap2);
                    //}

                    //var weekHdrList1 = dataList1.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                    //var obWeekHdrList1 = new List<WEEK_LIST_CAP_BKING_RPTModel>();
                    //foreach (GMT_WEEKLY_CAP_BKING_RPTModel obWeek in weekHdrList1)
                    //{
                    //    WEEK_LIST_CAP_BKING_RPTModel obCap2 = new WEEK_LIST_CAP_BKING_RPTModel();
                    //    obCap2.GMT_PRODUCT_TYP_ID = obWeek.GMT_PRODUCT_TYP_ID;
                    //    obCap2.RF_FISCAL_YEAR_ID = obWeek.RF_FISCAL_YEAR_ID;
                    //    obCap2.RF_CAL_MONTH_ID = obWeek.RF_CAL_MONTH_ID;
                    //    obCap2.WK_NAME_EN = obWeek.WK_NAME_EN;

                    //    obCap2.PLAN_PROD_PCS = dataList1.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.PLAN_PROD_PCS);
                    //    obCap2.PCS_BOOKED = dataList1.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.PCS_BOOKED);
                    //    obCap2.CARRY_ORVERED_QTY = dataList1.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.CARRY_ORVERED_PCS);
                    //    obCap2.BK_WITH_CO_PCS = dataList1.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.BK_WITH_CO_PCS);
                    //    obCap2.BAL_PCS = dataList1.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.BAL_PCS);
                    //    obCap2.OVER_BOOKED_PCT = Math.Round(((decimal)obCap2.BK_WITH_CO_PCS / obCap2.PLAN_PROD_PCS) * 100, 2);
                    //    obCap2.NEW_ORD_PCS = dataList1.Where(a => a.WK_NAME_EN == obCap2.WK_NAME_EN).ToList().Sum(a => a.NEW_ORD_PCS);
                    //    obCap2.NEW_OVER_BOOKED_PCT = Math.Round(((decimal)(obCap2.BK_WITH_CO_PCS + obCap2.NEW_ORD_PCS) / obCap2.PLAN_PROD_PCS) * 100, 2);

                    //    obWeekHdrList1.Add(obCap2);
                    //}

                    //var monthHdrList1 = dataList1.Distinct(new GmtWklyCapBkingRptComparer(a => a.MONTH_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();
                    //var obMonthHdrList1 = new List<MONTH_LIST_CAP_BKING_RPTModel>();
                    //foreach (GMT_WEEKLY_CAP_BKING_RPTModel obMonth in monthHdrList1)
                    //{
                    //    MONTH_LIST_CAP_BKING_RPTModel obCap2 = new MONTH_LIST_CAP_BKING_RPTModel();
                    //    obCap2.RF_FISCAL_YEAR_ID = obMonth.RF_FISCAL_YEAR_ID;
                    //    obCap2.RF_CAL_MONTH_ID = obMonth.RF_CAL_MONTH_ID;
                    //    obCap2.MONTH_NAME_EN = obMonth.MONTH_NAME_EN;

                    //    obCap2.MONTH_COL_SPAN = dataList1.Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().Where(a => a.RF_FISCAL_YEAR_ID == obCap2.RF_FISCAL_YEAR_ID && a.RF_CAL_MONTH_ID == obCap2.RF_CAL_MONTH_ID).ToList().Count();

                    //    obMonthHdrList1.Add(obCap2);
                    //}


                    //var obProdTypList1 = dataList1.Distinct(new GmtWklyCapBkingRptComparer(a => a.GMT_PRODUCT_TYP_ID)).ToList();

                    //foreach (GMT_WEEKLY_CAP_BKING_RPTModel obProd in obProdTypList1)
                    //{
                    //    GMT_WEEKLY_CAP_BKING_RPTModel obCap = new GMT_WEEKLY_CAP_BKING_RPTModel();
                    //    obCap.GMT_PRODUCT_TYP_ID = obProd.GMT_PRODUCT_TYP_ID;
                    //    obCap.GMT_PRODUCT_TYP_NAME = obProd.GMT_PRODUCT_TYP_NAME;

                    //    obCap.CAP_MONTH_HDR_LIST = (List<MONTH_LIST_CAP_BKING_RPTModel>)obMonthHdrList1;
                    //    obCap.CAP_WEEK_HDR_LIST = (List<WEEK_LIST_CAP_BKING_RPTModel>)obWeekHdrList1;

                    //    var obWeekList = dataList1.Where(a => a.GMT_PRODUCT_TYP_ID == obCap.GMT_PRODUCT_TYP_ID).ToList().Distinct(new GmtWklyCapBkingRptComparer(a => a.WK_NAME_EN)).ToList().OrderBy(o => o.RF_FISCAL_YEAR_ID).ThenBy(o => o.RF_CAL_MONTH_ID).ToList();                        
                    //    obCap.CAP_WEEK_DATA_LIST = (List<GMT_WEEKLY_CAP_BKING_RPTModel>)obWeekList;

                    //    obCapBkingMonthlyDtlList.Add(obCap);
                    //}
                    //========== End Monthly Capacity Booking Detail ============

                    //ob.CAP_BKING_MONTHLY_DTL = obCapBkingMonthlyDtlList;
                    //ob.CAP_BKING_DTL = obCapBkingDtlList;
                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_ORDER_ENTRY_NOTIFICATION_VM_Model GetOrdCnfrmList4MailAutoSend()
        {
            MC_ORDER_ENTRY_NOTIFICATION_VM_Model obRtn = new MC_ORDER_ENTRY_NOTIFICATION_VM_Model();

            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                var obList1 = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                    new CommandParameter() {ParameterName = "pOption", Value = 3031},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_HModel ob = new MC_ORDER_HModel();

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PROV"]);
                    ob.STYLE_ORD_SPAN = (dr["STYLE_ORD_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["STYLE_ORD_SPAN"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.GMT_TYPE_NAME = (dr["GMT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_TYPE_NAME"]);
                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);
                    
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_VALUE"]);

                    ob.PREPARED_BY = (dr["PREPARED_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PREPARED_BY"]);
                    
                    obList.Add(ob);
                }

                foreach (DataRow dr1 in ds.Tables[1].Rows)
                {
                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    
                    ob.GMT_TYPE_NAME = (dr1["GMT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["GMT_TYPE_NAME"]);
                    ob.TOTAL_REC = (dr1["NO_OF_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt32(dr1["NO_OF_ORDER"]);                    
                    ob.TOT_ORD_QTY = (dr1["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TOT_ORD_QTY"]);
                    ob.TOT_ORD_VALUE = (dr1["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["TOT_ORD_VALUE"]);

                    obList1.Add(ob);
                }

                obRtn.ORDER_ENTRY_LIST = obList;
                obRtn.ORDER_ENTRY_SUMMERY_LIST=obList1;
                return obRtn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ORDER_HModel> OrderDataList(Int64? pMC_BUYER_ID, Int64? pMC_ORDER_H_ID, Int64? pMC_STYLE_H_ID, Int64? pMC_BUYER_OFF_ID, Int64? pMC_BYR_ACC_ID, String pIS_PROV)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pIS_PROV", Value = pIS_PROV},
                    new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = pMC_BUYER_OFF_ID},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    if (dr["HR_COMPANY_ID"] != DBNull.Value)
                        ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);

                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.LK_STYL_DEV_TYP_ID = (dr["LK_STYL_DEV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_TYP_ID"]);

                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_EXT_NO = (dr["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_EXT_NO"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TNA_FINALIZED"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    if (dr["PROD_UNIT_ID"] != DBNull.Value)
                        ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_LOT_NO = (dr["ORDER_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LOT_NO"]);
                    if (dr["MC_BUYER_ID"] != DBNull.Value)
                        ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    if (dr["MAIN_MERCH_ID"] != DBNull.Value)
                        ob.MAIN_MERCH_ID = (dr["MAIN_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAIN_MERCH_ID"]);

                    if (dr["ALT_MERCH_ID"] != DBNull.Value)
                        ob.ALT_MERCH_ID = (dr["ALT_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_MERCH_ID"]);

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROV"]);
                    if (dr["LK_ORD_TYPE_ID"] != DBNull.Value)
                        ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                    if (dr["ORDER_DT"] != DBNull.Value)
                        ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);

                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = (dr["ART_WRK_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["TGT_ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.TGT_ORD_DOC_RCV_DT = Convert.ToDateTime(dr["TGT_ORD_DOC_RCV_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = Convert.ToDateTime(dr["SHIP_DT"]);

                    if (dr["BOOKING_PRD"] != DBNull.Value)
                        ob.BOOKING_PRD = (dr["BOOKING_PRD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_PRD"]);

                    ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);

                    if (dr["BOOKING_PRD"] != DBNull.Value)
                        ob.BOOKING_PRD = (dr["BOOKING_PRD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_PRD"]);

                    if (dr["RF_CALENDAR_WK_ID"] != DBNull.Value)
                        ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);


                    ob.HR_COUNTRY_ID_LST = (dr["HR_COUNTRY_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_COUNTRY_ID_LST"]);

                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_VALUE"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);

                    if (dr["RF_CURRENCY_ID"] != DBNull.Value)
                        ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);

                    if (dr["LK_ORD_STATUS_ID"] != DBNull.Value)
                        ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);

                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);

                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    ob.WEEK_NUM_DATE_RANGE = (dr["WEEK_NUM_DATE_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEEK_NUM_DATE_RANGE"]);

                    var obItemsOrdShipDt = new MC_ORDER_HModel().GetOrdShipList(ob.MC_ORDER_H_ID);
                    ob.itmsOrdShipDT = (List<MC_ORDER_SHIPModel>)obItemsOrdShipDt;                  

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ORDER_HModel> OrderHdrDataList(Int64? pMC_BUYER_ID, Int64? pMC_ORDER_H_ID, Int64? pMC_STYLE_H_ID, Int64? pMC_BUYER_OFF_ID, Int64? pMC_BYR_ACC_ID, Int64? pLK_ORD_STATUS_ID, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pEX_ORD_STATUS_ID = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = pMC_BUYER_OFF_ID},
                    new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = pLK_ORD_STATUS_ID},
                    new CommandParameter() {ParameterName = "pEX_ORD_STATUS_ID", Value = pEX_ORD_STATUS_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},                    

                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_EXT_NO = (dr["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_EXT_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TNA_FINALIZED"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_LOT_NO = (dr["ORDER_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LOT_NO"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    if (dr["MAIN_MERCH_ID"] != DBNull.Value)
                        ob.MAIN_MERCH_ID = (dr["MAIN_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAIN_MERCH_ID"]);

                    if (dr["ALT_MERCH_ID"] != DBNull.Value)
                        ob.ALT_MERCH_ID = (dr["ALT_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_MERCH_ID"]);

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROV"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                    if (dr["ORDER_DT"] != DBNull.Value)
                        ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);

                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = (dr["ART_WRK_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    if (dr["BOOKING_PRD"] != DBNull.Value)
                        ob.BOOKING_PRD = (dr["BOOKING_PRD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_PRD"]);

                    if (dr["RF_CALENDAR_WK_ID"] != DBNull.Value)
                        ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);

                    ob.HR_COUNTRY_ID_LST = (dr["HR_COUNTRY_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_COUNTRY_ID_LST"]);

                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_VALUE"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);

                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);

                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    ob.WEEK_NUM_DATE_RANGE = (dr["WEEK_NUM_DATE_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEEK_NUM_DATE_RANGE"]);
                    ob.TNA_REVISION_NO = (dr["TNA_REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TNA_REVISION_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ORDER_HModel> OrderHdrDataListWithTnaData(String pMC_STYLE_H_EXT_LST, string pHAS_EXT, Int64 pMC_TNA_TASK_ID, Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_LST", Value = pMC_STYLE_H_EXT_LST},
                    new CommandParameter() {ParameterName = "pHAS_EXT", Value = pHAS_EXT},
                    new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = pMC_TNA_TASK_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3010},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_EXT_NO = (dr["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_EXT_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TNA_FINALIZED"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_LOT_NO = (dr["ORDER_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LOT_NO"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    if (dr["MAIN_MERCH_ID"] != DBNull.Value)
                        ob.MAIN_MERCH_ID = (dr["MAIN_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAIN_MERCH_ID"]);

                    if (dr["ALT_MERCH_ID"] != DBNull.Value)
                        ob.ALT_MERCH_ID = (dr["ALT_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_MERCH_ID"]);

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROV"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                    if (dr["ORDER_DT"] != DBNull.Value)
                        ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);

                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = (dr["ART_WRK_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    if (dr["BOOKING_PRD"] != DBNull.Value)
                        ob.BOOKING_PRD = (dr["BOOKING_PRD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_PRD"]);

                    if (dr["RF_CALENDAR_WK_ID"] != DBNull.Value)
                        ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);

                    ob.HR_COUNTRY_ID_LST = (dr["HR_COUNTRY_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_COUNTRY_ID_LST"]);

                    if (dr["PLAN_START_DT"] != DBNull.Value)
                        ob.PLAN_START_DT = Convert.ToDateTime(dr["PLAN_START_DT"]);

                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.TOT_ORD_VALUE = 0; //(dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_VALUE"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);

                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);

                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string ExportOrderDataToExcel(Int64 pMC_ORDER_H_ID, string pSTYLE_NO)
        {
            string FileNameWithLocation = "d:\\" + pSTYLE_NO.ToString() + ".xlsx";
            string jsonStr = "{\"PMSG\":\"MULTI-001Successfully export\"}";
            var i = 1;

            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new MC_ORDER_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3012},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                ds.Tables[0].TableName = "ORDER_DATA";

                RF_COMMON_FUNCTIONModel obCommon = new RF_COMMON_FUNCTIONModel();
                obCommon.ExportDataSetToExcel(ds, FileNameWithLocation);
                //foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                //{
                //    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                //    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                //    {
                //        jsonStr += ",";
                //    }
                //    else
                //    {
                //        jsonStr += "}";
                //    }
                //    i++;
                //}
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public MC_ORDER_HModel OrderHdrData(long? pMC_ORDER_H_ID, long? pLK_ORD_STATUS_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var ob = new MC_ORDER_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = pLK_ORD_STATUS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    if (dr["HR_COMPANY_ID"] != DBNull.Value)
                        ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);

                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_EXT_NO = (dr["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_EXT_NO"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TNA_FINALIZED"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);

                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                    ob.TOTAL_PCS_QTY = (dr["TOTAL_PCS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_PCS_QTY"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_LOT_NO = (dr["ORDER_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LOT_NO"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);

                    if (dr["PROD_UNIT_ID"] != DBNull.Value)
                        ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);

                    if (dr["ORDER_DT"] != DBNull.Value)
                        ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);

                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = (dr["ART_WRK_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    if (dr["BOOKING_PRD"] != DBNull.Value)
                        ob.BOOKING_PRD = (dr["BOOKING_PRD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_PRD"]);

                    if (dr["RF_CALENDAR_WK_ID"] != DBNull.Value)
                        ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);
                    
                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);


                    ob.HR_COUNTRY_ID_LST = (dr["HR_COUNTRY_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_COUNTRY_ID_LST"]);

                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.TOT_ORD_VALUE = ob.TOT_ORD_QTY * ob.UNIT_PRICE;
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);

                    if (dr["REVISION_NO"] != DBNull.Value)
                    {
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                        ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    }
                    ob.IS_ORD_FINALIZED = (dr["IS_ORD_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ORD_FINALIZED"]);

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

                        obShipDummy.formItem = new { ORDER_QTY = 0, UNIT_PRICE = 0, QTY_MOU_ID = ob.QTY_MOU_ID };

                        obShipDummyList.Add(obShipDummy);

                        ob.itmsOrdShipDT = obShipDummyList;
                    }

                    var obBuyerList = new MC_BUYERModel().getBuyerDropdownList(ob.MC_BYR_ACC_ID);
                    ob.accWiseBuyerList = (List<MC_BUYERModel>)obBuyerList;

                    var obBuyerWiseStyle = new MC_STYLE_H_EXTModel().BuyerWiseStyleHExtList(ob.MC_BYR_ACC_ID, ob.MC_BUYER_ID);
                    ob.buyerWiseStyleList = (List<MC_STYLE_H_EXTModel>)obBuyerWiseStyle;

                    //var obOrderStatusList = new LookupDataModel().LookupListData(56);
                    //ob.orderStatusList = (List<LookupDataModel>)obOrderStatusList;

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_ORDER_SHIPModel> GetOrdShipList(long? pMC_ORDER_H_ID)
        {
            int i = 0;
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                OraDatabase db = new OraDatabase();
                
                List<MC_ORDER_SHIPModel> obOrdShipList = new List<MC_ORDER_SHIPModel>();
                var OrdShipDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                         new CommandParameter() {ParameterName = "pOption", Value = 3022},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                foreach (DataRow drOrdShip in OrdShipDs.Tables[0].Rows)
                {
                    MC_ORDER_SHIPModel obShipDt = new MC_ORDER_SHIPModel();
                    obShipDt.MC_ORDER_SHIP_ID = (drOrdShip["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drOrdShip["MC_ORDER_SHIP_ID"]);
                    obShipDt.MC_ORDER_H_ID = (drOrdShip["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drOrdShip["MC_ORDER_H_ID"]);
                    obShipDt.SHIP_DT = (drOrdShip["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drOrdShip["SHIP_DT"]);
                    obShipDt.SHIP_DESC = (drOrdShip["SHIP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(drOrdShip["SHIP_DESC"]);
                    obShipDt.SHIP_QTY = (drOrdShip["SHIP_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drOrdShip["SHIP_QTY"]);
                    obShipDt.QTY_MOU_ID = (drOrdShip["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drOrdShip["QTY_MOU_ID"]);
                    obShipDt.AVG_UNIT_PRICE = (drOrdShip["AVG_UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(drOrdShip["AVG_UNIT_PRICE"]);
                    obShipDt.cap_itms = new List<MC_GMT_CAP_ITEMModel>();
                    obShipDt.cap_itms = new MC_GMT_CAP_ITEMModel().Query(obShipDt.MC_ORDER_SHIP_ID);

                    if (i == 0)
                    {
                        i = i + 1;
                        obShipDt.IS_ACTIVE = true;
                    }

                    List<MC_ORDER_STYLModel> obStyleList = new List<MC_ORDER_STYLModel>();
                    var StyleDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = obShipDt.MC_ORDER_SHIP_ID},                         
                         new CommandParameter() {ParameterName = "pOption", Value = 3003},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                    foreach (DataRow drStyle in StyleDs.Tables[0].Rows)
                    {
                        MC_ORDER_STYLModel obStyle = new MC_ORDER_STYLModel();
                        obStyle.MC_ORDER_STYL_ID = (drStyle["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drStyle["MC_ORDER_STYL_ID"]);
                        obStyle.MC_ORDER_SHIP_ID = (drStyle["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drStyle["MC_ORDER_SHIP_ID"]);
                        obStyle.STYLE_NO = (drStyle["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drStyle["STYLE_NO"]);
                        obStyle.STYLE_EXT_NO = (drStyle["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drStyle["STYLE_EXT_NO"]);
                        obStyle.MC_STYLE_D_ITEM_ID = (drStyle["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drStyle["MC_STYLE_D_ITEM_ID"]);
                        if (drStyle["PARENT_ITEM_ID"] != DBNull.Value)
                            obStyle.PARENT_ITEM_ID = (drStyle["PARENT_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drStyle["PARENT_ITEM_ID"]);

                        obStyle.ORDER_QTY = (drStyle["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drStyle["ORDER_QTY"]);
                        obStyle.QTY_MOU_ID = (drStyle["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drStyle["QTY_MOU_ID"]);
                        obStyle.UNIT_PRICE = (drStyle["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(drStyle["UNIT_PRICE"]);

                        obStyle.COMBO_NO = (drStyle["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drStyle["COMBO_NO"]);

                        if (obStyle.COMBO_NO != "")
                        {
                            var vCboNo = obStyle.COMBO_NO.Split(',');
                            List<COMBO_NO_VmModel> obCboList = new List<COMBO_NO_VmModel>();
                            foreach (string cboItm in vCboNo)
                            {
                                COMBO_NO_VmModel obCboNo = new COMBO_NO_VmModel();
                                obCboNo.COMBO_NO = cboItm;

                                obCboList.Add(obCboNo);
                            }
                            obStyle.COMBO_NO_LIST = obCboList;
                        }
                        else
                        {
                            obStyle.COMBO_NO_LIST = new List<COMBO_NO_VmModel>();
                        }

                        obStyle.MODEL_NO = (drStyle["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drStyle["MODEL_NO"]);
                        obStyle.ITEM_NAME_EN = (drStyle["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drStyle["ITEM_NAME_EN"]) + " " + obStyle.MODEL_NO;

                        obStyle.MOU_NAME_EN = (drStyle["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drStyle["MOU_CODE"]);
                        obStyle.TOTAL_PRICE = obStyle.UNIT_PRICE * obStyle.ORDER_QTY;
                        obStyle.SUB_TOTAL_QTY = obStyle.ORDER_QTY;


                        //========================== Start For Parent Child
                        sp = "pkg_mc_order.mc_order_h_select";
                        List<MC_ORDER_STYLModel> obChildItmList = new List<MC_ORDER_STYLModel>();
                        var ChildItmDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = obShipDt.MC_ORDER_SHIP_ID},
                             new CommandParameter() {ParameterName = "pPARENT_ITEM_ID", Value = obStyle.MC_STYLE_D_ITEM_ID},
                             new CommandParameter() {ParameterName = "pOption", Value = 3006},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                        foreach (DataRow drChildItm in ChildItmDs.Tables[0].Rows)
                        {
                            MC_ORDER_STYLModel obChildItm = new MC_ORDER_STYLModel();
                            obChildItm.MC_ORDER_STYL_ID = (drChildItm["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItm["MC_ORDER_STYL_ID"]);
                            obChildItm.MC_ORDER_SHIP_ID = (drChildItm["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItm["MC_ORDER_SHIP_ID"]);
                            obChildItm.STYLE_NO = (drChildItm["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drChildItm["STYLE_NO"]);
                            obChildItm.STYLE_EXT_NO = (drChildItm["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drChildItm["STYLE_EXT_NO"]);
                            obChildItm.MC_STYLE_D_ITEM_ID = (drChildItm["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItm["MC_STYLE_D_ITEM_ID"]);
                            if (drChildItm["PARENT_ITEM_ID"] != DBNull.Value)
                                obChildItm.PARENT_ITEM_ID = (drChildItm["PARENT_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItm["PARENT_ITEM_ID"]);

                            obChildItm.ORDER_QTY = (drChildItm["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItm["ORDER_QTY"]);
                            obChildItm.QTY_MOU_ID = (drChildItm["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItm["QTY_MOU_ID"]);
                            obChildItm.UNIT_PRICE = (drChildItm["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(drChildItm["UNIT_PRICE"]);

                            obChildItm.COMBO_NO = (drChildItm["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drChildItm["COMBO_NO"]);

                            obChildItm.COMBO_NO_LIST = obStyle.COMBO_NO_LIST;

                            obChildItm.MODEL_NO = (drChildItm["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drChildItm["MODEL_NO"]);
                            obChildItm.ITEM_NAME_EN = (drChildItm["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drChildItm["ITEM_NAME_EN"]) + " " + obChildItm.MODEL_NO;

                            obChildItm.MOU_NAME_EN = (drChildItm["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drChildItm["MOU_CODE"]);
                            obChildItm.TOTAL_PRICE = obChildItm.UNIT_PRICE * obChildItm.ORDER_QTY;
                            obChildItm.SUB_TOTAL_QTY = obChildItm.ORDER_QTY;
                            //obStyle.SUB_TOTAL_PRICE = obStyle.STYLE_TOTAL_PRICE;


                            sp = "pkg_mc_order.mc_order_h_select";
                            List<MC_ORDER_COLModel> obChildItmColorList = new List<MC_ORDER_COLModel>();
                            var ChildItmColorDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_ORDER_STYL_ID", Value = obChildItm.MC_ORDER_STYL_ID},                                     
                                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                }, sp);
                            foreach (DataRow drChildItmColor in ChildItmColorDs.Tables[0].Rows)
                            {                                
                                MC_ORDER_COLModel obChildItmColor = new MC_ORDER_COLModel();

                                obChildItmColor.MC_ORDER_COL_ID = (drChildItmColor["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItmColor["MC_ORDER_COL_ID"]);
                                obChildItmColor.MC_ORDER_STYL_ID = (drChildItmColor["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItmColor["MC_ORDER_STYL_ID"]);
                                obChildItmColor.MC_COLOR_ID = (drChildItmColor["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItmColor["MC_COLOR_ID"]);

                                if (drChildItmColor["HR_COUNTRY_ID"] != DBNull.Value)
                                    obChildItmColor.HR_COUNTRY_ID = (drChildItmColor["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItmColor["HR_COUNTRY_ID"]);
                                
                                //obColor.COLOR_INDEX = (drColor["COLOR_INDEX"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["COLOR_INDEX"]);
                                //obColor.ITEM_INDEX = (drColor["ITEM_INDEX"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["ITEM_INDEX"]);
                                //obColor.COLOR_NAME_EN = (drColor["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(drColor["COLOR_NAME_EN"]);
                                obChildItmColor.COLOR_REF = (drChildItmColor["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drChildItmColor["COLOR_REF"]);
                                obChildItmColor.COMBO_NO = (drChildItmColor["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drChildItmColor["COMBO_NO"]);

                                //var vItmCboNo = obStyle.COMBO_NO.Split(',');
                                //List<COMBO_NO_VmModel> obItmCboList = new List<COMBO_NO_VmModel>();
                                //foreach (string cboItm in vItmCboNo)
                                //{
                                //    COMBO_NO_VmModel obItmCboNo = new COMBO_NO_VmModel();
                                //    obItmCboNo.COMBO_NO = cboItm;

                                //    obItmCboList.Add(obItmCboNo);
                                //}
                                obChildItmColor.COMBO_NO_LIST = obStyle.COMBO_NO_LIST;

                                if (drChildItmColor["PACK_NO"] != DBNull.Value)
                                { obChildItmColor.PACK_NO = (drChildItmColor["PACK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItmColor["PACK_NO"]); }

                                //obChildItmColor.COLOR_DISPLAY_ORDER = (drChildItmColor["COLOR_DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(drChildItmColor["COLOR_DISPLAY_ORDER"]);
                                obChildItmColor.TOTAL_QTY = 0;

                                sp = "pkg_mc_order.mc_order_h_select";
                                List<MC_ORDER_SIZEModel> obSzList = new List<MC_ORDER_SIZEModel>();
                                var SzDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pMC_ORDER_STYL_ID", Value = obChildItm.MC_ORDER_STYL_ID},                                         
                                         new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = obChildItmColor.MC_COLOR_ID},
                                         new CommandParameter() {ParameterName = "pMC_ORDER_COL_ID", Value = obChildItmColor.MC_ORDER_COL_ID},
                                         new CommandParameter() {ParameterName = "pOption", Value = 3005},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                    }, sp);
                                foreach (DataRow drSz in SzDs.Tables[0].Rows)
                                {
                                    MC_ORDER_SIZEModel obSz = new MC_ORDER_SIZEModel();
                                    obSz.MC_ORDER_SIZE_ID = (drSz["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_ORDER_SIZE_ID"]);
                                    obSz.MC_ORDER_COL_ID = (drSz["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_ORDER_COL_ID"]);
                                    //obSz.COLOR_INDEX = (drSz["COLOR_INDEX"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["COLOR_INDEX"]);
                                                                       
                                    if (drSz["MC_SIZE_ID"] != DBNull.Value)
                                        obSz.MC_SIZE_ID = (drSz["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_SIZE_ID"]);

                                    obSz.SIZE_CODE = (drSz["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drSz["SIZE_CODE"]);
                                    if (Convert.ToInt64(drSz["SIZE_QTY"]) != 0)
                                    { obSz.SIZE_QTY = (drSz["SIZE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["SIZE_QTY"]); }
                                    obSz.QTY_MOU_ID = (drSz["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["QTY_MOU_ID"]);
                                    obSz.UNIT_PRICE = (drSz["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(drSz["UNIT_PRICE"]);
                                    obSz.TOTAL_SIZE_PRICE = obSz.UNIT_PRICE * obSz.SIZE_QTY;

                                    obChildItmColor.TOTAL_QTY = obChildItmColor.TOTAL_QTY + ((drSz["SIZE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["SIZE_QTY"]));

                                    obSzList.Add(obSz);
                                }
                                obChildItmColor.itemsSize = obSzList;
                                obChildItmColorList.Add(obChildItmColor);
                            }
                            obChildItm.itemsColor = obChildItmColorList;
                            obChildItmList.Add(obChildItm);
                        }
                        obStyle.childItems = obChildItmList;
                        //========================== End For Parent Child


                        sp = "pkg_mc_order.mc_order_h_select";
                        List<MC_ORDER_COLModel> obColorList = new List<MC_ORDER_COLModel>();
                        var ColorDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                            {                                 
                                 new CommandParameter() {ParameterName = "pMC_ORDER_STYL_ID", Value = obStyle.MC_ORDER_STYL_ID},                                                                  
                                 new CommandParameter() {ParameterName = "pOption", Value = 3004},
                                 new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                            }, sp);
                        foreach (DataRow drColor in ColorDs.Tables[0].Rows)
                        {
                            MC_ORDER_COLModel obColor = new MC_ORDER_COLModel();

                            obColor.MC_ORDER_COL_ID = (drColor["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["MC_ORDER_COL_ID"]);
                            obColor.MC_ORDER_STYL_ID = (drColor["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["MC_ORDER_STYL_ID"]);
                            obColor.MC_COLOR_ID = (drColor["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["MC_COLOR_ID"]);

                            if (drColor["HR_COUNTRY_ID"] != DBNull.Value)
                                obColor.HR_COUNTRY_ID = (drColor["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["HR_COUNTRY_ID"]);

                            obColor.COLOR_REF = (drColor["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drColor["COLOR_REF"]);
                            obColor.COMBO_NO = (drColor["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drColor["COMBO_NO"]);

                            obColor.COMBO_NO_LIST = obStyle.COMBO_NO_LIST;

                            if (drColor["PACK_NO"] != DBNull.Value)
                            { obColor.PACK_NO = (drColor["PACK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["PACK_NO"]); }

                            //obColor.COLOR_DISPLAY_ORDER = (drColor["COLOR_DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["COLOR_DISPLAY_ORDER"]);
                            obColor.TOTAL_QTY = 0;

                            sp = "pkg_mc_order.mc_order_h_select";
                            List<MC_ORDER_SIZEModel> obSzList = new List<MC_ORDER_SIZEModel>();
                            var SzDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_ORDER_COL_ID", Value = obColor.MC_ORDER_COL_ID},                                     
                                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                }, sp);
                            foreach (DataRow drSz in SzDs.Tables[0].Rows)
                            {
                                MC_ORDER_SIZEModel obSz = new MC_ORDER_SIZEModel();
                                obSz.MC_ORDER_SIZE_ID = (drSz["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_ORDER_SIZE_ID"]);
                                obSz.MC_ORDER_COL_ID = (drSz["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_ORDER_COL_ID"]);
                                //obSz.COLOR_INDEX = (drSz["COLOR_INDEX"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["COLOR_INDEX"]);
                                
                                if (drSz["MC_SIZE_ID"] != DBNull.Value)
                                    obSz.MC_SIZE_ID = (drSz["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_SIZE_ID"]);

                                obSz.SIZE_CODE = (drSz["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drSz["SIZE_CODE"]);
                                if (Convert.ToInt64(drSz["SIZE_QTY"]) != 0)
                                { obSz.SIZE_QTY = (drSz["SIZE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["SIZE_QTY"]); }
                                obSz.QTY_MOU_ID = (drSz["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["QTY_MOU_ID"]);
                                obSz.UNIT_PRICE = (drSz["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(drSz["UNIT_PRICE"]);
                                obSz.TOTAL_SIZE_PRICE = obSz.UNIT_PRICE * obSz.SIZE_QTY;

                                obColor.TOTAL_QTY = obColor.TOTAL_QTY + ((drSz["SIZE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["SIZE_QTY"]));

                                obSzList.Add(obSz);
                            }
                            obColor.itemsSize = obSzList;
                            obColorList.Add(obColor);
                        }
                        obStyle.itemsColor = obColorList;
                        obStyleList.Add(obStyle);
                    }
                    obShipDt.itemsStyle = obStyleList;

                    obShipDt.formItem = new { QTY_MOU_ID = "", ORDER_QTY = 0, UNIT_PRICE = 0, TOTAL_PRICE = 0 };                        

                    obOrdShipList.Add(obShipDt);
                }
                return obOrdShipList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ORDER_CNTRYModel> GetOrdCountryList(long? pMC_ORDER_H_ID)
        {            
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                OraDatabase db = new OraDatabase();

                List<MC_ORDER_CNTRYModel> obOrdCntryList = new List<MC_ORDER_CNTRYModel>();
                var Ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                         new CommandParameter() {ParameterName = "pOption", Value = 3023},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    MC_ORDER_CNTRYModel ob = new MC_ORDER_CNTRYModel();
                    ob.MC_ORDER_CNTRY_ID = (dr["MC_ORDER_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_CNTRY_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.ORD_QTY = (dr["ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);

                    obOrdCntryList.Add(ob);
                }
                return obOrdCntryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ORDER_COLModel> GetOrdColList(long pMC_ORDER_H_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_COLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3024},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_COLModel ob = new MC_ORDER_COLModel();
                    ob.MC_ORDER_COL_ID = (dr["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_COL_ID"]);
                    ob.MC_ORDER_STYL_ID = (dr["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_STYL_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    //ob.STYL_COL_QTY = (dr["STYL_COL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STYL_COL_QTY"]);
                    //ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    //ob.AVG_UNIT_PRICE = (dr["AVG_UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_UNIT_PRICE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_D_ITEMModel> GetOrdItmList(Int64? pMC_ORDER_H_ID, Int64? pMC_COLOR_ID = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_STYLE_D_ITEMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3026},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_D_ITEMModel ob = new MC_STYLE_D_ITEMModel();
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //It will implemente........
        public List<MC_OrderShipDateVM> GetItemColorSizeQty(Int64 pMC_ORDER_STYL_ID, List<COMBO_NO_VmModel> pCOMBO_NO_LIST)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            OraDatabase db = new OraDatabase();
            List<MC_OrderShipDateVM> obShDtList = new List<MC_OrderShipDateVM>();
            var ShDtDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_STYL_ID", Value = pMC_ORDER_STYL_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3011},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
            foreach (DataRow drShDt in ShDtDs.Tables[0].Rows)
            {
                MC_OrderShipDateVM obShDt = new MC_OrderShipDateVM();
                obShDt.SHIP_DT = (drShDt["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drShDt["SHIP_DT"]);

                sp = "pkg_mc_order.mc_order_h_select";
                List<MC_OrderColorViewModel> obColorList = new List<MC_OrderColorViewModel>();
                var ColorDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pMC_ORDER_STYL_ID", Value = pMC_ORDER_STYL_ID},
                         new CommandParameter() {ParameterName = "pSHIP_DT", Value = obShDt.SHIP_DT},
                         new CommandParameter() {ParameterName = "pOption", Value = 3004},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                    }, sp);
                foreach (DataRow drColor in ColorDs.Tables[0].Rows)
                {
                    MC_OrderColorViewModel obColor = new MC_OrderColorViewModel();
                    obColor.MC_COLOR_ID = (drColor["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["MC_COLOR_ID"]);
                    obColor.COLOR_REF = (drColor["COLOR_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(drColor["COLOR_REF"]);
                    obColor.COMBO_NO = (drColor["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drColor["COMBO_NO"]);

                    obColor.COMBO_NO_LIST = pCOMBO_NO_LIST;

                    if (drColor["PACK_NO"] != DBNull.Value)
                    { obColor.PACK_NO = (drColor["PACK_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["PACK_NO"]); }

                    obColor.COLOR_DISPLAY_ORDER = (drColor["COLOR_DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(drColor["COLOR_DISPLAY_ORDER"]);
                    obColor.TOTAL_QTY = 0;

                    sp = "pkg_mc_order.mc_order_h_select";
                    List<MC_ORDER_SIZEModel> obSzList = new List<MC_ORDER_SIZEModel>();
                    var SzDs = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_ORDER_STYL_ID", Value = pMC_ORDER_STYL_ID},
                             new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = obColor.MC_COLOR_ID},
                             new CommandParameter() {ParameterName = "pSHIP_DT", Value = obShDt.SHIP_DT},
                             new CommandParameter() {ParameterName = "pCOLOR_DISPLAY_ORDER", Value = obColor.COLOR_DISPLAY_ORDER},
                             new CommandParameter() {ParameterName = "pOption", Value = 3005},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                        }, sp);
                    foreach (DataRow drSz in SzDs.Tables[0].Rows)
                    {
                        MC_ORDER_SIZEModel obSz = new MC_ORDER_SIZEModel();
                        obSz.MC_ORDER_SIZE_ID = (drSz["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_ORDER_SIZE_ID"]);
                        obSz.MC_ORDER_COL_ID = (drSz["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_ORDER_COL_ID"]);
                                                                        
                        if (drSz["MC_SIZE_ID"] != DBNull.Value)
                            obSz.MC_SIZE_ID = (drSz["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["MC_SIZE_ID"]);

                        obSz.SIZE_CODE = (drSz["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(drSz["SIZE_CODE"]);
                        if (Convert.ToInt64(drSz["SIZE_QTY"]) != 0)
                        { obSz.SIZE_QTY = (drSz["SIZE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["SIZE_QTY"]); }
                        obSz.QTY_MOU_ID = (drSz["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["QTY_MOU_ID"]);
                        obSz.UNIT_PRICE = (drSz["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(drSz["UNIT_PRICE"]);
                        obSz.TOTAL_SIZE_PRICE = obSz.UNIT_PRICE * obSz.SIZE_QTY;

                        obColor.TOTAL_QTY = obColor.TOTAL_QTY + ((drSz["SIZE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(drSz["SIZE_QTY"]));

                        obSzList.Add(obSz);
                    }
                    obColor.itemsSize = obSzList;
                    obColorList.Add(obColor);
                }
                obShDt.itemsColor = obColorList;
                obShDtList.Add(obShDt);
            }
            return obShDtList;
        }

        public List<MC_FAB_PROD_ORD_HModel> SelectShipmentMonth(Int64? MC_BYR_ACC_ID = null, string STYLE_NO = null, string ORDER_NO = null, string ORDER_TYPE = null, string NATURE_OF_ORDER = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_mc_order.order_h_ship_month_select";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = STYLE_DESC},
                    new CommandParameter() {ParameterName = "pSTYLE_NO", Value = STYLE_NO},
                    new CommandParameter() {ParameterName = "pORDER_NO", Value = ORDER_NO},
                    new CommandParameter() {ParameterName = "pORDER_TYPE_NAME_EN", Value = ORDER_TYPE},
                    new CommandParameter() {ParameterName = "pNATURE_OF_ORDER", Value = NATURE_OF_ORDER},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_HModel ob = new MC_FAB_PROD_ORD_HModel();

                    ob.MONTHOF = (dr["MONTHOF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTHOF"]);
                    ob.FIRSTDATE = (dr["FIRSTDATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FIRSTDATE"]);
                    ob.LASTDATE = (dr["LASTDATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LASTDATE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ORDER_HModel> OrderListForSCM(int pageNo, int pageSize, Int64? MC_BYR_ACC_ID = null, string STYLE_NO = null,
            string ORDER_NO = null, string ORDER_TYPE = null, string NATURE_OF_ORDER = null, string pFIRST_DATE = null)
        //public List<MC_ORDER_HModel> OrderListForSCM(Int64? pMC_BUYER_ID, Int64? pMC_ORDER_H_ID, Int64? pMC_STYLE_H_ID, Int64? pMC_BUYER_OFF_ID, Int64? pMC_BYR_ACC_ID, Int64? pLK_ORD_STATUS_ID, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = STYLE_DESC},
                    new CommandParameter() {ParameterName = "pSTYLE_NO", Value = STYLE_NO},
                    new CommandParameter() {ParameterName = "pORDER_NO", Value = ORDER_NO},
                    new CommandParameter() {ParameterName = "pORDER_TYPE_NAME_EN", Value = ORDER_TYPE},
                    new CommandParameter() {ParameterName = "pNATURE_OF_ORDER", Value = NATURE_OF_ORDER},
                    new CommandParameter() {ParameterName = "pFIRST_DATE", Value =pFIRST_DATE},

                    new CommandParameter() {ParameterName = "pOption", Value = 3014},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_EXT_NO = (dr["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_EXT_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TNA_FINALIZED"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_LOT_NO = (dr["ORDER_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LOT_NO"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORDER_TYPE_NAME_EN = (dr["ORDER_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE_NAME_EN"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);

                    if (dr["MAIN_MERCH_ID"] != DBNull.Value)
                        ob.MAIN_MERCH_ID = (dr["MAIN_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAIN_MERCH_ID"]);

                    if (dr["ALT_MERCH_ID"] != DBNull.Value)
                        ob.ALT_MERCH_ID = (dr["ALT_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_MERCH_ID"]);

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROV"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                    if (dr["ORDER_DT"] != DBNull.Value)
                        ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);

                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = (dr["ART_WRK_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    if (dr["BOOKING_PRD"] != DBNull.Value)
                        ob.BOOKING_PRD = (dr["BOOKING_PRD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_PRD"]);

                    if (dr["RF_CALENDAR_WK_ID"] != DBNull.Value)
                        ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);

                    ob.HR_COUNTRY_ID_LST = (dr["HR_COUNTRY_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_COUNTRY_ID_LST"]);

                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    //ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_VALUE"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);

                    if (dr["NATURE_OF_ORDER"] != DBNull.Value)
                        ob.NATURE_OF_ORDER = Convert.ToString(dr["NATURE_OF_ORDER"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    //ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    //ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    //ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    //ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);

                    //ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    //ob.WEEK_NUM_DATE_RANGE = (dr["WEEK_NUM_DATE_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEEK_NUM_DATE_RANGE"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_ORDER_HModel> OrderListForAccBk(int pageNo, int pageSize, Int64? MC_BYR_ACC_ID = null, string STYLE_NO = null,
            string ORDER_NO = null, string ORDER_TYPE = null, string NATURE_OF_ORDER = null, string pFIRST_DATE = null, Int64? MC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = MC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = MC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = STYLE_DESC},
                    new CommandParameter() {ParameterName = "pSTYLE_NO", Value = STYLE_NO},
                    new CommandParameter() {ParameterName = "pORDER_NO", Value = ORDER_NO},
                    new CommandParameter() {ParameterName = "pORDER_TYPE_NAME_EN", Value = ORDER_TYPE},
                    new CommandParameter() {ParameterName = "pNATURE_OF_ORDER", Value = NATURE_OF_ORDER},
                    new CommandParameter() {ParameterName = "pFIRST_DATE", Value =pFIRST_DATE},

                    new CommandParameter() {ParameterName = "pOption", Value = 3020},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_EXT_NO = (dr["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_EXT_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TNA_FINALIZED"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_LOT_NO = (dr["ORDER_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LOT_NO"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORDER_TYPE_NAME_EN = (dr["ORDER_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE_NAME_EN"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);

                    if (dr["MAIN_MERCH_ID"] != DBNull.Value)
                        ob.MAIN_MERCH_ID = (dr["MAIN_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAIN_MERCH_ID"]);

                    if (dr["ALT_MERCH_ID"] != DBNull.Value)
                        ob.ALT_MERCH_ID = (dr["ALT_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_MERCH_ID"]);

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROV"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                    if (dr["ORDER_DT"] != DBNull.Value)
                        ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);

                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = (dr["ART_WRK_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    if (dr["BOOKING_PRD"] != DBNull.Value)
                        ob.BOOKING_PRD = (dr["BOOKING_PRD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_PRD"]);

                    if (dr["RF_CALENDAR_WK_ID"] != DBNull.Value)
                        ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);

                    ob.HR_COUNTRY_ID_LST = (dr["HR_COUNTRY_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_COUNTRY_ID_LST"]);

                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    //ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_VALUE"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);

                    if (dr["NATURE_OF_ORDER"] != DBNull.Value)
                        ob.NATURE_OF_ORDER = Convert.ToString(dr["NATURE_OF_ORDER"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    //ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    //ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    //ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    //ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);

                    //ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    //ob.WEEK_NUM_DATE_RANGE = (dr["WEEK_NUM_DATE_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WEEK_NUM_DATE_RANGE"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_ORDER_HModel> OrderListForCollarCuff(int pageNo, int pageSize, string pBYR_ACC_NAME_EN = null, string pSTYLE_DESC = null, string pSTYLE_NO = null,
            string pORDER_NO = null, string pORDER_TYPE_NAME_EN = null, string pNATURE_OF_ORDER = null, Int64? pMC_STYLE_H_ID = null, Int64? pMC_BYR_ACC_ID = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value = pBYR_ACC_NAME_EN},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},                    
                    new CommandParameter() {ParameterName = "pSTYLE_DESC", Value = pSTYLE_DESC},
                    new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                    new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO},
                    new CommandParameter() {ParameterName = "pORDER_TYPE_NAME_EN", Value = pORDER_TYPE_NAME_EN},
                    new CommandParameter() {ParameterName = "pNATURE_OF_ORDER", Value = pNATURE_OF_ORDER},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},

                    new CommandParameter() {ParameterName = "pOption", Value = 3015},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_EXT_NO = (dr["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_EXT_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TNA_FINALIZED"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_LOT_NO = (dr["ORDER_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LOT_NO"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORDER_TYPE_NAME_EN = (dr["ORDER_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE_NAME_EN"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

                    if (dr["MAIN_MERCH_ID"] != DBNull.Value)
                        ob.MAIN_MERCH_ID = (dr["MAIN_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAIN_MERCH_ID"]);

                    if (dr["ALT_MERCH_ID"] != DBNull.Value)
                        ob.ALT_MERCH_ID = (dr["ALT_MERCH_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_MERCH_ID"]);

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROV"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);

                    if (dr["ORDER_DT"] != DBNull.Value)
                        ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);

                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = (dr["ART_WRK_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    if (dr["BOOKING_PRD"] != DBNull.Value)
                        ob.BOOKING_PRD = (dr["BOOKING_PRD"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BOOKING_PRD"]);

                    if (dr["RF_CALENDAR_WK_ID"] != DBNull.Value)
                        ob.RF_CALENDAR_WK_ID = (dr["RF_CALENDAR_WK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_WK_ID"]);

                    ob.HR_COUNTRY_ID_LST = (dr["HR_COUNTRY_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HR_COUNTRY_ID_LST"]);

                    ob.SEASON_REF = (dr["SEASON_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON_REF"]);
                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAD_TIME"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    //ob.TOT_ORD_VALUE = (dr["TOT_ORD_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_VALUE"]);

                    if (dr["QTY_MOU_ID"] != DBNull.Value)
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    if (dr["MC_TNA_TMPLT_H_ID"] != DBNull.Value)
                        ob.MC_TNA_TMPLT_H_ID = Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);

                    if (dr["NATURE_OF_ORDER"] != DBNull.Value)
                        ob.NATURE_OF_ORDER = Convert.ToString(dr["NATURE_OF_ORDER"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_ORDER_HModel> OrderListByOrderNo(string pORDER_NO, Int32? pLK_ORD_STATUS_ID, string pIS_PROV)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = pLK_ORD_STATUS_ID},
                      new CommandParameter() {ParameterName = "pIS_PROV", Value = pIS_PROV},
                   
                    new CommandParameter() {ParameterName = "pOption", Value = 3016},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_FAB_PROD_ORD_HModel> OrderShipmentMonth(Int64? pMC_BYR_ACC_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3019},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_HModel ob = new MC_FAB_PROD_ORD_HModel();

                    ob.MONTHOF = (dr["MONTHOF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MONTHOF"]);
                    ob.FIRSTDATE = (dr["FIRSTDATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FIRSTDATE"]);
                    ob.LASTDATE = (dr["LASTDATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LASTDATE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int? IS_IE_UPD { get; set; }
        public long? MC_ORDER_SHIP_ID { get; set; }
        public string STYL_KEY_IMG { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public decimal NET_GFAB_QTY { get; set; }

        public decimal NET_FFAB_QTY { get; set; }

        public decimal KNIT_QC_PASS_QTY { get; set; }

        public decimal DYE_QC_PASS_QTY { get; set; }

        public DateTime PROD_ORD_DT { get; set; }

        public string FIB_COMP_CODE { get; set; }

        public string FIN_GSM { get; set; }

        public string FAB_TYPE_SNAME { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string PRINT_EMB_AOP_YD { get; set; }

        public string GARM_DEPT_NAME { get; set; }

        public string STYLE_ORDER_NO { get; set; }
        public string IS_STYL_ITM_OPR_MAP { get; set; }

        private List<dynamic> _images = null;
        public List<dynamic> images
        {
            get
            {
                if (_images == null)
                {
                    return new List<dynamic>();
                }
                return _images;
            }
            set
            {
                _images = value;
            }
        }

        private object _FIRST_SAMPLE = null;
        public object FIRST_SAMPLE
        {
            get
            {
                if (_FIRST_SAMPLE == null)
                {
                    _FIRST_SAMPLE = new
                    {
                        ACT_START_DT = "",
                        ACT_END_DT = ""
                    };
                }
                return _FIRST_SAMPLE;
            }
            set
            {
                _FIRST_SAMPLE = value;
            }
        }

        private object _COUNTER_SAMPLE = null;
        public object COUNTER_SAMPLE
        {
            get
            {
                if (_COUNTER_SAMPLE == null)
                {
                    _COUNTER_SAMPLE = new
                    {
                        ACT_START_DT = "",
                        ACT_END_DT = ""
                    };
                }
                return _COUNTER_SAMPLE;
            }
            set
            {
                _COUNTER_SAMPLE = value;
            }
        }


        private object _EMBR_STRIKE = null;
        public object EMBR_STRIKE
        {
            get
            {
                if (_EMBR_STRIKE == null)
                {
                    _EMBR_STRIKE = new
                    {
                        ACT_START_DT = "",
                        ACT_END_DT = ""
                    };
                }
                return _EMBR_STRIKE;
            }
            set
            {
                _EMBR_STRIKE = value;
            }
        }

        private object _PRINT_STRIKE = null;
        public object PRINT_STRIKE
        {
            get
            {
                if (_PRINT_STRIKE == null)
                {
                    _PRINT_STRIKE = new
                    {
                        ACT_START_DT = "",
                        ACT_END_DT = ""
                    };
                }
                return _PRINT_STRIKE;
            }
            set
            {
                _PRINT_STRIKE = value;
            }
        }

        private object _AOP_STRIKE = null;
        public object AOP_STRIKE
        {
            get
            {
                if (_AOP_STRIKE == null)
                {
                    _AOP_STRIKE = new
                    {
                        ACT_START_DT = "",
                        ACT_END_DT = ""
                    };
                }
                return _AOP_STRIKE;
            }
            set
            {
                _AOP_STRIKE = value;
            }
        }


        public List<MC_ORDER_HModel> BuyerStyleOrderFollowupList(
            int pageNumber,
            int pageSize,
            long? pMC_BYR_ACC_ID,
            DateTime? pFIRSTDATE,
            DateTime? pLASTDATE,
            string pMC_ORDER_H_ID_LST,
            int? pRF_FAB_PROD_CAT_ID,
            string pSTYLE_ORDER_NO = null
         )
        {
            string sp = "PKG_FAB_PROD_ORDER.MC_FAB_PROD_ORD_H_SELECT";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                    new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                    
                    new CommandParameter() {ParameterName = "pORDER_NO_LST", Value =pMC_ORDER_H_ID_LST},
                    new CommandParameter() {ParameterName = "pSTYLE_ORDER_NO", Value =pSTYLE_ORDER_NO},

                    new CommandParameter() {ParameterName = "pOption", Value = 3006},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    MC_ORDER_HModel ob = new MC_ORDER_HModel();



                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.GARM_DEPT_NAME = (dr["GARM_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_DEPT_NAME"]);
                    ob.PRINT_EMB_AOP_YD = (dr["PRINT_EMB_AOP_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRINT_EMB_AOP_YD"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);

                    ob.FIB_COMP_CODE_FULL = (dr["FIB_COMP_CODE_FULL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE_FULL"]);

                    ob.STYLE_ORDER_NO = (dr["STYLE_ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_ORDER_NO"]);

                    if (dr["PROD_ORD_DT"] != DBNull.Value)
                        ob.PROD_ORD_DT = (dr["PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_ORD_DT"]);

                    //if (dr["ORDER_DT"] != DBNull.Value)
                    //    ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);



                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    if (dr["ACT_SHIP_DATE"] != DBNull.Value)
                        ob.ACT_SHIP_DATE = Convert.ToDateTime(dr["ACT_SHIP_DATE"]);


                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT =  Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_DOC_RCV_DT_P"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT_P = Convert.ToDateTime(dr["ORD_DOC_RCV_DT_P"]);

                    if (dr["ART_WRK_RCV_DT_P"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT_P = Convert.ToDateTime(dr["ART_WRK_RCV_DT_P"]);


                    if (dr["TRIM_BOOKING_DATE"] != DBNull.Value)
                        ob.TRIM_BOOKING_DATE = Convert.ToDateTime(dr["TRIM_BOOKING_DATE"]);

                    if (dr["TRIM_INHOUSE_DATE"] != DBNull.Value)
                        ob.TRIM_INHOUSE_DATE = Convert.ToDateTime(dr["TRIM_INHOUSE_DATE"]);

                    if (dr["COL_STD_RECV_DATE"] != DBNull.Value)
                        ob.COL_STD_RECV_DATE = Convert.ToDateTime(dr["COL_STD_RECV_DATE"]);

                    if (dr["LD_SEND_DATE"] != DBNull.Value)
                        ob.LD_SEND_DATE = Convert.ToDateTime(dr["LD_SEND_DATE"]);

                    if (dr["LD_APPROVE_DATE"] != DBNull.Value)
                        ob.LD_APPROVE_DATE = Convert.ToDateTime(dr["LD_APPROVE_DATE"]);

                    if (dr["YRN_BK_DATE"] != DBNull.Value)
                        ob.YRN_BK_DATE = Convert.ToDateTime(dr["YRN_BK_DATE"]);

                    if (dr["YRN_INH_DATE"] != DBNull.Value)
                        ob.YRN_INH_DATE = Convert.ToDateTime(dr["YRN_INH_DATE"]);



                    if (dr["TRIM_BOOKING_DATE_P"] != DBNull.Value)
                        ob.TRIM_BOOKING_DATE_P = Convert.ToDateTime(dr["TRIM_BOOKING_DATE_P"]);

                    if (dr["TRIM_INHOUSE_DATE_P"] != DBNull.Value)
                        ob.TRIM_INHOUSE_DATE_P = Convert.ToDateTime(dr["TRIM_INHOUSE_DATE_P"]);

                    if (dr["COL_STD_RECV_DATE_P"] != DBNull.Value)
                        ob.COL_STD_RECV_DATE_P = Convert.ToDateTime(dr["COL_STD_RECV_DATE_P"]);

                    if (dr["LD_SEND_DATE_P"] != DBNull.Value)
                        ob.LD_SEND_DATE_P = Convert.ToDateTime(dr["LD_SEND_DATE_P"]);

                    if (dr["LD_APPROVE_DATE_P"] != DBNull.Value)
                        ob.LD_APPROVE_DATE_P = Convert.ToDateTime(dr["LD_APPROVE_DATE_P"]);

                    if (dr["YRN_BK_DATE_P"] != DBNull.Value)
                        ob.YRN_BK_DATE_P = Convert.ToDateTime(dr["YRN_BK_DATE_P"]);

                    if (dr["YRN_INH_DATE_P"] != DBNull.Value)
                        ob.YRN_INH_DATE_P = Convert.ToDateTime(dr["YRN_INH_DATE_P"]);


                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.TOT_ORD_QTY_COL = (dr["TOT_ORD_QTY_COL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY_COL"]);


                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);

                    ob.NET_FFAB_QTY = (dr["NET_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_FFAB_QTY"]);
                    ob.KNIT_QC_PASS_QTY = (dr["KNIT_QC_PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["KNIT_QC_PASS_QTY"]);


                    ob.DYE_QC_PASS_QTY = (dr["DYE_QC_PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DYE_QC_PASS_QTY"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.STYLE_ORD_SPAN = (dr["STYLE_ORD_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STYLE_ORD_SPAN"]);
                    ob.STYLE_ORD_SL = (dr["STYLE_ORD_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STYLE_ORD_SL"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_STYLE_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    ob.IS_GMT_WASH = (dr["IS_GMT_WASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GMT_WASH"]);

                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    if (dr["LAST_REV_NO"] != DBNull.Value)
                    {
                        ob.LAST_REV_NO = Convert.ToInt32(dr["LAST_REV_NO"]);
                    }
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);

                    if (dr["LAST_REV_NO_BGT"] != DBNull.Value)
                    {
                        ob.LAST_REV_NO_BGT = Convert.ToInt32(dr["LAST_REV_NO_BGT"]);
                    }

                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LEAD_TIME"]);


                    ob.images = new List<dynamic>();

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3007 },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr1["STYL_KEY_IMG"] != DBNull.Value)
                        {
                            ob.images.Add(new
                            {
                                STYL_KEY_IMG = (dr1["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STYL_KEY_IMG"])
                            });
                        }

                    }


                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 23},// Fist Sample
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        ob.FIRST_SAMPLE = (new
                         {
                             PLAN_START_DT = (dr2["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr2["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                             PLAN_END_DT = (dr2["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr2["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                             ACT_START_DT = (dr2["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr2["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                             ACT_END_DT = (dr2["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr2["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                         });
                    }


                    var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 29 },// Counter Sample
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                    {
                        ob.COUNTER_SAMPLE = (new
                        {
                            PLAN_START_DT = (dr3["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr3["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                            PLAN_END_DT = (dr3["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr3["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_START_DT = (dr3["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr3["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_END_DT = (dr3["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr3["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                        });

                    }

                    var ds4 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 10 },// Print Strike-off
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr4 in ds4.Tables[0].Rows)
                    {
                        ob.PRINT_STRIKE = (new
                        {
                            PLAN_START_DT = (dr4["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr4["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                            PLAN_END_DT = (dr4["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr4["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_START_DT = (dr4["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr4["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_END_DT = (dr4["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr4["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                        });
                    }

                    var ds5 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 11 },// Embr Strike Off
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr5 in ds5.Tables[0].Rows)
                    {

                        ob.EMBR_STRIKE = (new
                        {
                            PLAN_START_DT = (dr5["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr5["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                            PLAN_END_DT = (dr5["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr5["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_START_DT = (dr5["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr5["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_END_DT = (dr5["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr5["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                        });
                    }



                    var ds6 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 12 },// AOP Strike Off
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr6 in ds6.Tables[0].Rows)
                    {
                        ob.AOP_STRIKE = (new
                        {
                            PLAN_START_DT = (dr6["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr6["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                            PLAN_END_DT = (dr6["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr6["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_START_DT = (dr6["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr6["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_END_DT = (dr6["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr6["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                        });
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


        public List<MC_ORDER_HModel> BuyerStyleOrderList(int pageNumber, int pageSize, long? pMC_BYR_ACC_ID, DateTime? pFIRSTDATE, DateTime? pLASTDATE, string pMC_ORDER_H_ID_LST, int? pRF_FAB_PROD_CAT_ID)
        {
            string sp = "PKG_FAB_PROD_ORDER.MC_FAB_PROD_ORD_H_SELECT";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                    new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                    
                    new CommandParameter() {ParameterName = "pORDER_NO_LST", Value =pMC_ORDER_H_ID_LST},

                    new CommandParameter() {ParameterName = "pOption", Value = 3008},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    MC_ORDER_HModel ob = new MC_ORDER_HModel();

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    //ob.GARM_DEPT_NAME = (dr["GARM_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_DEPT_NAME"]);
                    //ob.PRINT_EMB_AOP_YD = (dr["PRINT_EMB_AOP_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRINT_EMB_AOP_YD"]);
                    //ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    //ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.IS_STYL_ITM_OPR_MAP = (dr["IS_STYL_ITM_OPR_MAP"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_STYL_ITM_OPR_MAP"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);
                    ob.FIB_COMP_CODE_FULL = (dr["FIB_COMP_CODE_FULL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE_FULL"]);

                    //ob.STYLE_ORDER_NO = (dr["STYLE_ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_ORDER_NO"]);

                    if (dr["PROD_ORD_DT"] != DBNull.Value)
                        ob.PROD_ORD_DT = (dr["PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_ORD_DT"]);

                    //if (dr["ORDER_DT"] != DBNull.Value)
                    //    ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);

                    //if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                    //    ob.ORD_DOC_RCV_DT = (dr["ORD_DOC_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    //if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                    //    ob.ART_WRK_RCV_DT = (dr["ART_WRK_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);


                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);


                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_STYLE_H_ID"]);


                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LEAD_TIME"]);


                    ob.images = new List<dynamic>();

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3007 },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr1["STYL_KEY_IMG"] != DBNull.Value)
                        {
                            ob.images.Add(new
                            {
                                STYL_KEY_IMG = (dr1["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STYL_KEY_IMG"])
                            });
                        }

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



        public List<MC_ORDER_HModel> BuyerStyleOrderFollowupListForPln(
                 int pageNumber,
                 int pageSize,
                 long? pMC_BYR_ACC_ID,
                 DateTime? pFIRSTDATE,
                 DateTime? pLASTDATE,
                 string pMC_ORDER_H_ID_LST,
                 Int64? pINV_ITEM_CAT_ID_P,
                 Int64? pINV_ITEM_CAT_ID,
                 Int64? pLK_ORD_TYPE_ID,
                 int? pOption = null,
                 int? pLK_GARM_TYPE_ID = null
              )
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.get_list_for_planning";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                    new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID_P", Value = pINV_ITEM_CAT_ID_P},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = pLK_ORD_TYPE_ID},
                    new CommandParameter() {ParameterName = "pORDER_NO_LST", Value =pMC_ORDER_H_ID_LST},
                    new CommandParameter() {ParameterName = "pOption", Value = pOption??3009},
                    new CommandParameter() {ParameterName = "pLK_GARM_TYPE_ID", Value = pLK_GARM_TYPE_ID},

                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    MC_ORDER_HModel ob = new MC_ORDER_HModel();

                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);

                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);

                    ob.FIB_COMP_CODE_FULL = (dr["FIB_COMP_CODE_FULL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE_FULL"]);

                    ob.MC_ORDER_ITEM_PLN_LST = (dr["MC_ORDER_ITEM_PLN_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_ITEM_PLN_LST"]);


                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);


                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                  
                    ob.IS_IE_UPD = (dr["IS_IE_UPD"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_IE_UPD"]);

                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);

                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    ob.IS_GMT_WASH = (dr["IS_GMT_WASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GMT_WASH"]);

                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LEAD_TIME"]);
                    ob.images = new List<dynamic>();

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3007 },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr1["STYL_KEY_IMG"] != DBNull.Value)
                        {
                            ob.images.Add(new
                            {
                                STYL_KEY_IMG = (dr1["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STYL_KEY_IMG"])
                            });
                        }

                    }

                    ob.TODO_CNT = (dr["TODO_CNT"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TODO_CNT"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        public List<MC_ORDER_HModel> BuyerAccessoriesDashboard(int pageNumber, int pageSize, long? pMC_BYR_ACC_ID, DateTime? pFIRSTDATE, DateTime? pLASTDATE, string pMC_ORDER_H_ID_LST, int? pRF_FAB_PROD_CAT_ID, string pSTYLE_ORDER_NO = null)
        {
            string sp = "PKG_FAB_PROD_ORDER.MC_FAB_PROD_ORD_H_SELECT";
            try
            {
                var obList = new List<MC_ORDER_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                    new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                    
                    new CommandParameter() {ParameterName = "pORDER_NO_LST", Value =pMC_ORDER_H_ID_LST},
                    new CommandParameter() {ParameterName = "pSTYLE_ORDER_NO", Value =pSTYLE_ORDER_NO},

                    new CommandParameter() {ParameterName = "pOption", Value = 3016},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_HModel ob = new MC_ORDER_HModel();
                    
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.GARM_DEPT_NAME = (dr["GARM_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_DEPT_NAME"]);
                    ob.PRINT_EMB_AOP_YD = (dr["PRINT_EMB_AOP_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRINT_EMB_AOP_YD"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.FIB_COMP_CODE = (dr["FIB_COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE"]);

                    ob.FIB_COMP_CODE_FULL = (dr["FIB_COMP_CODE_FULL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_CODE_FULL"]);

                    ob.STYLE_ORDER_NO = (dr["STYLE_ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_ORDER_NO"]);

                    if (dr["PROD_ORD_DT"] != DBNull.Value)
                        ob.PROD_ORD_DT = (dr["PROD_ORD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_ORD_DT"]);
                    
                    if (dr["ORD_CONF_DT"] != DBNull.Value)
                        ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                        ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    if (dr["ACT_SHIP_DATE"] != DBNull.Value)
                        ob.ACT_SHIP_DATE = Convert.ToDateTime(dr["ACT_SHIP_DATE"]);


                    if (dr["ORD_DOC_RCV_DT"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT =  Convert.ToDateTime(dr["ORD_DOC_RCV_DT"]);

                    if (dr["ART_WRK_RCV_DT"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT = Convert.ToDateTime(dr["ART_WRK_RCV_DT"]);

                    if (dr["ORD_DOC_RCV_DT_P"] != DBNull.Value)
                        ob.ORD_DOC_RCV_DT_P = Convert.ToDateTime(dr["ORD_DOC_RCV_DT_P"]);

                    if (dr["ART_WRK_RCV_DT_P"] != DBNull.Value)
                        ob.ART_WRK_RCV_DT_P = Convert.ToDateTime(dr["ART_WRK_RCV_DT_P"]);


                    if (dr["TRIM_BOOKING_DATE"] != DBNull.Value)
                        ob.TRIM_BOOKING_DATE = Convert.ToDateTime(dr["TRIM_BOOKING_DATE"]);

                    if (dr["TRIM_INHOUSE_DATE"] != DBNull.Value)
                        ob.TRIM_INHOUSE_DATE = Convert.ToDateTime(dr["TRIM_INHOUSE_DATE"]);

                    if (dr["COL_STD_RECV_DATE"] != DBNull.Value)
                        ob.COL_STD_RECV_DATE = Convert.ToDateTime(dr["COL_STD_RECV_DATE"]);

                    if (dr["LD_SEND_DATE"] != DBNull.Value)
                        ob.LD_SEND_DATE = Convert.ToDateTime(dr["LD_SEND_DATE"]);

                    if (dr["LD_APPROVE_DATE"] != DBNull.Value)
                        ob.LD_APPROVE_DATE = Convert.ToDateTime(dr["LD_APPROVE_DATE"]);

                    if (dr["YRN_BK_DATE"] != DBNull.Value)
                        ob.YRN_BK_DATE = Convert.ToDateTime(dr["YRN_BK_DATE"]);

                    if (dr["YRN_INH_DATE"] != DBNull.Value)
                        ob.YRN_INH_DATE = Convert.ToDateTime(dr["YRN_INH_DATE"]);



                    if (dr["TRIM_BOOKING_DATE_P"] != DBNull.Value)
                        ob.TRIM_BOOKING_DATE_P = Convert.ToDateTime(dr["TRIM_BOOKING_DATE_P"]);

                    if (dr["TRIM_INHOUSE_DATE_P"] != DBNull.Value)
                        ob.TRIM_INHOUSE_DATE_P = Convert.ToDateTime(dr["TRIM_INHOUSE_DATE_P"]);

                    if (dr["COL_STD_RECV_DATE_P"] != DBNull.Value)
                        ob.COL_STD_RECV_DATE_P = Convert.ToDateTime(dr["COL_STD_RECV_DATE_P"]);

                    if (dr["LD_SEND_DATE_P"] != DBNull.Value)
                        ob.LD_SEND_DATE_P = Convert.ToDateTime(dr["LD_SEND_DATE_P"]);

                    if (dr["LD_APPROVE_DATE_P"] != DBNull.Value)
                        ob.LD_APPROVE_DATE_P = Convert.ToDateTime(dr["LD_APPROVE_DATE_P"]);

                    if (dr["YRN_BK_DATE_P"] != DBNull.Value)
                        ob.YRN_BK_DATE_P = Convert.ToDateTime(dr["YRN_BK_DATE_P"]);

                    if (dr["YRN_INH_DATE_P"] != DBNull.Value)
                        ob.YRN_INH_DATE_P = Convert.ToDateTime(dr["YRN_INH_DATE_P"]);


                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.TOT_ORD_QTY_COL = (dr["TOT_ORD_QTY_COL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY_COL"]);


                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);

                    ob.NET_FFAB_QTY = (dr["NET_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_FFAB_QTY"]);
                    ob.KNIT_QC_PASS_QTY = (dr["KNIT_QC_PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["KNIT_QC_PASS_QTY"]);


                    ob.DYE_QC_PASS_QTY = (dr["DYE_QC_PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DYE_QC_PASS_QTY"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.STYLE_ORD_SPAN = (dr["STYLE_ORD_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STYLE_ORD_SPAN"]);
                    ob.STYLE_ORD_SL = (dr["STYLE_ORD_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STYLE_ORD_SL"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_STYLE_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    ob.IS_GMT_WASH = (dr["IS_GMT_WASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_GMT_WASH"]);

                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    if (dr["LAST_REV_NO"] != DBNull.Value)
                    {
                        ob.LAST_REV_NO = Convert.ToInt32(dr["LAST_REV_NO"]);
                    }
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.MC_STYL_BGT_H_ID = (dr["MC_STYL_BGT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_BGT_H_ID"]);

                    if (dr["LAST_REV_NO_BGT"] != DBNull.Value)
                    {
                        ob.LAST_REV_NO_BGT = Convert.ToInt32(dr["LAST_REV_NO_BGT"]);
                    }

                    ob.LEAD_TIME = (dr["LEAD_TIME"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LEAD_TIME"]);

                    ob.images = new List<dynamic>();

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                            new CommandParameter() {ParameterName = "pOption", Value = 3007 },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr1["STYL_KEY_IMG"] != DBNull.Value)
                        {
                            ob.images.Add(new
                            {
                                STYL_KEY_IMG = (dr1["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["STYL_KEY_IMG"])
                            });
                        }
                    }


                    var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 23},// Fist Sample
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        ob.FIRST_SAMPLE = (new
                         {
                             PLAN_START_DT = (dr2["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr2["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                             PLAN_END_DT = (dr2["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr2["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                             ACT_START_DT = (dr2["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr2["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                             ACT_END_DT = (dr2["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr2["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                         });
                    }


                    var ds3 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 29 },// Counter Sample
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr3 in ds3.Tables[0].Rows)
                    {
                        ob.COUNTER_SAMPLE = (new
                        {
                            PLAN_START_DT = (dr3["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr3["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                            PLAN_END_DT = (dr3["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr3["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_START_DT = (dr3["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr3["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_END_DT = (dr3["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr3["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                        });

                    }

                    var ds4 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 10 },// Print Strike-off
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr4 in ds4.Tables[0].Rows)
                    {
                        ob.PRINT_STRIKE = (new
                        {
                            PLAN_START_DT = (dr4["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr4["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                            PLAN_END_DT = (dr4["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr4["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_START_DT = (dr4["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr4["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_END_DT = (dr4["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr4["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                        });
                    }

                    var ds5 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 11 },// Embr Strike Off
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr5 in ds5.Tables[0].Rows)
                    {

                        ob.EMBR_STRIKE = (new
                        {
                            PLAN_START_DT = (dr5["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr5["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                            PLAN_END_DT = (dr5["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr5["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_START_DT = (dr5["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr5["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_END_DT = (dr5["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr5["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                        });
                    }



                    var ds6 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = 12 },// AOP Strike Off
                            new CommandParameter() {ParameterName = "pOption", Value = 3003 },
                            new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID },
                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, "pkg_tna.mc_tna_task_select_for_rpt");
                    foreach (DataRow dr6 in ds6.Tables[0].Rows)
                    {
                        ob.AOP_STRIKE = (new
                        {
                            PLAN_START_DT = (dr6["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr6["PLAN_START_DT"]).ToString("dd-MMM-yyyy"),
                            PLAN_END_DT = (dr6["PLAN_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr6["PLAN_END_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_START_DT = (dr6["ACT_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr6["ACT_START_DT"]).ToString("dd-MMM-yyyy"),
                            ACT_END_DT = (dr6["ACT_END_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr6["ACT_END_DT"]).ToString("dd-MMM-yyyy")
                        });
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


        public int NO_OF_GMT_ENTRY { get; set; }

        public string MC_ORDER_ITEM_PLN_LST { get; set; }
    }


    public class MC_WORK_STYLEModel
    {
        public Int64? MC_ORDER_H_ID { get; set; }
        public string ORDER_NO { get; set; }
        public string JOB_TRAC_NO { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public Int64? MC_BUYER_ID { get; set; }
        public string BUYER_NAME_EN { get; set; }

        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? MC_STYLE_H_ID { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string HAS_SET { get; set; }


        public object WorkStyleListByUserBuyerAcc(Int64? pMC_STYLE_H_EXT_ID, Int64? pMC_STYLE_H_ID, string pIS_TNA_FINALIZED = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_WORK_STYLEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID },
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID },
                    new CommandParameter() {ParameterName = "pIS_TNA_FINALIZED", Value = pIS_TNA_FINALIZED },
                    new CommandParameter() {ParameterName = "pOption", Value = 3007},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_WORK_STYLEModel ob = new MC_WORK_STYLEModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);

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
    
    public class MC_ORDER_REVISIONModel
    {
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public string REV_REASON { get; set; }



        public string SaveOrderRevision()
        {
            const string sp = "pkg_mc_order.mc_order_revision_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;


            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
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


    }

    public class MC_ORDER_ENTRY_NOTIFICATION_VM_Model
    {                
        public List<MC_ORDER_HModel> ORDER_ENTRY_LIST { get; set; }
        public List<MC_ORDER_HModel> ORDER_ENTRY_SUMMERY_LIST { get; set; }        
    }


    public class CAP_BKING4OTP_VM_Model
    {
        public string MAIL_ADD_LST { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public DateTime SHIP_DT { get; set; }
        public long ORD_QTY_IN_PCS { get; set; }
        public string OTP_ORDR_BKNG { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string USER_EMAIL { get; set; }
        public string REDIRECT_URL { get; set; }
        

        public List<GMT_WEEKLY_CAP_BKING_RPTModel> PROD_TYP_WISE_DTL { get; set; }
        public List<GMT_WEEKLY_CAP_BKING_RPTModel> CAP_BKING_DTL { get; set; }
        public List<GMT_WEEKLY_CAP_BKING_RPTModel> CAP_BKING_MONTHLY_DTL { get; set; }
    }

    public class GMT_WEEKLY_CAP_BKING_RPTModel 
    {
        public Int64? GMT_PRODUCT_TYP_ID { get; set; }
        public string GMT_PRODUCT_TYP_NAME { get; set; }
        public Int64? RF_FISCAL_YEAR_ID { get; set; }
        public Int64? RF_CAL_MONTH_ID { get; set; }
        public string MONTH_NAME_EN { get; set; }
        public string WK_NAME_EN { get; set; }
        public int MN_WK_NO { get; set; }
        public Int64 PLAN_PROD_PCS { get; set; }
        public Int64 PCS_BOOKED { get; set; }
        public Int64 CARRY_ORVERED_PCS { get; set; }
        public Int64 BK_WITH_CO_PCS { get; set; }
        public string IS_SC_ALLOWED { get; set; }
        public Int64 BAL_PCS { get; set; }
        public Int64 NEW_ORD_PCS { get; set; }
        public decimal OVER_BOOKED_PCT { get; set; }        
        public decimal NEW_OVER_BOOKED_PCT { get; set; }

        public List<MONTH_LIST_CAP_BKING_RPTModel> CAP_MONTH_HDR_LIST { get; set; }
        public List<WEEK_LIST_CAP_BKING_RPTModel> CAP_WEEK_HDR_LIST { get; set; }
        public List<GMT_WEEKLY_CAP_BKING_RPTModel> CAP_WEEK_DATA_LIST { get; set; }
    }

    public class MONTH_LIST_CAP_BKING_RPTModel
    {        
        public Int64? RF_FISCAL_YEAR_ID { get; set; }
        public Int64? RF_CAL_MONTH_ID { get; set; }
        public string MONTH_NAME_EN { get; set; }
        public int MONTH_COL_SPAN { get; set; }
        //public List<WEEK_LIST_CAP_BKING_RPTModel> CAP_WEEK_LIST { get; set; }
    }

    public class WEEK_LIST_CAP_BKING_RPTModel
    {
        public Int64? GMT_PRODUCT_TYP_ID { get; set; }
        public Int64? RF_FISCAL_YEAR_ID { get; set; }
        public Int64? RF_CAL_MONTH_ID { get; set; }
        public string WK_NAME_EN { get; set; }     
        public Int64 PLAN_PROD_PCS { get; set; }
        public Int64 PCS_BOOKED { get; set; }
        public Int64 CARRY_ORVERED_QTY { get; set; }
        public Int64 BK_WITH_CO_PCS { get; set; }
        public Int64 BAL_PCS { get; set; }
        public Int64 NEW_ORD_PCS { get; set; }
        public decimal OVER_BOOKED_PCT { get; set; }
        public decimal NEW_OVER_BOOKED_PCT { get; set; }
    }

    class GmtWklyCapBkingRptComparer : IEqualityComparer<GMT_WEEKLY_CAP_BKING_RPTModel>
    {
        private Func<GMT_WEEKLY_CAP_BKING_RPTModel, object> _funcDistinct;
        public GmtWklyCapBkingRptComparer(Func<GMT_WEEKLY_CAP_BKING_RPTModel, object> funcDistinct)
        {
            this._funcDistinct = funcDistinct;
        }

        public bool Equals(GMT_WEEKLY_CAP_BKING_RPTModel x, GMT_WEEKLY_CAP_BKING_RPTModel y)
        {
            return _funcDistinct(x).Equals(_funcDistinct(y));
        }

        public int GetHashCode(GMT_WEEKLY_CAP_BKING_RPTModel obj)
        {
            return this._funcDistinct(obj).GetHashCode();
        }
    }

}