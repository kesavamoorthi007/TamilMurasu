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

namespace TamilMurasu.Controllers.Admin
{
    public class CategoryController : Controller
    {
        ICategoryService CategoryService;
        IConfiguration? _configuratio;
        private string? _connectionString;
        DataTransactions datatrans;
        public CategoryController(ICategoryService _CategoryService, IConfiguration _configuratio)
        {
            CategoryService = _CategoryService;
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString);
        }
        public IActionResult Category(string id)
        {
            Category br = new Category();

            if (id != null)
            {
               
            }
            else
            {
                DataTable dt = new DataTable();
                dt = CategoryService.GetEditCategory(id);

                if (dt.Rows.Count > 0)
                {
                    br.C_Name = dt.Rows[0]["C_Name"].ToString();
                    br.C_NameEN = dt.Rows[0]["C_NameEN"].ToString();
                    br.Title_Eng = dt.Rows[0]["Title_Eng"].ToString();
                    br.ID = id;

                }
            }
            return View(br);

        }
        public IActionResult ListCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Category(Category Cy, string id)
        {

            try
            {
                Cy.ID = id;
                string Strout = CategoryService.CategoryCRUD(Cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Cy.ID == null)
                    {
                        TempData["notice"] = "Category Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Category Updated Successfully...!";
                    }
                    return RedirectToAction("ListCategory");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Category";
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
        public ActionResult MyListCategorygrid()
        {
            List<Categorygrid> Reg = new List<Categorygrid>();
            DataTable dtUsers = new DataTable();
            dtUsers = CategoryService.GetAllCategory();
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {

                string DeleteRow = string.Empty;
                string EditRow = string.Empty;
                

               EditRow = "<a href=Category?id=" + dtUsers.Rows[i]["C_Id"].ToString() + "><img src='../Images/editing-icon-vector.jpg' alt='Edit' width='30' /></a>";
                

                Reg.Add(new Categorygrid
                {
                    id = Convert.ToInt64(dtUsers.Rows[i]["C_Id"].ToString()),
                    cname = dtUsers.Rows[i]["C_Name"].ToString(),
                    cnameeng = dtUsers.Rows[i]["C_NameEN"].ToString(),
                    tittle = dtUsers.Rows[i]["Title_Eng"].ToString(),
                    editrow = EditRow,

                });
            }

            return Json(new
            {
                Reg
            });

        }
    }
}
