using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    private GameObject connectedRoom;
    //Static makes the variable a class variable. and syncs its data across every copy of this code.
    //If it changes here, it changes everywhere
    private static ObjectPooler objectPooler;
    private bool isEnabled = false;
    public static Animator transition;
    private static List<GameObject> rooms;

    public string Room_Type;
    public PlayerStats playerStats;
    public int roomCost;

    public static int itemRooms;
    private int itemRoomsPerFloor = 2;

    //Start is a function called by unity at the start of the runtime
    private void Start()
    {
        //Finding an object named "Player" and getting the component on it
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        transition = GameObject.Find("RoomLoader").transform.GetChild(0).GetComponent<Animator>();
        objectPooler = GameObject.Find("ObjectPool").GetComponent<ObjectPooler>();
        rooms = GameObject.Find("RoomsList").GetComponent<RoomPicker>().GetList(Room_Type);
        SpawnRoom();
    }

    //void is the return type of the function, void means no return
    public void SpawnRoom()
    {
        connectedRoom = rooms[Random.Range(0, rooms.Count)];
        string thisRoom = gameObject.transform.parent.gameObject.name;
        if (connectedRoom.gameObject.name != thisRoom.Substring(0, thisRoom.Length - 7))
        {
            //Instantiate creates a copy of the object in the real space, on the screen
            connectedRoom = Instantiate(connectedRoom);
            connectedRoom.transform.position = new Vector3(0, 0, 0);
            if (Room_Type != "Item")
                //SetParent sets the parent of the object, allowing me to reference it locally
                connectedRoom.transform.SetParent(transform.parent.parent);
            //SetActice(false) makes the object invisible and uninteractable
            connectedRoom.SetActive(false);
            return;
        }
        SpawnRoom();
    }

    public void RoomEnd()
    {
        if (Room_Type == "Item" && itemRooms >= itemRoomsPerFloor)
        {
            gameObject.SetActive(false);
            itemRooms += 1;
            return;
        }
        isEnabled = true;
        //SpriteRenderer Is the unity component that handles visible UI elements
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    //OnTriggerEnter2D is a unity function that is called when 2 colliders touch eacher other
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking the string of the tag
        if (collision.tag != "Player")
            return;

        if (isEnabled)
        {
            if (Room_Type == "Item")
            {
                if (playerStats.coins < roomCost)
                    return;
                playerStats.GiveCoins(-roomCost);
                //Destroy destroys an object after a set time
                Destroy(gameObject, 2f);
            }
            //timeScale allows me to control the speed of playback, at 0 nothing is updated
            Time.timeScale = 0;
            //Triggers control animations
            transition.SetTrigger("Transition");
            int children = objectPooler.gameObject.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                objectPooler.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            //SetActive(true) makes an objetc visible and interactable
            connectedRoom.SetActive(true);
            transform.parent.gameObject.SetActive(false);
            //This scans the new room for changes in obstacles
            var graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
            collision.transform.position = new Vector3((-1 * gameObject.transform.position.x)-3, gameObject.transform.position.y, 1);
            transition.SetTrigger("TransitionEnd");
            Time.timeScale = 1;
        }
    }
}
