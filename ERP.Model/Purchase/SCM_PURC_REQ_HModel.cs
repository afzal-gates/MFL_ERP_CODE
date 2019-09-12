using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERP.Model.Purchase
{
    public class SCM_PURC_REQ_HModel
    {
        public Int64 SCM_PURC_REQ_H_ID { get; set; }
        public string PURC_REQ_NO { get; set; }
        public DateTime PURC_REQ_DT { get; set; }
        public string INV_ITEM_CAT_LST { get; set; }
        public Int64 PURC_REQ_BY { get; set; }
        public Int64 PURC_REQ_TO { get; set; }
        public string REQ_ATTN_MAIL { get; set; }
        public Int64 LK_REQ_TYPE_ID { get; set; }

        public Int64 LK_REQ_SRC_ID { get; set; }
        public Int64 LK_LOC_SRC_TYPE_ID { get; set; }
        public Int64 LK_PAY_MTHD_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 LK_PRIORITY_ID { get; set; }
        public string REMARKS { get; set; }
        public Int64 LK_REQ_STATUS_ID { get; set; }


        public string REQ_TYPE_NAME { get; set; }
        public string REQ_SOURCE_NAME_EN { get; set; }
        public string REQ_STATUS_NAME_EN { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string REQ_PRIORITY_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string XML_REQ_D { get; set; }

        public string Save()
        {
            const string sp = "pkg_pur_req.scm_purc_req_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = ob.SCM_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pPURC_REQ_NO", Value = ob.PURC_REQ_NO},
                     new CommandParameter() {ParameterName = "pPURC_REQ_DT", Value = ob.PURC_REQ_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pPURC_REQ_BY", Value = ob.PURC_REQ_BY},
                     new CommandParameter() {ParameterName = "pPURC_REQ_TO", Value = ob.PURC_REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pLK_REQ_TYPE_ID", Value = ob.LK_REQ_TYPE_ID},
             
                     new CommandParameter() {ParameterName = "pLK_REQ_SRC_ID", Value = ob.LK_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID==0?1:ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_PAY_MTHD_ID", Value = ob.LK_PAY_MTHD_ID==0?1:ob.LK_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},

                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = DateTime.Now},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = DateTime.Now},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pREMARKS",  Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pLK_REQ_STATUS_ID", Value = ob.LK_REQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},                   
                     new CommandParameter() {ParameterName = "pOption", Value =1000},                     
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opSCM_PURC_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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

        public List<SCM_PURC_REQ_HModel> SelectAll()
        {
            string sp = "pkg_pur_req.scm_purc_req_select";
            try
            {
                var obList = new List<SCM_PURC_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_PURC_REQ_HModel ob = new SCM_PURC_REQ_HModel();
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);

                    ob.PURC_REQ_BY = (dr["PURC_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_BY"]);
                    ob.PURC_REQ_TO = (dr["PURC_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_TO"]);

                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);

                    ob.LK_REQ_TYPE_ID = (dr["LK_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_TYPE_ID"]);
                    ob.LK_REQ_SRC_ID = (dr["LK_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_SRC_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.LK_PAY_MTHD_ID = (dr["LK_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PAY_MTHD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);

                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.REQ_SOURCE_NAME_EN = (dr["REQ_SOURCE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_SOURCE_NAME_EN"]);
                    ob.REQ_PRIORITY_NAME_EN = (dr["REQ_PRIORITY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_PRIORITY_NAME_EN"]);
                    ob.REQ_STATUS_NAME_EN = (dr["REQ_STATUS_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_STATUS_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);

                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);



                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SCM_PURC_REQ_HModel Select(Int64? ID)
        {
            string sp = "pkg_pur_req.scm_purc_req_select";
            try
            {
                if (ID == null)
                    ID = 0;
                var ob = new SCM_PURC_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_PURC_REQ_H_ID = (dr["SCM_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_H_ID"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);

                    ob.PURC_REQ_BY = (dr["PURC_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_BY"]);
                    ob.PURC_REQ_TO = (dr["PURC_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURC_REQ_TO"]);

                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);

                    ob.LK_REQ_TYPE_ID = (dr["LK_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_TYPE_ID"]);
                    ob.LK_REQ_SRC_ID = (dr["LK_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_SRC_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.LK_PAY_MTHD_ID = (dr["LK_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PAY_MTHD_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);

                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.REQ_SOURCE_NAME_EN = (dr["REQ_SOURCE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_SOURCE_NAME_EN"]);
                    ob.REQ_PRIORITY_NAME_EN = (dr["REQ_PRIORITY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_PRIORITY_NAME_EN"]);
                    ob.REQ_STATUS_NAME_EN = (dr["REQ_STATUS_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_STATUS_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);

                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string ITEM_NAME_LIST { get; set; }
    }
}
