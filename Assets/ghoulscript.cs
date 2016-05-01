using UnityEngine;
using System.Collections;

public class ghoulscript : ship {
    private GameObject targetUI;

	// Use this for initialization
	void Start () {
        targetUI = (GameObject)Instantiate(targetUIPrefab);
    }
	
	// Update is called once per frame
	void Update () {
        GameObject playerShip = GameObject.Find("PlayerShip");
        if (playerShip != null)
        {
            Camera cam = Camera.main;
            targetUI.transform.position = cam.ViewportToWorldPoint(transform.position);
        }

	}
}
