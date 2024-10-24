using UnityEngine;


//In this room, user fight with boss monster
public class BossRoom : Room
{
    public override string ToString()
    {
        return "Boss Room"; //representation
    }
    public override void OnRoomEntered(Player user)
    {
        Debug.Log("You entered Boss Room.");
        Debug.Log("You found a boss monster! Ready to fight!\n(Enter anykey)");
        BossMonster monster = new BossMonster();
        DieRoller dieRoller = new DieRoller();
        Debug.Log("Pumpking wants to kill you.");
        dieRoller.GamePlayLoop(user, monster); // dice roll battle starts
    }

    public override void OnRoomSearched(Player user)
    {
        user.ShowPlayerState();
        Debug.Log("Do you want to leave this room?\n(Enter anykey)");
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You left from Boss Room");
    }
}




