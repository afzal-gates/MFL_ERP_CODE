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
    public class DYE_STR_REQ_HModel
    {
        public Int64 DYE_STR_REQ_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public string STR_REQ_NO { get; set; }
        public DateTime STR_REQ_DT { get; set; }
        public string INV_ITEM_CAT_LST { get; set; }
        public Int64 STR_REQ_BY { get; set; }
        public Int64 STR_REQ_TO { get; set; }
        public string REQ_ATTN_MAIL { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 REQ_STORE_ID { get; set; }
        public Int64 ISS_STORE_ID { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }
        public Int64? DYE_RE_PROC_TYPE_ID { get; set; }

        public Int64? DYE_MACHINE_ID { get; set; }
        public DateTime? LOAD_TIME { get; set; }
        public DateTime? UN_LOAD_TIME { get; set; }
        public string REMARKS { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? PRE_DYE_STR_REQ_H_ID { get; set; }
        public Int64 MC_LD_RECIPE_H_ID { get; set; }
        public Int64? DYE_DOSE_TMPLT_H_ID { get; set; }

        public string SUP_LN_REQ_NO { get; set; }
        public string XML_REQ_D { get; set; }

        public string Save()
        {
            const string sp = "pkg_dye_dc_issue.dye_str_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO==0?ob.STR_REQ_BY:ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value = ob.REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID == 0 ? ob.REQ_STORE_ID : ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID==0?null:ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID==0?null:ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pSUP_LN_REQ_NO", Value = ob.SUP_LN_REQ_NO},                     
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},                     
                     new CommandParameter() {ParameterName = "pPICK_UP", Value = ob.PICK_UP},                     
                     //new CommandParameter() {ParameterName = "pIS_ITEM_ADJ", Value = ob.IS_ITEM_ADJ == null ? "N" : ob.IS_ITEM_ADJ},
                     //new CommandParameter() {ParameterName = "pADJ_BY_ITEM_ID", Value = ob.ADJ_BY_ITEM_ID <= 0 ? null : ob.ADJ_BY_ITEM_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},                     
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID==0?null:ob.DYE_RE_PROC_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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

        public string DeleteGenReqIssue()
        {
            const string sp = "pkg_dye_batch_program.dye_dc_iss_h_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
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



        public string DeleteGenRequisition()
        {
            const string sp = "pkg_dye_batch_program.dye_str_req_h_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
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


        public string SaveBP()
        {
            const string sp = "pkg_dye_batch_program.dye_bt_dc_req_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID==0?1:ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID==0?1:ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST==null?"3,4":ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID == 0 ? 7 : ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value = ob.REQ_STORE_ID==0?4:ob.REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID == 0 ? 4 : ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID==0?null:ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID==0?null:ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D1", Value = ob.XML_REQ_D1},                     
                     new CommandParameter() {ParameterName = "pXML_REQ_D2", Value = ob.XML_REQ_D2},                     
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID==0?1:ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID==0?1:ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID==0?1:ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID==0?1:ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPRE_DYE_STR_REQ_H_ID", Value = ob.PRE_DYE_STR_REQ_H_ID==0?null:ob.PRE_DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDF_RIB_SHADE_RPT_H_ID", Value = ob.DF_RIB_SHADE_RPT_H_ID == 0 ? 7 : ob.DF_RIB_SHADE_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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


        public string SaveAddition()
        {
            const string sp = "pkg_dye_batch_program.dye_bt_dc_req_add_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID==0?1:ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID==0?1:ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST==null?"3,4":ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID == 0 ? 7 : ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value = ob.REQ_STORE_ID==0?4:ob.REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID == 0 ? 4 : ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID==0?null:ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID==0?null:ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D1", Value = ob.XML_REQ_D1},                     
                     new CommandParameter() {ParameterName = "pXML_REQ_D2", Value = ob.XML_REQ_D2},                     
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID==0?1:ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID==0?1:ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID==0?1:ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID==0?1:ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPRE_DYE_STR_REQ_H_ID", Value = ob.PRE_DYE_STR_REQ_H_ID==0?null:ob.PRE_DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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

        public string SaveABP()
        {
            const string sp = "pkg_dye_batch_program.dye_bt_dc_req_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID==0?1:ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID==0?1:ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST==null?"3,4":ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID == 0 ? 7 : ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value = ob.REQ_STORE_ID==0?4:ob.REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID == 0 ? 4 : ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID==0?null:ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID==0?null:ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D1", Value = ob.XML_REQ_D1},                     
                     new CommandParameter() {ParameterName = "pXML_REQ_D2", Value = ob.XML_REQ_D2},                     
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID==0?1:ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID==0?1:ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID==0?1:ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID==0?1:ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPRE_DYE_STR_REQ_H_ID", Value = ob.PRE_DYE_STR_REQ_H_ID==0?null:ob.PRE_DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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

        public string UpdateBP()
        {
            const string sp = "pkg_dye_batch_program.dye_bt_dc_req_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID==0?1:ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID==0?1:ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST==null?"3,4":ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID == 0 ? 7 : ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value = ob.REQ_STORE_ID==0?4:ob.REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID == 0 ? 4 : ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID==0?null:ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID==0?null:ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D1", Value = ob.XML_REQ_D1},                     
                     new CommandParameter() {ParameterName = "pXML_REQ_D2", Value = ob.XML_REQ_D2},                     
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID==0?1:ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID==0?1:ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID==0?1:ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID==0?1:ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPRE_DYE_STR_REQ_H_ID", Value = ob.PRE_DYE_STR_REQ_H_ID==0?null:ob.PRE_DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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



        public string UpdateRunningBatch()
        {
            const string sp = "pkg_dye_batch_program.dye_bt_dc_req_re_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID==0?1:ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID==0?1:ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST==null?"3,4":ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID == 0 ? 7 : ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value = ob.REQ_STORE_ID==0?4:ob.REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID == 0 ? 4 : ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID==0?null:ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID==0?null:ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D1", Value = ob.XML_REQ_D1},                     
                     new CommandParameter() {ParameterName = "pXML_REQ_D2", Value = ob.XML_REQ_D2},                     
                     new CommandParameter() {ParameterName = "pDYE_DOSE_TMPLT_H_ID", Value = ob.DYE_DOSE_TMPLT_H_ID==0?1:ob.DYE_DOSE_TMPLT_H_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_RECIPE_H_ID", Value = ob.MC_LD_RECIPE_H_ID==0?1:ob.MC_LD_RECIPE_H_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID==0?1:ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID==0?1:ob.DYE_RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPRE_DYE_STR_REQ_H_ID", Value = ob.PRE_DYE_STR_REQ_H_ID==0?null:ob.PRE_DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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

        public string UpdateRequisition()
        {
            const string sp = "pkg_dye_batch_program.dye_batch_str_req_h_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID}, 
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
        /// <summary>
        /// Machine Wash Production
        /// </summary>
        /// <returns></returns>
        public string SaveMW()
        {
            const string sp = "pkg_dye_dc_issue.dye_str_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID==0?null:ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_dye_dc_issue.dye_str_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value = ob.REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID == 0 ? ob.REQ_STORE_ID : ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID==0?null:ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID==0?null:ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pSUP_LN_REQ_NO", Value = ob.SUP_LN_REQ_NO},                     
                     //new CommandParameter() {ParameterName = "pIS_ITEM_ADJ", Value = ob.IS_ITEM_ADJ == null ? "N" : ob.IS_ITEM_ADJ},
                     //new CommandParameter() {ParameterName = "pADJ_BY_ITEM_ID", Value = ob.ADJ_BY_ITEM_ID <= 0 ? null : ob.ADJ_BY_ITEM_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},                     
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID==0?null:ob.DYE_RE_PROC_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDYE_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "SP_DYE_STR_REQ_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value = ob.REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pLOAD_TIME", Value = ob.LOAD_TIME},
                     new CommandParameter() {ParameterName = "pUN_LOAD_TIME", Value = ob.UN_LOAD_TIME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pDYE_RE_PROC_TYPE_ID", Value = ob.DYE_RE_PROC_TYPE_ID},
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

        public string DeleteBP()
        {
            const string sp = "pkg_dye_batch_program.dye_bt_dc_req_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
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


        public string DeleteIssue()
        {
            const string sp = "pkg_dye_batch_program.dye_bt_dc_iss_h_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},                     
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
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


        public List<DYE_STR_REQ_HModel> SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pUSER_NAME_EN = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_dye_dc_issue.dye_str_req_h_select";
            try
            {
                var obList = new List<DYE_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = 0},
                     new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = pSTR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = pSTR_REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value = pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pUSER_NAME_EN", Value = pUSER_NAME_EN},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value = pEVENT_NAME},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = pUSER_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_NO", Value = pDYE_MACHINE_NO},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3000:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_REQ_HModel ob = new DYE_STR_REQ_HModel();
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.FROM_ST_NAME = (dr["FROM_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ST_NAME"]);
                    ob.TO_ST_NAME = (dr["TO_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ST_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.IS_ISSUED = (dr["IS_ISSUED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_ISSUED"]);
                    ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    if (dr["DYE_DC_ISS_H_ID"] != DBNull.Value)
                        ob.DYE_DC_ISS_H_ID = Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    ob.TTL_RQD = (dr["TTL_RQD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RQD"]);
                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);
                    ob.BALANCE_QTY = ob.TTL_RQD - ob.TTL_ISS_QTY;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_STR_REQ_HModel> SelectAllBatch(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pCOLOR_NAME_EN = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pDYE_BATCH_NO = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_dye_dc_issue.dye_str_req_h_select";
            try
            {
                var obList = new List<DYE_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = pDYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = pSTR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = pSTR_REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value = pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value = pEVENT_NAME},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_EN", Value = pCOLOR_NAME_EN},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_NO", Value = pDYE_MACHINE_NO},
                     
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = pUSER_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3003:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_REQ_HModel ob = new DYE_STR_REQ_HModel();
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    if (dr["LOAD_TIME"] != DBNull.Value)
                        ob.LOAD_TIME = Convert.ToDateTime(dr["LOAD_TIME"]);
                    //ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);

                    if (dr["UN_LOAD_TIME"] != DBNull.Value)
                        ob.UN_LOAD_TIME = Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    //ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);

                    if (pOption == 3004)
                    {
                        ob.IS_REQ_DONE = (dr["IS_REQ_DONE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["IS_REQ_DONE"]);                        
                    }

                    if (pOption == 3007)
                    {
                        ob.LK_RP_SRC_TYPE_ID = (dr["LK_RP_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_SRC_TYPE_ID"]);
                        ob.SOURCE_OF_REPROCESS = (dr["SOURCE_OF_REPROCESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SOURCE_OF_REPROCESS"]);
                        ob.SRC_OF_RP_ACTION = (dr["SRC_OF_RP_ACTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SRC_OF_RP_ACTION"]);                        
                    }
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.FROM_ST_NAME = (dr["FROM_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ST_NAME"]);
                    ob.TO_ST_NAME = (dr["TO_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ST_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.IS_ISSUED = (dr["IS_ISSUED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_ISSUED"]);
                    ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);

                    if (dr["COLOR_NAME_EN"] != DBNull.Value)
                        ob.COLOR_NAME_EN = Convert.ToString(dr["COLOR_NAME_EN"]);

                    if (dr["DYE_DC_ISS_H_ID"] != DBNull.Value)
                        ob.DYE_DC_ISS_H_ID = Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);

                    if (dr["PRE_DYE_STR_REQ_H_ID"] != DBNull.Value)
                        ob.PRE_DYE_STR_REQ_H_ID = Convert.ToInt64(dr["PRE_DYE_STR_REQ_H_ID"]);

                    if (dr["IS_ADDITION"] != DBNull.Value)
                        ob.IS_ADDITION = Convert.ToInt32(dr["IS_ADDITION"]);

                    if (dr["HAS_ADDITION"] != DBNull.Value)
                        ob.HAS_ADDITION = Convert.ToInt32(dr["HAS_ADDITION"]);

                    if (dr["IS_REPROCESS"] != DBNull.Value)
                        ob.IS_REPROCESS = Convert.ToInt32(dr["IS_REPROCESS"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    ob.TTL_RQD = (dr["TTL_RQD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RQD"]);
                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);
                    ob.BALANCE_QTY = ob.TTL_RQD - ob.TTL_ISS_QTY;
                    ob.IS_FINISHED = (dr["IS_FINISHED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_FINISHED"]);

                    ob.SUP_LN_REQ_NO = (dr["SUP_LN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_LN_REQ_NO"]);

                    if (pOption == 3011)
                    {
                        ob.ACT_PROD_DT = (dr["ACT_PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_PROD_DT"]);

                        if (dr["CHK_ROLL_DT"] != DBNull.Value)
                            ob.CHK_ROLL_DT = Convert.ToDateTime(dr["CHK_ROLL_DT"]);

                        if (dr["CK_ROLL_FIN_DT"] != DBNull.Value)
                            ob.CK_ROLL_FIN_DT = Convert.ToDateTime(dr["CK_ROLL_FIN_DT"]);

                        if (dr["CK_ROLL_RCV_DT"] != DBNull.Value)
                            ob.CK_ROLL_RCV_DT = Convert.ToDateTime(dr["CK_ROLL_RCV_DT"]);

                        if (dr["UN_HOLD_DT"] != DBNull.Value)
                            ob.LOAD_TIME = Convert.ToDateTime(dr["UN_HOLD_DT"]);

                        ob.IS_ROLL_OK = (dr["IS_ROLL_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ROLL_OK"]);

                        ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                        ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                        ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                        ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                        ob.CHQ_RL_STS_NAME = (dr["CHQ_RL_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHQ_RL_STS_NAME"]);
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                        ob.LK_CHQ_RL_STS_ID = (dr["LK_CHQ_RL_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_CHQ_RL_STS_ID"]);
                        ob.DYE_BT_PROD_ID = (dr["DYE_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PROD_ID"]);

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


        public List<DYE_STR_REQ_HModel> SelectAllQCBatch(int pageNo, int pageSize, string pFROM_DATE = null, string pTO_DATE = null, Int64? pMC_BYR_ACC_GRP_ID = null,
                                                        string pSTYLE_ORDER_NO = null, Int64? pMC_COLOR_ID = null, string pDYE_BATCH_NO = null, string pDT_TYPE_ID = null,
                                                        Int64? pLK_CHQ_RL_STS_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_dye_dc_issue.dye_str_req_h_select";
            try
            {
                var obList = new List<DYE_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value = pFROM_DATE},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value = pTO_DATE},
                     new CommandParameter() {ParameterName = "pDT_TYPE_ID", Value = pDT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_ORDER_NO", Value = pSTYLE_ORDER_NO},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pLK_CHQ_RL_STS_ID", Value = pLK_CHQ_RL_STS_ID},
                     
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = pUSER_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_REQ_HModel ob = new DYE_STR_REQ_HModel();
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    if (dr["LOAD_TIME"] != DBNull.Value)
                        ob.LOAD_TIME = Convert.ToDateTime(dr["LOAD_TIME"]);

                    if (dr["UN_LOAD_TIME"] != DBNull.Value)
                        ob.UN_LOAD_TIME = Convert.ToDateTime(dr["UN_LOAD_TIME"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.FROM_ST_NAME = (dr["FROM_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ST_NAME"]);
                    ob.TO_ST_NAME = (dr["TO_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ST_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.IS_ISSUED = (dr["IS_ISSUED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_ISSUED"]);
                    ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);

                    if (dr["COLOR_NAME_EN"] != DBNull.Value)
                        ob.COLOR_NAME_EN = Convert.ToString(dr["COLOR_NAME_EN"]);

                    if (dr["DYE_DC_ISS_H_ID"] != DBNull.Value)
                        ob.DYE_DC_ISS_H_ID = Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);

                    if (dr["PRE_DYE_STR_REQ_H_ID"] != DBNull.Value)
                        ob.PRE_DYE_STR_REQ_H_ID = Convert.ToInt64(dr["PRE_DYE_STR_REQ_H_ID"]);

                    if (dr["IS_ADDITION"] != DBNull.Value)
                        ob.IS_ADDITION = Convert.ToInt32(dr["IS_ADDITION"]);

                    if (dr["HAS_ADDITION"] != DBNull.Value)
                        ob.HAS_ADDITION = Convert.ToInt32(dr["HAS_ADDITION"]);

                    if (dr["IS_REPROCESS"] != DBNull.Value)
                        ob.IS_REPROCESS = Convert.ToInt32(dr["IS_REPROCESS"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    ob.TTL_RQD = (dr["TTL_RQD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RQD"]);
                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);
                    ob.BALANCE_QTY = ob.TTL_RQD - ob.TTL_ISS_QTY;
                    ob.IS_FINISHED = (dr["IS_FINISHED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_FINISHED"]);

                    ob.SUP_LN_REQ_NO = (dr["SUP_LN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_LN_REQ_NO"]);

                    if (pOption == 3011)
                    {
                        ob.ACT_PROD_DT = (dr["ACT_PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACT_PROD_DT"]);

                        if (dr["CHK_ROLL_DT"] != DBNull.Value)
                            ob.CHK_ROLL_DT = Convert.ToDateTime(dr["CHK_ROLL_DT"]);

                        if (dr["CK_ROLL_FIN_DT"] != DBNull.Value)
                            ob.CK_ROLL_FIN_DT = Convert.ToDateTime(dr["CK_ROLL_FIN_DT"]);

                        if (dr["CK_ROLL_RCV_DT"] != DBNull.Value)
                            ob.CK_ROLL_RCV_DT = Convert.ToDateTime(dr["CK_ROLL_RCV_DT"]);

                        if (dr["UN_HOLD_DT"] != DBNull.Value)
                            ob.LOAD_TIME = Convert.ToDateTime(dr["UN_HOLD_DT"]);

                        ob.IS_ROLL_OK = (dr["IS_ROLL_OK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ROLL_OK"]);

                        ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                        ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                        ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                        ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                        ob.CHQ_RL_STS_NAME = (dr["CHQ_RL_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHQ_RL_STS_NAME"]);
                        ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                        ob.LK_CHQ_RL_STS_ID = (dr["LK_CHQ_RL_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_CHQ_RL_STS_ID"]);
                        ob.DYE_BT_PROD_ID = (dr["DYE_BT_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_PROD_ID"]);

                        ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                        ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);

                        ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
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


        public List<DYE_STR_REQ_HModel> SelectRunningBatch(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pCOLOR_NAME_EN = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pDYE_BATCH_NO = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_dye_dc_issue.dye_str_req_h_select";
            try
            {
                var obList = new List<DYE_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = pDYE_STR_REQ_H_ID},
                     
                     new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = pSTR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = pSTR_REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value = pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value = pEVENT_NAME},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_EN", Value = pCOLOR_NAME_EN},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_NO", Value = pDYE_MACHINE_NO},
                     
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = pUSER_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_REQ_HModel ob = new DYE_STR_REQ_HModel();
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);

                    if (dr["LOAD_TIME"] != DBNull.Value)
                        ob.LOAD_TIME = Convert.ToDateTime(dr["LOAD_TIME"]);
                    //ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);

                    if (dr["UN_LOAD_TIME"] != DBNull.Value)
                        ob.UN_LOAD_TIME = Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    //ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.FROM_ST_NAME = (dr["FROM_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ST_NAME"]);
                    ob.TO_ST_NAME = (dr["TO_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ST_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.IS_ISSUED = (dr["IS_ISSUED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_ISSUED"]);
                    ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);

                    if (dr["COLOR_NAME_EN"] != DBNull.Value)
                        ob.COLOR_NAME_EN = Convert.ToString(dr["COLOR_NAME_EN"]);

                    if (dr["DYE_DC_ISS_H_ID"] != DBNull.Value)
                        ob.DYE_DC_ISS_H_ID = Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);

                    if (dr["PRE_DYE_STR_REQ_H_ID"] != DBNull.Value)
                        ob.PRE_DYE_STR_REQ_H_ID = Convert.ToInt64(dr["PRE_DYE_STR_REQ_H_ID"]);

                    if (dr["IS_ADDITION"] != DBNull.Value)
                        ob.IS_ADDITION = Convert.ToInt32(dr["IS_ADDITION"]);

                    if (dr["HAS_ADDITION"] != DBNull.Value)
                        ob.HAS_ADDITION = Convert.ToInt32(dr["HAS_ADDITION"]);

                    if (dr["IS_REPROCESS"] != DBNull.Value)
                        ob.IS_REPROCESS = Convert.ToInt32(dr["IS_REPROCESS"]);

                    //ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    ob.TTL_RQD = (dr["TTL_RQD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RQD"]);
                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);
                    ob.BALANCE_QTY = ob.TTL_RQD - ob.TTL_ISS_QTY;
                    ob.IS_FINISHED = (dr["IS_FINISHED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_FINISHED"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.SUP_LN_REQ_NO = (dr["SUP_LN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_LN_REQ_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_STR_REQ_HModel> LoanSelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pUSER_NAME_EN = null, string pEVENT_NAME = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null)
        {
            string sp = "pkg_dye_dc_issue.dye_str_req_h_select";
            try
            {
                var obList = new List<DYE_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = 0},
                     new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = pSTR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = pSTR_REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value = pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pUSER_NAME_EN", Value = pUSER_NAME_EN},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value = pEVENT_NAME},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = pUSER_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_REQ_HModel ob = new DYE_STR_REQ_HModel();
                    //ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    //ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    //ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    //ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    //ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    //ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    //ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    //ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    //ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    //ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    //ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    //ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    //ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    //ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    //ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    //ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    //ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    //ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    //ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    //ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    //ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    //ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    //ob.FROM_ST_NAME = (dr["FROM_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ST_NAME"]);
                    //ob.TO_ST_NAME = (dr["TO_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ST_NAME"]);
                    //ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    //ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    //ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    //ob.IS_ISSUED = (dr["IS_ISSUED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_ISSUED"]);
                    //ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);

                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.FROM_ST_NAME = (dr["FROM_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ST_NAME"]);
                    ob.TO_ST_NAME = (dr["TO_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ST_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.IS_ISSUED = (dr["IS_ISSUED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_ISSUED"]);
                    ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);

                    ob.SUP_LN_REQ_NO = (dr["SUP_LN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_LN_REQ_NO"]);

                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    ob.TTL_RQD = (dr["TTL_RQD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RQD"]);
                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);
                    ob.BALANCE_QTY = ob.TTL_RQD - ob.TTL_ISS_QTY;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_STR_REQ_HModel Select(Int64? pDYE_STR_REQ_H_ID, string pSTR_REQ_NO = null)
        {
            string sp = "pkg_dye_dc_issue.dye_str_req_h_select";
            try
            {
                var ob = new DYE_STR_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value =pDYE_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value =pSTR_REQ_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REQ_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.LOAD_TIME = (dr["LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_TIME"]);
                    ob.UN_LOAD_TIME = (dr["UN_LOAD_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["UN_LOAD_TIME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.DYE_RE_PROC_TYPE_ID = (dr["DYE_RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_RE_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.SCM_STORE_ID = (dr["REQ_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_STORE_ID"]);

                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.PICK_UP = (dr["PICK_UP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PICK_UP"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.SUP_LN_REQ_NO = (dr["SUP_LN_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_LN_REQ_NO"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.FROM_ST_NAME = (dr["FROM_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_ST_NAME"]);
                    ob.TO_ST_NAME = (dr["TO_ST_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_ST_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);
                    if (ob.LOAD_TIME > Convert.ToDateTime("2017-01-01") && ob.UN_LOAD_TIME > Convert.ToDateTime("2017-01-01"))
                        ob.IS_PRODUCTION = "Y";
                    else if (ob.LOAD_TIME > Convert.ToDateTime("2017-01-01") && ob.UN_LOAD_TIME < Convert.ToDateTime("2017-01-01"))
                        ob.IS_PRODUCTION = "L";
                    else
                        ob.IS_PRODUCTION = "N";

                    //ob.IS_ISSUED = (dr["IS_ISSUED"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_ISSUED"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<DYE_STR_REQ_HModel> SelectRunningBatch(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pLK_DIA_TYPE_ID = null, string pMONTHOF = null, Int64? pMC_STYLE_H_ID = null, Int64? pMC_COLOR_ID = null, string pFROM_DATE = null, Int64? pDF_PROC_TYPE_ID = null, Int64? pOption = null)
        {
            string sp = "PKG_DF_PRODUCTION.dye_bt_prod_dashboard_select";
            try
            {
                var obList = new List<DYE_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},                     
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value = pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value = pFROM_DATE},               

                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     //new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =1},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value =pLK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_ID", Value =pDF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMONTHOF", Value =pMONTHOF}, 

                     new CommandParameter() {ParameterName = "pOption", Value = pOption == null ? 3001 : pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_REQ_HModel ob = new DYE_STR_REQ_HModel();
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);

                    if (dr["LOAD_TIME"] != DBNull.Value)
                        ob.LOAD_TIME = Convert.ToDateTime(dr["LOAD_TIME"]);

                    if (dr["UN_LOAD_TIME"] != DBNull.Value)
                        ob.UN_LOAD_TIME = Convert.ToDateTime(dr["UN_LOAD_TIME"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.BATCH_QTY = (dr["BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_QTY"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    if (dr["COLOR_NAME_EN"] != DBNull.Value)
                        ob.COLOR_NAME_EN = Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);

                    if (dr["CHK_ROLL_DT"] != DBNull.Value)
                        ob.CHK_ROLL_DT = Convert.ToDateTime(dr["CHK_ROLL_DT"]);
                    if (dr["CK_ROLL_FIN_DT"] != DBNull.Value)
                        ob.CK_ROLL_FIN_DT = Convert.ToDateTime(dr["CK_ROLL_FIN_DT"]);
                    if (dr["CK_ROLL_RCV_DT"] != DBNull.Value)
                        ob.CK_ROLL_RCV_DT = Convert.ToDateTime(dr["CK_ROLL_RCV_DT"]);
                    if (dr["UN_HOLD_DT"] != DBNull.Value)
                        ob.UN_HOLD_DT = Convert.ToDateTime(dr["UN_HOLD_DT"]);

                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);
                    if (pOption == 3008)
                        ob.DF_PROC_TYPE_NAME = (dr["DF_PROC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TOTAL_REC { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string FROM_ST_NAME { get; set; }

        public string TO_ST_NAME { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public int IS_ISSUED { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public string STR_REQ_BY_NAME { get; set; }

        public object XML_REQ_D1 { get; set; }

        public object XML_REQ_D2 { get; set; }

        public long PERMISSION { get; set; }

        public decimal TTL_RQD { get; set; }

        public decimal TTL_ISS_QTY { get; set; }

        public decimal BALANCE_QTY { get; set; }

        public long DYE_DC_ISS_H_ID { get; set; }

        public int IS_ADDITION { get; set; }

        public string DYE_BATCH_NO { get; set; }

        public int IS_REPROCESS { get; set; }

        public int IS_FINISHED { get; set; }

        public int HAS_ADDITION { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string DYE_MACHINE_NO { get; set; }

        public string IS_PRODUCTION { get; set; }

        public decimal BATCH_QTY { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string ORDER_NO { get; set; }

        public string STYLE_NO { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public long SCM_STORE_ID { get; set; }

        public string DYE_LOT_NO { get; set; }

        public string FIN_GSM { get; set; }

        public DateTime? UN_HOLD_DT { get; set; }

        public DateTime? CK_ROLL_RCV_DT { get; set; }

        public DateTime? CK_ROLL_FIN_DT { get; set; }

        public DateTime? CHK_ROLL_DT { get; set; }

        public string BT_STS_TYP_NAME { get; set; }

        public DateTime ACT_PROD_DT { get; set; }

        public string IS_ROLL_OK { get; set; }

        public string CHQ_RL_STS_NAME { get; set; }

        public string IS_FINALIZED { get; set; }

        public int LK_CHQ_RL_STS_ID { get; set; }

        public long DYE_BT_PROD_ID { get; set; }

        public string FAB_TYPE_SNAME { get; set; }

        public string DF_PROC_TYPE_LST { get; set; }

        public string RQD_GSM { get; set; }

        public string DF_PROC_TYPE_NAME { get; set; }

        public long IS_REQ_DONE { get; set; }

        public Int64? DYE_BT_CARD_H_ID { get; set; }
        public Int64? PICK_UP { get; set; }

        public long LK_RP_SRC_TYPE_ID { get; set; }

        public string SOURCE_OF_REPROCESS { get; set; }

        public string SRC_OF_RP_ACTION { get; set; }

        public Int64? DF_RIB_SHADE_RPT_H_ID { get; set; }
    }
}