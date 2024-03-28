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
    public class AdangapaService : IAdangapaService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public AdangapaService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public DataTable GetAllAdangapa(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "select  I_Id,Foot_Note,publish_up,publish_down,News_head from TMImages_N WHERE TMImages_N.deletenews='Y' ORDER BY TMImages_N.I_Id DESC";
            }
            else
            {
                SvSql = "select  I_Id,Foot_Note,publish_up,publish_down,News_head from TMImages_N WHERE TMImages_N.deletenews='N' ORDER BY TMImages_N.I_Id DESC";
            }
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
        public string StatusDeleteMR(string tag, int id)
        {

            try
            {
                string svSQL = string.Empty;
                using (SqlConnection objConnT = new SqlConnection(_connectionString))
                {
                    svSQL = "UPDATE TMImages_N SET deletenews ='N' WHERE I_Id='" + id + "'";
                    SqlCommand objCmds = new SqlCommand(svSQL, objConnT);
                    objConnT.Open();
                    objCmds.ExecuteNonQuery();
                    objConnT.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string AdangapaCRUD(List<IFormFile> files, Adangapa Cy)
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
                                        svSQL = "Insert into TMImages_N (I_cat,I_Cid,S_Image,L_image,Foot_Note,Addeddate,publish_up,publish_down,News_head,deletenews,most_view,tag) VALUES ('21','21','" + name + "','0','" + Cy.Original + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Cy.PublishUp + "','" + Cy.PublishDown + "','" + Cy.Comedy + "','Y','0','0')";
                                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                                        objCmds.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        file.CopyTo(fileStream);
                                        svSQL = "Update TMImages_N set I_cat = '21',I_Cid = '21',S_Image = '" + name + "',L_image = '0',Foot_Note = '" + Cy.Original + "',publish_up = '" + Cy.PublishUp + "',publish_down = '" + Cy.PublishDown + "',News_head = '" + Cy.Comedy + "',deletenews = 'Y',most_view = '0',tag = '0' WHERE TMImages_N.I_Id ='" + Cy.ID + "'";
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

        public DataTable GetEditAdangapa(string id)
        {
            string SvSql = string.Empty;
            SvSql = "select I_Id,Foot_Note,publish_up,publish_down,News_head from TMImages_N  Where TMImages_N.I_Id='" + id + "' ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
    }
}
