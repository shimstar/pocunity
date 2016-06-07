using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AsteroidScript : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
         if (isServer)
         {
            ShipScript sc = collision.gameObject.GetComponent<ShipScript>();
            if (sc)
            {
                sc.setDamage(10);
                sc.setSpeed(0);
            }
         }

    }

    // Update is called once per frame
    void Update () {
	
	}
}
