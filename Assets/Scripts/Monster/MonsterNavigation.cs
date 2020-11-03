using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterNavigation : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navAgent;
    [SerializeField]
    Transform target;
    Transform tempTarget;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        tempTarget = GameObject.Find("Player").transform;
        FollowlingTarget(target.GetComponent<PlayerInteract>());
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            
             navAgent.SetDestination(target.position);
             FaceTarget();
        }
        else
        {
            navAgent.SetDestination(transform.position);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        navAgent.SetDestination(point);
    }
    public void FollowlingTarget(PlayerInteract newTarget)
    {
        navAgent.stoppingDistance = newTarget.getInteractRadius() * 0.8f;
        navAgent.updateRotation = false;

        //target = newTarget.getTransform();
    }
    public void StopFollowingTarget()
    {
        navAgent.updateRotation = true;
        navAgent.stoppingDistance = 0.0f;
        target = null;
    }
    public void resumeFollowingTarget()
    {
        target = tempTarget; 
    }
    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
