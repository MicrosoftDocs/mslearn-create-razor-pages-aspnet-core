using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel {
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get; set; } = default!;

        public PizzaListModel (PizzaService service) {
            _service = service;
        }

        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;

        public void OnGet () {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost () {
            if (!ModelState.IsValid || NewPizza == null) {
                return Page();
            }

            _service.AddPizza(NewPizza);
            
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id) {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
