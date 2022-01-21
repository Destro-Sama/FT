using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemCollection : CollectItem
{
    [SerializeField] private float pickUpRange;
    private static float pickUpCounter = 1f;

    public int shopPrice;


    private void Update()
    {

        pickUpCounter -= Time.deltaTime;

        Vector3 distanceToPlayer = player.transform.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetButtonDown("Interact") && pickUpCounter <= 0 && playerStats.coins >= shopPrice)
        {
            pickUpCounter = 1f;
            PickUp();
        }
    }

    private void PickUp()
    {
        Invoke(statToChange, 0.01f);
        playerStats.GiveCoins(-shopPrice);
        transform.position = new Vector3(100, 100, 1);
        Destroy(gameObject, 0.02f);
        if (playerStats.items.ContainsKey(gameObject.name))
        {
            playerStats.IncreaseItem(gameObject.name);
        }
        else
        {
            playerStats.NewItem(gameObject.name);
        }
        Debug.Log($"{gameObject.name}: {playerStats.items[gameObject.name]}");
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
