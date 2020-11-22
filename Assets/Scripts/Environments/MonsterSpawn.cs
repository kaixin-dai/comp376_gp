using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{

    public GameObject MonsterBugReference;
	public List<Transform> spawnPoints;
    
    public int numOfBug;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBug()
    {
        foreach (Transform point in spawnPoints)
        {
            for(int i = - numOfBug/2 ; i < numOfBug - numOfBug/2 ; i++)
            {
                Instantiate(MonsterBugReference,point.position + (i * 5 * Vector3.right),Quaternion.identity);
            }           
        }

    }
}
