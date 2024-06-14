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
using Microsoft.AspNetCore.Hosting;


namespace TamilMurasu.Controllers.Admin
{
    public class LatestNewsController : Controller
    {
        ILatestNewsService LatestNewsService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LatestNewsController(ILatestNewsService _LatestNewsService, IConfiguration _configuratio, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            LatestNewsService = _LatestNewsService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult LatestNews(string id)
        {
            LatestNews br = new LatestNews();

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = LatestNewsService.GetEditLatestNews(id);
                if (dt.Rows.Count > 0)
                {
                    br.NewsDetail = dt.Rows[0]["Foot_Note"].ToString();
                    br.PublishUp = dt.Rows[0]["AddedDateFormatted"].ToString();
                    br.PublishDown = dt.Rows[0]["AddedDateFormatted1"].ToString();
                    br.NewsHead = dt.Rows[0]["News_head"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListLatestNews()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LatestNews(LatestNews Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = LatestNewsService.LatestNewsCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "LatestNews Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "LatestNews Updated Successfully...!";
                    }
                    return RedirectToAction("ListLatestNews");
                }

                else
                {
                    ViewBag.PageTitle = "Edit LatestNews";
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
        public ActionResult MyLatestNewsgrid(string strStatus)
        {
            List<LatestNewsgrid> Reg = new List<LatestNewsgrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = LatestNewsService.GetAllLatestNews(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;

                EditRow = "<a href=LatestNews?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/edit(1).png' alt='Edit' width='30' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/delete(1).png' alt='Deactivate' width='20' /></a>";



                Reg.Add(new LatestNewsgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["I_Id"].ToString()),
                    footnote = dtUsers.Rows[i]["Foot_Note"].ToString(),
                    publishup = dtUsers.Rows[i]["AddedDateFormatted"].ToString(),
                    publishdown = dtUsers.Rows[i]["AddedDateFormatted1"].ToString(),
                    head = dtUsers.Rows[i]["News_head"].ToString(),
                    editrow = EditRow,
                    delrow = DeleteRow,

                });
            }

            return Json(new
            {
                Reg
            });

        }
        public ActionResult DeleteMR(string tag, int id)
        {

            string flag = LatestNewsService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListLatestNews");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListLatestNews");
            }
        }
        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload == null || upload.Length == 0)
                return BadRequest("File is empty");

            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + upload.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(),
                _webHostEnvironment.WebRootPath, "Uploads", fileName);

            var stream = new FileStream(path, FileMode.Create);
            upload.CopyToAsync(stream);
            return new JsonResult(new { path = "/Uploads/" + fileName });

        }
        [HttpGet]
        public IActionResult filebrowse()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(),
                _webHostEnvironment.WebRootPath, "Uploads"));
            ViewBag.fileInfo = dir.GetFiles();
            return View("~/Views/Home/FileExplorer.cshtml");

        }
    }
}
