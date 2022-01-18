using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] protected float shootCooldown;
    protected float shootTimer = 0;
    protected bool canShoot => shootTimer > shootCooldown;

    [SerializeField] protected Transform firePoint;

    [SerializeField] protected float bulletForce;
    [SerializeField] protected float angleDiff;
    [SerializeField] protected int bulletAmount;
    [SerializeField] protected string bulletType;

    protected ObjectPooler objectPooler;

    protected virtual void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    protected virtual void Update()
    {
        shootTimer += Time.deltaTime;

        if (canShoot)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    protected void Shoot()
    {
        if (bulletAmount == 1)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.rotation = firePoint.rotation;
            rb.velocity = Vector2.zero;
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            return;
        }

        float startAngle = (firePoint.rotation.eulerAngles.z - (angleDiff / 2)) + 90f;
        float endAngle = (firePoint.rotation.eulerAngles.z + (angleDiff / 2)) + 90f;
        float angleStep = (endAngle - startAngle) / bulletAmount;
        float angle = startAngle + (angleStep / 2);

        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * bulletForce;
            float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * bulletForce;

            rb.AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);

            angle += angleStep;
        }
    }

}
