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
    public class SCM_PURC_REQ_HModel
    {
        public Int64 SCM_PURC_REQ_H_ID { get; set; }
        public Int64? RF_FISCAL_YEAR_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }
        public string PURC_REQ_NO { get; set; }
        public DateTime PURC_REQ_DT { get; set; }
        public string INV_ITEM_CAT_ID { get; set; }
        public Int64 PURC_REQ_BY { get; set; }
        public Int64 PURC_REQ_TO { get; set; }
        public string REQ_ATTN_MAIL { get; set; }
        public Int64? RF_REQ_SRC_ID { get; set; }
        public Int64? RF_PAY_MTHD_ID { get; set; }
        public Int64? LK_PRIORITY_ID { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public string IS_ORDER_WISE { get; set; }
        public string IS_ALL_BYR { get; set; }
        public string REMARKS { get; set; }
        public Int64? MC_TNA_TASK_STATUS_ID { get; set; }
        public Int64? RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string IS_UPDATE { get; set; }

        public string XML_REQ_D { get; set; }
        public List<SCM_PURC_REQ_D_YRNModel> RequisitionItemList { get; set; }

        public string Save()
        {
            var xml0 = new System.Text.StringBuilder();
            xml0.Append("<trans>");


            const string sp = "pkg_scm_purchase.scm_purc_req_h_insert";
            string jsonStr = "{";
            var ob = this;

            foreach (var obj in ob.RequisitionItemList)
            {
                xml0.AppendLine();
                xml0.Append(" <row ");
                //xml1.Append(" ITEM_INDEX=\"" + itm.ITEM_INDEX + "\"");
                xml0.Append(" TARGET_DT=\"" + string.Format("{0:yyyy-MMM-dd}", obj.TARGET_DT) + "\"");
                xml0.Append(" SCM_PURC_REQ_D_ID=\"" + obj.SCM_PURC_REQ_D_ID + "\"");
                xml0.Append(" SCM_PURC_REQ_D_TMP_ID=\"" + obj.SCM_PURC_REQ_D_TMP_ID + "\"");
                xml0.Append(" SCM_PURC_REQ_H_ID=\"" + obj.SCM_PURC_REQ_H_ID + "\"");
                xml0.Append(" SCM_PURC_REQ_ORD_ID=\"" + obj.SCM_PURC_REQ_ORD_ID + "\"");
                xml0.Append(" INV_ITEM_ID=\"" + obj.INV_ITEM_ID + "\"");
                xml0.Append(" LK_YRN_COLR_GRP_ID=\"" + obj.LK_YRN_COLR_GRP_ID + "\"");
                xml0.Append(" MC_STYLE_H_EXT_ID=\"" + (Convert.ToInt64(obj.MC_STYLE_H_EXT_ID) == 0 ? null : obj.MC_STYLE_H_EXT_ID.ToString()) + "\"");
                //xml0.Append(" MC_ORDER_H_ID=\"" + (Convert.ToInt64(obj.MC_ORDER_H_ID) == 0 ? null : obj.MC_ORDER_H_ID.ToString()) + "\"");
                xml0.Append(" MC_FAB_PROD_ORD_H_ID=\"" + (Convert.ToInt64(obj.MC_FAB_PROD_ORD_H_ID) == 0 ? null : obj.MC_FAB_PROD_ORD_H_ID.ToString()) + "\"");
                xml0.Append(" RF_REQ_SRC_ID=\"" + (Convert.ToInt64(obj.RF_REQ_SRC_ID) == 0 ? null : obj.RF_REQ_SRC_ID.ToString()) + "\"");
                xml0.Append(" LK_PRIORITY_ID=\"" + (Convert.ToInt64(obj.LK_PRIORITY_ID) == 0 ? null : obj.LK_PRIORITY_ID.ToString()) + "\"");

                xml0.Append(" BUFR_ALOC_QTY=\"" + obj.BUFR_ALOC_QTY + "\"");
                xml0.Append(" CAL_RQD_QTY=\"" + obj.CAL_RQD_QTY + "\"");
                xml0.Append(" RQD_QTY=\"" + obj.RQD_QTY + "\"");
                xml0.Append(" CONS_DOZ=\"" + obj.CONS_DOZ + "\"");

                xml0.Append(" MKT_RATE=\"" + obj.MKT_RATE + "\"");
                xml0.Append(" RF_CURRENCY_ID=\"" + obj.RF_CURRENCY_ID + "\"");
                xml0.Append(" ORD_ADV_ALOC_QTY=\"" + obj.ORD_ADV_ALOC_QTY + "\"");
                xml0.Append(" NXT_BUFR_QTY=\"" + obj.NXT_BUFR_QTY + "\"");
                xml0.Append(" QTY_MOU_ID=\"" + obj.QTY_MOU_ID + "\"");

                xml0.Append(" LK_LOC_SRC_TYPE_ID=\"" + obj.LK_LOC_SRC_TYPE_ID + "\"");
                xml0.Append(" RF_BRAND_ID=\"" + obj.RF_BRAND_ID + "\"");
                xml0.Append(" LOT_REF_NO=\"" + obj.LOT_REF_NO + "\"");
                xml0.Append(" SP_NOTE=\"" + obj.SP_NOTE + "\"");

                xml0.Append(" />");
            }

            xml0.AppendLine();
            xml0.AppendLine("</trans>");

            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID==0?1:ob.RF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID==0?null:ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_NO", Value = ob.PURC_REQ_NO},
                     new CommandParameter() {ParameterName = "pPURC_REQ_DT", Value = ob.PURC_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_BY", Value = ob.PURC_REQ_BY},
                     new CommandParameter() {ParameterName = "pPURC_REQ_TO", Value = ob.PURC_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID==0?null:ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID==0?null:ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID==0?null:ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pIS_ORDER_WISE", Value = ob.IS_ORDER_WISE==null?"N":ob.IS_ORDER_WISE},
                     new CommandParameter() {ParameterName = "pIS_ALL_BYR", Value = ob.IS_ALL_BYR},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},

                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID==0?null:ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pLK_PURC_PROD_GRP_ID", Value = ob.LK_PURC_PROD_GRP_ID==0?790:ob.LK_PURC_PROD_GRP_ID},
                     
                     //new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.IS_ORDER_WISE=="Y"? xml0.ToString(): ob.XML_REQ_D},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = xml0.ToString()},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID==0?null:ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},                     
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


        public string AddCart()
        {
            var xml0 = new System.Text.StringBuilder();
            xml0.Append("<trans>");


            const string sp = "pkg_scm_purchase.scm_purc_req_h_addcart";
            string jsonStr = "{";
            var ob = this;

            foreach (var obj in ob.RequisitionItemList)
            {
                xml0.AppendLine();
                xml0.Append(" <row ");
                //xml1.Append(" ITEM_INDEX=\"" + itm.ITEM_INDEX + "\"");
                xml0.Append(" TARGET_DT=\"" + string.Format("{0:yyyy-MMM-dd}", obj.TARGET_DT) + "\"");
                xml0.Append(" SCM_PURC_REQ_D_ID=\"" + obj.SCM_PURC_REQ_D_ID + "\"");
                xml0.Append(" SCM_PURC_REQ_D_TMP_ID=\"" + obj.SCM_PURC_REQ_D_TMP_ID + "\"");
                xml0.Append(" SCM_PURC_REQ_H_ID=\"" + obj.SCM_PURC_REQ_H_ID + "\"");
                xml0.Append(" INV_ITEM_ID=\"" + obj.INV_ITEM_ID + "\"");
                xml0.Append(" LK_YRN_COLR_GRP_ID=\"" + obj.LK_YRN_COLR_GRP_ID + "\"");
                xml0.Append(" MC_STYLE_H_EXT_ID=\"" + (Convert.ToInt64(obj.MC_STYLE_H_EXT_ID) == 0 ? null : obj.MC_STYLE_H_EXT_ID.ToString()) + "\"");
                //xml0.Append(" MC_ORDER_H_ID=\"" + (Convert.ToInt64(obj.MC_ORDER_H_ID) == 0 ? null : obj.MC_ORDER_H_ID.ToString()) + "\"");
                xml0.Append(" MC_FAB_PROD_ORD_H_ID=\"" + (Convert.ToInt64(obj.MC_FAB_PROD_ORD_H_ID) == 0 ? null : obj.MC_FAB_PROD_ORD_H_ID.ToString()) + "\"");
                xml0.Append(" RF_REQ_SRC_ID=\"" + (Convert.ToInt64(obj.RF_REQ_SRC_ID) == 0 ? null : obj.RF_REQ_SRC_ID.ToString()) + "\"");
                xml0.Append(" LK_PRIORITY_ID=\"" + (Convert.ToInt64(obj.LK_PRIORITY_ID) == 0 ? null : obj.LK_PRIORITY_ID.ToString()) + "\"");

                xml0.Append(" BUFR_ALOC_QTY=\"" + obj.BUFR_ALOC_QTY + "\"");
                xml0.Append(" CAL_RQD_QTY=\"" + obj.CAL_RQD_QTY + "\"");
                xml0.Append(" RQD_QTY=\"" + obj.RQD_QTY + "\"");
                xml0.Append(" CONS_DOZ=\"" + obj.CONS_DOZ + "\"");

                xml0.Append(" MKT_RATE=\"" + obj.MKT_RATE + "\"");
                xml0.Append(" RF_CURRENCY_ID=\"" + obj.RF_CURRENCY_ID + "\"");
                xml0.Append(" ORD_ADV_ALOC_QTY=\"" + obj.ORD_ADV_ALOC_QTY + "\"");
                xml0.Append(" NXT_BUFR_QTY=\"" + obj.NXT_BUFR_QTY + "\"");
                xml0.Append(" QTY_MOU_ID=\"" + obj.QTY_MOU_ID + "\"");

                xml0.Append(" LK_LOC_SRC_TYPE_ID=\"" + obj.LK_LOC_SRC_TYPE_ID + "\"");
                xml0.Append(" RF_BRAND_ID=\"" + obj.RF_BRAND_ID + "\"");
                xml0.Append(" LOT_REF_NO=\"" + obj.LOT_REF_NO + "\"");
                xml0.Append(" SP_NOTE=\"" + obj.SP_NOTE + "\"");

                xml0.Append(" />");
            }

            xml0.AppendLine();
            xml0.AppendLine("</trans>");

            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.IS_ORDER_WISE=="Y"? xml0.ToString(): ob.XML_REQ_D},
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


        public string SaveGR()
        {
            var xml0 = new System.Text.StringBuilder();
            xml0.Append("<trans>");


            const string sp = "pkg_scm_purchase.scm_purc_req_h_insert_all";
            string jsonStr = "{";
            var ob = this;

            foreach (var obj in ob.RequisitionItemList)
            {
                xml0.AppendLine();
                xml0.Append(" <row ");
                //xml1.Append(" ITEM_INDEX=\"" + itm.ITEM_INDEX + "\"");
                xml0.Append(" TARGET_DT=\"" + string.Format("{0:yyyy-MMM-dd}", obj.TARGET_DT) + "\"");
                xml0.Append(" SCM_PURC_REQ_D_ID=\"" + obj.SCM_PURC_REQ_D_ID + "\"");
                xml0.Append(" SCM_PURC_REQ_D_TMP_ID=\"" + obj.SCM_PURC_REQ_D_TMP_ID + "\"");
                xml0.Append(" SCM_PURC_REQ_H_ID=\"" + obj.SCM_PURC_REQ_H_ID + "\"");
                xml0.Append(" SCM_PURC_REQ_ORD_ID=\"" + obj.SCM_PURC_REQ_ORD_ID + "\"");
                xml0.Append(" INV_ITEM_ID=\"" + obj.INV_ITEM_ID + "\"");
                xml0.Append(" LK_YRN_COLR_GRP_ID=\"" + obj.LK_YRN_COLR_GRP_ID + "\"");
                xml0.Append(" MC_STYLE_H_EXT_ID=\"" + (Convert.ToInt64(obj.MC_STYLE_H_EXT_ID) == 0 ? null : obj.MC_STYLE_H_EXT_ID.ToString()) + "\"");
                //xml0.Append(" MC_ORDER_H_ID=\"" + (Convert.ToInt64(obj.MC_ORDER_H_ID) == 0 ? null : obj.MC_ORDER_H_ID.ToString()) + "\"");
                xml0.Append(" MC_FAB_PROD_ORD_H_ID=\"" + (Convert.ToInt64(obj.MC_FAB_PROD_ORD_H_ID) == 0 ? null : obj.MC_FAB_PROD_ORD_H_ID.ToString()) + "\"");
                xml0.Append(" RF_REQ_SRC_ID=\"" + (Convert.ToInt64(obj.RF_REQ_SRC_ID) == 0 ? null : obj.RF_REQ_SRC_ID.ToString()) + "\"");
                xml0.Append(" LK_PRIORITY_ID=\"" + (Convert.ToInt64(obj.LK_PRIORITY_ID) == 0 ? null : obj.LK_PRIORITY_ID.ToString()) + "\"");

                xml0.Append(" BUFR_ALOC_QTY=\"" + obj.BUFR_ALOC_QTY + "\"");
                xml0.Append(" CAL_RQD_QTY=\"" + obj.CAL_RQD_QTY + "\"");
                xml0.Append(" RQD_QTY=\"" + obj.RQD_QTY + "\"");
                xml0.Append(" CONS_DOZ=\"" + obj.CONS_DOZ + "\"");

                xml0.Append(" MKT_RATE=\"" + obj.MKT_RATE + "\"");
                xml0.Append(" RF_CURRENCY_ID=\"" + obj.RF_CURRENCY_ID + "\"");
                xml0.Append(" ORD_ADV_ALOC_QTY=\"" + obj.ORD_ADV_ALOC_QTY + "\"");
                xml0.Append(" NXT_BUFR_QTY=\"" + obj.NXT_BUFR_QTY + "\"");
                xml0.Append(" QTY_MOU_ID=\"" + obj.QTY_MOU_ID + "\"");

                xml0.Append(" LK_LOC_SRC_TYPE_ID=\"" + obj.LK_LOC_SRC_TYPE_ID + "\"");
                xml0.Append(" RF_BRAND_ID=\"" + obj.RF_BRAND_ID + "\"");
                xml0.Append(" LOT_REF_NO=\"" + obj.LOT_REF_NO + "\"");
                xml0.Append(" SP_NOTE=\"" + obj.SP_NOTE + "\"");

                xml0.Append(" />");
            }

            xml0.AppendLine();
            xml0.AppendLine("</trans>");

            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID==0?1:ob.RF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID==0?null:ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_NO", Value = ob.PURC_REQ_NO},
                     new CommandParameter() {ParameterName = "pPURC_REQ_DT", Value = ob.PURC_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_BY", Value = ob.PURC_REQ_BY},
                     new CommandParameter() {ParameterName = "pPURC_REQ_TO", Value = ob.PURC_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID==0?null:ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID==0?null:ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pIS_ORDER_WISE", Value = ob.IS_ORDER_WISE},
                     new CommandParameter() {ParameterName = "pIS_ALL_BYR", Value = ob.IS_ALL_BYR},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.IS_ORDER_WISE=="Y"? xml0.ToString(): ob.XML_REQ_D},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},     
                     new CommandParameter() {ParameterName = "pLK_PURC_PROD_GRP_ID", Value = ob.LK_PURC_PROD_GRP_ID==0?790:ob.LK_PURC_PROD_GRP_ID},                
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


        public string Submit()
        {
            const string sp = "pkg_scm_purchase.scm_purc_req_h_acc_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID==0?1:ob.RF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID==0?null:ob.MC_FAB_PROD_ORD_H_ID},
                     //new CommandParameter() {ParameterName = "pMC_FAB_PROD_H_BOM_ID", Value = ob.MC_FAB_PROD_H_BOM_ID==0?null:ob.MC_FAB_PROD_H_BOM_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID==0?null:ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_NO", Value = ob.PURC_REQ_NO},
                     new CommandParameter() {ParameterName = "pPURC_REQ_DT", Value = ob.PURC_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_BY", Value = ob.PURC_REQ_BY},
                     new CommandParameter() {ParameterName = "pPURC_REQ_TO", Value = ob.PURC_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID==0?null:ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID==0?null:ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID==0?null:ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pIS_ORDER_WISE", Value = ob.IS_ORDER_WISE==null?"N":ob.IS_ORDER_WISE},
                     new CommandParameter() {ParameterName = "pIS_ALL_BYR", Value = ob.IS_ALL_BYR},
                     new CommandParameter() {ParameterName = "pIS_UPDATE", Value = ob.IS_UPDATE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},

                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID==0?null:ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pLK_PURC_PROD_GRP_ID", Value = ob.LK_PURC_PROD_GRP_ID==0?790:ob.LK_PURC_PROD_GRP_ID},
                     
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID==0?null:ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID==0?null:ob.RF_ACTN_STATUS_ID},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},               
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]}
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
            const string sp = "SP_SCM_PURC_REQ_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_NO", Value = ob.PURC_REQ_NO},
                     new CommandParameter() {ParameterName = "pPURC_REQ_DT", Value = ob.PURC_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_BY", Value = ob.PURC_REQ_BY},
                     new CommandParameter() {ParameterName = "pPURC_REQ_TO", Value = ob.PURC_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pIS_ORDER_WISE", Value = ob.IS_ORDER_WISE},
                     new CommandParameter() {ParameterName = "pIS_ALL_BYR", Value = ob.IS_ALL_BYR},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pMC_TNA_TASK_STATUS_ID", Value = ob.MC_TNA_TASK_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<SCM_PURC_REQ_HModel> SelectAll(int pageNo, int pageSize, string pPURC_REQ_NO = null, string pPURC_REQ_DT = null, string pCOMP_NAME_EN = null, string pDEPARTMENT_NAME_EN = null,
            string pREQ_STATUS_NAME_EN = null, string pREQ_PRIORITY_NAME_EN = null, string pREQ_SOURCE_NAME_EN = null, string pREMARKS = null, Int64? pRF_REQ_SRC_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_scm_purchase.scm_purc_req_h_select";
            try
            {
                var obList = new List<SCM_PURC_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pPURC_REQ_NO", Value =pPURC_REQ_NO},
                     new CommandParameter() {ParameterName = "pPURC_REQ_DT", Value =pPURC_REQ_DT},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pDEPARTMENT_NAME_EN", Value =pDEPARTMENT_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_STATUS_NAME_EN", Value =pREQ_STATUS_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_PRIORITY_NAME_EN", Value =pREQ_PRIORITY_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_SOURCE_NAME_EN", Value =pREQ_SOURCE_NAME_EN},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value =pRF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value =HttpContext.Current.Session["multiLoginEmpCoreDeptId"]},                     
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pREMARKS", Value =pREMARKS},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption==null?3000:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_PURC_REQ_HModel ob = new SCM_PURC_REQ_HModel();
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    //ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    //ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    //ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_ID"]);
                    ob.PURC_REQ_BY = (dr["PURC_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_BY"]);
                    ob.PURC_REQ_TO = (dr["PURC_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    //ob.RF_REQ_SRC_ID = (dr["RF_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_SRC_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    //ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.IS_ORDER_WISE = (dr["IS_ORDER_WISE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ORDER_WISE"]);
                    ob.IS_ALL_BYR = (dr["IS_ALL_BYR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ALL_BYR"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.LK_PURC_PROD_GRP_ID = (dr["LK_PURC_PROD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURC_PROD_GRP_ID"]);

                    ob.MC_BUYER_LST = (dr["MC_BUYER_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_LST"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    //ob.REQ_PRIORITY_NAME_EN = (dr["REQ_PRIORITY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_PRIORITY_NAME_EN"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);


                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
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

        public List<SCM_PURC_REQ_HModel> SelectPlan()
        {
            string sp = "pkg_scm_purchase.scm_purc_req_h_select";
            try
            {
                var obList = new List<SCM_PURC_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_PURC_REQ_HModel ob = new SCM_PURC_REQ_HModel();
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    //ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_ID"]);
                    ob.PURC_REQ_BY = (dr["PURC_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_BY"]);
                    ob.PURC_REQ_TO = (dr["PURC_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    //ob.RF_REQ_SRC_ID = (dr["RF_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_SRC_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    //ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.IS_ORDER_WISE = (dr["IS_ORDER_WISE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ORDER_WISE"]);
                    ob.IS_ALL_BYR = (dr["IS_ALL_BYR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ALL_BYR"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LK_PURC_PROD_GRP_ID = (dr["LK_PURC_PROD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURC_PROD_GRP_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SCM_PURC_REQ_HModel> SelectForFund()
        {
            string sp = "pkg_scm_purchase.scm_purc_req_h_select";
            try
            {
                var obList = new List<SCM_PURC_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_PURC_REQ_HModel ob = new SCM_PURC_REQ_HModel();
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    ob.ITEM_NAME_LIST = (dr["ITEM_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_LIST"]);

                    ob.PURC_REQ_BY = (dr["PURC_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_BY"]);
                    ob.PURC_REQ_TO = (dr["PURC_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_TO"]);
                    ob.RQD_AMT = (dr["RQD_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_AMT"]);

                    ob.RF_REQ_SRC_ID = (dr["RF_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_SRC_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.LK_PURC_PROD_GRP_ID = (dr["LK_PURC_PROD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURC_PROD_GRP_ID"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    //ob.REQ_SOURCE_NAME_EN = (dr["REQ_SOURCE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_SOURCE_NAME_EN"]);
                    //ob.REQ_PRIORITY_NAME_EN = (dr["REQ_PRIORITY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_PRIORITY_NAME_EN"]);
                    //ob.REQ_STATUS_NAME_EN = (dr["REQ_STATUS_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_STATUS_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SCM_PURC_REQ_HModel> SelectForGPO(Int32? pRF_REQ_SRC_ID = null, Int32? pSCM_PURC_REQ_H_ID = null, Int64? pLK_PURC_PROD_GRP_ID = null)
        {
            string sp = "pkg_scm_purchase.scm_purc_req_h_select";
            try
            {
                var obList = new List<SCM_PURC_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value =pRF_REQ_SRC_ID},                     
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =pSCM_PURC_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pLK_PURC_PROD_GRP_ID", Value =pLK_PURC_PROD_GRP_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_PURC_REQ_HModel ob = new SCM_PURC_REQ_HModel();
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    ob.ITEM_NAME_LIST = (dr["ITEM_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_LIST"]);
                    ob.PURC_REQ_BY = (dr["PURC_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_BY"]);
                    ob.PURC_REQ_TO = (dr["PURC_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_TO"]);
                    ob.RQD_AMT = (dr["RQD_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_AMT"]);

                    ob.RF_REQ_SRC_ID = (dr["RF_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_SRC_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.LK_PURC_PROD_GRP_ID = (dr["LK_PURC_PROD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURC_PROD_GRP_ID"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SCM_PURC_REQ_HModel> SelectForAccBooking(Int64? pMC_STYLE_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_scm_purchase.scm_purc_req_h_select";
            try
            {
                var obList = new List<SCM_PURC_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_PURC_REQ_HModel ob = new SCM_PURC_REQ_HModel();
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    ob.ITEM_NAME_LIST = (dr["ITEM_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_LIST"]);
                    ob.PURC_REQ_BY = (dr["PURC_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_BY"]);
                    ob.PURC_REQ_TO = (dr["PURC_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_TO"]);
                    ob.RQD_AMT = (dr["RQD_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_AMT"]);

                    ob.RF_REQ_SRC_ID = (dr["RF_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_SRC_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.LK_PURC_PROD_GRP_ID = (dr["LK_PURC_PROD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURC_PROD_GRP_ID"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.INV_ITEM_LST = (dr["INV_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_PURC_REQ_HModel Select(Int64? pSCM_PURC_REQ_H_ID = null)
        {
            string sp = "pkg_scm_purchase.scm_purc_req_h_select";
            try
            {
                var ob = new SCM_PURC_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =pSCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    //ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_ID"]);
                    ob.PURC_REQ_BY = (dr["PURC_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_BY"]);
                    ob.PURC_REQ_TO = (dr["PURC_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_SRC_ID = (dr["RF_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_SRC_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.IS_ORDER_WISE = (dr["IS_ORDER_WISE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ORDER_WISE"]);
                    ob.IS_ALL_BYR = (dr["IS_ALL_BYR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ALL_BYR"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MC_TNA_TASK_STATUS_ID = (dr["MC_TNA_TASK_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_TNA_TASK_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.LK_PURC_PROD_GRP_ID = (dr["LK_PURC_PROD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PURC_PROD_GRP_ID"]);

                    ob.MC_BUYER_LST = (dr["MC_BUYER_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_LST"]);
                    ob.ITEM_LIST = (dr["ITEM_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_LIST"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MC_BUYER_LST { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string REQ_PRIORITY_NAME_EN { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public long PERMISSION { get; set; }

        public int TOTAL_REC { get; set; }

        public string ITEM_NAME_LIST { get; set; }

        public Int64? LK_LOC_SRC_TYPE_ID { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public decimal RQD_AMT { get; set; }

        public string ORDER_NO_LST { get; set; }

        public object XML { get; set; }

        public string ITEM_LIST { get; set; }

        public Int64? MC_STYLE_H_EXT_ID { get; set; }

        public long MC_STYLE_H_ID { get; set; }

        public string INV_ITEM_LST { get; set; }

        public Int64? MC_FAB_PROD_H_BOM_ID { get; set; }

        public Int64 LK_PURC_PROD_GRP_ID { get; set; }
    }
}