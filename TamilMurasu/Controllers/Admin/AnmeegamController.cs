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

            if (id != null)
            {

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
                lstdesg.Add(new SelectListItem() { Text = "இந்து", Value = "இந்து" });
                lstdesg.Add(new SelectListItem() { Text = "கிறிஸ்தவம்", Value = "கிறிஸ்தவம்" });
                lstdesg.Add(new SelectListItem() { Text = "இஸ்லாம்", Value = "இஸ்லாம்" });

                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
