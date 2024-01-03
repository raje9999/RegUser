using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegUser.Data;
using RegUser.Model;

namespace RegUser.Pages.AllData
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AccountDetails UserDetails { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(string Email)
        {
            UserDetails = _db.UserDetails.Find(Email);
        }
        public async Task<IActionResult> OnPost()
        {
            
           
                _db.UserDetails.Remove(UserDetails);
          
                await _db.SaveChangesAsync();

               return RedirectToPage("UserData");
           
           
        }
    }
}


