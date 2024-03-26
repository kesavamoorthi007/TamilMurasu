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
    public class NewAlbumService : INewAlbumService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public NewAlbumService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }

        public string NewAlbumCRUD(NewAlbum cy)
        {
            throw new NotImplementedException();
        }
    }
}
