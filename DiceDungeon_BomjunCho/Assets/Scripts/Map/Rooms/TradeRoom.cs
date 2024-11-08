using UnityEngine;
//In this room, user can exchange their hp to dragon sword
public class TradeRoom : Room
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Trade Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Trade Room.");
        }
    }

    /*
    public override void OnRoomEntered(Player user)
    {
        Debug.Log("You entered Trade Room.");

        Debug.Log("You can see a demon on center of this room. He looks friendly\nPress anykey");
    }

    public override void OnRoomSearched(Player user)
    {
        Debug.Log("You are searching Trade Room.");
        //user.ShowPlayerState();
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
        //user.ShowPlayerState();
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving from Trade Room");
    }
    */
}




