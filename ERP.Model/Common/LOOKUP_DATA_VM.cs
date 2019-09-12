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
    public class LOOKUP_DATA_VM
    {
        public Int64 LOOKUP_DATA_ID { get; set; }
        public string LK_DATA_NAME_EN { get; set; }



        public List<LOOKUP_DATA_VM> GetSpinProcesListByFibTyp(long? LK_FIB_TYPE_ID)
        {
            string sp = "pkg_common.GetSpinProcesListByFibTyp";
            try
            {
                var obList = new List<LOOKUP_DATA_VM>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pLK_FIB_TYPE_ID", Value = LK_FIB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LOOKUP_DATA_VM ob = new LOOKUP_DATA_VM();
                    ob.LOOKUP_DATA_ID = (dr["LOOKUP_DATA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOOKUP_DATA_ID"]);
                    ob.LK_DATA_NAME_EN = (dr["LK_DATA_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DATA_NAME_EN"]);
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
