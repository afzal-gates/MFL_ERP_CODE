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
    public class DYE_DC_LRT_HModel
    {
        public Int64 DYE_DC_LRT_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string RET_CHALAN_NO { get; set; }
        public DateTime RET_CHALAN_DT { get; set; }
        public DateTime RCV_DT { get; set; }
        public Int64 ITEM_RCV_BY { get; set; }
        public string CARRIER_NAME { get; set; }
        public string CARRIER_NO { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_ADJ_D { get; set; }
        public Int32 TOTAL_REC { get; set; }
        public string IS_FINALIZED { get; set; }
        
        public string Save()
        {
            const string sp = "pkg_dye_dc_receive.dye_dc_lrt_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_LRT_H_ID", Value = ob.DYE_DC_LRT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value = ob.RET_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pCARRIER_NO", Value = ob.CARRIER_NO},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML_ADJ_D", Value = ob.XML_ADJ_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opDYE_DC_LRT_H_ID", Value =0, Direction = ParameterDirection.Output},
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

        public string Update()
        {
            const string sp = "SP_DYE_DC_LRT_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_LRT_H_ID", Value = ob.DYE_DC_LRT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value = ob.RET_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pCARRIER_NO", Value = ob.CARRIER_NO},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "SP_DYE_DC_LRT_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_LRT_H_ID", Value = ob.DYE_DC_LRT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value = ob.RET_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pCARRIER_NO", Value = ob.CARRIER_NO},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<DYE_DC_LRT_HModel> SelectAll(int pageNo, int pageSize, string pRET_CHALAN_NO = null, string pRET_CHALAN_DT = null, string pCOMP_NAME_EN = null, 
            string pSTORE_NAME_EN = null, string pSUP_TRD_NAME_EN = null, string pACTN_STATUS_NAME = null, string pITEM_RECV_BY_NAME = null)
        {
            string sp = "pkg_dye_dc_receive.dye_dc_lrt_h_select";
            try
            {
                DateTime? dt=null;
                if (pRET_CHALAN_DT!=null)
                    dt = Convert.ToDateTime(pRET_CHALAN_DT);
                var obList = new List<DYE_DC_LRT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_LRT_H_ID", Value =0},

                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value =pRET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value =dt},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pSTORE_NAME_EN", Value =pSTORE_NAME_EN},
                     new CommandParameter() {ParameterName = "pSUP_TRD_NAME_EN", Value =pSUP_TRD_NAME_EN},
                     new CommandParameter() {ParameterName = "pACTN_STATUS_NAME", Value =pACTN_STATUS_NAME},
                     new CommandParameter() {ParameterName = "pITEM_RECV_BY_NAME", Value =pITEM_RECV_BY_NAME},

                     new CommandParameter() {ParameterName = "pOption", Value =3000},                     
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_DC_LRT_HModel ob = new DYE_DC_LRT_HModel();
                    ob.DYE_DC_LRT_H_ID = (dr["DYE_DC_LRT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_LRT_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RET_CHALAN_NO = (dr["RET_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RET_CHALAN_NO"]);
                    ob.RET_CHALAN_DT = (dr["RET_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RET_CHALAN_DT"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.CARRIER_NO = (dr["CARRIER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NO"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);   
                 
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.ITEM_RECV_BY_NAME = (dr["ITEM_RECV_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_RECV_BY_NAME"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_DC_LRT_HModel Select(Int64? ID = null)
        {
            string sp = "pkg_dye_dc_receive.dye_dc_lrt_h_select";
            try
            {
                var ob = new DYE_DC_LRT_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_DC_LRT_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_DC_LRT_H_ID = (dr["DYE_DC_LRT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_LRT_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.RET_CHALAN_NO = (dr["RET_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RET_CHALAN_NO"]);
                    ob.RET_CHALAN_DT = (dr["RET_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RET_CHALAN_DT"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.CARRIER_NO = (dr["CARRIER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NO"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.ITEM_RECV_BY_NAME = (dr["ITEM_RECV_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_RECV_BY_NAME"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string COMP_NAME_EN { get; set; }

        public string STORE_NAME_EN { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string ITEM_RECV_BY_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }
    }
}