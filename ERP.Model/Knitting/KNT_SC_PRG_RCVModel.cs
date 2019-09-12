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
    public class KNT_SC_PRG_RCVModel
    {
        public Int64 KNT_SC_PRG_RCV_ID { get; set; }
        public string PRG_RCV_NO { get; set; }
        public DateTime SC_PRG_DT { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 LK_SC_PRG_STATUS_ID { get; set; }
        public string IS_PRG_NO_AUTO { get; set; }

        public string IS_FINALIZE { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string SC_ORDER_NO { get; set; }
        public string SC_STYLE_NO { get; set; }
        
        public string KNT_SC_YRN_RCV_H_XML { get; set; }
        public string KNT_SC_YRN_RCV_D_XML { get; set; }
        public string FAB_PROD_D_XML { get; set; }
        public string FAB_PROD_YRN_XML { get; set; }
        


        public string BatchSave()
        {
            const string sp = "pkg_knit_subcontract.knt_sc_prg_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = ob.KNT_SC_PRG_RCV_ID},                     
                     new CommandParameter() {ParameterName = "pSC_PRG_DT", Value = ob.SC_PRG_DT},
                     new CommandParameter() {ParameterName = "pPRG_RCV_NO", Value = ob.PRG_RCV_NO},
                     new CommandParameter() {ParameterName = "pIS_PRG_NO_AUTO", Value = ob.IS_PRG_NO_AUTO},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pLK_SC_PRG_STATUS_ID", Value = 537}, //537=Draft
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_H_XML", Value = ob.KNT_SC_YRN_RCV_H_XML},
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_D_XML", Value = ob.KNT_SC_YRN_RCV_D_XML},
                     new CommandParameter() {ParameterName = "pFAB_PROD_D_XML", Value = ob.FAB_PROD_D_XML},
                     new CommandParameter() {ParameterName = "pFAB_PROD_YRN_XML", Value = ob.FAB_PROD_YRN_XML},
                     new CommandParameter() {ParameterName = "pIS_FINALIZE", Value = ob.IS_FINALIZE},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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

        public string KntScFabOrdBatchSave()
        {
            const string sp = "pkg_knit_subcontract.knt_sc_prod_ord_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = ob.KNT_SC_PRG_RCV_ID},                     
                     new CommandParameter() {ParameterName = "pSC_PRG_DT", Value = ob.SC_PRG_DT},
                     //new CommandParameter() {ParameterName = "pPRG_RCV_NO", Value = ob.PRG_RCV_NO},
                     //new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     //new CommandParameter() {ParameterName = "pLK_SC_PRG_STATUS_ID", Value = 1},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     //new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_H_XML", Value = ob.KNT_SC_YRN_RCV_H_XML},
                     new CommandParameter() {ParameterName = "pFAB_PROD_D_XML", Value = ob.FAB_PROD_D_XML},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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

        public KNT_SC_PRG_RCVModel GetScRcvProgramByID(Int64 pKNT_SC_PRG_RCV_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sc_prg_rcv_select";
            try
            {                
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = pKNT_SC_PRG_RCV_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                KNT_SC_PRG_RCVModel ob = new KNT_SC_PRG_RCVModel();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {                    
                    ob.KNT_SC_PRG_RCV_ID = (dr["KNT_SC_PRG_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_RCV_ID"]);
                    ob.PRG_RCV_NO = (dr["PRG_RCV_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_RCV_NO"]);
                    ob.IS_PRG_NO_AUTO = (dr["IS_PRG_NO_AUTO"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PRG_NO_AUTO"]);

                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                }                
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_FAB_PROD_ORD_DModel> GetFabOrdListByKntScPrgID(Int64 pKNT_SC_PRG_RCV_ID)
        {
            string sp = "pkg_fab_prod_order.MC_fab_prod_ord_d_select";
            try
            {
                var obList = new List<MC_FAB_PROD_ORD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = pKNT_SC_PRG_RCV_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FAB_PROD_ORD_DModel ob = new MC_FAB_PROD_ORD_DModel();
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    //ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    //ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    
                    if (dr["RF_FAB_TYPE_ID"] != DBNull.Value)
                        ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);

                    if (dr["KNT_MC_DIA_ID"] != DBNull.Value)
                        ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                    if (dr["LK_MC_GG_ID"] != DBNull.Value)
                        ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);
                    
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    

                    //ob.LK_FK_DGN_TYP_ID = (dr["LK_FK_DGN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                        ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                        ob.LK_FEDER_TYPE_ID = (dr["LK_FEDER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]);
                    //ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    //ob.LK_DYE_MTHD_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                    //ob.LK_AOP_TYPE_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AOP_TYPE_ID"]);
                    //ob.WASH_TYPE_LIST = (dr["WASH_TYPE_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_TYPE_LIST"]);
                    //ob.FINISH_TYPE_LIST = (dr["FINISH_TYPE_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FINISH_TYPE_LIST"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.REV_GFAB_QTY = (dr["REV_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_GFAB_QTY"]);
                    ob.NET_GFAB_QTY = (dr["NET_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_GFAB_QTY"]);
                    ob.RQD_FFAB_QTY = (dr["RQD_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FFAB_QTY"]);
                    ob.REV_FFAB_QTY = (dr["REV_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_FFAB_QTY"]);
                    ob.NET_FFAB_QTY = (dr["NET_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_FFAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SC_PRG_SN = (dr["SC_PRG_SN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_SN"]);
                    ob.IS_SC_PRG_SN_AUTO = (dr["IS_SC_PRG_SN_AUTO"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC_PRG_SN_AUTO"]);

                    ob.SC_START_DT = (dr["SC_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_START_DT"]);
                    ob.SC_END_DT = (dr["SC_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_END_DT"]);
                    ob.SCM_SC_WO_REF_ID = (dr["SCM_SC_WO_REF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SC_WO_REF_ID"]);
                    ob.SC_BUYER_NAME = (dr["SC_BUYER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_BUYER_NAME"]);
                    ob.SC_WO_REF_NO = (dr["SC_WO_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_WO_REF_NO"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COL_TYPE_NAME = (dr["COL_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COL_TYPE_NAME"]);
                    //ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    //ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    //ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    //ob.DIA_TYPE = (dr["DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    //ob.COLOR_TYPE = (dr["COLOR_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_TYPE"]);
                    //ob.RQD_FFAB_QTY = (dr["RQD_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FFAB_QTY"]);
                    ob.NET_FFAB_QTY_NAME = (dr["NET_FFAB_QTY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NET_FFAB_QTY_NAME"]);

                    var obFabProdYrnList = new MC_FAB_PROD_D_YRNModel().GetYrnListByFabOrd(ob.MC_FAB_PROD_ORD_D_ID);
                    ob.fabProdYrn = (List<MC_FAB_PROD_D_YRNModel>)obFabProdYrnList;
                   
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetScRcvProgramByPartyID(long? pSCM_SUPPLIER_ID, Int64 pageNumber, Int64 pageSize, string pSUP_TRD_NAME_EN = null, string pSC_STYLE_NO = null, string pSC_ORDER_NO = null, string pLK_SC_PRG_STATUS_ID = null)
        {
            string sp = "pkg_knit_subcontract.knt_sc_prg_rcv_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SC_PRG_RCVModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_EN", Value = pSUP_TRD_NAME_EN},
                     new CommandParameter() {ParameterName = "pSC_STYLE_NO", Value = pSC_STYLE_NO},
                     new CommandParameter() {ParameterName = "pSC_ORDER_NO", Value = pSC_ORDER_NO},
                     new CommandParameter() {ParameterName = "pLK_SC_PRG_STATUS_ID", Value = pLK_SC_PRG_STATUS_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_PRG_RCVModel ob = new KNT_SC_PRG_RCVModel();
                    ob.KNT_SC_PRG_RCV_ID = (dr["KNT_SC_PRG_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_RCV_ID"]);
                    ob.PRG_RCV_NO = (dr["PRG_RCV_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_RCV_NO"]);
                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);

                    ob.SC_ORDER_NO = (dr["SC_ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_ORDER_NO"]);
                    ob.SC_STYLE_NO = (dr["SC_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_STYLE_NO"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }
                
                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object GetScRcvProgram(Int32? pSCM_SUPPLIER_ID, long? pKNT_SC_PRG_RCV_ID, string pPRG_RCV_NO)
        {
            string sp = "pkg_knit_subcontract.knt_sc_prg_rcv_select";
            try
            {
                var obList = new List<KNT_SC_PRG_RCVModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = pKNT_SC_PRG_RCV_ID},
                     new CommandParameter() {ParameterName = "pPRG_RCV_NO", Value = pPRG_RCV_NO},
                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SC_PRG_RCVModel ob = new KNT_SC_PRG_RCVModel();
                    ob.KNT_SC_PRG_RCV_ID = (dr["KNT_SC_PRG_RCV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_RCV_ID"]);
                    ob.PRG_RCV_NO = (dr["PRG_RCV_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_RCV_NO"]);
                    ob.SC_PRG_DT = (dr["SC_PRG_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SC_PRG_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.LK_SC_PRG_STATUS_ID = (dr["LK_SC_PRG_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SC_PRG_STATUS_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string BatchSaveSciChallan()
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
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_RCV_ID", Value = ob.KNT_SC_PRG_RCV_ID},                                          
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},                     
                     new CommandParameter() {ParameterName = "pIS_FINALIZE", Value = ob.IS_FINALIZE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_H_XML", Value = ob.KNT_SC_YRN_RCV_H_XML},
                     new CommandParameter() {ParameterName = "pKNT_SC_YRN_RCV_D_XML", Value = ob.KNT_SC_YRN_RCV_D_XML},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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