using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public Animator deathScreen;
    protected override void Death()
    {
        Debug.Log("You Died Dummy");
        deathScreen.SetTrigger("Death");
        StartCoroutine(Respawn());
        Time.timeScale = 0;
    }

    public void ChangeMaxHealth(int change)
    {
        maxHealth += change;
        health = maxHealth;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(3f);
        deathScreen.SetTrigger("Lived");
        ChangeHealth(-maxHealth);
        Debug.Log("You Live!!");
        Time.timeScale = 1;
        //invoke room change on the same room, to set the player back to the starting positon of the room. Or invoke a room change on "Start" room. 
        //Remove all items
    }
}
