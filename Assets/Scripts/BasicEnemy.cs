using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public Transform player;
    
    Animator animator;
    
    public int healthPoints = 100;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
            EnemyDeath();
    }

    public void TakeDamage(int damage)
    {
       if(healthPoints > 0)
        healthPoints -= damage;
    }
    private void EnemyDeath()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 3.0f);

        //spawn loot?
    }
}
