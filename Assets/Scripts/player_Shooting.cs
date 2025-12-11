using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player_Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLife;

    Vector2 pointingDirection;
    float thetaVal;

    public warpPlayer currentProjectile;

    void Update()
    {
        // Aim at mouse
        pointingDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        thetaVal = Mathf.Atan2(pointingDirection.y, pointingDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, thetaVal);

        // LEFT CLICK HANDLES BOTH ACTIONS
        if (Input.GetMouseButtonDown(0))
        {
            if (currentProjectile == null)
            {
                FireBullet();       // first click -> shoot
            }
            else
            {
                WarpBullet();       // second click -> warp
            }
        }
    }

    // Shoots a bullet and remembers it as currentProjectile
    void FireBullet()
    {
        GameObject bulletClone = Instantiate(bullet);

        currentProjectile = bulletClone.GetComponent<warpPlayer>();

        if (currentProjectile != null)
        {
            currentProjectile.setOwner(this.gameObject);
        }

        bulletClone.transform.position = firePoint.position;
        bulletClone.transform.rotation = Quaternion.Euler(0, 0, thetaVal);

        bulletClone.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * bulletSpeed;
        Destroy(bulletClone, bulletLife);   // if it times out, Unity will treat currentProjectile as null
    }

    // Warps player to the current bullet, then clears it
    void WarpBullet()
    {
        if (currentProjectile != null)
        {
            transform.position = currentProjectile.transform.position;
            Destroy(currentProjectile.gameObject);
            currentProjectile = null;       // so next click will shoot again
        }
    }
}
