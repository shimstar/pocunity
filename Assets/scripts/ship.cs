using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ship : NetworkBehaviour
{
    [SyncVar]
    public string name;
    public GameObject floatingNameText = null;
    protected float torqueStep = 80;
    [SyncVar]
    protected float speed = 0;
    protected float speedMax = 30;
    protected bool shipFromLocalPlayer = false;
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

        if (hull <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void onDestroy()
    {
        if (shipFromLocalPlayer == true)
        {
            GameObject playerOverScene = GameObject.Find("PlayerOverScene");
            if (playerOverScene != null)
            {
                PlayerScrip ps = playerOverScene.GetComponent<PlayerScrip>();
                if (ps != null)
                {
                    ps.manageDeath();
                }
            }
        }
    }
}
