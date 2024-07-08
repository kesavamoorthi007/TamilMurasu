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

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = RelexService.GetEditRelex(id);
                if (dt.Rows.Count > 0)
                {
                    br.Type = dt.Rows[0]["Foot_Note"].ToString();
                    br.ID = id;

                }
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
        public ActionResult MyRelexgrid(string strStatus)
        {
            List<Relexgrid> Reg = new List<Relexgrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = RelexService.GetAllRelex(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;

                if (dtUsers.Rows[i]["deletenews"].ToString() == "Y")
                {
                    EditRow = "<a href=Relex?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/EditIcon.png' alt='Edit' width='20' /></a>";
                    DeleteRow = "DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "";
                }
                else
                {

                    EditRow = "";
                    DeleteRow = "<a href=Remove?tag=Del&id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/close_icon.png' alt='Deactivate' /></a>";

                }

                Reg.Add(new Relexgrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["I_Id"].ToString()),
                    relex = dtUsers.Rows[i]["Foot_Note"].ToString(),
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

            string flag = RelexService.RemoveChange(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListRelex");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListRelex");
            }
        }
        public ActionResult DeleteMR(string tag, int id)
        {

            string flag = RelexService.StatusDeleteMR(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListRelex");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListRelex");
            }
        }
    }
}
