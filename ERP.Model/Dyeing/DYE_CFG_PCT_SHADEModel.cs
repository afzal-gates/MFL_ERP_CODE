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
    public class DYE_CFG_PCT_SHADEModel
    {
        public Int64 DYE_CFG_PCT_SHADE_ID { get; set; }
        public Int64 MC_COLOR_GRP_ID { get; set; }
        public Decimal PCT_SHADE_FRM { get; set; }
        public Decimal PCT_SHADE_TO { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_CFG_PCT_SHADE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_CFG_PCT_SHADE_ID", Value = ob.DYE_CFG_PCT_SHADE_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pPCT_SHADE_FRM", Value = ob.PCT_SHADE_FRM},
                     new CommandParameter() {ParameterName = "pPCT_SHADE_TO", Value = ob.PCT_SHADE_TO},
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
            const string sp = "SP_DYE_CFG_PCT_SHADE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_CFG_PCT_SHADE_ID", Value = ob.DYE_CFG_PCT_SHADE_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pPCT_SHADE_FRM", Value = ob.PCT_SHADE_FRM},
                     new CommandParameter() {ParameterName = "pPCT_SHADE_TO", Value = ob.PCT_SHADE_TO},
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
            const string sp = "SP_DYE_CFG_PCT_SHADE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_CFG_PCT_SHADE_ID", Value = ob.DYE_CFG_PCT_SHADE_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value = ob.MC_COLOR_GRP_ID},
                     new CommandParameter() {ParameterName = "pPCT_SHADE_FRM", Value = ob.PCT_SHADE_FRM},
                     new CommandParameter() {ParameterName = "pPCT_SHADE_TO", Value = ob.PCT_SHADE_TO},
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

        public List<DYE_CFG_PCT_SHADEModel> SelectAll()
        {
            string sp = "PKG_DYE_DC_RECEIVE.";
            try
            {
                var obList = new List<DYE_CFG_PCT_SHADEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_CFG_PCT_SHADE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_CFG_PCT_SHADEModel ob = new DYE_CFG_PCT_SHADEModel();
                    ob.DYE_CFG_PCT_SHADE_ID = (dr["DYE_CFG_PCT_SHADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_CFG_PCT_SHADE_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.PCT_SHADE_FRM = (dr["PCT_SHADE_FRM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_SHADE_FRM"]);
                    ob.PCT_SHADE_TO = (dr["PCT_SHADE_TO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_SHADE_TO"]);
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

        public DYE_CFG_PCT_SHADEModel Select(long ID)
        {
            string sp = "PKG_DYE_DC_RECEIVE.";
            try
            {
                var ob = new DYE_CFG_PCT_SHADEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_CFG_PCT_SHADE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_CFG_PCT_SHADE_ID = (dr["DYE_CFG_PCT_SHADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_CFG_PCT_SHADE_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.PCT_SHADE_FRM = (dr["PCT_SHADE_FRM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_SHADE_FRM"]);
                    ob.PCT_SHADE_TO = (dr["PCT_SHADE_TO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_SHADE_TO"]);
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

        public DYE_CFG_PCT_SHADEModel SelectByValue(Decimal? pPERCENT_QTY)
        {
            string sp = "pkg_dye_dc_receive.dye_cfg_pct_shade_select";
            try
            {
                var ob = new DYE_CFG_PCT_SHADEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPERCENT_QTY", Value =pPERCENT_QTY},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_CFG_PCT_SHADE_ID = (dr["DYE_CFG_PCT_SHADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_CFG_PCT_SHADE_ID"]);
                    ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                    ob.PCT_SHADE_FRM = (dr["PCT_SHADE_FRM"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_SHADE_FRM"]);
                    ob.PCT_SHADE_TO = (dr["PCT_SHADE_TO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_SHADE_TO"]);
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