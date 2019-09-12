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
    public class DYE_MACHINEModel
    {
        public Int64 DYE_MACHINE_ID { get; set; }
        public string DYE_MACHINE_NO { get; set; }
        public Int64 DYE_MC_TYPE_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public string MODEL_NO { get; set; }
        public Int64 C_ORIGIN_ID { get; set; }
        public Decimal D_PRD_CAPACITY { get; set; }
        public Int64 CAP_MOU_ID { get; set; }
        public Decimal PCT_EFFCNCY { get; set; }
        public string MC_SERIAL_NO { get; set; }
        public Int64 MFG_YEAR { get; set; }
        public Int64 LK_MAC_STATUS_ID { get; set; }
        public Int64 MC_RPM { get; set; }
        public Int64 NO_NOZLE { get; set; }
        public Int64 MAX_TEMP_C { get; set; }
        public string IS_SMP_BLK { get; set; }
        public Int64? RF_LOCATION_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? RF_MAC_PROD_STS_ID { get; set; }
        public Int64? HR_PROD_FLR_ID { get; set; }
        public Int64? MC_VERSON_NO { get; set; }
        

        private List<DYE_BATCH_PLANModel> _dyePrograms = null;
        public List<DYE_BATCH_PLANModel> dyePrograms
        {
            get
            {
                if (_dyePrograms == null)
                {
                    _dyePrograms = new List<DYE_BATCH_PLANModel>();
                }
                return _dyePrograms;
            }
            set
            {
                _dyePrograms = value;
            }
        }

        public string Save()
        {
            const string sp = "pkg_dye_common.dye_machine_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_NO", Value = ob.DYE_MACHINE_NO},
                     new CommandParameter() {ParameterName = "pDYE_MC_TYPE_ID", Value = ob.DYE_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pD_PRD_CAPACITY", Value = ob.D_PRD_CAPACITY},
                     new CommandParameter() {ParameterName = "pCAP_MOU_ID", Value = ob.CAP_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCT_EFFCNCY", Value = ob.PCT_EFFCNCY},
                     new CommandParameter() {ParameterName = "pMC_SERIAL_NO", Value = ob.MC_SERIAL_NO},
                     new CommandParameter() {ParameterName = "pMFG_YEAR", Value = ob.MFG_YEAR},
                     new CommandParameter() {ParameterName = "pLK_MAC_STATUS_ID", Value = ob.LK_MAC_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_MAC_PROD_STS_ID", Value = ob.RF_MAC_PROD_STS_ID},
                     new CommandParameter() {ParameterName = "pMC_RPM", Value = ob.MC_RPM},
                     new CommandParameter() {ParameterName = "pNO_NOZLE", Value = ob.NO_NOZLE},
                     new CommandParameter() {ParameterName = "pMAX_TEMP_C", Value = ob.MAX_TEMP_C},
                     new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value = ob.IS_SMP_BLK},
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pMC_VERSON_NO", Value = ob.MC_VERSON_NO},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opDYE_MACHINE_ID", Value =0, Direction = ParameterDirection.Output},
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
            const string sp = "PKG_DYE_COMMON.dye_machine_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_NO", Value = ob.DYE_MACHINE_NO},
                     new CommandParameter() {ParameterName = "pDYE_MC_TYPE_ID", Value = ob.DYE_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pD_PRD_CAPACITY", Value = ob.D_PRD_CAPACITY},
                     new CommandParameter() {ParameterName = "pCAP_MOU_ID", Value = ob.CAP_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCT_EFFCNCY", Value = ob.PCT_EFFCNCY},
                     new CommandParameter() {ParameterName = "pMC_SERIAL_NO", Value = ob.MC_SERIAL_NO},
                     new CommandParameter() {ParameterName = "pMFG_YEAR", Value = ob.MFG_YEAR},
                     new CommandParameter() {ParameterName = "pLK_MAC_STATUS_ID", Value = ob.LK_MAC_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_RPM", Value = ob.MC_RPM},
                     new CommandParameter() {ParameterName = "pNO_NOZLE", Value = ob.NO_NOZLE},
                     new CommandParameter() {ParameterName = "pMAX_TEMP_C", Value = ob.MAX_TEMP_C},
                     new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value = ob.IS_SMP_BLK},
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pMC_VERSON_NO", Value = ob.MC_VERSON_NO},
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

        public string Delete()
        {
            const string sp = "PKG_DYE_COMMON.dye_machine_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value = ob.DYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_NO", Value = ob.DYE_MACHINE_NO},
                     new CommandParameter() {ParameterName = "pDYE_MC_TYPE_ID", Value = ob.DYE_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pD_PRD_CAPACITY", Value = ob.D_PRD_CAPACITY},
                     new CommandParameter() {ParameterName = "pCAP_MOU_ID", Value = ob.CAP_MOU_ID},
                     new CommandParameter() {ParameterName = "pPCT_EFFCNCY", Value = ob.PCT_EFFCNCY},
                     new CommandParameter() {ParameterName = "pMC_SERIAL_NO", Value = ob.MC_SERIAL_NO},
                     new CommandParameter() {ParameterName = "pMFG_YEAR", Value = ob.MFG_YEAR},
                     new CommandParameter() {ParameterName = "pLK_MAC_STATUS_ID", Value = ob.LK_MAC_STATUS_ID},
                     new CommandParameter() {ParameterName = "pMC_RPM", Value = ob.MC_RPM},
                     new CommandParameter() {ParameterName = "pNO_NOZLE", Value = ob.NO_NOZLE},
                     new CommandParameter() {ParameterName = "pMAX_TEMP_C", Value = ob.MAX_TEMP_C},
                     new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value = ob.IS_SMP_BLK},
                     new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
                     new CommandParameter() {ParameterName = "pMC_VERSON_NO", Value = ob.MC_VERSON_NO},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<DYE_MACHINEModel> SelectAll()
        {
            //string sp = "pkg_dye_dc_issue.dye_machine_select";
            string sp = "pkg_dye_common.dye_machine_select";
            try
            {
                var obList = new List<DYE_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_MACHINEModel ob = new DYE_MACHINEModel();
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.DYE_MC_TYPE_ID = (dr["DYE_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    ob.CAP_MOU_ID = (dr["CAP_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAP_MOU_ID"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RPM"]);
                    ob.NO_NOZLE = (dr["NO_NOZLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_NOZLE"]);
                    ob.MAX_TEMP_C = (dr["MAX_TEMP_C"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_TEMP_C"]);
                    ob.IS_SMP_BLK = (dr["IS_SMP_BLK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_BLK"]);
                    ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);
                    ob.MC_VERSON_NO = (dr["MC_VERSON_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_VERSON_NO"]);
                    
                    if (dr["ACTN_TIME"] != DBNull.Value) 
                        ob.ACTN_TIME = Convert.ToDateTime(dr["ACTN_TIME"]);

                    ob.ACTN_REF_NO = (dr["ACTN_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_REF_NO"]);

                    ob.DYE_TYPE_NAME_EN = (dr["DYE_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_TYPE_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.MAC_STATUS_NAME = (dr["MAC_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_STATUS_NAME"]);
                    ob.LOCATION_NAME = (dr["LOCATION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOCATION_NAME"]);

                    ob.MAC_PROD_STS_COLOR = (dr["MAC_PROD_STS_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_COLOR"]);
                    ob.MAC_PROD_STS_EN_NAME = (dr["MAC_PROD_STS_EN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_EN_NAME"]);
                    ob.DM_ACTN_MODE_NAME = (dr["DM_ACTN_MODE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DM_ACTN_MODE_NAME"]);
                                      
                    ob.ACTN_TIME = (dr["ACTN_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ACTN_TIME"]);
                    ob.LK_DM_ACTN_MODE_ID = (dr["LK_DM_ACTN_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DM_ACTN_MODE_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.DYE_STR_REQ_H_ID = (dr["DYE_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_STR_REQ_H_ID"]);

                    ob.ACTN_REF_NO = (dr["ACTN_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_REF_NO"]);
                    ob.MC_VERSON_NO = (dr["MC_VERSON_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_VERSON_NO"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_MACHINEModel> GetMachineInfoByTypeID(Int64? pDYE_MC_TYPE_ID = null, Int64? pHR_COMPANY_ID=null)
        {
            //string sp = "pkg_dye_dc_issue.dye_machine_select";
            string sp = "pkg_dye_common.dye_machine_select";
            try
            {
                var obList = new List<DYE_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value =pDYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDYE_MC_TYPE_ID", Value =pDYE_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_MACHINEModel ob = new DYE_MACHINEModel();
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.DYE_MC_TYPE_ID = (dr["DYE_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    ob.CAP_MOU_ID = (dr["CAP_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAP_MOU_ID"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RPM"]);
                    ob.NO_NOZLE = (dr["NO_NOZLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_NOZLE"]);
                    ob.MAX_TEMP_C = (dr["MAX_TEMP_C"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_TEMP_C"]);
                    ob.IS_SMP_BLK = (dr["IS_SMP_BLK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_BLK"]);
                    ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);

                    ob.MC_VERSON_NO = (dr["MC_VERSON_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_VERSON_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_MACHINEModel> GetDataFabReqDyeBatch(Int64? pDYE_BATCH_SCDL_ID, Int16? pOption, string pIS_SMP_BLK, string pDYE_BATCH_NO, Int64? pHR_COMPANY_ID)
        {
            string sp = "pkg_DYE_BT_FAB_REQ.dye_batch_plan_select";
            try
            {
                var obList = new List<DYE_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value =pDYE_BATCH_SCDL_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
                     new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value = pIS_SMP_BLK},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value = pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_MACHINEModel ob = new DYE_MACHINEModel();
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.DYE_MC_TYPE_ID = (dr["DYE_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    
                    ob.CAP_MOU_ID = (dr["CAP_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAP_MOU_ID"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.MAX_LOAD = ob.D_PRD_CAPACITY * (ob.PCT_EFFCNCY / 100);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RPM"]);
                    ob.NO_NOZLE = (dr["NO_NOZLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_NOZLE"]);
                    ob.MAX_TEMP_C = (dr["MAX_TEMP_C"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_TEMP_C"]);
                    ob.IS_SMP_BLK = (dr["IS_SMP_BLK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_BLK"]);
                    ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);

                    ob.MC_VERSON_NO = (dr["MC_VERSON_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_VERSON_NO"]);
                    if (dr["DYE_BATCH_SCDL_ID"] != DBNull.Value)
                    {
                        ob.DYE_BATCH_SCDL_ID = Convert.ToInt64(dr["DYE_BATCH_SCDL_ID"]);
                    }
                    


                    ob.dyePrograms = new DYE_BATCH_PLANModel().getProgramData(ob.DYE_BATCH_SCDL_ID, ob.DYE_MACHINE_ID, (pIS_SMP_BLK == "S" ? 3003 : 3001), pDYE_BATCH_NO);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DYE_MACHINEModel Select(long ID)
        {
            string sp = "pkg_dye_common.dye_machine_select";
            try
            {
                var ob = new DYE_MACHINEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value =ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.DYE_MC_TYPE_ID = (dr["DYE_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    ob.CAP_MOU_ID = (dr["CAP_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAP_MOU_ID"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RPM"]);
                    ob.NO_NOZLE = (dr["NO_NOZLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_NOZLE"]);
                    ob.MAX_TEMP_C = (dr["MAX_TEMP_C"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_TEMP_C"]);
                    ob.IS_SMP_BLK = (dr["IS_SMP_BLK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_BLK"]);
                    ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_TYPE_NAME_EN = (dr["DYE_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_TYPE_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.MAC_STATUS_NAME = (dr["MAC_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_STATUS_NAME"]);
                    ob.LOCATION_NAME = (dr["LOCATION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOCATION_NAME"]);
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);
                    ob.MC_VERSON_NO = (dr["MC_VERSON_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_VERSON_NO"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DYE_MACHINEModel> SelectByCompany(Int64? pHR_COMPANY_ID = null)
        {
            //string sp = "pkg_dye_dc_issue.dye_machine_select";
            string sp = "pkg_dye_common.dye_machine_select";
            try
            {
                var obList = new List<DYE_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_MACHINEModel ob = new DYE_MACHINEModel();
                    ob.DYE_MACHINE_ID = (dr["DYE_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MACHINE_ID"]);
                    ob.DYE_MACHINE_NO = (dr["DYE_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_MACHINE_NO"]);
                    ob.DYE_MC_TYPE_ID = (dr["DYE_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    ob.CAP_MOU_ID = (dr["CAP_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CAP_MOU_ID"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                    ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    ob.LK_MAC_STATUS_ID = (dr["LK_MAC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_STATUS_ID"]);
                    ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RPM"]);
                    ob.NO_NOZLE = (dr["NO_NOZLE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_NOZLE"]);
                    ob.MAX_TEMP_C = (dr["MAX_TEMP_C"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_TEMP_C"]);
                    ob.IS_SMP_BLK = (dr["IS_SMP_BLK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SMP_BLK"]);
                    ob.RF_LOCATION_ID = (dr["RF_LOCATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_MAC_PROD_STS_ID = (dr["RF_MAC_PROD_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MAC_PROD_STS_ID"]);

                    ob.MC_VERSON_NO = (dr["MC_VERSON_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_VERSON_NO"]);
                    if (dr["ACTN_TIME"] != DBNull.Value)
                        ob.ACTN_TIME = Convert.ToDateTime(dr["ACTN_TIME"]);

                    ob.ACTN_REF_NO = (dr["ACTN_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_REF_NO"]);

                    ob.DYE_TYPE_NAME_EN = (dr["DYE_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_TYPE_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.MAC_STATUS_NAME = (dr["MAC_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_STATUS_NAME"]);
                    ob.LOCATION_NAME = (dr["LOCATION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOCATION_NAME"]);

                    ob.MAC_PROD_STS_COLOR = (dr["MAC_PROD_STS_COLOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_COLOR"]);
                    ob.MAC_PROD_STS_EN_NAME = (dr["MAC_PROD_STS_EN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAC_PROD_STS_EN_NAME"]);
                    ob.DM_ACTN_MODE_NAME = (dr["DM_ACTN_MODE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DM_ACTN_MODE_NAME"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DYE_TYPE_NAME_EN { get; set; }

        public string BRAND_NAME_EN { get; set; }

        public string COUNTRY_NAME_EN { get; set; }

        public string MOU_CODE { get; set; }

        public string MAC_STATUS_NAME { get; set; }

        public string LOCATION_NAME { get; set; }

        public long? DYE_BATCH_SCDL_ID { get; set; }

        public bool IS_SELECTED_BT { get; set; }

        public decimal MAX_LOAD { get; set; }

        public string ACTN_REF_NO { get; set; }

        public string MAC_PROD_STS_COLOR { get; set; }

        public string MAC_PROD_STS_EN_NAME { get; set; }

        public string DM_ACTN_MODE_NAME { get; set; }
        public DateTime? ACTN_TIME { get; set; }


        public long LK_DM_ACTN_MODE_ID { get; set; }

        public long DYE_STR_REQ_H_ID { get; set; }
    }
}