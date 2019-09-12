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
    public class BATCH_PLAN_RESOURCEModel
    {
        public Int64 id {get;set;}
        public string name { get; set; }
        public Int32 DYE_MC_TYPE_ID { get; set; }
        public string DYE_TYPE_NAME_EN { get; set; }
        public decimal D_PRD_CAPACITY { get; set; }
        public decimal PCT_EFFCNCY { get; set; }
        public decimal MAX_LOAD { get; set; }
        public int NO_NOZLE { get; set; }
        public string IS_SMP_BLK { get; set; }

        //public bool expanded { get; set; }

        public string XML { get; set; }


        //private List<MC_PLAN_RESOURCEModel> _children = null;
        //public List<MC_PLAN_RESOURCEModel> children
        //{
        //    get
        //    {
        //        if (_children == null)
        //        {
        //            _children = new List<MC_PLAN_RESOURCEModel>();
        //        }
        //        return _children;
        //    }
        //}


        //public string Save()
        //{
        //    const string sp = "pkg_knit_plan.knt_fab_roll_insert";
        //    string jsonStr = "{";
        //    var ob = this;
        //    var i = 1;
        //    try
        //    {
        //        OraDatabase db = new OraDatabase();
        //        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {


        //             new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
        //             new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
        //             new CommandParameter() {ParameterName = "pOption", Value =1000},
        //             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
        //         }, sp);

        //        foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
        //        {
        //            jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
        //            if (i < ds.Tables["OUTPARAM"].Rows.Count)
        //            {
        //                jsonStr += ",";
        //            }
        //            else
        //            {
        //                jsonStr += "}";
        //            }
        //            i++;
        //        }
        //        return jsonStr;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<BATCH_PLAN_RESOURCEModel> QueryData(Int64? pDYE_MC_TYPE_ID, String pIS_SMP_BLK, Int64 pDYE_BATCH_SCDL_ID, Int64? pDYE_MACHINE_ID, Int64? pHR_COMPANY_ID)
        {
            string sp = "PKG_DYE_BATCH_PLAN.GET_RESOURCE_DATA";
            try
            {
                var obList = new List<BATCH_PLAN_RESOURCEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_MC_TYPE_ID", Value =pDYE_MC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pIS_SMP_BLK", Value =pIS_SMP_BLK},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_SCDL_ID", Value =pDYE_BATCH_SCDL_ID},
                     new CommandParameter() {ParameterName = "pDYE_MACHINE_ID", Value =pDYE_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BATCH_PLAN_RESOURCEModel ob = new BATCH_PLAN_RESOURCEModel();
                    ob.id = (dr["ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ID"]);
                    ob.name = (dr["NAME_"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["NAME_"]);
                    ob.SCDL_DURATION = (dr["SCDL_DURATION"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SCDL_DURATION"]);
                    ob.DYE_MC_TYPE_ID = (dr["DYE_MC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DYE_MC_TYPE_ID"]);
                    ob.DYE_TYPE_NAME_EN = (dr["DYE_TYPE_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_TYPE_NAME_EN"]);
                    ob.DYE_MACHINE_NO = (dr["NAME_"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["NAME_"]);
                   
                    ob.NO_NOZLE = (dr["NO_NOZLE"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_NOZLE"]);
                    ob.IS_SMP_BLK = (dr["IS_SMP_BLK"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_SMP_BLK"]);
                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.PCT_EFFCNCY = (dr["PCT_EFFCNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_EFFCNCY"]);
                    ob.D_PRD_CAPACITY = (dr["D_PRD_CAPACITY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["D_PRD_CAPACITY"]);
                    ob.MAX_LOAD = ob.D_PRD_CAPACITY * (ob.PCT_EFFCNCY / 100);
                    ob.START_DT = (dr["START_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["START_DT"]);
                    ob.END_DT = (dr["END_DT"] == DBNull.Value) ? DateTime.Now : Convert.ToDateTime(dr["END_DT"]);
                    ob.name = "<b>" + ob.name + "</b><br />(" + ob.MAX_LOAD.ToString() + " Kg)";
                    ob.LAST_EVT_END = (dr["LAST_EVT_END"] == DBNull.Value) ? ob.START_DT : Convert.ToDateTime(dr["LAST_EVT_END"]).AddMinutes(10);

                    ob.DYE_MACHINE_ID = (dr["ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ID"]);
                  
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public string removeEmptyCell()
        //{
        //    const string sp = "pkg_mc_load_plan.remove_empty_cell";
        //    string jsonStr = "{";
        //    var ob = this;
        //    var i = 1;
        //    try
        //    {
        //        OraDatabase db = new OraDatabase();
        //        var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
        //        {

        //            new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
        //            new CommandParameter() {ParameterName = "pOption", Value =1000},
        //            new CommandParameter() {ParameterName = "pRF_LOCATION_ID", Value = ob.RF_LOCATION_ID},
        //            new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
        //            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}

        //         }, sp);

        //        foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
        //        {
        //            jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
        //            if (i < ds.Tables["OUTPARAM"].Rows.Count)
        //            {
        //                jsonStr += ",";
        //            }
        //            else
        //            {
        //                jsonStr += "}";
        //            }
        //            i++;
        //        }
        //        return jsonStr;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string MODEL_NO { get; set; }

        public decimal TTL_LOAD { get; set; }

        public DateTime LAST_EVT_END { get; set; }

        public decimal LOAD_TO_DO { get; set; }

        public decimal SCDL_DURATION { get; set; }

        public DateTime START_DT { get; set; }

        public DateTime END_DT { get; set; }

        public long DYE_MACHINE_ID { get; set; }

        public string DYE_MACHINE_NO { get; set; }
    }
}