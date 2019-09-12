using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.IO;

namespace ERP.Model
{
    public class KNT_FAB_ROLLModel
    {
        public Int64 KNT_FAB_ROLL_ID { get; set; }
        public DateTime PROD_DT { get; set; }

        private string _PROD_DT_STR;
        public String PROD_DT_STR
        {
            get
            {
                _PROD_DT_STR = this.PROD_DT.ToString("dd-MMM-yyyy");
                return _PROD_DT_STR;
            }
        }


        public Int64 KNT_JOB_CRD_H_ID { get; set; }
        public Int64? OPERATOR_ID { get; set; }
        public Int64? HR_SCHEDULE_H_ID { get; set; }
        public String FAB_ROLL_NO { get; set; }
        public Decimal FAB_ROLL_WT { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64? KNT_STYL_FAB_ITEM_ID { get; set; }
        public DateTime? REPORT_DT { get; set; }
        public Decimal ACT_FIN_DIA { get; set; }
        public string ACT_FIN_GSM { get; set; }
        public Int64? ACT_COLOR_ID { get; set; }
        public string REMARKS { get; set; }


        public string CHALAN_NO { get; set; }
        public DateTime? CHALAN_DT { get; set; }
        public DateTime? QC_DT { get; set; }
        public Decimal DED_ROLL_WT { get; set; }
        public Decimal TOTAL_PT { get; set; }
        public Decimal GRADE_PT { get; set; }
        public string GRADE_NO { get; set; }
        public Int64 KNT_QC_STS_TYPE_ID { get; set; }
        public String QC_NOTE { get; set; }


        public Int64? HOLD_DFCT_TYPE_ID { get; set; }

        public string KNT_YRN_LOT_LST { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string KNT_MACHINE_NO { get; set; }

        public string MC_DIA { get; set; }
        public string MC_GG { get; set; }
        public string MC_ORDER_H_ID_LST { get; set; }
        public string STYLE_NO { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string FIN_GSM { get; set; }
        public Int64 LK_QC_GRD_ID { get; set; }
        public Int64 GREY_QC_BY { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public long? KNT_SC_PRG_ISS_ID { get; set; }
        public decimal STITCH_LEN { get; set; }
        public Int64? ACT_MC_DIA_ID { get; set; }
        public Int64? KNT_MACHINE_ID { get; set; }
        public string IS_VALID { get; set; }
        public string COMP_SNAME { get; set; }
        public string RGB_COL_CODE_QC { get; set; }
        public string RL_STS_TYP_NAME_QC { get; set; }
        public string PRNTR_ADDRESS { get; set; }
        public Int64? HR_EMPLOYEE_ID { get; set; }


        public string Save()
        {
            const string sp = "pkg_knit_plan.knt_fab_roll_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                 
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pPROD_DT", Value = DateTime.Now.Date},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = ob.KNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pJOB_CRD_NO", Value = ob.JOB_CRD_NO},     
               
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value = ob.FAB_ROLL_NO},
                     new CommandParameter() {ParameterName = "pFAB_ROLL_WT", Value = ob.FAB_ROLL_WT},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},


                     new CommandParameter() {ParameterName = "pREPORT_DT", Value = ob.REPORT_DT},
                     new CommandParameter() {ParameterName = "pACT_FIN_DIA", Value = ob.ACT_FIN_DIA},
                     new CommandParameter() {ParameterName = "pACT_MC_DIA_ID", Value = ob.ACT_MC_DIA_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},

                     new CommandParameter() {ParameterName = "pACT_FIN_GSM", Value = ob.ACT_FIN_GSM},
                     new CommandParameter() {ParameterName = "pACT_COLOR_ID", Value = ob.ACT_COLOR_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},

                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pKNT_ROLL_STS_TYPE_ID", Value = ob.KNT_ROLL_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pDED_ROLL_WT", Value = ob.DED_ROLL_WT},
                     new CommandParameter() {ParameterName = "pTOTAL_PT", Value = ob.TOTAL_PT},
                     new CommandParameter() {ParameterName = "pGRADE_PT", Value = ob.GRADE_PT},
                     new CommandParameter() {ParameterName = "pGRADE_NO", Value = ob.GRADE_NO},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                
                     new CommandParameter() {ParameterName = "pPRNTR_ADDRESS", Value = ob.PRNTR_ADDRESS},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_QTY_LEFT", Value =0, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_KNT_FAB_ROLL_ID", Value =0, Direction = ParameterDirection.Output}
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

        public string XML_QC_D { get; set; }



        public string SaveQC()
        {
            const string sp = "PKG_KNIT_ROLL.knt_fab_roll_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pDED_ROLL_WT", Value = ob.DED_ROLL_WT},
                     new CommandParameter() {ParameterName = "pTOTAL_PT", Value = ob.TOTAL_PT},
                     new CommandParameter() {ParameterName = "pGRADE_PT", Value = ob.GRADE_PT},
                     new CommandParameter() {ParameterName = "pGRADE_NO", Value = ob.GRADE_NO},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHOLD_DFCT_TYPE_ID", Value = ob.HOLD_DFCT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                     new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pGREY_QC_BY", Value = ob.GREY_QC_BY}, //HttpContext.Current.Session["multiScUserId"]},                 

                     new CommandParameter() {ParameterName = "pXML_QC_D", Value = ob.XML_QC_D},
                     
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

        public string UpdateQC()
        {
            const string sp = "PKG_KNIT_ROLL.knt_fab_roll_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pDED_ROLL_WT", Value = ob.DED_ROLL_WT},
                     new CommandParameter() {ParameterName = "pTOTAL_PT", Value = ob.TOTAL_PT},
                     new CommandParameter() {ParameterName = "pGRADE_PT", Value = ob.GRADE_PT},
                     new CommandParameter() {ParameterName = "pGRADE_NO", Value = ob.GRADE_NO},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                     new CommandParameter() {ParameterName = "pLK_QC_GRD_ID", Value = ob.LK_QC_GRD_ID},
                     new CommandParameter() {ParameterName = "pGREY_QC_BY", Value = ob.GREY_QC_BY},                 
                                          
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


        public string SplitRoll()
        {
            const string sp = "pkg_knit_plan.knt_fab_roll_split_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                 
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},                
                     new CommandParameter() {ParameterName = "pFAB_ROLL_WT", Value = ob.FAB_ROLL_WT},

                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opFAB_ROLL_NO", Value ="", Direction = ParameterDirection.Output}

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

        public List<KNT_FAB_ROLLModel> QueryData(Int64? pKNT_JOB_CRD_H_ID, DateTime? pDateFilter)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                var obList = new List<KNT_FAB_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value =pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pDateFilter", Value =pDateFilter},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_ROLLModel ob = new KNT_FAB_ROLLModel();
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.REPORT_DT = (dr["REPORT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORT_DT"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FIN_DIA"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_COLOR_ID = (dr["ACT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_COLOR_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.KNT_ROLL_STS_TYPE_ID = (dr["KNT_ROLL_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_STS_TYPE_ID"]);

                    ob.RL_STS_TYP_NAME = (dr["RL_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME"]);

                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);

                    ob.RGB_COL_CODE_QC = (dr["RGB_COL_CODE_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE_QC"]);
                    ob.RL_STS_TYP_NAME_QC = (dr["RL_STS_TYP_NAME_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME_QC"]);

                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.OPERATOR_NAME = (dr["OPERATOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPERATOR_NAME"]);

                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    if ((dr["CHALAN_DT"] != DBNull.Value))
                    {
                        ob.CHALAN_DT = Convert.ToDateTime(dr["CHALAN_DT"]);
                    }

                    if ((dr["QC_DT"] != DBNull.Value))
                    {
                        ob.QC_DT = Convert.ToDateTime(dr["QC_DT"]);
                    }

                    if ((dr["KNT_SC_PRG_ISS_ID"] != DBNull.Value))
                    {
                        ob.KNT_SC_PRG_ISS_ID = Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    }

                    ob.DED_ROLL_WT = (dr["DED_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DED_ROLL_WT"]);
                    ob.TOTAL_PT = (dr["TOTAL_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOTAL_PT"]);
                    ob.GRADE_PT = (dr["GRADE_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT"]);
                    ob.GRADE_NO = (dr["GRADE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.QC_NOTE = (dr["QC_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NOTE"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_FAB_ROLLModel> getRollListByBatch(Int64 pDYE_BT_CARD_H_ID, Int64 pDYE_BT_CARD_GRP_ID, Int64 pDYE_GSTR_ISS_H_ID)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                var obList = new List<KNT_FAB_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_GRP_ID", Value =pDYE_BT_CARD_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value =pDYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_ROLLModel ob = new KNT_FAB_ROLLModel();
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);

                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.RL_STS_TYP_NAME = (dr["RL_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.RGB_COL_CODE_QC = (dr["RGB_COL_CODE_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE_QC"]);
                    ob.RL_STS_TYP_NAME_QC = (dr["RL_STS_TYP_NAME_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME_QC"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_FAB_ROLLModel> getFinishFabricInsRollByBatchID(Int64 pDYE_BT_CARD_H_ID)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                var obList = new List<KNT_FAB_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3009},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_ROLLModel ob = new KNT_FAB_ROLLModel();
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);

                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.RL_STS_TYP_NAME = (dr["RL_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.RGB_COL_CODE_QC = (dr["RGB_COL_CODE_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE_QC"]);
                    ob.RL_STS_TYP_NAME_QC = (dr["RL_STS_TYP_NAME_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME_QC"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_FAB_ROLLModel> getRollListByPreTreatment(Int64 pKNT_STYL_FAB_ITEM_ID, Int64 pDYE_GSTR_REQ_H_ID)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                var obList = new List<KNT_FAB_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value =pKNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value =pDYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_ROLLModel ob = new KNT_FAB_ROLLModel();
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);

                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.RL_STS_TYP_NAME = (dr["RL_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.RGB_COL_CODE_QC = (dr["RGB_COL_CODE_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE_QC"]);
                    ob.RL_STS_TYP_NAME_QC = (dr["RL_STS_TYP_NAME_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME_QC"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    if (dr["KNT_YRN_LOT_LST"] != DBNull.Value)
                        ob.KNT_YRN_LOT_LST = Convert.ToString(dr["KNT_YRN_LOT_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_FAB_ROLLModel> getAllRollListByBatchInfo(Int64 pDYE_BT_CARD_H_ID, Int64 pLK_FBR_GRP_ID, Int64? pDYE_BT_CARD_GRP_ID)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                var obList = new List<KNT_FAB_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value =pLK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_GRP_ID", Value =pDYE_BT_CARD_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_ROLLModel ob = new KNT_FAB_ROLLModel();
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);

                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.RL_STS_TYP_NAME = (dr["RL_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.RGB_COL_CODE_QC = (dr["RGB_COL_CODE_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE_QC"]);
                    ob.RL_STS_TYP_NAME_QC = (dr["RL_STS_TYP_NAME_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME_QC"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_FAB_ROLLModel> getAllRollListByKntFab(Int64 pKNT_STYL_FAB_ITEM_ID)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                var obList = new List<KNT_FAB_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value =pKNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_ROLLModel ob = new KNT_FAB_ROLLModel();
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);

                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.RL_STS_TYP_NAME = (dr["RL_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.RGB_COL_CODE_QC = (dr["RGB_COL_CODE_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE_QC"]);
                    ob.RL_STS_TYP_NAME_QC = (dr["RL_STS_TYP_NAME_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME_QC"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public KNT_FAB_ROLLModel QueryRollData(string pFAB_ROLL_NO, Int64? pOption = null)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                KNT_FAB_ROLLModel ob = new KNT_FAB_ROLLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value =pFAB_ROLL_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3002:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.REPORT_DT = (dr["REPORT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORT_DT"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FIN_DIA"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_COLOR_ID = (dr["ACT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_COLOR_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.KNT_ROLL_STS_TYPE_ID = (dr["KNT_ROLL_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_STS_TYPE_ID"]);
                    ob.LK_QC_GRD_ID = (dr["LK_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_QC_GRD_ID"]);

                    ob.RL_STS_TYP_NAME = (dr["RL_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.OPERATOR_NAME = (dr["OPERATOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPERATOR_NAME"]);
                    ob.HOLD_DFCT_TYPE_ID = (dr["HOLD_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOLD_DFCT_TYPE_ID"]);

                    ob.DIA_TYPE_NAME_EN = (dr["DIA_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME_EN"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    if ((dr["CHALAN_DT"] != DBNull.Value))
                    {
                        ob.CHALAN_DT = Convert.ToDateTime(dr["CHALAN_DT"]);
                    }

                    if ((dr["QC_DT"] != DBNull.Value))
                    {
                        ob.QC_DT = Convert.ToDateTime(dr["QC_DT"]);
                    }
                    ob.DED_ROLL_WT = (dr["DED_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DED_ROLL_WT"]);
                    ob.TOTAL_PT = (dr["TOTAL_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOTAL_PT"]);
                    ob.GRADE_PT = (dr["GRADE_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT"]);
                    ob.GRADE_NO = (dr["GRADE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.QC_NOTE = (dr["QC_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NOTE"]);

                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.STITCH_LEN = (dr["STITCH_LEN"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STITCH_LEN"]);
                    ob.GREY_QC_BY = (dr["GREY_QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GREY_QC_BY"]);

                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.MC_GG = (dr["MC_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.HR_EMPLOYEE_ID = ob.GREY_QC_BY;
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_FB_DFCT_TYPEModel> QueryRollQCData(string pFAB_ROLL_NO)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                List<RF_FB_DFCT_TYPEModel> List = new List<RF_FB_DFCT_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pFAB_ROLL_NO", Value =pFAB_ROLL_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_FB_DFCT_TYPEModel ob = new RF_FB_DFCT_TYPEModel();
                    ob.RF_FB_DFCT_TYPE_ID = (dr["RF_FB_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FB_DFCT_TYPE_ID"]);
                    ob.FB_DFCT_TYPE_CODE = (dr["FB_DFCT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_CODE"]);
                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_CALC_APLY = (dr["IS_CALC_APLY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CALC_APLY"]);
                    ob.DFCT_QTY = (dr["DFCT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DFCT_QTY"]);
                    //ob.EARN_PT = (dr["EARN_PT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EARN_PT"]);
                    ob.STD_PT = (dr["STD_PT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STD_PT"]);
                    List.Add(ob);
                }
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_FAB_ROLLModel> getRollDataList(Int64? pKNT_QC_STS_TYPE_ID = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_COLOR_ID = null, string pSTYLE_ORDER_NO = null, string pCOLOR_NAME_EN = null, string pQC_DT = null)
        {
            string sp = "pkg_knit_plan.knt_fab_roll_select";
            try
            {
                var oList = new List<KNT_FAB_ROLLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value =pKNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pSTYLE_ORDER_NO", Value =pSTYLE_ORDER_NO},
                     new CommandParameter() {ParameterName = "pCOLOR_NAME_EN", Value =pCOLOR_NAME_EN},
                     new CommandParameter() {ParameterName = "pQC_DT", Value =pQC_DT},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_FAB_ROLLModel ob = new KNT_FAB_ROLLModel();
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.REPORT_DT = (dr["REPORT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORT_DT"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FIN_DIA"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_COLOR_ID = (dr["ACT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_COLOR_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.KNT_ROLL_STS_TYPE_ID = (dr["KNT_ROLL_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_STS_TYPE_ID"]);
                    ob.LK_QC_GRD_ID = (dr["LK_QC_GRD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_QC_GRD_ID"]);

                    ob.RL_STS_TYP_NAME = (dr["RL_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RL_STS_TYP_NAME"]);
                    ob.RGB_COL_CODE = (dr["RGB_COL_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RGB_COL_CODE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.OPERATOR_NAME = (dr["OPERATOR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OPERATOR_NAME"]);

                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    if ((dr["CHALAN_DT"] != DBNull.Value))
                    {
                        ob.CHALAN_DT = Convert.ToDateTime(dr["CHALAN_DT"]);
                    }

                    if ((dr["QC_DT"] != DBNull.Value))
                    {
                        ob.QC_DT = Convert.ToDateTime(dr["QC_DT"]);
                    }
                    ob.DED_ROLL_WT = (dr["DED_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DED_ROLL_WT"]);
                    ob.TOTAL_PT = (dr["TOTAL_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOTAL_PT"]);
                    ob.GRADE_PT = (dr["GRADE_PT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GRADE_PT"]);
                    ob.GRADE_NO = (dr["GRADE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRADE_NO"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.QC_NOTE = (dr["QC_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NOTE"]);

                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.GREY_QC_BY = (dr["GREY_QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GREY_QC_BY"]);

                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.KNT_YRN_LOT_LST = (dr["KNT_YRN_LOT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_YRN_LOT_LST"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.MC_DIA = (dr["MC_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_DIA"]);
                    ob.MC_GG = (dr["MC_GG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_GG"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);

                    ob.IS_VALID = (dr["IS_VALID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_VALID"]);

                    oList.Add(ob);
                }
                return oList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public KNT_FAB_ROLLModel Select(long ID)
        {
            string sp = "Select_KNT_FAB_ROLL";
            try
            {
                var ob = new KNT_FAB_ROLLModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.PROD_DT = (dr["PROD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PROD_DT"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.FAB_ROLL_NO = (dr["FAB_ROLL_NO"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["FAB_ROLL_NO"]);
                    ob.FAB_ROLL_WT = (dr["FAB_ROLL_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_ROLL_WT"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.REPORT_DT = (dr["REPORT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REPORT_DT"]);
                    ob.ACT_FIN_DIA = (dr["ACT_FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FIN_DIA"]);
                    ob.ACT_FIN_GSM = (dr["ACT_FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_GSM"]);
                    ob.ACT_COLOR_ID = (dr["ACT_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_COLOR_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.KNT_ROLL_STS_TYPE_ID = (dr["KNT_ROLL_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_ROLL_STS_TYPE_ID"]);


                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object PrintLabel(long ID, string pPRNTR_ADDRESS)
        {
            string sp = "pkg_knit_plan.label_in_select";
            try
            {
                var ob = new LABEL_INModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value =ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.LABEL_IN_ID = (dr["LABEL_IN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LABEL_IN_ID"]);
                    ob.KNT_FAB_ROLL_ID = (dr["KNT_FAB_ROLL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_FAB_ROLL_ID"]);
                    ob.LABEL_NAME = (dr["LABEL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LABEL_NAME"]);
                    ob.DATE_TIME = (dr["DATE_TIME"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DATE_TIME"]);

                    ob.WEIGHT = (dr["WEIGHT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["WEIGHT"]);
                    ob.LINE1 = (dr["LINE1"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE1"]);
                    ob.LINE2 = (dr["LINE2"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE2"]);
                    ob.LINE3 = (dr["LINE3"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE3"]);
                    ob.LINE4 = (dr["LINE4"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE4"]);
                    ob.LINE5 = (dr["LINE5"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE5"]);
                    ob.LINE6 = (dr["LINE6"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE6"]);
                    ob.LINE7 = (dr["LINE7"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE7"]);
                    ob.LINE8 = (dr["LINE8"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE8"]);
                    ob.LINE9 = (dr["LINE9"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE9"]);
                    ob.LINE10 = (dr["LINE10"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE10"]);

                    //ob.FILE_PATH = @"D:\LabelPrint\" + Convert.ToString(dr["PRINTER_NAME"]) + "\\NORSEL-IMPORT.txt";
                    ob.FILE_PATH = @"D:\LabelPrint\" + pPRNTR_ADDRESS + "\\NORSEL-IMPORT.txt";


                    if (File.Exists(ob.FILE_PATH))
                    {
                        File.Delete(ob.FILE_PATH);
                    }

                    if (!File.Exists(ob.FILE_PATH))
                    {
                        File.Create(ob.FILE_PATH).Dispose();
                        using (TextWriter tw = new StreamWriter(ob.FILE_PATH))
                        {
                            tw.WriteLine(ob.LABEL_NAME);//Norsel;
                            tw.WriteLine(1);
                            tw.WriteLine(ob.LINE1);
                            tw.WriteLine(ob.LINE2);
                            tw.WriteLine(ob.LINE3);
                            tw.WriteLine(ob.LINE4);
                            tw.WriteLine(ob.LINE5);
                            tw.WriteLine(ob.LINE6);
                            tw.WriteLine(ob.LINE7);

                            if (ob.LINE8 != String.Empty)
                            {
                                tw.WriteLine(ob.LINE8);
                            }

                            if (ob.LINE9 != String.Empty)
                            {
                                tw.WriteLine(ob.LINE9);
                            }
                            if (ob.LINE10 != String.Empty)
                            {
                                tw.WriteLine(ob.LINE10);
                            }

                            tw.Close();
                        }

                    }



                }
                return new
                {
                    SUCCESS = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Int64 KNT_PLAN_H_ID { get; set; }

        public long? KNT_ROLL_STS_TYPE_ID { get; set; }

        public string JOB_CRD_NO { get; set; }

        public string RL_STS_TYP_NAME { get; set; }

        public string RGB_COL_CODE { get; set; }

        public string SCHEDULE_NAME_EN { get; set; }

        public string OPERATOR_NAME { get; set; }

        public string Remove()
        {
            const string sp = "pkg_knit_plan.knt_fab_roll_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "OP_QTY_LEFT", Value =0, Direction = ParameterDirection.Output}
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

        public string LabelPrint(long pKNT_FAB_ROLL_ID, string pPRNTR_ADDRESS)
        {
            PrintLabel(pKNT_FAB_ROLL_ID, pPRNTR_ADDRESS);
            const string sp = "pkg_knit_plan.knt_fab_roll_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = pKNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pPRNTR_ADDRESS", Value = pPRNTR_ADDRESS},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1002 },
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output },
                     new CommandParameter() {ParameterName = "OP_QTY_LEFT", Value =0, Direction = ParameterDirection.Output}
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

        public string DIA_TYPE_NAME_EN { get; set; }
    }
}