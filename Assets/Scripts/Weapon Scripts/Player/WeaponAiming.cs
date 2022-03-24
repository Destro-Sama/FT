using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    //SerializeField is a header that lets me edit private variables in the Unity Editor
    [SerializeField] protected Rigidbody2D rb;

    public Camera cam;
    protected Vector2 aimPos;
    protected Vector2 lookDir;
    protected float aimAngle;

    //Update is a unity function called every frame
    //Virtual allows me to inherit from this function
    protected virtual void Update()
    {
        aimPos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    //FixedUpdate is a unity function called every Physics Frame
    protected void FixedUpdate()
    {
        lookDir = aimPos - rb.position;
        aimAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }
}
