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
    public class KNT_CLCF_STR_RCV_HModel
    {
        public Int64 KNT_CLCF_STR_RCV_H_ID { get; set; }
        public DateTime RCV_DT { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 LK_CLCF_CHL_TYP_ID { get; set; }
        public string IS_SUB_CONTR { get; set; }
        public string IS_G_F { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }
        //public Int64? KNT_SCO_CLCF_PRG_H_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public Int64? LK_CLCF_MVM_STS_ID { get; set; }
        public string REMARKS { get; set; }
        public string USER_LEVEL { get; set; }
        public string CLCF_MVM_STS_NAME { get; set; }
        public string CLCF_CHL_TYPE_NAME { get; set; }
        public string CLCF_STR_RCV_D_XML { get; set; }










        


        

        //public List<KNT_CLCF_STR_RCV_DModel> GetChlnDtl4ClcfStrRcv(Int64 pKNT_SCO_CHL_RCV_H_ID)
        //{
        //    string sp = "pkg_knit_collar_cuff.knt_clcf_str_rcv_select";
        //    try
        //    {
        //        var obList = new List<KNT_CLCF_STR_RCV_DModel>();
        //        OraDatabase db = new OraDatabase();
        //        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {
        //             new CommandParameter() {ParameterName = "pKNT_SCO_CHL_RCV_H_ID", Value = pKNT_SCO_CHL_RCV_H_ID},                                         
        //             new CommandParameter() {ParameterName = "pOption", Value = 3001},
        //             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
        //         }, sp);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            KNT_CLCF_STR_RCV_DModel ob = new KNT_CLCF_STR_RCV_DModel();

        //            ob.KNT_CLCF_STR_RCV_D_ID = (dr["KNT_CLCF_STR_RCV_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STR_RCV_D_ID"]);
        //            ob.KNT_CLCF_STR_RCV_H_ID = (dr["KNT_CLCF_STR_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STR_RCV_H_ID"]);
        //            ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);

        //            ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);

        //            ob.PRD_QTY = (dr["PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_QTY"]);
        //            ob.DELV_QTY = (dr["DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_QTY"]);
        //            ob.DELV_NO_ROLL = (dr["DELV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_NO_ROLL"]);
        //            ob.DELV_ROLL_WT = (dr["DELV_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DELV_ROLL_WT"]);
        //            ob.WT_MOU_ID = (dr["WT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WT_MOU_ID"]);
                                        
        //            ob.PASS_QTY = (dr["PASS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PASS_QTY"]);

        //            if (ob.PASS_QTY < 1)
        //            {
        //                ob.PASS_QTY = ob.DELV_QTY;
        //            }

        //            ob.REJ_QTY = (dr["REJ_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJ_QTY"]);
        //            ob.HOLD_QTY = (dr["HOLD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOLD_QTY"]);
        //            ob.ADDL_QTY = (dr["ADDL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADDL_QTY"]);
        //            ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_QTY"]);

        //            ob.LK_CLCF_MVM_STS_ID = (dr["LK_CLCF_MVM_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CLCF_MVM_STS_ID"]);
        //            ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
        //            ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
        //            ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
        //            ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
        //            ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
        //            ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);

        //            ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

        //            obList.Add(ob);

        //        }
        //        return obList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        

        

        

        


    }
}