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
    public class MC_ORDER_SIZEModel
    {
        public Int64? MC_ORDER_SIZE_ID { get; set; }              
        public Int64? MC_SIZE_ID { get; set; }
        public string SIZE_CODE { get; set; }
        public Int64? MC_ORDER_CNTRY_ID { get; set; }
        //public DateTime? SHIP_DT { get; set; }        
        public Int64? MC_ORDER_COL_ID { get; set; }        
        //[Required(ErrorMessage="Please input size wise quantity")]
        public Int64? SIZE_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public Decimal? UNIT_PRICE { get; set; }
        public Int64? RF_CURRENCY_ID { get; set; }
        public Decimal? TOTAL_SIZE_PRICE { get; set; }
        
    }


    public class MC_DEV_ORDER_SIZEModel
    {
        public Int64? MC_ORDER_SIZE_ID { get; set; }
        public Int64? MC_ORDER_SHIP_ID { get; set; }
        public Int64? MC_ORDER_STYL_ID { get; set; }
        public Int64? MC_STYLE_D_ITEM_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64? MC_ORDER_COL_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public Int64? MC_SIZE_ID { get; set; }
        public Int64? SIZE_QTY { get; set; }

        public string MODEL_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string ITEM_SNAME { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string COMBO_NO { get; set; }
        public string SIZE_CODE { get; set; }

        //public Int64? ITEM_INDEX { get; set; }
        //public Int64? SHIP_DT_INDEX { get; set; }
        //public Int64? COLOR_INDEX { get; set; }
        //public Int64? SIZE_INDEX { get; set; }
        //public Decimal? TOTAL_SIZE_PRICE { get; set; }



        public List<MC_DEV_ORDER_SIZEModel> DevStyleWiseOrdSizeList(Int64 pMC_STYLE_H_ID, Int64? pMC_STYLE_H_EXT_ID)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_DEV_ORDER_SIZEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3017},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_DEV_ORDER_SIZEModel ob = new MC_DEV_ORDER_SIZEModel();
                    ob.MC_ORDER_SIZE_ID = (dr["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SIZE_ID"]);
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.MC_ORDER_STYL_ID = (dr["MC_ORDER_STYL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_STYL_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    if (dr["PARENT_ID"] != DBNull.Value)
                        ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);

                    ob.MC_ORDER_COL_ID = (dr["MC_ORDER_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_COL_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);

                    ob.MODEL_NO = (dr["MODEL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MODEL_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.ITEM_SNAME = (dr["ITEM_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SNAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.COMBO_NO = (dr["COMBO_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMBO_NO"]);
                    ob.SIZE_CODE = (dr["SIZE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE"]);

                    ob.SIZE_QTY = (dr["SIZE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SIZE_QTY"]);

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