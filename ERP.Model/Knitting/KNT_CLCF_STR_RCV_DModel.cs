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
    public class KNT_CLCF_STR_RCV_DModel
    {
        public Int64 KNT_CLCF_STR_RCV_D_ID { get; set; }
        public Int64 KNT_CLCF_STR_RCV_H_ID { get; set; }
        public Int64 KNT_CLCF_STYL_ITEM_ID { get; set; }
        public Int64 PRD_QTY { get; set; }
        public Int64 DELV_QTY { get; set; }
        public Int64 DELV_NO_ROLL { get; set; }
        public Decimal DELV_ROLL_WT { get; set; }
        public Int64 WT_MOU_ID { get; set; }
        public Int64 PASS_QTY { get; set; }
        public Int64 REJ_QTY { get; set; }
        public Int64 HOLD_QTY { get; set; }
        public Int64 ADDL_QTY { get; set; }
        public Int64 RCV_QTY { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string GARM_PART_NAME { get; set; }
        public string SIZE_CODE { get; set; }
        public string MESUREMENT { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public Int64 LK_CLCF_MVM_STS_ID { get; set; }
        public long MC_CLCF_ORD_REQ_ID { get; set; }



        public List<KNT_CLCF_STR_RCV_DModel> SelectAll()
        {
            string sp = "Select_KNT_CLCF_STR_RCV_D";
            try
            {
                var obList = new List<KNT_CLCF_STR_RCV_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STR_RCV_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_CLCF_STR_RCV_DModel ob = new KNT_CLCF_STR_RCV_DModel();
                    ob.KNT_CLCF_STR_RCV_D_ID = (dr["KNT_CLCF_STR_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STR_RCV_D_ID"]);
                    ob.KNT_CLCF_STR_RCV_H_ID = (dr["KNT_CLCF_STR_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STR_RCV_H_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
                    ob.DELV_NO_ROLL = (dr["DELV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_NO_ROLL"]);
                    ob.DELV_ROLL_WT = (dr["DELV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_ROLL_WT"]);
                    ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                    ob.PASS_QTY = (dr["PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PASS_QTY"]);
                    ob.REJ_QTY = (dr["REJ_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJ_QTY"]);
                    ob.HOLD_QTY = (dr["HOLD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOLD_QTY"]);
                    ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_QTY"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }








        public string KNT_YRN_LOT_LST { get; set; }
    }
}