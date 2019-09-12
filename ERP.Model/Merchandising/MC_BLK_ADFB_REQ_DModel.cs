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
    public class MC_BLK_ADFB_REQ_DModel
    {
        public Int64 MC_BLK_ADFB_REQ_D_ID { get; set; }
        public Int64 MC_BLK_ADFB_REQ_H_ID { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public Int64? GMT_COLOR_ID { get; set; }
        public Int64 MC_STYLE_D_FAB_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public string FIN_DIA { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public Int64? DIA_MOU_ID { get; set; }
        public string FIN_GSM { get; set; }
        public Decimal RQD_GFAB_QTY { get; set; }
        public Decimal PCT_LOSS_QTY { get; set; }
        public Decimal FAB_PLOSS_QTY { get; set; }
        public Decimal RQD_FFAB_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public string IS_CONTRAST { get; set; }
        
        public string GMT_COLOR_NAME_EN { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FABRIC_DESC { get; set; }
        public string DIA_MOU_CODE { get; set; }
        public string FIN_DIA_TYPE { get; set; }
        public string FIN_DIA_N_DIA_TYPE { get; set; }
        public string QTY_MOU_CODE { get; set; }
        public string RQD_FFAB_QTY_NAME { get; set; }



        public List<MC_BLK_ADFB_REQ_DModel> GetAddFabBkingDtl(Int64 pMC_BLK_ADFB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_h_select";
            try
            {
                var obList = new List<MC_BLK_ADFB_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_ADFB_REQ_H_ID", Value = pMC_BLK_ADFB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_ADFB_REQ_DModel ob = new MC_BLK_ADFB_REQ_DModel();
                    ob.MC_BLK_ADFB_REQ_D_ID = (dr["MC_BLK_ADFB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_ADFB_REQ_D_ID"]);
                    ob.MC_BLK_ADFB_REQ_H_ID = (dr["MC_BLK_ADFB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_ADFB_REQ_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);

                    ob.IS_CONTRAST = (dr["IS_CONTRAST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CONTRAST"]);
                    if (dr["GMT_COLOR_ID"] != DBNull.Value)
                        ob.GMT_COLOR_ID = Convert.ToInt64(dr["GMT_COLOR_ID"]);
                    
                    ob.GMT_COLOR_NAME_EN = (dr["GMT_COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GMT_COLOR_NAME_EN"]);
                    
                    if (dr["FIN_DIA"] != DBNull.Value)
                    {
                        ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                        ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                        ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    }
                     
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);
                    ob.PCT_LOSS_QTY = (dr["PCT_LOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_LOSS_QTY"]);
                    ob.FAB_PLOSS_QTY = (dr["FAB_PLOSS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FAB_PLOSS_QTY"]);
                    ob.RQD_FFAB_QTY = (dr["RQD_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FFAB_QTY"]);
                    
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.DIA_MOU_CODE = (dr["DIA_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_MOU_CODE"]);
                    ob.FIN_DIA_TYPE = (dr["FIN_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_TYPE"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                    ob.RQD_FFAB_QTY_NAME = (dr["RQD_FFAB_QTY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_FFAB_QTY_NAME"]);
                    
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