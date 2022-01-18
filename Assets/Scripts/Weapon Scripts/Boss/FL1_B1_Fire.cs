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

    private float enrageBulletForce = 0;
    private float enrageBulletAmount = 0;
    private float enrageAngleDiff = 0;

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
        enrageBulletForce += 10;
        enrageAngleDiff -= 15;
        enrageBulletAmount += 5;
        bulletType = "EnrageBullet";
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
            else if (15 < fireChoice && fireChoice <= 55)
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
            else if (55 < fireChoice && fireChoice <= 80)
            {
                if (previousAttack == "Attack3")
                {
                    Attack();
                }
                else
                {
                    attacking = true;
                    previousAttack = "Attack3";
                    StartCoroutine(StartAttack3());
                }
            }
            else if (80 < fireChoice && fireChoice <= 100)
            {
                if (previousAttack == "Attack4")
                {
                    Attack();
                }
                else
                {
                    attacking = true;
                    previousAttack = "Attack4";
                    StartCoroutine(StartAttack4());
                }
            }
        }
        else
        {
            if (0 < fireChoice && fireChoice <= 10)
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
            else if (10 < fireChoice && fireChoice <= 20)
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
            else if (20 < fireChoice && fireChoice <= 50)
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
            else if (50 < fireChoice && fireChoice <= 75)
            {
                if (previousAttack == "Attack3")
                {
                    Attack();
                }
                else
                {
                    attacking = true;
                    previousAttack = "Attack3";
                    StartCoroutine(StartAttack3());
                }
            }
            else if (75 < fireChoice && fireChoice <= 100)
            {
                if (previousAttack == "Attack4")
                {
                    Attack();
                }
                else
                {
                    attacking = true;
                    previousAttack = "Attack4";
                    StartCoroutine(StartAttack4());
                }
            }
        }
    }

    private void Shoot()
    {
        float startAngle = (firePoint.rotation.eulerAngles.z - (angleDiff+enrageAngleDiff / 2)) + 90f;
        float endAngle = (firePoint.rotation.eulerAngles.z + (angleDiff+enrageAngleDiff / 2)) + 90f;
        float angleStep = (endAngle - startAngle) / bulletAmount+enrageBulletAmount;
        float angle = startAngle + (angleStep / 2);

        for (int i = 0; i < bulletAmount+enrageBulletAmount; i++)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * bulletForce+enrageBulletForce;
            float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * bulletForce+enrageBulletForce;

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
        float angleStep = (endAngle - startAngle) / 8+enrageBulletAmount;
        float angle = startAngle + (angleStep / 2);

        for (int i = 0; i < 8+enrageBulletAmount; i++)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * (3+enrageBulletForce);
            float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * (3+enrageBulletForce);

            rb.AddForce(new Vector2(xcomponent, ycomponent), ForceMode2D.Impulse);

            angle += angleStep;
        }

        yield return new WaitForSeconds(0.5f);

        startAngle = (firePoint.rotation.eulerAngles.z - (350 / 2)) + 90f;
        endAngle = (firePoint.rotation.eulerAngles.z + (350 / 2)) + 90f;
        angleStep = (endAngle - startAngle) / 7+enrageBulletAmount;
        angle = startAngle + (angleStep / 2);

        for (int i = 0; i < 7+enrageBulletAmount; i++)
        {
            GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;

            float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * (3+enrageBulletForce);
            float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * (3+enrageBulletForce);

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
        if (enraged)
        {
            StartCoroutine(Attack2());
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Attack2());
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Attack2());
            yield return new WaitForSeconds(0.1f);
        }
        attacking = false;
    }

    private IEnumerator Attack2()
    {
        GameObject bullet = objectPooler.SpawnFromPool(bulletType, firePoint.position);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.rotation = firePoint.rotation;
        rb.velocity = Vector2.zero;
        rb.AddForce(firePoint.up * (20+enrageBulletForce), ForceMode2D.Impulse);
        yield return null;
    }

    private IEnumerator StartAttack3()
    {
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        if (enraged)
        {
            StartCoroutine(Attack3());
            StartCoroutine(Attack3());
            StartCoroutine(Attack3());
        }
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        if (enraged)
        {
            StartCoroutine(Attack3());
            StartCoroutine(Attack3());
        }
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        StartCoroutine(Attack3());
        if (enraged)
        {
            StartCoroutine(Attack3());
            StartCoroutine(Attack3());
            StartCoroutine(Attack3());
            StartCoroutine(Attack3());
            StartCoroutine(Attack3());
            StartCoroutine(Attack3());
        }
        yield return new WaitForSeconds(0.1f);
        attacking = false;
    }

    private IEnumerator Attack3()
    {
        GameObject bullet = objectPooler.SpawnFromPool(bulletType, new Vector3 (Random.Range(-15, 15), 16, 1));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.rotation = Quaternion.identity;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, -1) * (15+enrageBulletForce), ForceMode2D.Impulse);
        yield return null;
    }

    private IEnumerator StartAttack4()
    {
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        if (enraged)
        {
            StartCoroutine(Attack4());
            StartCoroutine(Attack4());
            StartCoroutine(Attack4());
        }
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        if (enraged)
        {
            StartCoroutine(Attack4());
            StartCoroutine(Attack4());
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        StartCoroutine(Attack4());
        if (enraged)
        {
            StartCoroutine(Attack4());
            StartCoroutine(Attack4());
            StartCoroutine(Attack4());
            StartCoroutine(Attack4());
            StartCoroutine(Attack4());
        }
        yield return new WaitForSeconds(0.1f);
        attacking = false;
    }

    private IEnumerator Attack4()
    {
        GameObject bullet = objectPooler.SpawnFromPool(bulletType, new Vector3(Random.Range(-15, 15), -2, 1));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.rotation = Quaternion.identity;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, 1) * (15+enrageBulletForce), ForceMode2D.Impulse);
        yield return null;
    }

}
