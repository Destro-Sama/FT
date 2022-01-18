using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public ItemPool itemPool;
    private List<GameObject> items;
    public string type;
    private int listIndex;

    private void Start()
    {
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
        items = itemPool.GetList(listIndex);
        int index = Random.Range(0, items.Count);
        GameObject pick = items[index];
        GameObject obj = Instantiate(pick);
        obj.transform.position = transform.position;
        obj.transform.SetParent(gameObject.transform.parent);
        Destroy(gameObject, 0.02f);
    }
    public void RoomEnd()
    {
        PickItem();
    }
}
