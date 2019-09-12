using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Purchase
{
    public class SCM_SUP_CPModel
    {
        public Int64 SCM_SUP_CP_ID { get; set; }
        public Int64 SCM_SUP_ADDRESS_ID { get; set; }
        public Int64 LK_CP_TYPE_ID { get; set; }

        [Required(ErrorMessage = "Please insert name")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string CP_NAME_EN { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string CP_NAME_BN { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string CP_DESIG { get; set; }

        [MaxLength(20, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string CP_WORK_PHONE { get; set; }
        [MaxLength(20, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string CP_MOB_PHONE { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can't be longer than {1} characters")]
        public string CP_EMAIL_ID { get; set; }
        public string SUP_OFFICE_NAME { get; set; }

        public string IS_DEFAULT { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }


        public string Save()
        {
            const string sp = "SP_SCM_SUP_CP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUP_CP_ID", Value = ob.SCM_SUP_CP_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUP_ADDRESS_ID", Value = ob.SCM_SUP_ADDRESS_ID},
                     new CommandParameter() {ParameterName = "pLK_CP_TYPE_ID", Value = ob.LK_CP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCP_NAME_EN", Value = ob.CP_NAME_EN},
                     new CommandParameter() {ParameterName = "pCP_NAME_BN", Value = ob.CP_NAME_BN},
                     new CommandParameter() {ParameterName = "pCP_DESIG", Value = ob.CP_DESIG},
                     new CommandParameter() {ParameterName = "pCP_WORK_PHONE", Value = ob.CP_WORK_PHONE},
                     new CommandParameter() {ParameterName = "pCP_MOB_PHONE", Value = ob.CP_MOB_PHONE},
                     new CommandParameter() {ParameterName = "pCP_EMAIL_ID", Value = ob.CP_EMAIL_ID},
                     new CommandParameter() {ParameterName = "pIS_DEFAULT", Value = ob.IS_DEFAULT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_SCM_SUP_CP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUP_CP_ID", Value = ob.SCM_SUP_CP_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUP_ADDRESS_ID", Value = ob.SCM_SUP_ADDRESS_ID},
                     new CommandParameter() {ParameterName = "pLK_CP_TYPE_ID", Value = ob.LK_CP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCP_NAME_EN", Value = ob.CP_NAME_EN},
                     new CommandParameter() {ParameterName = "pCP_NAME_BN", Value = ob.CP_NAME_BN},
                     new CommandParameter() {ParameterName = "pCP_DESIG", Value = ob.CP_DESIG},
                     new CommandParameter() {ParameterName = "pCP_WORK_PHONE", Value = ob.CP_WORK_PHONE},
                     new CommandParameter() {ParameterName = "pCP_MOB_PHONE", Value = ob.CP_MOB_PHONE},
                     new CommandParameter() {ParameterName = "pCP_EMAIL_ID", Value = ob.CP_EMAIL_ID},
                     new CommandParameter() {ParameterName = "pIS_DEFAULT", Value = ob.IS_DEFAULT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_SCM_SUP_CP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUP_CP_ID", Value = ob.SCM_SUP_CP_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUP_ADDRESS_ID", Value = ob.SCM_SUP_ADDRESS_ID},
                     new CommandParameter() {ParameterName = "pLK_CP_TYPE_ID", Value = ob.LK_CP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCP_NAME_EN", Value = ob.CP_NAME_EN},
                     new CommandParameter() {ParameterName = "pCP_NAME_BN", Value = ob.CP_NAME_BN},
                     new CommandParameter() {ParameterName = "pCP_DESIG", Value = ob.CP_DESIG},
                     new CommandParameter() {ParameterName = "pCP_WORK_PHONE", Value = ob.CP_WORK_PHONE},
                     new CommandParameter() {ParameterName = "pCP_MOB_PHONE", Value = ob.CP_MOB_PHONE},
                     new CommandParameter() {ParameterName = "pCP_EMAIL_ID", Value = ob.CP_EMAIL_ID},
                     new CommandParameter() {ParameterName = "pIS_DEFAULT", Value = ob.IS_DEFAULT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<SCM_SUP_CPModel> SelectAll()
        {
            string sp = "Select_SCM_SUP_CP";
            try
            {
                var obList = new List<SCM_SUP_CPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUP_CP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SUP_CPModel ob = new SCM_SUP_CPModel();
                    ob.SCM_SUP_CP_ID = (dr["SCM_SUP_CP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUP_CP_ID"]);
                    ob.SCM_SUP_ADDRESS_ID = (dr["SCM_SUP_ADDRESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUP_ADDRESS_ID"]);
                    ob.LK_CP_TYPE_ID = (dr["LK_CP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CP_TYPE_ID"]);
                    ob.CP_NAME_EN = (dr["CP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_NAME_EN"]);
                    ob.CP_NAME_BN = (dr["CP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_NAME_BN"]);
                    ob.CP_DESIG = (dr["CP_DESIG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_DESIG"]);
                    ob.CP_WORK_PHONE = (dr["CP_WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_WORK_PHONE"]);
                    ob.CP_MOB_PHONE = (dr["CP_MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_MOB_PHONE"]);
                    ob.CP_EMAIL_ID = (dr["CP_EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_EMAIL_ID"]);
                    ob.IS_DEFAULT = (dr["IS_DEFAULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFAULT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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

        public List<SCM_SUP_CPModel> Select(Int64? ID)
        {
            string sp = "pkg_pur_supplier.scm_sup_cp_select";
            try
            {
                var ob = new SCM_SUP_CPModel();
                var List = new List<SCM_SUP_CPModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =ID==null?0:ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob = new SCM_SUP_CPModel();
                    ob.SCM_SUP_CP_ID = (dr["SCM_SUP_CP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUP_CP_ID"]);
                    ob.SCM_SUP_ADDRESS_ID = (dr["SCM_SUP_ADDRESS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUP_ADDRESS_ID"]);
                    ob.LK_CP_TYPE_ID = (dr["LK_CP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CP_TYPE_ID"]);
                    ob.CP_NAME_EN = (dr["CP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_NAME_EN"]);
                    ob.SUP_OFFICE_NAME = (dr["SUP_OFFICE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_OFFICE_NAME"]);
                    
                    ob.CP_NAME_BN = (dr["CP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_NAME_BN"]);
                    ob.CP_DESIG = (dr["CP_DESIG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_DESIG"]);
                    ob.CP_WORK_PHONE = (dr["CP_WORK_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_WORK_PHONE"]);
                    ob.CP_MOB_PHONE = (dr["CP_MOB_PHONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_MOB_PHONE"]);
                    ob.CP_EMAIL_ID = (dr["CP_EMAIL_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CP_EMAIL_ID"]);
                    ob.IS_DEFAULT = (dr["IS_DEFAULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFAULT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    List.Add(ob);
                }
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
