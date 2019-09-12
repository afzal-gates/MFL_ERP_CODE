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
    public class GMT_PART_OPR_SPECModel
    {
        public Int64? GMT_PART_OPR_SPEC_ID { get; set; }
        public Int64? RF_GARM_PART_ID { get; set; }
        public string PART_OPR_SPEC { get; set; }
        public int? PROC_SPEC_SL { get; set; }
        public string GMT_PART_OPR_SPEC_XML { get; set; }


        public string BatchSave()
        {
            const string sp = "pkg_planning_common.gmt_part_opr_spec_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pGMT_PART_OPR_SPEC_ID", Value = ob.GMT_PART_OPR_SPEC_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pPART_OPR_SPEC", Value = ob.PART_OPR_SPEC},
                     new CommandParameter() {ParameterName = "pGMT_PART_OPR_SPEC_XML", Value = ob.GMT_PART_OPR_SPEC_XML},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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



        public List<GMT_PART_OPR_SPECModel> GetGmtPartProcSpecList(int pRF_GARM_PART_ID)
        {
            string sp = "pkg_planning_common.gmt_part_opr_spec_select";
            try
            {
                var obList = new List<GMT_PART_OPR_SPECModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = pRF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PART_OPR_SPECModel ob = new GMT_PART_OPR_SPECModel();
                    ob.GMT_PART_OPR_SPEC_ID = (dr["GMT_PART_OPR_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PART_OPR_SPEC_ID"]);
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.PART_OPR_SPEC = (dr["PART_OPR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_OPR_SPEC"]);

                    ob.PROC_SPEC_SL = (dr["PROC_SPEC_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PROC_SPEC_SL"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_PART_OPR_SPECModel Select(long ID)
        {
            string sp = "Select_GMT_PART_OPR_SPEC";
            try
            {
                var ob = new GMT_PART_OPR_SPECModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PART_OPR_SPEC_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_PART_OPR_SPEC_ID = (dr["GMT_PART_OPR_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PART_OPR_SPEC_ID"]);
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.PART_OPR_SPEC = (dr["PART_OPR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_OPR_SPEC"]);

                    
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