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
    public class MC_COLOR_GRPModel
    {
        public Int64 MC_COLOR_GRP_ID { get; set; }
        public string COLOR_GRP_CODE { get; set; }
        public string COLOR_GRP_NAME_EN { get; set; }
        public string COLOR_GRP_NAME_BN { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public List<MC_COLOR_GRPModel> SelectAll()
        {
            string sp = "pkg_merchandising.mc_color_grp_select";
            try
            {
                var obList = new List<MC_COLOR_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_COLOR_GRP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_COLOR_GRPModel ob = new MC_COLOR_GRPModel();
                            ob.MC_COLOR_GRP_ID = (dr["MC_COLOR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_GRP_ID"]);
                            ob.COLOR_GRP_CODE = (dr["COLOR_GRP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_CODE"]);
                            ob.COLOR_GRP_NAME_EN = (dr["COLOR_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_EN"]);
                            ob.COLOR_GRP_NAME_BN = (dr["COLOR_GRP_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME_BN"]);
                            ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
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