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
    public class MC_SMP_REQ_D_ASORTModel
    {
        public Int64 MC_SMP_REQ_D_ASORT_ID { get; set; }
        public Int64 MC_SMP_REQ_D_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public Int64 RQD_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string REMARKS { get; set; }
        public string STYLE_NO { get; set; }

       

       

        public List<MC_SMP_REQ_D_ASORTModel> SelectAll()
        {
            string sp = "Select_MC_SMP_REQ_D_ASORT";
            try
            {
                var obList = new List<MC_SMP_REQ_D_ASORTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ASORT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_SMP_REQ_D_ASORTModel ob = new MC_SMP_REQ_D_ASORTModel();
                            ob.MC_SMP_REQ_D_ASORT_ID = (dr["MC_SMP_REQ_D_ASORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ASORT_ID"]);
                            ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                            ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                            ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                            ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                            
                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MC_SMP_REQ_D_ASORTModel Select(long ID)
        {
            string sp = "Select_MC_SMP_REQ_D_ASORT";
            try
            {
                var ob = new MC_SMP_REQ_D_ASORTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_D_ASORT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.MC_SMP_REQ_D_ASORT_ID = (dr["MC_SMP_REQ_D_ASORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ASORT_ID"]);
                        ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                        ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                        ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                        ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        
               }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MC_SMP_REQ_D_ASORTModel> GetAssortList(Int64 pMC_SMP_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_smp_req_d_asort_select";
            try
            {
                var obList = new List<MC_SMP_REQ_D_ASORTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_SMP_REQ_H_ID", Value = pMC_SMP_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_SMP_REQ_D_ASORTModel ob = new MC_SMP_REQ_D_ASORTModel();
                    ob.MC_SMP_REQ_D_ASORT_ID = (dr["MC_SMP_REQ_D_ASORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ASORT_ID"]);
                    ob.MC_SMP_REQ_D_ID = (dr["MC_SMP_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_SMP_REQ_D_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    
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