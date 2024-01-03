using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RegUser.Data;
using ClosedXML.Excel;
using System.IO;
using RegUser.Model;
using System.Globalization;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using static Azure.Core.HttpHeader;
using DocumentFormat.OpenXml.Packaging;

namespace RegUser.Pages.AllData

{



    public class UserDataModel : PageModel

    {

        private readonly ApplicationDbContext _db;
        private readonly IConfiguration Configuration;

        // public AccountDetails UserDetails { get; set; }

        //public IEnumerable<AccountDetails> UserDetails { get; set; }

        public PaginationList<AccountDetails> UserDetails { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchElement { get; set; }
        [BindProperty]
        public string SortByName { get; set; }
        [BindProperty]
        public string SortByDate { get; set; }
        [BindProperty]
        public string SortByMOD { get; set; }
        public string CurrentFilter { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }


        public UserDataModel(ApplicationDbContext db, IConfiguration configuration)

        {

            _db = db;
            Configuration = configuration;

        }
   

        public async Task OnGetAsync(string sortOrder, string currentFilter, string SearchElement, int? pageIndex)
        {

            //var accs = from a in _db.UserDetails select a;
            IQueryable<AccountDetails> accs = from a in _db.UserDetails select a;
            if (!string.IsNullOrEmpty(SearchElement))
            {
                accs = accs.Where(s => s.FullName.Contains(SearchElement) || s.Email.Contains(SearchElement));
            }

            if (SearchElement != null)
            {
                pageIndex = 1;
            }
            else
            {
                SearchElement = currentFilter;
            }

            CurrentFilter = SearchElement;
            if(!accs.Any())
            {
                TempData["ErrorMessage"] = "User not found";
            }
           
            //UserDetails = await details.ToListAsync();



            SortByName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            SortByDate = sortOrder == "date_aesc" ? "date_desc" : "date_aesc";
            SortByMOD = sortOrder == "mod_aesc" ? "mod_desc" : "mod_aesc";

            switch (sortOrder)
            {
                case "name_aesc":
                    accs = accs.OrderBy(s => s.FullName);
                    break;
                case "name_desc":
                    accs = accs.OrderByDescending(s => s.FullName);
                    break;
                case "date_aesc":
                    accs = accs.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    accs = accs.OrderByDescending(s => s.Date);
                    break;
                case "mod_aesc":
                    accs = accs.OrderBy(s => s.ModDate);
                    break;
                case "mod_desc":
                    accs = accs.OrderByDescending(s => s.ModDate);
                    break;
                default:
                    accs = accs.OrderBy(s => s.Email);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            UserDetails = await PaginationList<AccountDetails>.CreateAsync(accs, pageIndex ?? 1, pageSize);

            //UserDetails = await accs.ToListAsync();
        }

        public FileResult OnPostExport()
        {
            DataTable td = new DataTable("Grid");
            td.Columns.AddRange(new DataColumn[8] { new DataColumn("Salutation"), new DataColumn("FullName"), new DataColumn("Email"), new DataColumn("MobileNumber"), new DataColumn("Date"), new DataColumn("Gender"), new DataColumn("Password"), new DataColumn("ConfirmPassword") });
            var expData = from exp in this._db.UserDetails.Take(20) select exp;
            foreach (var exp in expData)
            {
                td.Rows.Add(exp.Salutation, exp.FullName, exp.Email, exp.MobileNumber, exp.Date, exp.Gender, exp.Password, exp.ConfirmPassword);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(td);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }

        }

    }
}



