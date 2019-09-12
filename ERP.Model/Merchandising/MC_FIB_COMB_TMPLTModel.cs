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
    public class MC_FIB_COMB_TMPLTModel
    {
        public Int64 MC_FIB_COMB_TMPLT_ID { get; set; }
        public string FIB_COMB_TMPLT_NAME { get; set; }
        public string XML { get; set; }

        public string IS_ELA_MXD { get; set; }

        //private List<MC_FIB_COMB_CFGModel> _com_cfg_list = null;

        //public List<MC_FIB_COMB_CFGModel> com_cfg_list
        //{
        //    get
        //    {
        //        if (_com_cfg_list == null)
        //        {
        //            _com_cfg_list = new List<MC_FIB_COMB_CFGModel>();
        //        }
        //        return _com_cfg_list;
        //    }
        //    set
        //    {
        //        _com_cfg_list = value;
        //    }
        //}

        public string Save()
        {
            const string sp = "pkg_fabrication.fiber_conf_modal_insert";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FIB_COMB_TMPLT_ID", Value = ob.MC_FIB_COMB_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pFIB_COMB_TMPLT_NAME", Value = ob.FIB_COMB_TMPLT_NAME},
                     new CommandParameter() {ParameterName = "pIS_ELA_MXD", Value = ob.IS_ELA_MXD},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "V_MC_FIB_COMB_TMPLT_ID", Value =500, Direction = ParameterDirection.Output}
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_FIB_COMB_TMPLTModel> SelectAll(Int64? pMC_FIB_COMB_TMPLT_ID)
        {
            string sp = "pkg_fabrication.mc_fib_comb_tmplt_select";
            try
            {
                var obList = new List<MC_FIB_COMB_TMPLTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FIB_COMB_TMPLT_ID", Value = pMC_FIB_COMB_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_FIB_COMB_TMPLTModel ob = new MC_FIB_COMB_TMPLTModel();
                    ob.MC_FIB_COMB_TMPLT_ID = (dr["MC_FIB_COMB_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FIB_COMB_TMPLT_ID"]);
                    ob.FIB_COMB_TMPLT_NAME = (dr["FIB_COMB_TMPLT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMB_TMPLT_NAME"]);
                    ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? "" : Convert.ToString(dr["IS_ELA_MXD"]);
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