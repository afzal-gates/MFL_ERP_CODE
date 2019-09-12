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
    public class GMT_FIN_POLY_DModel
    {
        public Int64 GMT_FIN_POLY_D_ID { get; set; }
        public Int64 GMT_FIN_POLY_H_ID { get; set; }
        public Int64 MC_ORDER_SIZE_ID { get; set; }
        public Int64 POLY_QTY { get; set; }
        public Int64 MC_SIZE_ID { get; set; }
        public Int64 MC_COLOR_ID { get; set; }
        public Int64 HR_COUNTRY_ID { get; set; }
        public Int64 MC_STYLE_D_ITEM_ID { get; set; }
        public Int64 MC_ORDER_SHIP_ID { get; set; }




        public List<GMT_FIN_POLY_DModel> SelectAll()
        {
            string sp = "Select_GMT_FIN_POLY_D";
            try
            {
                var obList = new List<GMT_FIN_POLY_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_FIN_POLY_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_FIN_POLY_DModel ob = new GMT_FIN_POLY_DModel();
                    ob.GMT_FIN_POLY_D_ID = (dr["GMT_FIN_POLY_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_FIN_POLY_D_ID"]);
                    ob.GMT_FIN_POLY_H_ID = (dr["GMT_FIN_POLY_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_FIN_POLY_H_ID"]);
                    ob.MC_ORDER_SIZE_ID = (dr["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SIZE_ID"]);
                    ob.POLY_QTY = (dr["POLY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["POLY_QTY"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_FIN_POLY_DModel Select(long ID)
        {
            string sp = "Select_GMT_FIN_POLY_D";
            try
            {
                var ob = new GMT_FIN_POLY_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_FIN_POLY_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_FIN_POLY_D_ID = (dr["GMT_FIN_POLY_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_FIN_POLY_D_ID"]);
                    ob.GMT_FIN_POLY_H_ID = (dr["GMT_FIN_POLY_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_FIN_POLY_H_ID"]);
                    ob.MC_ORDER_SIZE_ID = (dr["MC_ORDER_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SIZE_ID"]);
                    ob.POLY_QTY = (dr["POLY_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["POLY_QTY"]);
                    ob.MC_SIZE_ID = (dr["MC_SIZE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SIZE_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.MC_STYLE_D_ITEM_ID = (dr["MC_STYLE_D_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_ITEM_ID"]);
                    ob.MC_ORDER_SHIP_ID = (dr["MC_ORDER_SHIP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_SHIP_ID"]);

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