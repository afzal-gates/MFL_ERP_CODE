using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class SC_ROLE_MENUModel //: ISC_ROLE_MENUModel
    {
        public Int64 SC_ROLE_MENU_ID { get; set; }

        [Required]
        public Int64 SC_ROLE_ID { get; set; }
        public Int64 SC_MENU_ID { get; set; }
        public string IS_ACTIVE { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        //public Int64 LAST_UPDATE_LOGIN { get; set; }
        //public Int64 VERSION_NO { get; set; }


        public Int64 SC_SYSTEM_MODULE_ID { get; set; }
        public bool IsChecked { get; set; }
        public string Save()
        {
            const string sp = "PKG_SECURITY.sc_role_menu_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_ROLE_MENU_ID", Value = ob.SC_ROLE_MENU_ID},
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = ob.SC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pSC_MENU_ID", Value = ob.SC_MENU_ID},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }

        
    }
}