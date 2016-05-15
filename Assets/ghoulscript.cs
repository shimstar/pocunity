using UnityEngine;
using System.Collections;

public class ghoulscript : ship {
    private GameObject targetUI;
    private GameObject goPoint;
    public float force = 0.1f;
    

    // Use this for initialization
    void Start () {
        goPoint = GameObject.Find("gopoint");
        this.rb = GetComponent<Rigidbody>();
        speed = 200;
    }
	
	// Update is called once per frame
	void Update () {
        if (isServer) {

            /* if(goPoint != null)
             {
                 Vector3 targetDelta = goPoint.transform.position - transform.position;
                 //get the angle between transform.forward and target delta
                 float angleDiff = Vector3.Angle(transform.forward, targetDelta);

                 // get its cross product, which is the axis of rotation to
                 // get from one vector to the other
                 Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

                 // apply torque along that axis according to the magnitude of the angle.
                 rb.AddTorque(cross * angleDiff * force,ForceMode.Impulse);
                 Debug.Log(cross * angleDiff * force);
             }*/
            rb.AddForce(rb.transform.forward * -speed, ForceMode.Acceleration);
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
