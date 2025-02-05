using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class OrderController : Controller
{
    private IOrderRepository _orderRepository;
    private Card card;
    public OrderController(Card cardService, IOrderRepository orderRepository)
    {
        card = cardService;
        _orderRepository = orderRepository;
    }

    public IActionResult CheckOut()
    {
        return View(new OrderModel() { Card = card });
    }

    [HttpPost]
    public IActionResult CheckOut(OrderModel model)
    {
        if (card.Items.Count == 0)
        {
            ModelState.AddModelError("", "Sepetinizde ürün yok.");
        }

        if (ModelState.IsValid)
        {
            Order order = new Order
            {
                Name = model.Name,
                Email = model.Email,
                City = model.City,
                Phone = model.Phone,
                AddressLine = model.AddressLine,
                OrderDate = DateTime.Now,
                OrderItems = card.Items.Select(i => new OrderItem
                {
                    ProductId = i.Product.Id,
                    Price = (double)i.Product.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            _orderRepository.SaveOrder(order);
            card.Clear();
            return RedirectToPage("/Completed", new { OrderId = order.Id });
        }
        else
        {
            model.Card = card;
            return View(model);
        }
    }

}