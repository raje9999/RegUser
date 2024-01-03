
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.RazorPages;

using RegUser.Data;
using RegUser.Model;
using System.ComponentModel.DataAnnotations;


namespace RegUser.Pages.AllData

{

    public class RegisterPageModel : PageModel

    {

        private readonly ApplicationDbContext _db;

        public RegisterPageModel(ApplicationDbContext db)

        {

            _db = db;

        }

        [BindProperty]

        public AccountDetails UserDetails { get; set; }

        public void OnGet()

        {

        }

        public async Task<IActionResult> OnPost()

        {

            if (!ModelState.IsValid)

            {

                return Page();

            }

            _db.UserDetails.Add(UserDetails);

            await _db.SaveChangesAsync();

            return RedirectToPage("LoginPage");

        }

    }

}