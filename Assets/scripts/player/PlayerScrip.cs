﻿using UnityEngine;
using System.Collections;

public class PlayerScrip : MonoBehaviour {
    private string playerName;
    private GameObject ship;
    private static GameObject currentPlayer;

    public static GameObject getCurrentPlayer()
    {
        return PlayerScrip.currentPlayer;
    }

	// Use this for initialization
	void Start () {
        PlayerScrip.currentPlayer = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
