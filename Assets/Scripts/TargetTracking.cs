using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracking : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    Transform tempTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;
    [SerializeField]
    bool meleeEnemy;

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
    bool mRangeAttacking;
    bool mAnimating;
    bool isStun;

    float attackDelay = 1f;
    float lastAttacked = -999f;

    public GameObject projectile;
    public float shootForce;
    public Transform attackPoint;

    void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
        mTarget = GameObject.Find("Player").transform;
        tempTarget = mTarget;
        mAnimating = true;
        mAnimator.enabled = mAnimating;
        if (!meleeEnemy)
        {
            mOffsetDistance = 20.0f;
        }
        
    }

    void Update ()
    {
        int currentHealth = this.GetComponent<Health>().getCurrentHealth();

        if (mTarget != null && currentHealth > 0)
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
                    if (meleeEnemy)
                    {
                       mAttacking = true;
                    }
                    if (!meleeEnemy)
                    {
                        mRangeAttacking = true;
                    }
                    DamageTarget();
                }

                SetAnimation();
            
            }
            else
            {
                mRunning = false;
                mAttacking = false;
                mRangeAttacking = false;
            }

            SetAnimation();
        }
        if (isStun)
        {
            StunStop();
            mAnimator.enabled = mAnimating;
        }
        else
        {
            StunResume();
            mAnimator.enabled = mAnimating;
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
        tempTarget = target;
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
        mAnimator.SetBool("Cast Spell", mRangeAttacking);
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

        if (mRangeAttacking)
        {
            if (Time.time > lastAttacked + attackDelay)
            {
                Vector3 direction = mTarget.position - transform.position;

                lastAttacked = Time.time;
                //instantiate bullet
                GameObject currentBullet = Instantiate(projectile, attackPoint.position, Quaternion.identity);
                currentBullet.transform.forward = direction.normalized;

                currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

            }
        }
    }
    public void StunStop()
    {
        mTarget = null;
        mAnimating = false;

    }
    public void StunResume()
    {
        mTarget = tempTarget;
        mAnimating = true;
    }
    public void SetStun(bool inputBool)
    {
        isStun = inputBool;
    }
}
