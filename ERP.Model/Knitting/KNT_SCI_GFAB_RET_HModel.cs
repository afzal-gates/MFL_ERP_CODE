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
    public class KNT_SCI_GFAB_RET_HModel
    {
        public Int64 KNT_SCI_GFAB_RET_H_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string RET_CHALAN_NO { get; set; }
        public DateTime RET_CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public Int64 LK_CHL_RET_STS_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZED { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string FAB_RTN_DTL_XML { get; set; }
        private List<KNT_SCI_GFAB_RET_DModel> _fabRtnDtl = null;
        public List<KNT_SCI_GFAB_RET_DModel> fabRtnDtl
        {
            get
            {
                if (_fabRtnDtl == null)
                {
                    _fabRtnDtl = new List<KNT_SCI_GFAB_RET_DModel>();
                }
                return _fabRtnDtl;
            }
            set
            {
                _fabRtnDtl = value;
            }
        }


        public string BatchSave4SciFabRtn()
        {
            const string sp = "pkg_knit_subcontract.knt_sci_gfab_ret_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCI_GFAB_RET_H_ID", Value = ob.KNT_SCI_GFAB_RET_H_ID},                     
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value = ob.RET_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pFAB_RTN_DTL_XML", Value = ob.FAB_RTN_DTL_XML.Replace("null", "")},
                     //new CommandParameter() {ParameterName = "pYRN_RCV_DTL_XML", Value = ob.YRN_RCV_DTL_XML.Replace("null", "")},
                     
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


        public KNT_SCI_GFAB_RET_HModel GetSciRtnChalnHdr(Int64 pKNT_SCI_GFAB_RET_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sci_gfab_ret_h_select";
            try
            {
                var ob = new KNT_SCI_GFAB_RET_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCI_GFAB_RET_H_ID", Value = pKNT_SCI_GFAB_RET_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCI_GFAB_RET_H_ID = (dr["KNT_SCI_GFAB_RET_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCI_GFAB_RET_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RET_CHALAN_NO = (dr["RET_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RET_CHALAN_NO"]);
                    ob.RET_CHALAN_DT = (dr["RET_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RET_CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.LK_CHL_RET_STS_ID = (dr["LK_CHL_RET_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHL_RET_STS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

                    var obFabRtnList = new KNT_SCI_GFAB_RET_DModel().GetSciFabRtnList(pKNT_SCI_GFAB_RET_H_ID);
                    ob.fabRtnDtl = obFabRtnList;

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_SCI_GFAB_RET_HModel> SelectAll()
        {
            string sp = "Select_KNT_SCI_GFAB_RET_H";
            try
            {
                var obList = new List<KNT_SCI_GFAB_RET_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCI_GFAB_RET_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCI_GFAB_RET_HModel ob = new KNT_SCI_GFAB_RET_HModel();
                    ob.KNT_SCI_GFAB_RET_H_ID = (dr["KNT_SCI_GFAB_RET_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCI_GFAB_RET_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RET_CHALAN_NO = (dr["RET_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RET_CHALAN_NO"]);
                    ob.RET_CHALAN_DT = (dr["RET_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RET_CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.LK_CHL_RET_STS_ID = (dr["LK_CHL_RET_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHL_RET_STS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetSciRtnChallanList(int pageNumber, int pageSize, Int32? pSCM_SUPPLIER_ID = null, Int32? pSCM_STORE_ID = null,
            string pRET_CHALAN_NO = null, DateTime? pRET_CHALAN_DT = null, string pSTYLE_NO = null, string pORDER_NO_LST = null)
        {
            string sp = "pkg_knit_subcontract.knt_sci_gfab_ret_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SCI_GFAB_RET_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                    new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},                                      
                    new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = pRET_CHALAN_NO},
                    new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value = pRET_CHALAN_DT},                    
                    new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                    new CommandParameter() {ParameterName = "pORDER_NO_LST", Value = pORDER_NO_LST},

                    new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                    new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                    new CommandParameter() {ParameterName = "pOption", Value =3003},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCI_GFAB_RET_HModel ob = new KNT_SCI_GFAB_RET_HModel();
                    ob.KNT_SCI_GFAB_RET_H_ID = (dr["KNT_SCI_GFAB_RET_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCI_GFAB_RET_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);                    
                    ob.RET_CHALAN_NO = (dr["RET_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RET_CHALAN_NO"]);
                    ob.RET_CHALAN_DT = (dr["RET_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RET_CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.LK_CHL_RET_STS_ID = (dr["LK_CHL_RET_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHL_RET_STS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
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


    }
}