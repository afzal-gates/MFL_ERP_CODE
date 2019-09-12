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
    public class MC_BLK_FAB_REQ_DModel
    {
        public Int64? MC_BLK_FAB_REQ_D_ID { get; set; }
        public Int64? MC_BLK_FAB_REQ_H_ID { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }

        //[Required(ErrorMessage="Please select fabric")]
        public Int64? MC_STYLE_D_FAB_ID { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public string FABRIC_DESC { get; set; }
        public string IS_ALL_ORDER { get; set; }
        public string MAP_ORDER_H_ID_LST { get; set; }        
        public string RF_GARM_PART_LST { get; set; }
        public string IS_CONTRAST { get; set; }
        public Int64? LK_YD_TYPE_ID { get; set; }
        public Int64? LK_FEDER_TYPE_ID { get; set; }
        public Int64? LK_FBR_GRP_ID { get; set; }
        public string FIN_DIA { get; set; }
        public string CUT_DIA { get; set; }
        public Int64? DIA_MOU_ID { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public Decimal? CONS_QTY { get; set; }
        public Int64? CONS_MOU_ID { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public Decimal? PCUT_WSTG { get; set; }
        public string STYLE_D_MODEL_LST { get; set; }
        public string STYLE_D_ITM_CAT_LST { get; set; }
        public string STYLE_D_ITEM_LST { get; set; }
        public string IS_ALL_COL { get; set; }
        public string MC_COLOR_LST { get; set; }
        public string COMBO_NO_LST { get; set; }
        public string IS_ALL_SIZE { get; set; }
        public string MC_SIZE_LST { get; set; }
        public string IS_SAME_GMT_COL { get; set; }
        public string FAB_COLOR_LST { get; set; }
        public string COLOR_SPEC { get; set; }
        public Int64? DISPLAY_ORDER { get; set; }


        public string GARM_PART_NAME_LST { get; set; }
        public string FABRIC_GROUP_NAME { get; set; }
        public string ITEM_PART_NAME_LST { get; set; }
        public string GARM_COLOR_NAME_LST { get; set; }
        public string FAB_COLOR_NAME_LST { get; set; }
        public string SIZE_NAME_LST { get; set; }
        public string FIN_DIA_DESC { get; set; }
        public string FIN_DIA_UNIT_NAME { get; set; }
        public string FIN_DIA_TYPE_NAME { get; set; }
        public string CONS_DOZ_DESC { get; set; }
        public string CONS_DOZ_UNIT_NAME { get; set; }
        public string IS_FEDER_TYPE { get; set; }
        public string IS_YD { get; set; }



        public List<MC_BLK_FAB_REQ_DModel> SelectAll()
        {
            string sp = "Select_MC_BLK_FAB_REQ_D";
            try
            {
                var obList = new List<MC_BLK_FAB_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_BLK_FAB_REQ_DModel ob = new MC_BLK_FAB_REQ_DModel();
                            ob.MC_BLK_FAB_REQ_D_ID = (dr["MC_BLK_FAB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_D_ID"]);
                            ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                            ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                            ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                            ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                            ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                            ob.CUT_DIA = (dr["CUT_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CUT_DIA"]);
                            ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                            ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                            ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);
                            ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                            ob.PCUT_WSTG = (dr["PCUT_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCUT_WSTG"]);          
                            ob.STYLE_D_MODEL_LST = (dr["STYLE_D_MODEL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_MODEL_LST"]);
                            ob.STYLE_D_ITM_CAT_LST = (dr["STYLE_D_ITM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITM_CAT_LST"]);
                            ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                            ob.IS_ALL_COL = (dr["IS_ALL_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_COL"]);
                            ob.MC_COLOR_LST = (dr["MC_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_COLOR_LST"]);
                            ob.COMBO_NO_LST = (dr["COMBO_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO_LST"]);
                            ob.IS_ALL_SIZE = (dr["IS_ALL_SIZE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_SIZE"]);
                            ob.MC_SIZE_LST = (dr["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_LST"]);
                            ob.IS_SAME_GMT_COL = (dr["IS_SAME_GMT_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SAME_GMT_COL"]);
                            ob.FAB_COLOR_LST = (dr["FAB_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_COLOR_LST"]);
                            ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                            
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_BLK_FAB_REQ_DModel Select(long ID)
        {
            string sp = "Select_MC_BLK_FAB_REQ_D";
            try
            {
                var ob = new MC_BLK_FAB_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_BLK_FAB_REQ_D_ID = (dr["MC_BLK_FAB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_D_ID"]);
                        ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                        ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                        ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);
                        ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                        ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                        ob.CUT_DIA = (dr["CUT_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CUT_DIA"]);
                        ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                        ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                        ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);
                        ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                        ob.PCUT_WSTG = (dr["PCUT_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCUT_WSTG"]);      
                        ob.STYLE_D_MODEL_LST = (dr["STYLE_D_MODEL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_MODEL_LST"]);
                        ob.STYLE_D_ITM_CAT_LST = (dr["STYLE_D_ITM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITM_CAT_LST"]);
                        ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                        ob.IS_ALL_COL = (dr["IS_ALL_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_COL"]);
                        ob.MC_COLOR_LST = (dr["MC_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_COLOR_LST"]);
                        ob.COMBO_NO_LST = (dr["COMBO_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO_LST"]);
                        ob.IS_ALL_SIZE = (dr["IS_ALL_SIZE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_SIZE"]);
                        ob.MC_SIZE_LST = (dr["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_LST"]);
                        ob.IS_SAME_GMT_COL = (dr["IS_SAME_GMT_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SAME_GMT_COL"]);
                        ob.FAB_COLOR_LST = (dr["FAB_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_COLOR_LST"]);
                        ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                        

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object BulkFabBookingDtlList(long pMC_BLK_FAB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_fab_req_d_select";
            try
            {
                var obList = new List<MC_BLK_FAB_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = pMC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_FAB_REQ_DModel ob = new MC_BLK_FAB_REQ_DModel();
                    ob.MC_BLK_FAB_REQ_D_ID = (dr["MC_BLK_FAB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_D_ID"]);
                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.IS_ALL_ORDER = (dr["IS_ALL_ORDER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_ORDER"]);                    
                    ob.MAP_ORDER_H_ID_LST = (dr["MAP_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAP_ORDER_H_ID_LST"]);
                    ob.RF_GARM_PART_LST = (dr["RF_GARM_PART_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RF_GARM_PART_LST"]);

                    if (dr["MC_STYLE_D_FAB_ID"] != DBNull.Value)
                    { ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]); }

                    if (dr["INV_ITEM_ID"] != DBNull.Value)
                    { ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]); }

                    if (dr["LK_FEDER_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_FEDER_TYPE_ID = (dr["LK_FEDER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FEDER_TYPE_ID"]);
                        ob.IS_FEDER_TYPE = "Y";
                    }
                    else
                    {
                        ob.IS_FEDER_TYPE = "N";
                    }

                    if (dr["LK_YD_TYPE_ID"] != DBNull.Value)
                    {
                        ob.LK_YD_TYPE_ID = (dr["LK_YD_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YD_TYPE_ID"]);
                        ob.IS_YD = "Y";
                    }
                    else
                    {
                        ob.IS_YD = "N";
                    }

                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.CUT_DIA = (dr["CUT_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CUT_DIA"]);

                    ob.FIN_DIA_UNIT_NAME = (dr["FIN_DIA_UNIT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_UNIT_NAME"]);
                    ob.FIN_DIA_TYPE_NAME = (dr["FIN_DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_TYPE_NAME"]);
                    ob.CONS_DOZ_UNIT_NAME = (dr["CONS_DOZ_UNIT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONS_DOZ_UNIT_NAME"]);


                    if (dr["DIA_MOU_ID"] != DBNull.Value)
                        ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    if (dr["LK_DIA_TYPE_ID"] != DBNull.Value)
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.CONS_QTY = (dr["CONS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_QTY"]);
                    ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.PCUT_WSTG = (dr["PCUT_WSTG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCUT_WSTG"]);
                    ob.STYLE_D_MODEL_LST = (dr["STYLE_D_MODEL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_MODEL_LST"]);
                    ob.STYLE_D_ITM_CAT_LST = (dr["STYLE_D_ITM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITM_CAT_LST"]);
                    ob.STYLE_D_ITEM_LST = (dr["STYLE_D_ITEM_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_D_ITEM_LST"]);
                    ob.IS_ALL_COL = (dr["IS_ALL_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_COL"]);
                    ob.MC_COLOR_LST = (dr["MC_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_COLOR_LST"]);
                    ob.COMBO_NO_LST = (dr["COMBO_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO_LST"]);
                    ob.IS_ALL_SIZE = (dr["IS_ALL_SIZE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ALL_SIZE"]);
                    ob.MC_SIZE_LST = (dr["MC_SIZE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_LST"]);
                    ob.IS_SAME_GMT_COL = (dr["IS_SAME_GMT_COL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SAME_GMT_COL"]);
                    ob.FAB_COLOR_LST = (dr["FAB_COLOR_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_COLOR_LST"]);
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);

                    ob.GARM_PART_NAME_LST = (dr["GARM_PART_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME_LST"]);
                    ob.ITEM_PART_NAME_LST = (dr["ITEM_PART_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_PART_NAME_LST"]);
                    ob.GARM_COLOR_NAME_LST = (dr["GARM_COLOR_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_COLOR_NAME_LST"]);
                    ob.FAB_COLOR_NAME_LST = (dr["FAB_COLOR_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_COLOR_NAME_LST"]);
                    ob.SIZE_NAME_LST = (dr["SIZE_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_NAME_LST"]);
                    ob.FABRIC_GROUP_NAME = (dr["FABRIC_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_GROUP_NAME"]);
                    ob.FIN_DIA_DESC = (dr["FIN_DIA_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_DESC"]);
                    ob.CONS_DOZ_DESC = (dr["CONS_DOZ_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CONS_DOZ_DESC"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}