using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ghoulscript : ship {
    private GameObject targetUI;
    
    public float force = 0.1f;
    private float torque = 1.0f;
    private int gopointNb = 0;
    GameObject target = null;
    private float lastShootTicks = 0.0f;
    private ArrayList patrolPoints = new ArrayList();
    

    // Use this for initialization
    void Start () {
        this.rb = GetComponent<Rigidbody>();
        speed = 30;
        torqueStep = 50;
        patrolPoints.Add(new Vector3(0, 2, 405));
        patrolPoints.Add(new Vector3(2100, 3, 1145));
        patrolPoints.Add(new Vector3(750, 3, -330));
    }
	
    float moveTo(Vector3 posTarget)
    {
        Vector3 localPoint = transform.InverseTransformDirection(transform.position - posTarget);
        float distance = Mathf.Sqrt(Mathf.Abs((posTarget.x - transform.position.x) * (posTarget.x - transform.position.x) + (posTarget.y - transform.position.y) * (posTarget.y - transform.position.y) + (posTarget.z - transform.position.z) * (posTarget.z - transform.position.z)));
        //Debug.Log(distance + "///" + localPoint);
        float factor = 0.0f;

        factor = localPoint.x > 20 ? -1.0f : (localPoint.x < -20 ? 1.0f : (localPoint.x > 1 ? -0.25f : (localPoint.x < -1 ? 0.25f : 0)));

        rb.AddTorque(rb.transform.up * torqueStep * Time.deltaTime * factor, ForceMode.Acceleration);

        factor = factor < 1 ? (localPoint.y > 20 ? 1.0f : (localPoint.y < -20 ? -1.0f : (localPoint.y > 1 ? 0.25f : (localPoint.y < -1 ? -0.25f : 0)))) : 0;
        rb.AddTorque(rb.transform.right * torqueStep * Time.deltaTime * factor, ForceMode.Acceleration);

        //rb.AddForce(rb.transform.forward * speed, ForceMode.Acceleration);

        return distance;
    }

    bool checkPositionTofire(Vector3 posTarget)
    {
        bool result = false;

        Vector3 localPoint = transform.InverseTransformDirection(transform.position - posTarget);
        
        if ( Mathf.Abs(localPoint.x) <= 1 && Mathf.Abs(localPoint.y) <= 1)
        {
            result = true;
        }

        return result;
    }

    void fire()
    {
        if(Time.time - lastShootTicks > 0.1) { 
            GameObject redbullet = Resources.Load("ships/redbullet") as GameObject;
            GameObject bul = (GameObject)Instantiate(redbullet);
            Vector3 location = transform.position + transform.forward * 4;
            NetworkServer.Spawn(bul);
            Rigidbody rbullet = bul.GetComponent<Rigidbody>();
            if (rbullet)
            {
                rbullet.transform.position = location;
                rbullet.transform.rotation = transform.rotation;
                rbullet.AddForce(rbullet.transform.forward * 5000, ForceMode.Acceleration);
            }

            lastShootTicks = Time.time;
        }
    }

	// Update is called once per frame
	void Update () {
        if (isServer) {
            float distance = 0.0f;

            target = GameObject.Find("cible");

            if (target == null) {
                speed = 30;
                if (patrolPoints.Count >= 1) {
                    /// using torque
                    distance = moveTo((Vector3)patrolPoints[gopointNb]);
                    if (distance < 100)
                    {
                        gopointNb += 1;
                        gopointNb %= 3;
                    }
                }
            }else
            {
                distance = moveTo(target.transform.position);
                //speed = distance > 1000 ? 70 : (distance > 500 ? 50 : (distance > 200 ? 30 : 0));
                if (checkPositionTofire(target.transform.position) == true)
                {
                   // fire();
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
