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
    public class KNT_CLCF_ORD_PRDModel
    {
        public Int64 KNT_CLCF_ORD_PRD_ID { get; set; }
        public DateTime PROD_DT { get; set; }
        public Int64 KNT_CLCF_STYL_ITEM_ID { get; set; }
        public Int64 MC_CLCF_ORD_REQ_ID { get; set; }        
        public Int64 KNT_MACHINE_ID { get; set; }
        public Int64 PRD_QTY { get; set; }
        public Int64? CPI { get; set; }
        public int? NO_PLY { get; set; }
        public string IS_FINALIZED { get; set; }

        public Int64? RF_FAB_PROD_CAT_ID { get; set; }
        public long? MC_FAB_PROD_ORD_H_ID { get; set; }
        public long? MC_FAB_PROD_ORD_D_ID { get; set; }
        public long? RF_FAB_TYPE_ID { get; set; }
        public long? RF_FIB_COMP_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? RF_GARM_PART_ID { get; set; }
        public string GARM_PART_NAME { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public Int64? MC_SIZE_ID { get; set; }
        public string SIZE_CODE { get; set; }
        public string MESUREMENT { get; set; }
        public string MEAS_COLLAR { get; set; }
        public string MEAS_CUFF { get; set; }
        public string KNT_MACHINE_NO { get; set; }
        public Int64? RQD_PCS_QTY { get; set; }
        public string IS_ACTIVE { get; set; }
        public long? ALREADY_PRD_QTY_COLLAR { get; set; }
        public long? ALREADY_PRD_QTY_CUFF { get; set; }
        public long? ALREADY_DELV_QTY { get; set; }
        public Int64? BAL_QTY { get; set; }
        public string XML_DTL { get; set; }



        public string Save()
        {
            const string sp = "SP_KNT_CLCF_ORD_PRD";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_CLCF_ORD_PRD_ID", Value = ob.KNT_CLCF_ORD_PRD_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pPRD_QTY", Value = ob.PRD_QTY},                     
                     //new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                    
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


        public List<KNT_CLCF_ORD_PRDModel> SelectAll()
        {
            string sp = "Select_KNT_CLCF_ORD_PRD";
            try
            {
                var obList = new List<KNT_CLCF_ORD_PRDModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_CLCF_ORD_PRD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_CLCF_ORD_PRDModel ob = new KNT_CLCF_ORD_PRDModel();
                    ob.KNT_CLCF_ORD_PRD_ID = (dr["KNT_CLCF_ORD_PRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_ORD_PRD_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.PRD_QTY = (dr["PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_CLCF_ORD_PRDModel Select(long ID)
        {
            string sp = "Select_KNT_CLCF_ORD_PRD";
            try
            {
                var ob = new KNT_CLCF_ORD_PRDModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_CLCF_ORD_PRD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_CLCF_ORD_PRD_ID = (dr["KNT_CLCF_ORD_PRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_ORD_PRD_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.PRD_QTY = (dr["PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_QTY"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetCollarCuffProd(long pRF_FAB_PROD_CAT_ID, long pMC_STYLE_H_EXT_ID, long pMC_COLOR_ID, DateTime pPROD_DT)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_ord_prd_select";
            try
            {
                Int64? vRowTotalQty = 0;
                DateTime? vProdDate = pPROD_DT;
                Int64? vMcID = 0;
                Int64? vKpdID = 0;

                var obListVM = new List<KNT_CLCF_ORD_PRD_VM>();
                OraDatabase db = new OraDatabase();
                var dsVM = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_CLCF_ORD_PRD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value =pPROD_DT},

                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow drVM in dsVM.Tables[0].Rows)
                {
                    KNT_CLCF_ORD_PRD_VM obVM = new KNT_CLCF_ORD_PRD_VM();

                    obVM.IS_DT_GRP = (drVM["IS_DT_GRP"] == DBNull.Value) ? "N" : Convert.ToString(drVM["IS_DT_GRP"]);
                    obVM.IS_MC_GRP = (drVM["IS_MC_GRP"] == DBNull.Value) ? "N" : Convert.ToString(drVM["IS_MC_GRP"]);

                    vRowTotalQty = 0;

                    if (obVM.IS_DT_GRP == "Y")
                        vProdDate = (drVM["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drVM["PROD_DT"]);

                    if (obVM.IS_MC_GRP == "Y")
                    {
                        vMcID = (drVM["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drVM["KNT_MACHINE_ID"]);
                        vKpdID = (drVM["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drVM["KNT_PLAN_D_ID"]);
                    }

                    if (drVM["PROD_DT"] != DBNull.Value)
                        obVM.PROD_DT = (drVM["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drVM["PROD_DT"]);

                    if (drVM["PROD_DT_DISP"] != DBNull.Value)
                        obVM.PROD_DT_DISP = (drVM["PROD_DT_DISP"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(drVM["PROD_DT_DISP"]);


                    if (drVM["KNT_MACHINE_NO"] != DBNull.Value)
                    {
                        obVM.KNT_MACHINE_ID = (drVM["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drVM["KNT_MACHINE_ID"]);
                        obVM.KNT_PLAN_D_ID = (drVM["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drVM["KNT_PLAN_D_ID"]);
                    }

                    obVM.KNT_MACHINE_NO = (drVM["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(drVM["KNT_MACHINE_NO"]);
                    obVM.KNT_YRN_LOT_LST = (drVM["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(drVM["KNT_YRN_LOT_LST"]);

                    if (drVM["RF_GARM_PART_ID"] != DBNull.Value)
                        obVM.RF_GARM_PART_ID = (drVM["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(drVM["RF_GARM_PART_ID"]);

                    obVM.IS_DT_SAVE = (drVM["IS_DT_SAVE"] == DBNull.Value) ? "N" : Convert.ToString(drVM["IS_DT_SAVE"]);
                    obVM.IS_MC_SAVE = (drVM["IS_MC_SAVE"] == DBNull.Value) ? "N" : Convert.ToString(drVM["IS_MC_SAVE"]);

                    obVM.GARM_PART_NAME = (drVM["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(drVM["GARM_PART_NAME"]);
                    obVM.DATE_ROW_SPAN = (drVM["DATE_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(drVM["DATE_ROW_SPAN"]);
                    obVM.MC_ROW_SPAN = (drVM["MC_ROW_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(drVM["MC_ROW_SPAN"]);

                    var obList = new List<KNT_CLCF_ORD_PRDModel>();
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                    {
                         new CommandParameter() {ParameterName = "pKNT_CLCF_ORD_PRD_ID", Value =0},
                         new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                         new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                         new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                         new CommandParameter() {ParameterName = "pPROD_DT", Value = pPROD_DT},
                         new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = vMcID},
                         new CommandParameter() {ParameterName = "pKNT_PLAN_D_ID", Value = vKpdID},
                         new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value =obVM.RF_GARM_PART_ID},

                         new CommandParameter() {ParameterName = "pOption", Value = 3004},
                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                     }, sp);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        KNT_CLCF_ORD_PRDModel ob = new KNT_CLCF_ORD_PRDModel();
                        ob.KNT_CLCF_ORD_PRD_ID = (dr["KNT_CLCF_ORD_PRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_ORD_PRD_ID"]);
                        ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);

                        if (dr["KNT_CLCF_STYL_ITEM_ID"] != DBNull.Value)
                            ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                        if (dr["MC_CLCF_ORD_REQ_ID"] != DBNull.Value)
                            ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);

                        ob.KNT_PLAN_D_ID = (dr["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_D_ID"]);

                        ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                        ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);

                        if (dr["PRD_QTY"] != DBNull.Value)
                            ob.PRD_QTY = (dr["PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRD_QTY"]);

                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                        vRowTotalQty = vRowTotalQty + ob.PRD_QTY;

                        ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                        ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                        //ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                        ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                        ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                        ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                        ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                        ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                        ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                        ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                        //ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                        ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                        ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                        ob.MEAS_COLLAR = (dr["MEAS_COLLAR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEAS_COLLAR"]);
                        ob.MEAS_CUFF = (dr["MEAS_CUFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEAS_CUFF"]);
                        ob.RQD_PCS_QTY = (dr["RQD_PCS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PCS_QTY"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACTIVE"]);

                        if (dr["CPI"] != DBNull.Value)
                            ob.CPI = (dr["CPI"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CPI"]);
                        if (dr["NO_PLY"] != DBNull.Value)
                            ob.NO_PLY = (dr["NO_PLY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_PLY"]);

                        ob.ALREADY_PRD_QTY_COLLAR = (dr["ALREADY_PRD_QTY_COLLAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALREADY_PRD_QTY_COLLAR"]);
                        ob.ALREADY_PRD_QTY_CUFF = (dr["ALREADY_PRD_QTY_CUFF"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALREADY_PRD_QTY_CUFF"]);

                        ob.ALREADY_DELV_QTY = (dr["ALREADY_DELV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALREADY_DELV_QTY"]);
                        
                        ob.BAL_QTY = (dr["BAL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BAL_QTY"]);
                        obList.Add(ob);
                    }

                    obVM.ROW_TOTAL_QTY = vRowTotalQty;
                    obVM.ordSizeList = obList;
                    obListVM.Add(obVM);
                }
                return obListVM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string BatchSave()
        {
            const string sp = "pkg_knit_collar_cuff.knt_clcf_ord_prd_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML_DTL", Value = ob.XML_DTL},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
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





        public long KNT_PLAN_D_ID { get; set; }
    }




    public class KNT_CLCF_ORD_PRD_VM
    {
        public DateTime? PROD_DT { get; set; }
        public DateTime? PROD_DT_DISP { get; set; }
        public string KNT_MACHINE_NO { get; set; }
        public string GARM_PART_NAME { get; set; }
        public Int64? KNT_MACHINE_ID { get; set; }
        public Int64? RF_GARM_PART_ID { get; set; }
        public string IS_DT_GRP { get; set; }
        public Int32? DATE_ROW_SPAN { get; set; }
        public string IS_MC_GRP { get; set; }
        public Int32? MC_ROW_SPAN { get; set; }
        public string IS_DT_SAVE { get; set; }
        public string IS_MC_SAVE { get; set; }
        public Int64? ROW_TOTAL_QTY { get; set; }
        public string KNT_YRN_LOT_LST { get; set; }
        public List<KNT_CLCF_ORD_PRDModel> ordSizeList { get; set; }
        public long? KNT_PLAN_D_ID { get; set; }
    }


    public class KNT_CLCF_ORDERModel
    {
        public Int64 MC_ORDER_H_ID { get; set; }
        public string ORDER_NO { get; set; }
        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO { get; set; }

        public object GetOrderList4CollarCuff(int pageNumber, int pageSize, string pORDER_NO)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_ord_prd_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_CLCF_ORDERModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pORDER_NO", Value =pORDER_NO},
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},

                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_CLCF_ORDERModel ob = new KNT_CLCF_ORDERModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);

                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);

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

    }


    public class KNT_CLCF_DESIG_TYPE_VM
    {
        public string COLLAR_DESIG_TYPE { get; set; }
        public string YARN_DETAIL { get; set; }
        public Int64? CPI { get; set; }
        public int? NO_PLY { get; set; }

        public KNT_CLCF_DESIG_TYPE_VM GetCollarCuffDesigType(long pRF_FAB_PROD_CAT_ID, long pMC_STYLE_H_EXT_ID, long pMC_COLOR_ID)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_ord_prd_select";
            try
            {
                KNT_CLCF_DESIG_TYPE_VM ob = new KNT_CLCF_DESIG_TYPE_VM();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.COLLAR_DESIG_TYPE = (dr["COLLAR_DESIG_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLLAR_DESIG_TYPE"]);
                    ob.YARN_DETAIL = (dr["YARN_DETAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_DETAIL"]);

                    if (dr["CPI"] != DBNull.Value)
                        ob.CPI = (dr["CPI"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CPI"]);
                    if (dr["NO_PLY"] != DBNull.Value)
                        ob.NO_PLY = (dr["NO_PLY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_PLY"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class KNT_CLCF_YARN_VM
    {
        public string KNT_YRN_LOT_LST { get; set; }
        public Int64? KNT_PLAN_H_ID { get; set; }
        public Int64? KNT_PLAN_D_ID { get; set; }


        public List<KNT_CLCF_YARN_VM> GetCollarCuffYarnList(Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null)
        {
            string sp = "pkg_knit_collar_cuff.knt_clcf_ord_prd_select";
            try
            {
                var obList = new List<KNT_CLCF_YARN_VM>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},

                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_CLCF_YARN_VM ob = new KNT_CLCF_YARN_VM();
                    ob.KNT_PLAN_D_ID = (dr["KNT_PLAN_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_D_ID"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}