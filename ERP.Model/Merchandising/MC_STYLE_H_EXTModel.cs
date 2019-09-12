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
    public class MC_STYLE_H_EXTModel
    {
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? MC_STYLE_H_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string IS_N_R { get; set; }
        public long MC_FAB_PROD_ORD_H_ID { get; set; }
        public long MC_BLK_FAB_REQ_H_ID { get; set; }
        public string FAB_PROD_XML { get; set; }
        public string FAB_PROD_CAT_SNAME { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64 LK_ORD_TYPE_ID { get; set; }
        private string _IS_N_R_ELA;
        public string IS_N_R_ELA
        {
            get
            {
                switch (this.IS_N_R)
                {
                    case "N":
                        _IS_N_R_ELA = "New";
                        this.LK_ORD_TYPE_ID = 175;
                        break;
                    case "R":
                        _IS_N_R_ELA = "Repeat";
                        this.LK_ORD_TYPE_ID = 174;
                        break;
                    case "D":
                        _IS_N_R_ELA = "Development";
                        this.LK_ORD_TYPE_ID = 311;
                        break;

                }
                return _IS_N_R_ELA;
            }
        }
        public string HAS_EXT { get; set; }
        public Int64? LK_STYL_DEV_ID { get; set; }
        public Int64? LK_STYL_DEV_TYP_ID { get; set; }
        public Int64? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public Int64? MC_BUYER_ID { get; set; }
        public string STYLE_DESC { get; set; }
        public string HAS_SET { get; set; }
        public string HAS_COMBO { get; set; }
        public string HAS_MULTI_COL_PACK { get; set; }
        public Int64? PCS_PER_PACK { get; set; }
        public string IS_BFAB_BKD { get; set; }
        public string BASE_STYLE { get; set; }
        public string ORDER_NO_LST { get; set; }




        public string Save()
        {
            const string sp = "pkg_mc_style.mc_style_h_ext_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pIS_N_R", Value = ob.IS_N_R},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = ob.LK_STYL_DEV_ID},     
                     new CommandParameter() {ParameterName = "pBASE_STYLE", Value = ob.BASE_STYLE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opMC_STYLE_H_EXT_ID", Value =0, Direction = ParameterDirection.Output}
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

        public string Update()
        {
            const string sp = "SP_MC_STYLE_H_EXT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = ob.STYLE_NO},
                     new CommandParameter() {ParameterName = "pIS_N_R", Value = ob.IS_N_R},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = ob.LK_STYL_DEV_ID},
                     //new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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


        public List<MC_STYLE_H_EXTModel> SelectAll()
        {
            string sp = "Select_MC_STYLE_H_EXT";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_STYLE_H_EXTModel Select(long ID)
        {
            string sp = "Select_MC_STYLE_H_EXT";
            try
            {
                var ob = new MC_STYLE_H_EXTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_H_EXTModel> BuyerWiseStyleHExtList(Int64? pMC_BYR_ACC_ID, Int64? pMC_BUYER_ID, string pSTYLE_NO = null, Int64? pMC_STYLE_H_EXT_ID = null,
            string pMC_STYLE_H_EXT_LIST = null, Int64? pMC_BYR_ACC_GRP_ID = null)
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_LIST", Value = pMC_STYLE_H_EXT_LIST},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);

                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_STYLE_H_EXTModel> ByrWiseBookingStyleHExtList(Int64? pMC_BYR_ACC_ID, Int64? pMC_BUYER_ID, string pSTYLE_NO = null, Int64? pMC_STYLE_H_EXT_ID = null, string pMC_STYLE_H_EXT_LIST = null)
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_LIST", Value = pMC_STYLE_H_EXT_LIST},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_H_EXTModel> MainStyleWrtStyleHExtList(Int64? pMC_STYLE_H_ID = null)
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.IS_BFAB_BKD = (dr["IS_BFAB_BKD"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BFAB_BKD"]);

                    ob.LK_STYL_DEV_TYP_ID = (dr["LK_STYL_DEV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_TYP_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_H_EXTModel> MultiByrAccWiseStyleListData(string pMC_BYR_ACC_IDS, string pIS_TNA_FINALIZED = null, string pHAS_SMP_YRN_INHOUSE_TNA = null)
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_IDS", Value = pMC_BYR_ACC_IDS},
                     new CommandParameter() {ParameterName = "pIS_TNA_FINALIZED", Value = pIS_TNA_FINALIZED },
                     new CommandParameter() {ParameterName = "pHAS_SMP_YRN_INHOUSE_TNA", Value = pHAS_SMP_YRN_INHOUSE_TNA },
                     new CommandParameter() {ParameterName = "pOption", Value = 3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<MC_STYLE_H_EXTModel> GetOrderList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BUYER_ID = null, string pSTYLE_NO = null,
            Int64? pMC_STYLE_H_EXT_ID = null, string pLK_ORDER_TYPE_LST = null, Int64? pRF_FAB_PROD_CAT_ID = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID}, 
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pLK_ORDER_TYPE_LST", Value = pLK_ORDER_TYPE_LST},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3018},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    if (dr["LK_STYL_DEV_TYP_ID"] != DBNull.Value)
                        ob.LK_STYL_DEV_TYP_ID = (dr["LK_STYL_DEV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_TYP_ID"]);

                    if (dr["MC_FAB_PROD_ORD_H_ID"] != DBNull.Value)
                        ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    if (dr["MC_BLK_FAB_REQ_H_ID"] != DBNull.Value)
                        ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);


                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_H_EXTModel> GetStyleExOrderList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BUYER_ID = null, string pSTYLE_NO = null, Int64? pMC_STYLE_H_EXT_ID = null, string pLK_ORDER_TYPE_LST = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID}, 
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                    new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                    new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                    new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                    new CommandParameter() {ParameterName = "pLK_ORDER_TYPE_LST", Value = pLK_ORDER_TYPE_LST},
                                          
                    new CommandParameter() {ParameterName = "pOption", Value =3025},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    if (dr["LK_STYL_DEV_TYP_ID"] != DBNull.Value)
                        ob.LK_STYL_DEV_TYP_ID = (dr["LK_STYL_DEV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_TYP_ID"]);

                    
                    if (dr["MC_BLK_FAB_REQ_H_ID"] != DBNull.Value)
                        ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);

                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);
                    
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_H_EXTModel> getFabProdOrderData(long? pMC_BYR_ACC_ID, Int64? pMC_BYR_ACC_GRP_ID, string pSTYLE_NO, long? pMC_FAB_PROD_ORD_H_ID, DateTime? pFIRSTDATE, DateTime? pLASTDATE,
            string pRF_FAB_PROD_CAT_LST, string pNATURE_OF_ORDER, string pHAS_FK_CLRCUF)
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_LST", Value = pRF_FAB_PROD_CAT_LST},
                     new CommandParameter() {ParameterName = "pNATURE_OF_ORDER", Value = pNATURE_OF_ORDER},
                     new CommandParameter() {ParameterName = "pHAS_FK_CLRCUF", Value = pHAS_FK_CLRCUF},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_H_EXTModel> getFabProdOrderDataOh(
            long? pMC_BYR_ACC_ID,
            Int64? pMC_BYR_ACC_GRP_ID,
            string pORDER_NO_LST,
            long? pMC_FAB_PROD_ORD_H_ID,
            DateTime? pFIRSTDATE,
            DateTime? pLASTDATE,
            string pRF_FAB_PROD_CAT_LST,
            string pNATURE_OF_ORDER,
            string pIS_YD_ONLY,
            string pMC_FAB_PROD_ORD_H_LST,
            long? pMC_STYLE_H_ID
        )
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO_LST", Value = pORDER_NO_LST},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_LST", Value = pRF_FAB_PROD_CAT_LST},
                     new CommandParameter() {ParameterName = "pNATURE_OF_ORDER", Value = pNATURE_OF_ORDER},
                     new CommandParameter() {ParameterName = "pIS_YD_ONLY", Value = pIS_YD_ONLY},
                     
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},

                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_LST", Value = pMC_FAB_PROD_ORD_H_LST},

                     new CommandParameter() {ParameterName = "pOption", Value =3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();

                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_H_EXTModel> getFabProdOrderDataOhMerge(long? pMC_FAB_PROD_ORD_H_ID, long? pFAB_COLOR_ID, Int64? pRF_FAB_TYPE_ID, string pORDER_NO_LST, Int64? pRF_FAB_PROD_CAT_ID, Int64? pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO_LST", Value = pORDER_NO_LST},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();

                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public object GetScoOrdStyleListByHasKnitCard(long pSCM_SUPPLIER_ID, string pSTYLE_NO = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_chl_rcv_h_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                                          
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_STYLE_H_EXTModel> ByrWiseStyleHExtWithoutBookingList(Int64? pMC_BUYER_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BYR_ACC_GRP_ID = null, string pSTYLE_NO = null)
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();

                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);

                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);
                    //ob.EXT_NO = (dr["EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXT_NO"]);
                    //ob.IS_EXT_AUTO = (dr["IS_EXT_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EXT_AUTO"]);
                    //ob.STYLE_NO_DISP = (dr["STYLE_NO_DISP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO_DISP"]);
                    ob.IS_BFAB_BKD = (dr["IS_BFAB_BKD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_BFAB_BKD"]);

                    ob.LK_STYL_DEV_TYP_ID = (dr["LK_STYL_DEV_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_TYP_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_STYLE_H_EXTModel> BookingStyleHExtList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BUYER_ID = null, string pSTYLE_NO = null, Int64? pMC_STYLE_H_EXT_ID = null, string pMC_STYLE_H_EXT_LIST = null)
        {
            string sp = "pkg_mc_style.mc_style_h_ext_select";
            try
            {
                var obList = new List<MC_STYLE_H_EXTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_LIST", Value = pMC_STYLE_H_EXT_LIST},
                     new CommandParameter() {ParameterName = "pOption", Value =3010},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_H_EXTModel ob = new MC_STYLE_H_EXTModel();
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO_LST = (dr["ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LST"]);
                    ob.IS_N_R = (dr["IS_N_R"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_N_R"]);
                    ob.LK_STYL_DEV_ID = (dr["LK_STYL_DEV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_STYL_DEV_ID"]);
                    ob.HAS_EXT = (dr["HAS_EXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_EXT"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.HAS_SET = (dr["HAS_SET"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_SET"]);
                    ob.HAS_COMBO = (dr["HAS_COMBO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_COMBO"]);
                    ob.HAS_MULTI_COL_PACK = (dr["HAS_MULTI_COL_PACK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HAS_MULTI_COL_PACK"]);
                    ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);

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