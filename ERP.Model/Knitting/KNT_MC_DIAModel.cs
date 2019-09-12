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
    public class KNT_MC_DIAModel
    {
        public Int64 KNT_MC_DIA_ID { get; set; }
        public string MC_DIA { get; set; }
        public string IS_ACTIVE { get; set; }

        public string Save()
        {
            const string sp = "SP_KNT_MC_DIA";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MC_DIA_ID", Value = ob.KNT_MC_DIA_ID},
                     new CommandParameter() {ParameterName = "pMC_DIA", Value = ob.MC_DIA},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
        public List<KNT_MC_DIAModel> SelectAll()
        {
            string sp = "pkg_knit_common.knt_mc_dia_select";
            try
            {
                var obList = new List<KNT_MC_DIAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MC_DIA_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            KNT_MC_DIAModel ob = new KNT_MC_DIAModel();
                            ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                            ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["MC_DIA"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_MC_DIAModel Select(long ID)
        {
            string sp = "Select_KNT_MC_DIA";
            try
            {
                var ob = new KNT_MC_DIAModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MC_DIA_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                        ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["MC_DIA"]);
                        ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

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