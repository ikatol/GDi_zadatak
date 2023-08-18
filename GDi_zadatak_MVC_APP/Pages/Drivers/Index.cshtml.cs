using GDi_zadatak_MVC_APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GDi_zadatak_MVC_APP.Pages.Drivers {
    public class IndexModel : PageModel {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty] public Driver Driver { get; set; }

        public void OnGet() {
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync("https://localhost:7021", Driver);
            if (response.IsSuccessStatusCode) {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
