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
    public class KNT_YD_FDR_REPEATModel
    {
        public Int64 KNT_YD_FDR_REPEAT_ID { get; set; }
        public Int64 KNT_PLAN_H_ID { get; set; }
        public string SL_LIST { get; set; }
        public Int64 NO_OF_REPEAT { get; set; }

        public List<KNT_YD_FDR_REPEATModel> QueryData(Int64 pKNT_PLAN_H_ID)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_fdr_repeat_select";
            try
            {
                var obList = new List<KNT_YD_FDR_REPEATModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        KNT_YD_FDR_REPEATModel ob = new KNT_YD_FDR_REPEATModel();
                        ob.KNT_YD_FDR_REPEAT_ID = (dr["KNT_YD_FDR_REPEAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_FDR_REPEAT_ID"]);
                        ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                        ob.SL_LIST = (dr["SL_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_LIST"]);
                        ob.NO_OF_REPEAT = (dr["NO_OF_REPEAT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_REPEAT"]);
                        obList.Add(ob);
                    }
                }
                else
                {
                        KNT_YD_FDR_REPEATModel ob = new KNT_YD_FDR_REPEATModel();
                        ob.KNT_YD_FDR_REPEAT_ID = -1;
                        ob.KNT_PLAN_H_ID = pKNT_PLAN_H_ID;
                        ob.NO_OF_REPEAT = 0;
                        obList.Add(ob);
                } 
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YD_FDR_REPEATModel Select(long ID)
        {
            string sp = "Select_KNT_YD_FDR_REPEAT";
            try
            {
                var ob = new KNT_YD_FDR_REPEATModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_FDR_REPEAT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.KNT_YD_FDR_REPEAT_ID = (dr["KNT_YD_FDR_REPEAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_FDR_REPEAT_ID"]);
                        ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                        ob.SL_LIST = (dr["SL_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL_LIST"]);
                        ob.NO_OF_REPEAT = (dr["NO_OF_REPEAT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_REPEAT"]);
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