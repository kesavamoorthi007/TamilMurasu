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
using DocumentFormat.OpenXml.Office2010.CustomUI;

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
            SvSql = "select N_Id,TMCategory_N.C_Name,NT_Head,N_Description,Keyword from TMNews_N left outer join TMCategory_N ON TMCategory_N.C_Id=TMNews_N.C_Id ";
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

        public string NewsCRUD(List<IFormFile> files,News Cy)
        {
            string msg = "";
            try
            {
                string StatementType = string.Empty;
                string svSQL = "";

                using (SqlConnection objConn = new SqlConnection(_connectionString))
                {
                    objConn.Open();
                    if (files != null && files.Count > 0)
                    {
                       
                        foreach (var file in files)
                        {
                            if (file.Length > 0)
                            {
                                // Get the file name and combine it with the target folder path
                                String strLongFilePath1 = file.FileName;
                                String sFileType1 = "";
                                sFileType1 = System.IO.Path.GetExtension(file.FileName);
                                sFileType1 = sFileType1.ToLower();

                                String strFleName = strLongFilePath1.Replace(sFileType1, "") + String.Format("{0:ddMMMyyyy-hhmmsstt}", DateTime.Now) + sFileType1;
                                var fileName = Path.Combine("wwwroot/Uploads", strFleName);
                                var fileName1 = Path.Combine("Uploads", strFleName);
                                var name = file.FileName;
                                // Save the file to the target folder

                                using (var fileStream = new FileStream(fileName, FileMode.Create))
                                {
                                    if (Cy.ID == null)
                                    {
                                        file.CopyTo(fileStream);
                                        svSQL = "Insert into TMNews_N (C_Id,NT_Head,N_Description,S_Image,L_Image,Banner,Highlights,EditorPick,Publish_Up,Publish_down,Keyword,Most_read,Most_comment,deletenews) VALUES ('" + Cy.Category + "','" + Cy.NewsHead + "','" + Cy.NewsDetail + "','" + name + "','" + name + "','" + Cy.Banner + "','" + Cy.Highlights + "','" + Cy.Editor + "','" + Cy.PublishUp + "','" + Cy.PublishDown + "','" + Cy.KeyWords + "','0','0','Y')";
                                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                                        objCmds.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        file.CopyTo(fileStream);
                                        svSQL = "Update TMNews_N set C_Id = '" + Cy.Category + "',NT_Head = '" + Cy.NewsHead + "',N_Description = '" + Cy.NewsDetail + "',S_Image = '" + name + "',L_Image = '" + name + "',Banner = '" + Cy.Banner + "',Highlights = '" + Cy.Highlights + "',EditorPick = '" + Cy.Editor + "',Publish_Up = '" + Cy.PublishUp + "',Publish_down = '" + Cy.PublishDown + "',Keyword = '" + Cy.KeyWords + "' WHERE TMNews_N.N_Id ='" + Cy.ID + "'";
                                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                                        objCmds.ExecuteNonQuery();

                                    }


                                }
                            }

                        }
                        objConn.Close();
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
    }
}
