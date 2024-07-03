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
            br.Categorylst = BindCategory();

            if (id == null)
            {

            }
            else
            {
                DataTable dt = new DataTable();
                dt = NewImageService.GetEditNewImage(id);
                if (dt.Rows.Count > 0)
                {
                    br.Categorylst = BindCategory();
                    br.Category = dt.Rows[0]["I_Cid"].ToString();
                    br.FootNote = dt.Rows[0]["Foot_Note"].ToString();
                    br.PublishUp = dt.Rows[0]["AddedDateFormatted"].ToString();
                    br.PublishDown = dt.Rows[0]["AddedDateFormatted1"].ToString();
                    br.filename1 = dt.Rows[0]["S_Image"].ToString();
                    br.filename2 = dt.Rows[0]["L_Image"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListNewImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewImage(List<IFormFile> file, List<IFormFile> file1, NewImage Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = NewImageService.NewImageCRUD(file, file1,Cy);
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
        public List<SelectListItem> BindCategory()
        {
            try
            {
                DataTable dtDesg = NewImageService.GetCategory();
                List<SelectListItem> lstdesg = new List<SelectListItem>();
                for (int i = 0; i < dtDesg.Rows.Count; i++)
                {
                    lstdesg.Add(new SelectListItem() { Text = dtDesg.Rows[i]["C_NameEN"].ToString(), Value = dtDesg.Rows[i]["C_Id"].ToString() });
                }
                return lstdesg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult MyNewImagegrid(string strStatus)
        {
            List<NewImagegrid> Reg = new List<NewImagegrid>();
            DataTable dtUsers = new DataTable();
            strStatus = strStatus == "" ? "Y" : strStatus;
            dtUsers = NewImageService.GetAllNewImage(strStatus);
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string EditRow = string.Empty;
                string DeleteRow = string.Empty;
                if (dtUsers.Rows[i]["deletenews"].ToString() == "Y")
                {
                    EditRow = "<a href=NewImage?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/EditIcon.png' alt='Edit' width='20' /></a>";
                    DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/DeleteIcon.png' alt='Deactivate' width='20' /></a>";
                }
                else
                {

                    EditRow = "";
                    DeleteRow = "<a href=Remove?tag=Del&id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/close_icon.png' alt='Deactivate' /></a>";

                }

                



                Reg.Add(new NewImagegrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["I_Id"].ToString()),
                    footnote = dtUsers.Rows[i]["Foot_Note"].ToString(),
                    Thumb = dtUsers.Rows[i]["S_Image"].ToString(),
                    large = dtUsers.Rows[i]["L_image"].ToString(),
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

            string flag = NewImageService.RemoveChange(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListNewImage");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListNewAlbum");
            }
        }
        public ActionResult DeleteMR(string tag, int id)
        {

            string flag = NewImageService.StatusDeleteMR(tag, id);
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
