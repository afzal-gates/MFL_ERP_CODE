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
    public class COUNT_LIST
    {
        public Int64 PART_ID { get; set; }
        public Int64? CNT_ID { get; set; }
        public Decimal RATIO { get; set; }
    }

    public class MC_FIB_COMB_CNT_CFGModel
    {
        public Int64 MC_FIB_COMB_CNT_CFG_ID { get; set; }
        public Int64 MC_YRN_CNT_CFG_ID { get; set; }
        public string LK_FIB_TYPE_LST { get; set; }
        public string IS_100PCT { get; set; }
        public string IS_BLEND_A_F { get; set; }
        public string IS_ELA_MXD { get; set; }
        public string COMBI_LST_XML { get; set; }
        public string FIB_CONF_XML { get; set; }
        public string PARTIAL_INSERT { get; set; }
        public string Save()
        {
            const string sp = "PKG_FABRICATION.mc_fib_comb_cnt_cfg_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_YRN_CNT_CFG_ID", Value = ob.MC_YRN_CNT_CFG_ID},
                     new CommandParameter() {ParameterName = "pCOMBI_LST_XML", Value = ob.COMBI_LST_XML},
                     new CommandParameter() {ParameterName = "pFIB_CONF_XML", Value = ob.FIB_CONF_XML},
                     new CommandParameter() {ParameterName = "pPARTIAL_INSERT", Value = ob.PARTIAL_INSERT},
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
    public class MC_YRN_CNT_CFGModel
    {
        public Int64 MC_YRN_CNT_CFG_ID { get; set; }
        public Int64? RF_FAB_TYPE_ID { get; set; }
        public Int64 FB_WT_MIN { get; set; }
        public Int64 FB_WT_MAX { get; set; }
        public string RF_YRN_CNT_LST { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML { get; set; }
        public Int64? LK_MAC_GG_ID { get; set; }
        public string MC_GAUGE_NAME { get; set; }

        private List<COUNT_LIST> _counts = null;
        public List<COUNT_LIST> counts
        {
            get
            {
                if (_counts == null)
                {
                    _counts = new List<COUNT_LIST>();
                }
                return _counts;
            }
            set
            {
                _counts = value;
            }
        }

        public Int64? MC_FIB_COMB_TMPLT_ID { get; set; }


        public string Save()
        {
            const string sp = "SP_MC_YRN_CNT_CFG";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_YRN_CNT_CFG_ID", Value = ob.MC_YRN_CNT_CFG_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFB_WT_MIN", Value = ob.FB_WT_MIN},
                     new CommandParameter() {ParameterName = "pFB_WT_MAX", Value = ob.FB_WT_MAX},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_LST", Value = ob.RF_YRN_CNT_LST},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public string Update()
        {
            const string sp = "SP_MC_YRN_CNT_CFG";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_YRN_CNT_CFG_ID", Value = ob.MC_YRN_CNT_CFG_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFB_WT_MIN", Value = ob.FB_WT_MIN},
                     new CommandParameter() {ParameterName = "pFB_WT_MAX", Value = ob.FB_WT_MAX},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_LST", Value = ob.RF_YRN_CNT_LST},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public string Delete()
        {
            const string sp = "SP_MC_YRN_CNT_CFG";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_YRN_CNT_CFG_ID", Value = ob.MC_YRN_CNT_CFG_ID},
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

        public List<MC_YRN_CNT_CFGModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_yrn_cnt_cfg_select";
            try
            {
                var obList = new List<MC_YRN_CNT_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_YRN_CNT_CFG_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_YRN_CNT_CFGModel ob = new MC_YRN_CNT_CFGModel();
                    ob.MC_YRN_CNT_CFG_ID = (dr["MC_YRN_CNT_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_YRN_CNT_CFG_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FB_WT_MIN = (dr["FB_WT_MIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MIN"]);
                    ob.FB_WT_MAX = (dr["FB_WT_MAX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MAX"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);

                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                            new CommandParameter() {ParameterName = "pMC_YRN_CNT_CFG_ID", Value =ob.MC_YRN_CNT_CFG_ID},
                                            new CommandParameter() {ParameterName = "pOption", Value =3004},
                                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        COUNT_LIST ob1 = new COUNT_LIST();
                        ob1.PART_ID = (dr1["PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PART_ID"]);
                        if (dr1["CNT_ID"] != DBNull.Value)
                        {
                            ob1.CNT_ID = Convert.ToInt64(dr1["CNT_ID"]);
                        }

                        ob.counts.Add(ob1);
                    }


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_YRN_CNT_CFGModel> CountConfigData(Int64? pRF_FAB_TYPE_ID, Int64? pMC_FIB_COMB_TMPLT_ID, Int64? pLK_MAC_GG_ID)
        {
            string sp = "pkg_merchandising.mc_yrn_cnt_cfg_select";
            try
            {
                var obList = new List<MC_YRN_CNT_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value =pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_FIB_COMB_TMPLT_ID", Value =pMC_FIB_COMB_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pLK_MAC_GG_ID", Value =pLK_MAC_GG_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_YRN_CNT_CFGModel ob = new MC_YRN_CNT_CFGModel();
                    ob.MC_YRN_CNT_CFG_ID = (dr["MC_YRN_CNT_CFG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_YRN_CNT_CFG_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.FB_WT_MIN = (dr["FB_WT_MIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MIN"]);
                    ob.FB_WT_MAX = (dr["FB_WT_MAX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FB_WT_MAX"]);
                    ob.RF_YRN_CNT_LST = (dr["RF_YRN_CNT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_YRN_CNT_LST"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    
                    if (dr["LK_MAC_GG_ID"] != DBNull.Value)
                    {
                        ob.LK_MAC_GG_ID = Convert.ToInt64(dr["LK_MAC_GG_ID"]);
                    }

                    if (dr["MC_FIB_COMB_TMPLT_ID"] != DBNull.Value)
                    {
                        ob.MC_FIB_COMB_TMPLT_ID = Convert.ToInt64(dr["MC_FIB_COMB_TMPLT_ID"]);
                    }


                    var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                            new CommandParameter() {ParameterName = "pMC_YRN_CNT_CFG_ID", Value =ob.MC_YRN_CNT_CFG_ID},
                                            new CommandParameter() {ParameterName = "pOption", Value =3004},
                                            new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                        }, sp);
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        COUNT_LIST ob1 = new COUNT_LIST();
                        ob1.PART_ID = (dr1["PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PART_ID"]);
                        if (dr1["CNT_ID"] != DBNull.Value)
                        {
                            ob1.CNT_ID = Convert.ToInt64(dr1["CNT_ID"]);
                        }

                        ob.counts.Add(ob1);
                    }
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string BatchSave()
        {
            const string sp = "pkg_merchandising.mc_yrn_cnt_cfg_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FIB_COMB_TMPLT_ID", Value = ob.MC_FIB_COMB_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_MAC_GG_ID", Value = ob.LK_MAC_GG_ID},

                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
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

        public List<COUNT_LIST> SuggestedCount(Int64? RF_FIB_COMP_ID, Int64 RF_FAB_TYPE_ID, Int64 FB_WT_MIN, Int64 FB_WT_MAX)
        {
            string sp = "pkg_merchandising.mc_yrn_cnt_cfg_select";
            try
            {
                var obList = new List<COUNT_LIST>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value =RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value =RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pFB_WT_MIN", Value =FB_WT_MIN},
                     new CommandParameter() {ParameterName = "pFB_WT_MAX", Value =FB_WT_MAX},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new COUNT_LIST();
                    ob.CNT_ID = (dr["CNT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CNT_ID"]);
                    ob.PART_ID = (dr["PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PART_ID"]);
                    ob.RATIO = (dr["RATIO"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATIO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_YRN_CNT_CFGModel> GetMachineGaugeList(Decimal pFB_WT_MAX, Int64? pRF_FIB_COMP_ID, Int64? pRF_FAB_TYPE_ID)
        {
            string sp = "pkg_common.rf_fab_type_select";
            try
            {
                var obList = new List<MC_YRN_CNT_CFGModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value =pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value =pRF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pFB_WT_MAX", Value =pFB_WT_MAX},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_YRN_CNT_CFGModel ob = new MC_YRN_CNT_CFGModel();
                    ob.LK_MAC_GG_ID = (dr["LK_MAC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MAC_GG_ID"]);
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