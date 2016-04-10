using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerhealth : MonoBehaviour {
    public float hull;
    public float maxHull = 100;
    public Slider HealthBar;

    // Use this for initialization
    void Start () {
        hull = maxHull;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
