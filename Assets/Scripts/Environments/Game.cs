using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    //This script will utilise all componemnt sctips

    public GameObject monsterSpawner;
    public GameObject teleport;
    // Start is called before the first frame update
    float time = 0.0f;
    void Start()
    {
    
        monsterSpawner = GameObject.Find("MonsterSpawner");
        teleport = GameObject.Find("Teleport");

    }

    // Update is called once per frame
    void Update()
    {
        // time = time + Time.deltaTime;
        // if(time > 10){
        //     GetComponent<Sun>().StartDay();
        //     time = 0;
        //     return;


        if(Input.GetKeyDown(KeyCode.M))
        {
            monsterSpawner.GetComponent<MonsterSpawn>().SpawnBug();
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            teleport.GetComponent<Teleport>().ToBase();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            GameManager.OnDay();
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            GameManager.OnNight();
        }
    }


}
