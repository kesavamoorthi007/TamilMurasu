using TamilMurasu.Interface;
using TamilMurasu.Interface.Admin;
using TamilMurasu.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using DocumentFormat.OpenXml.Bibliography;
using System.Linq;
using System.Data.SqlClient;


namespace TamilMurasu.Services.Admin
{
    public class LatestNewsService : ILatestNewsService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public LatestNewsService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }

        public DataTable GetAllLatestNews()
        {
            throw new NotImplementedException();
        }

        public string LatestNewsCRUD(LatestNews cy)
        {
            throw new NotImplementedException();
        }
    }
}
