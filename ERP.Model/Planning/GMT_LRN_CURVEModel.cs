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
    public class GMT_LRN_CURVEModel
    {
        public Int64? GMT_LRN_CURVE_ID { get; set; }
        public Int64? GMT_PRODUCT_TYP_ID { get; set; }
        public Int64? RF_FAB_CLASS_ID { get; set; }
        public Int64? DAY_NO { get; set; }
        public Decimal? PLAN_EFICNCY { get; set; }
        public Decimal? LOSS_EFICNCY { get; set; }

        public string GMT_LRN_CURVE_XML { get; set; }

        //public string PRODUCT_TYP_NAME_EN { get; set; }
        //public Int64? LEAST_POSBLE_EFF { get; set; }
        //public Int64? DAY1_PE { get; set; }
        //public Int64? DAY2_PE { get; set; }
        //public Int64? DAY3_PE { get; set; }
        //public Int64? DAY4_PE { get; set; }
        //public Int64? DAY5_PE { get; set; }
        //public Int64? DAY6_PE { get; set; }
        //public Int64? DAY7_PE { get; set; }
        //public Int64? DAY8_PE { get; set; }
        //public Int64? DAY9_PE { get; set; }
        //public Int64? DAY10_PE { get; set; }

        //public Int64? GMT_LRN_CURVE_ID_DAY1 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY2 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY3 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY4 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY5 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY6 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY7 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY8 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY9 { get; set; }
        //public Int64? GMT_LRN_CURVE_ID_DAY10 { get; set; }

        //public string IS_DISABLED_DAY1 { get; set; }
        //public string IS_DISABLED_DAY2 { get; set; }
        //public string IS_DISABLED_DAY3 { get; set; }
        //public string IS_DISABLED_DAY4 { get; set; }
        //public string IS_DISABLED_DAY5 { get; set; }
        //public string IS_DISABLED_DAY6 { get; set; }
        //public string IS_DISABLED_DAY7 { get; set; }
        //public string IS_DISABLED_DAY8 { get; set; }
        //public string IS_DISABLED_DAY9 { get; set; }
        //public string IS_DISABLED_DAY10 { get; set; }

        



        public string BatchSave()
        {
            const string sp = "pkg_planning_common.gmt_lrn_curve_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_LRN_CURVE_ID", Value = ob.GMT_LRN_CURVE_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = ob.GMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value = ob.RF_FAB_CLASS_ID},
                     new CommandParameter() {ParameterName = "pDAY_NO", Value = ob.DAY_NO},
                     new CommandParameter() {ParameterName = "pPLAN_EFICNCY", Value = ob.PLAN_EFICNCY},
                     new CommandParameter() {ParameterName = "pLOSS_EFICNCY", Value = ob.LOSS_EFICNCY},

                     new CommandParameter() {ParameterName = "pGMT_LRN_CURVE_XML", Value = ob.GMT_LRN_CURVE_XML},
                     
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
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_LRN_CURVEModel Select(long ID)
        {
            string sp = "Select_GMT_LRN_CURVE";
            try
            {
                var ob = new GMT_LRN_CURVEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_LRN_CURVE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_LRN_CURVE_ID = (dr["GMT_LRN_CURVE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);
                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_CLASS_ID"]);
                    ob.DAY_NO = (dr["DAY_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY_NO"]);
                    ob.PLAN_EFICNCY = (dr["PLAN_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_EFICNCY"]);
                    ob.LOSS_EFICNCY = (dr["LOSS_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOSS_EFICNCY"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



    public class GMT_LRN_CURVE_VMModel
    {        
        public Int64? GMT_PRODUCT_TYP_ID { get; set; }
        public Int64? RF_FAB_CLASS_ID { get; set; }        
        public string PRODUCT_TYP_NAME_EN { get; set; }
        public Int64? LEAST_POSBLE_EFF { get; set; }
        public Int64? DAY1_PE { get; set; }
        public Int64? DAY2_PE { get; set; }
        public Int64? DAY3_PE { get; set; }
        public Int64? DAY4_PE { get; set; }
        public Int64? DAY5_PE { get; set; }
        public Int64? DAY6_PE { get; set; }
        public Int64? DAY7_PE { get; set; }
        public Int64? DAY8_PE { get; set; }
        public Int64? DAY9_PE { get; set; }
        public Int64? DAY10_PE { get; set; }

        public Int64? GMT_LRN_CURVE_ID_DAY1 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY2 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY3 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY4 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY5 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY6 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY7 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY8 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY9 { get; set; }
        public Int64? GMT_LRN_CURVE_ID_DAY10 { get; set; }

        public string IS_DISABLED_DAY1 { get; set; }
        public string IS_DISABLED_DAY2 { get; set; }
        public string IS_DISABLED_DAY3 { get; set; }
        public string IS_DISABLED_DAY4 { get; set; }
        public string IS_DISABLED_DAY5 { get; set; }
        public string IS_DISABLED_DAY6 { get; set; }
        public string IS_DISABLED_DAY7 { get; set; }
        public string IS_DISABLED_DAY8 { get; set; }
        public string IS_DISABLED_DAY9 { get; set; }
        public string IS_DISABLED_DAY10 { get; set; }


        public List<GMT_LRN_CURVE_VMModel> GetLearningCurvList(Int64 pRF_FAB_CLASS_ID)
        {
            string sp = "pkg_planning_common.gmt_lrn_curve_select";
            try
            {
                var obList = new List<GMT_LRN_CURVE_VMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value = pRF_FAB_CLASS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_LRN_CURVE_VMModel ob = new GMT_LRN_CURVE_VMModel();
                    //ob.GMT_LRN_CURVE_ID = (dr["GMT_LRN_CURVE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);
                    ob.RF_FAB_CLASS_ID = (dr["RF_FAB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_CLASS_ID"]);
                    ob.LEAST_POSBLE_EFF = (dr["LEAST_POSBLE_EFF"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAST_POSBLE_EFF"]);

                    ob.PRODUCT_TYP_NAME_EN = (dr["PRODUCT_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_NAME_EN"]);

                    if (dr["DAY1_PE"] != DBNull.Value)
                        ob.DAY1_PE = (dr["DAY1_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY1_PE"]);
                    if (dr["DAY2_PE"] != DBNull.Value)
                        ob.DAY2_PE = (dr["DAY2_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY2_PE"]);
                    if (dr["DAY3_PE"] != DBNull.Value)
                        ob.DAY3_PE = (dr["DAY3_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY3_PE"]);
                    if (dr["DAY4_PE"] != DBNull.Value)
                        ob.DAY4_PE = (dr["DAY4_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY4_PE"]);
                    if (dr["DAY5_PE"] != DBNull.Value)
                        ob.DAY5_PE = (dr["DAY5_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY5_PE"]);
                    if (dr["DAY6_PE"] != DBNull.Value)
                        ob.DAY6_PE = (dr["DAY6_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY6_PE"]);
                    if (dr["DAY7_PE"] != DBNull.Value)
                        ob.DAY7_PE = (dr["DAY7_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY7_PE"]);
                    if (dr["DAY8_PE"] != DBNull.Value)
                        ob.DAY8_PE = (dr["DAY8_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY8_PE"]);
                    if (dr["DAY9_PE"] != DBNull.Value)
                        ob.DAY9_PE = (dr["DAY9_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY9_PE"]);
                    if (dr["DAY10_PE"] != DBNull.Value)
                        ob.DAY10_PE = (dr["DAY10_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DAY10_PE"]);

                    if (dr["GMT_LRN_CURVE_ID_DAY1"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY1 = (dr["DAY1_PE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY1"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY2"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY2 = (dr["GMT_LRN_CURVE_ID_DAY2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY2"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY3"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY3 = (dr["GMT_LRN_CURVE_ID_DAY3"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY3"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY4"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY4 = (dr["GMT_LRN_CURVE_ID_DAY4"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY4"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY5"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY5 = (dr["GMT_LRN_CURVE_ID_DAY5"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY5"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY6"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY6 = (dr["GMT_LRN_CURVE_ID_DAY6"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY6"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY7"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY7 = (dr["GMT_LRN_CURVE_ID_DAY7"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY7"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY8"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY8 = (dr["GMT_LRN_CURVE_ID_DAY8"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY8"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY9"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY9 = (dr["GMT_LRN_CURVE_ID_DAY9"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY9"]);
                    if (dr["GMT_LRN_CURVE_ID_DAY10"] != DBNull.Value)
                        ob.GMT_LRN_CURVE_ID_DAY10 = (dr["GMT_LRN_CURVE_ID_DAY10"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_LRN_CURVE_ID_DAY10"]);

                    ob.IS_DISABLED_DAY1 = (dr["IS_DISABLED_DAY1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY1"]);
                    ob.IS_DISABLED_DAY2 = (dr["IS_DISABLED_DAY2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY2"]);
                    ob.IS_DISABLED_DAY3 = (dr["IS_DISABLED_DAY3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY3"]);
                    ob.IS_DISABLED_DAY4 = (dr["IS_DISABLED_DAY4"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY4"]);
                    ob.IS_DISABLED_DAY5 = (dr["IS_DISABLED_DAY5"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY5"]);
                    ob.IS_DISABLED_DAY6 = (dr["IS_DISABLED_DAY6"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY6"]);
                    ob.IS_DISABLED_DAY7 = (dr["IS_DISABLED_DAY7"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY7"]);
                    ob.IS_DISABLED_DAY8 = (dr["IS_DISABLED_DAY8"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY8"]);
                    ob.IS_DISABLED_DAY9 = (dr["IS_DISABLED_DAY9"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY9"]);
                    ob.IS_DISABLED_DAY10 = (dr["IS_DISABLED_DAY10"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED_DAY10"]);

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