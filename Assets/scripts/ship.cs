using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ship : NetworkBehaviour
{
    protected float torqueStep = 80;
    [SyncVar]
    protected float speed = 0;
    protected float speedMax = 30;
    protected Rigidbody rb;
    [SyncVar]
    protected float hull;
    protected float maxHull = 100.0f;
    public Texture2D targetUITexture;

    public float getHull()
    {
        return hull;
    }

    public void setDamage(float damage)
    {
        hull -= damage;
    }
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
