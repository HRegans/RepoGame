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
    }

    public Merchant(string name) : base() {
        Mood = BarterMood.Neutral;
        Name = name;
    }

    public Merchant(string name, string mood) : base() {
        try {
            if (name != null) {
                Name = name;
            } else {
                Name = "John";
            }
            Mood = Enum.Parse<BarterMood>(mood, true);
        } catch (Exception) {
            System.Console.WriteLine("Bad mood label. Setting to neutral.");
            Mood = BarterMood.Neutral;
        }
    }
}

public enum BarterMood {
    Generous,
    Neutral,
    Stingy
}