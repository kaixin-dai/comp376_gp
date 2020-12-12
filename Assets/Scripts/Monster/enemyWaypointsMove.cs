using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWaypointsMove : MonoBehaviour
{


    
    public Transform mTarget;

    [SerializeField]
    bool isFinished;

    [SerializeField]
    float mRotateSmoothness = 10f;

    public float speed = 60f;
    private Transform target;
    private int wavepointIndex = 0;


    //animation booleans
    bool mRunning;
    bool mAttacking;
    bool mAnimating;
    bool isStun;


    Animator mAnimator;

    // Start is called before the first frame update
    void Start()
    {

        mAnimator = GetComponentInChildren<Animator>();
       // target = Waypoints.points[0];
        target = mTarget.GetChild(0);
        mAnimating = true;
        mAnimator.enabled = mAnimating;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        mRunning = true;
        LookAtTarget();
        SetAnimation();
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }


        if (!isFinished)
        {
            //disable targettracking
            gameObject.GetComponent<TargetTracking>().enabled = false;
            //enable waypoints move
            gameObject.GetComponent<enemyWaypointsMove>().enabled = true;
        }
        else
        {
            //disable waypoints
            gameObject.GetComponent<enemyWaypointsMove>().enabled = false;
            //enable targettracking
            gameObject.GetComponent<TargetTracking>().enabled = true;
        }


    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= mTarget.childCount-1)
        {
            isFinished = true;
            return;
        }
        wavepointIndex++;
        target = mTarget.GetChild(wavepointIndex);
    }
    private void LookAtTarget()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, mRotateSmoothness * Time.deltaTime);
    }

    public void SetAnimation()
    {
        mAnimator.SetBool("Run Forward", mRunning);
        mAnimator.SetBool("Stab Attack", mAttacking);
    }
}
