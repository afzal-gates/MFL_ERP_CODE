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
    public class DYE_GSTR_REQ_HModel
    {
        public Int64 DYE_GSTR_REQ_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public string STR_REQ_NO { get; set; }
        public DateTime STR_REQ_DT { get; set; }
        public Int64 STR_REQ_BY { get; set; }
        public string STR_REQ_TO { get; set; }
        public string REQ_ATTN_MAIL { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public string REMARKS { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }

        private List<DYE_BT_CARD_HModel> _items = null;
        public List<DYE_BT_CARD_HModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<DYE_BT_CARD_HModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public string Delete()
        {
            const string sp = "PKG_DYE_GFAB_REQ.DYE_GSTR_REQ_H_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = ob.DYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pIS_BATCH_STORE", Value = ob.IS_BATCH_STORE},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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



        public RF_PAGERModel getGreyFabReqList(Int64 pageNumber, Int64 pageSize, Int64? pMC_FAB_PROD_ORD_H_ID, Int64? pMC_BYR_ACC_ID, Int64? pRF_ACTN_STATUS_ID, Int64? pRF_ACTN_TYPE_ID, Int64? pSCM_SUPPLIER_ID, Int64? pSCM_STORE_ID, string pDYE_BATCH_NO = null, Int64? pMC_COLOR_ID=null)
        {


            string sp = "PKG_DYE_GFAB_REQ.grey_fab_req_list_select";
            try
            {
                var obList = new List<DYE_GSTR_REQ_HModel>();
                Int64 vTotalRec = 0;
                Int64 pSC_USER_ID = (Int64) HttpContext.Current.Session["multiScUserId"];
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},

                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = pRF_ACTN_STATUS_ID},

                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value = pRF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_GSTR_REQ_HModel ob = new DYE_GSTR_REQ_HModel();
                    ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);

                    ob.DYE_GSTR_ISS_H_ID = (dr["DYE_GSTR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_ISS_H_ID"]);

                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.EVENT_NAME_S = (dr["EVENT_NAME_S"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME_S"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    
                    //ob.IS_PERFORMER = (Convert.ToInt64(dr["SC_USER_ID"]) == pSC_USER_ID || pSC_USER_ID == 1) ? "Y" : "N";
                    ob.IS_PERFORMER = (Convert.ToInt64(dr["PERMISSION"]) == 1 || pSC_USER_ID == 1) ? "Y" : "N";

                    ob.PENDING = (dr["PENDING"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PENDING"]);
                    
                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }
                    ob.items = new DYE_BT_CARD_HModel().getBatchListByFabReqH(
                         ob.DYE_GSTR_REQ_H_ID,
                         ob.IS_PERFORMER,
                         ob.ACTN_ROLE_FLAG,
                         ob.NXT_ACTION_NAME,
                         ob.DYE_GSTR_ISS_H_ID
                    );
                    ob.SUP_TRD_NAME_EN = (ob.items.Count > 0) ? ob.items.First().SUP_TRD_NAME_EN : "";

                    ob.ACT_BATCH_QTY = ob.items.Sum(o => o.ACT_BATCH_QTY);
                    ob.PLAN_BATCH_QTY = ob.items.Sum(o => o.PLAN_BATCH_QTY);
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

        public DYE_GSTR_REQ_HModel Select(long ID)
        {
            string sp = "Select_DYE_GSTR_REQ_H";
            try
            {
                var ob = new DYE_GSTR_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                        ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                        ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                        ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                        ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                        ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_TO"]);
                        ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                        ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IS_PERFORMER { get; set; }
        public string ACTN_ROLE_FLAG { get; set; }
        public string NXT_ACTION_NAME { get; set; }
        public string EVENT_NAME { get; set; }
        public long? DYE_GSTR_ISS_H_ID { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }

        public string EVENT_NAME_S { get; set; }

        public Decimal ACT_BATCH_QTY { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string DYE_BATCH_NO { get; set; }

        public string PENDING { get; set; }

        public object IS_BATCH_STORE { get; set; }

        public long PLAN_BATCH_QTY { get; set; }
    }
}