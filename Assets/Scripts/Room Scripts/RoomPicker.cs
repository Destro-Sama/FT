using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPicker : MonoBehaviour
{
    //Creating a new empty List of GameObjects
    public List<GameObject> FL1_Rooms = new List<GameObject>();
    public List<GameObject> Item_Rooms = new List<GameObject>();
    public List<GameObject> bossRooms = new List<GameObject>();
    public List<GameObject> FL1_FL2 = new List<GameObject>();
    public RoomHandler roomHandler;
    private int roomsCleared;

    //Update is a function called by unity every frame
    private void Update()
    {
        //Finding the object called "RoomHandler" and getting the component on it
        roomHandler = GameObject.Find("RoomHandler").GetComponent<RoomHandler>();
        roomsCleared = roomHandler.GetRoomsCleared();
    }

    //List<GameObject> is the return type of the function
    public List<GameObject> GetList(string Type)
    {
        if ((roomsCleared + 1) % 10 != 0)
        {
            if (Type == "FL1")
                return FL1_Rooms;
            else if (Type == "Item")
                return Item_Rooms;
            else if (Type == "FL1-FL2")
                return FL1_FL2;
            else
                return null;
        }
        else
            return bossRooms;
    }
}
