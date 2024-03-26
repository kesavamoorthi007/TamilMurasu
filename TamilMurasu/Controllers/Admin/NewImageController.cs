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
    public class NewImageController : Controller
    {
        INewImageService NewImageService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public NewImageController(INewImageService _NewImageService, IConfiguration _configuratio)
        {
            NewImageService = _NewImageService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult NewImage(string id)
        {
            NewImage br = new NewImage();

            if (id != null)
            {

            }
            return View(br);

        }
        public IActionResult ListNewImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewImage(NewImage Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = NewImageService.NewImageCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "New Image Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "New Image Updated Successfully...!";
                    }
                    return RedirectToAction("ListNewImage");
                }

                else
                {
                    ViewBag.PageTitle = "Edit NewImage";
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
