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
    public class MC_BUYER_BOMModel
    {
        public Int64 MC_BUYER_BOM_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public Int64 MC_BYR_ACC_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal RATE_EST { get; set; }
        public Int64 PURCH_MOU_ID { get; set; }
        public string IS_COMMON { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 CONS_MOU_ID { get; set; }

        public Int64 LK_ACS_CONS_TYPE_ID { get; set; }
        public string IS_SUPL_NOM { get; set; }
        public List<MC_BYR_NOM_SUPModel> NOM_SUP_LST { get; set; }


        public string ITEM_NAME_EN { get; set; }
        public string XML { get; set; }

        public string Save()
        {
            const string sp = "pkg_mc_style.mc_buyer_bom_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                      new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
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

        public List<MC_BUYER_BOMModel> SelectAll()
        {
            string sp = "Select_MC_BUYER_BOM";
            try
            {
                var obList = new List<MC_BUYER_BOMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_BOM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BUYER_BOMModel ob = new MC_BUYER_BOMModel();
                    ob.MC_BUYER_BOM_ID = (dr["MC_BUYER_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_BOM_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RATE_EST = (dr["RATE_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_EST"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.LK_ACS_CONS_TYPE_ID = (dr["LK_ACS_CONS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACS_CONS_TYPE_ID"]);
                    ob.IS_SUPL_NOM = (dr["IS_SUPL_NOM"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SUPL_NOM"]);
                    ob.IS_COMMON = (dr["IS_COMMON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_COMMON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BUYER_BOMModel Select(Int64? pMC_BUYER_BOM_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pINV_ITEM_ID = null)
        {
            string sp = "pkg_mc_style.mc_buyer_bom_select";
            try
            {
                var ob = new MC_BUYER_BOMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_BOM_ID", Value =pMC_BUYER_BOM_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value =pINV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BUYER_BOM_ID = (dr["MC_BUYER_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_BOM_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.RATE_EST = (dr["RATE_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_EST"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.LK_ACS_CONS_TYPE_ID = (dr["LK_ACS_CONS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ACS_CONS_TYPE_ID"]);
                    ob.IS_SUPL_NOM = (dr["IS_SUPL_NOM"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SUPL_NOM"]);
                    ob.IS_COMMON = (dr["IS_COMMON"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_COMMON"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);

                    ob.NOM_SUP_LST = new MC_BYR_NOM_SUPModel().SelectByID(ob.MC_BYR_ACC_ID, ob.INV_ITEM_ID);

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