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
    public class RF_GARM_PARTModel
    {
        public Int64 RF_GARM_PART_ID { get; set; }
        public string GARM_PART_CODE { get; set; }
        public string GARM_PART_NAME { get; set; }
        public string IS_FLAT_CIR { get; set; }
        public string IS_ACTIVE { get; set; }
        public Int64? LK_FBR_GRP_ID { get; set; }
       
        

        public Decimal? PROD_RATE { get; set; }
        public Int64? LK_FK_DGN_TYP_ID { get; set; }
        public Int64? MC_FAB_PROC_RATE_ID { get; set; }
        public string FAB_PROC_NAME { get; set; }
        public string FK_DESIGN_TYPE_NAME { get; set; }
        public object FK_DESIGN_TYPE
        {
            get
            {
                return new { LK_FK_DGN_TYP_ID = this.LK_FK_DGN_TYP_ID, FK_DESIGN_TYPE_NAME = this.FK_DESIGN_TYPE_NAME ?? "" };
            }
        }
        public object FAB_PROC_RATE
        {
            get
            {
                return new { MC_FAB_PROC_RATE_ID = this.MC_FAB_PROC_RATE_ID, FAB_PROC_NAME = this.FAB_PROC_NAME ?? "" };
            }
        }



        public string Save()
        {
            const string sp = "pkg_common.rf_garm_part_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pGARM_PART_CODE", Value = ob.GARM_PART_CODE},
                     new CommandParameter() {ParameterName = "pGARM_PART_NAME", Value = ob.GARM_PART_NAME},
                     new CommandParameter() {ParameterName = "pIS_FLAT_CIR", Value = ob.IS_FLAT_CIR},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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



        public List<RF_GARM_PARTModel> SelectAll(string pRF_GARM_PART_LST = null, string pIS_PROC_GRP=null)
        {
            string sp = "pkg_common.rf_garm_part_select";
            try
            {
                var obList = new List<RF_GARM_PARTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = pRF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pIS_PROC_GRP", Value = pIS_PROC_GRP},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_GARM_PARTModel ob = new RF_GARM_PARTModel();
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.GARM_PART_CODE = (dr["GARM_PART_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_CODE"]);
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<RF_GARM_PARTModel> SelectByStyle(Int64? pMC_STYLE_H_ID = null)
        {
            string sp = "pkg_common.rf_garm_part_select";
            try
            {
                var obList = new List<RF_GARM_PARTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_GARM_PARTModel ob = new RF_GARM_PARTModel();
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.GARM_PART_CODE = (dr["GARM_PART_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_CODE"]);
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_GARM_PARTModel Select(long ID)
        {
            string sp = "pkg_common.rf_garm_part_select";
            try
            {
                var ob = new RF_GARM_PARTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.GARM_PART_CODE = (dr["GARM_PART_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_CODE"]);
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                   

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_GARM_PARTModel> GetScoClCfProgRateAssign(string pRF_GARM_PART_LST = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pKNT_SCO_CLCF_PRG_H_ID = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_clcf_prg_h_select";
            try
            {
                var obList = new List<RF_GARM_PARTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_LST", Value = pRF_GARM_PART_LST},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = pKNT_SCO_CLCF_PRG_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_GARM_PARTModel ob = new RF_GARM_PARTModel();
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.GARM_PART_CODE = (dr["GARM_PART_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_CODE"]);
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);
                    //ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    if (dr["LK_FK_DGN_TYP_ID"] != DBNull.Value)
                        ob.LK_FK_DGN_TYP_ID = (dr["LK_FK_DGN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    ob.FK_DESIGN_TYPE_NAME = (dr["FK_DESIGN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FK_DESIGN_TYPE_NAME"]);

                    if (dr["MC_FAB_PROC_RATE_ID"] != DBNull.Value)
                        ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.FAB_PROC_NAME = (dr["FAB_PROC_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_NAME"]);
                    if (dr["PROD_RATE"] != DBNull.Value)
                        ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}