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
    public class KNT_SC_RPTModel
    {
        public string PRG_ISS_NO { get; set; }
        public DateTime SC_PRG_DT { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string ADDRESS_DEFA { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string ORDER_NO_LST { get; set; }
        public string STYLE_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string STITCH_LEN { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string MC_DIA { get; set; }
        public string FIN_DIA { get; set; }
        public string FIN_DIA_MOU_CODE { get; set; }
        public string FIN_DIA_TYPE { get; set; }
        public string MACHIN_GG { get; set; }
        public string FIN_GSM { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public Int64 RQD_YRN_QTY { get; set; }
        public string PLAN_NOTE { get; set; }
        public Decimal BGT_RATE { get; set; }
        public string COLOR_NAME_EN_LST { get; set; }
        public DateTime START_DT { get; set; }
        public DateTime END_DT { get; set; }
        public string JOB_CRD_NO { get; set; }
        public string REMARKS { get; set; }
        public Decimal PROD_RATE { get; set; }
        public Int64 KNT_PLAN_H_ID { get; set; }

        private List<KNT_YD_PLAN_FDRModel> _feeder = null;
        public List<KNT_YD_PLAN_FDRModel> feeder
        {
            get
            {
                if (_feeder == null)
                {
                    _feeder = new List<KNT_YD_PLAN_FDRModel>();
                }
                return _feeder;
            }
            set
            {
                _feeder = value;
            }
        }

        public List<KNT_SC_RPTModel> Query(Int64 pKNT_SC_PRG_ISS_ID)
        {
            string sp = "pkg_knit_plan.knt_sc_prg_iss_select";
            try
            {
                var obList = new List<KNT_SC_RPTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = pKNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                             KNT_SC_RPTModel ob = new KNT_SC_RPTModel();

                            ob.PRG_ISS_NO = (dr["PRG_ISS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ISS_NO"]);
                            ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                            ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                            ob.ADDRESS_DEFA = (dr["ADDRESS_DEFA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_DEFA"]);
                            ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                            ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                            ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                            ob.STITCH_LEN = (dr["STITCH_LEN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STITCH_LEN"]);
                            ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                            ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                            ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                            ob.FIN_DIA_MOU_CODE = (dr["FIN_DIA_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_MOU_CODE"]);
                            ob.FIN_DIA_TYPE = (dr["FIN_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_TYPE"]);
                            ob.MACHIN_GG = (dr["MACHIN_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MACHIN_GG"]);
                            ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                            ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                            ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_YRN_QTY"]);
                            ob.PLAN_NOTE = (dr["PLAN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_NOTE"]);
                            ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                            ob.COLOR_NAME_EN_LST = (dr["COLOR_NAME_EN_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN_LST"]);
                            ob.START_DT = (dr["START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DT"]);
                            ob.END_DT = (dr["END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DT"]);
                            ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                            ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                            ob.feeder = new KNT_YD_PLAN_FDRModel().FeederArrangementData(ob.KNT_PLAN_H_ID);

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