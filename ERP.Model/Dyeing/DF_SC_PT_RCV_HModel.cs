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
    public class DF_SC_PT_RCV_HModel
    {
        public Int64 DF_SC_PT_RCV_H_ID { get; set; }
        public DateTime RCV_DT { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public string QC_NO { get; set; }
        public string MRR_NO { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public Int64 DF_SC_PT_TR_CHL_LNK_ID { get; set; }
        public Int64 DF_SC_PT_CHL_ISS_H_ID { get; set; }
        public Int64 XFER_SUPPLIER_ID { get; set; }

        public string RPT_PATH_URL { get; set; }
        public HttpPostedFileBase ATT_FILE { get; set; }
        public string XML_RCV_D { get; set; }
        public string XML_RTN_D { get; set; }
        public List<DF_SC_PT_RCV_DModel> fabList { get; set; }


        public string Save()
        {
            const string sp = "PKG_DF_PT.DF_SC_PT_RCV_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value = ob.DF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pQC_NO", Value = ob.QC_NO},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_TR_CHL_LNK_ID", Value = ob.DF_SC_PT_TR_CHL_LNK_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value = ob.DF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pXFER_SUPPLIER_ID", Value = ob.XFER_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_TRANSFER", Value = ob.IS_TRANSFER},
                     new CommandParameter() {ParameterName = "pXML_RCV_D", Value = ob.XML_RCV_D},
                     new CommandParameter() {ParameterName = "pXML_RTN_D", Value = ob.XML_RTN_D},
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
            const string sp = "PKG_DF_PT.DF_SC_PT_RCV_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value = ob.DF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pQC_NO", Value = ob.QC_NO},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML_RCV_D", Value = ob.XML_RCV_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_DF_PT.DF_SC_PT_RCV_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value = ob.DF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pQC_NO", Value = ob.QC_NO},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value = ob.MRR_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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

        public List<DF_SC_PT_RCV_HModel> SelectAll(int pageNo, int pageSize, string pMRR_NO = null, string pRCV_DT = null, string pCHALAN_DT = null, string pCHALAN_NO = null,
            Int64? pHR_COMPANY_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null, Int64? pUSER_ID = null, string pSC_PRG_NO = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_RCV_H_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pMRR_NO", Value =pMRR_NO},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value =pRCV_DT},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value =pCHALAN_DT},
                     new CommandParameter() {ParameterName = "pSC_PRG_NO", Value =pSC_PRG_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value =pCHALAN_NO},
                     //new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value =pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =pUSER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_RCV_HModel ob = new DF_SC_PT_RCV_HModel();
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.QC_NO = (dr["QC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NO"]);
                    ob.MRR_NO = (dr["MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRR_NO"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.SC_PRG_NO_LST = (dr["SC_PRG_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_NO_LST"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.REG_ADD_EN = (dr["REG_ADD_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REG_ADD_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_SC_PT_RCV_HModel> SelectScRcvForBill(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, string pCHALAN_NO = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_RCV_H_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value =pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_RCV_HModel ob = new DF_SC_PT_RCV_HModel();
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.QC_NO = (dr["QC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NO"]);
                    ob.MRR_NO = (dr["MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRR_NO"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.REG_ADD_EN = (dr["REG_ADD_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REG_ADD_EN"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);

                    ob.fabList = new DF_SC_PT_RCV_DModel().SelectAllForBillByID(ob.DF_SC_PT_RCV_H_ID);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SC_PT_RCV_HModel Select(Int64? pDF_SC_PT_RCV_H_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_RCV_H_SELECT";
            try
            {
                var ob = new DF_SC_PT_RCV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value =pDF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.QC_NO = (dr["QC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NO"]);
                    ob.MRR_NO = (dr["MRR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRR_NO"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? "N" : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.IS_TRANSFER = (dr["IS_TRANSFER"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFER"]);

                    ob.DF_SC_PT_TR_CHL_LNK_ID = (dr["DF_SC_PT_TR_CHL_LNK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_TR_CHL_LNK_ID"]);
                    ob.DF_SC_PT_CHL_ISS_H_ID = (dr["DF_SC_PT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_H_ID"]);
                    ob.XFER_SUPPLIER_ID = (dr["XFER_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["XFER_SUPPLIER_ID"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int TOTAL_REC { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public string STORE_NAME_EN { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string REG_ADD_EN { get; set; }

        public long HR_COMPANY_ID { get; set; }

        public string IS_TRANSFER { get; set; }

        public string DF_PROC_TYPE_LST { get; set; }

        public string SC_PRG_NO_LST { get; set; }
    }
}