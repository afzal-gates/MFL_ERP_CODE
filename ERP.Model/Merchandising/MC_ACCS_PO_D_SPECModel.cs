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
    public class MC_ACCS_PO_D_SPECModel
    {
        public Int64 MC_ACCS_PO_D_SPEC_ID { get; set; }
        public Int64 MC_ACCS_PO_D_ITEM_ID { get; set; }
        public string ITEM_SPEC_SHORT { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64? GMT_COLOR_ID { get; set; }
        public Int64? ITEM_COLOR_ID { get; set; }
        public string COMBO_DESC { get; set; }
        public string GMT_COL_DESC { get; set; }
        public string LOT_NO { get; set; }
        public Int64? MC_SIZE_ID { get; set; }
        public Int64? MC_BUYER_BRAND_ID { get; set; }
        public Int64? RF_ITEM_QLTY_ID { get; set; }
        public Int64? RF_YRN_CNT_ID { get; set; }
        public Int64? LK_PLY_TYPE_ID { get; set; }
        public Decimal GROSS_WT { get; set; }
        public Decimal NET_WT { get; set; }
        public string MAIN_MARK { get; set; }
        public string SIDE_MARK { get; set; }
        public string MEASUREMENT { get; set; }
        public string THICKNESS { get; set; }
        public string WIDTH { get; set; }
        public string WASH_INSTR { get; set; }
        public string FABRICATION { get; set; }
        public string PART_NO { get; set; }
        public string UVC_NO { get; set; }
        public string UFC_NO { get; set; }
        public string BAR_CODE { get; set; }
        public string SWATCH { get; set; }
        public Int64? NO_PRNT_COLOR { get; set; }
        public Decimal RQD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public decimal? CONF_RATE { get; set; }


        public string PANTON_NO {get;set;} 
        public string DESIGN {get;set;}
        public string ANILINE {get;set;}
        public Int64? PRINT_COLOR_ID {get;set;}
        public Int64? TYPE_OF_TAG_ID {get;set;} 
        public Int64? TYPE_OF_THREAD_ID {get;set;}
        public Int64? THREAD_COUNT_ID {get;set;} 
        public Int64? TYPE_OF_ITEM {get;set;} 
        public string SABU_NO {get;set;}
        public Int64? BTN_DYE_COLOR_ID {get;set;}
        public Int64? BTN_DEL_COLOR_ID { get; set; }
        public Int64? LK_DELIVERY_PART { get; set; }
        public string SIZE_RANGE { get; set; }
        public Int64? CARTON_QTY { get; set; }
        public string QUALITY_SPEC { get; set; }


        public string ITEM_SPEC_SHORT_ { get; set; }
        public string MC_ORDER_H_ID_ { get; set; }
        public string GMT_COLOR_ID_ { get; set; }
        public string ITEM_COLOR_ID_ { get; set; }
        public string COMBO_DESC_ { get; set; }
        public string GMT_COL_DESC_ { get; set; }
        public string LOT_NO_ { get; set; }
        public string MC_SIZE_ID_ { get; set; }
        public string MC_BUYER_BRAND_ID_ { get; set; }
        public string RF_ITEM_QLTY_ID_ { get; set; }
        public string RF_YRN_CNT_ID_ { get; set; }
        public string LK_PLY_TYPE_ID_ { get; set; }
        public string GROSS_WT_ { get; set; }
        public string NET_WT_ { get; set; }
        public string MAIN_MARK_ { get; set; }
        public string SIDE_MARK_ { get; set; }
        public string MEASUREMENT_ { get; set; }
        public string THICKNESS_ { get; set; }
        public string WIDTH_ { get; set; }
        public string WASH_INSTR_ { get; set; }
        public string FABRICATION_ { get; set; }
        public string PART_NO_ { get; set; }
        public string UVC_NO_ { get; set; }
        public string UFC_NO_ { get; set; }
        public string BAR_CODE_ { get; set; }
        public string SWATCH_ { get; set; }
        public string NO_PRNT_COLOR_ { get; set; }
        public string RQD_QTY_ { get; set; }
        public string QTY_MOU_ID_ { get; set; }
        public string REMARKS_ { get; set; }
        public string CONF_RATE_ { get; set; }

        public string PANTON_NO_ { get; set; }
        public string DESIGN_ { get; set; }
        public string ANILINE_ { get; set; }
        public string PRINT_COLOR_ID_ { get; set; }
        public string TYPE_OF_TAG_ID_ { get; set; }
        public string TYPE_OF_THREAD_ID_ { get; set; }
        public string THREAD_COUNT_ID_ { get; set; }
        public string TYPE_OF_ITEM_ { get; set; }
        public string SABU_NO_ { get; set; }
        public string BTN_DYE_COLOR_ID_ { get; set; }
        public string BTN_DEL_COLOR_ID_ { get; set; }
        public string LK_DELIVERY_PART_ { get; set; }
        public string SIZE_RANGE_ { get; set; }
        public string CARTON_QTY_ { get; set; }
        public string QUALITY_SPEC_ { get; set; }
        public string Submit()
        {
            const string sp = "SP_MC_ACCS_PO_D_SPEC";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_D_SPEC_ID", Value = ob.MC_ACCS_PO_D_SPEC_ID},
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_D_ITEM_ID", Value = ob.MC_ACCS_PO_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pITEM_SPEC_SHORT", Value = ob.ITEM_SPEC_SHORT},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pGMT_COLOR_ID", Value = ob.GMT_COLOR_ID},
                     new CommandParameter() {ParameterName = "pITEM_COLOR_ID", Value = ob.ITEM_COLOR_ID},
                     new CommandParameter() {ParameterName = "pCOMBO_DESC", Value = ob.COMBO_DESC},
                     new CommandParameter() {ParameterName = "pGMT_COL_DESC", Value = ob.GMT_COL_DESC},
                     new CommandParameter() {ParameterName = "pLOT_NO", Value = ob.LOT_NO},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_BRAND_ID", Value = ob.MC_BUYER_BRAND_ID},
                     new CommandParameter() {ParameterName = "pRF_ITEM_QLTY_ID", Value = ob.RF_ITEM_QLTY_ID},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = ob.RF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pLK_PLY_TYPE_ID", Value = ob.LK_PLY_TYPE_ID},
                     new CommandParameter() {ParameterName = "pGROSS_WT", Value = ob.GROSS_WT},
                     new CommandParameter() {ParameterName = "pNET_WT", Value = ob.NET_WT},
                     new CommandParameter() {ParameterName = "pMAIN_MARK", Value = ob.MAIN_MARK},
                     new CommandParameter() {ParameterName = "pSIDE_MARK", Value = ob.SIDE_MARK},
                     new CommandParameter() {ParameterName = "pMEASUREMENT", Value = ob.MEASUREMENT},
                     new CommandParameter() {ParameterName = "pTHICKNESS", Value = ob.THICKNESS},
                     new CommandParameter() {ParameterName = "pWIDTH", Value = ob.WIDTH},
                     new CommandParameter() {ParameterName = "pWASH_INSTR", Value = ob.WASH_INSTR},
                     new CommandParameter() {ParameterName = "pFABRICATION", Value = ob.FABRICATION},
                     new CommandParameter() {ParameterName = "pPART_NO", Value = ob.PART_NO},
                     new CommandParameter() {ParameterName = "pUVC_NO", Value = ob.UVC_NO},
                     new CommandParameter() {ParameterName = "pUFC_NO", Value = ob.UFC_NO},
                     new CommandParameter() {ParameterName = "pBAR_CODE", Value = ob.BAR_CODE},
                     new CommandParameter() {ParameterName = "pSWATCH", Value = ob.SWATCH},
                     new CommandParameter() {ParameterName = "pNO_PRNT_COLOR", Value = ob.NO_PRNT_COLOR},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MC_ACCS_PO_D_SPECModel> Query(Int64? pMC_ACCS_PO_D_ITEM_ID = null)
        {
            string sp = "pkg_acc_booking.mc_accs_po_d_spec_select";
            try
            {
                var obList = new List<MC_ACCS_PO_D_SPECModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_D_ITEM_ID", Value = pMC_ACCS_PO_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_ACCS_PO_D_SPECModel ob = new MC_ACCS_PO_D_SPECModel();
                            ob.MC_ACCS_PO_D_SPEC_ID = (dr["MC_ACCS_PO_D_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_D_SPEC_ID"]);
                            ob.MC_ACCS_PO_D_ITEM_ID = (dr["MC_ACCS_PO_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_D_ITEM_ID"]);
                            ob.ITEM_SPEC_SHORT = (dr["ITEM_SPEC_SHORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_SHORT"]);
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                            ob.ITEM_COLOR_ID = (dr["ITEM_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_COLOR_ID"]);
                            ob.COMBO_DESC = (dr["COMBO_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_DESC"]);
                            ob.GMT_COL_DESC = (dr["GMT_COL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_COL_DESC"]);
                            ob.LOT_NO = (dr["LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_NO"]);
                            ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                            ob.MC_BUYER_BRAND_ID = (dr["MC_BUYER_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_BRAND_ID"]);
                            ob.RF_ITEM_QLTY_ID = (dr["RF_ITEM_QLTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ITEM_QLTY_ID"]);
                            ob.RF_YRN_CNT_ID = (dr["RF_YRN_CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                            ob.LK_PLY_TYPE_ID = (dr["LK_PLY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PLY_TYPE_ID"]);
                            ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]);
                            ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]);
                            ob.MAIN_MARK = (dr["MAIN_MARK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_MARK"]);
                            ob.SIDE_MARK = (dr["SIDE_MARK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIDE_MARK"]);
                            ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                            ob.THICKNESS = (dr["THICKNESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["THICKNESS"]);
                            ob.WIDTH = (dr["WIDTH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WIDTH"]);
                            ob.WASH_INSTR = (dr["WASH_INSTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_INSTR"]);
                            ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                            ob.PART_NO = (dr["PART_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_NO"]);
                            ob.UVC_NO = (dr["UVC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UVC_NO"]);
                            ob.UFC_NO = (dr["UFC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UFC_NO"]);
                            ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);
                            ob.SWATCH = (dr["SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWATCH"]);
                            ob.NO_PRNT_COLOR = (dr["NO_PRNT_COLOR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_PRNT_COLOR"]);
                            ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                            if (dr["CONF_RATE"] != DBNull.Value)
                            {
                                ob.CONF_RATE = Convert.ToDecimal(dr["CONF_RATE"]);
                            }

                            ob.PANTON_NO = (dr["PANTON_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO"]);
                            ob.DESIGN = (dr["DESIGN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGN"]);
                            ob.ANILINE = (dr["ANILINE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ANILINE"]);


                            if (dr["PRINT_COLOR_ID"] != DBNull.Value)
                            {
                                ob.PRINT_COLOR_ID = Convert.ToInt64(dr["PRINT_COLOR_ID"]);
                            }

                            if (dr["TYPE_OF_TAG_ID"] != DBNull.Value)
                            {
                                ob.TYPE_OF_TAG_ID = Convert.ToInt64(dr["TYPE_OF_TAG_ID"]);
                            }
                            if (dr["TYPE_OF_THREAD_ID"] != DBNull.Value)
                            {
                                ob.TYPE_OF_THREAD_ID = Convert.ToInt64(dr["TYPE_OF_THREAD_ID"]);
                            }

                            if (dr["THREAD_COUNT_ID"] != DBNull.Value)
                            {
                                ob.THREAD_COUNT_ID = Convert.ToInt64(dr["THREAD_COUNT_ID"]);
                            }
                            if (dr["TYPE_OF_ITEM"] != DBNull.Value)
                            {
                                ob.TYPE_OF_ITEM = Convert.ToInt64(dr["TYPE_OF_ITEM"]);
                            }
                            if (dr["BTN_DYE_COLOR_ID"] != DBNull.Value)
                            {
                                ob.BTN_DYE_COLOR_ID = Convert.ToInt64(dr["BTN_DYE_COLOR_ID"]);
                            }
                            if (dr["BTN_DEL_COLOR_ID"] != DBNull.Value)
                            {
                                ob.BTN_DEL_COLOR_ID = Convert.ToInt64(dr["BTN_DEL_COLOR_ID"]);
                            }

                            if (dr["LK_DELIVERY_PART"] != DBNull.Value)
                            {
                                ob.LK_DELIVERY_PART = Convert.ToInt64(dr["LK_DELIVERY_PART"]);
                            }


                            ob.SABU_NO = (dr["SABU_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SABU_NO"]);

                            ob.SIZE_RANGE = (dr["SIZE_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_RANGE"]);

                            if (dr["CARTON_QTY"] != DBNull.Value)
                            {
                                ob.CARTON_QTY = Convert.ToInt64(dr["CARTON_QTY"]);
                            }
                            
                            ob.QUALITY_SPEC = (dr["QUALITY_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUALITY_SPEC"]);

                            

                            ob.ITEM_SPEC_SHORT_ = (dr["ITEM_SPEC_SHORT_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_SHORT_"]);
                            ob.MC_ORDER_H_ID_ = (dr["MC_ORDER_H_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_"]);
                            ob.GMT_COLOR_ID_ = (dr["GMT_COLOR_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_COLOR_ID_"]);
                            ob.ITEM_COLOR_ID_ = (dr["ITEM_COLOR_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_COLOR_ID_"]);
                            ob.COMBO_DESC_ = (dr["COMBO_DESC_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_DESC_"]);
                            ob.GMT_COL_DESC_ = (dr["GMT_COL_DESC_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_COL_DESC_"]);
                            ob.LOT_NO_ = (dr["LOT_NO_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_NO_"]);
                            ob.MC_SIZE_ID_ = (dr["MC_SIZE_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_ID_"]);
                            ob.MC_BUYER_BRAND_ID_ = (dr["MC_BUYER_BRAND_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_BRAND_ID_"]);
                            ob.RF_ITEM_QLTY_ID_ = (dr["RF_ITEM_QLTY_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_ITEM_QLTY_ID_"]);
                            ob.RF_YRN_CNT_ID_ = (dr["RF_YRN_CNT_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_ID_"]);
                            ob.LK_PLY_TYPE_ID_ = (dr["LK_PLY_TYPE_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PLY_TYPE_ID_"]);
                            ob.GROSS_WT_ = (dr["GROSS_WT_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GROSS_WT_"]);
                            ob.NET_WT_ = (dr["NET_WT_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NET_WT_"]);
                            ob.MAIN_MARK_ = (dr["MAIN_MARK_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_MARK_"]);
                            ob.SIDE_MARK_ = (dr["SIDE_MARK_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIDE_MARK_"]);
                            ob.MEASUREMENT_ = (dr["MEASUREMENT_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT_"]);
                            ob.THICKNESS_ = (dr["THICKNESS_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["THICKNESS_"]);
                            ob.WIDTH_ = (dr["WIDTH_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WIDTH_"]);
                            ob.WASH_INSTR_ = (dr["WASH_INSTR_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_INSTR_"]);
                            ob.FABRICATION_ = (dr["FABRICATION_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION_"]);
                            ob.PART_NO_ = (dr["PART_NO_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_NO_"]);
                            ob.UVC_NO_ = (dr["UVC_NO_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UVC_NO_"]);
                            ob.UFC_NO_ = (dr["UFC_NO_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UFC_NO_"]);
                            ob.BAR_CODE_ = (dr["BAR_CODE_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE_"]);
                            ob.SWATCH_ = (dr["SWATCH_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWATCH_"]);
                            ob.NO_PRNT_COLOR_ = (dr["NO_PRNT_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_QTY_"]);
                            ob.RQD_QTY_ = (dr["RQD_QTY_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_QTY_"]);
                            ob.QTY_MOU_ID_ = (dr["QTY_MOU_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_ID_"]);
                            ob.REMARKS_ = (dr["REMARKS_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_"]);

                            ob.CONF_RATE_ = (dr["CONF_RATE_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONF_RATE_"]);

                            ob.PANTON_NO_ = (dr["PANTON_NO_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PANTON_NO_"]);
                            ob.DESIGN_ = (dr["DESIGN_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGN_"]);
                            ob.ANILINE_ = (dr["ANILINE_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ANILINE_"]);
                            ob.PRINT_COLOR_ID_ = (dr["PRINT_COLOR_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRINT_COLOR_ID_"]);
                            ob.TYPE_OF_TAG_ID_ = (dr["TYPE_OF_TAG_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TYPE_OF_TAG_ID_"]);
                            ob.TYPE_OF_THREAD_ID_ = (dr["TYPE_OF_THREAD_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TYPE_OF_THREAD_ID_"]);
                            ob.THREAD_COUNT_ID_ = (dr["THREAD_COUNT_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["THREAD_COUNT_ID_"]);
                            ob.TYPE_OF_ITEM_ = (dr["TYPE_OF_ITEM_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TYPE_OF_ITEM_"]);
                            ob.SABU_NO_ = (dr["SABU_NO_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SABU_NO_"]);
                            ob.BTN_DYE_COLOR_ID_ = (dr["BTN_DYE_COLOR_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BTN_DYE_COLOR_ID_"]);
                            ob.BTN_DEL_COLOR_ID_ = (dr["BTN_DEL_COLOR_ID_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BTN_DEL_COLOR_ID_"]);
                            ob.LK_DELIVERY_PART_ = (dr["LK_DELIVERY_PART_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DELIVERY_PART_"]);

                            ob.SIZE_RANGE_ = (dr["SIZE_RANGE_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_RANGE_"]);

                            ob.CARTON_QTY_ = (dr["CARTON_QTY_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARTON_QTY_"]);
                            ob.QUALITY_SPEC_ = (dr["QUALITY_SPEC_"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUALITY_SPEC_"]);

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_ACCS_PO_D_SPECModel Select(long ID)
        {
            string sp = "Select_MC_ACCS_PO_D_SPEC";
            try
            {
                var ob = new MC_ACCS_PO_D_SPECModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_D_SPEC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_ACCS_PO_D_SPEC_ID = (dr["MC_ACCS_PO_D_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_D_SPEC_ID"]);
                        ob.MC_ACCS_PO_D_ITEM_ID = (dr["MC_ACCS_PO_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ACCS_PO_D_ITEM_ID"]);
                        ob.ITEM_SPEC_SHORT = (dr["ITEM_SPEC_SHORT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_SHORT"]);
                        ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                        ob.GMT_COLOR_ID = (dr["GMT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_COLOR_ID"]);
                        ob.ITEM_COLOR_ID = (dr["ITEM_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_COLOR_ID"]);
                        ob.COMBO_DESC = (dr["COMBO_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_DESC"]);
                        ob.GMT_COL_DESC = (dr["GMT_COL_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_COL_DESC"]);
                        ob.LOT_NO = (dr["LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_NO"]);
                        ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                        ob.MC_BUYER_BRAND_ID = (dr["MC_BUYER_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_BRAND_ID"]);
                        ob.RF_ITEM_QLTY_ID = (dr["RF_ITEM_QLTY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ITEM_QLTY_ID"]);
                        ob.RF_YRN_CNT_ID = (dr["RF_YRN_CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_YRN_CNT_ID"]);
                        ob.LK_PLY_TYPE_ID = (dr["LK_PLY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PLY_TYPE_ID"]);
                        ob.GROSS_WT = (dr["GROSS_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_WT"]);
                        ob.NET_WT = (dr["NET_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_WT"]);
                        ob.MAIN_MARK = (dr["MAIN_MARK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_MARK"]);
                        ob.SIDE_MARK = (dr["SIDE_MARK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIDE_MARK"]);
                        ob.MEASUREMENT = (dr["MEASUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEASUREMENT"]);
                        ob.THICKNESS = (dr["THICKNESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["THICKNESS"]);
                        ob.WIDTH = (dr["WIDTH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WIDTH"]);
                        ob.WASH_INSTR = (dr["WASH_INSTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WASH_INSTR"]);
                        ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                        ob.PART_NO = (dr["PART_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_NO"]);
                        ob.UVC_NO = (dr["UVC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UVC_NO"]);
                        ob.UFC_NO = (dr["UFC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["UFC_NO"]);
                        ob.BAR_CODE = (dr["BAR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BAR_CODE"]);
                        ob.SWATCH = (dr["SWATCH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SWATCH"]);
                        ob.NO_PRNT_COLOR = (dr["NO_PRNT_COLOR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_PRNT_COLOR"]);
                        ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);



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