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
    public class KNT_SRT_FAB_REQ_D2Model
    {
        public Int64 KNT_SRT_FAB_REQ_D2_ID { get; set; }
        public Int64 KNT_SRT_FAB_REQ_H_ID { get; set; }
        public Int64 REASON_SL { get; set; }
        public Int64 RF_SFAB_RSN_TYPE_ID { get; set; }
        public string REASON_DESC { get; set; }
        public string SFAB_RSN_TYPE_NAME { get; set; }

        
        public string Save()
        {
            const string sp = "pkg_fab_prod_order.knt_srt_fab_prod_ord_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D2_ID", Value = ob.KNT_SRT_FAB_REQ_D2_ID},
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = ob.KNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pREASON_SL", Value = ob.REASON_SL},
                     new CommandParameter() {ParameterName = "pRF_SFAB_RSN_TYPE_ID", Value = ob.RF_SFAB_RSN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
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



        public List<KNT_SRT_FAB_REQ_D2Model> GetSrtFabReasonByID(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                var obList = new List<KNT_SRT_FAB_REQ_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = pKNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SRT_FAB_REQ_D2Model ob = new KNT_SRT_FAB_REQ_D2Model();
                    ob.KNT_SRT_FAB_REQ_D2_ID = (dr["KNT_SRT_FAB_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D2_ID"]);
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.REASON_SL = (dr["REASON_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REASON_SL"]);
                    ob.RF_SFAB_RSN_TYPE_ID = (dr["RF_SFAB_RSN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SFAB_RSN_TYPE_ID"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);

                    ob.SFAB_RSN_TYPE_NAME = (dr["SFAB_RSN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_RSN_TYPE_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SRT_FAB_REQ_D2Model Select(long ID)
        {
            string sp = "Select_KNT_SRT_FAB_REQ_D2";
            try
            {
                var ob = new KNT_SRT_FAB_REQ_D2Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D2_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SRT_FAB_REQ_D2_ID = (dr["KNT_SRT_FAB_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D2_ID"]);
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.REASON_SL = (dr["REASON_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REASON_SL"]);
                    ob.RF_SFAB_RSN_TYPE_ID = (dr["RF_SFAB_RSN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SFAB_RSN_TYPE_ID"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}