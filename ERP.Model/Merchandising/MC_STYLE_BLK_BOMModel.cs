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
    public class MC_STYLE_BLK_BOMModel
    {
        public Int64 MC_STYLE_BLK_BOM_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal? QTY_EST { get; set; }
        public Decimal? PCT_ADD_QTY { get; set; }
        public Decimal QTY_ACT { get; set; }
        public Decimal? RATE_EST { get; set; }
        public Decimal RATE_ACT { get; set; }
        public Int64 ACCT_MOU_ID { get; set; }
        public Int64 CONS_MOU_ID { get; set; }
        public Int64 PURCH_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public Int64? REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string SUGGESTED { get; set; }
        public string XML { get; set; }
        public decimal PURCH_PRICE { get; set; }
        public decimal IMP_PRICE { get; set; }
        public decimal CONV_FACTOR { get; set; }
        public Int64 UNIT_QTY_REQ { get; set; }
        public Int64? LK_FAB_QTY_SRC { get; set; }
        public Int16 MC_STYL_BGT_H_ID { get; set; }
        public Boolean IS_TAB_ACT { get; set; }
        public string ACCT_MOU_NAME { get; set; }
        public string PURCH_MOU_NAME { get; set; }
        public long? MC_BUYER_ID { get; set; }
        public Int64 MC_ACCS_PO_D_ITEM_ID { get; set; }
        public string IS_PO_CREATED { get; set; }

        //private List<MC_ACCS_PO_D_SPECModel> _ITEM_SPEC_LIST = null;
        //public List<MC_ACCS_PO_D_SPECModel> ITEM_SPEC_LIST
        //{
        //    get
        //    {
        //        if (_ITEM_SPEC_LIST == null)
        //        {
        //            _ITEM_SPEC_LIST = new List<MC_ACCS_PO_D_SPECModel>();
        //        }
        //        return _ITEM_SPEC_LIST;
        //    }
        //    set
        //    {
        //        _ITEM_SPEC_LIST = value;
        //    }
        //}

        public string Save()
        {
            const string sp = "pkg_budget_sheet.mc_style_d_bom_insert";

            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = ob.MC_STYL_BGT_H_ID},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opMC_STYL_BGT_H_ID", Value =0, Direction = ParameterDirection.Output}

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
        public string Delete()
        {
            const string sp = "SP_MC_STYLE_BLK_BOM";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_BLK_BOM_ID", Value = ob.MC_STYLE_BLK_BOM_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<MC_STYLE_BLK_BOMModel> Query(string pBLK_BOM_LIST, Int64? pBLK_BOM_ACT = null, Int64? pMC_ACCS_PO_H_ID = null, Int64? pSCM_PURC_REQ_H_ID = null, Int64? pMC_STYL_BGT_H_ID = null, Int64? pOption=null)
        {
            string sp = "pkg_budget_sheet.mc_style_blk_bom_select";
            try
            {
                var obList = new List<MC_STYLE_BLK_BOMModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pBLK_BOM_LIST", Value = pBLK_BOM_LIST },
                     new CommandParameter() {ParameterName = "pMC_ACCS_PO_H_ID", Value = pMC_ACCS_PO_H_ID },
                     new CommandParameter() {ParameterName = "pMC_STYL_BGT_H_ID", Value = pMC_STYL_BGT_H_ID },
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_H_ID", Value = pSCM_PURC_REQ_H_ID },
                     new CommandParameter() {ParameterName = "pOption", Value = pOption==null?3000:pOption },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_BLK_BOMModel ob = new MC_STYLE_BLK_BOMModel();
                    ob.MC_STYLE_BLK_BOM_ID = (dr["MC_STYLE_BLK_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_BLK_BOM_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.QTY_EST = (dr["QTY_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_EST"]);
                    ob.PCT_ADD_QTY = (dr["PCT_ADD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ADD_QTY"]);
                    ob.RATE_EST = (dr["RATE_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_EST"]);

                    ob.ACCT_MOU_ID = (dr["ACCT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCT_MOU_ID"]);
                    ob.ACCT_MOU_NAME = (dr["ACCT_MOU_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACCT_MOU_NAME"]);

                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.PURCH_MOU_NAME = (dr["PURCH_MOU_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURCH_MOU_NAME"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.IS_TAB_ACT = (pBLK_BOM_ACT ?? 0) == ob.MC_STYLE_BLK_BOM_ID;
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_ACCS_PO_D_ITEM_ID = (dr["MC_ACCS_PO_D_ITEM_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_ACCS_PO_D_ITEM_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.ORD_MOU_CODE = (dr["ORD_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_MOU_CODE"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);

                    if (dr["ITEM_CAT_NAME_EN"] != DBNull.Value)
                        ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    if (dr["BUYER_NAME_EN"] != DBNull.Value)
                        ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);

                    //ob.ITEM_SPEC_LIST = new MC_ACCS_PO_D_SPECModel().Query(ob.MC_ACCS_PO_D_ITEM_ID);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYLE_BLK_BOMModel Select(long ID)
        {
            string sp = "Select_MC_STYLE_BLK_BOM";
            try
            {
                var ob = new MC_STYLE_BLK_BOMModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_BLK_BOM_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_STYLE_BLK_BOM_ID = (dr["MC_STYLE_BLK_BOM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_BLK_BOM_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.QTY_EST = (dr["QTY_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_EST"]);
                    ob.PCT_ADD_QTY = (dr["PCT_ADD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ADD_QTY"]);
                    ob.QTY_ACT = (dr["QTY_ACT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_ACT"]);
                    ob.RATE_EST = (dr["RATE_EST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_EST"]);
                    ob.RATE_ACT = (dr["RATE_ACT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_ACT"]);
                    ob.ACCT_MOU_ID = (dr["ACCT_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACCT_MOU_ID"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["LK_FAB_QTY_SRC"] != DBNull.Value)
                    {
                        ob.LK_FAB_QTY_SRC = Convert.ToInt64(dr["LK_FAB_QTY_SRC"]);
                    }

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string ORD_MOU_CODE { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public Int64 BOOKED_QTY { get; set; }

        public string ITEM_CAT_NAME_EN { get; set; }

        public string BUYER_NAME_EN { get; set; }
    }
}