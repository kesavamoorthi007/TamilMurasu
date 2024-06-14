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

        public DataTable GetAllLatestNews(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "select  I_Id,Foot_Note,CONVERT(varchar, TMImages_N.publish_up, 106) AS AddedDateFormatted,CONVERT(varchar, TMImages_N.publish_down, 106) AS AddedDateFormatted1,News_head from TMImages_N WHERE I_cat='21'  and TMImages_N.deletenews='Y' ORDER BY TMImages_N.I_Id DESC";
            }
            else
            {
                SvSql = "select  I_Id,Foot_Note,CONVERT(varchar, TMImages_N.publish_up, 106) AS AddedDateFormatted,CONVERT(varchar, TMImages_N.publish_down, 106) AS AddedDateFormatted1,News_head from TMImages_N WHERE I_cat='21'  and TMImages_N.deletenews='N' ORDER BY TMImages_N.I_Id DESC";
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
        public string LatestNewsCRUD(LatestNews Cy)
        {
            string msg = "";
            try
            {
                string StatementType = string.Empty;
                string svSQL = "";

                //if (Cy.ID == null)
                //{
                //    svSQL = " SELECT Count(C_Name) as cnt FROM TMCategory_N WHERE C_Name = LTRIM(RTRIM('" + Cy.C_Name + "')) ";
                //    if (datatrans.GetDataId(svSQL) > 0)
                //    {
                //        msg = "Category Name(Tamil) Already Existed";
                //        return msg;
                //    }
                //}
                using (SqlConnection objConn = new SqlConnection(_connectionString))
                {
                    objConn.Open();
                    if (Cy.ID == null)
                    {
                        svSQL = "Insert into TMImages_N (I_cat,I_Cid,S_Image,L_image,Foot_Note,Addeddate,publish_up,publish_down,News_head,deletenews,most_view,tag) VALUES ('21','21','0','0','" + Cy.NewsDetail + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Cy.PublishUp + "','" + Cy.PublishDown + "','" + Cy.NewsHead + "','Y','0','0')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update TMImages_N set Foot_Note = '" + Cy.NewsDetail + "',publish_up = '" + Cy.PublishUp + "',publish_down = '" + Cy.PublishDown + "',News_head = '" + Cy.NewsHead + "' WHERE TMImages_N.I_Id ='" + Cy.ID + "'";
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

        public DataTable GetEditLatestNews(string id)
        {
            string SvSql = string.Empty;
            SvSql = "select I_Id,Foot_Note,CONVERT(varchar, TMImages_N.publish_up, 106) AS AddedDateFormatted,CONVERT(varchar, TMImages_N.publish_down, 106) AS AddedDateFormatted1,News_head from TMImages_N  Where TMImages_N.I_Id='" + id + "' ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }
    }
}
