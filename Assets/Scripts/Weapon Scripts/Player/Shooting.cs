using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [SerializeField] private float bulletForce;
    [SerializeField] private float angleDiff;
    [SerializeField] private int bulletAmount;
    [SerializeField] private string bulletType;
    private GameObject player;
    private PlayerStats playerStats;
    private float bulletAdder;
    private float shotSpeedAdder;
    private float fireRateAdder;

    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler =  ObjectPooler.Instance;
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            bulletAdder = playerStats.ProjectileAdder;
            shotSpeedAdder = playerStats.ShotSpeedadder;
            //fireRateAdder = playerStats.FireRateAdder; currently dont have cooldowns set to fire, will fix later
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletAmount+bulletAdder == 1)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.rotation = firePoint.rotation;
            rb.velocity = Vector2.zero;
            rb.AddForce(firePoint.up * (bulletForce+shotSpeedAdder), ForceMode2D.Impulse);
            return;
        }

        float startAngle = (firePoint.rotation.eulerAngles.z - (angleDiff / 2)) + 90f;
        float endAngle = (firePoint.rotation.eulerAngles.z + (angleDiff / 2)) + 90f;
        float angleStep = (endAngle-startAngle)/ (bulletAmount+bulletAdder);
        float angle = startAngle + (angleStep/2);

        for (int i = 0; i < (bulletAmount+bulletAdder); i++)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * (bulletForce+shotSpeedAdder);
            float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * (bulletForce+shotSpeedAdder);

            rb.AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);

            angle += angleStep;
        }
    }
}
