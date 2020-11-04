using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2.0f;
    public int hPBar;
    public int goldPrize = 10;
    public float stunTime;
    bool isShocked = false;
    private void Start()
    {
        hPBar = GetComponent<Health>().getMaxHealth();
    }
    public void Update()
    {
        if (isShocked)
        {
            stunTime -= Time.deltaTime;
            if (stunTime<=0)
            {
                stunTime = 0;
                isShocked = false;
                GetComponent<MonsterNavigation>().resumeFollowingTarget();
                //GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }


    public void ShockStun(float stunTimer)
    {
        isShocked = true;
        stunTime = stunTimer;
        //GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<MonsterNavigation>().StopFollowingTarget();
    }
    void ReachDestination()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void takeDamage(int damage)
    {
        hPBar -= damage;
        if (hPBar <= 0)
        {
            ReachDestination();
        }
    }
}
