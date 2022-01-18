using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;

    private ItemPool itemPool;

    [SerializeField] private int coinMax;
    [SerializeField] private int coinMin;

    public GameObject coinPrefab;

    protected void Start()
    {
        health = maxHealth;
        itemPool = GameObject.Find("ItemDrops").GetComponent<ItemPool>();
    }

    protected void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    public void ChangeHealth(float dam)
    {
        health -= dam;
        if (health > maxHealth)
            health = maxHealth;
    }

    virtual protected void Death()
    {
        if (gameObject.tag == "Enemy")
        {
            if (coinMax != 0)
            {
                List<GameObject> coins = itemPool.GetList(999);
                GameObject pick = coins[0];
                GameObject coin = Instantiate(pick);
                coin.transform.position = transform.position;
                coin.transform.SetParent(gameObject.transform.parent.parent);
                coin.GetComponent<Coin>().SetValue(Random.Range(coinMin, coinMax));
            }
        }

        Destroy(gameObject);
    }
}
