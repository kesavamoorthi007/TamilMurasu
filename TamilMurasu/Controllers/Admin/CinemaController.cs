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
    public class CinemaController : Controller
    {
        ICinemaService CinemaService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public CinemaController(ICinemaService _CinemaService, IConfiguration _configuratio)
        {
            CinemaService = _CinemaService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Cinema(string id)
        {
            Cinema br = new Cinema();
            //br.Categorylst = BindCategory();

            if (id != null)
            {

            }
            return View(br);

        }
        public IActionResult ListCinema()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cinema(Cinema Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = CinemaService.CinemaCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "Cinema Gallery Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Cinema Gallery Updated Successfully...!";
                    }
                    return RedirectToAction("ListCinema");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Cinema";
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
