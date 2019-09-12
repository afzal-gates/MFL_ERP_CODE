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
    public class MC_CLCF_ORD_REQModel
    {
        public Int64 MC_CLCF_ORD_REQ_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64 RF_GARM_PART_ID { get; set; }
        public string MC_SIZE_ID_LST { get; set; }
        public string MESUREMENT { get; set; }
        public Int64 RF_MOU_ID { get; set; }
        public Int64 RQD_PCS_QTY { get; set; }

        public Int64 FAB_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string GARM_PART_NAME { get; set; }
        public Int64? DISPLAY_ORDER { get; set; }
        public string SIZE_CODE_LST { get; set; }



        public RF_PAGERModel GetCollarCuffOrdReq(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, string pORDER_NO = null,
            Int64? pRF_FAB_PROD_CAT_ID = null, string pFIRSTDATE = null, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            string sp = "PKG_FAB_PROD_ORDER.CLCF_ORD_REQ_SELECT";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<MC_CLCF_ORD_REQ_VM>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = pRF_FAB_PROD_CAT_ID}, 
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                    new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},                     
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pFIRSTDATE", Value = pFIRSTDATE},
                     new CommandParameter() {ParameterName = "pORDER_NO", Value = pORDER_NO},
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},                    
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_CLCF_ORD_REQ_VM ob = new MC_CLCF_ORD_REQ_VM();
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.FAB_PROD_CAT_SNAME = (dr["FAB_PROD_CAT_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROD_CAT_SNAME"]);

                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SHIP_DT"]);

                    ob.RQD_PC_QTY = (dr["RQD_PC_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PC_QTY"]);
                    ob.SCO_ASSIGN_QTY = (dr["SCO_ASSIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCO_ASSIGN_QTY"]);
                    ob.INHOUSE_ASSIGN_QTY = (dr["INHOUSE_ASSIGN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INHOUSE_ASSIGN_QTY"]);

                    ob.INHOUSE_PROD_QTY = (dr["INHOUSE_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INHOUSE_PROD_QTY"]);
                    ob.SCO_PROD_QTY = (dr["SCO_PROD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCO_PROD_QTY"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MC_CLCF_ORD_REQModel> GetCollarCuffOrdReqDtl(Int64 pMC_FAB_PROD_ORD_H_ID, Int64 pMC_STYLE_H_ID)
        {
            string sp = "PKG_FAB_PROD_ORDER.CLCF_ORD_REQ_SELECT";
            try
            {
                var obList = new List<MC_CLCF_ORD_REQModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                    MC_CLCF_ORD_REQModel ob = new MC_CLCF_ORD_REQModel();
                    ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                    ob.MC_SIZE_ID_LST = (dr["MC_SIZE_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_ID_LST"]);

                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.SIZE_CODE_LST = (dr["SIZE_CODE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SIZE_CODE_LST"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);

                    ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                    //ob.RF_MOU_ID = (dr["RF_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MOU_ID"]);
                    ob.RQD_PCS_QTY = (dr["RQD_PCS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PCS_QTY"]);
                            
                    obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_CLCF_ORD_REQModel Select(long ID)
        {
            string sp = "Select_MC_CLCF_ORD_REQ";
            try
            {
                var ob = new MC_CLCF_ORD_REQModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_CLCF_ORD_REQ_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_CLCF_ORD_REQ_ID = (dr["MC_CLCF_ORD_REQ_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_CLCF_ORD_REQ_ID"]);
                        ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                        ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                        ob.MC_SIZE_ID_LST = (dr["MC_SIZE_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_SIZE_ID_LST"]);
                        ob.MESUREMENT = (dr["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MESUREMENT"]);
                        ob.RF_MOU_ID = (dr["RF_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_MOU_ID"]);
                        ob.RQD_PCS_QTY = (dr["RQD_PCS_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_PCS_QTY"]);
                       

               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



    public class MC_CLCF_ORD_REQ_VM
    {
        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64? RF_FAB_PROD_CAT_ID { get; set; }
        public string FAB_PROD_CAT_SNAME { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public Int64? MC_STYLE_H_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_H_ID_LST { get; set; }        
        public string ORDER_NO { get; set; }
        public DateTime? SHIP_DT { get; set; }
        public long? RQD_PC_QTY { get; set; }
        public long? SCO_ASSIGN_QTY { get; set; }
        public long? INHOUSE_ASSIGN_QTY { get; set; }
        public long? MC_BYR_ACC_GRP_ID { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public long? INHOUSE_PROD_QTY { get; set; }
        public long? SCO_PROD_QTY { get; set; }
    }

}