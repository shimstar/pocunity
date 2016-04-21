using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class shimNWManager : NetworkManager {

    public override void OnStartServer()
    {
        base.OnStartServer();
        playerPrefab = Resources.Load("ships/dark_fighter_631") as GameObject;
        GameObject pref = Resources.Load("ships/dark_fighter_631") as GameObject;
        ClientScene.RegisterPrefab(pref);
        pref = Resources.Load("ships/lasergreen") as GameObject;
        ClientScene.RegisterPrefab(pref);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        // NetworkServer.Spawn(df);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject pref = Resources.Load("ships/dark_fighter_631") as GameObject;
        base.OnServerAddPlayer(conn, playerControllerId);
        GameObject df = (GameObject)Instantiate(pref);
        NetworkServer.AddPlayerForConnection(conn, df, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        ClientScene.AddPlayer(0);
        GameObject pref = Resources.Load("ships/dark_fighter_631") as GameObject;
        ClientScene.RegisterPrefab(pref);
        pref = Resources.Load("ships/lasergreen") as GameObject;
        ClientScene.RegisterPrefab(pref);
        pref = Resources.Load("ExplosionShim") as GameObject;
        ClientScene.RegisterPrefab(pref);
    }



}
