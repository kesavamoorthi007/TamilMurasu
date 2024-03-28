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
    public class NewImageService : INewImageService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public NewImageService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
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

        public string NewImageCRUD(NewImage cy)
        {
            throw new NotImplementedException();
        }
    }
}
