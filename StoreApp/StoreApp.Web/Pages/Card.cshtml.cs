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
        public CardModel(IStoreRepository repository, Card cartService)
        {
            _repository = repository;
            Card = cartService;
        }
        public Card? Card { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost(int id)
        {
            var product = _repository.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                Card?.AddItem(product, 1);
            }

            return RedirectToPage("/Card");
        }

        public IActionResult OnPostRemove(int id)
        {
            Card?.RemoveItem(Card?.Items.First(p => p.Product.Id == id).Product!);
            return RedirectToPage("/Card");
        }
    }
}
