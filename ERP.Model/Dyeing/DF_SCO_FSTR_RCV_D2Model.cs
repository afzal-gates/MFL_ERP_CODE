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
    public class DF_SCO_FSTR_RCV_D2Model
    {
        public Int64 DF_SCO_FSTR_RCV_D2_ID { get; set; }
        public Int64 DF_SCO_FSTR_RCV_H_ID { get; set; }
        public Int64 DYE_SC_PRG_ISS_ID { get; set; }
        public Int64 DYE_GSTR_BT_ISS_D2_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 KNT_CLCF_STYL_ITEM_ID { get; set; }
        public Int64 RCV_ROLL_QTY { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Decimal LOSS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string NOTE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string DYE_BATCH_NO { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string FAB_TYPE_NAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public long ISS_QTY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_SCO_FSTR_RCV_D2";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FSTR_RCV_D2_ID", Value = ob.DF_SCO_FSTR_RCV_D2_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_FSTR_RCV_H_ID", Value = ob.DF_SCO_FSTR_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = ob.DYE_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_BT_ISS_D2_ID", Value = ob.DYE_GSTR_BT_ISS_D2_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pLOSS_QTY", Value = ob.LOSS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNOTE", Value = ob.NOTE},
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
            const string sp = "SP_DF_SCO_FSTR_RCV_D2";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FSTR_RCV_D2_ID", Value = ob.DF_SCO_FSTR_RCV_D2_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_FSTR_RCV_H_ID", Value = ob.DF_SCO_FSTR_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = ob.DYE_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_BT_ISS_D2_ID", Value = ob.DYE_GSTR_BT_ISS_D2_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pLOSS_QTY", Value = ob.LOSS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNOTE", Value = ob.NOTE},
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
            const string sp = "SP_DF_SCO_FSTR_RCV_D2";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FSTR_RCV_D2_ID", Value = ob.DF_SCO_FSTR_RCV_D2_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_FSTR_RCV_H_ID", Value = ob.DF_SCO_FSTR_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_SC_PRG_ISS_ID", Value = ob.DYE_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_BT_ISS_D2_ID", Value = ob.DYE_GSTR_BT_ISS_D2_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRCV_ROLL_QTY", Value = ob.RCV_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pRCV_QTY", Value = ob.RCV_QTY},
                     new CommandParameter() {ParameterName = "pLOSS_QTY", Value = ob.LOSS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pNOTE", Value = ob.NOTE},
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

        public List<DF_SCO_FSTR_RCV_D2Model> SelectAll(Int64? pDF_SCO_FSTR_RCV_H_ID = null)
        {
            string sp = "PKG_DF_RCV_FIN_FAB.DF_SCO_FSTR_RCV_D2_Select";
            try
            {
                var obList = new List<DF_SCO_FSTR_RCV_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FSTR_RCV_H_ID", Value =pDF_SCO_FSTR_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SCO_FSTR_RCV_D2Model ob = new DF_SCO_FSTR_RCV_D2Model();
                    ob.DF_SCO_FSTR_RCV_D2_ID = (dr["DF_SCO_FSTR_RCV_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_FSTR_RCV_D2_ID"]);
                    ob.DF_SCO_FSTR_RCV_H_ID = (dr["DF_SCO_FSTR_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_FSTR_RCV_H_ID"]);
                    ob.DYE_SC_PRG_ISS_ID = (dr["DYE_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SC_PRG_ISS_ID"]);
                    ob.SC_BATCH_NO = (dr["SC_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_BATCH_NO"]);
                    //ob.DYE_GSTR_BT_ISS_D2_ID = (dr["DYE_GSTR_BT_ISS_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_BT_ISS_D2_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.LOSS_QTY = (dr["LOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOSS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);

                    ob.processTypeList = new DF_SCO_FSTR_RCV_PROCModel().SelectAll(ob.DF_SCO_FSTR_RCV_H_ID, ob.DYE_BT_CARD_H_ID);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SCO_FSTR_RCV_D2Model Select(long ID)
        {
            string sp = "Select_DF_SCO_FSTR_RCV_D2";
            try
            {
                var ob = new DF_SCO_FSTR_RCV_D2Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FSTR_RCV_D2_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SCO_FSTR_RCV_D2_ID = (dr["DF_SCO_FSTR_RCV_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_FSTR_RCV_D2_ID"]);
                    ob.DF_SCO_FSTR_RCV_H_ID = (dr["DF_SCO_FSTR_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_FSTR_RCV_H_ID"]);
                    ob.DYE_SC_PRG_ISS_ID = (dr["DYE_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SC_PRG_ISS_ID"]);
                    ob.DYE_GSTR_BT_ISS_D2_ID = (dr["DYE_GSTR_BT_ISS_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_BT_ISS_D2_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.RCV_ROLL_QTY = (dr["RCV_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_ROLL_QTY"]);
                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.LOSS_QTY = (dr["LOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOSS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.NOTE = (dr["NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NOTE"]);
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

        public List<DF_SCO_FSTR_RCV_D2Model> SelectForReceive(Int64? pSCM_SUPPLIER_ID = null)
        {
            string sp = "PKG_DF_RCV_FIN_FAB.dye_gstr_bt_iss_d2_select";
            try
            {
                List<DF_SCO_FSTR_RCV_D2Model> list = new List<DF_SCO_FSTR_RCV_D2Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new DF_SCO_FSTR_RCV_D2Model();
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.DYE_SC_PRG_ISS_ID = (dr["DYE_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SC_PRG_ISS_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);

                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    //ob.processTypeList = new DF_PROC_TYPEModel().SelectAll(5);
                    ob.processTypeList = new DF_SCO_FSTR_RCV_PROCModel().SelectAll();

                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DF_SCO_FSTR_RCV_PROCModel> processTypeList { get; set; }

        public string MESUREMENT { get; set; }

        public string SC_BATCH_NO { get; set; }
    }
}