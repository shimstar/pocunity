using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using SimpleJSON;

public class level1StartScript : NetworkBehaviour
{

    public void generateJson()
    {
        GameObject[] listOfAsteroid = GameObject.FindGameObjectsWithTag("asteroid");
        string jsonAsts = "{\"asteroids\":[";
        for(int itAst = 0; itAst < listOfAsteroid.Length; itAst++)
        {
            string jsonAst = "";
            if (itAst > 0)
            {
                jsonAst += ",";
            }
            Vector3 pos = listOfAsteroid[itAst].transform.position;
            Quaternion quat = listOfAsteroid[itAst].transform.rotation;
            Vector3 scale = listOfAsteroid[itAst].transform.localScale;
            jsonAst += "{\"posx\" : " + pos.x + ",\"posy\" : " + pos.y + ",\"posz\":" + pos.z;
            jsonAst += ",\"quatw\":" + quat.w + ",\"quatx\":" + quat.x + ",\"quaty\":" + quat.y + ",\"quatz\":" + quat.z;
            jsonAst += ",\"scalex\" : " + scale.x + ",\"scaley\" : " + scale.y + ",\"scalez\":" + scale.z;
            jsonAst += "}";

            jsonAsts += jsonAst;
        }
        jsonAsts += "],\"nbAsteroid\":" + listOfAsteroid.Length + "}";
        var sr = File.CreateText("c:\\pascal\\jsonlevel1.json");
        sr.Write(jsonAsts);
        sr.Close();
    }

    public void generateLevel()
    {
        GameObject asteroidPref = Resources.Load("zone/asteroid1") as GameObject;
        int cpt = 0;
        int nbAmas = Random.Range(50, 100);
        for (int itAmas = 0; itAmas < nbAmas; itAmas++)
        {
            GameObject ast = (GameObject)Instantiate(asteroidPref);
            ast.transform.position=new Vector3(Random.Range(-2000, 2000), Random.Range(-2000, 2000), Random.Range(-2000, 2000));
            
            ast.transform.Rotate( Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            ast.name = "Asteroid" + itAmas;
            NetworkServer.Spawn(ast);
            int nbAst = Random.Range(50, 100);
            cpt += 1;
            for (int itNbAst=0; itNbAst < nbAst; itNbAst++)
            {
                GameObject ast2 = (GameObject)Instantiate(asteroidPref);
                ast2.transform.position = new Vector3(ast.transform.position.x + Random.Range(-500, 500), ast.transform.position.y + Random.Range(-500, 500), ast.transform.position.z + Random.Range(-500, 500));
                ast2.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                ast2.name = "Asteroid" + itAmas + "_" + itNbAst;
                int scale = Random.Range(6, 20);
                ast2.transform.localScale = new Vector3(scale, scale, scale);
                NetworkServer.Spawn(ast2);
                cpt += 1;
            }
        }
        Debug.Log("nb asteroid " + cpt);
        generateJson();
    }


    public void initLevel()
    {
        GameObject pref = Resources.Load("ships/ghoul3") as GameObject;
        GameObject ennemy = (GameObject)Instantiate(pref);
        ennemy.transform.position.Set(600, 600, 600);
        ennemy.name = "test ennemy";
        NetworkServer.Spawn(ennemy);
        loadLevel();
    }

    public void loadLevel()
    {
        TextAsset txt = Resources.Load("zone/jsonlevel1") as TextAsset;
        string content = txt.text;
        var N = JSON.Parse(content);
        int nbAst = N["nbAsteroid"].AsInt;
        for(int itAst = 0; itAst < nbAst; itAst++)
        {
            GameObject asteroidPref = Resources.Load("zone/asteroid1") as GameObject;
            GameObject ast = (GameObject)Instantiate(asteroidPref);
            ast.transform.position = new Vector3(N["asteroids"][itAst]["posx"].AsFloat, N["asteroids"][itAst]["posy"].AsFloat, N["asteroids"][itAst]["posz"].AsFloat);
            ast.transform.rotation = new Quaternion(N["asteroids"][itAst]["quatw"].AsFloat, N["asteroids"][itAst]["quatx"].AsFloat, N["asteroids"][itAst]["quaty"].AsFloat, N["asteroids"][itAst]["quatz"].AsFloat);
            ast.transform.localScale = new Vector3(N["asteroids"][itAst]["scalex"].AsFloat, N["asteroids"][itAst]["scaley"].AsFloat, N["asteroids"][itAst]["scalez"].AsFloat);
        }
    }

	// Use this for initialization
	void Start () {
           
            //generateLevel();
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
