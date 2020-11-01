using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{   

    [SerializeField]
    [Range(0.1f, 1.5f)]
    float mFireRate = 1.0f;

    [SerializeField]
    [Range(1, 10)]
    int mDamage = 1;

    [SerializeField]
    Transform mFirePoint;

    [SerializeField]
    ParticleSystem muzzleParticle;

    [SerializeField]
    [Range(8, 32)]
    int mEnemyLayer;



    

    float mFireTimer;

    // Start is called before the first frame update
    void Start()
    {
        mFireTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mFireTimer += Time.deltaTime;
        if(mFireTimer > mFireRate)
        {
            if(Input.GetButton("Fire1"))
            {   
                mFireTimer = 0.0f;
                FireGun();
            }
        }
    }

    private void FireGun()
    {   

        muzzleParticle.Play();
        GetComponent<PlayerSoundsManager>().PlayShootSound();
        
        print(transform.forward);
        Debug.DrawRay(mFirePoint.position, mFirePoint.forward * 100, Color.red, 2.0f);

        Ray ray = new Ray(mFirePoint.position, mFirePoint.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100)){
            
            if(hit.collider.gameObject.layer == mEnemyLayer){
                print("guned an enemy");
            }

            var health = hit.collider.GetComponent<Health>();

			if (health != null)
				health.TakeDamage(mDamage);

        }
    }

    public void IncreaseGunDamage(int dmg)
    {
        mDamage += dmg;
    }

    public void IncreaseGunFireRate(float rate)
    {
        mFireRate -= rate;
    }


    
}
