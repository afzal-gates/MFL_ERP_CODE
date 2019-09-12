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
    public class MKT_INQR_QUOTDVModel
    {

        public DateTime INQR_DT { get; set; }

        public int NO_OF_INQUIRY { get; set; }

        public List<MKT_INQR_QUOTModel> inquiries { get; set; }
    }
    public class MKT_INQR_QUOTModel
    {
        public Int64 MKT_INQR_QUOT_ID { get; set; }
        public Int32? MC_BUYER_ID { get; set; }
        public Int32 MC_BYR_ACC_ID { get; set; }
        public string INQR_NO { get; set; }
        public string CMB_MDL_NO { get; set; }
        public DateTime INQR_DT { get; set; }
        public Int64? LAST_REV_NO { get; set; }
        public DateTime? LAST_REV_DT { get; set; }
        public Int32 LK_ITEM_GRP_ID { get; set; }
        public Int32 RF_CURRENCY_ID { get; set; }
        public Decimal CURR_CONV_TK { get; set; }
        public Int64 EST_ORD_QTY { get; set; }
        public Int32 QTY_MOU_ID { get; set; }
        public DateTime EST_SHIP_DT { get; set; }
        public Decimal AVG_PRICE_DOZ { get; set; }
        public Decimal QUOTE_PRICE_PCS { get; set; }
        public Decimal TGT_PRICE_PCS { get; set; }
        public bool IS_FINALIZED { get; set; }
        public Int64 LK_INQ_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Decimal AVG_PRICE_PCS { get; set; }        
        public List<MKT_INQR_QUOT_ITMModel> items { get; set; }
        public Int32? pOption {get;set;}

        public string Save()
         // pOption=1000=>General Form Save
        {
            const string sp ="pkg_inquiry.mkt_inqr_quot_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMKT_INQR_QUOT_ID", Value = ob.MKT_INQR_QUOT_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                       new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pINQR_NO", Value = ob.INQR_NO},
                     new CommandParameter() {ParameterName = "pCMB_MDL_NO", Value = ob.CMB_MDL_NO},
                     new CommandParameter() {ParameterName = "pINQR_DT", Value = ob.INQR_DT},
                     new CommandParameter() {ParameterName = "pLAST_REV_NO", Value = ob.LAST_REV_NO},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pLK_ITEM_GRP_ID", Value = ob.LK_ITEM_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCURR_CONV_TK", Value = ob.CURR_CONV_TK},
                     new CommandParameter() {ParameterName = "pEST_ORD_QTY", Value = ob.EST_ORD_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pEST_SHIP_DT", Value = ob.EST_SHIP_DT},
                     new CommandParameter() {ParameterName = "pAVG_PRICE_DOZ", Value = ob.AVG_PRICE_DOZ},
                     new CommandParameter() {ParameterName = "pQUOTE_PRICE_PCS", Value = ob.QUOTE_PRICE_PCS},
                     new CommandParameter() {ParameterName = "pTGT_PRICE_PCS", Value = ob.TGT_PRICE_PCS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED ? "Y" : "N"},
                     new CommandParameter() {ParameterName = "pLK_INQ_STATUS_ID", Value = ob.LK_INQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =ob.pOption ?? 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_MKT_INQR_QUOT_ID", Value =0, Direction = ParameterDirection.Output}
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



        private List<MKT_INQR_QUOTModel> GetInquiryHeaderData(DateTime pINQR_DT, DateTime? pINQR_DT_FROM, DateTime? pINQR_DT_TO, string pINQR_NO,
             Int32? pLK_INQ_STATUS_ID, Int32? pLK_ITEM_GRP_ID, Int32? pSC_USER_ID = null, Int32? pLK_FAB_TYPE_ID = null, Int32? pRF_FIB_COMP_ID = null)
        {
            string sp = "pkg_inquiry.mkt_inqr_quot_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<MKT_INQR_QUOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINQR_DT_FROM", Value = pINQR_DT_FROM},
                     new CommandParameter() {ParameterName = "pINQR_DT_TO", Value = pINQR_DT_TO},
                     new CommandParameter() {ParameterName = "pINQR_NO", Value = pINQR_NO},
                     new CommandParameter() {ParameterName = "pLK_INQ_STATUS_ID", Value = pLK_INQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pLK_ITEM_GRP_ID", Value = pLK_ITEM_GRP_ID},

                     new CommandParameter() {ParameterName = "pINQR_DT", Value = pINQR_DT},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},

                     new CommandParameter() {ParameterName = "pLK_FAB_TYPE_ID", Value = pLK_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = pRF_FIB_COMP_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3000},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MKT_INQR_QUOTModel ob = new MKT_INQR_QUOTModel();
                            ob.MKT_INQR_QUOT_ID = (dr["MKT_INQR_QUOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MKT_INQR_QUOT_ID"]);
                           
                            if (dr["MC_BUYER_ID"] != DBNull.Value)
                            {
                                ob.MC_BUYER_ID = Convert.ToInt32(dr["MC_BUYER_ID"]);
                            }

                            ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);
                            ob.INQR_NO = (dr["INQR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INQR_NO"]);
                            ob.CMB_MDL_NO = (dr["CMB_MDL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CMB_MDL_NO"]);
                            ob.INQR_DT = (dr["INQR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INQR_DT"]);
                            

                            if (dr["LAST_REV_NO"] != DBNull.Value)
                            {
                                ob.LAST_REV_NO =  Convert.ToInt64(dr["LAST_REV_NO"]);
                            }

                            if (dr["LAST_REV_DT"] != DBNull.Value)
                            {
                                ob.LAST_REV_DT = Convert.ToDateTime(dr["LAST_REV_DT"]);
                            }
                           
                            ob.LK_ITEM_GRP_ID = (dr["LK_ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_ITEM_GRP_ID"]);
                            ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_CURRENCY_ID"]);
                            ob.CURR_CONV_TK = (dr["CURR_CONV_TK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_TK"]);
                            ob.EST_ORD_QTY = (dr["EST_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EST_ORD_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QTY_MOU_ID"]);

                            ob.EST_SHIP_DT = (dr["EST_SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EST_SHIP_DT"]);
                            ob.AVG_PRICE_DOZ = (dr["AVG_PRICE_DOZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_PRICE_DOZ"]);
                            ob.QUOTE_PRICE_PCS = (dr["QUOTE_PRICE_PCS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QUOTE_PRICE_PCS"]);
                            ob.TGT_PRICE_PCS = (dr["TGT_PRICE_PCS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TGT_PRICE_PCS"]);
                            ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? false : Convert.ToString(dr["IS_FINALIZED"]) == "Y";
                            ob.LK_INQ_STATUS_ID = (dr["LK_INQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INQ_STATUS_ID"]);

                            ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                            ob.LK_ITEM_GRP_TXT = (dr["LK_ITEM_GRP_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ITEM_GRP_TXT"]);
                            ob.CURR_CODE = (dr["CURR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_CODE"]);
                            ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                            ob.LK_INQ_STATUS_TXT = (dr["LK_INQ_STATUS_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_INQ_STATUS_TXT"]);
                            ob.AVG_PRICE_PCS = (dr["AVG_PRICE_PCS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_PRICE_PCS"]);
                            ob.items = new List<MKT_INQR_QUOT_ITMModel>();
                            ob.items = new MKT_INQR_QUOT_ITMModel().getInquiryDetailsData(ob.MKT_INQR_QUOT_ID, 3004);
                            if (ob.LK_ITEM_GRP_ID == 236)
                            {
                                ob.MKT_INQR_QUOT_ITM_ID = 0;
                            }
                            else
                            {
                                ob.MKT_INQR_QUOT_ITM_ID = ob.items.Count() == 1 ? ob.items[0].MKT_INQR_QUOT_ITM_ID : 0 ;
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
        public List<MKT_INQR_QUOTDVModel> GetInqListData(
             DateTime? pINQR_DT_FROM,        DateTime? pINQR_DT_TO,         string pINQR_NO,
             Int32? pLK_INQ_STATUS_ID,       Int32? pLK_ITEM_GRP_ID, Int32? pSC_USER_ID,
             Int32? pLK_FAB_TYPE_ID,         Int32? pRF_FIB_COMP_ID
        )
        {
            string sp = "pkg_inquiry.mkt_inqr_quot_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<MKT_INQR_QUOTDVModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINQR_DT_FROM", Value = pINQR_DT_FROM},
                     new CommandParameter() {ParameterName = "pINQR_DT_TO", Value = pINQR_DT_TO},
                     new CommandParameter() {ParameterName = "pINQR_NO", Value = pINQR_NO},
                     new CommandParameter() {ParameterName = "pLK_INQ_STATUS_ID", Value = pLK_INQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pLK_ITEM_GRP_ID", Value = pLK_ITEM_GRP_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     
                     new CommandParameter() {ParameterName = "pLK_FAB_TYPE_ID", Value = pLK_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = pRF_FIB_COMP_ID},

                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MKT_INQR_QUOTDVModel ob = new MKT_INQR_QUOTDVModel();
                        ob.INQR_DT = (dr["INQR_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr["INQR_DT"]);
                        ob.NO_OF_INQUIRY = (dr["NO_OF_INQUIRY"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_INQUIRY"]);
                        ob.inquiries = new List<MKT_INQR_QUOTModel>();
                        ob.inquiries = this.GetInquiryHeaderData(Convert.ToDateTime(dr["INQR_DT"]), pINQR_DT_FROM, pINQR_DT_TO, pINQR_NO, pLK_INQ_STATUS_ID, pLK_ITEM_GRP_ID, pSC_USER_ID, pLK_FAB_TYPE_ID, pRF_FIB_COMP_ID);
                    obList.Add(ob);
            
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MKT_INQR_QUOTModel Select(Int64 pMKT_INQR_QUOT_ID, int pOption)
        {
            string sp = "pkg_inquiry.mkt_inqr_quot_select";
            //pOption=3001=>Select Single Data
            try
            {
                var ob = new MKT_INQR_QUOTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMKT_INQR_QUOT_ID", Value = pMKT_INQR_QUOT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   ob.MKT_INQR_QUOT_ID = (dr["MKT_INQR_QUOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MKT_INQR_QUOT_ID"]);
                   if (dr["MC_BUYER_ID"] != DBNull.Value)
                   {
                       ob.MC_BUYER_ID = Convert.ToInt32(dr["MC_BUYER_ID"]);
                   }
                   ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_ID"]);
                   ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MC_BYR_ACC_GRP_ID"]);

                   ob.INQR_NO = (dr["INQR_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INQR_NO"]);
                   ob.CMB_MDL_NO = (dr["CMB_MDL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CMB_MDL_NO"]);
                   ob.INQR_DT = (dr["INQR_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["INQR_DT"]);

                   if (dr["LAST_REV_NO"] != DBNull.Value)
                   {
                       ob.LAST_REV_NO = Convert.ToInt64(dr["LAST_REV_NO"]);
                   }

                   if (dr["LAST_REV_DT"] != DBNull.Value)
                   {
                       ob.LAST_REV_DT = Convert.ToDateTime(dr["LAST_REV_DT"]);
                   }

                   ob.LK_ITEM_GRP_ID = (dr["LK_ITEM_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_ITEM_GRP_ID"]);
                   ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["RF_CURRENCY_ID"]);
                   ob.CURR_CONV_TK = (dr["CURR_CONV_TK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_CONV_TK"]);
                   ob.EST_ORD_QTY = (dr["EST_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EST_ORD_QTY"]);
                   ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["QTY_MOU_ID"]);
                   ob.EST_SHIP_DT = (dr["EST_SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EST_SHIP_DT"]);
                   ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? false : Convert.ToString(dr["IS_FINALIZED"]) == "Y";
                   ob.LK_INQ_STATUS_ID = (dr["LK_INQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_INQ_STATUS_ID"]);
                   ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                   ob.LK_ITEM_GRP_TXT = (dr["LK_ITEM_GRP_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ITEM_GRP_TXT"]);
                   ob.CURR_CODE = (dr["CURR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_CODE"]);
                   ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                   ob.LK_INQ_STATUS_TXT = (dr["LK_INQ_STATUS_TXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_INQ_STATUS_TXT"]);
                   ob.items = new List<MKT_INQR_QUOT_ITMModel>();
                   ob.items = new MKT_INQR_QUOT_ITMModel().Query(ob.MKT_INQR_QUOT_ID, 3002);
                   ob.AVG_PRICE_DOZ = ob.items.Count > 0 ? decimal.Round(ob.items.Average(x => x.CAL_PRICE_DOZ), 2, MidpointRounding.AwayFromZero) : 0;
                   ob.AVG_PRICE_PCS = ob.items.Count > 0 ? decimal.Round(ob.items.Average(x => x.CAL_PRICE_DOZ)/12, 2, MidpointRounding.AwayFromZero) : 0;
                   ob.QUOTE_PRICE_PCS = (dr["QUOTE_PRICE_PCS"] == DBNull.Value) ? ob.AVG_PRICE_PCS : Convert.ToDecimal(dr["QUOTE_PRICE_PCS"]);
                   ob.TGT_PRICE_PCS = (dr["TGT_PRICE_PCS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TGT_PRICE_PCS"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BUYER_NAME_EN { get; set; }

        public string LK_ITEM_GRP_TXT { get; set; }

        public string CURR_CODE { get; set; }

        public string QTY_MOU_CODE { get; set; }

        public string LK_INQ_STATUS_TXT { get; set; }

        public long MKT_INQR_QUOT_ITM_ID { get; set; }

        public int MC_BYR_ACC_GRP_ID { get; set; }
    }
}