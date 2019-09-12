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
    public class GMT_MRKR_RATIO_GRPModel
    {
        public Int64 GMT_MRKR_RATIO_GRP_ID { get; set; }
        public string GRP_TEXT { get; set; }
        public Int64 SEQ { get; set; }
        public string IS_DEFAULT { get; set; }



        public List<GMT_MRKR_RATIO_GRPModel> GetMrkrRatioGrpList()
        {
            string sp = "pkg_cut_marker.gmt_mrkr_select";
            try
            {
                var obList = new List<GMT_MRKR_RATIO_GRPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_MRKR_RATIO_GRPModel ob = new GMT_MRKR_RATIO_GRPModel();
                    ob.GMT_MRKR_RATIO_GRP_ID = (dr["GMT_MRKR_RATIO_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_RATIO_GRP_ID"]);
                    ob.GRP_TEXT = (dr["GRP_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRP_TEXT"]);
                    ob.SEQ = (dr["SEQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEQ"]);
                    ob.IS_DEFAULT = (dr["IS_DEFAULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFAULT"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_MRKR_RATIO_GRPModel Select(long ID)
        {
            string sp = "Select_GMT_MRKR_RATIO_GRP";
            try
            {
                var ob = new GMT_MRKR_RATIO_GRPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MRKR_RATIO_GRP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_MRKR_RATIO_GRP_ID = (dr["GMT_MRKR_RATIO_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MRKR_RATIO_GRP_ID"]);
                    ob.GRP_TEXT = (dr["GRP_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GRP_TEXT"]);
                    ob.SEQ = (dr["SEQ"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SEQ"]);
                    ob.IS_DEFAULT = (dr["IS_DEFAULT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DEFAULT"]);

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