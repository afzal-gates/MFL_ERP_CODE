using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Web;

namespace ERP.Model
{
    public class MC_STYL_CLCF_MEASModel
    {
        public Int64 MC_STYL_CLCF_MEAS_ID { get; set; }
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64 RF_GARM_PART_ID { get; set; }
        public Int64 MC_SIZE_ID { get; set; }
        public decimal? MEAS_LENGTH { get; set; }
        public decimal? MEAS_WIDTH { get; set; }
        public string MESUREMENT { get; set; }
        public Int64 RF_MOU_ID { get; set; }
        public Int64? REVISION_NO { get; set; }
        public DateTime? REVISION_DT { get; set; }
        public string REASON_DESC { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public Int64? CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64? LAST_UPDATED_BY { get; set; }

        public string SIZE_CODE { get; set; }
        public string IS_FINALIZED { get; set; }
        public string XML_COLLERCUFF { get; set; }
        public Int64? IS_REVIZED { get; set; }



        public string Save()
        {
            const string sp = "pkg_merchandising.mc_styl_clcf_meas_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pXML_COLLERCUFF", Value = ob.XML_COLLERCUFF},
                    new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                    new CommandParameter() {ParameterName = "pIS_REVIZED", Value = ob.IS_REVIZED},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = ""},
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
            const string sp = "pkg_pur_supplier.acc_bk_account_supplier_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_STYL_CLCF_MEAS_ID", Value = ob.MC_STYL_CLCF_MEAS_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                    new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                    new CommandParameter() {ParameterName = "pMEAS_LENGTH", Value = ob.MEAS_LENGTH},
                    new CommandParameter() {ParameterName = "pMEAS_WIDTH", Value = ob.MEAS_WIDTH},
                    new CommandParameter() {ParameterName = "pMESUREMENT", Value = ob.MESUREMENT},
                    new CommandParameter() {ParameterName = "pRF_MOU_ID", Value = ob.RF_MOU_ID},
                    new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                    new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                    new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
                    new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                    new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},


                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
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
            const string sp = "pkg_pur_supplier.acc_bk_account_supplier_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_STYL_CLCF_MEAS_ID", Value = ob.MC_STYL_CLCF_MEAS_ID},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                    new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                    new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = ob.MC_SIZE_ID},
                    new CommandParameter() {ParameterName = "pMEAS_LENGTH", Value = ob.MEAS_LENGTH},
                    new CommandParameter() {ParameterName = "pMEAS_WIDTH", Value = ob.MEAS_WIDTH},
                    new CommandParameter() {ParameterName = "pMESUREMENT", Value = ob.MESUREMENT},
                    new CommandParameter() {ParameterName = "pRF_MOU_ID", Value = ob.RF_MOU_ID},
                    new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                    new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                    new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
                    new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                    new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},


                    new CommandParameter() {ParameterName = "pOption", Value = 4000},
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

        public List<MC_STYL_CLCF_MEASModel> GetStyleSizeList(Int64? pMC_STYLE_H_ID, string pMC_SIZE_ID)
        {
            string sp = "pkg_merchandising.mc_style_size_select";
            try
            {
                var obList = new List<MC_STYL_CLCF_MEASModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_SIZE_ID", Value = pMC_SIZE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYL_CLCF_MEASModel ob = new MC_STYL_CLCF_MEASModel();
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);
                    if (dr["RF_GARM_PART_ID"] != null)
                    { ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]); }
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    if (dr["MEAS_LENGTH"].ToString() != "")
                        ob.MEAS_LENGTH = (dr["MEAS_LENGTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MEAS_LENGTH"]);
                    if (dr["MEAS_WIDTH"].ToString() != "")
                        ob.MEAS_WIDTH = (dr["MEAS_WIDTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MEAS_WIDTH"]);

                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.MC_STYL_CLCF_MEAS_ID = (dr["MC_STYL_CLCF_MEAS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_CLCF_MEAS_ID"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYL_CLCF_MEASModel> SelectAll()
        {
            string sp = "pkg_pur_supplier.acc_bk_account_supplier_select";
            try
            {
                var obList = new List<MC_STYL_CLCF_MEASModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_BK_ACCOUNT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYL_CLCF_MEASModel ob = new MC_STYL_CLCF_MEASModel();
                    ob.MC_STYL_CLCF_MEAS_ID = (dr["MC_STYL_CLCF_MEAS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_CLCF_MEAS_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.MEAS_LENGTH = (dr["MEAS_LENGTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MEAS_LENGTH"]);
                    ob.MEAS_WIDTH = (dr["MEAS_WIDTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MEAS_WIDTH"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.RF_MOU_ID = (dr["RF_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MOU_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
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

        public MC_STYL_CLCF_MEASModel Select(long ID)
        {
            string sp = "pkg_pur_supplier.acc_bk_account_supplier_select";
            try
            {
                var obList = new List<MC_STYL_CLCF_MEASModel>();
                MC_STYL_CLCF_MEASModel ob = new MC_STYL_CLCF_MEASModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pACC_BK_ACCOUNT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_STYL_CLCF_MEAS_ID = (dr["MC_STYL_CLCF_MEAS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYL_CLCF_MEAS_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.MEAS_LENGTH = (dr["MEAS_LENGTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MEAS_LENGTH"]);
                    ob.MEAS_WIDTH = (dr["MEAS_WIDTH"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MEAS_WIDTH"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.RF_MOU_ID = (dr["RF_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MOU_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
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

    }
}