using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShimNetworkManager : NetworkManager {

    public void Start()
    {
        GameObject playerOverScene = GameObject.Find("PlayerOverScene");
        if (playerOverScene == null)
        {
            bool result =this.StartServer();
            Debug.Log("start server = " + result);
            if (result == true)
            {
                level1StartScript lsc = this.GetComponent<level1StartScript>();

                if (lsc != null)
                {
                    //lsc.generateLevel();
                    lsc.initLevel();
                }
            }
        }else
        {
            this.StartClient();
        }
    }

    public void respawn()
    {
        ClientScene.AddPlayer(0);
    }
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        
        this.playerPrefab = Resources.Load("ships/dark_fighter_631") as GameObject;
        GameObject pref = Resources.Load("ships/dark_fighter_631") as GameObject;
        ClientScene.RegisterPrefab(pref);
        pref = Resources.Load("ships/lasergreen") as GameObject;
        ClientScene.RegisterPrefab(pref);
        Debug.Log("onStartServer");
        
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject pref = Resources.Load("ships/dark_fighter_631") as GameObject;
        base.OnServerAddPlayer(conn, playerControllerId);

    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        ClientScene.AddPlayer(0);
        GameObject pref = Resources.Load("ships/dark_fighter_631") as GameObject;
        ClientScene.RegisterPrefab(pref);
        pref = Resources.Load("ships/lasergreen") as GameObject;
        ClientScene.RegisterPrefab(pref);
        pref = Resources.Load("ships/ghoul3") as GameObject;
        ClientScene.RegisterPrefab(pref);
        pref = Resources.Load("ships/redbullet") as GameObject;
        ClientScene.RegisterPrefab(pref);
        pref = Resources.Load("ships/greenbullet") as GameObject;
        ClientScene.RegisterPrefab(pref);

        pref = Resources.Load("ExplosionShim") as GameObject;
        ClientScene.RegisterPrefab(pref);
        Debug.Log("Client Connected");
        level1StartScript lsc = this.GetComponent<level1StartScript>();

        if (lsc != null)
        {
            lsc.loadLevel();
        }
    }



}
