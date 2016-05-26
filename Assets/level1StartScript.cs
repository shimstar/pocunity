using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class level1StartScript : NetworkBehaviour
{

    public void generateLevel()
    {
        GameObject asteroidPref = Resources.Load("zone/asteroid1") as GameObject;
        int cpt = 0;
        int nbAmas = Random.Range(500, 500);
        for (int itAmas = 0; itAmas < nbAmas; itAmas++)
        {
            GameObject ast = (GameObject)Instantiate(asteroidPref);
            ast.transform.position=new Vector3(Random.Range(-5000, 5000), Random.Range(-5000, 5000), Random.Range(-5000, 5000));
            
            ast.transform.Rotate( Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            ast.name = "Asteroid" + itAmas;
            NetworkServer.Spawn(ast);
            int nbAst = Random.Range(1, 1);
            cpt += 1;
            for (int itNbAst=0; itNbAst < nbAst; itNbAst++)
            {
                GameObject ast2 = (GameObject)Instantiate(asteroidPref);
                ast2.transform.position = new Vector3(ast.transform.position.x + Random.Range(-1000, 1000), ast.transform.position.y + Random.Range(-1000, 1000), ast.transform.position.z + Random.Range(-1000, 1000));
                ast2.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                ast2.name = "Asteroid" + itAmas + "_" + itNbAst;
                int scale = Random.Range(1, 4);
                ast2.transform.localScale = new Vector3(scale, scale, scale);
                NetworkServer.Spawn(ast2);
                cpt += 1;
            }
        }
        Debug.Log("nb asteroid " + cpt);
    }


    public void initLevel()
    {
        GameObject pref = Resources.Load("ships/ghoul3") as GameObject;
        GameObject ennemy = (GameObject)Instantiate(pref);
        ennemy.transform.position.Set(600, 600, 600);
        ennemy.name = "test ennemy";
        NetworkServer.Spawn(ennemy);
    }

	// Use this for initialization
	void Start () {
           
           // generateLevel();
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
