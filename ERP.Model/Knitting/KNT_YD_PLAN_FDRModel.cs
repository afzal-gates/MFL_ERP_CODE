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
    public class KNT_YD_PLAN_FDRModel
    {
        public Int64 KNT_YD_PLAN_FDR_ID { get; set; }
        public Int64 KNT_PLAN_H_ID { get; set; }
        public Int64 SL { get; set; }
        public Int64 SOLID_COLOR_ID { get; set; }
        public Int64 NO_OF_FDR { get; set; }
        public string REMARKS { get; set; }
        public string IS_SOLID_COL { get; set; }


        private List<YD_SOLID_COLOR_MODEL> _yd_colors = null;
        public List<YD_SOLID_COLOR_MODEL> yd_colors
        {
            get
            {
                if (_yd_colors == null)
                {
                    _yd_colors = new List<YD_SOLID_COLOR_MODEL>();
                }
                return _yd_colors;
            }
            set
            {
                _yd_colors = value;
            }
        }

        private List<YD_SOLID_COLOR_MODEL> _solid_colors = null;
        public List<YD_SOLID_COLOR_MODEL> solid_colors
        {
            get
            {
                if (_solid_colors == null)
                {
                    _solid_colors = new List<YD_SOLID_COLOR_MODEL>();
                }
                return _solid_colors;
            }
            set
            {
                _solid_colors = value;
            }
        }




        public List<KNT_YD_PLAN_FDRModel> queryData(Int64 pKNT_PLAN_H_ID, List<YD_SOLID_COLOR_MODEL> yd_col, List<YD_SOLID_COLOR_MODEL> solid_col)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_plan_fdr_select";
            try
            {
                var obList = new List<KNT_YD_PLAN_FDRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        KNT_YD_PLAN_FDRModel ob = new KNT_YD_PLAN_FDRModel();
                        ob.KNT_YD_PLAN_FDR_ID = (dr["KNT_YD_PLAN_FDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PLAN_FDR_ID"]);
                        ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                        ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);
                        ob.SOLID_COLOR_ID = (dr["SOLID_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SOLID_COLOR_ID"]);
                        ob.NO_OF_FDR = (dr["NO_OF_FDR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_FDR"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                        if (dr["LK_YFAB_PART_ID"] != DBNull.Value)
                        {
                            ob.LK_YFAB_PART_ID = Convert.ToInt64(dr["LK_YFAB_PART_ID"]);
                        }
                        ob.PCT_VALUE = (dr["PCT_VALUE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PCT_VALUE"]);

                        ob.YARN_PART_NAME = (dr["YARN_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_PART_NAME"]);
                        ob.IS_SOLID_COL = (dr["IS_SOLID_COL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SOLID_COL"]);

                        if (ob.IS_SOLID_COL == "Y")
                        {
                            ob.yd_colors = solid_col;
                            
                        }
                        else
                        {
                            ob.yd_colors = yd_col;
                        }
                        ob.solid_colors = solid_col;
                        
                        obList.Add(ob);
                    }
                }
                else
                {
                    KNT_YD_PLAN_FDRModel ob = new KNT_YD_PLAN_FDRModel();
                    ob.KNT_YD_PLAN_FDR_ID = -1;
                    ob.KNT_PLAN_H_ID = pKNT_PLAN_H_ID;

                    ob.solid_colors = solid_col;
                    ob.yd_colors = yd_col;

                    ob.SL = 1;
                    ob.IS_SOLID_COL = "N";
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_YD_PLAN_FDRModel> FeederArrangementData(Int64 pKNT_PLAN_H_ID)
        {
            string sp = "PKG_KNT_YD_PRG.knt_yd_plan_fdr_select";
            try
            {
                var obList = new List<KNT_YD_PLAN_FDRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YD_PLAN_FDRModel ob = new KNT_YD_PLAN_FDRModel();
                    ob.KNT_YD_PLAN_FDR_ID = (dr["KNT_YD_PLAN_FDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PLAN_FDR_ID"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);
                    ob.SOLID_COLOR_ID = (dr["SOLID_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SOLID_COLOR_ID"]);
                    ob.NO_OF_FDR = (dr["NO_OF_FDR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_FDR"]);

                    


                    ob.KNT_YD_FDR_REPEAT_ID_SPAN = (dr["KNT_YD_FDR_REPEAT_ID_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_FDR_REPEAT_ID_SPAN"]);
                    ob.KNT_YD_FDR_REPEAT_ID_SL = (dr["KNT_YD_FDR_REPEAT_ID_SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_FDR_REPEAT_ID_SL"]);

                    if (dr["NO_OF_REPEAT"] != DBNull.Value)
                    {
                        ob.NO_OF_REPEAT = Convert.ToInt64(dr["NO_OF_REPEAT"]);
                    }

                    if (dr["LK_YFAB_PART_ID"] != DBNull.Value)
                    {
                        ob.LK_YFAB_PART_ID = Convert.ToInt64(dr["LK_YFAB_PART_ID"]);
                    }

                    ob.YARN_PART_NAME = (dr["YARN_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_PART_NAME"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.YD_BATCH_NO_LST = (dr["YD_BATCH_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YD_BATCH_NO_LST"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.IS_SOLID_COL = (dr["IS_SOLID_COL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SOLID_COL"]);

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public KNT_YD_PLAN_FDRModel Select(long ID)
        {
            string sp = "Select_KNT_YD_PLAN_FDR";
            try
            {
                var ob = new KNT_YD_PLAN_FDRModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PLAN_FDR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YD_PLAN_FDR_ID = (dr["KNT_YD_PLAN_FDR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YD_PLAN_FDR_ID"]);
                    ob.KNT_PLAN_H_ID = (dr["KNT_PLAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_PLAN_H_ID"]);
                    ob.SL = (dr["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL"]);
                    ob.SOLID_COLOR_ID = (dr["SOLID_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SOLID_COLOR_ID"]);
                    ob.NO_OF_FDR = (dr["NO_OF_FDR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_FDR"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long KNT_YD_FDR_REPEAT_ID_SPAN { get; set; }
        public long KNT_YD_FDR_REPEAT_ID_SL { get; set; }
        public long? NO_OF_REPEAT { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string YD_BATCH_NO_LST { get; set; }
        public string YARN_PART_NAME { get; set; }
        public long? LK_YFAB_PART_ID { get; set; }

        public long PCT_VALUE { get; set; }
    }

 }