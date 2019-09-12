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
    public class GMT_MRKRModel
    {
        public Int64 GMT_MRKR_ID { get; set; }
        public string MRKR_REF_NO { get; set; }
        public string MRKR_SH_DESC { get; set; }
        public Int64 LK_WAY_TYPE_ID { get; set; }
        public Int64 LK_MRKR_TYPE_ID { get; set; }
        public Int64 MC_BYR_ACC_GRP_ID { get; set; }
        public Int64 MC_STYLE_H_EXT_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64 GMT_COLOR_ID { get; set; }
        public Int64 MC_STYLE_D_FAB_ID { get; set; }
        public Int64 DF_FAB_GRP_ID { get; set; }
        public string BK_FIN_DIA { get; set; }
        public Int64 WRK_FIN_DIA { get; set; }
        public Int64 CUT_FIN_DIA { get; set; }
        public Int64 DIA_MOU_ID { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Decimal? MRKR_LEN { get; set; }
        public Decimal? MRKR_WDT { get; set; }
        public Int64 LW_MOU_ID { get; set; }
        public int? MAX_PLY_QTY { get; set; }
        public Decimal? PCT_MRKR_EFFC { get; set; }
        public Decimal? CONS_PER_DZ { get; set; }
        public string RF_GARM_PART_LST { get; set; }
        public string[] RF_GARM_PART_ID_LIST { get; set; }
        public string IS_PATERN { get; set; }
        public string IS_APROVED { get; set; }
        public string IS_MAIN_SUPPORT { get; set; }

        public string FIN_DIA_N_DIA_TYPE { get; set; }
        public string OTHR_GMT_COLOR_ID_LST { get; set; }
        public string GMT_MRKR_D_XML { get; set; }
        
        public string WAY_TYPE_NAME { get; set; }
        public string MARKER_TYPE_NAME { get; set; }
        public string MRKR_STATUS_NAME { get; set; }
        public Int64? MC_STYLE_H_ID { get; set; }





        public string MrkrBatchSave()
        {
            const string sp = "pkg_cut_marker.gmt_mrkr_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MRKR_ID", Value = ob.GMT_MRKR_ID},
                     new CommandParameter() {ParameterName = "pMRKR_REF_NO", Value = ob.MRKR_REF_NO},
                     new CommandParameter() {ParameterName = "pMRKR_SH_DESC", Value = ob.MRKR_SH_DESC},
                     new CommandParameter() {ParameterName = "pLK_WAY_TYPE_ID", Value = ob.LK_WAY_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_MRKR_TYPE_ID", Value = ob.LK_MRKR_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = ob.MC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value = ob.GMT_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_FAB_ID", Value = ob.MC_STYLE_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pDF_FAB_GRP_ID", Value = ob.DF_FAB_GRP_ID},
                     new CommandParameter() {ParameterName = "pBK_FIN_DIA", Value = ob.BK_FIN_DIA},
                     new CommandParameter() {ParameterName = "pWRK_FIN_DIA", Value = ob.WRK_FIN_DIA},
                     new CommandParameter() {ParameterName = "pCUT_FIN_DIA", Value = ob.CUT_FIN_DIA},
                     new CommandParameter() {ParameterName = "pDIA_MOU_ID", Value = ob.DIA_MOU_ID},
                     new CommandParameter() {ParameterName = "pMAX_PLY_QTY", Value = ob.MAX_PLY_QTY},
                     new CommandParameter() {ParameterName = "pMRKR_LEN", Value = ob.MRKR_LEN},
                     new CommandParameter() {ParameterName = "pMRKR_WDT", Value = ob.MRKR_WDT},
                     new CommandParameter() {ParameterName = "pLW_MOU_ID", Value = ob.LW_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCT_MRKR_EFFC", Value = ob.PCT_MRKR_EFFC},
                     new CommandParameter() {ParameterName = "pCONS_PER_DZ", Value = ob.CONS_PER_DZ},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = ob.RF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pIS_PATERN", Value = ob.IS_PATERN},
                     new CommandParameter() {ParameterName = "pIS_APROVED", Value = ob.IS_APROVED},
                     new CommandParameter() {ParameterName = "pIS_MAIN_SUPPORT", Value = ob.IS_MAIN_SUPPORT},
                     new CommandParameter() {ParameterName = "pGMT_MRKR_D_XML", Value = ob.GMT_MRKR_D_XML},

                     new CommandParameter() {ParameterName = "pOTHR_GMT_COLOR_ID_LST", Value = ob.OTHR_GMT_COLOR_ID_LST},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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

        public List<GMT_MRKRModel> GetMarkerList(Int64 pMC_STYLE_H_ID, Int64? pMC_ORDER_H_ID = null, Int64? pGMT_COLOR_ID = null)
        {
            string sp = "pkg_cut_marker.gmt_mrkr_select";
            try
            {
                var obList = new List<GMT_MRKRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value =pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value =pGMT_COLOR_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_MRKRModel ob = new GMT_MRKRModel();
                    ob.GMT_MRKR_ID = (dr["GMT_MRKR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_ID"]);
                    ob.MRKR_REF_NO = (dr["MRKR_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_REF_NO"]);
                    ob.MRKR_SH_DESC = (dr["MRKR_SH_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_SH_DESC"]);
                    ob.LK_WAY_TYPE_ID = (dr["LK_WAY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_WAY_TYPE_ID"]);
                    ob.LK_MRKR_TYPE_ID = (dr["LK_MRKR_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MRKR_TYPE_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.DF_FAB_GRP_ID = (dr["DF_FAB_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_GRP_ID"]);
                    ob.BK_FIN_DIA = (dr["BK_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BK_FIN_DIA"]);
                    ob.WRK_FIN_DIA = (dr["WRK_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WRK_FIN_DIA"]);
                    ob.CUT_FIN_DIA = (dr["CUT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUT_FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);

                    if (dr["MRKR_LEN"] != DBNull.Value)
                        ob.MRKR_LEN = (dr["MRKR_LEN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MRKR_LEN"]);

                    if (dr["MRKR_WDT"] != DBNull.Value)
                        ob.MRKR_WDT = (dr["MRKR_WDT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MRKR_WDT"]);
                    
                    ob.LW_MOU_ID = (dr["LW_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LW_MOU_ID"]);

                    if (dr["MAX_PLY_QTY"] != DBNull.Value)
                        ob.MAX_PLY_QTY = (dr["MAX_PLY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MAX_PLY_QTY"]);

                    if (dr["CONS_PER_DZ"] != DBNull.Value)
                        ob.CONS_PER_DZ = (dr["CONS_PER_DZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_PER_DZ"]);

                    if (dr["PCT_MRKR_EFFC"] != DBNull.Value)
                        ob.PCT_MRKR_EFFC = (dr["PCT_MRKR_EFFC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_MRKR_EFFC"]);


                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                    if (ob.RF_GARM_PART_LST != "")
                        ob.RF_GARM_PART_ID_LIST = ob.RF_GARM_PART_LST.Split(',');
                    

                    ob.IS_PATERN = (dr["IS_PATERN"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PATERN"]);
                    ob.IS_APROVED = (dr["IS_APROVED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_APROVED"]);

                    ob.IS_MAIN_SUPPORT = (dr["IS_MAIN_SUPPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MAIN_SUPPORT"]);
                    
                    ob.WAY_TYPE_NAME = (dr["WAY_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WAY_TYPE_NAME"]);
                    ob.MARKER_TYPE_NAME = (dr["MARKER_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MARKER_TYPE_NAME"]);
                    ob.MRKR_STATUS_NAME = (dr["MRKR_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_STATUS_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_MRKRModel GetMrkrHdr(long pGMT_MRKR_ID)
        {
            string sp = "pkg_cut_marker.gmt_mrkr_select";
            try
            {
                var ob = new GMT_MRKRModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MRKR_ID", Value =pGMT_MRKR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_MRKR_ID = (dr["GMT_MRKR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_ID"]);
                    ob.MRKR_REF_NO = (dr["MRKR_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_REF_NO"]);
                    ob.MRKR_SH_DESC = (dr["MRKR_SH_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MRKR_SH_DESC"]);
                    ob.LK_WAY_TYPE_ID = (dr["LK_WAY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_WAY_TYPE_ID"]);
                    ob.LK_MRKR_TYPE_ID = (dr["LK_MRKR_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MRKR_TYPE_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.DF_FAB_GRP_ID = (dr["DF_FAB_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_GRP_ID"]);
                    ob.BK_FIN_DIA = (dr["BK_FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BK_FIN_DIA"]);
                    ob.WRK_FIN_DIA = (dr["WRK_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["WRK_FIN_DIA"]);
                    ob.CUT_FIN_DIA = (dr["CUT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUT_FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    
                    if (dr["MRKR_LEN"] != DBNull.Value)
                        ob.MRKR_LEN = (dr["MRKR_LEN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MRKR_LEN"]);

                    if (dr["MRKR_WDT"] != DBNull.Value)
                        ob.MRKR_WDT = (dr["MRKR_WDT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MRKR_WDT"]);

                    ob.LW_MOU_ID = (dr["LW_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LW_MOU_ID"]);

                    if (dr["MAX_PLY_QTY"] != DBNull.Value)
                        ob.MAX_PLY_QTY = (dr["MAX_PLY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MAX_PLY_QTY"]);

                    if (dr["CONS_PER_DZ"] != DBNull.Value)
                        ob.CONS_PER_DZ = (dr["CONS_PER_DZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_PER_DZ"]);

                    if (dr["PCT_MRKR_EFFC"] != DBNull.Value)
                        ob.PCT_MRKR_EFFC = (dr["PCT_MRKR_EFFC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_MRKR_EFFC"]);


                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                    if (ob.RF_GARM_PART_LST != "")
                        ob.RF_GARM_PART_ID_LIST = ob.RF_GARM_PART_LST.Split(',');


                    ob.IS_PATERN = (dr["IS_PATERN"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PATERN"]);
                    ob.IS_APROVED = (dr["IS_APROVED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_APROVED"]);

                    ob.IS_MAIN_SUPPORT = (dr["IS_MAIN_SUPPORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MAIN_SUPPORT"]);
                    
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