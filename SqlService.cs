using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi
{
    public class SqlService
    {
        private SqlCommand cmd;
        public SqlService(string procName, SqlConnection connection)
        {
            cmd = new SqlCommand(procName, connection);
        }
        private JsonResult _Execute(SqlConnection connection)
        {
            JsonResult result;
            connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
                result = new JsonResult("SUCCESS");
            }
            catch (Exception ex)
            {
                result = new JsonResult(ex);
            }
            connection.Close();
            return result;
        }
        private JsonResult _ExecuteQuery(SqlConnection connection)
        {
            JsonResult result;
            connection.Open();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();
                result = new JsonResult(table);
            }
            catch (Exception ex)
            {
                result = new JsonResult(ex);
            }
            connection.Close();
            return result;
        }
        public JsonResult Execute(SqlParam[] parameters, bool isQuery, SqlConnection connection)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                SqlParameter p = new SqlParameter();
                p.ParameterName = parameters[i].Name;
                p.SqlDbType = parameters[i].Type;
                p.Value = parameters[i].Value;
                cmd.Parameters.Add(p);
            }
            cmd.CommandType = CommandType.StoredProcedure;

            JsonResult result = null;
            if (isQuery) { result = _ExecuteQuery(connection); }
            if (!isQuery) { result = _Execute(connection); }
            return result;
        }

        public SqlParam NewParam(string name, SqlDbType type, dynamic value)
        {
            SqlParam p = new SqlParam();
            p.Name = name;
            p.Type = type;
            p.Value = value;
            return p;
        }
    }

    public class SqlParam
    {
        public string Name { get; set; }
        public SqlDbType Type { get; set; }
        public dynamic Value { get; set; }
    }
}
