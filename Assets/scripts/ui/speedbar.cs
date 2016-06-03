using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class speedbar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject PlayerObject = GameObject.Find("PlayerOverScene");
        PlayerScrip ps = PlayerObject.GetComponent<PlayerScrip>();
        if (ps)
        {
            GameObject plShip = ps.getShip();
            if (plShip)
            {
                ship shipScript = plShip.GetComponent<ship>();
                if (shipScript)
                {
                    Slider speedBar = (Slider)GetComponent<Slider>();
                    speedBar.value = shipScript.getPrcentSpeed();
                }


            }
        }
    }
}
