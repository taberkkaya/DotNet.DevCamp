using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class OrderController : Controller
{
    private IOrderRepository _orderRepository;
    private StoreApp.Web.Models.Card card;
    public OrderController(StoreApp.Web.Models.Card cardService, IOrderRepository orderRepository)
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
                OrderItems = card.Items.Select(i => new StoreApp.Data.Concrete.OrderItem
                {
                    ProductId = i.Product.Id,
                    Price = (double)i.Product.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            model.Card = card;

            var payment = ProcessPaymet(model);

            if (payment.Status == "success")
            {
                _orderRepository.SaveOrder(order);
                card.Clear();
                return RedirectToPage("/Completed", new { OrderId = order.Id });
            }

            model.Card = card;
            return View(model);
        }
        else
        {
            model.Card = card;
            return View(model);
        }
    }

    private Payment ProcessPaymet(OrderModel model)
    {
        Options options = new Options();
        options.ApiKey = "sandbox-NjrQOfSoKltvKoWt5E7C1mEi5PQ3p0tg";
        options.SecretKey = "sandbox-HavRHw7EQcgyvdzL3s1lUrhiwQq4fIMb";
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = new Random().Next(111111111, 999999999).ToString();
        request.Price = model?.Card?.CalculateTotal().ToString();
        request.PaidPrice = model?.Card?.CalculateTotal().ToString();
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B67832";
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = model?.CardName;
        paymentCard.CardNumber = model?.CardNumber;
        paymentCard.ExpireMonth = model?.ExpirationMonth;
        paymentCard.ExpireYear = model?.ExpirationYear;
        paymentCard.Cvc = model?.Cvc;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = "Ataberk";
        buyer.Surname = "Kaya";
        buyer.GsmNumber = "+905350000000";
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        buyer.Ip = "85.34.78.112";
        buyer.City = "Istanbul";
        buyer.Country = "Turkey";
        buyer.ZipCode = "34732";
        request.Buyer = buyer;

        Address shippingAddress = new Address();
        shippingAddress.ContactName = "Jane Doe";
        shippingAddress.City = "Istanbul";
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        shippingAddress.ZipCode = "34742";
        request.ShippingAddress = shippingAddress;

        Address billingAddress = new Address();
        billingAddress.ContactName = "Jane Doe";
        billingAddress.City = "Istanbul";
        billingAddress.Country = "Turkey";
        billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        billingAddress.ZipCode = "34742";
        request.BillingAddress = billingAddress;

        List<BasketItem> basketItems = new List<BasketItem>();

        foreach (var item in model?.Card?.Items ?? Enumerable.Empty<CardItem>())
        {
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = item.Product.Id.ToString();
            firstBasketItem.Name = item.Product.Name;
            firstBasketItem.Category1 = "Category";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = item.Product.Price.ToString();
            basketItems.Add(firstBasketItem);
        }

        request.BasketItems = basketItems;

        Payment payment = Payment.Create(request, options);

        return payment;
    }
}