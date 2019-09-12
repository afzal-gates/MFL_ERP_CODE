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


    public class GMT_PLN_CAPACITY_DSHBRD_H
    {

        public Int64 PROD_UNIT_ID { get; set; }
        public string title { get; set; }

        public string PLAN_PROD_QTY_LST { get; set; }
        public string BOOKED_PROD_QTY_LST { get; set; }
        public string PLAN_PROD_PCS_LST { get; set; }
        public string BOOKED_PROD_PCS_LST { get; set; }
        public string TTL_TRG_FOB_LST { get; set; }
        public string TTL_BKD_FOB_LST { get; set; }


        private List<GMT_PLN_CAPACITY_DSHBRD_D> _series = null;
        public List<GMT_PLN_CAPACITY_DSHBRD_D> series { 
            get {
                if (_series == null)
                {
                    _series = new List<GMT_PLN_CAPACITY_DSHBRD_D>();
                }
                return _series;
            }
            set {
                _series = value;
            }
        }

        public List<GMT_PLN_ODR_LIST_CAP_WKModel> order_lists { get; set; }


        public List<GMT_PLN_CAPACITY_DSHBRD_H> getGmtPlanDashboardData(
            
           Int64 pGMT_PROD_PLN_CLNDR_ID_MN,
           Int64? pGMT_PROD_PLN_CLNDR_ID_WK
        )
        {
            string sp = "pkg_planning_common.gmt_plan_capacity_bk_dashboard";
            try
            {
                var obList = new List<GMT_PLN_CAPACITY_DSHBRD_H>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID_WK", Value = pGMT_PROD_PLN_CLNDR_ID_WK},
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID_MN", Value = pGMT_PROD_PLN_CLNDR_ID_MN},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_CAPACITY_DSHBRD_H ob = new GMT_PLN_CAPACITY_DSHBRD_H();
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.title = (dr["OFFICE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_SNAME"]);

                    ob.PLAN_PROD_QTY_LST = (dr["PLAN_PROD_QTY_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["PLAN_PROD_QTY_LST"]);
                    ob.BOOKED_PROD_QTY_LST = (dr["BOOKED_PROD_QTY_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["BOOKED_PROD_QTY_LST"]);
                    ob.PLAN_PROD_PCS_LST = (dr["PLAN_PROD_PCS_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["PLAN_PROD_PCS_LST"]);
                    ob.BOOKED_PROD_PCS_LST = (dr["BOOKED_PROD_PCS_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["BOOKED_PROD_PCS_LST"]);
                    ob.TTL_TRG_FOB_LST = (dr["TTL_TRG_FOB_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["TTL_TRG_FOB_LST"]);
                    ob.TTL_BKD_FOB_LST = (dr["TTL_BKD_FOB_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["TTL_BKD_FOB_LST"]);

                    ob.LOAD_QTY_LST = (dr["LOAD_QTY_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["LOAD_QTY_LST"]);

                    ob.order_lists = new GMT_PLN_ODR_LIST_CAP_WKModel().Query(pGMT_PROD_PLN_CLNDR_ID_MN, pGMT_PROD_PLN_CLNDR_ID_WK, ob.PROD_UNIT_ID);

                    ob.series = new List<GMT_PLN_CAPACITY_DSHBRD_D>() { 
                        
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Target Pcs", color ="#5B297A", data = ob.PLAN_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList()},
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Boooked Pcs", color ="#9162CB", data = ob.BOOKED_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList()},
                        


                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Target SAH", color ="#226667", data = ob.PLAN_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList()},
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Booked SAH", color ="#3EBBAB", data = ob.BOOKED_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList()},
                        
                        
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Target FOB", color ="#7E722A", data = ob.TTL_TRG_FOB_LST.Split(',').Select(Int64.Parse).ToList()},
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Booked FOB", color ="#BDCC66", data = ob.TTL_BKD_FOB_LST.Split(',').Select(Int64.Parse).ToList()},

                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Load Pcs", color ="#9162CB", data = ob.LOAD_QTY_LST.Split(',').Select(Int64.Parse).ToList()},
                    };

                    ob.TTL_PLN_PCS = ob.PLAN_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList().Sum();
                    ob.TTL_PLN_SAH = ob.PLAN_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList().Sum();
                    ob.TTL_PLN_FOB = ob.TTL_TRG_FOB_LST.Split(',').Select(Int64.Parse).ToList().Sum();

                    ob.TTL_BKD_PCS = ob.BOOKED_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList().Sum();
                    ob.TTL_BKD_SAH = ob.BOOKED_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList().Sum();
                    ob.TTL_BKD_FOB = ob.TTL_BKD_FOB_LST.Split(',').Select(Int64.Parse).ToList().Sum();

                    ob.TTL_LOAD_QTY = ob.LOAD_QTY_LST.Split(',').Select(Int64.Parse).ToList().Sum();

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GMT_PLN_CAPACITY_DSHBRD_H> getGmtPlanCapcityDshBrdDataWkShip(DateTime pSHIP_DT)
        {
            string sp = "pkg_planning_common.gmt_plan_capacity_bk_dashboard";
            try
            {
                var obList = new List<GMT_PLN_CAPACITY_DSHBRD_H>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pSHIP_DT", Value = pSHIP_DT.Date},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_CAPACITY_DSHBRD_H ob = new GMT_PLN_CAPACITY_DSHBRD_H();
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.title = (dr["OFFICE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_SNAME"]);

                    ob.PLAN_PROD_QTY_LST = (dr["PLAN_PROD_QTY_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["PLAN_PROD_QTY_LST"]);
                    ob.BOOKED_PROD_QTY_LST = (dr["BOOKED_PROD_QTY_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["BOOKED_PROD_QTY_LST"]);
                    ob.PLAN_PROD_PCS_LST = (dr["PLAN_PROD_PCS_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["PLAN_PROD_PCS_LST"]);
                    ob.BOOKED_PROD_PCS_LST = (dr["BOOKED_PROD_PCS_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["BOOKED_PROD_PCS_LST"]);
                    ob.TTL_TRG_FOB_LST = (dr["TTL_TRG_FOB_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["TTL_TRG_FOB_LST"]);
                    ob.TTL_BKD_FOB_LST = (dr["TTL_BKD_FOB_LST"] == DBNull.Value) ? "0" : Convert.ToString(dr["TTL_BKD_FOB_LST"]);
                    ob.series = new List<GMT_PLN_CAPACITY_DSHBRD_D>() { 
                        
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Target Pcs", color ="#5B297A", data = ob.PLAN_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList()},
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Boooked Pcs", color ="#9162CB", data = ob.BOOKED_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList()},

                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Target SAH", color ="#226667", data = ob.PLAN_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList()},
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Booked SAH", color ="#3EBBAB", data = ob.BOOKED_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList()},
                        
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Target FOB", color ="#7E722A", data = ob.TTL_TRG_FOB_LST.Split(',').Select(Int64.Parse).ToList()},
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "Booked FOB", color ="#BDCC66", data = ob.TTL_BKD_FOB_LST.Split(',').Select(Int64.Parse).ToList()}
                    };
                    ob.TTL_PLN_PCS = ob.PLAN_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList().Sum();
                    ob.TTL_PLN_SAH = ob.PLAN_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList().Sum();
                    ob.TTL_PLN_FOB = ob.TTL_TRG_FOB_LST.Split(',').Select(Int64.Parse).ToList().Sum();

                    ob.TTL_BKD_PCS = ob.BOOKED_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList().Sum();
                    ob.TTL_BKD_SAH = ob.BOOKED_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList().Sum();
                    ob.TTL_BKD_FOB = ob.TTL_BKD_FOB_LST.Split(',').Select(Int64.Parse).ToList().Sum();


                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<GMT_PLN_CAPACITY_DSHBRD_H> getGmtPlanDashboardData_line_chrt(
            Int64 pGMT_PROD_PLN_CLNDR_ID_MN
          )
        {
            string sp = "pkg_planning_common.gmt_plan_capacity_bk_dashboard";
            try
            {
                var obList = new List<GMT_PLN_CAPACITY_DSHBRD_H>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pGMT_PROD_PLN_CLNDR_ID_MN", Value = pGMT_PROD_PLN_CLNDR_ID_MN},
                     new CommandParameter() {ParameterName = "pOption", Value = 3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_PLN_CAPACITY_DSHBRD_H ob = new GMT_PLN_CAPACITY_DSHBRD_H();
                    ob.PROD_UNIT_ID = (dr["PROD_UNIT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PROD_UNIT_ID"]);
                    ob.WK_CODE_LIST = new List<string>();
                    ob.WK_CODE_LIST = Convert.ToString(dr["WK_CODE_LIST"]).Split(',').ToList();
                    ob.BOOKED_PROD_PCS_LST = (dr["BK_PROD_PCS_LIST"] == DBNull.Value) ? "0" : Convert.ToString(dr["BK_PROD_PCS_LIST"]);
                    ob.BOOKED_PROD_QTY_LST = (dr["BK_SAH_LIST"] == DBNull.Value) ? "0" : Convert.ToString(dr["BK_SAH_LIST"]);
                    ob.TTL_BKD_FOB_LST = (dr["BK_FOB_LIST"] == DBNull.Value) ? "0" : Convert.ToString(dr["BK_FOB_LIST"]);

                    ob.series = new List<GMT_PLN_CAPACITY_DSHBRD_D>() { 
                        
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "PCS", color ="#5B297A", data = ob.BOOKED_PROD_PCS_LST.Split(',').Select(Int64.Parse).ToList()},
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "SAH", color ="#226667", data = ob.BOOKED_PROD_QTY_LST.Split(',').Select(Int64.Parse).ToList()},
                        new GMT_PLN_CAPACITY_DSHBRD_D() {name = "FOB", color ="#7E722A", data = ob.TTL_BKD_FOB_LST.Split(',').Select(Int64.Parse).ToList()},
                      
                    };

                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<string> WK_CODE_LIST { get; set; }

        public long TTL_PLN_PCS { get; set; }

        public long TTL_PLN_SAH { get; set; }

        public long TTL_PLN_FOB { get; set; }

        public long TTL_BKD_PCS { get; set; }

        public long TTL_BKD_SAH { get; set; }

        public long TTL_BKD_FOB { get; set; }

        public string LOAD_QTY_LST { get; set; }

        public long TTL_LOAD_QTY { get; set; }
    }
    public class GMT_PLN_CAPACITY_DSHBRD_D
    {
        public string name { get; set; }
        public string color { get; set; }
        private List<Int64> _data = null;
        public List<Int64> data {
            get {
                if (_data == null)
                {
                    _data = new List<long>();
                }
                return _data;
            }
            set {
                _data = value;
            }
        }
    }


}