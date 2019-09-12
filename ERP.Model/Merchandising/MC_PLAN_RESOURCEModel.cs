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
    public class MC_PLAN_RESOURCEModel
    {

        public Int64 id {get;set;}

        public int HR_PROD_FLR_ID { get; set; }
        public string name { get; set; }

        public string dia_gauge { get; set; }

        public int? no_feeder { get; set; }

        public bool expanded { get; set; }

        public string XML { get; set; }


        private List<MC_PLAN_RESOURCEModel> _children = null;
        public List<MC_PLAN_RESOURCEModel> children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<MC_PLAN_RESOURCEModel>();
                }
                return _children;
            }
        }


        public string Save()
        {
            const string sp = "pkg_knit_plan.knt_fab_roll_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {


                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
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

        public List<MC_PLAN_RESOURCEModel> QueryData(Int64? pHR_PROD_FLR_ID, int? pKNT_MACHINE_ID, int? pHR_PROD_BLDNG_ID, int? pHR_COMPANY_ID, int? pKNT_MC_DIA_ID)
        {
            string sp = "pkg_mc_load_plan.RESOURCE_SELECT";
            try
            {
                var obList = new List<MC_PLAN_RESOURCEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =pHR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_BLDNG_ID", Value =pHR_PROD_BLDNG_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value =pKNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pKNT_MC_DIA_ID", Value =pKNT_MC_DIA_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_PLAN_RESOURCEModel ob = new MC_PLAN_RESOURCEModel();
                    ob.id = (dr["ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ID"]);
                    ob.HR_PROD_FLR_ID = (dr["HR_PROD_FLR_ID"] == DBNull.Value) ? 0: Convert.ToInt16(dr["HR_PROD_FLR_ID"]);
                    ob.name = (dr["NAME_"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["NAME_"]);
                    ob.dia_gauge = (dr["DIA_GAUGE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DIA_GAUGE"]);
                    ob.no_feeder = (dr["NO_FEEDER"] == DBNull.Value) ? 0 : Convert.ToInt16(dr["NO_FEEDER"]);
                    ob.expanded = true;

                                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value =ob.HR_PROD_FLR_ID},
                                         new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value =pKNT_MACHINE_ID},
                                         new CommandParameter() {ParameterName = "pKNT_MC_DIA_ID", Value =pKNT_MC_DIA_ID},
                                         new CommandParameter() {ParameterName = "pOption", Value =3001},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                                    {
                                        MC_PLAN_RESOURCEModel ob1 = new MC_PLAN_RESOURCEModel();
                                        ob1.id = (dr1["ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["ID"]);
                                        ob1.HR_PROD_FLR_ID = (dr1["HR_PROD_FLR_ID"] == DBNull.Value) ? 0 : Convert.ToInt16(dr1["HR_PROD_FLR_ID"]);
                                        ob1.name = (dr1["NAME_"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["NAME_"]);
                                        ob1.dia_gauge = (dr1["DIA_GAUGE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["DIA_GAUGE"]);
                                        ob1.no_feeder = (dr1["NO_FEEDER"] == DBNull.Value) ? 0 : Convert.ToInt16(dr1["NO_FEEDER"]);

                                        ob.children.Add(ob1);
                                    }

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string removeEmptyCell()
        {
            const string sp = "pkg_mc_load_plan.remove_empty_cell";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {

                    new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                    new CommandParameter() {ParameterName = "pOption", Value =1000},
                    new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
    }
}