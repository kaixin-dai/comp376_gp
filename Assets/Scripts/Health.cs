using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    int mMaxHealth;

    int mCurrentHealth;
    
    GameObject DamagePrompt;

    
    Text DamagePromptText;

    public GameObject EssenceReference;
    public GameObject PowerUpReference;
    public GameObject HealthReference;
    // Start is called before the first frame update

    void Start(){
        mCurrentHealth = mMaxHealth;

        DamagePrompt = GameObject.Find("Damage Prompt");
        DamagePromptText = DamagePrompt.GetComponent<Text>();
    }

    public void TakeDamage(int damage){
        mCurrentHealth -= damage;
        print(gameObject.name + " health:" + mCurrentHealth);
        DamagePromptText.text = " - " + damage;
        GameManager.OnTakeDamage();

        if(mCurrentHealth <= 0)
        {
                Die();


        }
    }

    private void Die(){
        if(tag == "Player"){

            print("PLayer is dead");
            GameManager.OnPlayerDied();
        }

        if(name =="Ship"){
            GameManager.OnShipDestoryed();
        }

        if(gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            print("enmey died");
            DropResources();
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

    public void DropResources()
    {
        float selector = Random.Range(0.0f,10.0f);
        if(selector <= 3.0f)
        {
            DropPowerUp();
        }

        if(selector >3.0f && selector <= 6.0f)
        {
            DropHealth();
        }

        if(selector > 6.0f && selector <=10.0f)
        {
            DropEssence();
        }

    }

    void DropPowerUp()
    {
        Instantiate(PowerUpReference, transform.position, Quaternion.identity);
    }

    void DropHealth()
    {
        Instantiate(HealthReference, transform.position, Quaternion.identity);
    }

    void DropEssence()
    {
        Instantiate(EssenceReference, transform.position, Quaternion.identity);
    }
}
