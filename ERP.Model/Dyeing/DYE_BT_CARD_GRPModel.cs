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


    public class DYE_BT_CARD_ROLL
    {
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 KNT_FAB_ROLL_ID { get; set; }
        public string TYPE { get; set; }
        public string Save()
        {
            const string sp = "PKG_DYE_GFAB_REQ.knt_btc_roll_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID },
                     new CommandParameter() {ParameterName = "pDYE_GSTR_ISS_H_ID", Value = ob.DYE_GSTR_ISS_H_ID },
                     new CommandParameter() {ParameterName = "pKNT_FAB_ROLL_ID", Value = ob.KNT_FAB_ROLL_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_GRP_ID", Value = ob.DYE_BT_CARD_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},                     
                     new CommandParameter() {ParameterName = "pTYPE", Value = ob.TYPE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = (ob.TYPE =="insert"||ob.TYPE =="delete") ? 1000 : 1002},
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


        public Int64 DYE_BT_CARD_GRP_ID { get; set; }

        public Int64? DYE_GSTR_ISS_H_ID { get; set; }

        public Int64? LK_FBR_GRP_ID { get; set; }
    }
    public class DYE_BT_CARD_GRPModel
    {
        public Int64 DYE_BT_CARD_GRP_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 LK_FBR_GRP_ID { get; set; }
        public Int64 RQD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public string DYE_BT_CARD_GRP_NAME { get; set; }
        public string DYE_FAB_GRP_NAME { get; set; }
        public bool IS_TAB_ACT { get; set; }
        public List<DYE_BT_CARD_GRPModel> getGropData(Int64 pDYE_BT_CARD_H_ID)
        {
            string sp = "PKG_DYE_GFAB_REQ.grey_fab_req_list_select";
            try
            {
                var obList = new List<DYE_BT_CARD_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value =pDYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DYE_BT_CARD_GRPModel ob = new DYE_BT_CARD_GRPModel();
                        ob.DYE_BT_CARD_GRP_ID = (dr["DYE_BT_CARD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_GRP_ID"]);
                        ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                        ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                        ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                        ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                        ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                        ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                        ob.DYE_FAB_GRP_NAME = (dr["DYE_FAB_GRP_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_FAB_GRP_NAME"]);
                        ob.DYE_BT_CARD_GRP_NAME = (dr["DYE_BT_CARD_GRP_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_BT_CARD_GRP_NAME"]);

                        ob.IS_TAB_ACT = false;

                        obList.Add(ob);
                    }

                }
                else
                {
                    DYE_BT_CARD_GRPModel ob = new DYE_BT_CARD_GRPModel();
                    ob.DYE_BT_CARD_GRP_NAME = "Woven & Trims";

                    ob.IS_TAB_ACT = false;

                    obList.Add(ob);
                }


                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_BT_CARD_GRPModel> getGropDataForPreTreatment(long? pDYE_GSTR_REQ_H_ID)
        {
            string sp = "PKG_DYE_GFAB_REQ.grey_fab_req_list_select";
            try
            {
                var obList = new List<DYE_BT_CARD_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value =pDYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_BT_CARD_GRPModel ob = new DYE_BT_CARD_GRPModel();
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);
                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    //ob.DYE_BT_CARD_GRP_ID = (dr["DYE_BT_CARD_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_GRP_ID"]);
                    //ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.DYE_FAB_GRP_NAME = (dr["DYE_FAB_GRP_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_FAB_GRP_NAME"]);
                    ob.DYE_BT_CARD_GRP_NAME = (dr["DYE_BT_CARD_GRP_NAME"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["DYE_BT_CARD_GRP_NAME"]);

                    ob.IS_TAB_ACT = false;

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long KNT_STYL_FAB_ITEM_ID { get; set; }

        public long DF_SC_PT_ISS_D1_ID { get; set; }
    }
}