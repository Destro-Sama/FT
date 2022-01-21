using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    protected GameObject player;
    protected PlayerStats playerStats;
    protected PlayerHealth healthScript;
    protected PlayerMovement playerMovement;
    public string statToChange;
    public int statChange;

    protected void Start()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        healthScript = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            Invoke(statToChange, 0.01f);
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
    }

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
