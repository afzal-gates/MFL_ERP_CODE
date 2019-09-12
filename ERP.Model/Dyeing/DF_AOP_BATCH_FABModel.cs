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
    public class DF_AOP_BATCH_FABModel
    {
        public Int64 DF_AOP_BATCH_FAB_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 DF_SCO_CHL_RCV_H_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_H_ID { get; set; }
        public Int64 RCV_ROLL_QTY { get; set; }
        public Decimal BT_FAB_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_FAB { get; set; }

        public string Save()
        {
            const string sp = "pkg_df_sc_program.df_aop_batch_fab_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_AOP_BATCH_FAB_ID", Value = ob.DF_AOP_BATCH_FAB_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pBT_FAB_QTY", Value = ob.BT_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pBATCH_REQ_DT", Value = ob.BATCH_REQ_DT},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pACT_BATCH_QTY", Value = ob.ACT_BATCH_QTY},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pXML_FAB", Value = ob.XML_FAB},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opDYE_BT_CARD_H_ID", Value =0, Direction = ParameterDirection.Output},
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
            const string sp = "SP_DF_AOP_BATCH_FAB";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_AOP_BATCH_FAB_ID", Value = ob.DF_AOP_BATCH_FAB_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pBT_FAB_QTY", Value = ob.BT_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_AOP_BATCH_FAB";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_AOP_BATCH_FAB_ID", Value = ob.DF_AOP_BATCH_FAB_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value = ob.KNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pBT_FAB_QTY", Value = ob.BT_FAB_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<DF_AOP_BATCH_FABModel> SelectAll()
        {
            string sp = "pkg_df_sc_program.df_aop_batch_fab_select";
            try
            {
                var obList = new List<DF_AOP_BATCH_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_AOP_BATCH_FAB_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_AOP_BATCH_FABModel ob = new DF_AOP_BATCH_FABModel();
                    ob.DF_AOP_BATCH_FAB_ID = (dr["DF_AOP_BATCH_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_AOP_BATCH_FAB_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.BT_FAB_QTY = (dr["BT_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BT_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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

        public List<DF_AOP_BATCH_FABModel> SelectPendingAopFabForBatch(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            string sp = "pkg_df_sc_program.df_aop_batch_fab_select";
            try
            {
                var obList = new List<DF_AOP_BATCH_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_AOP_BATCH_FABModel ob = new DF_AOP_BATCH_FABModel();
                    ob.DF_AOP_BATCH_FAB_ID = (dr["DF_AOP_BATCH_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_AOP_BATCH_FAB_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.BT_FAB_QTY = (dr["BT_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BT_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.FAB_TYPE_DESC = (dr["FAB_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_DESC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DF_AOP_BATCH_FABModel> SelectByID(Int64? pDYE_BT_CARD_H_ID = null)
        {
            string sp = "pkg_df_sc_program.df_aop_batch_fab_select";
            try
            {
                var obList = new List<DF_AOP_BATCH_FABModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_AOP_BATCH_FABModel ob = new DF_AOP_BATCH_FABModel();
                    ob.DF_AOP_BATCH_FAB_ID = (dr["DF_AOP_BATCH_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_AOP_BATCH_FAB_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.BT_FAB_QTY = (dr["BT_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BT_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RCV_FIN_FAB_QTY = (dr["RCV_FIN_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_FIN_FAB_QTY"]);

                    //BATCH_QTY, RCV_FIN_FAB_QTY, RCV_GREY_FAB_QTY
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.BATCH_REQ_DT = (dr["BATCH_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BATCH_REQ_DT"]);
                    
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.FAB_TYPE_DESC = (dr["FAB_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_DESC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_AOP_BATCH_FABModel Select(long ID)
        {
            string sp = "Select_DF_AOP_BATCH_FAB";
            try
            {
                var ob = new DF_AOP_BATCH_FABModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_AOP_BATCH_FAB_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_AOP_BATCH_FAB_ID = (dr["DF_AOP_BATCH_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_AOP_BATCH_FAB_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.BT_FAB_QTY = (dr["BT_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BT_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
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

        public string DYE_BATCH_NO { get; set; }

        public string FAB_ITEM_DESC { get; set; }

        public string FAB_TYPE_DESC { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string DYE_LOT_NO { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public Decimal ACT_BATCH_QTY { get; set; }

        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }

        public DateTime? BATCH_REQ_DT { get; set; }

        public long RCV_FIN_FAB_QTY { get; set; }
    }
}