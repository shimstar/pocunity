﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class connection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        Debug.Log("connect");
        SceneManager.LoadScene("scenes/level1");
    }
}
