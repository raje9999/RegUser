using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegUser.Data;
using RegUser.Model;
using System.ComponentModel.DataAnnotations;

namespace RegUser.Pages.AllData
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AccountDetails UserDetails { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(string Email)
        {
            UserDetails = _db.UserDetails.Find(Email);

        }

        public async Task<IActionResult> OnPost()
        {
            //if(string.IsNullOrEmpty(Email))
            //{
            //    return RedirectToPage("/Index");
            //}

            //UserDetails = _db.UserDetails.FirstOrDefault(x => x.Email == Email);
            
            //if(UserDetails == null)
            //{
            //    return NotFound();
            //}
            if(ModelState.IsValid)
            {
                _db.UserDetails.Update(UserDetails);

                await _db.SaveChangesAsync();

                return RedirectToPage("UserData");
            }
            return Page();
        }
    }
}
