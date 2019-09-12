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
    public class KNT_ORD_TRN_REQ_HModel
    {
        public Int64 KNT_ORD_TRN_REQ_H_ID { get; set; }
        public string TRN_REQ_NO { get; set; }
        public DateTime TRN_REQ_DT { get; set; }
        public Int64 TRN_REQ_BY { get; set; }
        public Int64? TRN_REQ_TO { get; set; }
        public Int64 TRN_TYPE_ID { get; set; }
        public Int64 FRM_PROD_CAT_ID { get; set; }
        public Int64 TO_PROD_CAT_ID { get; set; }
        public Int64 LK_TRN_SRC_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZED { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string IS_PROJECTION_BK { get; set; }
        public string ORD_TRN_REQ_DTL_XML { get; set; }

        public string TRN_REQ_BY_NAME { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string TRN_REQ_TO_NAME { get; set; }
        public string TRN_TYPE_NAME { get; set; }
        public string ACTION_USER_TYPE { get; set; }
        public Int64 NEXT_RF_ACTN_STATUS_ID { get; set; }
        
        public string OTP_CODE { get; set; }
        public string MAIL_ADD_LST { get; set; }
        public List<KNT_ORD_TRN_REQ_DModel> fabTransList { get; set; }

        

        public string BatchSave()
        {
            const string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ORD_TRN_REQ_H_ID", Value = ob.KNT_ORD_TRN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pTRN_REQ_NO", Value = ob.TRN_REQ_NO},
                     new CommandParameter() {ParameterName = "pTRN_REQ_DT", Value = ob.TRN_REQ_DT},
                     new CommandParameter() {ParameterName = "pTRN_REQ_BY", Value = ob.TRN_REQ_BY},
                     new CommandParameter() {ParameterName = "pTRN_REQ_TO", Value = ob.TRN_REQ_TO},
                     
                     new CommandParameter() {ParameterName = "pLK_TRN_SRC_ID", Value = ob.LK_TRN_SRC_ID},
                     new CommandParameter() {ParameterName = "pFRM_PROD_CAT_ID", Value = ob.FRM_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pTO_PROD_CAT_ID", Value = ob.TO_PROD_CAT_ID},

                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pNEXT_RF_ACTN_STATUS_ID", Value = ob.NEXT_RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_PROJECTION_BK", Value = ob.IS_PROJECTION_BK},
                     new CommandParameter() {ParameterName = "pORD_TRN_REQ_DTL_XML", Value = ob.ORD_TRN_REQ_DTL_XML},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},

                     new CommandParameter() {ParameterName = "pKNT_ORD_TRN_REQ_H_ID_RTN", Value =0, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pTRN_REQ_NO_RTN", Value =0, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID_RTN", Value =0, Direction = ParameterDirection.Output}
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

        public object GetOrdWiseGreyTrnReqList(int pageNumber, int pageSize, int? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null,
            Int64? pKNT_ORD_TRN_REQ_H_ID = null, string pTRN_REQ_NO = null, DateTime? pTRN_REQ_DT = null)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                
                var obj = new RF_PAGERModel();
                var obList = new List<KNT_ORD_TRN_REQ_HModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize}, 
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_ORD_TRN_REQ_H_ID", Value = pKNT_ORD_TRN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pTRN_REQ_NO", Value = pTRN_REQ_NO},
                     new CommandParameter() {ParameterName = "pTRN_REQ_DT", Value = pTRN_REQ_DT},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_ORD_TRN_REQ_HModel ob = new KNT_ORD_TRN_REQ_HModel();
                    ob.KNT_ORD_TRN_REQ_H_ID = (dr["KNT_ORD_TRN_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ORD_TRN_REQ_H_ID"]);
                    ob.TRN_REQ_NO = (dr["TRN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_REQ_NO"]);
                    ob.TRN_REQ_DT = (dr["TRN_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRN_REQ_DT"]);
                    ob.TRN_REQ_BY = (dr["TRN_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRN_REQ_BY"]);
                    ob.TRN_REQ_TO = (dr["TRN_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRN_REQ_TO"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.LK_TRN_SRC_ID = (dr["LK_TRN_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_TRN_SRC_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.TRN_TYPE_NAME = (dr["TRN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_TYPE_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);

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

        public List<KNT_ORD_TRN_REQ_HModel> GetTransData4AutoSearch(string pTRN_REQ_NO = null)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_h_select";

            

            try
            {
                var obList = new List<KNT_ORD_TRN_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pTRN_REQ_NO", Value =pTRN_REQ_NO},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_ORD_TRN_REQ_HModel ob = new KNT_ORD_TRN_REQ_HModel();
                    ob.KNT_ORD_TRN_REQ_H_ID = (dr["KNT_ORD_TRN_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ORD_TRN_REQ_H_ID"]);
                    ob.TRN_REQ_NO = (dr["TRN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_REQ_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_ORD_TRN_REQ_HModel GetOrdWiseGreyTrnReqHdr(long pKNT_ORD_TRN_REQ_H_ID)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_h_select";
            try
            {
                var ob = new KNT_ORD_TRN_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ORD_TRN_REQ_H_ID", Value =pKNT_ORD_TRN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_ORD_TRN_REQ_H_ID = (dr["KNT_ORD_TRN_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ORD_TRN_REQ_H_ID"]);
                    ob.TRN_REQ_NO = (dr["TRN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_REQ_NO"]);
                    ob.TRN_REQ_DT = (dr["TRN_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRN_REQ_DT"]);
                    ob.TRN_REQ_BY = (dr["TRN_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRN_REQ_BY"]);
                    ob.TRN_REQ_TO = (dr["TRN_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRN_REQ_TO"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.TRN_TYPE_ID = (dr["TRN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRN_TYPE_ID"]);
                    ob.LK_TRN_SRC_ID = (dr["LK_TRN_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_TRN_SRC_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_PROJECTION_BK = (dr["IS_PROJECTION_BK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROJECTION_BK"]);

                    ob.TRN_REQ_BY_NAME = (dr["TRN_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_REQ_BY_NAME"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.TRN_REQ_TO_NAME = (dr["TRN_REQ_TO_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_REQ_TO_NAME"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_ORD_TRN_REQ_HModel SendOTP4GreyFabTrns(long pKNT_ORD_TRN_REQ_H_ID)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_h_select";
            try
            {
                var ob = new KNT_ORD_TRN_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_ORD_TRN_REQ_H_ID", Value =pKNT_ORD_TRN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_ORD_TRN_REQ_H_ID = (dr["KNT_ORD_TRN_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ORD_TRN_REQ_H_ID"]);
                    ob.TRN_REQ_NO = (dr["TRN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_REQ_NO"]);
                    ob.TRN_REQ_DT = (dr["TRN_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TRN_REQ_DT"]);
                    ob.TRN_REQ_BY = (dr["TRN_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRN_REQ_BY"]);
                    ob.TRN_REQ_TO = (dr["TRN_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRN_REQ_TO"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.LK_TRN_SRC_ID = (dr["LK_TRN_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_TRN_SRC_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_PROJECTION_BK = (dr["IS_PROJECTION_BK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PROJECTION_BK"]);

                    ob.TRN_REQ_BY_NAME = (dr["TRN_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_REQ_BY_NAME"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.TRN_REQ_TO_NAME = (dr["TRN_REQ_TO_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_REQ_TO_NAME"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);

                    ob.TRN_TYPE_NAME = (dr["TRN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_TYPE_NAME"]);
                    ob.OTP_CODE = (dr["OTP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTP_CODE"]);
                    ob.MAIL_ADD_LST = (dr["MAIL_ADD_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIL_ADD_LST"]);

                    var obFabTransList = new KNT_ORD_TRN_REQ_DModel().GetFabTransDtl(pKNT_ORD_TRN_REQ_H_ID);
                    ob.fabTransList = (List<KNT_ORD_TRN_REQ_DModel>)obFabTransList;

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> GetTransactionTypeList()
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_h_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        TRN_TYPE_ID = (dr["TRN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TRN_TYPE_ID"]),
                        TRN_TYPE_NAME = (dr["TRN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TRN_TYPE_NAME"]),
                        FRM_PROD_CAT_ID = (dr["FRM_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_PROD_CAT_ID"]),
                        TO_PROD_CAT_ID = (dr["TO_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_PROD_CAT_ID"]),
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<dynamic> GetOrdFabBkingList(Int64 pMC_FAB_PROD_ORD_H_ID, Int64? pFAB_COLOR_ID=null)
        {
            string sp = "pkg_fab_prod_order.mc_fab_prod_ord_h_select";
            try
            {
                var obList = new List<dynamic>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                    new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pFAB_COLOR_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3013},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    obList.Add(new
                    {
                        MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]),
                        BKING_FABRIC_DESC = (dr["BKING_FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BKING_FABRIC_DESC"]),

                        RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]),
                        RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]),
                        FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]),
                        LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]),

                        FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]),
                        FAB_COLOR_NAME_EN = (dr["FAB_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_COLOR_NAME_EN"]),
                        FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]),
                        GFAB_RQD_BAL_QTY = (dr["GFAB_RQD_BAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GFAB_RQD_BAL_QTY"])
                    });
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }





    public class KNT_PROD_BAL4TRANS_Model
    {
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FAB_ITEM_DESC { get; set; }

        public long RF_FAB_TYPE_ID { get; set; }
        public long RF_FIB_COMP_ID { get; set; }
        public string FIN_GSM { get; set; }

        public long LK_DIA_TYPE_ID { get; set; }

        public Int64 NO_OF_ROLL { get; set; }
        public Decimal FAB_ROLL_WT { get; set; }
        public Decimal ALREADY_TRN_FAB_QTY { get; set; }
        public Decimal BAL_QTY { get; set; }
        public Decimal TRN_FAB_QTY { get; set; }


        public object GetProdBalQty4TransList(Int64 pMC_FAB_PROD_ORD_H_ID, Int64? pFAB_COLOR_ID = null)
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_h_select";
            try
            {
                var obList = new List<KNT_PROD_BAL4TRANS_Model>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {    
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_PROD_BAL4TRANS_Model ob = new KNT_PROD_BAL4TRANS_Model();
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);

                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.ALREADY_TRN_FAB_QTY = (dr["ALREADY_TRN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ALREADY_TRN_FAB_QTY"]);
                    ob.BAL_QTY = (dr["BAL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_QTY"]);
                    ob.TRN_FAB_QTY = (dr["TRN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TRN_FAB_QTY"]);

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


    public class KNT_ORD_TRN_REQ_USR_LAVELModel
    {
        public string USER_SENDER_NAME { get; set; }
        public string USER_RECEIVER_NAME { get; set; }


        public KNT_ORD_TRN_REQ_USR_LAVELModel GetFabTransUserLavel()
        {
            string sp = "pkg_knit_fab_rcv_delv.knt_ord_trn_req_h_select";
            try
            {
                var ob = new KNT_ORD_TRN_REQ_USR_LAVELModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.USER_SENDER_NAME = (dr["USER_SENDER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_SENDER_NAME"]);
                    ob.USER_RECEIVER_NAME = (dr["USER_RECEIVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_RECEIVER_NAME"]);
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