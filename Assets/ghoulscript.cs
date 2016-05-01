using UnityEngine;
using System.Collections;

public class ghoulscript : ship {
    private GameObject targetUI;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        

	}

    void OnGUI()
    {
        GameObject playerShip = GameObject.Find("PlayerShip");
        if (playerShip != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.DrawTexture(new Rect(screenPos.x, screenPos.y, targetUITexture.width, targetUITexture.height), targetUITexture);
        }

    }
}
