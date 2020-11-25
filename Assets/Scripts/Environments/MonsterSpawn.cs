using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{

    public GameObject MonsterBugReference;
	public List<Transform> spawnPoints;
    
    public int numOfBug;
    bool nightMode;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnNight += SpawnBug;
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(nightMode)
        {
            
            if(GameObject.FindGameObjectsWithTag("night").Length == 0)
            {   
                
                nightMode = false;
                GameManager.OnDay();
            }
                
        }
    }

    public void SpawnBug()
    {
        nightMode = true;
        foreach (Transform point in spawnPoints)
        {
            GameObject bug;
            for(int i = - numOfBug/2 ; i < numOfBug - numOfBug/2 ; i++)
            {
                bug = Instantiate(MonsterBugReference,point.position + (i * 5 * Vector3.right),Quaternion.identity);
                bug.tag = "night";
            }

                       
        }

    }

    
}
