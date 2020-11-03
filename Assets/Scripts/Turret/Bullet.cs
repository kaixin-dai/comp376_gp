using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 5;

    public float speed = 20;

    private Transform target;
    public GameObject explosionEffect;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            ReachDestination();
        }


    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().takeDamage(damage);
            ReachDestination();
        }

    }
    public void ReachDestination()
    {
        
        GameObject ExploEffect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(ExploEffect, 0.5f);
        Destroy(gameObject);
    }
}
