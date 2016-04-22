using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class healthbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject PlayerObject = GameObject.Find("PlayerShip");
        if (PlayerObject) { 
            dfcontroller player = (dfcontroller)PlayerObject.GetComponent<dfcontroller>();
            if (player) { 
                Slider healthBar = (Slider)GetComponent<Slider>();
                healthBar.value = player.getPrcentHull();
            }
        }
    }
}
