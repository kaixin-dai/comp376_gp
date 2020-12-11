using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    public Transform partToRotateH;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Enemy")
        {
            if (!enemys.Contains(col.gameObject))
            {
                enemys.Add(col.gameObject);
            }

        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }

    //how many seconds for one shot
    private float timer = 0;


    [Header("Use Bullets (default)")]
    public GameObject ammoPrefab;//ammo
    public float attackSpeed = 1;

    [Header("Use Rocket")]
    public bool useRocket = false;
    public Transform secondFirePos;
    private Transform enemyLastPos;


    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public int maxDamage = 30;
    private float laserTimer = 4f;
    private float laserSpeed = 0.2f;
    private float laserSpeedTimer = 0.2f;

    [Header("Use Shock")]
    public bool useShock = false;
    public int shockDamage = 1;
    public float shockStunTime = 0.2f;
    public float shockSpeed = 2f;
    public GameObject shockEffect;

    [Header("Unity Setup Field")]
    public Transform firePos;
    //part to rotate
    //rotate speed
    void Start()
    {
        if (impactEffect!=null)
        {
            if (impactEffect.isPlaying) impactEffect.Stop();
        }
    }

    void Update()
    {
        if (useLaser)
        {
            LaserLockShoot();
        }
        else if (useShock)
        {
            shockCheck();
        }
        else if(useRocket){
            RocketLockShoot();
        }
        else
        {
            DefaultLockShoot();
        }
        if (enemys.Count>0)
        {
            PhysicsLock();
        }
    }
    public void PhysicsLock()
    {
        Vector3 dir = enemys[0].transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotateH.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    public void shockCheck()
    {
        timer += Time.deltaTime;
        if (enemys.Count > 0 && timer > shockSpeed)
        {
            timer -= shockSpeed;
            if (enemys[0] !=null)
            {
                ShockEnemy();
            }
        }
        if (enemys.Count > 0)
        {
            if (enemys[0] == null)
            {
                enemys.RemoveAt(0);
            }
        }
    }
    public void ShockEnemy()
    {
        for (int i =0; i < enemys.Count; i++)
        {
            if (enemys[i] != null)
            {
                enemys[i].GetComponent<Enemy>().ShockStun(shockStunTime);
                enemys[i].GetComponent<Enemy>().takeDamage(1);
            }
        }
        Instantiate(shockEffect, firePos.position, transform.rotation);
    }
    public void DefaultLockShoot()
    {
        timer += Time.deltaTime;

        if (enemys.Count > 0 && timer > attackSpeed)
        {
            timer -= attackSpeed;
            if (enemys[0] == null)
            {
                enemys.RemoveAt(0);
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

    public void RocketLockShoot()
    {
        timer += Time.deltaTime;

        if (enemys.Count > 0 && timer > attackSpeed)
        {
            timer -= attackSpeed;
            if (enemys[0] == null)
            {
                enemys.RemoveAt(0);
                timer += attackSpeed;
                return;
            }
            RocketAttack();
        }
        else if (enemys.Count == 0)
        {
            timer = attackSpeed;
        }
    }

    public void LaserLockShoot()
    {
        if (enemys.Count > 0)
        {

            if (enemys[0] == null)
            {
                enemys.RemoveAt(0);
                lineRenderer.SetPosition(0, firePos.position);
                lineRenderer.SetPosition(1, firePos.position);
                if (impactEffect.isPlaying) impactEffect.Stop();
                laserTimer = 4f;
                return;
            }
            LaserBeam(enemys[0]);
        }
        else
        {
            if (impactEffect.isPlaying) impactEffect.Stop();
            lineRenderer.SetPosition(0, firePos.position);
            lineRenderer.SetPosition(1, firePos.position);
        }

    }


    public void LaserBeam(GameObject enemy)
    {
        laserTimer -= Time.deltaTime;
        laserSpeedTimer -= Time.deltaTime;
        if(!impactEffect.isPlaying) impactEffect.Play();
        lineRenderer.SetPosition(0, firePos.position);
        lineRenderer.SetPosition(1, enemy.transform.position+new Vector3(0f, 0.5f,0f));

        Vector3 dir = firePos.position - enemy.transform.position;
        impactEffect.transform.position = enemy.transform.position + new Vector3(0f, 0.5f, 0f) + dir.normalized * 0.5f;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        


        if (laserSpeedTimer <= 0f)
        {
            laserSpeedTimer = laserSpeed;
            if (laserTimer <= 0f)
            {
                laserTimer = 0;
                lineRenderer.startColor = new Color(0, 1, 1, 0);
                lineRenderer.endColor = new Color(1, 0, 0, 0);  
            }
            else if (laserTimer <= 2f)
            {
                lineRenderer.startColor = new Color(0.5f, 1, 0, 0);
                lineRenderer.endColor = new Color(0.5f, 0, 0.1f, 0);
            }
            else
            {
                lineRenderer.startColor = new Color(0.5f, 0, 0, 0);
                lineRenderer.endColor = new Color(0.5f, 0.1f, 0.1f, 0);
            }

            enemy.GetComponent<Enemy>().takeDamage((int)(maxDamage *((4f-laserTimer)/4f)));
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

    void RocketAttack()
    {
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(ammoPrefab, firePos.position, firePos.rotation);
            GameObject bullet2 = GameObject.Instantiate(ammoPrefab, secondFirePos.position, secondFirePos.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            bullet2.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        //bullet.GetComponent<Bullet>().ReachDestination();
    }
}
