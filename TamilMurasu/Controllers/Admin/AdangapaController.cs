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
    public class AdangapaController : Controller
    {
        IAdangapaService AdangapaService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public AdangapaController(IAdangapaService _AdangapaService, IConfiguration _configuratio)
        {
            AdangapaService = _AdangapaService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Adangapa(string id)
        {
            Adangapa br = new Adangapa();
           

            if (id != null)
            {

            }
            return View(br);

        }
        public IActionResult ListAdangapa()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adangapa(Adangapa Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = AdangapaService.AdangapaCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "Adangapa Image Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Adangapa Image Updated Successfully...!";
                    }
                    return RedirectToAction("ListAdangapa");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Adangapa";
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
