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
    public class DYE_PRD_PHASE_TYPEModel
    {
        public Int64 DYE_PRD_PHASE_TYPE_ID { get; set; }
        public string PRD_PHASE_CODE { get; set; }
        public string PRD_PHASE_NAME { get; set; }
        public string PRD_TYPE_SNAME { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DYE_PRD_PHASE_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPRD_PHASE_CODE", Value = ob.PRD_PHASE_CODE},
                     new CommandParameter() {ParameterName = "pPRD_PHASE_NAME", Value = ob.PRD_PHASE_NAME},
                     new CommandParameter() {ParameterName = "pPRD_TYPE_SNAME", Value = ob.PRD_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "SP_DYE_PRD_PHASE_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPRD_PHASE_CODE", Value = ob.PRD_PHASE_CODE},
                     new CommandParameter() {ParameterName = "pPRD_PHASE_NAME", Value = ob.PRD_PHASE_NAME},
                     new CommandParameter() {ParameterName = "pPRD_TYPE_SNAME", Value = ob.PRD_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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
            const string sp = "SP_DYE_PRD_PHASE_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value = ob.DYE_PRD_PHASE_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPRD_PHASE_CODE", Value = ob.PRD_PHASE_CODE},
                     new CommandParameter() {ParameterName = "pPRD_PHASE_NAME", Value = ob.PRD_PHASE_NAME},
                     new CommandParameter() {ParameterName = "pPRD_TYPE_SNAME", Value = ob.PRD_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
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

        public List<DYE_PRD_PHASE_TYPEModel> SelectAll()
        {
            string sp = "pkg_mc_ld_recipe.dye_prd_phase_type_select";
            try
            {
                var obList = new List<DYE_PRD_PHASE_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_PRD_PHASE_TYPEModel ob = new DYE_PRD_PHASE_TYPEModel();
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.PRD_PHASE_CODE = (dr["PRD_PHASE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_CODE"]);
                    ob.PRD_PHASE_NAME = (dr["PRD_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_NAME"]);
                    ob.PRD_TYPE_SNAME = (dr["PRD_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_TYPE_SNAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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

        public DYE_PRD_PHASE_TYPEModel Select(long ID)
        {
            string sp = "Select_DYE_PRD_PHASE_TYPE";
            try
            {
                var ob = new DYE_PRD_PHASE_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_PRD_PHASE_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_PRD_PHASE_TYPE_ID = (dr["DYE_PRD_PHASE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_PRD_PHASE_TYPE_ID"]);
                    ob.PRD_PHASE_CODE = (dr["PRD_PHASE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_CODE"]);
                    ob.PRD_PHASE_NAME = (dr["PRD_PHASE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_PHASE_NAME"]);
                    ob.PRD_TYPE_SNAME = (dr["PRD_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRD_TYPE_SNAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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