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

    private static LTDescr delay;

    private void Awake()
    {
        LeanTween.init(800);
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
                LeanTween.cancel(delay.uniqueId);
                Delete();
            }
            else
            {
                collision.GetComponent<Health>().ChangeHealth(damage);
                LeanTween.cancel(delay.uniqueId);
                Delete();
            }
        }
        if (collision.tag == "Wall" || collision.tag == "Floor")
        {
            LeanTween.cancel(delay.uniqueId);
            Delete();
        }
        return;
    }

    private void OnEnable()
    {
        delay = LeanTween.delayedCall(bulletTime + playerStats.RangeAdder, () =>
        {
            Delete();
        });
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
