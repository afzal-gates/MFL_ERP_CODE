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
    public class DF_SC_BT_CHL_ISS_HModel
    {
        public Int64 DF_SC_BT_CHL_ISS_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 ISS_STORE_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public Int64? LK_CHLN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }


        public string COMP_NAME_EN { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string SC_PRG_STS_NAME { get; set; }
        public string DYE_BATCH_NO { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string EVENT_NAME { get; set; }
        public string NXT_ACTION_NAME { get; set; }
        public long PERMISSION { get; set; }
        public string ACTN_ROLE_FLAG { get; set; }
        public string IS_ALT_PARTY { get; set; }
        public string XML_ISS_D { get; set; }
        public string IS_AOP { get; set; }

        public string Save()
        {
            const string sp = "PKG_DF_SC_PROGRAM.DF_SC_BT_CHL_ISS_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_BT_CHL_ISS_H_ID", Value = ob.DF_SC_BT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pLK_CHLN_STATUS_ID", Value = ob.LK_CHLN_STATUS_ID==null?1: ob.LK_CHLN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_AOP", Value = ob.IS_AOP},
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
            const string sp = "SP_DF_SC_BT_CHL_ISS_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_BT_CHL_ISS_H_ID", Value = ob.DF_SC_BT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pLK_CHLN_STATUS_ID", Value = ob.LK_CHLN_STATUS_ID},
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
            const string sp = "SP_DF_SC_BT_CHL_ISS_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_BT_CHL_ISS_H_ID", Value = ob.DF_SC_BT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pLK_CHLN_STATUS_ID", Value = ob.LK_CHLN_STATUS_ID},
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


        public List<DF_SC_BT_CHL_ISS_HModel> SelectAll(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, string pCHALAN_NO = null, string pCHALAN_DT = null, Int64? pUSER_ID = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_SC_BT_CHL_ISS_H_SELECT";
            try
            {
                var obList = new List<DF_SC_BT_CHL_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value =pCHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value =pCHALAN_DT},
                     //new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     //new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value =pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =pUSER_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_BT_CHL_ISS_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_BT_CHL_ISS_HModel ob = new DF_SC_BT_CHL_ISS_HModel();
                    ob.DF_SC_BT_CHL_ISS_H_ID = (dr["DF_SC_BT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_BT_CHL_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.LK_CHLN_STATUS_ID = (dr["LK_CHLN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHLN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_AOP"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.SC_PRG_STS_NAME = (dr["SC_PRG_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_STS_NAME"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SC_BT_CHL_ISS_HModel SelectByID(Int64? pDF_SC_BT_CHL_ISS_H_ID = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_SC_BT_CHL_ISS_H_SELECT";
            try
            {
                var ob = new DF_SC_BT_CHL_ISS_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_BT_CHL_ISS_H_ID", Value =pDF_SC_BT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_BT_CHL_ISS_H_ID = (dr["DF_SC_BT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_BT_CHL_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.LK_CHLN_STATUS_ID = (dr["LK_CHLN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHLN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_AOP"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_SC_BT_CHL_ISS_HModel> SelectByPartyID(Int64? pSCM_SUPPLIER_ID = null, string pIS_AOP = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_SC_BT_CHL_ISS_H_SELECT";
            try
            {
                var obList = new List<DF_SC_BT_CHL_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pIS_AOP", Value =pIS_AOP},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_BT_CHL_ISS_HModel ob = new DF_SC_BT_CHL_ISS_HModel();
                    ob.DF_SC_BT_CHL_ISS_H_ID = (dr["DF_SC_BT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_BT_CHL_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.GATE_PASS_NO = (dr["GATE_PASS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GATE_PASS_NO"]);
                    ob.VEHICLE_NO = (dr["VEHICLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VEHICLE_NO"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.LK_CHLN_STATUS_ID = (dr["LK_CHLN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_CHLN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_AOP"]);

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

    }
}