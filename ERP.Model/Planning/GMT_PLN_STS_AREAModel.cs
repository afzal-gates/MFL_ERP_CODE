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

    public class GMT_PLN_STS_GROUPModel
    {
        public Int64? LK_PLN_STS_GRP_ID { get; set; }
        public string LK_PLN_STS_GRP_NAME { get; set; }
        public int? Option { get; set; }

        private List<GMT_PLN_STS_AREAModel> _items = null;
        public List<GMT_PLN_STS_AREAModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<GMT_PLN_STS_AREAModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        private List<GMT_PLN_STS_RULEModel> _details = null;
        public List<GMT_PLN_STS_RULEModel> details
        {
            get
            {
                if (_details == null)
                {
                    _details = new List<GMT_PLN_STS_RULEModel>();
                }
                return _details;
            }
            set
            {
                _details = value;
            }
        }



        public string XML { get; set; }

        public List<GMT_PLN_STS_GROUPModel> getPlanStatusAreas()
        {
            string sp = "PKG_GMT_PLN_STATUS.gmt_pln_sts_area_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_STS_GROUPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_STS_GROUPModel ob = new GMT_PLN_STS_GROUPModel();
                    ob.LK_PLN_STS_GRP_ID = (dr["LK_PLN_STS_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PLN_STS_GRP_ID"]);
                    ob.LK_PLN_STS_GRP_NAME = (dr["LK_PLN_STS_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PLN_STS_GRP_NAME"]);
                    ob.items = new GMT_PLN_STS_AREAModel().Query(ob.LK_PLN_STS_GRP_ID);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_PLN_STS_GROUPModel> getPlanStatusRules()
        {
            string sp = "PKG_GMT_PLN_STATUS.gmt_pln_sts_area_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_STS_GROUPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_STS_GROUPModel ob = new GMT_PLN_STS_GROUPModel();
                    ob.LK_PLN_STS_GRP_ID = (dr["LK_PLN_STS_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PLN_STS_GRP_ID"]);
                    ob.LK_PLN_STS_GRP_NAME = (dr["LK_PLN_STS_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_PLN_STS_GRP_NAME"]);
                    ob.details = new GMT_PLN_STS_RULEModel().Query(ob.LK_PLN_STS_GRP_ID);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string Save()
        {
            const string sp = "PKG_GMT_PLN_STATUS.gmt_pln_sts_area_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pOption", Value =ob.Option},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
    }

    public class GMT_PLN_STS_AREAModel
    {
        public Int64 GMT_PLN_STS_AREA_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public string PLN_STS_AREA_CODE { get; set; }
        public Int64 LK_PLN_STS_GRP_ID { get; set; }
        public Decimal WT_FACTOR { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_ACTIVE { get; set; }

        public string PLN_STS_AREA_NAME { get; set; }





        public List<GMT_PLN_STS_AREAModel> Query(Int64? pLK_PLN_STS_GRP_ID)
        {
            string sp = "PKG_GMT_PLN_STATUS.gmt_pln_sts_area_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_STS_AREAModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_PLN_STS_GRP_ID", Value =pLK_PLN_STS_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_STS_AREAModel ob = new GMT_PLN_STS_AREAModel();
                    ob.GMT_PLN_STS_AREA_ID = (dr["GMT_PLN_STS_AREA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_STS_AREA_ID"]);
                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.PLN_STS_AREA_CODE = (dr["PLN_STS_AREA_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STS_AREA_CODE"]);
                    ob.LK_PLN_STS_GRP_ID = (dr["LK_PLN_STS_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PLN_STS_GRP_ID"]);
                    ob.WT_FACTOR = (dr["WT_FACTOR"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["WT_FACTOR"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.PLN_STS_AREA_NAME = (dr["PLN_STS_AREA_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STS_AREA_NAME"]);
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

    public class GMT_PLN_STS_RULEModel
    {
        public Int64 GMT_PLN_STS_RULE_ID { get; set; }
        public Int64 LK_PLN_STS_GRP_ID { get; set; }
        public Int64 SCORE_RNG_1 { get; set; }
        public Int64 SCORE_RNG_2 { get; set; }

        public Int64 HR_PROD_LINE_ID { get; set; }

        public string PLN_STS_CODE { get; set; }
        public string PLN_STS_NAME { get; set; }
        public string STS_COLOR_CODE { get; set; }
        public Decimal POSTIVE_RANK { get; set; }


        public string PLN_STS_CODE_STOS { get; set; }
        public string PLN_STS_NAME_STOS { get; set; }
        public string STS_COLOR_CODE_STOS { get; set; }
        public Decimal POSTIVE_RANK_STOS { get; set; }

        public string PLN_STS_CODE_TNA { get; set; }
        public string PLN_STS_NAME_TNA { get; set; }
        public string STS_COLOR_CODE_TNA { get; set; }
        public Decimal POSTIVE_RANK_TNA { get; set; }


        public Decimal S2SM01_SCORE { get; set; }
        public Decimal S2SM02_SCORE { get; set; }
        public Decimal S2SM03_SCORE { get; set; }
        public Decimal S2SM04_SCORE { get; set; }
        public decimal SCORE_TNA04 { get; set; }
        public decimal SCORE_TNA03 { get; set; }
        public decimal SCORE_TNA02 { get; set; }
        public decimal SCORE_TNA01 { get; set; }
        public Decimal TTL_SCORE { get; set; }
        public string LINE_CODE { get; set; }
        public string IS_DISABLED { get; set; }





        public List<GMT_PLN_STS_RULEModel> Query(Int64? pLK_PLN_STS_GRP_ID)
        {
            string sp = "PKG_GMT_PLN_STATUS.gmt_pln_sts_area_select";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_STS_RULEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_PLN_STS_GRP_ID", Value =pLK_PLN_STS_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_STS_RULEModel ob = new GMT_PLN_STS_RULEModel();
                    ob.GMT_PLN_STS_RULE_ID = (dr["GMT_PLN_STS_RULE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PLN_STS_RULE_ID"]);
                    ob.LK_PLN_STS_GRP_ID = (dr["LK_PLN_STS_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PLN_STS_GRP_ID"]);
                    ob.SCORE_RNG_1 = (dr["SCORE_RNG_1"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCORE_RNG_1"]);
                    ob.SCORE_RNG_2 = (dr["SCORE_RNG_2"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCORE_RNG_2"]);
                    ob.PLN_STS_CODE = (dr["PLN_STS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STS_CODE"]);
                    ob.PLN_STS_NAME = (dr["PLN_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STS_NAME"]);
                    ob.STS_COLOR_CODE = (dr["STS_COLOR_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STS_COLOR_CODE"]);
                    ob.POSTIVE_RANK = (dr["POSTIVE_RANK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["POSTIVE_RANK"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<GMT_PLN_STS_RULEModel> findPlnStatusRulesWithLine(
            Int64 pRF_FAB_CLASS_ID, 
            Int64 pMC_STYLE_D_ITEM_ID, 
            Int64 pGMT_PRODUCT_TYP_ID, 
            Int64 pINV_ITEM_CAT_ID,
            DateTime? pPLAN_START_DT,
            DateTime? pPLAN_END_DT,
            DateTime? pSHIP_DT,
            Int64 pALLOCATED_QTY,
            Int64 pPLAN_MP,
            Int64 pPLAN_WH,
            String pIS_LRN_CRV_APP,
            Decimal? pPLAN_EFFICIENCY,
            Int64 pMC_ORDER_ITEM_PLN_ID,
            Decimal pSMV,
            int pPLAN_OP
        )
        {
            string sp = "PKG_GMT_PLN_LINE_LOAD.gmt_pln_status_rules_line";
           
            try
            {
                var obList = new List<GMT_PLN_STS_RULEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_CLASS_ID", Value =pRF_FAB_CLASS_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value =pMC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pGMT_PRODUCT_TYP_ID", Value =pGMT_PRODUCT_TYP_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},

                     new CommandParameter() {ParameterName = "pPLAN_START_DT", Value =pPLAN_START_DT},
                     new CommandParameter() {ParameterName = "pPLAN_END_DT", Value =pPLAN_END_DT},
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value =pSHIP_DT},

                     new CommandParameter() {ParameterName = "pALLOCATED_QTY", Value =pALLOCATED_QTY},
                     new CommandParameter() {ParameterName = "pPLAN_MP", Value =pPLAN_MP},
                     new CommandParameter() {ParameterName = "pPLAN_WH", Value =pPLAN_WH},
                     new CommandParameter() {ParameterName = "pIS_LRN_CRV_APP", Value =pIS_LRN_CRV_APP},
                     new CommandParameter() {ParameterName = "pPLAN_EFFICIENCY", Value =pPLAN_EFFICIENCY},

                     new CommandParameter() {ParameterName = "pMC_ORDER_ITEM_PLN_ID", Value =pMC_ORDER_ITEM_PLN_ID},
                     new CommandParameter() {ParameterName = "pSMV", Value =pSMV},

                     new CommandParameter() {ParameterName = "pPLAN_OP", Value =pPLAN_OP},

                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_STS_RULEModel ob = new GMT_PLN_STS_RULEModel();
                    ob.HR_PROD_LINE_ID = (dr["HR_PROD_LINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PROD_LINE_ID"]);


                    ob.PLN_STS_CODE_STOS = (dr["PLN_STS_CODE_STOS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STS_CODE_STOS"]);
                    ob.PLN_STS_NAME_STOS = (dr["PLN_STS_NAME_STOS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STS_NAME_STOS"]);
                    ob.STS_COLOR_CODE_STOS = (dr["STS_COLOR_CODE_STOS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STS_COLOR_CODE_STOS"]);
                    ob.POSTIVE_RANK_STOS = (dr["POSTIVE_RANK_STOS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["POSTIVE_RANK_STOS"]);

                    ob.PLN_STS_CODE_TNA = (dr["PLN_STS_CODE_TNA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STS_CODE_TNA"]);
                    ob.PLN_STS_NAME_TNA = (dr["PLN_STS_NAME_TNA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLN_STS_NAME_TNA"]);
                    ob.STS_COLOR_CODE_TNA = (dr["STS_COLOR_CODE_TNA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STS_COLOR_CODE_TNA"]);
                    ob.POSTIVE_RANK_TNA = (dr["POSTIVE_RANK_TNA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["POSTIVE_RANK_TNA"]);



                    ob.S2SM01_SCORE = (dr["S2SM01_SCORE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["S2SM01_SCORE"]);
                    ob.S2SM02_SCORE = (dr["S2SM02_SCORE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["S2SM02_SCORE"]);
                    ob.S2SM03_SCORE = (dr["S2SM03_SCORE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["S2SM03_SCORE"]);
                    ob.S2SM04_SCORE = (dr["S2SM04_SCORE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["S2SM04_SCORE"]);

                    ob.SCORE_TNA01 = (dr["SCORE_TNA01"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SCORE_TNA01"]);
                    ob.SCORE_TNA02 = (dr["SCORE_TNA02"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SCORE_TNA02"]);
                    ob.SCORE_TNA03 = (dr["SCORE_TNA03"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SCORE_TNA03"]);
                    ob.SCORE_TNA04 = (dr["SCORE_TNA04"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SCORE_TNA04"]);

                    ob.TTL_SCORE = (dr["TTL_SCORE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_SCORE"]);
                    ob.LINE_CODE = (dr["LINE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE"]);
                    ob.IS_DISABLED = (dr["IS_DISABLED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISABLED"]);

                    ob.OQ_PCT_ONTYM = (dr["OQ_PCT_ONTYM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OQ_PCT_ONTYM"]);
                    ob.ALLOCATED_QTY = pALLOCATED_QTY;
                    ob.LOAD_START_DT = (dr["LOAD_START_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_START_DT"]);
                    ob.LOAD_END_DT = (dr["LOAD_END_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LOAD_END_DT"]);
                    ob.OQ_ONTYM_DT = (dr["OQ_ONTYM_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OQ_ONTYM_DT"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long OQ_PCT_ONTYM { get; set; }
        public DateTime LOAD_START_DT { get; set; }
        public DateTime LOAD_END_DT { get; set; }
        public DateTime OQ_ONTYM_DT { get; set; }
        public long ALLOCATED_QTY { get; set; }
    }
}