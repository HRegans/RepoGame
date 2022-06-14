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