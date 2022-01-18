using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;
    private PlayerHealth healthScript;
    private PlayerMovement playerMovement;
    public string statToChange;
    public int statChange;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        healthScript = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            Invoke(statToChange, 0.01f);
            Destroy(gameObject, 0.02f);
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
