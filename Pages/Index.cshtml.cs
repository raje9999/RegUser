using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegUser.Data;
using RegUser.Model;
using System.ComponentModel.DataAnnotations;

namespace RegUser.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]

        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [BindProperty]

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public AccountDetails UserDetails { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            String thisEmail = _db.UserDetails.FirstOrDefault(x => x.Email == Email)?.Email ?? "";

            String thisPassword = _db.UserDetails.Where(x => x.Email == thisEmail).FirstOrDefault()?.Password ?? "";

            if (Email == null || thisEmail == "" || thisPassword == "")

            {

                ModelState.AddModelError(thisEmail, "User Not Found");

            }

            else if (Email != thisEmail || Password != thisPassword)

            {

                ModelState.AddModelError(thisPassword, "Invalid Credentials");

            }

            if (!ModelState.IsValid)

            {

                return Page();

            }
            TempData["UserName"] = _db.UserDetails.FirstOrDefault(x => x.Email == Email)?.FullName ?? "";

            return RedirectToPage("/AllData/WelcomePage");
        }

    }
}