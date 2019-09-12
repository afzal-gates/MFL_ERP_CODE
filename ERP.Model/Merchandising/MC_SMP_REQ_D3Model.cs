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
    public class MC_SMP_REQ_D3Model
    {
        public Int64? MC_SMP_REQ_D3_ID { get; set; }
        public Int64? MC_SMP_REQ_D2_ID { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public Int64? LK_COL_TYPE_ID { get; set; }
        public Int64? RQD_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public string COLOR_SPEC { get; set; }


        public Int64? MC_ORDER_H_ID { get; set; }
        public Int64? MC_STYLE_D_FAB_ID { get; set; }
        public string FIN_DIA { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public string IS_CONTRAST { get; set; }
        public List<MC_COLORModel> STYLE_COLOR_LIST { get; set; }

     

        public List<MC_SMP_REQ_D3Model> SelectAll()
        {
            string sp = "Select_MC_SMP_REQ_D3";
            try
            {
                var obList = new List<MC_SMP_REQ_D3Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D3_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   MC_SMP_REQ_D3Model ob = new MC_SMP_REQ_D3Model();
                   ob.MC_SMP_REQ_D3_ID = (dr["MC_SMP_REQ_D3_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D3_ID"]);
                   ob.MC_SMP_REQ_D2_ID = (dr["MC_SMP_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D2_ID"]);
                   ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                   ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                   ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                   ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                   ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                  
                   obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_REQ_D3Model Select(long ID)
        {
            string sp = "Select_MC_SMP_REQ_D3";
            try
            {
                var ob = new MC_SMP_REQ_D3Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D3_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                   ob.MC_SMP_REQ_D3_ID = (dr["MC_SMP_REQ_D3_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D3_ID"]);
                   ob.MC_SMP_REQ_D2_ID = (dr["MC_SMP_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D2_ID"]);
                   ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                   ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                   ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                   ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                   ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SMP_REQ_D3Model> SampFabColorWiseQtyByStyle(Int64 pMC_STYLE_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_d3_select";
            try
            {
                var obColorList = new List<MC_COLORModel>();
                var obList = new List<MC_SMP_REQ_D3Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_ID", Value = pMC_STYLE_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_COLORModel ob = new MC_COLORModel();                    
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    
                    obColorList.Add(ob);
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_D3Model ob = new MC_SMP_REQ_D3Model();
                    ob.MC_SMP_REQ_D3_ID = (dr["MC_SMP_REQ_D3_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D3_ID"]);
                    //ob.MC_SMP_REQ_D2_ID = (dr["MC_SMP_REQ_D2_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D2_ID"]);
                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.LK_COL_TYPE_ID = (dr["LK_COL_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_COL_TYPE_ID"]);
                    if (dr["RQD_QTY"] != DBNull.Value)
                    { ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]); }
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COLOR_SPEC = (dr["COLOR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_SPEC"]);
                    ob.STYLE_COLOR_LIST = obColorList;

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