using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemCollection : CollectItem
{
    //SerializeField is a header that makes private fields viewable within the unity editor
    [SerializeField] private float pickUpRange;
    //static is a qualifier that makes all references of this variable in all versions of this class the same. if this changes, all refernces of it change.
    private static float pickUpCounter = 1f;

    public int shopPrice;

    public static bool nearby;
    private bool thisNearby;

    //SpriteRender is a unity component that renders sprites to a screen
    private SpriteRenderer image;

    //awake is a function called by unity the moment the object is initialised
    //void is the return type of the function, void means no return
    private void Awake()
    {
        //sets the image variable to the spriterenderer on this object
        image = gameObject.GetComponent<SpriteRenderer>();
    }

    //update is a function called by unity every frame
    private void Update()
    {

        pickUpCounter -= Time.deltaTime;

        //creates a vector3 and sets it as the distance between 2 positions
        Vector3 distanceToPlayer = player.transform.position - transform.position;
        if ( (distanceToPlayer.magnitude <= pickUpRange && nearby == false) || (distanceToPlayer.magnitude <= pickUpRange && thisNearby == true))
        {
            nearby = true;
            thisNearby = true;
            //sets the colour of the image
            image.color = new Color(255, 0, 0);
            //checking if you are pressing the button labbled "Interact"
            if (Input.GetButtonDown("Interact") && pickUpCounter <= 0 && playerStats.coins >= shopPrice)
            {
                pickUpCounter = 1f;
                PickUp();
            }
        }
        else if (distanceToPlayer.magnitude <= pickUpRange && thisNearby == false)
        {
            image.color = new Color(255, 255, 255);
        }
        else if (distanceToPlayer.magnitude >= pickUpRange && thisNearby == true)
        {
            nearby = false;
            thisNearby = false;
            image.color = new Color(255, 255, 255);
        }
    }

    private void PickUp()
    {
        nearby = false;
        thisNearby = false;
        //invoke calls a function after a set amount of time
        Invoke(statToChange, 0.01f);
        playerStats.GiveCoins(-shopPrice);
        //setting the position of the object to a new vector3
        transform.position = new Vector3(100, 100, 1);
        //Destroys the object after a set time
        Destroy(gameObject, 0.02f);
        //checking if the item is already in our dictionary
        if (playerStats.items.ContainsKey(gameObject.name))
        {
            //adds a value to that item
            playerStats.IncreaseItem(gameObject.name);
        }
        else
        {
            //creates a reference to that item
            playerStats.NewItem(gameObject.name);
        }
        //debug.log is a function that sends a string to the console, for debugging
        Debug.Log($"{gameObject.name}: {playerStats.items[gameObject.name]}");
    }

    //OnTriggerEnter2D is a unity function that gets called when 2 colliders touch each other
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //this function is empty so it can inherit from CollectItem but not get called on a collision trigger
    }
}
