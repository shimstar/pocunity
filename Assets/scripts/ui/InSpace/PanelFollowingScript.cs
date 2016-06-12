using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFollowingScript : MonoBehaviour {
    private GameObject shipToFollow;

    public void setShipToFollow(GameObject ship)
    {
        //Debug.Log("aroira");
        this.shipToFollow = ship;
    }

	// Use this for initialization
	void Start () {
        //Debug.Log("start PanelFoolowingScript");
        Image img = this.gameObject.GetComponent<Image>();
        //Debug.Log(img.color);
        //img.color = new Color(112,51,136);
        img.color = Color.red;
        //Debug.Log(img.color);
        //Debug.Log(this.gameObject.activeSelf);
    }
	
	// Update is called once per frame
	void Update () {
        

	}
}
