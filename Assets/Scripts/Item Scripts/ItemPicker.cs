using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    //Assinging the value to the Class ItemPool
    public ItemPool itemPool;
    private List<GameObject> items;
    public string type;
    private int listIndex;

    //Start is a unity function called just after Awake is called
    private void Start()
    {
        //setting the varibale to the component class ItemPool
        itemPool = GameObject.Find("ItemDrops").GetComponent<ItemPool>();
    }

    public void PickItem()
    {
        if (type == "common")
            listIndex = 0;
        else if (type == "uncommon")
            listIndex = 1;
        else if (type == "testing")
            listIndex = 2;
        else if (type == "shop")
            listIndex = 998;
        items = itemPool.GetList(listIndex);
        int index = Random.Range(0, items.Count);
        GameObject pick = items[index];
        //instantiate creates the object as a physical thing in the unity space, on the screen
        GameObject obj = Instantiate(pick);
        obj.transform.position = transform.position;
        //set parents sets the parent of the object, which allows for local reference to objects
        obj.transform.SetParent(gameObject.transform.parent);
        //Destroy destorys an object after a set time
        Destroy(gameObject, 0.02f);
    }
    public void RoomEnd()
    {
        PickItem();
    }
}
