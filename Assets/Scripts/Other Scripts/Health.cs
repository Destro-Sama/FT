using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;

    private ItemPool itemPool;

    //SerializeField is a header that allows me to edit the values of private variables in the unity editor
    [SerializeField] private int coinMax;
    [SerializeField] private int coinMin;

    public GameObject coinPrefab;

    //start is a function called by unity at the start of the program
    protected void Start()
    {
        health = maxHealth;
        //Finding the item in the scene called "ItemDrops" and getting the component attatched to it
        itemPool = GameObject.Find("ItemDrops").GetComponent<ItemPool>();
    }

    //Update is a function called by unity every frame
    //virtual just means i can inherit from this function
    protected virtual void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void ChangeHealth(float dam)
    {
        health -= dam;
        if (health > maxHealth)
            health = maxHealth;
    }

    virtual protected void Death()
    {
        //checking the tag of the object
        if (gameObject.tag == "Enemy")
        {
            if (coinMax != 0)
            {
                //Getting the list of coin objects
                List<GameObject> coins = itemPool.GetList(999);
                GameObject pick = coins[0];
                //Instantiate creates a copy of the object in real space, on the screen
                GameObject coin = Instantiate(pick);
                coin.transform.position = transform.position;
                //SetParent sets the parent of the object, allowing me to reference it locally
                coin.transform.SetParent(gameObject.transform.parent.parent);
                coin.GetComponent<Coin>().SetValue(Random.Range(coinMin, coinMax));
            }
        }
        //Destroy destoys the object after a set time, in this case 0 seconds
        Destroy(gameObject);
    }
}
