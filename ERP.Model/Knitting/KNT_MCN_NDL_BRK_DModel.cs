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
    public class KNT_MCN_NDL_BRK_DModel
    {
        public Int64 KNT_MCN_NDL_BRK_D_ID { get; set; }
        public Int64 KNT_MCN_NDL_BRK_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 KNT_MACHINE_ID { get; set; }
        public Int64 OPERATOR_ID { get; set; }
        public Int64 BRK_QTY_PCS { get; set; }
        public Decimal COST_PRICE { get; set; }


        public string KNT_MACHINE_NO { get; set; }
        public string MC_DIA { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string ITEM_NAME_EN { get; set; }


        

        public KNT_MCN_NDL_BRK_DModel Select(long ID)
        {
            string sp = "Select_KNT_MCN_NDL_BRK_D";
            try
            {
                var ob = new KNT_MCN_NDL_BRK_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_NDL_BRK_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_MCN_NDL_BRK_D_ID = (dr["KNT_MCN_NDL_BRK_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_NDL_BRK_D_ID"]);
                    ob.KNT_MCN_NDL_BRK_H_ID = (dr["KNT_MCN_NDL_BRK_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_NDL_BRK_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);


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