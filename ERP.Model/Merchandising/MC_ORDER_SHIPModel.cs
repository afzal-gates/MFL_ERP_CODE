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
    public class MC_ORDER_SHIPModel
    {
        public Int64? MC_ORDER_SHIP_ID { get; set; }
        public Int64? MC_ORDER_H_ID { get; set; }
        public string ORDER_NO { get; set; }
        public DateTime? SHIP_DT { get; set; }
        public string SHIP_DESC { get; set; }
        public Int64? SHIP_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public Decimal? AVG_UNIT_PRICE { get; set; }
        public string IS_MULTI_SHIP_DT { get; set; }
        public bool IS_ACTIVE { get; set; }
        

        public Int64? SHIP_DT_INDEX { get; set; }
        private List<MC_ORDER_STYLModel> _itemsStyle = null;
        public List<MC_ORDER_STYLModel> itemsStyle
        {
            get
            {
                if (_itemsStyle == null)
                {
                    _itemsStyle = new List<MC_ORDER_STYLModel>();
                }
                return _itemsStyle;
            }
            set
            {
                _itemsStyle = value;
            }
        }
        public object formItem { get; set; }
        public List<MC_GMT_CAP_ITEMModel> cap_itms { get; set; }
        public string CapItmXML { get; set; }



        public List<MC_ORDER_SHIPModel> GetOrderShipmentList(Int64 pMC_STYLE_H_ID, Int32 pNUMBER_OF_REC, Int64? pMC_ORDER_SHIP_ID = null, string pORDER_NO = null)
        {
            string sp = "pkg_mc_order.mc_order_h_select";
            try
            {
                var obList = new List<MC_ORDER_SHIPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value =pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pNUMBER_OF_REC", Value =pNUMBER_OF_REC},
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value =pMC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value =pORDER_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3021},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_SHIPModel ob = new MC_ORDER_SHIPModel();
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                    ob.SHIP_DESC = (dr["SHIP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_DESC"]);
                    ob.SHIP_QTY = (dr["SHIP_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHIP_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.AVG_UNIT_PRICE = (dr["AVG_UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_UNIT_PRICE"]);

                    ob.IS_MULTI_SHIP_DT = (dr["IS_MULTI_SHIP_DT"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MULTI_SHIP_DT"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Save()
        // pOption=1000=>General Form Save
        {
            const string sp = "pkg_common.insert_order_ship_data";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = ob.MC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = ob.MC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pSHIP_DT_EXT_DT", Value = ob.SHIP_DT_EXT_DT},
                     new CommandParameter() {ParameterName = "pSHIP_DT_EXT_REASON", Value = ob.SHIP_DT_EXT_REASON},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public MC_ORDER_SHIPModel Select(long pMC_ORDER_SHIP_ID)
        {
            string sp = "pkg_common.select_order_ship_data";
            try
            {
                var ob = new MC_ORDER_SHIPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_SHIP_ID", Value = pMC_ORDER_SHIP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    ob.SHIP_DT_EXT_DT = Convert.ToDateTime(dr["SHIP_DT"]).AddDays(7);

                    ob.SHIP_DESC = (dr["SHIP_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_DESC"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.SHIP_QTY = (dr["SHIP_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHIP_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.AVG_UNIT_PRICE = (dr["AVG_UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["AVG_UNIT_PRICE"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string STYLE_NO { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public DateTime SHIP_DT_EXT_DT { get; set; }
        public string SHIP_DT_EXT_REASON { get; set; }
    }
}