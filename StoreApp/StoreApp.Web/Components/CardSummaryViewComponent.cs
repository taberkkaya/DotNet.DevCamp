using Microsoft.AspNetCore.Mvc;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components;

public class CardSummaryViewComponent : ViewComponent
{
    private Card card;

    public CardSummaryViewComponent(Card cardService)
    {
        card = cardService;
    }

    public IViewComponentResult Invoke()
    {
        return View(card);
    }
}
