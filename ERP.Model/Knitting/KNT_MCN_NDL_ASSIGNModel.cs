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
    public class KNT_MCN_NDL_ASSIGNModel
    {
        public Int64? KNT_MCN_NDL_ASSIGN_ID { get; set; }
        public Int64? KNT_MACHINE_ID { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public Int64? ASSIGN_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_FINALIZED { get; set; }
        public object INV_ITEM
        {
            get
            {
                return new { INV_ITEM_ID = this.INV_ITEM_ID, ITEM_NAME_EN = this.ITEM_NAME_EN ?? "" };
            }
        }
        public string KNT_MACHINE_NO { get; set; }
        public string MC_DIA { get; set; }
        public string MC_GG { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string MC_TYPE_NAME_EN { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public decimal? COST_PRICE { get; set; }
        public string QTY_MOU_CODE { get; set; }
        public string MCN_NDL_ASSIGN_XML { get; set; }


        public string BatchSave()
        {
            const string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_assign_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pKNT_MCN_NDL_ASSIGN_ID", Value = ob.KNT_MCN_NDL_ASSIGN_ID},
                     //new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     //new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     //new CommandParameter() {ParameterName = "pASSIGN_QTY", Value = ob.ASSIGN_QTY},
                     new CommandParameter() {ParameterName = "pMCN_NDL_ASSIGN_XML", Value = ob.MCN_NDL_ASSIGN_XML},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},                     
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


        public List<KNT_MCN_NDL_ASSIGNModel> GetNeedleAssignList(Int64 pHR_OFFICE_ID, Int64 pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_assign_select";
            try
            {
                var obList = new List<KNT_MCN_NDL_ASSIGNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MCN_NDL_ASSIGNModel ob = new KNT_MCN_NDL_ASSIGNModel();
                    ob.KNT_MCN_NDL_ASSIGN_ID = (dr["KNT_MCN_NDL_ASSIGN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_NDL_ASSIGN_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ASSIGN_QTY = (dr["ASSIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ASSIGN_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.MC_GG = (dr["MC_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.MC_TYPE_NAME_EN = (dr["MC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_TYPE_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetAssignNdleItemListByMc(Int64 pKNT_MACHINE_ID)
        {
            string sp = "pkg_knt_mcn_needle.knt_mcn_ndl_assign_select";
            try
            {
                var obList = new List<KNT_MCN_NDL_ASSIGNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = pKNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MCN_NDL_ASSIGNModel ob = new KNT_MCN_NDL_ASSIGNModel();
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);

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