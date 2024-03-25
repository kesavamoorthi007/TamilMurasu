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
    public class NewsService : INewsService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public NewsService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }

        public DataTable GetAllNews()
        {
            string SvSql = string.Empty;
            SvSql = "select N_Id,TMCategory_N.C_Name,NT_Head,N_Description,Keyword from TMNews_N left outer join TMCategory_N ON TMCategory_N.C_Id=TMNews_N.C_Id";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }

        public DataTable GetCategory()
        {
            string SvSql = string.Empty;
            SvSql = "select C_Id,C_NameEN from TMCategory_N";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }

        public string NewsCRUD(News cy)
        {
            throw new NotImplementedException();
        }
    }
}
