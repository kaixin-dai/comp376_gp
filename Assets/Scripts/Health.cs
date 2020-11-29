using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    int mMaxHealth = 100;

    [SerializeField]
    int mCurrentHealth;
    
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start(){
        mCurrentHealth = mMaxHealth;
        healthBar.SetMaxHealth(mMaxHealth);
    }

    // TEMPORARY
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage){
        mCurrentHealth -= damage;
        healthBar.SetHleath(mCurrentHealth);
        //print(gameObject.name + " health:" + mCurrentHealth);

        if(mCurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die(){
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
