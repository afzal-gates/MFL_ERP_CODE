using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Dynamic;

namespace ERP.Model
{
    public class KNT_SCO_CHL_RCV_HModel
    {
        public Int64 KNT_SCO_CHL_RCV_H_ID { get; set; }
        public DateTime RCV_DT { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZED { get; set; }
        public Int64 LK_CLCF_CHL_TYP_ID { get; set; }
        public Int64 CLCF_SRC_PROD_CAT_ID { get; set; }
        public string IS_SUB_CONTR { get; set; }
        public string IS_G_F { get; set; }
        public Int64? LK_CLCF_MVM_STS_ID { get; set; }
        
        public string USER_LEVEL { get; set; }
        public string CLCF_MVM_STS_NAME { get; set; }
        public string CLCF_CHL_TYPE_NAME { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO_LST { get; set; }
        public Decimal? RCV_ROLL_WT { get; set; }
        public string FAB_RCV_DTL_XML { get; set; }
        private List<KNT_SCO_CHL_RCV_DModel> _fabRcvDtl = null;
        public List<KNT_SCO_CHL_RCV_DModel> fabRcvDtl
        {
            get
            {
                if (_fabRcvDtl == null)
                {
                    _fabRcvDtl = new List<KNT_SCO_CHL_RCV_DModel>();
                }
                return _fabRcvDtl;
            }
            set
            {
                _fabRcvDtl = value;
            }
        }
        public string YRN_RCV_DTL_XML { get; set; }
        private List<KNT_SCO_CHL_YRN_RET_DModel> _yrnRcvDtl = null;
        public List<KNT_SCO_CHL_YRN_RET_DModel> yrnRcvDtl
        {
            get
            {
                if (_yrnRcvDtl == null)
                {
                    _yrnRcvDtl = new List<KNT_SCO_CHL_YRN_RET_DModel>();
                }
                return _yrnRcvDtl;
            }
            set
            {
                _yrnRcvDtl = value;
            }
        }
        public string CLCF_STR_RCV_D_XML { get; set; }
        private List<KNT_SCO_CHL_CLCF_RCV_DModel> _clcfRcvDtl = null;
        public List<KNT_SCO_CHL_CLCF_RCV_DModel> clcfRcvDtl
        {
            get
            {
                if (_clcfRcvDtl == null)
                {
                    _clcfRcvDtl = new List<KNT_SCO_CHL_CLCF_RCV_DModel>();
                }
                return _clcfRcvDtl;
            }
            set
            {
                _clcfRcvDtl = value;
            }
        }

        
        public string BatchSave()
        {
            const string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = ob.KNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pFAB_RCV_DTL_XML", Value = ob.FAB_RCV_DTL_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pYRN_RCV_DTL_XML", Value = ob.YRN_RCV_DTL_XML.Replace("null", "")},
                     new CommandParameter() {ParameterName = "pCLCF_STR_RCV_D_XML", Value = ob.CLCF_STR_RCV_D_XML.Replace("null", "")},
                     
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

        public List<KNT_SCO_CHL_RCV_HModel> SelectAll()
        {
            string sp = "Select_KNT_SCO_CHL_RCV_H";
            try
            {
                var obList = new List<KNT_SCO_CHL_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_RCV_HModel ob = new KNT_SCO_CHL_RCV_HModel();
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCO_CHL_RCV_HModel GetRcvChallanHdr(long pKNT_SCO_CHL_RCV_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_h_select";
            try
            {
                var ob = new KNT_SCO_CHL_RCV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value =pKNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    
                    var obFabRcvList = new KNT_SCO_CHL_RCV_DModel().GetScoFabRcvDtl(pKNT_SCO_CHL_RCV_H_ID);
                    ob.fabRcvDtl = obFabRcvList;

                    var obYrnRcvList = new KNT_SCO_CHL_YRN_RET_DModel().GetYrnRcvList(pKNT_SCO_CHL_RCV_H_ID);
                    ob.yrnRcvDtl = obYrnRcvList;
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetRcvChallanList(int pageNumber, int pageSize, Int32? pSCM_SUPPLIER_ID = null, Int32? pSCM_STORE_ID = null, DateTime? pRCV_DT = null,
            string pCHALAN_NO = null, DateTime? pCHALAN_DT = null, string pBYR_ACC_NAME_EN = null, string pSTYLE_NO = null, string pORDER_NO_LST = null,
            string pIS_TRANSFER = null, string pIS_FINALIZED = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SCO_CHL_RCV_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pRCV_DT", Value = pRCV_DT},                     
                    new CommandParameter() {ParameterName = "pCHALAN_NO", Value = pCHALAN_NO},
                    new CommandParameter() {ParameterName = "pCHALAN_DT", Value = pCHALAN_DT},
                    new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value = pBYR_ACC_NAME_EN},
                    new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                    new CommandParameter() {ParameterName = "pORDER_NO_LST", Value = pORDER_NO_LST},
                    new CommandParameter() {ParameterName = "pIS_TRANSFER", Value = pIS_TRANSFER},
                    new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = pIS_FINALIZED},

                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pOption", Value =3003},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_RCV_HModel ob = new KNT_SCO_CHL_RCV_HModel();
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SCM_STORE_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);

                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);


                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetChallanList4Bill(int pageNumber, int pageSize, Int64 pSCM_SUPPLIER_ID, string pCHALAN_NO)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SCO_CHL_RCV_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value =pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_RCV_HModel ob = new KNT_SCO_CHL_RCV_HModel();
                    
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SCM_STORE_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);

                    ob.fabRcvDtl = new KNT_SCO_CHL_RCV_DModel().GetChalnDtlt4Bill(ob.KNT_SCO_CHL_RCV_H_ID);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ExpandoObject> GetOrdCol4StrRcv(Int32? pLK_CLCF_CHL_TYP_ID = null, Int32? pSCM_SUPPLIER_ID = null, string pSTYLE_NO = null, string pWORK_STYLE_NO = null, string pMC_ORDER_NO_LST = null, string pCOLOR_NAME_EN = null, string pSCO_PRG_NO = null, string pRF_FAB_PROD_CAT_ID_LST = null)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_select";
            try
            {
                var obList = new List<ExpandoObject>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_CLCF_CHL_TYP_ID", Value =pLK_CLCF_CHL_TYP_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value =pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pWORK_STYLE_NO", Value =pWORK_STYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_ORDER_NO_LST", Value =pMC_ORDER_NO_LST},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_EN", Value =pCOLOR_NAME_EN},
                     new CommandParameter() {ParameterName = "pSCO_PRG_NO", Value =pSCO_PRG_NO},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID_LST", Value =pRF_FAB_PROD_CAT_ID_LST},

                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic ob = new ExpandoObject();

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    ob.KNT_SCO_CLCF_PRG_H_ID = (dr["KNT_SCO_CLCF_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_H_ID"]);
                    ob.SCO_PRG_NO = (dr["SCO_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCO_PRG_NO"]);

                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.BYR_ACC_GRP_SNAME = (dr["BYR_ACC_GRP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_SNAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExpandoObject> GetCollarCuff4StrRcv(Int32? pLK_CLCF_CHL_TYP_ID, DateTime? pRCV_DT, Int32? pSCM_STORE_ID, Int64? pMC_FAB_PROD_ORD_H_ID, Int64? pMC_STYLE_H_EXT_ID, Int64? pMC_COLOR_ID)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_select";
            try
            {
                var obList = new List<ExpandoObject>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pLK_CLCF_CHL_TYP_ID", Value = pLK_CLCF_CHL_TYP_ID},
                    new CommandParameter() {ParameterName = "pRCV_DT", Value = pRCV_DT},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     
                    new CommandParameter() {ParameterName = "pOption", Value =3000},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic ob = new ExpandoObject();
                    
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);

                    ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);

                    ob.PRD_QTY = (dr["PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_QTY"]);
                    ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
                    ob.DELV_NO_ROLL = (dr["DELV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_NO_ROLL"]);
                    ob.DELV_ROLL_WT = (dr["DELV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.PASS_QTY = (dr["PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PASS_QTY"]);
                    ob.REJ_QTY = (dr["REJ_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJ_QTY"]);
                    ob.HOLD_QTY = (dr["HOLD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOLD_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_QTY"]);

                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);

                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExpandoObject> GetClcf4StrRcvByScoProg(Int32? pLK_CLCF_CHL_TYP_ID, DateTime? pRCV_DT, Int32? pSCM_STORE_ID, Int64? pMC_FAB_PROD_ORD_H_ID, Int64? pMC_STYLE_H_EXT_ID, Int64? pMC_COLOR_ID, Int64? pKNT_SCO_CLCF_PRG_H_ID)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_select";
            try
            {
                var obList = new List<ExpandoObject>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pLK_CLCF_CHL_TYP_ID", Value = pLK_CLCF_CHL_TYP_ID},
                    new CommandParameter() {ParameterName = "pRCV_DT", Value = pRCV_DT}, 
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = pKNT_SCO_CLCF_PRG_H_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dynamic ob = new ExpandoObject();

                    //ob.KNT_CLCF_STR_RCV_D_ID = (dr["KNT_CLCF_STR_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STR_RCV_D_ID"]);
                    //ob.KNT_CLCF_STR_RCV_H_ID = (dr["KNT_CLCF_STR_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STR_RCV_H_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);

                    ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);


                    ob.PRD_QTY = (dr["PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_QTY"]);
                    ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
                    ob.DELV_NO_ROLL = (dr["DELV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_NO_ROLL"]);
                    ob.DELV_ROLL_WT = (dr["DELV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.PASS_QTY = (dr["PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PASS_QTY"]);
                    ob.REJ_QTY = (dr["REJ_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJ_QTY"]);
                    ob.HOLD_QTY = (dr["HOLD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOLD_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_QTY"]);

                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);

                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //==================== Start Collar/cuff internal challan =========================
        public object GetClcfInternalChlnList(Int64 pageNumber, Int64 pageSize, int pCLCF_SRC_PROD_CAT_ID)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SCO_CHL_RCV_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]}, 
                     new CommandParameter() {ParameterName = "pCLCF_SRC_PROD_CAT_ID", Value = pCLCF_SRC_PROD_CAT_ID}, 
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_CHL_RCV_HModel ob = new KNT_SCO_CHL_RCV_HModel();

                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.LK_CLCF_CHL_TYP_ID = (dr["LK_CLCF_CHL_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CLCF_CHL_TYP_ID"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
                    ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.LK_CLCF_MVM_STS_ID = (dr["LK_CLCF_MVM_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CLCF_MVM_STS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.USER_LEVEL = (dr["USER_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_LEVEL"]);
                    ob.CLCF_MVM_STS_NAME = (dr["CLCF_MVM_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CLCF_MVM_STS_NAME"]);
                    ob.CLCF_CHL_TYPE_NAME = (dr["CLCF_CHL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CLCF_CHL_TYPE_NAME"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCO_CHL_RCV_HModel GetClcfInternalChlnHdr(long pKNT_SCO_CHL_RCV_H_ID)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_select";
            try
            {
                var ob = new KNT_SCO_CHL_RCV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = pKNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_CHL_RCV_H_ID = (dr["KNT_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CHL_RCV_H_ID"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.LK_CLCF_CHL_TYP_ID = (dr["LK_CLCF_CHL_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CLCF_CHL_TYP_ID"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);
                    ob.IS_G_F = (dr["IS_G_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_G_F"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.LK_CLCF_MVM_STS_ID = (dr["LK_CLCF_MVM_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CLCF_MVM_STS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.USER_LEVEL = (dr["USER_LEVEL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_LEVEL"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BatchSaveClcfInternalRcv()
        {
            const string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = ob.KNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pLK_CLCF_CHL_TYP_ID", Value = ob.LK_CLCF_CHL_TYP_ID},
                     new CommandParameter() {ParameterName = "pCLCF_SRC_PROD_CAT_ID", Value = ob.CLCF_SRC_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pIS_SUB_CONTR", Value = ob.IS_SUB_CONTR},
                     new CommandParameter() {ParameterName = "pIS_G_F", Value = ob.IS_G_F},
                     //new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pLK_CLCF_MVM_STS_ID", Value = ob.LK_CLCF_MVM_STS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCLCF_STR_RCV_D_XML", Value = ob.CLCF_STR_RCV_D_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                 
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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

        public string SubmitClcfInternalChlnRcv()
        {
            const string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = ob.KNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pLK_CLCF_CHL_TYP_ID", Value = ob.LK_CLCF_CHL_TYP_ID},
                     new CommandParameter() {ParameterName = "pIS_SUB_CONTR", Value = ob.IS_SUB_CONTR},
                     new CommandParameter() {ParameterName = "pIS_G_F", Value = ob.IS_G_F},
                     //new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pLK_CLCF_MVM_STS_ID", Value = ob.LK_CLCF_MVM_STS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCLCF_STR_RCV_D_XML", Value = ob.CLCF_STR_RCV_D_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                 
                     new CommandParameter() {ParameterName = "pOption", Value = 1001},
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

        public string FinalizeClcfInternalChlnRcv()
        {
            const string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = ob.KNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pLK_CLCF_CHL_TYP_ID", Value = ob.LK_CLCF_CHL_TYP_ID},
                     new CommandParameter() {ParameterName = "pIS_SUB_CONTR", Value = ob.IS_SUB_CONTR},
                     new CommandParameter() {ParameterName = "pIS_G_F", Value = ob.IS_G_F},
                     //new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pLK_CLCF_MVM_STS_ID", Value = ob.LK_CLCF_MVM_STS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCLCF_STR_RCV_D_XML", Value = ob.CLCF_STR_RCV_D_XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                 
                     new CommandParameter() {ParameterName = "pOption", Value = 1002},
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
        //==================== End Collar/cuff internal challan =========================



    }



    public class KNT_SC_PRG_ISS_VModel
    {
        public Int64 KNT_SC_PRG_ISS_ID { get; set; }
        public string PRG_ISS_NO { get; set; }
        public DateTime SC_PRG_DT { get; set; }
        public Int64? KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }


        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string ACT_FIN_DIA { get; set; }
        public string FIN_GSM { get; set; }
        public string MC_DIA { get; set; }
        public string YARN_DETAILS { get; set; }

        public Decimal? NET_GFAB_QTY { get; set; }
        public Decimal? RCV_ROLL_WT { get; set; }
        public Decimal? REJ_ROLL_WT { get; set; }
        public Decimal? BAL_QTY { get; set; }
        public Decimal? ADJ_QTY { get; set; }

        //public List<KNT_SCO_CHL_RCV_DModel> fabRcvDtl { get; set; }

        //public Int64? SCM_SUPPLIER_ID { get; set; }
        //public Int64? LK_SC_PRG_STATUS_ID { get; set; }
        //public string SUP_TRD_NAME_EN { get; set; }
        //public string SC_PRG_STATUS_NAME { get; set; }
        //public string IS_REQS_DONE { get; set; }


        public List<KNT_SC_PRG_ISS_VModel> GetScPrgIssList(Int64? pSCM_SUPPLIER_ID, Int64? pKNT_SCO_CHL_RCV_H_ID, string pSEARCH_STR = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_h_select";
            try
            {
                var obList = new List<KNT_SC_PRG_ISS_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = pKNT_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pSEARCH_STR", Value = pSEARCH_STR},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_PRG_ISS_VModel ob = new KNT_SC_PRG_ISS_VModel();
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.PRG_ISS_NO = (dr["PRG_ISS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ISS_NO"]);
                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);

                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.YARN_DETAILS = (dr["YARN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAILS"]);

                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);
                    ob.RCV_ROLL_WT = (dr["RCV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_ROLL_WT"]);
                    ob.BAL_QTY = (dr["BAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_QTY"]);
                    ob.ADJ_QTY = (dr["ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_QTY"]);

                    //var obFabRcvList = new KNT_SCO_CHL_RCV_DModel().GetFabRcvOrdColorWise(pKNT_SCO_CHL_RCV_H_ID, ob.KNT_JOB_CRD_H_ID, ob.MC_COLOR_ID);
                    //ob.fabRcvDtl = (List<KNT_SCO_CHL_RCV_DModel>)obFabRcvList;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object GetScoQcRejFabList4Rtn(Int64? pSCM_SUPPLIER_ID = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_gfab_ret_h_select";
            try
            {
                var obList = new List<KNT_SC_PRG_ISS_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_PRG_ISS_VModel ob = new KNT_SC_PRG_ISS_VModel();
                    
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    //ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.YARN_DETAILS = (dr["YARN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAILS"]);
                    
                    ob.REJ_ROLL_WT = (dr["REJ_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REJ_ROLL_WT"]);
                    
                    //var obFabRcvList = new KNT_SCO_CHL_RCV_DModel().GetFabRcvOrdColorWise(pKNT_SCO_CHL_RCV_H_ID, ob.KNT_JOB_CRD_H_ID, ob.MC_COLOR_ID);
                    //ob.fabRcvDtl = (List<KNT_SCO_CHL_RCV_DModel>)obFabRcvList;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public object GetSciQcRejFabList4Rtn(Int64? pSCM_SUPPLIER_ID = null, string pSEARCH_STR = null)
        {
            string sp = "pkg_knit_subcontract.knt_sci_gfab_ret_h_select";
            try
            {
                var obList = new List<KNT_SC_PRG_ISS_VModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSEARCH_STR", Value = pSEARCH_STR},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_PRG_ISS_VModel ob = new KNT_SC_PRG_ISS_VModel();

                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);

                    //ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.YARN_DETAILS = (dr["YARN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAILS"]);

                    //ob.REJ_ROLL_WT = (dr["REJ_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REJ_ROLL_WT"]);

                    //var obFabRcvList = new KNT_SCO_CHL_RCV_DModel().GetFabRcvOrdColorWise(pKNT_SCO_CHL_RCV_H_ID, ob.KNT_JOB_CRD_H_ID, ob.MC_COLOR_ID);
                    //ob.fabRcvDtl = (List<KNT_SCO_CHL_RCV_DModel>)obFabRcvList;

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

}