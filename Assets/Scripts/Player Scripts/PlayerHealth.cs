using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//PlayerHealth Inherits from the Health class
public class PlayerHealth : Health
{
    //Animator is a component that controls the animations of an object
    public Animator deathScreen;
    //Image is the ui element that controls what is seen
    public Image healthBar;
    public float Iframes;
    private float hitCooldown = 0f;

    //Update is a function called by unity every frame
    //Override allows me to override the function of Update in the Health Script
    //Void is the return type of the function, void means no return
    protected override void Update()
    {
        //Base.Update allows me to get the exact same code as the Update function in the Health class, but add to it
        base.Update();
        hitCooldown -= Time.deltaTime;
    }

    protected override void Death()
    {
        //Debug.Log sends a string to the console for debugging
        Debug.Log("You Died Dummy");
        //Triggers are the controlers of animations, setting triggers can switch and control animations
        deathScreen.SetTrigger("Death");
        //StartCoroutine starts a Coroutine
        StartCoroutine(Respawn());
        //timeScale is the playback speed of the unity engine, at 0 it isnt calcualting any physics, at 1 it is normal speed, and so on.
        Time.timeScale = 0;
    }

    public void ChangeMaxHealth(int change)
    {
        maxHealth += change;
        health = maxHealth;
        UpdateUI();
    }

    public override void ChangeHealth(float dam)
    {
        if (hitCooldown <= 0)
        {
            hitCooldown = Iframes;
            base.ChangeHealth(dam);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        healthBar.GetComponent<RectTransform>().LeanScaleX(health / maxHealth, 0.5f);
    }

    //IEnumerator is the function header for Coroutines
    //Coroutines are functions that get called alongside the normal functions,
    //which mean they use their own time systems and features, allowing me to bypass the stop time from before
    IEnumerator Respawn()
    {
        //This allows me to wait for 3 seconds in realtime
        yield return new WaitForSecondsRealtime(3f);
        deathScreen.SetTrigger("Lived");
        ChangeHealth(-maxHealth);
        //LoadScene allows me to load sections of set objects, all at once
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        UpdateUI();
    }
}
