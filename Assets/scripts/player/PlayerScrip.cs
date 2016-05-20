using UnityEngine;
using System.Collections;

public class PlayerScrip : MonoBehaviour {
    private string playerName;
    private GameObject ship;
    private static GameObject currentPlayer;
    private int id;

    public void setName(string name)
    {
        playerName = name;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void manageDeath()
    {
        GameObject deathUi = GameObject.Find("/Canvas/deathpanel");
        if (deathUi != null)
        {
            deathUi.SetActive(true);
        }
    }

    public static GameObject getCurrentPlayer()
    {
        return PlayerScrip.currentPlayer;
    }

	// Use this for initialization
	void Start () {
        PlayerScrip.currentPlayer = this.gameObject;
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
