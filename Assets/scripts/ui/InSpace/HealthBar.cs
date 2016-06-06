using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

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
            if (plShip != null)
            {
                ShipScript shipScript = plShip.GetComponent<ShipScript>();
                if (shipScript)
                {
                    Slider healthBar = (Slider)GetComponent<Slider>();
                    healthBar.value = shipScript.getPrcentHull();
                }


            }
        }
        
    }
}
