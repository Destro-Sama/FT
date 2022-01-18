using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPicker : MonoBehaviour
{
    public List<GameObject> Rooms = new List<GameObject>();
    public List<GameObject> bossRooms = new List<GameObject>();
    public RoomHandler roomHandler;
    private int roomsCleared;

    private void Update()
    {
        roomHandler = GameObject.Find("RoomHandler").GetComponent<RoomHandler>();
        roomsCleared = roomHandler.GetRoomsCleared();
    }

    public List<GameObject> GetList()
    {
        if ((roomsCleared + 1) % 10 != 0)
            return Rooms;
        else
            return bossRooms;
    }
}
