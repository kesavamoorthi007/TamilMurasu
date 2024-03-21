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

                //if (ss.ID == null)
                //{
                //    svSQL = " SELECT Count(CITYNAME) as cnt FROM CITYMASTER WHERE CITYNAME = LTRIM(RTRIM('" + ss.Cit + "')) ";
                //    if (datatrans.GetDataId(svSQL) > 0)
                //    {
                //        msg = "City Already Existed";
                //        return msg;
                //    }
                //}

                using (SqlConnection objConn = new SqlConnection(_connectionString))
                {
                    if (Cy.ID == null)
                    {
                        svSQL = "Insert into TMCategory_N (C_Id,C_Name,C_NameEN,Title_Eng) VALUES ('" + Cy.ID + "','" + Cy.C_Name + "','" + Cy.C_NameEN + "','" + Cy.Title_Eng + "')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }

                    else
                    {
                        svSQL = "Update into TMCategory_N (C_Id,C_Name,C_NameEN,Title_Eng) VALUES ('" + Cy.ID + "','" + Cy.C_Name + "','" + Cy.C_NameEN + "','" + Cy.Title_Eng + "')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();
                    }
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
    }
}
