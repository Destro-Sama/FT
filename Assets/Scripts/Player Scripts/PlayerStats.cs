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

    public Dictionary<string, int> items = new Dictionary<string, int>();

    public void ChangeDamage(int damage)
    {
        DamageAdder += damage;
    }

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
