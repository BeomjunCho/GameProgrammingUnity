using UnityEngine;

internal class GameController : MonoBehaviour
{
    public void ProgramStart()
    {
        //instance
        Player user = new Player();
        StartingRoom emptyRoom = new StartingRoom();

        //object
        Room currentRoom;

        //Main game loop
        void DiceDungeonGame(Map map)
        {
            while (user.hp > 0)
            {
                currentRoom = map.currentRoom;
                    
                if (currentRoom == map.lastRoom && user.hp > 0)
                {
                    break;
                }
                else if (user.hp <= 0)
                {
                    break;
                }
            }
        }

        //------------------Game Start---------------------

        Map map = new Map(); // execute intro() and catch the returned map instance
        if (map != null) // Check if map is null
        {
            currentRoom = map.currentRoom;
            DiceDungeonGame(map); // Main game loop
        }
        else
        {
            Debug.Log("Failed to initialize map. Exiting the game.");
        }


    }


}




