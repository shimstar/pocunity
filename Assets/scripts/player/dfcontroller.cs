using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class dfcontroller : NetworkBehaviour
{
    private float torqueStep = 3;
    private float speed = 0;
    private float speedMax = 30;
    private Rigidbody rb;
    public GameObject greenLaserPrefab;
    public float hull;
    public float maxHull = 100.0f;

    public float getPrcentSpeed() {
        return speed / speedMax;
    }

    public float getPrcentHull()
    {
        return hull / maxHull;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Camera cam = Camera.main;
        cam.transform.parent = this.transform;
        cam.transform.position = new Vector3(0, 0, -1);
        hull = maxHull;

    }

    void OnCollisionEnter(Collision collision)
    {
        hull -= 10;
    }

    void Update()
    {
        if (rb)
        {
            if (Input.GetKey(KeyCode.Z) == true)
            {
                rb.AddTorque(transform.right * torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S) == true)
            {
                rb.AddTorque(transform.right * -torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D) == true)
            {
                rb.AddTorque(transform.up * -torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Q) == true)
            {
                rb.AddTorque(transform.up * torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A) == true)
            {

                if ((speed + (Time.deltaTime * 10)) > speedMax)
                {
                    speed = speedMax;
                }
                else
                {
                    speed += Time.deltaTime * 10;
                }


            }
            else if (Input.GetKey(KeyCode.W) == true)
            {

                if ((speed - (Time.deltaTime * 10)) < 0)
                {
                    speed = 0;
                }
                else
                {
                    speed -= Time.deltaTime * 10;
                }


            }

            rb.AddForce(rb.transform.forward * -speed, ForceMode.Acceleration);

        }
       

       
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
        
    }

    private void shoot()
    {
        Vector3 location = transform.position + transform.forward * -2;
        GameObject bul = (GameObject)Instantiate(greenLaserPrefab, location, transform.rotation);
        Rigidbody rbullet = bul.GetComponent<Rigidbody>();
        if (rbullet)
        {
            rbullet.AddForce(rbullet.transform.forward * -500, ForceMode.Acceleration);
        }
    }

    
}
