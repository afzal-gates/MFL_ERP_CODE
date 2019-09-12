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
    public class DYE_OTH_CONS_TMPLTModel
    {
        public Int64 DYE_OTH_CONS_TMPLT_ID { get; set; }
        public Int64 LK_DC_CONS_TYPE_ID { get; set; }
        public Int64 DC_AGENT_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_OTH_CONS_TMPLT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_OTH_CONS_TMPLT_ID", Value = ob.DYE_OTH_CONS_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pLK_DC_CONS_TYPE_ID", Value = ob.LK_DC_CONS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDC_AGENT_ID", Value = ob.DC_AGENT_ID},
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
            const string sp = "SP_DYE_OTH_CONS_TMPLT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_OTH_CONS_TMPLT_ID", Value = ob.DYE_OTH_CONS_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pLK_DC_CONS_TYPE_ID", Value = ob.LK_DC_CONS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDC_AGENT_ID", Value = ob.DC_AGENT_ID},
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
            const string sp = "SP_DYE_OTH_CONS_TMPLT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_OTH_CONS_TMPLT_ID", Value = ob.DYE_OTH_CONS_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pLK_DC_CONS_TYPE_ID", Value = ob.LK_DC_CONS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDC_AGENT_ID", Value = ob.DC_AGENT_ID},
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

        public List<DYE_OTH_CONS_TMPLTModel> SelectAll(Int64? pLK_DC_CONS_TYPE_ID=null)
        {
            string sp = "pkg_dye_dc_issue.dye_oth_cons_tmplt_select";
            try
            {
                var obList = new List<DYE_OTH_CONS_TMPLTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_OTH_CONS_TMPLT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pLK_DC_CONS_TYPE_ID", Value =pLK_DC_CONS_TYPE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_OTH_CONS_TMPLTModel ob = new DYE_OTH_CONS_TMPLTModel();
                    ob.DYE_OTH_CONS_TMPLT_ID = (dr["DYE_OTH_CONS_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_OTH_CONS_TMPLT_ID"]);
                    ob.LK_DC_CONS_TYPE_ID = (dr["LK_DC_CONS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DC_CONS_TYPE_ID"]);
                    ob.DC_AGENT_ID = (dr["DC_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_AGENT_ID"]);
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

        public DYE_OTH_CONS_TMPLTModel Select(long ID)
        {
            string sp = "pkg_dye_dc_issue.dye_oth_cons_tmplt_select";
            try
            {
                var ob = new DYE_OTH_CONS_TMPLTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_OTH_CONS_TMPLT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_OTH_CONS_TMPLT_ID = (dr["DYE_OTH_CONS_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_OTH_CONS_TMPLT_ID"]);
                    ob.LK_DC_CONS_TYPE_ID = (dr["LK_DC_CONS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DC_CONS_TYPE_ID"]);
                    ob.DC_AGENT_ID = (dr["DC_AGENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_AGENT_ID"]);
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