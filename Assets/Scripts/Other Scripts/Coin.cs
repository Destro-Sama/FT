using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int value;

    //Void is the return type of the function, Void meaning no return
    public void SetValue(int change)
    {
        value = change;
    }

    //OnTrigger2D is a unity function that gets called when 2 colliders touch eacher other.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking if the collision tag is "Player"
        if (collision.gameObject.tag == "Player")
        {
            //Getting the componet of "PlayerStats" from the object, and calling a function on it
            collision.gameObject.GetComponent<PlayerStats>().GiveCoins(value);
            //Destoy destoys an object after a set time, in this instance 0 seconds.
            Destroy(gameObject);
        }
    }
}
