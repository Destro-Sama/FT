using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHeadbut : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private float hitCooldown;
    private float hitTimer = 0;
    private bool canHit => hitTimer > hitCooldown;

    public AIPath aiPath;
    private int facingRight;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canHit)
        {
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(facingRight*1000, 1000));
            collision.GetComponent<Health>().ChangeHealth(Damage);
            hitTimer = 0;
        }
    }
}
