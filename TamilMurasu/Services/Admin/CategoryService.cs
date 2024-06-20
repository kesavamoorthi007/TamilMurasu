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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TamilMurasu.Services.Admin
{
    public class CategoryService : ICategoryService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public CategoryService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public string CategoryCRUD(Category Cy)
        {
            string msg = "";
            try
            {
                string StatementType = string.Empty;
                string svSQL = "";
                using (SqlConnection objConn = new SqlConnection(_connectionString))
                {
                    objConn.Open();
                    if (Cy.ID == null)
                    {
                        svSQL = "Insert into TMCategory_N (C_Name,C_NameEN,Title_Eng) VALUES (N'" + Cy.C_Name + "',N'" + Cy.C_NameEN + "',N'" + Cy.Title_Eng + "')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update TMCategory_N set C_Name = N'" + Cy.C_Name + "',C_NameEN = N'" + Cy.C_NameEN + "',Title_Eng = N'" + Cy.Title_Eng + "' WHERE TMCategory_N.C_Id ='" + Cy.ID + "'";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();
                    }
                    objConn.Close();
                }

              
            }
            catch (Exception ex)
            {
                msg = "Error Occurs, While inserting / updating Data";
                throw ex;
            }

            return msg;
        }
        public DataTable GetAllCategory()
        {
            string SvSql = string.Empty;
            SvSql = "select C_Id,C_Name,C_NameEN,Title_Eng from TMCategory_N ORDER BY C_Id DESC ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public DataTable GetEditCategory(string id)
        {
            string SvSql = string.Empty;
            SvSql = "select C_Id,C_Name,C_NameEN,Title_Eng from TMCategory_N  Where TMCategory_N.C_Id='" + id + "' ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
    }
}
