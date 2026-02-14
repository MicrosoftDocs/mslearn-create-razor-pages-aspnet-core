using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        // The [BindProperty] attribute is used to bind the NewPizza property to the Razor page.
        // When an HTTP POST request is made, the NewPizza property will be populated with the user's input.
        [BindProperty]
        // The default! keyword is used to initialize the NewPizza property to null.
        // This prevents the compiler from generating a warning about the NewPizza property being uninitialized.
        public Pizza NewPizza { get; set; } = default!;

        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get;set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost()
        {
            // The ModelState.IsValid property is used to determine if the user's input is valid.
            // The validation rules are inferred from attributes (such as Required and Range) on the Pizza class in
            // Models\Pizza.cs.
            if (!ModelState.IsValid || NewPizza == null)
            {
                // If the user's input is invalid, the Page method is called to re-render the page.
                return Page();
            }

            // The NewPizza property is used to add a new pizza to the _service object.
            _service.AddPizza(NewPizza);

            // The RedirectToAction method is used to redirect the user to the Get page handler, which will re-render
            // the page with the updated list of pizzas.
            return RedirectToAction("Get");
        }

        // The OnPostDelete method is called when the user clicks the Delete button for a pizza.
        // The page knows to use this method because the asp-page-handler attribute on the Delete button in
        // Pages\PizzaList.cshtml is set to Delete.
        // The id parameter is used to identify the pizza to delete.
        // The id parameter is bound to the id route value in the URL. This is accomplished with the asp-route-id
        // attribute on the Delete button in Pages\PizzaList.cshtml.
        public IActionResult OnPostDelete(int id)
        {
            // The DeletePizza method is called on the _service object to delete the pizza.
            _service.DeletePizza(id);

            // The RedirectToAction method is used to redirect the user to the Get page handler, which will re-render
            // the page with the updated list of pizzas.
            return RedirectToAction("Get");
        }
    }
}
