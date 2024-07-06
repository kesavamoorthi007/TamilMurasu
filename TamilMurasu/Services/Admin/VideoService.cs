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
    public class VideoService : IVideoService
    {
        private readonly string _connectionString;
        DataTransactions datatrans;
        public VideoService(IConfiguration _configuratio)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }

        public DataTable GetAllVideo(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "Select I_Id,I_cat,S_Image,Foot_Note,deletenews from TMImages_N WHERE I_cat =30 and deletenews='Y' Order by I_Id desc";
            }
            else
            {
                SvSql = "Select I_Id,I_cat,S_Image,Foot_Note,deletenews from TMImages_N WHERE I_cat =30 and deletenews='N' Order by I_Id desc";
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
        public string RemoveChange(string tag, int id)
        {

            try
            {
                string svSQL = string.Empty;
                using (SqlConnection objConnT = new SqlConnection(_connectionString))
                {
                    svSQL = "UPDATE TMImages_N SET deletenews ='Y' WHERE I_Id='" + id + "'";
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
        public string VideoCRUD(List<IFormFile> files, Video Cy)
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
                        if (files != null && files.Count > 0)
                        {
                            string filename1 = "";
                            string filesave1 = "";
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

                                    var fileNme1 = "../Uploads/" + strFleName;

                                    filename1 = filename1.Length > 0 ? filename1 + "," + fileName : fileName;

                                    filesave1 = filesave1.Length > 0 ? filesave1 + "," + fileNme1 : fileNme1;

                                    var name = file.FileName;
                                    // Save the file to the target folder

                                    using (var fileStream = new FileStream(fileName, FileMode.Create))
                                    {
                                        file.CopyTo(fileStream);
                                    }
                                }

                            }
                            svSQL = "Insert into TMImages_N (I_cat,I_Cid,S_Image,L_image,Foot_Note,publish_up,publish_down,News_head,deletenews,most_view,tag,AddedDate) VALUES ('30','30','" + filesave1 + "','0',N'" + Cy.VideoTittle + "','" + Cy.PublishUp + "','" + Cy.PublishDown +"','0','Y','1','0','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                            objCmds.ExecuteNonQuery();

                        }

                    }
                    else
                    {
                        svSQL = "Update TMImages_N set Foot_Note = N'" + Cy.VideoTittle + "',publish_up = '" + Cy.PublishUp + "',publish_down = '" + Cy.PublishDown + "' WHERE TMImages_N.I_Id ='" + Cy.ID + "'";
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
        public DataTable GetEditVideo(string id)
        {
            string SvSql = string.Empty;
            SvSql = "select I_Id,Foot_Note,CONVERT(varchar, TMImages_N.publish_up, 106) AS AddedDateFormatted,CONVERT(varchar, TMImages_N.publish_down, 106) AS AddedDateFormatted1,S_Image from TMImages_N WHERE TMImages_N.I_Id='" + id + "' ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
    }
}
