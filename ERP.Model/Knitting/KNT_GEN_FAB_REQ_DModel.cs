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
    public class KNT_GEN_FAB_REQ_DModel
    {
        public Int64 KNT_GEN_FAB_REQ_D_ID { get; set; }
        public Int64 KNT_GEN_FAB_REQ_H_ID { get; set; }        
        public Int64 FAB_COLOR_ID { get; set; }
        public Int64? MC_STYLE_D_FAB_ID { get; set; }
        public Int64 RF_FAB_TYPE_ID { get; set; }
        public Int64 RF_FIB_COMP_ID { get; set; }
        public string FIN_GSM { get; set; }
        public Int64? FIN_DIA { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public Int64? DIA_MOU_ID { get; set; }
        public Decimal RQD_FAB_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public string IS_SLUB { get; set; }
        public string IS_MELLANGE { get; set; }

        
        public string COLOR_NAME_EN { get; set; }
        public long? LK_COL_TYPE_ID { get; set; }
        public string FABRIC_DESC { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string FIN_DIA_N_DIA_TYPE { get; set; }
        public string RQD_FAB_QTY_NAME { get; set; }


        public List<KNT_GEN_FAB_REQ_DModel> GetFabReqDtl(Int64 pKNT_GEN_FAB_REQ_H_ID)
        {
            string sp = "PKG_FAB_PROD_ORDER.knt_gen_fab_req_h_select";
            try
            {
                var obList = new List<KNT_GEN_FAB_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_GEN_FAB_REQ_H_ID", Value =pKNT_GEN_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_GEN_FAB_REQ_DModel ob = new KNT_GEN_FAB_REQ_DModel();
                    ob.KNT_GEN_FAB_REQ_D_ID = (dr["KNT_GEN_FAB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_GEN_FAB_REQ_D_ID"]);
                    ob.KNT_GEN_FAB_REQ_H_ID = (dr["KNT_GEN_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_GEN_FAB_REQ_H_ID"]);                                       

                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);

                    if (dr["LK_COL_TYPE_ID"] != DBNull.Value)
                        ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);

                    if (dr["MC_STYLE_D_FAB_ID"] != DBNull.Value)
                        ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);

                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);

                    if (dr["FIN_DIA"] != DBNull.Value)
                    {
                        ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);

                        if (dr["LK_DIA_TYPE_ID"] != DBNull.Value)
                            ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);

                        if (dr["DIA_MOU_ID"] != DBNull.Value)
                            ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    }

                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);

                    ob.IS_SLUB = (dr["IS_SLUB"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SLUB"]);
                    ob.IS_MELLANGE = (dr["IS_MELLANGE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_MELLANGE"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.FABRIC_DESC = (dr["FABRIC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_DESC"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.FIN_DIA_N_DIA_TYPE = (dr["FIN_DIA_N_DIA_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA_N_DIA_TYPE"]);
                    ob.RQD_FAB_QTY_NAME = (dr["RQD_FAB_QTY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_FAB_QTY_NAME"]);                    
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_GEN_FAB_REQ_DModel Select(long ID)
        {
            string sp = "Select_KNT_GEN_FAB_REQ_D";
            try
            {
                var ob = new KNT_GEN_FAB_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_GEN_FAB_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_GEN_FAB_REQ_D_ID = (dr["KNT_GEN_FAB_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_GEN_FAB_REQ_D_ID"]);
                    ob.KNT_GEN_FAB_REQ_H_ID = (dr["KNT_GEN_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_GEN_FAB_REQ_H_ID"]);
                    
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.MC_STYLE_D_FAB_ID = (dr["MC_STYLE_D_FAB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_D_FAB_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.FIN_GSM = (dr["FIN_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.DIA_MOU_ID = (dr["DIA_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_MOU_ID"]);
                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FAB_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);                    
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