using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiming : WeaponAiming
{
    public GameObject player;

    //Start is a function called at the start of runtime
    private void Start()
    {
        //Finding the object named "Player"
        player = GameObject.Find("Player");
    }

    //Override means i am changing the code of the inherited function
    //Update is a unity function called every frame
    protected override void Update()
    {
        aimPos = player.transform.position;
    }
}
