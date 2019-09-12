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
    public class GMT_HRLY_PRODModel
    {
        public Int64 GMT_HRLY_PROD_ID { get; set; }
        public Int64 GMT_LN_LOAD_PLAN_ID { get; set; }
        public Int64 HR_PROD_LINE_ID { get; set; }
        public DateTime PROD_DT { get; set; }
        public Int64 H1_PRD_QTY { get; set; }
        public Int64 H2_PRD_QTY { get; set; }
        public Int64 H3_PRD_QTY { get; set; }
        public Int64 H4_PRD_QTY { get; set; }
        public Int64 H5_PRD_QTY { get; set; }
        public Int64 H6_PRD_QTY { get; set; }
        public Int64 H7_PRD_QTY { get; set; }
        public Int64 H8_PRD_QTY { get; set; }
        public Int64 OT_TGT_HR { get; set; }
        public Int64 OT_TGT_QTY { get; set; }
        public Int64 OT_ACT_PRD_QTY { get; set; }
        public Int64 HRLY_TGT_QTY { get; set; }
        public Int64 CUR_OPERATOR { get; set; }
        public Int64 CUR_HELPER { get; set; }
        public Int64 NO_OF_MC { get; set; }
        public string REMARKS { get; set; }


        public string Save()
        {
            const string sp = "SP_GMT_HRLY_PROD";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_HRLY_PROD_ID", Value = ob.GMT_HRLY_PROD_ID},
                     new CommandParameter() {ParameterName = "pGMT_LN_LOAD_PLAN_ID", Value = ob.GMT_LN_LOAD_PLAN_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = ob.PROD_DT},
                     new CommandParameter() {ParameterName = "pH1_PRD_QTY", Value = ob.H1_PRD_QTY},
                     new CommandParameter() {ParameterName = "pH2_PRD_QTY", Value = ob.H2_PRD_QTY},
                     new CommandParameter() {ParameterName = "pH3_PRD_QTY", Value = ob.H3_PRD_QTY},
                     new CommandParameter() {ParameterName = "pH4_PRD_QTY", Value = ob.H4_PRD_QTY},
                     new CommandParameter() {ParameterName = "pH5_PRD_QTY", Value = ob.H5_PRD_QTY},
                     new CommandParameter() {ParameterName = "pH6_PRD_QTY", Value = ob.H6_PRD_QTY},
                     new CommandParameter() {ParameterName = "pH7_PRD_QTY", Value = ob.H7_PRD_QTY},
                     new CommandParameter() {ParameterName = "pH8_PRD_QTY", Value = ob.H8_PRD_QTY},
                     new CommandParameter() {ParameterName = "pOT_TGT_HR", Value = ob.OT_TGT_HR},
                     new CommandParameter() {ParameterName = "pOT_TGT_QTY", Value = ob.OT_TGT_QTY},
                     new CommandParameter() {ParameterName = "pOT_ACT_PRD_QTY", Value = ob.OT_ACT_PRD_QTY},
                     new CommandParameter() {ParameterName = "pHRLY_TGT_QTY", Value = ob.HRLY_TGT_QTY},
                     new CommandParameter() {ParameterName = "pCUR_OPERATOR", Value = ob.CUR_OPERATOR},
                     new CommandParameter() {ParameterName = "pCUR_HELPER", Value = ob.CUR_HELPER},
                     new CommandParameter() {ParameterName = "pNO_OF_MC", Value = ob.NO_OF_MC},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},

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

        public List<GMT_HRLY_PRODModel> SelectAll()
        {
            string sp = "Select_GMT_HRLY_PROD";
            try
            {
                var obList = new List<GMT_HRLY_PRODModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_HRLY_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            GMT_HRLY_PRODModel ob = new GMT_HRLY_PRODModel();
                            ob.GMT_HRLY_PROD_ID = (dr["GMT_HRLY_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_HRLY_PROD_ID"]);
                            ob.GMT_LN_LOAD_PLAN_ID = (dr["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LN_LOAD_PLAN_ID"]);
                            ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                            ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                            ob.H1_PRD_QTY = (dr["H1_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H1_PRD_QTY"]);
                            ob.H2_PRD_QTY = (dr["H2_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H2_PRD_QTY"]);
                            ob.H3_PRD_QTY = (dr["H3_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H3_PRD_QTY"]);
                            ob.H4_PRD_QTY = (dr["H4_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H4_PRD_QTY"]);
                            ob.H5_PRD_QTY = (dr["H5_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H5_PRD_QTY"]);
                            ob.H6_PRD_QTY = (dr["H6_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H6_PRD_QTY"]);
                            ob.H7_PRD_QTY = (dr["H7_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H7_PRD_QTY"]);
                            ob.H8_PRD_QTY = (dr["H8_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H8_PRD_QTY"]);
                            ob.OT_TGT_HR = (dr["OT_TGT_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_TGT_HR"]);
                            ob.OT_TGT_QTY = (dr["OT_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_TGT_QTY"]);
                            ob.OT_ACT_PRD_QTY = (dr["OT_ACT_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_ACT_PRD_QTY"]);
                            ob.HRLY_TGT_QTY = (dr["HRLY_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HRLY_TGT_QTY"]);
                            ob.CUR_OPERATOR = (dr["CUR_OPERATOR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUR_OPERATOR"]);
                            ob.CUR_HELPER = (dr["CUR_HELPER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUR_HELPER"]);
                            ob.NO_OF_MC = (dr["NO_OF_MC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_MC"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_HRLY_PRODModel Select(long ID)
        {
            string sp = "Select_GMT_HRLY_PROD";
            try
            {
                var ob = new GMT_HRLY_PRODModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_HRLY_PROD_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.GMT_HRLY_PROD_ID = (dr["GMT_HRLY_PROD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_HRLY_PROD_ID"]);
                        ob.GMT_LN_LOAD_PLAN_ID = (dr["GMT_LN_LOAD_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LN_LOAD_PLAN_ID"]);
                        ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);
                        ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                        ob.H1_PRD_QTY = (dr["H1_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H1_PRD_QTY"]);
                        ob.H2_PRD_QTY = (dr["H2_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H2_PRD_QTY"]);
                        ob.H3_PRD_QTY = (dr["H3_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H3_PRD_QTY"]);
                        ob.H4_PRD_QTY = (dr["H4_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H4_PRD_QTY"]);
                        ob.H5_PRD_QTY = (dr["H5_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H5_PRD_QTY"]);
                        ob.H6_PRD_QTY = (dr["H6_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H6_PRD_QTY"]);
                        ob.H7_PRD_QTY = (dr["H7_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H7_PRD_QTY"]);
                        ob.H8_PRD_QTY = (dr["H8_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["H8_PRD_QTY"]);
                        ob.OT_TGT_HR = (dr["OT_TGT_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_TGT_HR"]);
                        ob.OT_TGT_QTY = (dr["OT_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_TGT_QTY"]);
                        ob.OT_ACT_PRD_QTY = (dr["OT_ACT_PRD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OT_ACT_PRD_QTY"]);
                        ob.HRLY_TGT_QTY = (dr["HRLY_TGT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HRLY_TGT_QTY"]);
                        ob.CUR_OPERATOR = (dr["CUR_OPERATOR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUR_OPERATOR"]);
                        ob.CUR_HELPER = (dr["CUR_HELPER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUR_HELPER"]);
                        ob.NO_OF_MC = (dr["NO_OF_MC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_MC"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

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