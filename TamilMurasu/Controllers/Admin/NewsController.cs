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

namespace TamilMurasu.Controllers.Admin
{
    public class NewsController : Controller
    {
        INewsService NewsService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public NewsController(INewsService _NewsService, IConfiguration _configuratio)
        {
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
                //DataTable dt = new DataTable();
                //dt = NewsService.GetEditNews(id);
                //if (dt.Rows.Count > 0)
                //{
                //    br.NewsHead = dt.Rows[0]["Foot_Note"].ToString();
                //    br.Category = dt.Rows[0]["News_head"].ToString();
                //    br.Category = dt.Rows[0]["News_head"].ToString();
                //    br.Category = dt.Rows[0]["News_head"].ToString();
                 
                //    br.ID = id;

                //}
            }
            return View(br);
        }
        public IActionResult ListNews()
        {
            return View();
        }
        [HttpPost]
        public ActionResult News(List<IFormFile> file,News Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = NewsService.NewsCRUD(file,Cy);
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
                    lstdesg.Add(new SelectListItem() { Text = dtDesg.Rows[i]["C_NameEN"].ToString(), Value = dtDesg.Rows[i]["C_Id"].ToString() });
                }
                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult MyListNewsgrid()
        {
            List<Newsgrid> Reg = new List<Newsgrid>();
            DataTable dtUsers = new DataTable();
            dtUsers = NewsService.GetAllNews();
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;

                EditRow = "<a href=News?id=" + dtUsers.Rows[i]["N_Id"].ToString() + "><img src='../Images/editing-icon-vector.jpg' alt='Edit' width='30' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["N_Id"].ToString() + "><img src='../Images/Inactive.png' alt='Deactivate' width='20' /></a>";


                Reg.Add(new Newsgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["N_Id"].ToString()),
                    cname = dtUsers.Rows[i]["C_Name"].ToString(),
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
    }
}
