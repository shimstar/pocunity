using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
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
    public InputField loginField;
    public InputField passwordField;
    public Button connectBtn;
    public Button playBtn;
    public Text failed;
    public Text success;
       

    // Use this for initialization
    void Start () {
        failed.gameObject.SetActive(false);
        success.gameObject.SetActive(false);
        playBtn.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void checkLogin()
    {
        //Debug.Log("connect");
        //  SceneManager.LoadScene("scenes/level1");
         
        string jsonData = "{\"login\":\"" + loginField.text +"\",\"password\":\"" + passwordField.text + "\"}";
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
            if (login.status == "1")
            {
                failed.gameObject.SetActive(false);
                success.gameObject.SetActive(true);
                connectBtn.gameObject.SetActive(false);
                playBtn.gameObject.SetActive(true);
            }
            else
            {
                failed.gameObject.SetActive(true);

            }
       
        }
       
    }

    public void play()
    {
        SceneManager.LoadScene("scenes/level1");
    }




    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server" + client.isConnected);

    }

}
