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
    public class DYE_MCN_STOP_DFCTModel
    {
        public Int64 DYE_MCN_STOP_DFCT_ID { get; set; }
        public Int64 DYE_MCN_STOP_TRAN_ID { get; set; }
        public Int64 RF_MCN_DFCT_TYPE_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_MCN_STOP_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_DFCT_ID", Value = ob.DYE_MCN_STOP_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_TRAN_ID", Value = ob.DYE_MCN_STOP_TRAN_ID},
                     new CommandParameter() {ParameterName = "pRF_MCN_DFCT_TYPE_ID", Value = ob.RF_MCN_DFCT_TYPE_ID},
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
            const string sp = "SP_DYE_MCN_STOP_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_DFCT_ID", Value = ob.DYE_MCN_STOP_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_TRAN_ID", Value = ob.DYE_MCN_STOP_TRAN_ID},
                     new CommandParameter() {ParameterName = "pRF_MCN_DFCT_TYPE_ID", Value = ob.RF_MCN_DFCT_TYPE_ID},
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
            const string sp = "SP_DYE_MCN_STOP_DFCT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_DFCT_ID", Value = ob.DYE_MCN_STOP_DFCT_ID},
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_TRAN_ID", Value = ob.DYE_MCN_STOP_TRAN_ID},
                     new CommandParameter() {ParameterName = "pRF_MCN_DFCT_TYPE_ID", Value = ob.RF_MCN_DFCT_TYPE_ID},
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

        public List<DYE_MCN_STOP_DFCTModel> SelectAll()
        {
            string sp = "PKG_DYE_MC_MAINTENANCE.DYE_MCN_STOP_DFCT_Select";
            try
            {
                var obList = new List<DYE_MCN_STOP_DFCTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_DFCT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_MCN_STOP_DFCTModel ob = new DYE_MCN_STOP_DFCTModel();
                    ob.DYE_MCN_STOP_DFCT_ID = (dr["DYE_MCN_STOP_DFCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MCN_STOP_DFCT_ID"]);
                    ob.DYE_MCN_STOP_TRAN_ID = (dr["DYE_MCN_STOP_TRAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MCN_STOP_TRAN_ID"]);
                    ob.RF_MCN_DFCT_TYPE_ID = (dr["RF_MCN_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MCN_DFCT_TYPE_ID"]);
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

        public DYE_MCN_STOP_DFCTModel Select(long ID)
        {
            string sp = "PKG_DYE_MC_MAINTENANCE.DYE_MCN_STOP_DFCT_Select";
            try
            {
                var ob = new DYE_MCN_STOP_DFCTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MCN_STOP_DFCT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_MCN_STOP_DFCT_ID = (dr["DYE_MCN_STOP_DFCT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MCN_STOP_DFCT_ID"]);
                    ob.DYE_MCN_STOP_TRAN_ID = (dr["DYE_MCN_STOP_TRAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_MCN_STOP_TRAN_ID"]);
                    ob.RF_MCN_DFCT_TYPE_ID = (dr["RF_MCN_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MCN_DFCT_TYPE_ID"]);
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