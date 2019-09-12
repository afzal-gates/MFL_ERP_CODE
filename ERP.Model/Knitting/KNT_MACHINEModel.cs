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
    public class KNT_MACHINEModel
    {
        public Int64 KNT_MACHINE_ID { get; set; }
        public string KNT_MACHINE_NO { get; set; }
        public Int64 KNT_MC_TYPE_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public string MODEL_NO { get; set; }
        public Int64 C_ORIGIN_ID { get; set; }
        public Int64? KNT_MC_DIA_ID { get; set; }
        public Int64? DF_MC_GG_ID { get; set; }
        public Decimal? D_PRD_CAPACITY { get; set; }
        public Int64? MC_RPM { get; set; }
        public Int64? NO_FEEDER { get; set; }
        public Decimal? PCT_EFFCNCY { get; set; }
        public string MC_SERIAL_NO { get; set; }
        public Int64? MFG_YEAR { get; set; }
        public string IS_ACTIVE { get; set; }

        
        public long? RF_LOCATION_ID_SL { get; set; }
        public long? RF_LOCATION_ID_SPAN { get; set; }
        public long? HR_PROD_FLR_ID { get; set; }
        public string FLOOR_DESC_EN { get; set; }
        public string MC_GAUGE_NAME { get; set; }
        public string MC_DIA { get; set; }
        public string[] KNT_MC_GG_ID_LIST { get; set; }


        
        public string Save()
        {
            const string sp = "pkg_knit_common.knt_machine_save";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_NO", Value = ob.KNT_MACHINE_NO},
                     new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value = ob.KNT_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pMODEL_NO", Value = ob.MODEL_NO},
                     new CommandParameter() {ParameterName = "pC_ORIGIN_ID", Value = ob.C_ORIGIN_ID},
                     new CommandParameter() {ParameterName = "pKNT_MC_DIA_ID", Value = ob.KNT_MC_DIA_ID},
                     new CommandParameter() {ParameterName = "pDF_MC_GG_ID", Value = ob.DF_MC_GG_ID},
                     new CommandParameter() {ParameterName = "pKNT_MC_GG_ID_LST", Value = String.Join(",",ob.KNT_MC_GG_ID_LIST)},
                     new CommandParameter() {ParameterName = "pD_PRD_CAPACITY", Value = ob.D_PRD_CAPACITY},
                     new CommandParameter() {ParameterName = "pMC_RPM", Value = ob.MC_RPM},
                     new CommandParameter() {ParameterName = "pNO_FEEDER", Value = ob.NO_FEEDER},
                     new CommandParameter() {ParameterName = "pPCT_EFFCNCY", Value = ob.PCT_EFFCNCY},
                     new CommandParameter() {ParameterName = "pMC_SERIAL_NO", Value = ob.MC_SERIAL_NO},
                     new CommandParameter() {ParameterName = "pMFG_YEAR", Value = ob.MFG_YEAR},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                                          
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID_RTN", Direction = ParameterDirection.Output}
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_MACHINEModel> GetMachineList(Int32? pKNT_MC_TYPE_ID = null)
        {
            string sp = "pkg_knit_common.knt_machine_select";
            try
            {
                var obList = new List<KNT_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value = pKNT_MC_TYPE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value =3000},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MACHINEModel ob = new KNT_MACHINEModel();
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.KNT_MC_TYPE_ID = (dr["KNT_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                    if (dr["KNT_MC_DIA_ID"] != DBNull.Value)
                        ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);

                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    
                    ob.DF_MC_GG_ID = (dr["DF_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MC_GG_ID"]);
                    
                    if (dr["D_PRD_CAPACITY"] != DBNull.Value)
                        ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    if (dr["MC_RPM"] != DBNull.Value)
                        ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RPM"]);
                    if (dr["NO_FEEDER"] != DBNull.Value)
                        ob.NO_FEEDER = (dr["NO_FEEDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_FEEDER"]);
                    if (dr["PCT_EFFCNCY"] != DBNull.Value)
                        ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    if (dr["MFG_YEAR"] != DBNull.Value)
                        ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                    
                    ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);                                        
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    ob.FLOOR_DESC_EN = (dr["FLOOR_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FLOOR_DESC_EN"]);
                    ob.MC_GAUGE_NAME = (dr["MC_GAUGE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GAUGE_NAME"]);

                    if (dr["KNT_MC_GG_ID_LST"] != DBNull.Value)
                    { ob.KNT_MC_GG_ID_LIST = Convert.ToString(dr["KNT_MC_GG_ID_LST"]).Split(','); }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_MACHINEModel> QueryData(int? pACT_MC_DIA_ID, int? pHR_PROD_FLR_ID, int? pHR_COMPANY_ID, int pKNT_MC_TYPE_ID = 1)
        {
            string sp = "pkg_knit_plan.knt_machine_select";
            try
            {
                var obList = new List<KNT_MACHINEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pACT_MC_DIA_ID", Value =pACT_MC_DIA_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pKNT_MC_TYPE_ID", Value =pKNT_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            KNT_MACHINEModel ob = new KNT_MACHINEModel();
                            ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                            ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                            ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                            ob.KNT_MC_TYPE_ID = (dr["KNT_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_TYPE_ID"]);
                            ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                            ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                            ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                            ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                            ob.DF_MC_GG_ID = (dr["DF_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MC_GG_ID"]);
                            ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                            ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RPM"]);
                            ob.NO_FEEDER = (dr["NO_FEEDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_FEEDER"]);
                            ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                            ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                            ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            ob.RF_LOCATION_ID_SPAN = (dr["RF_LOCATION_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID_SPAN"]);
                            ob.RF_LOCATION_ID_SL = (dr["RF_LOCATION_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LOCATION_ID_SL"]);
                            ob.FLOOR_DESC_EN = (dr["LOCATION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOCATION_NAME"]);
                            ob.MC_GAUGE_NAME = (dr["MC_GAUGE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GAUGE_NAME"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_MACHINEModel Select(long ID)
        {
            string sp = "Select_KNT_MACHINE";
            try
            {
                var ob = new KNT_MACHINEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                        ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                        ob.KNT_MC_TYPE_ID = (dr["KNT_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_TYPE_ID"]);
                        ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                        ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                        ob.C_ORIGIN_ID = (dr["C_ORIGIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["C_ORIGIN_ID"]);
                        ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                        ob.DF_MC_GG_ID = (dr["DF_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_MC_GG_ID"]);
                        ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                        ob.MC_RPM = (dr["MC_RPM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_RPM"]);
                        ob.NO_FEEDER = (dr["NO_FEEDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_FEEDER"]);
                        ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                        ob.MC_SERIAL_NO = (dr["MC_SERIAL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SERIAL_NO"]);
                        ob.MFG_YEAR = (dr["MFG_YEAR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MFG_YEAR"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

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