using System.Text.Json.Serialization;
using StoreApp.Data.Concrete;
using StoreApp.Web.Helpers;

namespace StoreApp.Web.Models;

public class SessionCard : Card
{
    public static Card GetCard(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
        SessionCard card = session?.GetJson<SessionCard>("card") ?? new SessionCard();
        card.Session = session;
        return card;
    }

    [JsonIgnore]
    public ISession? Session { get; set; }

    public override void AddItem(Product product, int quantity)
    {
        base.AddItem(product, quantity);
        // session ekle
        Session?.SetJson("card", this);
    }

    public override void RemoveItem(Product product)
    {
        base.RemoveItem(product);
        Session?.SetJson("card", this);
    }

    public override void Clear()
    {
        base.Clear();
        Session?.Remove("card");
    }


}
