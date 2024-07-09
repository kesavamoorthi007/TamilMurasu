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

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = CinemaService.GetEditCinema(id);
                if (dt.Rows.Count > 0)
                {
                    br.Album = dt.Rows[0]["Foot_Note"].ToString();
                    br.EnglishAlbum = dt.Rows[0]["News_head"].ToString();
                    br.Tag = dt.Rows[0]["tag"].ToString();
                    br.filename1 = dt.Rows[0]["S_Image"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListCinema()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cinema(List<IFormFile> file, Cinema Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = CinemaService.CinemaCRUD(file,Cy);
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
        public ActionResult MyCinemagrid(string strStatus)
        {
            List<MyCinemagrid> Reg = new List<MyCinemagrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = CinemaService.GetAllCinema(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;
                if (dtUsers.Rows[i]["deletenews"].ToString() == "Y")
                {
                    EditRow = "<a href=Cinema?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/EditIcon.png' alt='Edit' width='20' /></a>";

                    DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/Inactive.png' alt='Deactivate' width='20' /></a>";

                   // DeleteRow = "DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "";


                }
                else
                {

                    EditRow = "";
                    DeleteRow = "<a href=Remove?tag=Del&id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/close_icon.png' alt='Deactivate' /></a>";

                }






                Reg.Add(new MyCinemagrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["I_Id"].ToString()),
                    footnote = dtUsers.Rows[i]["Foot_Note"].ToString(),
                    editrow = EditRow,
                    delrow = DeleteRow,

                });
            }

            return Json(new
            {
                Reg
            });

        }
        public ActionResult Remove(string tag, int id)
        {

            string flag = CinemaService.RemoveChange(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListCinema");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListCinema");
            }
        }
        public ActionResult DeleteMR(string tag, int id)
        {

            string flag = CinemaService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListCinema");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListCinema");
            }
        }
    }
}
