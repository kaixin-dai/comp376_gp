using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{

    public GameObject MonsterBugReference;
	public List<Transform> spawnPoints;
    public GameObject waypointsLeft;
    public GameObject waypointsRight;
    public GameObject waypointsTop;

    public GameObject[] waypoints = new GameObject[3];
    
    public int numOfBug;
    bool nightMode;
    // Start is called before the first frame update
    void Awake()
    {
        waypointsLeft = GameObject.Find("Waypoints_left");
        waypointsRight = GameObject.Find("Waypoints_right");
        waypointsTop = GameObject.Find("Waypoints_top");

        waypoints[0] = waypointsLeft;
        waypoints[1] = waypointsRight;
        waypoints[2] = waypointsTop;


        GameManager.OnNight += SpawnBug;
        
    }

    // Update is called once per frame
    void Update()
    {   

        if(nightMode)
        {
            
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {   
                
                nightMode = false;
                GameManager.OnDay();
            }
                
        }
    }

    public void SpawnBug()
    {
        nightMode = true;
        int point_index = 0;
        foreach (Transform point in spawnPoints)
        {
            GameObject bug;
            for(int i = - numOfBug/2 ; i < numOfBug - numOfBug/2 ; i++)
            {

                bug = Instantiate(MonsterBugReference,point.position + (i * 5 * Vector3.right),Quaternion.identity);
                bug.AddComponent<enemyWaypointsMove>();
                bug.transform.localScale = new Vector3 (2.0f,2.0f,2.0f);
                
                bug.GetComponent<enemyWaypointsMove>().mTarget = waypoints[point_index].transform;
                bug.tag = "Enemy";
            }

            point_index = point_index + 1;

                       
        }

    }

    
}
