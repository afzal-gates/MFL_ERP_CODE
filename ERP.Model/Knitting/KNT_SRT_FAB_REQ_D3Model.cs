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
    public class KNT_SRT_FAB_REQ_D3Model
    {
        public Int64 KNT_SRT_FAB_REQ_D3_ID { get; set; }
        public Int64 KNT_SRT_FAB_REQ_H_ID { get; set; }
        public Int64 RF_RESP_DEPT_ID { get; set; }
        public Decimal DIST_QTY { get; set; }
        public Decimal PCT_DIST_QTY { get; set; }
        public string RESP_DEPT_NAME { get; set; }


        
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
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D3_ID", Value = ob.KNT_SRT_FAB_REQ_D3_ID},
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = ob.KNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_RESP_DEPT_ID", Value = ob.RF_RESP_DEPT_ID},
                     new CommandParameter() {ParameterName = "pDIST_QTY", Value = ob.DIST_QTY},
                     new CommandParameter() {ParameterName = "pPCT_DIST_QTY", Value = ob.PCT_DIST_QTY},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1003},
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



        public List<KNT_SRT_FAB_REQ_D3Model> GetSrtFabResponsibilityByID(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                var obList = new List<KNT_SRT_FAB_REQ_D3Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = pKNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SRT_FAB_REQ_D3Model ob = new KNT_SRT_FAB_REQ_D3Model();
                    ob.KNT_SRT_FAB_REQ_D3_ID = (dr["KNT_SRT_FAB_REQ_D3_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D3_ID"]);
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.DIST_QTY = (dr["DIST_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DIST_QTY"]);
                    ob.PCT_DIST_QTY = (dr["PCT_DIST_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_DIST_QTY"]);

                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SRT_FAB_REQ_D3Model Select(long ID)
        {
            string sp = "Select_KNT_SRT_FAB_REQ_D3";
            try
            {
                var ob = new KNT_SRT_FAB_REQ_D3Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D3_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SRT_FAB_REQ_D3_ID = (dr["KNT_SRT_FAB_REQ_D3_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D3_ID"]);
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.DIST_QTY = (dr["DIST_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DIST_QTY"]);
                    ob.PCT_DIST_QTY = (dr["PCT_DIST_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_DIST_QTY"]);                    
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