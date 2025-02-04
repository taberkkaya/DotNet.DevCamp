using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Data.Abstract;
using StoreApp.Web.Helpers;
using StoreApp.Web.Models;

namespace StoreApp.Web.Pages
{
    public class CardModel : PageModel
    {
        private IStoreRepository _repository;
        public CardModel(IStoreRepository repository)
        {
            _repository = repository;
        }
        public Card? Card { get; set; }
        public void OnGet()
        {
            Card = HttpContext.Session.GetJson<Card>("card") ?? new Card();
        }

        public IActionResult OnPost(int id)
        {
            var product = _repository.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                Card = HttpContext.Session.GetJson<Card>("card") ?? new Card();
                Card.AddItem(product, 1);
                HttpContext.Session.SetJson("card", Card);
            }

            return RedirectToPage("/Card");
        }

        public IActionResult OnPostRemove(int id)
        {
            Card = HttpContext.Session.GetJson<Card>("card") ?? new Card();
            var product = Card?.Items.First(p => p.Product.Id == id).Product;
            Card?.RemoveItem(product!);
            HttpContext.Session.SetJson("card", Card!);
            return RedirectToPage("/Card");
        }
    }
}
