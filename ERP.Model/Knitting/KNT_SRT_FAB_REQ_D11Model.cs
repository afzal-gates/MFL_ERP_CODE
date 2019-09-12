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
    public class KNT_SRT_FAB_REQ_D11Model
    {
        public Int64 KNT_SRT_FAB_REQ_D11_ID { get; set; }
        public Int64 KNT_SRT_FAB_REQ_D1_ID { get; set; }
        public Int64 MC_CLCF_ORD_REQ_ID { get; set; }
        public Int64 RQD_PC_QTY { get; set; }

        public Int64 RF_GARM_PART_ID { get; set; }
        public string MESUREMENT { get; set; }
        public string SIZE_CODE_LST { get; set; }


        public string Save()
        {
            const string sp = "SP_KNT_SRT_FAB_REQ_D11";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D11_ID", Value = ob.KNT_SRT_FAB_REQ_D11_ID},
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D1_ID", Value = ob.KNT_SRT_FAB_REQ_D1_ID},
                     new CommandParameter() {ParameterName = "pMC_CLCF_ORD_REQ_ID", Value = ob.MC_CLCF_ORD_REQ_ID},
                     new CommandParameter() {ParameterName = "pRQD_PC_QTY", Value = ob.RQD_PC_QTY},

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



        public List<KNT_SRT_FAB_REQ_D11Model> GetSrtCollarCuffReq(Int64 pMC_STYLE_H_EXT_ID, Int64 pFAB_COLOR_ID, Int64 pKNT_SRT_FAB_REQ_D1_ID)
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                var obList = new List<KNT_SRT_FAB_REQ_D11Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D1_ID", Value = pKNT_SRT_FAB_REQ_D1_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SRT_FAB_REQ_D11Model ob = new KNT_SRT_FAB_REQ_D11Model();
                    ob.KNT_SRT_FAB_REQ_D11_ID = (dr["KNT_SRT_FAB_REQ_D11_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D11_ID"]);
                    ob.KNT_SRT_FAB_REQ_D1_ID = (dr["KNT_SRT_FAB_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D1_ID"]);
                    ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);
                    ob.RQD_PC_QTY = (dr["RQD_PC_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PC_QTY"]);

                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.SIZE_CODE_LST = (dr["SIZE_CODE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE_LST"]);
                   
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SRT_FAB_REQ_D11Model Select(long ID)
        {
            string sp = "Select_KNT_SRT_FAB_REQ_D11";
            try
            {
                var ob = new KNT_SRT_FAB_REQ_D11Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_D11_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SRT_FAB_REQ_D11_ID = (dr["KNT_SRT_FAB_REQ_D11_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D11_ID"]);
                    ob.KNT_SRT_FAB_REQ_D1_ID = (dr["KNT_SRT_FAB_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_D1_ID"]);
                    ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);
                    ob.RQD_PC_QTY = (dr["RQD_PC_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PC_QTY"]);
                    
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