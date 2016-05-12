using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class level1StartScript : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
        GameObject pref = Resources.Load("ships/GhoulOBJ") as GameObject;
        GameObject ennemy = (GameObject)Instantiate(pref);
        ennemy.transform.position.Set(600, 600, 600);
        //NetworkServer.Spawn(ennemy);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
