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
    public class AnmeegamService : IAnmeegamService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public AnmeegamService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public DataTable GetAllAnmeegam()
        {
            string SvSql = string.Empty;
            SvSql = "select A_Id,A_Name,A_Decription from TMAanmegaKural ORDER BY TMAanmegaKural.A_Id DESC";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }

        public string AnmeegamCRUD(Anmeegam Cy)
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
                        svSQL = "Insert into TMAanmegaKural (A_Cat,A_Name,A_Decription,Addeddate,APublish_Up,APublish_Down) VALUES ('" + Cy.Category + "',N'" + Cy.Aanmegam + "',N'" + Cy.NewsDetail + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Cy.PublishUp + "','" + Cy.PublishDown + "')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update TMAanmegaKural set A_Cat = '" + Cy.Category + "',A_Name = N'" + Cy.Aanmegam + "',A_Decription = N'" + Cy.NewsDetail + "',APublish_Up = '" + Cy.PublishUp + "',APublish_Down = '" + Cy.PublishDown + "' WHERE TMAanmegaKural.A_Id ='" + Cy.ID + "'";
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

        public DataTable GetEditAnmeegam(string id)
        {
            string SvSql = string.Empty;
            SvSql = "select A_Id,A_Cat,A_Name,A_Decription,CONVERT(varchar, TMAanmegaKural.APublish_Up, 106) AS AddedDateFormatted,CONVERT(varchar, TMAanmegaKural.APublish_Down, 106) AS AddedDateFormatted1 from TMAanmegaKural  Where TMAanmegaKural.A_Id='" + id + "' ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
    }
}
