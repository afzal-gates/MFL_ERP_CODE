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
    public class GMT_CPM_EFF_ORD_VOLModel
    {
        public Int64 GMT_CPM_EFF_ORD_VOL_ID { get; set; }
        public Int64? GMT_PRODUCT_TYP_ID { get; set; }
        public Int64? ORDER_VOL_1 { get; set; }
        public Int64? ORDER_VOL_2 { get; set; }
        public Decimal? TOP_EFICNCY { get; set; }
        public Decimal? COST_PER_MIN { get; set; }

        public string PRODUCT_TYP_NAME_EN { get; set; }
        public string GMT_CPM_EFF_ORD_VOL_XML { get; set; }
        public object PRODUCT_TYP
        {
            get
            {
                return new { GMT_PRODUCT_TYP_ID = this.GMT_PRODUCT_TYP_ID, PRODUCT_TYP_NAME_EN = this.PRODUCT_TYP_NAME_EN ?? "" };
            }
        }

        public string BatchSave()
        {
            const string sp = "pkg_planning_common.gmt_cpm_eff_ord_vol_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CPM_EFF_ORD_VOL_ID", Value = ob.GMT_CPM_EFF_ORD_VOL_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value = ob.GMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pORDER_VOL_1", Value = ob.ORDER_VOL_1},
                     new CommandParameter() {ParameterName = "pORDER_VOL_2", Value = ob.ORDER_VOL_2},
                     new CommandParameter() {ParameterName = "pTOP_EFICNCY", Value = ob.TOP_EFICNCY},
                     new CommandParameter() {ParameterName = "pCOST_PER_MIN", Value = ob.COST_PER_MIN},
                     new CommandParameter() {ParameterName = "pGMT_CPM_EFF_ORD_VOL_XML", Value = ob.GMT_CPM_EFF_ORD_VOL_XML},
                     
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


        public List<GMT_CPM_EFF_ORD_VOLModel> GetOrdCpmEffList()
        {
            string sp = "pkg_planning_common.gmt_cpm_eff_ord_vol_select";
            try
            {
                var obList = new List<GMT_CPM_EFF_ORD_VOLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pGMT_CPM_EFF_ORD_VOL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_CPM_EFF_ORD_VOLModel ob = new GMT_CPM_EFF_ORD_VOLModel();
                    ob.GMT_CPM_EFF_ORD_VOL_ID = (dr["GMT_CPM_EFF_ORD_VOL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CPM_EFF_ORD_VOL_ID"]);
                    if (dr["GMT_PRODUCT_TYP_ID"] != DBNull.Value)
                        ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);

                    if (dr["ORDER_VOL_1"] != DBNull.Value)
                        ob.ORDER_VOL_1 = (dr["ORDER_VOL_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_VOL_1"]);
                    if (dr["ORDER_VOL_2"] != DBNull.Value)
                        ob.ORDER_VOL_2 = (dr["ORDER_VOL_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_VOL_2"]);
                    if (dr["TOP_EFICNCY"] != DBNull.Value)
                        ob.TOP_EFICNCY = (dr["TOP_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOP_EFICNCY"]);
                    if (dr["COST_PER_MIN"] != DBNull.Value)
                        ob.COST_PER_MIN = (dr["COST_PER_MIN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PER_MIN"]);

                    ob.PRODUCT_TYP_NAME_EN = (dr["PRODUCT_TYP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRODUCT_TYP_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_CPM_EFF_ORD_VOLModel Select(long ID)
        {
            string sp = "Select_GMT_CPM_EFF_ORD_VOL";
            try
            {
                var ob = new GMT_CPM_EFF_ORD_VOLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_CPM_EFF_ORD_VOL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_CPM_EFF_ORD_VOL_ID = (dr["GMT_CPM_EFF_ORD_VOL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_CPM_EFF_ORD_VOL_ID"]);
                    ob.GMT_PRODUCT_TYP_ID = (dr["GMT_PRODUCT_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PRODUCT_TYP_ID"]);
                    ob.ORDER_VOL_1 = (dr["ORDER_VOL_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_VOL_1"]);
                    ob.ORDER_VOL_2 = (dr["ORDER_VOL_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_VOL_2"]);
                    ob.TOP_EFICNCY = (dr["TOP_EFICNCY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOP_EFICNCY"]);
                    ob.COST_PER_MIN = (dr["COST_PER_MIN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PER_MIN"]);
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