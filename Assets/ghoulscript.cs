using UnityEngine;
using System.Collections;

public class ghoulscript : ship {
    private GameObject targetUI;
    
    private GameObject lookAtSphere;
    public float force = 0.1f;
    private float torque = 1.0f;
    private int gopointNb = 0;
    private ArrayList patrolPoints = new ArrayList();
    

    // Use this for initialization
    void Start () {
        this.rb = GetComponent<Rigidbody>();
        speed = 50;
        lookAtSphere = GameObject.Find("/" + this.name + "/lookat");
        torqueStep = 50;
        patrolPoints.Add(new Vector3(0, 2, 405));
        patrolPoints.Add(new Vector3(2100, 3, 1145));
        patrolPoints.Add(new Vector3(750, 3, -330));
    }
	
	// Update is called once per frame
	void Update () {
        if (isServer) {


            if (patrolPoints.Count >= 1) { 
                /// using torque
                //Vector3 localPoint = rb.transform.InverseTransformPoint(goPoint.transform.position);
                 Vector3 localPoint = transform.InverseTransformDirection(transform.position - (Vector3)patrolPoints[gopointNb] );
                 float distance =Mathf.Sqrt(Mathf.Abs((((Vector3)patrolPoints[gopointNb]).x - transform.position.x) * (((Vector3)patrolPoints[gopointNb]).x - transform.position.x) + (((Vector3)patrolPoints[gopointNb]).y - transform.position.y) * (((Vector3)patrolPoints[gopointNb]).y - transform.position.y) + (((Vector3)patrolPoints[gopointNb]).z - transform.position.z) * (((Vector3)patrolPoints[gopointNb]).z - transform.position.z)));
                 Debug.Log(localPoint + "////" + localPoint.magnitude + "////" + distance);
                 //Debug.Log(lookAtSphere.transform.position + "///" + transform.position);
                 float factor = 0.0f;

                 factor = localPoint.x > 20 ? -1.0f : (localPoint.x < -20 ? 1.0f : (localPoint.x>2? -0.25f:(localPoint.x < -2 ? 0.25f : 0)));


                 rb.AddTorque(rb.transform.up * torqueStep * Time.deltaTime * factor, ForceMode.Acceleration);

                 factor = factor < 1 ? (localPoint.y > 20 ? 1.0f : (localPoint.y < -20 ? -1.0f : (localPoint.y > 2 ? 0.25f : (localPoint.y < -2 ? -0.25f : 0)))): 0;
                 rb.AddTorque(rb.transform.right * torqueStep * Time.deltaTime * factor, ForceMode.Acceleration);

                 rb.AddForce(rb.transform.forward * speed, ForceMode.Acceleration);
                //Debug.Log(a);
                if (distance < 100)
                {
                    gopointNb += 1;
                    gopointNb %= 3;
                }
            }
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
