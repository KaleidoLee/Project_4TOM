using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1_Ricochet : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] Camera cam;
    [SerializeField] GameObject bulletPrefab;

    public Vector2 lookDirection;
    public float lookAngle;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        lookDirection = cam.ScreenToWorldPoint(Input.mousePosition);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 250f;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}

