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
    public class INV_TRMS_RCV_HModel
    {
        public Int64 INV_TRMS_RCV_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64? CM_IMP_LC_H_ID { get; set; }
        public Int64 RCV_BY { get; set; }
        public string MRR_NO { get; set; }
        public DateTime MRR_DT { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 LK_LOC_SRC_TYPE_ID { get; set; }
        public Int64 RF_PAY_MTHD_ID { get; set; }
        public string REQ_DOC_NO { get; set; }
        public DateTime? REQ_DOC_DT { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime? CHALAN_DT { get; set; }
        public string INVOICE_NO { get; set; }
        public DateTime? INVOICE_DT { get; set; }
        public Int64? RF_CURRENCY_ID { get; set; }
        public Decimal LOC_CONV_RATE { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_RCV_D { get; set; }
        public int TOTAL_REC { get; set; }
        public Int64 MC_STYLE_H_EXT_ID { get; set; }
        public string IS_PO { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        

        public string Save()
        {
            const string sp = "pkg_inv_trims_rcv.inv_trms_rcv_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value = ob.INV_TRMS_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID==0?null:ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_BY", Value = ob.RCV_BY},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pMRR_DT", Value = ob.MRR_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pREQ_DOC_NO", Value = ob.REQ_DOC_NO},
                     new CommandParameter() {ParameterName = "pREQ_DOC_DT", Value = ob.REQ_DOC_DT},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pINVOICE_NO", Value = ob.INVOICE_NO},
                     new CommandParameter() {ParameterName = "pINVOICE_DT", Value = ob.INVOICE_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID==0?null:ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pIS_UPDATE", Value = ob.IS_UPDATE},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID==0?null:ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pIS_BYR_WISE", Value = ob.IS_BYR_WISE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_PO", Value = ob.IS_PO},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pXML_RCV_D", Value = ob.XML_RCV_D},
                     
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
            const string sp = "pkg_inv_trims_rcv.inv_trms_rcv_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value = ob.INV_TRMS_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_BY", Value = ob.RCV_BY},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pMRR_DT", Value = ob.MRR_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pREQ_DOC_NO", Value = ob.REQ_DOC_NO},
                     new CommandParameter() {ParameterName = "pREQ_DOC_DT", Value = ob.REQ_DOC_DT},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pINVOICE_NO", Value = ob.INVOICE_NO},
                     new CommandParameter() {ParameterName = "pINVOICE_DT", Value = ob.INVOICE_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_PO", Value = ob.IS_PO},
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
            const string sp = "pkg_inv_trims_rcv.inv_trms_rcv_h_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value = ob.INV_TRMS_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_BY", Value = ob.RCV_BY},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pMRR_DT", Value = ob.MRR_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pREQ_DOC_NO", Value = ob.REQ_DOC_NO},
                     new CommandParameter() {ParameterName = "pREQ_DOC_DT", Value = ob.REQ_DOC_DT},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pINVOICE_NO", Value = ob.INVOICE_NO},
                     new CommandParameter() {ParameterName = "pINVOICE_DT", Value = ob.INVOICE_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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

        public List<INV_TRMS_RCV_HModel> SelectAll(int pageNo, int pageSize, string pMRR_NO = null, string pMRR_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pREMARKS = null)
        {
            string sp = "pkg_inv_trims_rcv.inv_trms_rcv_h_select";
            try
            {
                var obList = new List<INV_TRMS_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pMRR_NO", Value =pMRR_NO},
                     new CommandParameter() {ParameterName = "pMRR_DT", Value =pMRR_DT},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value =pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pPAY_MTHD_NAME", Value =pPAY_MTHD_NAME},
                     new CommandParameter() {ParameterName = "pLOC_SRC_TYPE_NAME", Value =pLOC_SRC_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value =pREMARKS},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    INV_TRMS_RCV_HModel ob = new INV_TRMS_RCV_HModel();
                    ob.INV_TRMS_RCV_H_ID = (dr["INV_TRMS_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_TRMS_RCV_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.RCV_BY = (dr["RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_BY"]);
                    ob.MRR_NO = (dr["MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRR_NO"]);
                    ob.MRR_DT = (dr["MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MRR_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.REQ_DOC_NO = (dr["REQ_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_DOC_NO"]);
                    ob.REQ_DOC_DT = (dr["REQ_DOC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REQ_DOC_DT"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                    ob.INVOICE_DT = (dr["INVOICE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INVOICE_DT"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.IS_PO = (dr["IS_PO"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PO"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.PAY_MTHD_NAME = (dr["PAY_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_MTHD_NAME"]);
                    ob.LOC_SRC_TYPE_NAME = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOC_SRC_TYPE_NAME"]);

                    ob.IS_BYR_WISE = (dr["IS_BYR_WISE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BYR_WISE"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);
                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public INV_TRMS_RCV_HModel Select(Int64 pINV_TRMS_RCV_H_ID)
        {
            string sp = "pkg_inv_trims_rcv.inv_trms_rcv_h_select";
            try
            {
                var ob = new INV_TRMS_RCV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_TRMS_RCV_H_ID", Value =pINV_TRMS_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.INV_TRMS_RCV_H_ID = (dr["INV_TRMS_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_TRMS_RCV_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.RCV_BY = (dr["RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_BY"]);
                    ob.MRR_NO = (dr["MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRR_NO"]);
                    ob.MRR_DT = (dr["MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MRR_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.REQ_DOC_NO = (dr["REQ_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_DOC_NO"]);

                    if (dr["REQ_DOC_DT"] != DBNull.Value)
                        ob.REQ_DOC_DT = (dr["REQ_DOC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REQ_DOC_DT"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    if (dr["CHALAN_DT"] != DBNull.Value)
                        ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.INVOICE_NO = (dr["INVOICE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INVOICE_NO"]);
                    if (dr["INVOICE_DT"] != DBNull.Value)
                        ob.INVOICE_DT = (dr["INVOICE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INVOICE_DT"]);

                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_PO = (dr["IS_PO"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PO"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_GRP_ID"]);

                    ob.IS_BYR_WISE = (dr["IS_BYR_WISE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BYR_WISE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string IS_BYR_WISE { get; set; }
        public string COMP_NAME_EN { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public string PAY_MTHD_NAME { get; set; }

        public string LOC_SRC_TYPE_NAME { get; set; }

        public Int32? MC_BYR_ACC_ID { get; set; }

        public Int32? MC_BYR_ACC_GRP_ID { get; set; }

        public string IS_UPDATE { get; set; }
    }
}