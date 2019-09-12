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
    public class DF_SC_PT_ISS_D3Model
    {
        public Int64 DF_SC_PT_ISS_D3_ID { get; set; }
        public Int64 DF_SC_PT_ISS_D2_ID { get; set; }
        public Int64 RF_DF_PARAM_TYPE_ID { get; set; }
        public string PARAM_DESC { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        

        public string PARAM_TYPE_CODE { get; set; }
        public string PARAM_TYPE_BN_NAME { get; set; }
        public string PARAM_TYPE_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }


        public string Save()
        {
            const string sp = "SP_DF_SC_PT_ISS_D3";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D3_ID", Value = ob.DF_SC_PT_ISS_D3_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D2_ID", Value = ob.DF_SC_PT_ISS_D2_ID},
                     new CommandParameter() {ParameterName = "pRF_DF_PARAM_TYPE_ID", Value = ob.RF_DF_PARAM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPARAM_DESC", Value = ob.PARAM_DESC},
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
            const string sp = "SP_DF_SC_PT_ISS_D3";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D3_ID", Value = ob.DF_SC_PT_ISS_D3_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D2_ID", Value = ob.DF_SC_PT_ISS_D2_ID},
                     new CommandParameter() {ParameterName = "pRF_DF_PARAM_TYPE_ID", Value = ob.RF_DF_PARAM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPARAM_DESC", Value = ob.PARAM_DESC},
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
            const string sp = "SP_DF_SC_PT_ISS_D3";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D3_ID", Value = ob.DF_SC_PT_ISS_D3_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D2_ID", Value = ob.DF_SC_PT_ISS_D2_ID},
                     new CommandParameter() {ParameterName = "pRF_DF_PARAM_TYPE_ID", Value = ob.RF_DF_PARAM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPARAM_DESC", Value = ob.PARAM_DESC},
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

        public List<DF_SC_PT_ISS_D3Model> SelectByID(Int64? pDF_SC_PT_ISS_D2_ID=null)
        {
            string sp = "PKG_DF_PT.df_sc_pt_iss_d3_select";
            try
            {
                var obList = new List<DF_SC_PT_ISS_D3Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D2_ID", Value =pDF_SC_PT_ISS_D2_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_D3Model ob = new DF_SC_PT_ISS_D3Model();
                    ob.DF_SC_PT_ISS_D3_ID = (dr["DF_SC_PT_ISS_D3_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D3_ID"]);
                    ob.DF_SC_PT_ISS_D2_ID = (dr["DF_SC_PT_ISS_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D2_ID"]);
                    ob.RF_DF_PARAM_TYPE_ID = (dr["RF_DF_PARAM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DF_PARAM_TYPE_ID"]);
                    ob.PARAM_DESC = (dr["PARAM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARAM_DESC"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.PARAM_TYPE_EN_NAME = (dr["PARAM_TYPE_EN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARAM_TYPE_EN_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SC_PT_ISS_D3Model Select(long ID)
        {
            string sp = "Select_DF_SC_PT_ISS_D3";
            try
            {
                var ob = new DF_SC_PT_ISS_D3Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D3_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PT_ISS_D3_ID = (dr["DF_SC_PT_ISS_D3_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D3_ID"]);
                    ob.DF_SC_PT_ISS_D2_ID = (dr["DF_SC_PT_ISS_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D2_ID"]);
                    ob.RF_DF_PARAM_TYPE_ID = (dr["RF_DF_PARAM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DF_PARAM_TYPE_ID"]);
                    ob.PARAM_DESC = (dr["PARAM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARAM_DESC"]);
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

        public string PARAM_TYPE_EN_NAME { get; set; }
    }
}