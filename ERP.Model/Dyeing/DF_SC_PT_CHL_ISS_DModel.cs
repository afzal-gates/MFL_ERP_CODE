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
    public class DF_SC_PT_CHL_ISS_DModel
    {
        public Int64 DF_SC_PT_CHL_ISS_D_ID { get; set; }
        public Int64 DF_SC_PT_CHL_ISS_H_ID { get; set; }
        public Int64 DF_SC_PT_ISS_D1_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 ISS_ROLL_QTY { get; set; }
        public Int64 ISS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string DF_PROC_TYPE_LST { get; set; }



        public string Save()
        {
            const string sp = "SP_DF_SC_PT_CHL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_ID", Value = ob.DF_SC_PT_CHL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value = ob.DF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_SC_PT_CHL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_ID", Value = ob.DF_SC_PT_CHL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value = ob.DF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
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
            const string sp = "SP_DF_SC_PT_CHL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_ID", Value = ob.DF_SC_PT_CHL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value = ob.DF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_D1_ID", Value = ob.DF_SC_PT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_ROLL_QTY", Value = ob.ISS_ROLL_QTY},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
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

        public List<DF_SC_PT_CHL_ISS_DModel> SelectAll(Int64? pDF_SC_PT_CHL_ISS_H_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_CHL_ISS_D_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_CHL_ISS_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value =pDF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_CHL_ISS_DModel ob = new DF_SC_PT_CHL_ISS_DModel();
                    ob.DF_SC_PT_CHL_ISS_D_ID = (dr["DF_SC_PT_CHL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_D_ID"]);
                    ob.DF_SC_PT_CHL_ISS_H_ID = (dr["DF_SC_PT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_H_ID"]);
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_TRANSFR = (dr["IS_TRANSFR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFR"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_SC_PT_CHL_ISS_DModel> SelectByID(Int64? pDF_SC_PT_CHL_ISS_H_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_CHL_ISS_D_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_CHL_ISS_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_H_ID", Value =pDF_SC_PT_CHL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_CHL_ISS_DModel ob = new DF_SC_PT_CHL_ISS_DModel();
                    ob.DF_SC_PT_CHL_ISS_D_ID = (dr["DF_SC_PT_CHL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_D_ID"]);
                    ob.DF_SC_PT_CHL_ISS_H_ID = (dr["DF_SC_PT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_H_ID"]);
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_TRANSFR = (dr["IS_TRANSFR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFR"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.SC_PRG_NO = (dr["SC_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_NO"]);
                    
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.PRE_RCV_QTY = (dr["PRE_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_RCV_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SC_PT_CHL_ISS_DModel Select(long ID)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_CHL_ISS_D_SELECT";
            try
            {
                var ob = new DF_SC_PT_CHL_ISS_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_CHL_ISS_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PT_CHL_ISS_D_ID = (dr["DF_SC_PT_CHL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_D_ID"]);
                    ob.DF_SC_PT_CHL_ISS_H_ID = (dr["DF_SC_PT_CHL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_CHL_ISS_H_ID"]);
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_ROLL_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_TRANSFR = (dr["IS_TRANSFR"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_TRANSFR"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long RQD_QTY { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string MC_ORDER_NO_LST { get; set; }

        public string FAB_ITEM_DESC { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public long PRE_RCV_QTY { get; set; }

        public string IS_TRANSFR { get; set; }

        public string SC_PRG_NO { get; set; }
    }
}