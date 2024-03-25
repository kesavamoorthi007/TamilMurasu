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
    public class LatestNewsController : Controller
    {
        ILatestNewsService LatestNewsService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public LatestNewsController(ILatestNewsService _LatestNewsService, IConfiguration _configuratio)
        {
            LatestNewsService = _LatestNewsService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult LatestNews(string id)
        {
            LatestNews br = new LatestNews();

            if (id != null)
            {

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
        public ActionResult MyLatestNewsgrid()
        {
            List<LatestNewsgrid> Reg = new List<LatestNewsgrid>();
            DataTable dtUsers = new DataTable();
            dtUsers = LatestNewsService.GetAllLatestNews();
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;

                EditRow = "<a href=LatestNews?id=" + dtUsers.Rows[i]["N_Id"].ToString() + "><img src='../Images/editing-icon-vector.jpg' alt='Edit' width='30' /></a>";


                Reg.Add(new LatestNewsgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["N_Id"].ToString()),
                    cname = dtUsers.Rows[i]["C_Name"].ToString(),
                    newshead = dtUsers.Rows[i]["NT_Head"].ToString(),
                    des = dtUsers.Rows[i]["N_Description"].ToString(),
                    keyword = dtUsers.Rows[i]["Keyword"].ToString(),
                    editrow = EditRow,

                });
            }

            return Json(new
            {
                Reg
            });

        }
    }
}
