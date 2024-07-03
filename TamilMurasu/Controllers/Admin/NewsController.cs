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
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Security.Policy;
using Microsoft.AspNetCore.Hosting;

namespace TamilMurasu.Controllers.Admin
{
    public class NewsController : Controller
    {
        INewsService NewsService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(INewsService _NewsService, IConfiguration _configuratio, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            NewsService = _NewsService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult News(string id)
        {
            News br = new News();
            br.Categorylst = BindCategory();

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = NewsService.GetEditNews(id);
                if (dt.Rows.Count > 0)
                {
                    br.NewsHead = dt.Rows[0]["NT_Head"].ToString();
                    br.Categorylst = BindCategory();
                    br.Category = dt.Rows[0]["C_Id"].ToString();
                    br.Editor = Convert.ToBoolean(dt.Rows[0]["EditorPick"].ToString());
                    br.Highlights = Convert.ToBoolean(dt.Rows[0]["Highlights"].ToString());
                    br.Banner = Convert.ToBoolean(dt.Rows[0]["Banner"].ToString());
                    br.NewsDetail = dt.Rows[0]["N_Description"].ToString();
                    br.PublishUp = dt.Rows[0]["AddedDateFormatted"].ToString();
                    br.PublishDown = dt.Rows[0]["AddedDateFormatted1"].ToString();
                    br.KeyWords = dt.Rows[0]["Keyword"].ToString();
                    br.filename1 = dt.Rows[0]["S_Image"].ToString();
                    br.filename2 = dt.Rows[0]["L_Image"].ToString();

                    br.ID = id;

                }
            }
            return View(br);
        }
        public IActionResult ListNews()
        {
            return View();
        }
        [HttpPost]
        public ActionResult News(List<IFormFile> file, List<IFormFile> file1,News Cy, string id)
       {

            try
            {
                Cy.ID = id;
                string Strout = NewsService.NewsCRUD(file,file1,Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "News Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "News Updated Successfully...!";
                    }
                    return RedirectToAction("ListNews");
                }

                else
                {
                    ViewBag.PageTitle = "Edit News";
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
       
        public List<SelectListItem> BindCategory()
        {
            try
            {
                DataTable dtDesg = NewsService.GetCategory();
                List<SelectListItem> lstdesg = new List<SelectListItem>();
                for (int i = 0; i < dtDesg.Rows.Count; i++)
                {
                    lstdesg.Add(new SelectListItem() { Text = dtDesg.Rows[i]["C_Name"].ToString(), Value = dtDesg.Rows[i]["C_Id"].ToString() });
                }
                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult MyListNewsgrid(string strStatus)
        {
            List<Newsgrid> Reg = new List<Newsgrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = NewsService.GetAllNews(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;

                if (dtUsers.Rows[i]["deletenews"].ToString() == "Y")
                {
                    EditRow = "<a href=News?id=" + dtUsers.Rows[i]["N_Id"].ToString() + "><img src='../Images/EditIcon.png' alt='Edit' width='20' /></a>";
                    DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["N_Id"].ToString() + "><img src='../Images/DeleteIcon.png' alt='Deactivate' width='20' /></a>";

                }
                else
                {

                    EditRow = "";
                    DeleteRow = "<a href=Remove?tag=Del&id=" + dtUsers.Rows[i]["N_Id"].ToString() + "><img src='../Images/close_icon.png' alt='Deactivate' /></a>";

                }
                


                Reg.Add(new Newsgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["N_Id"].ToString()),
                    //cname = dtUsers.Rows[i]["C_Name"].ToString(),
                    newshead = dtUsers.Rows[i]["NT_Head"].ToString(),
                    des = dtUsers.Rows[i]["N_Description"].ToString(),
                    keyword = dtUsers.Rows[i]["Keyword"].ToString(),
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

            string flag = NewsService.RemoveChange(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListNews");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListNews");
            }
        }
        public ActionResult DeleteMR(string tag, int id)
        {

            string flag = NewsService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListNews");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListNews");
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
