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
    public class KNT_SCI_YRN_RET_DModel
    {
        public Int64 KNT_SCI_YRN_RET_D_ID { get; set; }
        public Int64 KNT_SC_GFAB_DLV_H_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 SCM_SC_WO_REF_ID { get; set; }
        public Int64 LK_YRN_RET_TYPE_ID { get; set; }
        public Int64? LK_YRN_RET_COZ_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Decimal? PACK_RET_QTY { get; set; }
        public Int64? PACK_MOU_ID { get; set; }
        public Decimal? QTY_PER_PACK { get; set; }
        public Decimal? LOOSE_QTY { get; set; }
        public Decimal RET_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string SP_NOTE { get; set; }
        public string RETURN_TYPE_DESC { get; set; }
        public string SC_WO_REF_NO { get; set; }
        public string YR_SPEC_DESC { get; set; }
        public string PACK_MOU_CODE { get; set; }



        public List<KNT_SCI_YRN_RET_DModel> GetSciYarnRtnList(Int64 pKNT_SC_GFAB_DLV_H_ID)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_h_select";
            try
            {
                var obList = new List<KNT_SCI_YRN_RET_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_GFAB_DLV_H_ID", Value = pKNT_SC_GFAB_DLV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCI_YRN_RET_DModel ob = new KNT_SCI_YRN_RET_DModel();
                    ob.KNT_SCI_YRN_RET_D_ID = (dr["KNT_SCI_YRN_RET_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCI_YRN_RET_D_ID"]);
                    ob.KNT_SC_GFAB_DLV_H_ID = (dr["KNT_SC_GFAB_DLV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_GFAB_DLV_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_SC_WO_REF_ID = (dr["SCM_SC_WO_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SC_WO_REF_ID"]);
                    ob.LK_YRN_RET_TYPE_ID = (dr["LK_YRN_RET_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_RET_TYPE_ID"]);
                    if (dr["LK_YRN_RET_COZ_ID"] != DBNull.Value)
                        ob.LK_YRN_RET_COZ_ID = (dr["LK_YRN_RET_COZ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_RET_COZ_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    if (dr["PACK_RET_QTY"] != DBNull.Value)
                        ob.PACK_RET_QTY = (dr["PACK_RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RET_QTY"]);
                    if (dr["PACK_MOU_ID"] != DBNull.Value)
                        ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    
                    if(dr["QTY_PER_PACK"] != DBNull.Value)
                        ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RET_QTY = (dr["RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RET_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);

                    ob.RETURN_TYPE_DESC = (dr["RETURN_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RETURN_TYPE_DESC"]);
                    ob.SC_WO_REF_NO = (dr["SC_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_WO_REF_NO"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                  
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RemoveYarnReturn()
        {
            const string sp = "pkg_knit_fab_rcv_delv.knt_sc_gfab_dlv_remove";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCI_YRN_RET_D_ID", Value = ob.KNT_SCI_YRN_RET_D_ID},                                                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 4001},
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