using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Collections.Generic;
using System;

[Serializable]
public class login
{
    public string code;
    public string status;
    public string id;
}

public class connection : MonoBehaviour {
    private NetworkClient client;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        //Debug.Log("connect");
        //  SceneManager.LoadScene("scenes/level1");

        string jsonData = "{\"login\":\"test\",\"password\":\"test\"}";
        Dictionary<string, string> data = new Dictionary<string, string>();
       
        data.Add("Content-Type", "application/json");
        byte[] pData = Encoding.ASCII.GetBytes(jsonData.ToCharArray());
        
        WWW www = new WWW("http://127.0.0.1:15881/connect",pData,data);
        StartCoroutine("GetdataEnumerator", www);

    }

       IEnumerator GetdataEnumerator(WWW www)
    {
        //Wait for request to complete
        yield return null;
        yield return www;
    
        if (!string.IsNullOrEmpty(www.text))
        {
            var login = JsonUtility.FromJson<login>(www.text);
       
        }
       
    }




    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server" + client.isConnected);

    }

}
