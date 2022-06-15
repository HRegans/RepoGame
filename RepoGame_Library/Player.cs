namespace RepoGame_Library;
public class Player
{
    public List<string> Inventory {get; set;}

    public double Money {get; set;}

    public Player() {
        Inventory = new List<string>();
        Money = 250.00;
    }
    public Player(params string[] items) {
        Inventory = new List<string>();
        foreach (string item in items) {
            Inventory.Add(item);
        }
        Money = 250.00;
    }
    public bool CheckPrice(double price) {
        if (price > Money) {
            return false;
        } else {
            return true;
        }
    }

    public void checkInventory(){
        Console.WriteLine($"Current player money is {Money:C2}");
        int Counter = 1;
        foreach (string Item in Inventory)
        {
            Console.WriteLine($"{Counter} {Item}");
            Counter ++;
        }
    }
}