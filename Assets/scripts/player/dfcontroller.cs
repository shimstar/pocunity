﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class dfcontroller : MonoBehaviour {
    private float torqueStep = 3;
    private float speed=0;
    private float speedMax = 30;
    private Rigidbody rb;
    public Slider speedBar;
    public GameObject greenLaserPrefab;

    void start()
    {
         rb = GetComponent<Rigidbody>();
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
        else
        {
            rb = GetComponent<Rigidbody>();
        }

        float prcentSpeed = speed / speedMax;
        speedBar.value = prcentSpeed;
        
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
        
    }

    private void shoot()
    {
        GameObject bul = (GameObject)Instantiate(greenLaserPrefab, transform.position, transform.rotation);

    }

    
}
