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

    GameObject DamagePrompt;
    Text DamagePromptText;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start(){
        mCurrentHealth = mMaxHealth;

        DamagePrompt = GameObject.Find("Damage Prompt");
        DamagePromptText = DamagePrompt.GetComponent<Text>();
        if(tag == "Player")
        {
            healthBar.SetMaxHealth(mMaxHealth);
        }

    }

    void Update()
    {
    }

    public void TakeDamage(int damage){
        mCurrentHealth -= damage;
        print(gameObject.name + " health:" + mCurrentHealth);
        DamagePromptText.text = " - " + damage;
        GameManager.OnTakeDamage();
        healthBar.SetHleath(mCurrentHealth);
        //print(gameObject.name + " health:" + mCurrentHealth);
        if(mCurrentHealth <= 0)
        {

                Die();


        }
    }

    private void Die(){
        if(name == "Player"){
                //GameManager.OnPlayerDied();
        }

        if(name =="Ship"){
            GameManager.OnShipDestoryed();
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
