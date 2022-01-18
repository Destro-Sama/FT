using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    private bool isEnabled = false;
    public static int roomsCleared = 0;

    private void Update()
    {
        if (isEnabled == false)
        {
            int childrenCheck = gameObject.transform.parent.Find("Enemies").childCount;
            if (childrenCheck == 0)
            {
                isEnabled = true;
                roomsCleared += 1;
                Transform roomChange = gameObject.transform.parent.Find("RoomChanger");
                if (roomChange != null)
                    roomChange.GetComponent<RoomChange>().RoomEnd();
                Transform backRoom = gameObject.transform.parent.Find("BackRoomChanger");
                if (backRoom != null)
                    backRoom.GetComponent<RoomChangerBack>().RoomEnd();
                Transform itemPicker = gameObject.transform.parent.Find("ItemSpawner");
                if (itemPicker != null)
                    itemPicker.GetComponent<ItemPicker>().RoomEnd();
            }
        }
    }

    public int GetRoomsCleared()
    {
        return roomsCleared;
    }
}
