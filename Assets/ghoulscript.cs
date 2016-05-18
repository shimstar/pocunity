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
                /// hummmm
                Vector3 direction = goPoint.transform.position  - transform.position;
                float angleDiff = Vector3.Angle(transform.up, direction);
                Vector3 cross = Vector3.Cross(transform.up, direction);
                rb.AddTorque(transform.up * direction.magnitude * Time.deltaTime, ForceMode.Acceleration);

                /// using slerp
               /* Vector3 direction = goPoint.transform.position  - transform.position;
                direction.z = -direction.z;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);*/
                //rb.AddForce(rb.transform.forward * speed * Time., ForceMode.Acceleration); 

                /// using moveRotation
                /*Vector3 direction = goPoint.transform.position - transform.position;
                Vector3 localPoint = transform.InverseTransformDirection(goPoint.transform.position);
                float angle = Mathf.Atan2(localPoint.x, localPoint.z) * Mathf.Rad2Deg;
                Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);

                Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);

                angle = Mathf.Atan2(localPoint.y, localPoint.x) * Mathf.Rad2Deg;
                eulerAngleVelocity = new Vector3(0, 0, angle);
                deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotation);

                rb.AddForce(rb.transform.forward * speed, ForceMode.Acceleration);*/

                /// using torque
                //Vector3 localPoint = rb.transform.InverseTransformPoint(goPoint.transform.position);
                /* Vector3 localPoint = transform.InverseTransformDirection(goPoint.transform.position - transform.position);
                 float distance =Mathf.Sqrt(goPoint.transform.position.x - transform.position.x) * (goPoint.transform.position.x - transform.position.x) + (goPoint.transform.position.x - transform.position.x) * (goPoint.transform.position.x - transform.position.x) + (goPoint.transform.position.y - transform.position.y) * (goPoint.transform.position.z - transform.position.z);
                 Debug.Log(localPoint + "////" + localPoint.magnitude + "////" + distance);
                 //Debug.Log(lookAtSphere.transform.position + "///" + transform.position);
                 float factor = 0.0f;

                 factor = localPoint.x > 20 ? -1.0f : (localPoint.x < -20 ? 1.0f : (localPoint.x>2? -0.25f:(localPoint.x < -2 ? 0.25f : 0)));


                 rb.AddTorque(rb.transform.up * torqueStep * Time.deltaTime * factor, ForceMode.Acceleration);

                 factor = factor == 0 ? (localPoint.y > 20 ? 1.0f : (localPoint.y < -20 ? -1.0f : (localPoint.y > 2 ? 0.25f : (localPoint.y < -2 ? -0.25f : 0)))): 0;
                 rb.AddTorque(rb.transform.right * torqueStep * Time.deltaTime * factor, ForceMode.Acceleration);

                 if (factor==0) rb.AddForce(rb.transform.forward * speed, ForceMode.Acceleration);*/
                //Debug.Log(a);


            }
           //
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
