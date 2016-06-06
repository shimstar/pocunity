using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject PlayerObject = GameObject.Find("PlayerOverScene");
        PlayerScript ps = PlayerObject.GetComponent<PlayerScript>();
        if (ps)
        {
            GameObject plShip = ps.getShip();
            if (plShip)
            {
                ShipScript shipScript = plShip.GetComponent<ShipScript>();
                if (shipScript)
                {
                    Slider speedBar = (Slider)GetComponent<Slider>();
                    speedBar.value = shipScript.getPrcentSpeed();
                }


            }
        }
    }
}
