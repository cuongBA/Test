using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Test.Common;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using Elasticsearch.Net;
using Nest;
using System.Reflection;
namespace Test.DataAccess
{
    public class LocationDataAccess
    {
        private readonly string ConnectionString;
        private readonly SqlConnection con;
        public LocationDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
            con = new SqlConnection(ConnectionString);
        }

        public SqlConnection connect
        {
            get { return con; }
        }

        public string GetBulkStringForImport(SqlConnection con, string commandText, object operation)
        {
            StringBuilder builder = new StringBuilder();
            int x = 0;
            using (con)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = commandText,
                    CommandType = CommandType.Text
                };
                try
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        
                        var cols = new List<string>();
                        for (var i = 0; i < rdr.FieldCount; i++) cols.Add(rdr.GetName(i));
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        foreach (var col in cols) row.Add(col, rdr[col]);

                        builder.AppendLine(JsonConvert.SerializeObject(operation));
                        builder.AppendLine(JsonConvert.SerializeObject(row));                        
                    }
                }
                catch (Exception ex)
                {
                    
                    throw (ex);
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
                return builder.ToString();
            }
        }

        public BulkRequest GetBulkRequest(SqlConnection con, string commandText, object operation)
        {


            var x = operation.GetType().GetProperty("index").GetValue(operation);
            var y = x.GetType().GetProperty("_index").GetValue(x);
            var z = x.GetType().GetProperty("_type").GetValue(x);
            var request = new BulkRequest((string)y);
            request.TypeQueryString = (string)z;
            var operationList = new List<IBulkOperation>();
            object T = operation.GetType();
            using (con)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandText = commandText,
                    CommandType = CommandType.Text
                };
                try
                {
                    if (con.State == ConnectionState.Closed) con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var cols = new List<string>();
                        for (var i = 0; i < rdr.FieldCount; i++) cols.Add(rdr.GetName(i));
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        foreach (var col in cols) row.Add(col, rdr[col]);
                        var op = new BulkIndexOperation<object>(row);
                        op.Id = row["PoiID"].ToString();
                        operationList.Add(op);                        
                    }
                }
                catch (Exception ex)
                {

                    throw (ex);
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
                request.Operations = operationList;             
                return request;
            }
        }



    }
}
