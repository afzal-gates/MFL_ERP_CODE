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
    public class GMT_PLN_ODR_LIST_CAP_WKModel
    {
        public Int64 MC_ORDER_SHIP_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public string LK_DATA_NAME_EN { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORD_TYPE { get; set; }
        public DateTime SHIP_DT { get; set; }
        public string ORDER_NO { get; set; }
        public Int64 ORDER_QTY { get; set; }
        public Int64 TOT_ORD_QTY { get; set; }
        public Int64 FOB_BOOKED { get; set; }
        public Decimal SMV { get; set; }
        public DateTime? SEW_START_DT { get; set; }
        public DateTime? SEW_END_DT { get; set; }


        public string Save(string pXML)
        {
            const string sp = "PKG_GMT_CAPACITY_BK.gmt_cap_ord_shipdate_change";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pXML", Value = pXML},                                      
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},                     
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

        public List<GMT_PLN_ODR_LIST_CAP_WKModel> Query(
            Int64 pGMT_PROD_PLN_CLNDR_ID_MN,
            Int64? pGMT_PROD_PLN_CLNDR_ID_WK,
            Int64 pPROD_UNIT_ID
        )
        {
            string sp = "PKG_GMT_CAPACITY_BK.get_order_list_cap_week";
            //pOption=3000=>Select All Data
            try
            {
                var obList = new List<GMT_PLN_ODR_LIST_CAP_WKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID_MN", Value =pGMT_PROD_PLN_CLNDR_ID_MN},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID_WK", Value =pGMT_PROD_PLN_CLNDR_ID_WK},
                     new CommandParameter() {ParameterName = "pPROD_UNIT_ID", Value =pPROD_UNIT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                          GMT_PLN_ODR_LIST_CAP_WKModel ob = new GMT_PLN_ODR_LIST_CAP_WKModel();
                            ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
                            ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                            ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                            ob.ORD_TYPE = (dr["ORD_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORD_TYPE"]);
                            ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);
                            ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                            ob.ORDER_QTY = (dr["ORDER_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORDER_QTY"]);
                            ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);
                            ob.FOB_BOOKED = (dr["FOB_BOOKED"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FOB_BOOKED"]);
                            ob.SMV = (dr["SMV"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["SMV"]);

                            ob.IS_LOADABLE = (dr["IS_LOADABLE"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["IS_LOADABLE"]);
                            ob.IS_PROV = (dr["IS_PROV"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PROV"]);

        

                            ob.LK_ORD_STATUS_ID = (dr["LK_ORD_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LK_ORD_STATUS_ID"]);

                             if (dr["SEW_START_DT"] != DBNull.Value)  {
                                 ob.SEW_START_DT = Convert.ToDateTime(dr["SEW_START_DT"]);
                             }
                             
                             if (dr["SEW_END_DT"] != DBNull.Value)  {
                                 ob.SEW_END_DT = Convert.ToDateTime(dr["SEW_END_DT"]);
                             }
                                if (ob.IS_PROV == "Y")
                                {
                                   ob.DATA_STATUS = "Projected";
                                }
                                else
                                {
                                    if (ob.LK_ORD_STATUS_ID == 365)
                                    {
                                        ob.DATA_STATUS = "Shipout";
                                    }
                                    else
                                    {
                                        if (ob.IS_LOADABLE == 0)
                                        {
                                            ob.DATA_STATUS = "Data Missing";
                                        }
                                        else
                                        {
                                            ob.DATA_STATUS = "";
                                        }
                                    }
                                }

                             ob.PLAN_STATUS = (ob.SEW_START_DT == null) ? "No Plan" : "";
                             ob.SHIP_OFFSET = (dr["SHIP_OFFSET"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHIP_OFFSET"]);
                             ob.LINE_CODE_LST = (dr["LINE_CODE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LINE_CODE_LST"]);

                             ob.ALLOCATED_QTY = (dr["ALLOCATED_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALLOCATED_QTY"]);
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long SHIP_OFFSET { get; set; }

        public string LINE_CODE_LST { get; set; }
        public long ALLOCATED_QTY { get; set; }
        public int IS_LOADABLE { get; set; }
        public string IS_PROV { get; set; }
        public int LK_ORD_STATUS_ID { get; set; }

        public string DATA_STATUS { get; set; }

        public string PLAN_STATUS { get; set; }
    }
}