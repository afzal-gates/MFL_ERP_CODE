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
    public class DF_BT_SUB_LOT_CLCFModel
    {
        public Int64 DF_BT_SUB_LOT_CLCF_ID { get; set; }
        public Int64 DF_BT_SUB_LOT_ID { get; set; }
        public Int64 KNT_CLCF_STYL_ITEM_ID { get; set; }
        public string RQD_GREY_MEAS { get; set; }
        public string ACT_FIN_MEAS { get; set; }
        public Int64 RQD_GREY_PCS { get; set; }
        public Int64 ACT_FIN_PCS { get; set; }
        public Int64 REJECT_PCS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string SIZE_CODE_LST { get; set; }
        public string MEAS_WIDTH { get; set; }
        public string MEAS_HEIGHT { get; set; }


        public string Save()
        {
            const string sp = "SP_DF_BT_SUB_LOT_CLCF";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_CLCF_ID", Value = ob.DF_BT_SUB_LOT_CLCF_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_GREY_MEAS", Value = ob.RQD_GREY_MEAS},
                     new CommandParameter() {ParameterName = "pACT_FIN_MEAS", Value = ob.ACT_FIN_MEAS},
                     new CommandParameter() {ParameterName = "pRQD_GREY_PCS", Value = ob.RQD_GREY_PCS},
                     new CommandParameter() {ParameterName = "pACT_FIN_PCS", Value = ob.ACT_FIN_PCS},
                     new CommandParameter() {ParameterName = "pREJECT_PCS", Value = ob.REJECT_PCS},
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
            const string sp = "SP_DF_BT_SUB_LOT_CLCF";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_CLCF_ID", Value = ob.DF_BT_SUB_LOT_CLCF_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_GREY_MEAS", Value = ob.RQD_GREY_MEAS},
                     new CommandParameter() {ParameterName = "pACT_FIN_MEAS", Value = ob.ACT_FIN_MEAS},
                     new CommandParameter() {ParameterName = "pRQD_GREY_PCS", Value = ob.RQD_GREY_PCS},
                     new CommandParameter() {ParameterName = "pACT_FIN_PCS", Value = ob.ACT_FIN_PCS},
                     new CommandParameter() {ParameterName = "pREJECT_PCS", Value = ob.REJECT_PCS},
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
            const string sp = "SP_DF_BT_SUB_LOT_CLCF";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_CLCF_ID", Value = ob.DF_BT_SUB_LOT_CLCF_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pKNT_CLCF_STYL_ITEM_ID", Value = ob.KNT_CLCF_STYL_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRQD_GREY_MEAS", Value = ob.RQD_GREY_MEAS},
                     new CommandParameter() {ParameterName = "pACT_FIN_MEAS", Value = ob.ACT_FIN_MEAS},
                     new CommandParameter() {ParameterName = "pRQD_GREY_PCS", Value = ob.RQD_GREY_PCS},
                     new CommandParameter() {ParameterName = "pACT_FIN_PCS", Value = ob.ACT_FIN_PCS},
                     new CommandParameter() {ParameterName = "pREJECT_PCS", Value = ob.REJECT_PCS},
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

        public List<DF_BT_SUB_LOT_CLCFModel> SelectAll()
        {
            string sp = "Select_DF_BT_SUB_LOT_CLCF";
            try
            {
                var obList = new List<DF_BT_SUB_LOT_CLCFModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_CLCF_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOT_CLCFModel ob = new DF_BT_SUB_LOT_CLCFModel();
                    ob.DF_BT_SUB_LOT_CLCF_ID = (dr["DF_BT_SUB_LOT_CLCF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_CLCF_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.RQD_GREY_MEAS = (dr["RQD_GREY_MEAS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GREY_MEAS"]);
                    ob.ACT_FIN_MEAS = (dr["ACT_FIN_MEAS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_MEAS"]);
                    ob.RQD_GREY_PCS = (dr["RQD_GREY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_GREY_PCS"]);
                    ob.ACT_FIN_PCS = (dr["ACT_FIN_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_FIN_PCS"]);
                    ob.REJECT_PCS = (dr["REJECT_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_PCS"]);
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


        public List<DF_BT_SUB_LOT_CLCFModel> SelectByID(Int64? pDYE_BT_CARD_H_ID = null, Int64? pDF_BT_SUB_LOT_ID = null, Int64? pKNT_STYL_FAB_ITEM_H_ID = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_BT_SUB_LOT_CLCF_Select";
            try
            {
                var obList = new List<DF_BT_SUB_LOT_CLCFModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value =pDF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pKNT_STYL_FAB_ITEM_H_ID", Value =pKNT_STYL_FAB_ITEM_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOT_CLCFModel ob = new DF_BT_SUB_LOT_CLCFModel();
                    ob.DF_BT_SUB_LOT_CLCF_ID = (dr["DF_BT_SUB_LOT_CLCF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_CLCF_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.SIZE_CODE_LST = (dr["SIZE_CODE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE_LST"]);
                    ob.RQD_GREY_MEAS = (dr["RQD_GREY_MEAS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GREY_MEAS"]);
                    ob.ACT_FIN_MEAS = (dr["ACT_FIN_MEAS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_MEAS"]);
                    ob.RQD_GREY_PCS = (dr["RQD_GREY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_GREY_PCS"]);
                    ob.ACT_FIN_PCS = (dr["ACT_FIN_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_FIN_PCS"]);
                    ob.REJECT_PCS = (dr["REJECT_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_PCS"]);
                    if (ob.ACT_FIN_MEAS != string.Empty)
                    {
                        ob.MEAS_WIDTH = ob.ACT_FIN_MEAS.Split('x')[0].ToString();
                        ob.MEAS_HEIGHT = ob.ACT_FIN_MEAS.Split('x')[1].ToString();
                    }
                    else
                    {
                        ob.MEAS_WIDTH = ob.RQD_GREY_MEAS.Split('x')[0].ToString();
                        ob.MEAS_HEIGHT = ob.RQD_GREY_MEAS.Split('x')[1].ToString();
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

        public DF_BT_SUB_LOT_CLCFModel Select(long ID)
        {
            string sp = "Select_DF_BT_SUB_LOT_CLCF";
            try
            {
                var ob = new DF_BT_SUB_LOT_CLCFModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_CLCF_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_SUB_LOT_CLCF_ID = (dr["DF_BT_SUB_LOT_CLCF_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_CLCF_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.KNT_CLCF_STYL_ITEM_ID = (dr["KNT_CLCF_STYL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_CLCF_STYL_ITEM_ID"]);
                    ob.RQD_GREY_MEAS = (dr["RQD_GREY_MEAS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GREY_MEAS"]);
                    ob.ACT_FIN_MEAS = (dr["ACT_FIN_MEAS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACT_FIN_MEAS"]);
                    ob.RQD_GREY_PCS = (dr["RQD_GREY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_GREY_PCS"]);
                    ob.ACT_FIN_PCS = (dr["ACT_FIN_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_FIN_PCS"]);
                    ob.REJECT_PCS = (dr["REJECT_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REJECT_PCS"]);
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

    }
}