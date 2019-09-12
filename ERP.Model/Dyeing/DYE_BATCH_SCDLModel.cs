using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

namespace ERP.Model
{
    public class DYE_BATCH_SCDLModel
    {
        public Int64? DYE_BATCH_SCDL_ID { get; set; }
        public string SCDL_REF_NO { get; set; }
        public DateTime START_DT { get; set; }
        public DateTime END_DT { get; set; }
        public string SCDL_NOTE { get; set; }
        public string IS_FINALIZED { get; set; }
        public string XML { get; set; }
        public Int16 pOption { get; set; }
        public DateTime PRE_BC_END_DT { get; set; }
        public int DURATION_IN_DYS { get; set; }
        public string IS_ACTIVE { get; set; }
        public string IS_SMP_BLK { get; set; }
        public Int64? DYE_SC_PRG_ISS_ID { get; set; }

        public string Save()
        {
            const string sp = "PKG_DYE_BATCH_PLAN.dye_batch_scdl_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value = ob.DYE_BATCH_SCDL_ID},
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = ob.DYE_SC_PRG_ISS_ID},
                     
                     new CommandParameter() {ParameterName = "pSCDL_REF_NO", Value = ob.SCDL_REF_NO},
                     new CommandParameter() {ParameterName = "pSTART_DT", Value = ob.START_DT},
                     new CommandParameter() {ParameterName = "pEND_DT", Value = ob.END_DT},
                     new CommandParameter() {ParameterName = "pSCDL_NOTE", Value = ob.SCDL_NOTE},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                      new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value = ob.IS_SMP_BLK},
                      
                     new CommandParameter() {ParameterName = "pXML", Value =  ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_DYE_BATCH_SCDL_ID", Value =null, Direction = ParameterDirection.Output}
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

        public string getLastScPlanDt(string pIS_SMP_BLK)
        {
            const string sp = "PKG_DYE_BATCH_PLAN.Last_Sc_Plan_Dt";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value = pIS_SMP_BLK},    
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_PRE_BC_END_DT", Value =null, Direction = ParameterDirection.Output}
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

        public List<DYE_BATCH_SCDLModel> SelectAll(string pSCDL_REF_NO, Int64? pDYE_BATCH_SCDL_ID, string pIS_SMP_BLK)
        {
            string sp = "PKG_DYE_BATCH_PLAN.dye_batch_scdl_select";
            try
            {
                var obList = new List<DYE_BATCH_SCDLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCDL_REF_NO", Value = pSCDL_REF_NO},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value = pDYE_BATCH_SCDL_ID},
                      new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value = pIS_SMP_BLK},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BATCH_SCDLModel ob = new DYE_BATCH_SCDLModel();
                    ob.DYE_BATCH_SCDL_ID = (dr["DYE_BATCH_SCDL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_SCDL_ID"]);
                    ob.SCDL_REF_NO = (dr["SCDL_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCDL_REF_NO"]);

                    ob.START_DT = (dr["START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DT"]);
                    ob.END_DT = (dr["END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DT"]);

                    ob.DURATION_IN_DYS = (int)Math.Ceiling((ob.END_DT - ob.START_DT).TotalDays) + 1;

                    ob.PRE_BC_END_DT = (dr["PRE_BC_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRE_BC_END_DT"]);

                    ob.SCDL_NOTE = (dr["SCDL_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCDL_NOTE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACTIVE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_BATCH_SCDLModel Select(long? ID, string pIS_SMP_BLK)
        {
            string sp = "PKG_DYE_BATCH_PLAN.dye_batch_scdl_select";
            try
            {
                var ob = new DYE_BATCH_SCDLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value =ID},
                      new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value =pIS_SMP_BLK},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_BATCH_SCDL_ID = (dr["DYE_BATCH_SCDL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BATCH_SCDL_ID"]);
                    ob.SCDL_REF_NO = (dr["SCDL_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCDL_REF_NO"]);

                    ob.START_DT = (dr["START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["START_DT"]);
                    ob.END_DT = (dr["END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["END_DT"]);

                    ob.DURATION_IN_DYS = (int)Math.Ceiling((ob.END_DT - ob.START_DT).TotalDays) + 1;

                    ob.PRE_BC_END_DT = (dr["PRE_BC_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRE_BC_END_DT"]);

                    ob.SCDL_NOTE = (dr["SCDL_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCDL_NOTE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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