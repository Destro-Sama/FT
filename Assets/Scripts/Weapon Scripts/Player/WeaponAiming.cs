using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    public Camera cam;
    protected Vector2 aimPos;
    protected Vector2 lookDir;
    protected float aimAngle;

    protected virtual void Update()
    {
        aimPos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    protected void FixedUpdate()
    {
        lookDir = aimPos - rb.position;
        aimAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        //rb.rotation = aimAngle;
        transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }
}
