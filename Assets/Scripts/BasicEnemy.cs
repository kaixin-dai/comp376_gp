//Inspired from code by Dave / GameDevelopment (YouTube)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public Transform player;
    
    Animator animator;
    
    public int healthPoints = 100;
    public int attackPoints = 5;
    public float speed = 1.0f;
    //public bool hostile;

    public float lookRadius = 50.0f;

    public Vector3 destination;
    public bool destinationSet;

    public float attackCooldown = 2.5f;
    public bool justAttacked = false;
    public float attackRange;
    public bool playerInSight, playerAttackable;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //player = GameObject.Find("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= lookRadius)
            playerInSight = true;
        if (distance < 2.0f)
            playerAttackable = true;
        
        if (playerInSight && playerAttackable)
            AttackPlayer();
        if (playerInSight && !playerAttackable)
            FollowPlayer();
        if (!playerInSight && !playerAttackable)
            Patrol();

        if (healthPoints <= 0)
            EnemyDeath();
       
    }
    private void Patrol()
    {
        //find random destination
        if (!destinationSet)
        {
            float randomXPoint = Random.Range(-30, 30);
            float randomZPoint = Random.Range(-30, 30);

            destination = new Vector3(transform.position.x + randomXPoint, transform.position.y, transform.position.z + randomZPoint);

            destinationSet = true;
        }

        if (destinationSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
            transform.LookAt(destination);
            animator.SetTrigger("Walk Forward");
        }

        Vector3 distanceToDestination = transform.position - destination;

        if (distanceToDestination.magnitude < 1.0f)
        {
            animator.ResetTrigger("Walk Forward");
            destinationSet = false;
        }
    }
    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player.position);
        animator.SetTrigger("Walk Forward");

    }
    private void AttackPlayer()
    {
        transform.LookAt(player);

        if (!justAttacked)
        {
            animator.SetTrigger("Stab Attack");
            //add player taking damage

            justAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }
    private void ResetAttack()
    {
        justAttacked = false;
    }
    private void EnemyDeath()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 3.0f);

        //spawn loot?
    }
}
