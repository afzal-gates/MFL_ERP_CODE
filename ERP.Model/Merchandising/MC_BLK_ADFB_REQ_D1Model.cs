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
    public class MC_BLK_ADFB_REQ_D1Model
    {
        public Int64 MC_BLK_ADFB_REQ_D1_ID { get; set; }
        public Int64 MC_BLK_ADFB_REQ_D_ID { get; set; }
        public Int64 MC_CLCF_ORD_REQ_ID { get; set; }
        public Int64 RQD_PC_QTY { get; set; }

        public long RF_GARM_PART_ID { get; set; }
        public string MESUREMENT { get; set; }
        public string SIZE_CODE_LST { get; set; }


        public List<MC_BLK_ADFB_REQ_D1Model> GetAddFabBkingCollarCuffReq(Int64 pMC_STYLE_H_EXT_ID, Int64 pFAB_COLOR_ID, Int64 pMC_BLK_ADFB_REQ_D_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_h_select";
            try
            {
                var obList = new List<MC_BLK_ADFB_REQ_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = pFAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_ADFB_REQ_D_ID", Value = pMC_BLK_ADFB_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_ADFB_REQ_D1Model ob = new MC_BLK_ADFB_REQ_D1Model();
                    ob.MC_BLK_ADFB_REQ_D1_ID = (dr["MC_BLK_ADFB_REQ_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_ADFB_REQ_D1_ID"]);
                    ob.MC_BLK_ADFB_REQ_D_ID = (dr["MC_BLK_ADFB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_ADFB_REQ_D_ID"]);
                    ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);
                    ob.RQD_PC_QTY = (dr["RQD_PC_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PC_QTY"]);
                    
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    ob.SIZE_CODE_LST = (dr["SIZE_CODE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE_LST"]);

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