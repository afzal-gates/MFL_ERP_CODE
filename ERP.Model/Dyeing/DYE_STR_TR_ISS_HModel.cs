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
    public class DYE_STR_TR_ISS_HModel
    {
        public Int64 DYE_STR_TR_ISS_H_ID { get; set; }
        public Int64 DYE_STR_TR_REQ_H_ID { get; set; }
        public Int64 ITEM_ISS_BY { get; set; }
        public string ISS_REF_NO { get; set; }
        public DateTime ISS_REF_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string CARRIER_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 TO_STORE_ID { get; set; }
        public string XML_ISS_D { get; set; }
        public string USER_NAME_EN { get; set; }
        public Int64 FRM_STORE_ID { get; set; }

        public string Save()
        {
            const string sp = "pkg_dye_str_tf.dye_str_tr_iss_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_ISS_H_ID", Value = ob.DYE_STR_TR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ob.DYE_STR_TR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_ISS_BY", Value = ob.ITEM_ISS_BY==0?HttpContext.Current.Session["multiScUserId"]:ob.ITEM_ISS_BY},
                     new CommandParameter() {ParameterName = "pISS_REF_NO", Value = ob.ISS_REF_NO},
                     new CommandParameter() {ParameterName = "pISS_REF_DT", Value = ob.ISS_REF_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NO", Value = ob.CARRIER_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     
                     new CommandParameter() {ParameterName = "pFRM_STORE_ID", Value = ob.FRM_STORE_ID},                     
                     new CommandParameter() {ParameterName = "pTO_STORE_ID", Value = ob.TO_STORE_ID},                     
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_ISS_D", Value = ob.XML_ISS_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
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
            const string sp = "SP_DYE_STR_TR_ISS_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_ISS_H_ID", Value = ob.DYE_STR_TR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ob.DYE_STR_TR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_ISS_BY", Value = ob.ITEM_ISS_BY},
                     new CommandParameter() {ParameterName = "pISS_REF_NO", Value = ob.ISS_REF_NO},
                     new CommandParameter() {ParameterName = "pISS_REF_DT", Value = ob.ISS_REF_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NO", Value = ob.CARRIER_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
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
            const string sp = "SP_DYE_STR_TR_ISS_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_ISS_H_ID", Value = ob.DYE_STR_TR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ob.DYE_STR_TR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pITEM_ISS_BY", Value = ob.ITEM_ISS_BY},
                     new CommandParameter() {ParameterName = "pISS_REF_NO", Value = ob.ISS_REF_NO},
                     new CommandParameter() {ParameterName = "pISS_REF_DT", Value = ob.ISS_REF_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NO", Value = ob.CARRIER_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
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

        public List<DYE_STR_TR_ISS_HModel> SelectAll()
        {
            string sp = "Select_DYE_STR_TR_ISS_H";
            try
            {
                var obList = new List<DYE_STR_TR_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_ISS_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_TR_ISS_HModel ob = new DYE_STR_TR_ISS_HModel();
                    ob.DYE_STR_TR_ISS_H_ID = (dr["DYE_STR_TR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_ISS_H_ID"]);
                    ob.DYE_STR_TR_REQ_H_ID = (dr["DYE_STR_TR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_REQ_H_ID"]);
                    ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                    ob.ISS_REF_NO = (dr["ISS_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REF_NO"]);
                    ob.ISS_REF_DT = (dr["ISS_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_REF_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.CARRIER_NO = (dr["CARRIER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
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

        public DYE_STR_TR_ISS_HModel Select(Int64? ID)
        {
            string sp = "pkg_dye_str_tf.dye_str_tr_iss_h_select";
            try
            {
                var ob = new DYE_STR_TR_ISS_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_ISS_H_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_STR_TR_ISS_H_ID = (dr["DYE_STR_TR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_ISS_H_ID"]);
                    ob.DYE_STR_TR_REQ_H_ID = (dr["DYE_STR_TR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_REQ_H_ID"]);
                    ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                    ob.ISS_REF_NO = (dr["ISS_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REF_NO"]);
                    ob.ISS_REF_DT = (dr["ISS_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_REF_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.CARRIER_NO = (dr["CARRIER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.STR_TR_REQ_NO = (dr["STR_TR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_TR_REQ_NO"]);
                    ob.STR_TR_REQ_DT = (dr["STR_TR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_TR_REQ_DT"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                     ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.FRM_STORE_ID = (dr["FRM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_STORE_ID"]);
                    ob.TO_STORE_ID = (dr["TO_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_STORE_ID"]);
                    ob.REQ_REMARKS = (dr["REQ_REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_REMARKS"]);
                    
                    if (dr["B_DISABLE"] != DBNull.Value)
                        ob.B_DISABLE = (dr["B_DISABLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["B_DISABLE"]);
                    //HR_COMPANY_ID, HR_DEPARTMENT_ID, STR_TR_REQ_NO, STR_TR_REQ_DT, INV_ITEM_CAT_LST, STR_REQ_BY, STR_REQ_TO, REQ_ATTN_MAIL, RF_REQ_TYPE_ID, FRM_STORE_ID, TO_STORE_ID, REQ_REMARKS, CHALAN_NO, CHALAN_DT, ISS_REMARKS
                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_STR_TR_ISS_HModel> GetDyeStrTrIssue(Int64? ID)
        {
            string sp = "pkg_dye_str_tf.dye_str_tr_iss_h_select";
            try
            {
                var obList = new List<DYE_STR_TR_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_STR_TR_REQ_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_TR_ISS_HModel ob = new DYE_STR_TR_ISS_HModel();
                    ob.DYE_STR_TR_ISS_H_ID = (dr["DYE_STR_TR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_ISS_H_ID"]);
                    ob.DYE_STR_TR_REQ_H_ID = (dr["DYE_STR_TR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_REQ_H_ID"]);
                    ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                    ob.ISS_REF_NO = (dr["ISS_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REF_NO"]);
                    ob.ISS_REF_DT = (dr["ISS_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_REF_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.CARRIER_NO = (dr["CARRIER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_STR_TR_ISS_HModel> getStoreIssueListByRefNo(string ISS_REF_NO)
        {
            string sp = "pkg_dye_str_tf.dye_str_tr_iss_h_select";
            try
            {
                var obList = new List<DYE_STR_TR_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pISS_REF_NO", Value = ISS_REF_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_STR_TR_ISS_HModel ob = new DYE_STR_TR_ISS_HModel();
                    ob.DYE_STR_TR_ISS_H_ID = (dr["DYE_STR_TR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_ISS_H_ID"]);
                    ob.DYE_STR_TR_REQ_H_ID = (dr["DYE_STR_TR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_TR_REQ_H_ID"]);
                    ob.ITEM_ISS_BY = (dr["ITEM_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_ISS_BY"]);
                    ob.ISS_REF_NO = (dr["ISS_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REF_NO"]);
                    ob.ISS_REF_DT = (dr["ISS_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_REF_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.CARRIER_NO = (dr["CARRIER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long HR_COMPANY_ID { get; set; }

        public long HR_DEPARTMENT_ID { get; set; }

        public string STR_TR_REQ_NO { get; set; }

        public DateTime STR_TR_REQ_DT { get; set; }

        public string INV_ITEM_CAT_LST { get; set; }

        public long STR_REQ_BY { get; set; }

        public long STR_REQ_TO { get; set; }

        public long RF_REQ_TYPE_ID { get; set; }

        public string REQ_REMARKS { get; set; }

        public long B_DISABLE { get; set; }
    }
}