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
    public class KNT_NDL_STR_RCV_HModel
    {
        public Int64 KNT_NDL_STR_RCV_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 SCM_LOC_PO_H_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string MRR_NO { get; set; }
        public DateTime MRR_DT { get; set; }
        public Int64 ITEM_RCV_BY { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public Int64 LK_LOC_SRC_TYPE_ID { get; set; }
        public Int64 RF_PAY_MTHD_ID { get; set; }
        public string RCV_DOC_NO { get; set; }
        public DateTime RCV_DOC_DT { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
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

        public string Save()
        {
            const string sp = "PKG_KNT_RCV_NDL.KNT_NDL_STR_RCV_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_NDL_STR_RCV_H_ID", Value = ob.KNT_NDL_STR_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_LOC_PO_H_ID", Value = ob.SCM_LOC_PO_H_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pMRR_DT", Value = ob.MRR_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pRCV_DOC_NO", Value = ob.RCV_DOC_NO},
                     new CommandParameter() {ParameterName = "pRCV_DOC_DT", Value = ob.RCV_DOC_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_RCV_D", Value = ob.XML_RCV_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opKNT_NDL_STR_RCV_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "PKG_KNT_RCV_NDL.KNT_NDL_STR_RCV_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_NDL_STR_RCV_H_ID", Value = ob.KNT_NDL_STR_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_LOC_PO_H_ID", Value = ob.SCM_LOC_PO_H_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pMRR_DT", Value = ob.MRR_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pRCV_DOC_NO", Value = ob.RCV_DOC_NO},
                     new CommandParameter() {ParameterName = "pRCV_DOC_DT", Value = ob.RCV_DOC_DT},
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
            const string sp = "PKG_KNT_RCV_NDL.KNT_NDL_STR_RCV_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_NDL_STR_RCV_H_ID", Value = ob.KNT_NDL_STR_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_LOC_PO_H_ID", Value = ob.SCM_LOC_PO_H_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pMRR_DT", Value = ob.MRR_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pRCV_DOC_NO", Value = ob.RCV_DOC_NO},
                     new CommandParameter() {ParameterName = "pRCV_DOC_DT", Value = ob.RCV_DOC_DT},
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

        public List<KNT_NDL_STR_RCV_HModel> SelectAll(int pageNo, int pageSize, string pMRR_NO = null, string pMRR_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pREMARKS = null)
        {
            string sp = "PKG_KNT_RCV_NDL.KNT_NDL_STR_RCV_H_SELECT";
            try
            {
                var obList = new List<KNT_NDL_STR_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_NDL_STR_RCV_H_ID", Value =0},
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
                    KNT_NDL_STR_RCV_HModel ob = new KNT_NDL_STR_RCV_HModel();
                    ob.KNT_NDL_STR_RCV_H_ID = (dr["KNT_NDL_STR_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_NDL_STR_RCV_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_LOC_PO_H_ID = (dr["SCM_LOC_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_LOC_PO_H_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.MRR_NO = (dr["MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRR_NO"]);
                    ob.MRR_DT = (dr["MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MRR_DT"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.RCV_DOC_NO = (dr["RCV_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RCV_DOC_NO"]);
                    ob.RCV_DOC_DT = (dr["RCV_DOC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DOC_DT"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.LOC_SRC_TYPE_NAME = (dr["LOC_SRC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOC_SRC_TYPE_NAME"]);
                    ob.PAY_MTHD_NAME = (dr["PAY_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAY_MTHD_NAME"]);
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

        public KNT_NDL_STR_RCV_HModel Select(Int64? pKNT_NDL_STR_RCV_H_ID = null)
        {
            string sp = "PKG_KNT_RCV_NDL.KNT_NDL_STR_RCV_H_SELECT";
            try
            {
                var ob = new KNT_NDL_STR_RCV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_NDL_STR_RCV_H_ID", Value =pKNT_NDL_STR_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_NDL_STR_RCV_H_ID = (dr["KNT_NDL_STR_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_NDL_STR_RCV_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_LOC_PO_H_ID = (dr["SCM_LOC_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_LOC_PO_H_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.MRR_NO = (dr["MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRR_NO"]);
                    ob.MRR_DT = (dr["MRR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["MRR_DT"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                    ob.RCV_DOC_NO = (dr["RCV_DOC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RCV_DOC_NO"]);
                    ob.RCV_DOC_DT = (dr["RCV_DOC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DOC_DT"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TOTAL_REC { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string STORE_NAME_EN { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public string LOC_SRC_TYPE_NAME { get; set; }

        public string PAY_MTHD_NAME { get; set; }

        public string CURR_NAME_EN { get; set; }
    }
}