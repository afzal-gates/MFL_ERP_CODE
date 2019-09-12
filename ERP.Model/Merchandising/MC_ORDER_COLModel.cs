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
    public class MC_ORDER_COLModel
    {
        public Int64? MC_ORDER_COL_ID { get; set; }
        public Int64? MC_ORDER_STYL_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public Int64? STYL_COL_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public Decimal? AVG_UNIT_PRICE { get; set; }
        public string COMBO_NO { get; set; }
        public string COLOR_REF { get; set; }
        public Int64? PACK_NO { get; set; }
        //public Int64? MC_ORDER_CNTRY_ID { get; set; }
        public Int64? HR_COUNTRY_ID { get; set; }
        public List<COMBO_NO_VmModel> COMBO_NO_LIST { get; set; }        
        public Int64? TOTAL_QTY { get; set; }
        public List<MC_ORDER_SIZEModel> itemsSize { get; set; }
        public string COLOR_NAME_EN { get; set; }





        public List<MC_ORDER_COLModel> getOrderColorDataForGmt(long pMC_ORDER_H_ID)
        {
            string sp = "pkg_common.get_Order_Style_dd_for_gmt";
            try
            {
                var obList = new List<MC_ORDER_COLModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID", Value = pMC_ORDER_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_ORDER_COLModel ob = new MC_ORDER_COLModel();
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

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