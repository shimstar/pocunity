using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class speedbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject PlayerObject = GameObject.Find("Player");
        if (PlayerObject) { 
            dfcontroller player = (dfcontroller)PlayerObject.GetComponent<dfcontroller>();
            if (player) { 
                Slider speedBar = (Slider)GetComponent<Slider>();
                speedBar.value = player.getPrcentSpeed();
            }
        }
    }
}
