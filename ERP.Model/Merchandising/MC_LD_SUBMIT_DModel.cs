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
    public class MC_LD_SUBMIT_DModel
    {
        public Int64 MC_LD_SUBMIT_D_ID { get; set; }
        public Int64 MC_LD_SUBMIT_ID { get; set; }
        public string OPTION_NO { get; set; }
        public string LD_REF { get; set; }
        public string IS_APROVED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_MC_LD_SUBMIT_D";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_D_ID", Value = ob.MC_LD_SUBMIT_D_ID},
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value = ob.MC_LD_SUBMIT_ID},
                     new CommandParameter() {ParameterName = "pOPTION_NO", Value = ob.OPTION_NO},
                     new CommandParameter() {ParameterName = "pLD_REF", Value = ob.LD_REF},
                     new CommandParameter() {ParameterName = "pIS_APROVED", Value = ob.IS_APROVED},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<MC_LD_SUBMIT_DModel> QueryData(Int64 pMC_LD_SUBMIT_ID)
        {
            string sp = "PKG_Labdip_action.mc_ld_submit_d_select";
            try
            {
                var obList = new List<MC_LD_SUBMIT_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_ID", Value = pMC_LD_SUBMIT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);



                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        MC_LD_SUBMIT_DModel ob = new MC_LD_SUBMIT_DModel();
                        ob.MC_LD_SUBMIT_D_ID = (dr["MC_LD_SUBMIT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_SUBMIT_D_ID"]);
                        ob.MC_LD_SUBMIT_ID = (dr["MC_LD_SUBMIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_SUBMIT_ID"]);
                        ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);
                        ob.LD_REF = (dr["LD_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF"]);
                        ob.IS_APROVED = (dr["IS_APROVED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_APROVED"]);
                        obList.Add(ob);
                    }
                }
                else
                {

                    obList.Add(new MC_LD_SUBMIT_DModel() { 
                         MC_LD_SUBMIT_D_ID = -1,
                         OPTION_NO = "A",
                         LD_REF = "",
                         IS_APROVED = "N"
                    });

                    obList.Add(new MC_LD_SUBMIT_DModel()
                    {
                        MC_LD_SUBMIT_D_ID = -1,
                        OPTION_NO = "B",
                        LD_REF = "",
                        IS_APROVED = "N"
                    });

                    obList.Add(new MC_LD_SUBMIT_DModel()
                    {
                        MC_LD_SUBMIT_D_ID = -1,
                        OPTION_NO = "C",
                        LD_REF = "",
                        IS_APROVED = "N"
                    });

                }




                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_LD_SUBMIT_DModel Select(long ID)
        {
            string sp = "Select_MC_LD_SUBMIT_D";
            try
            {
                var ob = new MC_LD_SUBMIT_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_LD_SUBMIT_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_LD_SUBMIT_D_ID = (dr["MC_LD_SUBMIT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_SUBMIT_D_ID"]);
                        ob.MC_LD_SUBMIT_ID = (dr["MC_LD_SUBMIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_LD_SUBMIT_ID"]);
                        ob.OPTION_NO = (dr["OPTION_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPTION_NO"]);
                        ob.LD_REF = (dr["LD_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LD_REF"]);
                        ob.IS_APROVED = (dr["IS_APROVED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_APROVED"]);
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