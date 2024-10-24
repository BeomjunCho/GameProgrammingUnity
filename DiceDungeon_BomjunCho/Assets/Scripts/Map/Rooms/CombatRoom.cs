using System;
using UnityEngine;

//In this room, user fights with normal monster
public class CombatRoom : Room
{
    public override string ToString()
    {
        return "Combat Room"; //representation
    }
    public override void OnRoomEntered(Player user)
    {
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You entered Combat Room.");
            Debug.Log("You’ve encountered a fierce Minotaur. Prepare for battle!\n(Enter anykey)");
            Console.ReadLine();
            NormalMonster monster = new NormalMonster();
            DieRoller dieRoller = new DieRoller();
            dieRoller.GamePlayLoop(user, monster); // dice roll battle starts
        }
        else
        {
            Debug.Log("You entered Combat Room.");
            Debug.Log("You have returned to the Combat Room. There is a dead monster.");
        }
    }

    public override void OnRoomSearched(Player user)
    {
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You are searching Combat Room.");
            Debug.Log("You found one item from dead monster.");
            user.inventory.AddRndItem();
        }
        else
        {
            Debug.Log("You are searching Combat Room.");
            Debug.Log("There is nothing left to search in this room.");
        }
        user.ShowPlayerState();
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving from Combat Room");
        IsVisited = true; // Mark the room as visited
    }
}




