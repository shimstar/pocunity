using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class dfcontroller : ship
{
    public GameObject greenLaserPrefab;
    public Texture2D crosshairImage;
    

    

    void OnGUI()
    {
        if (isClient == true)
        {
            float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
            float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
            GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);

            GameObject[] listOfShips = GameObject.FindGameObjectsWithTag("ship");
            GameObject canvas = GameObject.Find("Canvas");

            for (int itShip = 0; itShip < listOfShips.Length; itShip++)
            {
                if (listOfShips[itShip] != this) { 
                    ship shipScript = listOfShips[itShip].GetComponent<ship>();
                    if (shipScript != null)
                    {
                        if (shipScript.floatingNameText == null)
                        {
                            GameObject floatingText = Resources.Load("ui/followingNameUIText") as GameObject;
                            shipScript.floatingNameText = (GameObject)Instantiate(floatingText);
                            shipScript.floatingNameText.transform.SetParent(canvas.transform);
                            shipScript.floatingNameText.GetComponent<Text>().text = shipScript.name;
                        }
                        else
                        {
                            Camera camera = GetComponent<Camera>();

                            Vector3 screenPos = Camera.main.WorldToScreenPoint(listOfShips[itShip].transform.position);
                            if (screenPos.z > 0 && screenPos.y > 0 && screenPos.x > 0)
                            {
                                screenPos.x += listOfShips[itShip].name.Length;
                                shipScript.floatingNameText.SetActive(true);
                                shipScript.floatingNameText.transform.position = screenPos;
                            }
                            else
                            {
                                shipScript.floatingNameText.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Camera cam = Camera.main;
        cam.transform.parent = this.transform;
        cam.transform.position = new Vector3(0, 0, -1);
        this.name = "PlayerShip";
        shipFromLocalPlayer = true;
        GameObject playerOverScene = GameObject.Find("PlayerOverScene");
        PlayerScrip ps = playerOverScene.GetComponent<PlayerScrip>();
        if (ps)
        {
            ps.setShip(this.gameObject);
        }
    }

    void Start()
    {
        this.rb = GetComponent<Rigidbody>(); 
        hull = maxHull;
        this.name = "playerShip";


    }

    void OnCollisionEnter(Collision collision)
    {
        if (isServer)
        {
            hull -= 10;
            speed = 0;
            Debug.Log(collision.collider);
        }
        
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
    private void CmdShoot()
    {
        Vector3 location = transform.position + transform.forward * -2;
        GameObject bul = (GameObject)Instantiate(greenLaserPrefab);
        //GameObject bul = (GameObject)Network.Instantiate(greenLaserPrefab, location, transform.rotation,0);
        NetworkServer.Spawn(bul);
        Rigidbody rbullet = bul.GetComponent<Rigidbody>();
        if (rbullet)
        {
            rbullet.transform.position = location;
            rbullet.transform.rotation = transform.rotation;
            rbullet.AddForce(rbullet.transform.forward * -500, ForceMode.Acceleration);
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
                CmdUpdateSpeed(Time.deltaTime * 10);
            }
            else if (Input.GetKey(KeyCode.W) == true)
            {
                CmdUpdateSpeed(Time.deltaTime * -10);
            }
            else if (Input.GetKey(KeyCode.Escape) == true)
            {
                Application.Quit();
            }

                if (Input.GetMouseButtonDown(0))
            {
                CmdShoot();
            }
        }
        else {
            rb.AddForce(rb.transform.forward * -speed, ForceMode.Acceleration);
        }
        
        
    }

   

    
}
