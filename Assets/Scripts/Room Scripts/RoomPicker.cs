using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPicker : MonoBehaviour
{
    public List<GameObject> FL1_Rooms = new List<GameObject>();
    public List<GameObject> Item_Rooms = new List<GameObject>();
    public List<GameObject> bossRooms = new List<GameObject>();
    public List<GameObject> FL1_FL2 = new List<GameObject>();
    public RoomHandler roomHandler;
    private int roomsCleared;

    private void Update()
    {
        roomHandler = GameObject.Find("RoomHandler").GetComponent<RoomHandler>();
        roomsCleared = roomHandler.GetRoomsCleared();
    }

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
