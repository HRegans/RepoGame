using RepoGame_Library;
public class Program {
    public static void Main() {
        Console.WriteLine("~~~~ Welcome to Money Grubbers Express ~~~~");

        // Intro description...
        // System.Console.WriteLine("");

        System.Console.WriteLine("* Objective is to progress through each town and buy/sell " + 
        "at the merchants in order to get the most money at the final destination.\n" + 
        "* Each of the merchants will have a unique personality, so they may give you better or worse deals.\n" + 
        "* Check out the markets to get a read on their moods.\n" + 
        "* You will see a list of numbered options below, these will control your actions throughout the game.");

        // Game Setup
        Player PlayerOne = new Player();
        PlayerOne.Inventory.Add("Bicycle");

        // Each city has new market of merchants -- need to create new market each time we travel
        // Possible mechanic -- 'cash-out' option at the end, where we get raw values of our remaining items, instead of selling them
        List<Merchant> Market = CreateMerchants();
        Market[0].Inventory.Add("Television");
        Market[1].Inventory.Add("Radio");
        Market[2].Inventory.Add("Xbox");

        // Universal -- items will have global prices, so catalog should only to be made once
        // Or, items' values change depending on city?
        List<(string,double)> Catalog = new List<(string, double)>();
        Catalog.Add(("Television" , 50.0));
        Catalog.Add(("Radio" , 35.0));
        Catalog.Add(("Xbox" , 350.0));
        Catalog.Add(("Bicycle" , 85.0));

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
            Console.Clear();
            Console.WriteLine("Checking inventory...");
            PlayerOne.checkInventory();
            Console.ReadKey();
            break;
            case "2":
            Console.WriteLine("Checking the local markets...");
            checkLocalMarkets(Market);
            Console.ReadKey();
            break;
            case "3":
            Console.Clear();
            Console.WriteLine("Checking out...");
            checkOut(Market, PlayerOne, Catalog);
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
        Merchant Merchant1 = new Merchant("Gary");
        Merchant1.Mood = Enum.Parse<BarterMood>("Neutral");
        Merchant Merchant2 = new Merchant("Abdul");
        Merchant2.Mood = Enum.Parse<BarterMood>("Stingy");
        Merchant Merchant3 = new Merchant("Christine");
        Merchant3.Mood = Enum.Parse<BarterMood>("Generous");
        result.Add (Merchant1);
        result.Add (Merchant2);
        result.Add (Merchant3);
        return result;
    }

    public static void ShowMerch(List<Merchant> Market) {
        foreach (Merchant vendor in Market) {
            System.Console.WriteLine($"{vendor.Name}'s stock:");
            if (vendor.Inventory.Count == 0) {
                System.Console.WriteLine("- Empty");
            } else {
                vendor.checkInventory();
            }
        }
    }

    public static void MakeSalePlayer(Player p, Merchant m, string itemLabel, List<(string label, double basePrice)> catalog) {
        
        (string label, double price) itemForSale = catalog.Find(item => item.label == itemLabel);
        double price = itemForSale.price * (1 + m.SellMarkup);

        if ( !m.Inventory.Contains(itemLabel) ) {
            System.Console.WriteLine("Merchant does not have the item.");
            return;
        }

        if (p.CheckPrice(price)) {
            p.Money -= price;
            m.Money += price;
            m.Inventory.Remove(itemLabel);
            p.Inventory.Add(itemLabel);
        } else {
            Console.WriteLine("Price is not good.");
        }
        
    }

    public static void MakeSaleMerchant(Merchant m, Player p, string itemLabel, List<(string label, double basePrice)> catalog) {
        
        (string label, double price) itemForSale = catalog.Find(item => item.label == itemLabel);
        double price = itemForSale.price * (1 - m.BuyMarkdown);

        if ( !p.Inventory.Contains(itemLabel) ) {
            System.Console.WriteLine("Player does not have the item.");
            return;
        }

        if (m.CheckPrice(price)) {
            m.Money -= price;
            p.Money += price;
            p.Inventory.Remove(itemLabel);
            m.Inventory.Add(itemLabel);
        }else{Console.WriteLine("Price is not good.");
        }
    }

    public static void checkLocalMarkets(List<Merchant> Market)
    {
        // Too direct, give player hints instead
        foreach (Merchant vendor in Market){
            System.Console.WriteLine($"{vendor.Name} is {vendor.Mood}.");
        }
    }

    public static void checkOut(List<Merchant> Market, Player PlayerOne, List<(string, double)> catalog)
    {
        System.Console.WriteLine("Do you want to buy or sell? (Enter 1 for 'buy' or 2 for 'sell')");
        string? buyOrSell = Console.ReadLine();

        if (buyOrSell == "1") {
            System.Console.WriteLine("What would you like to buy?");
            // Display options
            ShowMerch(Market);
            string? product = Console.ReadLine();
            if (product == null || product == "") {
                System.Console.WriteLine("Invalid product name. Returning to menu...");
                return;
            }
            Merchant? seller = Market.Find(vendor => vendor.Inventory.Contains(product));
            MakeSalePlayer(PlayerOne, seller, product, catalog);
        } else if (buyOrSell == "2") {
            System.Console.WriteLine("What would you like to sell?");
            string? product = Console.ReadLine();
            Console.WriteLine("Which Merchant would you like to sell to?");
            string? name = Console.ReadLine();
            Merchant? buyer = Market.Find(vendor => vendor.Name == name);
            MakeSaleMerchant(buyer, PlayerOne, product, catalog);
        } else {
            System.Console.WriteLine("Invalid selection. Returning to menu...");
        }
    }
}