using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class bulletscript : NetworkBehaviour
{
    private float timer;
    private float damage;
    // Use this for initialization
    void Start () {
        timer = 0;
        damage = 20;
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
            
            if(collision.gameObject.tag == "ship")
            {
                /*dfcontroller dfcontrol = collision.gameObject.GetComponent<dfcontroller>();
                if (dfcontrol != null)
                {
                    dfcontrol.setDamage(damage);
                }*/
                ship shipControl = collision.gameObject.GetComponent<ship>();
                
                if (shipControl != null)
                {
                    Debug.Log("damage");
                    shipControl.setDamage(damage);
                }else
                {
                    Debug.Log("not damage");
                }
            }
            Debug.Log("pppp");
            GameObject explosion = Resources.Load("ExplosionShim") as GameObject;
            GameObject expl = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            NetworkServer.Spawn(expl);
            Destroy(this.gameObject);
            
        }
    }
}
