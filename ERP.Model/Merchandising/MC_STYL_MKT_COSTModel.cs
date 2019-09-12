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
    public class MC_STYL_MKT_COSTModel
    {
        
        public Int64 MC_STYL_MKT_COST_ID { get; set; }
        public Int64 MC_STYLE_H_EXT_ID { get; set; }
        public Int64 MC_COST_HEAD_ID { get; set; }
        public string IS_PCT { get; set; }
        public Decimal PCT_COST { get; set; }
        public Decimal QTY_CONS { get; set; }
        public Int64 CONS_MOU_ID { get; set; }
        public Decimal RATE_CONS { get; set; }
        public Int64 PURCH_MOU_ID { get; set; }
        public Decimal HEAD_COST { get; set; }
        public string REMARKS { get; set; }
        public String COST_NAME_EN { get; set; }

        private List<MC_STYL_MKT_COSTModel> _items = null;
        public List<MC_STYL_MKT_COSTModel> items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<MC_STYL_MKT_COSTModel>();
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }


        public List<MC_STYL_MKT_COSTModel> FindMktCostHead(Int64 MC_STYLE_H_EXT_ID)
        {
            string sp = "PKG_COST_HEAD.mc_styl_mkt_cost_select";
            try
            {
                var obList = new List<MC_STYL_MKT_COSTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            MC_STYL_MKT_COSTModel ob = new MC_STYL_MKT_COSTModel();
                            ob.MC_STYL_MKT_COST_ID = (dr["MC_STYL_MKT_COST_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr["MC_STYL_MKT_COST_ID"]);
                            ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? MC_STYLE_H_EXT_ID : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                            ob.MC_COST_HEAD_ID = (dr["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COST_HEAD_ID"]);
                            ob.IS_PCT = (dr["IS_PCT"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_PCT"]);
                            ob.PCT_COST = (dr["PCT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_COST"]);                                                    
                            ob.RATE_CONS = (dr["RATE_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RATE_CONS"]);                           
                            ob.HEAD_COST = (dr["HEAD_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["HEAD_COST"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.COST_NAME_EN = (dr["COST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_NAME_EN"]);

                            ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);

                            if (ob.PARENT_ID == 3 && ob.MC_COST_HEAD_ID == 6)
                            {
                                ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 3 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                                ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value) ? 3 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                                ob.QTY_CONS = (dr["QTY_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_CONS"]);
                            }
                            else
                            {
                                ob.CONS_MOU_ID = (dr["CONS_MOU_ID"] == DBNull.Value) ? 5 : Convert.ToInt64(dr["CONS_MOU_ID"]);
                                ob.PURCH_MOU_ID = (dr["PURCH_MOU_ID"] == DBNull.Value)? 5 : Convert.ToInt64(dr["PURCH_MOU_ID"]);
                                ob.QTY_CONS = (dr["QTY_CONS"] == DBNull.Value) ? 1 : Convert.ToDecimal(dr["QTY_CONS"]);
                            }


                            var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                                {
                                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value =MC_STYLE_H_EXT_ID},
                                     new CommandParameter() {ParameterName = "pMC_COST_HEAD_ID", Value =ob.MC_COST_HEAD_ID},
                                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                                 }, sp);


                            foreach (DataRow dr1 in ds1.Tables[0].Rows)
                            {
                                MC_STYL_MKT_COSTModel ob1 = new MC_STYL_MKT_COSTModel();
                                ob1.MC_STYL_MKT_COST_ID = (dr1["MC_STYL_MKT_COST_ID"] == DBNull.Value) ? -1 : Convert.ToInt64(dr1["MC_STYL_MKT_COST_ID"]);
                                ob1.MC_STYLE_H_EXT_ID = (dr1["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? MC_STYLE_H_EXT_ID : Convert.ToInt64(dr1["MC_STYLE_H_EXT_ID"]);
                                ob1.MC_COST_HEAD_ID = (dr1["MC_COST_HEAD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_COST_HEAD_ID"]);
                                ob1.IS_PCT = (dr1["IS_PCT"] == DBNull.Value) ? "N" : Convert.ToString(dr1["IS_PCT"]);
                                ob1.PCT_COST = (dr1["PCT_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["PCT_COST"]);                                                               
                                ob1.RATE_CONS = (dr1["RATE_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["RATE_CONS"]);                               
                                ob1.HEAD_COST = (dr1["HEAD_COST"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["HEAD_COST"]);
                                ob1.REMARKS = (dr1["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["REMARKS"]);
                                ob1.COST_NAME_EN = (dr1["COST_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COST_NAME_EN"]);
                                ob1.PARENT_ID = (dr1["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["PARENT_ID"]);

                                if (ob1.PARENT_ID == 6)
                                {
                                    ob1.CONS_MOU_ID = (dr1["CONS_MOU_ID"] == DBNull.Value) ? 3 : Convert.ToInt64(dr1["CONS_MOU_ID"]);
                                    ob1.PURCH_MOU_ID = (dr1["PURCH_MOU_ID"] == DBNull.Value) ? 3 : Convert.ToInt64(dr1["PURCH_MOU_ID"]);
                                    ob1.QTY_CONS = (dr1["QTY_CONS"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr1["QTY_CONS"]); 
                                }
                                else
                                {
                                    ob1.CONS_MOU_ID = (dr1["CONS_MOU_ID"] == DBNull.Value) ? 5 : Convert.ToInt64(dr1["CONS_MOU_ID"]);
                                    ob1.PURCH_MOU_ID = (dr1["PURCH_MOU_ID"] == DBNull.Value) ? 5 : Convert.ToInt64(dr1["PURCH_MOU_ID"]);
                                    ob1.QTY_CONS = (dr1["QTY_CONS"] == DBNull.Value) ? 1 : Convert.ToDecimal(dr1["QTY_CONS"]); 
                                }


                                ob.items.Add(ob1);

                            }




                            obList.Add(ob);
               }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long PARENT_ID { get; set; }
    }
}