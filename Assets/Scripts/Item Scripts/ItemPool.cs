using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public List<GameObject> common = new List<GameObject>();
    public List<GameObject> unCommon = new List<GameObject>();
    public List<GameObject> testting = new List<GameObject>();
    public List<GameObject> coin = new List<GameObject>();

    public List<GameObject> GetList(int index)
    {
        if (index == 0)
            return common;
        else if (index == 1)
            return unCommon;
        else if (index == 2)
            return testting;
        else if (index == 999)
            return coin;
        else
            return common;
    }
}
