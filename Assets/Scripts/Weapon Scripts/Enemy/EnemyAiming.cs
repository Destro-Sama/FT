using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiming : WeaponAiming
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    protected override void Update()
    {
        aimPos = player.transform.position;
    }
}
