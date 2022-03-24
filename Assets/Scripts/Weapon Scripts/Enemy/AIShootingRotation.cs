using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIShootingRotation : MonoBehaviour
{
    public AIPath aiPath;

    //Update is a function called by Unity every frame
    private void Update()
    {

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
