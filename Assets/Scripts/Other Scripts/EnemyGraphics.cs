using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGraphics : MonoBehaviour
{
    public AIPath aiPath;

    //Update is a function called by unity every frame
    private void Update()
    {
        //DesiredVelocity is the vecloity the object is trying to match
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            //LocalScale is the scale of the object, with local reference to it's parent
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
