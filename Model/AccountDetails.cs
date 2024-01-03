using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RegUser.Model
{
    public class AccountDetails
    {

        [BindProperty]
        public string Salutation { get; set; } 
        [BindProperty]
        [Required(ErrorMessage = "The Full Name is Required")]
        public string FullName { get; set; } 
        [Key]
        [BindProperty]
        [Required(ErrorMessage = "The Email is Required")]
        public string Email { get; set; } 
        [BindProperty]
        [Required(ErrorMessage = "The Mobile Number is Required")]
        public string MobileNumber { get; set; } 
        [BindProperty]
        [Required(ErrorMessage = "The Gender is Required")]
        public string Gender { get; set; } 
        [BindProperty]
        [Required(ErrorMessage = "The Date is Required")]
        public DateTime Date { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password is Required")]
        public string Password { get; set; } 
        [BindProperty]
        [Required(ErrorMessage = "The Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password did not match")]
        public string ConfirmPassword { get; set; } 

        [BindProperty]
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Accept the Terms and Condition")]
        public bool Check { get; set; }
        public DateTime ModDate { get; set; } = DateTime.Now;
        //public bool Succeeded { get; internal set; }
    }
}
