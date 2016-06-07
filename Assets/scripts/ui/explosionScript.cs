using UnityEngine;
using System.Collections;

public class explosionScript : MonoBehaviour {
    private float timer;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if (timer > 5)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
	}
}
