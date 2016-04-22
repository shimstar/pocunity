using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class dfcontroller : NetworkBehaviour
{
    private float torqueStep = 50;
    [SyncVar]
    private float speed = 0;
    private float speedMax = 30;
    private Rigidbody rb;
    public GameObject greenLaserPrefab;
    [SyncVar]
    public float hull;
    public float maxHull = 100.0f;

    public float getPrcentSpeed() {
        return speed / speedMax;
    }

    public float getPrcentHull()
    {
        return hull / maxHull;
    }


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Camera cam = Camera.main;
        cam.transform.parent = this.transform;
        cam.transform.position = new Vector3(0, 0, -1);
        this.name = "PlayerShip";
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        hull = maxHull;

    }

    void OnCollisionEnter(Collision collision)
    {
        hull -= 10;
    }

    [Command]
    void CmdApplyTorqueUp(float torque)
    {
        rb.AddTorque(transform.right * torque,ForceMode.Acceleration);
    }

    [Command]
    void CmdApplyTorqueLeft(float torque)
    {
        rb.AddTorque(transform.up * torque, ForceMode.Acceleration);
    }

    [Command]
    void CmdUpdateSpeed(float speedDelta)
    {
        if((speed+speedDelta)> speedMax)
        {
            speed = speedMax;
        }
        else
        {
            if ((speed + speedDelta) < 0)
            {
                speed = 0;
            }
            else
            {
                speed += speedDelta;
            }
        }
    }

    [Command]
    private void CmdShoot()
    {
        Vector3 location = transform.position + transform.forward * -2;
        GameObject bul = (GameObject)Instantiate(greenLaserPrefab, location, transform.rotation);
        //GameObject bul = (GameObject)Network.Instantiate(greenLaserPrefab, location, transform.rotation,0);
        NetworkServer.Spawn(bul);
        Rigidbody rbullet = bul.GetComponent<Rigidbody>();
        if (rbullet)
        {
            rbullet.AddForce(rbullet.transform.forward * -500, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        if (!isServer)
        {
            if (Input.GetKey(KeyCode.Z) == true)
            {
                CmdApplyTorqueUp( torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S) == true)
            {
                CmdApplyTorqueUp(-torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D) == true)
            {
                CmdApplyTorqueLeft( -torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Q) == true)
            {
                CmdApplyTorqueLeft( torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A) == true)
            {
                CmdUpdateSpeed(Time.deltaTime * 10);
            }
            else if (Input.GetKey(KeyCode.W) == true)
            {
                CmdUpdateSpeed(Time.deltaTime * -10);
            }

            if (Input.GetMouseButtonDown(0))
            {
                CmdShoot();
            }
        }
        else {
            rb.AddForce(rb.transform.forward * -speed, ForceMode.Acceleration);
        }
        
        
    }

   

    
}
