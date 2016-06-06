using UnityEngine;
using System.Collections;

public class DeathPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	
	// Update is called once per frame
	void Update () {
	
	}

    public void onRespawnClick()
    {
        GameObject shimNWObject = GameObject.Find("NetworkManager");
        ShimNetworkManager shimNWscript = shimNWObject.GetComponent<ShimNetworkManager>();
        if (shimNWscript)
        {
            shimNWscript.respawn();
        }

    }
}
