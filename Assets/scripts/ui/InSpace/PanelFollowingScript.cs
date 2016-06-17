using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFollowingScript : MonoBehaviour {
    Text nameText;
    Text distanceText;
    Slider hullSlider;

	// Use this for initialization
	void Start () {
        nameText = this.transform.Find("name").gameObject.GetComponent<Text>();
        hullSlider = this.transform.Find("life").gameObject.GetComponent<Slider>();
        distanceText = this.transform.Find("distance").gameObject.GetComponent<Text>();
    }

    public void updateUi(string name, float prcentHull, int faction, float distance)
    {
        if(nameText == null)
        {
            nameText = this.transform.Find("name").gameObject.GetComponent<Text>();
            hullSlider = this.transform.Find("life").gameObject.GetComponent<Slider>();
            distanceText = this.transform.Find("distance").gameObject.GetComponent<Text>();
        }
        
        nameText.text = name;
        if (faction == 0)
        {
            nameText.color = Color.red;
        }
        
        hullSlider.value = prcentHull;

        distanceText.text = distance.ToString();
        
    }
	
	// Update is called once per frame
	void Update () {
        

	}
}
