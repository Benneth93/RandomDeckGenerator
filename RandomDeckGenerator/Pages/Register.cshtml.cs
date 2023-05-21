using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RandomDeckGenerator.Services;

namespace RandomDeckGenerator.Pages;

public class Register : PageModel
{
    public class RegistrationModel
    {
        [Required] 
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    [BindProperty]
    public RegistrationModel credentials { get; set; } = new();

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var userRegistrationResponse = await UserService.Register(credentials.Username, credentials.Password);
            
            if (userRegistrationResponse.isSuccess)
            {
                HttpContext.Session.SetInt32("isLoggedIn", 1);
                HttpContext.Session.SetString("Username", userRegistrationResponse.User.Username);
                HttpContext.Session.SetString("currentDataSet", JsonConvert.SerializeObject(userRegistrationResponse.User.StoredList));
                
                return RedirectToPage("/DeckInput");
            }
            else ModelState.AddModelError("",userRegistrationResponse.Message);
        }

        return null;
    }
}