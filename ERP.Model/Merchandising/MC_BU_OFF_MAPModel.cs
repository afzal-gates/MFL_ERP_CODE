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
    public class MC_BU_OFF_MAPModel
    {
        public Int64 MC_BU_OFF_MAP_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public Int64 MC_BUYER_OFF_ID { get; set; }
        public string IS_LOC_AGNT { get; set; }
        public string IS_ACTIVE { get; set; }

        public string CAN_BE_DEL { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save(List<MC_BU_OFF_MAPModel> obList)
        {
            const string sp = "pkg_merchandising.mc_bu_off_map_insert";
            string jsonStr = "";
            try
             {
                OraDatabase db = new OraDatabase();

                foreach (var ob in obList)
                {
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                             new CommandParameter() {ParameterName = "pMC_BU_OFF_MAP_ID", Value = ob.MC_BU_OFF_MAP_ID},
                             new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                             new CommandParameter() {ParameterName = "pMC_BUYER_OFF_ID", Value = ob.MC_BUYER_OFF_ID},
                             new CommandParameter() {ParameterName = "pIS_LOC_AGNT", Value = ob.IS_LOC_AGNT},
                             new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                             new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                             new CommandParameter() {ParameterName = "pCAN_BE_DEL", Value = ob.CAN_BE_DEL},
                             new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                             new CommandParameter() {ParameterName = "pOption", Value =1000},
                             new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                         }, sp);

                    foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                    {
                        jsonStr = "{" + Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"') + "}";
                    } 
                }

                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}