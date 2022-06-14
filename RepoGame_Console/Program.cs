using RepoGame_Library;
public class Program {
    public static void Main() {
        Console.WriteLine("~~~~ Welcome to Money Grubbers Express ~~~~");

        System.Console.WriteLine("Money Grubber Express is the one player game where a player must sell or buy items from merchants in each city he/she travels to. " + 
        "\nHe/she will meet different merchants with different moods/personalities that will impact his buying and selling...");

        System.Console.WriteLine("* Objective is to progress through each town and buy/sell " + 
        "at the merchants in order to get the most money at the final destination.\n" + 
        "* Each of the merchants will have a unique personality, so they may give you better or worse deals.\n" + 
        "* Check out the markets to get a read on their moods.\n" + 
        "* You will see a list of numbered options below, these will control your actions throughout the game.");

        // Game Setup, Three levels
        Player PlayerOne = new Player();
        PlayerOne.Inventory.Add("Bicycle");
        List<Merchant> CurrentCity=new List<Merchant>();

        // Each city has new market of merchants -- need to create new market each time we travel
        // Possible mechanic -- 'cash-out' option at the end, where we get raw values of our remaining items, instead of selling them
        // List<Merchant> Market = CreateMerchants();
        // Market[0].Inventory.Add("Television");
        // Market[1].Inventory.Add("Radio");
        // Market[2].Inventory.Add("Xbox");
        // Market[3].Inventory.Add("Tent")
        // Market[4].Inventory.Add("Cellphone")
        // Market[5].Inventory.Add("Nike Running Shoes")
        // Market[6].Inventory.Add("Rare Pokemon Cards")
        // Market[7].Inventory.Add("Sunglasses")
        // Market[8].Inventory.Add("Skateboard")
        // Market[9].Inventory.Add("Flashlight")
        
        // Universal -- items will have global prices, so catalog should only to be made once
        // Or, items' values change depending on city?
        List<(string,double)> Catalog = new List<(string, double)>();
        Catalog.Add(("Television" , 50.0));
        Catalog.Add(("Radio" , 35.0));
        Catalog.Add(("Xbox" , 350.0));
        Catalog.Add(("Bicycle" , 85.0));
        // Catalog.Add(("Tent" , 65.0));
        Catalog.Add(("Cellphone" , 650.0));
        Catalog.Add(("Nike Running Shoes" , 55.0));
        Catalog.Add(("Rare Pokemon Cards" , 135.0));
        // Catalog.Add(("Sunglasses" , 50.0));
        Catalog.Add(("Skateboard" , 125.0));
        Catalog.Add(("Flashlight" , 15.0));
        
        int TimesTraveled=0;
        bool continueGame=true;
        do {
        Console.WriteLine("Press 1 to Check Inventory & Money");
        Console.WriteLine("Press 2 to Checkout the local markets");
        Console.WriteLine("Press 3 to Make a purchase or a sale");
        Console.WriteLine("Press 4 to Move to next city");
        Console.WriteLine("Press 5 to Quit Game");
        string? var=Console.ReadLine();
        switch (var)
        {
            case "1":
            Console.Clear();
            Console.WriteLine("Checking inventory...");
            PlayerOne.checkInventory();
            // Console.ReadKey();
            break;
            case "2":
            Console.WriteLine("Checking the local markets...");
            checkLocalMarkets(CurrentCity);
            // Console.ReadKey();
            break;
            case "3":
            Console.Clear();
            Console.WriteLine("Checking out...");
            checkOut(CurrentCity, PlayerOne, Catalog);
            // Console.ReadKey();
            break;
            case "4":
            Console.Clear();
            Console.WriteLine("Traveling to next city...");
            switch(TimesTraveled) 
            {
            case 0:
            CurrentCity=CreateCityOne();
            break;
            case 1:
            CurrentCity=CreateCityTwo();
            break;
            case 2:
            CurrentCity=CreateCityThree();
            break;
            default:
            Console.WriteLine("Unable to travel");
            break;
            }
            TimesTraveled ++;
            break;
            case "5":
            Console.WriteLine("Quitting Game...");
            continueGame=false;
            break;
            default:
            continueGame=false;
            Console.WriteLine("");
            break;
        }} while(continueGame);
    }
    public static List<Merchant> CreateCityOne () {
        List<Merchant> result = new List<Merchant>();
        Merchant Merchant1 = new Merchant("Gary");
        Merchant1.Mood = Enum.Parse<BarterMood>("Neutral");
        Merchant1.Inventory.Add("Flashlight");

        Merchant Merchant2 = new Merchant("Abdul");
        Merchant2.Mood = Enum.Parse<BarterMood>("Stingy");
        Merchant2.Inventory.Add("Cellphone");

        Merchant Merchant3 = new Merchant("Christine");
        Merchant3.Mood = Enum.Parse<BarterMood>("Generous");
        Merchant3.Inventory.Add("Skateboard");

        result.Add (Merchant1);
        result.Add (Merchant2);
        result.Add (Merchant3);
        return result;
    }

    public static List<Merchant> CreateCityTwo () {
        List<Merchant> result = new List<Merchant>();
        Merchant Merchant1 = new Merchant("Jonathan");
        Merchant1.Mood = Enum.Parse<BarterMood>("Generous");
        Merchant1.Inventory.Add("Nike Running Shoes");

        Merchant Merchant2 = new Merchant("Laura");
        Merchant2.Mood = Enum.Parse<BarterMood>("Stingy");
        Merchant2.Inventory.Add("Radio");

        result.Add (Merchant1);
        result.Add (Merchant2);
        return result;
    }

    public static List<Merchant> CreateCityThree () {
        List<Merchant> result = new List<Merchant>();
        Merchant Merchant1 = new Merchant("Terra");
        Merchant1.Mood = Enum.Parse<BarterMood>("Stingy");
        Merchant1.Inventory.Add("Rare Pokemon Cards");

        result.Add (Merchant1);
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
        // Generous: Console.WriteLine("This Merchant went on a nice date last night. I bet he's in a good mood.");
        // Neutral: Console.WriteLine("This Merchant has had a few good customers and a few bad customers today.");
        // Stingy: Console.WriteLine("Oh no! This Merchant just spilled a cup of coffee on themselves.");
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
            PlayerOne.checkInventory();
            string? product = Console.ReadLine();
            string playerItem;
            if (product != "" || product != null) {
            int index = Convert.ToInt32(product);
            playerItem = PlayerOne.Inventory[index -1];
            } else {
            Console.WriteLine("Item not found.");
            return;
            }

            Console.WriteLine("Which Merchant would you like to sell to?");
            foreach (Merchant Vendor in Market) 
            {
                Console.WriteLine($"{Vendor.Name}");
            }

            string? name = Console.ReadLine();
            Merchant? buyer = Market.Find(vendor => vendor.Name == name);
            MakeSaleMerchant(buyer, PlayerOne, playerItem, catalog);
        } else {
            System.Console.WriteLine("Invalid selection. Returning to menu...");
        }
    }
}