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
    public class MKT_INQR_QUOT_ITMModel
    {
        public Int64 MKT_INQR_QUOT_ITM_ID { get; set; }
        public Int64 MKT_INQR_QUOT_ID { get; set; }
        public Int64 LK_ITEM_GRP_ID { get; set; }
        public Int64 LK_COL_TYPE_ID { get; set; }
        public Int64? LK_AOP_TYPE_ID { get; set; }
        public Int64? LK_DYE_MTHD_ID { get; set; }
        public Decimal COST_FAB { get; set; }
        public Decimal? COST_EMB { get; set; }
        public Decimal? COST_PRNT { get; set; }
        public Decimal? COST_LACE { get; set; }
        public Decimal? COST_ZIPR { get; set; }
        public Decimal COST_TRMS { get; set; }
        public Decimal? COST_WASH { get; set; }
        public Decimal? COST_MISC { get; set; }
        public Decimal COST_CM { get; set; }
        public Decimal PCT_COMM { get; set; }
        public Decimal COST_COMM { get; set; }
        public Decimal PCT_SERV { get; set; }
        public Decimal COST_SERV { get; set; }
        public Int64? LK_FAB_TYPE_ID { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public Int64 GSM { get; set; }
        public string FABRIC_DESC { get; set; }
        public string RF_YRN_CNT_LST { get; set; }
        public Decimal BODY_FAB_CONS { get; set; }
        public Decimal? RIB_FAB_CONS { get; set; }
        public Decimal? OTH_FAB_CONS { get; set; }
        public Decimal TOT_FAB_CONS { get; set; }
        public Int64 CONS_MOU_ID { get; set; }
        public Decimal? BODY_LEN { get; set; }
        public Decimal? BL_ALLOW { get; set; }

        public decimal? HOOD_LEN { get; set; }
        public decimal? HOOD_LEN_ALLOW { get; set; }
        public Int64? NO_LAYER { get; set; }


        public Decimal? SLV_LEN { get; set; }
        public Decimal? SL_ALLOW { get; set; }
        public Decimal? CW_LEN { get; set; }
        public Decimal? CW_ALLOW { get; set; }
        public Int32? NO_LEG_PRT { get; set; }
        public Int64 GMT_MOU_ID { get; set; }
        public Decimal RATE_YRN { get; set; }
        public Decimal? RATE_YD { get; set; }
        public Decimal RATE_KNIT { get; set; }
        public Decimal RATE_DYE { get; set; }
        public Decimal RATE_DF { get; set; }
        public Decimal? RATE_AOP { get; set; }
        public Decimal RATE_FAB { get; set; }
        public Decimal CAL_PRICE_DOZ { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public decimal? HALF_THG_WTDH { get; set; }
        public decimal? BTM_WTDH_ALLOW { get; set; }
        public decimal? BACKRISE { get; set; }
        public decimal? INSEAM { get; set; }
        public decimal? BTM_LEN_ALLOW { get; set; }

        public bool IS_COLLAR_ONLY { get; set; }
        public decimal COST_CLCF_KNT { get; set; }
        public string COST_MIS_DESC { get; set; }
        public Int32? GMT_IE_ITM_CM_ID { get; set; }

        public bool IS_HEAT_SET { get; set; }
        public bool IS_PEACE { get; set; }
        public bool IS_BRUSH { get; set; }




        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp ="pkg_inquiry.mkt_inqr_quot_itm_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMKT_INQR_QUOT_ITM_ID", Value = ob.MKT_INQR_QUOT_ITM_ID},
                     new CommandParameter() {ParameterName = "pMKT_INQR_QUOT_ID", Value = ob.MKT_INQR_QUOT_ID},

                     new CommandParameter() {ParameterName = "pIS_COLLAR_ONLY", Value = ob.IS_COLLAR_ONLY ? "Y" : "N"},
                     new CommandParameter() {ParameterName = "pCOST_CLCF_KNT", Value = ob.COST_CLCF_KNT},
                     new CommandParameter() {ParameterName = "pCOST_MIS_DESC", Value = ob.COST_MIS_DESC},

                     new CommandParameter() {ParameterName = "pLK_ITEM_GRP_ID", Value = ob.LK_ITEM_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_COL_TYPE_ID", Value = ob.LK_COL_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_AOP_TYPE_ID", Value = ob.LK_AOP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_DYE_MTHD_ID", Value = ob.LK_DYE_MTHD_ID},
                     new CommandParameter() {ParameterName = "pCOST_FAB", Value = ob.COST_FAB},
                     new CommandParameter() {ParameterName = "pCOST_EMB", Value = ob.COST_EMB},
                     new CommandParameter() {ParameterName = "pCOST_PRNT", Value = ob.COST_PRNT},
                     new CommandParameter() {ParameterName = "pCOST_LACE", Value = ob.COST_LACE},
                     new CommandParameter() {ParameterName = "pCOST_ZIPR", Value = ob.COST_ZIPR},
                     new CommandParameter() {ParameterName = "pCOST_TRMS", Value = ob.COST_TRMS},
                     new CommandParameter() {ParameterName = "pCOST_WASH", Value = ob.COST_WASH},
                     new CommandParameter() {ParameterName = "pCOST_MISC", Value = ob.COST_MISC},
                     new CommandParameter() {ParameterName = "pCOST_CM", Value = ob.COST_CM},
                     new CommandParameter() {ParameterName = "pPCT_COMM", Value = ob.PCT_COMM},
                     new CommandParameter() {ParameterName = "pCOST_COMM", Value = ob.COST_COMM},
                     new CommandParameter() {ParameterName = "pPCT_SERV", Value = ob.PCT_SERV},
                     new CommandParameter() {ParameterName = "pCOST_SERV", Value = ob.COST_SERV},
                     new CommandParameter() {ParameterName = "pLK_FAB_TYPE_ID", Value = ob.LK_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pGSM", Value = ob.GSM},
                     new CommandParameter() {ParameterName = "pFABRIC_DESC", Value = ob.FABRIC_DESC},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_LST", Value = ob.RF_YRN_CNT_LST },
                     new CommandParameter() {ParameterName = "pBODY_FAB_CONS", Value = ob.BODY_FAB_CONS},
                     new CommandParameter() {ParameterName = "pRIB_FAB_CONS", Value = ob.RIB_FAB_CONS},
                     new CommandParameter() {ParameterName = "pOTH_FAB_CONS", Value = ob.OTH_FAB_CONS},
                     new CommandParameter() {ParameterName = "pTOT_FAB_CONS", Value = ob.TOT_FAB_CONS},
                     new CommandParameter() {ParameterName = "pCONS_MOU_ID", Value = ob.CONS_MOU_ID},
                     new CommandParameter() {ParameterName = "pBODY_LEN", Value = ob.BODY_LEN},
                     new CommandParameter() {ParameterName = "pBL_ALLOW", Value = ob.BL_ALLOW},
                     new CommandParameter() {ParameterName = "pSLV_LEN", Value = ob.SLV_LEN},
                     new CommandParameter() {ParameterName = "pSL_ALLOW", Value = ob.SL_ALLOW},
                     new CommandParameter() {ParameterName = "pCW_LEN", Value = ob.CW_LEN},
                     new CommandParameter() {ParameterName = "pCW_ALLOW", Value = ob.CW_ALLOW},
                     new CommandParameter() {ParameterName = "pNO_LEG_PRT", Value = ob.NO_LEG_PRT},
                     new CommandParameter() {ParameterName = "pGMT_MOU_ID", Value = ob.GMT_MOU_ID},
                     new CommandParameter() {ParameterName = "pRATE_YRN", Value = ob.RATE_YRN},
                     new CommandParameter() {ParameterName = "pRATE_YD", Value = ob.RATE_YD},
                     new CommandParameter() {ParameterName = "pRATE_KNIT", Value = ob.RATE_KNIT},
                     new CommandParameter() {ParameterName = "pRATE_DYE", Value = ob.RATE_DYE},
                     new CommandParameter() {ParameterName = "pRATE_DF", Value = ob.RATE_DF},
                     new CommandParameter() {ParameterName = "pRATE_AOP", Value = ob.RATE_AOP},
                     new CommandParameter() {ParameterName = "pRATE_FAB", Value = ob.RATE_FAB},
                     new CommandParameter() {ParameterName = "pCAL_PRICE_DOZ", Value = ob.CAL_PRICE_DOZ},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},

                     new CommandParameter() {ParameterName = "pLK_YD_TYPE_ID", Value = ob.LK_YD_TYPE_ID },
                     new CommandParameter() {ParameterName = "pLK_DF_TYPE_ID", Value = ob.LK_DF_TYPE_ID},

                     new CommandParameter() {ParameterName = "pQUOTE_PRICE_PCS", Value = ob.QUOTE_PRICE_PCS},
                     new CommandParameter() {ParameterName = "pAVG_PRICE_PCS", Value = ob.AVG_PRICE_PCS},
                     
                     new CommandParameter() {ParameterName = "pTGT_PRICE_PCS", Value = ob.TGT_PRICE_PCS},
                     new CommandParameter() {ParameterName = "pLK_INQ_STATUS_ID", Value = ob.LK_INQ_STATUS_ID},
                      new CommandParameter() {ParameterName = "pIS_FINAL_SAVE", Value = ob.IS_FINAL_SAVE},

                      new CommandParameter() {ParameterName = "pHALF_THG_WTDH", Value = ob.HALF_THG_WTDH},
                      new CommandParameter() {ParameterName = "pBTM_WTDH_ALLOW", Value = ob.BTM_WTDH_ALLOW},
                      new CommandParameter() {ParameterName = "pBACKRISE", Value = ob.BACKRISE},

                      new CommandParameter() {ParameterName = "pLK_COTN_TYPE_ID", Value = ob.LK_COTN_TYPE_ID},
                      new CommandParameter() {ParameterName = "pLK_SPN_PRCS_ID", Value = ob.LK_SPN_PRCS_ID},
                      new CommandParameter() {ParameterName = "pIS_IMPORT_FABRC", Value = ob.IS_IMPORT_FABRC ? "Y" : "N"},
                      new CommandParameter() {ParameterName = "pPCT_FAB_LOSS", Value = ob.PCT_FAB_LOSS},

                      new CommandParameter() {ParameterName = "pINSEAM", Value = ob.INSEAM},
                      new CommandParameter() {ParameterName = "pBTM_LEN_ALLOW", Value = ob.BTM_LEN_ALLOW},

                       new CommandParameter() {ParameterName = "pNO_LAYER", Value = ob.NO_LAYER},
                        new CommandParameter() {ParameterName = "pHOOD_LEN", Value = ob.HOOD_LEN},
                         new CommandParameter() {ParameterName = "pHOOD_LEN_ALLOW", Value = ob.HOOD_LEN_ALLOW},

                         new CommandParameter() {ParameterName = "pGMT_IE_ITM_CM_ID", Value = ob.GMT_IE_ITM_CM_ID},
                         new CommandParameter() {ParameterName = "pIS_BRUSH", Value = ob.IS_BRUSH ? "Y" : "N"},
                         new CommandParameter() {ParameterName = "pIS_PEACE", Value = ob.IS_PEACE  ? "Y" : "N"},
                         new CommandParameter() {ParameterName = "pIS_HEAT_SET", Value = ob.IS_HEAT_SET  ? "Y" : "N"},

                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_MKT_INQR_QUOT_ITM_ID", Value =0, Direction = ParameterDirection.Output}
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

        public List<MKT_INQR_QUOT_ITMModel> Query(Int64 pMKT_INQR_QUOT_ID, int pOption)
        {
            string sp = "pkg_inquiry.mkt_inqr_quot_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<MKT_INQR_QUOT_ITMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMKT_INQR_QUOT_ID", Value = pMKT_INQR_QUOT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MKT_INQR_QUOT_ITMModel ob = new MKT_INQR_QUOT_ITMModel();
                            ob.MKT_INQR_QUOT_ITM_ID = (dr["MKT_INQR_QUOT_ITM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MKT_INQR_QUOT_ITM_ID"]);
                            ob.MKT_INQR_QUOT_ID = (dr["MKT_INQR_QUOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MKT_INQR_QUOT_ID"]);
                            ob.LK_ITEM_GRP_ID = (dr["LK_ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_GRP_ID"]);

                            ob.LK_ITEM_GRP_TXT = (dr["LK_ITEM_GRP_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ITEM_GRP_TXT"]);

                            ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);

                            if (dr["LK_AOP_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_AOP_TYPE_ID = Convert.ToInt64(dr["LK_AOP_TYPE_ID"]);
                            }
                            if (dr["LK_DYE_MTHD_ID"] != DBNull.Value)
                            {
                                ob.LK_DYE_MTHD_ID =  Convert.ToInt64(dr["LK_DYE_MTHD_ID"]);
                            }
                            ob.COST_FAB = (dr["COST_FAB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_FAB"]);
                           
                            if (dr["COST_EMB"] != DBNull.Value)
                            {
                                ob.COST_EMB = Convert.ToDecimal(dr["COST_EMB"]);
                            }

                            if (dr["COST_PRNT"] != DBNull.Value)
                            {
                                ob.COST_PRNT = Convert.ToDecimal(dr["COST_PRNT"]);
                            }

                            if (dr["COST_LACE"] != DBNull.Value)
                            {
                                ob.COST_LACE = Convert.ToDecimal(dr["COST_LACE"]);
                            }

                            if (dr["COST_ZIPR"] != DBNull.Value)
                            {
                                ob.COST_ZIPR = Convert.ToDecimal(dr["COST_ZIPR"]);
                            }
                            ob.COST_TRMS = (dr["COST_TRMS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_TRMS"]);

                            if (dr["COST_WASH"] != DBNull.Value)
                            {
                                ob.COST_WASH = Convert.ToDecimal(dr["COST_WASH"]);
                            }

                            if (dr["COST_MISC"] != DBNull.Value)
                            {
                                ob.COST_MISC = Convert.ToDecimal(dr["COST_MISC"]);
                            }

                            ob.COST_CM = (dr["COST_CM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_CM"]);
                            ob.PCT_COMM = (dr["PCT_COMM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_COMM"]);
                            ob.COST_COMM = (dr["COST_COMM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_COMM"]);
                            ob.PCT_SERV = (dr["PCT_SERV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_SERV"]);
                            ob.COST_SERV = (dr["COST_SERV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_SERV"]);
                          

                            if (dr["LK_FAB_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_FAB_TYPE_ID = Convert.ToInt64(dr["LK_FAB_TYPE_ID"]);
                            }

                            if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                            {
                                ob.RF_FIB_COMP_ID = Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                            }

                            ob.GSM = (dr["GSM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GSM"]);
                            ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                            ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);
                            ob.BODY_FAB_CONS = (dr["BODY_FAB_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BODY_FAB_CONS"]);
                            ob.RIB_FAB_CONS = (dr["RIB_FAB_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RIB_FAB_CONS"]);
                            ob.OTH_FAB_CONS = (dr["OTH_FAB_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["OTH_FAB_CONS"]);
                            ob.TOT_FAB_CONS = (dr["TOT_FAB_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_FAB_CONS"]);
                            ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);

                            ob.NO_LAYER = (dr["NO_LAYER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_LAYER"]);

                            if (dr["HOOD_LEN"] != DBNull.Value)
                            {
                                ob.HOOD_LEN = Convert.ToDecimal(dr["HOOD_LEN"]);
                            }

                            if (dr["HOOD_LEN_ALLOW"] != DBNull.Value)
                            {
                                ob.HOOD_LEN_ALLOW = Convert.ToDecimal(dr["HOOD_LEN_ALLOW"]);
                            }


                            if (dr["BODY_LEN"] != DBNull.Value)
                            {
                                ob.BODY_LEN = Convert.ToDecimal(dr["BODY_LEN"]);
                            }

                            if (dr["BL_ALLOW"] != DBNull.Value)
                            {
                                ob.BL_ALLOW = Convert.ToDecimal(dr["BL_ALLOW"]);
                            }

                            if (dr["SLV_LEN"] != DBNull.Value)
                            {
                                ob.SLV_LEN = Convert.ToDecimal(dr["SLV_LEN"]);
                            }

                            if (dr["SL_ALLOW"] != DBNull.Value)
                            {
                                ob.SL_ALLOW = Convert.ToDecimal(dr["SL_ALLOW"]);
                            }

                            if (dr["CW_LEN"] != DBNull.Value)
                            {
                                ob.CW_LEN = Convert.ToDecimal(dr["CW_LEN"]);
                            }

                            if (dr["CW_ALLOW"] != DBNull.Value)
                            {
                                ob.CW_ALLOW = Convert.ToDecimal(dr["CW_ALLOW"]);
                            }

                            if (dr["NO_LEG_PRT"] != DBNull.Value)
                            {
                                ob.NO_LEG_PRT = Convert.ToInt32(dr["NO_LEG_PRT"]);
                            }
                            ob.GMT_MOU_ID = (dr["GMT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MOU_ID"]);
                            ob.RATE_YRN = (dr["RATE_YRN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_YRN"]);

                            if (dr["RATE_YD"] != DBNull.Value)
                            {
                                ob.RATE_YD = Convert.ToDecimal(dr["RATE_YD"]);
                            }
                           

                            ob.RATE_KNIT = (dr["RATE_KNIT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_KNIT"]);
                            ob.RATE_DYE = (dr["RATE_DYE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_DYE"]);
                            ob.RATE_DF = (dr["RATE_DF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_DF"]);
                          
                            if (dr["RATE_AOP"] != DBNull.Value)
                            {
                                ob.RATE_AOP = Convert.ToDecimal(dr["RATE_AOP"]);
                            }

                            ob.RATE_FAB = (dr["RATE_FAB"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_FAB"]);

                            ob.PCT_FAB_LOSS = (dr["PCT_FAB_LOSS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_FAB_LOSS"]);
                          
                            if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_COTN_TYPE_ID = Convert.ToInt32(dr["LK_COTN_TYPE_ID"]);
                            }

                            if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                            {
                                ob.LK_SPN_PRCS_ID = Convert.ToInt32(dr["LK_SPN_PRCS_ID"]);
                            }

                            
                            ob.IS_IMPORT_FABRC = (dr["IS_IMPORT_FABRC"] == DBNull.Value) ? false : Convert.ToString(dr["IS_IMPORT_FABRC"]) == "Y";


                            if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_YD_TYPE_ID = Convert.ToInt32(dr["LK_YD_TYPE_ID"]);
                            }

                            if (dr["LK_DF_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_DF_TYPE_ID = Convert.ToInt32(dr["LK_DF_TYPE_ID"]);
                            }

                  

                            ob.CAL_PRICE_DOZ = (dr["CAL_PRICE_DOZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE_DOZ"]);

                            ob.LK_FAB_TYPE_TXT = (dr["LK_FAB_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FAB_TYPE_TXT"]);
                            ob.LK_YD_TYPE_TXT = (dr["LK_YD_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YD_TYPE_TXT"]);
                            ob.LK_AOP_TYPE_TXT = (dr["LK_AOP_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_AOP_TYPE_TXT"]);
                            ob.LK_DYE_MTHD_TXT = (dr["LK_DYE_MTHD_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DYE_MTHD_TXT"]);
                            ob.LK_DF_TYPE_TXT = (dr["LK_DF_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DF_TYPE_TXT"]);
                            ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                            ob.LK_FAB_TYPE_RATE = (dr["LK_FAB_TYPE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_FAB_TYPE_RATE"]);
                            ob.LK_YD_TYPE_RATE = (dr["LK_YD_TYPE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_YD_TYPE_RATE"]);
                            ob.LK_AOP_TYPE_RATE = (dr["LK_AOP_TYPE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_AOP_TYPE_RATE"]);
                            ob.LK_DYE_MTHD_RATE = (dr["LK_DYE_MTHD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_DYE_MTHD_RATE"]);
                            ob.LK_DF_TYPE_RATE = (dr["LK_DF_TYPE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_DF_TYPE_RATE"]);

                  

                            if (dr["LK_FAB_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_FAB_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = (dr["LK_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_FAB_TYPE_ID"]),
                                    LK_DATA_NAME_EN = (dr["LK_FAB_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FAB_TYPE_TXT"]),
                                    LK_DATA_PARAM_VALUE =(dr["LK_FAB_TYPE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_FAB_TYPE_RATE"])
                                };
                            }
                            else
                            {
                                ob.LK_FAB_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = "",
                                    LK_DATA_NAME_EN = "",
                                    LK_DATA_PARAM_VALUE = 0
                                };

                            }

                            if (dr["LK_COTN_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_COTN_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = (dr["LK_COTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_COTN_TYPE_ID"]),
                                    LK_DATA_NAME_EN = (dr["LK_COTN_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_COTN_TYPE_TXT"])
                                };
                            }
                            else
                            {
                                ob.LK_COTN_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = "",
                                    LK_DATA_NAME_EN = "",
                                };

                            }

                            if (dr["LK_SPN_PRCS_ID"] != DBNull.Value)
                            {
                                ob.LK_SPN_PRCS_OBJ = new
                                {
                                    LOOKUP_DATA_ID = (dr["LK_SPN_PRCS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_SPN_PRCS_ID"]),
                                    LK_DATA_NAME_EN = (dr["LK_SPN_PRCS_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_SPN_PRCS_TXT"])
                                };
                            }
                            else
                            {
                                ob.LK_SPN_PRCS_OBJ = new
                                {
                                    LOOKUP_DATA_ID = "",
                                    LK_DATA_NAME_EN = ""
                                };

                            }


                            if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_YD_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_YD_TYPE_ID"]),
                                    LK_DATA_NAME_EN = (dr["LK_YD_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YD_TYPE_TXT"]),
                                    LK_DATA_PARAM_VALUE = (dr["LK_YD_TYPE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_YD_TYPE_RATE"])
                                };
                            }
                            else
                            {
                                ob.LK_YD_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = "",
                                    LK_DATA_NAME_EN = "",
                                    LK_DATA_PARAM_VALUE = 0
                                };

                            }
                            if (dr["LK_AOP_TYPE_ID"] != DBNull.Value)
                            {
                                ob.LK_AOP_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = (dr["LK_AOP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_AOP_TYPE_ID"]),
                                    LK_DATA_NAME_EN = (dr["LK_AOP_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_AOP_TYPE_TXT"]),
                                    LK_DATA_PARAM_VALUE = (dr["LK_AOP_TYPE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_AOP_TYPE_RATE"])
                                };
                            } else {
                                ob.LK_AOP_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = "",
                                    LK_DATA_NAME_EN = "",
                                    LK_DATA_PARAM_VALUE = 0
                                };
                            }

                            if (dr["LK_DYE_MTHD_ID"] != DBNull.Value)
                            {
                                ob.LK_DYE_MTHD_OBJ = new
                                {
                                    LOOKUP_DATA_ID = (dr["LK_DYE_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_DYE_MTHD_ID"]),
                                    LK_DATA_NAME_EN = (dr["LK_DYE_MTHD_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DYE_MTHD_TXT"]),
                                    LK_DATA_PARAM_VALUE = (dr["LK_DYE_MTHD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_DYE_MTHD_RATE"])
                                };
                            }
                            else
                            {
                                ob.LK_DYE_MTHD_OBJ = new
                                {
                                    LOOKUP_DATA_ID = "",
                                    LK_DATA_NAME_EN = "",
                                    LK_DATA_PARAM_VALUE = 0
                                };
                            }

                            if (dr["LK_DF_TYPE_ID"] != DBNull.Value)
                            {

                                ob.LK_DF_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = (dr["LK_DF_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_DF_TYPE_ID"]),
                                    LK_DATA_NAME_EN = (dr["LK_DF_TYPE_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DF_TYPE_TXT"]),
                                    LK_DATA_PARAM_VALUE = (dr["LK_DF_TYPE_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_DF_TYPE_RATE"])
                                };
                            }
                            else
                            {

                                ob.LK_DF_TYPE_OBJ = new
                                {
                                    LOOKUP_DATA_ID = "",
                                    LK_DATA_NAME_EN = "",
                                    LK_DATA_PARAM_VALUE = 0
                                };
                            }
                            if (dr["RF_FIB_COMP_ID"] != DBNull.Value)
                            {
                                ob.RF_FIB_COMP_OBJ = new
                                {
                                    RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_FIB_COMP_ID"]),
                                    FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]),
                                    RATE_YRN = (dr["RATE_YRN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_YRN"]),
                                    IS_COTTON = (dr["IS_COTTON"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_COTTON"]),
                                    IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]),
                                    IS_PC_CVC = (dr["IS_PC_CVC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PC_CVC"])
                                };
                            }
                            else
                            {
                                ob.RF_FIB_COMP_OBJ = new
                                {
                                    RF_FIB_COMP_ID = "",
                                    FIB_COMP_NAME = "",
                                    RATE_YRN = 0,
                                    IS_COTTON=  0,
                                    IS_ELA_MXD = "N",
                                    IS_PC_CVC = "N",
                                };
                            }

                            if (dr["HALF_THG_WTDH"] != DBNull.Value)
                            {
                                ob.HALF_THG_WTDH =  Convert.ToDecimal(dr["HALF_THG_WTDH"]);
                            }
                            if (dr["BTM_WTDH_ALLOW"] != DBNull.Value)
                            {
                                ob.BTM_WTDH_ALLOW = Convert.ToDecimal(dr["BTM_WTDH_ALLOW"]);
                            }

                            if (dr["BACKRISE"] != DBNull.Value)
                            {
                                ob.BACKRISE = Convert.ToDecimal(dr["BACKRISE"]);
                            }

                            if (dr["INSEAM"] != DBNull.Value)
                            {
                                ob.INSEAM = Convert.ToDecimal(dr["INSEAM"]);
                            }

                            if (dr["BTM_LEN_ALLOW"] != DBNull.Value)
                            {
                                ob.BTM_LEN_ALLOW = Convert.ToDecimal(dr["BTM_LEN_ALLOW"]);
                            }

                            ob.IS_COLLAR_ONLY = (dr["IS_COLLAR_ONLY"] == DBNull.Value) ? false : Convert.ToString(dr["IS_COLLAR_ONLY"]) == "Y";
                            ob.COST_CLCF_KNT = (dr["COST_CLCF_KNT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_CLCF_KNT"]);
                            ob.COST_MIS_DESC = (dr["COST_MIS_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_MIS_DESC"]);

                            if (dr["GMT_IE_ITM_CM_ID"] != DBNull.Value)
                            {
                                ob.GMT_IE_ITM_CM_ID = Convert.ToInt32(dr["GMT_IE_ITM_CM_ID"]);
                            }

                            ob.IS_HEAT_SET = (dr["IS_HEAT_SET"] == DBNull.Value) ? false : Convert.ToString(dr["IS_HEAT_SET"]) == "Y";
                            ob.IS_PEACE = (dr["IS_PEACE"] == DBNull.Value) ? false : Convert.ToString(dr["IS_PEACE"]) == "Y";
                            ob.IS_BRUSH = (dr["IS_BRUSH"] == DBNull.Value) ? false : Convert.ToString(dr["IS_BRUSH"]) == "Y";

                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MKT_INQR_QUOT_ITMModel> getInquiryDetailsData(Int64 pMKT_INQR_QUOT_ID, int pOption)
        {
            string sp = "pkg_inquiry.mkt_inqr_quot_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<MKT_INQR_QUOT_ITMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMKT_INQR_QUOT_ID", Value = pMKT_INQR_QUOT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MKT_INQR_QUOT_ITMModel ob = new MKT_INQR_QUOT_ITMModel();
                    ob.MKT_INQR_QUOT_ITM_ID = (dr["MKT_INQR_QUOT_ITM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MKT_INQR_QUOT_ITM_ID"]);
                    ob.LK_ITEM_GRP_TXT = (dr["LK_ITEM_GRP_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ITEM_GRP_TXT"]);
                    ob.CAL_PRICE_DOZ = (dr["CAL_PRICE_DOZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_PRICE_DOZ"]);
                    ob.CAL_PRICE_PCS = decimal.Round(ob.CAL_PRICE_DOZ / 12, 2, MidpointRounding.AwayFromZero);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? LK_YD_TYPE_ID { get; set; }
        public int? LK_DF_TYPE_ID { get; set; }
        public object LK_FAB_TYPE_OBJ { get; set; }
        public object RF_FIB_COMP_OBJ { get; set; }
        public object LK_YD_TYPE_OBJ { get; set; }
        public object LK_DF_TYPE_OBJ { get; set; }
        public object LK_AOP_TYPE_OBJ { get; set; }
        public object LK_DYE_MTHD_OBJ { get; set; }


        public decimal LK_FAB_TYPE_RATE { get; set; }

        public decimal LK_YD_TYPE_RATE { get; set; }

        public decimal LK_AOP_TYPE_RATE { get; set; }

        public decimal LK_DYE_MTHD_RATE { get; set; }

        public decimal LK_DF_TYPE_RATE { get; set; }

        public string FIB_COMP_NAME { get; set; }

        public string LK_DF_TYPE_TXT { get; set; }

        public string LK_DYE_MTHD_TXT { get; set; }

        public string LK_AOP_TYPE_TXT { get; set; }

        public string LK_YD_TYPE_TXT { get; set; }

        public string LK_FAB_TYPE_TXT { get; set; }

        public string LK_ITEM_GRP_TXT { get; set; }
        public string IS_FINAL_SAVE { get; set; }
        public decimal QUOTE_PRICE_PCS { get; set; }
        public decimal TGT_PRICE_PCS { get; set; }
        public decimal LK_INQ_STATUS_ID { get; set; }
        public decimal CAL_PRICE_PCS { get; set; }
        public decimal AVG_PRICE_PCS { get; set; }

        public decimal PCT_FAB_LOSS { get; set; }

        public int? LK_COTN_TYPE_ID { get; set; }

        public int? LK_SPN_PRCS_ID { get; set; }

        public bool IS_IMPORT_FABRC { get; set; }

        public object LK_SPN_PRCS_OBJ { get; set; }

        public object LK_COTN_TYPE_OBJ { get; set; }

        public List<LookupDataModel> LookupListData(Int64 lookupTableId)
        {
            string sp = "pkg_inquiry.mkt_fab_rate_tmplt_select";
            try
            {
                var obList = new List<LookupDataModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pLOOKUP_TABLE_ID", Value = lookupTableId}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LookupDataModel ob = new LookupDataModel();
                    ob.LOOKUP_DATA_ID = (dr["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_DATA_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_GRP_ID"]);

                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                    ob.LK_DATA_PARAM_VALUE = (dr["LK_DATA_PARAM_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LK_DATA_PARAM_VALUE"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["VERSION_NO"]);

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