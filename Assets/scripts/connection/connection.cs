using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

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
        Debug.Log("connect");
        SceneManager.LoadScene("scenes/level1");
      /*  client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.Connect("127.0.0.1", 7777);*/
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server" + client.isConnected);

    }

}
