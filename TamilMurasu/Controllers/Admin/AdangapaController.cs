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

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = AdangapaService.GetEditAdangapa(id);
                if (dt.Rows.Count > 0)
                {
                    br.Original = dt.Rows[0]["Foot_Note"].ToString();
                    br.PublishUp = dt.Rows[0]["AddedDateFormatted"].ToString();
                    br.PublishDown = dt.Rows[0]["AddedDateFormatted1"].ToString();
                    br.Comedy = dt.Rows[0]["News_head"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListAdangapa()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adangapa(List<IFormFile> file, Adangapa Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = AdangapaService.AdangapaCRUD(file,Cy);
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
        public ActionResult MyAdangapagrid(string strStatus)
        {
            List<Adangapagrid> Reg = new List<Adangapagrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = AdangapaService.GetAllAdangapa(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;

                EditRow = "<a href=Adangapa?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/editing-icon-vector.jpg' alt='Edit' width='30' /></a>";
                DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/Inactive.png' alt='Deactivate' width='20' /></a>";



                Reg.Add(new Adangapagrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["I_Id"].ToString()),
                    footnote = dtUsers.Rows[i]["Foot_Note"].ToString(),
                    publishup = dtUsers.Rows[i]["AddedDateFormatted"].ToString(),
                    publishdown = dtUsers.Rows[i]["AddedDateFormatted1"].ToString(),
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

            string flag = AdangapaService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListAdangapa");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListAdangapa");
            }
        }
    }
}
