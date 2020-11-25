using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int mMaxHealth;

    int mCurrentHealth;
    // Start is called before the first frame update

    void Start(){
        mCurrentHealth = mMaxHealth;
    }

    public void TakeDamage(int damage){
        mCurrentHealth -= damage;
        print(gameObject.name + " health:" + mCurrentHealth);

        if(mCurrentHealth <= 0)
        {

                Die();


        }
    }

    private void Die(){
        if(name == "Player"){
                GameManager.OnPlayerDied();
        }
        Destroy(gameObject);
    }

    public void IncreaseHealth(int increase)
    {
        mCurrentHealth += increase;
        if(mCurrentHealth > mMaxHealth)
        {
            mCurrentHealth = mMaxHealth;
        }
    }

    public void ResetHealth()
    {
        mCurrentHealth = mMaxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        mMaxHealth = maxHealth;
    }
    public int getMaxHealth()
    {
        return mMaxHealth;
    }
}
