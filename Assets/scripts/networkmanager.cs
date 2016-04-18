using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class networkmanager : MonoBehaviour
{

    public bool isAtStartup = true;
    NetworkClient client;
    public GameObject dfPrefab;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isAtStartup)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetupServer();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                SetupClient();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                SetupServer();
                SetupLocalClient();
            }
        }
    }

    void OnGUI()
    {
        if (isAtStartup)
        {
            GUI.Label(new Rect(2, 10, 150, 100), "Press S for server");
            GUI.Label(new Rect(2, 30, 150, 100), "Press B for both");
            GUI.Label(new Rect(2, 50, 150, 100), "Press C for client");
        }
    }

    public void SetupServer()
    {
        NetworkServer.Listen(4444);
        isAtStartup = false;
 

    }

    // Create a client and connect to the server port
    public void SetupClient()
    {

        ClientScene.RegisterPrefab(dfPrefab);
        client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.Connect("127.0.0.1", 4444);
       
        isAtStartup = false;
        
    }

    // Create a local client and connect to the local server
    public void SetupLocalClient()
    {
        client = ClientScene.ConnectLocalServer();
        client.RegisterHandler(MsgType.Connect, OnConnected);
        isAtStartup = false;
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server" + client.isConnected);

    }

}
