using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Learning_Dotnet.Data
{
    public class DataContextDapper
    {
        private readonly IDbConnection _dapperContext;

        public DataContextDapper(IConfiguration config) {
            _dapperContext = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        } 

        public IEnumerable<T> LoadData<T>(string sql) {
            return _dapperContext.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql) {
            return _dapperContext.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql) {
            return _dapperContext.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql) {
            return _dapperContext.Execute(sql);
        }
    }
}