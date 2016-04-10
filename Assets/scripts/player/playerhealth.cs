using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerhealth : MonoBehaviour {
    public float hull;
    public float maxHull = 100.0f;
    public Slider HealthBar;
    
    // Use this for initialization
    void Start () {
        hull = 30.0f;
        
    }
	
	// Update is called once per frame
	void Update () {
        float prcentHealth = hull/maxHull;
        HealthBar.value = prcentHealth;

	}
}
