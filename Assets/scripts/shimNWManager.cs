using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class shimNWManager : NetworkManager {

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        GameObject df = (GameObject)Instantiate(Resources.Load("dark_fighter_631"));
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        Debug.Log("POGI2");
    }

}
