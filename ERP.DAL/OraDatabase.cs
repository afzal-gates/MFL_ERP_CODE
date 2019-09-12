using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Data;

namespace ERP.Model
{
    public class OraDatabase
    {

        public DataSet ExecuteStoredProcedure(List<CommandParameter> commandParameters, String spName)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var cn = new Oracle.DataAccess.Client.OracleConnection(constr);

            try
            {
                var cm = new Oracle.DataAccess.Client.OracleCommand(spName, cn)
                {
                    BindByName = true,
                    CommandType = CommandType.StoredProcedure
                };

                cn.Open();
                Oracle.DataAccess.Client.OracleCommandBuilder.DeriveParameters(cm);
                cn.Close();

                foreach (CommandParameter commandParameter in commandParameters)
                {
                    if (cm.Parameters.Contains(commandParameter.ParameterName))
                    {
                        cm.Parameters[commandParameter.ParameterName].Value = commandParameter.Value;
                    }
                    else
                    {
                        throw new Exception("Store Procedure does not contain parameter : " + commandParameter.ParameterName);
                    }
                }

                var ds = new DataSet();
                var adap = new Oracle.DataAccess.Client.OracleDataAdapter(cm);
                adap.Fill(ds);

                var dt = new DataTable("OUTPARAM");
                dt.Columns.Add("KEY");
                dt.Columns.Add("VALUE");

                foreach (Oracle.DataAccess.Client.OracleParameter op in cm.Parameters)
                {
                    if (op.OracleDbType != OracleDbType.RefCursor && (op.Direction == ParameterDirection.InputOutput || op.Direction == ParameterDirection.Output))
                    {
                        DataRow dr = dt.NewRow();
                        dr["KEY"] = op.ParameterName;
                        dr["VALUE"] = op.Value;
                        dt.Rows.Add(dr);
                    }
                }

                ds.Tables.Add(dt);
                cm.Dispose();
                adap.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cn.Close();
            }
        }

        public DataSet ExecuteSQLStatement(String SQL)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var cn = new Oracle.DataAccess.Client.OracleConnection(constr);

            try
            {
                var cm = new Oracle.DataAccess.Client.OracleCommand(SQL, cn);

                cn.Open();
                cm.ExecuteReader();
                cn.Close();

                var ds = new DataSet();
                var adap = new Oracle.DataAccess.Client.OracleDataAdapter(cm);
                adap.Fill(ds);
                cm.Dispose();
                adap.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cn.Close();
            }
        }


    }

    //****Helper Class**********************************
    public class CommandParameter
    {
        public String ParameterName { get; set; }
        public Object Value { get; set; }
        public ParameterDirection Direction { get; set; }

        public CommandParameter()
        {
            Direction = ParameterDirection.Input;
        }
    }

}
