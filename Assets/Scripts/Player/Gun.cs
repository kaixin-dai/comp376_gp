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
    int mEnemyLayer = 8;

    public bool IsEnpowered;
    float EnpowerCounter;
    public float EnpowerDuration = 10.0f;

    int OriginalDamage;
    float OriginalFireRate;

    float mFireTimer;

    // Start is called before the first frame update
    void Start()
    {
        mFireTimer = 0.0f;
        OriginalDamage = mDamage;
        OriginalFireRate = mFireRate;
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

        if(IsEnpowered)
        {
            if(EnpowerCounter < EnpowerDuration)
            {
                EnpowerCounter += Time.deltaTime;

            }
            else
            {
                EnpowerCounter = 0.0f;
                Reset();
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
            {
                health.TakeDamage(mDamage);
            }


				

        }
    }

    public void IncreaseGunDamage(int dmg)
    {
        mDamage += dmg;
        OriginalDamage = mDamage;
        
    }

    public void IncreaseGunFireRate(float rate)
    {
        mFireRate -= rate;
        OriginalFireRate = mFireRate;
    }

    public void Reset()
    {
        IsEnpowered = false;
        mDamage = OriginalDamage;
        mFireRate = OriginalFireRate;
        GetComponent<PlayerMovement>().ResetSpeed();
    }

    public void Enpower()
    {
        IsEnpowered = true;
        GetComponent<PlayerMovement>().Enpower();
        mDamage = mDamage * 2;
        mFireRate = mFireRate / 2;
        
    }


    
}
