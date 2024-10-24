using UnityEngine;
//In this room, user can exchange their hp to dragon sword
public class TradeRoom : Room
{
    public override string ToString()
    {
        return "Trade Room"; //representation
    }
    public override void OnRoomEntered(Player user)
    {
        Debug.Log("--------------------------------------------------------------------------You entered Trade Room.");

        Debug.Log("You can see a demon on center of this room. He looks friendly\nPress anykey");
    }

    public override void OnRoomSearched(Player user)
    {
        Debug.Log("--------------------------------------------------------------------------You are searching Trade Room.");
        user.ShowPlayerState();
        bool validAnswer = false;
        while (!validAnswer)
        {
            Debug.Log("Do you want to trade? (yes or no)");
            //string answer = Console.ReadLine() ?? string.Empty ;
            string answer = "yes";
                    
            if (answer == "yes")
            {
                Debug.Log("You lost -5 HP points");
                Debug.Log("You got one item.");
                user.hp -= 5;
                user.inventory.TradeItemAdd(); // user get dragon sword
                validAnswer = true;
            }
            else if (answer == "no")
            {
                validAnswer = true;
                Debug.Log("Trade demon looks sad.");
            }
            else
            {
                Debug.Log("Please answer correctly.(yes or no)");
            }
        }
        user.ShowPlayerState();
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("--------------------------------------------------------------------------You are leaving from Trade Room");
    }
}




