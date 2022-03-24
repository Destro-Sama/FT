using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChangerBack : MonoBehaviour
{
    private GameObject connectedRoom;
    //Static makes the variable a class variable. and syncs its data across every copy of this code.
    //If it changes here, it changes everywhere
    private static ObjectPooler objectPooler;
    private bool isEnabled = false;
    //Animator is a unity componet that control animations
    public static Animator transition;

    //Start is a unity function called at the start of the runtime.
    private void Start()
    {
        //Finding an object called "RoomLoader", getting the first child, and getting the Animator component
        transition = GameObject.Find("RoomLoader").transform.GetChild(0).GetComponent<Animator>();
        objectPooler = GameObject.Find("ObjectPool").GetComponent<ObjectPooler>();
        FindRoom();
    }

    //Void is the return type of the function. void means no return
    public void FindRoom()
    {
        //Checking the tag of the object
        if (transform.parent.gameObject.tag != "Item_Room")
            connectedRoom = transform.parent.parent.GetChild(transform.parent.GetSiblingIndex() - 1).gameObject;
        else
        {
            connectedRoom = GameObject.Find("Rooms").transform.GetChild(GameObject.Find("Rooms").transform.childCount-1).gameObject;
        }
    }

    public void RoomEnd()
    {
        isEnabled = true;
        //Enabled allows me to change the visibility of an object
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    //OnTriggerEnter2D is a unity function called when 2 colliders touch
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking the tag of the collison
        if (collision.tag != "Player")
            return;

        if (isEnabled)
        {
            //timeScale allows me to control the physics runback of the system. at 0, nothing gets updated, at 1 everything gets updated normally
            Time.timeScale = 0;
            //Triggers allow me to control the animations of an object
            transition.SetTrigger("Transition");
            //Getting the number of children of objectPooler
            int children = objectPooler.gameObject.transform.childCount;
            for (int i = 0; i < children; i++)
            {
                //SetActive(false) makes the objects invisible and uninteractable
                objectPooler.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            //SetActive(true) allows me to make the object visible and interactable
            connectedRoom.SetActive(true);
            transform.parent.gameObject.SetActive(false);
            //This scans a grid again, to check for any new objects, or old objetcs moving or dissapearing
            var graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
            collision.transform.position = new Vector3((-1 * gameObject.transform.position.x)+3, gameObject.transform.position.y, 1);
            transition.SetTrigger("TransitionEnd");
            Time.timeScale = 1;
        }
    }
}
