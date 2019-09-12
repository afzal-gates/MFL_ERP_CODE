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
    public class DYE_DC_RCV_HModel
    {
        public Int64 DYE_DC_RCV_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 CM_IMP_LC_H_ID { get; set; }
        public Int64 DYE_PO_H_ID { get; set; }
        public Int64 ITEM_RCV_BY { get; set; }
        public string DC_MRR_NO { get; set; }
        public DateTime DC_MRR_DT { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 LK_LOC_SRC_TYPE_ID { get; set; }
        public Int64 RF_PAY_MTHD_ID { get; set; }
        public string REQ_DOC_NO { get; set; }
        public DateTime REQ_DOC_DT { get; set; }
        public string IS_LC_PNDNG { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string INVOICE_NO { get; set; }
        public DateTime? INVOICE_DT { get; set; }
        public string BL_NO { get; set; }
        public DateTime? BL_DT { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Decimal LOC_CONV_RATE { get; set; }
        public string TRK_RCT_NO { get; set; }
        public Int64 LK_RCV_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        
        public string COMP_NAME_EN { get; set; }
        public string REQ_TYPE_NAME { get; set; }
        public string PAY_MTHD_NAME { get; set; }
        public string LOC_SRC_TYPE_NAME { get; set; }
        public string XML_RCV_D { get; set; }
        public int TOTAL_REC { get; set; }

        public string IS_FINALIZED { get; set; }
        public string IMP_LC_NO { get; set; }
        public DateTime? IMP_LC_DT { get; set; }
        

        public string Save()
        {
            const string sp = "pkg_dye_dc_receive.dye_dc_rcv_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ob.DYE_DC_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID==0?1:HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pDYE_PO_H_ID", Value = ob.DYE_PO_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pDC_MRR_NO", Value = ob.DC_MRR_NO},
                     new CommandParameter() {ParameterName = "pDC_MRR_DT", Value = ob.DC_MRR_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pREQ_DOC_NO", Value = ob.REQ_DOC_NO},
                     new CommandParameter() {ParameterName = "pREQ_DOC_DT", Value = ob.REQ_DOC_DT},
                     new CommandParameter() {ParameterName = "pIMP_LC_DT", Value = ob.IMP_LC_DT},
                     new CommandParameter() {ParameterName = "pIS_LC_PNDNG", Value = ob.IS_LC_PNDNG},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pINVOICE_NO", Value = ob.INVOICE_NO},
                     new CommandParameter() {ParameterName = "pINVOICE_DT", Value = ob.INVOICE_DT},
                     new CommandParameter() {ParameterName = "pBL_NO", Value = ob.BL_NO},
                     new CommandParameter() {ParameterName = "pBL_DT", Value = ob.BL_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pTRK_RCT_NO", Value = ob.TRK_RCT_NO},
                     new CommandParameter() {ParameterName = "pLK_RCV_STATUS_ID", Value = ob.LK_RCV_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED!="Y"?"N":ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML_RCV_D", Value = ob.XML_RCV_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_DC_RCV_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "SP_DYE_DC_RCV_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ob.DYE_DC_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_PO_H_ID", Value = ob.DYE_PO_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pDC_MRR_NO", Value = ob.DC_MRR_NO},
                     new CommandParameter() {ParameterName = "pDC_MRR_DT", Value = ob.DC_MRR_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pREQ_DOC_NO", Value = ob.REQ_DOC_NO},
                     new CommandParameter() {ParameterName = "pREQ_DOC_DT", Value = ob.REQ_DOC_DT},
                     new CommandParameter() {ParameterName = "pIS_LC_PNDNG", Value = ob.IS_LC_PNDNG},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pINVOICE_NO", Value = ob.INVOICE_NO},
                     new CommandParameter() {ParameterName = "pINVOICE_DT", Value = ob.INVOICE_DT},
                     new CommandParameter() {ParameterName = "pBL_NO", Value = ob.BL_NO},
                     new CommandParameter() {ParameterName = "pBL_DT", Value = ob.BL_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pTRK_RCT_NO", Value = ob.TRK_RCT_NO},
                     new CommandParameter() {ParameterName = "pLK_RCV_STATUS_ID", Value = ob.LK_RCV_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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
            const string sp = "SP_DYE_DC_RCV_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ob.DYE_DC_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_PO_H_ID", Value = ob.DYE_PO_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pDC_MRR_NO", Value = ob.DC_MRR_NO},
                     new CommandParameter() {ParameterName = "pDC_MRR_DT", Value = ob.DC_MRR_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pREQ_DOC_NO", Value = ob.REQ_DOC_NO},
                     new CommandParameter() {ParameterName = "pREQ_DOC_DT", Value = ob.REQ_DOC_DT},
                     new CommandParameter() {ParameterName = "pIS_LC_PNDNG", Value = ob.IS_LC_PNDNG},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pINVOICE_NO", Value = ob.INVOICE_NO},
                     new CommandParameter() {ParameterName = "pINVOICE_DT", Value = ob.INVOICE_DT},
                     new CommandParameter() {ParameterName = "pBL_NO", Value = ob.BL_NO},
                     new CommandParameter() {ParameterName = "pBL_DT", Value = ob.BL_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pTRK_RCT_NO", Value = ob.TRK_RCT_NO},
                     new CommandParameter() {ParameterName = "pLK_RCV_STATUS_ID", Value = ob.LK_RCV_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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

        public List<DYE_DC_RCV_HModel> SelectAll(int pageNo, int pageSize, Int64? pRF_REQ_TYPE_ID = null, string pDC_MRR_NO = null, string pDC_MRR_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pCHALAN_NO = null, string pIMP_LC_NO = null, string pREMARKS = null)
        {
            string sp = "pkg_dye_dc_receive.dye_dc_rcv_h_select";
            try
            {
                var obList = new List<DYE_DC_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pDC_MRR_NO", Value =pDC_MRR_NO},
                     new CommandParameter() {ParameterName = "pDC_MRR_DT", Value =pDC_MRR_DT},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value =pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pPAY_MTHD_NAME", Value =pPAY_MTHD_NAME},
                     new CommandParameter() {ParameterName = "pLOC_SRC_TYPE_NAME", Value =pLOC_SRC_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value =pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pIMP_LC_NO", Value =pIMP_LC_NO},
                     new CommandParameter() {ParameterName = "pREMARKS", Value =pREMARKS},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DC_RCV_HModel ob = new DYE_DC_RCV_HModel();
                    ob.DYE_DC_RCV_H_ID = (dr["DYE_DC_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.DYE_PO_H_ID = (dr["DYE_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PO_H_ID"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.DC_MRR_NO = (dr["DC_MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DC_MRR_NO"]);
                    ob.DC_MRR_DT = (dr["DC_MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DC_MRR_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.REQ_DOC_NO = (dr["REQ_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_DOC_NO"]);
                    ob.REQ_DOC_DT = (dr["REQ_DOC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REQ_DOC_DT"]);
                    ob.IS_LC_PNDNG = (dr["IS_LC_PNDNG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LC_PNDNG"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                    ob.INVOICE_DT = (dr["INVOICE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INVOICE_DT"]);
                    ob.BL_NO = (dr["BL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BL_NO"]);
                    ob.BL_DT = (dr["BL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BL_DT"]);
                    ob.IMP_LC_DT = (dr["IMP_LC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["IMP_LC_DT"]);                    
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.TRK_RCT_NO = (dr["TRK_RCT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRK_RCT_NO"]);
                    //ob.LK_RCV_STATUS_ID = (dr["LK_RCV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RCV_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.PAY_MTHD_NAME = (dr["PAY_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_MTHD_NAME"]);
                    ob.LOC_SRC_TYPE_NAME = (dr["LOC_SRC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOC_SRC_TYPE_NAME"]);
                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);
                    
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

        public DYE_DC_RCV_HModel Select(Int64? ID)
        {
            string sp = "pkg_dye_dc_receive.dye_dc_rcv_h_select";
            try
            {
                var ob = new DYE_DC_RCV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_DC_RCV_H_ID = (dr["DYE_DC_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.DYE_PO_H_ID = (dr["DYE_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PO_H_ID"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.DC_MRR_NO = (dr["DC_MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DC_MRR_NO"]);
                    ob.DC_MRR_DT = (dr["DC_MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DC_MRR_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.REQ_DOC_NO = (dr["REQ_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_DOC_NO"]);
                    ob.REQ_DOC_DT = (dr["REQ_DOC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REQ_DOC_DT"]);
                    ob.IS_LC_PNDNG = (dr["IS_LC_PNDNG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LC_PNDNG"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                    //ob.IMP_LC_DT = (dr["IMP_LC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["IMP_LC_DT"]);

                    if (dr["IMP_LC_DT"] != DBNull.Value)
                        ob.IMP_LC_DT = Convert.ToDateTime(dr["IMP_LC_DT"]);

                    if (dr["INVOICE_DT"] != DBNull.Value)
                        ob.INVOICE_DT = Convert.ToDateTime(dr["INVOICE_DT"]);

                    ob.BL_NO = (dr["BL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BL_NO"]);

                    if (dr["BL_DT"] != DBNull.Value)
                        ob.BL_DT = Convert.ToDateTime(dr["BL_DT"]);

                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.TRK_RCT_NO = (dr["TRK_RCT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRK_RCT_NO"]);
                    //ob.LK_RCV_STATUS_ID = (dr["LK_RCV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RCV_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.CURRENCY_NANE = (dr["CURRENCY_NANE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURRENCY_NANE"]);
                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string CURRENCY_NANE { get; set; }
    }
}