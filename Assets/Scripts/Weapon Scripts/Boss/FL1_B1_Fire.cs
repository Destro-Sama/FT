using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FL1_B1_Fire : MonoBehaviour
{
    private bool enraged;
    private Health bossHealth;

    [SerializeField] private float shootCooldown;
    [SerializeField] private float shootTimer = 0;
    private bool canShoot => shootTimer > shootCooldown;

    [SerializeField] private Transform firePoint;

    [SerializeField] private float bulletForce;
    [SerializeField] private float angleDiff;
    [SerializeField] private int bulletAmount;
    [SerializeField] private string bulletType;

    private ObjectPooler objectPooler;

    private string previousAttack;
    private bool attacking;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        bossHealth = gameObject.transform.parent.parent.GetComponent<Health>();
    }

    private void Update()
    {
        if (bossHealth.health <= (bossHealth.maxHealth / 2) && enraged == false)
        {
            Enrage();
            enraged = true;
        }

        if (attacking == false)
            shootTimer += Time.deltaTime;

        if (canShoot)
        {
            Attack();
            shootTimer = 0;
        }
    }

    private void Enrage()
    {
        bulletForce += 10;
        angleDiff -= 15;
        bulletAmount += 4;
    }

    private void Attack()
    {

        int fireChoice = Random.Range(1, 100);


        if (enraged != true)
        {
            if (0 < fireChoice && fireChoice <= 5)
            {
                if (previousAttack == "shoot")
                {
                    Attack();
                }
                else
                {
                    attacking = true;
                    previousAttack = "shoot";
                    Shoot();
                    Invoke("Shoot", 0.3f);
                    Invoke("Shoot", 0.6f);
                    Invoke("Shoot", 0.9f);
                    attacking = false;
                }
            }
            else if (5 < fireChoice && fireChoice <= 15)
            {
                if (previousAttack == "Attack1")
                {
                    Attack();
                }
                else
                {
                    attacking = true;
                    previousAttack = "Attack1";
                    StartCoroutine(StartAttack1());
                }
            }
            else if (15 < fireChoice && fireChoice <= 35)
            {
                if (previousAttack == "Attack2")
                {
                    Attack();
                }
                else
                {
                    attacking = true;
                    previousAttack = "Attack2";
                    StartCoroutine(StartAttack2());
                }
            }
        }
    }

    private void Shoot()
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

    private IEnumerator StartAttack1()
    {
        StartCoroutine(Attack1());
        yield return new WaitForSeconds(1);
        StartCoroutine(Attack1());
        yield return new WaitForSeconds(1);
        StartCoroutine(Attack1());
        yield return new WaitForSeconds(0.1f);
        attacking = false;
    }

    private IEnumerator Attack1()
    {
        float startAngle = (firePoint.rotation.eulerAngles.z - (360 / 2)) + 90f;
        float endAngle = (firePoint.rotation.eulerAngles.z + (360 / 2)) + 90f;
        float angleStep = (endAngle - startAngle) / 8;
        float angle = startAngle + (angleStep / 2);

        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * 3;
            float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * 3;

            rb.AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);

            angle += angleStep;
        }

        yield return new WaitForSeconds(0.5f);

        startAngle = (firePoint.rotation.eulerAngles.z - (350 / 2)) + 90f;
        endAngle = (firePoint.rotation.eulerAngles.z + (350 / 2)) + 90f;
        angleStep = (endAngle - startAngle) / 7;
        angle = startAngle + (angleStep / 2);

        for (int i = 0; i < 7; i++)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * 3;
            float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * 3;

            rb.AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);

            angle += angleStep;
        }
    }

    private IEnumerator StartAttack2()
    {
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(0.1f);
        attacking = false;
    }

    private IEnumerator Attack2()
    {
        GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.rotation = firePoint.rotation;
        rb.velocity = Vector2.zero;
        rb.AddForce(firePoint.up * 20, ForceMode2D.Impulse);
        yield return null;
    }

}
