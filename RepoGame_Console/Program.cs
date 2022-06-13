using RepoGame_Library;
public class Program {
    public static void Main() {
        Console.WriteLine("Welcome to Money Grubbers Express!!! Enter the ______");
    bool continueGame=true;
    do {
        Console.WriteLine("Press 1 to Check Inventory & Money");
        Console.WriteLine("Press 2 to Checkout the local markets");
        Console.WriteLine("Press 3 to Make a purchase or a sale");
        Console.WriteLine("Press 4 to Quit Game");
        string? var=Console.ReadLine();

        switch (var)
        {
            case "1":
            Console.WriteLine("Checking inventory...");
            checkInventory();
            break;
            case "2":
            Console.WriteLine("Checking the local markets...");
            checkLocalmarkets();
            break;
            case "3":
            Console.WriteLine("Checking out...");
            checkOut();
            break;
            case "4":
            Console.WriteLine("Quitting Game...");
            continueGame=false;
            break;
            default:
            continueGame=false;
            Console.WriteLine("");
            break;
        }} while(continueGame);
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

    public void checkInventory(){}
    public void checkLocalmarkets(){}
    public void checkOut(){}
}