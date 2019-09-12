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
    public class MC_PROV_FAB_BK_HModel
    {
        public Int64 MC_PROV_FAB_BK_H_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 MC_STYLE_H_EXT_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public Int64 MC_BYR_ACC_ID { get; set; }
        public string PROV_FAB_BK_NO { get; set; }
        public DateTime PROV_FAB_BK_DT { get; set; }
        public Int64 PROV_FAB_BK_BY { get; set; }
        public Int64 PROV_FAB_BK_TO { get; set; }
        public Int64? REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public string REV_REASON { get; set; }
        public string REMARKS { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string XML_ORD_D { get; set; }
        public string XML_FAB_BK { get; set; }

        public List<MC_PROV_ORD_DETModel> CallOffOrderList { get; set; }


        public string Save()
        {
            const string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            var xml0 = new System.Text.StringBuilder();
            xml0.Append("<trans>");

            foreach (var obj in ob.CallOffOrderList)
            {
                xml0.AppendLine();
                xml0.Append(" <row ");
                //xml1.Append(" ITEM_INDEX=\"" + itm.ITEM_INDEX + "\"");
                xml0.Append(" MC_PROV_ORD_DET_ID=\"" + obj.MC_PROV_ORD_DET_ID + "\"");
                xml0.Append(" MC_ORDER_H_ID=\"" + obj.MC_ORDER_H_ID + "\"");
                //xml0.Append(" CALL_OFF_DT=\"" + string.Format("{0:yyyy-MMM-dd}", obj.CALL_OFF_DT) + "\"");
                //xml0.Append(" SHIP_DT=\"" + string.Format("{0:yyyy-MMM-dd}", obj.SHIP_DT) + "\"");
                //xml0.Append(" DC_WK_NO=\"" + obj.DC_WK_NO + "\"");
                xml0.Append(" MC_COLOR_ID=\"" + obj.MC_COLOR_ID + "\"");
                xml0.Append(" ORDER_QTY=\"" + obj.ORDER_QTY + "\"");
                xml0.Append(" QTY_MOU_ID=\"" + (Convert.ToInt64(obj.QTY_MOU_ID) == 0 ? null : obj.QTY_MOU_ID.ToString()) + "\"");

                xml0.Append(" />");
            }

            xml0.AppendLine();
            xml0.AppendLine("</trans>");



            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_NO", Value = ob.PROV_FAB_BK_NO},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_DT", Value = ob.PROV_FAB_BK_DT},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_BY", Value = ob.PROV_FAB_BK_BY},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_TO", Value = ob.PROV_FAB_BK_TO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO==0?null:ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = ob.ORDER_NO},
                     new CommandParameter() {ParameterName = "pORDER_LOT_NO", Value = ob.ORDER_LOT_NO},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pORDER_DT", Value = ob.ORDER_DT},
                     new CommandParameter() {ParameterName = "pORD_CONF_DT", Value = ob.ORD_CONF_DT},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value = ob.SHIP_DT},
                     new CommandParameter() {ParameterName = "pTOT_ORD_QTY", Value = ob.TOT_ORD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID==0?2:ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = ob.LK_ORD_STATUS_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_EXT_NO", Value = ob.STYLE_EXT_NO},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value = ob.WORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_TYP_ID", Value = ob.LK_STYL_DEV_TYP_ID},
                     
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = ob.LK_STYL_DEV_ID},
                     new CommandParameter() {ParameterName = "pHAS_EXT", Value = ob.HAS_EXT},
                     new CommandParameter() {ParameterName = "pEXT_NO", Value = ob.EXT_NO},
                     new CommandParameter() {ParameterName = "pIS_EXT_AUTO", Value = ob.IS_EXT_AUTO},

                     new CommandParameter() {ParameterName = "pXML_ORD_D", Value = xml0},
                     new CommandParameter() {ParameterName = "pXML_FAB_BK", Value = ob.XML_FAB_BK},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_UPDATED", Value = ob.IS_UPDATED},
                     
                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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


        public string Revise()
        {
            const string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_h_revise";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            var xml0 = new System.Text.StringBuilder();
            xml0.Append("<trans>");

            foreach (var obj in ob.CallOffOrderList)
            {
                xml0.AppendLine();
                xml0.Append(" <row ");
                xml0.Append(" MC_PROV_ORD_DET_ID=\"" + obj.MC_PROV_ORD_DET_ID + "\"");
                xml0.Append(" MC_ORDER_H_ID=\"" + obj.MC_ORDER_H_ID + "\"");
                xml0.Append(" MC_COLOR_ID=\"" + obj.MC_COLOR_ID + "\"");
                xml0.Append(" ORDER_QTY=\"" + obj.ORDER_QTY + "\"");
                xml0.Append(" QTY_MOU_ID=\"" + (Convert.ToInt64(obj.QTY_MOU_ID) == 0 ? null : obj.QTY_MOU_ID.ToString()) + "\"");

                xml0.Append(" />");
            }

            xml0.AppendLine();
            xml0.AppendLine("</trans>");



            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_NO", Value = ob.PROV_FAB_BK_NO},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_DT", Value = ob.PROV_FAB_BK_DT},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_BY", Value = ob.PROV_FAB_BK_BY},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_TO", Value = ob.PROV_FAB_BK_TO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = ob.ORDER_NO},
                     new CommandParameter() {ParameterName = "pORDER_LOT_NO", Value = ob.ORDER_LOT_NO},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pORDER_DT", Value = ob.ORDER_DT},
                     new CommandParameter() {ParameterName = "pORD_CONF_DT", Value = ob.ORD_CONF_DT},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value = ob.SHIP_DT},
                     new CommandParameter() {ParameterName = "pTOT_ORD_QTY", Value = ob.TOT_ORD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID==0?2:ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = ob.LK_ORD_STATUS_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_EXT_NO", Value = ob.STYLE_EXT_NO},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value = ob.WORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_TYP_ID", Value = ob.LK_STYL_DEV_TYP_ID},
                     
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = ob.LK_STYL_DEV_ID},
                     new CommandParameter() {ParameterName = "pHAS_EXT", Value = ob.HAS_EXT},
                     new CommandParameter() {ParameterName = "pEXT_NO", Value = ob.EXT_NO},
                     new CommandParameter() {ParameterName = "pIS_EXT_AUTO", Value = ob.IS_EXT_AUTO},

                     new CommandParameter() {ParameterName = "pXML_ORD_D", Value = xml0},
                     new CommandParameter() {ParameterName = "pXML_FAB_BK", Value = ob.XML_FAB_BK},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_UPDATED", Value = ob.IS_UPDATED},
                     
                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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



        public string Cancel()
        {
            const string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_h_cancel";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            var xml0 = new System.Text.StringBuilder();
            xml0.Append("<trans>");

            foreach (var obj in ob.CallOffOrderList)
            {
                xml0.AppendLine();
                xml0.Append(" <row ");
                xml0.Append(" MC_PROV_ORD_DET_ID=\"" + obj.MC_PROV_ORD_DET_ID + "\"");
                xml0.Append(" MC_ORDER_H_ID=\"" + obj.MC_ORDER_H_ID + "\"");
                xml0.Append(" MC_COLOR_ID=\"" + obj.MC_COLOR_ID + "\"");
                xml0.Append(" ORDER_QTY=\"" + obj.ORDER_QTY + "\"");
                xml0.Append(" QTY_MOU_ID=\"" + (Convert.ToInt64(obj.QTY_MOU_ID) == 0 ? null : obj.QTY_MOU_ID.ToString()) + "\"");

                xml0.Append(" />");
            }

            xml0.AppendLine();
            xml0.AppendLine("</trans>");



            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_NO", Value = ob.PROV_FAB_BK_NO},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_DT", Value = ob.PROV_FAB_BK_DT},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_BY", Value = ob.PROV_FAB_BK_BY},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_TO", Value = ob.PROV_FAB_BK_TO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_TNA_TMPLT_H_ID", Value = ob.MC_TNA_TMPLT_H_ID},
                     
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value = ob.PROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = ob.ORDER_NO},
                     new CommandParameter() {ParameterName = "pORDER_LOT_NO", Value = ob.ORDER_LOT_NO},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value = ob.LK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pORDER_DT", Value = ob.ORDER_DT},
                     new CommandParameter() {ParameterName = "pORD_CONF_DT", Value = ob.ORD_CONF_DT},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value = ob.SHIP_DT},
                     new CommandParameter() {ParameterName = "pTOT_ORD_QTY", Value = ob.TOT_ORD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID==0?2:ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_STATUS_ID", Value = ob.LK_ORD_STATUS_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_EXT_NO", Value = ob.STYLE_EXT_NO},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value = ob.WORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_TYP_ID", Value = ob.LK_STYL_DEV_TYP_ID},
                     
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = ob.LK_STYL_DEV_ID},
                     new CommandParameter() {ParameterName = "pHAS_EXT", Value = ob.HAS_EXT},
                     new CommandParameter() {ParameterName = "pEXT_NO", Value = ob.EXT_NO},
                     new CommandParameter() {ParameterName = "pIS_EXT_AUTO", Value = ob.IS_EXT_AUTO},

                     new CommandParameter() {ParameterName = "pXML_ORD_D", Value = xml0},
                     new CommandParameter() {ParameterName = "pXML_FAB_BK", Value = ob.XML_FAB_BK},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_UPDATED", Value = ob.IS_UPDATED},
                     
                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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


        public string Update()
        {
            const string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_NO", Value = ob.PROV_FAB_BK_NO},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_DT", Value = ob.PROV_FAB_BK_DT},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_BY", Value = ob.PROV_FAB_BK_BY},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_TO", Value = ob.PROV_FAB_BK_TO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
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
            const string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_h_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value = ob.MC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_NO", Value = ob.PROV_FAB_BK_NO},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_DT", Value = ob.PROV_FAB_BK_DT},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_BY", Value = ob.PROV_FAB_BK_BY},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_TO", Value = ob.PROV_FAB_BK_TO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<MC_PROV_FAB_BK_HModel> SelectAll(int? pageNo, int? pageSize, string pORDER_NO = null, string pSTYLE_NO = null, string pPROV_FAB_BK_DT = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_ID = null)
        {
            string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_h_select";
            try
            {
                var obList = new List<MC_PROV_FAB_BK_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value =pORDER_NO},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value =pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pPROV_FAB_BK_DT", Value =pPROV_FAB_BK_DT},
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PROV_FAB_BK_HModel ob = new MC_PROV_FAB_BK_HModel();
                    ob.MC_PROV_FAB_BK_H_ID = (dr["MC_PROV_FAB_BK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PROV_FAB_BK_H_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.PROV_FAB_BK_NO = (dr["PROV_FAB_BK_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROV_FAB_BK_NO"]);
                    ob.PROV_FAB_BK_DT = (dr["PROV_FAB_BK_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROV_FAB_BK_DT"]);
                    ob.PROV_FAB_BK_BY = (dr["PROV_FAB_BK_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROV_FAB_BK_BY"]);
                    ob.PROV_FAB_BK_TO = (dr["PROV_FAB_BK_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROV_FAB_BK_TO"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PERMISSION"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.REVISION_LIST = new MC_PROV_FAB_BK_HModel().SelectRevisionByID(ob.MC_STYLE_H_EXT_ID);

                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_PROV_FAB_BK_HModel> SelectRevisionByID(Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_h_select";
            try
            {
                var obList = new List<MC_PROV_FAB_BK_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},                  
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PROV_FAB_BK_HModel ob = new MC_PROV_FAB_BK_HModel();
                    ob.MC_PROV_FAB_BK_H_ID = (dr["MC_PROV_FAB_BK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PROV_FAB_BK_H_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_PROV_FAB_BK_HModel Select(Int64? pMC_PROV_FAB_BK_H_ID = null)
        {
            string sp = "pkg_mc_fab_booking.mc_prov_fab_bk_h_select";
            try
            {
                var ob = new MC_PROV_FAB_BK_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_PROV_FAB_BK_H_ID", Value =pMC_PROV_FAB_BK_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_PROV_FAB_BK_H_ID = (dr["MC_PROV_FAB_BK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_PROV_FAB_BK_H_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.PROV_FAB_BK_NO = (dr["PROV_FAB_BK_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PROV_FAB_BK_NO"]);
                    ob.PROV_FAB_BK_DT = (dr["PROV_FAB_BK_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROV_FAB_BK_DT"]);
                    ob.PROV_FAB_BK_BY = (dr["PROV_FAB_BK_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROV_FAB_BK_BY"]);
                    ob.PROV_FAB_BK_TO = (dr["PROV_FAB_BK_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROV_FAB_BK_TO"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.MC_TNA_TMPLT_H_ID = (dr["MC_TNA_TMPLT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TMPLT_H_ID"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.LK_ORD_TYPE_ID = (dr["LK_ORD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_TYPE_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ORD_STATUS_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.LK_STYL_DEV_TYP_ID = (dr["LK_STYL_DEV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_TYP_ID"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.ORDER_LOT_NO = (dr["ORDER_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LOT_NO"]);
                    ob.STYLE_EXT_NO = (dr["STYLE_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_EXT_NO"]);
                    ob.JOB_TRAC_NO = (dr["JOB_TRAC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_TRAC_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);
                    ob.EXT_NO = (dr["EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXT_NO"]);
                    ob.IS_EXT_AUTO = (dr["IS_EXT_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EXT_AUTO"]);

                    ob.ORDER_DT = (dr["ORDER_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORDER_DT"]);
                    ob.ORD_CONF_DT = (dr["ORD_CONF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ORD_CONF_DT"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PROV"]);
                    ob.EMAIL_TO_LST = (dr["EMAIL_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMAIL_TO_LST"]);
                    ob.IS_TNA_FINALIZED = (dr["IS_TNA_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TNA_FINALIZED"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int64 PROD_UNIT_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string ORDER_LOT_NO { get; set; }
        public Int64 LK_ORD_TYPE_ID { get; set; }
        public DateTime ORDER_DT { get; set; }
        public DateTime ORD_CONF_DT { get; set; }
        public DateTime SHIP_DT { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Int64 LK_ORD_STATUS_ID { get; set; }
        public string STYLE_EXT_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public Int64 LK_STYL_DEV_TYP_ID { get; set; }
        public Int64 LK_STYL_DEV_ID { get; set; }
        public string HAS_EXT { get; set; }
        public string EXT_NO { get; set; }
        public string IS_EXT_AUTO { get; set; }

        public string JOB_TRAC_NO { get; set; }

        public string IS_FINALIZED { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public string COMP_NAME_EN { get; set; }

        public int TOTAL_REC { get; set; }

        public object IS_UPDATED { get; set; }

        public string IS_PROV { get; set; }

        public string EMAIL_TO_LST { get; set; }

        public int PERMISSION { get; set; }

        public Int64 MC_TNA_TMPLT_H_ID { get; set; }

        public string IS_TNA_FINALIZED { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public string IS_ACTIVE { get; set; }

        public List<MC_PROV_FAB_BK_HModel> REVISION_LIST { get; set; }
    }
}