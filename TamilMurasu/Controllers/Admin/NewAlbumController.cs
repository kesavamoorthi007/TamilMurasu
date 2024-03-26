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
    public class NewAlbumController : Controller
    {
        INewAlbumService NewAlbumService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public NewAlbumController(INewAlbumService _NewAlbumService, IConfiguration _configuratio)
        {
            NewAlbumService = _NewAlbumService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult NewAlbum(string id)
        {
            NewAlbum br = new NewAlbum();
            //br.Categorylst = BindCategory();

            if (id != null)
            {

            }
            return View(br);

        }
        public IActionResult ListNewAlbum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAlbum(NewAlbum Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = NewAlbumService.NewAlbumCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "New For Album Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "New For Album Updated Successfully...!";
                    }
                    return RedirectToAction("ListNews");
                }

                else
                {
                    ViewBag.PageTitle = "Edit NewAlbum";
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
