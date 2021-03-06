﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private string playerName;
    public GameObject ship;
    private static GameObject currentPlayer;
    private int id;
    private string ipToGo = "0.0.0.0";

    public string getPlayerName()
    {
        return playerName;
    }

    public void setPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public string getIpToGo()
    {
        return ipToGo;
    }

    public void setIpToGo(string ip)
    {
        ipToGo = ip;
    }


    public void setShip(GameObject ship)
    {
        this.ship = ship;
    }

    public GameObject getShip()
    {
        return this.ship;
    }

    public void setName(string name)
    {
        playerName = name;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void manageDeath()
    {
        GameObject deathUi = GameObject.Find("/Canvas/deathpanel");
        if (deathUi != null)
        {
            deathUi.SetActive(true);
        }
    }

    public static GameObject getCurrentPlayer()
    {
        return PlayerScript.currentPlayer;
    }

	// Use this for initialization
	void Start () {
        PlayerScript.currentPlayer = this.gameObject;
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
