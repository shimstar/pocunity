﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class level1StartScript : NetworkBehaviour
{

    void generateLevel()
    {
        GameObject asteroidPref = Resources.Load("zone/asteroid1") as GameObject;
        int nbAmas = Random.Range(1, 10);
        for (int itAmas = 0; itAmas < nbAmas; itAmas++)
        {
            GameObject ast = (GameObject)Instantiate(asteroidPref);
            ast.transform.position.Set(Random.Range(-100000, 100000), Random.Range(-100000, 100000), Random.Range(-100000, 100000));
            ast.transform.Rotate( Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            ast.name = "Asteroid" + itAmas;
            int nbAst = Random.Range(1, 10);
            for (int itNbAst=0; itNbAst < nbAst; itNbAst++)
            {
                GameObject ast2 = (GameObject)Instantiate(asteroidPref);
                ast2.transform.position.Set(ast.transform.position.x + Random.Range(-10000, 10000), ast.transform.position.y + Random.Range(-10000, 10000), ast.transform.position.z + Random.Range(-10000, 10000));
                ast2.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                ast2.name = "Asteroid" + itAmas + "_" + itNbAst;
            }
        }
    }

	// Use this for initialization
	void Start () {
            GameObject pref = Resources.Load("ships/GhoulOBJ") as GameObject;
            GameObject ennemy = (GameObject)Instantiate(pref);
            ennemy.transform.position.Set(600, 600, 600);
            generateLevel();
            GameObject uiDeath = GameObject.Find("/Canvas/deathpanel");
        
            if (uiDeath != null)
            {
                uiDeath.SetActive(false);
            }


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
