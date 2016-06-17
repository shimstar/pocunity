using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class DarkFighterController : ShipScript
{
    public GameObject greenLaserPrefab;
    public Texture2D crosshairImage;
    
    private float calcDistance(GameObject target)
    {
        float value = 0;

        Vector3 posTarget = target.transform.position;
        float dx = posTarget.x - transform.position.x;
        float dy = posTarget.y - transform.position.y;
        float dz = posTarget.z - transform.position.z;

        value = (float) Math.Round(Math.Sqrt(dx*dx+dy*dy+dz*dz),2);

        return value;
    }
    void OnGUI()
    {
        if (isClient == true)
        {
          /*  float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
            float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
            */
            GameObject[] listOfShips = GameObject.FindGameObjectsWithTag("ship");
            GameObject canvas = GameObject.Find("Canvas");
            for (int itShip = 0; itShip < listOfShips.Length; itShip++)
            {
                if (listOfShips[itShip] != this.gameObject) {
                    ShipScript shipScript = listOfShips[itShip].GetComponent<ShipScript>();
                    if (shipScript != null)
                    {
                        if (shipScript.getFloatingNameText() == null)
                        {
                            GameObject floatingText = Resources.Load("ui/PanelShip") as GameObject;
                            GameObject targetUI = Resources.Load("ui/TargetImage") as GameObject;
                            GameObject floatingTextInstance = Instantiate(floatingText) as GameObject;
                            GameObject targetUIInstance = Instantiate(targetUI) as GameObject;
                            shipScript.setFloatingNameText(floatingTextInstance);
                            shipScript.setTargetUi(targetUIInstance);
                            floatingTextInstance.transform.SetParent(canvas.transform);
                            PanelFollowingScript pf = floatingTextInstance.GetComponent<PanelFollowingScript>();
                        }
                        else
                        {
                            Camera camera = GetComponent<Camera>();

                            Vector3 screenPos = Camera.main.WorldToScreenPoint(listOfShips[itShip].transform.position);
                            GameObject floatingNameText = shipScript.getFloatingNameText();
                            GameObject targetUI = shipScript.getTargetUi();
                            if (screenPos.z > 0 && screenPos.y > 0 && screenPos.x > 0)
                            {
                                targetUI.SetActive(true);
                                targetUI.transform.position = screenPos;
                                screenPos.x -= listOfShips[itShip].name.Length;
                                screenPos.y += 40;
                                float distance = calcDistance(listOfShips[itShip]);
                                floatingNameText.SetActive(true);
                                floatingNameText.transform.position = screenPos;
                                GameObject distanceObj = floatingNameText.transform.Find("distance").gameObject;
                                
                                PanelFollowingScript pf = floatingNameText.GetComponent<PanelFollowingScript>();
                                if (pf)
                                {
                                    pf.updateUi(shipScript.getPlayerName(), shipScript.getPrcentHull(), shipScript.getFaction(), distance);
                                }

                            }
                            else
                            {
                                floatingNameText.SetActive(false);
                                targetUI.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
        if (isClient) { 
            Camera cam = Camera.main;
            if (cam.transform.parent == this.transform)
            {
                cam.transform.parent = null;
            
            }
            showUiGame(false);
            ClientScene.RemovePlayer(0);
           
        }
        else
        {
            //ClientScene.AddPlayer(0);
        }
    }

    private void showUiGame(Boolean show)
    {
        GameObject speedBar = GameObject.Find("Canvas").transform.Find("SpeedBar").gameObject;
        if (speedBar)
        {
            speedBar.SetActive(show);
        }
        GameObject healthBar = GameObject.Find("Canvas").transform.Find("HealthBar").gameObject;
        if (healthBar)
        {
            healthBar.SetActive(show);
        }
        GameObject deathPanelGO = GameObject.Find("Canvas").transform.Find("DeathPanel").gameObject;
        if (deathPanelGO)
        {
            deathPanelGO.SetActive(!show);
        }
        GameObject reticule = GameObject.Find("Canvas").transform.Find("Reticule").gameObject;
        if (reticule)
        {
            reticule.SetActive(show);
        }

        if (!show)
        {
            GameObject[] listOfShips = GameObject.FindGameObjectsWithTag("ship");
            for (int itShip = 0; itShip < listOfShips.Length; itShip++)
            {
                if (listOfShips[itShip] != this)
                {
                    ShipScript shipScript = listOfShips[itShip].GetComponent<ShipScript>();
                    if (shipScript != null)
                    {
                        if (shipScript.getFloatingNameText() != null)
                        {
                            shipScript.getFloatingNameText().SetActive(false);
                            shipScript.getTargetUi().SetActive(false);
                        }
                     }
                }
            }
        }
    }

    [Command]
    private void CmdSetPlayerName(string plName)
    {
        this.setPlayerName(plName);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Camera camToPlayer = Camera.main;
        camToPlayer.transform.parent = this.transform;
        camToPlayer.transform.position = new Vector3(0, 0, -1);
        //this.name = "PlayerShip";
        shipFromLocalPlayer = true;
        GameObject playerOverScene = GameObject.Find("PlayerOverScene");
        PlayerScript ps = playerOverScene.GetComponent<PlayerScript>();
        if (ps)
        {
            ps.setShip(this.gameObject);
            this.CmdSetPlayerName(ps.getPlayerName());
        }
        showUiGame(true);

    }

    void Start()
    {
        this.rb = GetComponent<Rigidbody>(); 
        hull = maxHull;
        this.name = "playerShip";
        speedMax = 200;
        this.setFaction(1);  

    }

    void OnCollisionEnter(Collision collision)
    {
       /* if (isServer)
        {
            hull -= 10;
            speed = 0;
            Debug.Log(collision.collider);
        }*/
        
    }



    [Command]
    void CmdApplyTorqueUp(float torque)
    {
        rb.AddTorque(transform.right * torque,ForceMode.Acceleration);
    }

    [Command]
    void CmdApplyTorqueLeft(float torque)
    {
        rb.AddTorque(transform.up * torque, ForceMode.Acceleration);
    }

    [Command]
    void CmdUpdateSpeed(float speedDelta)
    {
        if((speed+speedDelta)> speedMax)
        {
            speed = speedMax;
        }
        else
        {
            if ((speed + speedDelta) < 0)
            {
                speed = 0;
            }
            else
            {
                speed += speedDelta;
            }
        }
    }

    [Command]
    private void CmdShoot(Vector3 playerPosition, Vector3 playerForward,Quaternion playerRotation)
    {
        Vector3 location = playerPosition + playerForward * -2;
        GameObject bul = (GameObject)Instantiate(greenLaserPrefab);
        //GameObject bul = (GameObject)Network.Instantiate(greenLaserPrefab, location, transform.rotation,0);
        NetworkServer.Spawn(bul);
        Rigidbody rbullet = bul.GetComponent<Rigidbody>();
        if (rbullet)
        {
            rbullet.transform.position = location;
            rbullet.transform.rotation = playerRotation;
            rbullet.AddForce(rbullet.transform.forward * -1500, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        if (!isServer)
        {
            if (Input.GetKey(KeyCode.Z) == true)
            {
                CmdApplyTorqueUp( torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S) == true)
            {
                CmdApplyTorqueUp(-torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D) == true)
            {
                CmdApplyTorqueLeft( -torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Q) == true)
            {
                CmdApplyTorqueLeft( torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A) == true)
            {
                CmdUpdateSpeed(Time.deltaTime * stepAcceleration);
            }
            else if (Input.GetKey(KeyCode.W) == true)
            {
                CmdUpdateSpeed(Time.deltaTime * -stepAcceleration);
            }
            else if (Input.GetKey(KeyCode.Escape) == true)
            {
                Application.Quit();
            }

                if (Input.GetMouseButtonDown(0))
            {
                CmdShoot(rb.transform.position, rb.transform.forward, rb.transform.rotation);
            }
        }
        else {
            rb.AddForce(rb.transform.forward * -speed, ForceMode.Acceleration);
            //this.setDamage(1);
        }
        
        
    }

   

    
}
