namespace RepoGame_Library;
public class Merchant : Player
{
    public string name {get; set;}
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
    }

    public Merchant(string mood) : base() {
        try {
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