using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RandomDeckGenerator.Models;
using RandomDeckGenerator.Services;

namespace RandomDeckGenerator.Pages;


public class Login : PageModel
{
    public class LoginModel
    {
        [Required] 
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    [BindProperty]
    public LoginModel credentials { get; set; } = new();

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var user = await UserService.Login(credentials.Username, credentials.Password);
            
            if (user != null)
            {
                HttpContext.Session.SetInt32("isLoggedIn", 1);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("currentDataSet", JsonConvert.SerializeObject(user.StoredList));
            }
            else ModelState.AddModelError("","Wrong username and password");
        }

        return null;
    }
}