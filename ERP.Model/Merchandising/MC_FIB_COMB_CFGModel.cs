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
    public class MC_FIB_COMB_CFGModel
    {
        public Int64 MC_FIB_COMB_CFG_ID { get; set; }
        public string LK_FIB_TYPE_LST { get; set; }
        public string IS_100PCT { get; set; }
        public string IS_BLEND_A_F { get; set; }
        public string IS_ELA_MXD { get; set; }
        public string IS_100PCT_ELA { get; set; }
        private List<MC_FIB_COMB_LSTModel> _FIB_COMB_LST = null;
        public List<MC_FIB_COMB_LSTModel> FIB_COMB_LST
        {
            get
            {
                if (_FIB_COMB_LST == null)
                {
                    _FIB_COMB_LST = new List<MC_FIB_COMB_LSTModel>();
                }
                return _FIB_COMB_LST;
            }
            set
            {
                _FIB_COMB_LST = value;
            }
        }
        public string COMBINATION_NAME { get; set; }


        public string Save()
        {
            const string sp = "SP_MC_FIB_COMB_CFG";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FIB_COMB_CFG_ID", Value = ob.MC_FIB_COMB_CFG_ID},
                     new CommandParameter() {ParameterName = "pLK_FIB_TYPE_LST", Value = ob.LK_FIB_TYPE_LST},
                     new CommandParameter() {ParameterName = "pIS_100PCT", Value = ob.IS_100PCT},
                     new CommandParameter() {ParameterName = "pIS_BLEND_A_F", Value = ob.IS_BLEND_A_F},
                     new CommandParameter() {ParameterName = "pIS_ELA_MXD", Value = ob.IS_ELA_MXD},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
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
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_FIB_COMB_CFGModel> SelectAll(Int64? pOption, Int64? pMC_FIB_COMB_TMPLT_ID)
        {
            string sp = "pkg_fabrication.mc_fib_comb_cfg_select";
            try
            {
                var obList = new List<MC_FIB_COMB_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FIB_COMB_TMPLT_ID", Value = pMC_FIB_COMB_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_FIB_COMB_CFGModel ob = new MC_FIB_COMB_CFGModel();
                            ob.MC_FIB_COMB_CFG_ID = (dr["MC_FIB_COMB_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FIB_COMB_CFG_ID"]);
                            ob.LK_FIB_TYPE_LST = (dr["LK_FIB_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FIB_TYPE_LST"]);
                            ob.IS_100PCT = (dr["IS_100PCT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_100PCT"]);
                            ob.IS_BLEND_A_F = (dr["IS_BLEND_A_F"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BLEND_A_F"]);
                            ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ELA_MXD"]);
                            ob.IS_100PCT_ELA = (dr["IS_100PCT_ELA"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_100PCT_ELA"]);
                            ob.COMBINATION_NAME = (dr["COMBINATION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBINATION_NAME"]);
                  

                            var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                        new CommandParameter() {ParameterName = "pMC_FIB_COMB_CFG_ID", Value =ob.MC_FIB_COMB_CFG_ID },
                                        new CommandParameter() {ParameterName = "pOption", Value =3003},
                                        new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                  }, sp);
                            foreach (DataRow dr1 in ds1.Tables[0].Rows)
                            {
                                MC_FIB_COMB_LSTModel ob1 = new MC_FIB_COMB_LSTModel();
                                ob1.MC_FIB_COMB_LST_ID = (dr1["MC_FIB_COMB_LST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_FIB_COMB_LST_ID"]);
                                ob1.FIB_COMBINATION_ID = (dr1["FIB_COMBINATION_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["FIB_COMBINATION_ID"]);
                                ob.FIB_COMB_LST.Add(ob1);
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

    }
}