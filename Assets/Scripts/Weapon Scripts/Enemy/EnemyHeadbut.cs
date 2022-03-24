using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHeadbut : MonoBehaviour
{
    //SerializeField is a header that makes private variables editable in the Unity Editor
    [SerializeField] private int Damage;
    [SerializeField] private float hitCooldown;
    private float hitTimer = 0;
    private bool canHit => hitTimer > hitCooldown;

    public AIPath aiPath;
    private int facingRight;

    //Update is a function called by Unity every frame
    private void Update()
    {

        hitTimer += Time.deltaTime;

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = 1;
        }
        else if (aiPath.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = -1;
        }
    }

    //OnTriggerEnter2D is a function called when 2 colliders touch
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checking the string against the tag of the collided object
        if (collision.tag == "Player" && canHit)
        {
            //Getting the component of Rigidbody2D
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingRight*1000, 1000));
            collision.GetComponent<Health>().ChangeHealth(Damage);
            hitTimer = 0;
        }
    }
}
