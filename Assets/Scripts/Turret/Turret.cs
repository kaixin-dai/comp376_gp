using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    public float attackSpeed = 1;//how many seconds for one shot
    private float timer = 0;

    public GameObject ammoPrefab;//ammo
    public Transform firePos;
    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (enemys.Count > 0 && timer > attackSpeed)
        {
            timer -= attackSpeed;
            if (enemys[0] == null)
            {
                enemys.Remove(enemys[0]);
                timer += attackSpeed;
                return;
            }
            Attack();
        }
        else if (enemys.Count == 0)
        {
            timer = attackSpeed;
        }
    }

    void Attack()
    {
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(ammoPrefab, firePos.position, firePos.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        //bullet.GetComponent<Bullet>().ReachDestination();
    }
}
