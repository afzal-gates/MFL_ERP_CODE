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
    public class MC_ORDER_CNTRYModel
    {
        public Int64 MC_ORDER_CNTRY_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64 HR_COUNTRY_ID { get; set; }
        public Int64 ORD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string COUNTRY_NAME_EN { get; set; }



        

        public MC_ORDER_CNTRYModel Select(long ID)
        {
            string sp = "Select_MC_ORDER_CNTRY";
            try
            {
                var ob = new MC_ORDER_CNTRYModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_CNTRY_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_ORDER_CNTRY_ID = (dr["MC_ORDER_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_CNTRY_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.HR_COUNTRY_ID = (dr["HR_COUNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COUNTRY_ID"]);
                    ob.ORD_QTY = (dr["ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);

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