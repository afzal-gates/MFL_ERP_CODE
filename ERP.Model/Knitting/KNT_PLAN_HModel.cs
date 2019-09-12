using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

namespace ERP.Model
{
    public class KNT_PLAN_HModel
    {
        public Int64 KNT_PLAN_H_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_D_ID { get; set; }
        public string KNT_PLAN_NO { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime KNT_PLAN_DT { get; set; }
        public Int64 KNT_MC_DIA_ID { get; set; }
        public Int64 LK_MC_GG_ID { get; set; }
        public Int64? MC_FAB_PROC_RATE_ID { get; set; }
        public Decimal BGT_RATE { get; set; }
        public Decimal PROD_RATE { get; set; }
        public Decimal RQD_GFAB_QTY { get; set; }
        public Decimal REV_GFAB_QTY { get; set; }
        public Decimal NET_GFAB_QTY { get; set; }
        public Decimal DUE_GFAB_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string PLAN_NOTE { get; set; }
        public string IS_ACTIVE { get; set; }
        public string IS_FINALIZED { get; set; }

        public Int64 MC_STYLE_D_FAB_ID { get; set; }


        public string COLOR_TYPE { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string COLOR_GRP_NAME_EN { get; set; }
        public string FABRIC_SNAME { get; set; }
        public string DIA_TYPE { get; set; }
        public string FIN_DIA_N_DIA_TYPE { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string BASE_STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string ORDER_NO_LIST { get; set; }
        public string MOU_CODE { get; set; }

        public string IS_REVISED { get; set; }
        public Decimal? REQ_YRN_QTY_DONE { get; set; }

        public string XML { get; set; }

        public string XML_FEED { get; set; }
        public string XML_TIMES { get; set; }


        private List<KNT_PLAN_DModel> _items = null;
        public List<KNT_PLAN_DModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<KNT_PLAN_DModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }


        private List<MC_FAB_PROC_RATEModel> _rates = null;
        public List<MC_FAB_PROC_RATEModel> rates
        {
            get
            {
                if (_rates == null)
                {
                    _rates = new List<MC_FAB_PROC_RATEModel>();
                }
                return _rates;
            }
            set
            {
                _rates = value;
            }
        }


        private List<YD_SOLID_COLOR_MODEL> _yd_colors = null;
        public List<YD_SOLID_COLOR_MODEL> yd_colors
        {
            get
            {
                if (_yd_colors == null)
                {
                    _yd_colors = new List<YD_SOLID_COLOR_MODEL>();
                }
                return _yd_colors;
            }
            set
            {
                _yd_colors = value;
            }
        }


        private List<YD_SOLID_COLOR_MODEL> _solid_colors = null;
        public List<YD_SOLID_COLOR_MODEL> solid_colors
        {
            get
            {
                if (_solid_colors == null)
                {
                    _solid_colors = new List<YD_SOLID_COLOR_MODEL>();
                }
                return _solid_colors;
            }
            set
            {
                _solid_colors = value;
            }
        }


        private List<KNT_YD_FDR_REPEATModel> _yd_repeats = null;
        public List<KNT_YD_FDR_REPEATModel> yd_repeats
        {
            get
            {
                if (_yd_repeats == null)
                {
                    _yd_repeats = new List<KNT_YD_FDR_REPEATModel>();
                }
                return _yd_repeats;
            }
            set
            {
                _yd_repeats = value;
            }
        }
        

        private List<KNT_YD_PLAN_FDRModel> _kp_feeders = null;
        public List<KNT_YD_PLAN_FDRModel> kp_feeders
        {
            get
            {
                if (_kp_feeders == null)
                {
                    _kp_feeders = new List<KNT_YD_PLAN_FDRModel>();
                }
                return _kp_feeders;
            }
            set
            {
                _kp_feeders = value;
            }
        }

        private string _IS_PLAN_NOTE;
        public string IS_PLAN_NOTE
        {
            get
            {
                if (this.PLAN_NOTE == String.Empty)
                {
                    _IS_PLAN_NOTE = "N";
                }
                else
                {
                    _IS_PLAN_NOTE = "Y";
                }

                return _IS_PLAN_NOTE;
            }
        }

        public string Save()
        {
            const string sp = "pkg_knit_plan.knt_plan_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = ob.KNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_D_ID", Value = ob.MC_FAB_PROD_ORD_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_NO", Value = ob.KNT_PLAN_NO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_DT", Value = ob.KNT_PLAN_DT},
                     new CommandParameter() {ParameterName = "pKNT_MC_DIA_ID", Value = ob.KNT_MC_DIA_ID},
                     new CommandParameter() {ParameterName = "pLK_MC_GG_ID", Value = ob.LK_MC_GG_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pBGT_RATE", Value = ob.BGT_RATE},
                     new CommandParameter() {ParameterName = "pPROD_RATE", Value = ob.PROD_RATE},
                     new CommandParameter() {ParameterName = "pRQD_GFAB_QTY", Value = ob.RQD_GFAB_QTY},
                     new CommandParameter() {ParameterName = "pDUE_GFAB_QTY", Value = ob.DUE_GFAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pPLAN_NOTE", Value = ob.PLAN_NOTE},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_REVISED", Value = ob.IS_REVISED},

                     

                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDIA_MOU_ID", Value = ob.DIA_MOU_ID},
                     new CommandParameter() {ParameterName = "pFIN_DIA", Value = ob.FIN_DIA??0},
                     new CommandParameter() {ParameterName = "pFIN_GSM", Value = ob.FIN_GSM},

                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     
                     new CommandParameter() {ParameterName = "pXML_FEED", Value = ob.XML_FEED},
                     new CommandParameter() {ParameterName = "pXML_TIMES", Value = ob.XML_TIMES},

                     new CommandParameter() {ParameterName = "pCOLL_CUFF_XML", Value = ob.COLL_CUFF_XML},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pIS_FDR_OPT", Value = ob.IS_FDR_OPT},

                     new CommandParameter() {ParameterName = "pIS_KNT_CLOSED", Value = ob.IS_KNT_CLOSED},
                     new CommandParameter() {ParameterName = "pKNT_CLOSE_NOTE", Value = ob.KNT_CLOSE_NOTE},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_PLAN_H_ID", Value =0, Direction = ParameterDirection.Output}

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

        public string fleece_stitch_len_ratio_find(Int64 pRF_FAB_TYPE_ID, Int64 pLK_YFAB_PART_ID, Int64 pRF_YRN_CNT_ID, Decimal? pSTITCH_LEN)
        {
            const string sp = "pkg_knit_plan.fleece_stitch_len_ratio_find";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = pRF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_YFAB_PART_ID", Value = pLK_YFAB_PART_ID},
                     new CommandParameter() {ParameterName = "pRF_YRN_CNT_ID", Value = pRF_YRN_CNT_ID},
                     new CommandParameter() {ParameterName = "pSTITCH_LEN", Value = pSTITCH_LEN},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_STITCH_LEN", Value =0, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_RATIO", Value =0, Direction = ParameterDirection.Output}
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

        public KNT_PLAN_HModel QueryData(Int64 pMC_FAB_PROD_ORD_D_ID, int? pRF_FAB_PROD_CAT_ID)
        {
            string sp = "pkg_knit_plan.knt_plan_h_select";
            try
            {
                OraDatabase db = new OraDatabase();
                KNT_PLAN_HModel ob = new KNT_PLAN_HModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_D_ID", Value = pMC_FAB_PROD_ORD_D_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.KNT_PLAN_NO = (dr["KNT_PLAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_PLAN_NO"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.KNT_PLAN_DT = (dr["KNT_PLAN_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr["KNT_PLAN_DT"]);

                    ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);

                    if (dr["KNT_SC_PRG_RCV_ID"] != DBNull.Value)
                    {
                        ob.KNT_SC_PRG_RCV_ID = Convert.ToInt64(dr["KNT_SC_PRG_RCV_ID"]);
                    }

                    if (dr["RF_FAB_PROD_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_FAB_PROD_CAT_ID = Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    }

                    ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);

                    if (dr["MC_FAB_PROC_RATE_ID"] != DBNull.Value)
                    {
                        ob.MC_FAB_PROC_RATE_ID = Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    }
                    ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.DUE_GFAB_QTY = (dr["DUE_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DUE_GFAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PLAN_NOTE = (dr["PLAN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_NOTE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);


                    ob.BAL_GFAB_QTY = (dr["BAL_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_GFAB_QTY"]);

                    ob.GFAB_ADJ_QTY = (dr["GFAB_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GFAB_ADJ_QTY"]);
                    ob.GFAB_PROD_QTY = (dr["GFAB_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GFAB_PROD_QTY"]);

                    ob.IS_KNT_CLOSED = (dr["IS_KNT_CLOSED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_KNT_CLOSED"]);
                    ob.KNT_CLOSE_NOTE = (dr["KNT_CLOSE_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_CLOSE_NOTE"]);

                    ob.IS_KNT_CLOSED_ORI = ob.IS_KNT_CLOSED;

                    ob.KC_DONE = (dr["KC_DONE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["KC_DONE"]);

                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    //ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.IS_FLAT_CIR = (dr["IS_FLAT_CIR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FLAT_CIR"]);

                    ob.DIA_TYPE = (dr["DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE"]);

                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);

                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);

                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FIN_DIA"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.IS_FBP_VISIBLE = (dr["IS_FBP_VISIBLE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_FBP_VISIBLE"]);
                    ob.IS_ELA_MXD = (dr["IS_ELA_MXD"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_ELA_MXD"]);

                    ob.IS_FDR_OPT = (dr["IS_FDR_OPT"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FDR_OPT"]);

                    ob.FIB_COMBINATION_ID = (dr["FIB_COMBINATION_ID"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FIB_COMBINATION_ID"]);

                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_YD_TYPE_ID = Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    }
                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }


                    if (dr["LK_FK_DGN_TYP_ID"] != DBNull.Value)
                    {
                        ob.LK_FK_DGN_TYP_ID = Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    }
                    
                    ob.KNT_YRN_STR_REQ_D_ID = (dr["KNT_YRN_STR_REQ_D_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_D_ID"]);
                    ob.REQ_YRN_QTY_DONE = (dr["REQ_YRN_QTY_DONE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_YRN_QTY_DONE"]);

                    ob.items = new KNT_PLAN_DModel().QueryData(ob.KNT_PLAN_H_ID, ob.KNT_YRN_STR_REQ_D_ID, ob.LK_COL_TYPE_ID);

                    ob.ASSIGN_QTY = ob.items.Select(x => x.RQD_YRN_QTY).Sum();
                    if (ob.LK_COL_TYPE_ID == 360)
                    {
                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                                         new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                                         new CommandParameter() {ParameterName = "pOption", Value =3003},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                        foreach (DataRow dr1 in ds1.Tables[0].Rows)
                        {
                            YD_SOLID_COLOR_MODEL ob1 = new YD_SOLID_COLOR_MODEL();
                            ob1.SOLID_COLOR_ID = (dr1["SOLID_COLOR_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["SOLID_COLOR_ID"]);
                            ob1.COLOR_NAME_EN = (dr1["COLOR_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["COLOR_NAME_EN"]);
                            ob.yd_colors.Add(ob1);
                        }

                        var ds2 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pOption", Value =3004},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                        foreach (DataRow dr2 in ds2.Tables[0].Rows)
                        {
                            YD_SOLID_COLOR_MODEL ob2 = new YD_SOLID_COLOR_MODEL();
                            ob2.SOLID_COLOR_ID = (dr2["SOLID_COLOR_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr2["SOLID_COLOR_ID"]);
                            ob2.COLOR_NAME_EN = (dr2["COLOR_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr2["COLOR_NAME_EN"]);
                            ob.solid_colors.Add(ob2);
                        }


                    }

                    if (ob.LK_COL_TYPE_ID == 360)
                    {
                        ob.kp_feeders = new KNT_YD_PLAN_FDRModel().queryData(ob.KNT_PLAN_H_ID, ob.yd_colors, ob.solid_colors);
                    }


                    if (ob.LK_COL_TYPE_ID == 360)
                    {
                        ob.yd_repeats = new KNT_YD_FDR_REPEATModel().QueryData(ob.KNT_PLAN_H_ID);
                    }

                    ob.rates = new MC_FAB_PROC_RATEModel().KntRateData(ob.FIB_COMBINATION_ID, ob.RF_FAB_TYPE_ID, ob.LK_COL_TYPE_ID, ob.LK_YD_TYPE_ID, ob.LK_FEDER_TYPE_ID, ob.LK_FK_DGN_TYP_ID);



                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public KNT_PLAN_HModel QueryDataCollarCuff(Int64 pMC_FAB_PROD_ORD_D_ID, int? pRF_FAB_PROD_CAT_ID)
        {
            string sp = "pkg_knit_plan.knt_plan_h_select";
            try
            {
                OraDatabase db = new OraDatabase();
                KNT_PLAN_HModel ob = new KNT_PLAN_HModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_D_ID", Value = pMC_FAB_PROD_ORD_D_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.KNT_PLAN_NO = (dr["KNT_PLAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_PLAN_NO"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.KNT_PLAN_DT = (dr["KNT_PLAN_DT"] == DBNull.Value) ? DateTime.Today : Convert.ToDateTime(dr["KNT_PLAN_DT"]);

                    ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);

                    if (dr["KNT_SC_PRG_RCV_ID"] != DBNull.Value)
                    {
                        ob.KNT_SC_PRG_RCV_ID = Convert.ToInt64(dr["KNT_SC_PRG_RCV_ID"]);
                    }

                    if (dr["RF_FAB_PROD_CAT_ID"] != DBNull.Value)
                    {
                        ob.RF_FAB_PROD_CAT_ID = Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    }

                    ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);
                    if (dr["MC_FAB_PROC_RATE_ID"] != DBNull.Value)
                    {
                        ob.MC_FAB_PROC_RATE_ID = Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    }
                    ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.DUE_GFAB_QTY = (dr["DUE_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DUE_GFAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PLAN_NOTE = (dr["PLAN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_NOTE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);


                    ob.BAL_GFAB_QTY = (dr["BAL_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_GFAB_QTY"]);
                    ob.KC_DONE = (dr["KC_DONE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["KC_DONE"]);

                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);


                    //ob.COLOR_TYPE = (dr["COLOR_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_TYPE"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    //ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);

                    ob.DIA_TYPE = (dr["DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE"]);

                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);

                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);

                    //ob.BASE_STYLE_NO = (dr["BASE_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BASE_STYLE_NO"]);

                    //ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);

                    ob.IS_FBP_VISIBLE = (dr["IS_FBP_VISIBLE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_FBP_VISIBLE"]);



                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    ob.FIB_COMBINATION_ID = (dr["FIB_COMBINATION_ID"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FIB_COMBINATION_ID"]);

                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_YD_TYPE_ID = Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                    }
                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                    }


                    if (dr["LK_FK_DGN_TYP_ID"] != DBNull.Value)
                    {
                        ob.LK_FK_DGN_TYP_ID = Convert.ToInt64(dr["LK_FK_DGN_TYP_ID"]);
                    }

                    ob.ASSIGN_QTY = ob.items.Select(x => x.RQD_YRN_QTY).Sum();

                    if (ob.LK_COL_TYPE_ID == 360)
                    {
                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                    {
                                         new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                                         new CommandParameter() {ParameterName = "pOption", Value =3003},
                                         new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                     }, sp);
                        foreach (DataRow dr1 in ds1.Tables[0].Rows)
                        {
                            YD_SOLID_COLOR_MODEL ob1 = new YD_SOLID_COLOR_MODEL();
                            ob1.SOLID_COLOR_ID = (dr1["SOLID_COLOR_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["SOLID_COLOR_ID"]);
                            ob1.COLOR_NAME_EN = (dr1["COLOR_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr1["COLOR_NAME_EN"]);
                            ob.yd_colors.Add(ob1);
                        }
                    }

                    if (ob.LK_COL_TYPE_ID == 360)
                    {
                        ob.kp_feeders = new KNT_YD_PLAN_FDRModel().queryData(ob.KNT_PLAN_H_ID, null, null);
                    }


                    if (ob.LK_COL_TYPE_ID == 360)
                    {
                        ob.yd_repeats = new KNT_YD_FDR_REPEATModel().QueryData(ob.KNT_PLAN_H_ID);
                    }

                    ob.rates = new MC_FAB_PROC_RATEModel().KntRateData(ob.FIB_COMBINATION_ID, ob.RF_FAB_TYPE_ID, ob.LK_COL_TYPE_ID, ob.LK_YD_TYPE_ID, ob.LK_FEDER_TYPE_ID, ob.LK_FK_DGN_TYP_ID);
                    ob.items = new KNT_PLAN_DModel().QueryData(ob.KNT_PLAN_H_ID);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_PLAN_HModel> CollarCuffQueryData(Int64 pMC_FAB_PROD_ORD_H_ID, Int64 pFAB_COLOR_ID)
        {
            string sp = "pkg_knit_plan.knt_plan_h_select";
            try
            {
                OraDatabase db = new OraDatabase();
                List<KNT_PLAN_HModel> list = new List<KNT_PLAN_HModel>();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    KNT_PLAN_HModel ob = new KNT_PLAN_HModel();

                    if (dr["MC_FAB_PROD_ORD_D_ID"] != DBNull.Value)
                    {
                        ob = QueryDataCollarCuff(Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]), Convert.ToInt32(dr["RF_FAB_PROD_CAT_ID"]));
                    }
                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public KNT_PLAN_HModel Select(long ID)
        {
            string sp = "Select_KNT_PLAN_H";
            try
            {
                var ob = new KNT_PLAN_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.KNT_PLAN_NO = (dr["KNT_PLAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_PLAN_NO"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.KNT_PLAN_DT = (dr["KNT_PLAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["KNT_PLAN_DT"]);
                    ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                    ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);
                    if (dr["MC_FAB_PROC_RATE_ID"] != DBNull.Value)
                    {
                        ob.MC_FAB_PROC_RATE_ID = Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    }

                    ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.DUE_GFAB_QTY = (dr["DUE_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DUE_GFAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PLAN_NOTE = (dr["PLAN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_NOTE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_PLAN_HModel> JobCardDB(int pageNo,
            int pageSize,
            Int64? pMC_BYR_ACC_ID,
            Int64? pMC_BUYER_ID,
            Int64? pMC_STYLE_H_EXT_ID,
            string pBUYER_NAME_EN,
            string pSTYLE_NO,
            string pORDER_NO,
            string pCOLOR_NAME_EN,
            string pFAB_TYPE_NAME)
        {
            string sp = "pkg_knit_jobcard.knt_plan_h_jobcard_select";
            try
            {
                List<KNT_PLAN_HModel> list = new List<KNT_PLAN_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                        new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value =pMC_BYR_ACC_ID},
                        new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                        new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                        new CommandParameter() {ParameterName = "pBUYER_NAME_EN", Value =pBUYER_NAME_EN},
                        new CommandParameter() {ParameterName = "pSTYLE_NO", Value =pSTYLE_NO},
                        new CommandParameter() {ParameterName = "pORDER_NO", Value =pORDER_NO},
                        new CommandParameter() {ParameterName = "pCOLOR_NAME_EN", Value =pCOLOR_NAME_EN},
                        new CommandParameter() {ParameterName = "pFAB_TYPE_NAME", Value =pFAB_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new KNT_PLAN_HModel();
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.KNT_PLAN_NO = (dr["KNT_PLAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_PLAN_NO"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.KNT_PLAN_DT = (dr["KNT_PLAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["KNT_PLAN_DT"]);
                    ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);
                    ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);

                    if (dr["MC_FAB_PROC_RATE_ID"] != DBNull.Value)
                    {
                        ob.MC_FAB_PROC_RATE_ID = Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    }

                    ob.BGT_RATE = (dr["BGT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BGT_RATE"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.DUE_GFAB_QTY = (dr["DUE_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DUE_GFAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PLAN_NOTE = (dr["PLAN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_NOTE"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.MC_GG_NAME = (dr["MC_GG_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG_NAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    ob.ORDER_TYPE = (dr["ORDER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_TYPE"]);

                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIN_DIA_TYPE = (dr["FIN_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_TYPE"]);
                    ob.MC_DIA_GG = (dr["MC_DIA_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA_GG"]);

                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string CreateStoreRequisition(Int64? pKNT_SC_PRG_ISS_ID, Int64? pKNT_JOB_CRD_H_ID)
        {
            const string sp = "pkg_knit_plan.create_store_requisition";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = pKNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
        public string CreateStoreRequisitionYdSc(Int64? pKNT_SC_PRG_ISS_ID, Int64? pKNT_JOB_CRD_H_ID)
        {
            const string sp = "pkg_knit_plan.create_store_requisition_yd";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = pKNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public string CreateStoreRequisitionYdKp(Int64 pKNT_PLAN_H_ID)
        {
            const string sp = "pkg_knit_plan.create_store_requisition_yd_kp";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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


        public string CreateStoreRequisitionSR(string XML, Int64? pKNT_YRN_STR_REQ_H_ID = null, String IS_EDIT_MODE = "N", string IS_SUBMIT = "N")
        {
            const string sp = "pkg_knit_plan.create_store_requisition_sr";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = XML},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = pKNT_YRN_STR_REQ_H_ID },
                     new CommandParameter() {ParameterName = "pIS_EDIT_MODE", Value = IS_EDIT_MODE },
                     new CommandParameter() {ParameterName = "pIS_SUBMIT", Value = IS_SUBMIT },
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_YRN_STR_REQ_H_ID", Value =500, Direction = ParameterDirection.Output}
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

        public string CreateStoreRequisitionFBR(string XML, Int64? pKNT_YRN_STR_REQ_H_ID = null, String IS_EDIT_MODE = "N", string IS_SUBMIT = "N")
        {
            const string sp = "pkg_knit_plan.create_store_requisition_fbr";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = XML},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = pKNT_YRN_STR_REQ_H_ID },
                     new CommandParameter() {ParameterName = "pIS_EDIT_MODE", Value = IS_EDIT_MODE },
                     new CommandParameter() {ParameterName = "pIS_SUBMIT", Value = IS_SUBMIT },
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_YRN_STR_REQ_H_ID", Value =500, Direction = ParameterDirection.Output}
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

        public string CreateStoreRequisitionCollarCuff(string XML, Int64? pKNT_YRN_STR_REQ_H_ID = null, Int64? pKNT_SCO_CLCF_PRG_H_ID = null, String IS_EDIT_MODE = "N", string IS_SUBMIT = "N")
        {
            const string sp = "pkg_knit_plan.create_store_requisition_cc";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = XML},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = pKNT_YRN_STR_REQ_H_ID },
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = pKNT_SCO_CLCF_PRG_H_ID },
                     new CommandParameter() {ParameterName = "pIS_EDIT_MODE", Value = IS_EDIT_MODE },
                     new CommandParameter() {ParameterName = "pIS_SUBMIT", Value = IS_SUBMIT },
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_YRN_STR_REQ_H_ID", Value =500, Direction = ParameterDirection.Output}
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


        public string STYLE_NO { get; set; }
        public string MC_GG_NAME { get; set; }
        public string FIN_GSM { get; set; }
        public string ORDER_NO { get; set; }
        public long TOTAL_REC { get; set; }
        public string ORDER_TYPE { get; set; }
        public string FAB_TYPE_NAME { get; set; }
        public string FIN_DIA_TYPE { get; set; }
        public string MC_DIA_GG { get; set; }

        public long MC_FAB_PROD_ORD_H_ID { get; set; }

        public long? KNT_SC_PRG_RCV_ID { get; set; }

        public long? RF_FAB_PROD_CAT_ID { get; set; }

        public string IS_FBP_VISIBLE { get; set; }

        public long RF_FAB_TYPE_ID { get; set; }

        public decimal BAL_GFAB_QTY { get; set; }

        public decimal KC_DONE { get; set; }

        public long FAB_COLOR_ID { get; set; }

        public String COLL_CUFF_XML { get; set; }

        public decimal ASSIGN_QTY { get; set; }

        public string IS_ELA_MXD { get; set; }

        public string IS_FLAT_CIR { get; set; }

        public long LK_FBR_GRP_ID { get; set; }

        public decimal? FIN_DIA { get; set; }

        public long DIA_MOU_ID { get; set; }

        public long LK_DIA_TYPE_ID { get; set; }

        public long LK_COL_TYPE_ID { get; set; }
        public string FIB_COMBINATION_ID { get; set; }

        public long? LK_YD_TYPE_ID { get; set; }

        public long? LK_FK_DGN_TYP_ID { get; set; }

        public long? LK_FEDER_TYPE_ID { get; set; }

        public string CreateStoreRequisitionYdScTrns(long? pKNT_SC_PRG_ISS_ID, long? pKNT_JOB_CRD_H_ID)
        {
            const string sp = "pkg_knit_plan.create_store_req_yd_trns";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = pKNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public KNT_PLAN_HModel getDataByKpIdForMail(long pKNT_PLAN_H_ID)
        {
            string sp = "pkg_knit_plan.knt_plan_h_select";
            try
            {
                OraDatabase db = new OraDatabase();
                KNT_PLAN_HModel ob = new KNT_PLAN_HModel();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                     ob.KNT_PLAN_NO = (dr["KNT_PLAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_PLAN_NO"]);
                     ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                     ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                     ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                     ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                     ob.CREATED_BY_NAME = (dr["CREATED_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CREATED_BY_NAME"]);
                     ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CREATION_DATE"]);
                     ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long KNT_YRN_STR_REQ_D_ID { get; set; }

        public string IS_FDR_OPT { get; set; }

        public decimal GFAB_ADJ_QTY { get; set; }

        public decimal GFAB_PROD_QTY { get; set; }

        public string IS_KNT_CLOSED { get; set; }

        public string KNT_CLOSE_NOTE { get; set; }

        public string IS_KNT_CLOSED_ORI { get; set; }


    
public  string CREATED_BY_NAME { get; set; }
public  string CREATION_DATE { get; set; }}
    public class YD_SOLID_COLOR_MODEL
    {
        public Int64 SOLID_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
    }

}