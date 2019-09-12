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
    public class GMT_MAP_GARM_PARTModel
    {
        public Int64? GMT_MAP_GARM_PART_ID { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }
        public Int64? RF_GARM_PART_ID { get; set; }
        public string IS_ACTIVE { get; set; }

        public Int64? GMT_MAP_ITM_OPR_SPEC_ID { get; set; }
        public long? GMT_PART_OPR_SPEC_ID { get; set; }
        public string PART_OPR_SPEC { get; set; }
        public string GARM_PART_NAME { get; set; }
        public string GMT_MAP_GARM_PART_XML { get; set; }
        
        
        public string BatchSave()
        {
            const string sp = "pkg_planning_common.gmt_map_garm_part_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MAP_GARM_PART_ID", Value = ob.GMT_MAP_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pRF_GARM_PART_ID", Value = ob.RF_GARM_PART_ID},
                     new CommandParameter() {ParameterName = "pGMT_MAP_GARM_PART_XML", Value = ob.GMT_MAP_GARM_PART_XML},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<GMT_MAP_GARM_PART_VM> GetCatItmWiseItmOprMapList(int pINV_ITEM_CAT_ID, Int64 pMC_STYLE_D_ITEM_ID)
        {
            string sp = "pkg_planning_common.gmt_map_garm_part_select";
            try
            {
                var obList = new List<GMT_MAP_GARM_PARTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_D_ITEM_ID", Value = pMC_STYLE_D_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_MAP_GARM_PARTModel ob = new GMT_MAP_GARM_PARTModel();
                    ob.GMT_MAP_GARM_PART_ID = (dr["GMT_MAP_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MAP_GARM_PART_ID"]);

                    if (dr["INV_ITEM_CAT_ID"] != DBNull.Value)
                        ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    if (dr["RF_GARM_PART_ID"] != DBNull.Value)
                        ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);

                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);

                    ob.GMT_MAP_ITM_OPR_SPEC_ID = (dr["GMT_MAP_ITM_OPR_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MAP_ITM_OPR_SPEC_ID"]);
                    ob.GMT_PART_OPR_SPEC_ID = (dr["GMT_PART_OPR_SPEC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_PART_OPR_SPEC_ID"]);
                    ob.PART_OPR_SPEC = (dr["PART_OPR_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PART_OPR_SPEC"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACTIVE"]);

                    obList.Add(ob);
                }

                var obPartList = new List<GMT_MAP_GARM_PART_VM>();

                var partList = obList.Distinct(new ProductComparare(a => a.RF_GARM_PART_ID));
                foreach (var x in partList)
                {
                    GMT_MAP_GARM_PART_VM ob1 = new GMT_MAP_GARM_PART_VM();

                    ob1.RF_GARM_PART_ID = x.RF_GARM_PART_ID;
                    ob1.GARM_PART_NAME = x.GARM_PART_NAME;

                    var partOprList = obList.Where(p => p.RF_GARM_PART_ID == x.RF_GARM_PART_ID).ToList();
                    ob1.PART_OPR_COUNT = partOprList.Count();
                    ob1.itemsPartOprList = partOprList;

                    //ob1.GMT_PART_OPR_SPEC_ID = obList.Where(p => p.IS_ACTIVE == "Y").ToList()[0].GMT_PART_OPR_SPEC_ID;
                    foreach(var i in partOprList)
                    {
                        if (i.IS_ACTIVE == "Y")
                            ob1.GMT_PART_OPR_SPEC_ID = i.GMT_PART_OPR_SPEC_ID;
                    }
                    
                    obPartList.Add(ob1);
                }


                return obPartList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        class ProductComparare : IEqualityComparer<GMT_MAP_GARM_PARTModel>
        {
            private Func<GMT_MAP_GARM_PARTModel, object> _funcDistinct;
            public ProductComparare(Func<GMT_MAP_GARM_PARTModel, object> funcDistinct)
            {
                this._funcDistinct = funcDistinct;
            }

            public bool Equals(GMT_MAP_GARM_PARTModel x, GMT_MAP_GARM_PARTModel y)
            {
                return _funcDistinct(x).Equals(_funcDistinct(y));
            }

            public int GetHashCode(GMT_MAP_GARM_PARTModel obj)
            {
                return this._funcDistinct(obj).GetHashCode();
            }
        }

        public List<GMT_MAP_GARM_PARTModel> GetCategGmtPartMapList(int pINV_ITEM_CAT_ID)
        {
            string sp = "pkg_planning_common.gmt_map_garm_part_select";
            try
            {
                var obList = new List<GMT_MAP_GARM_PARTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = pINV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GMT_MAP_GARM_PARTModel ob = new GMT_MAP_GARM_PARTModel();
                    ob.GMT_MAP_GARM_PART_ID = (dr["GMT_MAP_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MAP_GARM_PART_ID"]);

                    if (dr["INV_ITEM_CAT_ID"] != DBNull.Value)
                        ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    if (dr["RF_GARM_PART_ID"] != DBNull.Value)
                        ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);

                    ob.GARM_PART_NAME = (dr["GARM_PART_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GARM_PART_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACTIVE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GMT_MAP_GARM_PARTModel Select(long ID)
        {
            string sp = "Select_GMT_MAP_GARM_PART";
            try
            {
                var ob = new GMT_MAP_GARM_PARTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pGMT_MAP_GARM_PART_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.GMT_MAP_GARM_PART_ID = (dr["GMT_MAP_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GMT_MAP_GARM_PART_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.RF_GARM_PART_ID = (dr["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_GARM_PART_ID"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }


    public class GMT_MAP_GARM_PART_VM
    {
        public Int64? RF_GARM_PART_ID { get; set; }
        public string GARM_PART_NAME { get; set; }
        public int PART_OPR_COUNT { get; set; }
        public Int64? GMT_PART_OPR_SPEC_ID { get; set; }

        private List<GMT_MAP_GARM_PARTModel> _itemsPartOprList = null;
        public List<GMT_MAP_GARM_PARTModel> itemsPartOprList
        {
            get
            {
                if (_itemsPartOprList == null)
                {
                    _itemsPartOprList = new List<GMT_MAP_GARM_PARTModel>();
                }
                return _itemsPartOprList;
            }
            set
            {
                _itemsPartOprList = value;
            }
        }

    }

}