using UnityEngine;
using System.Collections;

public class dfcontroller : MonoBehaviour {
    private float torqueStep = 1;
    private float speed=0;
    private float speedMax = 100;

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.Z)==true)
        {
            rb.AddTorque(transform.right * torqueStep);
        }
        else if (Input.GetKey(KeyCode.S) == true)
        {
            rb.AddTorque(transform.right * -torqueStep);
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            rb.AddTorque(transform.up* -torqueStep);
        }
        else if (Input.GetKey(KeyCode.Q) == true)
        {
            rb.AddTorque(transform.up* torqueStep);
        }
        else if (Input.GetKey(KeyCode.A) == true)
        {
            
            if ((speed+(Time.deltaTime * 10)) > speedMax)
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

        //  rb.AddForce(transform.forward * 100);
    }

    
}
