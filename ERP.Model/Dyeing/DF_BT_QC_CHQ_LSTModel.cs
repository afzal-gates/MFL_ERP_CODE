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
    public class DF_BT_QC_CHQ_LSTModel
    {
        public Int64 DF_BT_QC_CHQ_LST_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 RF_DF_QC_PARAM_TYPE_ID { get; set; }
        public Int64 LK_PARAM_QC_STS_ID { get; set; }
        public string CHQ_IGNR_RSN_DESC { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_BT_QC_CHQ_LST";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_QC_CHQ_LST_ID", Value = ob.DF_BT_QC_CHQ_LST_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pRF_DF_QC_PARAM_TYPE_ID", Value = ob.RF_DF_QC_PARAM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_PARAM_QC_STS_ID", Value = ob.LK_PARAM_QC_STS_ID},
                     new CommandParameter() {ParameterName = "pCHQ_IGNR_RSN_DESC", Value = ob.CHQ_IGNR_RSN_DESC},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_DF_BT_QC_CHQ_LST";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_QC_CHQ_LST_ID", Value = ob.DF_BT_QC_CHQ_LST_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pRF_DF_QC_PARAM_TYPE_ID", Value = ob.RF_DF_QC_PARAM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_PARAM_QC_STS_ID", Value = ob.LK_PARAM_QC_STS_ID},
                     new CommandParameter() {ParameterName = "pCHQ_IGNR_RSN_DESC", Value = ob.CHQ_IGNR_RSN_DESC},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_DF_BT_QC_CHQ_LST";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_QC_CHQ_LST_ID", Value = ob.DF_BT_QC_CHQ_LST_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pRF_DF_QC_PARAM_TYPE_ID", Value = ob.RF_DF_QC_PARAM_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_PARAM_QC_STS_ID", Value = ob.LK_PARAM_QC_STS_ID},
                     new CommandParameter() {ParameterName = "pCHQ_IGNR_RSN_DESC", Value = ob.CHQ_IGNR_RSN_DESC},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<DF_BT_QC_CHQ_LSTModel> SelectAll()
        {
            string sp = "pkg_df_inspection.df_bt_qc_chq_lst_select";
            try
            {
                var obList = new List<DF_BT_QC_CHQ_LSTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_QC_CHQ_LST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_QC_CHQ_LSTModel ob = new DF_BT_QC_CHQ_LSTModel();
                    ob.DF_BT_QC_CHQ_LST_ID = (dr["DF_BT_QC_CHQ_LST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_QC_CHQ_LST_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.RF_DF_QC_PARAM_TYPE_ID = (dr["RF_DF_QC_PARAM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DF_QC_PARAM_TYPE_ID"]);
                    ob.LK_PARAM_QC_STS_ID = (dr["LK_PARAM_QC_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PARAM_QC_STS_ID"]);
                    ob.CHQ_IGNR_RSN_DESC = (dr["CHQ_IGNR_RSN_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHQ_IGNR_RSN_DESC"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DF_BT_QC_CHQ_LSTModel> SelectByID(Int64? pDYE_BT_CARD_H_ID = null, Int64? pDF_RIB_SHADE_RPT_H_ID = null)
        {
            string sp = "pkg_df_inspection.df_bt_qc_chq_lst_select";
            try
            {
                var obList = new List<DF_BT_QC_CHQ_LSTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_QC_CHQ_LSTModel ob = new DF_BT_QC_CHQ_LSTModel();
                    ob.DF_BT_QC_CHQ_LST_ID = (dr["DF_BT_QC_CHQ_LST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_QC_CHQ_LST_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.RF_RESP_DEPT_ID = (dr["RF_RESP_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RESP_DEPT_ID"]);
                    ob.RF_DF_QC_PARAM_TYPE_ID = (dr["RF_DF_QC_PARAM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DF_QC_PARAM_TYPE_ID"]);
                    ob.LK_PARAM_QC_STS_ID = (dr["LK_PARAM_QC_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PARAM_QC_STS_ID"]);
                    ob.CHQ_IGNR_RSN_DESC = (dr["CHQ_IGNR_RSN_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHQ_IGNR_RSN_DESC"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CQ_PARAM_EN_NAME = (dr["CQ_PARAM_EN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CQ_PARAM_EN_NAME"]);
                    ob.LK_FBR_GRP_LST = (dr["LK_FBR_GRP_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FBR_GRP_LST"]);
                    if (pDF_RIB_SHADE_RPT_H_ID > 0)
                    {
                        var ListDtl = new DF_RIB_SHADE_RPT_DModel().SelectByID(pDF_RIB_SHADE_RPT_H_ID, ob.LK_FBR_GRP_LST);
                        var models = ListDtl.ToList().Select(
                            md => new DYE_BT_CARD_HModel
                            {
                                DF_RIB_SHADE_RPT_D_ID=md.DF_RIB_SHADE_RPT_D_ID,
                                DF_RIB_SHADE_RPT_H_ID = md.DF_RIB_SHADE_RPT_H_ID,
                                KNT_STYL_FAB_ITEM_H_ID = md.KNT_STYL_FAB_ITEM_H_ID,
                                DYE_BT_CARD_H_ID = md.DYE_BT_CARD_H_ID,
                                BATCH_QTY = Convert.ToInt64(md.BATCH_QTY),
                                FINIS_QTY = md.FINIS_QTY,
                                ROLL_QTY = md.ROLL_QTY,
                                QTY_MOU_ID = md.QTY_MOU_ID,
                                FAB_ITEM_DESC = md.FAB_ITEM_DESC,
                            }).OrderBy(m => m.FAB_ITEM_DESC);
                        ob.KNIT_ITEM_LST = models.ToList();
                    }
                    else
                        ob.KNIT_ITEM_LST = new DYE_BT_CARD_HModel().GetFabricByBatchNo(pDYE_BT_CARD_H_ID, ob.LK_FBR_GRP_LST);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DF_BT_QC_CHQ_LSTModel Select(long ID)
        {
            string sp = "Select_DF_BT_QC_CHQ_LST";
            try
            {
                var ob = new DF_BT_QC_CHQ_LSTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_QC_CHQ_LST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_QC_CHQ_LST_ID = (dr["DF_BT_QC_CHQ_LST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_QC_CHQ_LST_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.RF_DF_QC_PARAM_TYPE_ID = (dr["RF_DF_QC_PARAM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DF_QC_PARAM_TYPE_ID"]);
                    ob.LK_PARAM_QC_STS_ID = (dr["LK_PARAM_QC_STS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PARAM_QC_STS_ID"]);
                    ob.CHQ_IGNR_RSN_DESC = (dr["CHQ_IGNR_RSN_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHQ_IGNR_RSN_DESC"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CQ_PARAM_EN_NAME { get; set; }

        public List<DYE_BT_CARD_HModel> KNIT_ITEM_LST { get; set; }

        public string LK_FBR_GRP_LST { get; set; }




        public long RF_RESP_DEPT_ID { get; set; }
    }
}