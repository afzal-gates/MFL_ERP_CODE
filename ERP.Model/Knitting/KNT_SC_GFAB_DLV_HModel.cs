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
    public class KNT_SC_GFAB_DLV_HModel
    {
        public Int64 KNT_SC_GFAB_DLV_H_ID { get; set; }
        public Int64 KNT_SC_PRG_RCV_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string GT_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public string RECEIVER_NAME { get; set; }
        public string RECEIVER_PHONE_NO { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZE { get; set; }
        

        public string GFAB_DLV_DTL_XML { get; set; }
        public string YRN_RTN_DTL_XML { get; set; }

        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string PRG_RCV_NO { get; set; }
        public DateTime SC_PRG_DT { get; set; }
        public Decimal? DELV_ROLL_WT { get; set; }

        private List<KNT_SC_GFAB_DLV_D1Model> _itmDtl = null;
        public List<KNT_SC_GFAB_DLV_D1Model> itmDtl
        {
            get
            {
                if (_itmDtl == null)
                {
                    _itmDtl = new List<KNT_SC_GFAB_DLV_D1Model>();
                }
                return _itmDtl;
            }
            set
            {
                _itmDtl = value;
            }
        }




        public string BatchSave()
        {
            const string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_GFAB_DLV_H_ID", Value = ob.KNT_SC_GFAB_DLV_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGT_PASS_NO", Value = ob.GT_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},

                     new CommandParameter() {ParameterName = "pRECEIVER_NAME", Value = ob.RECEIVER_NAME},
                     new CommandParameter() {ParameterName = "pRECEIVER_PHONE_NO", Value = ob.RECEIVER_PHONE_NO},

                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZE", Value = ob.IS_FINALIZE},
                     new CommandParameter() {ParameterName = "pGFAB_DLV_DTL_XML", Value = ob.GFAB_DLV_DTL_XML}, 
                     new CommandParameter() {ParameterName = "pYRN_RTN_DTL_XML", Value = ob.YRN_RTN_DTL_XML}, 
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = (ob.IS_FINALIZE=="Y")?1001:1000},
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


        public List<KNT_SC_GFAB_DLV_HModel> SelectAll()
        {
            string sp = "Select_KNT_SC_GFAB_DLV_H";
            try
            {
                var obList = new List<KNT_SC_GFAB_DLV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_GFAB_DLV_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_GFAB_DLV_HModel ob = new KNT_SC_GFAB_DLV_HModel();
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GT_PASS_NO = (dr["GT_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GT_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
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

        public KNT_SC_GFAB_DLV_HModel GetScGreyFabDelvHdr(long pKNT_SC_GFAB_DLV_H_ID)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_h_select";
            try
            {
                var ob = new KNT_SC_GFAB_DLV_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_GFAB_DLV_H_ID", Value =pKNT_SC_GFAB_DLV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GT_PASS_NO = (dr["GT_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GT_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);

                    ob.RECEIVER_NAME = (dr["RECEIVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RECEIVER_NAME"]);
                    ob.RECEIVER_PHONE_NO = (dr["RECEIVER_PHONE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RECEIVER_PHONE_NO"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZE = (dr["IS_FINALIZE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZE"]);
                    
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    //ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetScFabDelvChlnListByPartyID(int pageNumber, int pageSize, Int64 pSCM_SUPPLIER_ID, Int64? pKNT_SC_PRG_RCV_ID)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SC_GFAB_DLV_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},                     
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_GFAB_DLV_HModel ob = new KNT_SC_GFAB_DLV_HModel();
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GT_PASS_NO = (dr["GT_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GT_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZE = (dr["IS_FINALIZE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZE"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);

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


        public object ScGreyFabDelvChallanAuto(string pCHALAN_NO)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SC_GFAB_DLV_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = pCHALAN_NO},                     

                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_GFAB_DLV_HModel ob = new KNT_SC_GFAB_DLV_HModel();
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GT_PASS_NO = (dr["GT_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GT_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    //ob.PRG_RCV_NO = (dr["PRG_RCV_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_RCV_NO"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetChallanList4Bill(int pageNumber, int pageSize, Int64 pSCM_SUPPLIER_ID, string pCHALAN_NO)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SC_GFAB_DLV_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value =pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value = 3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_GFAB_DLV_HModel ob = new KNT_SC_GFAB_DLV_HModel();

                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);

                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);

                    ob.DELV_ROLL_WT = (dr["DELV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_ROLL_WT"]);

                    ob.itmDtl = new KNT_SC_GFAB_DLV_D1Model().GetScFabDelvDtl(ob.KNT_SC_GFAB_DLV_H_ID);

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