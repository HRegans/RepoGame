namespace RepoGame_Library;
public class Merchant : Player
{
    public string? Name {get; set;}
    public BarterMood Mood { get; set;}

    public double SellMarkup { get {
    return Mood switch {
        BarterMood.Generous => 0.1,
        BarterMood.Neutral => 0.25,
        BarterMood.Stingy => 0.4,
        _ => 0.25
    };
    }}

    public double BuyMarkdown { get {
        return Mood switch {
            BarterMood.Generous => 0.1,
            BarterMood.Neutral => 0.25,
            BarterMood.Stingy => 0.5,
            _ => 0.25
        };
    }}

    public Merchant() : base() {
        Mood = BarterMood.Neutral;
        Name = "John";
        Money = 10_000.00;
    }

    public Merchant(string name) : base() {
        Mood = BarterMood.Neutral;
        Name = name;
        Money = 10_000.00;
    }

    public Merchant(string name, string mood) : base() {
        try {
            if (name != null) {
                Name = name;
            } else {
                Name = "John";
            }
            Money = 10_000.00;
            Mood = Enum.Parse<BarterMood>(mood, true);
        } catch (Exception) {
            System.Console.WriteLine("Bad mood label. Setting to neutral.");
            Mood = BarterMood.Neutral;
        }
    }

    public void checkInventory(List<(string label, double basePrice)> catalog){
        int counter = 1;
        foreach (string Item in Inventory)
        {
            double price = catalog.Find(item => item.label == Item).basePrice;
            Console.WriteLine($"{counter} {Item} {price:C2}");
            counter++;
        }
    }
}

public enum BarterMood {
    Generous,
    Neutral,
    Stingy
}