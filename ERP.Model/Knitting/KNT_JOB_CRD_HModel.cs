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
    public class KNT_JOB_CRD_HModel
    {
        public Int64 KNT_JOB_CRD_H_ID { get; set; }
        public Int64 KNT_PLAN_H_ID { get; set; }
        public string JOB_CRD_NO { get; set; }
        public Int64? KNT_SC_PRG_ISS_ID { get; set; }
        public Int64? KNT_MACHINE_ID { get; set; }

        public Int64? HR_PROD_FLR_ID { get; set; }
        public decimal ACT_FIN_DIA { get; set; }
        public string ACT_FIN_GSM { get; set; }
        public Decimal? ASIGN_QTY { get; set; }
        public Decimal? REQ_YRN_QTY_DONE { get; set; }
        public Decimal? MAX_ROLL_PROD_WT { get; set; }
        public Decimal? TG_D_PROD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public string IS_SUB_CONTR { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }
        public string IS_FINALIZED { get; set; }
        public string IS_REQS_DONE { get; set; }

        public Int64 MC_FAB_PROD_ORD_D_ID { get; set; }

        public Int64 KNT_MC_DIA_ID { get; set; }

        public Decimal RQD_GFAB_QTY { get; set; }
        public Decimal QTY_LEFT { get; set; }
        public string COLOR_TYPE { get; set; }

        public string MC_GAUGE_NO { get; set; }
        public string KNT_MACHINE_NO { get; set; }
        public string ADDL_NOTE { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string COLOR_GRP_NAME_EN { get; set; }
        public string FABRIC_SNAME { get; set; }
        public string FAB_ITEM_DESC { get; set; }
        public string DIA_TYPE { get; set; }
        public string FIN_DIA_N_DIA_TYPE { get; set; }
        public string ACT_FIN_DIA_N_DIA_TYPE { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string WORK_STYLE_NO { get; set; }
        public string BASE_STYLE_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string ORDER_NO_LIST { get; set; }
        public string MOU_CODE { get; set; }
        public string XML { get; set; }
        public String JC_STS_TYP_NAME { get; set; }
        public String RGB_COL_CODE { get; set; }
        public int KNT_JC_STS_TYPE_ID { get; set; }
        public decimal UN_ASIGN_QTY { get; set; }
        public decimal UN_ASIGN_QTY_NEW { get; set; }
        public string REASON_DESC { get; set; }
        public DateTime? UN_ASIGN_DT { get; set; }
        public Int64? LK_MC_GG_ID { get; set; }
        public DateTime? START_DT { get; set; }
        public DateTime? END_DT { get; set; }

        private List<KNT_JOB_CRD_DModel> _details = null;
        public List<KNT_JOB_CRD_DModel> details
        {
            get
            {
                if (_details == null)
                {
                    _details = new List<KNT_JOB_CRD_DModel>();
                }
                return _details;
            }
            set
            {
                _details = value;
            }
        }

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



        private List<KNT_YD_PLAN_FDRModel> _feeder = null;
        public List<KNT_YD_PLAN_FDRModel> feeder
        {
            get
            {
                if (_feeder == null)
                {
                    _feeder = new List<KNT_YD_PLAN_FDRModel>();
                }
                return _feeder;
            }
            set
            {
                _feeder = value;
            }
        }



        private string _IS_REMARKS;
        public string IS_PLAN_NOTE
        {
            get
            {
                if (this.REMARKS == String.Empty)
                {
                    _IS_REMARKS = "N";
                }
                else
                {
                    _IS_REMARKS = "Y";
                }

                return _IS_REMARKS;
            }
        }

        public string KNT_MC_DIA_ID_GG { get; set; }
        public Int64? ACT_MC_DIA_ID { get; set; }
        public long MC_FAB_PROD_ORD_H_ID { get; set; }

        public string ACT_MC_DIA { get; set; }

        public string FAB_PROD_CAT_SNAME { get; set; }

        public long? MC_STYLE_H_EXT_ID { get; set; }

        public string MC_ORDER_H_ID_LST { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public string STR_REQ_NO { get; set; }

        public long LK_REQ_STATUS_ID { get; set; }

        public string PLAN_NOTE { get; set; }

        public long? MC_BUYER_ID { get; set; }

        public long? MC_BYR_ACC_ID { get; set; }

        public long? MC_STYLE_H_ID { get; set; }

        public string IS_FBP_VISIBLE { get; set; }

        public string[] YRN_DETAILS_LST { get; set; }
        public string[] SL_DETAILS_LST { get; set; }
        public Decimal PROD_RATE { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string ADDRESS_EN { get; set; }
        public DateTime? SHIP_DT { get; set; }
        public string SL_DETAILS { get; set; }
        public Int64 RF_FAB_PROD_CAT_ID { get; set; }
        public String MC_DIA { get; set; }
        public String FIN_DIA { get; set; }
        public string FAB_TYPE_NAME { get; set; }
        public decimal PROD_QTY { get; set; }
        public decimal LAST_PROD_MIN { get; set; }
        public string XML_ISSUE_RETURN { get; set; }





        public string Save()
        {
            const string sp = "pkg_knit_plan.knt_job_crd_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = ob.KNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = ob.JOB_CRD_NO},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID},

                     new CommandParameter() {ParameterName = "pHR_PROD_FLR_ID", Value = ob.HR_PROD_FLR_ID},

                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pACT_MC_DIA_ID", Value = ob.ACT_MC_DIA_ID},
                     new CommandParameter() {ParameterName = "pACT_FIN_DIA", Value = ob.ACT_FIN_DIA},
                     new CommandParameter() {ParameterName = "pASIGN_QTY", Value = ob.ASIGN_QTY},
                     new CommandParameter() {ParameterName = "pTG_D_PROD_QTY", Value = ob.TG_D_PROD_QTY},
                     new CommandParameter() {ParameterName = "pPROD_RATE", Value = ob.PROD_RATE},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_SUB_CONTR", Value = ob.IS_SUB_CONTR},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_REQS_DONE", Value = ob.IS_REQS_DONE},
                     new CommandParameter() {ParameterName = "pSTART_DT", Value = ob.START_DT},
                     new CommandParameter() {ParameterName = "pEND_DT", Value = ob.END_DT},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},

                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = ob.MC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = ob.MC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pACT_FIN_GSM", Value = ob.ACT_FIN_GSM},
                     new CommandParameter() {ParameterName = "pADDL_NOTE", Value = ob.ADDL_NOTE},
                     new CommandParameter() {ParameterName = "pFAB_ITEM_DESC", Value = ob.FAB_ITEM_DESC},

                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_JOB_CRD_H_ID", Value =0, Direction = ParameterDirection.Output}
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


        public object getKnitCardList(Int64 pageNumber, Int64 pageSize, Int32? pRF_FAB_PROD_CAT_ID = null, string pJOB_CRD_NO = null)
        {
            string sp = "pkg_knit_plan.knt_job_crd_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_JOB_CRD_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = pJOB_CRD_NO},
                     new CommandParameter() {ParameterName = "pOption", Value = 3009 },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_JOB_CRD_HModel ob = new KNT_JOB_CRD_HModel();
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);

                    ob.KNT_JC_STS_TYPE_ID = (dr["KNT_JC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["KNT_JC_STS_TYPE_ID"]);


                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);

                    if (dr["ACT_MC_DIA_ID"] != DBNull.Value)
                    {
                        ob.ACT_MC_DIA_ID = Convert.ToInt64(dr["ACT_MC_DIA_ID"]);
                    }
                    ob.ASIGN_QTY = (dr["ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ASIGN_QTY"]);


                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_JOB_CRD_HModel> SelectAll(Int64? pKNT_PLAN_H_ID, Int64? pKNT_SC_PRG_ISS_ID, Int32? pOption)
        {
            string sp = "pkg_knit_plan.knt_job_crd_h_select";
            try
            {
                var obList = new List<KNT_JOB_CRD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = pKNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption },//3003 Default
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_JOB_CRD_HModel ob = new KNT_JOB_CRD_HModel();
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);

                    ob.KNT_JC_STS_TYPE_ID = (dr["KNT_JC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["KNT_JC_STS_TYPE_ID"]);


                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SUB_CONTR"]);

                    if (dr["ACT_MC_DIA_ID"] != DBNull.Value)
                    {
                        ob.ACT_MC_DIA_ID = Convert.ToInt64(dr["ACT_MC_DIA_ID"]);
                    }
                    ob.ASIGN_QTY = (dr["ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ASIGN_QTY"]);

                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.ACT_FIN_DIA_N_DIA_TYPE = (dr["ACT_FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_DIA_N_DIA_TYPE"]);
                    ob.MC_GAUGE_NO = (dr["MC_GAUGE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GAUGE_NO"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.REQ_YRN_QTY_DONE = (dr["REQ_YRN_QTY_DONE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_YRN_QTY_DONE"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.BASE_STYLE_NO = (dr["BASE_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BASE_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.YRN_DETAILS = (dr["YRN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_DETAILS"]);


                    ob.JC_STS_TYP_NAME = (dr["JC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JC_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);

                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.ACT_MC_DIA = (dr["ACT_MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_MC_DIA"]);

                    ob.PROD_QTY = (dr["PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_QTY"]);

                    if (pOption == 3007)
                    {
                        ob.details = new KNT_JOB_CRD_DModel().QueryData(ob.KNT_PLAN_H_ID, ob.KNT_JOB_CRD_H_ID);
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

        public List<KNT_JOB_CRD_HModel> getKPList(Int64? pKNT_SC_PRG_ISS_ID, Int32? pOption)
        {
            string sp = "pkg_knit_plan.knt_job_crd_h_select";
            try
            {
                var obList = new List<KNT_JOB_CRD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = pKNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_JOB_CRD_HModel ob = new KNT_JOB_CRD_HModel();

                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);


                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.BASE_STYLE_NO = (dr["BASE_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BASE_STYLE_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.YRN_DETAILS = (dr["YRN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_DETAILS"]);

                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.MC_GAUGE_NO = (dr["MC_GAUGE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GAUGE_NO"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);

                    if (pOption == 3008)
                    {
                        ob.items = new KNT_PLAN_DModel().QueryData(ob.KNT_PLAN_H_ID, pKNT_SC_PRG_ISS_ID);
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


        public KNT_JOB_CRD_HModel getJobCardDataByMcId(Int64? pKNT_MACHINE_ID)
        {
            string sp = "pkg_knit_plan.knt_job_crd_h_select";
            try
            {
                KNT_JOB_CRD_HModel ob = new KNT_JOB_CRD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = pKNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);

                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FIN_DIA"]);


                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.ASIGN_QTY = (dr["ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ASIGN_QTY"]);

                    ob.LAST_PROD_MIN = (dr["LAST_PROD_MIN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_PROD_MIN"]);

                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.PROD_QTY = (dr["PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_QTY"]);
                    ob.PLAN_NOTE = (dr["PLAN_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PLAN_NOTE"]);

                    ob.KNT_ROLL_MAX_EXTRA_KGS = (dr["KNT_ROLL_MAX_EXTRA_KGS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_MAX_EXTRA_KGS"]);
                    ob.MAX_ROLL_PROD_WT = ((dr["MAX_ROLL_PROD_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MAX_ROLL_PROD_WT"])) + ob.KNT_ROLL_MAX_EXTRA_KGS;

                    ob.IS_KNT_CLOSED = (dr["IS_KNT_CLOSED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_KNT_CLOSED"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public KNT_JOB_CRD_HModel Select(Int64? pKNT_PLAN_H_ID, Int64? pKNT_JOB_CRD_H_ID, Int64? pJOB_CRD_NO)
        {
            string sp = "pkg_knit_plan.knt_job_crd_h_select";
            try
            {
                var ob = new KNT_JOB_CRD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = pJOB_CRD_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_RATE"]);


                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);

                    ob.KNT_JC_STS_TYPE_ID = (dr["KNT_JC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["KNT_JC_STS_TYPE_ID"]);

                    if (dr["KNT_SC_PRG_ISS_ID"] != DBNull.Value)
                    {
                        ob.KNT_SC_PRG_ISS_ID = Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    }


                    if (dr["KNT_MACHINE_ID"] != DBNull.Value)
                    {
                        ob.KNT_MACHINE_ID = Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    }


                    ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);

                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);


                    if (new Int64[] { 1, 3 }.Contains(ob.RF_FAB_PROD_CAT_ID))
                    {
                        if (dr["ACT_MC_DIA_ID"] != DBNull.Value)
                        {
                            ob.ACT_MC_DIA_ID = Convert.ToInt64(dr["ACT_MC_DIA_ID"]);
                        }


                    }
                    else
                    {
                        ob.ACT_MC_DIA_ID = (dr["ACT_MC_DIA_ID"] == DBNull.Value) ? ob.KNT_MC_DIA_ID : Convert.ToInt64(dr["ACT_MC_DIA_ID"]);
                    }

                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FIN_DIA"]);


                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? (Regex.IsMatch(ob.FIN_DIA, @"^\d+$") ? Convert.ToDecimal(ob.FIN_DIA) : 0) : Convert.ToDecimal(dr["ACT_FIN_DIA"]);


                    if (dr["ASIGN_QTY"] != DBNull.Value)
                    {
                        ob.ASIGN_QTY = Convert.ToDecimal(dr["ASIGN_QTY"]);
                    }

                    if (dr["TG_D_PROD_QTY"] != DBNull.Value)
                    {
                        ob.TG_D_PROD_QTY = Convert.ToDecimal(dr["TG_D_PROD_QTY"]);
                    }
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.ADDL_NOTE = (dr["ADDL_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDL_NOTE"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SUB_CONTR"]);

                    if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                    {
                        ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    }

                    ob.QTY_LEFT = (dr["QTY_LEFT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_LEFT"]);

                    ob.MAX_ROLL_PROD_WT = (dr["MAX_ROLL_PROD_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MAX_ROLL_PROD_WT"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.IS_REQS_DONE = (dr["IS_REQS_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REQS_DONE"]);


                    ob.COLOR_TYPE = (dr["COLOR_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_TYPE"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);
                    ob.FAB_ITEM_DESC = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);

                    ob.DIA_TYPE = (dr["DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE"]);

                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);

                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);

                    ob.BASE_STYLE_NO = (dr["BASE_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BASE_STYLE_NO"]);

                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    if ((pJOB_CRD_NO ?? -1) == -1)
                    {
                        ob.details = new KNT_JOB_CRD_DModel().QueryData(pKNT_PLAN_H_ID, ob.KNT_JOB_CRD_H_ID);
                    }

                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);


                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);


                    if (dr["MC_STYLE_H_EXT_ID"] != DBNull.Value)
                    {
                        ob.MC_STYLE_H_EXT_ID = Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    }

                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);


                    ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);

                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);

                    ob.START_DT = (dr["START_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["START_DT"]);
                    ob.END_DT = (dr["END_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["END_DT"]);

                    if (dr["MC_BUYER_ID"] != DBNull.Value)
                    {
                        ob.MC_BUYER_ID = Convert.ToInt64(dr["MC_BUYER_ID"]);
                    }
                    if (dr["MC_BYR_ACC_ID"] != DBNull.Value)
                    {
                        ob.MC_BYR_ACC_ID = Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    }
                    if (dr["MC_STYLE_H_ID"] != DBNull.Value)
                    {
                        ob.MC_STYLE_H_ID = Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    }

                    if (dr["HR_PROD_FLR_ID"] != DBNull.Value)
                    {
                        ob.HR_PROD_FLR_ID = Convert.ToInt64(dr["HR_PROD_FLR_ID"]);
                    }

                    ob.IS_FBP_VISIBLE = (dr["IS_FBP_VISIBLE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FBP_VISIBLE"]);

                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.IS_FDR_OPT = (dr["IS_FDR_OPT"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FDR_OPT"]);


                    ob.IS_KNT_CLOSED = (dr["IS_KNT_CLOSED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_KNT_CLOSED"]);

                    if (dr["UN_ASIGN_QTY"] != DBNull.Value)
                    {
                        ob.UN_ASIGN_QTY = Convert.ToDecimal(dr["UN_ASIGN_QTY"]);
                    }

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_JOB_CRD_HModel> getYarnTestKP(Int64? pKNT_YRN_LOT_TEST_H_ID, Int32? pOption)
        {
            string sp = "pkg_knt_yarn_test.knt_yrn_lot_test_d1_select";
            try
            {
                var obList = new List<KNT_JOB_CRD_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_TEST_H_ID", Value = pKNT_YRN_LOT_TEST_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = pOption },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_JOB_CRD_HModel ob = new KNT_JOB_CRD_HModel();

                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.ASIGN_QTY = (dr["ASIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ASIGN_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveUnassignJobCardData()
        {
            const string sp = "pkg_knit_plan.knt_job_crd_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = ob.KNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pUN_ASIGN_QTY", Value = ob.UN_ASIGN_QTY},
                     new CommandParameter() {ParameterName = "pUN_ASIGN_QTY_NEW", Value = ob.UN_ASIGN_QTY_NEW},
                     new CommandParameter() {ParameterName = "pUN_ASIGN_DT", Value = DateTime.Now},
                     new CommandParameter() {ParameterName = "pREASON_DESC", Value = ob.REASON_DESC},
                     new CommandParameter() {ParameterName = "pKNT_JC_STS_TYPE_ID", Value =  (ob.ASIGN_QTY-ob.PROD_QTY) == (ob.UN_ASIGN_QTY+ob.UN_ASIGN_QTY_NEW) ? 6 : ob.KNT_JC_STS_TYPE_ID },
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
                     new CommandParameter() {ParameterName = "pXML", Value = ob.XML},
                     new CommandParameter() {ParameterName = "pXML_ISSUE_RETURN", Value = ob.XML_ISSUE_RETURN},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_JOB_CRD_H_ID", Value =0, Direction = ParameterDirection.Output}
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

        public KNT_JOB_CRD_HModel SelectRollProd(long? pKNT_JOB_CRD_H_ID, long? pJOB_CRD_NO)
        {
            string sp = "pkg_knit_plan.knt_job_crd_h_select";
            try
            {
                var ob = new KNT_JOB_CRD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = pJOB_CRD_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);

                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);

                    if (dr["KNT_SC_PRG_ISS_ID"] != DBNull.Value)
                    {
                        ob.KNT_SC_PRG_ISS_ID = Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    }

                    if (dr["KNT_MACHINE_ID"] != DBNull.Value)
                    {
                        ob.KNT_MACHINE_ID = Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    }

                    ob.KNT_MC_DIA_ID = (dr["KNT_MC_DIA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MC_DIA_ID"]);

                    if (dr["ACT_MC_DIA_ID"] != DBNull.Value)
                    {
                        ob.ACT_MC_DIA_ID = Convert.ToInt64(dr["ACT_MC_DIA_ID"]);
                    }

                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);

                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? (Regex.IsMatch(ob.FIN_DIA, @"^\d+$") ? Convert.ToDecimal(ob.FIN_DIA) : 0) : Convert.ToDecimal(dr["ACT_FIN_DIA"]);

                    if (dr["ASIGN_QTY"] != DBNull.Value)
                    {
                        ob.ASIGN_QTY = Convert.ToDecimal(dr["ASIGN_QTY"]);
                    }

                    if (dr["UN_ASIGN_QTY"] != DBNull.Value)
                    {
                        ob.UN_ASIGN_QTY = Convert.ToDecimal(dr["UN_ASIGN_QTY"]);
                    }

                    if (dr["TG_D_PROD_QTY"] != DBNull.Value)
                    {
                        ob.TG_D_PROD_QTY = Convert.ToDecimal(dr["TG_D_PROD_QTY"]);
                    }

                    if (dr["START_DT"] != DBNull.Value)
                    {
                        ob.START_DT = Convert.ToDateTime(dr["START_DT"]);
                    }


                    if (dr["END_DT"] != DBNull.Value)
                    {
                        ob.END_DT = Convert.ToDateTime(dr["END_DT"]);
                    }

                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.ADDL_NOTE = (dr["ADDL_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDL_NOTE"]);
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SUB_CONTR"]);

                    if (dr["SCM_SUPPLIER_ID"] != DBNull.Value)
                    {
                        ob.SCM_SUPPLIER_ID = Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    }

                    ob.QTY_LEFT = (dr["QTY_LEFT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_LEFT"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.IS_REQS_DONE = (dr["IS_REQS_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REQS_DONE"]);


                    ob.COLOR_TYPE = (dr["COLOR_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_TYPE"]);



                    ob.FAB_TYPE_NAME = Convert.ToString(dr["FAB_TYPE_NAME"]);


                    ob.MC_DIA_GG = (dr["MC_DIA_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA_GG"]);


                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);

                    ob.FABRIC_SNAME = (dr["FABRIC_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_SNAME"]);

                    ob.DIA_TYPE = (dr["DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE"]);

                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);

                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);

                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);

                    ob.BASE_STYLE_NO = (dr["BASE_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BASE_STYLE_NO"]);

                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.MC_FAB_PROD_ORD_D_ID = (dr["MC_FAB_PROD_ORD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_D_ID"]);
                    ob.LK_MC_GG_ID = (dr["LK_MC_GG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_MC_GG_ID"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);

                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);

                    ob.ACT_FIN_GSM = ob.FIN_GSM;

                    ob.YRN_DETAILS = (dr["YRN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_DETAILS"]);

                    ob.SL_DETAILS = (dr["SL_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_DETAILS"]);

                    ob.YRN_DETAILS_LST = new String[10];
                    int i = 0;
                    foreach (var x in ob.YRN_DETAILS.Split(','))
                    {
                        ob.YRN_DETAILS_LST[i] = x.Trim();
                        i++;
                    }


                    ob.SL_DETAILS_LST = new String[10];
                    int j = 0;
                    foreach (var x in ob.SL_DETAILS.Split(','))
                    {
                        ob.SL_DETAILS_LST[j] = x.Trim();
                        j++;
                    }


                    ob.TIME_STR = DateTime.Now.ToShortTimeString();
                    ob.IS_SUB_CONTR = (dr["IS_SUB_CONTR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SUB_CONTR"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.ADDRESS_EN = (dr["ADDRESS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDRESS_EN"]);

                    if (dr["SHIP_DT"] != DBNull.Value)
                    {
                        ob.SHIP_DT = Convert.ToDateTime(dr["SHIP_DT"]);
                    }
                    ob.feeder = new KNT_YD_PLAN_FDRModel().FeederArrangementData(ob.KNT_PLAN_H_ID);

                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.KNT_ROLL_MAX_EXTRA_KGS = (dr["KNT_ROLL_MAX_EXTRA_KGS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_MAX_EXTRA_KGS"]);
                    ob.MAX_ROLL_PROD_WT = ((dr["MAX_ROLL_PROD_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MAX_ROLL_PROD_WT"])) + ob.KNT_ROLL_MAX_EXTRA_KGS;
                    ob.IS_KNT_CLOSED = (dr["IS_KNT_CLOSED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_KNT_CLOSED"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public KNT_JOB_CRD_HModel getScheduleByMachine(Int64 pKNT_MACHINE_ID, long pDURATION, Int64? pKNT_JOB_CRD_H_ID)
        {
            string sp = "pkg_knit_plan.knt_job_crd_h_select";
            try
            {
                var ob = new KNT_JOB_CRD_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = pKNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pDURATION", Value = pDURATION},

                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = pKNT_JOB_CRD_H_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.START_DT = (dr["START_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["START_DT"]);
                    ob.END_DT = (dr["END_DT"] == DBNull.Value) ? DateTime.Now.Date : Convert.ToDateTime(dr["END_DT"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MC_DIA_GG { get; set; }

        public string FIN_GSM { get; set; }

        public string YRN_DETAILS { get; set; }

        public string TIME_STR { get; set; }

        public string saveYarnReqInHouseProd(string pXML)
        {
            const string sp = "pkg_knit_plan.create_store_requisition_prod";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = pXML},
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
        public string Update()
        {
            const string sp = "pkg_knit_plan.knt_job_crd_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_JOB_CRD_H_ID},

                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pTG_D_PROD_QTY", Value = ob.TG_D_PROD_QTY},
                     new CommandParameter() {ParameterName = "pSTART_DT", Value = ob.START_DT},
                     new CommandParameter() {ParameterName = "pEND_DT", Value = ob.END_DT},
               
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_JOB_CRD_H_ID", Value =0, Direction = ParameterDirection.Output}
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


        public string IS_FDR_OPT { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }

        public long KNT_STYL_FAB_ITEM_ID { get; set; }
        public long KNT_ROLL_MAX_EXTRA_KGS { get; set; }

        public string IS_KNT_CLOSED { get; set; }
    }
}