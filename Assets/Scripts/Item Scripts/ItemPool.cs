using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    //Creates An Empty List
    public List<GameObject> common = new List<GameObject>();
    public List<GameObject> unCommon = new List<GameObject>();
    public List<GameObject> testting = new List<GameObject>();
    public List<GameObject> coin = new List<GameObject>();
    public List<GameObject> shop = new List<GameObject>();

    //List<GameObject> is the return type of the function
    public List<GameObject> GetList(int index)
    {
        if (index == 0)
            return common;
        else if (index == 1)
            return unCommon;
        else if (index == 2)
            return testting;
        else if (index == 998)
            return shop;
        else if (index == 999)
            return coin;
        else
            return common;
    }
}
