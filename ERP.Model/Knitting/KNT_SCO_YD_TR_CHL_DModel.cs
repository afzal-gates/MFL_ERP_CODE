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
    public class KNT_SCO_YD_TR_CHL_DModel
    {
        public Int64 KNT_SCO_YD_TR_CHL_D_ID { get; set; }
        public Int64 KNT_SCO_YD_TR_CHL_H_ID { get; set; }
        public string KNT_SC_PRG_ISS_LST { get; set; }
        public string[] KNT_SC_PRG_ISS_LIST { get; set; }        
        public Int64 KNT_YDP_D_COL_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Decimal PACK_TR_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal TR_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string SP_NOTE { get; set; }

        public string COLOR_NAME_EN { get; set; }
        public string YR_SPEC_DESC { get; set; }
        public string YRN_LOT_NO { get; set; }
        public List<KNT_SC_PRG_ISSModel> ScoIssProgList { get; set; }


        public List<KNT_SC_PRG_ISSModel> GetScoYdTrChlPrgList(Int64 pSCM_SUPPLIER_ID, Int64? pKNT_SCO_YD_TR_CHL_H_ID)
        {
            string sp = "pkg_kny_yd_recv_chaln.knt_sco_yd_tr_chl_h_select";
            try
            {
                var obList = new List<KNT_SC_PRG_ISSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID}, 
                    new CommandParameter() {ParameterName = "pKNT_SCO_YD_TR_CHL_H_ID", Value =pKNT_SCO_YD_TR_CHL_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_PRG_ISSModel ob = new KNT_SC_PRG_ISSModel();
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.PRG_ISS_NO = (dr["PRG_ISS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ISS_NO"]);
                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                    ob.IS_REQS_DONE = (dr["IS_REQS_DONE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_REQS_DONE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_SCO_YD_TR_CHL_DModel> GetScoYdTrChalnDtl(Int64 pKNT_SCO_YD_TR_CHL_H_ID)
        {
            string sp = "pkg_kny_yd_recv_chaln.knt_sco_yd_tr_chl_h_select";
            try
            {
                var obList = new List<KNT_SCO_YD_TR_CHL_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_YD_TR_CHL_H_ID", Value =pKNT_SCO_YD_TR_CHL_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_YD_TR_CHL_DModel ob = new KNT_SCO_YD_TR_CHL_DModel();
                    ob.KNT_SCO_YD_TR_CHL_D_ID = (dr["KNT_SCO_YD_TR_CHL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YD_TR_CHL_D_ID"]);
                    ob.KNT_SCO_YD_TR_CHL_H_ID = (dr["KNT_SCO_YD_TR_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YD_TR_CHL_H_ID"]);
                    ob.KNT_SC_PRG_ISS_LST = (dr["KNT_SC_PRG_ISS_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_SC_PRG_ISS_LST"]);

                    if (ob.KNT_SC_PRG_ISS_LST != null)
                        ob.KNT_SC_PRG_ISS_LIST = ob.KNT_SC_PRG_ISS_LST.Split(',');

                    ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_TR_QTY = (dr["PACK_TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_TR_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.TR_QTY = (dr["TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TR_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.YR_SPEC_DESC = (dr["YR_SPEC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_SPEC_DESC"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);

                    if (Convert.ToString(dr["IS_FINALIZED"]) == "N")
                    {
                        var obScoIssProgList = new KNT_SCO_YD_TR_CHL_DModel().GetScoYdTrChlPrgList(Convert.ToInt64(dr["SCM_SUPPLIER_ID"]), null);
                        ob.ScoIssProgList = (List<KNT_SC_PRG_ISSModel>)obScoIssProgList;
                    }
                    else
                    {
                        var obScoIssProgList = new KNT_SCO_YD_TR_CHL_DModel().GetScoYdTrChlPrgList(Convert.ToInt64(dr["SCM_SUPPLIER_ID"]), ob.KNT_SCO_YD_TR_CHL_H_ID);
                        ob.ScoIssProgList = (List<KNT_SC_PRG_ISSModel>)obScoIssProgList;
                    }
                   

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCO_YD_TR_CHL_DModel Select(long ID)
        {
            string sp = "Select_KNT_SCO_YD_TR_CHL_D";
            try
            {
                var ob = new KNT_SCO_YD_TR_CHL_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_YD_TR_CHL_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_YD_TR_CHL_D_ID = (dr["KNT_SCO_YD_TR_CHL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YD_TR_CHL_D_ID"]);
                    ob.KNT_SCO_YD_TR_CHL_H_ID = (dr["KNT_SCO_YD_TR_CHL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_YD_TR_CHL_H_ID"]);
                    ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_TR_QTY = (dr["PACK_TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_TR_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.TR_QTY = (dr["TR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TR_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
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