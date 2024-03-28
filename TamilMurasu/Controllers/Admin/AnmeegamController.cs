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
    public class AnmeegamController : Controller
    {
        IAnmeegamService AnmeegamService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public AnmeegamController(IAnmeegamService _AnmeegamService, IConfiguration _configuratio)
        {
            AnmeegamService = _AnmeegamService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Anmeegam(string id)
        {
            Anmeegam br = new Anmeegam();
            br.Categorylst = BindCategory();

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = AnmeegamService.GetEditAnmeegam(id);
                if (dt.Rows.Count > 0)
                {
                    br.Category = dt.Rows[0]["A_Cat"].ToString();
                    br.Aanmegam = dt.Rows[0]["A_Name"].ToString();
                    br.NewsDetail = dt.Rows[0]["A_Decription"].ToString();
                    br.PublishUp = dt.Rows[0]["APublish_Up"].ToString();
                    br.PublishDown = dt.Rows[0]["APublish_Down"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListAnmeegam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Anmeegam(Anmeegam Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = AnmeegamService.AnmeegamCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "Anmeegam Kural Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Anmeegam Kural Updated Successfully...!";
                    }
                    return RedirectToAction("ListAnmeegam");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Anmeegam";
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
                List<SelectListItem> lstdesg = new List<SelectListItem>();
                lstdesg.Add(new SelectListItem() { Text = "இந்து", Value = "1" });
                lstdesg.Add(new SelectListItem() { Text = "கிறிஸ்தவம்", Value = "2" });
                lstdesg.Add(new SelectListItem() { Text = "இஸ்லாம்", Value = "3" });

                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult MyAnmeegamgrid()
        {
            List<Anmeegamgrid> Reg = new List<Anmeegamgrid>();
            DataTable dtUsers = new DataTable();
            dtUsers = AnmeegamService.GetAllAnmeegam();
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;

                EditRow = "<a href=Anmeegam?id=" + dtUsers.Rows[i]["A_Id"].ToString() + "><img src='../Images/editing-icon-vector.jpg' alt='Edit' width='30' /></a>";


                Reg.Add(new Anmeegamgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["A_Id"].ToString()),
                    name = dtUsers.Rows[i]["A_Name"].ToString(),
                    desc = dtUsers.Rows[i]["A_Decription"].ToString(),
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
