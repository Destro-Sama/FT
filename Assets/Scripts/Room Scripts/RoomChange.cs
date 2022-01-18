using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    private GameObject connectedRoom;
    private static ObjectPooler objectPooler;
    private bool isEnabled = false;
    public static Animator transition;
    private static List<GameObject> rooms;

    private void Start()
    {
        transition = GameObject.Find("RoomLoader").transform.GetChild(0).GetComponent<Animator>();
        objectPooler = GameObject.Find("ObjectPool").GetComponent<ObjectPooler>();
        rooms = GameObject.Find("RoomsList").GetComponent<RoomPicker>().GetList();
        SpawnRoom();
    }

    public void SpawnRoom()
    {
        connectedRoom = rooms[Random.Range(0, rooms.Count)];
        string thisRoom = gameObject.transform.parent.gameObject.name;
        if (connectedRoom.gameObject.name != thisRoom.Substring(0, thisRoom.Length - 7))
        {
            connectedRoom = Instantiate(connectedRoom);
            connectedRoom.transform.position = new Vector3(0, 0, 0);
            connectedRoom.transform.SetParent(transform.parent.parent);
            connectedRoom.SetActive(false);
            return;
        }
        SpawnRoom();
    }

    public void RoomEnd()
    {
        isEnabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        if (isEnabled)
        {
            Time.timeScale = 0;
            transition.SetTrigger("Transition");
            int children = objectPooler.gameObject.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                objectPooler.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            connectedRoom.SetActive(true);
            transform.parent.gameObject.SetActive(false);
            var graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
            collision.transform.position = new Vector3((-1 * gameObject.transform.position.x)-3, gameObject.transform.position.y, 1);
            transition.SetTrigger("TransitionEnd");
            Time.timeScale = 1;
        }
    }
}
