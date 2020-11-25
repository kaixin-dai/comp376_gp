﻿using System.Collections;
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
    public bool isBug;

   //Animator mAnimator;
    private void Start()
    {
        hPBar = GetComponent<Health>().getMaxHealth();
        //mAnimator = GetComponentInChildren<Animator>();
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
                if (!isBug)
                    GetComponent<MonsterNavigation>().resumeFollowingTarget();
                else
                {
                    GetComponent<TargetTracking>().SetStun(isShocked);
                }
                //GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }


    public void ShockStun(float stunTimer)
    {
        isShocked = true;
        stunTime = stunTimer;
        //GetComponent<NavMeshAgent>().enabled = false;
        if (!isBug)
        {
            GetComponent<MonsterNavigation>().StopFollowingTarget();
        }
        else
        {
            GetComponent<TargetTracking>().SetStun(isShocked);
        }
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
            //mAnimator.SetTrigger("Die");
            //Invoke("ReachDestination", 5.0f);
            ReachDestination();
        }
    }
}
