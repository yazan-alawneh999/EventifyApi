using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient; // Or any other provider namespace.
using LearningHub.Core.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;


namespace LearningHub.Infra.Common
{
    public class DbContext : IDbContext
    {
    
        private readonly IConfiguration _configuration;


        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        public DbConnection DbConnection => new OracleConnection(_configuration.GetConnectionString("DatabaseConnection"));
       
    }
}