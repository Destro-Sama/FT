using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollection : MonoBehaviour
{
    //SerializeField is a header that allows me to edit private variables in the Unity Editor
    [SerializeField] private Shooting gunScript;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private Transform player, gunContainer, Cam;
    private WeaponCollection equiptGun;

    [SerializeField] private float pickUpRange;
    [SerializeField] private float dropForwardForce, dropUpwardForce;

    private float pickUpCooldown = 2f;
    private static float pickUpCounter = 2f;

    public bool equipped;
    public static GameObject room;

    //Start is a unity function called at the start of runtime
    private void Start()
    {
        //Finding the first object in the scene called "Player"
        player = GameObject.Find("Player").transform;
        gunContainer = player.Find("WeaponSlot");
        Cam = GameObject.Find("Main Camera").transform;
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    //Update is a unity function called every frame
    private void Update()
    {

        pickUpCounter -= Time.deltaTime;

        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetButtonDown("Interact") && pickUpCounter <= 0)
        {
            pickUpCounter = pickUpCooldown;
            equiptGun = player.Find("WeaponSlot").GetChild(0).GetComponent<WeaponCollection>();
            PickUp();
            equiptGun.Invoke("Drop", 0.2f);
        }
    }

    //Void is the return type of the function, void means no return
    private void PickUp()
    {
        equipped = true;

        room = transform.parent.gameObject;

        //Setting the parent so we can access it's local values later
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;
        rb.velocity = Vector2.zero;
        //Setting the constraints of a Rigidbody2D
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        gunScript.enabled = true;
    }

    public void Drop()
    {
        equipped = false;

        transform.SetParent(room.transform);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = player.GetComponent<Rigidbody2D>().velocity;
        rb.AddForce(Cam.right * dropForwardForce, ForceMode2D.Impulse);
        rb.AddForce(Cam.up * dropUpwardForce, ForceMode2D.Impulse);

        gunScript.enabled = false;
    }
}
