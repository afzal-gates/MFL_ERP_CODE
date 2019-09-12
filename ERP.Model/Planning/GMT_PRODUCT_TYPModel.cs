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
    public class GMT_PRODUCT_TYPModel
    {
        public Int64 GMT_PRODUCT_TYP_ID { get; set; }
        public string PRODUCT_TYP_CODE { get; set; }
        public string PRODUCT_TYP_NAME_EN { get; set; }
        public string PRODUCT_TYP_NAME_BN { get; set; }
        public string PRODUCT_TYP_SNAME { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public Int64 NO_OF_OP_1 { get; set; }
        public Int64 NO_OF_OP_2 { get; set; }
        public Int64 NO_OF_HLP_1 { get; set; }
        public Int64 NO_OF_HLP_2 { get; set; }
        public Decimal SMV_1 { get; set; }
        public Decimal SMV_2 { get; set; }
        public Int64 LC_PICK_DAYS { get; set; }
        public Int64 AVG_MP { get; set; }
        public Decimal AVG_SMV { get; set; }
        public Decimal LEAST_POSBLE_EFF { get; set; }
        public string XML { get; set; }
        public Int32 Option { get; set; }
        public Int64? HR_PROD_FLR_ID { get; set; }
        public Int64? HR_PROD_LINE_ID { get; set; }


        public string Save()
        {
            const string sp = "pkg_planning_common.gmt_prod_type_line_map_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = ob.GMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = Option},
                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},
                     new CommandParameter() {ParameterName = "pHR_PROD_LINE_ID", Value = ob.HR_PROD_LINE_ID},
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

        public List<GMT_PRODUCT_TYPModel> GetProdTypeList()
        {
            string sp = "pkg_planning_common.gmt_product_typ_select";
            try
            {
                var obList = new List<GMT_PRODUCT_TYPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PRODUCT_TYPModel ob = new GMT_PRODUCT_TYPModel();
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);
                    ob.PRODUCT_TYP_CODE = (dr["PRODUCT_TYP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_CODE"]);
                    ob.PRODUCT_TYP_NAME_EN = (dr["PRODUCT_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_NAME_EN"]);
                    ob.PRODUCT_TYP_NAME_BN = (dr["PRODUCT_TYP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_NAME_BN"]);
                    ob.PRODUCT_TYP_SNAME = (dr["PRODUCT_TYP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.NO_OF_OP_1 = (dr["NO_OF_OP_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_OP_1"]);
                    ob.NO_OF_OP_2 = (dr["NO_OF_OP_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_OP_2"]);
                    ob.NO_OF_HLP_1 = (dr["NO_OF_HLP_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_HLP_1"]);
                    ob.NO_OF_HLP_2 = (dr["NO_OF_HLP_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_HLP_2"]);
                    ob.SMV_1 = (dr["SMV_1"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV_1"]);
                    ob.SMV_2 = (dr["SMV_2"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV_2"]);

                    ob.LC_PICK_DAYS = (dr["LC_PICK_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LC_PICK_DAYS"]);
                    ob.AVG_MP = (dr["AVG_MP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AVG_MP"]);
                    ob.AVG_SMV = (dr["AVG_SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_SMV"]);
                    ob.LEAST_POSBLE_EFF = (dr["LEAST_POSBLE_EFF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LEAST_POSBLE_EFF"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_PRODUCT_TYPModel Select(long ID)
        {
            string sp = "Select_GMT_PRODUCT_TYP";
            try
            {
                var ob = new GMT_PRODUCT_TYPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);
                    ob.PRODUCT_TYP_CODE = (dr["PRODUCT_TYP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_CODE"]);
                    ob.PRODUCT_TYP_NAME_EN = (dr["PRODUCT_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_NAME_EN"]);
                    ob.PRODUCT_TYP_NAME_BN = (dr["PRODUCT_TYP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_NAME_BN"]);
                    ob.PRODUCT_TYP_SNAME = (dr["PRODUCT_TYP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_SNAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.NO_OF_OP_1 = (dr["NO_OF_OP_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_OP_1"]);
                    ob.NO_OF_OP_2 = (dr["NO_OF_OP_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_OP_2"]);
                    ob.NO_OF_HLP_1 = (dr["NO_OF_HLP_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_HLP_1"]);
                    ob.NO_OF_HLP_2 = (dr["NO_OF_HLP_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_HLP_2"]);
                    ob.SMV_1 = (dr["SMV_1"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV_1"]);
                    ob.SMV_2 = (dr["SMV_2"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV_2"]);
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