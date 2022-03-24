using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    //GameObject, PlayerStats, PlayerHealth, PlayerMovement are class components of an object
    protected GameObject player;
    protected PlayerStats playerStats;
    protected PlayerHealth healthScript;
    protected PlayerMovement playerMovement;
    public string statToChange;
    public int statChange;

    //Start is a function called by unity at the start, just after Awake.
    protected void Start()
    {
        //Getting the gameobject called Player
        player = GameObject.Find("Player");
        //getting the component playerstats
        playerStats = player.GetComponent<PlayerStats>();
        healthScript = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    //OnTriggerEnter2D is called when two colliders touch
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //checking the tag set on the collision
        if (collision.tag == "Player")
        { 
            //Invoke calls a function after a set time
            Invoke(statToChange, 0.01f);
            transform.position = new Vector3(100, 100, 1);
            //Destroys an object after a set time
            Destroy(gameObject, 0.02f);
            //Checking if the item is already in the dictionary
            if (playerStats.items.ContainsKey(gameObject.name))
            {
                //Increasing the number in the dictionary
                playerStats.IncreaseItem(gameObject.name);
            }
            else
            {
                //Creating a new item in the ditionary
                playerStats.NewItem(gameObject.name);
            }
            //Debug.log sends a string to the console for debugging
            Debug.Log($"{gameObject.name}: {playerStats.items[gameObject.name]}");
        }
    }


    //These functions send info to a different class to update values,
    //there is probably a better way to do this, but this is the best I knew at the time
    public void Damage()
    {
        playerStats.ChangeDamage(statChange);
    }

    public void ShotSpeed()
    {
        playerStats.ChangeShotSpeed(statChange);
    }

    public void FireRate()
    {
        playerStats.ChangeFireRate(statChange);
    }

    public void ProjectileAdder()
    {
        playerStats.ChangeProjectileAdder(statChange);
    }

    public void Range()
    {
        playerStats.ChangeRange(statChange);
    }

    public void Health()
    {
        healthScript.ChangeMaxHealth(statChange);
    }

    public void SpeedMultiplier()
    {
        playerMovement.ChangeSpeedMultiplier(statChange);
    }

    public void JumpHeight()
    {
        playerMovement.ChangeJumpHeight(statChange);
    }

    public void JumpAmount()
    {
        playerMovement.ChangeJumpAmount(statChange);
    }
}
