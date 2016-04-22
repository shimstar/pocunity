using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class bulletscript : NetworkBehaviour
{
    private float timer;
    // Use this for initialization
    void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 20)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isServer) { 
            GameObject explosion = Resources.Load("ExplosionShim") as GameObject;
            GameObject bul = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            NetworkServer.Spawn(bul);
            Destroy(this.gameObject);
            
        }
    }
}
