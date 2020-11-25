using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;
    public float lifeTime;

    Transform mTarget;
    private void Start()
    {
        mTarget = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            mTarget.GetComponent<Health>().TakeDamage(1);
            Destroy(gameObject);

        }

    }
}
