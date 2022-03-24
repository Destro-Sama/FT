using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    private bool isEnabled = false;
    //Static makes the variable a class variable. and syncs its data across every copy of this code.
    //If it changes here, it changes everywhere
    public static int roomsCleared = 0;

    //Update is a unity function called every frame
    //Void is the return type, void means no return
    private void Update()
    {
        if (isEnabled == false)
        {
            //Finding how many enemies are in the room
            int childrenCheck = gameObject.transform.parent.Find("Enemies").childCount;
            if (childrenCheck == 0)
            {
                isEnabled = true;
                if (transform.parent.tag == "Battle_Room")
                    roomsCleared += 1;
                RoomChange[] roomChange = transform.parent.GetComponentsInChildren<RoomChange>();
                if (roomChange != null)
                    foreach (var item in roomChange)
                    {
                        item.RoomEnd();
                    }
                RoomChangerBack[] backRoom = transform.parent.GetComponentsInChildren<RoomChangerBack>();
                if (backRoom != null)
                    foreach (var item in backRoom)
                    {
                        item.RoomEnd();
                    }
                ItemPicker[] itemPicker = transform.parent.GetComponentsInChildren<ItemPicker>();
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
