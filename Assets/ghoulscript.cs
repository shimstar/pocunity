using UnityEngine;
using System.Collections;

public class ghoulscript : ship {
    private GameObject targetUI;
    private GameObject goPoint;
    private GameObject lookAtSphere;
    public float force = 0.1f;
    private float torque = 1.0f;
    

    // Use this for initialization
    void Start () {
        goPoint = GameObject.Find("gopoint");
        this.rb = GetComponent<Rigidbody>();
        speed = 50;
        lookAtSphere = GameObject.Find("/" + this.name + "/lookat");
    }
	
	// Update is called once per frame
	void Update () {
        if (isServer) {

             if(goPoint != null)
             {
                lookAtSphere.transform.LookAt(goPoint.transform);
                
                
                float diffX = transform.rotation.x - lookAtSphere.transform.rotation.x;
                float diffY = transform.rotation.y - lookAtSphere.transform.rotation.y;
                float diffZ = transform.rotation.z - lookAtSphere.transform.rotation.z;
                Debug.Log(diffX +"/" + diffY + "/" +diffZ);

                if (diffX < 0)
                {
                    rb.AddTorque(transform.up * torque, ForceMode.Acceleration);
                }
                else
                {
                    if (diffX > 0.1)
                    {
                        rb.AddTorque(transform.up * -torque, ForceMode.Acceleration);
                    }
                }
                /*
               if(cross.x < -5)
               {
                   rb.AddTorque(transform.up * torque, ForceMode.Acceleration);
               }else
               {
                   if(cross.x > 5)
                   {
                       rb.AddTorque(transform.up * -torque, ForceMode.Acceleration);
                   }
               }

               if(cross.y < -5)
               {
                   rb.AddTorque(transform.right * torque, ForceMode.Acceleration);
               }else
               {
                   if (cross.y > 5)
                       rb.AddTorque(transform.right * -torque, ForceMode.Acceleration);
               }

                // apply torque along that axis according to the magnitude of the angle.
                //rb.AddTorque(cross * angleDiff * force,ForceMode.Impulse);
                */


            }
           // rb.AddForce(rb.transform.forward * speed, ForceMode.Acceleration);
        }


    }

    void OnGUI()
    {
        GameObject playerShip = GameObject.Find("PlayerShip");
        if (playerShip != null)
        {
            Camera camera = GetComponent<Camera>();

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Debug.Log(screenPos);
            if (screenPos.z>0 && screenPos.y>0 && screenPos.x > 0) { 
                GUI.DrawTexture(new Rect(screenPos.x - targetUITexture.width / 2, Screen.height-screenPos.y- targetUITexture.height/2, targetUITexture.width, targetUITexture.height), targetUITexture);
            }
        }

    }
}
