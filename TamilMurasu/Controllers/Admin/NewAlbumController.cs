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

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = NewAlbumService.GetEditNewAlbum(id);
                if (dt.Rows.Count > 0)
                {
                    br.Album = dt.Rows[0]["Foot_Note"].ToString();
                    br.EnglishAlbum = dt.Rows[0]["News_head"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListNewAlbum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAlbum(List<IFormFile> file, NewAlbum Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = NewAlbumService.NewAlbumCRUD(file,Cy);
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
                    return RedirectToAction("ListNewAlbum");
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
        public ActionResult MyNewAlbumgrid(string strStatus)
        {
            List<NewAlbumgrid> Reg = new List<NewAlbumgrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = NewAlbumService.GetAllNewAlbum(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;

                EditRow = "<a href=NewAlbum?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/edit(1).png' alt='Edit' width='30' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/delete(1).png' alt='Deactivate' width='20' /></a>";



                Reg.Add(new NewAlbumgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["I_Id"].ToString()),
                    footnote = dtUsers.Rows[i]["Foot_Note"].ToString(),
                    head = dtUsers.Rows[i]["News_head"].ToString(),
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

            string flag = NewAlbumService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListNewImage");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListNewImage");
            }
        }
    }
}
