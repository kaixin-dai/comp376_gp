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

    //how many seconds for one shot
    private float timer = 0;
    

    [Header("Use Bullets (default)")]
    public GameObject ammoPrefab;//ammo
    public float attackSpeed = 1;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public int maxDamage = 30;
    private float laserTimer = 4f;
    private float laserSpeed = 0.2f;
    private float laserSpeedTimer = 0.2f;

    [Header("Use Shock")]
    public bool useShock = false;
    public int shockDamage = 1;
    public float shockStunTime = 0.3f;
    public float shockSpeed = 2f;
    public GameObject shockEffect;

    [Header("Unity Setup Field")]
    public Transform firePos;
    //part to rotate
    //rotate speed
    void Start()
    {

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
        else
        {
            DefaultLockShoot();
        }
    }
    public void shockCheck()
    {
        timer += Time.deltaTime;
        if (enemys.Count > 0 && timer > shockSpeed)
        {
            timer -= shockSpeed;
            ShockEnemy();
        }
    }
    public void ShockEnemy()
    {
        for (int i =0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<Enemy>().ShockStun(shockStunTime);
            enemys[i].GetComponent<Enemy>().takeDamage(1);
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

    public void LaserLockShoot()
    {
        if (enemys.Count > 0)
        {

            if (enemys[0] == null)
            {
                enemys.Remove(enemys[0]);
                lineRenderer.SetPosition(0, firePos.position);
                lineRenderer.SetPosition(1, firePos.position);
                laserTimer = 4f;
                return;
            }
            LaserBeam(enemys[0]);
        }
        else
        {
            lineRenderer.SetPosition(0, firePos.position);
            lineRenderer.SetPosition(1, firePos.position);
        }

    }


    public void LaserBeam(GameObject enemy)
    {
        laserTimer -= Time.deltaTime;
        laserSpeedTimer -= Time.deltaTime;
        lineRenderer.SetPosition(0, firePos.position);
        lineRenderer.SetPosition(1, enemy.transform.position);
        
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
}
