using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class bulletscript : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (isServer) { 
            GameObject explosion = Resources.Load("ExplosionShim") as GameObject;
            GameObject bul = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            NetworkServer.Spawn(bul);
            //Rigidbody rbullet = bul.GetComponent<Rigidbody>();
            Destroy(this.gameObject);
            
        }
    }
}
