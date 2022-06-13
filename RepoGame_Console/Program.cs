using RepoGame_Library;
public class Program {
    public static void Main() {
        Console.WriteLine("Welcome to Money Grubbers Express!!! Enter the ______");
        Player PlayerOne = new Player();
        PlayerOne.Inventory.Add("Bicycle");
        List<Merchant> Market = CreateMerchants();
        Market[0].Inventory.Add("Television");
        List<(string,double)> Catalog = new List<(string, double)>();
        Catalog.Add(("Television" , 50.0));

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
            checkInventory(PlayerOne);
            break;
            case "2":
            Console.WriteLine("Checking the local markets...");
            checkLocalMarkets(Market);
            break;
            case "3":
            Console.WriteLine("Checking out...");
            checkOut(Market, PlayerOne);
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
public static List<Merchant> CreateMerchants () {
    List<Merchant> result = new List<Merchant>();
    Merchant Merchant1 = new Merchant();
    Merchant Merchant2 = new Merchant();
    Merchant Merchant3 = new Merchant();
    Merchant3.Mood = Enum.Parse<BarterMood>("Generous");
    result.Add (Merchant1);
    result.Add (Merchant2);
    result.Add (Merchant3);
    return result;


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

    public static void checkInventory(Player p){
        Console.WriteLine($"Current money is {p.Money:C2}");
        foreach (string Item in p.Inventory)
        {
            Console.WriteLine($"{Item}");
        }
    }
    public static void checkLocalMarkets(List<Merchant> Market)
    {
        foreach (Merchant Vendor in Market){
            Console.WriteLine($"{Vendor.Mood}");

        }

    }
    public static void checkOut(List<Merchant> Market, Player PlayerOne)
    {
    
    }
}