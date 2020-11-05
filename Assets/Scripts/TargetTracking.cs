using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracking : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    [SerializeField]
    float mRotateSmoothness;

    [SerializeField]
    float mDistance;

    //distance where the monster is close enough to attack the player
    [SerializeField]
    float mOffsetDistance = 3.0f;

    Animator mAnimator;

    //animation booleans
    bool mRunning;
    bool mAttacking;

    float attackDelay = 1f;
    float lastAttacked = -999f;

    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();

    }

    void Update ()
    {

        

        if(mTarget != null)
        {   
            mDistance = Vector3.Distance(mTarget.position, transform.position);
            if(mDistance < mFollowRange){
                mRunning = true;
                mAttacking = false;
                
                LookAtTarget();

                if(mDistance > mOffsetDistance)
                {
                    FollowTarget();
                }
                else
                {
                    mRunning = false;
                    mAttacking = true;
                    DamageTarget();
                }

                SetAnimation();
            
            }
            else
            {
                mRunning = false;
                mAttacking = false;
            }

            SetAnimation();


  
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    private void LookAtTarget()
    {
        Quaternion desiredRotation = Quaternion.LookRotation(mTarget.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, mRotateSmoothness * Time.deltaTime);
    }

    private void FollowTarget()
    {
        Vector3 direction = mTarget.position - transform.position;
        direction = Vector3.ClampMagnitude(direction, 1.0f);
        transform.Translate(direction * mFollowSpeed * Time.deltaTime, Space.World);
    }

    public void SetAnimation()
    {
        mAnimator.SetBool("Run Forward", mRunning);
        mAnimator.SetBool("Stab Attack", mAttacking);
    }
    private void DamageTarget()
    {
        if (mAttacking)
        {
            if (Time.time > lastAttacked + attackDelay)
            {
                lastAttacked = Time.time;
                mTarget.GetComponent<Health>().TakeDamage(1);
            }
        }
    }
}
