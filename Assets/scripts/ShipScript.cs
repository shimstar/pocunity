﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShipScript : NetworkBehaviour
{
    
    [SyncVar]
    private int faction;
    private GameObject floatingNameText = null;
    private GameObject targetUi = null;
    protected float torqueStep = 80;
    [SyncVar]
    protected float speed = 0;
    protected float speedMax = 30;
    protected float stepAcceleration = 25;
    protected bool shipFromLocalPlayer = false;
    protected Rigidbody rb;
    [SyncVar]
    protected float hull;
    protected float maxHull = 100.0f;
    public Texture2D targetUITexture;

    public GameObject getFloatingNameText()
    {
        return this.floatingNameText;
    }

    public int getFaction()
    {
        return this.faction;
    }

    public void setFaction(int faction)
    {
        this.faction = faction;
    }

    public void setFloatingNameText(GameObject flText)
    {
        this.floatingNameText = flText;
    }

    public GameObject getTargetUi()
    {
        return this.targetUi;
    }

    public void setTargetUi(GameObject tUI)
    {
        this.targetUi = tUI;
    }

    public float getHull()
    {
        return hull;
    }

    public void setSpeed(float pSpeed)
    {
        this.speed = pSpeed;
    }

    public float getPrcentSpeed()
    {
        return speed / speedMax;
    }

    public float getPrcentHull()
    {
        return hull / maxHull;
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
        faction = 0;
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
                PlayerScript ps = playerOverScene.GetComponent<PlayerScript>();
                if (ps != null)
                {
                    ps.manageDeath();
                }
            }
        }
    }
}
