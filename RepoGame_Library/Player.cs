namespace RepoGame_Library;
public class Player
{
    public List<string> Inventory {get; set;}

    public double Money {get; set;}

    public Player() {
        Inventory = new List<string>();
        Money = 100.00;
    }

    public bool CheckPrice(double price) {
        if (price > Money) {
            return false;
        } else {
            return true;
        }
    }
}