using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //Awake is a function called by unity when an object is first initialised
    void Awake()
    {
        //DontDestroyOnLoad is a unity specific function that doesnt destoy the gameobject attached, when loading new scenes,
        //which allows you to carry values and objects across scenes
        DontDestroyOnLoad(this.gameObject);
    }
}
