using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float DamageAdder;
    public float ShotSpeedadder;
    public float FireRateAdder;
    public float ProjectileAdder;
    public float RangeAdder;
    public float coins;

    public Coins coinUI;

    //Creating a new empty dictionary with key type string and item type integer.
    public Dictionary<string, int> items = new Dictionary<string, int>();


    //All these functions are there to take in stat changes from any script, and make them all local to this one reference
    //Although this is messy, I feel this is better than trying to sync multiple references of the same vairbale across different objects
    public void ChangeDamage(int damage)
    {
        DamageAdder += damage;
    }

    //Void is the return type of the function, void means no return
    public void ChangeShotSpeed(int speed)
    {
        ShotSpeedadder += speed;
    }

    public void ChangeFireRate(int rate)
    {
        FireRateAdder += rate;
    }

    public void ChangeProjectileAdder(int proj)
    {
        ProjectileAdder += proj;
    }

    public void ChangeRange(int range)
    {
        RangeAdder += range;
    }

    public void GiveCoins(int change)
    {
        coins += change;
        coinUI.UpdateCoins();
    }

    public void IncreaseItem(string name)
    {
        items[name] += 1;
    }

    public void NewItem(string name)
    {
        items[name] = 1;
    }
}
