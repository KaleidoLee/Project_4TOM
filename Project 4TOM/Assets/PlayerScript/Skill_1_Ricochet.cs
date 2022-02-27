using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Photon.Pun;

public class Skill_1_Ricochet : MonoBehaviour
{
    [SerializeField] GameObject firePoint;
    [SerializeField] Camera cam;
    [SerializeField] GameObject bulletPrefab;

    public Vector2 lookDirection;
    public float lookAngle;

    LineRenderer lr;

    //multiplayer parameters
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        firePoint = this.gameObject.transform.GetChild(0).gameObject;

        lr = firePoint.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is me
        if (view.IsMine)
        {
            lookDirection = cam.ScreenToWorldPoint(Input.mousePosition); //get Vector2 component of mouse position on screen
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 180f; //calculate angle between firepoint and mouse position
            firePoint.transform.rotation = Quaternion.Euler(0, 0, lookAngle); //firepoint rotates towards mouse position

            //create a line of trajectory 
            Vector2[] trajectory = Plot(bulletPrefab.GetComponent<Rigidbody2D>(), firePoint.transform.position, lookDirection, 500);

            //create a new point in the line renderer for each element in the list
            lr.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }
            lr.SetPositions(positions); //set position of points for each element in Vector3[] positions

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);

    }

    //function to plot each points in the preview line for line renderer
    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps]; //create a new Vector2 list

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos; //put the Vector2 position into list results[]
        }

        return results; //return list
    }
}

