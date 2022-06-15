using RepoGame_Library;
public class Program {
    public static void Main() {
        Console.BackgroundColor=ConsoleColor.DarkBlue;
        Console.WriteLine("~~~~ Welcome to Money Grubbers Express ~~~~");

        Console.WriteLine("**********************************************************************************************************************************");
        Console.WriteLine(@"*      ________   _____   _______     ____   _______       _____ ______  ____ ___ ______   ____     ____  _____   ________       *");
        Console.WriteLine(@"*     |    |   | /     \ |       \   /  __] |   |   |     /     ||     \ |   |   ||     \ |     \  /  __]|     \ /     __/       *");
        Console.WriteLine(@"*     |  _   _ ||       ||   _    | /  [__  |   |   |    |   ___||   D  )    |   ||   O  )|   O  )/  [_  |   D  )(     \_        *");
        Console.WriteLine(@"*     |   \_/  ||       ||    |   ||     _] |   ~   |    |  |   ||     / |   |   ||      ||      ||   _] |      \ \__    |       *");
        Console.WriteLine(@"*     |    |   ||       ||    |   ||     [_ | ___,  |    |  |__ ||   _  \|   :   ||   O  ||   O  ||  [__ |   _   \ /     |       *");
        Console.WriteLine(@"*     |__  | __| \_____/ | __ | __||_______|| _____/     |____,_||__| \__\ ____,_||______||_____ ||_____||___| \_| \____ |       *");
        Console.WriteLine(@"*                                                                                                                                *");
        Console.WriteLine("**********************************************************************************************************************************");

        System.Console.WriteLine("Money Grubber Express is a one player game where the player must sell or buy items from merchants in each city they travel to. " + 
        "\nThey will meet different merchants with different personalities that will impact their buying and selling...");

        System.Console.WriteLine("* Objective is to progress through each town and buy/sell " + 
        "at the merchants in order to get the most money at the final destination.\n" + 
        "* Each of the merchants will have a unique personality, so they may give you better or worse deals.\n" + 
        "* Check out the markets to get a read on their moods.\n" + 
        "* You will see a list of numbered options below, these will control your actions throughout the game.\n"+
        "* Looks like there's nothing in this city. Travel to the next one to start your journey!");

        // 850 - 650 - 450
        System.Console.WriteLine("\n*******************************");
        System.Console.WriteLine("*         Ranking System      *");
        System.Console.WriteLine("*******************************");
        System.Console.WriteLine("*      Gold >>>> $850.00      *");
        System.Console.WriteLine("*      Silver >>>> $650.00    *");
        System.Console.WriteLine("*      Bronze >>>> $450.00    *");
        System.Console.WriteLine("*******************************\n");

        // Game Setup
        Player PlayerOne = new Player();
        PlayerOne.Inventory.Add("Bicycle");
        PlayerOne.Inventory.Add("Xbox");
        PlayerOne.Inventory.Add("Tent");
        PlayerOne.Inventory.Add("Sunglasses");
        List<Merchant> CurrentCity=new List<Merchant>();
        
        // Universal -- items will have global prices, so catalog should only to be made once
        List<(string,double)> Catalog = new List<(string, double)>{
            ("Television" , 50.0), ("Radio" , 35.0), ("Xbox" , 350.0), ("Bicycle" , 85.0),
            ("Tent" , 65.0), ("Cellphone" , 450.0), ("Nike Running Shoes" , 120.0), ("Sleeping Bag" , 85.00),
            ("Rare Pokemon Cards" , 600.0), ("Sunglasses" , 50.0), ("Skateboard" , 180.0), ("Flashlight" , 15.0),
            ("Telescope" , 350.0), ("Backpack" , 65.0),
        };
        
        int TimesTraveled=0;
        bool continueGame=true;
        do {
        Console.WriteLine("\nPress 1 to Check Inventory & Money");
        Console.WriteLine("Press 2 to Checkout the local markets");
        Console.WriteLine("Press 3 to Make a purchase or a sale");
        Console.WriteLine("Press 4 to Move to next city");
        Console.WriteLine("Press 5 to Quit Game\n");
        string? playerCommand=Console.ReadLine();
        switch (playerCommand)
        {
            case "1":
            Console.Clear();
            Console.WriteLine("Checking inventory...");
            PlayerOne.checkInventory();
            Console.WriteLine("Press 'Enter' to Continue");
            Console.ReadKey();
            break;
            case "2":
            Console.WriteLine("Checking the local markets...");
            checkLocalMarkets(CurrentCity);
            Console.WriteLine("Press 'Enter' to Continue");
            Console.ReadKey();
            break;
            case "3":
            Console.Clear();
            Console.WriteLine("Checking out...");
            checkOut(CurrentCity, PlayerOne, Catalog);
            Console.WriteLine("Press 'Enter' to Continue");
            Console.ReadKey();
            break;
            case "4":
            Console.Clear();
            Console.WriteLine("Traveling to next city...");
            switch(TimesTraveled) 
            {
            case 0:
            CurrentCity=CreateCityOne();
            System.Console.WriteLine("Arrived at Austin, Texas.");
            break;
            case 1:
            CurrentCity=CreateCityTwo();
            System.Console.WriteLine("Now in New York City.");
            break;
            case 2:
            CurrentCity=CreateCityThree();
            System.Console.WriteLine("Made it to San Francisco, California.");
            break;
            default:
            Console.WriteLine("Sorry, no airlines are running right now. Let's see how much profit you've made...");
            CashOutPlayer(PlayerOne, Catalog);
            TallyFinalScore(PlayerOne.Money);
            System.Console.WriteLine("--- Game Over ---");
            continueGame = false;
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
            Console.WriteLine("Invalid input. Quitting game...");
            break;
        }} while(continueGame);
    }
    public static List<Merchant> CreateCityOne () {
        List<Merchant> result = new List<Merchant>();
        Merchant Merchant1 = new Merchant("Gary");
        Merchant1.Mood = Enum.Parse<BarterMood>("Neutral");
        Merchant1.Inventory.Add("Flashlight");
        Merchant1.Inventory.Add("Bicycle");
        Merchant1.Inventory.Add("Radio");

        Merchant Merchant2 = new Merchant("Abdul");
        Merchant2.Mood = Enum.Parse<BarterMood>("Stingy");
        Merchant2.Inventory.Add("Cellphone");
        Merchant2.Inventory.Add("Sunglasses");

        Merchant Merchant3 = new Merchant("Christine");
        Merchant3.Mood = Enum.Parse<BarterMood>("Generous");
        Merchant3.Inventory.Add("Skateboard");
        Merchant3.Inventory.Add("Sleeping Bag");

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
        Merchant1.Inventory.Add("Backpack");

        Merchant Merchant2 = new Merchant("Laura");
        Merchant2.Mood = Enum.Parse<BarterMood>("Stingy");
        Merchant2.Inventory.Add("Radio");
        Merchant2.Inventory.Add("Sleeping Bag");

        result.Add (Merchant1);
        result.Add (Merchant2);
        return result;
    }

    public static List<Merchant> CreateCityThree () {
        List<Merchant> result = new List<Merchant>();
        Merchant Merchant1 = new Merchant("Terra");
        Merchant1.Mood = Enum.Parse<BarterMood>("Stingy");
        Merchant1.Inventory.Add("Rare Pokemon Cards");
        Merchant1.Inventory.Add("Telescope");

        result.Add (Merchant1);
        return result;
    }
    public static void ShowMerch(List<Merchant> Market, List<(string, double)> catalog) {
        foreach (Merchant vendor in Market) {
            System.Console.WriteLine($"\n{vendor.Name}'s stock:");
            if (vendor.Inventory.Count == 0) {
                System.Console.WriteLine("- Empty\n");
            } else {
                vendor.checkInventory(catalog);
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
            Console.WriteLine($"Player Money: {p.Money:C2}");
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
            Console.WriteLine($"Player Money: {p.Money:C2}");
        }else{Console.WriteLine("Price is not good.");
        }
    }

    public static void checkLocalMarkets(List<Merchant> Market)
    {
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
            ShowMerch(Market, catalog);

            string? product = Console.ReadLine();
            
            if (product == null || product == "") {
                System.Console.WriteLine("Invalid product entered. Returning to menu...");
                return;
            }

            Merchant? seller = Market.Find(vendor => vendor.Inventory.Contains(product));
            if (seller == null || seller == default) {
                System.Console.WriteLine("No merchant has that item.");
                return;
            }
            MakeSalePlayer(PlayerOne, seller, product, catalog);

        } else if (buyOrSell == "2") {
            System.Console.WriteLine("What would you like to sell?");
            Console.WriteLine("Enter the number of the item to select it.");
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
            if (buyer == null) {
                System.Console.WriteLine("No merchant found by that name.");
                return;
            }
            MakeSaleMerchant(buyer, PlayerOne, playerItem, catalog);
        } else {
            System.Console.WriteLine("Invalid selection. Returning to menu...");
            return;
        }
    }

    public static void CashOutPlayer(Player playerOne, List<(string label, double basePrice)> catalog) {
        System.Console.WriteLine("Cashing out your remaining inventory...");
        foreach (string itemLabel in playerOne.Inventory) {
            playerOne.Money += catalog.Find(item => item.label == itemLabel).basePrice;
        }
        System.Console.WriteLine($"You ended up with {playerOne.Money:C2}.");
    }

    public static void TallyFinalScore(double playerMoney) {
        if (playerMoney >= 850.0) {
            System.Console.WriteLine("#### Congrats! Gold Medal earned! ####");
        } else if (playerMoney >= 650.0 && playerMoney < 850.0) {
            System.Console.WriteLine("*** Not bad. Silver Medal earned! ***");
        } else if (playerMoney >= 450.0 && playerMoney < 650.0) {
            System.Console.WriteLine("-- Chump change. Bronze Medal earned. --");
        } else {
            System.Console.WriteLine("_ Better luck next time. _");
        }
    }
}