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
using System.Text;

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
                    int shift = 1;
                    br.Original = dt.Rows[0]["Foot_Note"].ToString();
                    br.Original = Decrypt(br.Original, shift);
                    br.PublishUp = dt.Rows[0]["AddedDateFormatted"].ToString();
                    br.PublishDown = dt.Rows[0]["AddedDateFormatted1"].ToString();
                    br.Comedy = dt.Rows[0]["News_head"].ToString();
                    br.filename1 = dt.Rows[0]["S_Image"].ToString();
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
                if (dtUsers.Rows[i]["deletenews"].ToString() == "Y")
                {
                    EditRow = "<a href=Adangapa?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/EditIcon.png' alt='Edit' width='20' /></a>";
                    DeleteRow = "<a href=DeleteMR?id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/DeleteIcon.png' alt='Deactivate' width='20' /></a>";

                }
                else
                {

                    EditRow = "";
                    DeleteRow = "<a href=Remove?tag=Del&id=" + dtUsers.Rows[i]["I_Id"].ToString() + "><img src='../Images/close_icon.png' alt='Deactivate' /></a>";

                }

              


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
        public ActionResult Remove(string tag, int id)
        {

            string flag = AdangapaService.RemoveChange(tag, id);
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

        public string Decrypt(string cipherText, int shift)
        {
            // StringBuilder to store the decrypted text
            StringBuilder decryptedText = new StringBuilder();
             cipherText = "A®•à®¾à®²à®¿à®¸à¯?à®¤à®¾à®©à¯? à®†à®¤à®°à®µà®¾à®³à®°à¯?à®•à®³à®¾à®²à¯? à®‡à®¨à¯?à®¤à®¿à®¯ à®ªà®¤à¯?à®¤à®¿à®°à®¿à®•à¯ˆà®¯à®¾à®³à®°à¯?à®•à®³à¯? à®®à¯€à®¤à¯? à®¤à®¾à®•à¯?à®•à¯?à®¤à®²à¯? à®µà®¾à®·à®¿à®™à¯?à®Ÿà®©à®¿à®²à¯? à®ªà®°à®ªà®°à®ªà¯?à®ªà¯?";

            // Iterate through each character in the cipherText
            foreach (char ch in cipherText)
            {
                // Check if the character is a Tamil letter (Unicode range: 0B80 to 0BFF)
                if (ch >= 0x0B80  && ch <= 0x0BFF)
                {
                    // Shift the character back by the shift value and wrap around if necessary
                    int newCharCode = ((ch - 0x0B80) - shift + 128) % 128 + 0x0B80;
                    decryptedText.Append((char)newCharCode);
                }
                else
                {
                    // If the character is not a Tamil letter, add it to the decrypted text without changing it
                    decryptedText.Append(ch);
                }
            }

            // Return the decrypted text
            return decryptedText.ToString();
        }

       

    }
}
