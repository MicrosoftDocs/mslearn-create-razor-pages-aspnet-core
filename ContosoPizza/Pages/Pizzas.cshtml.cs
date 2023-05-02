using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoPizza.Data;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzasModel : PageModel
    {
        private readonly PizzaService _service;

        public PizzasModel(PizzaService service)
        {
            _service = service;
        }

        public IList<Pizza> PizzaList { get;set; } = default!;

        [BindProperty]
        public Pizza Pizza { get; set; } = default!;

        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Pizza == null)
            {
                return Page();
            }

            _service.AddPizza(Pizza);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
