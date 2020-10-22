using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{   

    
    [SerializeField]
    Transform mAttackPoint;

    [SerializeField]
    float mAttackRadius = 0.5f;

    [SerializeField]
    LayerMask mEnemyLayer;

    [SerializeField]
    [Range(0.1f, 1.5f)]
    float mAttackRate = 0.5f;

    float mAttackTimer;

    int mDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        mAttackTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {   
        mAttackTimer += Time.deltaTime;
        if(mAttackTimer > mAttackRate)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                mAttackTimer = 0.0f;
                Attack();
                
            }
        }
        
    }

    private void Attack()
    {


        //Todo implement melee attack animation

        //detect all enemies that are hit
        GetComponent<PlayerSoundsManager>().PlayMeleeSound();

        Collider[] hitEnemies = Physics.OverlapSphere(mAttackPoint.position, mAttackRadius,mEnemyLayer);


        //damage detected enemies
        foreach(Collider enemy in hitEnemies)
        {

            var health = enemy.GetComponent<Health>();
            if(health != null)
                health.TakeDamage(mDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(mAttackPoint.position, mAttackRadius);
    }


    public void IncreaseMeleeDamage(int dmg)
    {
        mDamage += dmg; 
    }

    public void IncreaseMeleeFireRate(float rate)
    {
        mAttackRate -= rate;
    }
}
