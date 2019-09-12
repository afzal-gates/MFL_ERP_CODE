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
    public class MC_ORDER_STYLModel
    {
        public Int64? MC_ORDER_STYL_ID { get; set; }
        public Int64? MC_ORDER_SHIP_ID { get; set; }
        public Int64? GMT_CAPACITY_WK_ID { get; set; }
        public string STYLE_EXT_NO { get; set; }
        public Int64? MC_STYLE_D_ITEM_ID { get; set; }
        public Int64? PARENT_ITEM_ID { get; set; }
        public Int64? ORDER_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public Decimal? UNIT_PRICE { get; set; }
        public long MC_STYLE_H_ID { get; set; }
        public Int64? RF_CURRENCY_ID { get; set; }        
        public string ORDER_NO { get; set; }
        
        public string STYLE_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string MODEL_NO { get; set; }
        public string MOU_NAME_EN { get; set; }
        public Decimal? STYLE_TOTAL_PRICE { get; set; }
        public Int64? SUB_TOTAL_QTY { get; set; }
        public Decimal? TOTAL_PRICE { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }


        public string COMBO_NO { get; set; }
        public List<COMBO_NO_VmModel> COMBO_NO_LIST { get; set; }
        public Int64? ITEM_INDEX { get; set; }
        //public List<MC_ORDER_SIZEModel> items { get; set; }
        private List<MC_ORDER_COLModel> _itemsColor = null;
        public List<MC_ORDER_COLModel> itemsColor
        {
            get
             {
                if (_itemsColor == null)
                {
                    _itemsColor = new List<MC_ORDER_COLModel>();
                     }  
                return _itemsColor;
                      }  
            set
            {
                _itemsColor = value;
            }
        }
        private List<MC_ORDER_STYLModel> _childItems = null;
        public List<MC_ORDER_STYLModel> childItems
                 {
            get
                     { 
                if (_childItems == null)
                      {  
                    _childItems = new List<MC_ORDER_STYLModel>();
                 }
                return _childItems;
            }
            set
            {
                _childItems = value;
            }
        }





        public List<MC_ORDER_STYLModel> getOrderStyleDropDownData(long? pMC_BYR_ACC_ID, string pORDER_NO, Int64? pMC_ORDER_H_ID, DateTime? pFIRSTDATE = null, DateTime? pLASTDATE = null, int? pOption=null, Int64? pRF_FAB_PROD_CAT_ID=null)
        {
            string sp = "pkg_common.get_Order_Style_Drop_Down_Data";
            try
            {
                var obList = new List<MC_ORDER_STYLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO},
                      new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                      new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                      new CommandParameter() {ParameterName = "pLASTDATE", Value = pLASTDATE},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption},

                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_STYLModel ob = new MC_ORDER_STYLModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_ORDER_STYLModel> getOrderStyleDropDownDataForPln(

              Int64? pMC_BYR_ACC_ID,
              String pORDER_NO,
              DateTime? pFIRSTDATE,
              DateTime? pLASTDATE,
              Int64? pINV_ITEM_CAT_ID_P,
              Int64? pINV_ITEM_CAT_ID,
              Int64? pLK_ORD_TYPE_ID

            )
        {
            string sp = "pkg_common.get_Order_Style_dd_for_pln";
            try
            {
                var obList = new List<MC_ORDER_STYLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO},
                      new CommandParameter(){ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                      new CommandParameter(){ParameterName = "pLASTDATE", Value = pLASTDATE},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID_P", Value =pINV_ITEM_CAT_ID_P},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value =pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pLK_ORD_TYPE_ID", Value =pLK_ORD_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_STYLModel ob = new MC_ORDER_STYLModel();
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_ORDER_STYLModel> getOrderStyleItemDropDownData(long? pMC_ORDER_H_ID, string pITEM_NAME_EN, Int64? pMC_ORDER_SHIP_ID)
        {
            string sp = "pkg_common.get_Order_Style_Drop_Down_Data";
            try
            {
                var obList = new List<MC_ORDER_STYLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value =pMC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_STYLModel ob = new MC_ORDER_STYLModel();
                    ob.MC_ORDER_STYL_ID = (dr["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_STYL_ID"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public List<MC_ORDER_STYLModel> getOrderStyleDropDownDataGmt(long? pMC_BYR_ACC_GRP_ID, Int64? pMC_STYLE_H_ID, string pORDER_NO, Int64? pGMT_DF_WASH_REQ_ID=null)
        {
            string sp = "pkg_common.get_Order_Style_dd_for_gmt";
            try
            {
                var obList = new List<MC_ORDER_STYLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pGMT_DF_WASH_REQ_ID", Value = pGMT_DF_WASH_REQ_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_STYLModel ob = new MC_ORDER_STYLModel();
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
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