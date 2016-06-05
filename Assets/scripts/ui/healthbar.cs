using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class healthbar : MonoBehaviour {

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
            Debug.Log("lll " + plShip);
            if (plShip != null)
            {
                ship shipScript = plShip.GetComponent<ship>();
               if (shipScript)
                {
                    Slider healthBar = (Slider)GetComponent<Slider>();
                    healthBar.value = shipScript.getPrcentHull();
                }


            }
        }
        
    }
}
