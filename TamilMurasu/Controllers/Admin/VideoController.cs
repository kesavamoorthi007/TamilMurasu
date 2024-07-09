using System.Collections.Generic;
using System.Data;
using TamilMurasu.Interface;
using TamilMurasu.Interface.Admin;
using TamilMurasu.Models;
using TamilMurasu.Services;
using TamilMurasu.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;

namespace TamilMurasu.Controllers.Admin
{
    public class VideoController : Controller
    {
        IVideoService VideoService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public VideoController(IVideoService _VideoService, IConfiguration _configuratio)
        {
            VideoService = _VideoService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Video(string id)
        {
            Video br = new Video();

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = VideoService.GetEditVideo(id);
                if (dt.Rows.Count > 0)
                {
                    br.VideoTittle = dt.Rows[0]["Foot_Note"].ToString();
                    br.PublishUp = dt.Rows[0]["AddedDateFormatted"].ToString();
                    br.PublishDown = dt.Rows[0]["AddedDateFormatted1"].ToString();
                    br.filename1 = dt.Rows[0]["S_Image"].ToString();
                    br.ID = id;

                }
            }
            return View(br);
        }
        public IActionResult ListVideo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Video(List<IFormFile> file, Video Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = VideoService.VideoCRUD(file,Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "Video Image Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Video Image Updated Successfully...!";
                    }
                    return RedirectToAction("ListVideo");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Video";
                    TempData["notice"] = Strout;
                    //return View();
                }

                // }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(Cy);
        }
        public ActionResult MyVideogrid(string strStatus)
        {
            List<Videogrid> Reg = new List<Videogrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = VideoService.GetAllVideo(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;
                if (dtUsers.Rows[i]["deletenews"].ToString() == "Y")
                {
                    EditRow = "<a href=Video?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/EditIcon.png' alt='Edit' width='20' /></a>";
                    DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/Inactive.png' alt='Deactivate' width='20' /></a>";
                }
                else
                {

                    EditRow = "";
                    DeleteRow = "<a href=Remove?tag=Del&id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/close_icon.png' alt='Deactivate' /></a>";

                }

                Reg.Add(new Videogrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["I_Id"].ToString()),
                    title = dtUsers.Rows[i]["S_Image"].ToString(),
                    foot = dtUsers.Rows[i]["Foot_Note"].ToString(),
                    editrow = EditRow,
                    delrow = DeleteRow,

                });
            }

            return Json(new
            {
                Reg
            });

        }
        public ActionResult Remove(string tag, int id)
        {

            string flag = VideoService.RemoveChange(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListVideo");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListVideo");
            }
        }
        public ActionResult DeleteMR(string tag, int id)
        {

            string flag = VideoService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListVideo");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListVideo");
            }
        }
    }
}
