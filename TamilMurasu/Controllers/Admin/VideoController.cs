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

            if (id != null)
            {

            }
            return View(br);

        }
        public IActionResult ListNews()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Video(Video Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = VideoService.VideoCRUD(Cy);
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
    }
}
