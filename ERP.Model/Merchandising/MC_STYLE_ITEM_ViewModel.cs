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


    public class BA_STYLE_ITEM_PAGER
    {
        public Int64 total { get; set; }

        private List<MC_STYLE_ITEM_ViewModel> _data = null;

        public List<MC_STYLE_ITEM_ViewModel> data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<MC_STYLE_ITEM_ViewModel>();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
    public class MC_STYLE_ITEM_ViewModel
    {
        public Int64 MC_STYLE_H_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string STYL_EXT_NO { get; set; }
        public string STYLE_DESC { get; set; }
        public string SZ_RANGE { get; set; }
        public string SEASON { get; set; }

        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string COMBO_NO { get; set; }
        public string BUYER_ITEM_NO { get; set; }
        public string FABRICATION { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public string TECH_SPEC { get; set; }
        public Int64 LK_GARM_TYPE_ID { get; set; }
        public Int64 LK_GARM_DEPT_ID { get; set; }
        public Int64 LK_SLV_TYPE_ID { get; set; }
        public Int64 LK_NECK_TYPE_ID { get; set; }
        public string LK_WASH_TYPE_ID { get; set; }
        public string LK_FINIS_TYPE_ID { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 PCS_PER_PACK { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public string IS_YD { get; set; }
        public string IS_AOP { get; set; }
        public string IS_EMB { get; set; }
        public string IS_PRINT { get; set; }
        public string STYL_KEY_IMG { get; set; }
        public string STYL_ALT_IMG { get; set; }
        public Int64 LK_ITEM_STATUS_ID { get; set; }

        public string ITEM_CAT_NAME_EN { get; set; }
        public string GARM_TYPE_NAME { get; set; }
        public string GARM_DEPT_NAME { get; set; }
        public string SLV_TYPE_NAME { get; set; }
        public string NECK_TYPE_NAME { get; set; }


        public BA_STYLE_ITEM_PAGER BuyerWiseStyleList(Int64? pMC_BYR_ACC_ID, Int64? pageNumber, Int64? pageSize, String pSTYLE_NO = null, String pITEM_CAT_NAME_EN = null, String pGARM_TYPE_NAME = null, String pGARM_DEPT_NAME = null, String pSLV_TYPE_NAME = null, String pNECK_TYPE_NAME = null, Int64? pLK_STYL_DEV_ID=null)
        {
            string sp = "pkg_merchandising.select_data_style_galary_grid";
            try
            {
                var obj = new BA_STYLE_ITEM_PAGER();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pLK_STYL_DEV_ID", Value = pLK_STYL_DEV_ID},
                      new CommandParameter() {ParameterName = "pSTYLE_NO", Value = pSTYLE_NO},
                     
                     new CommandParameter() {ParameterName = "total", Value =0, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                        obj.total = (dr["VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VALUE"]);

                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEM_ViewModel ob = new MC_STYLE_ITEM_ViewModel();
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.SEASON = (dr["SEASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);



                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.BUYER_ITEM_NO = (dr["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_ITEM_NO"]);
                    ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.TECH_SPEC = (dr["TECH_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_GARM_DEPT_ID = (dr["LK_GARM_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_DEPT_ID"]);
                    ob.LK_SLV_TYPE_ID = (dr["LK_SLV_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SLV_TYPE_ID"]);
                    ob.LK_NECK_TYPE_ID = (dr["LK_NECK_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NECK_TYPE_ID"]);

                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);

                    ob.STYL_ALT_IMG = (dr["STYL_ALT_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_ALT_IMG"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);

                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.GARM_TYPE_NAME = (dr["GARM_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_TYPE_NAME"]);
                    ob.GARM_DEPT_NAME = (dr["GARM_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_DEPT_NAME"]);

                    ob.SLV_TYPE_NAME = (dr["SLV_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SLV_TYPE_NAME"]);
                    ob.NECK_TYPE_NAME = (dr["NECK_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NECK_TYPE_NAME"]);

                    obj.data.Add(ob);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public List<MC_STYLE_ITEM_ViewModel> StyleWiseStyleList(Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_merchandising.mc_style_h_select";
            try
            {
                var obList = new List<MC_STYLE_ITEM_ViewModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_STYLE_ITEM_ViewModel ob = new MC_STYLE_ITEM_ViewModel();
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {
                        ob.PARENT_ID = Convert.ToInt64(dr["PARENT_ID"]);
                    }

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.STYL_EXT_NO = (dr["STYL_EXT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_EXT_NO"]);
                    ob.STYLE_DESC = (dr["STYLE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_DESC"]);
                    ob.SEASON = (dr["SEASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SEASON"]);
                    ob.SZ_RANGE = (dr["SZ_RANGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SZ_RANGE"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);

                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.BUYER_ITEM_NO = (dr["BUYER_ITEM_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_ITEM_NO"]);
                    ob.FABRICATION = (dr["FABRICATION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRICATION"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.TECH_SPEC = (dr["TECH_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TECH_SPEC"]);
                    ob.LK_GARM_TYPE_ID = (dr["LK_GARM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_TYPE_ID"]);
                    ob.LK_GARM_DEPT_ID = (dr["LK_GARM_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GARM_DEPT_ID"]);
                    ob.LK_SLV_TYPE_ID = (dr["LK_SLV_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SLV_TYPE_ID"]);
                    ob.LK_NECK_TYPE_ID = (dr["LK_NECK_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_NECK_TYPE_ID"]);
                    //ob.LK_WASH_TYPE_ID = (dr["LK_WASH_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_WASH_TYPE_ID"]);
                    //ob.LK_FINIS_TYPE_ID = (dr["LK_FINIS_TYPE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FINIS_TYPE_ID"]);
                    //ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    //ob.PCS_PER_PACK = (dr["PCS_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCS_PER_PACK"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.IS_YD = (dr["IS_YD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_YD"]);
                    ob.IS_AOP = (dr["IS_AOP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_AOP"]);
                    ob.IS_EMB = (dr["IS_EMB"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_EMB"]);
                    ob.IS_PRINT = (dr["IS_PRINT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PRINT"]);
                    ob.STYL_KEY_IMG = (dr["STYL_KEY_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_KEY_IMG"]);

                    ////Convert Base64 to String
                    //byte[] byt = Convert.FromBase64String(ob.STYL_KEY_IMG);
                    //ob.STYL_KEY_IMG = System.Text.Encoding.UTF8.GetString(byt);

                    ob.STYL_ALT_IMG = (dr["STYL_ALT_IMG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYL_ALT_IMG"]);
                    ob.LK_ITEM_STATUS_ID = (dr["LK_ITEM_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ITEM_STATUS_ID"]);

                    ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    ob.GARM_TYPE_NAME = (dr["GARM_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_TYPE_NAME"]);
                    ob.GARM_DEPT_NAME = (dr["GARM_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_DEPT_NAME"]);

                    ob.SLV_TYPE_NAME = (dr["SLV_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SLV_TYPE_NAME"]);
                    ob.NECK_TYPE_NAME = (dr["NECK_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NECK_TYPE_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MODEL_NO { get; set; }
    }
}