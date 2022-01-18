using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Rigidbody2D rb;
    public float damage;
    public string yourTag;
    public string enemyTag;
    public float bulletTime;
    private GameObject player;
    [SerializeField] private PlayerStats playerStats;

    private void Awake()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == enemyTag)
        {
            if (enemyTag != "Player")
            {
                collision.GetComponent<Health>().ChangeHealth(damage + playerStats.DamageAdder);
                //Debug.Log(damage + playerStats.DamageAdder);
                Delete();
            }
            else
            {
                collision.GetComponent<Health>().ChangeHealth(damage);
                Delete();
            }
        }
        if (collision.tag == "Wall" || collision.tag == "Floor")
            Delete();
        return;
    }

    private void OnEnable()
    {
        Invoke("Delete", (bulletTime+playerStats.RangeAdder));
    }

    private void Delete()
    {
        if (gameObject.activeSelf == false)
            return;
        gameObject.SetActive(false);
        rb.velocity = Vector2.zero;
        GameObject em = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(em, 1.5f);
    }
}
