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
                RoomChange[] roomChange = gameObject.transform.parent.GetComponentsInChildren<RoomChange>();
                if (roomChange != null)
                    foreach (var item in roomChange)
                    {
                        item.RoomEnd();
                    }
                RoomChangerBack[] backRoom = gameObject.transform.parent.GetComponentsInChildren<RoomChangerBack>();
                if (backRoom != null)
                    foreach (var item in backRoom)
                    {
                        item.RoomEnd();
                    }
                ItemPicker[] itemPicker = gameObject.transform.parent.GetComponentsInChildren<ItemPicker>();
                if (itemPicker != null)
                    foreach (var item in itemPicker)
                    {
                        item.RoomEnd();
                    }
            }
        }
    }

    public int GetRoomsCleared()
    {
        return roomsCleared;
    }
}
