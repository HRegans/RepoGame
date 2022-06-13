using RepoGame_Library;
public class Program {
    public static void Main() {

    }

    public static void MakeSale(Player p, Merchant m, string itemLabel, List<(string label, double basePrice)> catalog) {
        
        (string label, double price) itemForSale = catalog.Find(item => item.label == itemLabel);
        double price = itemForSale.price * (1 + m.SellMarkup);
        if (p.CheckPrice(price)) {
            p.Money -= price;
            m.Money += price;
            m.Inventory.Remove(itemLabel);
            p.Inventory.Add(itemLabel);
        }
    }
}