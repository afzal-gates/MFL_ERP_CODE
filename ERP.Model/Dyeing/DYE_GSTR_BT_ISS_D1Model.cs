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
    public class DYE_GSTR_BT_ISS_D1Model
    {
        public Int64 DYE_GSTR_BT_ISS_D1_ID { get; set; }
        public Int64 DYE_GSTR_ISS_H_ID { get; set; }
        public Int64 DYE_BT_CARD_D_FAB_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 KNT_STYL_FAB_ITEM_ID { get; set; }
        public Int64 ISS_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 NO_OF_ROLL { get; set; }
        public string HOLE_NO { get; set; }
        public string XML_DATA { get; set; }

        public string Save()
        {
            const string sp = "pkg_knit_plan.dye_gstr_bt_iss_d1_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_BT_ISS_D1_ID", Value = ob.DYE_GSTR_BT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = ob.DYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_D_FAB_ID", Value = ob.DYE_BT_CARD_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pNO_OF_ROLL", Value = ob.NO_OF_ROLL},
                     new CommandParameter() {ParameterName = "pHOLE_NO", Value = ob.HOLE_NO},
                     new CommandParameter() {ParameterName = "pXML_DATA", Value = ob.XML_DATA},
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
            const string sp = "pkg_knit_plan.dye_gstr_bt_iss_d1_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_BT_ISS_D1_ID", Value = ob.DYE_GSTR_BT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = ob.DYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_D_FAB_ID", Value = ob.DYE_BT_CARD_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pNO_OF_ROLL", Value = ob.NO_OF_ROLL},
                     new CommandParameter() {ParameterName = "pHOLE_NO", Value = ob.HOLE_NO},
                     new CommandParameter() {ParameterName = "pXML_DATA", Value = ob.XML_DATA},
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

        public string Delete()
        {
            const string sp = "SP_DYE_GSTR_BT_ISS_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_BT_ISS_D1_ID", Value = ob.DYE_GSTR_BT_ISS_D1_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = ob.DYE_GSTR_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_D_FAB_ID", Value = ob.DYE_BT_CARD_D_FAB_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_ID", Value = ob.KNT_STYL_FAB_ITEM_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY", Value = ob.ISS_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pNO_OF_ROLL", Value = ob.NO_OF_ROLL},
                     new CommandParameter() {ParameterName = "pHOLE_NO", Value = ob.HOLE_NO},
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

        public List<DYE_GSTR_BT_ISS_D1Model> SelectAll()
        {
            string sp = "Select_DYE_GSTR_BT_ISS_D1";
            try
            {
                var obList = new List<DYE_GSTR_BT_ISS_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_BT_ISS_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_GSTR_BT_ISS_D1Model ob = new DYE_GSTR_BT_ISS_D1Model();
                    ob.DYE_GSTR_BT_ISS_D1_ID = (dr["DYE_GSTR_BT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_BT_ISS_D1_ID"]);
                    ob.DYE_GSTR_ISS_H_ID = (dr["DYE_GSTR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_ISS_H_ID"]);
                    ob.DYE_BT_CARD_D_FAB_ID = (dr["DYE_BT_CARD_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_D_FAB_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.HOLE_NO = (dr["HOLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOLE_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DYE_GSTR_BT_ISS_D1Model Select(long ID)
        {
            string sp = "Select_DYE_GSTR_BT_ISS_D1";
            try
            {
                var ob = new DYE_GSTR_BT_ISS_D1Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_BT_ISS_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DYE_GSTR_BT_ISS_D1_ID = (dr["DYE_GSTR_BT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_BT_ISS_D1_ID"]);
                    ob.DYE_GSTR_ISS_H_ID = (dr["DYE_GSTR_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_ISS_H_ID"]);
                    ob.DYE_BT_CARD_D_FAB_ID = (dr["DYE_BT_CARD_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_D_FAB_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.HOLE_NO = (dr["HOLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HOLE_NO"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}