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
    public class KNT_SC_YRN_RCV_HModel
    {
        public Int64 KNT_SC_YRN_RCV_H_ID { get; set; }
        public Int64 KNT_SC_PRG_RCV_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string REMARKS { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string IS_FINALIZE { get; set; }


        public object GetScYrnRcvHdrListByPrgID(Int64 pKNT_SC_PRG_RCV_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sc_prg_rcv_select";
            try
            {
                var obList = new List<KNT_SC_YRN_RCV_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = pKNT_SC_PRG_RCV_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_YRN_RCV_HModel ob = new KNT_SC_YRN_RCV_HModel();

                    ob.KNT_SC_YRN_RCV_H_ID = (dr["KNT_SC_YRN_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_YRN_RCV_H_ID"]);
                    ob.KNT_SC_PRG_RCV_ID = (dr["KNT_SC_PRG_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_RCV_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZE = (dr["IS_FINALIZE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZE"]);

                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FinalizeSciChallan()
        {
            const string sp = "pkg_knit_subcontract.knt_sci_chl_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_H_ID", Value = ob.KNT_SC_YRN_RCV_H_ID},                                          
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

    }
}