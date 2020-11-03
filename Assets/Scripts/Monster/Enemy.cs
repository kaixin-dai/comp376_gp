using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2.0f;
    public int hPBar = 50;
    public int goldPrize = 10;


    void ReachDestination()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void takeDamage(int damage)
    {
        hPBar -= damage;
        if (hPBar <= 0)
        {
            ReachDestination();
        }
    }
}
