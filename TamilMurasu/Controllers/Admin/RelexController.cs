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
    public class RelexController : Controller
    {
        IRelexService RelexService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public RelexController(IRelexService _RelexService, IConfiguration _configuratio)
        {
            RelexService = _RelexService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Relex(string id)
        {
            Relex br = new Relex();

            if (id != null)
            {

            }
            return View(br);

        }
        public IActionResult ListRelex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Relex(Relex Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = RelexService.RelexCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "Relex Image Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Relex Image Updated Successfully...!";
                    }
                    return RedirectToAction("ListRelex");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Relex";
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
